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
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
using System;
//UPGRADE_TODO: The type 'VassalSharp.tools.concurrent.listener.EventListener' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using EventListener = VassalSharp.tools.concurrent.listener.EventListener;
using ZipFileImageTiler = VassalSharp.tools.image.tilecache.ZipFileImageTiler;
namespace VassalSharp.launch
{
	
	/// <summary> A state machine for parsing the output of {@link ZipFileImageTiler}.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	class TileProgressPumpStateMachine
	{
		public TileProgressPumpStateMachine()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			this.nameListener = nameListener;
			this.progListener = progListener;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected final EventListener < String > nameListener;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected final EventListener < Integer > progListener;
		
		/// <summary> Creates a <code>TileProgressPumpStateMachine</code>.
		/// 
		/// </summary>
		/// <param name="nameListener">the listener for new filename events
		/// </param>
		/// <param name="progListener">the listener for progress events
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public TileProgressPumpStateMachine(EventListener < String > nameListener, 
		EventListener < Integer > progListener)
		
		public const int INIT = 0;
		public const int NAME = 1;
		public const int NAME_LF = 2;
		public const int DOTS = 3;
		public const int DOTS_LF = 4;
		public const int DONE = 5;
		
		protected internal virtual void  appendName(StringBuilder sb, sbyte[] buf, int beg, int end)
		{
			sb.append(new System.String(SupportClass.ToCharArray(buf), beg, end - beg));
		}
		
		protected internal virtual bool hasName(StringBuilder sb)
		{
			return sb.length() > 0;
		}
		
		protected internal virtual void  sendName(StringBuilder sb)
		{
			nameListener.receive(this, sb.toString());
			sb.setLength(0);
		}
		
		protected internal virtual void  sendProgress(int prog)
		{
			progListener.receive(this, prog);
		}
		
		protected internal virtual int[] runName(sbyte[] buf, int beg, int end, StringBuilder sb)
		{
			// look for end of line
			for (int pos = beg; pos < end; ++pos)
			{
				if (buf[pos] == '\r' || buf[pos] == '\n')
				{
					// found the end of line
					
					// terminate if the buffer is empty
					if (pos == beg && !hasName(sb))
					{
						return new int[]{DONE, end};
					}
					
					// otherwise, send the buffer up to this position as the filename
					appendName(sb, buf, beg, pos);
					sendName(sb);
					
					// found a carriage return
					if (buf[pos] == '\r')
					{
						// now look for a linefeed
						return new int[]{NAME_LF, pos + 1};
					}
					else
					{
						// now look for dots
						return new int[]{DOTS, pos + 1};
					}
					
					// found a regular character, keep looking
				}
			}
			
			// exhausted the buffer without finding end of line
			
			// store the pratial filename we've read
			appendName(sb, buf, beg, end);
			
			// continue looking for end of line
			return new int[]{NAME, end};
		}
		
		protected internal virtual int[] runNameLF(sbyte[] buf, int beg, int end, StringBuilder sb)
		{
			// look for a linefeed
			switch (buf[beg])
			{
				
				case (sbyte) '\n': 
					// found a linefeed
					// now look for dots
					return new int[]{DOTS, beg + 1};
				
				
				default: 
					// found something else, protocol violation
					throw new System.SystemException("found '" + buf[beg] + "', expecting '\\n'");
				
			}
		}
		
		protected internal virtual int[] runDots(sbyte[] buf, int beg, int end, StringBuilder sb)
		{
			// look for end of line
			for (int pos = beg; pos < end; ++pos)
			{
				switch (buf[pos])
				{
					
					case (sbyte) '\r': 
					case (sbyte) '\n': 
						// found the end of line
						
						// send the buffer up to this position as the progress
						sendProgress(pos - beg);
						
						// found a carriage return
						if (buf[pos] == '\r')
						{
							// now look for a linefeed
							return new int[]{DOTS_LF, pos + 1};
						}
						else
						{
							// now look for filename
							return new int[]{NAME, pos + 1};
						}
						goto case (sbyte) '.';
					
					
					case (sbyte) '.': 
						// found a progress dot, keep looking
						break;
					
					
					default: 
						// found somethine else, protocol violation
						throw new System.SystemException("found '" + buf[pos] + "', expecting '.'");
					
				}
			}
			
			// exhausted the buffer without finding end of line
			
			// send the progress to this point
			sendProgress(end - beg);
			
			// continue looking for end of line
			return new int[]{DOTS, end};
		}
		
		protected internal virtual int[] runDotsLF(sbyte[] buf, int beg, int end, StringBuilder sb)
		{
			// look for a linefeed
			switch (buf[beg])
			{
				
				case (sbyte) '\n': 
					// found a linefeed
					// now look for filename
					return new int[]{NAME, beg + 1};
				
				
				default: 
					// found something else, protocol violation
					throw new System.SystemException("found '" + buf[beg] + "', expecting '\\n'");
				
			}
		}
		
		/// <summary> Run the state machine to the end of the buffer.
		/// 
		/// </summary>
		/// <param name="state">the current state
		/// </param>
		/// <param name="buf">the byte buffer to read
		/// </param>
		/// <param name="beg">the beginning position in the buffer (inclusive)
		/// </param>
		/// <param name="end">the the ending position in the buffer (exclusive)
		/// </param>
		/// <param name="sb">the string builder for holding name
		/// </param>
		public virtual int run(int state, sbyte[] buf, int beg, int end, StringBuilder sb)
		{
			if (state == DONE)
			{
				throw new System.ArgumentException("DONE is terminal");
			}
			
			if (state == INIT)
			{
				state = NAME;
			}
			
			while (beg < end)
			{
				int[] result;
				
				switch (state)
				{
					
					case NAME:  result = runName(buf, beg, end, sb); break;
					
					case NAME_LF:  result = runNameLF(buf, beg, end, sb); break;
					
					case DOTS:  result = runDots(buf, beg, end, sb); break;
					
					case DOTS_LF:  result = runDotsLF(buf, beg, end, sb); break;
					
					
					default: 
						// should never happen
						throw new System.SystemException("state == " + state);
					
				}
				
				state = result[0];
				beg = result[1];
			}
			
			return state;
		}
	}
}