CREATE SCHEMA P1;
GO

-- ///////////////////DROP TABLES///////////////////////
-- DROP TABLE P1.Tickets
-- DROP TABLE P1.Employee

-- ///////////////////CREATE TABLES///////////////////////////

-- ///////////////////EMPLOYEES/////////////////////////

CREATE TABLE P1.Employee (
	ID INT NOT NULL IDENTITY(1,1),
	Email NVARCHAR(255) UNIQUE NOT NULL,
	Password BINARY(20) NOT NULL,
	Role NVARCHAR(8) NOT NULL DEFAULT 'Employee',
	CHECK (Role IN ('Employee', 'Manager') AND Email LIKE '%@%.%'),
	PRIMARY KEY (ID)
);

--////////////////////TICKETS/////////////////////////////////

CREATE TABLE P1.Ticket (
	TicketID INT NOT NULL IDENTITY(1,1),
	EmplID INT NOT NULL,
	Description NVARCHAR(255) NOT NULL,
	Amount DECIMAL(18,2) NOT NULL,
	Status NVARCHAR(8) NOT NULL DEFAULT 'Pending',
    	CHECK (Status in ('Pending', 'Approved', 'Denied')),
	PRIMARY KEY (TicketID)
);

--////////////////////ALTER TABLES/////////////////////////////////
ALTER TABLE P1.Ticket
	ADD CONSTRAINT FK__Ticket__EmplID FOREIGN KEY (EmplID)
	REFERENCES P1.Employee (ID);
