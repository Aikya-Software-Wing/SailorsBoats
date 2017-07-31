using SailorsBoats.DAL;
using SailorsBoats.Models;
using SailorsBoats.Validators;
using SailorsReserves.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CreateEditReservation.xaml
    /// </summary>
    public partial class CreateEditReservation : Window
    {
        private int ReservationId = -1;
        private ReserveDAL reserveDal;
        private SailorDAL sailorDal;
        private BoatDAL boatDal;

        public CreateEditReservation()
        {
            InitializeComponent();
            reserveDal = ReserveDAL.Instance;
            sailorDal = SailorDAL.Instance;
            boatDal = BoatDAL.Instance;

            PopulateSailorDropDown();
            PopulateBoatDropDown();
            SetUpForm();
        }

        public CreateEditReservation(int ReservationId) : this()
        {
            this.ReservationId = ReservationId;
            DisplayReservationForEdit();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            Reserve reserve = GetReserveObjectFromInput();

            if (ReservationId == -1)
            {
                reserveDal.AddReserve(reserve);
            }
            else
            {
                reserveDal.UpdateReserve(ReservationId, reserve);
            }

            Close();
        }

        #region Helpers
        private void DisplayReservationForEdit()
        {
            Reserve reserve = reserveDal.GetReserve(ReservationId);
            SailorName_TextBox.SelectedValue = reserve.SailorId;
            BoatName_TextBox.SelectedValue = reserve.BoatId;
            ReservationDate_TextBox.SelectedDate = reserve.Date;
        }

        private void SetUpForm()
        {
            ReservationDate_TextBox.SelectedDate = DateTime.Today;
        }

        private void PopulateSailorDropDown()
        {
            SailorName_TextBox.ItemsSource = sailorDal.GetAllSailors();
        }

        private void PopulateBoatDropDown()
        {
            BoatName_TextBox.ItemsSource = boatDal.GetAllBoats();
        }

        private Reserve GetReserveObjectFromInput()
        {
            int sailorName = (int)SailorName_TextBox.SelectedValue;
            int boatName = (int)BoatName_TextBox.SelectedValue;
            DateTime date = ReservationDate_TextBox.SelectedDate.Value;

            Reserve reserve = new Reserve
            {
                SailorId = sailorName,
                BoatId = boatName,
                Date = date
            };
            return reserve;
        }

        #endregion
    }
}
