using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class BookingTicketAccess
    {
        public BookingTicketAccess() { }
        string state = string.Empty;
        public string AutoID()
        {
            SqlConnection con = SqlConnectionData.Connect();
            con.Open();

            SqlCommand cmd = new SqlCommand("select COUNT(*) from BOOKING_TICKET", con);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            i++;
            return i.ToString("TK000");
        }
        public string Add_BookingTicket(string CusID, string FligthID, string TicketClassID, int TicketStatus, DateTime date)
        {
            SqlConnection con = SqlConnectionData.Connect();
            con.Open();
            using (SqlTransaction transaction = con.BeginTransaction())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO BOOKING_TICKET (TicketID, FlightID, ID, TicketClassID, TicketStatus, BookingDate, isDeleted) VALUES (@TID, @FID, @ID, @TCID, @Status, @Date, 0)";
                        cmd.Connection = con;
                        cmd.Transaction = transaction;

                        SqlParameter parTID = new SqlParameter("@TID", SqlDbType.VarChar, 20)
                        {
                            Value = AutoID()
                        };
                        SqlParameter parFID = new SqlParameter("@FID", SqlDbType.VarChar, 20)
                        {
                            Value = FligthID
                        };
                        SqlParameter parID = new SqlParameter("@ID", SqlDbType.VarChar, 20)
                        {
                            Value = CusID
                        };
                        SqlParameter parTCID = new SqlParameter("@TCID", SqlDbType.VarChar, 20)
                        {
                            Value = TicketClassID
                        };
                        SqlParameter parStatus = new SqlParameter("@Status", SqlDbType.Int)
                        {
                            Value = TicketStatus
                        };
                        SqlParameter parDate = new SqlParameter("@Date", SqlDbType.SmallDateTime)
                        {
                            Value = date
                        };

                        state = FligthID + CusID + TicketClassID + TicketStatus + date.ToString();

                        cmd.Parameters.Add(parTID);
                        cmd.Parameters.Add(parFID);
                        cmd.Parameters.Add(parID);
                        cmd.Parameters.Add(parTCID);
                        cmd.Parameters.Add(parStatus);
                        cmd.Parameters.Add(parDate);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            transaction.Rollback();
                            return "No rows were inserted.";
                        }
                    }

                    transaction.Commit();
                    return string.Empty; // Chuỗi rỗng xem như thành công
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return $"Insert failed: {ex.Message}";
                }
            }
        }
        public List<BookingTicketDTO> GetBookingTicket(string TicketID, string CustomerID, string FLigthID, int Status)
        {
            List<BookingTicketDTO> data = new List<BookingTicketDTO>(); 
            SqlConnection con = SqlConnectionData.Connect();
            this.state = string.Empty;
            try
            {
                con.Open();
                string query = @"select TicketID, FlightID, ID, TicketClassID, TicketStatus, BookingDate
                                from BOOKING_TICKET
                                where (isDeleted = 0)
                                AND (@TicketID is NULL or @TicketID = TicketID)
                                AND (@CustomerID is NULL or @CustomerID = ID)
                                AND (@FLigthID is NULL or @FLigthID = FLigthID)
                                AND (@Status is NULL or @Status = TicketStatus)";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@TicketID", TicketID ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CustomerID", CustomerID ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@FLigthID", FLigthID ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Status", Status == -1  ? (object)DBNull.Value : Status);


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BookingTicketDTO dto = new BookingTicketDTO() 
                            {
                                TicketID = reader["TicketID"].ToString(),
                                FlightID = reader["FlightID"].ToString(),
                                ID = reader["ID"].ToString(),
                                TicketClassID = reader["TicketClassID"].ToString(),
                                TicketStatus = Convert.ToInt32(reader["status"]),
                                BookingDate = Convert.ToDateTime(reader["BookingDate"])
                            };
                            data.Add(dto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                state = $"Error: {ex.Message}";
            }
            con.Close();
            return data;
        }
        public (List<ReportByFlightDTO> reportByFlightDTOs, int total) GetReportByFlightDAL(int month, int year)
        {
            List<ReportByFlightDTO> data = new List<ReportByFlightDTO>();
            int sum = 0;
            SqlConnection con = SqlConnectionData.Connect();
            this.state = string.Empty;
            try
            {
                con.Open();
                //lay ma chuyen bay, voi moi ma lay doanh thu cua chuyen bay do, so ve va tong doanh thu cac chuyen bay
                string query = @"select F.FlightID, B.TONG_DOANH_THU2, SUM(F.Price * TC.BaseMultiplier) AS TONG_DOANH_THU, COUNT(BT.ID) AS SO_LUONG_VE
                                from BOOKING_TICKET BT, FLIGHT F, TICKET_CLASS TC, 
			                            (SELECT SUM(F2.Price * TC2.BaseMultiplier)	AS TONG_DOANH_THU2
						                FROM BOOKING_TICKET BT2, FLIGHT F2, TICKET_CLASS TC2
						                WHERE BT2.TicketClassID = TC2.TicketClassID AND BT2.FlightID = F2.FlightID
						                AND F2.isDeleted = 0 AND BT2.isDeleted = 0 AND TC2.isDeleted = 0) AS B
                                WHERE BT.TicketClassID = TC.TicketClassID AND BT.FlightID = F.FlightID
	                            AND F.isDeleted = 0 AND BT.isDeleted = 0 AND TC.isDeleted = 0
                                AND YEAR(BT.BookingDate) = @Year
                                AND MONTH(BT.BookingDate) = @Month
                                GROUP BY F.FlightID, B.TONG_DOANH_THU2";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Year", year);
                    command.Parameters.AddWithValue("@Month", month);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sum = Convert.ToInt32(reader["B.TONG_DOANH_THU2"]);
                            ReportByFlightDTO dto = new ReportByFlightDTO()
                            {
                                flightID = reader["F.FlightID"].ToString(),
                                ticketsSold = Convert.ToInt32(reader["SO_LUONG_VE"]),
                                revenue = Convert.ToDecimal(reader["B.TONG_DOANH_THU"]),
                                ratio = Convert.ToDecimal(reader["B.TONG_DOANH_THU"]) / Convert.ToDecimal(reader["B.TONG_DOANH_THU2"])
                            };
                            data.Add(dto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                state = $"Error: {ex.Message}";
            }
            con.Close();
            return (data, sum);
        }

        public (List<ReportByMonthDTO> reportByMonthDTOs, int total) GetReportByMonthDAL(int Year)
        {
            List<ReportByMonthDTO> data = new List<ReportByMonthDTO>();
            int sum = 0;
            SqlConnection con = SqlConnectionData.Connect();
            this.state = string.Empty;
            try
            {
                con.Open();
                
                // vô sql coi giải thích comment 
                string query = @"
                        select 
                                    MONTH(BT.BookingDate) as thang, YEAR(BT.BookingDate) as nam, 
                                    SUM(F.Price * TC.BaseMultiplier) AS DOANH_THU_THEO_THANG,
                                    COUNT(F.FlightID) AS SO_CHUYEN_BAY, DOANH_THU_CA_NAM

                        from 
                                    BOOKING_TICKET BT, FLIGHT F, TICKET_CLASS TC, 

                                    (
                                    select 
                                        SUM(F2.Price * TC2.BaseMultiplier) AS DOANH_THU_CA_NAM
	                                from 
                                        BOOKING_TICKET BT2, FLIGHT F2, TICKET_CLASS TC2
                                    where 
                                        BT2.FlightID = F2.FlightID AND BT2.TicketClassID = TC2.TicketClassID
                                        AND BT2.isDeleted = 0 AND F2.isDeleted = 0 AND TC2.isDeleted = 0
                                    ) AS A
                        where 
                                    BT.FlightID = F.FlightID 
                                    AND BT.TicketClassID = TC.TicketClassID
                                    AND (YEAR(BT.BookingDate) = @Year)
                                    AND (BT.isDeleted = 0 AND F.isDeleted = 0 AND TC.isDeleted = 0)

                        group by 
                                    MONTH(BT.BookingDate) as thang, YEAR(BT.BookingDate) as nam, DOANH_THU_CA_NAM";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Year", Year);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sum = Convert.ToInt32(reader["DOANH_THU_CA_NAM"]);
                            ReportByMonthDTO dto = new ReportByMonthDTO()
                            {
                                time = new DateTime(Convert.ToInt32(reader["nam"]), Convert.ToInt32(reader["thang"]), 1),
                                flightQuantity = Convert.ToInt32(reader["SO_CHUYEN_BAY"]),
                                revenue = Convert.ToDecimal(reader["DOANH_THU_THEO_THANG"]),
                                ratio = Convert.ToDecimal(reader["DOANH_THU_THEO_THANG"]) / Convert.ToDecimal(reader["DOANH_THU_CA_NAM"])
                            }; data.Add(dto);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                state = $"Error: {ex.Message}";
            }

            con.Close();
            return (data, sum);
        }

        public string GetState()
        {
            return this.state;
        }
    }
}
