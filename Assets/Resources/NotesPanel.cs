using Assets.Common;
using Assets.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using StateManager = Assets.Common.StateManager;

public class NotesPanel : MonoBehaviour
{
    public GameObject itemTemplate;
    public GameObject content;

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
            var copy = Instantiate(itemTemplate);
            //   copy.GetComponentInChildren todo set note text and title
            copy.transform.SetParent(content.transform);
        });
    }

    void OnDisable()
    {
        removeAllNotes();
    }

    private void removeAllNotes()
    {
        int childs = content.transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.Destroy(content.transform.GetChild(i).gameObject);
        }
    }
}
