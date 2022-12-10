using System;
namespace VassalSharp.tools.ipc
{
	
	[Serializable]
	class Fin:SimpleIPCMessage
	{
		override public bool Reply
		{
			get
			{
				return true;
			}
			
		}
		private const long serialVersionUID = 1L;
		
		public Fin()
		{
		}
		
		public Fin(Halt halt)
		{
			InReplyTo = halt.Id;
		}
	}
}