using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.Models;

namespace WpfClient.ViewModels
{
    public class UserViewModel
    {
        public UserLocal User { get; private set; }

        public UserViewModel(UserLocal user)
        {
            User = user;
        }
    }
}
