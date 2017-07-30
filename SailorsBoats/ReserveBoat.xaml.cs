using SailorsReserves.DAL;
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
    /// Interaction logic for ReserveBoat.xaml
    /// </summary>
    public partial class ReserveBoat : Window
    {
        private ReserveDAL dal;

        public ReserveBoat()
        {
            InitializeComponent();
            dal = ReserveDAL.Instance;
            ReservesDataGrid.ItemsSource = dal.GetAllReserves();
        }

        private void NewReservation_Click(object sender, RoutedEventArgs e)
        {
            CreateEditReservation createEditReservationWindow = new CreateEditReservation();
            createEditReservationWindow.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button editButton = (Button)sender;
            int reservationId = (int)editButton.Tag;

            CreateEditReservation createEditReservationWindow = new CreateEditReservation(reservationId);
            createEditReservationWindow.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = (Button)sender;
            int reservationId = (int)deleteButton.Tag;

            MessageBoxResult result = MessageBox.Show(this, "Are you sure you want to delete sailor with ID "
                + reservationId + "?", "Are you sure?", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    dal.DeleteReserve(reservationId);
                    //SailorsDataGrid.Items.Refresh();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
