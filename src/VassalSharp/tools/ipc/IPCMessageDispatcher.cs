
using System;
using System.Collections.Concurrent;

using IOUtils = VassalSharp.tools.io.IOUtils;

namespace VassalSharp.tools.ipc
{
	
	public class IPCMessageDispatcher : IThreadRunnable
	{
		public IPCMessageDispatcher()
		{
		}

		protected BlockingCollection<IPCMessage> queue;
		protected internal System.IO.BinaryWriter out_Renamed;
		
		public IPCMessageDispatcher(BlockingCollection< IPCMessage > queue, System.IO.BinaryWriter out_Renamed)
		{
			this.queue = queue;
			this.out_Renamed = out_Renamed;
		}

		public virtual void  Run()
		{
			IPCMessage msg;
			
			try
			{
				do 
				{
					msg = queue.Take();
					SupportClass.Serialize(out_Renamed, msg);
					out_Renamed.Flush();
				}
				while (!(msg is Fin));
				
				out_Renamed.Close();
			}
			catch (System.IO.IOException e)
			{
				// FIXME
				SupportClass.WriteStackTrace(e, Console.Error);
			}
			catch (System.Threading.ThreadInterruptedException e)
			{
				// FIXME
				SupportClass.WriteStackTrace(e, Console.Error);
			}
			finally
			{
				IOUtils.closeQuietly(out_Renamed);
			}
		}
	}
}