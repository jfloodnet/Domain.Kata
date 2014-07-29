using Ploeh.AutoFixture.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;
using Should;
using Ploeh.AutoFixture;

namespace Domain.Kata.Tests
{
    public class PunctuationInvariantPropertyMatcherTests
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

    public class LocationAndAgencyCodePropertyMatcherTests
    {
        private const decimal smallestLevelOfPrecision = .0000000000000000000000000001M;

        private readonly Property agencyProperty;
        private readonly Property databaseProperty;        

        public LocationAndAgencyCodePropertyMatcherTests()
        {
            var fixture = new Fixture().Build<Property>()
                    .With(p => p.AgencyCode, "323")
                    .With(p => p.Latitude, -33.7024400M)
                    .With(p => p.Longitude, 151.0993100M);

            this.agencyProperty = fixture.Create<Property>();
            this.databaseProperty = fixture.Create<Property>();          
        }

        [Theory, AutoData]
        public void Should_throw_argument_null_exception_for_null_agency_property(
            LocationAndAgencyCodePropertyMatcher sut)
        {
            new Action(() => sut.IsMatch(null, databaseProperty)).ShouldThrow<ArgumentNullException>();
        }

        [Theory, AutoData]
        public void Should_throw_argument_null_exception_for_null_database_property(
            LocationAndAgencyCodePropertyMatcher sut)
        {
            new Action(() => sut.IsMatch(agencyProperty, null)).ShouldThrow<ArgumentNullException>();
        }

        [Theory, AutoData]
        public void Should_match_property(
            LocationAndAgencyCodePropertyMatcher sut)
        {
            agencyProperty.Latitude = -33.7024400M + sut.SensitivityInDegrees;
            sut.IsMatch(agencyProperty, databaseProperty).ShouldBeTrue();
        }

        [Theory, AutoData]
        public void Should_not_match_property(
            LocationAndAgencyCodePropertyMatcher sut)
        {
            agencyProperty.Latitude = -33.7024400M + sut.SensitivityInDegrees + smallestLevelOfPrecision;
            sut.IsMatch(agencyProperty, databaseProperty).ShouldBeFalse();
        }

        [Theory, AutoData]
        public void Should_match_property_when_lat_and_long_are_off_by_half_the_sensitivity_in_degrees(
            LocationAndAgencyCodePropertyMatcher sut)
        {            
            agencyProperty.Latitude = -33.7024400M + (sut.SensitivityInDegrees * (2/3));
            agencyProperty.Longitude = 151.0993100M + (sut.SensitivityInDegrees * (2/3));

            sut.IsMatch(agencyProperty, databaseProperty).ShouldBeTrue();
        }

        [Theory, AutoData]
        public void Should_NOT_match_property_when_lat_or_long_are_off_by_more_than_half_the_sensitivity_in_degrees(
            LocationAndAgencyCodePropertyMatcher sut)
        {
            agencyProperty.Latitude = agencyProperty.Latitude + (sut.SensitivityInDegrees / 2) + smallestLevelOfPrecision;
            agencyProperty.Longitude = agencyProperty.Longitude + (sut.SensitivityInDegrees / 2) + smallestLevelOfPrecision;

            sut.IsMatch(agencyProperty, databaseProperty).ShouldBeFalse();
        }
    }
}
