-----------------------------
-- AutoAuctionDB - H2 Project
-----------------------------
-- Create AutoAuctionDB Database
	-- Contains:
		-- Database creation script
		-- Tables (14)
			-- 1. Users
			-- 2. CorporateUsers
			-- 3. PrivateUsers
			-- 4. Vehicles
			-- 5. HeavyVehicles
			-- 6. PersonalCars
			-- 7. Bus
			-- 8. Truck
			-- 9. PrivatePersonalCars
			-- 10. ProfessionalCars
			-- 11. Auctions
			-- 12. ActiveAuctions
			-- 13. FinishedAuctions
			-- 14. Bids
		-- Stored Procedures (9)
			-- 1. InsertPrivatePersonalCar_sp
			-- 2. InsertProfessionalCar_sp
			-- 3. InsertTruck_sp
			-- 4. InsertBus_sp
			-- 5. CreateNewUser_sp
			-- 6. CreateAuction_sp
			-- 7. PlaceBid_sp
			-- 8. EndAuction_sp
			-- 9. GetActiveauctions_sp
		-- Views (7)
			-- 1. vw_CorporateUserDetails
			-- 2. vw_PrivateUserDetails
			-- 3. vw_PrivateCarDetails
			-- 4. vw_ProfessionalCarDetails
			-- 5. vw_TruckDetails
			-- 6. vw_BusDetails
			-- 7. vw_AuctionDetails
		-- User Roles
			-- UserCreatorRole
		-- Logins
			-- NewUserCreator
		-- Indexing (2)
			-- 1. idx_username  - Users (Username)
			-- 2. idx_vehicleid - Vehicles (Id)
		-- Triggers (4)
			-- 1. trg_ProfileCreation
			-- 2. trg_ProfileDeletion
			-- 3. trg_AuctionCreation
			-- 4. trg_AuctionEnding

-- TODO:
	-- More Views?
	-- More Stored Procedures?
	-- Triggers
	-- Roles
-----------------------------------
-- DROP TABLES + DATABASE IF EXISTS
-----------------------------------
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'AutoAuctionDB')
BEGIN

    DROP DATABASE AutoAuctionDB;
END

-- DROP LOGIN
IF EXISTS (SELECT * FROM sys.server_principals WHERE name = 'NewUserCreator')
BEGIN
    DROP LOGIN NewUserCreator;
END
------------------
-- CREATE DATABASE
------------------
CREATE DATABASE AutoAuctionDB;
GO

USE AutoAuctionDB;
GO

---------------------------------
-- TABLES - named in pluralform
---------------------------------
-- Contains:
	-- User tables:
		-- Users
		-- CorporateUsers
		-- PrivateUsers
	-- Vehicle tables:
		-- Vehicles
		-- HeavyVehicles
		-- PersonalCars
		-- Buses
		-- Trucks
		-- PrivatePersonalCars
		-- ProfessionalCars
	-- Auction tables:
		-- Auctions 
		-- ActiveAuctions
		-- FinishedAuctions
		-- Bids
	-- Logging tables:
		-- ProfileCreationLog
		-- ProfileDeletionLog
		-- AuctionCreationLog
		-- AuctionEndingLog

--------------
-- USER TABLES 1, 2, 3
--------------
CREATE TABLE Users (
	Id INT PRIMARY KEY IDENTITY(1,1),
	Username VARCHAR(50) UNIQUE ,
	Balance DECIMAL(19, 4) NOT NULL,
	PostCode VARCHAR(4) 
	);

CREATE TABLE CorporateUsers (
	Id INT PRIMARY KEY IDENTITY(1,1),
	Username VARCHAR(50) UNIQUE,
	FOREIGN KEY (Username) REFERENCES Users(Username),
	CVR VARCHAR(8) NOT NULL,
	Credit DECIMAL(19, 4) NOT NULL
	);

CREATE TABLE PrivateUsers (
	Id INT PRIMARY KEY IDENTITY(1,1),
	Username VARCHAR(50) UNIQUE,
	FOREIGN KEY (Username) REFERENCES Users(Username),
	CPR VARCHAR(11) NOT NULL
);

