using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.ComponentModel;

namespace BLL
{
    public class InsertProcessor
    {
        public InsertProcessor() { }
        // luu booking ticket vao db
        public string Add_BookingTicket(CustomerDTO customer, FlightDTO flight, TicketClassDTO ticketClass, DateTime date, int status)
        {
            // luu khach hang
            string processState_InsertCustomer = new DAL.CustomerAsccess().Add_Customer(customer);
            if (processState_InsertCustomer != string.Empty)
            {
                return processState_InsertCustomer + "_BLL_processState_InsertCustomer";
            }
            // luu ve
            string processState_InsertBookingTicket = new DAL.BookingTicketAccess().Add_BookingTicket(customer.ID, flight.FlightID, ticketClass.TicketClassID, status, date);
            if (processState_InsertBookingTicket != string.Empty)
            {
                return processState_InsertCustomer + "_BLL_processState_InsertBookingTicket";
            }
            return string.Empty; // Chuỗi rỗng xem như thành công
        }
        // luu tai khoan vao db
        public void SignUp(ACCOUNT User, ref string kq)
        {
            kq = new DAL.AccountAccess().SignUp(User);
        }
        // luu thong tin khach hang vao db
        public string Add_Customer(List<CustomerDTO> customer)
        {
            string kq = "";
            foreach (CustomerDTO dto in customer)
            {
                if(!new DAL.CustomerAsccess().isExits(dto))
                {
                    kq = new DAL.CustomerAsccess().Add_Customer(dto);
                }
                else
                {
                    return "ID da ton tai";
                }
            }
            return kq;
        }
        // luu thong tin ticket class flight vao db
        public string InsertTicketClassFlight(List<TicketClassFlightDTO> listTicketClassFlightDTO)
        {
            return new DAL.TicketClassFlightAccess().insertListTicketClass(listTicketClassFlightDTO);
        }
        // luu intermediate airport vao db
        public string InsertIntermediateAirport(List<IntermediateAirportDTO> listIntermediateAirportDTO)
        {
            return new DAL.IntermidateAirportAccess().insertListItermedateAirport(listIntermediateAirportDTO);
        }
        // luu chuyen bay vao db
        public string AddFlightInfor(FlightDTO flight, List<TicketClassFlightDTO> listTicketClassFlightDTO, List<IntermediateAirportDTO> listIntermediateAirportDTO)
        {
            string processState_InsertFlight = new DAL.FlightAccess().Add_Flights(flight);
            if (processState_InsertFlight != string.Empty)
            {
                return processState_InsertFlight + "_BLL_processState_InsertFlight";
            }

            string processState_InsertTicketClassFlight = new BLL.InsertProcessor().InsertTicketClassFlight(listTicketClassFlightDTO);
            if (processState_InsertTicketClassFlight != string.Empty)
            {
                return processState_InsertTicketClassFlight + "_BLL_processState_InsertTicketClassFlight";
            }
            if (listIntermediateAirportDTO.Count > 0)
            {
                string processState_InsertIntermediateAirport = new BLL.InsertProcessor().InsertIntermediateAirport(listIntermediateAirportDTO);
                if (processState_InsertIntermediateAirport != string.Empty)
                {
                    return processState_InsertIntermediateAirport + "_BLL_processState_InsertIntermediateAirport";
                }
            }
            return string.Empty; // Chuỗi rỗng xem như thành công
        }
        // insert Paramater
        public string InsertParamater(ParameterDTO parameterDTO)
        {
            return new DAL.ParameterAccess().InsertParamater(parameterDTO);
        }
    }
}
