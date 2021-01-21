using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models.Requests
{
    class AddFriendRequest
    {
        public string UserId { get; set; }
        public string FriendId { get; set; }

    }
}
