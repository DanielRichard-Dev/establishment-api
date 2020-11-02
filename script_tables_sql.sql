create table Establishment
(
	EstablishmentId INT NOT NULL IDENTITY,
	CompanyName VARCHAR(256) NOT NULL,
	FantasyName VARCHAR(256) NULL,
	CNPJ VARCHAR(14) NOT NULL,
	Email VARCHAR(256) NULL,
	Telephone VARCHAR(70) NULL,
	DateOfRegistration DATETIME NULL,
	Status BIT NOT NULL,
	CategoryId INT NULL
)

create table EstablishmentAddress
(
	EstablishmentAdressId INT NOT NULL IDENTITY,
	EstablishmentId INT NOT NULL,
	Address VARCHAR(256) NULL,
	City VARCHAR(256) NULL,
	State VARCHAR(70) NULL
)

create table EstablishmentAccount
(
	EstablishmentAccountId INT NOT NULL IDENTITY,
	EstablishmentId INT NOT NULL,
	Agency VARCHAR(70) NULL,
	Account VARCHAR(70) NULL
)

create table EstablishmentCategory
(
	EstablishmentCategoryId INT NOT NULL IDENTITY,
	CategoryDescription VARCHAR(256) NOT NULL
)
