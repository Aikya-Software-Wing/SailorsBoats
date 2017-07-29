using SailorsBoats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatsBoats.DAL
{
    class BoatDAL
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

        private List<Boat> BoatList = new List<Boat>
        {
            new Boat { Id = 1, Name = "Boat 1", Color = "Red" },
            new Boat { Id = 2, Name = "Boat 4", Color = "Red" },
            new Boat { Id = 3, Name = "Boat 6", Color = "Red" },
            new Boat { Id = 4, Name = "Boat 3", Color = "Red" }
        };

        public List<Boat> GetAllBoats()
        {
            return BoatList;
        }

        public void AddBoat(Boat boat)
        {
            BoatList.Add(boat);
        }

        public Boat GetBoat(int id)
        {
            return BoatList.Where(x => x.Id == id).First();
        }

        public void UpdateBoat(int id, Boat boat)
        {
            BoatList.Remove(GetBoat(id));
            AddBoat(boat);
        }

        public void DeleteBoat(int id)
        {
            BoatList.Remove(GetBoat(id));
        }
    }
}
