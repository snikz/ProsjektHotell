﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HotellDLL;

namespace WCFhotell
{
    public class Service2
    {
        public int id { get; set; }
        public String note { get; set; }
        public int roomId { get; set; }
        public int status { get; set; }
        public int type { get; set; }
    }
    public class HotelServices : IHotelServices
    {
        DatabaseDataContext Data;

        
        public HotelServices()
        {
            Data = new DatabaseDataContext();
        }

        public List<Service2> GetServices(string serviceType)
        {
            var services = from service in Data.Services
                           where service.type == Convert.ToInt32(serviceType)
                           select new Service2
                           {
                               id = service.id,
                               note = service.note,
                               roomId = service.roomId,
                               status = service.status,
                               type = service.type
                           };
            var test = services.ToList();
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
