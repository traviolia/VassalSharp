/*
* $Id$
*
* Copyright (c) 2000-2003 by Brent Easton
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
using RollSet = VassalSharp.build.module.dice.RollSet;
using Prefs = VassalSharp.preferences.Prefs;
namespace VassalSharp.build.module
{
	
	/// <author>  Brent Easton
	/// 
	/// Dialog for defining a {@link DieManager.RollSet}
	/// For use with internet dice rollers
	/// </author>
	[Serializable]
	public class MultiRoll:System.Windows.Forms.Form
	{
		static private System.Int32 state256;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(MultiRoll enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(MultiRoll enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MultiRoll enclosingInstance;
			public MultiRoll Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs e)
			{
				e.Cancel = true;
				Enclosing_Instance.rollCancelled = true;
				Enclosing_Instance.Visible = false;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassKeyAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassKeyAdapter
		{
			public AnonymousClassKeyAdapter(MultiRoll enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(MultiRoll enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MultiRoll enclosingInstance;
			public MultiRoll Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
			{
				Enclosing_Instance.description = Enclosing_Instance.descText.Text;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(MultiRoll enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(MultiRoll enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MultiRoll enclosingInstance;
			public MultiRoll Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.rollCancelled = false;
				int dieCount = 0;
				for (int i = 0; i < VassalSharp.build.module.MultiRoll.MAX_ROLLS; i++)
				{
					dieCount += (Enclosing_Instance.useDie[i]?1:0);
				}
				if (dieCount == 0)
				{
					SupportClass.OptionPaneSupport.ShowMessageDialog(Enclosing_Instance.me, "No dice selected for Roll.", "Roll Cancelled", (int) System.Windows.Forms.MessageBoxIcon.Error);
					return ;
				}
				Enclosing_Instance.Visible = false;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(MultiRoll enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(MultiRoll enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MultiRoll enclosingInstance;
			public MultiRoll Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.rollCancelled = true;
				Enclosing_Instance.Visible = false;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener2
		{
			public AnonymousClassActionListener2(MultiRoll enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(MultiRoll enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MultiRoll enclosingInstance;
			public MultiRoll Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.updateEmailAddress();
			}
		}
		private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			state256 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		virtual public System.String Description
		{
			get
			{
				return description;
			}
			
			set
			{
				description = value;
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				descText.Text = value;
			}
			
		}
		virtual public RollSet RollSet
		{
			get
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				ArrayList < DieRoll > l = new ArrayList < DieRoll >();
				for (int i = 0; i < MAX_ROLLS; ++i)
				{
					if (useDie[i])
					{
						l.add(rolls[i]);
					}
				}
				DieRoll[] rolls = l.toArray(new DieRoll[l.size()]);
				return new RollSet(Description, rolls);
			}
			
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
		//UPGRADE_TODO: More than one of the Java class members are converted to this same member in .NET. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1231'"
		new virtual public bool Visible
		{
			get
			{
				return base.Visible;
			}
			
			/*
			* Reset the status display before making visible in case preferences
			* have been changed.
			*/
			
			set
			{
				setServerHeader();
				setEmailHeader();
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				base.Visible = value;
			}
			
		}
		private const long serialVersionUID = 1L;
		
		private System.Windows.Forms.Button rollButton = SupportClass.ButtonSupport.CreateStandardButton("Roll");
		private System.Windows.Forms.Button canButton = SupportClass.ButtonSupport.CreateStandardButton("Cancel");
		private System.Windows.Forms.Button emailButton = SupportClass.ButtonSupport.CreateStandardButton("Change Email Address");
		
		private System.Windows.Forms.Form me;
		private System.Windows.Forms.Panel serverPanel;
		private System.Windows.Forms.Label serverLabel;
		private System.Windows.Forms.Panel emailPanel;
		private System.Windows.Forms.Label emailLabel;
		private System.Windows.Forms.Panel descPanel;
		private System.Windows.Forms.TextBox descText;
		private System.Windows.Forms.Panel topPanel;
		private System.Windows.Forms.Panel buttonPanel;
		private System.Windows.Forms.Panel detailPanel;
		protected internal int lastSelectedRow, lastSelectedCol;
		private System.String description = "";
		
		protected internal RollRow[] rollRows;
		
		public const int COL_IDX = 0;
		public const int COL_ROLL = 1;
		public const int COL_DESC = 2;
		public const int COL_NDICE = 3;
		public const int COL_NSIDES = 4;
		public const int COL_ADD = 5;
		public const int COL_TOTAL = 6;
		public const int NUMCOLS = 7;
		
		public const int MAX_ROLLS = 10;
		public const int ROW_HEIGHT = 20;
		
		public const int COL1_WIDTH = 31;
		public const int COL2_WIDTH = 30;
		public const int COL3_WIDTH = 137;
		public const int COL4_WIDTH = 50;
		public const int COL5_WIDTH = 50;
		public const int COL6_WIDTH = 25;
		public const int COL7_WIDTH = 35;
		
		protected internal DieManager dieManager;
		protected internal DieRoll[] rolls = new DieRoll[MAX_ROLLS];
		protected internal bool[] useDie = new bool[MAX_ROLLS];
		protected internal System.String verification = "";
		protected internal bool rollCancelled = false;
		protected internal bool singleRoll;
		
		protected internal MultiRoll()
		{
			
			//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(this).windowClosing);
		}
		
		public MultiRoll(DieManager d, int dfltNDice, int dfltNSides):this()
		{
			dieManager = d;
			for (int i = 0; i < MAX_ROLLS; i++)
			{
				rolls[i] = new DieRoll("", dfltNDice, dfltNSides);
			}
			initConfig(dfltNDice, dfltNSides);
			clearDie();
		}
		
		private void  clearDie()
		{
			for (int i = 0; i < MAX_ROLLS; i++)
			{
				useDie[i] = false;
			}
		}
		
		public virtual bool wasCancelled()
		{
			return rollCancelled;
		}
		
		// Multi-roll Configuration code
		private void  initConfig(int nd, int ns)
		{
			
			//UPGRADE_ISSUE: Method 'java.awt.Dialog.setModal' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtDialogsetModal_boolean'"
			setModal(true);
			
			Text = "Multi Roller";
			//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
			Size = new System.Drawing.Size(380, 206);
			BackColor = System.Drawing.Color.Gray;
			
			// Create a panel to hold all other components
			topPanel = new System.Windows.Forms.Panel();
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			topPanel.setLayout(new BoxLayout(topPanel, BoxLayout.PAGE_AXIS));
			
			// Build the Server/Email header
			serverPanel = new System.Windows.Forms.Panel();
			serverLabel = new System.Windows.Forms.Label();
			setServerHeader();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			serverPanel.Controls.Add(serverLabel);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			topPanel.Controls.Add(serverPanel);
			
			emailPanel = new System.Windows.Forms.Panel();
			emailLabel = new System.Windows.Forms.Label();
			setEmailHeader();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			emailPanel.Controls.Add(emailLabel);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			topPanel.Controls.Add(emailPanel);
			
			// And the body
			descPanel = new System.Windows.Forms.Panel();
			System.Windows.Forms.Label temp_label;
			temp_label = new System.Windows.Forms.Label();
			temp_label.Text = "Roll Description";
			System.Windows.Forms.Label descLabel = temp_label;
			//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
			descText = new System.Windows.Forms.TextBox();
			descText.setText(GameModule.getGameModule().getChatter().getInputField().getText());
			descText.KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.build.module.MultiRoll.keyDown);
			descText.KeyUp += new System.Windows.Forms.KeyEventHandler(new AnonymousClassKeyAdapter(this).keyReleased);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			descPanel.Controls.Add(descLabel);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			descPanel.Controls.Add(descText);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			topPanel.Controls.Add(descPanel);
			
			detailPanel = new System.Windows.Forms.Panel();
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			detailPanel.setLayout(new BoxLayout(detailPanel, BoxLayout.PAGE_AXIS));
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
			System.Windows.Forms.ControlPaint.DrawBorder3D(detailPanel.CreateGraphics(), 0, 0, detailPanel.Width, detailPanel.Height, System.Windows.Forms.Border3DStyle.Flat);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = new HeaderRow();
			detailPanel.Controls.Add(temp_Control);
			
			rollRows = new RollRow[MAX_ROLLS];
			for (int i = 0; i < MAX_ROLLS; i++)
			{
				rollRows[i] = new RollRow(this, i, nd, ns);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				detailPanel.Controls.Add(rollRows[i]);
			}
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			topPanel.Controls.Add(detailPanel);
			
			// Add Some buttons
			buttonPanel = new System.Windows.Forms.Panel();
			rollButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(rollButton);
			canButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(canButton);
			
			emailButton.Click += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(emailButton);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonPanel.Controls.Add(rollButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonPanel.Controls.Add(canButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonPanel.Controls.Add(emailButton);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(topPanel);
			topPanel.Dock = new System.Windows.Forms.DockStyle();
			topPanel.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(buttonPanel);
			buttonPanel.Dock = new System.Windows.Forms.DockStyle();
			buttonPanel.BringToFront();
			
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
		}
		
		protected internal virtual void  setServerHeader()
		{
			serverLabel.Text = "Server: " + dieManager.Server.Name;
		}
		
		private const System.String EMAIL_OFF = "Off";
		
		protected internal virtual void  setEmailHeader()
		{
			
			System.String label;
			Prefs prefs = GameModule.getGameModule().getPrefs();
			
			if (((System.Boolean) prefs.getValue(DieManager.USE_EMAIL)))
			{
				label = ((System.String) prefs.getValue(DieManager.SECONDARY_EMAIL));
			}
			else
			{
				label = EMAIL_OFF;
			}
			emailLabel.Text = "Email: " + label;
		}
		
		protected internal virtual void  updateEmailAddress()
		{
			
			Prefs prefs = GameModule.getGameModule().getPrefs();
			System.String[] aBook = (System.String[]) prefs.getValue(DieManager.ADDRESS_BOOK);
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Windows.Forms.ContextMenu popup = new System.Windows.Forms.ContextMenu();
			
			System.Windows.Forms.MenuItem menuItem = new System.Windows.Forms.MenuItem(EMAIL_OFF);
			menuItem.Click += new System.EventHandler(this.actionPerformed);
			SupportClass.CommandManager.CheckCommand(menuItem);
			popup.MenuItems.Add(menuItem);
			
			for (int i = 0; i < aBook.Length; i++)
			{
				menuItem = new System.Windows.Forms.MenuItem();
				menuItem.Text = aBook[i];
				menuItem.Click += new System.EventHandler(this.actionPerformed);
				SupportClass.CommandManager.CheckCommand(menuItem);
				popup.MenuItems.Add(menuItem);
			}
			
			//UPGRADE_TODO: Method 'javax.swing.JPopupMenu.show' was converted to 'System.Windows.Forms.ContextMenu.Show' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPopupMenushow_javaawtComponent_int_int'"
			popup.Show(emailButton, new System.Drawing.Point(emailButton.Left, emailButton.Top));
		}
		
		public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			System.String address = SupportClass.CommandManager.GetCommand(event_sender);
			Prefs prefs = GameModule.getGameModule().getPrefs();
			if (address.Equals(EMAIL_OFF))
			{
				prefs.setValue(DieManager.USE_EMAIL, (System.Object) false);
			}
			else
			{
				prefs.setValue(DieManager.SECONDARY_EMAIL, address);
				prefs.setValue(DieManager.USE_EMAIL, (System.Object) true);
			}
			setEmailHeader();
		}
		
		[Serializable]
		protected internal class HeaderRow:System.Windows.Forms.Panel
		{
			private const long serialVersionUID = 1L;
			
			public HeaderRow()
			{
				
				//UPGRADE_TODO: Interface 'javax.swing.border.Border' was converted to 'System.Windows.Forms.Border3DStyle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.Windows.Forms.Border3DStyle raisedbevel = System.Windows.Forms.Border3DStyle.Raised;
				//UPGRADE_TODO: Interface 'javax.swing.border.Border' was converted to 'System.Windows.Forms.Border3DStyle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.Windows.Forms.Border3DStyle myBorder = raisedbevel;
				
				//setLayout(new BoxLayout(this, BoxLayout.LINE_AXIS));
				//setBorder(blackline);
				
				System.Windows.Forms.Label temp_label;
				temp_label = new System.Windows.Forms.Label();
				temp_label.Text = "Roll";
				System.Windows.Forms.Label col1 = temp_label;
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				col1.Size = new System.Drawing.Size(VassalSharp.build.module.MultiRoll.COL1_WIDTH, VassalSharp.build.module.MultiRoll.ROW_HEIGHT);
				//UPGRADE_TODO: Method 'javax.swing.JLabel.setHorizontalAlignment' was converted to 'System.Windows.Forms.Label.ImageAlign' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJLabelsetHorizontalAlignment_int'"
				col1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
				System.Windows.Forms.ControlPaint.DrawBorder3D(col1.CreateGraphics(), 0, 0, col1.Width, col1.Height, myBorder);
				
				//      JLabel col2 = new JLabel("Roll");
				//      col2.setPreferredSize(new Dimension(COL2_WIDTH, ROW_HEIGHT));
				//      col2.setHorizontalAlignment(JTextField.CENTER);
				//            col2.setBorder(myBorder);
				
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = "Details";
				System.Windows.Forms.Label col3 = temp_label2;
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				col3.Size = new System.Drawing.Size(VassalSharp.build.module.MultiRoll.COL3_WIDTH, VassalSharp.build.module.MultiRoll.ROW_HEIGHT);
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
				System.Windows.Forms.ControlPaint.DrawBorder3D(col3.CreateGraphics(), 0, 0, col3.Width, col3.Height, myBorder);
				
				System.Windows.Forms.Label temp_label3;
				temp_label3 = new System.Windows.Forms.Label();
				temp_label3.Text = "nDice";
				System.Windows.Forms.Label col4 = temp_label3;
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
				System.Windows.Forms.ControlPaint.DrawBorder3D(col4.CreateGraphics(), 0, 0, col4.Width, col4.Height, myBorder);
				//UPGRADE_TODO: Method 'javax.swing.JLabel.setHorizontalAlignment' was converted to 'System.Windows.Forms.Label.ImageAlign' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJLabelsetHorizontalAlignment_int'"
				col4.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				col4.Size = new System.Drawing.Size(VassalSharp.build.module.MultiRoll.COL4_WIDTH, VassalSharp.build.module.MultiRoll.ROW_HEIGHT);
				
				System.Windows.Forms.Label temp_label4;
				temp_label4 = new System.Windows.Forms.Label();
				temp_label4.Text = "nSides";
				System.Windows.Forms.Label col5 = temp_label4;
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
				System.Windows.Forms.ControlPaint.DrawBorder3D(col5.CreateGraphics(), 0, 0, col5.Width, col5.Height, myBorder);
				//UPGRADE_TODO: Method 'javax.swing.JLabel.setHorizontalAlignment' was converted to 'System.Windows.Forms.Label.ImageAlign' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJLabelsetHorizontalAlignment_int'"
				col5.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				col5.Size = new System.Drawing.Size(VassalSharp.build.module.MultiRoll.COL5_WIDTH, VassalSharp.build.module.MultiRoll.ROW_HEIGHT);
				
				System.Windows.Forms.Label temp_label5;
				temp_label5 = new System.Windows.Forms.Label();
				temp_label5.Text = "add";
				System.Windows.Forms.Label col6 = temp_label5;
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
				System.Windows.Forms.ControlPaint.DrawBorder3D(col6.CreateGraphics(), 0, 0, col6.Width, col6.Height, myBorder);
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				col6.Size = new System.Drawing.Size(VassalSharp.build.module.MultiRoll.COL6_WIDTH, VassalSharp.build.module.MultiRoll.ROW_HEIGHT);
				
				System.Windows.Forms.Label temp_label6;
				temp_label6 = new System.Windows.Forms.Label();
				temp_label6.Text = "Total";
				System.Windows.Forms.Label col7 = temp_label6;
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
				System.Windows.Forms.ControlPaint.DrawBorder3D(col7.CreateGraphics(), 0, 0, col7.Width, col7.Height, myBorder);
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				col7.Size = new System.Drawing.Size(VassalSharp.build.module.MultiRoll.COL7_WIDTH, VassalSharp.build.module.MultiRoll.ROW_HEIGHT);
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(col1);
				//add(col2);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(col3);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(col4);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(col5);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(col6);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(col7);
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'RollRow' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
		[Serializable]
		protected internal class RollRow:System.Windows.Forms.Panel
		{
			static private System.Int32 state255;
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener3
			{
				public AnonymousClassActionListener3(RollRow enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(RollRow enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private RollRow enclosingInstance;
				public RollRow Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					Enclosing_Instance.col1.switchState();
					Enclosing_Instance.Enclosing_Instance.useDie[Enclosing_Instance.myRow] = Enclosing_Instance.col1.State;
					Enclosing_Instance.Enabled = Enclosing_Instance.col1.State;
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassKeyAdapter1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassKeyAdapter1
			{
				public AnonymousClassKeyAdapter1(RollRow enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(RollRow enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private RollRow enclosingInstance;
				public RollRow Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
				{
					Enclosing_Instance.Enclosing_Instance.rolls[Enclosing_Instance.myRow].Description = Enclosing_Instance.col3.Text;
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassMouseAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassMouseAdapter
			{
				public AnonymousClassMouseAdapter(RollRow enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(RollRow enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private RollRow enclosingInstance;
				public RollRow Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
				{
					if (!Enclosing_Instance.col3.Focused)
					{
						Enclosing_Instance.col3.SelectAll();
					}
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener4' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener4
			{
				public AnonymousClassActionListener4(RollRow enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(RollRow enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private RollRow enclosingInstance;
				public RollRow Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox) event_sender;
					Enclosing_Instance.Enclosing_Instance.rolls[Enclosing_Instance.myRow].NumDice = System.Int32.Parse((System.String) cb.SelectedItem);
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener5' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener5
			{
				public AnonymousClassActionListener5(RollRow enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(RollRow enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private RollRow enclosingInstance;
				public RollRow Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox) event_sender;
					Enclosing_Instance.Enclosing_Instance.rolls[Enclosing_Instance.myRow].NumSides = System.Int32.Parse((System.String) cb.SelectedItem);
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassKeyAdapter2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassKeyAdapter2
			{
				public AnonymousClassKeyAdapter2(RollRow enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(RollRow enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private RollRow enclosingInstance;
				public RollRow Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
				{
					try
					{
						Enclosing_Instance.Enclosing_Instance.rolls[Enclosing_Instance.myRow].Plus = System.Int32.Parse(Enclosing_Instance.col3.Text);
					}
					catch (System.FormatException ev)
					{
						// TODO use IntConfigurer for col3
						Enclosing_Instance.Enclosing_Instance.rolls[Enclosing_Instance.myRow].Plus = 0;
					}
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassItemListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassItemListener
			{
				public AnonymousClassItemListener(RollRow enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(RollRow enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private RollRow enclosingInstance;
				public RollRow Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  itemStateChanged(System.Object event_sender, System.EventArgs e)
				{
					if (event_sender is System.Windows.Forms.MenuItem)
						((System.Windows.Forms.MenuItem) event_sender).Checked = !((System.Windows.Forms.MenuItem) event_sender).Checked;
					//UPGRADE_ISSUE: Method 'java.awt.event.ItemEvent.getStateChange' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventItemEventgetStateChange'"
					//UPGRADE_ISSUE: Field 'java.awt.event.ItemEvent.SELECTED' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventItemEventSELECTED_f'"
					Enclosing_Instance.Enclosing_Instance.rolls[Enclosing_Instance.myRow].ReportTotal = (e.getStateChange() == ItemEvent.SELECTED);
				}
			}
			private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
			{
				state255 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
			}
			private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
			{
				state255 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
			}
			private void  InitBlock(MultiRoll enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				rowDim = new System.Drawing.Size(40, VassalSharp.build.module.MultiRoll.ROW_HEIGHT);
				myBorder = raisedbevel;
			}
			private MultiRoll enclosingInstance;
			//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
			//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
			new virtual public bool Enabled
			{
				get
				{
					return base.Enabled;
				}
				
				set
				{
					col3.Enabled = value;
					col4.Enabled = value;
					col5.Enabled = value;
					col6.Enabled = value;
					col7.Enabled = value;
				}
				
			}
			public MultiRoll Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			internal int myRow;
			internal bool selected;
			internal System.String description;
			internal int nDice, nSides, plus;
			internal bool reportTotal;
			//UPGRADE_NOTE: The initialization of  'rowDim' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			internal System.Drawing.Size rowDim
			{
				get
				{
					return rowDim_Renamed;
				}
				
				set
				{
					rowDim_Renamed = value;
				}
				
			}
			internal System.Drawing.Size rowDim_Renamed;
			
			internal StateButton col1;
			internal System.Windows.Forms.CheckBox col2, col7;
			internal System.Windows.Forms.ComboBox col4, col5;
			internal System.Windows.Forms.TextBox col3, col6;
			
			//UPGRADE_TODO: Interface 'javax.swing.border.Border' was converted to 'System.Windows.Forms.Border3DStyle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			internal System.Windows.Forms.Border3DStyle blackline = System.Windows.Forms.Border3DStyle.Flat;
			//UPGRADE_TODO: Interface 'javax.swing.border.Border' was converted to 'System.Windows.Forms.Border3DStyle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_TODO: Field 'javax.swing.border.EtchedBorder.RAISED' was converted to 'System.Windows.Forms.Border3DStyle.Raised' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingborderEtchedBorderRAISED_f'"
			internal System.Windows.Forms.Border3DStyle raisedetched = (System.Windows.Forms.Border3DStyle.Etched & (System.Windows.Forms.Border3DStyle) System.Windows.Forms.Border3DStyle.Bump);
			//UPGRADE_TODO: Interface 'javax.swing.border.Border' was converted to 'System.Windows.Forms.Border3DStyle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_TODO: Field 'javax.swing.border.EtchedBorder.LOWERED' was converted to 'System.Windows.Forms.Border3DStyle.Sunken' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingborderEtchedBorderLOWERED_f'"
			internal System.Windows.Forms.Border3DStyle loweredetched = (System.Windows.Forms.Border3DStyle.Etched & (System.Windows.Forms.Border3DStyle) System.Windows.Forms.Border3DStyle.Etched);
			//UPGRADE_TODO: Interface 'javax.swing.border.Border' was converted to 'System.Windows.Forms.Border3DStyle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			internal System.Windows.Forms.Border3DStyle raisedbevel = System.Windows.Forms.Border3DStyle.Raised;
			//UPGRADE_TODO: Interface 'javax.swing.border.Border' was converted to 'System.Windows.Forms.Border3DStyle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			internal System.Windows.Forms.Border3DStyle loweredbevel = System.Windows.Forms.Border3DStyle.Sunken;
			//UPGRADE_TODO: Interface 'javax.swing.border.Border' was converted to 'System.Windows.Forms.Border3DStyle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_NOTE: The initialization of  'myBorder' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			internal System.Windows.Forms.Border3DStyle myBorder;
			
			public RollRow(MultiRoll enclosingInstance, int row, int nd, int ns)
			{
				InitBlock(enclosingInstance);
				
				myRow = row;
				
				//setLayout(new BoxLayout(this, BoxLayout.LINE_AXIS));
				
				col1 = new StateButton((row + 1) + "");
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				col1.Size = new System.Drawing.Size(VassalSharp.build.module.MultiRoll.COL1_WIDTH, VassalSharp.build.module.MultiRoll.ROW_HEIGHT);
				col1.State = Enclosing_Instance.useDie[myRow];
				col1.Click += new System.EventHandler(new AnonymousClassActionListener3(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(col1);
				
				// Roll Description
				//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
				col3 = new System.Windows.Forms.TextBox();
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
				System.Windows.Forms.ControlPaint.DrawBorder3D(col3.CreateGraphics(), 0, 0, col3.Width, col3.Height, myBorder);
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				col3.Size = new System.Drawing.Size(VassalSharp.build.module.MultiRoll.COL3_WIDTH, VassalSharp.build.module.MultiRoll.ROW_HEIGHT);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				col3.Text = Enclosing_Instance.rolls[myRow].Description + "";
				col3.KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.build.module.MultiRoll.RollRow.keyDown);
				col3.KeyUp += new System.Windows.Forms.KeyEventHandler(new AnonymousClassKeyAdapter1(this).keyReleased);
				col3.MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.build.module.MultiRoll.RollRow.mouseDown);
				col3.MouseDown += new System.Windows.Forms.MouseEventHandler(new AnonymousClassMouseAdapter(this).mousePressed);
				col3.Enabled = false;
				
				// Number of Dice
				int[] allowableDice = Enclosing_Instance.dieManager.Server.getnDiceList();
				System.String[] diceData = new System.String[allowableDice.Length];
				int defaultNDIdx = 0;
				for (int i = 0; i < diceData.Length; i++)
				{
					diceData[i] = allowableDice[i] + "";
					if (nd == allowableDice[i])
						defaultNDIdx = i;
				}
				col4 = SupportClass.ComboBoxSupport.CreateComboBox(diceData);
				col4.SelectedIndex = defaultNDIdx;
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				col4.Size = new System.Drawing.Size(VassalSharp.build.module.MultiRoll.COL4_WIDTH, VassalSharp.build.module.MultiRoll.ROW_HEIGHT);
				col4.SelectedValueChanged += new System.EventHandler(new AnonymousClassActionListener4(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(col4);
				col4.Enabled = false;
				
				// Number of Sides
				int[] allowableSides = Enclosing_Instance.dieManager.Server.getnSideList();
				System.String[] sideData = new System.String[allowableSides.Length];
				int defaultNSIdx = 0;
				for (int i = 0; i < sideData.Length; i++)
				{
					sideData[i] = allowableSides[i] + "";
					if (ns == allowableSides[i])
						defaultNSIdx = i;
				}
				col5 = SupportClass.ComboBoxSupport.CreateComboBox(sideData);
				col5.SelectedIndex = defaultNSIdx;
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				col5.Size = new System.Drawing.Size(VassalSharp.build.module.MultiRoll.COL5_WIDTH, VassalSharp.build.module.MultiRoll.ROW_HEIGHT);
				col5.SelectedValueChanged += new System.EventHandler(new AnonymousClassActionListener5(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(col5);
				col5.Enabled = false;
				
				// Add to Total
				//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
				col6 = new System.Windows.Forms.TextBox();
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
				System.Windows.Forms.ControlPaint.DrawBorder3D(col6.CreateGraphics(), 0, 0, col6.Width, col6.Height, myBorder);
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				col6.Size = new System.Drawing.Size(VassalSharp.build.module.MultiRoll.COL6_WIDTH, VassalSharp.build.module.MultiRoll.ROW_HEIGHT);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				col6.Text = Enclosing_Instance.rolls[myRow].Plus + "";
				col6.KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.build.module.MultiRoll.RollRow.keyDown);
				col6.KeyUp += new System.Windows.Forms.KeyEventHandler(new AnonymousClassKeyAdapter2(this).keyReleased);
				col6.Enabled = false;
				
				// Report Total Only
				col7 = new System.Windows.Forms.CheckBox();
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
				System.Windows.Forms.ControlPaint.DrawBorder3D(col7.CreateGraphics(), 0, 0, col7.Width, col7.Height, myBorder);
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				col7.Size = new System.Drawing.Size(VassalSharp.build.module.MultiRoll.COL7_WIDTH, VassalSharp.build.module.MultiRoll.ROW_HEIGHT);
				//UPGRADE_TODO: Method 'javax.swing.AbstractButton.setHorizontalAlignment' was converted to 'System.Windows.Forms.ButtonBase.ImageAlign' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractButtonsetHorizontalAlignment_int'"
				//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.SwingConstants.CENTER' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				col7.ImageAlign = (System.Drawing.ContentAlignment) System.Windows.Forms.HorizontalAlignment.Center;
				col7.Checked = Enclosing_Instance.rolls[myRow].ReportTotal;
				col7.CheckedChanged += new System.EventHandler(new AnonymousClassItemListener(this).itemStateChanged);
				col7.Enabled = false;
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(col1);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(col3);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(col4);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(col5);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(col6);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(col7);
			}
		}
		
		/*
		* An on/off button that changes state to show it's status
		*/
		[Serializable]
		protected internal class StateButton:System.Windows.Forms.Button
		{
			virtual public bool State
			{
				get
				{
					return state;
				}
				
				set
				{
					state = value;
					if (state)
					{
						//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
						System.Windows.Forms.ControlPaint.DrawBorder3D(CreateGraphics(), 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.Sunken);
					}
					else
					{
						//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
						System.Windows.Forms.ControlPaint.DrawBorder3D(CreateGraphics(), 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.Raised);
					}
				}
				
			}
			private const long serialVersionUID = 1L;
			
			internal bool state = false;
			
			internal StateButton(System.String s, bool b):base()
			{
				this.Text = s;
				//UPGRADE_TODO: Method 'javax.swing.AbstractButton.setHorizontalAlignment' was converted to 'System.Windows.Forms.ButtonBase.ImageAlign' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractButtonsetHorizontalAlignment_int'"
				//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.SwingConstants.CENTER' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				ImageAlign = (System.Drawing.ContentAlignment) System.Windows.Forms.HorizontalAlignment.Center;
				State = b;
			}
			
			internal StateButton(System.String s):this(s, false)
			{
			}
			
			public virtual void  switchState()
			{
				State = !state;
			}
		}
	}
}