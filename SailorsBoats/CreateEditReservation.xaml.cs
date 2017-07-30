using SailorsBoats.Models;
using SailorsBoats.Validators;
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
    /// Interaction logic for CreateEditReservation.xaml
    /// </summary>
    public partial class CreateEditReservation : Window
    {
        private int ReservationId = -1;
        private ReserveDAL dal;

        public CreateEditReservation()
        {
            InitializeComponent();
            dal = ReserveDAL.Instance;
        }

        public CreateEditReservation(int ReservationId) : this()
        {
            this.ReservationId = ReservationId;
            DisplayReservationForEdit();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateReservationAndDisplayMessages(SailorName_TextBox.Text, BoatName_TextBox.Text, 
                ReservationDate_TextBox.Text))
            {
                return;
            }

            Reserve reserve = GetReserveObjectFromInput();

            if (ReservationId == -1)
            {
                dal.AddReserve(reserve);
            }
            else
            {
                dal.UpdateReserve(ReservationId, reserve);
            }

            Close();
        }

        #region Helpers
        private void DisplayReservationForEdit()
        {
            Reserve reserve = dal.GetReserve(ReservationId);
            SailorName_TextBox.Text = reserve.SailorId + "";
            BoatName_TextBox.Text = reserve.BoatId + "";
            ReservationDate_TextBox.Text = reserve.Date.ToLongDateString();
        }

        private Reserve GetReserveObjectFromInput()
        {
            int sailorName = int.Parse(SailorName_TextBox.Text);
            int boatName = int.Parse(BoatName_TextBox.Text);
            DateTime date = DateTime.Now;

            Reserve reserve = new Reserve
            {
                SailorId = sailorName,
                BoatId = boatName,
                Date = date
            };
            return reserve;
        }

        private bool ValidateReservationAndDisplayMessages(string sailorId, string boatId, string date)
        {
            bool allPropertiesValid = true;

            if (!ReserveValidator.IsSailorIdValid(sailorId, out string errorMessage))
            {
                allPropertiesValid = false;
                SailorName_ValidationLabel.Content = errorMessage;
            }

            if (!ReserveValidator.IsBoatIdValid(boatId, out errorMessage))
            {
                allPropertiesValid = false;
                BoatName_ValidationLabel.Content = errorMessage;
            }

            if (!ReserveValidator.IsDateValid(date, out errorMessage))
            {
                allPropertiesValid = false;
                ReservationDate_ValidationLabel.Content = errorMessage;
            }

            return allPropertiesValid;
        }
        #endregion
    }
}
