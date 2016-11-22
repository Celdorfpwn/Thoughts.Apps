using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfClient.Models;
using WpfClient.ViewModels;

namespace WpfClient.Views
{
    /// <summary>
    /// Interaction logic for Contacts.xaml
    /// </summary>
    public partial class Contacts : UserControl
    {

        ContactsViewModel _viewModel { get; set; }

        public Contacts()
        {
            _viewModel = new ContactsViewModel();
            InitializeComponent();
            this.DataContext = _viewModel;
            this.Loaded += Contacts_Loaded;
        }

        private async void Contacts_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.RequestContacts();
        }

        private async void Chat_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var contactModel = button.DataContext as ContactModel;
            await _viewModel.SendChatRequest(contactModel);
        }
    }
}
