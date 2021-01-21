using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Constants;
using Assets.Common;
using Assets.Models.Requests;
using Assets.Models.Responses;
using System;

namespace Assets.Handlers.Login
{
    public interface IDisplayMessage
    {
        GameObject PopupPrefab { get; set; }
        GameObject CanvasTarget { get; set; }
    }

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
                var token = await StateManager.HttpServiceClient.PostAsync<TokenResponse>(FridgeNotesEndpoints.AuthToken, request);

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
                //display error or something

                var instantiatedPopup = Instantiate(PopupPrefab);
                instantiatedPopup.transform.SetParent(CanvasTarget.transform);

                var popupHandler = instantiatedPopup.GetComponent<PopupHandler>();
                popupHandler.SetMessage(ex.Message);

                var rect = instantiatedPopup.GetComponent<RectTransform>();
                rect.SetLeft(0);
                rect.SetRight(0);
                rect.SetTop(0);
                rect.SetBottom(0);


                Debug.LogError(ex);
            }
        }

    }



    public static class RectTransformExtensions
    {
        public static void SetLeft(this RectTransform rt, float left)
        {
            rt.offsetMin = new Vector2(left, rt.offsetMin.y);
        }

        public static void SetRight(this RectTransform rt, float right)
        {
            rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
        }

        public static void SetTop(this RectTransform rt, float top)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
        }

        public static void SetBottom(this RectTransform rt, float bottom)
        {
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
        }
    }

}
