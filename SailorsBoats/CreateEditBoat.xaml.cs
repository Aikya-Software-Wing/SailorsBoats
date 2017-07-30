using SailorsBoats.DAL;
using SailorsBoats.Models;
using SailorsBoats.Validators;
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
    /// Interaction logic for CreateEditBoat.xaml
    /// </summary>
    public partial class CreateEditBoat : Window
    {
        private int BoatId = -1;
        private BoatDAL dal;

        public CreateEditBoat()
        {
            InitializeComponent();
            dal = BoatDAL.Instance;
        }

        public CreateEditBoat(int BoatId) : this()
        {
            this.BoatId = BoatId;
            DisplayBoatForEdit();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateSailorAndDisplayMessages(BoatId_TextBox.Text, BoatName_TextBox.Text,
                BoatColor_TextBox.Text))
            {
                return;
            }

            Boat boat = GetBoatObjectFromInput();

            if (BoatId == -1)
            {
                dal.AddBoat(boat);
            }
            else
            {
                dal.UpdateBoat(BoatId, boat);
            }

            Close();
        }

        #region Helpers
        private void DisplayBoatForEdit()
        {
            Boat boat = dal.GetBoat(BoatId);
            BoatId_TextBox.Text = boat.Id + "";
            BoatName_TextBox.Text = boat.Name + "";
            BoatColor_TextBox.Text = boat.Color + "";
        }

        private Boat GetBoatObjectFromInput()
        {
            int id = int.Parse(BoatId_TextBox.Text);
            string name = BoatName_TextBox.Text;
            string color = BoatColor_TextBox.Text;

            Boat boat = new Boat
            {
                Id = id,
                Name = name,
                Color = color
            };
            return boat;
        }

        private bool ValidateSailorAndDisplayMessages(string id, string name, string color)
        {
            bool allPropertiesValid = true;
            string errorMessage;

            if (!BoatValidator.IsIdValid(id, out errorMessage))
            {
                allPropertiesValid = false;
                BoatId_ValidationLabel.Content = errorMessage;
            }

            if (!BoatValidator.IsNameValid(name, out errorMessage))
            {
                allPropertiesValid = false;
                BoatName_ValidationLabel.Content = errorMessage;
            }

            if (!BoatValidator.IsColorValid(color, out errorMessage))
            {
                allPropertiesValid = false;
                BoatColor_ValidationLabel.Content = errorMessage;
            }

            return allPropertiesValid;
        }
        #endregion
    }
}
