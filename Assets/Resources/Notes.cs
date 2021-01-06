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
        TMPro.TMP_InputField myInputField = GameObject.Find("InputFieldMakeNewNote").GetComponent<TMPro.TMP_InputField>();
        string textOfField = myInputField.text;

        IEnumerable<Vuforia.TrackableBehaviour> tbs = Vuforia.TrackerManager.Instance.GetStateManager().GetActiveTrackableBehaviours();
        var tb = tbs.FirstOrDefault();

        if (tb != null && !string.IsNullOrEmpty(textOfField))
        {
            Debug.Log("Note for imageTarget: " + tb.name + " added: " + textOfField);
            //todomp send note by api
        }
        myInputField.text = "";
    }
}
