using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }
        public ComplexNumber(double realPart, double imaginaryPart)
        {
            RealPart = realPart;
            ImaginaryPart = imaginaryPart;
        }

        /// <summary>
        /// represents the complex number zero
        /// </summary>
        public static ComplexNumber Zero => new ComplexNumber(0, 0);

        public ComplexNumber Multiply(ComplexNumber other)
        {
            // (a + bi)(c + di) = (ac - bd) + (ad + bc)i
            double realPart =
                RealPart * other.RealPart - ImaginaryPart * other.ImaginaryPart;
            double imaginaryPart =
                RealPart * other.ImaginaryPart + ImaginaryPart * other.RealPart;
            return new ComplexNumber(realPart, imaginaryPart);
        }
        public ComplexNumber Add(ComplexNumber other)
        {
            double realPart = RealPart + other.RealPart;
            double imaginaryPart = ImaginaryPart + other.ImaginaryPart;
            return new ComplexNumber(realPart, imaginaryPart);
        }

        public ComplexNumber Subtract(ComplexNumber other)
        {
            double realPart = RealPart - other.RealPart;
            double imaginaryPart = ImaginaryPart - other.ImaginaryPart;
            return new ComplexNumber(realPart, imaginaryPart);
        }
        internal ComplexNumber Divide(ComplexNumber other)
        {
            // divide using the complex conjugate of the denominator
            ComplexNumber conjugate = new ComplexNumber(
                other.RealPart,
                -other.ImaginaryPart
            );
            ComplexNumber numerator = Multiply(conjugate);
            double denominator =
                other.RealPart * other.RealPart +
                other.ImaginaryPart * other.ImaginaryPart;

            double realPart = numerator.RealPart / denominator;
            double imaginaryPart = numerator.ImaginaryPart / denominator;

            return new ComplexNumber(realPart, imaginaryPart);
        }

        public double GetMagnitude()
        {
            return Math.Sqrt(
                    RealPart * RealPart +
                    ImaginaryPart * ImaginaryPart
                );
        }

        public double GetAngle()
        {
            double angle =
                Math.Atan(
                    ImaginaryPart / RealPart
                );
            return angle;
        }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber other)
            {
                return RealPart == other.RealPart
                    && ImaginaryPart == other.ImaginaryPart;
            }
            return false;
        }

        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }

        public override int GetHashCode()
        {
            int hashCode = 1382181547;
            hashCode = hashCode * -1521134295 + RealPart.GetHashCode();
            hashCode = hashCode * -1521134295 + ImaginaryPart.GetHashCode();
            return hashCode;
        }
    }
}