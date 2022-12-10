using System;
namespace VassalSharp.build.module.noteswindow
{
	
	/*
	* $Id$
	*
	* Copyright (c) 2004 by Rodney Kinney
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
	
	/// <summary> A text message with an owner</summary>
	public class PrivateText
	{
		virtual public System.String Owner
		{
			get
			{
				return owner;
			}
			
		}
		virtual public System.String Text
		{
			get
			{
				return text;
			}
			
		}
		private System.String owner;
		private System.String text;
		
		public PrivateText(System.String owner, System.String text)
		{
			this.owner = owner;
			this.text = text;
		}
		
		/// <summary> Two PrivateTexts with the same owner are considered equal</summary>
		/// <param name="o">
		/// </param>
		/// <returns>
		/// </returns>
		public  override bool Equals(System.Object o)
		{
			if (this == o)
				return true;
			if (!(o is PrivateText))
				return false;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'privateText '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			PrivateText privateText = (PrivateText) o;
			
			if (!owner.Equals(privateText.owner))
				return false;
			
			return true;
		}
		
		public override int GetHashCode()
		{
			return owner.GetHashCode();
		}
	}
}