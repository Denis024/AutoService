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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для PosPage.xaml
    /// </summary>
    public partial class PosPage : Page
    {
        АМ_ИгишевEntities context = new АМ_ИгишевEntities();
        private Client myclient;
        public PosPage(Client client)
        {
            InitializeComponent();

            myclient = client;
            if (client.ClientService.Count == 0)
            {
                MessageBox.Show("Нет посещений у выбранного клиента");
            }
            else
            {
                var table = context.ClientService.Select(a =>
                new
                {
                    id = a.ID,
                    clientid = a.ClientID,
                    serv = a.Service.Title,
                    data = (DateTime?)a.StartTime,
                    col = a.DocumentByService.Count()
                })
                    .Where(s => s.clientid == myclient.ID)
                    .ToList();

                lv.ItemsSource = table;
            }            
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.myFrame.Navigate(new ClientPage());
        }
    }
}
