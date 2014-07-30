﻿using System;
using Domain.Kata.LocationPropertyMatchers;
using Ploeh.AutoFixture;
using Should;
using Xunit;

namespace Domain.Kata.Tests
{
    public class ReverseWordOrderPropertyMatcherTests
    {
        private readonly Property agencyProperty;
        private readonly Property databaseProperty;

        private readonly ReverseWordOrderPropertyMatcher sut;

        public ReverseWordOrderPropertyMatcherTests()
        {
            var fixture = new Fixture();

            this.agencyProperty = fixture.Create<Property>();
            this.databaseProperty = fixture.Create<Property>();

            sut = new ReverseWordOrderPropertyMatcher();
        }

        [Fact]
        public void Should_match_reverse_order_words()
        {
            agencyProperty.Name = "The Summit Apartments";
            databaseProperty.Name = "Apartments Summit The";
            sut.IsMatch(agencyProperty, databaseProperty).ShouldBeTrue();
        }

        [Fact]
        public void Should_NOT_match_out_of_order_words()
        {
            agencyProperty.Name = "The Summit Apartments";
            databaseProperty.Name = "Summit Apartments The";
            sut.IsMatch(agencyProperty, databaseProperty).ShouldBeFalse();
        }
    }

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
        public void Should_match_property_when_less_than_two_hundred_metres_away()
        {
            agencyProperty.Latitude = 32.999098M;
            agencyProperty.Longitude = 150.998147M;
            sut.IsMatch(agencyProperty, databaseProperty).ShouldBeTrue();
        }

        [Fact]
        public void Should_not_match_property_when_more_than_200_metres_away()
        {
            agencyProperty.Latitude = 32.999053M;
            agencyProperty.Longitude = 150.998054M;

            sut.IsMatch(agencyProperty, databaseProperty).ShouldBeFalse();
        }
    }
}