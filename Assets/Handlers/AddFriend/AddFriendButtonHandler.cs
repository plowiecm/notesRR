using Assets.Common;
using Assets.Constants;
using Assets.Handlers.Login;
using Assets.UI;
using System;
using UnityEngine;

namespace Assets.Handlers.AddFriend
{
    public class AddFriendButtonHandler : MonoBehaviour, IDisplayMessage
    {
        [SerializeField]
        private GameObject _popupPrefab;

        public GameObject PopupPrefab { get => _popupPrefab; set => _popupPrefab = value; }

        [SerializeField]
        private GameObject _canvasTarget;

        public GameObject CanvasTarget { get => _canvasTarget; set => _canvasTarget = value; }

        public GameObject DetailsView;

        public async void AddFriend()
        {
            try
            {
                var handler = DetailsView.GetComponent<UserDisplayHandler>().Handler;

                var uri = string.Format(FridgeNotesEndpoints.AddFriend, handler.Users[handler.Index].Id);

                await StateManager.HttpServiceClient.PostAsync(uri);
            }
            catch(Exception ex)
            {
                var uiHandler = new UIHandler(PopupPrefab, CanvasTarget);
                uiHandler.DisplayPopup(ex.Message);

                Debug.LogError(ex);
            }
        }

    }
}
