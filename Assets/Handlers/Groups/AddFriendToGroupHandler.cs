using Assets.Common;
using Assets.Constants;
using Assets.Extensions;
using Assets.Handlers.Groups;
using Assets.Handlers.Login;
using Assets.Models;
using Assets.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddFriendToGroupHandler : MonoBehaviour, IDisplayMessage
{
    public GameObject ParentPanel;
    public GameObject Prefab;

    public InputField UsernameInput;

    public static Guid SelectedUserId;
    public static Guid SelectedGroupId;

    private static string _selectedUsername;
    public static string SelectedUsername
    {
        get => _selectedUsername;
        set
        {
            _selectedUsername = value;
            OnPropertyChanged(nameof(SelectedUsername));
        }
    }

    public static event PropertyChangedEventHandler PropertyChanged = delegate { };

    private static void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(typeof(AddFriendToGroupHandler), new PropertyChangedEventArgs(propertyName));
    }

    public static event EventHandler MemberAdded;


    public List<UserDto> Friends { get; private set; }

    [SerializeField]
    private GameObject _popupPrefab;

    public GameObject PopupPrefab { get => _popupPrefab; set => _popupPrefab = value; }

    [SerializeField]
    private GameObject _canvasTarget;

    public GameObject CanvasTarget { get => _canvasTarget; set => _canvasTarget = value; }

    private void Start()
    {
        PropertyChanged += UpdateUi;
    }

    private void UpdateUi(object sender, PropertyChangedEventArgs e)
    {
        UsernameInput.text = SelectedUsername;
    }

    private async void OnEnable()
    {
        var friends = await StateManager.HttpServiceClient.GetAsync<IEnumerable<UserDto>>(FridgeNotesEndpoints.GetFriends);
        Friends = friends.ToList();

        foreach (var friend in friends)
        {
            ParentPanel.DestroyAllChildren();

            var offset = 25;
            foreach (var member in Friends)
            {
                var detail = Instantiate(Prefab);
                detail.transform.SetParent(ParentPanel.transform);
                detail.GetComponentInChildren<TextMeshProUGUI>().text = member.Username;

                var addUserToGroupModel = detail.GetComponent<AddUserToGroupModel>();
                addUserToGroupModel.Username = member.Username;
                addUserToGroupModel.UserId = member.Id;

                var rect = detail.GetComponent<RectTransform>();
                rect.SetLeft(0);
                rect.SetRight(0);
                rect.SetTop(0);
                rect.SetBottom(0);

                rect.localPosition = new Vector3(400, offset);

                rect.sizeDelta = new Vector2(0, rect.sizeDelta.y + 50f);
                offset -= 50;
            }
        }
    }

    public async void ShareNote()
    {
        try
        {
            if (SelectedUserId == Guid.Empty || SelectedGroupId == Guid.Empty)
                throw new Exception("User not selected!");

            var uri = string.Format(FridgeNotesEndpoints.AddMemberToGroup, SelectedGroupId, SelectedUserId);
            await StateManager.HttpServiceClient.PostAsync(uri);

            MemberAdded.Invoke(this, EventArgs.Empty);

            this.gameObject.SetActive(false);
        }
        catch (Exception ex)
        {
            var uiHandler = new UIHandler(PopupPrefab, CanvasTarget);
            uiHandler.DisplayPopup(ex.Message);

            Debug.LogError(ex);
        }
    }
}
