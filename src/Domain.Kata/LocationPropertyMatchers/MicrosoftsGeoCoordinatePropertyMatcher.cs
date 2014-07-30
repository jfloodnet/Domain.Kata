using System.Device.Location;

namespace Domain.Kata.LocationPropertyMatchers
{
    public class MicrosoftsGeoCoordinatePropertyMatcher : IPropertyMatcher
    {
        private readonly double sensitivityInMetres;

        public MicrosoftsGeoCoordinatePropertyMatcher(double sensitivityInMetres)
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

            var sCoord = new GeoCoordinate((double)agencyProperty.Latitude, (double)agencyProperty.Longitude);
            var eCoord = new GeoCoordinate((double)databaseProperty.Latitude, (double)databaseProperty.Longitude);

            return sCoord.GetDistanceTo(eCoord) <= sensitivityInMetres;
        }
    }
}