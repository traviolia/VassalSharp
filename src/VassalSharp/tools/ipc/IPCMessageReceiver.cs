using System;
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
using MultiEventListenerSupport = VassalSharp.tools.concurrent.listener.MultiEventListenerSupport;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.tools.ipc
{
	
	class IPCMessageReceiver : IThreadRunnable
	{
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.tools.ipc.IPCMessageReceiver'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'in '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Interface 'java.io.ObjectInput' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectInput'"
		protected internal System.IO.BinaryReader in_Renamed;
		//UPGRADE_NOTE: Final was removed from the declaration of 'lsup '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal MultiEventListenerSupport lsup;
		
		//UPGRADE_TODO: Interface 'java.io.ObjectInput' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectInput'"
		public IPCMessageReceiver(System.IO.BinaryReader in_Renamed, MultiEventListenerSupport lsup)
		{
			this.in_Renamed = in_Renamed;
			this.lsup = lsup;
		}
		
		public virtual void  Run()
		{
			IPCMessage msg;
			
			try
			{
				do 
				{
					//UPGRADE_ISSUE: Method 'java.io.ObjectInput.readObject' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioObjectInputreadObject'"
					msg = (IPCMessage) in_Renamed.readObject();
					
					//System.err.println("received " + msg);
					
					lsup.notify(msg);
				}
				while (!(msg is Fin));
				
				in_Renamed.Close();
			}
			catch (System.InvalidCastException e)
			{
				throw new IllegalStateException(e);
			}
			//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			catch (System.Exception e)
			{
				throw new IllegalStateException(e);
			}
			catch (System.Runtime.Serialization.SerializationException e)
			{
				throw new IllegalStateException(e);
			}
			catch (System.IO.IOException e)
			{
				throw new IllegalStateException(e);
			}
			//UPGRADE_ISSUE: Class 'java.io.OptionalDataException' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioOptionalDataException'"
			catch (OptionalDataException e)
			{
				throw new IllegalStateException(e);
			}
			catch (System.IO.IOException e)
			{
				// FIXME: should communicate this outward somehow
				logger.error("", e);
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
		}
		static IPCMessageReceiver()
		{
			logger = LoggerFactory.getLogger(typeof(IPCMessageReceiver));
		}
	}
}