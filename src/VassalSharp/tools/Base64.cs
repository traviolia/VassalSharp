using System;
namespace VassalSharp.tools
{
	
	/// <summary> <p>Encodes and decodes to and from Base64 notation.</p>
	/// <p>Homepage: <a href="http://iharder.net/base64">http://iharder.net/base64</a>.</p>
	/// 
	/// <p>The <tt>options</tt> parameter, which appears in a few places, is used to pass
	/// several pieces of information to the encoder. In the "higher level" methods such as
	/// encodeBytes( bytes, options ) the options parameter can be used to indicate such
	/// things as first gzipping the bytes before encoding them, not inserting linefeeds
	/// (though that breaks strict Base64 compatibility), and encoding using the URL-safe
	/// and Ordered dialects.</p>
	/// 
	/// <p>The constants defined in Base64 can be OR-ed together to combine options, so you
	/// might make a call like this:</p>
	/// 
	/// <code>String encoded = Base64.encodeBytes( mybytes, Base64.GZIP | Base64.DONT_BREAK_LINES );</code>
	/// 
	/// <p>to compress the data before encoding it and then making the output have no newline characters.</p>
	/// 
	/// 
	/// <p>
	/// Change Log:
	/// </p>
	/// <ul>
	/// <li>v2.2.2 - Fixed encodeFileToFile and decodeFileToFile to use the
	/// Base64.InputStream class to encode and decode on the fly which uses
	/// less memory than encoding/decoding an entire file into memory before writing.</li>
	/// <li>v2.2.1 - Fixed bug using URL_SAFE and ORDERED encodings. Fixed bug
	/// when using very small files (~< 40 bytes).</li>
	/// <li>v2.2 - Added some helper methods for encoding/decoding directly from
	/// one file to the next. Also added a main() method to support command line
	/// encoding/decoding from one file to the next. Also added these Base64 dialects:
	/// <ol>
	/// <li>The default is RFC3548 format.</li>
	/// <li>Calling Base64.setFormat(Base64.BASE64_FORMAT.URLSAFE_FORMAT) generates
	/// URL and file name friendly format as described in Section 4 of RFC3548.
	/// http://www.faqs.org/rfcs/rfc3548.html</li>
	/// <li>Calling Base64.setFormat(Base64.BASE64_FORMAT.ORDERED_FORMAT) generates
	/// URL and file name friendly format that preserves lexical ordering as described
	/// in http://www.faqs.org/qa/rfcc-1940.html</li>
	/// </ol>
	/// Special thanks to Jim Kellerman at <a href="http://www.powerset.com/">http://www.powerset.com/</a>
	/// for contributing the new Base64 dialects.
	/// </li>
	/// 
	/// <li>v2.1 - Cleaned up javadoc comments and unused variables and methods. Added
	/// some convenience methods for reading and writing to and from files.</li>
	/// <li>v2.0.2 - Now specifies UTF-8 encoding in places where the code fails on systems
	/// with other encodings (like EBCDIC).</li>
	/// <li>v2.0.1 - Fixed an error when decoding a single byte, that is, when the
	/// encoded data was a single byte.</li>
	/// <li>v2.0 - I got rid of methods that used booleans to set options.
	/// Now everything is more consolidated and cleaner. The code now detects
	/// when data that's being decoded is gzip-compressed and will decompress it
	/// automatically. Generally things are cleaner. You'll probably have to
	/// change some method calls that you were making to support the new
	/// options format (<tt>int</tt>s that you "OR" together).</li>
	/// <li>v1.5.1 - Fixed bug when decompressing and decoding to a
	/// byte[] using <tt>decode( String s, boolean gzipCompressed )</tt>.
	/// Added the ability to "suspend" encoding in the Output Stream so
	/// you can turn on and off the encoding if you need to embed base64
	/// data in an otherwise "normal" stream (like an XML file).</li>
	/// <li>v1.5 - Output stream pases on flush() command but doesn't do anything itself.
	/// This helps when using GZIP streams.
	/// Added the ability to GZip-compress objects before encoding them.</li>
	/// <li>v1.4 - Added helper methods to read/write files.</li>
	/// <li>v1.3.6 - Fixed OutputStream.flush() so that 'position' is reset.</li>
	/// <li>v1.3.5 - Added flag to turn on and off line breaks. Fixed bug in input stream
	/// where last buffer being read, if not completely full, was not returned.</li>
	/// <li>v1.3.4 - Fixed when "improperly padded stream" error was thrown at the wrong time.</li>
	/// <li>v1.3.3 - Fixed I/O streams which were totally messed up.</li>
	/// </ul>
	/// 
	/// <p>
	/// I am placing this code in the Public Domain. Do with it as you will.
	/// This software comes with no guarantees or warranties but with
	/// plenty of well-wishing instead!
	/// Please visit <a href="http://iharder.net/base64">http://iharder.net/base64</a>
	/// periodically to check for updates or to contribute improvements.
	/// </p>
	/// 
	/// </summary>
	/// <author>  Robert Harder
	/// </author>
	/// <author>  rob@iharder.net
	/// </author>
	/// <version>  2.2.2
	/// 
	/// </version>
	/// <deprecated>} Use {@link net.iharder.Base64} instead.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class Base64
	{
		
		/* ********  P U B L I C   F I E L D S  ******** */
		
		
		/// <summary>No options specified. Value is zero. </summary>
		public const int NO_OPTIONS = 0;
		
		/// <summary>Specify encoding. </summary>
		public const int ENCODE = 1;
		
		
		/// <summary>Specify decoding. </summary>
		public const int DECODE = 0;
		
		
		/// <summary>Specify that data should be gzip-compressed. </summary>
		public const int GZIP = 2;
		
		
		/// <summary>Don't break lines when encoding (violates strict Base64 specification) </summary>
		public const int DONT_BREAK_LINES = 8;
		
		/// <summary> Encode using Base64-like encoding that is URL- and Filename-safe as described
		/// in Section 4 of RFC3548:
		/// <a href="http://www.faqs.org/rfcs/rfc3548.html">http://www.faqs.org/rfcs/rfc3548.html</a>.
		/// It is important to note that data encoded this way is <em>not</em> officially valid Base64,
		/// or at the very least should not be called Base64 without also specifying that is
		/// was encoded using the URL- and Filename-safe dialect.
		/// </summary>
		public const int URL_SAFE = 16;
		
		
		/// <summary> Encode using the special "ordered" dialect of Base64 described here:
		/// <a href="http://www.faqs.org/qa/rfcc-1940.html">http://www.faqs.org/qa/rfcc-1940.html</a>.
		/// </summary>
		public const int ORDERED = 32;
		
		
		/* ********  P R I V A T E   F I E L D S  ******** */
		
		
		/// <summary>Maximum line length (76) of Base64 output. </summary>
		private const int MAX_LINE_LENGTH = 76;
		
		
		/// <summary>The equals sign (=) as a byte. </summary>
		private static sbyte EQUALS_SIGN = (sbyte) '=';
		
		
		/// <summary>The new line character (\n) as a byte. </summary>
		private static sbyte NEW_LINE = (sbyte) '\n';
		
		
		/// <summary>Preferred encoding. </summary>
		private const System.String PREFERRED_ENCODING = "UTF-8";
		
		
		// I think I end up not using the BAD_ENCODING indicator.
		//private final static byte BAD_ENCODING    = -9; // Indicates error in encoding
		private const sbyte WHITE_SPACE_ENC = - 5; // Indicates white space in encoding
		private const sbyte EQUALS_SIGN_ENC = - 1; // Indicates equals sign in encoding
		
		
		/* ********  S T A N D A R D   B A S E 6 4   A L P H A B E T  ******** */
		
		/// <summary>The 64 valid Base64 values. </summary>
		//private final static byte[] ALPHABET;
		/* Host platform me be something funny like EBCDIC, so we hardcode these values. */
		private static sbyte[] _STANDARD_ALPHABET = new sbyte[]{(sbyte) 'A', (sbyte) 'B', (sbyte) 'C', (sbyte) 'D', (sbyte) 'E', (sbyte) 'F', (sbyte) 'G', (sbyte) 'H', (sbyte) 'I', (sbyte) 'J', (sbyte) 'K', (sbyte) 'L', (sbyte) 'M', (sbyte) 'N', (sbyte) 'O', (sbyte) 'P', (sbyte) 'Q', (sbyte) 'R', (sbyte) 'S', (sbyte) 'T', (sbyte) 'U', (sbyte) 'V', (sbyte) 'W', (sbyte) 'X', (sbyte) 'Y', (sbyte) 'Z', (sbyte) 'a', (sbyte) 'b', (sbyte) 'c', (sbyte) 'd', (sbyte) 'e', (sbyte) 'f', (sbyte) 'g', (sbyte) 'h', (sbyte) 'i', (sbyte) 'j', (sbyte) 'k', (sbyte) 'l', (sbyte) 'm', (sbyte) 'n', (sbyte) 'o', (sbyte) 'p', (sbyte) 'q', (sbyte) 'r', (sbyte) 's', (sbyte) 't', (sbyte) 'u', (sbyte) 'v', (sbyte) 'w', (sbyte) 'x', (sbyte) 'y', (sbyte) 'z', (sbyte) '0', (sbyte) '1', (sbyte) '2', (sbyte) '3', (sbyte) '4', (sbyte) '5', (sbyte) '6', (sbyte) '7', (sbyte) '8', (sbyte) '9', (sbyte) '+', (sbyte) '/'};
		
		
		/// <summary> Translates a Base64 value to either its 6-bit reconstruction value
		/// or a negative number indicating some other meaning.
		/// 
		/// </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of '_STANDARD_DECODABET'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly sbyte[] _STANDARD_DECODABET = new sbyte[]{- 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 5, - 5, - 9, - 9, - 5, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 5, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, 62, - 9, - 9, - 9, 63, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, - 9, - 9, - 9, - 1, - 9, - 9, - 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, - 9, - 9, - 9, - 9, - 9, - 9, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, - 9, - 9, - 9, - 9};
		
		
		/* ********  U R L   S A F E   B A S E 6 4   A L P H A B E T  ******** */
		
		/// <summary> Used in the URL- and Filename-safe dialect described in Section 4 of RFC3548:
		/// <a href="http://www.faqs.org/rfcs/rfc3548.html">http://www.faqs.org/rfcs/rfc3548.html</a>.
		/// Notice that the last two bytes become "hyphen" and "underscore" instead of "plus" and "slash."
		/// </summary>
		private static sbyte[] _URL_SAFE_ALPHABET = new sbyte[]{(sbyte) 'A', (sbyte) 'B', (sbyte) 'C', (sbyte) 'D', (sbyte) 'E', (sbyte) 'F', (sbyte) 'G', (sbyte) 'H', (sbyte) 'I', (sbyte) 'J', (sbyte) 'K', (sbyte) 'L', (sbyte) 'M', (sbyte) 'N', (sbyte) 'O', (sbyte) 'P', (sbyte) 'Q', (sbyte) 'R', (sbyte) 'S', (sbyte) 'T', (sbyte) 'U', (sbyte) 'V', (sbyte) 'W', (sbyte) 'X', (sbyte) 'Y', (sbyte) 'Z', (sbyte) 'a', (sbyte) 'b', (sbyte) 'c', (sbyte) 'd', (sbyte) 'e', (sbyte) 'f', (sbyte) 'g', (sbyte) 'h', (sbyte) 'i', (sbyte) 'j', (sbyte) 'k', (sbyte) 'l', (sbyte) 'm', (sbyte) 'n', (sbyte) 'o', (sbyte) 'p', (sbyte) 'q', (sbyte) 'r', (sbyte) 's', (sbyte) 't', (sbyte) 'u', (sbyte) 'v', (sbyte) 'w', (sbyte) 'x', (sbyte) 'y', (sbyte) 'z', (sbyte) '0', (sbyte) '1', (sbyte) '2', (sbyte) '3', (sbyte) '4', (sbyte) '5', (sbyte) '6', (sbyte) '7', (sbyte) '8', (sbyte) '9', (sbyte) '-', (sbyte) '_'};
		
		/// <summary> Used in decoding URL- and Filename-safe dialects of Base64.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of '_URL_SAFE_DECODABET'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly sbyte[] _URL_SAFE_DECODABET = new sbyte[]{- 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 5, - 5, - 9, - 9, - 5, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 5, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, 62, - 9, - 9, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, - 9, - 9, - 9, - 1, - 9, - 9, - 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, - 9, - 9, - 9, - 9, 63, - 9, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, - 9, - 9, - 9, - 9};
		
		
		
		/* ********  O R D E R E D   B A S E 6 4   A L P H A B E T  ******** */
		
		/// <summary> I don't get the point of this technique, but it is described here:
		/// <a href="http://www.faqs.org/qa/rfcc-1940.html">http://www.faqs.org/qa/rfcc-1940.html</a>.
		/// </summary>
		private static sbyte[] _ORDERED_ALPHABET = new sbyte[]{(sbyte) '-', (sbyte) '0', (sbyte) '1', (sbyte) '2', (sbyte) '3', (sbyte) '4', (sbyte) '5', (sbyte) '6', (sbyte) '7', (sbyte) '8', (sbyte) '9', (sbyte) 'A', (sbyte) 'B', (sbyte) 'C', (sbyte) 'D', (sbyte) 'E', (sbyte) 'F', (sbyte) 'G', (sbyte) 'H', (sbyte) 'I', (sbyte) 'J', (sbyte) 'K', (sbyte) 'L', (sbyte) 'M', (sbyte) 'N', (sbyte) 'O', (sbyte) 'P', (sbyte) 'Q', (sbyte) 'R', (sbyte) 'S', (sbyte) 'T', (sbyte) 'U', (sbyte) 'V', (sbyte) 'W', (sbyte) 'X', (sbyte) 'Y', (sbyte) 'Z', (sbyte) '_', (sbyte) 'a', (sbyte) 'b', (sbyte) 'c', (sbyte) 'd', (sbyte) 'e', (sbyte) 'f', (sbyte) 'g', (sbyte) 'h', (sbyte) 'i', (sbyte) 'j', (sbyte) 'k', (sbyte) 'l', (sbyte) 'm', (sbyte) 'n', (sbyte) 'o', (sbyte) 'p', (sbyte) 'q', (sbyte) 'r', (sbyte) 's', (sbyte) 't', (sbyte) 'u', (sbyte) 'v', (sbyte) 'w', (sbyte) 'x', (sbyte) 'y', (sbyte) 'z'};
		
		/// <summary> Used in decoding the "ordered" dialect of Base64.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of '_ORDERED_DECODABET'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly sbyte[] _ORDERED_DECODABET = new sbyte[]{- 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 5, - 5, - 9, - 9, - 5, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 5, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, - 9, 0, - 9, - 9, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, - 9, - 9, - 9, - 1, - 9, - 9, - 9, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, - 9, - 9, - 9, - 9, 37, - 9, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, - 9, - 9, - 9, - 9};
		
		
		/* ********  D E T E R M I N E   W H I C H   A L H A B E T  ******** */
		
		
		/// <summary> Returns one of the _SOMETHING_ALPHABET byte arrays depending on
		/// the options specified.
		/// It's possible, though silly, to specify ORDERED and URLSAFE
		/// in which case one of them will be picked, though there is
		/// no guarantee as to which one will be picked.
		/// </summary>
		private static sbyte[] getAlphabet(int options)
		{
			if ((options & URL_SAFE) == URL_SAFE)
				return _URL_SAFE_ALPHABET;
			else if ((options & ORDERED) == ORDERED)
				return _ORDERED_ALPHABET;
			else
				return _STANDARD_ALPHABET;
		} // end getAlphabet
		
		
		/// <summary> Returns one of the _SOMETHING_DECODABET byte arrays depending on
		/// the options specified.
		/// It's possible, though silly, to specify ORDERED and URL_SAFE
		/// in which case one of them will be picked, though there is
		/// no guarantee as to which one will be picked.
		/// </summary>
		private static sbyte[] getDecodabet(int options)
		{
			if ((options & URL_SAFE) == URL_SAFE)
				return _URL_SAFE_DECODABET;
			else if ((options & ORDERED) == ORDERED)
				return _ORDERED_DECODABET;
			else
				return _STANDARD_DECODABET;
		} // end getAlphabet
		
		
		
		/// <summary>Defeats instantiation. </summary>
		private Base64()
		{
		}
		
		
		/// <summary> Encodes or decodes two files from the command line;
		/// <strong>feel free to delete this method (in fact you probably should)
		/// if you're embedding this code into a larger program.</strong>
		/// </summary>
		[STAThread]
		public static void  Main(System.String[] args)
		{
			if (args.Length < 3)
			{
				usage("Not enough arguments.");
			}
			// end if: args.length < 3
			else
			{
				System.String flag = args[0];
				System.String infile = args[1];
				System.String outfile = args[2];
				if (flag.Equals("-e"))
				{
					Base64.encodeFileToFile(infile, outfile);
				}
				// end if: encode
				else if (flag.Equals("-d"))
				{
					Base64.decodeFileToFile(infile, outfile);
				}
				// end else if: decode
				else
				{
					usage("Unknown flag: " + flag);
				} // end else
			} // end else
		} // end main
		
		/// <summary> Prints command line usage.
		/// 
		/// </summary>
		/// <param name="msg">A message to include with usage info.
		/// </param>
		private static void  usage(System.String msg)
		{
			System.Console.Error.WriteLine(msg);
			System.Console.Error.WriteLine("Usage: java Base64 -e|-d inputfile outputfile");
		} // end usage
		
		
		/* ********  E N C O D I N G   M E T H O D S  ******** */
		
		
		/// <summary> Encodes up to the first three bytes of array <var>threeBytes</var>
		/// and returns a four-byte array in Base64 notation.
		/// The actual number of significant bytes in your array is
		/// given by <var>numSigBytes</var>.
		/// The array <var>threeBytes</var> needs only be as big as
		/// <var>numSigBytes</var>.
		/// Code can reuse a byte array by passing a four-byte array as <var>b4</var>.
		/// 
		/// </summary>
		/// <param name="b4">A reusable byte array to reduce array instantiation
		/// </param>
		/// <param name="threeBytes">the array to convert
		/// </param>
		/// <param name="numSigBytes">the number of significant bytes in your array
		/// </param>
		/// <returns> four byte array in Base64 notation.
		/// </returns>
		/// <since> 1.5.1
		/// </since>
		private static sbyte[] encode3to4(sbyte[] b4, sbyte[] threeBytes, int numSigBytes, int options)
		{
			encode3to4(threeBytes, 0, numSigBytes, b4, 0, options);
			return b4;
		} // end encode3to4
		
		
		/// <summary> <p>Encodes up to three bytes of the array <var>source</var>
		/// and writes the resulting four Base64 bytes to <var>destination</var>.
		/// The source and destination arrays can be manipulated
		/// anywhere along their length by specifying
		/// <var>srcOffset</var> and <var>destOffset</var>.
		/// This method does not check to make sure your arrays
		/// are large enough to accomodate <var>srcOffset</var> + 3 for
		/// the <var>source</var> array or <var>destOffset</var> + 4 for
		/// the <var>destination</var> array.
		/// The actual number of significant bytes in your array is
		/// given by <var>numSigBytes</var>.</p>
		/// <p>This is the lowest level of the encoding methods with
		/// all possible parameters.</p>
		/// 
		/// </summary>
		/// <param name="source">the array to convert
		/// </param>
		/// <param name="srcOffset">the index where conversion begins
		/// </param>
		/// <param name="numSigBytes">the number of significant bytes in your array
		/// </param>
		/// <param name="destination">the array to hold the conversion
		/// </param>
		/// <param name="destOffset">the index where output will be put
		/// </param>
		/// <returns> the <var>destination</var> array
		/// </returns>
		/// <since> 1.3
		/// </since>
		private static sbyte[] encode3to4(sbyte[] source, int srcOffset, int numSigBytes, sbyte[] destination, int destOffset, int options)
		{
			sbyte[] ALPHABET = getAlphabet(options);
			
			//           1         2         3
			// 01234567890123456789012345678901 Bit position
			// --------000000001111111122222222 Array position from threeBytes
			// --------|    ||    ||    ||    | Six bit groups to index ALPHABET
			//          >>18  >>12  >> 6  >> 0  Right shift necessary
			//                0x3f  0x3f  0x3f  Additional AND
			
			// Create buffer with zero-padding if there are only one or two
			// significant bytes passed in the array.
			// We have to shift left 24 in order to flush out the 1's that appear
			// when Java treats a value as negative that is cast from a byte to an int.
			int inBuff = (numSigBytes > 0?(SupportClass.URShift((source[srcOffset] << 24), 8)):0) | (numSigBytes > 1?(SupportClass.URShift((source[srcOffset + 1] << 24), 16)):0) | (numSigBytes > 2?(SupportClass.URShift((source[srcOffset + 2] << 24), 24)):0);
			
			switch (numSigBytes)
			{
				
				case 3: 
					destination[destOffset] = ALPHABET[(SupportClass.URShift(inBuff, 18))];
					destination[destOffset + 1] = ALPHABET[(SupportClass.URShift(inBuff, 12)) & 0x3f];
					destination[destOffset + 2] = ALPHABET[(SupportClass.URShift(inBuff, 6)) & 0x3f];
					destination[destOffset + 3] = ALPHABET[(inBuff) & 0x3f];
					return destination;
				
				
				case 2: 
					destination[destOffset] = ALPHABET[(SupportClass.URShift(inBuff, 18))];
					destination[destOffset + 1] = ALPHABET[(SupportClass.URShift(inBuff, 12)) & 0x3f];
					destination[destOffset + 2] = ALPHABET[(SupportClass.URShift(inBuff, 6)) & 0x3f];
					destination[destOffset + 3] = EQUALS_SIGN;
					return destination;
				
				
				case 1: 
					destination[destOffset] = ALPHABET[(SupportClass.URShift(inBuff, 18))];
					destination[destOffset + 1] = ALPHABET[(SupportClass.URShift(inBuff, 12)) & 0x3f];
					destination[destOffset + 2] = EQUALS_SIGN;
					destination[destOffset + 3] = EQUALS_SIGN;
					return destination;
				
				
				default: 
					return destination;
				
			} // end switch
		} // end encode3to4
		
		
		
		/// <summary> Serializes an object and returns the Base64-encoded
		/// version of that serialized object. If the object
		/// cannot be serialized or there is another error,
		/// the method will return <tt>null</tt>.
		/// The object is not GZip-compressed before being encoded.
		/// 
		/// </summary>
		/// <param name="serializableObject">The object to encode
		/// </param>
		/// <returns> The Base64-encoded object
		/// </returns>
		/// <since> 1.4
		/// </since>
		public static System.String encodeObject(System.Runtime.Serialization.ISerializable serializableObject)
		{
			return encodeObject(serializableObject, NO_OPTIONS);
		} // end encodeObject
		
		
		
		/// <summary> Serializes an object and returns the Base64-encoded
		/// version of that serialized object. If the object
		/// cannot be serialized or there is another error,
		/// the method will return <tt>null</tt>.
		/// <p>
		/// Valid options:<pre>
		/// GZIP: gzip-compresses object before encoding it.
		/// DONT_BREAK_LINES: don't break lines at 76 characters
		/// <i>Note: Technically, this makes your encoding non-compliant.</i>
		/// </pre>
		/// <p>
		/// Example: <code>encodeObject( myObj, Base64.GZIP )</code> or
		/// <p>
		/// Example: <code>encodeObject( myObj, Base64.GZIP | Base64.DONT_BREAK_LINES )</code>
		/// 
		/// </summary>
		/// <param name="serializableObject">The object to encode
		/// </param>
		/// <param name="options">Specified options
		/// </param>
		/// <returns> The Base64-encoded object
		/// </returns>
		/// <seealso cref="Base64.GZIP">
		/// </seealso>
		/// <seealso cref="Base64.DONT_BREAK_LINES">
		/// </seealso>
		/// <since> 2.0
		/// </since>
		public static System.String encodeObject(System.Runtime.Serialization.ISerializable serializableObject, int options)
		{
			// Streams
			System.IO.MemoryStream baos = null;
			System.IO.Stream b64os = null;
			//UPGRADE_TODO: Class 'java.io.ObjectOutputStream' was converted to 'System.IO.BinaryWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectOutputStream'"
			System.IO.BinaryWriter oos = null;
			//UPGRADE_ISSUE: Class 'java.util.zip.GZIPOutputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipGZIPOutputStream'"
			java.util.zip.GZIPOutputStream gzos = null;
			
			// Isolate options
			int gzip = (options & GZIP);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			SuppressWarnings(unused) 
			int dontBreakLines =(options & DONT_BREAK_LINES);
			
			try
			{
				// ObjectOutputStream -> (GZIP) -> Base64 -> ByteArrayOutputStream
				baos = new System.IO.MemoryStream();
				b64os = new Base64.OutputStream(baos, ENCODE | options);
				
				// GZip?
				if (gzip == GZIP)
				{
					//UPGRADE_ISSUE: Constructor 'java.util.zip.GZIPOutputStream.GZIPOutputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipGZIPOutputStream'"
					gzos = new java.util.zip.GZIPOutputStream(b64os);
					//UPGRADE_TODO: Class 'java.io.ObjectOutputStream' was converted to 'System.IO.BinaryWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectOutputStream'"
					oos = new System.IO.BinaryWriter(gzos);
				}
				// end if: gzip
				else
				{
					//UPGRADE_TODO: Class 'java.io.ObjectOutputStream' was converted to 'System.IO.BinaryWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectOutputStream'"
					oos = new System.IO.BinaryWriter(b64os);
				}
				
				//UPGRADE_TODO: Method 'java.io.ObjectOutputStream.writeObject' was converted to 'SupportClass.Serialize' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectOutputStreamwriteObject_javalangObject'"
				SupportClass.Serialize(oos, serializableObject);
			}
			// end try
			catch (System.IO.IOException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
				return null;
			}
			// end catch
			finally
			{
				try
				{
					oos.Close();
				}
				catch (System.Exception e)
				{
				}
				try
				{
					//UPGRADE_ISSUE: Method 'java.util.zip.GZIPOutputStream.close' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipGZIPOutputStream'"
					gzos.close();
				}
				catch (System.Exception e)
				{
				}
				try
				{
					b64os.Close();
				}
				catch (System.Exception e)
				{
				}
				try
				{
					baos.Close();
				}
				catch (System.Exception e)
				{
				}
			} // end finally
			
			// Return value according to relevant encoding.
			try
			{
				//UPGRADE_TODO: The differences in the Format  of parameters for constructor 'java.lang.String.String'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
				return System.Text.Encoding.GetEncoding(PREFERRED_ENCODING).GetString(SupportClass.ToByteArray(SupportClass.ToSByteArray(baos.ToArray())));
			}
			// end try
			catch (System.IO.IOException uue)
			{
				return new System.String(SupportClass.ToCharArray(SupportClass.ToByteArray(SupportClass.ToSByteArray(baos.ToArray()))));
			} // end catch
		} // end encode
		
		
		
		/// <summary> Encodes a byte array into Base64 notation.
		/// Does not GZip-compress data.
		/// 
		/// </summary>
		/// <param name="source">The data to convert
		/// </param>
		/// <since> 1.4
		/// </since>
		public static System.String encodeBytes(sbyte[] source)
		{
			return encodeBytes(source, 0, source.Length, NO_OPTIONS);
		} // end encodeBytes
		
		
		
		/// <summary> Encodes a byte array into Base64 notation.
		/// <p>
		/// Valid options:<pre>
		/// GZIP: gzip-compresses object before encoding it.
		/// DONT_BREAK_LINES: don't break lines at 76 characters
		/// <i>Note: Technically, this makes your encoding non-compliant.</i>
		/// </pre>
		/// <p>
		/// Example: <code>encodeBytes( myData, Base64.GZIP )</code> or
		/// <p>
		/// Example: <code>encodeBytes( myData, Base64.GZIP | Base64.DONT_BREAK_LINES )</code>
		/// 
		/// 
		/// </summary>
		/// <param name="source">The data to convert
		/// </param>
		/// <param name="options">Specified options
		/// </param>
		/// <seealso cref="Base64.GZIP">
		/// </seealso>
		/// <seealso cref="Base64.DONT_BREAK_LINES">
		/// </seealso>
		/// <since> 2.0
		/// </since>
		public static System.String encodeBytes(sbyte[] source, int options)
		{
			return encodeBytes(source, 0, source.Length, options);
		} // end encodeBytes
		
		
		/// <summary> Encodes a byte array into Base64 notation.
		/// Does not GZip-compress data.
		/// 
		/// </summary>
		/// <param name="source">The data to convert
		/// </param>
		/// <param name="off">Offset in array where conversion should begin
		/// </param>
		/// <param name="len">Length of data to convert
		/// </param>
		/// <since> 1.4
		/// </since>
		public static System.String encodeBytes(sbyte[] source, int off, int len)
		{
			return encodeBytes(source, off, len, NO_OPTIONS);
		} // end encodeBytes
		
		
		
		/// <summary> Encodes a byte array into Base64 notation.
		/// <p>
		/// Valid options:<pre>
		/// GZIP: gzip-compresses object before encoding it.
		/// DONT_BREAK_LINES: don't break lines at 76 characters
		/// <i>Note: Technically, this makes your encoding non-compliant.</i>
		/// </pre>
		/// <p>
		/// Example: <code>encodeBytes( myData, Base64.GZIP )</code> or
		/// <p>
		/// Example: <code>encodeBytes( myData, Base64.GZIP | Base64.DONT_BREAK_LINES )</code>
		/// 
		/// 
		/// </summary>
		/// <param name="source">The data to convert
		/// </param>
		/// <param name="off">Offset in array where conversion should begin
		/// </param>
		/// <param name="len">Length of data to convert
		/// </param>
		/// <param name="options">Specified options
		/// </param>
		/// <param name="options">alphabet type is pulled from this (standard, url-safe, ordered)
		/// </param>
		/// <seealso cref="Base64.GZIP">
		/// </seealso>
		/// <seealso cref="Base64.DONT_BREAK_LINES">
		/// </seealso>
		/// <since> 2.0
		/// </since>
		public static System.String encodeBytes(sbyte[] source, int off, int len, int options)
		{
			// Isolate options
			int dontBreakLines = (options & DONT_BREAK_LINES);
			int gzip = (options & GZIP);
			
			// Compress?
			if (gzip == GZIP)
			{
				System.IO.MemoryStream baos = null;
				//UPGRADE_ISSUE: Class 'java.util.zip.GZIPOutputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipGZIPOutputStream'"
				java.util.zip.GZIPOutputStream gzos = null;
				Base64.OutputStream b64os = null;
				
				
				try
				{
					// GZip -> Base64 -> ByteArray
					baos = new System.IO.MemoryStream();
					b64os = new Base64.OutputStream(baos, ENCODE | options);
					//UPGRADE_ISSUE: Constructor 'java.util.zip.GZIPOutputStream.GZIPOutputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipGZIPOutputStream'"
					gzos = new java.util.zip.GZIPOutputStream(b64os);
					
					//UPGRADE_ISSUE: Method 'java.util.zip.GZIPOutputStream.write' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipGZIPOutputStream'"
					gzos.write(source, off, len);
					//UPGRADE_ISSUE: Method 'java.util.zip.GZIPOutputStream.close' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipGZIPOutputStream'"
					gzos.close();
				}
				// end try
				catch (System.IO.IOException e)
				{
					SupportClass.WriteStackTrace(e, Console.Error);
					return null;
				}
				// end catch
				finally
				{
					try
					{
						//UPGRADE_ISSUE: Method 'java.util.zip.GZIPOutputStream.close' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipGZIPOutputStream'"
						gzos.close();
					}
					catch (System.Exception e)
					{
					}
					try
					{
						b64os.Close();
					}
					catch (System.Exception e)
					{
					}
					try
					{
						baos.Close();
					}
					catch (System.Exception e)
					{
					}
				} // end finally
				
				// Return value according to relevant encoding.
				try
				{
					//UPGRADE_TODO: The differences in the Format  of parameters for constructor 'java.lang.String.String'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
					return System.Text.Encoding.GetEncoding(PREFERRED_ENCODING).GetString(SupportClass.ToByteArray(SupportClass.ToSByteArray(baos.ToArray())));
				}
				// end try
				catch (System.IO.IOException uue)
				{
					return new System.String(SupportClass.ToCharArray(SupportClass.ToByteArray(SupportClass.ToSByteArray(baos.ToArray()))));
				} // end catch
			}
			// end if: compress
			
			// Else, don't compress. Better not to use streams at all then.
			else
			{
				// Convert option to boolean in way that code likes it.
				bool breakLines = dontBreakLines == 0;
				
				int len43 = len * 4 / 3;
				sbyte[] outBuff = new sbyte[(len43) + ((len % 3) > 0?4:0) + (breakLines?(len43 / MAX_LINE_LENGTH):0)]; // New lines
				int d = 0;
				int e = 0;
				int len2 = len - 2;
				int lineLength = 0;
				for (; d < len2; d += 3, e += 4)
				{
					encode3to4(source, d + off, 3, outBuff, e, options);
					
					lineLength += 4;
					if (breakLines && lineLength == MAX_LINE_LENGTH)
					{
						outBuff[e + 4] = NEW_LINE;
						e++;
						lineLength = 0;
					} // end if: end of line
				} // en dfor: each piece of array
				
				if (d < len)
				{
					encode3to4(source, d + off, len - d, outBuff, e, options);
					e += 4;
				} // end if: some padding needed
				
				
				// Return value according to relevant encoding.
				try
				{
					System.String tempStr;
					//UPGRADE_TODO: The differences in the Format  of parameters for constructor 'java.lang.String.String'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
					tempStr = System.Text.Encoding.GetEncoding(PREFERRED_ENCODING).GetString(SupportClass.ToByteArray(outBuff));
					return new System.String(tempStr.ToCharArray(), 0, e);
				}
				// end try
				catch (System.IO.IOException uue)
				{
					return new System.String(SupportClass.ToCharArray(outBuff), 0, e);
				} // end catch
			} // end else: don't compress
		} // end encodeBytes
		
		
		
		
		
		/* ********  D E C O D I N G   M E T H O D S  ******** */
		
		
		/// <summary> Decodes four bytes from array <var>source</var>
		/// and writes the resulting bytes (up to three of them)
		/// to <var>destination</var>.
		/// The source and destination arrays can be manipulated
		/// anywhere along their length by specifying
		/// <var>srcOffset</var> and <var>destOffset</var>.
		/// This method does not check to make sure your arrays
		/// are large enough to accomodate <var>srcOffset</var> + 4 for
		/// the <var>source</var> array or <var>destOffset</var> + 3 for
		/// the <var>destination</var> array.
		/// This method returns the actual number of bytes that
		/// were converted from the Base64 encoding.
		/// <p>This is the lowest level of the decoding methods with
		/// all possible parameters.</p>
		/// 
		/// 
		/// </summary>
		/// <param name="source">the array to convert
		/// </param>
		/// <param name="srcOffset">the index where conversion begins
		/// </param>
		/// <param name="destination">the array to hold the conversion
		/// </param>
		/// <param name="destOffset">the index where output will be put
		/// </param>
		/// <param name="options">alphabet type is pulled from this (standard, url-safe, ordered)
		/// </param>
		/// <returns> the number of decoded bytes converted
		/// </returns>
		/// <since> 1.3
		/// </since>
		private static int decode4to3(sbyte[] source, int srcOffset, sbyte[] destination, int destOffset, int options)
		{
			sbyte[] DECODABET = getDecodabet(options);
			
			// Example: Dk==
			if (source[srcOffset + 2] == EQUALS_SIGN)
			{
				// Two ways to do the same thing. Don't know which way I like best.
				//int outBuff =   ( ( DECODABET[ source[ srcOffset    ] ] << 24 ) >>>  6 )
				//              | ( ( DECODABET[ source[ srcOffset + 1] ] << 24 ) >>> 12 );
				int outBuff = ((DECODABET[source[srcOffset]] & 0xFF) << 18) | ((DECODABET[source[srcOffset + 1]] & 0xFF) << 12);
				
				destination[destOffset] = (sbyte) (SupportClass.URShift(outBuff, 16));
				return 1;
			}
			// Example: DkL=
			else if (source[srcOffset + 3] == EQUALS_SIGN)
			{
				// Two ways to do the same thing. Don't know which way I like best.
				//int outBuff =   ( ( DECODABET[ source[ srcOffset     ] ] << 24 ) >>>  6 )
				//              | ( ( DECODABET[ source[ srcOffset + 1 ] ] << 24 ) >>> 12 )
				//              | ( ( DECODABET[ source[ srcOffset + 2 ] ] << 24 ) >>> 18 );
				int outBuff = ((DECODABET[source[srcOffset]] & 0xFF) << 18) | ((DECODABET[source[srcOffset + 1]] & 0xFF) << 12) | ((DECODABET[source[srcOffset + 2]] & 0xFF) << 6);
				
				destination[destOffset] = (sbyte) (SupportClass.URShift(outBuff, 16));
				destination[destOffset + 1] = (sbyte) (SupportClass.URShift(outBuff, 8));
				return 2;
			}
			// Example: DkLE
			else
			{
				try
				{
					// Two ways to do the same thing. Don't know which way I like best.
					//int outBuff =   ( ( DECODABET[ source[ srcOffset     ] ] << 24 ) >>>  6 )
					//              | ( ( DECODABET[ source[ srcOffset + 1 ] ] << 24 ) >>> 12 )
					//              | ( ( DECODABET[ source[ srcOffset + 2 ] ] << 24 ) >>> 18 )
					//              | ( ( DECODABET[ source[ srcOffset + 3 ] ] << 24 ) >>> 24 );
					int outBuff = ((DECODABET[source[srcOffset]] & 0xFF) << 18) | ((DECODABET[source[srcOffset + 1]] & 0xFF) << 12) | ((DECODABET[source[srcOffset + 2]] & 0xFF) << 6) | ((DECODABET[source[srcOffset + 3]] & 0xFF));
					
					
					destination[destOffset] = (sbyte) (outBuff >> 16);
					destination[destOffset + 1] = (sbyte) (outBuff >> 8);
					destination[destOffset + 2] = (sbyte) (outBuff);
					
					return 3;
				}
				catch (System.Exception e)
				{
					System.Console.Out.WriteLine("" + source[srcOffset] + ": " + (DECODABET[source[srcOffset]]));
					System.Console.Out.WriteLine("" + source[srcOffset + 1] + ": " + (DECODABET[source[srcOffset + 1]]));
					System.Console.Out.WriteLine("" + source[srcOffset + 2] + ": " + (DECODABET[source[srcOffset + 2]]));
					System.Console.Out.WriteLine("" + source[srcOffset + 3] + ": " + (DECODABET[source[srcOffset + 3]]));
					return - 1;
				} // end catch
			}
		} // end decodeToBytes
		
		
		
		
		/// <summary> Very low-level access to decoding ASCII characters in
		/// the form of a byte array. Does not support automatically
		/// gunzipping or any other "fancy" features.
		/// 
		/// </summary>
		/// <param name="source">The Base64 encoded data
		/// </param>
		/// <param name="off">   The offset of where to begin decoding
		/// </param>
		/// <param name="len">   The length of characters to decode
		/// </param>
		/// <returns> decoded data
		/// </returns>
		/// <since> 1.3
		/// </since>
		public static sbyte[] decode(sbyte[] source, int off, int len, int options)
		{
			sbyte[] DECODABET = getDecodabet(options);
			
			int len34 = len * 3 / 4;
			sbyte[] outBuff = new sbyte[len34]; // Upper limit on size of output
			int outBuffPosn = 0;
			
			sbyte[] b4 = new sbyte[4];
			int b4Posn = 0;
			int i = 0;
			sbyte sbiCrop = 0;
			sbyte sbiDecode = 0;
			for (i = off; i < off + len; i++)
			{
				sbiCrop = (sbyte) (source[i] & 0x7f); // Only the low seven bits
				sbiDecode = DECODABET[sbiCrop];
				
				if (sbiDecode >= WHITE_SPACE_ENC)
				// White space, Equals sign or better
				{
					if (sbiDecode >= EQUALS_SIGN_ENC)
					{
						b4[b4Posn++] = sbiCrop;
						if (b4Posn > 3)
						{
							outBuffPosn += decode4to3(b4, 0, outBuff, outBuffPosn, options);
							b4Posn = 0;
							
							// If that was the equals sign, break out of 'for' loop
							if (sbiCrop == EQUALS_SIGN)
								break;
						} // end if: quartet built
					} // end if: equals sign or better
				}
				// end if: white space, equals sign or better
				else
				{
					System.Console.Error.WriteLine("Bad Base64 input character at " + i + ": " + source[i] + "(decimal)");
					return null;
				} // end else:
			} // each input character
			
			sbyte[] out_Renamed = new sbyte[outBuffPosn];
			Array.Copy(outBuff, 0, out_Renamed, 0, outBuffPosn);
			return out_Renamed;
		} // end decode
		
		
		
		
		/// <summary> Decodes data from Base64 notation, automatically
		/// detecting gzip-compressed data and decompressing it.
		/// 
		/// </summary>
		/// <param name="s">the string to decode
		/// </param>
		/// <returns> the decoded data
		/// </returns>
		/// <since> 1.4
		/// </since>
		public static sbyte[] decode(System.String s)
		{
			return decode(s, NO_OPTIONS);
		}
		
		
		/// <summary> Decodes data from Base64 notation, automatically
		/// detecting gzip-compressed data and decompressing it.
		/// 
		/// </summary>
		/// <param name="s">the string to decode
		/// </param>
		/// <param name="options">encode options such as URL_SAFE
		/// </param>
		/// <returns> the decoded data
		/// </returns>
		/// <since> 1.4
		/// </since>
		public static sbyte[] decode(System.String s, int options)
		{
			sbyte[] bytes;
			try
			{
				//UPGRADE_TODO: Method 'java.lang.String.getBytes' was converted to 'System.Text.Encoding.GetEncoding(string).GetBytes(string)' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangStringgetBytes_javalangString'"
				bytes = SupportClass.ToSByteArray(System.Text.Encoding.GetEncoding(PREFERRED_ENCODING).GetBytes(s));
			}
			// end try
			catch (System.IO.IOException uee)
			{
				bytes = SupportClass.ToSByteArray(SupportClass.ToByteArray(s));
			} // end catch
			//</change>
			
			// Decode
			bytes = decode(bytes, 0, bytes.Length, options);
			
			
			// Check to see if it's gzip-compressed
			// GZIP Magic Two-Byte Number: 0x8b1f (35615)
			if (bytes != null && bytes.Length >= 4)
			{
				
				int head = ((int) bytes[0] & 0xff) | ((bytes[1] << 8) & 0xff00);
				//UPGRADE_ISSUE: Field 'java.util.zip.GZIPInputStream.GZIP_MAGIC' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipGZIPInputStream'"
				if (java.util.zip.GZIPInputStream.GZIP_MAGIC == head)
				{
					System.IO.MemoryStream bais = null;
					//UPGRADE_ISSUE: Class 'java.util.zip.GZIPInputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipGZIPInputStream'"
					java.util.zip.GZIPInputStream gzis = null;
					System.IO.MemoryStream baos = null;
					sbyte[] buffer = new sbyte[2048];
					int length = 0;
					
					try
					{
						baos = new System.IO.MemoryStream();
						bais = new System.IO.MemoryStream(SupportClass.ToByteArray(bytes));
						//UPGRADE_ISSUE: Constructor 'java.util.zip.GZIPInputStream.GZIPInputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipGZIPInputStream'"
						gzis = new java.util.zip.GZIPInputStream(bais);
						
						while ((length = SupportClass.ReadInput(gzis.BaseStream, buffer, 0, buffer.Length)) >= 0)
						{
							baos.Write(SupportClass.ToByteArray(buffer), 0, length);
						} // end while: reading input
						
						// No error? Get new bytes.
						bytes = SupportClass.ToSByteArray(baos.ToArray());
					}
					// end try
					catch (System.IO.IOException e)
					{
						// Just return originally-decoded bytes
					}
					// end catch
					finally
					{
						try
						{
							baos.Close();
						}
						catch (System.Exception e)
						{
						}
						try
						{
							//UPGRADE_ISSUE: Method 'java.util.zip.GZIPInputStream.close' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipGZIPInputStream'"
							gzis.close();
						}
						catch (System.Exception e)
						{
						}
						try
						{
							bais.Close();
						}
						catch (System.Exception e)
						{
						}
					} // end finally
				} // end if: gzipped
			} // end if: bytes.length >= 2
			
			return bytes;
		} // end decode
		
		
		
		
		/// <summary> Attempts to decode Base64 data and deserialize a Java
		/// Object within. Returns <tt>null</tt> if there was an error.
		/// 
		/// </summary>
		/// <param name="encodedObject">The Base64 data to decode
		/// </param>
		/// <returns> The decoded and deserialized object
		/// </returns>
		/// <since> 1.5
		/// </since>
		public static System.Object decodeToObject(System.String encodedObject)
		{
			// Decode and gunzip if necessary
			sbyte[] objBytes = decode(encodedObject);
			
			System.IO.MemoryStream bais = null;
			//UPGRADE_TODO: Class 'java.io.ObjectInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectInputStream'"
			System.IO.BinaryReader ois = null;
			System.Object obj = null;
			
			try
			{
				bais = new System.IO.MemoryStream(SupportClass.ToByteArray(objBytes));
				//UPGRADE_TODO: Class 'java.io.ObjectInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectInputStream'"
				ois = new System.IO.BinaryReader(bais);
				
				//UPGRADE_WARNING: Method 'java.io.ObjectInputStream.readObject' was converted to 'SupportClass.Deserialize' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
				obj = SupportClass.Deserialize(ois);
			}
			// end try
			catch (System.IO.IOException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
				obj = null;
			}
			// end catch
			//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			catch (System.Exception e)
			{
				//UPGRADE_ISSUE: Method 'java.lang.ClassNotFoundException.printStackTrace' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassNotFoundExceptionprintStackTrace'"
				e.printStackTrace();
				obj = null;
			}
			// end catch
			finally
			{
				try
				{
					bais.Close();
				}
				catch (System.Exception e)
				{
				}
				try
				{
					ois.Close();
				}
				catch (System.Exception e)
				{
				}
			} // end finally
			
			return obj;
		} // end decodeObject
		
		
		
		/// <summary> Convenience method for encoding data to a file.
		/// 
		/// </summary>
		/// <param name="dataToEncode">byte array of data to encode in base64 form
		/// </param>
		/// <param name="filename">Filename for saving encoded data
		/// </param>
		/// <returns> <tt>true</tt> if successful, <tt>false</tt> otherwise
		/// 
		/// </returns>
		/// <since> 2.1
		/// </since>
		public static bool encodeToFile(sbyte[] dataToEncode, System.String filename)
		{
			bool success = false;
			Base64.OutputStream bos = null;
			try
			{
				//UPGRADE_TODO: Constructor 'java.io.FileOutputStream.FileOutputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileOutputStreamFileOutputStream_javalangString'"
				bos = new Base64.OutputStream(new System.IO.FileStream(filename, System.IO.FileMode.Create), Base64.ENCODE);
				bos.Write(SupportClass.ToByteArray(dataToEncode));
				success = true;
			}
			// end try
			catch (System.IO.IOException e)
			{
				
				success = false;
			}
			// end catch: IOException
			finally
			{
				try
				{
					bos.Close();
				}
				catch (System.Exception e)
				{
				}
			} // end finally
			
			return success;
		} // end encodeToFile
		
		
		/// <summary> Convenience method for decoding data to a file.
		/// 
		/// </summary>
		/// <param name="dataToDecode">Base64-encoded data as a string
		/// </param>
		/// <param name="filename">Filename for saving decoded data
		/// </param>
		/// <returns> <tt>true</tt> if successful, <tt>false</tt> otherwise
		/// 
		/// </returns>
		/// <since> 2.1
		/// </since>
		public static bool decodeToFile(System.String dataToDecode, System.String filename)
		{
			bool success = false;
			Base64.OutputStream bos = null;
			try
			{
				//UPGRADE_TODO: Constructor 'java.io.FileOutputStream.FileOutputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileOutputStreamFileOutputStream_javalangString'"
				bos = new Base64.OutputStream(new System.IO.FileStream(filename, System.IO.FileMode.Create), Base64.DECODE);
				//UPGRADE_TODO: Method 'java.lang.String.getBytes' was converted to 'System.Text.Encoding.GetEncoding(string).GetBytes(string)' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangStringgetBytes_javalangString'"
				bos.Write(SupportClass.ToByteArray(SupportClass.ToSByteArray(System.Text.Encoding.GetEncoding(PREFERRED_ENCODING).GetBytes(dataToDecode))));
				success = true;
			}
			// end try
			catch (System.IO.IOException e)
			{
				success = false;
			}
			// end catch: IOException
			finally
			{
				try
				{
					bos.Close();
				}
				catch (System.Exception e)
				{
				}
			} // end finally
			
			return success;
		} // end decodeToFile
		
		
		
		
		/// <summary> Convenience method for reading a base64-encoded
		/// file and decoding it.
		/// 
		/// </summary>
		/// <param name="filename">Filename for reading encoded data
		/// </param>
		/// <returns> decoded byte array or null if unsuccessful
		/// 
		/// </returns>
		/// <since> 2.1
		/// </since>
		public static sbyte[] decodeFromFile(System.String filename)
		{
			sbyte[] decodedData = null;
			Base64.InputStream bis = null;
			try
			{
				// Set up some useful variables
				System.IO.FileInfo file = new System.IO.FileInfo(filename);
				sbyte[] buffer = null;
				int length = 0;
				int numBytes = 0;
				
				// Check for size of file
				if (SupportClass.FileLength(file) > System.Int32.MaxValue)
				{
					System.Console.Error.WriteLine("File is too big for this convenience method (" + SupportClass.FileLength(file) + " bytes).");
					return null;
				} // end if: file too big for int index
				buffer = new sbyte[(int) SupportClass.FileLength(file)];
				
				// Open a stream
				//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javaioFile'"
				bis = new Base64.InputStream(new System.IO.BufferedStream(new System.IO.FileStream(file.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read)), Base64.DECODE);
				
				// Read until done
				while ((numBytes = bis.read(buffer, length, 4096)) >= 0)
					length += numBytes;
				
				// Save in a variable to return
				decodedData = new sbyte[length];
				Array.Copy(buffer, 0, decodedData, 0, length);
			}
			// end try
			catch (System.IO.IOException e)
			{
				System.Console.Error.WriteLine("Error decoding from file " + filename);
			}
			// end catch: IOException
			finally
			{
				try
				{
					//UPGRADE_TODO: Method 'java.io.FilterInputStream.close' was converted to 'System.IO.BinaryReader.Close' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFilterInputStreamclose'"
					bis.Close();
				}
				catch (System.Exception e)
				{
				}
			} // end finally
			
			return decodedData;
		} // end decodeFromFile
		
		
		
		/// <summary> Convenience method for reading a binary file
		/// and base64-encoding it.
		/// 
		/// </summary>
		/// <param name="filename">Filename for reading binary data
		/// </param>
		/// <returns> base64-encoded string or null if unsuccessful
		/// 
		/// </returns>
		/// <since> 2.1
		/// </since>
		public static System.String encodeFromFile(System.String filename)
		{
			System.String encodedData = null;
			Base64.InputStream bis = null;
			try
			{
				// Set up some useful variables
				System.IO.FileInfo file = new System.IO.FileInfo(filename);
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				sbyte[] buffer = new sbyte[System.Math.Max((int) (SupportClass.FileLength(file) * 1.4), 40)]; // Need max() for math on small files (v2.2.1)
				int length = 0;
				int numBytes = 0;
				
				// Open a stream
				//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javaioFile'"
				bis = new Base64.InputStream(new System.IO.BufferedStream(new System.IO.FileStream(file.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read)), Base64.ENCODE);
				
				// Read until done
				while ((numBytes = bis.read(buffer, length, 4096)) >= 0)
					length += numBytes;
				
				// Save in a variable to return
				System.String tempStr;
				//UPGRADE_TODO: The differences in the Format  of parameters for constructor 'java.lang.String.String'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
				tempStr = System.Text.Encoding.GetEncoding(Base64.PREFERRED_ENCODING).GetString(SupportClass.ToByteArray(buffer));
				encodedData = new System.String(tempStr.ToCharArray(), 0, length);
			}
			// end try
			catch (System.IO.IOException e)
			{
				System.Console.Error.WriteLine("Error encoding from file " + filename);
			}
			// end catch: IOException
			finally
			{
				try
				{
					//UPGRADE_TODO: Method 'java.io.FilterInputStream.close' was converted to 'System.IO.BinaryReader.Close' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFilterInputStreamclose'"
					bis.Close();
				}
				catch (System.Exception e)
				{
				}
			} // end finally
			
			return encodedData;
		} // end encodeFromFile
		
		
		
		
		/// <summary> Reads <tt>infile</tt> and encodes it to <tt>outfile</tt>.
		/// 
		/// </summary>
		/// <param name="infile">Input file
		/// </param>
		/// <param name="outfile">Output file
		/// </param>
		/// <returns> true if the operation is successful
		/// </returns>
		/// <since> 2.2
		/// </since>
		public static bool encodeFileToFile(System.String infile, System.String outfile)
		{
			bool success = false;
			System.IO.Stream in_Renamed = null;
			System.IO.Stream out_Renamed = null;
			try
			{
				//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javalangString'"
				in_Renamed = new Base64.InputStream(new System.IO.BufferedStream(new System.IO.FileStream(infile, System.IO.FileMode.Open, System.IO.FileAccess.Read)), Base64.ENCODE);
				//UPGRADE_TODO: Constructor 'java.io.FileOutputStream.FileOutputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileOutputStreamFileOutputStream_javalangString'"
				out_Renamed = new System.IO.BufferedStream(new System.IO.FileStream(outfile, System.IO.FileMode.Create));
				sbyte[] buffer = new sbyte[65536]; // 64K
				int read = - 1;
				while ((read = in_Renamed is VassalSharp.tools.image.PNGChunkSkipInputStream?((VassalSharp.tools.image.PNGChunkSkipInputStream) in_Renamed).read(buffer):SupportClass.ReadInput(in_Renamed, buffer, 0, buffer.Length)) >= 0)
				{
					out_Renamed.Write(SupportClass.ToByteArray(buffer), 0, read);
				} // end while: through file
				success = true;
			}
			catch (System.IO.IOException exc)
			{
				SupportClass.WriteStackTrace(exc, Console.Error);
			}
			finally
			{
				try
				{
					in_Renamed.Close();
				}
				catch (System.Exception exc)
				{
				}
				try
				{
					out_Renamed.Close();
				}
				catch (System.Exception exc)
				{
				}
			} // end finally
			
			return success;
		} // end encodeFileToFile
		
		
		
		/// <summary> Reads <tt>infile</tt> and decodes it to <tt>outfile</tt>.
		/// 
		/// </summary>
		/// <param name="infile">Input file
		/// </param>
		/// <param name="outfile">Output file
		/// </param>
		/// <returns> true if the operation is successful
		/// </returns>
		/// <since> 2.2
		/// </since>
		public static bool decodeFileToFile(System.String infile, System.String outfile)
		{
			bool success = false;
			System.IO.Stream in_Renamed = null;
			System.IO.Stream out_Renamed = null;
			try
			{
				//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javalangString'"
				in_Renamed = new Base64.InputStream(new System.IO.BufferedStream(new System.IO.FileStream(infile, System.IO.FileMode.Open, System.IO.FileAccess.Read)), Base64.DECODE);
				//UPGRADE_TODO: Constructor 'java.io.FileOutputStream.FileOutputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileOutputStreamFileOutputStream_javalangString'"
				out_Renamed = new System.IO.BufferedStream(new System.IO.FileStream(outfile, System.IO.FileMode.Create));
				sbyte[] buffer = new sbyte[65536]; // 64K
				int read = - 1;
				while ((read = in_Renamed is VassalSharp.tools.image.PNGChunkSkipInputStream?((VassalSharp.tools.image.PNGChunkSkipInputStream) in_Renamed).read(buffer):SupportClass.ReadInput(in_Renamed, buffer, 0, buffer.Length)) >= 0)
				{
					out_Renamed.Write(SupportClass.ToByteArray(buffer), 0, read);
				} // end while: through file
				success = true;
			}
			catch (System.IO.IOException exc)
			{
				SupportClass.WriteStackTrace(exc, Console.Error);
			}
			finally
			{
				try
				{
					in_Renamed.Close();
				}
				catch (System.Exception exc)
				{
				}
				try
				{
					out_Renamed.Close();
				}
				catch (System.Exception exc)
				{
				}
			} // end finally
			
			return success;
		} // end decodeFileToFile
		
		
		/* ********  I N N E R   C L A S S   I N P U T S T R E A M  ******** */
		
		
		
		/// <summary> A {@link Base64.InputStream} will read data from another
		/// <tt>java.io.InputStream</tt>, given in the constructor,
		/// and encode/decode to/from Base64 notation on the fly.
		/// 
		/// </summary>
		/// <seealso cref="Base64">
		/// </seealso>
		/// <since> 1.3
		/// </since>
		public class InputStream:System.IO.BinaryReader
		{
			private bool encode; // Encoding or decoding
			private int position; // Current position in the buffer
			private sbyte[] buffer; // Small buffer holding converted data
			private int bufferLength; // Length of buffer (3 or 4)
			private int numSigBytes; // Number of meaningful bytes in the buffer
			private int lineLength;
			private bool breakLines; // Break lines at less than 80 characters
			private int options; // Record options used to create the stream.
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			SuppressWarnings(unused)
			private sbyte[] alphabet; // Local copies to avoid extra method calls
			private sbyte[] decodabet; // Local copies to avoid extra method calls
			
			
			/// <summary> Constructs a {@link Base64.InputStream} in DECODE mode.
			/// 
			/// </summary>
			/// <param name="in">the <tt>java.io.InputStream</tt> from which to read data.
			/// </param>
			/// <since> 1.3
			/// </since>
			public InputStream(System.IO.Stream in_Renamed):this(in_Renamed, VassalSharp.tools.Base64.DECODE)
			{
			} // end constructor
			
			
			/// <summary> Constructs a {@link Base64.InputStream} in
			/// either ENCODE or DECODE mode.
			/// <p>
			/// Valid options:<pre>
			/// ENCODE or DECODE: Encode or Decode as data is read.
			/// DONT_BREAK_LINES: don't break lines at 76 characters
			/// (only meaningful when encoding)
			/// <i>Note: Technically, this makes your encoding non-compliant.</i>
			/// </pre>
			/// <p>
			/// Example: <code>new Base64.InputStream( in, Base64.DECODE )</code>
			/// 
			/// 
			/// </summary>
			/// <param name="in">the <tt>java.io.InputStream</tt> from which to read data.
			/// </param>
			/// <param name="options">Specified options
			/// </param>
			/// <seealso cref="Base64.ENCODE">
			/// </seealso>
			/// <seealso cref="Base64.DECODE">
			/// </seealso>
			/// <seealso cref="Base64.DONT_BREAK_LINES">
			/// </seealso>
			/// <since> 2.0
			/// </since>
			public InputStream(System.IO.Stream in_Renamed, int options):base(in_Renamed)
			{
				this.breakLines = (options & VassalSharp.tools.Base64.DONT_BREAK_LINES) != VassalSharp.tools.Base64.DONT_BREAK_LINES;
				this.encode = (options & VassalSharp.tools.Base64.ENCODE) == VassalSharp.tools.Base64.ENCODE;
				this.bufferLength = encode?4:3;
				this.buffer = new sbyte[bufferLength];
				this.position = - 1;
				this.lineLength = 0;
				this.options = options; // Record for later, mostly to determine which alphabet to use
				this.alphabet = VassalSharp.tools.Base64.getAlphabet(options);
				this.decodabet = VassalSharp.tools.Base64.getDecodabet(options);
			} // end constructor
			
			/// <summary> Reads enough of the input stream to convert
			/// to/from Base64 and returns the next byte.
			/// 
			/// </summary>
			/// <returns> next byte
			/// </returns>
			/// <since> 1.3
			/// </since>
			public  override int Read()
			{
				// Do we need to get data?
				if (position < 0)
				{
					if (encode)
					{
						sbyte[] b3 = new sbyte[3];
						int numBinaryBytes = 0;
						for (int i = 0; i < 3; i++)
						{
							try
							{
								int b = ((System.IO.Stream) BaseStream).ReadByte();
								
								// If end of stream, b is -1.
								if (b >= 0)
								{
									b3[i] = (sbyte) b;
									numBinaryBytes++;
								} // end if: not end of stream
							}
							// end try: read
							catch (System.IO.IOException e)
							{
								// Only a problem if we got no data at all.
								if (i == 0)
									throw e;
							} // end catch
						} // end for: each needed input byte
						
						if (numBinaryBytes > 0)
						{
							VassalSharp.tools.Base64.encode3to4(b3, 0, numBinaryBytes, buffer, 0, options);
							position = 0;
							numSigBytes = 4;
						}
						// end if: got data
						else
						{
							return - 1;
						} // end else
					}
					// end if: encoding
					
					// Else decoding
					else
					{
						sbyte[] b4 = new sbyte[4];
						int i = 0;
						for (i = 0; i < 4; i++)
						{
							// Read four "meaningful" bytes:
							int b = 0;
							do 
							{
								b = ((System.IO.Stream) BaseStream).ReadByte();
							}
							while (b >= 0 && decodabet[b & 0x7f] <= VassalSharp.tools.Base64.WHITE_SPACE_ENC);
							
							if (b < 0)
								break; // Reads a -1 if end of stream
							
							b4[i] = (sbyte) b;
						} // end for: each needed input byte
						
						if (i == 4)
						{
							numSigBytes = VassalSharp.tools.Base64.decode4to3(b4, 0, buffer, 0, options);
							position = 0;
						}
						// end if: got four characters
						else if (i == 0)
						{
							return - 1;
						}
						// end else if: also padded correctly
						else
						{
							// Must have broken out from above.
							throw new System.IO.IOException("Improperly padded Base64 input.");
						} // end
					} // end else: decode
				} // end else: get data
				
				// Got data?
				if (position >= 0)
				{
					// End of relevant data?
					if (position >= numSigBytes)
						return - 1;
					
					if (encode && breakLines && lineLength >= VassalSharp.tools.Base64.MAX_LINE_LENGTH)
					{
						lineLength = 0;
						return '\n';
					}
					// end if
					else
					{
						lineLength++; // This isn't important when decoding
						// but throwing an extra "if" seems
						// just as wasteful.
						
						int b = buffer[position++];
						
						if (position >= bufferLength)
							position = - 1;
						
						return b & 0xFF; // This is how you "cast" a byte that's
						// intended to be unsigned.
					} // end else
				}
				// end if: position >= 0
				
				// Else error
				else
				{
					// When JDK1.4 is more accepted, use an assertion here.
					throw new System.IO.IOException("Error in Base64 code reading stream.");
				} // end else
			} // end read
			
			
			/// <summary> Calls {@link #read()} repeatedly until the end of stream
			/// is reached or <var>len</var> bytes are read.
			/// Returns number of bytes read into array or -1 if
			/// end of stream is encountered.
			/// 
			/// </summary>
			/// <param name="dest">array to hold values
			/// </param>
			/// <param name="off">offset for array
			/// </param>
			/// <param name="len">max number of bytes to read into array
			/// </param>
			/// <returns> bytes read into array or -1 if end of stream is encountered.
			/// </returns>
			/// <since> 1.3
			/// </since>
			//UPGRADE_NOTE: The equivalent of method 'java.io.FilterInputStream.read' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public int read(sbyte[] dest, int off, int len)
			{
				int i;
				int b;
				for (i = 0; i < len; i++)
				{
					b = Read();
					
					//if( b < 0 && i == 0 )
					//    return -1;
					
					if (b >= 0)
						dest[off + i] = (sbyte) b;
					else if (i == 0)
						return - 1;
					else
						break; // Out of 'for' loop
				} // end for: each byte read
				return i;
			} // end read
		} // end inner class InputStream
		
		
		
		
		
		
		/* ********  I N N E R   C L A S S   O U T P U T S T R E A M  ******** */
		
		
		
		/// <summary> A {@link Base64.OutputStream} will write data to another
		/// <tt>java.io.OutputStream</tt>, given in the constructor,
		/// and encode/decode to/from Base64 notation on the fly.
		/// 
		/// </summary>
		/// <seealso cref="Base64">
		/// </seealso>
		/// <since> 1.3
		/// </since>
		public class OutputStream:System.IO.BinaryWriter
		{
			private bool encode;
			private int position;
			private sbyte[] buffer;
			private int bufferLength;
			private int lineLength;
			private bool breakLines;
			private sbyte[] b4; // Scratch used in a few places
			private bool suspendEncoding_Renamed_Field;
			private int options; // Record for later
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			SuppressWarnings(unused)
			private sbyte[] alphabet; // Local copies to avoid extra method calls
			private sbyte[] decodabet; // Local copies to avoid extra method calls
			
			/// <summary> Constructs a {@link Base64.OutputStream} in ENCODE mode.
			/// 
			/// </summary>
			/// <param name="out">the <tt>java.io.OutputStream</tt> to which data will be written.
			/// </param>
			/// <since> 1.3
			/// </since>
			public OutputStream(System.IO.Stream out_Renamed):this(out_Renamed, VassalSharp.tools.Base64.ENCODE)
			{
			} // end constructor
			
			
			/// <summary> Constructs a {@link Base64.OutputStream} in
			/// either ENCODE or DECODE mode.
			/// <p>
			/// Valid options:<pre>
			/// ENCODE or DECODE: Encode or Decode as data is read.
			/// DONT_BREAK_LINES: don't break lines at 76 characters
			/// (only meaningful when encoding)
			/// <i>Note: Technically, this makes your encoding non-compliant.</i>
			/// </pre>
			/// <p>
			/// Example: <code>new Base64.OutputStream( out, Base64.ENCODE )</code>
			/// 
			/// </summary>
			/// <param name="out">the <tt>java.io.OutputStream</tt> to which data will be written.
			/// </param>
			/// <param name="options">Specified options.
			/// </param>
			/// <seealso cref="Base64.ENCODE">
			/// </seealso>
			/// <seealso cref="Base64.DECODE">
			/// </seealso>
			/// <seealso cref="Base64.DONT_BREAK_LINES">
			/// </seealso>
			/// <since> 1.3
			/// </since>
			public OutputStream(System.IO.Stream out_Renamed, int options):base(out_Renamed)
			{
				this.breakLines = (options & VassalSharp.tools.Base64.DONT_BREAK_LINES) != VassalSharp.tools.Base64.DONT_BREAK_LINES;
				this.encode = (options & VassalSharp.tools.Base64.ENCODE) == VassalSharp.tools.Base64.ENCODE;
				this.bufferLength = encode?3:4;
				this.buffer = new sbyte[bufferLength];
				this.position = 0;
				this.lineLength = 0;
				this.suspendEncoding_Renamed_Field = false;
				this.b4 = new sbyte[4];
				this.options = options;
				this.alphabet = VassalSharp.tools.Base64.getAlphabet(options);
				this.decodabet = VassalSharp.tools.Base64.getDecodabet(options);
			} // end constructor
			
			
			/// <summary> Writes the byte to the output stream after
			/// converting to/from Base64 notation.
			/// When encoding, bytes are buffered three
			/// at a time before the output stream actually
			/// gets a write() call.
			/// When decoding, bytes are buffered four
			/// at a time.
			/// 
			/// </summary>
			/// <param name="theByte">the byte to write
			/// </param>
			/// <since> 1.3
			/// </since>
			//UPGRADE_NOTE: The equivalent of method 'java.io.FilterOutputStream.write' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public void  write(int theByte)
			{
				// Encoding suspended?
				if (suspendEncoding_Renamed_Field)
				{
					base.BaseStream.WriteByte((System.Byte) theByte);
					return ;
				} // end if: supsended
				
				// Encode?
				if (encode)
				{
					buffer[position++] = (sbyte) theByte;
					if (position >= bufferLength)
					// Enough to encode.
					{
						sbyte[] temp_sbyteArray;
						temp_sbyteArray = VassalSharp.tools.Base64.encode3to4(b4, buffer, bufferLength, options);
						BaseStream.Write(SupportClass.ToByteArray(temp_sbyteArray), 0, temp_sbyteArray.Length);
						
						lineLength += 4;
						if (breakLines && lineLength >= VassalSharp.tools.Base64.MAX_LINE_LENGTH)
						{
							BaseStream.WriteByte((byte) VassalSharp.tools.Base64.NEW_LINE);
							lineLength = 0;
						} // end if: end of line
						
						position = 0;
					} // end if: enough to output
				}
				// end if: encoding
				
				// Else, Decoding
				else
				{
					// Meaningful Base64 character?
					if (decodabet[theByte & 0x7f] > VassalSharp.tools.Base64.WHITE_SPACE_ENC)
					{
						buffer[position++] = (sbyte) theByte;
						if (position >= bufferLength)
						// Enough to output.
						{
							int len = Base64.decode4to3(buffer, 0, b4, 0, options);
							BaseStream.Write(SupportClass.ToByteArray(b4), 0, len);
							//out.write( Base64.decode4to3( buffer ) );
							position = 0;
						} // end if: enough to output
					}
					// end if: meaningful base64 character
					else if (decodabet[theByte & 0x7f] != VassalSharp.tools.Base64.WHITE_SPACE_ENC)
					{
						throw new System.IO.IOException("Invalid character in Base64 data.");
					} // end else: not white space either
				} // end else: decoding
			} // end write
			
			
			
			/// <summary> Calls {@link #write(int)} repeatedly until <var>len</var>
			/// bytes are written.
			/// 
			/// </summary>
			/// <param name="theBytes">array from which to read bytes
			/// </param>
			/// <param name="off">offset for array
			/// </param>
			/// <param name="len">max number of bytes to read into array
			/// </param>
			/// <since> 1.3
			/// </since>
			public  override void  Write(System.Byte[] theBytes, int off, int len)
			{
				// Encoding suspended?
				if (suspendEncoding_Renamed_Field)
				{
					base.BaseStream.Write(SupportClass.ToByteArray(theBytes), off, len);
					return ;
				} // end if: supsended
				
				for (int i = 0; i < len; i++)
				{
					write(theBytes[off + i]);
				} // end for: each byte written
			} // end write
			
			
			
			/// <summary> Method added by PHIL. [Thanks, PHIL. -Rob]
			/// This pads the buffer without closing the stream.
			/// </summary>
			public virtual void  flushBase64()
			{
				if (position > 0)
				{
					if (encode)
					{
						sbyte[] temp_sbyteArray;
						temp_sbyteArray = VassalSharp.tools.Base64.encode3to4(b4, buffer, position, options);
						BaseStream.Write(SupportClass.ToByteArray(temp_sbyteArray), 0, temp_sbyteArray.Length);
						position = 0;
					}
					// end if: encoding
					else
					{
						throw new System.IO.IOException("Base64 input not properly padded.");
					} // end else: decoding
				} // end if: buffer partially full
			} // end flush
			
			
			/// <summary> Flushes and closes (I think, in the superclass) the stream.
			/// 
			/// </summary>
			/// <since> 1.3
			/// </since>
			public override void  Close()
			{
				// 1. Ensure that pending characters are written
				flushBase64();
				
				// 2. Actually close the stream
				// Base class both flushes and closes.
				base.Close();
				
				buffer = null;
				BaseStream = null;
			} // end close
			
			
			
			/// <summary> Suspends encoding of the stream.
			/// May be helpful if you need to embed a piece of
			/// base640-encoded data in a stream.
			/// 
			/// </summary>
			/// <since> 1.5.1
			/// </since>
			public virtual void  suspendEncoding()
			{
				flushBase64();
				this.suspendEncoding_Renamed_Field = true;
			} // end suspendEncoding
			
			
			/// <summary> Resumes encoding of the stream.
			/// May be helpful if you need to embed a piece of
			/// base640-encoded data in a stream.
			/// 
			/// </summary>
			/// <since> 1.5.1
			/// </since>
			public virtual void  resumeEncoding()
			{
				this.suspendEncoding_Renamed_Field = false;
			} // end resumeEncoding
		} // end inner class OutputStream
	} // end class Base64
}