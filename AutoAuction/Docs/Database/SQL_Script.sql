-----------------------------
-- AutoAuctionDB - H2 Project
-----------------------------
-- Create AutoAuctionDB Database
	-- Contains:
		-- Database creation script
		-- Tables (10)
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
		-- Stored Procedures (4)
			-- 1. InsertPrivatePersonalCar_sp
			-- 2. InsertProfessionalCar_sp
			-- 3. InsertTruck_sp
			-- 4. InsertBus_sp
		-- Views (6)
			-- 1. vw_CorporateUserDetails
			-- 2. vw_PrivateUserDetails
			-- 3. vw_PrivateCarDetails
			-- 4. vw_ProfessionalCarDetails
			-- 5. vw_TruckDetails
			-- 6. vw_BusDetails
		-- User Roles
			-- UserCreatorRole
		-- Logins
			-- NewUserCreator
		

-- TODO:
	-- More Views?
	-- More Stored Procedures?
	-- Triggers
	-- Auctions
	-- Bids History
	-- Log Profile Creation
	-- Log Profile Deletion
	-- Log Auction Creation
	-- Log Auction Ending
	-- Log Succesful Logins
	-- Roles
-----------------------------------
-- DROP TABLES + DATABASE IF EXISTS
-----------------------------------
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'AutoAuctionDB')
BEGIN
    DROP DATABASE AutoAuctionDB;
END

------------------
-- CREATE DATABASE
------------------
CREATE DATABASE AutoAuctionDB;
GO

USE AutoAuctionDB;
GO

---------------------------------
-- TABLES - Users + Vehicles
---------------------------------
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

CREATE TABLE Bus (
	Id INT PRIMARY KEY IDENTITY(1,1),
	HeavyVehicleID INT UNIQUE,
	FOREIGN KEY (HeavyVehicleID) REFERENCES HeavyVehicles(Id),
	NumberOfSeats TINYINT NOT NULL,
	NumberOfSleepingSpots TINYINT NOT NULL,
	Toilet BIT NOT NULL -- 1 = Yes - 0 = No
	);

CREATE TABLE Truck (
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
GO;


-------------------------------------
-- STORED PROCEDURES - suffixed '_sp'
-------------------------------------
-- Contains:
	-- InsertPrivatePersonalCar_sp
	-- InsertProfessionalCar_sp
	-- InsertTruck_sp
	-- InsertBus_sp
	-- CreateNewUser_sp

--------------------------------------------
-- InsertPrivatePersonalCar Stored Procedure
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
GO;

------------------------------------------
-- Insert ProfessionalCar Stored Procedure
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
GO;

--------------------------------
-- Insert Truck Stored Procedure
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
GO;

------------------------------
-- Insert Bus Stored Procedure
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
GO;

-----------------------------------
-- Create New User Stored Procedure
-----------------------------------
CREATE PROCEDURE CreateNewUser_sp
	@Username VARCHAR(50),
	@Password VARCHAR(50),
	@PostCode VARCHAR(4),
	@IsCorporate BIT,
	@CPR VARCHAR(11) = NULL,
	@CVR VARCHAR(8) = NULL,
	@Balance DECIMAL(19, 4) = 0,
	@Credit DECIMAL(19, 4) = 50000
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
GO;


-------------------------
-- VIEWS - prefixed 'vw_'
-------------------------
-- Contains:
	-- vw_CorporateUserDetails
	-- vw_PrivateUserDetails
	-- vw_PrivateCarDetails
	-- vw_ProfessionalCarDetails
	-- vw_TruckDetails
	-- vw_BusDetails

--------------------------
-- vw_CorporateUserDetails
--------------------------
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
GO;

------------------------
-- vw_PrivateUserDetails
------------------------
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
GO;

-----------------------
-- vw_PrivateCarDetails
-----------------------
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
GO;

----------------------------
-- vw_ProfessionalCarDetails
----------------------------
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
GO;

------------------
-- vw_TruckDetails
------------------
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
		Truck t ON hv.Id = t.HeavyVehicleID;
GO;

----------------
-- vw_BusDetails
----------------
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
		Bus b ON hv.Id = b.HeavyVehicleID;
GO;


-------------------------
-- ROLES - CreatorRole
-------------------------
CREATE ROLE UserCreatorRole;
GRANT EXECUTE ON OBJECT::dbo.CreateNewUser_sp TO UserCreatorRole;


-----------------------
-- LOGINS - CreatorUser
-----------------------
CREATE LOGIN NewUserCreator WITH PASSWORD = 'Creator123!';

	USE AutoAuctionDB;
	CREATE USER NewUserCreator FOR LOGIN NewUserCreator;
	ALTER ROLE UserCreatorRole ADD MEMBER NewUserCreator;