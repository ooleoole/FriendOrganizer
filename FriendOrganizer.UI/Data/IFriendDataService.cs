using System.Collections.Generic;
using FriendOrganizer.Model;

namespace FriendOrganizer.Data
{
    public interface IFriendDataService
    {
        IEnumerable<Friend> GetAll();
    }
}