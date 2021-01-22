﻿using Assets.Common;
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
            friends.Add(new UserDto()
            {
                Username = "MP MP"
            });


            friends.Add(new UserDto()
            {
                Username = "MP MP22"
            });
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
            groups.Add(new GroupDto()
            {
                Name = "AAA"
            });


            groups.Add(new GroupDto()
            {
                Name = "BBBBBBB"
            });
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
        //todo
    }

    public async void ShareWithGroupBtnClicked()
    {
        //todo
    }

    void OnDisable()
    {
        removeAllUsersAndGroupsFromView();
    }

    private void removeAllUsersAndGroupsFromView()
    {
        //todo
    }
}
