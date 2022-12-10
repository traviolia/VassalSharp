/*
* $Id$
*
* Copyright (c) 2006-2009 by Joel Uckelman
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
//UPGRADE_TODO: The type 'org.apache.commons.lang.SystemUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SystemUtils = org.apache.commons.lang.SystemUtils;
using DirectoryConfigurer = VassalSharp.configure.DirectoryConfigurer;
namespace VassalSharp.tools.filechooser
{
	
	/// <summary> FileChooser provides a wrapper for {@link javax.swing.JFileChooser} and
	/// {@link java.awt.FileDialog}, selecting whichever is preferred on the
	/// user's OS. <code>FileChooser</code>'s methods mirror those of
	/// <code>JFileChooser</code>.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	public abstract class FileChooser
	{
		public abstract System.IO.FileInfo CurrentDirectory{get;set;}
		public abstract System.IO.FileInfo SelectedFile{get;set;}
		public abstract System.String DialogTitle{get;set;}
		public abstract FileFilter FileFilter{get;set;}
		protected internal System.Windows.Forms.Control parent;
		protected internal DirectoryConfigurer prefs;
		//UPGRADE_NOTE: Final was removed from the declaration of 'APPROVE_OPTION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JFileChooser.APPROVE_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
		public static readonly int APPROVE_OPTION = (int) System.Windows.Forms.DialogResult.OK;
		//UPGRADE_NOTE: Final was removed from the declaration of 'CANCEL_OPTION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JFileChooser.CANCEL_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
		public static readonly int CANCEL_OPTION = (int) System.Windows.Forms.DialogResult.Cancel;
		//UPGRADE_NOTE: Final was removed from the declaration of 'ERROR_OPTION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JFileChooser.ERROR_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
		public static readonly int ERROR_OPTION = (int) System.Windows.Forms.DialogResult.Abort;
		//UPGRADE_NOTE: Final was removed from the declaration of 'FILES_ONLY '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly int FILES_ONLY = 0;
		//UPGRADE_NOTE: Final was removed from the declaration of 'DIRECTORIES_ONLY '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly int DIRECTORIES_ONLY = 1;
		
		protected internal FileChooser(System.Windows.Forms.Control parent, DirectoryConfigurer pref)
		{
			this.parent = parent;
			this.prefs = pref;
		}
		
		public static FileChooser createFileChooser(System.Windows.Forms.Control parent)
		{
			return createFileChooser(parent, null);
		}
		
		public static FileChooser createFileChooser(System.Windows.Forms.Control parent, DirectoryConfigurer prefs)
		{
			return createFileChooser(parent, prefs, FILES_ONLY);
		}
		
		/// <summary> Creates a FileChooser appropriate for the user's OS.
		/// 
		/// </summary>
		/// <param name="parent">The Component over which the FileChooser should appear.
		/// </param>
		/// <param name="prefs">A FileConfigure that stores the preferred starting directory of the FileChooser in preferences
		/// </param>
		public static FileChooser createFileChooser(System.Windows.Forms.Control parent, DirectoryConfigurer prefs, int mode)
		{
			FileChooser fc;
			if (SystemUtils.IS_OS_MAC_OSX)
			{
				// Mac has a good native file dialog
				fc = new NativeFileChooser(parent, prefs, mode);
			}
			else if (mode == FILES_ONLY && SystemUtils.IS_OS_WINDOWS)
			{
				// Window has a good native file dialog, but it doesn't support selecting directories
				fc = new NativeFileChooser(parent, prefs, mode);
			}
			else
			{
				// Linux's native dialog is inferior to Swing's
				fc = new SwingFileChooser(parent, prefs, mode);
			}
			return fc;
		}
		
		public abstract void  rescanCurrentDirectory();
		
		public abstract int showOpenDialog(System.Windows.Forms.Control parent);
		
		public abstract int showSaveDialog(System.Windows.Forms.Control parent);
		
		public abstract void  addChoosableFileFilter(FileFilter filter);
		
		public abstract bool removeChoosableFileFilter(FileFilter filter);
		
		public abstract void  resetChoosableFileFilters();
		
		/// <summary> Selects <tt>filename.sav</tt> if <tt>filename.foo</tt> is selected.</summary>
		public virtual void  selectDotSavFile()
		{
			System.IO.FileInfo file = SelectedFile;
			if (file != null)
			{
				System.String name = file.FullName;
				if (name != null)
				{
					int index = name.LastIndexOf('.');
					if (index > 0)
					{
						name = name.Substring(0, (index) - (0)) + ".vsav";
						SelectedFile = new System.IO.FileInfo(name);
					}
				}
			}
		}
		
		/// <summary> Same as {@link #showOpenDialog(Component)}, but uses the <tt>parent</tt> set on creation of this FileDialog.</summary>
		public virtual int showOpenDialog()
		{
			return showOpenDialog(parent);
		}
		
		/// <summary> Same as {@link #showSaveDialog(Component)}, but uses the <tt>parent</tt> set on creation of this FileDialog.</summary>
		public virtual int showSaveDialog()
		{
			return showSaveDialog(parent);
		}
		
		protected internal virtual void  updateDirectoryPreference()
		{
			if (prefs != null && CurrentDirectory != null && !CurrentDirectory.Equals(prefs.FileValue))
			{
				prefs.setValue(CurrentDirectory);
			}
		}
		
		private class SwingFileChooser:FileChooser
		{
			override public System.IO.FileInfo CurrentDirectory
			{
				get
				{
					//UPGRADE_TODO: Method 'javax.swing.JFileChooser.getCurrentDirectory' was converted to 'System.Windows.Forms.OpenDialog.InitialDirectory' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJFileChoosergetCurrentDirectory'"
					return new System.IO.FileInfo(fc.InitialDirectory);
				}
				
				set
				{
					//UPGRADE_TODO: Method 'javax.swing.JFileChooser.setCurrentDirectory' was converted to 'System.Windows.Forms.OpenFileDialog.InitialDirectory' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJFileChoosersetCurrentDirectory_javaioFile'"
					fc.InitialDirectory = value.DirectoryName;
				}
				
			}
			override public System.IO.FileInfo SelectedFile
			{
				get
				{
					return new System.IO.FileInfo(fc.FileName);
				}
				
				set
				{
					fc.FileName = value.FullName;
				}
				
			}
			virtual public int FileSelectionMode
			{
				get
				{
					//UPGRADE_ISSUE: Method 'javax.swing.JFileChooser.getFileSelectionMode' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJFileChoosergetFileSelectionMode'"
					return fc.getFileSelectionMode();
				}
				
				set
				{
					//UPGRADE_ISSUE: Method 'javax.swing.JFileChooser.setFileSelectionMode' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJFileChoosersetFileSelectionMode_int'"
					fc.setFileSelectionMode(value);
				}
				
			}
			override public System.String DialogTitle
			{
				get
				{
					return fc.Title;
				}
				
				set
				{
					fc.Title = value;
				}
				
			}
			override public FileFilter FileFilter
			{
				get
				{
					//UPGRADE_ISSUE: Class 'javax.swing.filechooser.FileFilter' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingfilechooserFileFilter'"
					//UPGRADE_ISSUE: Method 'javax.swing.JFileChooser.getFileFilter' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJFileChoosergetFileFilter'"
					javax.swing.filechooser.FileFilter ff = fc.getFileFilter();
					return ff is FileFilter?(FileFilter) ff:null;
				}
				
				set
				{
					//UPGRADE_ISSUE: Method 'javax.swing.JFileChooser.setFileFilter' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJFileChoosersetFileFilter_javaxswingfilechooserFileFilter'"
					fc.setFileFilter(value);
				}
				
			}
			//UPGRADE_TODO: Constructor may need to be changed depending on function performed by the 'System.Windows.Forms.FileDialog' object. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1270'"
			private System.Windows.Forms.FileDialog fc = new System.Windows.Forms.OpenFileDialog();
			
			public SwingFileChooser(System.Windows.Forms.Control parent, DirectoryConfigurer prefs, int mode):base(parent, prefs)
			{
				if (prefs != null && prefs.FileValue != null)
				{
					CurrentDirectory = prefs.FileValue;
				}
				if (mode == DIRECTORIES_ONLY)
				{
					FileFilter = new DirectoryFileFilter();
				}
				//UPGRADE_ISSUE: Method 'javax.swing.JFileChooser.setFileSelectionMode' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJFileChoosersetFileSelectionMode_int'"
				fc.setFileSelectionMode(mode);
			}
			
			public override void  rescanCurrentDirectory()
			{
				//UPGRADE_ISSUE: Method 'javax.swing.JFileChooser.rescanCurrentDirectory' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJFileChooserrescanCurrentDirectory'"
				fc.rescanCurrentDirectory();
			}
			
			public override int showOpenDialog(System.Windows.Forms.Control parent)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.JFileChooser.showOpenDialog' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				int value_Renamed = (int) fc.ShowDialog(parent);
				updateDirectoryPreference();
				return value_Renamed;
			}
			
			public override int showSaveDialog(System.Windows.Forms.Control parent)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.JFileChooser.showSaveDialog' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				int value_Renamed = (int) fc.ShowDialog(parent);
				bool tmpBool;
				if (System.IO.File.Exists(SelectedFile.FullName))
					tmpBool = true;
				else
					tmpBool = System.IO.Directory.Exists(SelectedFile.FullName);
				//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.NO_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				if (value_Renamed == APPROVE_OPTION && tmpBool && (int) System.Windows.Forms.DialogResult.No == SupportClass.OptionPaneSupport.ShowConfirmDialog(parent, "Overwrite " + SelectedFile.Name + "?", "File Exists", (int) System.Windows.Forms.MessageBoxButtons.YesNo))
				{
					value_Renamed = CANCEL_OPTION;
				}
				updateDirectoryPreference();
				return value_Renamed;
			}
			
			public override void  addChoosableFileFilter(FileFilter filter)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.JFileChooser.addChoosableFileFilter' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJFileChooseraddChoosableFileFilter_javaxswingfilechooserFileFilter'"
				fc.addChoosableFileFilter(filter);
			}
			
			public override bool removeChoosableFileFilter(FileFilter filter)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.JFileChooser.removeChoosableFileFilter' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJFileChooserremoveChoosableFileFilter_javaxswingfilechooserFileFilter'"
				return fc.removeChoosableFileFilter(filter);
			}
			
			public override void  resetChoosableFileFilters()
			{
				//UPGRADE_TODO: Method 'javax.swing.JFileChooser.resetChoosableFileFilters' was converted to 'System.Windows.Forms.OpenFileDialog.Filter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJFileChooserresetChoosableFileFilters'"
				fc.Filter = "All files (*.*)|*.*";
			}
		}
		
		private class NativeFileChooser:FileChooser
		{
			private void  InitBlock()
			{
				mode = FILES_ONLY;
			}
			override public System.IO.FileInfo CurrentDirectory
			{
				get
				{
					return cur == null?null:new System.IO.FileInfo(cur.DirectoryName);
				}
				
				set
				{
					cur = value;
				}
				
			}
			override public System.IO.FileInfo SelectedFile
			{
				get
				{
					return cur;
				}
				
				set
				{
					cur = value;
				}
				
			}
			virtual public int FileSelectionMode
			{
				get
				{
					return mode;
				}
				
				set
				{
					this.mode = value;
				}
				
			}
			override public System.String DialogTitle
			{
				get
				{
					return title;
				}
				
				set
				{
					this.title = value;
				}
				
			}
			override public FileFilter FileFilter
			{
				get
				{
					return filter;
				}
				
				set
				{
					this.filter = value;
				}
				
			}
			private System.IO.FileInfo cur;
			private System.String title;
			private FileFilter filter;
			//UPGRADE_NOTE: The initialization of  'mode' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private int mode;
			
			public NativeFileChooser(System.Windows.Forms.Control parent, DirectoryConfigurer prefs, int mode):base(parent, prefs)
			{
				
				if (prefs != null && prefs.FileValue != null)
				{
					CurrentDirectory = prefs.FileValue;
				}
				
				this.mode = mode;
				
				if (mode == DIRECTORIES_ONLY)
				{
					FileFilter = new DirectoryFileFilter();
				}
			}
			
			public override void  rescanCurrentDirectory()
			{
			}
			
			//
			// On Java 1.5, we cannot create parentless Dialogs. Instead, we make
			// a single global hidden Frame to be the parent of all such orphans.
			// This can be removed when we stop supporting Java 1.5.
			//
			protected internal static bool isJava15;
			//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
			protected internal static System.Windows.Forms.Form dummy;
			
			protected internal virtual System.Windows.Forms.FileDialog awt_file_dialog_init(System.Windows.Forms.Control parent)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'fd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.FileDialog fd;
				
				if (parent == null)
				{
					if (isJava15)
					{
						// Parentless Dialogs throw IllegalArgumentException with Java 1.5.
						System.Windows.Forms.OpenFileDialog temp_file_dialog;
						temp_file_dialog = new System.Windows.Forms.OpenFileDialog();
						temp_file_dialog.Title = title;
						fd = temp_file_dialog;
					}
					else
					{
						System.Windows.Forms.OpenFileDialog temp_file_dialog2;
						temp_file_dialog2 = new System.Windows.Forms.OpenFileDialog();
						temp_file_dialog2.Title = title;
						fd = temp_file_dialog2;
					}
				}
				else if (parent is System.Windows.Forms.Form)
				{
					fd = new FileDialog((System.Windows.Forms.Form) parent, title);
				}
				else
				{
					//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
					if (parent is System.Windows.Forms.Form)
					{
						System.Windows.Forms.OpenFileDialog temp_file_dialog3;
						temp_file_dialog3 = new System.Windows.Forms.OpenFileDialog();
						temp_file_dialog3.Title = title;
						fd = temp_file_dialog3;
					}
					else
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.getAncestorOfClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
						System.Windows.Forms.Form d = (System.Windows.Forms.Form) SwingUtilities.getAncestorOfClass(typeof(System.Windows.Forms.Form), parent);
						if (d != null)
						{
							fd = new FileDialog(d, title);
						}
						else
						{
							//UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
							//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.getAncestorOfClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
							System.Windows.Forms.Form f = (System.Windows.Forms.Form) SwingUtilities.getAncestorOfClass(typeof(System.Windows.Forms.Form), parent);
							if (f != null)
							{
								System.Windows.Forms.OpenFileDialog temp_file_dialog4;
								temp_file_dialog4 = new System.Windows.Forms.OpenFileDialog();
								temp_file_dialog4.Title = title;
								fd = temp_file_dialog4;
							}
							else
							{
								// should be impossible, parent is not in a dialog or frame!
								throw new System.ArgumentException("parent is contained in neither a Dialog nor a Frame");
							}
						}
					}
				}
				
				//UPGRADE_ISSUE: Method 'java.awt.Dialog.setModal' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtDialogsetModal_boolean'"
				fd.setModal(true);
				//UPGRADE_ISSUE: Method 'java.awt.FileDialog.setFilenameFilter' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtFileDialogsetFilenameFilter_javaioFilenameFilter'"
				fd.setFilenameFilter(filter);
				if (cur != null)
				{
					if (System.IO.Directory.Exists(cur.FullName))
						fd.InitialDirectory = cur.FullName;
					else
					{
						fd.InitialDirectory = cur.DirectoryName;
						fd.FileName = cur.Name;
					}
				}
				return fd;
			}
			
			public override int showOpenDialog(System.Windows.Forms.Control parent)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'fd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.FileDialog fd = awt_file_dialog_init(parent);
				//UPGRADE_ISSUE: Method 'java.awt.FileDialog.setMode' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtFileDialogsetMode_int'"
				fd.setMode(0);
				//UPGRADE_ISSUE: Method 'java.lang.System.setProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangSystem'"
				System_Renamed.setProperty("apple.awt.fileDialogForDirectories", System.Convert.ToString(mode == DIRECTORIES_ONLY));
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				SupportClass.ShowDialog(fd, true);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int value_Renamed;
				//UPGRADE_TODO: Method 'java.awt.FileDialog.getFile' was converted to 'System.Windows.Forms.FileDialog.FileName' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFileDialoggetFile'"
				if (fd.FileName != null)
				{
					//UPGRADE_TODO: Method 'java.awt.FileDialog.getFile' was converted to 'System.Windows.Forms.FileDialog.FileName' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFileDialoggetFile'"
					cur = new System.IO.FileInfo(fd.InitialDirectory + "\\" + fd.FileName);
					value_Renamed = FileChooser.APPROVE_OPTION;
				}
				else
				{
					value_Renamed = FileChooser.CANCEL_OPTION;
				}
				updateDirectoryPreference();
				return value_Renamed;
			}
			
			public override int showSaveDialog(System.Windows.Forms.Control parent)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'fd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.FileDialog fd = awt_file_dialog_init(parent);
				//UPGRADE_ISSUE: Method 'java.awt.FileDialog.setMode' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtFileDialogsetMode_int'"
				fd.setMode(1);
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				SupportClass.ShowDialog(fd, true);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int value_Renamed;
				//UPGRADE_TODO: Method 'java.awt.FileDialog.getFile' was converted to 'System.Windows.Forms.FileDialog.FileName' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFileDialoggetFile'"
				if (fd.FileName != null)
				{
					//UPGRADE_TODO: Method 'java.awt.FileDialog.getFile' was converted to 'System.Windows.Forms.FileDialog.FileName' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFileDialoggetFile'"
					cur = new System.IO.FileInfo(fd.InitialDirectory + "\\" + fd.FileName);
					value_Renamed = FileChooser.APPROVE_OPTION;
				}
				else
				{
					value_Renamed = FileChooser.CANCEL_OPTION;
				}
				updateDirectoryPreference();
				return value_Renamed;
			}
			
			public override void  addChoosableFileFilter(FileFilter filter)
			{
			}
			
			public override bool removeChoosableFileFilter(FileFilter filter)
			{
				return false;
			}
			
			public override void  resetChoosableFileFilters()
			{
			}
			static NativeFileChooser()
			{
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'jvmver '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_ISSUE: Method 'java.lang.System.getProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangSystem'"
					System.String jvmver = System_Renamed.getProperty("java.version");
					isJava15 = jvmver == null || jvmver.StartsWith("1.5");
					//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
					dummy = isJava15?new System.Windows.Forms.Form():null;
				}
			}
		}
	}
}