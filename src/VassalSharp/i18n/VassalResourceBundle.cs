/*
* $Id$
*
* Copyright (c) 2007 by Rodney Kinney, Brent Easton
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
namespace VassalSharp.i18n
{
	
	/// <summary> VASSAL PropertyResourceBundle
	/// - Expose setParent()
	/// 
	/// </summary>
	/// <author>  Brent Easton
	/// 
	/// </author>
	public class VassalResourceBundle:System.Resources.ResourceManager
	{
		/// <summary> Standard constructor - read properties from a file
		/// 
		/// </summary>
		/// <param name="in">Input stream
		/// </param>
		/// <throws>  IOException </throws>
		//UPGRADE_ISSUE: Constructor 'java.util.PropertyResourceBundle.PropertyResourceBundle' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilPropertyResourceBundlePropertyResourceBundle_javaioInputStream'"
		public VassalResourceBundle(System.IO.Stream in_Renamed):base(in_Renamed)
		{
		}
		
		/// <summary> Expose the protected setParent() routine in the superclass as public.</summary>
		//UPGRADE_NOTE: The equivalent of method 'java.util.ResourceBundle.setParent' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public void  setParent(System.Resources.ResourceManager parent)
		{
			//UPGRADE_ISSUE: Method 'java.util.ResourceBundle.setParent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilResourceBundlesetParent_javautilResourceBundle'"
			base.setParent(parent);
		}
	}
}