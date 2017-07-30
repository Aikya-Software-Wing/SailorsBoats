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
    public class BoatDAL
    {
        #region Singleton

        private static volatile BoatDAL instance;
        private static object padLock = new Object();

        private BoatDAL()
        {
        }

        public static BoatDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padLock)
                    {
                        if (instance == null)
                        {
                            instance = new BoatDAL();
                        }
                    }
                }

                return instance;
            }
        }
        #endregion

        private ObservableCollection<Boat> ListOfBoats = new ObservableCollection<Boat>();

        public ObservableCollection<Boat> GetAllBoats()
        {
            ListOfBoats.Clear();

            string query = "SELECT * " +"FROM boats";

            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListOfBoats.Add(new Boat
                            {
                                Id = (int)reader[0],
                                Name = (string)reader[1],
                                Color = (string)reader[2]
                            });
                        }
                        reader.Close();
                    }
                }
            }

            return ListOfBoats;
        }

        public void AddBoat(Boat boat)
        {
            ListOfBoats.Add(boat);

            string queryString = "INSERT INTO Boats " + "VALUES(@id, @name, @color)";

            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@id", boat.Id);
                    command.Parameters.AddWithValue("@name", boat.Name);
                    command.Parameters.AddWithValue("@color", boat.Color);
                    
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public Boat GetBoat(int id)
        {
            Boat boat = null;

            string query = "SELECT * " + "FROM Boats " + "WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            boat = new Boat
                            {
                                Id = (int)reader[0],
                                Name = (string)reader[1],
                                Color = (string)reader[2]
                            };
                        }
                    }
                }
            }

            return boat;
        }

        public void UpdateBoat(int id, Boat boat)
        {
            ListOfBoats.Remove(ListOfBoats.Where(x => x.Id == id).First());
            ListOfBoats.Add(boat);

            // this query is very very particular about spaces
            string query = "UPDATE Boats " + "SET id = @newId, name = @name, color = @color " +
                "WHERE id = @oldId";

            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@oldId", id);
                    command.Parameters.AddWithValue("@newId", boat.Id);
                    command.Parameters.AddWithValue("@name", boat.Name);
                    command.Parameters.AddWithValue("@color", boat.Color);
                    
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteBoat(int id)
        {
            ListOfBoats.Remove(ListOfBoats.Where(x => x.Id == id).First());

            string query = "DELETE FROM Boats " + "WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
