-- CREATE

CREATE TABLE Client (
    Id int NOT NULL,
    Pesel varchar(11) NOT NULL,
    CONSTRAINT Client_Pk PRIMARY KEY(Id)
);

CREATE TABLE Room (
    Number int NOT NULL,
    RoomType varchar(10) NOT NULL,
    Price numeric(6,2) NOT NULL
    CONSTRAINT Room_Pk PRIMARY KEY(Number)
);

CREATE TABLE Reservation (
    Id int NOT NULL,
    ClientId int NOT NULL,
    RoomId int NOT NULL,
    StartDate Datetime NOT NULL,
    EndDate Datetime NOT NULL,
    Price numeric(8,2) NOT NULL
    CONSTRAINT Reservation_Pk PRIMARY KEY(Id)
);

CREATE TABLE Convenience (
    Id int NOT NULL,
    Name varchar(50) NOT NULL,
    Price numeric(6,2) NOT NULL
    CONSTRAINT Convenience_Pk PRIMARY KEY(Id)
);

CREATE TABLE ReservationConvenience (
    ReservationId int NOT NULL,
    ConvenienceId int NOT NULL
    CONSTRAINT ReservationConvenience_Pk PRIMARY KEY(ReservationId, ConvenienceId)
);

ALTER TABLE Reservation
ADD CONSTRAINT Fk_Client_Reservation
FOREIGN KEY (ClientId) REFERENCES Client(Id);

ALTER TABLE Reservation
ADD CONSTRAINT Fk_Room_Reservation
FOREIGN KEY (RoomId) REFERENCES Room(Number);

ALTER TABLE ReservationConvenience
ADD CONSTRAINT Fk_Reservation_ReservationConvenience
FOREIGN KEY (ReservationId) REFERENCES Reservation(Id);

ALTER TABLE ReservationConvenience
ADD CONSTRAINT Fk_Convenience_ReservationConvenience
FOREIGN KEY (ConvenienceId) REFERENCES Convenience(Id);

-- INSERT

INSERT INTO Room (Number, RoomType, Price) VALUES (1, 'SGL', 150);
INSERT INTO Room (Number, RoomType, Price) VALUES (2, 'DBL', 250);
INSERT INTO Room (Number, RoomType, Price) VALUES (3, 'TRIPLE', 350);

INSERT INTO Convenience (Id, Name, Price) VALUES (1, 'Telewizor', 50);
INSERT INTO Convenience (Id, Name, Price) VALUES (2, '¯elazko', 20);
INSERT INTO Convenience (Id, Name, Price) VALUES (3, 'Suszarka', 40);
INSERT INTO Convenience (Id, Name, Price) VALUES (4, 'Internet', 100);
INSERT INTO Convenience (Id, Name, Price) VALUES (5, 'SPA', 300);
INSERT INTO Convenience (Id, Name, Price) VALUES (6, 'Œniadanie', 150);
INSERT INTO Convenience (Id, Name, Price) VALUES (7, 'Czajnik', 10);

-- DROP

ALTER TABLE Reservation
DROP CONSTRAINT Fk_Client_Reservation;

ALTER TABLE Reservation
DROP CONSTRAINT Fk_Room_Reservation;

ALTER TABLE ReservationConvenience
DROP CONSTRAINT Fk_Reservation_ReservationConvenience;

ALTER TABLE ReservationConvenience
DROP CONSTRAINT Fk_Convenience_ReservationConvenience;

ALTER TABLE ReservationConvenience
DROP CONSTRAINT ReservationConvenience_Pk;

ALTER TABLE Convenience
DROP CONSTRAINT Convenience_Pk;

ALTER TABLE Room
DROP CONSTRAINT Room_Pk;

ALTER TABLE Client
DROP CONSTRAINT Client_Pk;

DROP TABLE Convenience;
DROP TABLE Reservation;
DROP TABLE ReservationConvenience;
DROP TABLE Room; 
DROP TABLE Client; 
