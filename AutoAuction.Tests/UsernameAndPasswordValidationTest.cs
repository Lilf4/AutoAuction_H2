using Xunit;

namespace AutoAuctionTests;

public class UsernameAndPasswordValidationTest {

    [Theory]
    [InlineData("invalid-email", "Invalid email format")]
    [InlineData("", "Invalid email format")]
    [InlineData("plainaddress", "Invalid email format")]
    [InlineData("@missingusername.com", "Invalid email format")]
    [InlineData("username@.com", "Invalid email format")]
    [InlineData("username@domain..com", "Invalid email format")]
    [InlineData("username@domain.c", "Invalid email format")]
    [InlineData("x@host", "Invalid email format")]
    [InlineData("customer/department=shipping@domain.com", "")]
    [InlineData("user+mailbox@domain.com", "Invalid email format")]
    [Trait ("User - Email Validation", "Invalid Emails")]
    public void IsValidEmail_InvalidateEmail(string email, string expectedErrorMessage) {
        // Arrange
        var user = new User(1, "test@example.com", 12345, 100);

        // Act
        var result = user.IsValidEmail(email, out string errorMessage);

        // Assert
        Assert.False(result);
        Assert.Contains(expectedErrorMessage, errorMessage);
    }

    [Theory]
    [InlineData("test@domain.com", "")]
    [InlineData("first.test@examptsale.com", "")]
    [InlineData("user@subdomain.domain.com", "")]
    [InlineData("user@domain.co.uk", "")]
    [Trait("User - Email Validation", "Valid Emails")]
    public void IsValidEmail_ValidateEmail(string email, string expectedErrorMessage) {
        // Arrange
        var user = new User(1, "test@example.com", 12345, 100);

        // Act
        var result = user.IsValidEmail(email, out string errorMessage);

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
    public void IsValidEmail_SqlInjectionEmail(string email) {
        // Arrange
        var user = new User(1, "test@example.com", 12345, 100);

        // Act
        var result = user.IsValidEmail(email, out string errorMessage);

        // Assert
        Assert.False(result);
        Assert.Contains("Invalid email format", errorMessage);
    }
}