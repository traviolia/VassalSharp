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
using Room = VassalSharp.chat.Room;
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
namespace VassalSharp.chat.peer2peer
{
	
	public class TextClient
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(TextClient enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TextClient enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TextClient enclosingInstance;
			public TextClient Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				Enclosing_Instance.availableRoomsChanged(event_sender, evt);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener1
		{
			public AnonymousClassPropertyChangeListener1(TextClient enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TextClient enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TextClient enclosingInstance;
			public TextClient Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				Enclosing_Instance.incomingMessageReceived(event_sender, evt);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener2
		{
			public AnonymousClassPropertyChangeListener2(TextClient enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TextClient enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TextClient enclosingInstance;
			public TextClient Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				Enclosing_Instance.statusReceived(event_sender, evt);
			}
		}
		virtual public ChatServerConnection Client
		{
			get
			{
				return client;
			}
			
		}
		private bool reportRooms = true;
		private ChatServerConnection client;
		
		public TextClient(ChatServerConnection client)
		{
			this.client = client;
			client.addPropertyChangeListener(VassalSharp.chat.ChatServerConnection_Fields.AVAILABLE_ROOMS, new AnonymousClassPropertyChangeListener(this));
			client.addPropertyChangeListener(VassalSharp.chat.ChatServerConnection_Fields.INCOMING_MSG, new AnonymousClassPropertyChangeListener1(this));
			client.addPropertyChangeListener(VassalSharp.chat.ChatServerConnection_Fields.STATUS, new AnonymousClassPropertyChangeListener2(this));
			client.setConnected(true);
		}
		
		private void  statusReceived(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
		{
			System.Console.Out.WriteLine(System.Convert.ToString(evt.NewValue)); //$NON-NLS-1$
		}
		
		private void  incomingMessageReceived(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
		{
			System.String msg = (System.String) evt.NewValue;
			if (msg.StartsWith("CHAT"))
			{
				//$NON-NLS-1$
				System.Console.Out.WriteLine(msg.Substring(4));
			}
		}
		
		private void  availableRoomsChanged(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
		{
			if (reportRooms)
			{
				//UPGRADE_TODO: Method 'java.util.Date.toString' was converted to 'System.DateTime.ToString' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilDatetoString'"
				System.Console.Out.WriteLine("----------" + (System.DateTime.Now).ToString("r") + "---------"); //$NON-NLS-1$ //$NON-NLS-2$
				System.Console.Out.Write(report((Room[]) evt.NewValue));
			}
		}
		
		public static System.String report(Room[] r)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'buffer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder buffer = new StringBuilder();
			for (int i = 0; i < r.Length; ++i)
			{
				buffer.append(r[i].getName() + ": "); //$NON-NLS-1$
				
				Player[] p = (Player[]) r[i].getPlayerList().toArray();
				for (int j = 0; j < p.Length; ++j)
				{
					buffer.append(p[j]);
					if (j < p.Length - 1)
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
		System.out.println("Usage:  -module <module> -name <username> -type <hier|CGI|Brokered|Proxy> -host <host> -port <port>");
		System.exit(0);
		}
		if (System.getProperty("stderr") != null) {
		System.setErr(new PrintStream(new FileOutputStream(System.getProperty("stderr"))));
		}
		Properties p = new ArgsParser(args).getProperties();
		String modName = p.getProperty("module", "test");
		String myName = p.getProperty("name", "rk");
		String poolType = p.getProperty("type", "Brokered");
		String host = p.getProperty("host", "localhost");
		int port = Integer.parseInt(p.getProperty("port", "5050"));
		final BufferedReader input = new BufferedReader(new InputStreamReader(System.in));
		final String moduleName = modName;
		final String userName = myName;
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
		ChatServerConnection clientConnection;
		PeerPoolInfo info = new PeerPoolInfo() {
		public String getModuleName() {
		return moduleName;
		}
		
		public String getUserName() {
		return userName;
		}
		};
		CommandEncoder encoder = new Encoder();
		if (poolType.startsWith("hier")) {
		clientConnection = new SocketNodeClient(encoder, info, host, port, msgSvr, welcomer);
		}
		else {
		PeerPool pool;
		if ("CGI".equals(poolType)) {
		pool = new CgiPeerPool(info, "http://www.vassalengine.org/util/");
		}
		else if ("Proxy".equals(poolType)) {
		pool = new ProxyPeerPool(info, host, port);
		}
		else {
		pool = new BrokeredPeerPool(info, host, port);
		}
		clientConnection = new P2PClient(encoder, msgSvr, welcomer, pool);
		if (pool instanceof BrokeredPeerPool) {
		final BrokeredPeerPool reportStatus = (BrokeredPeerPool) pool;
		final ChatServerConnection c = clientConnection;
		PropertyChangeListener l = new PropertyChangeListener() {
		public void propertyChange(PropertyChangeEvent evt) {
		reportStatus.sendStatus(c, moduleName);
		}
		};
		clientConnection.addPropertyChangeListener(ChatServerConnection.ROOM, l);
		clientConnection.addPropertyChangeListener(ChatServerConnection.PLAYER_INFO, l);
		}
		}
		final TextClient client = new TextClient(clientConnection);
		client.getClient().setUserInfo(new SimplePlayer(userName));
		Runnable r = new Runnable() {
		public void run() {
		String s;
		try {
		while ((s = input.readLine()) != null) {
		if (s.startsWith("-room")) {
		client.reportRooms = false;
		}
		else if (s.startsWith("+room")) {
		client.reportRooms = true;
		}
		else if (s.startsWith("JOIN")) {
		s = s.substring("JOIN".length()).trim();
		client.getClient().setRoom(new SimpleRoom(s));
		}
		else if (s.startsWith("BYE")) {
		client.getClient().setConnected(false);
		}
		else if (s.startsWith("HELLO")) {
		client.getClient().setConnected(true);
		}
		else if (s.startsWith("SHOUT")) {
		s = s.substring("SHOUT".length()).trim();
		s = "CHAT<" + client.getClient().getUserInfo().getName()
		+ "> - " + s;
		client.shout(s);
		}
		else if (s.startsWith("!")) {
		try {
		int length = Integer.parseInt(s.substring(1));
		StringBuilder msg = new StringBuilder();
		msg.append("CHAT<" + client.getClient().getUserInfo().getName()
		+ "> ("+length+" chars) -");
		for (int i=0;i<length;++i) {
		msg.append((char)('0'+i%10));
		}
		Command c = new ShowText(msg.toString());
		client.getClient().sendToOthers(c);
		client.getClient().sendTo(client.getClient().getUserInfo(),c);
		}
		catch (NumberFormatException e) {
		}
		}
		else if (s.length() > 0) {
		String msg = " - <" + client.getClient().getUserInfo().getName()
		+ "> - " + s;
		Command c = new ShowText(msg);
		client.getClient().sendToOthers(c);
		client.getClient().sendTo(client.getClient().getUserInfo(),c);
		}
		}
		}
		catch (IOException ex) {
		ex.printStackTrace();
		}
		}
		};
		new Thread(r).start();
		}*/
		public class ShowText:Command
		{
			virtual public System.String Message
			{
				get
				{
					return msg;
				}
				
			}
			private System.String msg;
			
			public ShowText(System.String msg)
			{
				this.msg = msg;
			}
			
			//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
			public override void  executeCommand()
			{
				System.Console.Out.WriteLine(msg);
			}
			
			//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
			public override Command myUndoCommand()
			{
				return null;
			}
		}
		
		public class Encoder : CommandEncoder
		{
			public virtual Command decode(System.String command)
			{
				Command c = null;
				if (command.StartsWith("CHAT"))
				{
					//$NON-NLS-1$
					c = new ShowText(command.Substring(4));
				}
				return c;
			}
			
			public virtual System.String encode(Command c)
			{
				System.String s = null;
				if (c is ShowText)
				{
					return "CHAT" + ((ShowText) c).Message; //$NON-NLS-1$
				}
				return s;
			}
		}
	}
}