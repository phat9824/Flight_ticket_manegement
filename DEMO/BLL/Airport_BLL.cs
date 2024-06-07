﻿using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Airport_BLL
    {
        public List<AirportDTO> L_airport()
        {
            /*AirportAccess airportAccess = new AirportAccess();
            return airportAccess.L_airport();*/
            return new DAL.AirportAccess().L_airport();
        }
        public string insertAirport(string airportName)
        {
            return new DAL.AirportAccess().AddAirport(airportName);
        }
        public int deleteAirport(string airportID)
        {
            return new DAL.AirportAccess().DeleteAirport(airportID);
        }
        
    }
}
