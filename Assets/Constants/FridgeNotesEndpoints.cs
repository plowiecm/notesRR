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
        public const string AddNote = "/api/note";
        public const string GetNotesForUser = "/api/notes/user";
        public const string RegisterMember = "/api/user";

        /// <summary>
        /// /api/users/{userName}/{pageNumber}
        /// </summary>
        public const string GetFriend = "/api/users/{0}/{1}";
        public const string AddFriend = "/api/friend";
        public const string AddGroup = "/api/group";
    }
}
