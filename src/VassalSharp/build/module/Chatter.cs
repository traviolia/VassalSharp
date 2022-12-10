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
using Buildable = VassalSharp.build.Buildable;
using GameModule = VassalSharp.build.GameModule;
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
using ColorConfigurer = VassalSharp.configure.ColorConfigurer;
using FontConfigurer = VassalSharp.configure.FontConfigurer;
using Resources = VassalSharp.i18n.Resources;
using Prefs = VassalSharp.preferences.Prefs;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using KeyStrokeSource = VassalSharp.tools.KeyStrokeSource;
using ScrollPane = VassalSharp.tools.ScrollPane;
namespace VassalSharp.build.module
{
	
	/// <summary> The chat window component.  Displays text messages and
	/// accepts input.  Also acts as a {@link CommandEncoder},
	/// encoding/decoding commands that display message in the text area
	/// </summary>
	[Serializable]
	public class Chatter:System.Windows.Forms.Panel, CommandEncoder, Buildable
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassComponentAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassComponentAdapter
		{
			public AnonymousClassComponentAdapter(Chatter enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Chatter enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Chatter enclosingInstance;
			public Chatter Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  componentResized(System.Object event_sender, System.EventArgs e)
			{
				int temp_Maxsize;
				//UPGRADE_ISSUE: Method 'javax.swing.JScrollPane.getVerticalScrollBar' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanegetVerticalScrollBar'"
				//UPGRADE_WARNING: Method javax.swing.JScrollbar.setValue was converted to 'System.Windows.Forms.ScrollBar.Value' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
				//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
				temp_Maxsize = Enclosing_Instance.scroll.getVerticalScrollBar().Maximum - Enclosing_Instance.scroll.getVerticalScrollBar().LargeChange;
				Enclosing_Instance.scroll.getVerticalScrollBar().Value = Enclosing_Instance.scroll.getVerticalScrollBar().Maximum > temp_Maxsize?temp_Maxsize:Enclosing_Instance.scroll.getVerticalScrollBar().Maximum;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(Chatter enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Chatter enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Chatter enclosingInstance;
			public Chatter Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.send(Enclosing_Instance.formatChat(SupportClass.CommandManager.GetCommand(event_sender)));
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				Enclosing_Instance.input.Text = ""; //$NON-NLS-1$
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(Chatter enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Chatter enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Chatter enclosingInstance;
			public Chatter Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				Enclosing_Instance.Font = (System.Drawing.Font) evt.NewValue;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener1
		{
			public AnonymousClassPropertyChangeListener1(Chatter enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Chatter enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Chatter enclosingInstance;
			public Chatter Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
			{
				Enclosing_Instance.gameMsg = (System.Drawing.Color) e.NewValue;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener2
		{
			public AnonymousClassPropertyChangeListener2(Chatter enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Chatter enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Chatter enclosingInstance;
			public Chatter Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
			{
				Enclosing_Instance.systemMsg = (System.Drawing.Color) e.NewValue;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener3
		{
			public AnonymousClassPropertyChangeListener3(Chatter enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Chatter enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Chatter enclosingInstance;
			public Chatter Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
			{
				Enclosing_Instance.myChat = (System.Drawing.Color) e.NewValue;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener4' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener4
		{
			public AnonymousClassPropertyChangeListener4(Chatter enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Chatter enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Chatter enclosingInstance;
			public Chatter Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
			{
				Enclosing_Instance.otherChat = (System.Drawing.Color) e.NewValue;
			}
		}
		public static System.String AnonymousUserName
		{
			get
			{
				return Resources.getString("Chat.anonymous"); //$NON-NLS-1$
			}
			
		}
		virtual public System.Windows.Forms.TextBox InputField
		{
			get
			{
				return input;
			}
			
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		/// <summary> Set the Font used by the text area</summary>
		public override System.Drawing.Font Font
		{
			get
			{
				return base.Font;
			}
			
			set
			{
				if (input != null)
				{
					if (input.Text.Length == 0)
					{
						//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
						input.Text = "XXX"; //$NON-NLS-1$
						input.Font = value;
						//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
						input.Text = ""; //$NON-NLS-1$
					}
					else
						input.Font = value;
				}
				if (conversation != null)
				{
					conversation.Font = value;
				}
			}
			
		}
		private const long serialVersionUID = 1L;
		
		protected internal System.Windows.Forms.TextBox conversation;
		protected internal System.Windows.Forms.TextBox input;
		//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		//UPGRADE_ISSUE: Field 'javax.swing.ScrollPaneConstants.VERTICAL_SCROLLBAR_ALWAYS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingScrollPaneConstants'"
		//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.HORIZONTAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsHORIZONTAL_SCROLLBAR_AS_NEEDED_f'"
		protected internal System.Windows.Forms.ScrollableControl scroll = new ScrollPane(JScrollPane.VERTICAL_SCROLLBAR_ALWAYS, true);
		protected internal const System.String MY_CHAT_COLOR = "myChatColor"; //$NON-NLS-1$
		protected internal const System.String OTHER_CHAT_COLOR = "otherChatColor"; //$NON-NLS-1$
		protected internal const System.String GAME_MSG_COLOR = "gameMessageColor"; //$NON-NLS-1$
		protected internal const System.String SYS_MSG_COLOR = "systemMessageColor"; //$NON-NLS-1$
		
		protected internal System.Drawing.Color gameMsg
		{
			get
			{
				return gameMsg_Renamed;
			}
			
			set
			{
				gameMsg_Renamed = value;
			}
			
		}
		protected internal System.Drawing.Color gameMsg_Renamed;
		protected internal System.Drawing.Color systemMsg
		{
			get
			{
				return systemMsg_Renamed;
			}
			
			set
			{
				systemMsg_Renamed = value;
			}
			
		}
		protected internal System.Drawing.Color systemMsg_Renamed;
		protected internal System.Drawing.Color myChat
		{
			get
			{
				return myChat_Renamed;
			}
			
			set
			{
				myChat_Renamed = value;
			}
			
		}
		protected internal System.Drawing.Color myChat_Renamed;
		protected internal System.Drawing.Color otherChat
		{
			get
			{
				return otherChat_Renamed;
			}
			
			set
			{
				otherChat_Renamed = value;
			}
			
		}
		protected internal System.Drawing.Color otherChat_Renamed;
		
		public Chatter()
		{
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
			System.Windows.Forms.TextBox temp_TextBox;
			temp_TextBox = new System.Windows.Forms.TextBox();
			temp_TextBox.Multiline = true;
			temp_TextBox.WordWrap = false;
			temp_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			conversation = temp_TextBox;
			for (int i = 0; i < 15; ++i)
			{
				conversation.AppendText("\n"); //$NON-NLS-1$
			}
			conversation.ReadOnly = !false;
			conversation.WordWrap = true;
			conversation.WordWrap = true;
			//UPGRADE_ISSUE: Method 'javax.swing.text.JTextComponent.setUI' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextJTextComponentsetUI_javaxswingplafTextUI'"
			conversation.setUI(new UI(this));
			conversation.Resize += new System.EventHandler(new AnonymousClassComponentAdapter(this).componentResized);
			//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
			input = new System.Windows.Forms.TextBox();
			input.setFocusTraversalKeysEnabled(false);
			//UPGRADE_TODO: Method 'javax.swing.JTextField.addActionListener' was converted to 'System.Windows.Forms.TextBox.KeyPress' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldaddActionListener_javaawteventActionListener'"
			input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(new AnonymousClassActionListener(this).actionPerformed);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetMaximumSize'"
			input.setMaximumSize(new System.Drawing.Size(input.getMaximumSize().Width, input.Size.Height));
			scroll.Controls.Add(conversation);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(scroll);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(input);
		}
		
		private System.String formatChat(System.String text)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'id '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String id = GlobalOptions.Instance.getPlayerId();
			return "<" + (id.Length == 0?"(" + AnonymousUserName + ")":id) + "> - " + text; //$NON-NLS-1$ //$NON-NLS-2$
		}
		
		/// <summary> Display a message in the text area</summary>
		public virtual void  show(System.String s)
		{
			conversation.AppendText("\n" + s); //$NON-NLS-1$
		}
		
		/// <deprecated> use GlobalOptions.getPlayerId() 
		/// </deprecated>
		public virtual void  setHandle(System.String s)
		{
		}
		
		/// <deprecated> use GlobalOptions.getPlayerId() 
		/// </deprecated>
		public virtual System.String getHandle()
		{
			return GlobalOptions.Instance.getPlayerId();
		}
		
		public virtual void  build(System.Xml.XmlElement e)
		{
		}
		
		public virtual System.Xml.XmlElement getBuildElement(System.Xml.XmlDocument doc)
		{
			return doc.CreateElement(GetType().FullName);
		}
		
		/// <summary> Expects to be added to a GameModule.  Adds itself to the
		/// controls window and registers itself as a
		/// {@link CommandEncoder} 
		/// </summary>
		public virtual void  addTo(Buildable b)
		{
			GameModule mod = (GameModule) b;
			mod.setChatter(this);
			mod.addCommandEncoder(this);
			mod.addKeyStrokeSource(new KeyStrokeSource(this, 1));
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'chatFont '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FontConfigurer chatFont = new FontConfigurer("ChatFont", Resources.getString("Chatter.chat_font_preference"));
			chatFont.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
			
			mod.getControlPanel().add(this, System.Windows.Forms.DockStyle.Fill);
			
			chatFont.fireUpdate();
			mod.getPrefs().addOption(Resources.getString("Chatter.chat_window"), chatFont); //$NON-NLS-1$
			
			//Bug 10179 - Do not re-read Chat colors each time the Chat Window is repainted.
			//UPGRADE_NOTE: Final was removed from the declaration of 'globalPrefs '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Prefs globalPrefs = Prefs.GlobalPrefs;
			
			//
			// game message color
			//
			//UPGRADE_NOTE: Final was removed from the declaration of 'gameMsgColor '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Color tempAux = System.Drawing.Color.Magenta;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			ColorConfigurer gameMsgColor = new ColorConfigurer(GAME_MSG_COLOR, Resources.getString("Chatter.game_messages_preference"), ref tempAux);
			
			gameMsgColor.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener1(this).propertyChange);
			
			globalPrefs.addOption(Resources.getString("Chatter.chat_window"), gameMsgColor);
			
			gameMsg = (System.Drawing.Color) globalPrefs.getValue(GAME_MSG_COLOR);
			
			//
			// system message color
			//
			//UPGRADE_NOTE: Final was removed from the declaration of 'systemMsgColor '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Color tempAux2 = System.Drawing.Color.FromArgb(160, 160, 160);
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			ColorConfigurer systemMsgColor = new ColorConfigurer(SYS_MSG_COLOR, Resources.getString("Chatter.system_message_preference"), ref tempAux2);
			
			systemMsgColor.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener2(this).propertyChange);
			
			globalPrefs.addOption(Resources.getString("Chatter.chat_window"), systemMsgColor);
			
			systemMsg = (System.Drawing.Color) globalPrefs.getValue(SYS_MSG_COLOR);
			
			//
			// my message color
			//
			//UPGRADE_NOTE: Final was removed from the declaration of 'myChatColor '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Color tempAux3 = System.Drawing.Color.Gray;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			ColorConfigurer myChatColor = new ColorConfigurer(MY_CHAT_COLOR, Resources.getString("Chatter.my_text_preference"), ref tempAux3);
			
			myChatColor.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener3(this).propertyChange);
			
			globalPrefs.addOption(Resources.getString("Chatter.chat_window"), myChatColor);
			
			myChat = (System.Drawing.Color) globalPrefs.getValue(MY_CHAT_COLOR);
			
			//
			// other message color
			//
			//UPGRADE_NOTE: Final was removed from the declaration of 'otherChatColor '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Color tempAux4 = System.Drawing.Color.Black;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			ColorConfigurer otherChatColor = new ColorConfigurer(OTHER_CHAT_COLOR, Resources.getString("Chatter.other_text_preference"), ref tempAux4);
			
			otherChatColor.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener4(this).propertyChange);
			
			globalPrefs.addOption(Resources.getString("Chatter.chat_window"), otherChatColor);
			
			otherChat = (System.Drawing.Color) globalPrefs.getValue(OTHER_CHAT_COLOR);
		}
		
		public virtual void  add(Buildable b)
		{
		}
		
		public virtual Command decode(System.String s)
		{
			if (s.StartsWith("CHAT"))
			{
				//$NON-NLS-1$
				return new DisplayText(this, s.Substring(4));
			}
			else
			{
				return null;
			}
		}
		
		public virtual System.String encode(Command c)
		{
			if (c is DisplayText)
			{
				return "CHAT" + ((DisplayText) c).msg; //$NON-NLS-1$
			}
			else
			{
				return null;
			}
		}
		
		/// <summary> Displays the message, Also logs and sends to the server
		/// a {@link Command} that displays this message
		/// </summary>
		public virtual void  send(System.String msg)
		{
			if (msg != null && msg.Length > 0)
			{
				show(msg);
				GameModule.getGameModule().sendAndLog(new DisplayText(this, msg));
			}
		}
		
		
		/// <summary> Classes other than the Chatter itself may forward KeyEvents
		/// to the Chatter by using this method
		/// </summary>
		public virtual void  keyCommand(System.Windows.Forms.KeyEventArgs e)
		{
			//UPGRADE_ISSUE: Field 'java.awt.event.KeyEvent.CHAR_UNDEFINED' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventKeyEventCHAR_UNDEFINED_f'"
			if ((e.KeyValue == 0 || e.KeyValue == KeyEvent.CHAR_UNDEFINED) && !(System.Char.GetUnicodeCategory((char) e.KeyValue) == System.Globalization.UnicodeCategory.Control))
			{
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				input.Text += (char) e.KeyValue;
			}
			else
			{
				//UPGRADE_ISSUE: Method 'javax.swing.KeyStroke.isOnKeyRelease' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingKeyStrokeisOnKeyRelease'"
				if (e.isOnKeyRelease())
				{
					switch (e.KeyValue)
					{
						
						case (int) System.Windows.Forms.Keys.Enter: 
							if (input.Text.Length > 0)
								send(formatChat(input.Text));
							//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
							input.Text = ""; //$NON-NLS-1$
							break;
						
						case (int) System.Windows.Forms.Keys.Back: 
						case (int) System.Windows.Forms.Keys.Delete: 
							System.String s = input.Text;
							if (s.Length > 0)
							{
								//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
								input.Text = s.Substring(0, (s.Length - 1) - (0));
							}
							break;
						}
				}
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'UI' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_TODO: Class 'javax.swing.plaf.basic.BasicTextAreaUI' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		private class UI:BasicTextAreaUI
		{
			public UI(Chatter enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Chatter enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Chatter enclosingInstance;
			public Chatter Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: Class 'javax.swing.text.View' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextView'"
			//UPGRADE_ISSUE: Interface 'javax.swing.text.Element' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextElement'"
			public View create(javax.swing.text.Element elem)
			{
				//UPGRADE_TODO: Method 'javax.swing.plaf.basic.BasicTextUI.getComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				System.Windows.Forms.TextBoxBase c = getComponent();
				if (c is System.Windows.Forms.TextBox)
				{
					System.Windows.Forms.TextBox area = (System.Windows.Forms.TextBox) c;
					//UPGRADE_ISSUE: Class 'javax.swing.text.View' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextView'"
					View v;
					if (area.WordWrap)
					{
						//UPGRADE_TODO: Method 'javax.swing.JTextArea.getWrapStyleWord' was converted to 'System.Windows.Forms.TextBox.WordWrap' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextAreagetWrapStyleWord'"
						v = new WrappedView(enclosingInstance, elem, area.WordWrap);
					}
					else
					{
						v = new PView(enclosingInstance, elem);
					}
					return v;
				}
				return null;
			}
		}
		
		//UPGRADE_ISSUE: Interface 'javax.swing.text.TabExpander' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextTabExpander'"
		//UPGRADE_ISSUE: Interface 'javax.swing.text.Document' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDocument'"
		//UPGRADE_ISSUE: Interface 'javax.swing.text.Element' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextElement'"
		private int drawColoredText(System.Drawing.Graphics g, int x, int y, TabExpander ex, Document doc, int p0, int p1, Element elem)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SupportClass.SegmentSupport s = new SupportClass.SegmentSupport();
			//UPGRADE_ISSUE: Method 'javax.swing.text.Document.getText' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDocument'"
			doc.getText(p0, p1 - p0, s);
			SupportClass.GraphicsManager.manager.SetColor(g, getColor(elem));
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'g2d '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Graphics g2d = (System.Drawing.Graphics) g;
			//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setRenderingHint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetRenderingHint_javaawtRenderingHintsKey_javalangObject'"
			//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_ANTIALIASING' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_ANTIALIASING_f'"
			//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.VALUE_ANTIALIAS_ON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsVALUE_ANTIALIAS_ON_f'"
			g2d.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
			//UPGRADE_ISSUE: Method 'javax.swing.text.Utilities.drawTabbedText' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextUtilities'"
			return Utilities.drawTabbedText(s, x, y, g, ex, p0);
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'WrappedView' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_ISSUE: Class 'javax.swing.text.WrappedPlainView' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextWrappedPlainView'"
		private class WrappedView:WrappedPlainView
		{
			private void  InitBlock(Chatter enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Chatter enclosingInstance;
			public Chatter Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: Constructor 'javax.swing.text.WrappedPlainView.WrappedPlainView' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextWrappedPlainView'"
			//UPGRADE_ISSUE: Interface 'javax.swing.text.Element' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextElement'"
			internal WrappedView(Chatter enclosingInstance, Element el, bool wrap):base(el, wrap)
			{
				InitBlock(enclosingInstance);
			}
			
			protected internal int drawUnselectedText(System.Drawing.Graphics g, int x, int y, int p0, int p1)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'root '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Interface 'javax.swing.text.Element' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextElement'"
				//UPGRADE_ISSUE: Method 'javax.swing.text.View.getElement' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextView'"
				Element root = getElement();
				//UPGRADE_ISSUE: Method 'javax.swing.text.View.getDocument' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextView'"
				//UPGRADE_ISSUE: Method 'javax.swing.text.Element.getElement' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextElement'"
				//UPGRADE_ISSUE: Method 'javax.swing.text.Element.getElementIndex' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextElement'"
				return Enclosing_Instance.drawColoredText(g, x, y, this, getDocument(), p0, p1, root.getElement(root.getElementIndex(p0)));
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'PView' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_ISSUE: Class 'javax.swing.text.PlainView' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextPlainView'"
		private class PView:PlainView
		{
			private void  InitBlock(Chatter enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Chatter enclosingInstance;
			public Chatter Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: Constructor 'javax.swing.text.PlainView.PlainView' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextPlainView'"
			//UPGRADE_ISSUE: Interface 'javax.swing.text.Element' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextElement'"
			internal PView(Chatter enclosingInstance, Element el):base(el)
			{
				InitBlock(enclosingInstance);
			}
			
			protected internal int drawUnselectedText(System.Drawing.Graphics g, int x, int y, int p0, int p1)
			{
				//UPGRADE_ISSUE: Interface 'javax.swing.text.Element' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextElement'"
				//UPGRADE_ISSUE: Method 'javax.swing.text.View.getElement' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextView'"
				Element root = getElement();
				//UPGRADE_ISSUE: Method 'javax.swing.text.View.getDocument' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextView'"
				//UPGRADE_ISSUE: Method 'javax.swing.text.Element.getElement' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextElement'"
				//UPGRADE_ISSUE: Method 'javax.swing.text.Element.getElementIndex' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextElement'"
				return Enclosing_Instance.drawColoredText(g, x, y, this, getDocument(), p0, p1, root.getElement(root.getElementIndex(p0)));
			}
		}
		
		/// <summary> Determines the color with which to draw a given line of text</summary>
		/// <returns> the Color to draw
		/// </returns>
		//UPGRADE_ISSUE: Interface 'javax.swing.text.Element' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextElement'"
		protected internal virtual System.Drawing.Color getColor(Element elem)
		{
			System.Drawing.Color col = System.Drawing.Color.Empty;
			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Method 'javax.swing.text.Document.getText' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDocument'"
				//UPGRADE_ISSUE: Method 'javax.swing.text.Element.getDocument' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextElement'"
				//UPGRADE_ISSUE: Method 'javax.swing.text.Element.getStartOffset' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextElement'"
				//UPGRADE_ISSUE: Method 'javax.swing.text.Element.getEndOffset' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextElement'"
				System.String s = elem.getDocument().getText(elem.getStartOffset(), elem.getEndOffset() - elem.getStartOffset()).Trim();
				
				if (s.Length > 0)
				{
					switch (s[0])
					{
						
						case '*': 
							col = gameMsg;
							break;
						
						case '-': 
							col = systemMsg;
							break;
						
						default: 
							if (s.StartsWith(formatChat("")))
							{
								//$NON-NLS-1$
								col = myChat;
							}
							else
							{
								col = otherChat;
							}
							break;
						
					}
				}
			}
			//UPGRADE_TODO: Class 'javax.swing.text.BadLocationException' was converted to 'System.Exception' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			catch (System.Exception e)
			{
				ErrorDialog.bug(e);
			}
			
			return col.IsEmpty?System.Drawing.Color.Black:col;
		}
		
		/// <summary> This is a {@link Command} object that, when executed, displays
		/// a text message in the Chatter's text area     
		/// </summary>
		public class DisplayText:Command
		{
			virtual public System.String Message
			{
				get
				{
					return msg;
				}
				
			}
			override public System.String Details
			{
				get
				{
					return msg;
				}
				
			}
			private System.String msg;
			private Chatter c;
			
			public DisplayText(Chatter c, System.String s)
			{
				this.c = c;
				msg = s;
				if (msg.StartsWith("<>"))
				{
					msg = "<(" + Chatter.AnonymousUserName + ")>" + s.Substring(2);
				}
				else
				{
					msg = s;
				}
			}
			
			public override void  executeCommand()
			{
				c.show(msg);
			}
			
			public override Command myUndoCommand()
			{
				return new DisplayText(c, Resources.getString("Chatter.undo_message", msg)); //$NON-NLS-1$
			}
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			Chatter chat = new Chatter();
			System.Windows.Forms.Form f = new System.Windows.Forms.Form();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			f.Controls.Add(chat);
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			f.pack();
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			f.Visible = true;
		}
	}
}