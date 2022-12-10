using System;
namespace VassalSharp.tools.ipc
{
	
	[Serializable]
	class Halt:SimpleIPCMessage
	{
		private const long serialVersionUID = 1L;

		public override bool expectsReply()
		{
			return true;
		}
	}
}