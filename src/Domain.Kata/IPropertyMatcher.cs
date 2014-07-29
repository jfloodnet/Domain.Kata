using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace Domain.Kata
{
    public interface IPropertyMatcher
    {
        bool IsMatch(Property agencyProperty, Property databaseProperty);
    }

    public class LocationAndAgencyCodePropertyMatcher : IPropertyMatcher
    {
        private const double sensitivityInMetres = 200;
        private const decimal numberOfMetresInOneDegree = 111000;

        public decimal SensitivityInDegrees
        {
            get
            {
                return (decimal)sensitivityInMetres / numberOfMetresInOneDegree;
            }
        }

        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            Ensure.NotNull(agencyProperty, "agencyProperty");
            Ensure.NotNull(databaseProperty, "databaseProperty");

            var sCoord = new Coordinate(agencyProperty.Latitude, agencyProperty.Longitude);
            var eCoord = new Coordinate(databaseProperty.Latitude, databaseProperty.Longitude);    

            return sCoord.GetDistanceInMetresTo(eCoord) <= sensitivityInMetres;
        }

        private class Coordinate
        {
            private const double numberOfMetresInOneDegree = 111000;

            private readonly double latitude;
            private readonly double longitude;

            public Coordinate(decimal latitude, decimal longitude)
            {
                this.latitude = (double)latitude;
                this.longitude = (double)longitude;
            }

            public double GetDistanceInMetresTo(Coordinate x)
            {
                double latDifference = this.latitude - x.latitude;
                double latDistanceInMetres = latDifference * numberOfMetresInOneDegree;

                double lonDifference = this.longitude - x.longitude;
                double avgLatitude = (this.latitude + x.latitude) / 2;
                double lonDistanceInMetres = lonDifference * numberOfMetresInOneDegree * Math.Cos(avgLatitude);

                return Math.Sqrt(Math.Pow((double)latDistanceInMetres, 2) + Math.Pow((double)lonDistanceInMetres, 2));
            }
        }
    }
}
