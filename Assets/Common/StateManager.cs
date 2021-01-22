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

            SceneManager.sceneLoaded += SceneLoaded;

            try
            {
                LoadAppState();
                SceneManager.LoadScene(PagesConstants.DashboardPage);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                SceneManager.LoadScene(PagesConstants.LoginPage);
            }
        }

        private void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            CurrentAppState.ScenesStack.Push(scene.name);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                var currentPage = CurrentAppState.ScenesStack.Peek();
                CurrentAppState.ScenesStack.Pop();

                if (CurrentAppState.ScenesStack.Peek() == PagesConstants.LoadingPage || 
                    (currentPage != PagesConstants.RegisterPage && CurrentAppState.ScenesStack.Peek() == PagesConstants.LoginPage))
                    Application.Quit();
                else
                    SceneManager.LoadScene(CurrentAppState.ScenesStack.Peek());
            }
        }

        private void PersistDataOnTokenUpdate(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            => PersistData();

        private void LoadAppState()
        {
            var serialized = File.ReadAllText(CurrentAppState.AppStateStorageFilePath);

            var deserialized = JsonConvert.DeserializeObject<AppState>(serialized);

            if (deserialized.TokenExpiration < DateTime.Now)
                throw new Exception("Token expired");

            InitState(deserialized);
        }

        public static void InitState(AppState appStateToInit)
        {
            CurrentAppState.TokenExpiration = appStateToInit.TokenExpiration;
            CurrentAppState.Username = appStateToInit.Username;
            CurrentAppState.Token = appStateToInit.Token;
        }

        private void PersistData()
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
