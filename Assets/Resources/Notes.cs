using Assets.Common;
using Assets.Constants;
using Assets.Models.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Notes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddNewNote()
    {
        TMPro.TMP_InputField inputFieldTitle = GameObject.Find("InputFieldTitle").GetComponent<TMPro.TMP_InputField>();
        string title = inputFieldTitle.text;

        TMPro.TMP_InputField myInputField = GameObject.Find("InputFieldNote").GetComponent<TMPro.TMP_InputField>();
        string note = myInputField.text;

        IEnumerable<Vuforia.TrackableBehaviour> tbs = Vuforia.TrackerManager.Instance.GetStateManager().GetActiveTrackableBehaviours();
        var tb = tbs.FirstOrDefault();
        var req = new AddNoteRequest
        {
            Title = title,
            FormattedText = note,
            ImageTargetId = tb.name
        };

        var res = StateManager.HttpServiceClient.PostAsync<Guid>(FridgeNotesEndpoints.AuthToken, req);
        myInputField.text = "";
    }
}
