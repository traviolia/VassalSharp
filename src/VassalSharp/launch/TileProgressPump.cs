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
namespace VassalSharp.launch
{
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import static VassalSharp.launch.TileProgressPumpStateMachine.DONE;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import static VassalSharp.launch.TileProgressPumpStateMachine.INIT;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.IOException;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.InputStream;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.tools.concurrent.listener.EventListener;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.tools.image.tilecache.ZipFileImageTiler;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.tools.io.InputStreamPump;
	
	/// <summary> A stream pump which receives input from {@link ZipFileImageTiler}.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	class TileProgressPump : InputStreamPump
	{
		public TileProgressPump()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			sm = new TileProgressPumpStateMachine(nameListener, progListener);
			this.ioexListener = ioexListener;
		}
		/// <summary>{@inheritDoc} </summary>
		virtual public InputStream InputStream
		{
			set
			{
				if (running)
					throw new System.NotSupportedException();
				
				this.in_Renamed = value;
			}
			
		}
		
		protected internal InputStream in_Renamed;
		
		protected internal volatile bool running = false;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'sm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal TileProgressPumpStateMachine sm;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected final EventListener < IOException > ioexListener;
		
		/// <summary> Create a <code>TileProgressPump</code>.
		/// 
		/// </summary>
		/// <param name="nameListener">the listener for new filename events
		/// </param>
		/// <param name="progListener">the listener for progress events
		/// </param>
		/// <param name="ioexListener">the listener for {@link IOException}s
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public TileProgressPump(EventListener < String > nameListener, 
		EventListener < Integer > progListener, 
		EventListener < IOException > ioexListener)
		
		/// <summary>{@inheritDoc} </summary>
		public virtual void  run()
		{
			running = true;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'buf '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] buf = new sbyte[256];
			//UPGRADE_NOTE: Final was removed from the declaration of 'sb '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder sb = new StringBuilder();
			
			int state = INIT;
			int count;
			
			try
			{
				while ((count = in_Renamed.read(buf)) != - 1)
				{
					state = sm.run(state, buf, 0, count, sb);
				}
				
				if (state != INIT && state != DONE)
				{
					throw new IOException("Stream ended before DONE");
				}
			}
			catch (IOException e)
			{
				// Tell someone who cares.
				ioexListener.receive(this, e);
			}
		}
	}
}