using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Alexandria.LibraryApp.Command;
using Alexandria.LibraryApp.Data;
using Alexandria.LibraryApp.Model;

namespace Alexandria.LibraryApp.ViewModel
{
    public class PatronsViewModel : ViewModelBase

    {
        private readonly IPatronDataProvider _patronDataProvider;
        private PatronItemViewModel? _selectedPatron;
        private readonly ObservableCollection<PatronItemViewModel> _patrons = new();
        private NavigationSide _navigationSide;
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand MoveNavigationCommand { get; set; }

        public DelegateCommand DeleteCommand { get; set; }

        public PatronsViewModel(IPatronDataProvider patronDataProvider)
        {
            _patronDataProvider = patronDataProvider;
            AddCommand = new DelegateCommand(Add);
            MoveNavigationCommand = new DelegateCommand(MoveNavigation);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
        }

        public ObservableCollection<PatronItemViewModel> Patrons => _patrons;

        public PatronItemViewModel? SelectedPatron
        {
            get => _selectedPatron;
            set
            {
                if (Equals(value, _selectedPatron)) return;
                _selectedPatron = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsPatronSelected));
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsPatronSelected => SelectedPatron is not null;

        public NavigationSide NavigationSide
        {
            get => _navigationSide;
            private set
            {
                _navigationSide = value;
                RaisePropertyChanged();
            }
        }

        public override async Task LoadAsync()
        {
            if (Patrons.Any())
            {
                return;
            }

            var patrons = await _patronDataProvider.GetAllAsync();
            if (patrons is not null)
            {
                foreach (var patron in patrons)
                {
                    Patrons.Add(new PatronItemViewModel(patron));
                }
            }
        }

        private void Add(object? parameter)
        {
            var patron = new Patron() { FirstName = "New" };
            var viewModel = new PatronItemViewModel(patron);
            Patrons.Add(viewModel);
            SelectedPatron = viewModel;
        }

        private void MoveNavigation(object? parameter)
        {
            NavigationSide = NavigationSide == NavigationSide.Left 
                ? NavigationSide.Right : NavigationSide.Left;
        }

        private void Delete(object? parameter)
        {
            if (SelectedPatron is not null)
            {
                Patrons.Remove(SelectedPatron);
                SelectedPatron = null;
            }
        }

        private bool CanDelete(object? parameter) => SelectedPatron != null;
    }

    public enum NavigationSide
    {
        Left,
        Right
    }
}
