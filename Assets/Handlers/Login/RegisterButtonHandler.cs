using Assets.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Handlers.Login
{
    public class RegisterButtonHandler : MonoBehaviour
    {
         public void RegisterBtnClicked()
        {
            SceneManager.LoadScene(PagesConstants.RegisterPage);
        }
    }

}
