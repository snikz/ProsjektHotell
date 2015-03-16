using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFhotell
{
    public class HotelServices : IHotelServices
    {
        HotelServiceDataModelDataContext Data;

        public HotelServices()
        {
            Data = new HotelServiceDataModelDataContext();
        }

        public List<Service> GetServices(string serviceType)
        {
            var services = from service in Data.Services
                           where service.type == Convert.ToInt32(serviceType)
                           select service;
            return services.ToList();
        }

        public bool ChangeServiceStatus(string serviceID, string status, string note)
        {
            bool changeSuccessful = false;
            if (note.Equals("empty"))
            {
                note = "";
            }
            Service choosenService = (from service in Data.Services
                                      where service.id == Convert.ToInt32(serviceID)
                                      select service).First();
            choosenService.status = Convert.ToInt32(status);
            choosenService.note = note;
            try
            {
                Data.SubmitChanges();
                changeSuccessful = true;
            }
            catch
            {
                changeSuccessful = false;
            }

            return changeSuccessful;
        }
    }
}
