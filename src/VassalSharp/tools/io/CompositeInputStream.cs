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
namespace VassalSharp.tools.io
{
	
	/// <summary> An {@link InputStream} which concatenates other <code>InputStreams</code>.
	/// As with {@link SequenceInputStream}, the first stream is read until EOF,
	/// followed by the second, and so on, until all input streams are exhausted.
	/// 
	/// Note: {@link SequenceInputStream#available()} returns only the number of
	/// bytes available from the current stream in the sequence, which makes it
	/// difficult to efficiently allocate a buffer into which to read the bytes.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	/// <seealso cref="SequenceInputStream">
	/// </seealso>
	public class CompositeInputStream:System.IO.Stream
	{
		public CompositeInputStream()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			queue = new LinkedList < InputStream >(streams);
			in_Renamed = queue.poll();
			this(Arrays.asList(streams));
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected final LinkedList < InputStream > queue;
		
		protected internal System.IO.Stream in_Renamed;
		
		/// <summary> Creates a <code>CompositeInputStream</code> from the given sequence
		/// of <code>InputStream</code>s.
		/// 
		/// </summary>
		/// <param name="streams">the <code>InputStream</code>s to be concatenated
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public CompositeInputStream(List < InputStream > streams)
		
		/// <summary> Creates a <code>CompositeInputStream</code> from the given sequence
		/// of <code>InputStream</code>s.
		/// 
		/// </summary>
		/// <param name="streams">the <code>InputStream</code>s to be concatenated
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public CompositeInputStream(InputStream...streams)
		
		protected internal virtual void  nextStream()
		{
			if (in_Renamed != null)
			{
				in_Renamed.Close();
				in_Renamed = queue.poll();
			}
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_NOTE: The equivalent of method 'java.io.InputStream.available' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public int available()
		{
			long available;
			available = in_Renamed.Length - in_Renamed.Position;
			int bytes = in_Renamed != null?((in_Renamed is VassalSharp.tools.io.RereadableInputStream || in_Renamed is VassalSharp.tools.io.CompositeInputStream || in_Renamed is VassalSharp.tools.image.PNGChunkSkipInputStream)?(int) SupportClass.InvokeMethodAsVirtual(in_Renamed, "available", new System.Object[]{}):(int) available):0;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(InputStream ch: queue)
			{
				bytes += Math.max(ch.available(), 0);
			}
			return bytes;
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override int ReadByte()
		{
			if (in_Renamed == null)
				return - 1;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int c = in_Renamed.ReadByte();
			if (c != - 1)
				return c;
			
			nextStream();
			return ReadByte();
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_NOTE: The equivalent of method 'java.io.InputStream.read' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public int read(sbyte[] b, int off, int len)
		{
			if (in_Renamed == null)
				return - 1;
			if (len == 0)
				return 0;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'count '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int count = (in_Renamed is VassalSharp.tools.io.RereadableInputStream || in_Renamed is VassalSharp.tools.io.CompositeInputStream || in_Renamed is VassalSharp.tools.image.PNGChunkSkipInputStream)?(int) SupportClass.InvokeMethodAsVirtual(in_Renamed, "read", new System.Object[]{b, off, len}):SupportClass.ReadInput(in_Renamed, b, off, len);
			if (count > 0)
				return count;
			
			nextStream();
			return read(b, off, len);
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override void  Close()
		{
			while (in_Renamed != null)
				nextStream();
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override void  Flush()
		{
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override System.Int64 Seek(System.Int64 offset, System.IO.SeekOrigin origin)
		{
			return 0;
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override void  SetLength(System.Int64 value)
		{
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override System.Int32 Read(System.Byte[] buffer, System.Int32 offset, System.Int32 count)
		{
			return 0;
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override void  Write(System.Byte[] buffer, System.Int32 offset, System.Int32 count)
		{
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override System.Boolean CanRead
		{
			get
			{
				return false;
			}
			
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override System.Boolean CanSeek
		{
			get
			{
				return false;
			}
			
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override System.Boolean CanWrite
		{
			get
			{
				return false;
			}
			
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override System.Int64 Length
		{
			get
			{
				return 0;
			}
			
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override System.Int64 Position
		{
			get
			{
				return 0;
			}
			
			set
			{
			}
			
		}
	}
}