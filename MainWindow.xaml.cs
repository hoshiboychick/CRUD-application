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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CrudExample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            using(UserContext db = new UserContext())
            {

                //User user1 = new User() { Name = "user1", Age = 10 };
                //User user2 = new User() { Name = "user2", Age = 10 };
                //User user3 = new User() { Name = "user3", Age = 10 };

                //db.Users.AddRange(user1, user2, user3);


                try
                {
                  db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.ToString());
                }
                



                userGrid.ItemsSource = db.Users.ToList();

                DataGrid ug1 = userGrid;

                Proxy.UserDataGrid = ug1;
                
            }

        }


        private void AddUserClick(object sender, RoutedEventArgs e)
        {
            new AddUserWindow(null).ShowDialog();
        }



        private void EditUserDoubleClick(object sender, MouseButtonEventArgs e)
        {


            User user = ((User)userGrid.SelectedItem);


            new AddUserWindow(user).ShowDialog();


            //MessageBox.Show(((User)userGrid.SelectedItem).Name.ToString());

        }

        private void DeleteUserClick(object sender, RoutedEventArgs e)
        {
            using (UserContext db = new UserContext())
            {
                if (userGrid.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < userGrid.SelectedItems.Count; i++)
                    {
                        User user = userGrid.SelectedItems[i] as User;
                        if (user != null)
                        {
                            db.Users.Remove(user);
                        }
                    }
                }
                db.SaveChanges();
                userGrid.ItemsSource = db.Users.ToList();
            }       
        }
    }
}
