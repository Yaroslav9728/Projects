CREATE TABLE `Bus` (
	`BusID` INT NOT NULL,
	`ModelID` INT NOT NULL,
	`RegisrtyNumber` varchar(50) NOT NULL,
	`AmountSeats` INT NOT NULL,
	PRIMARY KEY (`BusID`)
);

CREATE TABLE `Model` (
	`ModelID` int NOT NULL,
	`ModelName` varchar(50) NOT NULL,
	PRIMARY KEY (`ModelID`)
);

CREATE TABLE `Sale` (
	`SaleID` int NOT NULL,
	`ScheduleID` int NOT NULL,
	`DateofSaling` DATETIME NOT NULL,
	`AmountTickets` int NOT NULL,
	`PriceSum` int NOT NULL,
	PRIMARY KEY (`SaleID`)
);

CREATE TABLE `Schedule` (
	`ScheduleID` int NOT NULL,
	`TripID` int NOT NULL,
	`DepartureDate` DATETIME NOT NULL,
	`SuccessTrip` bit NOT NULL,
	PRIMARY KEY (`ScheduleID`)
);

CREATE TABLE `Trip` (
	`TripID` int NOT NULL,
	`CityArrival` int NOT NULL,
	`CityDeparture` int NOT NULL,
	`BusID` int NOT NULL,
	`TimeArrival` DATETIME NOT NULL,
	`Distance` int NOT NULL,
	`TicketPrice` FLOAT NOT NULL,
	`TimeDeparture` DATETIME NOT NULL,
	PRIMARY KEY (`TripID`)
);

CREATE TABLE `CityArrival` (
	`CityArrivalID` int NOT NULL,
	`CityArrivalName` varchar(50) NOT NULL,
	PRIMARY KEY (`CityArrivalID`)
);

CREATE TABLE `CityDeparture` (
	`CityDepartureID` int NOT NULL,
	`CityDepartureName` varchar(50) NOT NULL,
	PRIMARY KEY (`CityDepartureID`)
);

ALTER TABLE `Bus` ADD CONSTRAINT `Bus_fk0` FOREIGN KEY (`ModelID`) REFERENCES `Model`(`ModelID`);

ALTER TABLE `Sale` ADD CONSTRAINT `Sale_fk0` FOREIGN KEY (`ScheduleID`) REFERENCES `Schedule`(`ScheduleID`);

ALTER TABLE `Schedule` ADD CONSTRAINT `Schedule_fk0` FOREIGN KEY (`TripID`) REFERENCES `Trip`(`TripID`);

ALTER TABLE `Trip` ADD CONSTRAINT `Trip_fk0` FOREIGN KEY (`CityArrival`) REFERENCES `CityArrival`(`CityArrivalID`);

ALTER TABLE `Trip` ADD CONSTRAINT `Trip_fk1` FOREIGN KEY (`CityDeparture`) REFERENCES `CityDeparture`(`CityDepartureID`);

ALTER TABLE `Trip` ADD CONSTRAINT `Trip_fk2` FOREIGN KEY (`BusID`) REFERENCES `Bus`(`BusID`);

