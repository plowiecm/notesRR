using Assets.Common;
using Assets.Constants;
using DanielLochner.Assets.SimpleScrollSnap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using StateManager = Assets.Common.StateManager;

public class NotesPanel : MonoBehaviour
{
    public GameObject itemTemplate;

    public GameObject panel, toggle;
    public SimpleScrollSnap sss;


    private float toggleWidth;

    private void Awake()
    {
        toggleWidth = toggle.GetComponent<RectTransform>().sizeDelta.x * (Screen.width / 2048f); ;
    }


    void OnEnable()
    {
        LoadNotes();
    }

    private async void LoadNotes()
    {
        List<Note> notes = new List<Note>();

        try
        {
            List<Note> notesForUser = await StateManager.HttpServiceClient.GetAsync<List<Note>>(FridgeNotesEndpoints.GetNotesForUser);
            notes.AddRange(notesForUser.Where(note => note.ImageTargetId.Equals(VuforiaAR.tb.name)).ToList());
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }

        notes.ForEach(note =>
        {
            Add(note.Id, note.FormattedText);
        });
    }

    private void Add(string id, string formattedText)
    {
        //Pagination
        Instantiate(toggle, sss.pagination.transform.position + new Vector3(toggleWidth * (sss.NumberOfPanels + 1), 0, 0), Quaternion.identity, sss.pagination.transform);
        sss.pagination.transform.position -= new Vector3(toggleWidth / 2f, 0, 0);

        //Panel
        panel.GetComponentInChildren<Text>().text = formattedText;
        panel.GetComponent<Panel>().id = id;
        sss.Add(panel, 0);
    }

    public async void DeleteBtnClicked()
    {
        string noteIdToDelete = sss.Panels[sss.CurrentPanel].GetComponent<Panel>().id;

        try
        {
            string endpoint = string.Format(FridgeNotesEndpoints.DeleteNote, noteIdToDelete);
            await StateManager.HttpServiceClient.DeleteAsync<HttpResponseMessage>(endpoint);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }

        removeNote(sss.CurrentPanel);
    }

    void OnDisable()
    {
        removeAllNotes();
    }

    private void removeAllNotes()
    {
        int panels = sss.NumberOfPanels - 1;
        for (int i = panels; i >= 0; i--)
        {
            removeNote(i);
        }
    }

    private void removeNote(int i)
    {
        //Pagination
        DestroyImmediate(sss.pagination.transform.GetChild(sss.NumberOfPanels - 1).gameObject);
        sss.pagination.transform.position += new Vector3(toggleWidth / 2f, 0, 0);

        //Panel
        sss.Remove(i);
    }
}
