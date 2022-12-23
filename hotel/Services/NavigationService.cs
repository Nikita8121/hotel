using hotel.Stores;
using hotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotel.Services
{
    public class NavigationService
    {
        private readonly NavigationStore _navigateStore;
        private readonly Func<ViewModelBase> _createViewModel;

        public NavigationService(NavigationStore navigateStore, Func<ViewModelBase> createViewModel)
        {
            _navigateStore = navigateStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigateStore.CurrentViewModel = _createViewModel();
        }
    }
}
