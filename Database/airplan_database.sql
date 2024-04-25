CREATE DATABASE airplan_database
USE airplan_database


CREATE TABLE PERMISSION
(
	PermissionID INT PRIMARY KEY ,
	PermissionName VARCHAR(40)
)

INSERT INTO PERMISSION VALUES ('1', 'Admin')
INSERT INTO PERMISSION VALUES ('2', 'Staff')

CREATE TABLE ACCOUNT
(
	UserID VARCHAR(20) PRIMARY KEY,
	UserName NVARCHAR(40),
	Phone INT,
	Email VARCHAR(40),
	Birth SMALLDATETIME,
	PasswordUser VARCHAR(40),
	PermissonID INT FOREIGN KEY REFERENCES PERMISSION(PermissionID)
)

insert into ACCOUNT values ('0','admin','1','abc@gmail.com','2004/09/08','1','1')
insert into ACCOUNT values ('1','s1','1','s1@gmail.com','2004/02/01','2','2')
insert into ACCOUNT values ('2','s2','1','s2@gmail.com','2004/02/04','1','2')

CREATE TABLE AIRPORT
(
	AirportID VARCHAR(20) PRIMARY KEY,
	AirportName NVARCHAR(40)
)
CREATE TABLE TICKET_CLASS
(
	TicketClassID VARCHAR(20) PRIMARY KEY,
	TicketClassName VARCHAR(40)
)

CREATE TABLE FLIGHT
(
	FlightID VARCHAR(20) PRIMARY KEY,
	SourceAirportID VARCHAR(20) FOREIGN KEY REFERENCES AIRPORT(AirportID),
	DestinationAirportID VARCHAR(20) FOREIGN KEY REFERENCES AIRPORT(AirportID),
	FlightDay SMALLDATETIME,
	FlightTime TIME,
	Price MONEY
)

CREATE TABLE CUSTOMER
(
	ID VARCHAR(20) PRIMARY KEY,
	CustomerName VARCHAR(40),
	Phone INT,
	Email VARCHAR(40),
	Birth SMALLDATETIME,
	FlightID VARCHAR(20) FOREIGN KEY REFERENCES FLIGHT(FlightID),
	AirportID VARCHAR(20) FOREIGN KEY REFERENCES AIRPORT(AirportID)
)

CREATE TABLE BOOKING_TICKET
(
	FlightID VARCHAR(20) FOREIGN KEY REFERENCES Flight(FlightID),
	ID VARCHAR(20) FOREIGN KEY REFERENCES CUSTOMER(ID),
	TicketClassID VARCHAR(20) FOREIGN KEY REFERENCES TICKET_CLASS(TicketClassID),
	TicketStatus INT,
	BookingDate SMALLDATETIME,
	PRIMARY KEY(FlightID, ID)
)

CREATE TABLE INTERMEDIATE_AIRPORT
(
	AirportID VARCHAR(20) FOREIGN KEY REFERENCES AIRPORT(AirportID),
	FlightID VARCHAR(20) FOREIGN KEY REFERENCES FLIGHT(FlightID),
	layoverTime TIME,
	Note VARCHAR(100),
	PRIMARY KEY(AirportID, FlightID)
)

CREATE TABLE SELLING_TICKET
(
	FlightID VARCHAR(20) FOREIGN KEY REFERENCES FLIGHT(FlightID),
	ID VARCHAR(20) FOREIGN KEY REFERENCES CUSTOMER(ID),
	TicketClassID VARCHAR(20) FOREIGN KEY REFERENCES TICKET_CLASS(TicketClassID),
	AirportID VARCHAR(20) FOREIGN KEY REFERENCES AIRPORT(AirportID),
	SellingDate SMALLDATETIME,
	CustomerName VARCHAR(40),
	Phone INT,
	Email VARCHAR(40),
	PRIMARY KEY(FlightID, ID)
)

CREATE TABLE TICKETCLASS_FLIGHT
(
	TicketClassID VARCHAR(20) FOREIGN KEY REFERENCES TICKET_CLASS(TicketClassID),
	FLightID VARCHAR(20) FOREIGN KEY REFERENCES FLIGHT(FlightID),
	Quantity INT,
	PRIMARY KEY (TicketClassID, FlightID)
)
CREATE TABLE PARAMETER
(
    AirportCount            int,            -- Number of airports
    DepartureTime           time,           -- Departure time
    IntermediateAirportCount int,           -- Number of intermediate airports
    MinStopTime             int,            -- Minimum stop time
    MaxStopTime             int,            -- Maximum stop time
    TicketClassCount        int,            -- Number of ticket class
    SlowestBookingTime      time,           -- Slowest booking time
    CancelTime              time            -- Cancellation time
);

insert into AIRPORT values ('000',N'Nội Bài')
insert into AIRPORT values ('001',N'Tân Sơn Nhất')
insert into AIRPORT values ('002',N'Đà Nẵng')
insert into AIRPORT values ('003',N'Phú Quốc')
insert into AIRPORT values ('004',N'Cam Ranh')
insert into AIRPORT values ('005',N'Điện Biên Phủ')
insert into AIRPORT values ('006',N'sample1')
insert into AIRPORT values ('007',N'sample2')
insert into AIRPORT values ('008',N'sample3')
insert into AIRPORT values ('009',N'sample4')

INSERT INTO FLIGHT (FlightID, SourceAirportID, DestinationAirportID, FlightDay, FlightTime, Price) VALUES
('FL001', '000', '001', '2023-04-15', '06:00:00', 3000000),
('FL002', '000', '002', '2023-04-16', '09:30:00', 1500000),
('FL003', '001', '003', '2023-04-17', '11:00:00', 3500000),
('FL004', '002', '004', '2023-04-18', '13:45:00', 1800000),
('FL005', '003', '005', '2023-04-19', '15:30:00', 2200000),
('FL006', '004', '006', '2023-04-20', '17:15:00', 2800000),
('FL007', '005', '007', '2023-04-21', '19:00:00', 2500000),
('FL008', '006', '008', '2023-04-22', '20:45:00', 1900000),
('FL009', '007', '009', '2023-04-23', '22:30:00', 2300000),
('FL010', '008', '000', '2023-04-24', '12:15:00', 3100000),
('FL011', '009', '001', '2023-04-25', '05:00:00', 2000000),
('FL012', '001', '002', '2023-04-26', '07:45:00', 3200000),
('FL013', '002', '003', '2023-04-27', '10:30:00', 1500000),
('FL014', '003', '004', '2023-04-28', '14:00:00', 2600000),
('FL015', '004', '005', '2023-04-29', '16:20:00', 1750000),
('FL016', '005', '006', '2023-04-30', '18:55:00', 2850000),
('FL017', '006', '007', '2023-05-01', '21:10:00', 1950000),
('FL018', '007', '008', '2023-05-02', '23:25:00', 3050000),
('FL019', '008', '009', '2023-05-03', '01:40:00', 2450000),
('FL020', '009', '000', '2023-05-04', '03:55:00', 2150000);


select *from AIRPORT
select *from ACCOUNT
delete from AIRPORT
delete from FLIGHT
