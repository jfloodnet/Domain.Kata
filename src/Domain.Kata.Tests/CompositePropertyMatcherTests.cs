using Domain.Kata.Specifications;
using Should;
using Xunit;

namespace Domain.Kata.Tests
{
    public class CompositePropertyMatcherTests
    {
        private readonly TruePropertyMatcher trueMatcher;
        private readonly FalsePropertyMatcher falseMatcher;
        private readonly Property anonymousProperty;

        public CompositePropertyMatcherTests()
        {
            trueMatcher = new TruePropertyMatcher();
            falseMatcher = new FalsePropertyMatcher();
            anonymousProperty = new Property();
        }

        [Fact]
        public void When_specifying_AND_condition_then_should_NOT_match_with_both_false_property_matcher()
        {
            var sut = new CompositePropertyMatcher(trueMatcher);

            sut.And(falseMatcher).IsMatch(anonymousProperty, anonymousProperty).ShouldBeFalse();
        }

        [Fact]
        public void When_specifying_AND_condition_then_should_match_with_true_property_matcher()
        {
            var sut = new CompositePropertyMatcher(trueMatcher);

            sut.And(trueMatcher).IsMatch(anonymousProperty, anonymousProperty).ShouldBeTrue();
        }

        [Fact]
        public void When_specifying_OR_condition_then_should_NOT_match_with_both_false_property_matcher()
        {
            var sut = new CompositePropertyMatcher(falseMatcher);

            sut.Or(falseMatcher).IsMatch(anonymousProperty, anonymousProperty).ShouldBeFalse();
        }


        [Fact]
        public void When_specifying_OR_condition_then_should_match_with_true_property_matcher()
        {
            var sut = new CompositePropertyMatcher(trueMatcher);

            sut.Or(trueMatcher).IsMatch(anonymousProperty, anonymousProperty).ShouldBeTrue();
        }


        [Fact]
        public void When_specifying_OR_condition_then_should_match_with_a_true_and_false_property_matcher()
        {
            var sut = new CompositePropertyMatcher(trueMatcher);

            sut.Or(falseMatcher).IsMatch(anonymousProperty, anonymousProperty).ShouldBeTrue();
        }
    }

    public class FalsePropertyMatcher : IPropertyMatcher
    {
        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            return false;
        }
    }

    public class TruePropertyMatcher : IPropertyMatcher
    {
        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            return true;
        }
    }
}