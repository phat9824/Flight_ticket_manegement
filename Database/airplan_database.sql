CREATE DATABASE airplan_database
USE airplan_database


INSERT INTO PERMISSION VALUES ('1', 'Admin')
INSERT INTO PERMISSION VALUES ('2', 'Staff')

CREATE TABLE PERMISSION
(
	PermissionID INT PRIMARY KEY ,
	PermissionName VARCHAR(40)
)


insert into ACCOUNT values ('0','admin','1','abc@gmail.com','2004/09/08','1','1')
insert into ACCOUNT values ('1','s1','1','s1@gmail.com','2004/02/01','2','2')
insert into ACCOUNT values ('2','s2','1','s2@gmail.com','2004/02/04','1','2')

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
CREATE TABLE AIRPORT
(
	AirportID VARCHAR(20) PRIMARY KEY,
	AirportName NVARCHAR(40)
)
CREATE TABLE TICKET_CLASS
(
	TicketClassID VARCHAR(20) PRIMARY KEY,
	TicketClassName NVARCHAR(40)
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
	CustomerName NVARCHAR(40),
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
	CustomerName NVARCHAR(40),
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
select *from AIRPORT
select *from ACCOUNT
select *from FLIGHT

delete from AIRPORT

delete from FLIGHT
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