-----------------
-- VEHICLE TABLES 4, 5, 6, 7, 8, 9, 10
-----------------
CREATE TABLE Vehicles (
	Id INT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(100) NOT NULL,
	KmDriven INT NOT NULL,
	RegCode VARCHAR(7) UNIQUE NOT NULL,
	Year INT NOT NULL,
	TowHook BIT NOT NULL,
	LicenseType TINYINT NOT NULL,
	MotorSize FLOAT NOT NULL,
	KmPerUnit FLOAT NOT NULL,
	FuelType TINYINT NOT NULL,
	EnergyClass TINYINT NOT NULL
	);

CREATE TABLE HeavyVehicles (
	Id INT PRIMARY KEY IDENTITY(1,1),
	VehicleID INT UNIQUE,
	FOREIGN KEY (VehicleID) REFERENCES Vehicles(Id),
	Weight INT NOT NULL,
	Height FLOAT NOT NULL,
	Length FLOAT NOT NULL
	);

CREATE TABLE PersonalCars (
	Id INT PRIMARY KEY IDENTITY(1,1),
	VehicleID INT UNIQUE,
	FOREIGN KEY (VehicleID) REFERENCES Vehicles(Id),
	NumberOfSeats TINYINT NOT NULL,
	BootSize INT, -- Indicates Volume (Liters)
	);

CREATE TABLE Buses (
	Id INT PRIMARY KEY IDENTITY(1,1),
	HeavyVehicleID INT UNIQUE,
	FOREIGN KEY (HeavyVehicleID) REFERENCES HeavyVehicles(Id),
	NumberOfSeats TINYINT NOT NULL,
	NumberOfSleepingSpots TINYINT NOT NULL,
	Toilet BIT NOT NULL -- 1 = Yes // 0 = No
	);

CREATE TABLE Trucks (
	Id INT PRIMARY KEY IDENTITY(1,1),
	HeavyVehicleID INT UNIQUE,
	FOREIGN KEY (HeavyVehicleID) REFERENCES HeavyVehicles(Id),
	LoadCapacity INT NOT NULL
	);

CREATE TABLE PrivatePersonalCars (
	Id INT PRIMARY KEY IDENTITY(1,1),
	PersonalCarID INT UNIQUE,
	FOREIGN KEY (PersonalCarID) REFERENCES PersonalCars(Id),
	Isofix BIT NOT NULL
	);

CREATE TABLE ProfessionalCars (
	Id INT PRIMARY KEY IDENTITY(1,1),
	PersonalCarID INT UNIQUE,
	FOREIGN KEY (PersonalCarID) REFERENCES PersonalCars(Id),
	SafetyBar BIT NOT NULL,
	LoadCapacity INT NOT NULL
	);

-----------------
-- AUCTION TABLES 11, 12, 13, 14
-----------------
CREATE TABLE Auctions (
Id INT PRIMARY KEY IDENTITY(1,1),
SellerID INT,
FOREIGN KEY (SellerID) REFERENCES Users(Id),
VehicleID INT,
FOREIGN KEY (VehicleID) REFERENCES Vehicles(Id),
MinimumPrice DECIMAL NOT NULL
);

CREATE TABLE ActiveAuctions (
id INT PRIMARY KEY IDENTITY (1,1),
AuctionID INT,
FOREIGN KEY (AuctionID) REFERENCES Auctions(Id)
);

CREATE TABLE FinishedAuctions (
id INT PRIMARY KEY IDENTITY (1,1),
AuctionID INT,
FOREIGN KEY (AuctionID) REFERENCES Auctions(Id)
);

CREATE TABLE Bids (
id INT PRIMARY KEY IDENTITY (1,1),
AuctionID INT,
FOREIGN KEY (AuctionID) REFERENCES Auctions(Id),
BidderID INT,
FOREIGN KEY (BidderID) REFERENCES Users(Id),
BidAmount DECIMAL NOT NULL
);
GO

