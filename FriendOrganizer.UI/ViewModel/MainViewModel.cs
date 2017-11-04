using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.View.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModel
{
  public class MainViewModel : ViewModelBase
  {
    private readonly IEventAggregator _eventAggregator;
    private readonly Func<IFriendDetailViewModel> _friendDetailViewModelCreator;
    private IDetailViewModel _detailViewModel;
    private readonly IMessageDialogService _messageDialogService;

    public MainViewModel(INavigationViewModel navigationViewModel,
      Func<IFriendDetailViewModel> friendDetailViewModelCreator,
      IEventAggregator eventAggregator,
      IMessageDialogService messageDialogService)
    {
      _eventAggregator = eventAggregator;
      _friendDetailViewModelCreator = friendDetailViewModelCreator;
      _messageDialogService = messageDialogService;

      _eventAggregator.GetEvent<OpenDetailViewEvent>()
       .Subscribe(OnOpenDetailView);
      _eventAggregator.GetEvent<AfterDetailDeletedEvent>()
        .Subscribe(AfterDetailDeleted);

      CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);

      NavigationViewModel = navigationViewModel;
    }

    public async Task LoadAsync()
    {
      await NavigationViewModel.LoadAsync();
    }

    public ICommand CreateNewDetailCommand { get; }

    public INavigationViewModel NavigationViewModel { get; }

    public IDetailViewModel DetailViewModel
    {
      get => _detailViewModel;
        private set
      {
        _detailViewModel = value;
        OnPropertyChanged();
      }
    }

    private async void OnOpenDetailView(OpenDetailViewEventArgs args)
    {
      if (DetailViewModel != null && DetailViewModel.HasChanges)
      {
        var result = _messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?", "Question");
        if (result == MessageDialogResult.Cancel)
        {
          return;
        }
      }

        if (args.ViewModelName == nameof(FriendDetailViewModel))
            DetailViewModel = _friendDetailViewModelCreator();

        if (DetailViewModel != null) await DetailViewModel.LoadAsync(args.Id);
    }

    private void OnCreateNewDetailExecute(Type viewModelType)
    {
      OnOpenDetailView(
        new OpenDetailViewEventArgs { ViewModelName = viewModelType.Name });
    }

    private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
    {
      DetailViewModel = null;
    }
  }
}
