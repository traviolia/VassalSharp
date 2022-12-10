/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney
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
using Command = VassalSharp.command.Command;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using Localization = VassalSharp.i18n.Localization;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
namespace VassalSharp.counters
{
	
	[Serializable]
	public class KeyCommand:SupportClass.ActionSupport
	{
		virtual public System.String Name
		{
			get
			{
				return name;
			}
			
		}
		virtual public System.Windows.Forms.KeyEventArgs KeyStroke
		{
			get
			{
				return stroke;
			}
			
		}
		virtual public NamedKeyStroke NamedKeyStroke
		{
			get
			{
				return namedKeyStroke;
			}
			
		}
		virtual public GamePiece Target
		{
			get
			{
				return target;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> If true, then this action will apply to all selected pieces</summary>
		/// <returns>
		/// </returns>
		/// <summary> If true, then this action will apply to all selected pieces</summary>
		/// <param name="global">
		/// </param>
		virtual public bool Global
		{
			get
			{
				return global;
			}
			
			set
			{
				this.global = value;
			}
			
		}
		/// <summary> The human-readable text that will appear in the right-click menu, translated to the user's Locale</summary>
		/// <returns>
		/// </returns>
		virtual public System.String LocalizedMenuText
		{
			get
			{
				if (localizedMenuText == null && name != null)
				{
					System.String localizedName = name;
					if (i18nPiece != null && GameModule.getGameModule().isLocalizationEnabled())
					{
						System.String key = null;
						//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
						for(PieceI18nData.Property p: i18nPiece.getI18nData().getProperties())
						{
							if (p.Name.equals(name))
							{
								key = VassalSharp.i18n.TranslatablePiece_Fields.PREFIX + p.Name;
							}
						}
						if (key != null)
						{
							localizedName = Localization.Instance.translate(key, name);
						}
					}
					localizedMenuText = stroke == null?localizedName:localizedName + "  " + NamedHotKeyConfigurer.getString(stroke);
				}
				return localizedMenuText;
			}
			
		}
		private const long serialVersionUID = 1L;
		
		private System.String name;
		protected internal System.String untranslatedName;
		protected internal System.String localizedMenuText;
		private System.Windows.Forms.KeyEventArgs stroke;
		private GamePiece target;
		private bool global;
		new private bool enabled = true;
		
		protected internal TranslatablePiece i18nPiece;
		protected internal NamedKeyStroke namedKeyStroke;
		
		public KeyCommand(System.String name, System.Windows.Forms.KeyEventArgs key, GamePiece target):this(name, key, target, null)
		{
		}
		
		public KeyCommand(System.String name, NamedKeyStroke key, GamePiece target):this(name, key, target, null)
		{
		}
		
		//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
		public KeyCommand(System.String name, System.Windows.Forms.KeyEventArgs key, GamePiece target, TranslatablePiece i18nPiece):base(key == null?name:name + "  " + NamedHotKeyConfigurer.getString(key))
		{
			this.target = target;
			this.name = name;
			this.stroke = key;
			this.i18nPiece = i18nPiece;
		}
		
		public KeyCommand(System.String name, NamedKeyStroke key, GamePiece target, TranslatablePiece i18nPiece):this(name, key == null?null:key.KeyStroke, target, i18nPiece)
		{
			namedKeyStroke = key == null?NamedKeyStroke.NULL_KEYSTROKE:key;
		}
		
		public KeyCommand(System.String name, NamedKeyStroke key, GamePiece target, bool enabled):this(name, key == null?null:key.KeyStroke, target, enabled)
		{
			namedKeyStroke = key == null?NamedKeyStroke.NULL_KEYSTROKE:key;
		}
		
		public KeyCommand(System.String name, System.Windows.Forms.KeyEventArgs key, GamePiece target, bool enabled):this(name, key, target, null)
		{
			setEnabled(enabled);
		}
		
		public KeyCommand(KeyCommand command):this(command.name, command.stroke, command.target, command.isEnabled())
		{
			this.i18nPiece = command.i18nPiece;
		}
		
		public virtual bool matches(System.Windows.Forms.KeyEventArgs key)
		{
			return isEnabled() && key != null && key.Equals(stroke);
		}
		
		//UPGRADE_NOTE: The equivalent of method 'javax.swing.AbstractAction.isEnabled' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public bool isEnabled()
		{
			return enabled;
		}
		
		//UPGRADE_NOTE: The equivalent of method 'javax.swing.AbstractAction.setEnabled' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public void  setEnabled(bool b)
		{
			enabled = b;
		}
		
		public override void  actionPerformed(System.Object event_sender, System.EventArgs evt)
		{
			if (stroke != null)
			{
				if (global)
				{
					GameModule.getGameModule().sendAndLog(KeyBuffer.Buffer.keyCommand(stroke));
				}
				else
				{
					BoundsTracker t = new BoundsTracker();
					GamePiece outer = Decorator.getOutermost(target);
					t.addPiece(outer);
					outer.setProperty(VassalSharp.counters.Properties_Fields.SNAPSHOT, PieceCloner.Instance.clonePiece(outer)); // save state prior to command
					Command c = outer.keyEvent(stroke);
					if (target.Id != null)
					{
						GameModule.getGameModule().sendAndLog(c);
					}
					t.addPiece(outer);
					t.repaint();
				}
			}
		}
	}
}