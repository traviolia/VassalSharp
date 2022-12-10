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
using Prefs = VassalSharp.preferences.Prefs;
using ArchiveWriter = VassalSharp.tools.ArchiveWriter;
using FileChooser = VassalSharp.tools.filechooser.FileChooser;
namespace VassalSharp.configure
{
	
	/// <summary> A Configurer for java.io.File values</summary>
	public class FileConfigurer:Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDocumentListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_TODO: Interface 'javax.swing.event.DocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		private class AnonymousClassDocumentListener : DocumentListener
		{
			public AnonymousClassDocumentListener(FileConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(FileConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private FileConfigurer enclosingInstance;
			public FileConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
			public virtual void  changedUpdate(DocumentEvent evt)
			{
				update();
			}
			
			//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
			public virtual void  insertUpdate(DocumentEvent evt)
			{
				update();
			}
			
			//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
			public virtual void  removeUpdate(DocumentEvent evt)
			{
				update();
			}
			
			public virtual void  update()
			{
				System.String text = Enclosing_Instance.tf.Text;
				System.IO.FileInfo f = text != null && text.Length > 0 && !"null".Equals(text)?new System.IO.FileInfo(text):null;
				Enclosing_Instance.noUpdate = true;
				Enclosing_Instance.setValue(f);
				Enclosing_Instance.noUpdate = false;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(FileConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(FileConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private FileConfigurer enclosingInstance;
			public FileConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.chooseNewValue();
			}
		}
		private class AnonymousClassPropertyChangeListener
		{
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				System.Console.Error.WriteLine(System.Convert.ToString(evt.NewValue));
			}
		}
		override public System.String ValueString
		{
			get
			{
				if (archive == null)
				{
					return FileValue == null?"null":FileValue.FullName;
				}
				else
				{
					return FileValue == null?"null":FileValue.Name;
				}
			}
			
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (p == null)
				{
					p = new System.Windows.Forms.Panel();
					//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
					//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.X_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					p.setLayout(new BoxLayout(p, BoxLayout.X_AXIS));
					System.Windows.Forms.Label temp_label2;
					temp_label2 = new System.Windows.Forms.Label();
					temp_label2.Text = getName();
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control;
					temp_Control = temp_label2;
					p.Controls.Add(temp_Control);
					System.Windows.Forms.Button b = SupportClass.ButtonSupport.CreateStandardButton("Select");
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(b);
					
					System.Windows.Forms.TextBox temp_text_box;
					temp_text_box = new System.Windows.Forms.TextBox();
					temp_text_box.Text = ValueString;
					tf = temp_text_box;
					tf.ReadOnly = !editable;
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetMaximumSize'"
					tf.setMaximumSize(new System.Drawing.Size(tf.getMaximumSize().Width, tf.Size.Height));
					//UPGRADE_ISSUE: Method 'javax.swing.text.Document.addDocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDocument'"
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.getDocument' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentgetDocument'"
					((System.String) tf.Text).addDocumentListener(new AnonymousClassDocumentListener(this));
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(tf);
					b.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(b);
				}
				return p;
			}
			
		}
		virtual public System.IO.FileInfo FileValue
		{
			get
			{
				return (System.IO.FileInfo) value_Renamed;
			}
			
		}
		virtual public bool Editable
		{
			get
			{
				return editable;
			}
			
			set
			{
				this.editable = value;
				if (tf != null)
				{
					tf.ReadOnly = !value;
				}
			}
			
		}
		protected internal ArchiveWriter archive;
		protected internal System.Windows.Forms.Panel p;
		protected internal System.Windows.Forms.TextBox tf;
		protected internal FileChooser fc;
		protected internal bool editable;
		protected internal DirectoryConfigurer startingDirectory;
		
		public FileConfigurer(System.String key, System.String name):this(key, name, (DirectoryConfigurer) null)
		{
		}
		
		/// <summary> </summary>
		/// <param name="key">
		/// </param>
		/// <param name="name">
		/// </param>
		/// <param name="startingDirectory">If non-null, points to a preferences setting that specifies the starting directory for the "Select" button
		/// </param>
		public FileConfigurer(System.String key, System.String name, DirectoryConfigurer startingDirectory):base(key, name)
		{
			setValue(null);
			editable = true;
			this.startingDirectory = startingDirectory;
			fc = initFileChooser();
		}
		
		protected internal virtual FileChooser initFileChooser()
		{
			FileChooser fc = FileChooser.createFileChooser(null, startingDirectory);
			if (startingDirectory == null && GameModule.getGameModule() != null)
			{
				fc.CurrentDirectory = (System.IO.FileInfo) Prefs.GlobalPrefs.getValue(Prefs.MODULES_DIR_KEY);
			}
			return fc;
		}
		
		/// <summary> If a non-null {@link ArchiveWriter} is used in the constructor, then invoking {@link #setValue} on this
		/// FileConfigurer will automatically add the file to the archive
		/// </summary>
		public FileConfigurer(System.String key, System.String name, ArchiveWriter archive):this(key, name)
		{
			this.archive = archive;
		}
		
		public override void  setValue(System.Object o)
		{
			// FIXME: this creates a problem when the referenced file is in the JAR
			System.IO.FileInfo f = (System.IO.FileInfo) o;
			bool tmpBool;
			if (System.IO.File.Exists(f.FullName))
				tmpBool = true;
			else
				tmpBool = System.IO.Directory.Exists(f.FullName);
			if (f != null && tmpBool)
			{
				if (archive != null)
				{
					addToArchive(f);
				}
			}
			base.setValue(f);
			if (tf != null && !noUpdate)
			{
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				tf.Text = ValueString;
			}
		}
		
		protected internal virtual void  addToArchive(System.IO.FileInfo f)
		{
			archive.addFile(f.FullName, f.Name);
		}
		
		public override void  setValue(System.String s)
		{
			if (s == null)
				setValue((System.Object) null);
			else
			{
				setValue(new System.IO.FileInfo(s));
			}
		}
		
		public virtual void  chooseNewValue()
		{
			if (fc.showOpenDialog(Controls) != FileChooser.APPROVE_OPTION)
			{
				setValue((System.Object) null);
			}
			else
			{
				bool tmpBool;
				if (System.IO.File.Exists(fc.SelectedFile.FullName))
					tmpBool = true;
				else
					tmpBool = System.IO.Directory.Exists(fc.SelectedFile.FullName);
				setValue(tmpBool?fc.SelectedFile:(System.Object) null);
			}
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Form f = new System.Windows.Forms.Form();
			//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FileConfigurer c = new ImageConfigurer(null, "Test file", new ArchiveWriter("testArchive"));
			c.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener().propertyChange);
			//UPGRADE_TODO: Method 'javax.swing.JFrame.getContentPane' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJFramegetContentPane'"
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			((System.Windows.Forms.ContainerControl) f).Controls.Add(c.Controls);
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			f.pack();
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			f.Visible = true;
		}
	}
}