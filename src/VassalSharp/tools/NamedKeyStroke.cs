/*
* $Id$
*
* Copyright (c) 2008-2009 by Brent Easton
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
namespace VassalSharp.tools
{
	
	/// <summary> A NamedKeyStroke is a KeyStroke with a name given by the module developer.
	/// An actual KeyStroke is allocated from a pool of KeyStrokes at run-time and
	/// associated with the name.
	/// 
	/// KeyStrokes have been replaced by NamedKeyStrokes within Vassal wherever
	/// the Stroke is saved. A standard KeyStroke is represented as a NamedKeyStroke
	/// with a null or zero-length name.
	/// 
	/// NamedKeyStroke variables should never be null, use NULL_KEYSTROKE to
	/// represent a NamedKeyStroke with no value.
	/// </summary>
	public class NamedKeyStroke
	{
		/// <summary> Is there a name associated with this KeyStroke? No name means
		/// it is a standard KeyStroke.
		/// </summary>
		/// <returns>
		/// </returns>
		virtual public bool Named
		{
			get
			{
				return !(name == null || name.Length == 0);
			}
			
		}
		virtual public System.String Name
		{
			get
			{
				return name;
			}
			
		}
		virtual public bool Null
		{
			get
			{
				return (stroke == null && name == null) || (stroke != null & stroke.KeyValue == 0 && (int) stroke.Modifiers == 0);
			}
			
		}
		/// <summary> Return the raw KeyStroke stored in this NamedKeyStroke</summary>
		/// <returns> KeyStroke
		/// </returns>
		virtual public KeyStroke Stroke
		{
			get
			{
				return stroke;
			}
			
		}
		/// <summary> Return the allocated KeyStroke associated with this KeyStroke</summary>
		virtual public KeyStroke KeyStroke
		{
			get
			{
				if (Named)
				{
					return NamedKeyManager.Instance.getKeyStroke(this);
				}
				else
				{
					return Stroke;
				}
			}
			
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'NULL_KEYSTROKE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly NamedKeyStroke NULL_KEYSTROKE = new NamedKeyStroke();
		
		protected internal KeyStroke stroke;
		protected internal System.String name;
		
		public NamedKeyStroke(int code, int modifiers):this(code, modifiers, null)
		{
		}
		
		public NamedKeyStroke(int code, int modifiers, System.String s):this(new KeyStroke(code | modifiers), s)
		{
		}
		
		public NamedKeyStroke(KeyStroke k, System.String s)
		{
			stroke = k;
			name = s;
		}
		
		public NamedKeyStroke(KeyStroke k):this(k, null)
		{
		}
		
		public NamedKeyStroke(System.String s):this(NamedKeyManager.MarkerKeyStroke, s)
		{
		}
		
		public NamedKeyStroke():this(null, null)
		{
		}
		
		public  override bool Equals(System.Object o)
		{
			if (o is NamedKeyStroke)
			{
				if (KeyStroke == null)
				{
					return ((NamedKeyStroke) o).KeyStroke == null;
				}
				else
				{
					return KeyStroke.Equals(((NamedKeyStroke) o).KeyStroke);
				}
			}
			else if (o is System.Windows.Forms.KeyEventArgs)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'a '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				KeyStroke a = KeyStroke;
				if (a == null)
				{
					return o == null;
				}
				return a.Equals(o);
			}
			return false;
		}
		
		public static NamedKeyStroke getNamedKeyStroke(char c)
		{
			return getNamedKeyStroke(c, 0);
		}
		
		public static NamedKeyStroke getNamedKeyStroke(char c, int mod)
		{
			return new NamedKeyStroke(new KeyStroke(c | mod));
		}
		
		public static NamedKeyStroke getNamedKeyStroke(int c, int mod)
		{
			return new NamedKeyStroke(new KeyStroke(c | mod));
		}
		
		//UPGRADE_TODO: The equivalent of method getKeyStrokeForEvent needs to be overloaded, to match all the event parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1142'"
		public static NamedKeyStroke getKeyStrokeForEvent(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.KeyStroke.getKeyStrokeForEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingKeyStrokegetKeyStrokeForEvent_javaawteventKeyEvent'"
			return new NamedKeyStroke(VassalSharp.tools.KeyStroke.getKeyStrokeForEvent(e), 0);
		}
		//UPGRADE_NOTE: The following method implementation was automatically added to preserve functionality. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1306'"
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}