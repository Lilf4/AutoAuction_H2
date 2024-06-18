
namespace AutoAuction.Tests {


    public class PasswordValidationTest {

        // TODO
        public void IsPasswordValid_InvalidatePassword_ExpectedFalse(string email, string expectedErrorMessage) {
            // Act
            var result = User.IsPasswordValid(email, out string errorMessage);

            // Assert
            Assert.False(result);
            Assert.Contains(expectedErrorMessage, errorMessage);
        }
    }
}
