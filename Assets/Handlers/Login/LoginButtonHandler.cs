using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Constants;
using Assets.Common;
using Assets.Models.Requests;
using Assets.Models.Responses;
using System;
using Assets.UI;

namespace Assets.Handlers.Login
{
    public class LoginButtonHandler : MonoBehaviour, IDisplayMessage
    {
        public InputField login;
        public InputField password;

        [SerializeField]
        private GameObject _popupPrefab;

        public GameObject PopupPrefab { get => _popupPrefab; set => _popupPrefab = value; }

        [SerializeField]
        private GameObject _canvasTarget;

        public GameObject CanvasTarget { get => _canvasTarget; set => _canvasTarget = value; }

        public async void LoginBtnClicked()
        {
            var request = new TokenRequest
            {
                Username = login.text,
                Password = password.text
            };

            try
            {
                TokenResponse token = await StateManager.HttpServiceClient.PostAsync<TokenResponse>(FridgeNotesEndpoints.AuthToken, request);

                var appState = new AppState
                {
                    Token = token.AccessToken,
                    TokenExpiration = DateTime.Now.AddSeconds(token.ExpiresIn),
                    Username = request.Username
                };

                StateManager.InitState(appState);

                SceneManager.LoadScene(PagesConstants.DashboardPage);
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
