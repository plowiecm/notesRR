using Assets.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Handlers.Dashboard
{
    public class GroupsButtonHandler : MonoBehaviour
    {
        public void GroupsBtnClicked()
        {
            SceneManager.LoadScene(PagesConstants.GroupsPage);
        }

    }

}
