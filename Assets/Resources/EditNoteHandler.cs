using Assets.Common;
using Assets.Constants;
using Assets.Models.Requests;
using System;
using TMPro;
using UnityEngine;

public class EditNoteHandler : MonoBehaviour
{
    public static Guid NoteId;
    public static string CurrentTitle;
    public static string CurrentContent;

    public TMP_InputField Title;
    public TMP_InputField Content;

    private void Start()
    {
        Title.text = CurrentTitle;
        Content.text = CurrentContent;
    }

    public async void EditNote()
    {
        var req = new EditNoteRequest
        {
            NoteId = NoteId,
            Title = Title.text,
            Content = Content.text
        };

        await StateManager.HttpServiceClient.PutAsync(FridgeNotesEndpoints.Note, req);
    }


}
