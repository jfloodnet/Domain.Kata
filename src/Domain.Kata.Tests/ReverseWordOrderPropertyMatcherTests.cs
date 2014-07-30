using Domain.Kata.Matchers;
using Domain.Kata.Model;
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

    public interface IFlagFeatures
    {
        bool IsOn(Feature feature, UserProfile currentUser);
    }

    public class Feature
    {
        public Feature()
        {
        }
    }

    public class FeatureFlagProvider : IFlagFeatures 
    {
        
    }
}