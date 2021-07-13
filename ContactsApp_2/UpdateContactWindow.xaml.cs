using ContactsApp_2.Classes;
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
using System.Windows.Shapes;

namespace ContactsApp_2
{
    /// <summary>
    /// Interaction logic for UpdateContactWindow.xaml
    /// </summary>
    public partial class UpdateContactWindow : Window
    {
        Contact contact;
        // u konstruktor šaljemo argument objekt Contact
        // kada se iz MainWindow-a selektira kontakt i klikne dugme update, 
        // poziva se ovaj konstruktor i u njega se šalje selektirani item tipa Contact
        public UpdateContactWindow(Contact contact)
        {
            InitializeComponent();
            this.contact = contact;

            // uzimaju se vrijetnosti i ispisuju u odgovarajuće textboxeve
            nameTextBox.Text = contact.Name;
            emailTextBox.Text = contact.Email;
            phoneTextBox.Text = contact.Phone;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            // zatvara prozor
            Close();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            // spremanje vrijednosti iz textBoxeva u objekt contact
            contact.Name = nameTextBox.Text;
            contact.Email = emailTextBox.Text;
            contact.Phone = phoneTextBox.Text;

            // spajanje na sqlite bazu
            using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>();
                connection.Update(contact);

                // zatvaranje prozora
                Close();
            }
        }
    }
}
