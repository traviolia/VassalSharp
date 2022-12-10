using System;
namespace VassalSharp.tools.ipc
{
	
	[Serializable]
	public class SimpleIPCMessage : IPCMessage
	{
		virtual public long Id
		{
			get
			{
				return id;
			}
			
			set
			{
				this.id = value;
			}
			
		}
		virtual public bool Reply
		{
			get
			{
				return false;
			}
			
		}
		virtual public long InReplyTo
		{
			get
			{
				return inReplyTo;
			}
			
			set
			{
				this.inReplyTo = value;
			}
			
		}
		private const long serialVersionUID = 1L;
		
		protected internal long id;
		protected internal long inReplyTo;
		
		public virtual bool expectsReply()
		{
			return false;
		}

		public override int GetHashCode()
		{
			return (int) (id ^ (SupportClass.URShift(id, 32)));
		}

		public  override bool Equals(System.Object o)
		{
			if (o == this)
				return true;
			if (!(o is IPCMessage))
				return false;
			
			return id == ((IPCMessage) o).Id;
		}

		public override System.String ToString()
		{
			return GetType().FullName + "[id=" + id + ",inReplyTo=" + inReplyTo + "]";
		}
	}
}