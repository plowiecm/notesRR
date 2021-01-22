using Assets.Common;
using Assets.Constants;
using Assets.Models;
using DanielLochner.Assets.SimpleScrollSnap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using StateManager = Assets.Common.StateManager;

public class SharePanel : MonoBehaviour
{
    public static Guid NoteId;
    public GameObject panel, toggle1, toggle2;
    public SimpleScrollSnap usersSss;

    public SimpleScrollSnap groupsSss;


    private float toggleWidth;

    private void Awake()
    {
        toggleWidth = toggle1.GetComponent<RectTransform>().sizeDelta.x * (Screen.width / 2048f);
    }


    void OnEnable()
    {
        LoadUsers();
        LoadGroups();
    }

    private async void LoadUsers()
    {
        List<UserDto> friends = new List<UserDto>();

        try
        {
            List<UserDto> friendsForUser = await StateManager.HttpServiceClient.GetAsync<List<UserDto>>(FridgeNotesEndpoints.GetFriends);
            friends.AddRange(friendsForUser);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }

        friends.ForEach(friend =>
        {
            AddToScrollView(friend.Username, friend.Id.ToString(), usersSss, toggle1);
        });
    }

    private async void LoadGroups()
    {
        List<GroupDto> groups = new List<GroupDto>();

        try
        {
            List<GroupDto> groupsForUser = await StateManager.HttpServiceClient.GetAsync<List<GroupDto>>(FridgeNotesEndpoints.GetGroups);
            groups.AddRange(groupsForUser);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }

        groups.ForEach(group =>
        {
            AddToScrollView(group.Name, group.Id.ToString(), groupsSss, toggle2);
        });
    }

    private void AddToScrollView(string name, string Id, SimpleScrollSnap sss, GameObject toggle)
    {
        //Pagination
        Instantiate(toggle, sss.pagination.transform.position + new Vector3(toggleWidth * (sss.NumberOfPanels + 1), 0, 0), Quaternion.identity, sss.pagination.transform);
        sss.pagination.transform.position -= new Vector3(toggleWidth / 2f, 0, 0);

        //Panel
        var textObject = panel.GetComponentInChildren<TextMeshProUGUI>();
        textObject.text = name;

        panel.GetComponent<Panel>().id = Id;
        sss.Add(panel, 0);
    }

    public async void ShareWithUserBtnClicked()
    {
        try
        {
            string userId = usersSss.Panels[usersSss.CurrentPanel].GetComponent<Panel>().id;
            string uri = string.Format(FridgeNotesEndpoints.ShareNoteWithUser, NoteId, userId);
            await StateManager.HttpServiceClient.PutAsync<HttpResponseMessage>(uri);

            removeFromView(usersSss, usersSss.CurrentPanel);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    public async void ShareWithGroupBtnClicked()
    {
        try
        {
            string groupId = groupsSss.Panels[groupsSss.CurrentPanel].GetComponent<Panel>().id;
            string uri = string.Format(FridgeNotesEndpoints.ShareNoteWithGroup, NoteId, groupId);
            await StateManager.HttpServiceClient.PutAsync<HttpResponseMessage>(uri);

            removeFromView(groupsSss, groupsSss.CurrentPanel);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    void OnDisable()
    {
        removeAllUsersAndGroupsFromView();
    }

    private void removeAllUsersAndGroupsFromView()
    {
        removeAllFromView(usersSss);
        removeAllFromView(groupsSss);
    }

    private void removeAllFromView(SimpleScrollSnap sss)
    {
        int panels = sss.NumberOfPanels - 1;
        for (int i = panels; i >= 0; i--)
        {
            removeFromView(sss, i);
        }
    }

    private void removeFromView(SimpleScrollSnap sss, int index)
    {
        //Pagination
        DestroyImmediate(sss.pagination.transform.GetChild(sss.NumberOfPanels - 1).gameObject);
        sss.pagination.transform.position += new Vector3(toggleWidth / 2f, 0, 0);

        //Panel
        sss.Remove(index);
    }
}
