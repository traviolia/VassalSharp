using System;
using ActivePeerManager = org.litesoft.p2pchat.ActivePeerManager;
using MyInfo = org.litesoft.p2pchat.MyInfo;
using PeerInfo = org.litesoft.p2pchat.PeerInfo;
using PeerReader = org.litesoft.p2pchat.PeerReader;
using PeerWriter = org.litesoft.p2pchat.PeerWriter;
using PendingPeerManager = org.litesoft.p2pchat.PendingPeerManager;
using UserDialog = org.litesoft.p2pchat.UserDialog;
namespace VassalSharp.chat.peer2peer
{
	
	public class UnitTest : UserDialog
	{
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(System.Object lock_Renamed, int port)
			{
				InitBlock(lock_Renamed, port);
			}
			private void  InitBlock(System.Object lock_Renamed, int port)
			{
				this.lock_Renamed = lock_Renamed;
				this.port = port;
			}
			//UPGRADE_NOTE: Final variable lock_Renamed was copied into class AnonymousClassRunnable. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Object lock_Renamed;
			//UPGRADE_NOTE: Final variable port was copied into class AnonymousClassRunnable. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private int port;
			public virtual void  Run()
			{
				try
				{
					System.Net.Sockets.TcpListener server;
					lock (lock_Renamed)
					{
						System.Net.Sockets.TcpListener temp_tcpListener;
						temp_tcpListener = new System.Net.Sockets.TcpListener(System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0], port);
						temp_tcpListener.Start();
						server = temp_tcpListener;
						System.Threading.Monitor.PulseAll(lock_Renamed);
					}
					
					//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Net.Sockets.TcpClient s = server.AcceptTcpClient();
					PeerReader reader = null;
					try
					{
						reader = new PeerReader(s.GetStream());
						while (true)
						{
							//UPGRADE_NOTE: Final was removed from the declaration of 'msg '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							System.String msg = reader.readLine();
							System.Console.Error.WriteLine(msg == null?"":msg); //$NON-NLS-1$
							if (msg == null)
							{
								break;
							}
						}
						System.Console.Error.WriteLine("Done"); //$NON-NLS-1$
						reader.close();
					}
					finally
					{
						if (reader != null)
							reader.close();
					}
				}
				catch (System.IO.IOException e)
				{
					SupportClass.WriteStackTrace(e, Console.Error);
				}
			}
		}
		virtual public ActivePeerManager ActivePeerManager
		{
			set
			{
			}
			
		}
		virtual public PendingPeerManager PendingPeerManager
		{
			set
			{
			}
			
		}
		private System.String id;
		
		public UnitTest(System.String id)
		{
			this.id = id;
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
			System.Collections.Specialized.NameValueCollection p = new System.Collections.Specialized.NameValueCollection();
			
			if ("true".Equals(p.Get("reader")))
			{
				//$NON-NLS-1$ //$NON-NLS-2$
				//testReadWrite();
				testPeer();
			}
			
			if ("true".Equals(p.Get("manager")))
			{
				//$NON-NLS-1$ //$NON-NLS-2$
				testPeerManager();
			}
			
			if ("true".Equals(p.Get("managers")))
			{
				//$NON-NLS-1$ //$NON-NLS-2$
				testPeerManagers();
			}
		}
		
		private static void  testPeerManager()
		{
			startSocketReader(5555);
			//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			UserDialog d = new UnitTest("Client"); //$NON-NLS-1$
			//UPGRADE_NOTE: Final was removed from the declaration of 'ppm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			PendingPeerManager ppm = new PendingPeerManager(d);
			//UPGRADE_NOTE: Final was removed from the declaration of 'apm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			ActivePeerManager apm = new ActivePeerManager(new MyInfo("TestClient", 5556), d, ppm); //$NON-NLS-1$
			//UPGRADE_NOTE: Final was removed from the declaration of 'info '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			PeerInfo info = new PeerInfo("SocketReader", "localhost", 5555); //$NON-NLS-1$ //$NON-NLS-2$
			info.ID = "SocketReader"; //$NON-NLS-1$
			ppm.addNewPeer(info);
			//UPGRADE_TODO: Method 'java.lang.Thread.sleep' was converted to 'System.Threading.Thread.Sleep' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangThreadsleep_long'"
			System.Threading.Thread.Sleep(new System.TimeSpan((System.Int64) 10000 * 10000));
			apm.sendToAllCHAT("A message"); //$NON-NLS-1$
			apm.getPeerListenerByID("1").close(); //$NON-NLS-1$
			//    apm.clear();
			System.Console.Error.WriteLine("Closed peer"); //$NON-NLS-1$
		}
		
		private static void  testPeerManagers()
		{
			UserDialog serverDialog = new UnitTest("Server"); //$NON-NLS-1$
			PendingPeerManager serverPpm = new PendingPeerManager(serverDialog);
			new AcceptPeerThread(5555, serverPpm).Start();
			
			UserDialog d = new UnitTest("Client"); //$NON-NLS-1$
			PendingPeerManager ppm = new PendingPeerManager(d);
			ActivePeerManager apm = new ActivePeerManager(new MyInfo("Client", 5556), d, ppm); //$NON-NLS-1$
			PeerInfo info = new PeerInfo("Server", "localhost", 5555); //$NON-NLS-1$ //$NON-NLS-2$
			info.ID = "Server"; //$NON-NLS-1$
			ppm.addNewPeer(info);
			//UPGRADE_TODO: Method 'java.lang.Thread.sleep' was converted to 'System.Threading.Thread.Sleep' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangThreadsleep_long'"
			System.Threading.Thread.Sleep(new System.TimeSpan((System.Int64) 10000 * 5000));
			apm.sendToAllCHAT("A message"); //$NON-NLS-1$
			apm.clear();
		}
		
		private static void  testPeer()
		{
			startSocketReader(5555);
			/*
			UserDialog d = new UnitTest("Client");
			PendingPeerManager ppm = new PendingPeerManager(d);
			MyInfo myInfo = new MyInfo("TestClient",5556);
			ActivePeerManager apm = new ActivePeerManager(myInfo,d,ppm);
			PeerInfo info = new PeerInfo("Server","localhost",5555);*/
			//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Net.Sockets.TcpClient s = new System.Net.Sockets.TcpClient("localhost", 5555); //$NON-NLS-1$
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'reader '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			PeerReader reader = new PeerReader(s.GetStream());
			System.Console.Error.WriteLine("Created reader " + reader); //$NON-NLS-1$
			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'writer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				PeerWriter writer = new PeerWriter(s.GetStream());
				System.Console.Error.WriteLine("Created writer" + writer); //$NON-NLS-1$
				//      ActivePeer peer = new ActivePeer(myInfo,d, apm, ppm, info, reader, writer);
				//      peer.sendCHAT("A message");
				
				try
				{
					writer.writeLine("A message"); //$NON-NLS-1$
					//UPGRADE_TODO: Method 'java.lang.Thread.sleep' was converted to 'System.Threading.Thread.Sleep' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangThreadsleep_long'"
					System.Threading.Thread.Sleep(new System.TimeSpan((System.Int64) 10000 * 2000));
				}
				finally
				{
					writer.close();
				}
			}
			finally
			{
				reader.close();
			}
		}
		
		private static void  startSocketReader(int port)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'lock '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Object lock_Renamed = new System.Object();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'runnable '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			IThreadRunnable runnable = new AnonymousClassRunnable(lock_Renamed, port);
			
			lock (lock_Renamed)
			{
				new SupportClass.ThreadClass(new System.Threading.ThreadStart(runnable.Run)).Start();
				System.Threading.Monitor.Wait(lock_Renamed);
			}
		}
		
		public virtual void  showUnrecognized(PeerInfo pPeerInfo, System.String pBadMessage)
		{
			System.Console.Error.WriteLine(this + "Unrecognized " + pPeerInfo); //$NON-NLS-1$
		}
		
		public virtual void  showStreamsFailed(PeerInfo pPeerInfo)
		{
			System.Console.Error.WriteLine(this + "Streams Failed " + pPeerInfo); //$NON-NLS-1$
		}
		
		public virtual void  showConnectFailed(PeerInfo pPeerInfo)
		{
			System.Console.Error.WriteLine(this + "Connect Failed " + pPeerInfo); //$NON-NLS-1$
		}
		
		public virtual void  showConnect(PeerInfo pPeerInfo)
		{
			System.Console.Error.WriteLine(this + "Connect " + pPeerInfo); //$NON-NLS-1$
		}
		
		public virtual void  showDisconnect(PeerInfo pPeerInfo)
		{
			System.Console.Error.WriteLine(this + "Disconnect " + pPeerInfo); //$NON-NLS-1$
		}
		
		public virtual void  showCHAT(PeerInfo pPeerInfo, System.String pMessage)
		{
			System.Console.Error.WriteLine(this + "Chat " + pPeerInfo); //$NON-NLS-1$
		}
		
		public virtual void  showPMSG(PeerInfo pPeerInfo, System.String pMessage)
		{
			System.Console.Error.WriteLine(this + "Private Chat " + pPeerInfo); //$NON-NLS-1$
		}
		
		public virtual void  showNAME(PeerInfo pPeerInfo)
		{
			System.Console.Error.WriteLine(this + "Name " + pPeerInfo); //$NON-NLS-1$
		}
		
		public virtual void  showHELO(PeerInfo pPeerInfo)
		{
			System.Console.Error.WriteLine(this + "Hello " + pPeerInfo); //$NON-NLS-1$
		}
		
		public override System.String ToString()
		{
			return id + ": "; //$NON-NLS-1$
		}
	}
}