USE master
GO
CREATE DATABASE AcmeBank
GO
USE AcmeBank

CREATE TABLE People (
	Id INT NOT NULL IDENTITY,
	FullName NVARCHAR(100) NOT NULL,
	Gender SMALLINT NOT NULL,
	Age SMALLINT NOT NULL,
	IdentityNumber INT NOT NULL,
	Address NVARCHAR(300) NULL,
	PhoneNumber NVARCHAR(50) NULL,
	CONSTRAINT PK_People PRIMARY KEY ([Id])
);

CREATE TABLE Customers (
	Id INT NOT NULL,
	Password NVARCHAR(100) NOT NULL,
	Status BIT NOT NULL,
    CONSTRAINT PK_Customers PRIMARY KEY (Id),
    CONSTRAINT FK_Customers_People_Id FOREIGN KEY (Id) REFERENCES People (Id) ON DELETE NO ACTION
);

CREATE TABLE Accounts (
	Id INT NOT NULL IDENTITY,
	Number NVARCHAR(50) NOT NULL,
	Type SMALLINT NOT NULL,
	InitialBalance DECIMAL(17,5) NOT NULL,
	Status BIT NOT NULL,
	CustomerId INT NOT NULL,
    CONSTRAINT PK_Accounts PRIMARY KEY (Id),
    CONSTRAINT FK_Accounts_Customers_CustomerId FOREIGN KEY (CustomerId) REFERENCES Customers (Id) ON DELETE NO ACTION
);

CREATE TABLE Movements (
	Id INT NOT NULL IDENTITY,
	Date DATETIME NOT NULL,
	Type SMALLINT NOT NULL,
	Amount DECIMAL(17,5) NOT NULL,
	InitialBalance DECIMAL(17,5) NOT NULL,
	Balance DECIMAL(17,5) NOT NULL,
	AccountId INT NOT NULL,
    CONSTRAINT PK_Movements PRIMARY KEY (Id),
    CONSTRAINT FK_Movements_Accounts_AccountId FOREIGN KEY (AccountId) REFERENCES Accounts (Id) ON DELETE NO ACTION
);

-- POPULATE DATABASE

-- Customers and people
INSERT INTO People (FullName, Gender, Age, IdentityNumber, Address, PhoneNumber)
VALUES ('Jose Lema', 0, 18, 4663365, 'Otavalo sn y principal', '098254785' );

INSERT INTO Customers (Id, Password, Status)
VALUES ((SELECT MAX(Id) FROM People), '123', 1);

INSERT INTO People (FullName, Gender, Age, IdentityNumber, Address, PhoneNumber)
VALUES ('Marianela Montalvo', 0, 25, 3562245, 'Amazonas y  NNUU', '097548965');

INSERT INTO Customers (Id, Password, Status)
VALUES ((SELECT MAX(Id) FROM People), '5678', 1);

INSERT INTO People (FullName, Gender, Age, IdentityNumber, Address, PhoneNumber)
VALUES ('Juan Osorio', 0, 45, 2421245, '13 junio y Equinoccial', '098874587');

INSERT INTO Customers (Id, Password, Status)
VALUES ((SELECT MAX(Id) FROM People), '1245', 1);

-- Accounts
INSERT INTO Accounts (Number, Type, InitialBalance, Status, CustomerId)
VALUES ('478758', 0, 2000, 1, (SELECT Id FROM People WHERE FullName = 'Jose Lema'));

INSERT INTO Accounts (Number, Type, InitialBalance, Status, CustomerId)
VALUES ('225487', 1, 100, 1, (SELECT Id FROM People WHERE FullName = 'Marianela Montalvo'));

INSERT INTO Accounts (Number, Type, InitialBalance, Status, CustomerId)
VALUES ('495878', 0, 0, 1, (SELECT Id FROM People WHERE FullName = 'Juan Osorio'));

INSERT INTO Accounts (Number, Type, InitialBalance, Status, CustomerId)
VALUES ('496825', 0, 540, 1, (SELECT Id FROM People WHERE FullName = 'Marianela Montalvo'));