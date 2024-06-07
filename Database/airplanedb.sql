﻿CREATE DATABASE airplanedb
USE airplanedb


CREATE TABLE PERMISSION
(
	PermissionID INT PRIMARY KEY ,
	PermissionName VARCHAR(40),
	isDeleted int
)
select * from ACCOUNT
CREATE TABLE ACCOUNT
(
	UserID VARCHAR(20) PRIMARY KEY,
	UserName NVARCHAR(40) NOT NULL,
	Phone VARCHAR(20) NULL,
	Email VARCHAR(60) NULL,
	Birth SMALLDATETIME NOT NULL,
	PasswordUser VARCHAR(60) NOT NULL,
	PermissionID INT FOREIGN KEY REFERENCES PERMISSION(PermissionID),
	isDeleted int
)



CREATE TABLE AIRPORT
(
	AirportID VARCHAR(20) PRIMARY KEY,
	AirportName NVARCHAR(40) NOT NULL,
	isDeleted int
)

CREATE TABLE TICKET_CLASS
(
	TicketClassID VARCHAR(20) PRIMARY KEY,
	TicketClassName NVARCHAR(40) NOT NULL,
	BaseMultiplier FLOAT NOT NULL,
	isDeleted int,
)

CREATE TABLE FLIGHT
(
	FlightID VARCHAR(20) PRIMARY KEY,
	SourceAirportID VARCHAR(20) NOT NULL,
	DestinationAirportID VARCHAR(20) NOT NULL,
	FlightDay SMALLDATETIME NOT NULL,
	FlightTime TIME NOT NULL,
	Price MONEY NOT NULL,
	isDeleted int,
	FOREIGN KEY (SourceAirportID) REFERENCES AIRPORT(AirportID),
	FOREIGN KEY (DestinationAirportID) REFERENCES AIRPORT(AirportID)
)

CREATE TABLE CUSTOMER
(
	ID VARCHAR(20) PRIMARY KEY,
	CustomerName NVARCHAR(40) NOT NULL,
	Phone VARCHAR(20) NULL,
	Email VARCHAR(60) NULL,
	Birth SMALLDATETIME NOT NULL,
	isDeleted int,
)

CREATE TABLE BOOKING_TICKET
(
	TicketID VARCHAR(20) PRIMARY KEY NOT NULL,
    FlightID VARCHAR(20) NOT NULL,
    ID VARCHAR(20) NOT NULL,
    TicketClassID VARCHAR(20) NOT NULL,
    TicketStatus INT NOT NULL,
    BookingDate SMALLDATETIME NOT NULL,
	isDeleted int,
    FOREIGN KEY (FlightID) REFERENCES Flight(FlightID),
    FOREIGN KEY (ID) REFERENCES CUSTOMER(ID),
    FOREIGN KEY (TicketClassID) REFERENCES TICKET_CLASS(TicketClassID)
)

CREATE TABLE INTERMEDIATE_AIRPORT
(
	AirportID VARCHAR(20) FOREIGN KEY REFERENCES AIRPORT(AirportID),
	FlightID VARCHAR(20) FOREIGN KEY REFERENCES FLIGHT(FlightID),
	layoverTime TIME NOT NULL, --loi chinh ta
	Note NVARCHAR(100) NULL,
	isDeleted int,
	PRIMARY KEY(AirportID, FlightID)
)

CREATE TABLE TICKETCLASS_FLIGHT
(
	TicketClassID VARCHAR(20) FOREIGN KEY REFERENCES TICKET_CLASS(TicketClassID),
	FlightID VARCHAR(20) FOREIGN KEY REFERENCES FLIGHT(FlightID),
	Quantity INT,
	Multiplier FLOAT NOT NULL,
	isDeleted int,
	PRIMARY KEY (TicketClassID, FlightID)
)

CREATE TABLE PARAMETER
(
    AirportCount            int,            -- Number of airports
    MinFlightTime			time,
	IntermediateAirportCount int,           -- Number of intermediate airports
    MinStopTime             time,            -- Minimum stop time
    MaxStopTime             time,            -- Maximum stop time
    TicketClassCount        int,            -- Number of ticket class
    SlowestBookingTime      time,           -- Slowest booking time
    CancelTime              time,            -- Cancellation time
	isDeleted int,
);

delete from ACCOUNT

select *from FLIGHT
select *from AIRPORT
select *from ACCOUNT
select *from TICKET_CLASS
select *from TICKETCLASS_FLIGHT
select *from BOOKING_TICKET
select *from CUSTOMER
select *from PARAMETER
----------TEST CASE--------
--PERMISSION
INSERT INTO PERMISSION VALUES (1, 'Admin', 0);
INSERT INTO PERMISSION VALUES (2, 'Staff', 0);

--ACCOUNT

-- tk: admin@example.com; pass: password1
-- tk: staff1@example.com; pass: password2
-- tk: admin1@example.com; pass: password3
INSERT INTO ACCOUNT VALUES ('000', 'admin', '0123456789', 'admin@example.com', '1980-01-01', '7c6a180b36896a0a8c02787eeafb0e4c', 1, 0);
INSERT INTO ACCOUNT VALUES ('001', 'staff1', '0123456790', 'staff1@example.com', '1985-02-02', '6cb75f652a9b52798eb6cf2201057c73', 2, 0);
INSERT INTO ACCOUNT VALUES ('002', 'staff2', '0123456791', 'admin1@example.com', '1990-03-03', '819b0643d6b89dc9b579fdfc9094f28e', 2, 0);

