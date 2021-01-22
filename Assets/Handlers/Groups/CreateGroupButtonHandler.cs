using Assets.Common;
using Assets.Constants;
using Assets.Models.Requests;
using Assets.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Handlers.Groups
{
    public class CreateGroupButtonHandler : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        [SerializeField]
        private GameObject _popupPrefab;

        public GameObject PopupPrefab { get => _popupPrefab; set => _popupPrefab = value; }

        [SerializeField]
        private GameObject _canvasTarget;

        public GameObject CanvasTarget { get => _canvasTarget; set => _canvasTarget = value; }


        public InputField GroupName;

        public GameObject Panel;

     


        public async void CreateGroupBtnClicked()
        {
            var request = new AddGroupRequest
            {
                GroupName = GroupName.text
            };

            try
            {
                await StateManager.HttpServiceClient.PostAsync<Guid>(FridgeNotesEndpoints.AddGroup, request);
                Panel.SetActive(false);

            }
            catch (Exception ex)
            {
                var uiHandler = new UIHandler(PopupPrefab, CanvasTarget);
                uiHandler.DisplayPopup(ex.Message);

                Debug.LogError(ex);
            }
        }
    }

}
