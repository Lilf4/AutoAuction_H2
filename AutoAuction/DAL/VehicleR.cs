using AutoAuction.Models;
using System.Data.SqlClient;
using System.Diagnostics;
using static AutoAuction.Models.Vehicle;

namespace AutoAuction.DAL {
    public class VehicleR : DBUtil, IVehicle {
        public Vehicle? GetVehicle(int ID) {
            SqlConnection conn = GetConnection(MasterUser);
            conn.Open();
            SqlCommand cmd = new SqlCommand("GetVehicleDetails_sp", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VehicleID", ID);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows) {
                reader.Read();

                switch (reader.GetString(reader.GetOrdinal("VehicleType"))) {
                    case "Bus":
                        Bus bus = new Bus(
                            id: reader.GetInt32(reader.GetOrdinal("VehicleID")),
                            name: reader.GetString(reader.GetOrdinal("Name")),
                            kmDriven: reader.GetInt32(reader.GetOrdinal("KmDriven")),
                            regCode: reader.GetString(reader.GetOrdinal("RegCode")),
                            year: reader.GetInt32(reader.GetOrdinal("Year")),
                            towHook: reader.GetBoolean(reader.GetOrdinal("TowHook")),
                            kmPerUnit: reader.GetDouble(reader.GetOrdinal("KmPerUnit")),
                            fueltType: (FuelTypes)reader.GetByte(reader.GetOrdinal("FuelType")),
                            height: (float)reader.GetDouble(reader.GetOrdinal("Height")),
                            length: (float)reader.GetDouble(reader.GetOrdinal("Length")),
                            weight: reader.GetInt32(reader.GetOrdinal("Weight")),
                            seats: reader.GetByte(reader.GetOrdinal("NumberOfSeats")),
                            standingPlaces: reader.GetByte(reader.GetOrdinal("NumberOfSleepingSpots")),
                            toilets: reader.GetBoolean(reader.GetOrdinal("Toilet"))
                            ) {
                            MotorSize = reader.GetDouble(reader.GetOrdinal("MotorSize"))
                        };
                        return bus;
                    case "Truck":
                        Truck truck = new Truck(
                            id: reader.GetInt32(reader.GetOrdinal("VehicleID")),
                            name: reader.GetString(reader.GetOrdinal("Name")),
                            kmDriven: reader.GetInt32(reader.GetOrdinal("KmDriven")),
                            regCode: reader.GetString(reader.GetOrdinal("RegCode")),
                            year: reader.GetInt32(reader.GetOrdinal("Year")),
                            towHook: reader.GetBoolean(reader.GetOrdinal("TowHook")),
                            kmPerUnit: reader.GetDouble(reader.GetOrdinal("KmPerUnit")),
                            fueltType: (FuelTypes)reader.GetByte(reader.GetOrdinal("FuelType")),
                            height: (float)reader.GetDouble(reader.GetOrdinal("Height")),
                            length: (float)reader.GetDouble(reader.GetOrdinal("Length")),
                            weight: reader.GetInt32(reader.GetOrdinal("Weight")),
                            LoadCapacity: reader.GetInt32(reader.GetOrdinal("LoadCapacity"))
                            ) {

                            MotorSize = reader.GetDouble(reader.GetOrdinal("MotorSize"))
                        };
                        return truck;
                    case "Professinal":
                        ProfessionelPersonalCar professionelCar = new ProfessionelPersonalCar(
                            id: reader.GetInt32(reader.GetOrdinal("VehicleID")),
                            name: reader.GetString(reader.GetOrdinal("Name")),
                            kmDriven: reader.GetInt32(reader.GetOrdinal("KmDriven")),
                            regCode: reader.GetString(reader.GetOrdinal("RegCode")),
                            year: reader.GetInt32(reader.GetOrdinal("Year")),
                            towhook: reader.GetBoolean(reader.GetOrdinal("TowHook")),
                            safetyBar: reader.GetBoolean(reader.GetOrdinal("SafetyBar")),
                            kmPerUnit: reader.GetDouble(reader.GetOrdinal("KmPerUnit")),
                            fuelType: (FuelTypes)reader.GetByte(reader.GetOrdinal("FuelType")),
                            bootSize: reader.GetInt32(reader.GetOrdinal("BootSize")),
                            LoadCapacity: reader.GetInt32(reader.GetOrdinal("LoadCapacity"))
                            ) {
                            MotorSize = reader.GetFloat(reader.GetOrdinal("MotorSize"))
                        };
                        return professionelCar;
                    case "Private":
                        PrivatePersonalCar privateCar = new PrivatePersonalCar(
                            id: reader.GetInt32(reader.GetOrdinal("VehicleID")),
                            name: reader.GetString(reader.GetOrdinal("Name")),
                            kmDriven: reader.GetInt32(reader.GetOrdinal("KmDriven")),
                            regCode: reader.GetString(reader.GetOrdinal("RegCode")),
                            year: reader.GetInt32(reader.GetOrdinal("Year")),
                            towhook: reader.GetBoolean(reader.GetOrdinal("TowHook")),
                            isofix: reader.GetBoolean(reader.GetOrdinal("Isofix")),
                            kmPerUnit: reader.GetDouble(reader.GetOrdinal("KmPerUnit")),
                            fuelType: (FuelTypes)reader.GetInt32(reader.GetOrdinal("FuelType")),
                            bootSize: reader.GetInt32(reader.GetOrdinal("BootSize")),
                            numberOfSeats: reader.GetByte(reader.GetOrdinal("NumberOfSeats"))
                            ) {
                            MotorSize = reader.GetFloat(reader.GetOrdinal("MotorSize"))
                        };
                        return privateCar;
                    default:
                        return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
