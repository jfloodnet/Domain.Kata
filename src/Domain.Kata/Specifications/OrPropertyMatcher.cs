﻿namespace Domain.Kata.Specifications
{
    public class OrPropertyMatcher : IPropertyMatcher
    {
        private readonly IPropertyMatcher first;
        private readonly IPropertyMatcher second;

        public OrPropertyMatcher(IPropertyMatcher first, IPropertyMatcher second)
        {
            Ensure.NotNull(first, "first");
            Ensure.NotNull(second, "second");

            this.first = first;
            this.second = second;
        }

        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            Ensure.NotNull(agencyProperty, "agencyProperty");
            Ensure.NotNull(databaseProperty, "databaseProperty");

            return
                first.IsMatch(agencyProperty, databaseProperty) ||
                second.IsMatch(agencyProperty, databaseProperty);
        }
    }
}