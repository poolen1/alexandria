
using System.Threading.Tasks;
using Alexandria.LibraryApp.Command;

namespace Alexandria.LibraryApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        
        private ViewModelBase _selectedViewModel;

        public MainViewModel(PatronsViewModel patronsViewModel,
            ProductsViewModel productsViewModel)
        {
            PatronsViewModel = patronsViewModel;
            ProductsViewModel = productsViewModel;
            SelectedViewModel = PatronsViewModel;
            SelectViewModelCommand = new DelegateCommand(SelectViewModel);
        }

        public ViewModelBase SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                RaisePropertyChanged();
            }
        }
        public PatronsViewModel PatronsViewModel { get; }
        public ProductsViewModel ProductsViewModel { get; }
        public DelegateCommand SelectViewModelCommand { get; }

        public override async Task LoadAsync()
        {
            if (SelectedViewModel is not null)
            {
                await SelectedViewModel.LoadAsync();
            }
        }
        private async void SelectViewModel(object? parameter)
        {
            SelectedViewModel = parameter as ViewModelBase;
            await LoadAsync();
        }
    }
}
