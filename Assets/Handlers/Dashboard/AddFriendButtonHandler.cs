using Assets.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Handlers.Dashboard
{
    public class AddFriendButtonHandler : MonoBehaviour
    {
        public void AddFriendBtnClicked()
        {
            SceneManager.LoadScene(PagesConstants.ARPage);
        }
    }
}

