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
        public string InsertTicketClassFlight(List<TicketClassFlightDTO> listTicketClassFlightDTO)
        {
            return new DAL.TicketClassFlightAccess().insertListTicketClass(listTicketClassFlightDTO);
        }

        public string InsertIntermediateAirport(List<IntermediateAirportDTO> listIntermediateAirportDTO) 
        {
            return new DAL.IntermidateAirportAccess().insertListItermedateAirport(listIntermediateAirportDTO);
        }
        
        public string AddFlightInfor(FlightDTO flight, List<TicketClassFlightDTO> listTicketClassFlightDTO, List<IntermediateAirportDTO> listIntermediateAirportDTO)
        {
            string processState_InsertFlight = new BLL.Flight_BLL().Add_Flights(flight);
            if (processState_InsertFlight != string.Empty)
            {
                return processState_InsertFlight + "_BLL_processState_InsertFlight";
            }

            string processState_InsertTicketClassFlight = new BLL.InsertProcessor().InsertTicketClassFlight(listTicketClassFlightDTO);
            if (processState_InsertTicketClassFlight != string .Empty)
            { 
                return processState_InsertTicketClassFlight + "_BLL_processState_InsertTicketClassFlight";
            }

            string processState_InsertIntermediateAirport = new BLL.InsertProcessor().InsertIntermediateAirport(listIntermediateAirportDTO);
            if (processState_InsertIntermediateAirport != string.Empty) 
            {
                return processState_InsertIntermediateAirport + "_BLL_processState_InsertIntermediateAirport";
            }
            return string.Empty; // Chuỗi rỗng xem như thành công
        }
    }
}
