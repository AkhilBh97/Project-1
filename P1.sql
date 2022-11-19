CREATE SCHEMA P1;
GO

-- ///////////////////DROP TABLES///////////////////////
-- DROP TABLE P1.Tickets
-- DROP TABLE P1.User

-- ///////////////////CREATE TABLES///////////////////////////

-- ///////////////////EMPLOYEES/////////////////////////

CREATE TABLE P1.Employee (
	Email NVARCHAR(255) NOT NULL,
	Password VARBINARY(64) NOT NULL,
	Role NVARCHAR(8) NOT NULL DEFAULT 'Employee',
	CHECK (Role IN ('Employee', 'Manager') AND Email LIKE '%@%.%'),
	PRIMARY KEY (Email)
);

--////////////////////TICKETS/////////////////////////////////

CREATE TABLE P1.Ticket (
	TicketID INT NOT NULL IDENTITY(1,1),
	EmplEmail NVARCHAR(255) NOT NULL,
	Status NVARCHAR(8) NOT NULL DEFAULT 'Pending',
	Description NVARCHAR(255) NOT NULL,
	Amount DECIMAL(18,2) NOT NULL,
	PRIMARY KEY (TicketID)
);

--////////////////////ALTER TABLES/////////////////////////////////
ALTER TABLE P1.Ticket
	ADD CONSTRAINT FK__Ticket__EmplEmail FOREIGN KEY (EmplEmail)
	REFERENCES P1.Employee (Email);