using Assets.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Handlers.Dashboard
{
    public class NotesButtonHandler : MonoBehaviour
    {
        public void NotesBtnClicked()
        {
            SceneManager.LoadScene(PagesConstants.ARPage);
        }
    }
}

