using SailorsBoats.DAL;
using SailorsBoats.Models;
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

namespace SailorsBoats
{
    /// <summary>
    /// Interaction logic for Sailors.xaml
    /// </summary>
    public partial class Sailors : Window
    {
        private SailorDAL dal;

        public Sailors()
        {
            InitializeComponent();
            dal = SailorDAL.Instance;
            SailorsDataGrid.ItemsSource = dal.GetAllSailors();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = (Button)sender;
            int sailorId = (int)deleteButton.Tag;

            MessageBoxResult result =  MessageBox.Show(this, "Are you sure you want to delete sailor with ID " 
                + sailorId + "?", "Are you sure?", MessageBoxButton.YesNo);
            switch(result)
            {
                case MessageBoxResult.Yes:
                    dal.DeleteSailor(sailorId);
                    SailorsDataGrid.Items.Refresh();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button editButton = (Button)sender;
            int sailorId = (int)editButton.Tag;

            CreateEditSailors createEditSailorsWindow = new CreateEditSailors(sailorId);
            createEditSailorsWindow.ShowDialog();
            SailorsDataGrid.Items.Refresh();
        }

        private void NewSailor_Click(object sender, RoutedEventArgs e)
        {
            CreateEditSailors createEditSailorsWindow = new CreateEditSailors();
            createEditSailorsWindow.ShowDialog();
            SailorsDataGrid.Items.Refresh();
        }
    }
}
