using Domain.Kata.LocationPropertyMatchers;
using Domain.Kata.Specifications;
using Ploeh.AutoFixture.Xunit;
using Should;
using Xunit.Extensions;

namespace Domain.Kata.Tests
{
    public class AgencyCodePropertyMatcherTests
    {
        [Theory, AutoData]
        public void Should_match_identical_agency_code(
            Property matchingAgencyProperty,
            AgencyCodePropertyMatcher sut)
        {
            sut.IsMatch(matchingAgencyProperty, matchingAgencyProperty).ShouldBeTrue();
        }

        [Theory, AutoData]
        public void Should_not_match_different_agency_code(
            Property property,
            Property differentPropery,
            AgencyCodePropertyMatcher sut)
        {
            sut.IsMatch(property, differentPropery).ShouldBeFalse();
        }

        [Theory, AutoData]
        public void Should_ignore_casing(
            Property agencyProperty,
            Property databaseProperty,
            AgencyCodePropertyMatcher sut)
        {
            agencyProperty.AgencyCode = "CODE";
            databaseProperty.AgencyCode = "code";

            sut.IsMatch(agencyProperty, databaseProperty).ShouldBeTrue();
        }

        [Theory, AutoData]
        public void Should_match_when_both_location_and_code_matches(
            Property matchingAgencyProperty,
            HaversineLocationPropertyMatcher locationMatcher,
            AgencyCodePropertyMatcher agencyCodeMatcher)
        {
            var composite = new CompositePropertyMatcher(agencyCodeMatcher).And(locationMatcher);
            composite.IsMatch(matchingAgencyProperty, matchingAgencyProperty).ShouldBeTrue();
        }
    }
}