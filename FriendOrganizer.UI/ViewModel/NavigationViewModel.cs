﻿using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Event;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;
using System.Linq;
using FriendOrganizer.UI.Data.Lookups;

namespace FriendOrganizer.UI.ViewModel
{
  public class NavigationViewModel : ViewModelBase, INavigationViewModel
  {
    private readonly IFriendLookupDataService _friendLookupService;
    private readonly IEventAggregator _eventAggregator;

    public NavigationViewModel(IFriendLookupDataService friendLookupService,
      IEventAggregator eventAggregator)
    {
      _friendLookupService = friendLookupService;
      _eventAggregator = eventAggregator;
      Friends = new ObservableCollection<NavigationItemViewModel>();
      _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
      _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
    }



    public async Task LoadAsync()
    {
      var lookup = await _friendLookupService.GetFriendLookupAsync();
      Friends.Clear();
      foreach (var item in lookup)
      {
        Friends.Add(new NavigationItemViewModel(item.Id, item.DisplayMember,
          nameof(FriendDetailViewModel),
          _eventAggregator));
      }
    }

    public ObservableCollection<NavigationItemViewModel> Friends { get; }

    private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
    {
        if (args.ViewModelName != nameof(FriendDetailViewModel)) return;
        var friend = Friends.SingleOrDefault(f => f.Id == args.Id);
        if (friend != null)
        {
            Friends.Remove(friend);
        }
    }

    private void AfterDetailSaved(AfterDetailSavedEventArgs obj)
    {
        if (obj.ViewModelName != nameof(FriendDetailViewModel)) return;
        var lookupItem = Friends.SingleOrDefault(l => l.Id == obj.Id);
        if (lookupItem == null)
        {
            Friends.Add(new NavigationItemViewModel(obj.Id, obj.DisplayMember,
                nameof(FriendDetailViewModel),
                _eventAggregator));
        }
        else
        {
            lookupItem.DisplayMember = obj.DisplayMember;
        }
    }
  }
}
