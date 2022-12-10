using System;
namespace VassalSharp.tools.ipc
{
	
	public interface IPCMessage
	{
		long Id
		{
			get;
			
			set;
			
		}
		bool Reply
		{
			get;
			
		}
		long InReplyTo
		{
			get;
			
			set;
			
		}
		
		bool expectsReply();
	}
}