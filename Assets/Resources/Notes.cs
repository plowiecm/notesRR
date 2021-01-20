using Assets.Common;
using Assets.Constants;
using Assets.Models.Requests;
using Assets.Models.Responses;
using Assets.Resources;
using Assets.Web;
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

        var req = new AddNoteRequest
        {
            Title = title,
            FormattedText = note
        };

        var res = StateManager.HttpServiceClient.PostAsync<Guid>(FridgeNotesEndpoints.AuthToken, req);




        //  IEnumerable<Vuforia.TrackableBehaviour> tbs = Vuforia.TrackerManager.Instance.GetStateManager().GetActiveTrackableBehaviours();
        //  var tb = tbs.FirstOrDefault();

        /*if (tb != null && !string.IsNullOrEmpty(title))
        {*/
        // Debug.Log("Note for imageTarget: " + tb.name + " added: " + title);
        //notesAPIClient.CreateNote(new Note(title, note, "0cb9474a-aeb1-4c82-a02b-a42ea0c6acc6")); //todo id of current user
        // }
        myInputField.text = "";
    }
}
