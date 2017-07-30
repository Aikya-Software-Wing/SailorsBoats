using SailorsBoats.DAL;
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
    /// Interaction logic for Boats.xaml
    /// </summary>
    public partial class Boats : Window
    {
        private BoatDAL dal;

        public Boats()
        {
            InitializeComponent();
            dal = BoatDAL.Instance;
            BoatsDataGrid.ItemsSource = dal.GetAllBoats();
        }

        private void NewBoat_Click(object sender, RoutedEventArgs e)
        {
            CreateEditBoat createEditBoatWindow = new CreateEditBoat();
            createEditBoatWindow.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button editButton = (Button)sender;
            int boatId = (int)editButton.Tag;

            CreateEditBoat createEditBoatWindow = new CreateEditBoat(boatId);
            createEditBoatWindow.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = (Button)sender;
            int boatId = (int)deleteButton.Tag;

            MessageBoxResult result = MessageBox.Show(this, "Are you sure you want to delete sailor with ID "
                + boatId + "?", "Are you sure?", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    dal.DeleteBoat(boatId);
                    //SailorsDataGrid.Items.Refresh();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
