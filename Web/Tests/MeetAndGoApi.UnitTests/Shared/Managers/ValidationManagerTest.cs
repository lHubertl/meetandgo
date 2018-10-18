using System.Linq;
using MeetAndGo.Shared.Managers;
using MeetAndGo.Shared.Models;
using NUnit.Framework;

namespace MeetAndGoApi.UnitTests.Shared.Managers
{
    [TestFixture]
    public class ValidationManagerTest
    {
        [Test]
        public void Validate_PhoneNumber_Valide()
        {
            // Arrange
            var error = string.Empty;
            var phoneNumber1 = "+380938632760";
            var phoneNumber2 = "+(380)938632760";
            var phoneNumber3 = "(380)938632760";
            var phoneNumber4 = "+(380)938-632760";
            var phoneNumber5 = "0938632760";

            // Act

            var validationManager = ValidationManager.Create()
                .ValidatePhoneNumber(phoneNumber1, error)
                .ValidatePhoneNumber(phoneNumber2, error)
                .ValidatePhoneNumber(phoneNumber3, error)
                .ValidatePhoneNumber(phoneNumber4, error)
                .ValidatePhoneNumber(phoneNumber5, error);

            // Assert
            Assert.IsTrue(validationManager.IsValid);
        }

        [Test]
        public void Validate_PhoneNumber_AllInvalide()
        {
            // Arrange
            var phoneNumber1 = "KC38632760";
            var phoneNumber2 = "-380938632760";
            var phoneNumber3 = "(380)93863276B";
            var phoneNumber4 = "38093-8632760";
            var phoneNumber5 = "0000";

            // Act

            var validationManager = ValidationManager.Create()
                .ValidatePhoneNumber(phoneNumber1, phoneNumber1)
                .ValidatePhoneNumber(phoneNumber2, phoneNumber2)
                .ValidatePhoneNumber(phoneNumber3, phoneNumber3)
                .ValidatePhoneNumber(phoneNumber4, phoneNumber4)
                .ValidatePhoneNumber(phoneNumber5, phoneNumber5);

            // Assert
            Assert.IsFalse(validationManager.IsValid);
            Assert.IsTrue(validationManager.Errors.Count == 5);
        }

        [Test]
        public void Validate_PhoneNumber_ThirdOnlyInvalide()
        {
            // Arrange
            var phoneNumber1 = "+380938632760";
            var phoneNumber2 = "+(380)938632760";
            var phoneNumber3 = "(380)93863276B";
            var phoneNumber4 = "+(380)938-632760";
            var phoneNumber5 = "0938632760";

            // Act

            var validationManager = ValidationManager.Create()
                .ValidatePhoneNumber(phoneNumber1, phoneNumber1)
                .ValidatePhoneNumber(phoneNumber2, phoneNumber2)
                .ValidatePhoneNumber(phoneNumber3, phoneNumber3)
                .ValidatePhoneNumber(phoneNumber4, phoneNumber4)
                .ValidatePhoneNumber(phoneNumber5, phoneNumber5);

            var result = validationManager.Errors.FirstOrDefault()?.Equals(phoneNumber3) ?? false;

            // Assert
            Assert.IsFalse(validationManager.IsValid);
            Assert.IsTrue(result);
        }
    }
}
