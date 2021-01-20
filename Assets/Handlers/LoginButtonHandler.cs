using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Constants;
using Assets.Common;
using Assets.Models.Requests;
using Assets.Models.Responses;
using System;

namespace Assets.Handlers
{
    public class LoginButtonHandler : MonoBehaviour
    {
        public InputField login;
        public InputField password;
        public async void LoginBtnClicked()
        {
            var req = new TokenRequest
            {
                Username = "TestAccount",
                Password = "Abc_pow!"
            };

            try
            {
                var token = await StateManager.HttpServiceClient.PostAsync<TokenResponse>(FridgeNotesEndpoints.AuthToken, req);

                var appState = new AppState
                {
                    Token = token.AccessToken,
                    TokenExpiration = DateTime.Now.AddSeconds(token.ExpiresIn),
                    Username = req.Username
                };

                StateManager.InitState(appState);


                var req1 = new AddNoteRequest
                {
                    Title = "TEST",
                    FormattedText = "Some formatted text"
                };

                var res = await StateManager.HttpServiceClient.PostAsync<Guid>(FridgeNotesEndpoints.AddNote, req1);

                //SceneManager.LoadScene(PagesConstants.ARPage);
            }
            catch(Exception ex)
            {
                //display error or something
                Debug.LogError(ex);
            }
        }
    }
}
