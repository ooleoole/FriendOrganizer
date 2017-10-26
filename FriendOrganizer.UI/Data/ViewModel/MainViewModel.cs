using System.Collections.ObjectModel;
using FriendOrganizer.Data;
using FriendOrganizer.Model;

namespace schoolappmvvm.Data.ViewModel
{
    public class MainViewModel
    {
        private IFriendDataService _friendDataService;
        public ObservableCollection<Friend> Friends { get; set; }

        public MainViewModel(IFriendDataService friendDataService)
        {
            Friends= new ObservableCollection<Friend>();
            _friendDataService = friendDataService;
        }

        public void Load()
        {
            var friends = _friendDataService.GetAll();
        }


    }
}
