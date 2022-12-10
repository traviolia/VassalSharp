/*
* $Id$
*
* Copyright (c) 2007-2008 by Joel Uckelman
*
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License (LGPL) as published by the Free Software Foundation.
*
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
using System;
namespace VassalSharp.tools.image
{
	
	/// <summary> Adds additional hints to {@link RenderingHints}.
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	/// <deprecated> All scaling is now done via the high-quality scaler.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class RenderingClues:System.Collections.Hashtable
	{
		//UPGRADE_TODO: Class 'java.awt.RenderingHints.Key' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		internal class AnonymousClassKey:Key
		{
			//UPGRADE_TODO: Constructor 'java.awt.RenderingHints.Key.Key' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			internal AnonymousClassKey(int Param1):base(Param1)
			{
			}
			public bool isCompatibleValue(System.Object val)
			{
				return val == VassalSharp.tools.image.RenderingClues.VALUE_INTERPOLATION_LANCZOS_MITCHELL || val == (System.Object) System.Drawing.Drawing2D.InterpolationMode.Bicubic || val == (System.Object) System.Drawing.Drawing2D.InterpolationMode.Bilinear || val == (System.Object) System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			}
		}
		internal class AnonymousClassObject:System.Object
		{
		}
		private void  InitBlock()
		{
			base(init);
		}
		
		private const int INTKEY_EXT_INTERPOLATION = unchecked((int) 0xDEADBEEF);
		
		/// <summary>Extended interpolation hint key. </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'KEY_EXT_INTERPOLATION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'java.awt.RenderingHints.Key' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		//UPGRADE_NOTE: The initialization of  'KEY_EXT_INTERPOLATION' was moved to static method 'VassalSharp.tools.image.RenderingClues'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		public static readonly Key KEY_EXT_INTERPOLATION;
		
		/// <summary> Interpolation hint value -- Lanczos interpolation is used for
		/// downscaling, while Mitchell interpolation is used for upscaling.
		/// </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'VALUE_INTERPOLATION_LANCZOS_MITCHELL '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'VALUE_INTERPOLATION_LANCZOS_MITCHELL' was moved to static method 'VassalSharp.tools.image.RenderingClues'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		public static readonly System.Object VALUE_INTERPOLATION_LANCZOS_MITCHELL;
		
		/// <summary> Constructs an empty collection of <code>RenderingClues</code>.</summary>
		public RenderingClues():base(null)
		{
			InitBlock();
		}
		
		/// <summary>  Constructs a collection of <code>RenderingClues</code> from
		/// the given <code>Map</code>.
		/// 
		/// </summary>
		/// <param name="init">a map of key/value pairs to initialize the hints
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public RenderingClues(Map < Key, ? > init)
		static RenderingClues()
		{
			KEY_EXT_INTERPOLATION = new AnonymousClassKey(INTKEY_EXT_INTERPOLATION);
			VALUE_INTERPOLATION_LANCZOS_MITCHELL = new AnonymousClassObject();
		}
	}
}