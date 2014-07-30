using System;

namespace Domain.Kata.Model
{
    public struct Coordinate
    {
        private const int earthsRadiusInKm = 6371;

        private readonly double latitude;
        private readonly double longitude;

        public Coordinate(decimal latitude, decimal longitude)
        {
            this.latitude = (double)latitude;
            this.longitude = (double)longitude;
        }

        public double GetDistanceInMetres(Coordinate x)
        {
            double dLat = this.ToRadian(x.latitude - this.latitude);
            double dLon = this.ToRadian(x.longitude - this.longitude);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(this.ToRadian(this.latitude)) * Math.Cos(this.ToRadian(x.latitude)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            double d = earthsRadiusInKm * c;

            return d * 1000;
        }
 
        private double ToRadian(double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}