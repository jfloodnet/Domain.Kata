using System;
using System.Linq;

namespace Domain.Kata
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

    public class ReverseWordOrderPropertyMatcher : IPropertyMatcher
    {
        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            Ensure.NotNull(agencyProperty, "agencyProperty");
            Ensure.NotNull(databaseProperty, "databaseProperty");

            var agencyWordsInOrder = agencyProperty.Name.Split(' ').Reverse();
            var databaseWordsInOrder = databaseProperty.Name.Split(' ');

            return agencyWordsInOrder.SequenceEqual(databaseWordsInOrder);
        }
    }
}