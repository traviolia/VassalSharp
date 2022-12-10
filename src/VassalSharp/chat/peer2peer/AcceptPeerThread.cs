using System;
using PendingPeerManager = org.litesoft.p2pchat.PendingPeerManager;
namespace VassalSharp.chat.peer2peer
{
	
	/// <summary> Date: Mar 11, 2003</summary>
	public class AcceptPeerThread:SupportClass.ThreadClass
	{
		virtual public int Port
		{
			get
			{
				return port;
			}
			
		}
		private bool running = true;
		private System.Net.Sockets.TcpListener socket;
		private PendingPeerManager ppm;
		private int port;
		private const int MAX_ATTEMPTS = 10;
		
		public AcceptPeerThread(int initialPort, PendingPeerManager ppm)
		{
			this.ppm = ppm;
			for (int i = 0; i < MAX_ATTEMPTS; ++i)
			{
				port = initialPort + i;
				try
				{
					System.Net.Sockets.TcpListener temp_tcpListener;
					temp_tcpListener = new System.Net.Sockets.TcpListener(System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0], port);
					temp_tcpListener.Start();
					socket = temp_tcpListener;
					break;
				}
				// FIXME: review error message
				catch (System.Exception ex)
				{
					if (i == MAX_ATTEMPTS - 1)
					{
						// FIXME: switch to IOException(Throwable) ctor in Java 1.6
						throw (System.IO.IOException) new System.IO.IOException().initCause(ex);
					}
				}
			}
		}
		
		public AcceptPeerThread(System.Net.Sockets.TcpListener socket, PendingPeerManager ppm)
		{
			this.socket = socket;
			this.ppm = ppm;
		}
		
		override public void  Start()
		{
			running = true;
		}
		
		override public void  Run()
		{
			while (running)
			{
				try
				{
					ppm.addNewPeer(socket.AcceptTcpClient());
				}
				// FIXME: review error message
				catch (System.Exception ex)
				{
					halt();
				}
			}
		}
		
		public virtual void  halt()
		{
			Interrupt();
			running = false;
			try
			{
				socket.Stop();
			}
			// FIXME: review error message
			catch (System.IO.IOException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
			}
		}
	}
}