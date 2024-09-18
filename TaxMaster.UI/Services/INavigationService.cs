using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxMaster.UI.Services
{
    public interface INavigationService
    {
        Task NavigateToNextPage();
        Task NavigateToPreviousPage();
        void RegisterNavigationFlow(List<Type> pageSequence);
    }
}
