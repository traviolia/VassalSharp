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
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using Configurer = VassalSharp.configure.Configurer;
namespace VassalSharp.build.module.gamepieceimage
{
	
	/// <summary> Configurer for {@link Color} values</summary>
	public class NewColorConfigurer:Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(NewColorConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(NewColorConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private NewColorConfigurer enclosingInstance;
			public NewColorConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
			{
				Enclosing_Instance.colorBox.Visible = !Enclosing_Instance.bc.booleanValue();
				Enclosing_Instance.swatchBox.Visible = Enclosing_Instance.bc.booleanValue();
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
				((System.Windows.Forms.Form) Enclosing_Instance.bc.Controls.Parent).pack();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(NewColorConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(NewColorConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private NewColorConfigurer enclosingInstance;
			public NewColorConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.getName();
				//UPGRADE_TODO: Method 'javax.swing.JColorChooser.showDialog' was converted to 'SupportClass.ShowColorDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJColorChoosershowDialog_javaawtComponent_javalangString_javaawtColor'"
				Enclosing_Instance.setValue(SupportClass.ShowColorDialog(Enclosing_Instance.colorValue()));
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				Enclosing_Instance.csc.setValue(new ColorSwatch("", ref new System.Drawing.Color[]{(System.Drawing.Color) Enclosing_Instance.getValue()}[0])); //$NON-NLS-1$
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener1
		{
			public AnonymousClassPropertyChangeListener1(NewColorConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(NewColorConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private NewColorConfigurer enclosingInstance;
			public NewColorConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
			{
				Enclosing_Instance.setValue(Enclosing_Instance.csc.ValueColor);
			}
		}
		override public System.String ValueString
		{
			get
			{
				System.Drawing.Color tempAux = colorValue();
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return value_Renamed == null?"":colorToString(ref tempAux); //$NON-NLS-1$
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
					//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					p.setLayout(new BoxLayout(p, BoxLayout.Y_AXIS));
					
					//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					Box box = Box.createHorizontalBox();
					System.Windows.Forms.Label temp_label2;
					temp_label2 = new System.Windows.Forms.Label();
					temp_label2.Text = "Use Named Colors?";
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control;
					temp_Control = temp_label2;
					box.Controls.Add(temp_Control);
					System.Boolean tempAux = false;
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					bc = new BooleanConfigurer(null, "", ref tempAux); //$NON-NLS-1$
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					box.Controls.Add(bc.Controls);
					bc.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(box);
					
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					colorBox = Box.createHorizontalBox();
					System.Windows.Forms.Label temp_label4;
					temp_label4 = new System.Windows.Forms.Label();
					temp_label4.Text = getName();
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control2;
					temp_Control2 = temp_label4;
					colorBox.Controls.Add(temp_Control2);
					cp = new Panel(this);
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
					cp.setMaximumSize(new System.Drawing.Size(40, 40));
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMinimumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMinimumSize_javaawtDimension'"
					cp.setMinimumSize(new System.Drawing.Size(40, 40));
					//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_javaawtDimension'"
					cp.Size = new System.Drawing.Size(40, 40);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					colorBox.Controls.Add(cp);
					System.Windows.Forms.Button b = SupportClass.ButtonSupport.CreateStandardButton("Select");
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					colorBox.Controls.Add(b);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(colorBox);
					
					b.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(b);
					
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					swatchBox = Box.createHorizontalBox();
					csc = new ColorSwatchConfigurer(null, "Select Color:", "WHITE"); //$NON-NLS-2$
					csc.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener1(this).propertyChange);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					swatchBox.Controls.Add(csc.Controls);
					swatchBox.Visible = false;
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(swatchBox);
				}
				return p;
			}
			
		}
		private System.Windows.Forms.Panel p;
		private Panel cp;
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public NewColorConfigurer(System.String key, System.String name):this(key, name, ref tempAux)
		{
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public NewColorConfigurer(System.String key, System.String name, ref System.Drawing.Color val):base(key, name, val)
		{
		}
		
		public override void  setValue(System.Object o)
		{
			//  if (o == null)
			//      o = Color.black;
			base.setValue(o);
			if (cp != null)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				cp.Refresh();
			}
		}
		
		public override void  setValue(System.String s)
		{
			setValue(stringToColor(s));
		}
		
		//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
		protected internal Box colorBox;
		//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
		protected internal Box swatchBox;
		internal BooleanConfigurer bc;
		internal ColorSwatchConfigurer csc;
		
		private System.Drawing.Color colorValue()
		{
			return (System.Drawing.Color) value_Renamed;
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Panel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class Panel:System.Windows.Forms.Panel
		{
			public Panel(NewColorConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(NewColorConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private NewColorConfigurer enclosingInstance;
			public NewColorConfigurer Enclosing_Instance
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
				if (!Enclosing_Instance.colorValue().IsEmpty)
				{
					SupportClass.GraphicsManager.manager.SetColor(g, Enclosing_Instance.colorValue());
					g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), 0, 0, Size.Width, Size.Height);
				}
				else
				{
					g.FillRegion(new System.Drawing.SolidBrush(SupportClass.GraphicsManager.manager.GetBackColor(g)), new System.Drawing.Region(new System.Drawing.Rectangle(0, 0, Size.Width, Size.Height)));
				}
			}
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public static System.String colorToString(ref System.Drawing.Color c)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Color.getRed' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Color.getGreen' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Color.getBlue' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			return c.IsEmpty?null:(int) c.R + "," + (int) c.G + "," + (int) c.B;
		}
		
		public static System.Drawing.Color stringToColor(System.String s)
		{
			if (s == null || "null".Equals(s))
			{
				//$NON-NLS-1$
				return System.Drawing.Color.Empty;
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SupportClass.Tokenizer st = new SupportClass.Tokenizer(s, ","); //$NON-NLS-1$
			try
			{
				return System.Drawing.Color.FromArgb(System.Int32.Parse(st.NextToken()), System.Int32.Parse(st.NextToken()), System.Int32.Parse(st.NextToken()));
			}
			// FIXME: review error message
			catch (System.FormatException e)
			{
				return System.Drawing.Color.Empty;
			}
			catch (System.ArgumentException e)
			{
				return System.Drawing.Color.Empty;
			}
		}
		private static System.Drawing.Color tempAux = System.Drawing.Color.Black;
	}
}