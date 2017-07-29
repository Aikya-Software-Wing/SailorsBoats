using SailorsBoats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailorsBoats.DAL
{
    public class SailorDAL
    {
        private static volatile SailorDAL instance;
        private static object padLock = new Object();
        private List<Sailor> SailorList;

        private SailorDAL()
        {
            SailorList = new List<Sailor>
            {
                new Sailor { Age = 10, Id = 1, Name = "Sailor 1", Rating = 10 },
                new Sailor { Age = 13, Id = 2, Name = "Sailor 4", Rating = 10 },
                new Sailor { Age = 15, Id = 3, Name = "Sailor 6", Rating = 10 },
                new Sailor { Age = 12, Id = 4, Name = "Sailor 3", Rating = 10 }
            };
        }

        public static SailorDAL Instance
        {
            get
            {
                if(instance == null)
                {
                    lock(padLock)
                    {
                        if(instance == null)
                        {
                            instance = new SailorDAL();
                        }
                    }
                }

                return instance;
            }
        }

        public List<Sailor> GetAllSailors()
        {
            return SailorList;
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
