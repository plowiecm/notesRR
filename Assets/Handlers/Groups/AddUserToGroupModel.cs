using System;
using UnityEngine;

namespace Assets.Handlers.Groups
{
    public class AddUserToGroupModel : MonoBehaviour
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }

        public void SelectUser()
        {
            AddFriendToGroupHandler.SelectedUserId = UserId;
            AddFriendToGroupHandler.SelectedUsername = Username;
        }
    }
}
