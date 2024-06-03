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

        public (List<ReportByMonthDTO> reportByMonthDTOs, int total) GetReportByMonthDAL(int year)
        {

            //begin

            
                // Gọi phương thức DAL để lấy dữ liệu báo cáo
                var (data, total) = new DAL.BookingTicketAccess().GetReportByMonthDAL(year);

                // Tạo một dictionary để lưu trữ dữ liệu cho việc tra cứu dễ dàng
                var monthData = data.ToDictionary(dto => dto.time.Month);

                // Khởi tạo danh sách để chứa kết quả cho cả 12 tháng
                List<ReportByMonthDTO> fullYearData = new List<ReportByMonthDTO>();

                for (int month = 1; month <= 12; month++)
                {
                    if (monthData.TryGetValue(month, out var dto))
                    {
                        // Nếu có dữ liệu cho tháng, thêm vào danh sách
                        fullYearData.Add(dto);
                    }
                    else
                    {
                        // Nếu không có dữ liệu cho tháng, thêm giá trị mặc định
                        fullYearData.Add(new ReportByMonthDTO
                        {
                            time = new DateTime(year, month, 1),
                            flightQuantity = 0,
                            revenue = 0,
                            ratio = 0
                        });
                    }
                }

                // Trả về danh sách đầy đủ và tổng doanh thu
                return (fullYearData, total);

            //end

            //return new DAL.BookingTicketAccess().GetReportByMonthDAL(year);

        }

    }
}
