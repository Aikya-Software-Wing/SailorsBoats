using SailorsBoats.Models;
using SailorsBoats.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailorsBoats.DAL
{
    public class SailorDAL
    {
        #region Singleton

        private static volatile SailorDAL instance;
        private static object padLock = new Object();

        private SailorDAL()
        {
        }

        public static SailorDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padLock)
                    {
                        if (instance == null)
                        {
                            instance = new SailorDAL();
                        }
                    }
                }

                return instance;
            }
        }
        #endregion

        private ObservableCollection<Sailor> SailorList = new ObservableCollection<Sailor>();

        public ObservableCollection<Sailor> GetAllSailors()
        {
            ObservableCollection<Sailor> sailorList = new ObservableCollection<Sailor>();

            string queryString = "SELECT * FROM Sailors";
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sailorList.Add(new Sailor
                    {
                        Id = (int)reader[0],
                        Name = (string)reader[1],
                        Rating = (int)reader[2],
                        Age = (int)reader[3]
                    });
                }
                reader.Close();
            }

            return sailorList;
        }

        public void AddSailor(Sailor sailor)
        {
            SailorList.Add(sailor);
        }

        public Sailor GetSailor(int id)
        {
            return SailorList.Where(x => x.Id == id).First();
        }

        public void UpdateSailor(int id, Sailor sailor)
        {
            SailorList.Remove(GetSailor(id));
            AddSailor(sailor);
        }

        public void DeleteSailor(int id)
        {
            SailorList.Remove(GetSailor(id));
        }
    }
}
