using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FriendOrganizer.DataAccess;
using FriendOrganizer.Model;

namespace FriendOrganizer.Data
{
    public class FriendDataService : IFriendDataService
    {
        private readonly Func<FriendOrganizerDbContext> _contextCreater;

        public FriendDataService(Func<FriendOrganizerDbContext> contextCreater)
        {
            _contextCreater = contextCreater;
        }

        public async Task<List<Friend>> GetAllAsync()
        {
            using (var ctx = _contextCreater())
            {
                return await ctx.Friends.AsNoTracking().ToListAsync();
            }
        }
    }
}
