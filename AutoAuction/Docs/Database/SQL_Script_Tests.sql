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


-- CreateNewUser_sp - PrivateUser
EXEC CreateNewUser_sp
	@Username = 'TestUser@PrivateUser.com',
	@Password = 'TestUserPassword123',
	@Postcode = '1234',
	@IsCorporate = 0,
	@CPR = '123456-9876'


-- CreateNewUser_sp - CorporateUser
EXEC CreateNewUser_sp
	@Username = 'TestUser@CorporateUser.com',
	@Password = 'TestUserPassword123',
	@Postcode = '4321',
	@IsCorporate = 1,
	@CVR = '12345678'


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

-- ProfessionalCarDetails
SELECT * 
	FROM vw_ProfessionalCarDetails;
