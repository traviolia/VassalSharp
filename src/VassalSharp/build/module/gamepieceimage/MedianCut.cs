using System;
namespace VassalSharp.build.module.gamepieceimage
{
	
	/// <summary>Converts an RGB image to 8-bit index color using Heckbert's median-cut
	/// color quantization algorithm. Based on median.c by Anton Kruger from the
	/// September, 1994 issue of Dr. Dobbs Journal.
	/// </summary>
	/// <deprecated>
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class MedianCut
	{
		virtual internal int ColorCount
		{
			get
			{
				int count = 0;
				for (int i = 0; i < HSIZE; i++)
					if (hist[i] > 0)
						count++;
				return count;
			}
			
		}
		virtual internal System.Drawing.Color ModalColor
		{
			get
			{
				int max = 0;
				int c = 0;
				for (int i = 0; i < HSIZE; i++)
					if (hist[i] > max)
					{
						max = hist[i];
						c = i;
					}
				return System.Drawing.Color.FromArgb(red(c), green(c), blue(c));
			}
			
		}
		
		internal const int MAXCOLORS = 256; // maximum # of output colors
		internal const int HSIZE = 32768; // size of image histogram
		private int[] hist; // RGB histogram and reverse color lookup table
		private int[] histPtr; // points to colors in "hist"
		private Cube[] list; // list of cubes
		private int[] pixels32;
		private int width, height;
		private SupportClass.IndexedColorArray cm;
		
		public MedianCut(System.Drawing.Bitmap image)
		{
			int color16;
			
			width = image.Width;
			height = image.Height;
			//UPGRADE_ISSUE: Method 'java.awt.image.BufferedImage.getRGB' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImagegetRGB_int_int_int_int_int[]_int_int'"
			pixels32 = image.getRGB(0, 0, width, height, null, 0, width);
			
			//build 32x32x32 RGB histogram
			hist = new int[HSIZE];
			for (int i = 0; i < width * height; i++)
			{
				color16 = rgb(pixels32[i]);
				hist[color16]++;
			}
		}
		
		
		// Convert from 24-bit to 15-bit color
		private int rgb(int c)
		{
			int r = (c & 0xf80000) >> 19;
			int g = (c & 0xf800) >> 6;
			int b = (c & 0xf8) << 7;
			return b | g | r;
		}
		
		// Get red component of a 15-bit color
		private int red(int x)
		{
			return (x & 31) << 3;
		}
		
		// Get green component of a 15-bit color
		private int green(int x)
		{
			return (x >> 2) & 0xf8;
		}
		
		// Get blue component of a 15-bit color
		private int blue(int x)
		{
			return (x >> 7) & 0xf8;
		}
		
		
		/// <summary>Uses Heckbert's median-cut algorithm to divide the color space defined by
		/// "hist" into "maxcubes" cubes. The centroids (average value) of each cube
		/// are are used to create a color table. "hist" is then updated to function
		/// as an inverse color map that is used to generate an 8-bit image. 
		/// </summary>
		public virtual System.Drawing.Bitmap convert(int maxcubes)
		{
			return convertToByte(maxcubes);
		}
		
		public virtual SupportClass.IndexedColorArray buildColorModel(int maxcubes)
		{
			convertToByte(maxcubes);
			return cm;
		}
		
		/// <summary>This is a version of convert that returns a ByteProcessor. </summary>
		public virtual System.Drawing.Bitmap convertToByte(int maxcubes)
		{
			int lr, lg, lb;
			int i, median, color;
			int count;
			int k, level, ncubes, splitpos;
			int longdim = 0; //longest dimension of cube
			Cube cube, cubeA, cubeB;
			
			// Create initial cube
			list = new Cube[MAXCOLORS];
			histPtr = new int[HSIZE];
			ncubes = 0;
			cube = new Cube();
			for (i = 0, color = 0; i <= HSIZE - 1; i++)
			{
				if (hist[i] != 0)
				{
					histPtr[color++] = i;
					cube.count = cube.count + hist[i];
				}
			}
			cube.lower = 0; cube.upper = color - 1;
			cube.level = 0;
			Shrink(cube);
			list[ncubes++] = cube;
			
			//Main loop
			while (ncubes < maxcubes)
			{
				
				// Search the list of cubes for next cube to split, the lowest level cube
				level = 255; splitpos = - 1;
				for (k = 0; k <= ncubes - 1; k++)
				{
					if (list[k].lower == list[k].upper)
					{
					}
					// single color; cannot be split
					else if (list[k].level < level)
					{
						level = list[k].level;
						splitpos = k;
					}
				}
				if (splitpos == - 1)
				// no more cubes to split
					break;
				
				// Find longest dimension of this cube
				cube = list[splitpos];
				lr = cube.rmax - cube.rmin;
				lg = cube.gmax - cube.gmin;
				lb = cube.bmax - cube.bmin;
				if (lr >= lg && lr >= lb)
					longdim = 0;
				if (lg >= lr && lg >= lb)
					longdim = 1;
				if (lb >= lr && lb >= lg)
					longdim = 2;
				
				// Sort along "longdim"
				reorderColors(histPtr, cube.lower, cube.upper, longdim);
				quickSort(histPtr, cube.lower, cube.upper);
				restoreColorOrder(histPtr, cube.lower, cube.upper, longdim);
				
				// Find median
				count = 0;
				for (i = cube.lower; i <= cube.upper - 1; i++)
				{
					if (count >= cube.count / 2)
						break;
					color = histPtr[i];
					count = count + hist[color];
				}
				median = i;
				
				// Now split "cube" at the median and add the two new
				// cubes to the list of cubes.
				cubeA = new Cube();
				cubeA.lower = cube.lower;
				cubeA.upper = median - 1;
				cubeA.count = count;
				cubeA.level = cube.level + 1;
				Shrink(cubeA);
				list[splitpos] = cubeA; // add in old slot
				
				cubeB = new Cube();
				cubeB.lower = median;
				cubeB.upper = cube.upper;
				cubeB.count = cube.count - count;
				cubeB.level = cube.level + 1;
				Shrink(cubeB);
				list[ncubes++] = cubeB; // add in new slot */
			}
			
			// We have enough cubes, or we have split all we can. Now
			// compute the color map, the inverse color map, and return
			// an 8-bit image.
			makeInverseMap(hist, ncubes);
			return makeImage();
		}
		
		internal virtual void  Shrink(Cube cube)
		{
			// Encloses "cube" with a tight-fitting cube by updating the
			// (rmin,gmin,bmin) and (rmax,gmax,bmax) members of "cube".
			
			int r, g, b;
			int color;
			int rmin, rmax, gmin, gmax, bmin, bmax;
			
			rmin = 255; rmax = 0;
			gmin = 255; gmax = 0;
			bmin = 255; bmax = 0;
			for (int i = cube.lower; i <= cube.upper; i++)
			{
				color = histPtr[i];
				r = red(color);
				g = green(color);
				b = blue(color);
				if (r > rmax)
					rmax = r;
				if (r < rmin)
					rmin = r;
				if (g > gmax)
					gmax = g;
				if (g < gmin)
					gmin = g;
				if (b > bmax)
					bmax = b;
				if (b < bmin)
					bmin = b;
			}
			cube.rmin = rmin; cube.rmax = rmax;
			cube.gmin = gmin; cube.gmax = gmax;
			cube.bmin = bmin; cube.bmax = bmax;
		}
		
		
		internal virtual void  makeInverseMap(int[] hist, int ncubes)
		{
			// For each cube in the list of cubes, computes the centroid
			// (average value) of the colors enclosed by that cube, and
			// then loads the centroids in the color map. Next loads
			// "hist" with indices into the color map
			
			int r, g, b;
			int color;
			float rsum, gsum, bsum;
			Cube cube;
			sbyte[] rLUT = new sbyte[256];
			sbyte[] gLUT = new sbyte[256];
			sbyte[] bLUT = new sbyte[256];
			
			for (int k = 0; k <= ncubes - 1; k++)
			{
				cube = list[k];
				rsum = gsum = bsum = (float) 0.0;
				for (int i = cube.lower; i <= cube.upper; i++)
				{
					color = histPtr[i];
					r = red(color);
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					rsum += (float) r * (float) hist[color];
					g = green(color);
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					gsum += (float) g * (float) hist[color];
					b = blue(color);
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					bsum += (float) b * (float) hist[color];
				}
				
				// Update the color map
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				r = (int) (rsum / (float) cube.count);
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				g = (int) (gsum / (float) cube.count);
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				b = (int) (bsum / (float) cube.count);
				if (r == 248 && g == 248 && b == 248)
					r = g = b = 255; // Restore white (255,255,255)
				rLUT[k] = (sbyte) r;
				gLUT[k] = (sbyte) g;
				bLUT[k] = (sbyte) b;
			}
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.awt.image.IndexColorModel' and 'SupportClass.IndexedColorArray' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			cm = new SupportClass.IndexedColorArray(8, ncubes, SupportClass.ToByteArray(rLUT), SupportClass.ToByteArray(gLUT), SupportClass.ToByteArray(bLUT));
			
			// For each color in each cube, load the corre-
			// sponding slot in "hist" with the centroid of the cube.
			for (int k = 0; k <= ncubes - 1; k++)
			{
				cube = list[k];
				for (int i = cube.lower; i <= cube.upper; i++)
				{
					color = histPtr[i];
					hist[color] = k;
				}
			}
		}
		
		
		internal virtual void  reorderColors(int[] a, int lo, int hi, int longDim)
		{
			// Change the ordering of the 5-bit colors in each word of int[]
			// so we can sort on the 'longDim' color
			
			int c, r, g, b;
			switch (longDim)
			{
				
				case 0:  //red
					for (int i = lo; i <= hi; i++)
					{
						c = a[i];
						r = c & 31;
						a[i] = (r << 10) | (c >> 5);
					}
					break;
				
				case 1:  //green
					for (int i = lo; i <= hi; i++)
					{
						c = a[i];
						r = c & 31;
						g = (c >> 5) & 31;
						b = c >> 10;
						a[i] = (g << 10) | (b << 5) | r;
					}
					break;
				
				case 2:  //blue; already in the needed order
					break;
				}
		}
		
		
		internal virtual void  restoreColorOrder(int[] a, int lo, int hi, int longDim)
		{
			// Restore the 5-bit colors to the original order
			
			int c, r, g, b;
			switch (longDim)
			{
				
				case 0:  //red
					for (int i = lo; i <= hi; i++)
					{
						c = a[i];
						r = c >> 10;
						a[i] = ((c & 1023) << 5) | r;
					}
					break;
				
				case 1:  //green
					for (int i = lo; i <= hi; i++)
					{
						c = a[i];
						r = c & 31;
						g = c >> 10;
						b = (c >> 5) & 31;
						a[i] = (b << 10) | (g << 5) | r;
					}
					break;
				
				case 2:  //blue
					break;
				}
		}
		
		
		internal virtual void  quickSort(int[] a, int lo0, int hi0)
		{
			// Based on the QuickSort method by James Gosling from Sun's SortDemo applet
			
			int lo = lo0;
			int hi = hi0;
			int mid, t;
			
			if (hi0 > lo0)
			{
				mid = a[(lo0 + hi0) / 2];
				while (lo <= hi)
				{
					while ((lo < hi0) && (a[lo] < mid))
						++lo;
					while ((hi > lo0) && (a[hi] > mid))
						--hi;
					if (lo <= hi)
					{
						t = a[lo];
						a[lo] = a[hi];
						a[hi] = t;
						++lo;
						--hi;
					}
				}
				if (lo0 < hi)
					quickSort(a, lo0, hi);
				if (lo < hi0)
					quickSort(a, lo, hi0);
			}
		}
		
		
		internal virtual System.Drawing.Bitmap makeImage()
		{
			// Generate 8-bit image
			
			//Image img8;
			sbyte[] pixels8;
			int color16;
			
			pixels8 = new sbyte[width * height];
			for (int i = 0; i < width * height; i++)
			{
				color16 = rgb(pixels32[i]);
				pixels8[i] = (sbyte) hist[color16];
			}
			
			//UPGRADE_TODO: Class 'java.awt.image.SampleModel' was converted to 'System.Drawing.Bitmap' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageSampleModel'"
			//UPGRADE_TODO: Class 'java.awt.image.PixelInterleavedSampleModel' was converted to 'System.Drawing.Bitmap' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimagePixelInterleavedSampleModel'"
			System.Drawing.Bitmap sampleModel = new System.Drawing.Bitmap(width, height);
			System.Object[] tempObjectArray;
			tempObjectArray = new System.Object[pixels8.Length];
			System.Array.Copy(pixels8, tempObjectArray, tempObjectArray.Length);
			System.Collections.ArrayList Buffer = new System.Collections.ArrayList(tempObjectArray);
			//UPGRADE_TODO: Class 'java.awt.image.WritableRaster' was converted to 'System.Drawing.Bitmap' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageWritableRaster'"
			System.IO.MemoryStream tempDataBuffer;
			tempDataBuffer = new System.IO.MemoryStream();
			sampleModel.Save(tempDataBuffer, System.Drawing.Imaging.ImageFormat.Bmp);
			//UPGRADE_TODO: Method 'java.awt.image.Raster.createWritableRaster' was converted to 'System.Drawing.Bitmap' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageRastercreateWritableRaster_javaawtimageSampleModel_javaawtimageDataBuffer_javaawtPoint'"
			System.Drawing.Bitmap raster = new System.Drawing.Bitmap(tempDataBuffer);
			
			//UPGRADE_ISSUE: Constructor 'java.awt.image.BufferedImage.BufferedImage' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImageBufferedImage_javaawtimageColorModel_javaawtimageWritableRaster_boolean_javautilHashtable'"
			return new BufferedImage(cm, raster, false, null);
		}
	} //class MedianCut
	
	
	class Cube
	{
		// structure for a cube in color space
		internal int lower; // one corner's index in histogram
		internal int upper; // another corner's index in histogram
		internal int count; // cube's histogram count
		internal int level; // cube's level
		internal int rmin, rmax;
		internal int gmin, gmax;
		internal int bmin, bmax;
		
		internal Cube()
		{
			count = 0;
		}
		
		public override System.String ToString()
		{
			System.String s = "lower=" + lower + " upper=" + upper; //$NON-NLS-1$ //$NON-NLS-2$
			s = s + " count=" + count + " level=" + level; //$NON-NLS-1$ //$NON-NLS-2$
			s = s + " rmin=" + rmin + " rmax=" + rmax; //$NON-NLS-1$ //$NON-NLS-2$
			s = s + " gmin=" + gmin + " gmax=" + gmax; //$NON-NLS-1$ //$NON-NLS-2$
			s = s + " bmin=" + bmin + " bmax=" + bmax; //$NON-NLS-1$ //$NON-NLS-2$
			return s;
		}
	}
}