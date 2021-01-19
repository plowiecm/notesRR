using Assets.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Notes : MonoBehaviour
{
    private NotesAPIClient notesAPIClient;
    // Start is called before the first frame update
    void Start()
    {
        notesAPIClient = new NotesAPIClient();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddNewNote()
    {
        TMPro.TMP_InputField myInputField = GameObject.Find("InputFieldNote").GetComponent<TMPro.TMP_InputField>();
        string note = myInputField.text;

        TMPro.TMP_InputField inputFieldTitle = GameObject.Find("InputFieldTitle").GetComponent<TMPro.TMP_InputField>();
        string title = inputFieldTitle.text;

      //  IEnumerable<Vuforia.TrackableBehaviour> tbs = Vuforia.TrackerManager.Instance.GetStateManager().GetActiveTrackableBehaviours();
      //  var tb = tbs.FirstOrDefault();

        /*if (tb != null && !string.IsNullOrEmpty(title))
        {*/
           // Debug.Log("Note for imageTarget: " + tb.name + " added: " + title);
            notesAPIClient.CreateNote(new Note(title, note, "0cb9474a-aeb1-4c82-a02b-a42ea0c6acc6")); //todo id of current user
       // }
        myInputField.text = "";
    }
}
