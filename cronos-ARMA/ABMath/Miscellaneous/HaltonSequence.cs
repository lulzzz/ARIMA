#region License Info
//Component of Cronos Package, http://www.codeplex.com/cronos
//Copyright (C) 2009 Anthony Brockwell

//This program is free software; you can redistribute it and/or
//modify it under the terms of the GNU General Public License
//as published by the Free Software Foundation; either version 2
//of the License, or (at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program; if not, write to the Free Software
//Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
#endregion

using System;
using MathNet.Numerics.LinearAlgebra;

namespace ABMath.Miscellaneous
{
    /// <summary>
    /// This class generates a multi-dimensional low-discrepancy sequence.
    /// See discussion at http://en.wikipedia.org/wiki/Constructions_of_low-discrepancy_sequences
    /// </summary>
    public class HaltonSequence
    {
        private readonly VanderCorputSequence[] vdcGenerators;
        private readonly int dimension;
        private readonly int[] firstPrimes =
            {
                2,
                3,
                5,
                7,
                11,
                13,
                17,
                19,
                23,
                29,
                31,
                37,
                41,
                43,
                47,
                53,
                59,
                61,
                67,
                71,
                73,
                79,
                83,
                89
            };

        /// <summary>
        /// Constructor just takes as argument the dimension of the elements of the sequence.
        /// </summary>
        /// <param name="dimension"></param>
        public HaltonSequence(int dimension)
        {
            if (dimension > firstPrimes.Length)
                throw new 
                    ArgumentOutOfRangeException("dimension");

            this.dimension = dimension;
            vdcGenerators = new VanderCorputSequence[this.dimension];
            for (int i=0 ; i<this.dimension ; ++i)
                vdcGenerators[i] = new VanderCorputSequence(firstPrimes[i]);
        }

        /// <summary>
        /// iterates through the Halton low-discrepancy sequence
        /// </summary>
        /// <returns>a new dimension x 1 matrix containing the next element in the sequence</returns>
        public Vector GetNext()
        {
            var retval = new Vector(dimension);
            for (int i = 0; i < dimension; ++i)
                retval[i] = vdcGenerators[i].GetNext();
            return retval;
        }
    }
}