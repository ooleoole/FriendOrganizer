using System.Collections.Generic;
using FriendOrganizer.Model;

namespace FriendOrganizer.Data.ViewModel
{
    public class FriendDataService : IFriendDataService
    {
        public IEnumerable<Friend> GetAll()
        {
            yield return new Friend { FirstName = "Calle", LastName = "Anka" };
            yield return new Friend { FirstName = "Arne", LastName = "Anka" };
            yield return new Friend { FirstName = "Kajsa", LastName = "Anka" };
            yield return new Friend { FirstName = "Jocke", LastName = "Anka" };
        }
    }
}
