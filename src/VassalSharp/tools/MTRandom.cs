/*
* MTRandom : A Java implementation of the MT19937 (Mersenne Twister)
*            pseudo random number generator algorithm based upon the
*            original C code by Makoto Matsumoto and Takuji Nishimura.
* Author   : David Beaumont
* Email    : mersenne-at-www.goui.net
*
* For the original C code, see:
*     http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/emt.html
*
* This version, Copyright (C) 2005, David Beaumont.
*
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Lesser General Public
* License as published by the Free Software Foundation; either
* version 2.1 of the License, or (at your option) any later version.
*
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
* Lesser General Public License for more details.
*
* You should have received a copy of the GNU Lesser General Public
* License along with this library; if not, write to the Free Software
* Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/
using System;
using System.Runtime.InteropServices;
namespace VassalSharp.tools
{
	
	/// <version>  1.0
	/// </version>
	/// <author>  David Beaumont, Copyright 2005
	/// <p>
	/// A Java implementation of the MT19937 (Mersenne Twister) pseudo
	/// random number generator algorithm based upon the original C code
	/// by Makoto Matsumoto and Takuji Nishimura (see
	/// <a href="http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/emt.html">
	/// http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/emt.html</a> for
	/// more information.
	/// <p>
	/// As a subclass of java.util.Random this class provides a single
	/// canonical method next() for generating bits in the pseudo random
	/// number sequence.  Anyone using this class should invoke the public
	/// inherited methods (nextInt(), nextFloat etc.) to obtain values as
	/// normal.  This class should provide a drop-in replacement for the
	/// standard implementation of java.util.Random with the additional
	/// advantage of having a far longer period and the ability to use a
	/// far larger seed value.
	/// <p>
	/// This is <b>not</b> a cryptographically strong source of randomness
	/// and should <b>not</b> be used for cryptographic systems or in any
	/// other situation where true random numbers are required.
	/// <p>
	/// <!-- Creative Commons License -->
	/// <a href="http://creativecommons.org/licenses/LGPL/2.1/"><img alt="CC-GNU LGPL" border="0" src="http://creativecommons.org/images/public/cc-LGPL-a.png" /></a><br />
	/// This software is licensed under the <a href="http://creativecommons.org/licenses/LGPL/2.1/">CC-GNU LGPL</a>.
	/// <!-- /Creative Commons License -->
	/// 
	/// <!--
	/// <rdf:RDF xmlns="http://web.resource.org/cc/"
	/// xmlns:dc="http://purl.org/dc/elements/1.1/"
	/// xmlns:rdf="http://www.w3.org/1999/02/22-rdf-syntax-ns#">
	/// 
	/// <Work rdf:about="">
	/// <license rdf:resource="http://creativecommons.org/licenses/LGPL/2.1/" />
	/// <dc:type rdf:resource="http://purl.org/dc/dcmitype/Software" />
	/// </Work>
	/// 
	/// <License rdf:about="http://creativecommons.org/licenses/LGPL/2.1/">
	/// <permits rdf:resource="http://web.resource.org/cc/Reproduction" />
	/// <permits rdf:resource="http://web.resource.org/cc/Distribution" />
	/// <requires rdf:resource="http://web.resource.org/cc/Notice" />
	/// <permits rdf:resource="http://web.resource.org/cc/DerivativeWorks" />
	/// <requires rdf:resource="http://web.resource.org/cc/ShareAlike" />
	/// <requires rdf:resource="http://web.resource.org/cc/SourceCode" />
	/// </License>
	/// 
	/// </rdf:RDF>
	/// -->
	/// </author>
	/// <deprecated> Use {@link java.secure.SecureRandom} instead.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	[Serializable]
	public class MTRandom:System.Random
	{
		
		/// <summary> Auto-generated serial version UID.  Note that MTRandom does NOT
		/// support serialisation of its internal state and it may even be
		/// necessary to implement read/write methods to re-seed it properly.
		/// This is only here to make Eclipse shut up about it being missing.
		/// </summary>
		private const long serialVersionUID = - 515082678588212038L;
		
		// Constants used in the original C implementation
		private const int UPPER_MASK = unchecked((int) 0x80000000);
		private const int LOWER_MASK = 0x7fffffff;
		
		private const int N = 624;
		private const int M = 397;
		//UPGRADE_NOTE: Final was removed from the declaration of 'MAGIC'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly int[] MAGIC = new int[]{0x0, unchecked((int) 0x9908b0df)};
		private const int MAGIC_FACTOR1 = 1812433253;
		private const int MAGIC_FACTOR2 = 1664525;
		private const int MAGIC_FACTOR3 = 1566083941;
		private const int MAGIC_MASK1 = unchecked((int) 0x9d2c5680);
		private const int MAGIC_MASK2 = unchecked((int) 0xefc60000);
		private const int MAGIC_SEED = 19650218;
		private const long DEFAULT_SEED = 5489L;
		
		// Internal state
		[NonSerialized]
		private int[] mt;
		[NonSerialized]
		private int mti;
		[NonSerialized]
		private bool compat = false;
		
		// Temporary buffer used during setSeed(long)
		[NonSerialized]
		private int[] ibuf;
		
		/// <summary> The default constructor for an instance of MTRandom.  This invokes
		/// the no-argument constructor for java.util.Random which will result
		/// in the class being initialised with a seed value obtained by calling
		/// System.currentTimeMillis().
		/// </summary>
		public MTRandom()
		{
		}
		
		/// <summary> This version of the constructor can be used to implement identical
		/// behaviour to the original C code version of this algorithm including
		/// exactly replicating the case where the seed value had not been set
		/// prior to calling genrand_int32.
		/// <p>
		/// If the compatibility flag is set to true, then the algorithm will be
		/// seeded with the same default value as was used in the original C
		/// code.  Furthermore the setSeed() method, which must take a 64 bit
		/// long value, will be limited to using only the lower 32 bits of the
		/// seed to facilitate seamless migration of existing C code into Java
		/// where identical behaviour is required.
		/// <p>
		/// Whilst useful for ensuring backwards compatibility, it is advised
		/// that this feature not be used unless specifically required, due to
		/// the reduction in strength of the seed value.
		/// 
		/// </summary>
		/// <param name="compatible">Compatibility flag for replicating original
		/// behaviour.
		/// </param>
		public MTRandom(bool compatible):base((System.Int32) 0L)
		{
			compat = compatible;
			setSeed(compat?DEFAULT_SEED:(System.DateTime.Now.Ticks - 621355968000000000) / 10000);
		}
		
		/// <summary> This version of the constructor simply initialises the class with
		/// the given 64 bit seed value.  For a better random number sequence
		/// this seed value should contain as much entropy as possible.
		/// 
		/// </summary>
		/// <param name="seed">The seed value with which to initialise this class.
		/// </param>
		public MTRandom(long seed):base((System.Int32) seed)
		{
		}
		
		/// <summary> This version of the constructor initialises the class with the
		/// given byte array.  All the data will be used to initialise this
		/// instance.
		/// 
		/// </summary>
		/// <param name="buf">The non-empty byte array of seed information.
		/// </param>
		/// <throws>  NullPointerException if the buffer is null. </throws>
		/// <throws>  IllegalArgumentException if the buffer has zero length. </throws>
		public MTRandom(sbyte[] buf):base((System.Int32) 0L)
		{
			setSeed(buf);
		}
		
		/// <summary> This version of the constructor initialises the class with the
		/// given integer array.  All the data will be used to initialise
		/// this instance.
		/// 
		/// </summary>
		/// <param name="buf">The non-empty integer array of seed information.
		/// </param>
		/// <throws>  NullPointerException if the buffer is null. </throws>
		/// <throws>  IllegalArgumentException if the buffer has zero length. </throws>
		public MTRandom(int[] buf):base((System.Int32) 0L)
		{
			setSeed(buf);
		}
		
		// Initializes mt[N] with a simple integer seed. This method is
		// required as part of the Mersenne Twister algorithm but need
		// not be made public.
		private void  setSeed(int seed)
		{
			
			// Annoying runtime check for initialisation of internal data
			// caused by java.util.Random invoking setSeed() during init.
			// This is unavoidable because no fields in our instance will
			// have been initialised at this point, not even if the code
			// were placed at the declaration of the member variable.
			if (mt == null)
				mt = new int[N];
			
			// ---- Begin Mersenne Twister Algorithm ----
			mt[0] = seed;
			for (mti = 1; mti < N; mti++)
			{
				mt[mti] = (MAGIC_FACTOR1 * (mt[mti - 1] ^ (SupportClass.URShift(mt[mti - 1], 30))) + mti);
			}
			// ---- End Mersenne Twister Algorithm ----
		}
		
		/// <summary> This method resets the state of this instance using the 64
		/// bits of seed data provided.  Note that if the same seed data
		/// is passed to two different instances of MTRandom (both of
		/// which share the same compatibility state) then the sequence
		/// of numbers generated by both instances will be identical.
		/// <p>
		/// If this instance was initialised in 'compatibility' mode then
		/// this method will only use the lower 32 bits of any seed value
		/// passed in and will match the behaviour of the original C code
		/// exactly with respect to state initialisation.
		/// 
		/// </summary>
		/// <param name="seed">The 64 bit value used to initialise the random
		/// number generator state.
		/// </param>
		//UPGRADE_NOTE: The equivalent of method 'java.util.Random.setSeed' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'setSeed'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public void  setSeed(long seed)
		{
			lock (this)
			{
				if (compat)
				{
					setSeed((int) seed);
				}
				else
				{
					
					// Annoying runtime check for initialisation of internal data
					// caused by java.util.Random invoking setSeed() during init.
					// This is unavoidable because no fields in our instance will
					// have been initialised at this point, not even if the code
					// were placed at the declaration of the member variable.
					if (ibuf == null)
						ibuf = new int[2];
					
					ibuf[0] = (int) seed;
					ibuf[1] = (int) (SupportClass.URShift(seed, 32));
					setSeed(ibuf);
				}
			}
		}
		
		/// <summary> This method resets the state of this instance using the byte
		/// array of seed data provided.  Note that calling this method
		/// is equivalent to calling "setSeed(pack(buf))" and in particular
		/// will result in a new integer array being generated during the
		/// call.  If you wish to retain this seed data to allow the pseudo
		/// random sequence to be restarted then it would be more efficient
		/// to use the "pack()" method to convert it into an integer array
		/// first and then use that to re-seed the instance.  The behaviour
		/// of the class will be the same in both cases but it will be more
		/// efficient.
		/// 
		/// </summary>
		/// <param name="buf">The non-empty byte array of seed information.
		/// </param>
		/// <throws>  NullPointerException if the buffer is null. </throws>
		/// <throws>  IllegalArgumentException if the buffer has zero length. </throws>
		public void  setSeed(sbyte[] buf)
		{
			setSeed(pack(buf));
		}
		
		/// <summary> This method resets the state of this instance using the integer
		/// array of seed data provided.  This is the canonical way of
		/// resetting the pseudo random number sequence.
		/// 
		/// </summary>
		/// <param name="buf">The non-empty integer array of seed information.
		/// </param>
		/// <throws>  NullPointerException if the buffer is null. </throws>
		/// <throws>  IllegalArgumentException if the buffer has zero length. </throws>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'setSeed'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public void  setSeed(int[] buf)
		{
			lock (this)
			{
				int length = buf.Length;
				if (length == 0)
					throw new System.ArgumentException("Seed buffer may not be empty");
				// ---- Begin Mersenne Twister Algorithm ----
				int i = 1, j = 0, k = (N > length?N:length);
				setSeed(MAGIC_SEED);
				for (; k > 0; k--)
				{
					mt[i] = (mt[i] ^ ((mt[i - 1] ^ (SupportClass.URShift(mt[i - 1], 30))) * MAGIC_FACTOR2)) + buf[j] + j;
					i++; j++;
					if (i >= N)
					{
						mt[0] = mt[N - 1]; i = 1;
					}
					if (j >= length)
						j = 0;
				}
				for (k = N - 1; k > 0; k--)
				{
					mt[i] = (mt[i] ^ ((mt[i - 1] ^ (SupportClass.URShift(mt[i - 1], 30))) * MAGIC_FACTOR3)) - i;
					i++;
					if (i >= N)
					{
						mt[0] = mt[N - 1]; i = 1;
					}
				}
				mt[0] = UPPER_MASK; // MSB is 1; assuring non-zero initial array
				// ---- End Mersenne Twister Algorithm ----
			}
		}
		
		/// <summary> This method forms the basis for generating a pseudo random number
		/// sequence from this class.  If given a value of 32, this method
		/// behaves identically to the genrand_int32 function in the original
		/// C code and ensures that using the standard nextInt() function
		/// (inherited from Random) we are able to replicate behaviour exactly.
		/// <p>
		/// Note that where the number of bits requested is not equal to 32
		/// then bits will simply be masked out from the top of the returned
		/// integer value.  That is to say that:
		/// <pre>
		/// mt.setSeed(12345);
		/// int foo = mt.nextInt(16) + (mt.nextInt(16) << 16);</pre>
		/// will not give the same result as
		/// <pre>
		/// mt.setSeed(12345);
		/// int foo = mt.nextInt(32);</pre>
		/// 
		/// </summary>
		/// <param name="bits">The number of significant bits desired in the output.
		/// </param>
		/// <returns> The next value in the pseudo random sequence with the
		/// specified number of bits in the lower part of the integer.
		/// </returns>
		//UPGRADE_NOTE: The equivalent of method 'java.util.Random.next' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'next'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		protected internal int next(int bits)
		{
			lock (this)
			{
				// ---- Begin Mersenne Twister Algorithm ----
				int y, kk;
				if (mti >= N)
				{
					// generate N words at one time
					
					// In the original C implementation, mti is checked here
					// to determine if initialisation has occurred; if not
					// it initialises this instance with DEFAULT_SEED (5489).
					// This is no longer necessary as initialisation of the
					// Java instance must result in initialisation occurring
					// Use the constructor MTRandom(true) to enable backwards
					// compatible behaviour.
					
					for (kk = 0; kk < N - M; kk++)
					{
						y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
						mt[kk] = mt[kk + M] ^ (SupportClass.URShift(y, 1)) ^ MAGIC[y & 0x1];
					}
					for (; kk < N - 1; kk++)
					{
						y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
						mt[kk] = mt[kk + (M - N)] ^ (SupportClass.URShift(y, 1)) ^ MAGIC[y & 0x1];
					}
					y = (mt[N - 1] & UPPER_MASK) | (mt[0] & LOWER_MASK);
					mt[N - 1] = mt[M - 1] ^ (SupportClass.URShift(y, 1)) ^ MAGIC[y & 0x1];
					
					mti = 0;
				}
				
				y = mt[mti++];
				
				// Tempering
				y ^= (SupportClass.URShift(y, 11));
				y ^= (y << 7) & MAGIC_MASK1;
				y ^= (y << 15) & MAGIC_MASK2;
				y ^= (SupportClass.URShift(y, 18));
				// ---- End Mersenne Twister Algorithm ----
				return (SupportClass.URShift(y, (32 - bits)));
			}
		}
		
		// This is a fairly obscure little code section to pack a
		// byte[] into an int[] in little endian ordering.
		
		/// <summary> This simply utility method can be used in cases where a byte
		/// array of seed data is to be used to repeatedly re-seed the
		/// random number sequence.  By packing the byte array into an
		/// integer array first, using this method, and then invoking
		/// setSeed() with that; it removes the need to re-pack the byte
		/// array each time setSeed() is called.
		/// <p>
		/// If the length of the byte array is not a multiple of 4 then
		/// it is implicitly padded with zeros as necessary.  For example:
		/// <pre>    byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06 }</pre>
		/// becomes
		/// <pre>    int[]  { 0x04030201, 0x00000605 }</pre>
		/// <p>
		/// Note that this method will not complain if the given byte array
		/// is empty and will produce an empty integer array, but the
		/// setSeed() method will throw an exception if the empty integer
		/// array is passed to it.
		/// 
		/// </summary>
		/// <param name="buf">The non-null byte array to be packed.
		/// </param>
		/// <returns> A non-null integer array of the packed bytes.
		/// </returns>
		/// <throws>  NullPointerException if the given byte array is null. </throws>
		public static int[] pack(sbyte[] buf)
		{
			int k, blen = buf.Length, ilen = (SupportClass.URShift((buf.Length + 3), 2));
			int[] ibuf = new int[ilen];
			for (int n = 0; n < ilen; n++)
			{
				int m = (n + 1) << 2;
				if (m > blen)
					m = blen;
				for (k = buf[--m] & 0xff; (m & 0x3) != 0; k = (k << 8) | buf[--m] & 0xff)
					;
				ibuf[n] = k;
			}
			return ibuf;
		}
	}
}