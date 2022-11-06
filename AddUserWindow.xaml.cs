using CrudExample.Models;
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

namespace CrudExample
{
    public partial class AddUserWindow : Window
    {
        User currentUser;

        public AddUserWindow(User user)
        {
            InitializeComponent();

            if (user != null) {
                currentUser = user;
                DataContext = currentUser;
            }
            else
            {
                currentUser = null;
            }
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {

            using (UserContext db = new UserContext())
            {

                if(currentUser != null)
                {
                    db.Users.Update(currentUser);
                }
                else
                {
                    User user = new User() { Name = NameBox.Text, Age = Convert.ToInt32(AgeBox.Text)};
                    db.Users.Add(user);
                }

                try
                {
                    db.SaveChanges();
                    Proxy.UserDataGrid.ItemsSource = db.Users.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.InnerException.ToString());
                }
                
                


            }
        



                
              

            

        }

    }
}
