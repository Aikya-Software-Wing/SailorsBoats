using SailorsBoats.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ObservableCollection<Reserve> ReserveList = new ObservableCollection<Reserve>
        {
            new Reserve { Id = 0, SailorId = 1, BoatId =1, Date = DateTime.Now },
            new Reserve { Id = 1, SailorId = 2, BoatId =3, Date = DateTime.Now },
            new Reserve { Id = 2, SailorId = 3, BoatId =4, Date = DateTime.Now },
        };

        public ObservableCollection<Reserve> GetAllReserves()
        {
            return ReserveList;
        }

        public void AddReserve(Reserve reserve)
        {
            ReserveList.Add(reserve);
        }

        public Reserve GetReserve(int sailorId, int boatId, DateTime date)
        {
            return ReserveList.Where(x => x.SailorId == sailorId && x.BoatId == boatId
                 && x.Date == date).First();
        }

        public void UpdateReserve(int sailorId, int boatId, DateTime date, Reserve reserve)
        {
            ReserveList.Remove(GetReserve(sailorId, boatId, date));
            AddReserve(reserve);
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
