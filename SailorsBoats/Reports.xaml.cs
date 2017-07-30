using SailorsBoats.ViewModel;
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
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Window
    {
        private List<ReportViewModel> reportList;

        public Reports()
        {
            InitializeComponent();
            reportList = new List<ReportViewModel>
            {
                new ReportViewModel{ Id = 1, Description = "Names of all sailors with a rating above 8"},
                new ReportViewModel{ Id = 2, Description = "Names of all sailors who have reserved boat number 1"},
                new ReportViewModel{ Id = 3, Description = "Colors of boats reserved by Andy Dufresne"},
                new ReportViewModel{ Id = 4, Description = "Names of sailors who have reserved a Absolute Zero or Cinnabar boat"},
                new ReportViewModel{ Id = 5, Description = "Sailor with highest rating"},
                new ReportViewModel{ Id = 6, Description = "Average age of all sailors with rating 10"},
                new ReportViewModel{ Id = 7, Description = "Name and age of the oldest sailor"},
                new ReportViewModel{ Id = 8, Description = "Count the number of different sailor names"},
                new ReportViewModel{ Id = 9, Description = "Age of youngest sailor for each rating level"},
                new ReportViewModel{ Id = 10, Description = "Average age of sailors for each rating that has atleast 2 sailors"}
            };

            Report_ListView.ItemsSource = reportList;
        }

        private void GenerateReport_Button_Click(object sender, RoutedEventArgs e)
        {
            Button generateReportButton = (Button)sender;
            int reportId = (int)generateReportButton.Tag;

            ReportViewer reportViewer = new ReportViewer(reportId);
            reportViewer.ShowDialog();
        }
    }
}
