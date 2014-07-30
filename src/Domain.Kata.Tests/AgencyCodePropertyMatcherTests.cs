using Domain.Kata.Matchers;
using Domain.Kata.Model;
using Domain.Kata.Specifications;
using Ploeh.AutoFixture.Xunit;
using Should;
using Xunit.Extensions;

namespace Domain.Kata.Tests
{
    public class AgencyCodePropertyMatcherTests
    {
        [Theory, AutoData]
        public void When_agency_code_is_identical_then_should_match_identical_agency_code(
            Property matchingAgencyProperty,
            AgencyCodePropertyMatcher sut)
        {
            sut.IsMatch(matchingAgencyProperty, matchingAgencyProperty).ShouldBeTrue();
        }

        [Theory, AutoData]
        public void When_a_different_agency_code_then_should_not_match(
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
        public void When_both_location_and_code_matches_then_should_match(
            Property matchingAgencyProperty,
            HaversineLocationPropertyMatcher locationMatcher,
            AgencyCodePropertyMatcher agencyCodeMatcher)
        {
            var composite = new CompositePropertyMatcher(agencyCodeMatcher).And(locationMatcher);
            composite.IsMatch(matchingAgencyProperty, matchingAgencyProperty).ShouldBeTrue();
        }
    }
}