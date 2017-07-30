using SailorsBoats.Models;
using SailorsBoats.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailorsReserves.DAL
{
    public class ReserveDAL
    {
        #region Singleton

        private static volatile ReserveDAL instance;
        private static object padLock = new Object();

        private ReserveDAL()
        {
        }

        public static ReserveDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padLock)
                    {
                        if (instance == null)
                        {
                            instance = new ReserveDAL();
                        }
                    }
                }

                return instance;
            }
        }
        #endregion

        private ObservableCollection<Reserve> ReserveList = new ObservableCollection<Reserve>();

        public ObservableCollection<Reserve> GetAllReserves()
        {
            ReserveList.Clear();

            string queryString = "SELECT * " +
                "FROM Reserves";
            int uid = 0;

            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ReserveList.Add(new Reserve
                            {
                                Id = uid++,
                                SailorId = (int)reader[0],
                                BoatId = (int)reader[1],
                                Date = (DateTime)reader[2]
                            });
                        }
                        reader.Close();
                    }
                }
            }

            return ReserveList;
        }

        public void AddReserve(Reserve reserve)
        {
            ReserveList.Add(reserve);

            string queryString = "INSERT INTO Reserves " +
                "VALUES(@sailorId, @boatId, @reserveDate)";

            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@sailorId", reserve.SailorId);
                    command.Parameters.AddWithValue("@boatId", reserve.BoatId);
                    command.Parameters.AddWithValue("@reserveDate", reserve.Date);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public Reserve GetReserve(int sailorId, int boatId, DateTime date)
        {
            return ReserveList.Where(x => x.SailorId == sailorId && x.BoatId == boatId
                 && x.Date == date).First();
        }

        public Reserve GetReserve(int id)
        {
            Reserve reserve = ReserveList.Where(x => x.Id == id).First();
            return GetReserve(reserve.SailorId, reserve.BoatId, reserve.Date);
        }

        public void UpdateReserve(int sailorId, int boatId, DateTime date, Reserve reserve)
        {
            ReserveList.Remove(GetReserve(sailorId, boatId, date));
            AddReserve(reserve);
        }

        public void UpdateReserve(int id, Reserve reserve)
        {
            Reserve oldReserve = ReserveList.Where(x => x.Id == id).First();
            UpdateReserve(oldReserve.SailorId, oldReserve.BoatId, oldReserve.Date, reserve);
        }

        public void DeleteReserve(int sailorId, int boatId, DateTime date)
        {
            ReserveList.Remove(GetReserve(sailorId, boatId, date));
        }

        public void DeleteReserve(int id)
        {
            Reserve reserve = ReserveList.Where(x => x.Id == id).First();
            DeleteReserve(reserve.SailorId, reserve.BoatId, reserve.Date);
        }
    }
}
