/*
 * Copyright (c) 2000-2009 by Rodney Kinney, Joel Uckelman
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Library General Public
 * License (LGPL) as published by the Free Software Foundation.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Library General Public License for more details.
 *
 * You should have received a copy of the GNU Library General Public
 * License along with this library; if not, copies are available
 * at http://www.opensource.org.
 */

 using Microsoft.Extensions.Logging;

using System;
using System.Windows.Forms;

using Info = VassalSharp.Info;
using GameModule = VassalSharp.build.GameModule;
using ExtensionsLoader = VassalSharp.build.module.ExtensionsLoader;
using Resources = VassalSharp.i18n.Resources;
//using ErrorDialog = VassalSharp.tools.ErrorDialog;
using ThrowableUtils = VassalSharp.tools.ThrowableUtils;
using  VassalSharp.tools.concurrent.listener;
using IPCMessenger = VassalSharp.tools.ipc.IPCMessenger;
using SimpleIPCMessage = VassalSharp.tools.ipc.SimpleIPCMessage;
using MenuManager = VassalSharp.tools.menu.MenuManager;

namespace VassalSharp.launch
{
	
	/// <author>Joel Uckelman</author>
	/// <since> 3.1.0</since>
	public abstract class Launcher
	{
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(Launcher launcher)
			{
				this.Launcher = launcher;
			}

            public Launcher Launcher { get; private set; }

            public virtual void  Run()
			{
				try
				{
					Launcher.launch();
				}
				catch (ExtensionsLoader.LoadExtensionException e2)
				{
					warn(e2);
				}
				catch (System.IO.IOException e1)
				{
					warn(e1);
				}
			}
			
