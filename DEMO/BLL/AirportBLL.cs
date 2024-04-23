using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DTO;
using DAL;
using System.Data.SqlClient;
using System.Data;
using System.Security.Principal;

namespace BLL
{
    public class AirportBLL
    {
        public static List<AirportDTO> GetAirports()
        {
            AirportDAL airportDAL = new AirportDAL();
            return airportDAL.GetAirports();
        }
    }
}
