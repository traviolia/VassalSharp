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
//UPGRADE_TODO: The type 'org.apache.commons.lang.StringUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using StringUtils = org.apache.commons.lang.StringUtils;
using AutoConfigurable = VassalSharp.build.AutoConfigurable;
using ScrollPane = VassalSharp.tools.ScrollPane;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.configure
{
	
	/// <summary> A Configurer that allows multi-line string input via a JTextArea</summary>
	public class TextConfigurer:Configurer, ConfigurerFactory
	{
		static private System.Int32 state462;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassKeyAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassKeyAdapter
		{
			public AnonymousClassKeyAdapter(TextConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TextConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TextConfigurer enclosingInstance;
			public TextConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs evt)
			{
				Enclosing_Instance.queueForUpdate(Enclosing_Instance.textArea.Text);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(TextConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassRunnable1 : IThreadRunnable
			{
				public AnonymousClassRunnable1(AnonymousClassRunnable enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(AnonymousClassRunnable enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private AnonymousClassRunnable enclosingInstance;
				public AnonymousClassRunnable Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  Run()
				{
					Enclosing_Instance.Enclosing_Instance.executeUpdate();
				}
			}
			private void  InitBlock(TextConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TextConfigurer enclosingInstance;
			public TextConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				try
				{
					//UPGRADE_TODO: Method 'java.lang.Thread.sleep' was converted to 'System.Threading.Thread.Sleep' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangThreadsleep_long'"
					System.Threading.Thread.Sleep(new System.TimeSpan((System.Int64) 10000 * Enclosing_Instance.updateFrequencey));
				}
				catch (System.Threading.ThreadInterruptedException e)
				{
				}
				
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				SwingUtilities.invokeLater(new AnonymousClassRunnable1(this));
			}
		}
		private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			state462 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		override public System.String ValueString
		{
			get
			{
				return escapeNewlines((System.String) getValue());
			}
			
		}
		virtual public bool WordWrap
		{
			set
			{
				wordWrap = value;
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
					System.Windows.Forms.TextBox temp_TextBox;
					temp_TextBox = new System.Windows.Forms.TextBox();
					temp_TextBox.Multiline = true;
					temp_TextBox.WordWrap = false;
					temp_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
					textArea = temp_TextBox;
					if (wordWrap)
					{
						textArea.WordWrap = true;
						textArea.WordWrap = true;
					}
					textArea.KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.configure.TextConfigurer.keyDown);
					textArea.KeyUp += new System.Windows.Forms.KeyEventHandler(new AnonymousClassKeyAdapter(this).keyReleased);
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
					textArea.Text = (System.String) getValue();
					//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					System.Windows.Forms.ScrollableControl scroll = new ScrollPane(textArea);
					if (name != null)
					{
						//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
						//UPGRADE_ISSUE: Constructor 'javax.swing.border.TitledBorder.TitledBorder' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingborderTitledBorder'"
						System.Windows.Forms.ControlPaint.DrawBorder3D(scroll.CreateGraphics(), 0, 0, scroll.Width, scroll.Height, new TitledBorder(name));
					}
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(scroll);
				}
				return p;
			}
			
		}
		private System.Windows.Forms.TextBox textArea;
		private System.Windows.Forms.Panel p;
		private bool wordWrap;
		
		public TextConfigurer():this(null, null, null)
		{
		}
		
		public TextConfigurer(System.String key, System.String name):this(key, name, "")
		{
		}
		
		public TextConfigurer(System.String key, System.String name, System.String val):base(key, name, val)
		{
		}
		
		public TextConfigurer(System.String key, System.String name, System.String val, bool wrap):this(key, name, val)
		{
			WordWrap = wrap;
		}
		
		public virtual Configurer getConfigurer(AutoConfigurable c, System.String key, System.String name)
		{
			this.key = key;
			this.name = name;
			return this;
		}
		
		/// <summary> Encodes a string by replacing newlines with '|' characters
		/// 
		/// </summary>
		/// <param name="s">
		/// </param>
		/// <returns>
		/// </returns>
		public static System.String escapeNewlines(System.String s)
		{
			SequenceEncoder se = new SequenceEncoder('|');
			SupportClass.Tokenizer st = new SupportClass.Tokenizer(s, "\n\r", true);
			bool wasNewLine = true;
			while (st.HasMoreTokens())
			{
				System.String token = st.NextToken();
				switch (token[0])
				{
					
					case '\n': 
						if (wasNewLine)
						{
							se.append("");
						}
						wasNewLine = true;
						break;
					
					case '\r': 
						break;
					
					default: 
						se.append(token);
						wasNewLine = false;
						break;
					
				}
			}
			return se.Value == null?"":se.Value;
		}
		
		public override void  setValue(System.String s)
		{
			System.String text = restoreNewlines(s);
			setValue((System.Object) text);
		}
		
		public override void  setValue(System.Object o)
		{
			base.setValue(o);
			if (!noUpdate && textArea != null)
			{
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				textArea.Text = (System.String) o;
			}
		}
		
		/// <summary> Restores a string by replacing '|' with newlines
		/// 
		/// </summary>
		/// <param name="s">
		/// </param>
		/// <returns>
		/// </returns>
		public static System.String restoreNewlines(System.String s)
		{
			return StringUtils.join(new SequenceEncoder.Decoder(s, '|'), '\n');
		}
		
		private long lastUpdate = (System.DateTime.Now.Ticks - 621355968000000000) / 10000;
		private System.String updatedValue;
		private bool updateQueued = false;
		private long updateFrequencey = 1000L;
		
		private void  queueForUpdate(System.String s)
		{
			updatedValue = s;
			if ((System.DateTime.Now.Ticks - 621355968000000000) / 10000 > lastUpdate + updateFrequencey)
			{
				executeUpdate();
			}
			else if (!updateQueued)
			{
				updateQueued = true;
				IThreadRunnable delayedUpdate = new AnonymousClassRunnable(this);
				new SupportClass.ThreadClass(new System.Threading.ThreadStart(delayedUpdate.Run)).Start();
			}
		}
		
		private void  executeUpdate()
		{
			noUpdate = true;
			setValue((System.Object) updatedValue);
			lastUpdate = (System.DateTime.Now.Ticks - 621355968000000000) / 10000;
			updateQueued = false;
			noUpdate = false;
		}
	}
}