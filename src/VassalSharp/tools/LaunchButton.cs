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
using Configurer = VassalSharp.configure.Configurer;
using IconConfigurer = VassalSharp.configure.IconConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using Localization = VassalSharp.i18n.Localization;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.tools
{
	
	/// <summary> A JButton for placing into a VASSAL component's toolbar.
	/// Handles configuration of a hotkey shortcut, maintains appropriate
	/// tooltip text, etc.
	/// </summary>
	[Serializable]
	public class LaunchButton:System.Windows.Forms.Button
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(java.awt.event_Renamed.ActionListener al, LaunchButton enclosingInstance)
			{
				InitBlock(al, enclosingInstance);
			}
			private void  InitBlock(java.awt.event_Renamed.ActionListener al, LaunchButton enclosingInstance)
			{
				this.al = al;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			//UPGRADE_NOTE: Final variable al was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private java.awt.event_Renamed.ActionListener al;
			private LaunchButton enclosingInstance;
			public LaunchButton Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				if (Enclosing_Instance.Enabled && Enclosing_Instance.Parent != null && Enclosing_Instance.Parent.Visible)
				{
					//UPGRADE_TODO: Method 'java.awt.event.ActionListener.actionPerformed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					al.actionPerformed(event_sender, e);
				}
			}
		}
		virtual public System.String NameAttribute
		{
			get
			{
				return nameAtt;
			}
			
		}
		virtual public System.String HotkeyAttribute
		{
			get
			{
				return keyAtt;
			}
			
		}
		virtual public System.String IconAttribute
		{
			get
			{
				return iconAtt;
			}
			
		}
		virtual public Configurer NameConfigurer
		{
			get
			{
				if (nameConfig == null && nameAtt != null)
				{
					nameConfig = new StringConfigurer(nameAtt, Resources.getString("Editor.button_text_label"), Text); //$NON-NLS-1$
				}
				return nameConfig;
			}
			
		}
		virtual public Configurer HotkeyConfigurer
		{
			get
			{
				if (keyConfig == null && keyAtt != null)
				{
					keyConfig = new NamedHotKeyConfigurer(keyAtt, Resources.getString("Editor.hotkey_label"), keyListener.NamedKeyStroke); //$NON-NLS-1$
				}
				return keyConfig;
			}
			
		}
		private const long serialVersionUID = 1L;
		public const System.String UNTRANSLATED_TEXT = "unTranslatedText"; //$NON-NLS-1$
		protected internal System.String tooltipAtt;
		protected internal System.String nameAtt;
		protected internal System.String keyAtt;
		protected internal System.String iconAtt;
		protected internal IconConfigurer iconConfig;
		protected internal System.String toolTipText;
		protected internal NamedKeyStrokeListener keyListener;
		protected internal Configurer nameConfig, keyConfig;
		
		//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public LaunchButton(System.String text, System.String textAttribute, System.String hotkeyAttribute, ActionListener al):this(text, textAttribute, hotkeyAttribute, null, al)
		{
		}
		
		//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public LaunchButton(System.String text, System.String tooltipAttribute, System.String textAttribute, System.String hotkeyAttribute, System.String iconAttribute, ActionListener al):this(text, textAttribute, hotkeyAttribute, iconAttribute, al)
		{
			tooltipAtt = tooltipAttribute;
		}
		
		//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public LaunchButton(System.String text, System.String textAttribute, System.String hotkeyAttribute, System.String iconAttribute, ActionListener al):base()
		{
			this.Text = text;
			nameAtt = textAttribute;
			keyAtt = hotkeyAttribute;
			iconAtt = iconAttribute;
			iconConfig = new IconConfigurer(iconAtt, null, null);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentY_float'"
			setAlignmentY(0.0F);
			keyListener = new NamedKeyStrokeListener(new AnonymousClassActionListener(al, this));
			if (al != null)
			{
				GameModule.getGameModule().addKeyStrokeListener(keyListener);
				Click += new System.EventHandler(al.actionPerformed);
				SupportClass.CommandManager.CheckCommand(this);
			}
			setFocusable(false);
			checkVisibility();
		}
		
		public virtual System.String getAttributeValueString(System.String key)
		{
			if (key.Equals(nameAtt))
			{
				return Text;
			}
			else if (key.Equals(keyAtt))
			{
				return NamedHotKeyConfigurer.encode(keyListener.NamedKeyStroke);
			}
			else if (key.Equals(iconAtt))
			{
				return iconConfig.ValueString;
			}
			else if (key.Equals(tooltipAtt))
			{
				return toolTipText;
			}
			else
			{
				return null;
			}
		}
		
		public virtual void  setAttribute(System.String key, System.Object value_Renamed)
		{
			if (key != null)
			{
				if (key.Equals(nameAtt))
				{
					if (Localization.Instance.TranslationInProgress)
					{
						//UPGRADE_ISSUE: Method 'javax.swing.JComponent.putClientProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentputClientProperty_javalangObject_javalangObject'"
						putClientProperty(UNTRANSLATED_TEXT, Text);
					}
					Text = (System.String) value_Renamed;
					checkVisibility();
				}
				else if (key.Equals(keyAtt))
				{
					if (value_Renamed is System.String)
					{
						value_Renamed = NamedHotKeyConfigurer.decode((System.String) value_Renamed);
					}
					if (value_Renamed is NamedKeyStroke)
					{
						keyListener.setKeyStroke((NamedKeyStroke) value_Renamed);
					}
					else
					{
						keyListener.setKeyStroke((System.Windows.Forms.KeyEventArgs) value_Renamed); // Compatibility - custom code
					}
					setToolTipText(toolTipText);
				}
				else if (key.Equals(tooltipAtt))
				{
					toolTipText = ((System.String) value_Renamed);
					setToolTipText(toolTipText);
				}
				else if (key.Equals(iconAtt))
				{
					if (value_Renamed is System.String)
					{
						iconConfig.setValue((System.String) value_Renamed);
						Image = iconConfig.IconValue;
					}
					checkVisibility();
				}
			}
		}
		
		//UPGRADE_NOTE: The equivalent of method 'javax.swing.JComponent.setToolTipText' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public void  setToolTipText(System.String text)
		{
			toolTipText = text;
			if (keyListener.getKeyStroke() != null)
			{
				text = (text == null?"":text + " "); //$NON-NLS-1$ //$NON-NLS-2$
				if (!keyListener.NamedKeyStroke.Named)
				{
					text += ("[" + NamedHotKeyConfigurer.getString(keyListener.getKeyStroke()) + "]"); //$NON-NLS-1$ //$NON-NLS-2$
				}
			}
			SupportClass.ToolTipSupport.setToolTipText((System.Windows.Forms.Button) this, text);
		}
		
		protected internal virtual void  checkVisibility()
		{
			Visible = (Text != null && Text.Length > 0) || Image != null;
		}
	}
}