			private void  warn(System.Exception e1)
			{
				if (Launcher.ipc == null)
				{
					// we are standalone, so warn the user directly
                    MessageBox.Show("Moddule_load_failed because " + e1.Message + Environment.NewLine + e1.StackTrace.ToString(),
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
				}
				else
				{
					// we have a manager, so pass the load failure back to it
					try
					{
						Launcher.ipc.send(new AbstractLaunchAction.NotifyOpenModuleFailed(Launcher.lr, e1));
					}
					catch (System.IO.IOException e2)
					{
                        // warn the user directly as a last resort
                        MessageBox.Show("Moddule_load_failed because " + e1.Message + Environment.NewLine + e1.StackTrace.ToString(),
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);

                        MessageBox.Show("Communication error because " + e2.Message + Environment.NewLine + e2.StackTrace.ToString(),
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
				
				System.Environment.Exit(1);
			}
		}

        private static Launcher _instance = null;

        public static Launcher Instance
		{
			get
			{
				return _instance;
			}
			
		}
		private static readonly ILogger logger;
		
		protected internal IPCMessenger ipc = null;
		
		protected internal LaunchRequest lr;
		
		
		protected internal Launcher(string[] args)
		{
			if (_instance != null)
				throw new InvalidOperationException("An instance of Launcher already exists");
			_instance = this;
			
			LaunchRequest lreq = null;
			try
			{
				lreq = LaunchRequest.parseArgs(args);
			}
			catch (LaunchRequestException e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Console.Error.WriteLine("VASSAL: " + e.Message);
				System.Environment.Exit(1);
			}
			
			lr = lreq;
			
			// Note: We could do more sanity checking of the launch request
			// in standalone mode, but we don't bother because this is meant
			// only for debugging, not for normal use. If you pass nonsense
			// arguments (e.g., '-e' to the Player), don't expect it to work.
			//UPGRADE_NOTE: Final was removed from the declaration of 'standalone '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			bool standalone = lr.standalone;
			
			/*
			// parse the command line args now if we're standalone, since they
			// could be messed up and so we'll bail before setup
			LaunchRequest lr = null;
			if (standalone) {
			// Note: We could do more sanity checking of the launch request
			// in standalone mode, but we don't bother because this is meant
			// only for debugging, not for normal use. If you pass nonsense
			// arguments (e.g., '-e' to the Player), don't expect it to work.
			try {
			lr = LaunchRequest.parseArgs(args);
			}
			catch (LaunchRequestException e) {
			System.err.println("VASSAL: " + e.getMessage());
			System.exit(1);
			}
			}*/
			
			// start the error log and setup system properties
			//UPGRADE_NOTE: Final was removed from the declaration of 'start '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StartUp start = new StartUp();
			
			start.startErrorLog();

			logger.LogInformation(GetType().Name);
			start.initSystemProperties();
			
			// if we're not standalone, contact the module manager for instructions
			if (!standalone)
			{
				try
				{
					int port = System.Int32.Parse(System_Renamed.getProperty("VassalSharp.port"));
					
					System.Net.IPAddress lo = System.Net.Dns.Resolve(null).AddressList[0];
					System.Net.Sockets.TcpClient cs = new System.Net.Sockets.TcpClient(lo.ToString(), port);
					
					ipc = new IPCMessenger(cs);
					
					ipc.addEventListener(typeof(CloseRequest), new CloseRequestListener(this));
					
					ipc.start();
					
					ipc.send(new StartedNotice(Info.InstanceID));
				}
				catch (System.IO.IOException e)
				{
					// What we've got here is failure to communicate.
					//ErrorDialog.show(e, "Error.communication_error", Resources.getString(GetType().Name + ".app_name"));
					System.Environment.Exit(1);
				}
			}
			
			createMenuManager();
			
			//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
			//SwingUtilities.invokeLater(new AnonymousClassRunnable(this));
		}
		
		protected internal abstract void  launch();
		
		protected internal abstract MenuManager createMenuManager();
		
		[Serializable]
		public class CloseRequest : SimpleIPCMessage
		{
		}
		
		[Serializable]
        public class CloseAccept : SimpleIPCMessage
        {
            public long pid;

            public CloseAccept(long pid)
            {
                this.pid = pid;
            }
        }

        [Serializable]
        public class CloseReject : SimpleIPCMessage
        {
            public long pid;

            public CloseReject(long pid)
            {
                this.pid = pid;
            }
        }

        [Serializable]
        public class StartedNotice : SimpleIPCMessage
        {
            public long pid;

            public StartedNotice(long pid)
            {
                this.pid = pid;
            }
        }

        protected internal class CloseRequestListener : EventListener<CloseRequest>
		{
            private class AnonymousClassRunnable1 : IThreadRunnable
			{
				public AnonymousClassRunnable1(VassalSharp.build.GameModule module, CloseRequestListener closeRequestListener)
				{
					this.module = module;
					this.CloseRequestListener = closeRequestListener;
				}

				private VassalSharp.build.GameModule module;

                public CloseRequestListener CloseRequestListener { get; }

                public virtual void  Run()
				{
					//module.Frame.BringToFront();
					//CloseRequestListener.shutdown = module.shutDown();
				}
			}

            public CloseRequestListener(Launcher launcher)
            {
                this.Launcher = launcher;
            }

            public Launcher Launcher { get; }

			private bool shutdown;
			
			public virtual void receive(System.Object src, CloseRequest msg)
			{
				GameModule module = GameModule.getGameModule();
				if (module != null)
				{
					try
					{
						//SwingUtilities.invokeAndWait(new AnonymousClassRunnable1(module, this));
					}
					catch (System.Threading.ThreadInterruptedException e)
					{
						VassalSharp.launch.Launcher.logger.LogError(e, "");
						shutdown = false;
					}
					catch (System.Reflection.TargetInvocationException e)
					{
						//ErrorDialog.bug(e);
						shutdown = false;
					}
				}
				
				if (shutdown)
				{
					if (Launcher.ipc != null)
					{
						try
						{
							Launcher.ipc.send(new CloseAccept(- msg.Id));
						}
						catch (System.IO.IOException e)
						{
							SupportClass.WriteStackTrace(e, Console.Error);
						}
					}
					System.Environment.Exit(0);
				}
				else
				{
					if (Launcher.ipc != null)
					{
						try
						{
							Launcher.ipc.send(new CloseReject(- msg.Id));
						}
						catch (System.IO.IOException e)
						{
							SupportClass.WriteStackTrace(e, Console.Error);
						}
					}
				}
			}
		}
		
		public virtual void  sendSaveCmd(System.IO.FileInfo f)
		{
			if (ipc != null)
			{
				try
				{
					ipc.send(new AbstractLaunchAction.NotifySaveFileOk(f));
				}
				// FIXME: review error message
				catch (System.IO.IOException e)
				{
				}
			}
		}
	}
}