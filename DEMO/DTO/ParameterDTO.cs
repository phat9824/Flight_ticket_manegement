﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ParameterDTO
    {
        private int airportCount;
        private TimeSpan departureTime;
        private int intermediateAirportCount;
        private int ticketClassCount;
        private int minStopTime;
        private int maxStopTime;
        private int seatCount;
        private TimeSpan slowestBookingTime;
        private TimeSpan cancelTime;

        public int AirportCount
        {
            get => airportCount;
            set => airportCount = value;
        }

        public TimeSpan DepartureTime
        {
            get => departureTime;
            set => departureTime = value;
        }

        public int IntermediateAirportCount
        {
            get => intermediateAirportCount;
            set => intermediateAirportCount = value;
        }

        public int TicketClassCount
        {
            get => ticketClassCount;
            set => ticketClassCount = value;
        }

        public int MinStopTime
        {
            get => minStopTime;
            set => minStopTime = value;
        }

        public int MaxStopTime
        {
            get => maxStopTime;
            set => maxStopTime = value;
        }

        public TimeSpan SlowestBookingTime
        {
            get => slowestBookingTime;
            set => slowestBookingTime = value;
        }

        public TimeSpan CancelTime
        {
            get => cancelTime;
            set => cancelTime = value;
        }
    }
}
