
namespace AutoAuction.Tests {


    public class PasswordValidationTest {

        
        [Theory]
        [InlineData("Password123!", true, "")]
        [InlineData("Pass1234", true, "")]
        [InlineData("Pa@ssw0rd", true, "")]
        [InlineData("MixedCASE12$", true, "")]
        [InlineData("1234!abcdEF", true, "")]
        [InlineData("PerRules123!", true, "")]
        public void TestIsPasswordValid_PassingCases(string password, bool expectedIsValid, string expectedErrorMessage)
        {
            var result = User.IsPasswordValid(password, out var errorMessage);
            Assert.Equal(expectedIsValid, result);
            Assert.Equal(expectedErrorMessage, errorMessage);
        }

        [Theory]
        [InlineData("", false, "Password should not be empty")]
        [InlineData("pass", false, "Password must not be less than 8 characters nor greater than 20 characters")]
        [InlineData("ThisPasswordIsWayTooLong123", false, "Password must not be less than 8 characters nor greater than 20 characters")]
        [InlineData("PASSWORD", false, "Password must contain at least three of the following: uppercase letter, lowercase letter, number, special character.")]
        [InlineData("password", false, "Password must contain at least three of the following: uppercase letter, lowercase letter, number, special character.")]
        [InlineData("12345678", false, "Password must contain at least three of the following: uppercase letter, lowercase letter, number, special character.")]
        [InlineData("pass5678", false, "Password must contain at least three of the following: uppercase letter, lowercase letter, number, special character.")]
        [InlineData("'; DROP TABLE Users; --", false, "Password must not be less than 8 characters nor greater than 20 characters")]
        public void TestIsPasswordValid_FailingCases(string password, bool expectedIsValid, string expectedErrorMessage)
        {
            
                var result = User.IsPasswordValid(password, out var errorMessage);
                Assert.Equal(expectedIsValid, result);
                Assert.Equal(expectedErrorMessage, errorMessage);
            
        }

    }
}
