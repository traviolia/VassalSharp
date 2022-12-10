/*
* Created by IntelliJ IDEA.
* User: rkinney
* Date: Jul 23, 2002
* Time: 6:04:16 AM
* To change template for new class use
* Code Style | Class Templates options (Tools | IDE Options).
*/
using System;
using ChatServerConnection = VassalSharp.chat.ChatServerConnection;
using Player = VassalSharp.chat.Player;
using SimpleRoom = VassalSharp.chat.SimpleRoom;
using WelcomeMessageServer = VassalSharp.chat.WelcomeMessageServer;
using MessageBoard = VassalSharp.chat.messageboard.MessageBoard;
namespace VassalSharp.chat.peer2peer
{
	
	public class ClientTest:P2PClient, IThreadRunnable
	{
		private static System.Random rng = new System.Random();
		private int changeRoom;
		private int numRooms;
		//UPGRADE_TODO: Class 'java.io.FileWriter' was converted to 'System.IO.StreamWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileWriter'"
		private System.IO.StreamWriter log;
		
		//UPGRADE_TODO: Class 'java.io.FileWriter' was converted to 'System.IO.StreamWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileWriter'"
		public ClientTest(PeerPool pool, MessageBoard msgSvr, WelcomeMessageServer welcomer, int changeRoom, int numRooms, System.IO.StreamWriter log):base(new TextClient.Encoder(), msgSvr, welcomer, pool)
		{
			this.changeRoom = changeRoom;
			this.numRooms = numRooms;
			this.log = log;
			addPropertyChangeListener(VassalSharp.chat.ChatServerConnection_Fields.AVAILABLE_ROOMS, this);
			setConnected(true);
			new SupportClass.ThreadClass(new System.Threading.ThreadStart(this.Run)).Start();
		}
		
		public virtual void  Run()
		{
			while (true)
			{
				try
				{
					//UPGRADE_TODO: Method 'java.lang.Thread.sleep' was converted to 'System.Threading.Thread.Sleep' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangThreadsleep_long'"
					System.Threading.Thread.Sleep(new System.TimeSpan((System.Int64) 10000 * changeRoom * 1000));
				}
				catch (System.Threading.ThreadInterruptedException e)
				{
				}
				
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				System.String newRoom = "Room" + (int) (numRooms * (float) rng.NextDouble()); //$NON-NLS-1$
				setRoom(new SimpleRoom(newRoom));
			}
		}
		
		public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
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
		
		public static System.String report(VassalSharp.chat.Room[] r)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'buffer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder buffer = new StringBuilder();
			for (int i = 0; i < r.Length; ++i)
			{
				buffer.append(r[i].getName() + ": "); //$NON-NLS-1$
				//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				for (int j = 0; j < l.size(); ++j)
				{
					buffer.append(l.get_Renamed(j));
					if (j < l.size() - 1)
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
		System.err.println("Usage:  -module <module> -clients <clientCount> -rooms <roomCount> -wait <delay> -type <CGI|Proxy>");
		System.exit(0);
		}
		Properties p = new ArgsParser(args).getProperties();
		String modName = p.getProperty("module", "test");
		boolean useProxy = "CGI".equals(p.getProperty("type", "Proxy"));
		int nClients = Integer.parseInt(p.getProperty("clients", "2"));
		int nRooms = Integer.parseInt(p.getProperty("rooms", "4"));
		int wait = Integer.parseInt(p.getProperty("wait", "10"));
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
		for (int i = 0; i < nClients; ++i) {
		Thread.sleep((int) (wait * 1000 * rng.nextFloat()));
		final String moduleName = modName;
		final String userName = "client" + i;
		PeerPoolInfo info = new PeerPoolInfo() {
		public String getModuleName() {
		return moduleName;
		}
		
		public String getUserName() {
		return userName;
		}
		};
		PeerPool pool = new DirectPeerPool();
		new ClientTest(pool, msgSvr, welcomer, wait, nRooms, new FileWriter("Log" + i));
		}
		}*/
	}
}