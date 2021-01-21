using Assets.Extensions;
using Assets.Handlers.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.UI
{
    public class UIHandler : MonoBehaviour
    {
        private readonly GameObject _popupPrefab;
        private readonly GameObject _canvas;

        public UIHandler(GameObject popupPrefab, GameObject canvas)
        {
            _popupPrefab = popupPrefab;
            _canvas = canvas;
        }

        public void DisplayPopup(string message)
        {
            var instantiatedPopup = Instantiate(_popupPrefab);
            instantiatedPopup.transform.SetParent(_canvas.transform);

            var popupHandler = instantiatedPopup.GetComponent<PopupHandler>();
            popupHandler.SetMessage(message);
            popupHandler.BaseObj = instantiatedPopup;

            var rect = instantiatedPopup.GetComponent<RectTransform>();
            rect.SetLeft(0);
            rect.SetRight(0);
            rect.SetTop(0);
            rect.SetBottom(0);
        }
    }
}
