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
using AbstractBuildable = VassalSharp.build.AbstractBuildable;
using Buildable = VassalSharp.build.Buildable;
using GameModule = VassalSharp.build.GameModule;
using Map = VassalSharp.build.module.Map;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.build.module.map
{
	
	/// <summary> This component listens to key events on a Map window and
	/// scrolls the map.  Depending on the USE_ARROWS attribute,
	/// will use number keypad or arrow keys, or will offer a
	/// preferences setting for the user to choose
	/// </summary>
	public class Scroller:AbstractBuildable
	{
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[]{PROMPT};
			}
			
		}
		/// <summary> The attribute name for whether to use arrow keys
		/// instead of number keypad.  Should be one of ALWAYS, NEVER, or PROMPT
		/// </summary>
		public const System.String USE_ARROWS = "useArrows";
		public const System.String ALWAYS = "always";
		public const System.String NEVER = "never";
		public const System.String PROMPT = "prompt";
		
		protected internal System.String usingArrows = PROMPT;
		
		private Map map;
		private char noEcho = (char) (0);
		
		public override void  addTo(Buildable parent)
		{
			map = (Map) parent;
			map.getView().addKeyListener(this);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Boolean tempAux = false;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			BooleanConfigurer c = new BooleanConfigurer(USE_ARROWS, Resources.getString("Scroller.use_arrow_keys_preference"), ref tempAux);
			
			if (ALWAYS.Equals(usingArrows))
			{
				GameModule.getGameModule().getPrefs().addOption(null, c);
				c.setValue((System.Object) true);
			}
			else if (PROMPT.Equals(usingArrows))
			{
				GameModule.getGameModule().getPrefs().addOption(c);
			}
			else if (NEVER.Equals(usingArrows))
			{
				GameModule.getGameModule().getPrefs().addOption(null, c);
				c.setValue((System.Object) false);
			}
		}
		
		public override void  add(Buildable b)
		{
		}
		
		public override void  setAttribute(System.String name, System.Object value_Renamed)
		{
			if (USE_ARROWS.Equals(name))
			{
				usingArrows = ((System.String) value_Renamed);
			}
		}
		
		public override System.String getAttributeValueString(System.String name)
		{
			if (USE_ARROWS.Equals(name))
			{
				return usingArrows;
			}
			else
			{
				return null;
			}
		}
		
		protected internal int xStep = 100;
		protected internal int yStep = 100;
		
		public virtual void  keyPressed(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.Handled)
				return ;
			
			int dx;
			int dy;
			
			if (true.Equals(GameModule.getGameModule().getPrefs().getValue(USE_ARROWS)))
			{
				//UPGRADE_TODO: Method 'java.awt.event.KeyEvent.getKeyCode' was converted to 'System.Windows.Forms.KeyEventArgs.KeyValue' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventKeyEventgetKeyCode'"
				switch (e.KeyValue)
				{
					
					case (int) System.Windows.Forms.Keys.Up:  dx = 0; dy = - 1; break;
					
					case (int) System.Windows.Forms.Keys.Down:  dx = 0; dy = 1; break;
					
					case (int) System.Windows.Forms.Keys.Right:  dx = 1; dy = 0; break;
					
					case (int) System.Windows.Forms.Keys.Left:  dx = - 1; dy = 0; break;
					
					default:  return ;
					
				}
			}
			else
			{
				//UPGRADE_TODO: Method 'java.awt.event.KeyEvent.getKeyCode' was converted to 'System.Windows.Forms.KeyEventArgs.KeyValue' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventKeyEventgetKeyCode'"
				switch (e.KeyValue)
				{
					
					case (int) System.Windows.Forms.Keys.NumPad1:  dx = - 1; dy = 1; noEcho = '1'; break;
					
					case (int) System.Windows.Forms.Keys.NumPad2:  dx = 0; dy = 1; noEcho = '2'; break;
					
					case (int) System.Windows.Forms.Keys.NumPad3:  dx = 1; dy = 1; noEcho = '3'; break;
					
					case (int) System.Windows.Forms.Keys.NumPad4:  dx = - 1; dy = 0; noEcho = '4'; break;
					
					case (int) System.Windows.Forms.Keys.NumPad6:  dx = 1; dy = 0; noEcho = '6'; break;
					
					case (int) System.Windows.Forms.Keys.NumPad7:  dx = - 1; dy = - 1; noEcho = '7'; break;
					
					case (int) System.Windows.Forms.Keys.NumPad8:  dx = 0; dy = - 1; noEcho = '8'; break;
					
					case (int) System.Windows.Forms.Keys.NumPad9:  dx = 1; dy = - 1; noEcho = '9'; break;
					
					default:  return ;
					
				}
			}
			
			map.scroll(dx * xStep, dy * yStep);
			e.Handled = true;
		}
		
		public virtual void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
		}
		
		public virtual void  keyTyped(System.Object event_sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.Handled)
			{
				return ;
			}
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.event.KeyEvent.getKeyChar' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			if (e.KeyChar == noEcho)
			{
				e.Handled = true;
				noEcho = (char) (0);
			}
		}
	}
}