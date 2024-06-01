namespace AutoAuctionTests;


public class UsernameValidationTest {

    public static User SetupTestUser() {
        var user = new User(1, "test@example.com", 12345, 100);
        return user;
    }


    [Theory]
    [InlineData("", "Invalid email format")]
    [InlineData("@missingusername.com", "Invalid email format")]
    [InlineData("username@.com", "Invalid email format")]
    [InlineData("username@domain..com", "Invalid email format")]
    [InlineData("username@domain.c", "Invalid email format")]
    [InlineData("x@host", "Invalid email format")]
    [InlineData("customer/department=shipping@domain.com", "")]
    [Trait ("User - Email Validation", "Invalid Emails")]
    public void IsValidEmail_InvalidateEmail_ExpectedFalse(string email, string expectedErrorMessage) {
        // Arrange
        var user = SetupTestUser();

        // Act
        var result = user.IsEmailValid(email, out string errorMessage);

        // Assert
        Assert.False(result);
        Assert.Contains(expectedErrorMessage, errorMessage);
    }

    [Theory]
    [InlineData("test@domain.com", "")]
    [InlineData("first.test@examptsale.com", "")]
    [InlineData("user@domain.co.uk", "")]
    [Trait("User - Email Validation", "Valid Emails")]
    public void IsValidEmail_ValidateEmail_ExpectedTrue(string email, string expectedErrorMessage) {
        // Arrange
        var user = SetupTestUser();

        // Act
        var result = user.IsEmailValid(email, out string errorMessage);

        // Assert
        Assert.True(result);
        Assert.Contains(expectedErrorMessage, errorMessage);
    }

    [Theory]
    [InlineData("'; EXEC xp_cmdshell('dir');--@example.com")]
    [InlineData("' OR 1=1 --@example.com")]
    [InlineData("admin'--@example.com")]
    [InlineData("admin' #@example.com")]
    [Trait("User - Email Validation", "Sql-Injection")]
    public void IsValidEmail_SqlInjectionEmail_ExpectFalse(string email) {
        // Arrange
        var user = SetupTestUser();

        // Act
        var result = user.IsEmailValid(email, out string errorMessage);

        // Assert
        Assert.False(result);
        Assert.Contains("Invalid email format", errorMessage);
    }
}