-----------------
-- LOGGING TABLES
-----------------
CREATE TABLE ProfileCreationLog (
LogId INT PRIMARY KEY IDENTITY(1,1),
Username VARCHAR(50),
CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE ProfileDeletionLog (
LogId INT PRIMARY KEY IDENTITY(1,1),
Username VARCHAR(50),
DeletedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE AuctionCreationLog (
LogId INT PRIMARY KEY IDENTITY(1,1),
AuctionID INT,
SellerID INT,
CreatedAt DATETIME DEFAULT GETDATE()
);

 CREATE TABLE AuctionEndingLog (
LogId INT PRIMARY KEY IDENTITY(1,1),
AuctionID INT,
EndedAt DATETIME DEFAULT GETDATE()
);
GO

-------------------------------------
-- STORED PROCEDURES - suffixed '_sp'
-------------------------------------
-- Contains:
	-- 1. InsertPrivatePersonalCar_sp
	-- 2. InsertProfessionalCar_sp
	-- 3. InsertTruck_sp
	-- 4. InsertBus_sp
	-- 5. CreateNewUser_sp
	-- 6. CreateAuction_sp
	-- 7. PlaceBid_sp
	-- 8. EndAuction_sp - WIP

--------------------------------------------
-- 1. InsertPrivatePersonalCar Stored Procedure
--------------------------------------------
CREATE PROCEDURE InsertPrivatePersonalCar_sp
	@Name NVARCHAR(50),
	@KmDriven INT,
	@RegCode VARCHAR(7),
	@Year INT,
	@TowHook BIT,
	@LicenseType TINYINT,
	@MotorSize FLOAT,
	@KmPerUnit FLOAT,
	@FuelType TINYINT,
	@EnergyClass TINYINT,
	@NumberOfSeats INT,
	@BootSize INT,
	@Isofix BIT
AS
BEGIN
	BEGIN TRANSACTION;

	BEGIN TRY
		-- Vehicles TABLE
		INSERT INTO Vehicles (Name, KmDriven, RegCode, Year, TowHook, LicenseType, MotorSize, KmPerUnit, FuelType, EnergyClass)
		VALUES (@Name, @KmDriven, @RegCode, @Year, @TowHook, @LicenseType, @MotorSize, @KmPerUnit, @FuelType, @EnergyClass);

		DECLARE @VehicleID INT;
		SET @VehicleID = SCOPE_IDENTITY();

		-- PersonalCars TABLE
		INSERT INTO PersonalCars (VehicleID, NumberOfSeats, BootSize)
		VALUES (@VehicleID, @NumberOfSeats, @BootSize);

		DECLARE @PersonalCarID INT;
		SET @PersonalCarID = SCOPE_IDENTITY();

		-- PrivatePersonalCars TABLE
		INSERT INTO PrivatePersonalCars (PersonalCarID, Isofix)
		VALUES (@PersonalCarID, @Isofix);

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION;

		THROW;
	END CATCH
END;
GO

------------------------------------------
-- 2. Insert ProfessionalCar Stored Procedure
------------------------------------------
CREATE PROCEDURE InsertProfessionalCar_sp
	@Name NVARCHAR(50),
	@KmDriven INT,
	@RegCode VARCHAR(7),
	@Year INT,
	@TowHook BIT,
	@LicenseType TINYINT,
	@MotorSize FLOAT,
	@KmPerUnit FLOAT,
	@FuelType TINYINT,
	@EnergyClass TINYINT,
	@NumberOfSeats INT,
	@BootSize INT,
	@SafetyBar BIT,
	@LoadCapacity INT
AS
BEGIN
	BEGIN TRANSACTION;

	BEGIN TRY
		-- Vehicles TABLE
		INSERT INTO Vehicles (Name, KmDriven, RegCode, Year, TowHook, LicenseType, MotorSize, KmPerUnit, FuelType, EnergyClass)
		VALUES (@Name, @KmDriven, @RegCode, @Year, @TowHook, @LicenseType, @MotorSize, @KmPerUnit, @FuelType, @EnergyClass);

		DECLARE @VehicleID INT;
		SET @VehicleID = SCOPE_IDENTITY();

		-- PersonalCars TABLE
		INSERT INTO PersonalCars (VehicleID, NumberOfSeats, BootSize)
		VALUES (@VehicleID, @NumberOfSeats, @BootSize);

		DECLARE @PersonalCarID INT;
		SET @PersonalCarID = SCOPE_IDENTITY();

		-- ProfessionalCars TABLE
		INSERT INTO ProfessionalCars (PersonalCarID, SafetyBar, LoadCapacity)
		VALUES (@PersonalCarID, @SafetyBar, @LoadCapacity);

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		
		ROLLBACK TRANSACTION;

		THROW;
	END CATCH
END;
GO

--------------------------------
-- 3. Insert Truck Stored Procedure
--------------------------------
CREATE PROCEDURE InsertTruck_sp
    @Name NVARCHAR(100),
    @KmDriven INT,
    @RegCode VARCHAR(7),
    @Year INT,
    @TowHook BIT,
    @LicenseType TINYINT,
    @MotorSize FLOAT,
    @KmPerUnit FLOAT,
    @FuelType TINYINT,
    @EnergyClass TINYINT,
    @Weight INT,
    @Height FLOAT,
    @Length FLOAT,
    @LoadCapacity INT
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Vehicles TABLE
        INSERT INTO Vehicles (Name, KmDriven, RegCode, Year, TowHook, LicenseType, MotorSize, KmPerUnit, FuelType, EnergyClass)
        VALUES (@Name, @KmDriven, @RegCode, @Year, @TowHook, @LicenseType, @MotorSize, @KmPerUnit, @FuelType, @EnergyClass);

        DECLARE @VehicleID INT;
        SET @VehicleID = SCOPE_IDENTITY();

        -- HeavyVehicles TABLE
        INSERT INTO HeavyVehicles (VehicleID, Weight, Height, Length)
        VALUES (@VehicleID, @Weight, @Height, @Length);

        DECLARE @HeavyVehicleID INT;
        SET @HeavyVehicleID = SCOPE_IDENTITY();

        -- Truck TABLE
        INSERT INTO Truck (HeavyVehicleID, LoadCapacity)
        VALUES (@HeavyVehicleID, @LoadCapacity);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

------------------------------
-- 4. Insert Bus Stored Procedure
------------------------------
CREATE PROCEDURE InsertBus_sp
    @Name NVARCHAR(100),
    @KmDriven INT,
    @RegCode VARCHAR(7),
    @Year INT,
    @TowHook BIT,
    @LicenseType TINYINT,
    @MotorSize FLOAT,
    @KmPerUnit FLOAT,
    @FuelType TINYINT,
    @EnergyClass TINYINT,
    @Weight INT,
    @Height FLOAT,
    @Length FLOAT,
    @NumberOfSeats TINYINT,
    @NumberOfSleepingSpots TINYINT,
    @Toilet BIT
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Vehicles TABLE
        INSERT INTO Vehicles (Name, KmDriven, RegCode, Year, TowHook, LicenseType, MotorSize, KmPerUnit, FuelType, EnergyClass)
        VALUES (@Name, @KmDriven, @RegCode, @Year, @TowHook, @LicenseType, @MotorSize, @KmPerUnit, @FuelType, @EnergyClass);

        DECLARE @VehicleID INT;
        SET @VehicleID = SCOPE_IDENTITY();

        -- HeavyVehicles TABLE
        INSERT INTO HeavyVehicles (VehicleID, Weight, Height, Length)
        VALUES (@VehicleID, @Weight, @Height, @Length);

        DECLARE @HeavyVehicleID INT;
        SET @HeavyVehicleID = SCOPE_IDENTITY();

        -- Bus TABLE
        INSERT INTO Bus (HeavyVehicleID, NumberOfSeats, NumberOfSleepingSpots, Toilet)
        VALUES (@HeavyVehicleID, @NumberOfSeats, @NumberOfSleepingSpots, @Toilet);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

-----------------------------------
-- 5. Create New User Stored Procedure
-----------------------------------
CREATE PROCEDURE CreateNewUser_sp
	@Username VARCHAR(50),
	@Password VARCHAR(50),
	@PostCode VARCHAR(4),
	@IsCorporate BIT,
	@CPR VARCHAR(11) = NULL,
	@CVR VARCHAR(8) = NULL,
	@Balance DECIMAL(19, 4) = 0,
	@Credit DECIMAL(19, 4) = 50000 -- starting Credit
AS
BEGIN
	SET NOCOUNT ON;
		
	BEGIN TRY

		BEGIN TRANSACTION;

		DECLARE @SQL NVARCHAR(MAX);
		SET @SQL = 'CREATE LOGIN [' + @Username + '] WITH PASSWORD = ''' + @Password + ''';';
		EXEC sp_executesql @SQL;

		SET @SQL = 'USE AutoAuctionDB; CREATE USER [' + @Username + '] FOR LOGIN [' + @Username + '];';
		EXEC sp_executesql @SQL;

		INSERT INTO Users (Username, PostCode, Balance)
		VALUES (@Username, @PostCode, @Balance);

		IF @IsCorporate = 1
		BEGIN
			INSERT INTO CorporateUsers (Username, CVR, Credit)
			VALUES (@Username, @CVR, @Credit);
		END
		ELSE
		BEGIN
			INSERT INTO PrivateUsers (Username, CPR)
			VALUES (@Username, @CPR);
		END

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;

		THROW;
	END CATCH
END;
GO

----------------------------------
-- 6. Create Auction Stored Procedure
----------------------------------
CREATE PROCEDURE CreateAuction_sp
	@SellerID INT,
	@VehicleID INT,
	@MinimumPrice DECIMAL(19, 4)
AS
BEGIN
	BEGIN TRANSACTION;

	BEGIN TRY
		INSERT INTO Auctions (SellerID, VehicleID, MinimumPrice)
		VALUES (@SellerID, @VehicleID, @MinimumPrice);

		DECLARE @AuctionID INT;
		SET @AuctionID = SCOPE_IDENTITY();

		INSERT INTO ActiveAuctions (AuctionID)
		VALUES (@AuctionID);

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW 50000, 'ERROR - Auction Was Not Created', 1;
	END CATCH
END;
GO

-----------------------------
-- 7. Place Bid Stored Procedure
-----------------------------
CREATE PROCEDURE PlaceBid_sp
	@AuctionID INT,
	@BidderID INT,
	@BidAmount Decimal(19, 4)
AS
BEGIN
	BEGIN TRANSACTION;

    BEGIN TRY
		IF NOT EXISTS (SELECT 1 FROM ActiveAuctions WHERE AuctionID = @AuctionID)
        BEGIN
			THROW 50001, 'ERROR - Auction is not active or does not exist.', 1;
		END

		IF EXISTS (SELECT 1 FROM Bids WHERE AuctionID = @AuctionID)
		BEGIN
			DECLARE @CurrentHighestBid DECIMAL (19, 4);
			SELECT @CurrentHighestBid = MAX(BidAmount) FROM Bids WHERE AuctionID = @AuctionID;

			IF @BidAmount <= @CurrentHighestBid
			BEGIN
				THROW 50002, 'ERROR - Bid must be higher than the current bid.', 2;
			END
		END

		INSERT INTO Bids (AuctionID, BidderID, BidAmount)
		VALUES (@AuctionID, @BidderID, @BidAmount);

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW 50000, 'ERROR - Could not place Bid.', 3;
	END CATCH
END;
GO

-------------------------------
-- 8. End Auction Stored Procedure
-------------------------------
CREATE PROCEDURE EndAuction_sp
	@AuctionID INT
AS
BEGIN
	BEGIN TRANSACTION;

	BEGIN TRY
		IF NOT EXISTS (SELECT 1 FROM ActiveAuctions WHERE AuctionID = @AuctionId)
		BEGIN
			THROW 50001, 'ERROR - Auction is not active or does not exists.', 1;
		END

		INSERT INTO FinishedAuctions (AuctionID)
		SELECT  AuctionID From ActiveAuctions Where AuctionID = @AuctionID;
		DELETE FROM ActiveAuctions Where AuctionID = @AuctionID;

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW 50000, 'ERROR - Could not end Auction', 2;
	END CATCH
END;
GO

------------------------------------------
-- 9. Get Active Auctions Stored Procedure
------------------------------------------
CREATE PROCEDURE GetActiveauctions_sp
AS
BEGIN
	SELECT
		a.Id AS AuctionID,
		a.SellerID,
		u.Username AS SellerUsername,
		v.id AS VehicleID,
		v.Name AS BehicleName,
		a.MinimumPrice
	FROM
		ActiveAuctions aa
	JOIN
		Auctions a ON aa.AuctionID = a.Id
	JOIN
		Users u ON a.SellerId = u.Id
	JOIN
		Vehicles v ON a.VehicleID = v.Id;
END;
GO

-------------------------
-- VIEWS - prefixed 'vw_'
-------------------------
-- Contains:
	-- 1. vw_CorporateUserDetails
	-- 2. vw_PrivateUserDetails
	-- 3. vw_PrivateCarDetails
	-- 4. vw_ProfessionalCarDetails
	-- 5. vw_TruckDetails
	-- 6. vw_BusDetails
	-- 7. vw_AuctionDetails

-----------------------------
-- 1. vw_CorporateUserDetails
-----------------------------
CREATE VIEW vw_CorporateUserDetails AS
SELECT
    u.Id AS UserID,
    u.Username,
    u.Balance,
    u.PostCode,
    cu.CVR,
    cu.Credit
FROM
    Users u
JOIN
    CorporateUsers cu ON u.Username = cu.Username;
GO

---------------------------
-- 2. vw_PrivateUserDetails
---------------------------
CREATE VIEW vw_PrivateUserDetails AS
SELECT
    u.Id AS UserID,
    u.Username,
    u.Balance,
    u.PostCode,
    pu.CPR
FROM
    Users u
JOIN
    PrivateUsers pu ON u.Username = pu.Username;
GO

--------------------------
-- 3. vw_PrivateCarDetails
--------------------------
CREATE VIEW vw_PrivateCarDetails AS
	SELECT 
		v.Id AS VehicleID,
		v.Name,
		v.KmDriven,
		v.RegCode,
		v.Year,
		v.TowHook,
		v.LicenseType,
		v.MotorSize,
		v.KmPerUnit,
		v.FuelType,
		v.EnergyClass,
		pc.NumberOfSeats,
		pc.BootSize,
		ppc.Isofix
	FROM 
		Vehicles v
	JOIN 
		PersonalCars pc ON v.Id = pc.VehicleID
	JOIN 
		PrivatePersonalCars ppc ON pc.Id = ppc.PersonalCarID;
GO

-------------------------------
-- 4. vw_ProfessionalCarDetails
-------------------------------
CREATE VIEW vw_ProfessionalCarDetails AS
	SELECT 
		v.Id AS VehicleID,
		v.Name,
		v.KmDriven,
		v.RegCode,
		v.Year,
		v.TowHook,
		v.LicenseType,
		v.MotorSize,
		v.KmPerUnit,
		v.FuelType,
		v.EnergyClass,
		pc.NumberOfSeats,
		pc.BootSize,
		pfc.SafetyBar,
		pfc.LoadCapacity
	FROM 
		Vehicles v
	JOIN 
		PersonalCars pc ON v.Id = pc.VehicleID
	JOIN 
		ProfessionalCars pfc ON pc.Id = pfc.PersonalCarID;
GO

---------------------
-- 5. vw_TruckDetails
---------------------
CREATE VIEW vw_TruckDetails AS
	SELECT 
		v.Id AS VehicleID,
		v.Name,
		v.KmDriven,
		v.RegCode,
		v.Year,
		v.TowHook,
		v.LicenseType,
		v.MotorSize,
		v.KmPerUnit,
		v.FuelType,
		v.EnergyClass,
		hv.Weight,
		hv.Height,
		hv.Length,
		t.LoadCapacity
	FROM 
		Vehicles v
	JOIN 
		HeavyVehicles hv ON v.Id = hv.VehicleID
	JOIN 
		Trucks t ON hv.Id = t.HeavyVehicleID;
GO

-------------------
-- 6. vw_BusDetails
--------------------
CREATE VIEW vw_BusDetails AS
	SELECT 
		v.Id AS VehicleID,
		v.Name,
		v.KmDriven,
		v.RegCode,
		v.Year,
		v.TowHook,
		v.LicenseType,
		v.MotorSize,
		v.KmPerUnit,
		v.FuelType,
		v.EnergyClass,
		hv.Weight,
		hv.Height,
		hv.Length,
		b.NumberOfSeats,
		b.NumberOfSleepingSpots,
		b.Toilet
	FROM 
		Vehicles v
	JOIN 
		HeavyVehicles hv ON v.Id = hv.VehicleID
	JOIN 
		Buses b ON hv.Id = b.HeavyVehicleID;
GO

-----------------------
-- 7. vw_AuctionDetails
-----------------------
CREATE VIEW vw_AuctionDetails AS
	SELECT
		a.Id AS AuctionID,
		a.SellerID,
		u.Username AS SellerUsername,
		v.Id AS VehicleID,
		v.Name AS VehicleName,
		v.KmDriven,
		v.RegCode,
		v.Year,
		v.TowHook,
		v.LicenseType,
		v.MotorSize,
		v.KmPerUnit,
		v.FuelType,
		v.EnergyClass,
		a.MinimumPrice,
		CASE
			WHEN aa.AuctionID IS NOT NULL THEN 'Active'
			WHEN fa.AuctionID IS NOT NULL THEN 'Finished'
			ELSE 'Unknown'
		END AS AuctionStatus
	FROM
		Auctions a
	JOIN
		Users u ON a.SellerID = u.Id
	JOIN
		Vehicles v ON a.VehicleID = v.Id
	LEFT JOIN
		ActiveAuctions aa ON a.ID = aa.AuctionID
	LEFT JOIN
		FinishedAuctions fa ON a.id = fa.AuctionID;
GO

-----------------------
-- ROLES - CreatorRole
-----------------------
CREATE ROLE UserCreatorRole;
GRANT EXECUTE ON OBJECT::dbo.CreateNewUser_sp TO UserCreatorRole;
GO

-----------------------
-- LOGINS - CreatorUser
-----------------------
CREATE LOGIN NewUserCreator WITH PASSWORD = 'Creator123!';

	USE AutoAuctionDB;
	CREATE USER NewUserCreator FOR LOGIN NewUserCreator;
	ALTER ROLE UserCreatorRole ADD MEMBER NewUserCreator;
GO


---------------------------
-- INDEXS - prefixed 'inx_'
---------------------------
-- Contains:
	-- idx_username -  Users (Username)
	-- idx_vehicleid - Vehicles (Id)

-----------------------------------
-- 1. idx_username -  Users (Username)
-----------------------------------
CREATE INDEX idx_username ON Users (Username);
GO

--------------------------------
-- 2. idx_vehicleid - Vehicles (Id)
--------------------------------
CREATE INDEX idx_vehicleid ON Vehicles (Id);
GO

----------------------------
-- TRIGGERS - prefixed 'trg'
----------------------------
-- Contains:
	-- trg_ProfileCreation	- WIP
	-- trg_ProfileDeletion	- WIP
	-- trg_AuctionCreation	- WIP
	-- trg_AuctionEnding	- WIP

---------------------------
-- Profile Creation Trigger
---------------------------
CREATE TRIGGER trg_ProfileCreation
ON Users
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO ProfileCreationLog (Username)
	SELECT Username FROM inserted;
END;
GO

---------------------------
-- Profile Deletion Trigger
---------------------------
CREATE TRIGGER trg_ProfileDeletion
ON Users
AFTER DELETE
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO ProfileDeletionLog (Username)
	SELECT Username FROM deleted;
END;
GO

---------------------------
-- Auction Creation Trigger
---------------------------
CREATE TRIGGER trg_AuctionCreation
ON Auctions
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO AuctionCreationLog (AuctionID, SellerID)
	SELECT id, SellerID FROM inserted;
END;
GO

-------------------------
-- Auction Ending Trigger
-------------------------
CREATE TRIGGER trg_AuctionEnding
ON FinishedAuctions
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuctionEndingLog (AuctionID)
    SELECT AuctionID FROM inserted;
END;
GO

