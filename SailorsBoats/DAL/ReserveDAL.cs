using SailorsBoats.DAL;
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

            string queryString = "select R.sailorId, R.boatId, R.reserveDate, B.name, S.name " +
                "from (Reserves R inner join Boats B on R.boatId = B.id) " +
                "inner join Sailors S on R.sailorId = S.id";
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
                                Date = (DateTime)reader[2],
                                BoatName = (string)reader[3],
                                SailorName = (string)reader[4]
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
            SailorDAL sailorDal = SailorDAL.Instance;
            BoatDAL boatDal = BoatDAL.Instance;
            reserve.Id = ReserveList.Max(x => x.Id) + 1;
            reserve.SailorName = sailorDal.GetSailor(reserve.SailorId).Name;
            reserve.BoatName = boatDal.GetBoat(reserve.BoatId).Name;
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
            Reserve reserve = null;

            string queryString = "SELECT * " +
                "FROM Reserves " +
                "WHERE sailorId = @sailorId AND boatId = @boatId AND reserveDate = @reserveDate";

            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@sailorId", sailorId);
                    command.Parameters.AddWithValue("@boatId", boatId);
                    command.Parameters.AddWithValue("@reserveDate", date);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reserve = new Reserve
                            {
                                SailorId = (int)reader[0],
                                BoatId = (int)reader[1],
                                Date = (DateTime)reader[2]
                            };
                        }
                    }
                }
            }

            return reserve;
        }

        public Reserve GetReserve(int id)
        {
            Reserve reserve = ReserveList.Where(x => x.Id == id).First();

            reserve =  GetReserve(reserve.SailorId, reserve.BoatId, reserve.Date);
            reserve.Id = id;

            return reserve;
        }

        public void UpdateReserve(int sailorId, int boatId, DateTime date, Reserve reserve)
        {
            SailorDAL sailorDal = SailorDAL.Instance;
            BoatDAL boatDal = BoatDAL.Instance;
            ReserveList.Remove(ReserveList.Where(x => x.SailorId == sailorId
                && x.BoatId == boatId && x.Date == date).First());
            reserve.Id = ReserveList.Max(x => x.Id) + 1;
            reserve.SailorName = sailorDal.GetSailor(reserve.SailorId).Name;
            reserve.BoatName = boatDal.GetBoat(reserve.BoatId).Name;
            ReserveList.Add(reserve);

            string queryString = "UPDATE Reserves " +
                "SET sailorId = @newSailorId, boatId = @newBoatId, reserveDate = @newReserveDate " +
                "WHERE sailorId = @oldSailorId AND boatId = @oldBoatId AND reserveDate = @oldReserveDate";

            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@oldSailorId", sailorId);
                    command.Parameters.AddWithValue("@oldBoatId", boatId);
                    command.Parameters.AddWithValue("@oldReserveDate", date);
                    command.Parameters.AddWithValue("@newSailorId", reserve.SailorId);
                    command.Parameters.AddWithValue("@newBoatId", reserve.BoatId);
                    command.Parameters.AddWithValue("@newReserveDate", reserve.Date);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateReserve(int id, Reserve reserve)
        {
            Reserve oldReserve = ReserveList.Where(x => x.Id == id).First();
            UpdateReserve(oldReserve.SailorId, oldReserve.BoatId, oldReserve.Date, reserve);
        }

        public void DeleteReserve(int sailorId, int boatId, DateTime date)
        {
            ReserveList.Remove(ReserveList.Where(x => x.SailorId == sailorId 
                && x.BoatId == boatId && x.Date == date).First());

            string queryString = "DELETE FROM Reserves " +
                "WHERE sailorId = @sailorId AND boatId = @boatId AND reserveDate = @reserveDate";

            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@sailorId", sailorId);
                    command.Parameters.AddWithValue("@boatId", boatId);
                    command.Parameters.AddWithValue("@reserveDate", date);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteReserve(int id)
        {
            Reserve reserve = ReserveList.Where(x => x.Id == id).First();
            DeleteReserve(reserve.SailorId, reserve.BoatId, reserve.Date);
        }
    }
}
