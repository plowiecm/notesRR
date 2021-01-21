using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Models.Requests;
using System;
using Assets.Common;
using Assets.Constants;
using Assets.Handlers.Login;
using Assets.UI;

namespace Assets.Handlers.Register
{
    public class RegisterButtonHandler : MonoBehaviour, IDisplayMessage
    {
        [SerializeField]
        private GameObject _popupPrefab;

        public GameObject PopupPrefab { get => _popupPrefab; set => _popupPrefab = value; }

        [SerializeField]
        private GameObject _canvasTarget;

        public GameObject CanvasTarget { get => _canvasTarget; set => _canvasTarget = value; }

        public InputField Email;
        public InputField Login;
        public InputField Password;

        public async void RegisterMember()
        {
            var request = new CreateUserRequest
            {
                Username = Login.text,
                Password = Password.text,
                Email = Email.text
            };

            try
            {
                await  StateManager.HttpServiceClient.PostAsync<Guid>(FridgeNotesEndpoints.RegisterMember, request);
                SceneManager.LoadScene(PagesConstants.LoginPage);
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