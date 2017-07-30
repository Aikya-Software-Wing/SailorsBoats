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
    /// Interaction logic for ReportViewer.xaml
    /// </summary>
    public partial class ReportViewer : Window
    {
        private int reportId;

        public ReportViewer()
        {
            InitializeComponent();
        }

        public ReportViewer(int id) : this()
        {
            reportId = id;
            ShowReport();
        }

        public void ShowReport()
        {
            switch(reportId)
            {
                case 1:
                    List<OutputColumnsReport1> output = new List<OutputColumnsReport1>();

                    // fill up the list
                    output.Add(new OutputColumnsReport1
                    {
                        Column1 = "data",
                        Column2 = "more data"
                    });

                    ReportDataGrid.ItemsSource = output;
                    break;
                case 2:
                    break;
            }
        }
    }

    public class OutputColumnsReport1
    {
        public string Column1 { get; set; }
        public string Column2 { get; set; }
    }
}
