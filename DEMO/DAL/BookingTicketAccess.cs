﻿using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class BookingTicketAccess
    {
        public BookingTicketAccess() { }
        public string AutoID()
        {
            SqlConnection con = SqlConnectionData.Connect();
            con.Open();

            SqlCommand cmd = new SqlCommand("select *from BOOKING_TICKET", con);
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
                        cmd.CommandText = "INSERT INTO BOOKING_TICKET (TicketID, FlightID, ID, TicketClassID, TicketStatus, BookingDate) VALUES (@TID, @FID, @ID, @TCID, @Status, @date)";
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
    }
}