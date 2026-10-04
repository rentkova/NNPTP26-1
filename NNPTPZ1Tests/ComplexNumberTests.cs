using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class ComplexNumberTest
    {

        [TestMethod]
        public void Add_ShouldReturnSumOfTwoComplexNumbers()
        {
            ComplexNumber first = new ComplexNumber(10, 20);
            ComplexNumber second = new ComplexNumber(1, 2);
            ComplexNumber result = first.Add(second);
            ComplexNumber expected = new ComplexNumber(11, 22);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Add_WhenAddingZero_ShouldReturnSameComplexNumber()
        {
            ComplexNumber number = new ComplexNumber(1, -1);
            ComplexNumber actual = number.Add(ComplexNumber.Zero);
            ComplexNumber expected = new ComplexNumber(1, -1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToString_ShouldReturnComplexNumberInExpectedFormat()
        {
            ComplexNumber number = new ComplexNumber(10, 20);
            string actual = number.ToString();
            Assert.AreEqual("(10 + 20i)", actual);
        }

        [TestMethod]
        public void ToString_WithNegativeImaginaryPart_ShouldPreserveOriginalFormat()
        {
            ComplexNumber number = new ComplexNumber(1, -1);
            string actual = number.ToString();
            Assert.AreEqual("(1 + -1i)", actual);
        }

        [TestMethod]
        public void ToString_ForZero_ShouldReturnZeroInExpectedFormat()
        {
            string actual = ComplexNumber.Zero.ToString();
            Assert.AreEqual("(0 + 0i)", actual);
        }

        [TestMethod]
        [DataRow(0.0, 1.0)]
        [DataRow(1.0, 2.0)]
        [DataRow(2.0, 5.0)]
        public void Eval_ShouldReturnExpectedValue(
            double input,
            double expectedRealPart)
        {
            Polynomial polynomial = CreatePolynomial();
            ComplexNumber actual =
                polynomial.Eval(new ComplexNumber(input, 0));
            ComplexNumber expected =
                new ComplexNumber(expectedRealPart, 0);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToString_ShouldReturnPolynomialInExpectedFormat()
        {
            Polynomial polynomial = CreatePolynomial();
            string actual = polynomial.ToString();
            string expected =
                "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(expected, actual);
        }

        private static Polynomial CreatePolynomial()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.AddCoefficients(
                new List<ComplexNumber>
                {
                    new ComplexNumber(1, 0),
                    ComplexNumber.Zero,
                    new ComplexNumber(1, 0)
                }
            );
            return polynomial;
        }
    }
}