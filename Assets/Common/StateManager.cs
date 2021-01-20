using Assets.Constants;
using Assets.Web;
using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Common
{
    public class StateManager : MonoBehaviour
    {
        private static IHttpServiceClient _httpServiceClient;
        public static IHttpServiceClient HttpServiceClient
        {
            get
            {
                if (_httpServiceClient == null)
                    _httpServiceClient = new HttpServiceClient();

                return _httpServiceClient;
            }
        }

        private static AppState _appState;
        public static AppState CurrentAppState
        {
            get
            {
                if (_appState == null)
                    _appState = new AppState();

                return _appState;
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);

            CurrentAppState.PropertyChanged += PersistDataOnTokenUpdate;

            try
            {
                LoadAppState();
                SceneManager.LoadScene(PagesConstants.ARPage);
            }catch(Exception ex)
            {
                Debug.Log(ex.Message);
                SceneManager.LoadScene(PagesConstants.LoginPage);
            }
        }

        private void PersistDataOnTokenUpdate(object sender, System.ComponentModel.PropertyChangedEventArgs e) 
            => PersistData();

        private void LoadAppState()
        {
            var serialized = File.ReadAllText(CurrentAppState.AppStateStorageFilePath);

            var deserialized = JsonConvert.DeserializeObject<AppState>(serialized);

            if (deserialized.TokenExpiration < DateTime.Now)    //renew token here
                return;

            InitState(deserialized);
        }

        public static void InitState(AppState appStateToInit)
        {
            CurrentAppState.Token = appStateToInit.Token;
            CurrentAppState.TokenExpiration = appStateToInit.TokenExpiration;
            CurrentAppState.Username = appStateToInit.Username;
        }

        public void PersistData() // might be worth calling on some event 
        {
            try
            {
                var serialized = JsonConvert.SerializeObject(CurrentAppState);

                File.WriteAllText(CurrentAppState.AppStateStorageFilePath, serialized);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }
    }
}
