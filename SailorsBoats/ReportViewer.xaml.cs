using SailorsBoats.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                    List<OutputColumnsReport1> output1 = new List<OutputColumnsReport1>();

                    string query1 = "SELECT name " +"FROM Sailors "+"WHERE rating > 8";

                    using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query1, connection))
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    output1.Add(new OutputColumnsReport1
                                    {
                                        name = (string)reader[0]
                                    });
                                }
                                reader.Close();
                            }
                        }
                    }

                    ReportDataGrid.ItemsSource = output1;
                    break;

                case 2:
                    List<OutputColumnsReport2> output2 = new List<OutputColumnsReport2>();

                    string query2 = "SELECT S.name " + "FROM Sailors S,Reserves R " + 
                        "WHERE S.id=R.sailorId AND R.boatId=1 ";

                    using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query2, connection))
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    output2.Add(new OutputColumnsReport2
                                    {
                                        name = (string)reader[0]
                                    });
                                }
                                reader.Close();
                            }
                        }
                    }

                    ReportDataGrid.ItemsSource = output2;
                    break;

                case 3:
                    List<OutputColumnsReport3> output3 = new List<OutputColumnsReport3>();

                    string query3 = "SELECT B.color " + "FROM Sailors S,Reserves R,Boats B " +
                        "WHERE S.id=R.sailorId AND R.boatId=B.id AND S.name='Andy Dufresne'";

                    using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query3, connection))
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    output3.Add(new OutputColumnsReport3
                                    {
                                        color = (string)reader[0]
                                    });
                                }
                                reader.Close();
                            }
                        }
                    }

                    ReportDataGrid.ItemsSource = output3;
                    break;

                case 4:
                    List<OutputColumnsReport4> output4 = new List<OutputColumnsReport4>();

                    string query4 = "SELECT S.name " + "FROM Sailors S,Reserves R,Boats B " +
                        "WHERE S.id=R.sailorId AND R.boatId=B.id AND (B.color='Absolute Zero' OR B.color='Cinnabar')";

                    using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query4, connection))
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    output4.Add(new OutputColumnsReport4
                                    {
                                        name = (string)reader[0]
                                    });
                                }
                                reader.Close();
                            }
                        }
                    }

                    ReportDataGrid.ItemsSource = output4;
                    break;

                case 5:
                    List<OutputColumnsReport5> output5 = new List<OutputColumnsReport5>();

                    string query5 = "SELECT S.id, S.name " + "FROM Sailors S " +
                        "WHERE S.rating >= ALL(SELECT S1.rating FROM Sailors S1)";

                    using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query5, connection))
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    output5.Add(new OutputColumnsReport5
                                    {
                                        id=(int)reader[0],
                                        name = (string)reader[1]
                                    });
                                }
                                reader.Close();
                            }
                        }
                    }

                    ReportDataGrid.ItemsSource = output5;
                    break;

                case 6:
                    List<OutputColumnsReport6> output6 = new List<OutputColumnsReport6>();

                    string query6 = "SELECT AVG(age)" + "FROM Sailors " +
                        "WHERE rating=10";

                    using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query6, connection))
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    output6.Add(new OutputColumnsReport6
                                    {
                                        age = (int)reader[0]
                                    });
                                }
                                reader.Close();
                            }
                        }
                    }

                    ReportDataGrid.ItemsSource = output6;
                    break;

                case 7:
                    List<OutputColumnsReport7> output7 = new List<OutputColumnsReport7>();

                    string query7 = "SELECT S.name, S.age " + "FROM Sailors S " +
                        "WHERE S.age = (SELECT MAX(S1.age) FROM Sailors S1) ";

                    using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query7, connection))
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    output7.Add(new OutputColumnsReport7
                                    {
                                        name=(string)reader[0],
                                        age = (int)reader[1]
                                    });
                                }
                                reader.Close();
                            }
                        }
                    }

                    ReportDataGrid.ItemsSource = output7;
                    break;

                case 8:
                    List<OutputColumnsReport8> output8 = new List<OutputColumnsReport8>();

                    string query8 = "SELECT COUNT(DISTINCT name) " + "FROM Sailors ";

                    using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query8, connection))
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    output8.Add(new OutputColumnsReport8
                                    {
                                        count = (int)reader[0]
                                    });
                                }
                                reader.Close();
                            }
                        }
                    }

                    ReportDataGrid.ItemsSource = output8;
                    break;

                case 9:
                    List<OutputColumnsReport9> output9 = new List<OutputColumnsReport9>();

                    string query9 = "SELECT MIN(age) " + "FROM Sailors S " + "GROUP BY rating";

                    using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query9, connection))
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    output9.Add(new OutputColumnsReport9
                                    {
                                        age = (int)reader[0]
                                    });
                                }
                                reader.Close();
                            }
                        }
                    }

                    ReportDataGrid.ItemsSource = output9;
                    break;

                case 10:
                    List<OutputColumnsReport10> output10 = new List<OutputColumnsReport10>();

                    string query10 = "SELECT AVG(S.age) " + "FROM Sailors S " + "GROUP BY S.rating " +
                        "HAVING 2 <= (SELECT COUNT(S1.id) FROM Sailors S1 WHERE S.rating=S1.rating)";

                    using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query10, connection))
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    output10.Add(new OutputColumnsReport10
                                    {
                                        averageAge = (int)reader[0]
                                    });
                                }
                                reader.Close();
                            }
                        }
                    }

                    ReportDataGrid.ItemsSource = output10;
                    break;
            }
        }
    }

    public class OutputColumnsReport1
    {
        public string name { get; set; }
    }

    public class OutputColumnsReport2
    {
        public string name { get; set; }
    }

    public class OutputColumnsReport3
    {
        public string color { get; set; }
    }

    public class OutputColumnsReport4
    {
        public string name { get; set; }
    }

    public class OutputColumnsReport5
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class OutputColumnsReport6
    {
        public int age { get; set; }
    }

    public class OutputColumnsReport7
    {
        public string name { get; set; }
        public int age { get; set; }
    }

    public class OutputColumnsReport8
    { 
        public int count { get; set; }
    }

    public class OutputColumnsReport9
    {
        public int age { get; set; }
    }

    public class OutputColumnsReport10
    {
        public int averageAge { get; set; }
    }
}
