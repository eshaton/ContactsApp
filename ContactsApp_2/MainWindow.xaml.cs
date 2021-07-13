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
using ContactsApp_2.Classes;
using SQLite;

namespace ContactsApp_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // stvara se lista tipa Contacts
        List<Contact> contactsList;

        public MainWindow()
        {
            InitializeComponent();

            // stvaranje objekta u konstruktoru
            contactsList = new List<Contact>();
            readFromDatabase();
        }

        void readFromDatabase()
        {
            // stvara bazu podatak u MyDocument mapu na disku i stvara lisu koju onda ispisuje u
            // ListWiev u MainWindow prozoru
            using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>();

                // u listu se sepremaju vrijednosti iz baze
                contactsList = connection.Table<Contact>().ToList();
            }

            // popunjavanje contactListView liste glavnog prozora

            //if (contactsList != null)
            //{
            //    foreach (var c in contactsList)
            //    {
            //        contactsListView.Items.Add(new ListViewItem()
            //        {
            //            Content = c
            //        });
            //    }
            //}

            if (contactsList != null)
            {
                // povezivanje contactListView liste sa elementima contactListe
                // kasnije se u xaml-u bindaju za pojedini properti (name, email, phone)
                contactsListView.ItemsSource = contactsList;
            }
        }

        private void addNewButton_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog();

            readFromDatabase();
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filteredList = contactsList.Where(c => c.Name.ToLower().Contains(searchTextBox.Text)).ToList();
            contactsListView.ItemsSource = filteredList;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Contact selectedContact = (Contact)contactsListView.SelectedItem;

            if (selectedContact != null)
            {
                // mesage box za potvrdu brisanja rekorda
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(App.databasePath))
                    {
                        connection.CreateTable<Contact>();
                        connection.Delete(selectedContact);
                        readFromDatabase();
                    }
                }
                
            }
            
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            // stvaranje objekta selected contact koji uzima selektirani kontakt u listi
            Contact selectedContact = (Contact)contactsListView.SelectedItem;

            // ako je kontakt selektiran
            if (selectedContact != null)
            {
                // napravi instancu UpdateContactWindow,
                // pošalji u njegov konstruktor selektirani kontakt
                UpdateContactWindow updateContactWindow = new UpdateContactWindow(selectedContact);

                // otvori prozor
                updateContactWindow.ShowDialog();

                // kada se updateOCntactWindow zatvori učitati će se novo stanje baze
                readFromDatabase();
            }   
        }
    }
}
