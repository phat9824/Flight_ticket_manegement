using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Search
    {
        public Search() { }
        public List<FlightInformationSearchDTO> GetInformationSearch(string sourceAirportID, string destinationAirportID
                                                                       , DateTime startDate, DateTime endDate)
        {
            // Nhận đầu vào là các tham số cho truy vấn và thuộc tính được sort khi trả về:
            // Các thuộc tính có thể có hoặc không
            // Trả về danh sách dữ liệu theo yêu cầu
            FlightAccess flightAccess = new FlightAccess();
            
            List<FlightInformationSearchDTO> data = new List<FlightInformationSearchDTO>();

            data = flightAccess.getFlight(sourceAirportID, destinationAirportID, startDate, endDate);
            return data;
        }
    }
}
