using Domain.Kata.Model;
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
        public void When_specifying_AND_condition_then_should_NOT_match_with_at_least_one_false_property_matcher()
        {
            var sut = new CompositePropertyMatcher(trueMatcher).And(falseMatcher);
            sut.IsMatch(anonymousProperty, anonymousProperty).ShouldBeFalse();
        }

        [Fact]
        public void When_specifying_AND_condition_then_should_match_with_two_true_property_matchers()
        {
            var sut = new CompositePropertyMatcher(trueMatcher).And(trueMatcher);
            sut.IsMatch(anonymousProperty, anonymousProperty).ShouldBeTrue();
        }

        [Fact]
        public void When_specifying_OR_condition_then_should_NOT_match_with_both_false_property_matcher()
        {
            var sut = new CompositePropertyMatcher(falseMatcher).Or(falseMatcher);
            sut.IsMatch(anonymousProperty, anonymousProperty).ShouldBeFalse();
        }

        [Fact]
        public void When_specifying_OR_condition_then_should_match_with_true_property_matcher()
        {
            var sut = new CompositePropertyMatcher(trueMatcher).Or(trueMatcher);
            sut.IsMatch(anonymousProperty, anonymousProperty).ShouldBeTrue();
        }

        [Fact]
        public void When_specifying_OR_condition_then_should_match_with_at_least_one_property_matcher()
        {
            var sut = new CompositePropertyMatcher(trueMatcher).Or(falseMatcher);
            sut.IsMatch(anonymousProperty, anonymousProperty).ShouldBeTrue();
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