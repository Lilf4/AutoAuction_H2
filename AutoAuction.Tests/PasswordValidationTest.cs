
namespace AutoAuction.Tests {


    public class PasswordValidationTest {

        
        public void IsPasswordValid_InvalidatePassword_ExpectedFalse(string email, string expectedErrorMessage) {
            // Arrange
            var user = new User(1, "test@example.com", 12345, 100);

            // Act
            var result = user.IsPasswordValid(email, out string errorMessage);

            // Assert
            Assert.False(result);
            Assert.Contains(expectedErrorMessage, errorMessage);
        }
    }
}
