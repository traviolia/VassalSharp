/*
* $Id$
*
* Copyright (c) 2008-2009 by Joel Uckelman
*
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License (LGPL) as published by the Free Software Foundation.
*
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
using System;
namespace VassalSharp.tools.logging
{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class LogOutputStreamAdapter : LogListener
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'out '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.IO.Stream out_Renamed;
		
		public LogOutputStreamAdapter(System.IO.Stream out_Renamed)
		{
			if (out_Renamed == null)
				throw new System.NullReferenceException();
			this.out_Renamed = out_Renamed;
		}
		
		public virtual void  handle(LogEntry entry)
		{
			try
			{
				sbyte[] temp_sbyteArray;
				temp_sbyteArray = SupportClass.ToSByteArray(SupportClass.ToByteArray(entry.ToString()));
				out_Renamed.Write(SupportClass.ToByteArray(temp_sbyteArray), 0, temp_sbyteArray.Length);
				out_Renamed.WriteByte((System.Byte) '\n');
				out_Renamed.Flush();
			}
			catch (System.IO.IOException e)
			{
				// FIXME: What to do here????
			}
		}
	}
}