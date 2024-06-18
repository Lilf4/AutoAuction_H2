------------------------
-- TESTING AutoAuctionDB
------------------------
-- Contains:
	-- Testing Stored Procedures
	-- Testing Views

----------------------------
-- TESTING STORED PROCEDURES
----------------------------
-- Contains:
	-- Testing InsertPrivatePersonalCar_sp
	-- Testing InsertProfessionalCar_sp
	-- Testing CreateNewUser_sp - PrivateUser
	-- Testing CreateNewUser_sp - CorporateUser
	-- Testing CreateAuction_sp - Bus

-- InsertPrivatePersonalCar_sp
EXEC InsertPrivatePersonalCar_sp
	@Name = 'Toyota Corolla',
	@KmDriven = 15000,
	@RegCode = 'AB12347',
	@Year = 2020,
	@TowHook = 1,
	@LicenseType = 1,
	@MotorSize = 1.8,
	@KmPerUnit = 15.0,
	@FuelType = 1,
	@EnergyClass = 2,
	@NumberOfSeats = 5,
	@BootSize = 450,
	@Isofix = 1;
GO

-- InsertProfessionalCar_sp
EXEC InsertProfessionalCar_sp 
	@Name = 'Ford Transit',
	@KmDriven = 50000,
	@RegCode = 'DE56756',
	@Year = 2018,
	@TowHook = 1,
	@LicenseType = 2,
	@MotorSize = 2.2,
	@KmPerUnit = 10.0,
	@FuelType = 2,
	@EnergyClass = 3,
	@NumberOfSeats = 3,
	@BootSize = NULL,
	@SafetyBar = 1,
	@LoadCapacity = 1200;
GO

-- CreateNewUser_sp - PrivateUser
EXEC CreateNewUser_sp
	@Username = 'TestUser@PrivateUser.com',
	@Password = 'TestUserPassword123',
	@Postcode = '1234',
	@IsCorporate = 0,
	@CPR = '123456-9876'
GO

-- CreateNewUser_sp - CorporateUser
EXEC CreateNewUser_sp
	@Username = 'TestUser@CorporateUser.com',
	@Password = 'TestUserPassword123',
	@Postcode = '4321',
	@IsCorporate = 1,
	@CVR = '12345678'
GO

-- Create Auction - BUS
DECLARE @SellerID INT = 1; -- Example SellerID
DECLARE @VehicleType NVARCHAR(50) = 'Bus';
DECLARE @Name NVARCHAR(100) = 'Luxury Bus';
DECLARE @KmDriven INT = 50000;
DECLARE @RegCode VARCHAR(7) = 'BUS1234';
DECLARE @Year INT = 2018;
DECLARE @TowHook BIT = 0;
DECLARE @LicenseType TINYINT = 2;
DECLARE @MotorSize FLOAT = 6.0;
DECLARE @KmPerUnit FLOAT = 8.5;
DECLARE @FuelType TINYINT = 1;
DECLARE @EnergyClass TINYINT = 2;
DECLARE @Weight INT = 15000;
DECLARE @Height FLOAT = 4.0;
DECLARE @Length FLOAT = 12.5;
DECLARE @NumberOfSeats TINYINT = 50;
DECLARE @NumberOfSleepingSpots TINYINT = 10;
DECLARE @Toilet BIT = 1;
DECLARE @MinimumPrice DECIMAL(19, 4) = 100000.00;

EXEC CreateAuction_sp 
    @SellerID = @SellerID,
    @VehicleType = @VehicleType,
    @Name = @Name,
    @KmDriven = @KmDriven,
    @RegCode = @RegCode,
    @Year = @Year,
    @TowHook = @TowHook,
    @LicenseType = @LicenseType,
    @MotorSize = @MotorSize,
    @KmPerUnit = @KmPerUnit,
    @FuelType = @FuelType,
    @EnergyClass = @EnergyClass,
    @NumberOfSeats = @NumberOfSeats,
    @BootSize = NULL, 
    @Isofix = NULL, 
    @SafetyBar = NULL, 
    @LoadCapacity = NULL, 
    @Weight = @Weight,
    @Height = @Height,
    @Length = @Length,
    @NumberOfSleepingSpots = @NumberOfSleepingSpots,
    @Toilet = @Toilet,
    @MinimumPrice = @MinimumPrice;



----------------
-- TESTING VIEWS
----------------
-- Contains:
	-- Testing vw_PrivateCarDetails
	-- Testing vw_ProfessionalCarDetails

-- PrivateCarDetails
SELECT * 
	FROM vw_PrivateCarDetails
	WHERE VehicleID = 1
GO

-- ProfessionalCarDetails
SELECT * 
	FROM vw_ProfessionalCarDetails;
GO




