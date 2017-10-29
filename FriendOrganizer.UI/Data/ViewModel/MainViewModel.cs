using System.Collections.ObjectModel;
using System.Linq;
using FriendOrganizer.Data;
using FriendOrganizer.Model;

namespace FriendOrganizer.Data.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        private readonly IFriendDataService _friendDataService;
        private Friend _selectedFriend;

        public ObservableCollection<Friend> Friends { get; set; }
        public Friend SelectedFriend
        {
            get { return _selectedFriend; }
            set
            {
                _selectedFriend = value; 
                OnPropertyChanged();
            }
        }


        public MainViewModel(IFriendDataService friendDataService)
        {
            Friends = new ObservableCollection<Friend>();
            _friendDataService = friendDataService;
        }

        public void Load()
        {
            var friends = _friendDataService.GetAll();
            Friends.Clear();
            foreach (var friend in friends)
                Friends.Add(friend);
        }


        
    }
}
