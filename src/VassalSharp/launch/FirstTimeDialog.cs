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
//UPGRADE_TODO: The type 'org.jdesktop.layout.GroupLayout' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using GroupLayout = org.jdesktop.layout.GroupLayout;
//UPGRADE_TODO: The type 'org.jdesktop.layout.LayoutStyle' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LayoutStyle = org.jdesktop.layout.LayoutStyle;
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
using Documentation = VassalSharp.build.module.Documentation;
using ShowHelpAction = VassalSharp.configure.ShowHelpAction;
using Resources = VassalSharp.i18n.Resources;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using ImageUtils = VassalSharp.tools.image.ImageUtils;
using ImageIOException = VassalSharp.tools.image.ImageIOException;
namespace VassalSharp.launch
{
	
	/// <summary> A dialog for first-time users.
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	[Serializable]
	public class FirstTimeDialog:System.Windows.Forms.Form
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(FirstTimeDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(FirstTimeDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private FirstTimeDialog enclosingInstance;
			public FirstTimeDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs e)
			{
				e.Cancel = true;
				System.Environment.Exit(0);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(FirstTimeDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(FirstTimeDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private FirstTimeDialog enclosingInstance;
			public FirstTimeDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				Enclosing_Instance.Dispose();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDefaultListCellRenderer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_ISSUE: Class 'javax.swing.DefaultListCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultListCellRenderer'"
		[Serializable]
		private class AnonymousClassDefaultListCellRenderer:DefaultListCellRenderer
		{
			public AnonymousClassDefaultListCellRenderer(FirstTimeDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(FirstTimeDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private FirstTimeDialog enclosingInstance;
			public FirstTimeDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			public System.Windows.Forms.Control getListCellRendererComponent(System.Windows.Forms.ListBox list, System.Object value_Renamed, int index, bool isSelected, bool cellHasFocus)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.DefaultListCellRenderer.getListCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultListCellRenderer'"
				base.getListCellRendererComponent(list, value_Renamed, index, isSelected, cellHasFocus);
				//UPGRADE_TODO: Method 'java.util.Locale.getDisplayName' was converted to 'System.Globalization.CultureInfo.NativeName' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilLocalegetDisplayName_javautilLocale'"
				Text = new System.Globalization.CultureInfo(Resources.Locale.LCID).NativeName;
				return this;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(System.Windows.Forms.ComboBox langbox, System.Windows.Forms.Label welcome, System.Windows.Forms.Button tour, System.Windows.Forms.Button jump, System.Windows.Forms.Button help, System.Windows.Forms.Label lang, FirstTimeDialog enclosingInstance)
			{
				InitBlock(langbox, welcome, tour, jump, help, lang, enclosingInstance);
			}
			private void  InitBlock(System.Windows.Forms.ComboBox langbox, System.Windows.Forms.Label welcome, System.Windows.Forms.Button tour, System.Windows.Forms.Button jump, System.Windows.Forms.Button help, System.Windows.Forms.Label lang, FirstTimeDialog enclosingInstance)
			{
				this.langbox = langbox;
				this.welcome = welcome;
				this.tour = tour;
				this.jump = jump;
				this.help = help;
				this.lang = lang;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable langbox was copied into class AnonymousClassActionListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.ComboBox langbox;
			//UPGRADE_NOTE: Final variable welcome was copied into class AnonymousClassActionListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.Label welcome;
			//UPGRADE_NOTE: Final variable tour was copied into class AnonymousClassActionListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.Button tour;
			//UPGRADE_NOTE: Final variable jump was copied into class AnonymousClassActionListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.Button jump;
			//UPGRADE_NOTE: Final variable help was copied into class AnonymousClassActionListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.Button help;
			//UPGRADE_NOTE: Final variable lang was copied into class AnonymousClassActionListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.Label lang;
			private FirstTimeDialog enclosingInstance;
			public FirstTimeDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Resources.Locale = (System.Globalization.CultureInfo) langbox.SelectedItem;
				
				// update the text for the new locale
				welcome.Text = Resources.getString("Main.welcome"); //$NON-NLS-1$
				tour.Text = Resources.getString("Main.tour"); //$NON-NLS-1$
				jump.Text = Resources.getString("Main.jump_right_in"); //$NON-NLS-1$
				help.Text = Resources.getString(Resources.HELP);
				lang.Text = Resources.getString("Prefs.language") + ":";
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				Enclosing_Instance.pack();
				// langbox picks up the new locale automatically from getDisplayName()
			}
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.launch.FirstTimeDialog'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		public FirstTimeDialog(System.Windows.Forms.Form parent):base()
		{
			//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.SetDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtFrame_boolean'"
			SupportClass.DialogSupport.SetDialog(this, parent);
			
			Closing += new System.ComponentModel.CancelEventHandler(this.FirstTimeDialog_Closing_DO_NOTHING_ON_CLOSE);
			//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(this).windowClosing);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'about '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label about = new System.Windows.Forms.Label();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'welcome '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label welcome = new System.Windows.Forms.Label();
			//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
			welcome.Font = new System.Drawing.Font("SansSerif", 40, System.Drawing.FontStyle.Bold); //$NON-NLS-1$
			welcome.Text = Resources.getString("Main.welcome"); //$NON-NLS-1$
			welcome.ForeColor = System.Drawing.Color.Black;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'tour '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Constructor 'javax.swing.JButton.JButton' was converted to 'System.Windows.Forms.Button.Button' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJButtonJButton_javaxswingAction'"
			System.Windows.Forms.Button tour = new System.Windows.Forms.Button();
			SupportClass.ActionSupport tmp_action = new LaunchTourAction(parent);
			tour.Click += new System.EventHandler(tmp_action.actionPerformed);
			tour.Image = tmp_action.Icon;
			tour.Text = tmp_action.Description;
			//UPGRADE_NOTE: Final was removed from the declaration of 'jump '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Button jump = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString("Main.jump_right_in")); //$NON-NLS-1$
			//UPGRADE_NOTE: Final was removed from the declaration of 'help '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Button help = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.HELP));
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'closer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			ActionListener closer = new AnonymousClassActionListener(this);
			
			tour.Click += new System.EventHandler(closer.actionPerformed);
			SupportClass.CommandManager.CheckCommand(tour);
			jump.Click += new System.EventHandler(closer.actionPerformed);
			SupportClass.CommandManager.CheckCommand(jump);
			
			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'readme '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.IO.FileInfo readme = new System.IO.FileInfo(Documentation.DocumentationBaseDir.FullName + "\\" + "README.html");
				help.Click += new System.EventHandler(new ShowHelpAction(readme.toURI().toURL(), null).actionPerformed);
				SupportClass.CommandManager.CheckCommand(help);
			}
			catch (System.UriFormatException e)
			{
				ErrorDialog.bug(e);
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'lang '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label temp_label;
			temp_label = new System.Windows.Forms.Label();
			temp_label.Text = Resources.getString("Prefs.language") + ":";
			System.Windows.Forms.Label lang = temp_label;
			//UPGRADE_NOTE: Final was removed from the declaration of 'langbox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.ComboBox langbox = new JComboBox(Resources.getSupportedLocales().toArray());
			//UPGRADE_ISSUE: Method 'javax.swing.JComboBox.setRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComboBoxsetRenderer_javaxswingListCellRenderer'"
			langbox.setRenderer(new AnonymousClassDefaultListCellRenderer(this));
			
			langbox.SelectedItem = Resources.Locale;
			langbox.SelectedValueChanged += new System.EventHandler(new AnonymousClassActionListener1(langbox, welcome, tour, jump, help, lang, this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(langbox);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'panel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();
			//UPGRADE_NOTE: Final was removed from the declaration of 'layout '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GroupLayout layout = new GroupLayout(panel);
			panel.setLayout(layout);
			
			layout.setAutocreateGaps(true);
			layout.setAutocreateContainerGaps(true);
			
			layout.setHorizontalGroup(layout.createParallelGroup(GroupLayout.CENTER, true).add(about).add(welcome).add(layout.createSequentialGroup().add(tour).add(jump).add(help)).add(layout.createSequentialGroup().add(0, 0, System.Int32.MaxValue).add(lang).add(langbox).add(0, 0, System.Int32.MaxValue)));
			
			layout.setVerticalGroup(layout.createSequentialGroup().add(about).addPreferredGap(LayoutStyle.UNRELATED, GroupLayout.DEFAULT_SIZE, System.Int32.MaxValue).add(welcome).addPreferredGap(LayoutStyle.UNRELATED, GroupLayout.DEFAULT_SIZE, System.Int32.MaxValue).add(layout.createParallelGroup(GroupLayout.BASELINE, false).add(tour).add(jump).add(help)).addPreferredGap(LayoutStyle.UNRELATED, GroupLayout.DEFAULT_SIZE, System.Int32.MaxValue).add(layout.createParallelGroup(GroupLayout.BASELINE, false).add(lang).add(langbox)));
			
			layout.linkSize(new System.Windows.Forms.Control[]{tour, jump, help});
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(panel);
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
			
			// load the splash image
			System.Drawing.Bitmap img = null;
			try
			{
				img = ImageUtils.getImageResource("/images/Splash.png");
			}
			catch (ImageIOException e)
			{
				logger.error("", e);
			}
			
			if (img != null)
			{
				// ensure that the dialog fits on the screen
				//UPGRADE_NOTE: Final was removed from the declaration of 'screen '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Method 'java.awt.GraphicsEnvironment.getLocalGraphicsEnvironment' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphicsEnvironment'"
				System.Drawing.Rectangle screen = GraphicsEnvironment.getLocalGraphicsEnvironment().getMaximumWindowBounds();
				//UPGRADE_NOTE: Final was removed from the declaration of 'dsize '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Size dsize = Size;
				//UPGRADE_NOTE: Final was removed from the declaration of 'remainder '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Size remainder = new System.Drawing.Size(System.Math.Max(screen.Width - dsize.Width, 0), System.Math.Max(screen.Height - dsize.Height, 0));
				
				if (remainder.Width == 0 || remainder.Height == 0)
				{
					// no room for the image, do nothing
				}
				else if (remainder.Width >= img.Width && remainder.Height >= img.Height)
				{
					// the whole image fits, use it as-is
					about.Image = (System.Drawing.Image) img.Clone();
				}
				else
				{
					// downscale the image to fit
					//UPGRADE_NOTE: Final was removed from the declaration of 'scale '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					double scale = System.Math.Min(remainder.Width / (double) img.Width, remainder.Height / (double) img.Height);
					about.Image = (System.Drawing.Image) ImageUtils.transform(img, scale, 0.0).Clone(); ;
				}
				
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				pack();
			}
			
			setMinimumSize(Size);
			//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		}
		private void  FirstTimeDialog_Closing_DO_NOTHING_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 0);
		}
		static FirstTimeDialog()
		{
			logger = LoggerFactory.getLogger(typeof(FirstTimeDialog));
		}
	}
}