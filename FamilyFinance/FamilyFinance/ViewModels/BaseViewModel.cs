using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyFinance.ViewModels
{
    public class BaseViewModel
    {
        protected APIClient apiClient;

        public BaseViewModel()
        {
            apiClient = new APIClient();
        }
    }
}
