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
                new ReportViewModel{ Id = 1, Description = "Report #1"},
                new ReportViewModel{ Id = 2, Description = "Report #2"},
                new ReportViewModel{ Id = 3, Description = "Report #3"},
                new ReportViewModel{ Id = 4, Description = "Report #4"}
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
