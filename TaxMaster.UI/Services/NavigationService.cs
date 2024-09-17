using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxMaster.UI.Services
{
    public class NavigationService : INavigationService
    {
        private List<Type> _pages;
        private int _currentPageIndex = 0;

        public void RegisterNavigationFlow(List<Type> pageSequence)
        {
            _pages = pageSequence;
            _currentPageIndex = 0; // Start at the first page
        }

        public async Task NavigateToNextPage()
        {
            if (_currentPageIndex < _pages.Count - 1)
            {
                _currentPageIndex++;
                Type nextPage = _pages[_currentPageIndex];
                await Shell.Current.GoToAsync(nextPage.Name);
            }
        }

        public async Task NavigateToPreviousPage()
        {
            if (_currentPageIndex > 0)
            {
                _currentPageIndex--;
                Type previousPage = _pages[_currentPageIndex];
                await Shell.Current.GoToAsync(previousPage.Name);
            }
        }
    }
}
