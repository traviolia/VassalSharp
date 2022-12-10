/*
* $Id$
*
* Copyright (c) 2008-2009 by Brent Easton
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
//UPGRADE_TODO: The type 'javazoom.jl.decoder.JavaLayerException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using JavaLayerException = javazoom.jl.decoder.JavaLayerException;
//UPGRADE_TODO: The type 'javazoom.jl.player.Player' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Player = javazoom.jl.player.Player;
using BadDataReport = VassalSharp.build.BadDataReport;
using GameModule = VassalSharp.build.GameModule;
using Resources = VassalSharp.i18n.Resources;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.tools
{
	
	//UPGRADE_ISSUE: Interface 'java.applet.AudioClip' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaappletAudioClip'"
	public class Mp3AudioClip : AudioClip
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassThread' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassThread:SupportClass.ThreadClass
		{
			public AnonymousClassThread(Mp3AudioClip enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Mp3AudioClip enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Mp3AudioClip enclosingInstance;
			public Mp3AudioClip Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			override public void  Run()
			{
				try
				{
					Enclosing_Instance.player.play();
				}
				catch (JavaLayerException e)
				{
					ErrorDialog.bug(e);
				}
				finally
				{
					IOUtils.closeQuietly(Enclosing_Instance.stream);
				}
			}
		}
		
		protected internal System.String name;
		protected internal Player player = null;
		protected internal System.IO.Stream stream = null;
		
		public Mp3AudioClip(System.String name)
		{
			this.name = name;
		}
		
		public virtual void  play()
		{
			// load the stream
			stream = null;
			try
			{
				stream = GameModule.getGameModule().getDataArchive().getInputStream(name);
			}
			catch (System.IO.FileNotFoundException e)
			{
				ErrorDialog.dataError(new BadDataReport(Resources.getString("Error.not_found", name), "", e));
				return ;
			}
			catch (System.IO.IOException e)
			{
				ErrorDialog.bug(e);
				return ;
			}
			
			// create the player
			player = null;
			try
			{
				player = new Player(stream);
			}
			catch (JavaLayerException e)
			{
				ErrorDialog.bug(e);
				return ;
			}
			finally
			{
				if (player == null)
				{
					// close the stream if player ctor fails
					// otherwise, keep it open for the thread to close
					IOUtils.closeQuietly(stream);
				}
			}
			
			// run in new thread to play in background
			new AnonymousClassThread(this).Start();
		}
		
		public virtual void  stop()
		{
			if (player != null)
			{
				try
				{
					player.close();
				}
				finally
				{
					IOUtils.closeQuietly(stream);
				}
			}
		}
		
		public virtual void  loop()
		{
			// Not used by Vassal
		}
	}
}