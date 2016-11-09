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
        public User User { get; private set; }

        public UserViewModel(User user)
        {
            User = user;
        }
    }
}
