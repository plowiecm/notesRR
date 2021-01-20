using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Common
{
    public class AppState : INotifyPropertyChanged
    {
        [JsonIgnore]
        public readonly string AppStateStorageFilePath = $"{Application.persistentDataPath}/appstate.data";

        public string Username { get; set; }

        private string _token;
        public string Token
        {
            get
            {
                return _token;
            }
            set
            {
                if (_token != value)
                {
                    _token = value;
                    OnPropertyChanged(nameof(Token));
                }
            }
        }
        public DateTime? TokenExpiration { get; set; }


        /// <summary>
        /// this should only upon TOKEN prop change 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
