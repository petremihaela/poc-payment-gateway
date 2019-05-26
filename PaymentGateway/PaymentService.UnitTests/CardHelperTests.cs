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
            var cardNumber = "1234567899";

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
            //Act
            Action isValidCardNumberAction = () => CardHelper.IsValidCardNumber(cardNumber);

            //Arrange
            Should.Throw(isValidCardNumberAction, typeof(ArgumentException));
        }

        [Theory]
        [InlineData("1234")]
        [InlineData("12345123451234512345")]
        public void WhenACardNumberWithInvalidLenghtIsUsed_IsValidCardNumberShouldThrowFormatException(string cardNumber)
        {
            //Act
            Action isValidCardNumberAction = () => CardHelper.IsValidCardNumber(cardNumber);

            //Assert
            Should.Throw(isValidCardNumberAction, typeof(FormatException));
        }

        [Fact]
        public void WhenAValidCardNumberIsUsed_MaskShouldReturnMaskedCardNumberWithTheLastFourDigitsNotMasked()
        {
            //Arrange
            var cardNumber = "123456781234";

            //Act
            var maskedCardNumber = CardHelper.Mask(cardNumber);

            //Assert
            maskedCardNumber.ShouldBe("********1234");
        }
    }
}