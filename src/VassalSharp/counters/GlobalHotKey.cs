using System;
using GameModule = VassalSharp.build.GameModule;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Command = VassalSharp.command.Command;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> Adds a menu entry that fires a specified key event to the module window.
	/// Effectively allows a Game Piece to activate a button in the toolbar
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class GlobalHotKey:Decorator, TranslatablePiece
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
				return (description == null || description.Length == 0)?"Global Hotkey":"Global Hotkey:  " + description;
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("GlobalHotKey.htm");
			}
			
		}
		public const System.String ID = "globalhotkey;";
		
		protected internal NamedKeyStroke commandKey;
		protected internal NamedKeyStroke globalHotKey;
		protected internal System.String commandName = "Hotkey";
		protected internal KeyCommand[] commands;
		protected internal KeyCommand command;
		protected internal System.String description = "";
		
		public GlobalHotKey():this(ID, null)
		{
		}
		
		public GlobalHotKey(System.String type, GamePiece inner)
		{
			mySetType(type);
			setInner(inner);
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			if (commands == null)
			{
				command = new KeyCommand(commandName, commandKey, Decorator.getOutermost(this), this);
				command.setEnabled(getMap() != null);
				if (commandName != null && commandName.Length > 0 && commandKey != null && !commandKey.Null)
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
		
		public override System.String myGetState()
		{
			return "";
		}
		
		public override System.String myGetType()
		{
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(commandName).append(commandKey).append(globalHotKey).append(description);
			return ID + se.Value;
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			myGetKeyCommands();
			if (command.matches(stroke))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'gm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GameModule gm = GameModule.getGameModule();
				//UPGRADE_NOTE: Final was removed from the declaration of 'loggingPausedByMe '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				bool loggingPausedByMe = gm.pauseLogging();
				GameModule.getGameModule().fireKeyStroke(globalHotKey);
				if (loggingPausedByMe)
				{
					return gm.resumeLogging();
				}
			}
			return null;
		}
		
		public override void  mySetState(System.String newState)
		{
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			return piece.boundingBox();
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			piece.draw(g, x, y, obs, zoom);
		}
		
		public override System.String getName()
		{
			return piece.getName();
		}
		
		public virtual void  mySetType(System.String type)
		{
			SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(type.Substring(ID.Length), ';');
			commandName = sd.nextToken();
			commandKey = sd.nextNamedKeyStroke('H');
			globalHotKey = sd.nextNamedKeyStroke(null);
			description = sd.nextToken("");
			commands = null;
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		public override PieceI18nData getI18nData()
		{
			return getI18nData(commandName, Description + " command");
		}
		
		public class Ed : PieceEditor
		{
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return controls;
				}
				
			}
			virtual public System.String State
			{
				get
				{
					return "";
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					SequenceEncoder se = new SequenceEncoder(';');
					se.append(commandConfig.ValueString).append(commandKeyConfig.ValueString).append(hotKeyConfig.ValueString).append(descConfig.ValueString);
					return VassalSharp.counters.GlobalHotKey.ID + se.Value;
				}
				
			}
			
			private StringConfigurer commandConfig;
			private NamedHotKeyConfigurer commandKeyConfig;
			private NamedHotKeyConfigurer hotKeyConfig;
			protected internal StringConfigurer descConfig;
			
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			private Box controls;
			
			public Ed(GlobalHotKey k)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				controls = Box.createVerticalBox();
				
				descConfig = new StringConfigurer(null, "Description:  ", k.description);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(descConfig.Controls);
				
				commandConfig = new StringConfigurer(null, "Menu text:  ", k.commandName);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(commandConfig.Controls);
				
				commandKeyConfig = new NamedHotKeyConfigurer(null, "Keyboard Command:  ", k.commandKey);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(commandKeyConfig.Controls);
				
				hotKeyConfig = new NamedHotKeyConfigurer(null, "Global Hotkey:  ", k.globalHotKey);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(hotKeyConfig.Controls);
			}
		}
	}
}