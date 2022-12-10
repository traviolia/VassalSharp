/*
* $Id$
*
* Copyright (c) 2003 by Rodney Kinney
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
using FileChooser = VassalSharp.tools.filechooser.FileChooser;
using ImageFileFilter = VassalSharp.tools.filechooser.ImageFileFilter;
using Op = VassalSharp.tools.imageop.Op;
namespace VassalSharp.configure
{
	
	public class IconConfigurer:Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassJPanel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassJPanel:System.Windows.Forms.Panel
		{
			public AnonymousClassJPanel(IconConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(IconConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private IconConfigurer enclosingInstance;
			public IconConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			protected override void  OnPaint(System.Windows.Forms.PaintEventArgs g_EventArg)
			{
				System.Drawing.Graphics g = null;
				if (g_EventArg != null)
					g = g_EventArg.Graphics;
				g.FillRegion(new System.Drawing.SolidBrush(SupportClass.GraphicsManager.manager.GetBackColor(g)), new System.Drawing.Region(new System.Drawing.Rectangle(0, 0, Size.Width, Size.Height)));
				//UPGRADE_NOTE: Final was removed from the declaration of 'i '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Image i = Enclosing_Instance.IconValue;
				if (i != null)
				{
					//UPGRADE_TODO: Method 'javax.swing.Icon.paintIcon' was converted to 'System.Drawing.Graphics.drawImage' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingIconpaintIcon_javaawtComponent_javaawtGraphics_int_int'"
					g.DrawImage(i, Size.Width / 2 - i.Width / 2, Size.Height / 2 - i.Height / 2);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(System.Windows.Forms.Panel p, IconConfigurer enclosingInstance)
			{
				InitBlock(p, enclosingInstance);
			}
			private void  InitBlock(System.Windows.Forms.Panel p, IconConfigurer enclosingInstance)
			{
				this.p = p;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable p was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.Panel p;
			private IconConfigurer enclosingInstance;
			public IconConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.selectImage();
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				p.Refresh();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(System.Windows.Forms.Panel p, IconConfigurer enclosingInstance)
			{
				InitBlock(p, enclosingInstance);
			}
			private void  InitBlock(System.Windows.Forms.Panel p, IconConfigurer enclosingInstance)
			{
				this.p = p;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable p was copied into class AnonymousClassActionListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.Panel p;
			private IconConfigurer enclosingInstance;
			public IconConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.setValue(Enclosing_Instance.defaultImage);
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				p.Refresh();
			}
		}
		override public System.String ValueString
		{
			get
			{
				return imageName;
			}
			
		}
		virtual public System.Drawing.Image IconValue
		{
			get
			{
				return icon;
			}
			
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (controls == null)
				{
					controls = new System.Windows.Forms.Panel();
					//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
					//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.X_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					controls.setLayout(new BoxLayout(controls, BoxLayout.X_AXIS));
					System.Windows.Forms.Label temp_label2;
					temp_label2 = new System.Windows.Forms.Label();
					temp_label2.Text = getName();
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control;
					temp_Control = temp_label2;
					controls.Controls.Add(temp_Control);
					//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Windows.Forms.Panel p = new AnonymousClassJPanel(this);
					//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					p.Size = new System.Drawing.Size(32, 32);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					controls.Controls.Add(p);
					//UPGRADE_NOTE: Final was removed from the declaration of 'reset '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Windows.Forms.Button reset = SupportClass.ButtonSupport.CreateStandardButton("Select");
					reset.Click += new System.EventHandler(new AnonymousClassActionListener(p, this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(reset);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					controls.Controls.Add(reset);
					if (defaultImage != null)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'useDefault '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.Windows.Forms.Button useDefault = SupportClass.ButtonSupport.CreateStandardButton("Default");
						useDefault.Click += new System.EventHandler(new AnonymousClassActionListener1(p, this).actionPerformed);
						SupportClass.CommandManager.CheckCommand(useDefault);
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
						controls.Controls.Add(useDefault);
					}
				}
				return controls;
			}
			
		}
		private System.Windows.Forms.Panel controls;
		private System.String imageName;
		private System.String defaultImage;
		private System.Drawing.Image icon;
		
		public IconConfigurer(System.String key, System.String name, System.String defaultImage):base(key, name)
		{
			this.defaultImage = defaultImage;
		}
		
		public override void  setValue(System.String s)
		{
			icon = null;
			imageName = s == null?"":s;
			
			if (imageName.Length > 0)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'img '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Image img = Op.load(imageName).getImage();
				if (img != null)
					icon = (System.Drawing.Image) img.Clone();
			}
			
			setValue((System.Object) imageName);
		}
		
		private void  selectImage()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'fc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FileChooser fc = GameModule.getGameModule().getFileChooser();
			fc.FileFilter = new ImageFileFilter();
			
			if (fc.showOpenDialog(Controls) != FileChooser.APPROVE_OPTION)
			{
				setValue(null);
			}
			else
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.IO.FileInfo f = fc.SelectedFile;
				bool tmpBool;
				if (System.IO.File.Exists(f.FullName))
					tmpBool = true;
				else
					tmpBool = System.IO.Directory.Exists(f.FullName);
				if (f != null && tmpBool)
				{
					GameModule.getGameModule().getArchiveWriter().addImage(f.FullName, f.Name);
					setValue(f.Name);
				}
			}
		}
	}
}