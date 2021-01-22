using Assets.Common;
using Assets.Constants;
using Assets.Handlers.Login;
using Assets.Models;
using Assets.Models.Requests;
using Assets.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using StateManager = Assets.Common.StateManager;

public class NewNoteHandler : MonoBehaviour, IDisplayMessage
{
    [SerializeField]
    private GameObject _popupPrefab;

    public GameObject PopupPrefab { get => _popupPrefab; set => _popupPrefab = value; }

    [SerializeField]
    private GameObject _canvasTarget;

    public GameObject CanvasTarget { get => _canvasTarget; set => _canvasTarget = value; }

    public async void AddNewNoteBtnClicked()
    {
        var inputFieldTitle = GameObject.Find("InputFieldTitle").GetComponent<InputField>();
        var title = inputFieldTitle.text;

        var myInputField = GameObject.Find("InputFieldNote").GetComponent<TMP_InputField>();
        var note = myInputField.text;

        try
        {
            var uri = string.Format(FridgeNotesEndpoints.GetImageTarget, VuforiaAR.tb.InstanceId);
            var getImageTarget = await StateManager.HttpServiceClient.GetAsync<ImageDto>(uri);

            var req = new AddNoteRequest
            {
                Title = title,
                FormattedText = note,
                ImageTargetId = getImageTarget.TargetId.ToString()
            };

            var res = await StateManager.HttpServiceClient.PostAsync<Guid>(FridgeNotesEndpoints.Note, req);
            myInputField.text = "";
            inputFieldTitle.text = "";

            //todo go to allnotes page?
        }
        catch (Exception ex)
        {
            var uiHandler = new UIHandler(PopupPrefab, CanvasTarget);
            uiHandler.DisplayPopup(ex.Message);

            Debug.LogError(ex);
        }

    }
}
