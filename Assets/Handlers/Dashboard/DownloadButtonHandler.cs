using Assets.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownloadButtonHandler : MonoBehaviour
{
    public void OpenUrl()
    {
        Application.OpenURL(FridgeNotesEndpoints.DownloadImages);
    }
}
