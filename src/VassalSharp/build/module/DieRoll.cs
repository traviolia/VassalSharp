/*
* $Id$
*
* Copyright (c) 2000-2003 by Brent Easton
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
namespace VassalSharp.build.module
{
	
	
	/// <author>  Brent Easton
	/// 
	/// Describes a single roll of one or more identical dice.
	/// For use with internet dice rollers
	/// </author>
	public class DieRoll
	{
		virtual public System.String Description
		{
			get
			{
				return description;
			}
			
			set
			{
				this.description = value;
			}
			
		}
		virtual public int NumDice
		{
			get
			{
				return result.Length;
			}
			
			set
			{
				result = new int[value];
			}
			
		}
		virtual public int NumSides
		{
			get
			{
				return numSides;
			}
			
			set
			{
				this.numSides = value;
			}
			
		}
		virtual public int Plus
		{
			get
			{
				return plus;
			}
			
			set
			{
				this.plus = value;
			}
			
		}
		virtual public bool ReportTotal
		{
			get
			{
				return reportTotal;
			}
			
			set
			{
				this.reportTotal = value;
			}
			
		}
		
		private System.String description = ""; //$NON-NLS-1$
		private int numSides;
		private int plus;
		private bool reportTotal;
		private int[] result;
		
		public DieRoll(System.String d, int dice, int sides, int add, bool r)
		{
			Description = d;
			NumDice = dice;
			NumSides = sides;
			Plus = add;
			ReportTotal = r;
			result = new int[dice];
		}
		
		
		public DieRoll(System.String d, int dice, int sides, int add):this(d, dice, sides, add, false)
		{
		}
		
		public DieRoll(System.String d, int dice, int sides):this(d, dice, sides, 0)
		{
		}
		
		public virtual int getResult(int pos)
		{
			return result[pos];
		}
		
		public virtual void  setResult(int pos, int res)
		{
			result[pos] = res;
		}
	}
}