using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; }

        /// <summary>
        /// initializes a new polynomial with an empty list of coefficients
        /// </summary>
        public Polynomial()
        {
            Coefficients = new List<ComplexNumber>();
        }

        /// <summary>
        /// adds coefficients to the polynomial
        /// </summary>
        /// <param name="coefficients">the coefficients to add</param>
        public void AddCoefficients(List<ComplexNumber> coefficients)
        {
            Coefficients.AddRange(coefficients);
        }


        /// <summary>
        /// creates the derivative of this polynomial
        /// </summary>
        /// <returns>the derived polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial derivative = new Polynomial();
            for (int index = 1; index < Coefficients.Count; index++)
            {
                derivative.Coefficients.Add(
                    Coefficients[index].Multiply(
                        new ComplexNumber(index, 0)
                    )
                );
            }
            return derivative;
        }

        /// <summary>
        /// evaluates the polynomial at the given value
        /// </summary>
        /// <param name="value">the value at which the polynomial is evaluated</param>
        /// <returns>the result of the polynomial evaluation</returns>
        public ComplexNumber Eval(double value)
        {
            return Eval(
                new ComplexNumber(value, 0)
            );
        }

        /// <summary>
        /// evaluates the polynomial at the given complex value
        /// </summary>
        /// <param name="value">the complex value at which the polynomial is evaluated</param>
        /// <returns>the result of the polynomial evaluation</returns>
        public ComplexNumber Eval(ComplexNumber value)
        {
            ComplexNumber result = ComplexNumber.Zero;
            for (int index = 0; index < Coefficients.Count; index++)
            {
                ComplexNumber coefficient = Coefficients[index];
                ComplexNumber valuePower = value;

                if (index > 0)
                {
                    for (int power = 0; power < index - 1; power++)
                    {
                        valuePower = valuePower.Multiply(value);
                    }
                    coefficient = coefficient.Multiply(valuePower);
                }
                result = result.Add(coefficient);
            }
            return result;
        }
        /// <summary>
        /// returns a string representation of the polynomial
        /// </summary>
        /// <returns>the polynomial represented as a string</returns>
        public override string ToString()
        {
            string result = "";
            for (int index = 0; index < Coefficients.Count; index++)
            {
                result += Coefficients[index];
                for (int power = 0; power < index; power++)
                {
                    result += "x";
                }
                if (index + 1 < Coefficients.Count)
                {
                    result += " + ";
                }
            }
            return result;
        }
    }
}