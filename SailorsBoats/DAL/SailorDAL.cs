﻿using SailorsBoats.Models;
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
            SailorList.Clear();

            string queryString = "SELECT * " +
                "FROM Sailors";

            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SailorList.Add(new Sailor
                            {
                                Id = (int)reader[0],
                                Name = (string)reader[1],
                                Rating = (int)reader[2],
                                Age = (int)reader[3]
                            });
                        }
                        reader.Close();
                    }
                }
            }

            return SailorList;
        }

        public void AddSailor(Sailor sailor)
        {
            SailorList.Add(sailor);

            string queryString = "INSERT INTO Sailors " +
                "VALUES(@id, @name, @rating, @age)";

            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@id", sailor.Id);
                    command.Parameters.AddWithValue("@name", sailor.Name);
                    command.Parameters.AddWithValue("@rating", sailor.Rating);
                    command.Parameters.AddWithValue("@age", sailor.Age);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public Sailor GetSailor(int id)
        {
            Sailor sailor = null;

            string queryString = "SELECT * " +
                "FROM Sailors " +
                "WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sailor = new Sailor
                            {
                                Id = (int)reader[0],
                                Name = (string)reader[1],
                                Rating = (int)reader[2],
                                Age = (int)reader[3]
                            };
                        }
                    }
                }
            }

            return sailor;
        }

        public void UpdateSailor(int id, Sailor sailor)
        {
            SailorList.Remove(SailorList.Where(x => x.Id == id).First());
            SailorList.Add(sailor);

            string queryString = "UPDATE Sailors " +
                "SET id = @newId, name = @name, rating = @rating, age = @age " +
                "WHERE id = @oldId";

            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@oldId", id);
                    command.Parameters.AddWithValue("@newId", sailor.Id);
                    command.Parameters.AddWithValue("@name", sailor.Name);
                    command.Parameters.AddWithValue("@rating", sailor.Rating);
                    command.Parameters.AddWithValue("@age", sailor.Age);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteSailor(int id)
        {
            SailorList.Remove(SailorList.Where(x => x.Id == id).First());

            string queryString = "DELETE FROM Sailors " +
                "WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
