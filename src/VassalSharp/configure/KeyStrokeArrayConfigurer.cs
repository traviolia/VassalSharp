/*
* $Id$
*
* Copyright (c) 2004 by Rodney Kinney
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
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.configure
{
	
	/// <summary> Configures an array of keystrokes</summary>
	public class KeyStrokeArrayConfigurer:Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(KeyStrokeArrayConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(KeyStrokeArrayConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private KeyStrokeArrayConfigurer enclosingInstance;
			public KeyStrokeArrayConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.addKey(null);
			}
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (panel == null)
				{
					//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
					new BorderLayout();
					//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
					panel = new System.Windows.Forms.Panel();
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					controls = Box.createVerticalBox();
					//UPGRADE_NOTE: Final was removed from the declaration of 'scroll '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
					System.Windows.Forms.ScrollableControl temp_scrollablecontrol;
					temp_scrollablecontrol = new System.Windows.Forms.ScrollableControl();
					temp_scrollablecontrol.AutoScroll = true;
					temp_scrollablecontrol.Controls.Add(controls);
					System.Windows.Forms.ScrollableControl scroll = temp_scrollablecontrol;
					//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					Box b = Box.createHorizontalBox();
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					controls.Controls.Add(b);
					System.Windows.Forms.Label temp_label;
					temp_label = new System.Windows.Forms.Label();
					temp_label.Text = getName();
					System.Windows.Forms.Label l = temp_label;
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					b.Controls.Add(l);
					System.Windows.Forms.Button button = SupportClass.ButtonSupport.CreateStandardButton("Add");
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					b.Controls.Add(button);
					button.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(button);
					
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
					panel.Controls.Add(scroll);
					scroll.Dock = System.Windows.Forms.DockStyle.Fill;
					scroll.BringToFront();
					
					System.Windows.Forms.KeyEventArgs[] keyStrokes = (System.Windows.Forms.KeyEventArgs[]) value_Renamed;
					if (keyStrokes != null)
					{
						for (int i = 0; i < keyStrokes.Length; i++)
						{
							addKey(keyStrokes[i]);
						}
					}
					addKey(null);
				}
				return panel;
			}
			
		}
		override public System.String ValueString
		{
			get
			{
				return encode(KeyStrokes);
			}
			
		}
		virtual public System.Windows.Forms.KeyEventArgs[] KeyStrokes
		{
			get
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				ArrayList < KeyStroke > l = new ArrayList < KeyStroke >();
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(HotKeyConfigurer hotKeyConfigurer: configs)
				{
					System.Windows.Forms.KeyEventArgs value_Renamed = (System.Windows.Forms.KeyEventArgs) hotKeyConfigurer.getValue();
					if (value_Renamed != null)
					{
						l.add(value_Renamed);
					}
				}
				return l.toArray(new System.Windows.Forms.KeyEventArgs[l.size()]);
			}
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < HotKeyConfigurer > configs = new ArrayList < HotKeyConfigurer >();
		//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
		private Box controls;
		private System.Windows.Forms.Panel panel;
		
		public KeyStrokeArrayConfigurer(System.String key, System.String name):base(key, name)
		{
		}
		
		public KeyStrokeArrayConfigurer(System.String key, System.String name, System.Windows.Forms.KeyEventArgs[] val):base(key, name, val)
		{
		}
		
		private void  addKey(System.Windows.Forms.KeyEventArgs keyStroke)
		{
			HotKeyConfigurer config = new HotKeyConfigurer(null, null, keyStroke);
			configs.add(config);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			controls.Controls.Add(config.Controls);
			if (configs.size() > 5)
			{
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				panel.Size = new System.Drawing.Size(((panel is VassalSharp.tools.ComponentSplitter.SplitPane || panel is VassalSharp.counters.Obscurable.Ed.AnonymousClassJPanel || panel is VassalSharp.counters.NonRectangular.Ed.AnonymousClassJPanel || panel is VassalSharp.build.widget.PieceSlot.Panel || panel is VassalSharp.build.module.map.View || panel is VassalSharp.build.module.map.boardPicker.board.Config.View || panel is VassalSharp.build.module.map.boardPicker.board.mapgrid.SquareGridNumbering.AnonymousClassJPanel || panel is VassalSharp.build.module.map.boardPicker.board.mapgrid.HexGridNumbering.AnonymousClassJPanel || panel is org.netbeans.modules.wizard.InstructionsPanel || panel is org.netbeans.api.wizard.displayer.WizardDisplayerImpl.AnonymousClassJPanel)?(System.Drawing.Size) SupportClass.InvokeMethodAsVirtual(panel, "getPreferredSize", new System.Object[]{}):panel.Size).Width, 150);
			}
			else
			{
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.Drawing.Size tempAux = System.Drawing.Size.Empty;
				panel.Size = tempAux;
			}
			//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
			System.Windows.Forms.Form w = (System.Windows.Forms.Form) controls.Parent;
			if (w != null)
			{
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				w.pack();
			}
		}
		
		public override void  setValue(System.String s)
		{
			setValue(decode(s));
		}
		
		public override void  setValue(System.Object o)
		{
			base.setValue(o);
			if (controls != null)
			{
				System.Windows.Forms.KeyEventArgs[] keyStrokes = (System.Windows.Forms.KeyEventArgs[]) o;
				if (keyStrokes == null)
				{
					keyStrokes = new System.Windows.Forms.KeyEventArgs[0];
				}
				
				for (int i = 0; i < keyStrokes.Length; ++i)
				{
					if (i > configs.size())
					{
						addKey(keyStrokes[i]);
					}
					else
					{
						configs.get_Renamed(i).setValue(keyStrokes[i]);
					}
				}
				
				for (int i = keyStrokes.Length; i < configs.size(); ++i)
				{
					configs.get_Renamed(i).setValue(null);
				}
			}
		}
		
		public static System.Windows.Forms.KeyEventArgs[] decode(System.String s)
		{
			if (s == null)
			{
				return null;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < KeyStroke > l = new ArrayList < KeyStroke >();
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(s, ',');
			while (st.hasMoreTokens())
			{
				l.add(HotKeyConfigurer.decode(st.nextToken()));
			}
			return l.toArray(new System.Windows.Forms.KeyEventArgs[l.size()]);
		}
		
		public static System.String encode(System.Windows.Forms.KeyEventArgs[] keys)
		{
			if (keys == null)
			{
				return null;
			}
			SequenceEncoder se = new SequenceEncoder(',');
			for (int i = 0; i < keys.Length; i++)
			{
				System.Windows.Forms.KeyEventArgs key = keys[i];
				se.append(HotKeyConfigurer.encode(key));
			}
			return se.Value != null?se.Value:"";
		}
	}
}