using System.Linq;
using Domain.Kata.Model;

namespace Domain.Kata.Matchers
{
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