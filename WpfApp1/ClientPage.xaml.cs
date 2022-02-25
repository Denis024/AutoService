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
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        PageList PageList;
        static АМ_ИгишевEntities context = new АМ_ИгишевEntities();
        List<Client> sort = context.Client.ToList();     

        public ClientPage()
        {
            InitializeComponent();
            mytable.ItemsSource = АМ_ИгишевEntities.GetContext().Client.ToList();          
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.myFrame.Navigate(new AddClientPage(0));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            ii.DataContext = mytable.SelectedItem;
            Client editclient = context.Client.Find(int.Parse(ii.Text));
            Manager.myFrame.Navigate(new AddClientPage(int.Parse(ii.Text)));           
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                АМ_ИгишевEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                mytable.ItemsSource = АМ_ИгишевEntities.GetContext().Client.ToList();
            }
        }

        private void Update()
        {
            if (x2.IsChecked.Value)
            {
                List<Client> clienttable = context.Client.Where(c => c.Gender.Code == "1").ToList();
                List<Client> filtred = clienttable.Where(a => a.FirstName.StartsWith(TextName.Text)
                || a.Email.StartsWith(TextName.Text)
                || a.Phone.StartsWith(TextName.Text)
                || a.LastName.StartsWith(TextName.Text)
                || a.Patronymic.StartsWith(TextName.Text))
                    .ToList();

                var filtered = clienttable.Where(a => a.FirstName.StartsWith(TextName.Text)
                || a.Email.StartsWith(TextName.Text)
                || a.Phone.StartsWith(TextName.Text)
                || a.LastName.StartsWith(TextName.Text)
                || a.Patronymic.StartsWith(TextName.Text));

                sort = filtered.ToList();
                switch (cd.SelectedIndex)
                {
                    case 0: { sort = filtered.OrderBy(s => s.FirstName).ToList(); break; }
                    case 1: { sort = filtered.OrderBy(s => s.RegistrationDate).ToList(); break; }
                    case 2: { sort = filtered.OrderByDescending(s => s.ClientService.Count).ToList(); break; }
                    case 3: { sort = filtered.ToList(); break; }
                }

                PageList = new PageList(sort);
                mytable.ItemsSource = PageList.OffsetClients;
            }
            else
                if (x3.IsChecked.Value)
            {
                List<Client> clienttable = context.Client.Where(c => c.Gender.Code == "2").ToList();
                List<Client> filtred = clienttable.Where(a => a.FirstName.StartsWith(TextName.Text)
                || a.Email.StartsWith(TextName.Text)
                || a.Phone.StartsWith(TextName.Text)
                || a.LastName.StartsWith(TextName.Text)
                || a.Patronymic.StartsWith(TextName.Text))
                    .ToList();

                var filtered = clienttable.Where(a => a.FirstName.StartsWith(TextName.Text)
                || a.Email.StartsWith(TextName.Text)
                || a.Phone.StartsWith(TextName.Text)
                || a.LastName.StartsWith(TextName.Text)
                || a.Patronymic.StartsWith(TextName.Text));

                sort = filtered.ToList();
                switch (cd.SelectedIndex)
                {
                    case 0: { sort = filtered.OrderBy(s => s.FirstName).ToList(); break; }
                    case 1: { sort = filtered.OrderBy(s => s.RegistrationDate).ToList(); break; }
                    case 2: { sort = filtered.OrderByDescending(s => s.ClientService.Count).ToList(); break; }
                    case 3: { sort = filtered.ToList(); break; }
                }

                PageList = new PageList(sort);
                mytable.ItemsSource = PageList.OffsetClients;
            }
            else
            {
                List<Client> clienttable = context.Client.ToList();
                List<Client> filtred = clienttable.Where(a => a.FirstName.StartsWith(TextName.Text)
                || a.Email.StartsWith(TextName.Text)
                || a.Phone.StartsWith(TextName.Text)
                || a.LastName.StartsWith(TextName.Text)
                || a.Patronymic.StartsWith(TextName.Text))
                    .ToList();

                var filtered = clienttable.Where(a => a.FirstName.StartsWith(TextName.Text)
                || a.Email.StartsWith(TextName.Text)
                || a.Phone.StartsWith(TextName.Text)
                || a.LastName.StartsWith(TextName.Text)
                || a.Patronymic.StartsWith(TextName.Text));

                sort = filtered.ToList();
                switch (cd.SelectedIndex)
                {
                    case 0: { sort = filtered.OrderBy(s => s.FirstName).ToList(); break; }
                    case 1: { sort = filtered.OrderBy(s => s.RegistrationDate).ToList(); break; }
                    case 2: { sort = filtered.OrderByDescending(s => s.ClientService.Count).ToList(); break; }
                    case 3: { sort = filtered.ToList(); break; }
                }

                PageList = new PageList(sort);
                mytable.ItemsSource = PageList.OffsetClients;
            }

            int k = context.Client.Count();
            int n = sort.Count();
            col.Text = "Количество записей: " + n.ToString() + " Всего записей: " + k.ToString();       
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Update();            

            int n = DateTime.Now.Month;
            var sbd = context.Client.Where
                (r => r.Birthday.Value.Month == n).ToList();
            lb1.ItemsSource = sbd;

            PageList = new PageList(context.Client.ToList());
            rb1.IsChecked = true;        
        }

        private void x2_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void cd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void TextName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void BtnPos_Click(object sender, RoutedEventArgs e)
        {
            ii.DataContext = mytable.SelectedItem;
            int i = int.Parse(ii.Text);
            Manager.myFrame.Navigate(new PosPage(context.Client.Find(i)));
        }

        private void LastPageButton_Click(object sender, RoutedEventArgs e)
        {
            PageList.CurrentPage--;
            mytable.ItemsSource = PageList.OffsetClients;
            LastPageButton.IsEnabled = PageList.IsFirstPage;
            NextPageButton.IsEnabled = PageList.IsLastPage;
        }

        private void ChangeOffset_Click(object sender, RoutedEventArgs e)
        {
            PageList.CountInPage = Convert.ToInt32((sender as RadioButton).Content);
            mytable.ItemsSource = PageList.OffsetClients;
            LastPageButton.IsEnabled = PageList.IsFirstPage;
            NextPageButton.IsEnabled = PageList.IsLastPage;
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            PageList.CurrentPage++;
            mytable.ItemsSource = PageList.OffsetClients;
            LastPageButton.IsEnabled = PageList.IsFirstPage;
            NextPageButton.IsEnabled = PageList.IsLastPage;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            ii.DataContext = mytable.SelectedItem;
            int i = int.Parse(ii.Text);
            Client delclient = context.Client.Find(i);
            if (delclient.ClientService.Count != 0)
            {
                MessageBox.Show("Невозможно удалить клиента, так как есть связанные данные.");
                return;
            }
            if (MessageBox.Show("Вы действительно хотите удалить клиента?", "Удаление",
                MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            context.Client.Remove(delclient);
            context.SaveChanges();
            PageList.Clients.Remove(delclient);
            mytable.ItemsSource = PageList.OffsetClients;
            MessageBox.Show("Клиент успешно удален");
        }       
    }
}
