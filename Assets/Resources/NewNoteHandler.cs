using Assets.Common;
using Assets.Constants;
using Assets.Models.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using StateManager = Assets.Common.StateManager;

public class NewNoteHandler : MonoBehaviour
{
    public void AddNewNoteBtnClicked()
    {
        var inputFieldTitle = GameObject.Find("InputFieldTitle").GetComponent<InputField>();
        var title = inputFieldTitle.text;

        var myInputField = GameObject.Find("InputFieldNote").GetComponent<TMP_InputField>();
        var note = myInputField.text;

        var req = new AddNoteRequest
        {
            Title = title,
            FormattedText = note,
            ImageTargetId = VuforiaAR.tb.name
        };

        var res = StateManager.HttpServiceClient.PostAsync<Guid>(FridgeNotesEndpoints.Note, req);
        myInputField.text = "";
        inputFieldTitle.text = "";

        //todo go to allnotes page?
    }
}
