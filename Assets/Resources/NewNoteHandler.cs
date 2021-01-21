﻿using Assets.Common;
using Assets.Constants;
using Assets.Models.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using StateManager = Assets.Common.StateManager;

public class NewNoteHandler : MonoBehaviour
{
    void Start()
    {

    }

    public void AddNewNoteBtnClicked()
    {
        InputField inputFieldTitle = GameObject.Find("InputFieldTitle").GetComponent<InputField>();
        string title = inputFieldTitle.text;

        InputField myInputField = GameObject.Find("InputFieldNote").GetComponent<InputField>();
        string note = myInputField.text;

        var req = new AddNoteRequest
        {
            Title = title,
            FormattedText = note,
            ImageTargetId = VuforiaAR.tb.name
        };

        var res = StateManager.HttpServiceClient.PostAsync<Guid>(FridgeNotesEndpoints.AddNote, req);
        myInputField.text = "";
        inputFieldTitle.text = "";

        //todo go to allnotes page?
    }
}
