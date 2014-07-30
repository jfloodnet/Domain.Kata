using System.Device.Location;
using Domain.Kata.Model;
using Should;
using Xunit;

namespace Domain.Kata.Tests
{
    public class CoordinateTests
    {
        [Fact]
        public void Should_be_within_the_range_of_half_a_metre_to_microsofts_implementation()
        {
            var sut = new Coordinate(33, 151);
            var other = new Coordinate(32.999098M, 150.998147M); //200 metres away

            var ms = new GeoCoordinate(33, 151);
            var otherMs = new GeoCoordinate(32.999098, 150.998147);

            var result = sut.GetDistanceInMetres(other);
            var expected = ms.GetDistanceTo(otherMs);

            result.ShouldBeInRange(expected - .5, expected + .5);
        }
    }
}