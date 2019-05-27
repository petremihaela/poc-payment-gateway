using PaymentService.Core.Helpers;
using Shouldly;
using System;
using Xunit;

namespace PaymentService.UnitTests
{
    public class CardHelperTests
    {
        [Fact]
        public void WhenAValidOnlyDigitsCardNumberIsUsed_IsValidCardNumberShouldReturnTrue()
        {
            //Arrange
            const string cardNumber = "1234567899";

            //Act
            var isValidCardNumber = CardHelper.IsValidCardNumber(cardNumber);

            //Assert
            isValidCardNumber.ShouldBe(true);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WhenANullOrEmptyCardNumberIsUsed_IsValidCardNumberShouldThrowArgumentException(string cardNumber)
        {
            //Arrange
            Should.Throw(() => CardHelper.IsValidCardNumber(cardNumber), typeof(ArgumentException));
        }

        [Theory]
        [InlineData("1234")]
        [InlineData("12345123451234512345")]
        public void WhenACardNumberWithInvalidLengthIsUsed_IsValidCardNumberShouldThrowFormatException(string cardNumber)
        {
            //Assert
            Should.Throw(() => CardHelper.IsValidCardNumber(cardNumber), typeof(FormatException));
        }

        [Fact]
        public void WhenAValidCardNumberIsUsed_MaskShouldReturnMaskedCardNumberWithTheLastFourDigitsNotMasked()
        {
            //Arrange
            const string cardNumber = "123456781234";

            //Act
            var maskedCardNumber = CardHelper.Mask(cardNumber);

            //Assert
            maskedCardNumber.ShouldBe("********1234");
        }
    }
}