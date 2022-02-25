using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using Microsoft.Win32;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AddClientPage.xaml
    /// </summary>
    public partial class AddClientPage : Page
    {
        private Client myclient = new Client();
        АМ_ИгишевEntities context = new АМ_ИгишевEntities();
        List<CustomTag> customTags;
        List<Tag> tags;

        public AddClientPage(int id)
        {
            InitializeComponent();

            Client editclient = context.Client.Find(id);

            if (editclient != null)
            {
                myclient = editclient;
                if (myclient.GenderCode != "1") wbut.IsChecked = true;
            }

            DataContext = myclient;

            tags = context.Tag.ToList();
            customTags = new List<CustomTag>();
            tags.ForEach(tag => customTags.Add(new CustomTag(tag)));
            customTags.ForEach(tag =>
            {
                if (myclient.Tag.Any(t => t.ID == tag.ID))
                    tag.IsSelected = true;
            });
            TagsDataGrid.ItemsSource = customTags;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.myFrame.GoBack();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            int k = 1;
            if (wbut.IsChecked.Value) k = 2;
            myclient.GenderCode = k.ToString();            

            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(myclient.FirstName))
                errors.AppendLine("Укажите фамилию");
            if (string.IsNullOrWhiteSpace(myclient.LastName))
                errors.AppendLine("Укажите имя");
            if (string.IsNullOrWhiteSpace(myclient.Patronymic))
                errors.AppendLine("Укажите отчество");
            if (myclient.Birthday == null)
                errors.AppendLine("Укажите дату рождения");
            if (myclient.RegistrationDate == null)
                errors.AppendLine("Укажите дату регистрации");            
            if (string.IsNullOrWhiteSpace(myclient.FirstName))
                errors.AppendLine("Укажите Email");
            if (string.IsNullOrWhiteSpace(myclient.FirstName))
                errors.AppendLine("Укажите телефон");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (myclient.ID == 0)
            {
                myclient.Tag.Clear();
                customTags.ForEach(tag =>
                {
                    if (tag.IsSelected)
                        myclient.Tag.Add(context.Tag.Find(tag.ID));
                });
                context.Client.Add(myclient);                
            }
            else
            {
                myclient = context.Client.Find(myclient.ID);
                context.Entry(myclient).State = EntityState.Modified;
            }

            try
            {                
                context.SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Manager.myFrame.Navigate(new ClientPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }            
        }

        private void FirstName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (Int32.TryParse(e.Text, out val) && e.Text != "-")
            {
                e.Handled = true;
            }
        }

        private void BtnPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                FOT.Source = new BitmapImage(new Uri(op.FileName));
            }

            var FileNameToSave = DateTime.Now.ToFileTime() + Path.GetExtension(op.FileName);
            var IMG = Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}Клиенты\\{FileNameToSave}");
            TXImage.Text = IMG;
            TXImage.Focus();
            File.Copy(op.FileName, IMG);           
        }

        private void TXImage_TextChanged(object sender, TextChangedEventArgs e)
        {
            Focus();
        }
    }
}
