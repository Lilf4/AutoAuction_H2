using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static AutoAuction.Models.Vehicle;

namespace AutoAuction.Models {
    public class User {
        public int Id { get; set; }

        // Has to be in the form of an email address
        public string UserName { get; set; }
        public int Postcode { get; set; }

        // Can't be negative
        public decimal Balance { get; set; }


        public User(int id, string username, int postcode, decimal balance) {
            this.Id = id;
            this.UserName = username;
            this.Postcode = postcode;
            this.Balance = balance;
        }

        public override string ToString() {
            return $"User: ID{Id}, Username: {UserName}, Postcode: {Postcode}";
        }



        public static bool IsEmailValid(string emailaddress, out string errorMessage) {

            if (string.IsNullOrWhiteSpace(emailaddress)) {
                errorMessage = "Invalid email format";
                return false;
            }

            try {
                string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

                if (regex.IsMatch(emailaddress)) {
                    errorMessage = "Correct";
                    return true;

                }
                errorMessage = "Invalid email format";
                return false;
            }
            catch (Exception ex) {
                errorMessage = $"Invalid email format {ex.Message}";
                return false;
            }
        }



        public static bool IsPasswordValid(string password, out string errorMessage) {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(password)) {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new Regex(@"[0-9]+", RegexOptions.Compiled);
            var hasUpperChar = new Regex(@"[A-Z]+", RegexOptions.Compiled);
            var hasMiniMaxChars = new Regex(@".{8,20}", RegexOptions.Compiled);
            var hasLowerChar = new Regex(@"[a-z]+", RegexOptions.Compiled);
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]", RegexOptions.Compiled);

            if (!hasMiniMaxChars.IsMatch(password)) {
                errorMessage = "Password must not be less than 8 characters nor greater than 20 characters";
                return false;
            }

            int criteriaMet = 0;

            if (hasUpperChar.IsMatch(password)) criteriaMet++;
            if (hasLowerChar.IsMatch(password)) criteriaMet++;
            if (hasNumber.IsMatch(password)) criteriaMet++;
            if (hasSymbols.IsMatch(password)) criteriaMet++;

            if (criteriaMet >= 3) {
                return true;
            }
            else {
                errorMessage = "Password must contain at least three of the following: uppercase letter, lowercase letter, number, special character.";
                return false;
            }
        }

    }
}
