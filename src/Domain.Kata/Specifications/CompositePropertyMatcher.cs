using Domain.Kata.Model;

namespace Domain.Kata.Specifications
{
    public class CompositePropertyMatcher : IPropertyMatcher
    {
        private readonly IPropertyMatcher inner;

        public CompositePropertyMatcher(IPropertyMatcher first)
        {
            Ensure.NotNull(first, "first");

            this.inner = first;
        }

        public CompositePropertyMatcher And(IPropertyMatcher matcher)
        {
            Ensure.NotNull(matcher, "matcher");

            return new CompositePropertyMatcher(
                new AndPropertyMatcher(matcher, inner));
        }

        public CompositePropertyMatcher Or(IPropertyMatcher matcher)
        {
            Ensure.NotNull(matcher, "matcher");

            return new CompositePropertyMatcher(
                new OrPropertyMatcher(matcher, inner));
        }

        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            Ensure.NotNull(agencyProperty, "agencyProperty");
            Ensure.NotNull(databaseProperty, "databaseProperty");

            return inner.IsMatch(agencyProperty, databaseProperty);
        }
    }
}