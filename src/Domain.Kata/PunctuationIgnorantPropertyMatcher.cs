using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Kata
{
    public class PunctuationIgnorantPropertyMatcher : IPropertyMatcher
    {
        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            Ensure.NotNull(agencyProperty, "agencyProperty");
            Ensure.NotNull(databaseProperty, "databaseProperty");

            var agencyName = StripWhiteSpace(StripPunctuation(agencyProperty.Name));
            var agencyAddress = StripWhiteSpace(StripPunctuation(agencyProperty.Address));

            var databaseName = StripWhiteSpace(StripPunctuation(databaseProperty.Name));
            var databaseAddress = StripWhiteSpace(StripPunctuation(databaseProperty.Address));

            return ToLowerMatch(agencyName, databaseName) &&
                   ToLowerMatch(agencyAddress, databaseAddress);
        }

        private bool ToLowerMatch(string input, string other)
        {
            return string.Equals(input, other, StringComparison.InvariantCultureIgnoreCase);
        }

        private string StripPunctuation(string input)
        {
            return new string(input.Where(c => !char.IsPunctuation(c)).ToArray());
        }

        private string StripWhiteSpace(string input)
        {
            return new string(input.Where(c => !char.IsWhiteSpace(c)).ToArray());
        }
    }
}
