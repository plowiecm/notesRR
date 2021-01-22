using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Constants
{
    public class FridgeNotesEndpoints
    {
        public const string AuthToken = "/auth/token";
        public const string Note = "/api/note";


        /// <summary>
        /// /api/image/name/{targetName}
        /// </summary>
        public const string GetImageTarget = "/api/image/name/{0}";

        /// <summary>
        /// /api/notes/{noteId}
        /// </summary>
        public const string DeleteNote = "api/note/{0}";
        public const string GetNotesForUser = "/api/notes/user";
        public const string RegisterMember = "/api/user";

        /// <summary>
        /// /api/note/{noteId}/share/user/{userId}
        /// </summary>
        public const string ShareNoteWithUser = "/api/note/{0}/share/user/{1}";

        /// <summary>
        /// /api/note/{noteId}/share/group/{groupId}
        /// </summary>
        public const string ShareNoteWithGroup = "/api/note/{0}/share/group/{1}";

        /// <summary>
        /// /api/users/{userName}/{pageNumber}
        /// </summary>
        public const string GetUsers = "/api/users/{0}/{1}";

        public const string GetFriends = "/api/friends";


        /// <summary>
        /// /api/group/{groupId}/member/{memberId}
        /// </summary>
        public const string AddMemberToGroup = "/api/group/{0}/member/{1}";

        /// <summary>
        /// api/friend/{friendId}
        /// </summary>
        public const string AddFriend = "/api/friend/{0}";
        public const string AddGroup = "/api/group";
        public const string GetGroups = "/api/groups/user";
    }
}
