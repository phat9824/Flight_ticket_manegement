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
<<<<<<< HEAD
select *from AIRPORT
select *from ACCOUNT
select *from FLIGHT
=======
>>>>>>> 2ec5174f66589509a2857ba12d6e4e27a8020238

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

INSERT INTO CUSTOMER (ID, CustomerName, Phone, Email, Birth, FlightID, AirportID) VALUES
('C001', 'Nguyễn Văn A', 123456789, 'a@gmail.com', '1990-01-01', 'FL001', '000'),
('C002', 'Trần Thị B', 123456788, 'b@gmail.com', '1991-02-02', 'FL002', '000'),
('C003', 'Lê Văn C', 123456787, 'c@gmail.com', '1992-03-03', 'FL003', '001'),
('C004', 'Phạm Thị D', 123456786, 'd@gmail.com', '1993-04-04', 'FL004', '002'),
('C005', 'Hoàng Văn E', 123456785, 'e@gmail.com', '1994-05-05', 'FL005', '003'),
('C006', 'Nguyễn Thị F', 123456784, 'f@gmail.com', '1995-06-06', 'FL006', '004'),
('C007', 'Trần Văn G', 123456783, 'g@gmail.com', '1996-07-07', 'FL007', '005'),
('C008', 'Lê Thị H', 123456782, 'h@gmail.com', '1997-08-08', 'FL008', '006'),
('C009', 'Phạm Văn I', 123456781, 'i@gmail.com', '1998-09-09', 'FL009', '007'),
('C010', 'Hoàng Thị J', 123456780, 'j@gmail.com', '1999-10-10', 'FL010', '008'),
('C011', 'Nguyễn Văn K', 123456779, 'k@gmail.com', '2000-11-11', 'FL011', '009'),
('C012', 'Trần Thị L', 123456778, 'l@gmail.com', '2001-12-12', 'FL012', '001'),
('C013', 'Lê Văn M', 123456777, 'm@gmail.com', '2002-01-13', 'FL013', '002'),
('C014', 'Phạm Thị N', 123456776, 'n@gmail.com', '2003-02-14', 'FL014', '003'),
('C015', 'Hoàng Văn O', 123456775, 'o@gmail.com', '2004-03-15', 'FL015', '004'),
('C016', 'Nguyễn Thị P', 123456774, 'p@gmail.com', '2005-04-16', 'FL016', '005'),
('C017', 'Trần Văn Q', 123456773, 'q@gmail.com', '2006-05-17', 'FL017', '006'),
('C018', 'Lê Thị R', 123456772, 'r@gmail.com', '2007-06-18', 'FL018', '007'),
('C019', 'Phạm Văn S', 123456771, 's@gmail.com', '2008-07-19', 'FL019', '008'),
('C020', 'Hoàng Thị T', 123456770, 't@gmail.com', '2009-08-20', 'FL020', '009');

INSERT INTO TICKET_CLASS (TicketClassID, TicketClassName) VALUES
('TC01', 'Economy'),
('TC02', 'Economy Plus'),
('TC03', 'Business'),
('TC04', 'First Class'),
('TC05', 'Premium Economy'),
('TC06', 'Standard'),
('TC07', 'Flex'),
('TC08', 'Business Standard'),
('TC09', 'Business Flex'),
('TC10', 'First Class Flex'),
('TC11', 'Economy Saver'),
('TC12', 'Economy Premium'),
('TC13', 'Business Saver'),
('TC14', 'First Class Saver'),
('TC15', 'Advanced Economy'),
('TC16', 'Advanced Business'),
('TC17', 'Elite Economy'),
('TC18', 'Elite Business'),
('TC19', 'Luxury Economy'),
('TC20', 'Luxury Business');

INSERT INTO SELLING_TICKET (FlightID, ID, TicketClassID, AirportID, SellingDate, CustomerName, Phone, Email) VALUES
('FL001', 'C001', 'TC01', '000', '2023-04-14', 'Nguyen Van A', 123456789, 'a.nguyen@gmail.com'),
('FL002', 'C002', 'TC02', '001', '2023-04-15', 'Tran Thi B', 987654321, 'b.tran@gmail.com'),
('FL003', 'C003', 'TC01', '002', '2023-04-16', 'Le Van C', 456789123, 'c.le@gmail.com'),
('FL004', 'C004', 'TC02', '003', '2023-04-17', 'Pham Thi D', 321654987, 'd.pham@gmail.com'),
('FL005', 'C005', 'TC01', '004', '2023-04-18', 'Hoang Van E', 654321789, 'e.hoang@gmail.com'),
('FL006', 'C006', 'TC02', '005', '2023-04-19', 'Bui Thi F', 789456123, 'f.bui@gmail.com'),
('FL007', 'C007', 'TC01', '006', '2023-04-20', 'Vo Van G', 147258369, 'g.vo@gmail.com'),
('FL008', 'C008', 'TC02', '007', '2023-04-21', 'Nguyen Thi H', 258369147, 'h.nguyen@gmail.com'),
('FL009', 'C009', 'TC01', '008', '2023-04-22', 'Tran Van I', 369147258, 'i.tran@gmail.com'),
('FL010', 'C010', 'TC02', '009', '2023-04-23', 'Le Thi J', 321987654, 'j.le@gmail.com'),
('FL011', 'C011', 'TC01', '000', '2023-04-24', 'Pham Van K', 987321456, 'k.pham@gmail.com'),
('FL012', 'C012', 'TC02', '001', '2023-04-25', 'Hoang Thi L', 654987321, 'l.hoang@gmail.com'),
('FL013', 'C013', 'TC01', '002', '2023-04-26', 'Bui Van M', 789123456, 'm.bui@gmail.com'),
('FL014', 'C014', 'TC02', '003', '2023-04-27', 'Vo Thi N', 456123789, 'n.vo@gmail.com'),
('FL015', 'C015', 'TC01', '004', '2023-04-28', 'Nguyen Van O', 123789456, 'o.nguyen@gmail.com'),
('FL016', 'C016', 'TC02', '005', '2023-04-29', 'Tran Thi P', 987654123, 'p.tran@gmail.com'),
('FL017', 'C017', 'TC01', '006', '2023-04-30', 'Le Van Q', 321456987, 'q.le@gmail.com'),
('FL018', 'C018', 'TC02', '007', '2023-05-01', 'Pham Thi R', 654123987, 'r.pham@gmail.com'),
('FL019', 'C019', 'TC01', '008', '2023-05-02', 'Hoang Van S', 789654123, 's.hoang@gmail.com')

INSERT INTO TICKETCLASS_FLIGHT (TicketClassID, FlightID, Quantity) VALUES
('TC01', 'FL001', 150),
('TC02', 'FL001', 50),
('TC03', 'FL001', 20),
('TC04', 'FL001', 10),
('TC05', 'FL002', 150),
('TC06', 'FL002', 50),
('TC07', 'FL002', 20),
('TC08', 'FL002', 10),
('TC09', 'FL003', 150),
('TC10', 'FL003', 50),
('TC11', 'FL003', 20),
('TC12', 'FL003', 10),
('TC13', 'FL004', 150),
('TC14', 'FL004', 50),
('TC15', 'FL004', 20),
('TC16', 'FL004', 10),
('TC17', 'FL005', 150),
('TC18', 'FL005', 50),
('TC19', 'FL005', 20),
('TC20', 'FL005', 10);

select *from AIRPORT
select *from ACCOUNT
delete from AIRPORT
delete from FLIGHT