INSERT INTO ACCOUNT VALUES ('003', 'Quan', '0987654321', 'quan@gmail.com', '2004-10-14', 'e80b5017098950fc58aad83c8c14978e', 1, 0);
INSERT INTO ACCOUNT VALUES ('005', 'Quan', '0987654322', 'quan2@gmail.com', '2004-10-14', 'e80b5017098950fc58aad83c8c14978e', 2, 0);


INSERT INTO ACCOUNT VALUES ('004', 'admin', '1', '1@gmail.com', '1980-01-01', 'c4ca4238a0b923820dcc509a6f75849b', 1, 0);
--AIRPORT 
INSERT INTO AIRPORT VALUES ('000', N'Nội Bài', 0);
INSERT INTO AIRPORT VALUES ('001', N'Tân Sơn Nhất', 0);
INSERT INTO AIRPORT VALUES ('002', N'Đà Nẵng', 0);
insert into AIRPORT values ('003',N'Phú Quốc', 0)
insert into AIRPORT values ('004',N'Cam Ranh', 0)
insert into AIRPORT values ('005',N'Điện Biên Phủ', 0)
INSERT INTO AIRPORT VALUES ('006', N'Cần Thơ', 0);
INSERT INTO AIRPORT VALUES ('007', N'Vinh', 0);
INSERT INTO AIRPORT VALUES ('008', N'Hải Phòng', 0);
INSERT INTO AIRPORT VALUES ('009', N'Phù Cát', 0);

--TICKET_CLASS
INSERT INTO TICKET_CLASS VALUES ('001', N'Economy', 1.0, 0);
INSERT INTO TICKET_CLASS VALUES ('002', N'Business', 1.5, 0);
INSERT INTO TICKET_CLASS VALUES ('003', N'First Class', 2.0, 0);

--FLIGHT
INSERT INTO FLIGHT VALUES ('FL001', '000', '001', '2024-06-01', '08:00:00', 100.0, 0);
INSERT INTO FLIGHT VALUES ('FL002', '001', '002', '2024-06-02', '09:00:00', 150.0, 0);
INSERT INTO FLIGHT VALUES ('FL003', '002', '000', '2024-06-03', '10:00:00', 200.0, 0);

--CUSTOMER
INSERT INTO CUSTOMER VALUES ('12345678901', 'Nguyen Van A', '0123456789', 'nva@example.com', '1990-01-01', 0);
INSERT INTO CUSTOMER VALUES ('12345678902', 'Tran Thi B', '0123456790', 'ttb@example.com', '1992-02-02', 0);
INSERT INTO CUSTOMER VALUES ('12345678903', 'Le Van C', '0123456791', 'lvc@example.com', '1994-03-03', 0);
INSERT INTO CUSTOMER VALUES ('12345678904', 'Hoang Thi D', '0123456792', 'htd@example.com', '1996-04-04', 0);
INSERT INTO CUSTOMER VALUES ('12345678905', 'Pham Van E', '0123456793', 'pve@example.com', '1998-05-05', 0);
INSERT INTO CUSTOMER VALUES ('12345678906', 'Vu Thi F', '0123456794', 'vtf@example.com', '2000-06-06', 0);
INSERT INTO CUSTOMER VALUES ('12345678907', 'Dang Van G', '0123456795', 'dvg@example.com', '2002-07-07', 0);
INSERT INTO CUSTOMER VALUES ('12345678908', 'Mai Thi H', '0123456796', 'mth@example.com', '1988-08-08', 0);
--BOOKING_TICKET
INSERT INTO BOOKING_TICKET VALUES ('BT001', 'FL001', '12345678901', '001', 1, '2024-05-01', 0);
INSERT INTO BOOKING_TICKET VALUES ('BT002', 'FL002', '12345678902', '002', 1, '2024-05-02', 0);
INSERT INTO BOOKING_TICKET VALUES ('BT003', 'FL003', '12345678903', '003', 1, '2024-05-03', 0);
INSERT INTO BOOKING_TICKET VALUES ('BT004', 'FL003', '12345678904', '003', 1, '2024-05-03', 0);
INSERT INTO BOOKING_TICKET VALUES ('BT005', 'FL002', '12345678905', '002', 1, '2024-05-03', 0);
INSERT INTO BOOKING_TICKET VALUES ('BT006', 'FL001', '12345678906', '002', 1, '2024-05-03', 0);
INSERT INTO BOOKING_TICKET VALUES ('BT007', 'FL002', '12345678907', '001', 1, '2024-05-03', 0);
INSERT INTO BOOKING_TICKET VALUES ('BT008', 'FL002', '12345678908', '001', 1, '2024-05-03', 0);

--INTERMEDIATE_AIRPORT
INSERT INTO INTERMEDIATE_AIRPORT VALUES ('001', 'FL001', '01:00:00', 'Brief stop', 0);
INSERT INTO INTERMEDIATE_AIRPORT VALUES ('002', 'FL002', '01:30:00', 'Refuel', 0);
INSERT INTO INTERMEDIATE_AIRPORT VALUES ('000', 'FL003', '02:00:00', 'Maintenance', 0);

--TICKETCLASS_FLIGHT
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL001', 30, 1.0, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL001', 20, 1.5, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL002', 30, 1.0, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL002', 30, 1.5, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL003', 20, 2.0, 0);

--PARAMETER

INSERT INTO PARAMETER VALUES (10, '08:00:00', 2, '10:00:00', '20:00:00', 2, '07:00:00', '06:00:00', 0)

