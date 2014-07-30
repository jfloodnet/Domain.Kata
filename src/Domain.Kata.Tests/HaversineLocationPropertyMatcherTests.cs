using System;
using Domain.Kata.Matchers;
using Domain.Kata.Model;
using Ploeh.AutoFixture;
using Should;
using Xunit;

namespace Domain.Kata.Tests
{
    public class HaversineLocationPropertyMatcherTests
    {
        private readonly Property agencyProperty;
        private readonly Property databaseProperty;

        private readonly HaversineLocationPropertyMatcher sut;

        public HaversineLocationPropertyMatcherTests()
        {
            var fixture = new Fixture().Build<Property>()
                .With(p => p.AgencyCode, "323")
                .With(p => p.Latitude, 33M)
                .With(p => p.Longitude, 151M);

            this.agencyProperty = fixture.Create<Property>();
            this.databaseProperty = fixture.Create<Property>();

            sut = new HaversineLocationPropertyMatcher(sensitivityInMetres: 200);
        }

        [Fact]
        public void Should_throw_argument_null_exception_for_null_agency_property()
        {
            new Action(() => sut.IsMatch(null, databaseProperty)).ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Should_throw_argument_null_exception_for_null_database_property()
        {
            new Action(() => sut.IsMatch(agencyProperty, null)).ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void When_less_than_two_hundred_metres_away_then_should_match_property()
        {
            agencyProperty.Latitude = 32.999098M;
            agencyProperty.Longitude = 150.998147M;
            sut.IsMatch(agencyProperty, databaseProperty).ShouldBeTrue();
        }

        [Fact]
        public void When_more_than_200_metres_away_then_should_NOT_match_property_()
        {
            agencyProperty.Latitude = 32.999053M;
            agencyProperty.Longitude = 150.998054M;

            sut.IsMatch(agencyProperty, databaseProperty).ShouldBeFalse();
        }
    }
}