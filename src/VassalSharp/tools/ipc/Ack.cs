using System;
namespace VassalSharp.tools.ipc
{
	
	[Serializable]
	class Ack:SimpleIPCMessage
	{
		override public bool Reply
		{
			get
			{
				return true;
			}
			
		}
		
		public Ack()
		{
		}
		
		public Ack(IPCMessage msg)
		{
			InReplyTo = msg.Id;
		}
	}
}