using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Models.Requests;
using System;
using Assets.Common;
using Assets.Constants;
using Assets.Handlers.Login;

namespace Assets.Handlers.Register
{
    public class RegisterButtonHandler : MonoBehaviour
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

}