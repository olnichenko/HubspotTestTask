using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
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
using Newtonsoft.Json;
using ExternalDAL.Api;
using ExternalDAL.Abstract;
using InternalDAL.Services;
using InternalDAL.Models;

namespace ImportHubspotData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IHubspotApi _hubspotApi;
        public MainWindow()
        {
            // Better use Dependency Injection
            _hubspotApi = new HubspotApi();
            InitializeComponent();
        }

        private async void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lstResult.Items.Clear();
                lblError.Content = string.Empty;

                var apiKey = txtApikey.Text;
                var data = await _hubspotApi.GetAllContactsAsync(apiKey);
                var contactsList = new List<Contact>();

                // Better use Automapper
                foreach (var item in data)
                {
                    var contact = new Contact
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Email = item.Email
                    };
                    DateTime birthDate;
                    DateTime.TryParse(item.BirthdayDate, out birthDate);
                    contact.BirthDate = birthDate;
                    contactsList.Add(contact);
                }
                Dictionary<string, string> result;
                using(var contactsService = new ContactsService())
                {
                    result = await contactsService.AddOrUpdateRangeAsync(contactsList);
                }

                foreach (var item in result)
                {
                    lstResult.Items.Add(item.Key + " - " + item.Value);
                }
            }
            catch (Exception ex)
            {
                lblError.Content = "Error! " + ex.Message;
            }
        }
    }    
}
