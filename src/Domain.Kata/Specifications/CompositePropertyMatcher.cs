using Domain.Kata.Model;

namespace Domain.Kata.Specifications
{
    public class CompositePropertyMatcher : IPropertyMatcher
    {
        private IPropertyMatcher inner;

        public CompositePropertyMatcher(IPropertyMatcher first)
        {
            Ensure.NotNull(first, "first");

            this.inner = first;
        }

        public IPropertyMatcher And(IPropertyMatcher matcher)
        {
            Ensure.NotNull(matcher, "matcher");

            inner = new AndPropertyMatcher(matcher, inner);
            return this;
        }

        public IPropertyMatcher Or(IPropertyMatcher matcher)
        {
            Ensure.NotNull(matcher, "matcher");

            inner = new OrPropertyMatcher(matcher, inner);
            return this;
        }

        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            Ensure.NotNull(agencyProperty, "agencyProperty");
            Ensure.NotNull(databaseProperty, "databaseProperty");

            return inner.IsMatch(agencyProperty, databaseProperty);
        }
    }
}