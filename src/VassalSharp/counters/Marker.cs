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
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Command = VassalSharp.command.Command;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> A generic Decorator that retains in its state the value of a property.
	/// That is, if {@link #setProperty(Object,Object)} is invoked with a key
	/// that is one of {@link #getKeys()}, the <code>String</code> value of that
	/// property will be reflected in the {@link #myGetState(String)} method.
	/// </summary>
	public class Marker:Decorator, EditablePiece
	{
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < String > l = new ArrayList < String >();
			for (int i = 0; i < keys.Length; ++i)
			{
				l.add(keys[i]);
			}
			return l;
		}
		virtual public System.String[] Keys
		{
			get
			{
				return keys;
			}
			
		}
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		override public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				return piece.Shape;
			}
			
		}
		virtual public System.String Description
		{
			get
			{
				if (keys != null && keys.Length > 0 && keys[0].Length > 0 && values.Length > 0 && values[0].Length > 0)
				{
					return "Marker - " + keys[0] + " = " + values[0];
				}
				else
					return "Marker";
			}
			
		}
		virtual public VassalSharp.build.module.documentation.HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("PropertyMarker.htm");
			}
			
		}
		public const System.String ID = "mark;";
		
		protected internal System.String[] keys;
		protected internal System.String[] values;
		
		public Marker():this(ID, null)
		{
		}
		
		public Marker(System.String type, GamePiece p)
		{
			InitBlock();
			mySetType(type);
			setInner(p);
		}
		
		public virtual void  mySetType(System.String s)
		{
			s = s.Substring(ID.Length);
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(s, ',');
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < String > l = new ArrayList < String >();
			while (st.hasMoreTokens())
			{
				l.add(st.nextToken());
			}
			keys = l.toArray(new System.String[l.size()]);
			values = new System.String[keys.Length];
			SupportClass.ArraySupport.Fill(values, "");
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			piece.draw(g, x, y, obs, zoom);
		}
		
		public override System.String getName()
		{
			return piece.getName();
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			return piece.boundingBox();
		}
		
		public override System.Object getProperty(System.Object key)
		{
			for (int i = 0; i < keys.Length; ++i)
			{
				if (keys[i].Equals(key))
				{
					return values[i];
				}
			}
			return base.getProperty(key);
		}
		
		public override System.Object getLocalizedProperty(System.Object key)
		{
			for (int i = 0; i < keys.Length; ++i)
			{
				if (keys[i].Equals(key))
				{
					return values[i];
				}
			}
			return base.getLocalizedProperty(key);
		}
		
		public override void  setProperty(System.Object key, System.Object value_Renamed)
		{
			for (int i = 0; i < keys.Length; ++i)
			{
				if (keys[i].Equals(key))
				{
					values[i] = ((System.String) value_Renamed);
					return ;
				}
			}
			base.setProperty(key, value_Renamed);
		}
		
		public override System.String myGetState()
		{
			SequenceEncoder se = new SequenceEncoder(',');
			for (int i = 0; i < values.Length; ++i)
			{
				se.append(values[i]);
			}
			return se.Value;
		}
		
		public override void  mySetState(System.String state)
		{
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(state, ',');
			int i = 0;
			while (st.hasMoreTokens() && i < values.Length)
			{
				values[i++] = st.nextToken();
			}
		}
		
		public override System.String myGetType()
		{
			SequenceEncoder se = new SequenceEncoder(',');
			for (int i = 0; i < keys.Length; ++i)
			{
				se.append(keys[i]);
			}
			return ID + se.Value;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			return new KeyCommand[0];
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			return null;
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		/// <summary> Return Property names exposed by this trait</summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getPropertyNames()
		
		private class Ed : PieceEditor
		{
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return panel;
				}
				
			}
			virtual public System.String State
			{
				get
				{
					return propValue.ValueString;
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					return Marker.ID + propName.ValueString;
				}
				
			}
			private StringConfigurer propName;
			private StringConfigurer propValue;
			private System.Windows.Forms.Panel panel;
			
			internal Ed(Marker m)
			{
				panel = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				panel.setLayout(new BoxLayout(panel, BoxLayout.Y_AXIS));
				SequenceEncoder seKeys = new SequenceEncoder(',');
				for (int i = 0; i < m.keys.Length; ++i)
				{
					seKeys.append(m.keys[i]);
				}
				
				SequenceEncoder seValues = new SequenceEncoder(',');
				for (int i = 0; i < m.values.Length; ++i)
				{
					seValues.append(m.values[i]);
				}
				
				propName = new StringConfigurer(null, "Property name:  ", m.keys.Length == 0?"":seKeys.Value);
				propValue = new StringConfigurer(null, "Property value:  ", m.values.Length == 0?"":seValues.Value);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(propName.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(propValue.Controls);
			}
		}
	}
}