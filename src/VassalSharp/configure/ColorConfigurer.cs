/*
* $Id$
*
* Copyright (c) 2000-2008 by Rodney Kinney, Brent Easton, Joel Uckelman
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
using BadDataReport = VassalSharp.build.BadDataReport;
using ColorButton = VassalSharp.tools.ColorButton;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
namespace VassalSharp.configure
{
	
	/// <summary> Configurer for {@link Color} values.</summary>
	public class ColorConfigurer:Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(ColorConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ColorConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ColorConfigurer enclosingInstance;
			public ColorConfigurer Enclosing_Instance
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
			}
		}
		override public System.String ValueString
		{
			get
			{
				System.Drawing.Color tempAux = colorValue();
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return value_Renamed == null?"":colorToString(ref tempAux);
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
					
					System.Drawing.Color tempAux = colorValue();
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					cb = new ColorButton(ref tempAux);
					cb.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(cb);
					
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(cb);
				}
				return p;
			}
			
		}
		private System.Windows.Forms.Panel p;
		private ColorButton cb;
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public ColorConfigurer(System.String key, System.String name):this(key, name, ref tempAux)
		{
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public ColorConfigurer(System.String key, System.String name, ref System.Drawing.Color val):base(key, name, val)
		{
		}
		
		public override void  setValue(System.Object o)
		{
			base.setValue(o);
			if (cb != null)
			{
				cb.Color = (System.Drawing.Color) o;
			}
		}
		
		public override void  setValue(System.String s)
		{
			setValue(stringToColor(s));
		}
		
		private System.Drawing.Color colorValue()
		{
			return (System.Drawing.Color) value_Renamed;
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
			if (s == null || s.Length == 0 || "null".Equals(s))
			{
				return System.Drawing.Color.Empty;
			}
			
			try
			{
				if (s.StartsWith("0X") || s.StartsWith("0x"))
				{
					//UPGRADE_ISSUE: Method 'java.awt.Color.decode' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtColordecode_javalangString'"
					return Color.decode(s);
				}
				else
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					SupportClass.Tokenizer st = new SupportClass.Tokenizer(s, ",");
					return System.Drawing.Color.FromArgb(System.Int32.Parse(st.NextToken()), System.Int32.Parse(st.NextToken()), System.Int32.Parse(st.NextToken()));
				}
			}
			catch (System.FormatException e)
			{
				ErrorDialog.dataError(new BadDataReport("not an integer", s, e));
			}
			catch (System.ArgumentException e)
			{
				ErrorDialog.dataError(new BadDataReport("bad color", s, e));
			}
			catch (System.ArgumentOutOfRangeException e)
			{
				ErrorDialog.dataError(new BadDataReport("bad color", s, e));
			}
			
			// default to black in case of bad data
			return Color.BLACK;
		}
		private static System.Drawing.Color tempAux = System.Drawing.Color.Black;
	}
}