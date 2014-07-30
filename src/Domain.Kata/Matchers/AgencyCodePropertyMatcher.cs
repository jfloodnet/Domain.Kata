using System;
using Domain.Kata.Model;

namespace Domain.Kata.Matchers
{
    public class AgencyCodePropertyMatcher : IPropertyMatcher
    {
        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            Ensure.NotNull(agencyProperty, "agencyProperty");
            Ensure.NotNull(databaseProperty, "databaseProperty");

            return string.Equals(agencyProperty.AgencyCode, databaseProperty.AgencyCode,
                StringComparison.InvariantCultureIgnoreCase);
        }
    }
}