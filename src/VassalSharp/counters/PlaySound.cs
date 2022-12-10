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
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Command = VassalSharp.command.Command;
using PlayAudioClipCommand = VassalSharp.command.PlayAudioClipCommand;
using AudioClipConfigurer = VassalSharp.configure.AudioClipConfigurer;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using Resources = VassalSharp.i18n.Resources;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using FormattedString = VassalSharp.tools.FormattedString;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> A trait that plays a sound clip
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class PlaySound:Decorator, TranslatablePiece
	{
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		override public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				return piece.Shape;
			}
			
		}
		virtual public System.String Description
		{
			get
			{
				return format.Format.Length == 0?"Play Sound":"Play Sound - " + format.Format;
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("PlaySound.htm");
			}
			
		}
		public const System.String ID = "playSound;";
		protected internal System.String menuText;
		protected internal NamedKeyStroke stroke;
		protected internal bool sendToOthers;
		protected internal KeyCommand command;
		protected internal KeyCommand[] commands;
		protected internal FormattedString format = new FormattedString();
		
		public PlaySound():this(ID, null)
		{
		}
		
		public PlaySound(System.String type, GamePiece piece)
		{
			mySetType(type);
			setInner(piece);
		}
		
		public override void  mySetState(System.String newState)
		{
		}
		
		public override System.String myGetState()
		{
			return "";
		}
		
		public override System.String myGetType()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(format.Format).append(menuText).append(stroke).append(sendToOthers);
			return ID + se.Value;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			if (commands == null)
			{
				command = new KeyCommand(menuText, stroke, Decorator.getOutermost(this), this);
				if (menuText.Length > 0 && stroke != null && !stroke.Null)
				{
					commands = new KeyCommand[]{command};
				}
				else
				{
					commands = new KeyCommand[0];
				}
			}
			return commands;
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			myGetKeyCommands();
			Command c = null;
			if (command.matches(stroke))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'clipName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String clipName = format.getText(Decorator.getOutermost(this));
				c = new PlayAudioClipCommand(clipName);
				try
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'clip '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_ISSUE: Interface 'java.applet.AudioClip' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaappletAudioClip'"
					AudioClip clip = GameModule.getGameModule().getDataArchive().getCachedAudioClip(clipName);
					if (clip != null)
					{
						//UPGRADE_ISSUE: Method 'java.applet.AudioClip.play' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaappletAudioClip'"
						clip.play();
					}
				}
				catch (System.IO.IOException e)
				{
					reportDataError(this, Resources.getString("Error.not_found", "Audio Clip"), "Clip=" + clipName, e);
				}
			}
			return c;
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			piece.draw(g, x, y, obs, zoom);
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			return piece.boundingBox();
		}
		
		public override System.String getName()
		{
			return piece.getName();
		}
		
		public virtual void  mySetType(System.String type)
		{
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			st.nextToken();
			format = new FormattedString(st.nextToken(""));
			menuText = st.nextToken("Play Sound");
			stroke = st.nextNamedKeyStroke('P');
			sendToOthers = st.nextBoolean(false);
			commands = null;
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		public override PieceI18nData getI18nData()
		{
			return getI18nData(menuText, "Play Sound command");
		}
		
		public class Ed : PieceEditor
		{
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return panel;
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					SequenceEncoder se = new SequenceEncoder(';');
					se.append(soundConfig.ValueString).append(menuConfig.ValueString).append(keyConfig.ValueString).append(sendConfig.ValueString);
					return VassalSharp.counters.PlaySound.ID + se.Value;
				}
				
			}
			virtual public System.String State
			{
				get
				{
					return "";
				}
				
			}
			private StringConfigurer menuConfig;
			private NamedHotKeyConfigurer keyConfig;
			private AudioClipConfigurer soundConfig;
			private BooleanConfigurer sendConfig;
			private System.Windows.Forms.Panel panel;
			
			public Ed(PlaySound p)
			{
				menuConfig = new StringConfigurer(null, "Menu Text:  ", p.menuText);
				keyConfig = new NamedHotKeyConfigurer(null, "Keyboard Command:  ", p.stroke);
				soundConfig = new AudioClipConfigurer(null, "Sound Clip:  ", GameModule.getGameModule().getArchiveWriter());
				soundConfig.setValue(p.format.Format);
				soundConfig.Editable = true;
				sendConfig = new BooleanConfigurer(null, "Send sound to other players?", p.sendToOthers);
				panel = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				panel.setLayout(new BoxLayout(panel, BoxLayout.Y_AXIS));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(menuConfig.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(keyConfig.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(soundConfig.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(sendConfig.Controls);
			}
		}
	}
}