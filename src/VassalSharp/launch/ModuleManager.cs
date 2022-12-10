/*
* $Id$
*
* Copyright (c) 2000-2008 by Rodney Kinney, Joel Uckelman
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
using System;
////UPGRADE_TODO: The type 'java.io.Closeable' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using Closeable = java.io.Closeable;
////UPGRADE_TODO: The type 'java.nio.channels.FileLock' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using FileLock = java.nio.channels.FileLock;
////UPGRADE_TODO: The type 'java.nio.channels.OverlappingFileLockException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using OverlappingFileLockException = java.nio.channels.OverlappingFileLockException;
////UPGRADE_TODO: The type 'org.apache.commons.io.FileUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using FileUtils = org.apache.commons.io.FileUtils;
////UPGRADE_TODO: The type 'org.apache.commons.lang.SystemUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using SystemUtils = org.apache.commons.lang.SystemUtils;
////UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using Logger = org.slf4j.Logger;
////UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using LoggerFactory = org.slf4j.LoggerFactory;
using Info = VassalSharp.Info;
using AbstractMetaData = VassalSharp.build.module.metadata.AbstractMetaData;
using MetaDataFactory = VassalSharp.build.module.metadata.MetaDataFactory;
using SaveMetaData = VassalSharp.build.module.metadata.SaveMetaData;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using LongConfigurer = VassalSharp.configure.LongConfigurer;
using Resources = VassalSharp.i18n.Resources;
//using TranslateVassalWindow = VassalSharp.i18n.TranslateVassalWindow;
using Prefs = VassalSharp.preferences.Prefs;
//using ErrorDialog = VassalSharp.tools.ErrorDialog;
//using ThrowableUtils = VassalSharp.tools.ThrowableUtils;
using IOUtils = VassalSharp.tools.io.IOUtils;
using ZipArchive = VassalSharp.tools.io.ZipArchive;
//using LoggedOutputStream = VassalSharp.tools.logging.LoggedOutputStream;
using VassalSharp.UiInterfaces;
using VassalSharp.UiWinForms;
using Microsoft.Extensions.Logging;

namespace VassalSharp.launch
{
	
	/// <summary> Tracks recently-used modules and builds the main GUI window for
	/// interacting with modules.
	/// 
	/// </summary>
	/// <author>  rodneykinney
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class ModuleManager
	{
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public virtual void  Run()
			{
				// FIXME: does this window exit on close?
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//new TranslateVassalWindow(null).Visible = true;
			}
		}

		public static ModuleManager Instance
		{
			get
			{
				return _Instance;
			}
			
		}

		public static IUiProvider UiProvider { get; internal set; }

		public static ILogger Logger { get; internal set; }

		private const string NEXT_VERSION_CHECK = "nextVersionCheck";
				
		[STAThread]
		public static void  Main(string[] args)
		{
			// FIXME: We need to catch more exceptions in main() and then exit in
			// order to avoid situations where the main thread ends due to an uncaught
			// exception, but there are other threads still running, and so VASSAL
			// does not quit. For example, this can happen if an IllegalArgumentException
			// is thrown here...
			
			// parse command-line arguments
			LaunchRequest lr = null;
			try
			{
				lr = LaunchRequest.parseArgs(args);
			}
			catch (LaunchRequestException e)
			{
				// FIXME: should be a dialog...
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Console.Error.WriteLine("VASSAL: " + e.Message);
				System.Environment.Exit(1);
			}

			//
			Logger = null;
			// Select the UI to be used.
			UiProvider = new WinFormsUiProvider();
			
			if (lr.mode == LaunchRequest.Mode.TRANSLATE)
			{
				//// show the translation window in translation mode
				////UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				//SwingUtilities.invokeLater(new AnonymousClassRunnable());
				return ;
			}
			
			//
			// How we start exactly one request server:
			//
			// To ensure that exactly one process acts as the request server, we
			// acquire a lock on the ~/VASSAL/key file, and then attempt to acquire
			// a lock on the ~/VASSAL/lock file. If we cannot lock ~/VASSAL/lock,
			// then there is already a server running; in that case, we read the
			// port number and security key from ~/VASSAL/key. If we can lock
			// ~/VASSAL/lock, then we start the server, write the port number and
			// key to ~/VASSAL/key, and continue to hold the lock on ~/VASSAL/lock.
			// Finally, we unlock ~/VASSAL/key and proceed to act as a client,
			// sending requests over localhost:port using the security key.
			//
			// The advantages of this method are:
			//
			// (1) No race conditions between processes started at the same time.
			// (2) No port collisions, because we don't use a predetermined port.
			//
			
			string keyfile =  System.IO.Path.Combine(Info.ConfDir , "key");
			string lockfile =  System.IO.Path.Combine(Info.ConfDir, "lock");
			
			int port = 0;
			long key = 0;

			System.IO.FileStream keyFileStream = null;
			try
			{
				// acquire an exclusive lock on the key file
				keyFileStream = System.IO.File.Open(keyfile, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.None);

				// determine whether we are the server or a client

				// Note: We purposely keep lockFileStream open in the case where we are the
				// server, because closing lockFileStream will release the lock.
				System.IO.FileStream lockFileStream = null;
				try
				{
					lockFileStream = System.IO.File.Open(keyfile, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.None);
				}
				catch
				{
				}

				if (lockFileStream != null)
                {
					// we have the lock, so we will be the request server

					// bind to an available port on the loopback device
					var listener = new System.Net.Sockets.TcpListener(new System.Net.IPEndPoint(System.Net.Dns.GetHostAddresses(string.Empty)[0], 0));
					listener.Start();

					// write the port number where we listen to the key file
					var writer = new System.IO.BinaryWriter(keyFileStream);
					port = ((System.Net.IPEndPoint)listener.LocalEndpoint).Port;
					writer.Write(port);

					// create new security key and write it to the key file
					key = (long) Math.Truncate((new System.Random().NextDouble() * long.MaxValue));
					writer.Write(key);

					// create a new Module Manager and run the server until it exits.
					new ModuleManager(listener, key, lockFileStream).RunServer();
				} 
				else
				{
					// we do not have the lock, so we will be a request client
					lockFileStream.Close();
					
					// read the port number we will connect to from the key file
					System.IO.BinaryReader temp_BinaryReader;
					temp_BinaryReader = new System.IO.BinaryReader(keyFileStream);
					temp_BinaryReader.BaseStream.Position = keyFileStream.Position;
					port = temp_BinaryReader.ReadInt32();
					
					// read the security key from the key file
					System.IO.BinaryReader temp_BinaryReader2;
					temp_BinaryReader2 = new System.IO.BinaryReader(keyFileStream);
					temp_BinaryReader2.BaseStream.Position = keyFileStream.Position;
					key = temp_BinaryReader2.ReadInt64();
				}
				
				keyFileStream.Close();
			}
			catch (System.IO.IOException e)
			{
				// FIXME: should be a dialog...
				System.Console.Error.WriteLine("VASSAL: IO error");
				SupportClass.WriteStackTrace(e, Console.Error);
				System.Environment.Exit(1);
			}
			finally
			{
				// this will also release the lock on the key file
				IOUtils.closeQuietly(keyFileStream);
			}
			
			lr.key = key;
			
			// pass launch parameters on to the ModuleManager via the socket
			System.Net.Sockets.TcpClient clientSocket = null;
			//UPGRADE_TODO: Class 'java.io.ObjectOutputStream' was converted to 'System.IO.BinaryWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectOutputStream'"
			System.IO.BinaryWriter out_Renamed = null;
			System.IO.Stream in_Renamed = null;
			try
			{
				clientSocket = new System.Net.Sockets.TcpClient((string) null, port);
				
				//UPGRADE_TODO: Class 'java.io.ObjectOutputStream' was converted to 'System.IO.BinaryWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectOutputStream'"
				out_Renamed = new System.IO.BinaryWriter(new System.IO.BufferedStream(clientSocket.GetStream()));
				//UPGRADE_TODO: Method 'java.io.ObjectOutputStream.writeObject' was converted to 'SupportClass.Serialize' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectOutputStreamwriteObject_javalangObject'"
				SupportClass.Serialize(out_Renamed, lr);
				out_Renamed.Flush();
				
				in_Renamed = clientSocket.GetStream();
				//IOUtils.copy(in_Renamed, System.Console.Error);
			}
			catch (System.IO.IOException e)
			{
				// FIXME: should be a dialog...
				System.Console.Error.WriteLine("VASSAL: Problem with socket on port " + port);
				SupportClass.WriteStackTrace(e, Console.Error);
				System.Environment.Exit(1);
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
				IOUtils.closeQuietly(out_Renamed);
				IOUtils.closeQuietly(clientSocket);
			}
		}
		
		private static ModuleManager _Instance = null;
		
		private long key;		
		private System.IO.FileStream lockFileStream;		
		private System.Net.Sockets.TcpListener serverSocket;

		public ModuleManager(System.Net.Sockets.TcpListener serverSocket, long key, System.IO.FileStream lockFileStream)
		{
			if (_Instance != null) throw new System.InvalidOperationException("Module Manager already exists.");
			_Instance = this;

			this.serverSocket = serverSocket;
			this.key = key;

			// we hang on to this to prevent the lock from being lost
			this.lockFileStream = lockFileStream;
		}

		public void RunServer()
		{ 

#if TO_DO
			// log everything which comes across our stderr
			//UPGRADE_TODO: Method 'java.lang.System.setErr' was converted to 'System.Console.SetError' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangSystemsetErr_javaioPrintStream'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Console.SetError(new System.IO.StreamWriter(new LoggedOutputStream()));
			
			Thread.setDefaultUncaughtExceptionHandler(new ExceptionHandler());
			
			
			// try to migrate old preferences if there are no current ones
			//UPGRADE_NOTE: Final was removed from the declaration of 'pdir '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			string prefsDir = Info.PrefsDir;
			bool tmpBool;
			if (System.IO.File.Exists(prefsDir))
				tmpBool = true;
			else
				tmpBool = System.IO.Directory.Exists(prefsDir);
			if (!tmpBool)
			{
				// Check the 3.2.0 through 3.2.7 location
				System.IO.FileInfo pzip = new System.IO.FileInfo(Info.HomeDir.FullName + "\\" + "Preferences");
				bool tmpBool2;
				if (System.IO.File.Exists(pzip.FullName))
					tmpBool2 = true;
				else
					tmpBool2 = System.IO.Directory.Exists(pzip.FullName);
				if (!tmpBool2)
				{
					// Check the pre-3.2 location.
					//UPGRADE_TODO: Method 'java.lang.System.getProperty' was converted to 'System.Environment.GetEnvironmentVariable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangSystemgetProperty_javalangString'"
					pzip = new System.IO.FileInfo(System.Environment.GetEnvironmentVariable("userprofile") + "\\" + "VASSAL/Preferences");
				}
				
				bool tmpBool3;
				if (System.IO.File.Exists(pzip.FullName))
					tmpBool3 = true;
				else
					tmpBool3 = System.IO.Directory.Exists(pzip.FullName);
				if (tmpBool3)
				{
					FileUtils.forceMkdir(prefsDir);
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'buf '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					sbyte[] buf = new sbyte[4096];
					
					try
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'za '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						ZipArchive za = new ZipArchive(pzip);
						try
						{
							//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
							foreach (String f in za.getFiles())
							{
								//UPGRADE_NOTE: Final was removed from the declaration of 'ofile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
								System.IO.FileInfo ofile = new File(prefsDir, "VASSAL".Equals(f)?"V_Global":Prefs.sanitize(f));
								
								System.IO.Stream in_Renamed = null;
								try
								{
									in_Renamed = za.getInputStream(f);
									
									System.IO.Stream out_Renamed = null;
									try
									{
										//UPGRADE_TODO: Constructor 'java.io.FileOutputStream.FileOutputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileOutputStreamFileOutputStream_javaioFile'"
										out_Renamed = new System.IO.FileStream(ofile.FullName, System.IO.FileMode.Create);
										IOUtils.copy(in_Renamed, out_Renamed, buf);
										out_Renamed.Close();
									}
									finally
									{
										IOUtils.closeQuietly(out_Renamed);
									}
									
									in_Renamed.Close();
								}
								finally
								{
									IOUtils.closeQuietly(in_Renamed);
								}
							}
							za.close();
						}
						finally
						{
							IOUtils.closeQuietly(za);
						}
					}
					catch (System.IO.IOException e)
					{
						logger.error("Failed to convert legacy preferences file.", e);
					}
				}
			}
#endif
						
			// ModuleManagerWindow.getInstance() != null now, so listen on the socket
			//UPGRADE_NOTE: Final was removed from the declaration of 'socketListener '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SupportClass.ThreadClass socketListener = new SupportClass.ThreadClass(new System.Threading.ThreadStart(new SocketListener(this, serverSocket).Run), "socket listener");
			socketListener.IsBackground = true;
			socketListener.Start();
			
			////UPGRADE_NOTE: Final was removed from the declaration of 'globalPrefs '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//Prefs globalPrefs = Prefs.GlobalPrefs;
			
			//// determine when we should next check on the current version of VASSAL
			////UPGRADE_NOTE: Final was removed from the declaration of 'nextVersionCheckConfig '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//LongConfigurer nextVersionCheckConfig = new LongConfigurer(NEXT_VERSION_CHECK, null, - 1L);
			//globalPrefs.addOption(null, nextVersionCheckConfig);
			
			//long nextVersionCheck = nextVersionCheckConfig.getLongValue(- 1L);
			//if (nextVersionCheck < (System.DateTime.Now.Ticks - 621355968000000000) / 10000)
			//{
			//	new UpdateCheckRequest().execute();
			//}
			
			//// set the time for the next version check
			//if (nextVersionCheck == - 1L)
			//{
			//	// this was our first check; randomly check after 0-10 days to
			//	// to spread version checks evenly over a 10-day period
			//	//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			//	nextVersionCheck = (System.DateTime.Now.Ticks - 621355968000000000) / 10000 + (long) (SupportClass.Random.NextDouble() * 10 * 86400000);
			//}
			//else
			//{
			//	// check again in 10 days
			//	nextVersionCheck += 10 * 86400000;
			//}
			
			//nextVersionCheckConfig.setValue(nextVersionCheck);

			Launch();
		}

		internal void Launch()
		{
			Logger.info("Manager");

			//bool tmpBool;
			//if (System.IO.File.Exists(Info.PrefsDir.FullName))
			//	tmpBool = true;
			//else
			//	tmpBool = System.IO.Directory.Exists(Info.PrefsDir.FullName);
			//bool isFirstTime = !tmpBool;

			//if (isFirstTime)
			//{
			//	//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//	//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			//	new FirstTimeDialog(window).Visible = true;
			//}

			UiProvider.NewUiApplication().Run(UiProvider.NewModuleManagerWindow());
		}

		public virtual void  ShutDown()
		{
			lockFileStream.Close();
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'SocketListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class SocketListener : IThreadRunnable
		{

			private ModuleManager _moduleManager;
			public ModuleManager ModuleManager
			{
				get
				{
					return _moduleManager;
				}
				
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'serverSocket '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private System.Net.Sockets.TcpListener serverSocket;
			
			public SocketListener(ModuleManager moduleManager, System.Net.Sockets.TcpListener serverSocket)
			{
				this._moduleManager = moduleManager;
				this.serverSocket = serverSocket;
			}
			
			public virtual void  Run()
			{
				try
				{
					//UPGRADE_TODO: Class 'java.io.ObjectInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectInputStream'"
					System.IO.BinaryReader in_Renamed = null;
					//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
					System.IO.StreamWriter out_Renamed = null;
					System.Net.Sockets.TcpClient clientSocket = null;
					while (true)
					{
						try
						{
							clientSocket = serverSocket.AcceptTcpClient();
							//UPGRADE_TODO: Class 'java.io.ObjectInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectInputStream'"
							in_Renamed = new System.IO.BinaryReader(new System.IO.BufferedStream(clientSocket.GetStream()));
							
							//UPGRADE_NOTE: Final was removed from the declaration of 'message '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							//UPGRADE_WARNING: Method 'java.io.ObjectInputStream.readObject' was converted to 'SupportClass.Deserialize' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
							string message = ModuleManager.execute(SupportClass.Deserialize(in_Renamed));
							in_Renamed.Close();
							clientSocket.Close();
							
							if (message == null || !clientSocket.Connected)
								continue;
							
							//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
							out_Renamed = new System.IO.StreamWriter(new System.IO.BufferedStream(clientSocket.GetStream()));
							out_Renamed.WriteLine(message);
							out_Renamed.Close();
						}
						catch (System.IO.IOException e)
						{
							ErrorDialog.showDetails(e, ThrowableUtils.getStackTrace(e), "Error.socket_error");
						}
						//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
						catch (System.Exception e)
						{
							ErrorDialog.bug(e);
						}
						finally
						{
							IOUtils.closeQuietly(in_Renamed);
							IOUtils.closeQuietly(out_Renamed);
							IOUtils.closeQuietly(clientSocket);
						}
					}
				}
				finally
				{
					IOUtils.closeQuietly(serverSocket);
				}
			}
		}
		
		protected internal virtual string execute(System.Object req)
		{
			if (req is LaunchRequest)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'lr '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				LaunchRequest lr = (LaunchRequest) req;
				
				if (lr.key != key)
				{
					// FIXME: translate
					return "incorrect key";
				}
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'handler '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				LaunchRequestHandler handler = new LaunchRequestHandler(lr);
				try
				{
					//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeAndWait' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
					SwingUtilities.invokeAndWait(handler);
				}
				catch (System.Threading.ThreadInterruptedException e)
				{
					return "interrupted"; // FIXME
				}
				catch (System.Reflection.TargetInvocationException e)
				{
					ErrorDialog.bug(e);
					return null;
				}
				
				return handler.Result;
			}
			else
			{
				return "unrecognized command"; // FIXME
			}
		}
		
		private class LaunchRequestHandler : IThreadRunnable
		{
			virtual public string Result
			{
				get
				{
					return result;
				}
				
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'lr '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private LaunchRequest lr;
			private string result;
			
			public LaunchRequestHandler(LaunchRequest lr)
			{
				this.lr = lr;
			}
			
			public virtual void  Run()
			{
				result = handle();
			}
			
			private string handle()
			{
				////UPGRADE_NOTE: Final was removed from the declaration of 'window '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//ModuleManagerWindow window = ModuleManagerWindow.Instance;
				
				//switch (lr.mode)
				//{
					
				//	case LaunchRequest.Mode.MANAGE: 
				//		window.BringToFront();
				//		break;
					
				//	case LaunchRequest.Mode.LOAD: 
				//		if (Player.LaunchAction.isEditing(lr.module))
				//			return "module open for editing"; // FIXME
						
				//		if (lr.module == null && lr.game != null)
				//		{
				//			// attempt to find the module for the saved game or log
				//			//UPGRADE_NOTE: Final was removed from the declaration of 'data '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//			AbstractMetaData data = MetaDataFactory.buildMetaData(lr.game);
				//			if (data != null && data is SaveMetaData)
				//			{
				//				// we found save metadata
				//				//UPGRADE_NOTE: Final was removed from the declaration of 'moduleName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//				string moduleName = ((SaveMetaData) data).ModuleName;
				//				if (moduleName != null && moduleName.Length > 0)
				//				{
				//					// get the module file by module name
				//					lr.module = window.getModuleByName(moduleName);
				//				}
				//				else
				//				{
				//					// this is a pre 3.1 save file, can't tell the module name
				//					// FIXME: show some error here
				//					return "cannot find module";
				//				}
				//			}
				//		}
						
				//		if (lr.module == null)
				//		{
				//			return "cannot find module";
				//			// FIXME: show some error here
				//		}
				//		else if (lr.game == null)
				//		{
				//			new Player.LaunchAction(window, lr.module).actionPerformed(null);
				//		}
				//		else
				//		{
				//			new Player.LaunchAction(window, lr.module, lr.game).actionPerformed(null);
				//		}
				//		break;
					
				//	case LaunchRequest.Mode.EDIT: 
				//		if (Editor.LaunchAction.isInUse(lr.module))
				//			return "module open for play"; // FIXME
				//		if (Editor.LaunchAction.isEditing(lr.module))
				//			return "module open for editing"; // FIXME
						
				//		new Editor.LaunchAction(window, lr.module).actionPerformed(null);
				//		break;
					
				//	case LaunchRequest.Mode.IMPORT: 
				//		//UPGRADE_TODO: Method 'javax.swing.AbstractAction.actionPerformed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				//		new Editor.ImportLaunchAction(window, lr.importFile).actionPerformed(null);
				//		break;
					
				//	case LaunchRequest.Mode.NEW: 
				//		//UPGRADE_TODO: Method 'javax.swing.AbstractAction.actionPerformed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				//		new Editor.NewModuleLaunchAction(window).actionPerformed(null);
				//		break;
					
				//	case LaunchRequest.Mode.EDIT_EXT: 
				//		return "not yet implemented"; // FIXME
					
				//	case LaunchRequest.Mode.NEW_EXT: 
				//		return "not yet implemented"; // FIXME
					
				//	default: 
				//		return "unrecognized mode"; // FIXME
					
				//}
				
				return null;
			}
		}
	}
}