using DAL;
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
    public class ReportBLL
    {
        public (List<ReportByFlightDTO> reportByFlightDTOs, int total) GetReportByFlightBLL(int Month, int Year)
        {
            return new DAL.BookingTicketAccess().GetReportByFlightDAL(Month, Year);
        }
    }
}
