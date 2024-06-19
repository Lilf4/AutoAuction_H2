using AutoAuction.Models;
using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AutoAuction.DAL {
    internal class UserR : DBUtil, IUser {
        public User? Login(string username, string password) {
            User user = new User();
            user.UserName = username;
            user.Password = password;

            SqlConnection conn = GetConnection(user);
            try {
                conn.Open();
            }
            catch (SqlException e) {
                Debug.WriteLine(e.Message);
                return null;
            }
            finally {
                conn.Close();
            }

            conn = GetConnection(MasterUser);
            try {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetUserDetails_sp", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", user.UserName);
                
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows) {
                    reader.Read();
                    try {
                        //Corporate
                        reader.GetString(reader.GetOrdinal("CVR"));
                        CoporateUser corporateUser = new CoporateUser(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(3),
                            reader.GetDecimal(2),
                            reader.GetDecimal(7),
                            reader.GetString(6)
                        );
                        user = corporateUser;
                    }
                    catch {
                        //Private
                        PrivateUser privateUser = new PrivateUser(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(3),
                            reader.GetDecimal(2),
                            reader.GetString(6)
                        );
                        user = privateUser;
                    }

                    user.Password = password;
                    return user;
                }
                else {
                    return null;
                }
            }
            catch (SqlException e) {
                Debug.WriteLine(e.Message);
                return null;
            }
            finally {
                conn.Close();
            }
            return user;
        }

        public void Logout() {
            throw new NotImplementedException();
        }

        public void Register(User user, bool isCorporate, string CPR, string CVR) {
            SqlConnection conn = GetConnection(MasterUser);
            try {
                conn.Open();
                SqlCommand cmd = new SqlCommand("CreateNewUser_sp", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", user.UserName);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@PostCode", user.Postcode);
                cmd.Parameters.AddWithValue("@IsCorporate", isCorporate);
                cmd.Parameters.AddWithValue("@CPR", CPR);
                cmd.Parameters.AddWithValue("@CVR", CVR);
                cmd.Parameters.AddWithValue("@Balance", user.Balance);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e) {
                Debug.WriteLine(e.Message);
            }
            finally {
                conn.Close();
            }
        }
    }
}
