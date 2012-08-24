using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Numerics_Generator
{
    class Grade
    {
        /// <summary>
        /// Calculates the binomial coefficient (n k).
        /// </summary>
        /// <remarks>Will not overflow internally unless final result would overflow.</remarks>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        [Pure]
        public static long BinomialCoefficient(long n, long k)
        {
            Contract.Ensures(0 <= Contract.Result<long>());

            if (k > n)
                return 0;

            long r = 1;
            for (long d = 1; d <= k; d++)
            {
                r *= n--;
                r /= d;
            }
            return r;
        }

        static string[] Names = new string[] {
            "Scalar", "Vector", "Bivector", "Trivector", "Quadvector", 
        };

        public static int Grades(int dimension)
        {
            if (dimension < 0 || dimension > 4)
                throw new ArgumentOutOfRangeException("dimension", "dimension is out of range");
            return dimension + 1;
        }

        public Grade(int grade, int dimensions)
        {
            int grades = Grades(dimensions);
            if (grade < 0 || grade > grades)
                throw new ArgumentOutOfRangeException("grade", "grade is out of range");

            int mid = grades / 2;
            mid = grades % 2 == 0 ? mid : mid + 1;

            if (grade < mid)
                Name = Names[grade];
            else
                Name = "Anti" + Names[grades - grade - 1].ToLower();

            Elements = (int)BinomialCoefficient(dimensions, grade);
        }

        public string Name { get; private set; }
        public int Elements { get; private set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
