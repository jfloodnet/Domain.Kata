using Domain.Kata.Model;

namespace Domain.Kata.Matchers
{
    public class HaversineLocationPropertyMatcher : IPropertyMatcher
    {
        private readonly double sensitivityInMetres;

        public HaversineLocationPropertyMatcher(double sensitivityInMetres)
        {
            this.sensitivityInMetres = sensitivityInMetres;
        }

        public double SensitivityInMetres
        {
            get
            {

                return sensitivityInMetres;
            }
        }

        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            Ensure.NotNull(agencyProperty, "agencyProperty");
            Ensure.NotNull(databaseProperty, "databaseProperty");

            var sCoord = new Coordinate(agencyProperty.Latitude, agencyProperty.Longitude);
            var eCoord = new Coordinate(databaseProperty.Latitude, databaseProperty.Longitude);

            return sCoord.GetDistanceInMetres(eCoord) <= sensitivityInMetres;
        }
    }
}