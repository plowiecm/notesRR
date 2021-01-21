using Assets.Common;
using Assets.Constants;
using Assets.Extensions;
using Assets.Handlers.Login;
using Assets.Models.Requests;
using Assets.Models.Responses;
using Assets.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Handlers.AddFriend
{
    public class SearchButtonHandler : MonoBehaviour, IDisplayMessage
    {

        public InputField Username;

        [SerializeField]
        private GameObject _popupPrefab;

        public GameObject PopupPrefab { get => _popupPrefab; set => _popupPrefab = value; }

        [SerializeField]
        private GameObject _canvasTarget;

        public GameObject CanvasTarget { get => _canvasTarget; set => _canvasTarget = value; }

        public GameObject DetailsView;

        public async void SearchBtnClicked()
        {          
            try
            {
                var uri = string.Format(FridgeNotesEndpoints.GetFriend, Username.text, 0);

                var friendList = await StateManager.HttpServiceClient.GetAsync<GetUsersResponse>(uri);
                // SceneManager.LoadScene(PagesConstants.LoginPage);

                if (!friendList.Users.Any())
                    return;

                var handler = DetailsView.GetComponent<UserDisplayHandler>().Handler;
                handler.Users = friendList.Users;
                handler.Index = 0;

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

