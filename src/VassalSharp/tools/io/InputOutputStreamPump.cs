/*
* $Id$
*
* Copyright (c) 2008-2010 by Joel Uckelman
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
using DummyEventListener = VassalSharp.tools.concurrent.listener.DummyEventListener;
//UPGRADE_TODO: The type 'VassalSharp.tools.concurrent.listener.EventListener' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using EventListener = VassalSharp.tools.concurrent.listener.EventListener;
namespace VassalSharp.tools.io
{
	
	/// <summary> Pumps an {@link InputStream} to an {@link OutputStream}.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.2.0
	/// </since>
	public class InputOutputStreamPump : InputStreamPump, OutputStreamPump
	{
		private void  InitBlock()
		{
			this(null, null, ioexListener);
			this.in_Renamed = in_Renamed;
			this.out_Renamed = out_Renamed;
			this.ioexListener = ioexListener;
		}
		/// <summary> Sets the input stream.
		/// 
		/// </summary>
		/// <param name="in">the input stream
		/// </param>
		/// <throws>  UnsupportedOperationException if called after the pump is started </throws>
		virtual public System.IO.Stream InputStream
		{
			set
			{
				if (running)
					throw new System.NotSupportedException();
				
				this.in_Renamed = value;
			}
			
		}
		/// <summary> Sets the output stream.
		/// 
		/// </summary>
		/// <param name="out">the output stream
		/// </param>
		/// <throws>  UnsupportedOperationException if called after the pump is started </throws>
		virtual public System.IO.Stream OutputStream
		{
			set
			{
				if (running)
					throw new System.NotSupportedException();
				
				this.out_Renamed = value;
			}
			
		}
		protected internal System.IO.Stream in_Renamed;
		protected internal System.IO.Stream out_Renamed;
		
		protected internal volatile bool running = false;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected final EventListener < IOException > ioexListener;
		
		/// <summary> Creates an <code>InputOutputStreamPump</code>.</summary>
		public InputOutputStreamPump()
		{
			InitBlock();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			this(null, null, new DummyEventListener < IOException >());
		}
		
		/// <summary> Creates an <code>InputOutputStreamPump</code>.
		/// 
		/// </summary>
		/// <param name="ioexListener">the exception listener
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public InputOutputStreamPump(EventListener < IOException > ioexListener)
		
		/// <summary> Creates an <code>InputOutputStreamPump</code>.
		/// 
		/// </summary>
		/// <param name="in">the input stream
		/// </param>
		/// <param name="out">the output stream
		/// </param>
		public InputOutputStreamPump(System.IO.Stream in_Renamed, System.IO.Stream out_Renamed)
		{
			InitBlock();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			this(in, out, new DummyEventListener < IOException >());
		}
		
		/// <summary> Creates an <code>InputOutputStreamPump</code>.
		/// 
		/// </summary>
		/// <param name="in">the input stream
		/// </param>
		/// <param name="out">the output stream
		/// </param>
		/// <param name="ioexListener">the exception listener
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public InputOutputStreamPump(InputStream in, OutputStream out, 
		EventListener < IOException > ioexListener)
		
		/// <summary>{@inheritDoc} </summary>
		public override void  Run()
		{
			running = true;
			
			try
			{
				IOUtils.copy(in_Renamed, out_Renamed);
			}
			catch (System.IO.IOException e)
			{
				// Tell someone who cares.
				ioexListener.receive(this, e);
			}
		}
	}
}