using Domain.Kata.Matchers;
using Domain.Kata.Model;
using Ploeh.AutoFixture.Xunit;
using System;
using Xunit.Extensions;
using Should;

namespace Domain.Kata.Tests
{
    public class PunctuationIgnorantPropertyMatcherTests
    {
        [Theory, AutoData]
        public void Should_throw_argumentnull_exception_for_null_agency_property(
            Property databaseProperty,
            PunctuationIgnorantPropertyMatcher sut)
        {
            new Action(() => sut.IsMatch(null, databaseProperty)).ShouldThrow<ArgumentNullException>();
        }

        [Theory, AutoData]
        public void Should_throw_argumentnull_exception_for_null_database_property(
            Property agencyProperty,
            PunctuationIgnorantPropertyMatcher sut)
        {
            new Action(() => sut.IsMatch(agencyProperty, null)).ShouldThrow<ArgumentNullException>();
        }

        [Theory, AutoData]
        public void Should_match_property(
            Property agencyProperty, 
            Property databaseProperty, 
            PunctuationIgnorantPropertyMatcher sut)
        {
            agencyProperty.Name = "*Super*-High! APARTMENTS (Sydney)";
            agencyProperty.Address = "32 Sir John-Young Crescent, Sydney, NSW";

            databaseProperty.Name = "Super High Apartments, Sydney";
            databaseProperty.Address = "32 Sir John Young Crescent, Sydney NSW";

            sut.IsMatch(agencyProperty, databaseProperty).ShouldBeTrue();
        }

        [Theory, AutoData]
        public void Should_not_match_property(
            Property agencyProperty,
            Property databaseProperty,
            PunctuationIgnorantPropertyMatcher sut)
        {
            agencyProperty.Name = "*Fairly*-High! APARTMENTS (Sydney)";
            agencyProperty.Address = "42 Mr John-Young Crescent, Sydney, NSW";

            databaseProperty.Name = "Super High Apartments, Sydney";
            databaseProperty.Address = "32 Sir John Young Crescent, Sydney NSW";

            sut.IsMatch(agencyProperty, databaseProperty).ShouldBeFalse();
        }
    }
}
