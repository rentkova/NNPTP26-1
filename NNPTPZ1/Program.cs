
using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        private const double ZeroCoordinateReplacement = 0.0001;

        static void Main(string[] args)
        {
            int width = int.Parse(args[0]);
            int height = int.Parse(args[1]);

            double xMin = double.Parse(args[2]);
            double xMax = double.Parse(args[3]);
            double yMin = double.Parse(args[4]);
            double yMax = double.Parse(args[5]);

            string outputName = args[6];

            Bitmap bitmap = new Bitmap(width, height);

            double xStep = (xMax - xMin) / width;
            double yStep = (yMax - yMin) / height;

            List<ComplexNumber> roots = new List<ComplexNumber>();

            Polynomial polynomial = CreatePolynomial();
            Polynomial derivative = polynomial.Derive();

            Console.WriteLine(polynomial);
            Console.WriteLine(derivative);

            Color[] colors = CreateColors();

            GenerateFractal(
                bitmap,
                width,
                height,
                xMin,
                yMin,
                xStep,
                yStep,
                polynomial,
                derivative,
                roots,
                colors
            );

            bitmap.Save(outputName ?? "../../../out.png");
        }

        private static Polynomial CreatePolynomial()
        {
            Polynomial polynomial = new Polynomial();

            polynomial.AddCoefficients(
                new List<ComplexNumber>
                {
                    new ComplexNumber(1, 0),
                    ComplexNumber.Zero,
                    ComplexNumber.Zero,
                    new ComplexNumber(1, 0)
                }
            );

            return polynomial;
        }

        private static Color[] CreateColors()
        {
            return new Color[]
            {
                Color.Red,
                Color.Blue,
                Color.Green,
                Color.Yellow,
                Color.Orange,
                Color.Fuchsia,
                Color.Gold,
                Color.Cyan,
                Color.Magenta
            };
        }

        private static void GenerateFractal(
            Bitmap bitmap,
            int width,
            int height,
            double xMin,
            double yMin,
            double xStep,
            double yStep,
            Polynomial polynomial,
            Polynomial derivative,
            List<ComplexNumber> roots,
            Color[] colors)
        {
            // generate a color for every pixel in image
            for (int firstIndex = 0; firstIndex < width; firstIndex++)
            {
                for (int secondIndex = 0; secondIndex < height; secondIndex++)
                {
                    GeneratePixel(
                        bitmap,
                        firstIndex,
                        secondIndex,
                        xMin,
                        yMin,
                        xStep,
                        yStep,
                        polynomial,
                        derivative,
                        roots,
                        colors
                    );
                }
            }
        }

        private static void GeneratePixel(
            Bitmap bitmap,
            int firstIndex,
            int secondIndex,
            double xMin,
            double yMin,
            double xStep,
            double yStep,
            Polynomial polynomial,
            Polynomial derivative,
            List<ComplexNumber> roots,
            Color[] colors)
        {
            // calculate coordinates represented by the current pixel
            double yCoordinate = yMin + firstIndex * yStep;
            double xCoordinate = xMin + secondIndex * xStep;

            ComplexNumber startPoint =
                CreateStartPoint(xCoordinate, yCoordinate);

            // find the root using Newton's method
            (ComplexNumber root, float iterationCount) =
                FindRoot(polynomial, derivative, startPoint);

            // get index used to identify the found root
            int rootIndex =
                GetOrAddRootIndex(root, roots);

            // determine pixel color based on the root and iteration count
            Color pixelColor =
                GetPixelColor(rootIndex, iterationCount, colors);

            bitmap.SetPixel(secondIndex, firstIndex, pixelColor);
        }

        private static ComplexNumber CreateStartPoint(
            double xCoordinate,
            double yCoordinate)
        {
            // avoid using zero as a starting coordinate
            if (xCoordinate == 0)
                xCoordinate = ZeroCoordinateReplacement;

            if (yCoordinate == 0)
                yCoordinate = ZeroCoordinateReplacement;

            return new ComplexNumber(xCoordinate, yCoordinate);
        }

        private static (ComplexNumber Root, float IterationCount) FindRoot(
            Polynomial polynomial,
            Polynomial derivative,
            ComplexNumber currentPoint)
        {
            float iterationCount = 0;

            for (int iteration = 0; iteration < 30; iteration++)
            {
                ComplexNumber difference =
                    polynomial.Eval(currentPoint)
                        .Divide(derivative.Eval(currentPoint));

                currentPoint = currentPoint.Subtract(difference);

                if (
                    Math.Pow(difference.RealPart, 2) +
                    Math.Pow(difference.ImaginaryPart, 2) >= 0.5)
                {
                    iteration--;
                }

                iterationCount++;
            }

            return (currentPoint, iterationCount);
        }

        private static int GetOrAddRootIndex(
            ComplexNumber currentPoint,
            List<ComplexNumber> roots)
        {
            bool rootFound = false;
            int rootIndex = 0;

            for (int candidateIndex = 0;
                 candidateIndex < roots.Count;
                 candidateIndex++)
            {
                if (
                    Math.Pow(
                        currentPoint.RealPart - roots[candidateIndex].RealPart,
                        2
                    ) +
                    Math.Pow(
                        currentPoint.ImaginaryPart - roots[candidateIndex].ImaginaryPart,
                        2
                    ) <= 0.01)
                {
                    rootFound = true;
                    rootIndex = candidateIndex;
                }
            }

            if (!rootFound)
            {
                roots.Add(currentPoint);
                rootIndex = roots.Count;
            }

            return rootIndex;
        }

        private static Color GetPixelColor(
            int rootIndex,
            float iterationCount,
            Color[] colors)
        {
            Color pixelColor = colors[rootIndex % colors.Length];

            pixelColor = Color.FromArgb(
                pixelColor.R,
                pixelColor.G,
                pixelColor.B
            );

            // darken the color based on the number of iterations
            pixelColor = Color.FromArgb(
                Math.Min(
                    Math.Max(0, pixelColor.R - (int)iterationCount * 2),
                    255
                ),
                Math.Min(
                    Math.Max(0, pixelColor.G - (int)iterationCount * 2),
                    255
                ),
                Math.Min(
                    Math.Max(0, pixelColor.B - (int)iterationCount * 2),
                    255
                )
            );

            return pixelColor;
        }
    }
}
