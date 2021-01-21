using UnityEngine;

namespace Assets.Handlers.Login
{
    public interface IDisplayMessage
    {
        GameObject PopupPrefab { get; set; }
        GameObject CanvasTarget { get; set; }
    }
}
