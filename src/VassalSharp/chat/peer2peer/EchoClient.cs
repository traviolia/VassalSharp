/*
* Created by IntelliJ IDEA.
* User: rkinney
* Date: Jul 23, 2002
* Time: 6:04:16 AM
* To change template for new class use
* Code Style | Class Templates options (Tools | IDE Options).
*/
using System;
using PeerInfo = org.litesoft.p2pchat.PeerInfo;
using Chatter = VassalSharp.build.module.Chatter;
using ChatServerConnection = VassalSharp.chat.ChatServerConnection;
using Player = VassalSharp.chat.Player;
using SimpleRoom = VassalSharp.chat.SimpleRoom;
using Command = VassalSharp.command.Command;
namespace VassalSharp.chat.peer2peer
{
	
	public class EchoClient : IThreadRunnable
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(EchoClient enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(EchoClient enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private EchoClient enclosingInstance;
			public EchoClient Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				System.Console.Error.WriteLine(System.Convert.ToString(evt.NewValue)); //$NON-NLS-1$
			}
		}
		public const System.String NAME = "EchoBot"; //$NON-NLS-1$
		private static System.Random rng = new System.Random();
		private int changeRoom;
		private int numRooms;
		//UPGRADE_TODO: Class 'java.io.FileWriter' was converted to 'System.IO.StreamWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileWriter'"
		private System.IO.StreamWriter log;
		private ChatServerConnection client;
		
		//UPGRADE_TODO: Class 'java.io.FileWriter' was converted to 'System.IO.StreamWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileWriter'"
		public EchoClient(ChatServerConnection client, int changeRoom, int numRooms, System.IO.StreamWriter log)
		{
			this.client = client;
			this.changeRoom = changeRoom;
			this.numRooms = numRooms;
			this.log = log;
			client.addPropertyChangeListener(VassalSharp.chat.ChatServerConnection_Fields.AVAILABLE_ROOMS, this);
			client.addPropertyChangeListener(VassalSharp.chat.ChatServerConnection_Fields.STATUS, new AnonymousClassPropertyChangeListener(this));
			client.setConnected(true);
			client.setRoom(new SimpleRoom("Room0")); //$NON-NLS-1$
			new SupportClass.ThreadClass(new System.Threading.ThreadStart(this.Run)).Start();
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'showCHAT'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  showCHAT(PeerInfo pPeerInfo, System.String msg)
		{
			lock (this)
			{
				Player sender = new P2PPlayer(pPeerInfo);
				if (!sender.getName().StartsWith(NAME) && msg.StartsWith("CHAT"))
				{
					//$NON-NLS-1$
					msg = "<" + client.UserInfo.getName() + msg.Substring(msg.IndexOf("> -")); //$NON-NLS-1$
					Command c = new Chatter.DisplayText(null, msg);
					client.sendTo(sender, c);
				}
			}
		}
		
		public virtual void  Run()
		{
			while (true)
			{
				try
				{
					//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_float'"
					int nextSleep = (int) System.Math.Round((double) ((float) rng.NextDouble() * 2 * changeRoom * 1000));
					//UPGRADE_TODO: Method 'java.lang.Thread.sleep' was converted to 'System.Threading.Thread.Sleep' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangThreadsleep_long'"
					System.Threading.Thread.Sleep(new System.TimeSpan((System.Int64) 10000 * nextSleep));
				}
				catch (System.Threading.ThreadInterruptedException e)
				{
				}
				
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				System.String newRoom = "Room" + (int) (numRooms * (float) rng.NextDouble()); //$NON-NLS-1$
				client.setRoom(new SimpleRoom(newRoom));
			}
		}
		
		public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
		{
			if (log != null && evt.PropertyName.Equals(VassalSharp.chat.ChatServerConnection_Fields.AVAILABLE_ROOMS))
			{
				try
				{
					//UPGRADE_TODO: Method 'java.util.Date.toString' was converted to 'System.DateTime.ToString' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilDatetoString'"
					log.Write("----------" + (System.DateTime.Now).ToString("r") + "---------\n"); //$NON-NLS-1$ //$NON-NLS-2$
					log.Write(report((VassalSharp.chat.Room[]) evt.NewValue));
					log.Flush();
				}
				// FIXME: review error message
				catch (System.IO.IOException e)
				{
					SupportClass.WriteStackTrace(e, Console.Error);
				}
			}
		}
		
		public static System.String report(VassalSharp.chat.Room[] r)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'buffer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder buffer = new StringBuilder();
			for (int i = 0; i < r.Length; ++i)
			{
				buffer.append(r[i].getName() + ": "); //$NON-NLS-1$
				VassalSharp.chat.Player[] l = (VassalSharp.chat.Player[]) r[i].getPlayerList().toArray();
				for (int j = 0; j < l.Length; ++j)
				{
					buffer.append(l[j]);
					if (j < l.Length - 1)
					{
						buffer.append(", "); //$NON-NLS-1$
					}
				}
				buffer.append("\n"); //$NON-NLS-1$
			}
			return buffer.toString();
		}
		
		/*  public static void main(String[] args) throws Exception {
		if (args.length == 0) {
		System.out.println("-module [module name] -clients [num clients] -host [host] -port [port] -rooms [num rooms] -wait [wait time] -log [true|false] -type [CGI|Proxy|Brokered|hierarchical]");
		System.exit(0);
		}
		Properties p = new ArgsParser(args).getProperties();
		String modName = p.getProperty("module","test");
		String host = p.getProperty("host","localhost");
		int port = Integer.parseInt(p.getProperty("port","5050"));
		int nClients = Integer.parseInt(p.getProperty("clients","2"));
		int nRooms = Integer.parseInt(p.getProperty("rooms","4"));
		int wait = Integer.parseInt(p.getProperty("wait","10"));
		boolean writeLogs = "true".equals(p.getProperty("log","true"));
		String poolType = p.getProperty("type","Proxy");
		StringTokenizer st = new StringTokenizer(modName, ",");
		MessageBoard msgSvr = new MessageBoard() {
		public Message[] getMessages() {
		return new Message[0];
		}
		
		public void postMessage(String msg) {
		}
		};
		WelcomeMessageServer welcomer = new WelcomeMessageServer() {
		public Command getWelcomeMessage() {
		return new NullCommand();
		}
		};
		while (st.hasMoreTokens()) {
		final String moduleName = st.nextToken();
		for (int i = 0; i < nClients; ++i) {
		final String userName = NAME+i;
		PeerPoolInfo info = new PeerPoolInfo() {
		public String getModuleName() {
		return moduleName;
		}
		
		public String getUserName() {
		return userName;
		}
		};
		PeerPool pool;
		pool = new DirectPeerPool();
		Thread.sleep((int) (wait * 1000 * rng.nextFloat()));
		ChatServerConnection client = null;
		if (poolType.startsWith("hier")) {
		client = new SocketNodeClient(new TextClient.Encoder(), info, host, port, msgSvr, welcomer);
		}
		else {
		client = new P2PClient(new TextClient.Encoder(), msgSvr, welcomer, pool);
		}
		client.setUserInfo(new SimplePlayer(userName));
		new EchoClient(client, wait, nRooms,
		writeLogs ? new FileWriter("Log" + i) : null);
		}
		}
		}*/
	}
}