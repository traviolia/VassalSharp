/*
* $Id$
*
* Copyright (c) 2007 by Brent Easton
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
using Info = VassalSharp.Info;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using HelpWindow = VassalSharp.build.module.documentation.HelpWindow;
using ConfigureTree = VassalSharp.configure.ConfigureTree;
using ShowHelpAction = VassalSharp.configure.ShowHelpAction;
using ReadErrorDialog = VassalSharp.tools.ReadErrorDialog;
using WriteErrorDialog = VassalSharp.tools.WriteErrorDialog;
using ExtensionFileFilter = VassalSharp.tools.filechooser.ExtensionFileFilter;
using FileChooser = VassalSharp.tools.filechooser.FileChooser;
using IOUtils = VassalSharp.tools.io.IOUtils;

namespace VassalSharp.i18n
{
	
	[Serializable]
	public class TranslateVassalWindow:TranslateWindow
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(TranslateVassalWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TranslateVassalWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TranslateVassalWindow enclosingInstance;
			public TranslateVassalWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				System.Globalization.CultureInfo l = Enclosing_Instance.localeConfig.ValueLocale;
				if (!Resources.getSupportedLocales().contains(l))
				{
					//UPGRADE_TODO: Method 'java.util.Locale.getLanguage' was converted to 'System.Globalization.CultureInfo.TwoLetterISOLanguageName' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilLocalegetLanguage'"
					l = new Locale(l.TwoLetterISOLanguageName);
				}
				
				if (Resources.getSupportedLocales().contains(l))
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'filename '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String filename = "VASSAL_" + l + ".properties";
					//UPGRADE_NOTE: Final was removed from the declaration of 'is '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_ISSUE: Method 'java.lang.Class.getResourceAsStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetResourceAsStream_javalangString'"
					System.IO.Stream is_Renamed = GetType().getResourceAsStream(filename);
					if (is_Renamed != null)
					{
						System.IO.BufferedStream in_Renamed = null;
						try
						{
							in_Renamed = new System.IO.BufferedStream(is_Renamed);
							((VassalTranslation) Enclosing_Instance.target).loadProperties(in_Renamed);
							//UPGRADE_ISSUE: Method 'javax.swing.table.AbstractTableModel.fireTableDataChanged' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableAbstractTableModelfireTableDataChanged'"
							((MyTableModel) Enclosing_Instance.keyTable.DataSource).fireTableDataChanged();
							in_Renamed.Close();
						}
						catch (System.IO.IOException e)
						{
							ReadErrorDialog.error(e, filename);
						}
						finally
						{
							IOUtils.closeQuietly(in_Renamed);
						}
					}
				}
				else
				{
					((VassalTranslation) Enclosing_Instance.target).clearProperties();
					//UPGRADE_ISSUE: Method 'javax.swing.table.AbstractTableModel.fireTableDataChanged' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableAbstractTableModelfireTableDataChanged'"
					((MyTableModel) Enclosing_Instance.keyTable.DataSource).fireTableDataChanged();
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		new private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(TranslateVassalWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TranslateVassalWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TranslateVassalWindow enclosingInstance;
			public TranslateVassalWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.loadTranslation();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		new private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(TranslateVassalWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TranslateVassalWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TranslateVassalWindow enclosingInstance;
			public TranslateVassalWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				try
				{
					// FIXME: can this ever throw?
					Enclosing_Instance.save();
				}
				catch (System.IO.IOException e1)
				{
					// FIXME: error dialog
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		new private class AnonymousClassActionListener2
		{
			public AnonymousClassActionListener2(TranslateVassalWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TranslateVassalWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TranslateVassalWindow enclosingInstance;
			public TranslateVassalWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.cancel();
			}
		}
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public virtual void  Run()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				TranslateVassalWindow w = new TranslateVassalWindow(null);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				w.Visible = true;
			}
		}
		virtual protected internal FileChooser FileChooser
		{
			get
			{
				if (fileChooser == null)
				{
					fileChooser = FileChooser.createFileChooser(this, null);
				}
				else
				{
					fileChooser.resetChoosableFileFilters();
					fileChooser.rescanCurrentDirectory();
				}
				return fileChooser;
			}
			
		}
		override protected internal System.Windows.Forms.Control HeaderPanel
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'headPanel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Panel headPanel = new System.Windows.Forms.Panel();
				//UPGRADE_TODO: Method 'java.util.Locale.getLanguage' was converted to 'System.Globalization.CultureInfo.TwoLetterISOLanguageName' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilLocalegetLanguage'"
				//UPGRADE_TODO: Method 'java.util.Locale.getDefault' was converted to 'System.Threading.Thread.CurrentThread.CurrentCulture' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				localeConfig = new LocaleConfigurer(null, "", new Locale(System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName));
				
				localeConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				headPanel.Controls.Add(localeConfig.Controls);
				return headPanel;
			}
			
		}
		override protected internal System.Windows.Forms.Control ButtonPanel
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'buttonBox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Panel buttonBox = new System.Windows.Forms.Panel();
				//UPGRADE_NOTE: Final was removed from the declaration of 'helpButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Button helpButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.HELP));
				helpButton.Click += new System.EventHandler(new ShowHelpAction(HelpFile.getReferenceManualPage("Translations.htm", "application").getContents(), null).actionPerformed);
				SupportClass.CommandManager.CheckCommand(helpButton);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'loadButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Button loadButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.LOAD));
				loadButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(loadButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				buttonBox.Controls.Add(helpButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				buttonBox.Controls.Add(loadButton);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'okButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Button okButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.SAVE));
				okButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(okButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				buttonBox.Controls.Add(okButton);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'cancelButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Button cancelButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.CANCEL));
				cancelButton.Click += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(cancelButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				buttonBox.Controls.Add(cancelButton);
				return buttonBox;
			}
			
		}
		private const long serialVersionUID = 1L;
		protected internal LocaleConfigurer localeConfig;
		
		protected internal FileChooser fileChooser;
		
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		public TranslateVassalWindow(System.Windows.Forms.Form owner, bool modal, Translatable target, HelpWindow helpWindow, ConfigureTree tree):base(owner, modal, target, helpWindow, tree)
		{
		}
		
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		public TranslateVassalWindow(System.Windows.Forms.Form owner):base(owner, false, new VassalTranslation(), null, null)
		{
			currentTranslation = (Translation) target;
			keyTable.Enabled = true;
			newTranslation();
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected internal override System.Windows.Forms.Control buildMainPanel()
		{
			//UPGRADE_TODO: Class 'javax.swing.JSplitPane' was converted to 'SupportClass.SplitterPanelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			SupportClass.SplitterPanelSupport pane = (SupportClass.SplitterPanelSupport) base.buildMainPanel();
			return pane.SecondControl;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		
		protected internal virtual void  newTranslation()
		{
			((VassalTranslation) target).clearProperties();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < String > keyList = new ArrayList < String >(Resources.getVassalKeys());
			Collections.sort(keyList);
			keys = keyList.toArray(new System.String[keyList.size()]);
			copyButtons = new CopyButton[keys.Length];
			((MyTableModel) keyTable.DataSource).update();
		}
		
		protected internal virtual void  loadTranslation()
		{
			if (currentTranslation.isDirty())
			{
				try
				{
					if (!querySave())
					{
						return ;
					}
				}
				catch (System.IO.IOException e)
				{
					ReadErrorDialog.error(e, currentTranslation.getBundleFileName());
					return ;
				}
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'fc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FileChooser fc = FileChooser;
			fc.FileFilter = new ExtensionFileFilter("Property Files", new System.String[]{".properties"});
			fc.CurrentDirectory = Info.HomeDir;
			if (fc.showOpenDialog(this) != FileChooser.APPROVE_OPTION)
				return ;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'file '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.FileInfo file = fc.SelectedFile;
			if (!file.Name.EndsWith(".properties"))
			{
				// FIXME: review error message
				loadError("Module Properties files must end in '.properties'.");
				return ;
			}
			else
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'language '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String language = file.Name.Substring(7, (9) - (7));
				System.String country = "";
				if (file.Name[9] == '_')
				{
					country = file.Name.Substring(10, (12) - (10));
				}
				//UPGRADE_NOTE: Final was removed from the declaration of 'locale '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_WARNING: Constructor 'java.util.Locale.Locale' was converted to 'System.Globalization.CultureInfo' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
				System.Globalization.CultureInfo locale = new System.Globalization.CultureInfo(language + "-" + country);
				localeConfig.setValue(locale);
			}
			
			System.IO.BufferedStream in_Renamed = null;
			try
			{
				//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javaioFile'"
				in_Renamed = new System.IO.BufferedStream(new System.IO.FileStream(file.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read));
				((VassalTranslation) target).loadProperties(in_Renamed);
				in_Renamed.Close();
			}
			catch (System.IO.IOException e)
			{
				ReadErrorDialog.error(e, file);
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
			
			//UPGRADE_ISSUE: Method 'javax.swing.table.AbstractTableModel.fireTableDataChanged' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableAbstractTableModelfireTableDataChanged'"
			((MyTableModel) keyTable.DataSource).fireTableDataChanged();
		}
		
		protected internal virtual void  loadError(System.String mess)
		{
			SupportClass.OptionPaneSupport.ShowMessageDialog(this, mess, "Invalid Properties file name", (int) System.Windows.Forms.MessageBoxIcon.Error);
			return ;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected internal override bool saveTranslation()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'fc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FileChooser fc = FileChooser;
			//UPGRADE_NOTE: Final was removed from the declaration of 'l '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Globalization.CultureInfo l = localeConfig.ValueLocale;
			//UPGRADE_TODO: Method 'java.util.Locale.getLanguage' was converted to 'System.Globalization.CultureInfo.TwoLetterISOLanguageName' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilLocalegetLanguage'"
			System.String bundle = "VASSAL_" + l.TwoLetterISOLanguageName;
			if (new System.Globalization.RegionInfo(l.LCID).TwoLetterISORegionName != null && new System.Globalization.RegionInfo(l.LCID).TwoLetterISORegionName.Length > 0)
			{
				bundle += ("_" + new System.Globalization.RegionInfo(l.LCID).TwoLetterISORegionName);
			}
			bundle += ".properties";
			
			fc.SelectedFile = new System.IO.FileInfo(Info.HomeDir.FullName + "\\" + bundle);
			if (fc.showSaveDialog(this) != FileChooser.APPROVE_OPTION)
				return false;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'outputFile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.FileInfo outputFile = fc.SelectedFile;
			try
			{
				((VassalTranslation) target).saveProperties(outputFile, localeConfig.ValueLocale);
			}
			catch (System.IO.IOException e)
			{
				WriteErrorDialog.error(e, outputFile);
				return false;
			}
			
			return true;
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
			SwingUtilities.invokeLater(new AnonymousClassRunnable());
		}
	}
}