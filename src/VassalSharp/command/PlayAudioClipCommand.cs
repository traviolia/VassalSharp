/*
* $Id$
*
* Copyright (c) 2000-2006 by Rodney Kinney
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
using GameModule = VassalSharp.build.GameModule;
using ReadErrorDialog = VassalSharp.tools.ReadErrorDialog;
namespace VassalSharp.command
{
	
	public class PlayAudioClipCommand:Command
	{
		public const System.String COMMAND_PREFIX = "AUDIO\t";
		private System.String clipName;
		
		public PlayAudioClipCommand(System.String clipName)
		{
			this.clipName = clipName;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override void  executeCommand()
		{
			try
			{
				GameModule.getGameModule().getDataArchive().getCachedAudioClip(clipName).play();
			}
			catch (System.IO.IOException e)
			{
				ReadErrorDialog.error(e, clipName);
			}
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override Command myUndoCommand()
		{
			return null;
		}
		
		public static PlayAudioClipCommand decode(System.String s)
		{
			if (s.StartsWith(COMMAND_PREFIX))
			{
				return new PlayAudioClipCommand(s.Substring(COMMAND_PREFIX.Length));
			}
			else
			{
				return null;
			}
		}
		
		public virtual System.String encode()
		{
			return COMMAND_PREFIX + clipName;
		}
	}
}