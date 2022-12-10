/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney, Jim Urbas
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
//UPGRADE_TODO: The type 'javax.swing.JSpinner' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using JSpinner = javax.swing.JSpinner;
using GameModule = VassalSharp.build.GameModule;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using ChangePiece = VassalSharp.command.ChangePiece;
using Command = VassalSharp.command.Command;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using ScrollPane = VassalSharp.tools.ScrollPane;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> A Decorator class that endows a GamePiece with a dialog.</summary>
	public class PropertySheet:Decorator, TranslatablePiece
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(PropertySheet enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PropertySheet enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PropertySheet enclosingInstance;
			public PropertySheet Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs event_Renamed)
			{
				if (Enclosing_Instance.applyButton != null)
				{
					Enclosing_Instance.applyButton.Enabled = false;
				}
				Enclosing_Instance.updateStateFromFields();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDocumentListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_TODO: Interface 'javax.swing.event.DocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		private class AnonymousClassDocumentListener : DocumentListener
		{
			public AnonymousClassDocumentListener(PropertySheet enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassRunnable : IThreadRunnable
			{
				public AnonymousClassRunnable(AnonymousClassDocumentListener enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(AnonymousClassDocumentListener enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private AnonymousClassDocumentListener enclosingInstance;
				public AnonymousClassDocumentListener Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  Run()
				{
					Enclosing_Instance.Enclosing_Instance.updateStateFromFields();
				}
			}
			private void  InitBlock(PropertySheet enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PropertySheet enclosingInstance;
			public PropertySheet Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
			public virtual void  insertUpdate(DocumentEvent e)
			{
				update(e);
			}
			
			//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
			public virtual void  removeUpdate(DocumentEvent e)
			{
				update(e);
			}
			
			//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
			public virtual void  changedUpdate(DocumentEvent e)
			{
				update(e);
			}
			
			//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
			public virtual void  update(DocumentEvent e)
			{
				if (!Enclosing_Instance.isUpdating)
				{
					switch (Enclosing_Instance.commitStyle)
					{
						
						case VassalSharp.counters.PropertySheet.COMMIT_IMMEDIATELY: 
							// queue commit operation because it could do something
							// unsafe in a an event update
							//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
							SwingUtilities.invokeLater(new AnonymousClassRunnable(this));
							break;
						
						case VassalSharp.counters.PropertySheet.COMMIT_ON_APPLY: 
							Enclosing_Instance.applyButton.Enabled = true;
							break;
						
						case VassalSharp.counters.PropertySheet.COMMIT_ON_CLOSE: 
							break;
						
						default: 
							throw new System.SystemException();
						
					}
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(PropertySheet enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PropertySheet enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PropertySheet enclosingInstance;
			public PropertySheet Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.updateStateFromFields();
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				Enclosing_Instance.frame.Visible = false;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(PropertySheet enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PropertySheet enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PropertySheet enclosingInstance;
			public PropertySheet Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs evt)
			{
				evt.Cancel = true;
				Enclosing_Instance.updateStateFromFields();
			}
		}
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < String > l = new ArrayList < String >();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(String prop: properties.keySet())
			{
				l.add(prop);
			}
			return l;
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
				return "Property Sheet";
			}
			
		}
		virtual public VassalSharp.build.module.documentation.HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("PropertySheet.htm");
			}
			
		}
		public const System.String ID = "propertysheet;";
		
		protected internal System.String oldState;
		
		// properties
		protected internal System.String menuName;
		protected internal NamedKeyStroke launchKeyStroke;
		protected internal KeyCommand launch;
		protected internal System.Drawing.Color backgroundColor;
		protected internal System.String m_definition;
		
		protected internal PropertySheetDialog frame;
		protected internal System.Windows.Forms.Button applyButton;
		
		// Commit type definitions
		//UPGRADE_NOTE: Final was removed from the declaration of 'COMMIT_VALUES'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		internal static readonly System.String[] COMMIT_VALUES = new System.String[]{"Every Keystroke", "Apply Button or Enter Key", "Close Window or Enter Key"};
		internal const int COMMIT_IMMEDIATELY = 0;
		internal const int COMMIT_ON_APPLY = 1;
		internal const int COMMIT_ON_CLOSE = 2;
		//UPGRADE_NOTE: Final was removed from the declaration of 'COMMIT_DEFAULT '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		internal static readonly int COMMIT_DEFAULT = COMMIT_IMMEDIATELY;
		
		// Field type definitions
		//UPGRADE_NOTE: Final was removed from the declaration of 'TYPE_VALUES'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		internal static readonly System.String[] TYPE_VALUES = new System.String[]{"Text", "Multi-line text", "Label Only", "Tick Marks", "Tick Marks with Max Field", "Tick Marks with Value Field", "Tick Marks with Value & Max", "Spinner"};
		internal const int TEXT_FIELD = 0;
		internal const int TEXT_AREA = 1;
		internal const int LABEL_ONLY = 2;
		internal const int TICKS = 3;
		internal const int TICKS_MAX = 4;
		internal const int TICKS_VAL = 5;
		internal const int TICKS_VALMAX = 6;
		internal const int SPINNER = 7;
		
		protected internal int commitStyle = COMMIT_DEFAULT;
		
		protected internal bool isUpdating;
		
		protected internal System.String state;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected Map < String, Object > properties = new HashMap < String, Object >();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < JComponent > m_fields;
		
		
		internal const char TYPE_DELIMITOR = ';';
		internal const char DEF_DELIMITOR = '~';
		internal const char STATE_DELIMITOR = '~';
		internal const char LINE_DELIMINATOR = '|';
		internal const char VALUE_DELIMINATOR = '/';
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'PropertySheetDialog' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
		[Serializable]
		protected internal class PropertySheetDialog:System.Windows.Forms.Form
		{
			private void  InitBlock(PropertySheet enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PropertySheet enclosingInstance;
			public PropertySheet Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
			public PropertySheetDialog(PropertySheet enclosingInstance, System.Windows.Forms.Form owner):base()
			{
				InitBlock(enclosingInstance);
				//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.SetDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtFrame_boolean'"
				SupportClass.DialogSupport.SetDialog(this, owner);
			}
			
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs event_Renamed)
			{
				if (Enclosing_Instance.applyButton != null)
				{
					Enclosing_Instance.applyButton.Enabled = false;
				}
				Enclosing_Instance.updateStateFromFields();
			}
		}
		
		
		public PropertySheet():this(ID + ";Properties;P;;;;", null)
		{
		}
		
		
		public PropertySheet(System.String type, GamePiece p)
		{
			InitBlock();
			mySetType(type);
			setInner(p);
		}
		
		
		/// <summary>Changes the "type" definition this decoration, which discards all value data and structures.
		/// Format: definition; name; keystroke
		/// </summary>
		public virtual void  mySetType(System.String s)
		{
			
			s = s.Substring(ID.Length);
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(s, TYPE_DELIMITOR);
			
			m_definition = st.nextToken();
			menuName = st.nextToken();
			//UPGRADE_NOTE: Final was removed from the declaration of 'launchKeyToken '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String launchKeyToken = st.nextToken("");
			commitStyle = st.nextInt(COMMIT_DEFAULT);
			System.String red = st.hasMoreTokens()?st.nextToken():"";
			System.String green = st.hasMoreTokens()?st.nextToken():"";
			System.String blue = st.hasMoreTokens()?st.nextToken():"";
			//UPGRADE_NOTE: Final was removed from the declaration of 'launchKeyStrokeToken '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String launchKeyStrokeToken = st.nextToken("");
			
			backgroundColor = red.Equals("")?System.Drawing.Color.Empty:System.Drawing.Color.FromArgb(atoi(red), atoi(green), atoi(blue));
			frame = null;
			
			// Handle conversion from old character only key
			if (launchKeyStrokeToken.Length > 0)
			{
				launchKeyStroke = NamedHotKeyConfigurer.decode(launchKeyStrokeToken);
			}
			else if (launchKeyToken.Length > 0)
			{
				launchKeyStroke = new NamedKeyStroke(launchKeyToken[0], (int) System.Windows.Forms.Keys.Control);
			}
			else
			{
				launchKeyStroke = new NamedKeyStroke('P', (int) System.Windows.Forms.Keys.Control);
			}
		}
		
		public virtual void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
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
		
		public override System.String myGetState()
		{
			return state;
		}
		
		public override void  mySetState(System.String state)
		{
			this.state = state;
			updateFieldsFromState();
		}
		
		/// <summary>returns string defining the field types </summary>
		public override System.String myGetType()
		{
			SequenceEncoder se = new SequenceEncoder(TYPE_DELIMITOR);
			
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Color.getRed' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.String red = backgroundColor.IsEmpty?"":System.Convert.ToString((int) backgroundColor.R);
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Color.getGreen' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.String green = backgroundColor.IsEmpty?"":System.Convert.ToString((int) backgroundColor.G);
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Color.getBlue' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.String blue = backgroundColor.IsEmpty?"":System.Convert.ToString((int) backgroundColor.B);
			System.String commit = System.Convert.ToString(commitStyle);
			
			se.append(m_definition).append(menuName).append("").append(commit).append(red).append(green).append(blue).append(launchKeyStroke);
			
			
			return ID + se.Value;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			launch = new KeyCommand(menuName, launchKeyStroke, Decorator.getOutermost(this), this);
			return new KeyCommand[]{launch};
		}
		
		/// <summary>parses leading integer from string </summary>
		internal virtual int atoi(System.String s)
		{
			int value_Renamed = 0;
			if (s != null)
			{
				for (int i = 0; i < s.Length && System.Char.IsDigit(s[i]); ++i)
				{
					value_Renamed = value_Renamed * 10 + s[i] - '0';
				}
			}
			return value_Renamed;
		}
		
		/// <summary>parses trailing integer from string </summary>
		internal virtual int atoiRight(System.String s)
		{
			int value_Renamed = 0;
			if (s != null)
			{
				int base_Renamed = 1;
				for (int i = s.Length - 1; i >= 0 && System.Char.IsDigit(s[i]); --i, base_Renamed *= 10)
				{
					value_Renamed = value_Renamed + base_Renamed * (s[i] - '0');
				}
			}
			return value_Renamed;
		}
		
		
		// stores field values
		private void  updateStateFromFields()
		{
			SequenceEncoder encoder = new SequenceEncoder(STATE_DELIMITOR);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Object field: m_fields)
			{
				if (field is System.Windows.Forms.TextBoxBase)
				{
					encoder.append(((System.Windows.Forms.TextBoxBase) field).Text.Replace('\n', LINE_DELIMINATOR));
				}
				else if (field is TickPanel)
				{
					encoder.append(((TickPanel) field).Value);
				}
				else
				{
					encoder.append("Unknown");
				}
			}
			
			if (encoder.Value != null && !encoder.Value.Equals(state))
			{
				mySetState(encoder.Value);
				
				GamePiece outer = Decorator.getOutermost(this);
				if (outer.Id != null)
				{
					GameModule.getGameModule().sendAndLog(new ChangePiece(outer.Id, oldState, outer.State));
				}
			}
		}
		
		private void  updateFieldsFromState()
		{
			isUpdating = true;
			properties.clear();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'defDecoder '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder.Decoder defDecoder = new SequenceEncoder.Decoder(m_definition, DEF_DELIMITOR);
			//UPGRADE_NOTE: Final was removed from the declaration of 'stateDecoder '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder.Decoder stateDecoder = new SequenceEncoder.Decoder(state, STATE_DELIMITOR);
			
			for (int iField = 0; defDecoder.hasMoreTokens(); ++iField)
			{
				System.String name = defDecoder.nextToken();
				if (name.Length == 0)
				{
					continue;
				}
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int type = name[0] - '0';
				name = name.Substring(1);
				System.String value_Renamed = stateDecoder.nextToken("");
				switch (type)
				{
					
					case TICKS: 
					case TICKS_VAL: 
					case TICKS_MAX: 
					case TICKS_VALMAX: 
						//UPGRADE_NOTE: Final was removed from the declaration of 'index '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						int index = value_Renamed.IndexOf('/');
						properties.put(name, index > 0?value_Renamed.Substring(0, (index) - (0)):value_Renamed);
						break;
					
					default: 
						properties.put(name, value_Renamed);
						break;
					
				}
				
				value_Renamed = value_Renamed.Replace(LINE_DELIMINATOR, '\n');
				
				if (frame != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'field '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Object field = m_fields.get_Renamed(iField);
					if (field is System.Windows.Forms.TextBoxBase)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'tf '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.Windows.Forms.TextBoxBase tf = (System.Windows.Forms.TextBoxBase) field;
						//UPGRADE_NOTE: Final was removed from the declaration of 'pos '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						int pos = System.Math.Min(tf.SelectionStart, value_Renamed.Length);
						//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
						tf.Text = value_Renamed;
						tf.SelectionStart = pos;
					}
					else if (field is TickPanel)
					{
						((TickPanel) field).updateValue(value_Renamed);
					}
				}
			}
			
			if (applyButton != null)
			{
				applyButton.Enabled = false;
			}
			
			isUpdating = false;
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			myGetKeyCommands();
			
			if (launch.matches(stroke))
			{
				if (frame == null)
				{
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					m_fields = new ArrayList < JComponent >();
					VassalSharp.build.module.Map map = piece.getMap();
					//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
					System.Windows.Forms.Form parent = null;
					if (map != null && map.getView() != null)
					{
						System.Windows.Forms.Control topWin = map.getView().getTopLevelAncestor();
						if (topWin is System.Windows.Forms.Form)
						{
							//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
							parent = (System.Windows.Forms.Form) topWin;
						}
					}
					else
					{
						parent = GameModule.getGameModule().getFrame();
					}
					
					frame = new PropertySheetDialog(this, parent);
					
					System.Windows.Forms.Panel pane = new System.Windows.Forms.Panel();
					//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.HORIZONTAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsHORIZONTAL_SCROLLBAR_AS_NEEDED_f'"
					System.Windows.Forms.ScrollableControl temp_scrollablecontrol;
					//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.VERTICAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsVERTICAL_SCROLLBAR_AS_NEEDED_f'"
					temp_scrollablecontrol = new System.Windows.Forms.ScrollableControl();
					temp_scrollablecontrol.AutoScroll = true;
					temp_scrollablecontrol.Controls.Add(pane);
					System.Windows.Forms.ScrollableControl scroll = temp_scrollablecontrol;
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					frame.Controls.Add(scroll);
					
					// set up Apply button
					if (commitStyle == COMMIT_ON_APPLY)
					{
						applyButton = SupportClass.ButtonSupport.CreateStandardButton("Apply");
						
						applyButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
						SupportClass.CommandManager.CheckCommand(applyButton);
						
						//UPGRADE_ISSUE: Method 'javax.swing.AbstractButton.setMnemonic' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractButtonsetMnemonic_int'"
						applyButton.setMnemonic((int) System.Windows.Forms.Keys.A); // respond to Alt+A
						applyButton.Enabled = false;
					}
					
					// ... enable APPLY button when field changes
					//UPGRADE_TODO: Interface 'javax.swing.event.DocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					DocumentListener changeListener = new AnonymousClassDocumentListener(this);
					
					//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
					//UPGRADE_ISSUE: Constructor 'java.awt.GridBagLayout.GridBagLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagLayout'"
					pane.setLayout(new GridBagLayout());
					//UPGRADE_ISSUE: Class 'java.awt.GridBagConstraints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
					//UPGRADE_ISSUE: Constructor 'java.awt.GridBagConstraints.GridBagConstraints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
					GridBagConstraints c = new GridBagConstraints();
					//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.fill' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
					//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.BOTH' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
					c.fill = GridBagConstraints.BOTH;
					//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.insets' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
					c.insets = new System.Int32[]{1, 3, 1, 3};
					//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
					c.gridx = 0;
					//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridy' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
					c.gridy = 0;
					//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.fill' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
					//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.BOTH' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
					c.fill = GridBagConstraints.BOTH;
					SequenceEncoder.Decoder defDecoder = new SequenceEncoder.Decoder(m_definition, DEF_DELIMITOR);
					SequenceEncoder.Decoder stateDecoder = new SequenceEncoder.Decoder(state, STATE_DELIMITOR);
					
					while (defDecoder.hasMoreTokens())
					{
						System.String code = defDecoder.nextToken();
						if (code.Length == 0)
						{
							break;
						}
						int type = code[0] - '0';
						System.String name = code.Substring(1);
						System.Windows.Forms.Control field;
						switch (type)
						{
							
							case TEXT_FIELD: 
								System.Windows.Forms.TextBox temp_text_box;
								temp_text_box = new System.Windows.Forms.TextBox();
								temp_text_box.Text = stateDecoder.nextToken("");
								field = temp_text_box;
								//UPGRADE_ISSUE: Method 'javax.swing.text.Document.addDocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDocument'"
								//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.getDocument' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentgetDocument'"
								((System.String) ((System.Windows.Forms.TextBoxBase) field).Text).addDocumentListener(changeListener);
								//UPGRADE_TODO: Method 'javax.swing.JTextField.addActionListener' was converted to 'System.Windows.Forms.TextBox.KeyPress' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldaddActionListener_javaawteventActionListener'"
								((System.Windows.Forms.TextBox) field).KeyPress += new System.Windows.Forms.KeyPressEventHandler(frame.actionPerformed);
								m_fields.add(field);
								break;
							
							case TEXT_AREA: 
								System.Windows.Forms.TextBox temp_TextBox;
								temp_TextBox = new System.Windows.Forms.TextBox();
								temp_TextBox.Multiline = true;
								temp_TextBox.WordWrap = false;
								temp_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
								temp_TextBox.Text = stateDecoder.nextToken("").Replace(LINE_DELIMINATOR, '\n');
								field = temp_TextBox;
								//UPGRADE_ISSUE: Method 'javax.swing.text.Document.addDocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDocument'"
								//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.getDocument' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentgetDocument'"
								((System.String) ((System.Windows.Forms.TextBoxBase) field).Text).addDocumentListener(changeListener);
								m_fields.add(field);
								field = new ScrollPane(field);
								break;
							
							case TICKS: 
							case TICKS_VAL: 
							case TICKS_MAX: 
							case TICKS_VALMAX: 
								field = new TickPanel(this, stateDecoder.nextToken(""), type);
								((TickPanel) field).addDocumentListener(changeListener);
								((TickPanel) field).addActionListener(frame);
								if (!backgroundColor.IsEmpty)
									field.BackColor = backgroundColor;
								m_fields.add(field);
								break;
							
							case SPINNER: 
								JSpinner spinner = new JSpinner();
								System.Windows.Forms.TextBox textField = ((JSpinner.DefaultEditor) spinner.getEditor()).getTextField();
								//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
								textField.Text = stateDecoder.nextToken("");
								//UPGRADE_ISSUE: Method 'javax.swing.text.Document.addDocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDocument'"
								//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.getDocument' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentgetDocument'"
								((System.String) textField.Text).addDocumentListener(changeListener);
								m_fields.add(textField);
								field = spinner;
								break;
							
							case LABEL_ONLY: 
							default: 
								stateDecoder.nextToken("");
								field = null;
								m_fields.add(field);
								break;
							}
						//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridwidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
						c.gridwidth = type == TEXT_AREA || type == LABEL_ONLY?2:1;
						
						if (name != null && !name.Equals(""))
						{
							//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
							c.gridx = 0;
							//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weighty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
							c.weighty = 0.0;
							//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weightx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
							//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridwidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
							c.weightx = c.gridwidth == 2?1.0:0.0;
							System.Windows.Forms.Label temp_label2;
							temp_label2 = new System.Windows.Forms.Label();
							temp_label2.Text = getTranslation(name);
							//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
							System.Windows.Forms.Control temp_Control;
							temp_Control = temp_label2;
							pane.Controls.Add(temp_Control);
							temp_Control.Dock = new System.Windows.Forms.DockStyle();
							temp_Control.BringToFront();
							//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridwidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
							if (c.gridwidth == 2)
							{
								//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridy' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
								++c.gridy;
							}
						}
						
						if (field != null)
						{
							//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weightx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
							c.weightx = 1.0;
							//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weighty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
							c.weighty = type == TEXT_AREA?1.0:0.0;
							//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
							c.gridx = type == TEXT_AREA?0:1;
							//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
							pane.Controls.Add(field);
							field.Dock = new System.Windows.Forms.DockStyle();
							field.BringToFront();
							//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridy' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
							++c.gridy;
						}
					}
					
					if (!backgroundColor.IsEmpty)
					{
						pane.BackColor = backgroundColor;
					}
					
					if (commitStyle == COMMIT_ON_APPLY)
					{
						// setup Close button
						System.Windows.Forms.Button closeButton = SupportClass.ButtonSupport.CreateStandardButton("Close");
						//UPGRADE_ISSUE: Method 'javax.swing.AbstractButton.setMnemonic' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractButtonsetMnemonic_int'"
						closeButton.setMnemonic((int) System.Windows.Forms.Keys.C); // respond to Alt+C // key event cannot be resolved
						
						closeButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
						SupportClass.CommandManager.CheckCommand(closeButton);
						
						//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridwidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
						c.gridwidth = 1;
						//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weighty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
						c.weighty = 0.0;
						//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.anchor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
						//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.SOUTHEAST' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
						c.anchor = GridBagConstraints.SOUTHEAST;
						//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridwidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
						c.gridwidth = 2; // use the whole row
						//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
						c.gridx = 0;
						//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weightx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
						c.weightx = 0.0;
						
						System.Windows.Forms.Panel buttonRow = new System.Windows.Forms.Panel();
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
						buttonRow.Controls.Add(applyButton);
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
						buttonRow.Controls.Add(closeButton);
						if (!backgroundColor.IsEmpty)
						{
							applyButton.BackColor = backgroundColor;
							closeButton.BackColor = backgroundColor;
							buttonRow.BackColor = backgroundColor;
						}
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
						pane.Controls.Add(buttonRow);
						buttonRow.Dock = new System.Windows.Forms.DockStyle();
						buttonRow.BringToFront();
					}
					
					
					// move window
					System.Drawing.Point p = GameModule.getGameModule().getFrame().getLocation();
					if (getMap() != null)
					{
						p = getMap().getView().getLocationOnScreen();
						System.Drawing.Point p2 = getMap().componentCoordinates(Position);
						p.Offset(p2.X, p2.Y);
					}
					//UPGRADE_TODO: Method 'java.awt.Component.setLocation' was converted to 'System.Windows.Forms.Control.Location' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetLocation_int_int'"
					frame.Location = new System.Drawing.Point(p.X, p.Y);
					
					// watch for window closing - save state
					//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
					frame.Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(this).windowClosing);
					//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
					frame.pack();
				}
				
				// Name window and make it visible
				frame.Text = LocalizedName;
				oldState = Decorator.getOutermost(this).State;
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				frame.Visible = true;
				return null;
			}
			else
			{
				return null;
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.Object getLocalizedProperty(System.Object key)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Object value_Renamed = properties.get_Renamed(key);
			return value_Renamed == null?base.getLocalizedProperty(key):value_Renamed;
		}
		
		public override System.Object getProperty(System.Object key)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Object value_Renamed = properties.get_Renamed(key);
			return value_Renamed == null?base.getProperty(key):value_Renamed;
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		/// <summary> A generic panel for editing unit traits.
		/// Not directly related to "PropertySheets".
		/// </summary>
		[Serializable]
		internal class PropertyPanel:System.Windows.Forms.Panel
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener2
			{
				public AnonymousClassActionListener2(PropertyPanel enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(PropertyPanel enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private PropertyPanel enclosingInstance;
				public PropertyPanel Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs event_Renamed)
				{
					System.Windows.Forms.Button button = (System.Windows.Forms.Button) event_sender;
					System.Drawing.Color value_Renamed = button.BackColor;
					//UPGRADE_TODO: Method 'javax.swing.JColorChooser.showDialog' was converted to 'SupportClass.ShowColorDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJColorChoosershowDialog_javaawtComponent_javalangString_javaawtColor'"
					System.Drawing.Color newColor = SupportClass.ShowColorDialog(value_Renamed);
					if (!newColor.IsEmpty)
					{
						button.BackColor = newColor;
						button.Text = "sample";
					}
					else
					{
						button.BackColor = Enclosing_Instance.BackColor;
						button.Text = "Default";
					}
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener3
			{
				public AnonymousClassActionListener3(VassalSharp.counters.PropertySheet.PropertyPanel.SmartTable table, PropertyPanel enclosingInstance)
				{
					InitBlock(table, enclosingInstance);
				}
				private void  InitBlock(VassalSharp.counters.PropertySheet.PropertyPanel.SmartTable table, PropertyPanel enclosingInstance)
				{
					this.table = table;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable table was copied into class AnonymousClassActionListener3. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private VassalSharp.counters.PropertySheet.PropertyPanel.SmartTable table;
				private PropertyPanel enclosingInstance;
				public PropertyPanel Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					
					if (((System.Data.DataTable) table.DataSource).DefaultView[table.CurrentCell.RowNumber].IsEdit)
					{
						//UPGRADE_ISSUE: Method 'javax.swing.CellEditor.stopCellEditing' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingCellEditor'"
						//UPGRADE_ISSUE: Method 'javax.swing.JTable.getCellEditor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetCellEditor'"
						table.getCellEditor().stopCellEditing();
					}
					
					//UPGRADE_TODO: Interface 'javax.swing.ListSelectionModel' was converted to 'SupportClass.ListSelectionModelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					//UPGRADE_ISSUE: Method 'javax.swing.JTable.getSelectionModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetSelectionModel'"
					SupportClass.ListSelectionModelSupport selection = table.getSelectionModel();
					int iSelection;
					if (selection.SelectedItems.Count.Equals(0))
					{
						Enclosing_Instance.tableModel.Rows.Add(Enclosing_Instance.defaultValues);
						iSelection = Enclosing_Instance.tableModel.Rows.Count - 1;
					}
					else
					{
						iSelection = selection.GetMaxSelectionIndex();
						//UPGRADE_TODO: Method 'javax.swing.table.DefaultTableModel.insertRow' was converted to 'SupportClass.InsertRow' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtableDefaultTableModelinsertRow_int_javalangObject[]'"
						SupportClass.InsertRow(Enclosing_Instance.tableModel, iSelection, Enclosing_Instance.defaultValues);
					}
					//UPGRADE_ISSUE: Method 'javax.swing.table.AbstractTableModel.fireTableDataChanged' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableAbstractTableModelfireTableDataChanged'"
					Enclosing_Instance.tableModel.fireTableDataChanged(); // BING BING BING
					selection.SetSelectionInterval(iSelection, iSelection);
					table.Focus();
					//UPGRADE_ISSUE: Method 'javax.swing.JTable.editCellAt' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTableeditCellAt_int_int'"
					table.editCellAt(iSelection, 0);
					//UPGRADE_ISSUE: Method 'javax.swing.table.TableCellEditor.getTableCellEditorComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableTableCellEditor'"
					//UPGRADE_ISSUE: Method 'javax.swing.JTable.getCellEditor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetCellEditor'"
					System.Windows.Forms.Control comp = table.getCellEditor().getTableCellEditorComponent(table, null, true, iSelection, 0);
					if (comp is System.Windows.Forms.Control)
					{
						((System.Windows.Forms.Control) comp).Focus();
					}
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener4' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener4
			{
				public AnonymousClassActionListener4(VassalSharp.counters.PropertySheet.PropertyPanel.SmartTable table, PropertyPanel enclosingInstance)
				{
					InitBlock(table, enclosingInstance);
				}
				private void  InitBlock(VassalSharp.counters.PropertySheet.PropertyPanel.SmartTable table, PropertyPanel enclosingInstance)
				{
					this.table = table;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable table was copied into class AnonymousClassActionListener4. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private VassalSharp.counters.PropertySheet.PropertyPanel.SmartTable table;
				private PropertyPanel enclosingInstance;
				public PropertyPanel Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					
					if (((System.Data.DataTable) table.DataSource).DefaultView[table.CurrentCell.RowNumber].IsEdit)
					{
						//UPGRADE_ISSUE: Method 'javax.swing.CellEditor.stopCellEditing' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingCellEditor'"
						//UPGRADE_ISSUE: Method 'javax.swing.JTable.getCellEditor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetCellEditor'"
						table.getCellEditor().stopCellEditing();
					}
					
					//UPGRADE_TODO: Interface 'javax.swing.ListSelectionModel' was converted to 'SupportClass.ListSelectionModelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					//UPGRADE_ISSUE: Method 'javax.swing.JTable.getSelectionModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetSelectionModel'"
					SupportClass.ListSelectionModelSupport selection = table.getSelectionModel();
					for (int i = selection.GetMaxSelectionIndex(); i >= selection.GetMinSelectionIndex(); --i)
					{
						if (selection.SelectedIndices.Contains(i))
						{
							Enclosing_Instance.tableModel.Rows.RemoveAt(i);
						}
					}
					//UPGRADE_ISSUE: Method 'javax.swing.table.AbstractTableModel.fireTableDataChanged' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableAbstractTableModelfireTableDataChanged'"
					Enclosing_Instance.tableModel.fireTableDataChanged(); // BING BING BING
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassListSelectionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassListSelectionListener
			{
				public AnonymousClassListSelectionListener(System.Windows.Forms.Button deleteButton, PropertyPanel enclosingInstance)
				{
					InitBlock(deleteButton, enclosingInstance);
				}
				private void  InitBlock(System.Windows.Forms.Button deleteButton, PropertyPanel enclosingInstance)
				{
					this.deleteButton = deleteButton;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable deleteButton was copied into class AnonymousClassListSelectionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private System.Windows.Forms.Button deleteButton;
				private PropertyPanel enclosingInstance;
				public PropertyPanel Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  valueChanged(System.Object event_sender, System.EventArgs event_Renamed)
				{
					//Ignore extra messages.
					//UPGRADE_ISSUE: Method 'javax.swing.event.ListSelectionEvent.getValueIsAdjusting' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventListSelectionEventgetValueIsAdjusting'"
					if (!event_Renamed.getValueIsAdjusting())
					{
						//UPGRADE_TODO: Interface 'javax.swing.ListSelectionModel' was converted to 'SupportClass.ListSelectionModelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
						//UPGRADE_TODO: The method 'javax.swing.event.ListSelectionEvent.getSource' needs to be in a event handling method in order to be properly converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1171'"
						SupportClass.ListSelectionModelSupport lsm = (SupportClass.ListSelectionModelSupport) event_Renamed.getSource();
						deleteButton.Enabled = !lsm.SelectedItems.Count.Equals(0);
					}
				}
			}
			private const long serialVersionUID = 1L;
			
			//UPGRADE_ISSUE: Class 'java.awt.GridBagConstraints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			//UPGRADE_ISSUE: Constructor 'java.awt.GridBagConstraints.GridBagConstraints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			internal GridBagConstraints c = new GridBagConstraints();
			
			public PropertyPanel():base()
			{
				//UPGRADE_ISSUE: Constructor 'java.awt.GridBagLayout.GridBagLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagLayout'"
				new GridBagLayout();
				//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.insets' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.insets = new System.Int32[]{0, 4, 0, 4};
				//c.ipadx = 5;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.anchor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.WEST' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.anchor = GridBagConstraints.WEST;
			}
			
			public virtual NamedHotKeyConfigurer addKeyStrokeConfig(NamedKeyStroke value_Renamed)
			{
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridy' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				++c.gridy;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.gridx = 0;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weightx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.weightx = 0.0;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.fill' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.NONE' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.fill = GridBagConstraints.NONE;
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = "Keystroke:";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				Controls.Add(temp_Control);
				temp_Control.Dock = new System.Windows.Forms.DockStyle();
				temp_Control.BringToFront();
				
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				++c.gridx;
				//UPGRADE_NOTE: Final was removed from the declaration of 'config '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				NamedHotKeyConfigurer config = new NamedHotKeyConfigurer(null, "", value_Renamed);
				//UPGRADE_NOTE: Final was removed from the declaration of 'field '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Control field = config.Controls;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				Controls.Add(field);
				field.Dock = new System.Windows.Forms.DockStyle();
				field.BringToFront();
				
				return config;
			}
			
			public virtual System.Windows.Forms.TextBox addStringCtrl(System.String name, System.String value_Renamed)
			{
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridy' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				++c.gridy;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.gridx = 0;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weightx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.weightx = 0.0;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.fill' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.HORIZONTAL' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.fill = GridBagConstraints.HORIZONTAL;
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = name;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				Controls.Add(temp_Control);
				temp_Control.Dock = new System.Windows.Forms.DockStyle();
				temp_Control.BringToFront();
				
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weightx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.weightx = 1.0;
				System.Windows.Forms.TextBox temp_text_box;
				temp_text_box = new System.Windows.Forms.TextBox();
				temp_text_box.Text = value_Renamed;
				System.Windows.Forms.TextBox field = temp_text_box;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				++c.gridx;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				Controls.Add(field);
				field.Dock = new System.Windows.Forms.DockStyle();
				field.BringToFront();
				return field;
			}
			
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			public virtual System.Windows.Forms.Button addColorCtrl(System.String name, ref System.Drawing.Color value_Renamed)
			{
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridy' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				++c.gridy;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.gridx = 0;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weightx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.weightx = 0.0;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.fill' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.NONE' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.fill = GridBagConstraints.NONE;
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = name;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				Controls.Add(temp_Control);
				temp_Control.Dock = new System.Windows.Forms.DockStyle();
				temp_Control.BringToFront();
				
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weightx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.weightx = 0.0;
				System.Windows.Forms.Button button = SupportClass.ButtonSupport.CreateStandardButton("Default");
				
				if (!value_Renamed.IsEmpty)
				{
					button.BackColor = value_Renamed;
					button.Text = "sample";
				}
				button.Click += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(button);
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				++c.gridx;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				Controls.Add(button);
				button.Dock = new System.Windows.Forms.DockStyle();
				button.BringToFront();
				return button;
			}
			
			public virtual System.Windows.Forms.ComboBox addComboBox(System.String name, System.String[] values, int initialRow)
			{
				System.Windows.Forms.ComboBox comboBox = SupportClass.ComboBoxSupport.CreateComboBox(values);
				comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
				comboBox.SelectedIndex = initialRow;
				
				addCtrl(name, comboBox);
				
				return comboBox;
			}
			
			
			public virtual void  addCtrl(System.String name, System.Windows.Forms.Control ctrl)
			{
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridy' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				++c.gridy;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.gridx = 0;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weightx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.weightx = 0.0;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.fill' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.NONE' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.fill = GridBagConstraints.NONE;
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = name;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				Controls.Add(temp_Control);
				temp_Control.Dock = new System.Windows.Forms.DockStyle();
				temp_Control.BringToFront();
				
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weightx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.weightx = 0.0;
				
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				++c.gridx;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				Controls.Add(ctrl);
				ctrl.Dock = new System.Windows.Forms.DockStyle();
				ctrl.BringToFront();
			}
			
			
			//UPGRADE_TODO: Class 'javax.swing.table.DefaultTableModel' was converted to 'System.Data.DataTable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			internal System.Data.DataTable tableModel;
			internal System.String[] defaultValues;
			
			
			//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			[Serializable]
			public class SmartTable:System.Windows.Forms.DataGrid
			{
				private const long serialVersionUID = 1L;
				
				//UPGRADE_ISSUE: Constructor 'javax.swing.JTable.JTable' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000'"
				//UPGRADE_TODO: Interface 'javax.swing.table.TableModel' was converted to 'System.Data.DataTable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				internal SmartTable(System.Data.DataTable m):base(m)
				{
					setSurrendersFocusOnKeystroke(true);
				}
				
				//Prepares the editor by querying the data model for the value and selection state of the cell at row, column.    }
				//UPGRADE_NOTE: The equivalent of method 'javax.swing.JTable.prepareEditor' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				//UPGRADE_ISSUE: Interface 'javax.swing.table.TableCellEditor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableTableCellEditor'"
				public System.Windows.Forms.Control prepareEditor(TableCellEditor editor, int row, int column)
				{
					if (row == ((System.Data.DataTable) DataSource).Rows.Count - 1)
					{
						//UPGRADE_TODO: Class 'javax.swing.table.DefaultTableModel' was converted to 'System.Data.DataTable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
						((System.Data.DataTable) DataSource).Rows.Add(VassalSharp.counters.PropertySheet.Ed.DEFAULT_ROW);
					}
					//UPGRADE_ISSUE: Method 'javax.swing.JTable.prepareEditor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTableprepareEditor_javaxswingtableTableCellEditor_int_int'"
					System.Windows.Forms.Control component = base.prepareEditor(editor, row, column);
					
					if (component is System.Windows.Forms.TextBoxBase)
					{
						((System.Windows.Forms.TextBoxBase) component).Focus();
						((System.Windows.Forms.TextBoxBase) component).SelectAll();
					}
					return component;
				}
			}
			
			//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_TODO: Class 'javax.swing.table.DefaultTableModel' was converted to 'System.Data.DataTable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			public virtual System.Windows.Forms.DataGrid addTableCtrl(System.String name, System.Data.DataTable theTableModel, System.String[] theDefaultValues)
			{
				tableModel = theTableModel;
				this.defaultValues = theDefaultValues;
				
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridy' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				++c.gridy;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.gridx = 0;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weighty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.weighty = 0.0;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weightx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.weightx = 1.0;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridwidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.gridwidth = 2;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.fill' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.HORIZONTAL' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.fill = GridBagConstraints.HORIZONTAL;
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = name;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				Controls.Add(temp_Control);
				temp_Control.Dock = new System.Windows.Forms.DockStyle();
				temp_Control.BringToFront();
				
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridy' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				++c.gridy;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weighty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.weighty = 1.0;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.fill' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.BOTH' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.fill = GridBagConstraints.BOTH;
				//UPGRADE_NOTE: Final was removed from the declaration of 'table '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SmartTable table = new SmartTable(tableModel);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = new ScrollPane(table);
				Controls.Add(temp_Control2);
				temp_Control2.Dock = new System.Windows.Forms.DockStyle();
				temp_Control2.BringToFront();
				
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridwidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.gridwidth = 1;
				
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridy' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				++c.gridy;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.fill' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.NONE' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.fill = GridBagConstraints.NONE;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.anchor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.EAST' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.anchor = GridBagConstraints.EAST;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridwidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.gridwidth = 2;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weighty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.weighty = 0.0;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weightx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.weightx = 1.0;
				
				System.Windows.Forms.Panel buttonPanel = new System.Windows.Forms.Panel();
				
				// add button
				System.Windows.Forms.Button addButton = SupportClass.ButtonSupport.CreateStandardButton("Insert Row");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				buttonPanel.Controls.Add(addButton);
				addButton.Click += new System.EventHandler(new AnonymousClassActionListener3(table, this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(addButton);
				
				// delete button
				//UPGRADE_NOTE: Final was removed from the declaration of 'deleteButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Button deleteButton = SupportClass.ButtonSupport.CreateStandardButton("Delete Row");
				deleteButton.Enabled = false;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				buttonPanel.Controls.Add(deleteButton);
				deleteButton.Click += new System.EventHandler(new AnonymousClassActionListener4(table, this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(deleteButton);
				
				// Ask to be notified of selection changes.
				//UPGRADE_ISSUE: Method 'javax.swing.ListSelectionModel.addListSelectionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingListSelectionModeladdListSelectionListener_javaxswingeventListSelectionListener'"
				//UPGRADE_ISSUE: Method 'javax.swing.JTable.getSelectionModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetSelectionModel'"
				table.getSelectionModel().addListSelectionListener(new AnonymousClassListSelectionListener(deleteButton, this));
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				Controls.Add(buttonPanel);
				buttonPanel.Dock = new System.Windows.Forms.DockStyle();
				buttonPanel.BringToFront();
				
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.anchor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.WEST' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.anchor = GridBagConstraints.WEST;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weightx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.weightx = 0.0;
				return table;
			}
			
			public virtual void  focusGained(System.Object event_sender, System.EventArgs event_Renamed)
			{
			}
			
			// make sure we save user's changes
			public virtual void  focusLost(System.Object event_sender, System.EventArgs event_Renamed)
			{
				//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				if (((System.Windows.Forms.Control) event_sender) is System.Windows.Forms.DataGrid)
				{
					//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					System.Windows.Forms.DataGrid table = (System.Windows.Forms.DataGrid) ((System.Windows.Forms.Control) event_sender);
					if (((System.Data.DataTable) table.DataSource).DefaultView[table.CurrentCell.RowNumber].IsEdit)
					{
						//UPGRADE_ISSUE: Method 'javax.swing.CellEditor.stopCellEditing' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingCellEditor'"
						//UPGRADE_ISSUE: Method 'javax.swing.JTable.getCellEditor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetCellEditor'"
						table.getCellEditor().stopCellEditing();
					}
				}
			}
		}
		
		public override PieceI18nData getI18nData()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final ArrayList < String > items = new ArrayList < String >();
			//UPGRADE_NOTE: Final was removed from the declaration of 'defDecoder '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder.Decoder defDecoder = new SequenceEncoder.Decoder(m_definition, DEF_DELIMITOR);
			while (defDecoder.hasMoreTokens())
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'item '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String item = defDecoder.nextToken();
				items.add(item.Length == 0?"":item.Substring(1));
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'menuNames '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String[] menuNames = new System.String[items.size() + 1];
			//UPGRADE_NOTE: Final was removed from the declaration of 'descriptions '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String[] descriptions = new System.String[items.size() + 1];
			menuNames[0] = menuName;
			descriptions[0] = "Property Sheet command";
			int j = 1;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(String s: items)
			{
				menuNames[j] = s;
				descriptions[j] = "Property Sheet item " + j;
				j++;
			}
			return getI18nData(menuNames, descriptions);
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
					return m_panel;
				}
				
			}
			/// <summary>returns the type-definition in the format:
			/// definition, name, keystroke, commit, red, green, blue
			/// </summary>
			virtual public System.String Type
			{
				get
				{
					
					if (((System.Data.DataTable) propertyTable.DataSource).DefaultView[propertyTable.CurrentCell.RowNumber].IsEdit)
					{
						//UPGRADE_ISSUE: Method 'javax.swing.CellEditor.stopCellEditing' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingCellEditor'"
						//UPGRADE_ISSUE: Method 'javax.swing.JTable.getCellEditor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetCellEditor'"
						propertyTable.getCellEditor().stopCellEditing();
					}
					
					SequenceEncoder defEncoder = new SequenceEncoder(VassalSharp.counters.PropertySheet.DEF_DELIMITOR);
					//      StringBuilder definition = new StringBuilder();
					//      int numRows = propertyTable.getRowCount();
					for (int iRow = 0; iRow < ((System.Data.DataTable) propertyTable.DataSource).Rows.Count; ++iRow)
					{
						System.String typeString = (System.String) ((System.Data.DataTable) propertyTable.DataSource).Rows[iRow][1];
						for (int iType = 0; iType < VassalSharp.counters.PropertySheet.TYPE_VALUES.Length; ++iType)
						{
							if (typeString.matches(VassalSharp.counters.PropertySheet.TYPE_VALUES[iType]) && !DEFAULT_ROW[0].Equals(((System.Data.DataTable) propertyTable.DataSource).Rows[iRow][0]))
							{
								defEncoder.append(iType + (System.String) ((System.Data.DataTable) propertyTable.DataSource).Rows[iRow][0]);
								break;
							}
						}
					}
					
					SequenceEncoder typeEncoder = new SequenceEncoder(VassalSharp.counters.PropertySheet.TYPE_DELIMITOR);
					
					// calc color strings
					System.String red, green, blue;
					if (colorCtrl.Text.Equals("Default"))
					{
						red = "";
						green = "";
						blue = "";
					}
					else
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Color.getRed' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						red = System.Convert.ToString((int) colorCtrl.BackColor.R);
						//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Color.getGreen' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						green = System.Convert.ToString((int) colorCtrl.BackColor.G);
						//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Color.getBlue' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						blue = System.Convert.ToString((int) colorCtrl.BackColor.B);
					}
					
					System.String definitionString = defEncoder.Value;
					typeEncoder.append(definitionString == null?"":definitionString).append(menuNameCtrl.Text).append("").append(System.Convert.ToString(commitCtrl.SelectedIndex)).append(red).append(green).append(blue).append(keyStrokeConfig.ValueString);
					
					return VassalSharp.counters.PropertySheet.ID + typeEncoder.Value;
				}
				
			}
			/// <summary>returns a default value-string for the given definition </summary>
			virtual public System.String State
			{
				get
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'buf '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					StringBuilder buf = new StringBuilder();
					for (int i = 0; i < ((System.Data.DataTable) propertyTable.DataSource).Rows.Count; ++i)
					{
						buf.append(VassalSharp.counters.PropertySheet.STATE_DELIMITOR);
					}
					
					return buf.toString();
				}
				
			}
			
			private PropertyPanel m_panel;
			private System.Windows.Forms.TextBox menuNameCtrl;
			private NamedHotKeyConfigurer keyStrokeConfig;
			private System.Windows.Forms.Button colorCtrl;
			//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			private System.Windows.Forms.DataGrid propertyTable;
			private System.Windows.Forms.ComboBox commitCtrl;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'COLUMN_NAMES'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			internal static readonly System.String[] COLUMN_NAMES = new System.String[]{"Name", "Type"};
			//UPGRADE_NOTE: Final was removed from the declaration of 'DEFAULT_ROW'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			internal static readonly System.String[] DEFAULT_ROW = new System.String[]{"*new property*", "Text"};
			
			public Ed(PropertySheet propertySheet)
			{
				m_panel = new PropertyPanel();
				menuNameCtrl = m_panel.addStringCtrl("Menu Text:", propertySheet.menuName);
				keyStrokeConfig = m_panel.addKeyStrokeConfig(propertySheet.launchKeyStroke);
				commitCtrl = m_panel.addComboBox("Commit changes on:", VassalSharp.counters.PropertySheet.COMMIT_VALUES, propertySheet.commitStyle);
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				colorCtrl = m_panel.addColorCtrl("Background Color:", ref propertySheet.backgroundColor);
				//UPGRADE_TODO: Class 'javax.swing.table.DefaultTableModel' was converted to 'System.Data.DataTable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				//UPGRADE_TODO: Constructor 'javax.swing.table.DefaultTableModel.DefaultTableModel' was converted to 'SupportClass.DataTableSupport.CreateDataTable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtableDefaultTableModelDefaultTableModel_javalangObject[][]_javalangObject[]'"
				System.Data.DataTable dataModel = SupportClass.DataTableSupport.CreateDataTable(getTableData(propertySheet.m_definition), COLUMN_NAMES);
				AddCreateRow(dataModel);
				propertyTable = m_panel.addTableCtrl("Properties:", dataModel, DEFAULT_ROW);
				
				//UPGRADE_ISSUE: Class 'javax.swing.DefaultCellEditor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultCellEditor'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.DefaultCellEditor.DefaultCellEditor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultCellEditor'"
				DefaultCellEditor typePicklist = new DefaultCellEditor(SupportClass.ComboBoxSupport.CreateComboBox(VassalSharp.counters.PropertySheet.TYPE_VALUES));
				//UPGRADE_TODO: Method 'javax.swing.table.TableColumn.setCellEditor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.table.TableColumnModel.getColumn' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				((System.Data.DataTable) propertyTable.DataSource).Columns[1].setCellEditor(typePicklist);
			}
			
			//UPGRADE_TODO: Class 'javax.swing.table.DefaultTableModel' was converted to 'System.Data.DataTable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			protected internal virtual void  AddCreateRow(System.Data.DataTable data)
			{
				data.Rows.Add(DEFAULT_ROW);
			}
			
			protected internal virtual System.String[][] getTableData(System.String definition)
			{
				SequenceEncoder.Decoder decoder = new SequenceEncoder.Decoder(definition, VassalSharp.counters.PropertySheet.DEF_DELIMITOR);
				
				int numRows = !definition.Equals("") && decoder.hasMoreTokens()?1:0;
				//UPGRADE_WARNING: Method 'java.lang.String.indexOf' was converted to 'System.String.IndexOf' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
				for (int iDef = - 1; (iDef = definition.IndexOf((System.Char) VassalSharp.counters.PropertySheet.DEF_DELIMITOR, iDef + 1)) >= 0; )
				{
					++numRows;
				}
				
				System.String[][] tmpArray = new System.String[numRows][];
				for (int i = 0; i < numRows; i++)
				{
					tmpArray[i] = new System.String[2];
				}
				System.String[][] rows = tmpArray;
				
				for (int iRow = 0; decoder.hasMoreTokens() && iRow < numRows; ++iRow)
				{
					System.String token = decoder.nextToken();
					rows[iRow][0] = token.Substring(1);
					rows[iRow][1] = VassalSharp.counters.PropertySheet.TYPE_VALUES[token[0] - '0'];
				}
				
				return rows;
			}
		}
		
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'TickPanel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_TODO: Interface 'javax.swing.event.DocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		[Serializable]
		internal class TickPanel:System.Windows.Forms.Panel, DocumentListener
		{
			private void  InitBlock(PropertySheet enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PropertySheet enclosingInstance;
			virtual public System.String Value
			{
				get
				{
					commitTextFields();
					return System.Convert.ToString(numTicks) + VassalSharp.counters.PropertySheet.VALUE_DELIMINATOR + maxTicks;
				}
				
			}
			public PropertySheet Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			private int numTicks;
			private int maxTicks;
			private int panelType;
			private System.Windows.Forms.TextBox valField;
			private System.Windows.Forms.TextBox maxField;
			private TickLabel ticks;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private List < ActionListener > actionListeners = 
			new ArrayList < ActionListener >();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private List < DocumentListener > documentListeners = 
			new ArrayList < DocumentListener >();
			
			public TickPanel(PropertySheet enclosingInstance, System.String value_Renamed, int type):base()
			{
				InitBlock(enclosingInstance);
				//UPGRADE_ISSUE: Constructor 'java.awt.GridBagLayout.GridBagLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagLayout'"
				new GridBagLayout();
				//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
				set_Renamed(value_Renamed);
				
				panelType = type;
				
				//UPGRADE_ISSUE: Class 'java.awt.GridBagConstraints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				//UPGRADE_ISSUE: Constructor 'java.awt.GridBagConstraints.GridBagConstraints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				GridBagConstraints c = new GridBagConstraints();
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.fill' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.BOTH' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.fill = GridBagConstraints.BOTH;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.ipadx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.ipadx = 1;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weightx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.weightx = 0.0;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.gridx = 0;
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridy' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.gridy = 0;
				
				System.Drawing.Size minSize;
				
				if (panelType == VassalSharp.counters.PropertySheet.TICKS_VAL || panelType == VassalSharp.counters.PropertySheet.TICKS_VALMAX)
				{
					System.Windows.Forms.TextBox temp_text_box;
					temp_text_box = new System.Windows.Forms.TextBox();
					temp_text_box.Text = System.Convert.ToString(numTicks);
					valField = temp_text_box;
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getMinimumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetMinimumSize'"
					minSize = valField.getMinimumSize();
					minSize.Width = 24;
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMinimumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMinimumSize_javaawtDimension'"
					valField.setMinimumSize(minSize);
					//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					valField.Size = minSize;
					//UPGRADE_TODO: Method 'javax.swing.JTextField.addActionListener' was converted to 'System.Windows.Forms.TextBox.KeyPress' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldaddActionListener_javaawteventActionListener'"
					valField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.actionPerformed);
					valField.Enter += new System.EventHandler(this.focusGained);
					valField.Leave += new System.EventHandler(this.focusLost);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
					Controls.Add(valField);
					valField.Dock = new System.Windows.Forms.DockStyle();
					valField.BringToFront();
					//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
					++c.gridx;
				}
				if (panelType == VassalSharp.counters.PropertySheet.TICKS_MAX || panelType == VassalSharp.counters.PropertySheet.TICKS_VALMAX)
				{
					System.Windows.Forms.TextBox temp_text_box2;
					temp_text_box2 = new System.Windows.Forms.TextBox();
					temp_text_box2.Text = System.Convert.ToString(maxTicks);
					maxField = temp_text_box2;
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getMinimumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetMinimumSize'"
					minSize = maxField.getMinimumSize();
					minSize.Width = 24;
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMinimumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMinimumSize_javaawtDimension'"
					maxField.setMinimumSize(minSize);
					//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					maxField.Size = minSize;
					//UPGRADE_TODO: Method 'javax.swing.JTextField.addActionListener' was converted to 'System.Windows.Forms.TextBox.KeyPress' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldaddActionListener_javaawteventActionListener'"
					maxField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.actionPerformed);
					maxField.Enter += new System.EventHandler(this.focusGained);
					maxField.Leave += new System.EventHandler(this.focusLost);
					if (panelType == VassalSharp.counters.PropertySheet.TICKS_VALMAX)
					{
						System.Windows.Forms.Label temp_label2;
						temp_label2 = new System.Windows.Forms.Label();
						temp_label2.Text = "/";
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
						System.Windows.Forms.Control temp_Control;
						temp_Control = temp_label2;
						Controls.Add(temp_Control);
						temp_Control.Dock = new System.Windows.Forms.DockStyle();
						temp_Control.BringToFront();
						//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
						++c.gridx;
					}
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
					Controls.Add(maxField);
					maxField.Dock = new System.Windows.Forms.DockStyle();
					maxField.BringToFront();
					//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
					++c.gridx;
				}
				
				ticks = new TickLabel(enclosingInstance, numTicks, maxTicks, panelType);
				ticks.addActionListener(this);
				//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weightx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
				c.weightx = 1.0;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				Controls.Add(ticks);
				ticks.Dock = new System.Windows.Forms.DockStyle();
				ticks.BringToFront();
				PerformLayout();
			}
			
			public virtual void  updateValue(System.String value_Renamed)
			{
				set_Renamed(value_Renamed);
				updateFields();
			}
			
			private void  set_Renamed(System.String value_Renamed)
			{
				set_Renamed(Enclosing_Instance.atoi(value_Renamed), Enclosing_Instance.atoiRight(value_Renamed));
			}
			
			private bool set_Renamed(int num, int max)
			{
				
				bool changed = false;
				
				if (numTicks == 0 && maxTicks == 0 && num == 0 && max > 0)
				{
					//        num = max;  // This causes a bug in which ticks set to zero go to max after save/reload of a game
					changed = true;
				}
				if (numTicks == 0 && maxTicks == 0 && max == 0 && num > 0)
				{
					max = num;
					changed = true;
				}
				numTicks = System.Math.Min(max, num);
				maxTicks = max;
				
				return changed;
			}
			
			
			private void  commitTextFields()
			{
				if (valField != null || maxField != null)
				{
					if (set_Renamed(valField != null?Enclosing_Instance.atoi(valField.Text):numTicks, maxField != null?Enclosing_Instance.atoi(maxField.Text):maxTicks))
					{
						updateFields();
					}
				}
			}
			
			private void  updateFields()
			{
				if (valField != null)
				{
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
					valField.Text = System.Convert.ToString(numTicks);
				}
				if (maxField != null)
				{
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
					maxField.Text = System.Convert.ToString(maxTicks);
				}
				ticks.set_Renamed(numTicks, maxTicks);
			}
			
			private bool areFieldValuesValid()
			{
				int max = maxField == null?maxTicks:Enclosing_Instance.atoi(maxField.Text);
				int val = valField == null?numTicks:Enclosing_Instance.atoi(valField.Text);
				return val < max && val >= 0;
			}
			
			// field changed
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs event_Renamed)
			{
				
				if (event_sender == maxField || event_sender == valField)
				{
					commitTextFields();
					ticks.set_Renamed(numTicks, maxTicks);
					fireActionEvent();
				}
				else if (event_sender == ticks)
				{
					commitTextFields();
					numTicks = ticks.NumTicks;
					if (maxField == null)
					{
						maxTicks = ticks.MaxTicks;
					}
					updateFields();
					fireDocumentEvent();
				}
			}
			
			//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			public virtual void  addActionListener(ActionListener listener)
			{
				actionListeners.add(listener);
			}
			
			public virtual void  fireActionEvent()
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(ActionListener l: actionListeners)
				{
					l.actionPerformed(new System.EventArgs());
				}
			}
			
			
			//UPGRADE_TODO: Interface 'javax.swing.event.DocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			internal virtual void  addDocumentListener(DocumentListener listener)
			{
				if (valField != null)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.text.Document.addDocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDocument'"
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.getDocument' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentgetDocument'"
					((System.String) valField.Text).addDocumentListener(this);
				}
				if (maxField != null)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.text.Document.addDocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDocument'"
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.getDocument' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentgetDocument'"
					((System.String) maxField.Text).addDocumentListener(this);
				}
				documentListeners.add(listener);
			}
			
			public virtual void  fireDocumentEvent()
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(DocumentListener l: documentListeners)
				{
					l.changedUpdate(null);
				}
			}
			
			// FocusListener Interface
			public virtual void  focusLost(System.Object event_sender, System.EventArgs event_Renamed)
			{
				commitTextFields();
				ticks.set_Renamed(numTicks, maxTicks);
			}
			
			public virtual void  focusGained(System.Object event_sender, System.EventArgs event_Renamed)
			{
			}
			
			//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
			public virtual void  changedUpdate(DocumentEvent event_Renamed)
			{
				// do not propagate events unless min/max is valid.  We don't want to trigger both
				// a state update. A state update might trigger a fix-min/max operation, which would
				// cause the text fields to update which will throw an IllegalStateException
				if (areFieldValuesValid())
				{
					fireDocumentEvent();
				}
			}
			
			//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
			public virtual void  insertUpdate(DocumentEvent event_Renamed)
			{
				changedUpdate(event_Renamed);
			}
			
			//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
			public virtual void  removeUpdate(DocumentEvent event_Renamed)
			{
				changedUpdate(event_Renamed);
			}
		}
		
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'TickLabel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
		[Serializable]
		public class TickLabel:System.Windows.Forms.Label
		{
			static private System.Int32 state561;
			private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
			{
				state561 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
			}
			private void  InitBlock(PropertySheet enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PropertySheet enclosingInstance;
			virtual public int NumTicks
			{
				get
				{
					return numTicks;
				}
				
			}
			virtual public int MaxTicks
			{
				get
				{
					return maxTicks;
				}
				
			}
			public PropertySheet Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			private int numTicks = 0;
			private int maxTicks = 0;
			protected internal int panelType;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private List < ActionListener > actionListeners = 
			new ArrayList < ActionListener >();
			
			//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			public virtual void  addActionListener(ActionListener listener)
			{
				actionListeners.add(listener);
			}
			
			public TickLabel(PropertySheet enclosingInstance, int numTicks, int maxTicks, int panelType):base()
			{
				InitBlock(enclosingInstance);
				this.Text = " ";
				//Debug.trace("TickLabel( " + maxTicks + ", " + numTicks + " )");
				set_Renamed(numTicks, maxTicks);
				this.panelType = panelType;
				MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.counters.PropertySheet.TickLabel.mouseDown);
				Click += new System.EventHandler(this.mouseClicked);
				MouseEnter += new System.EventHandler(this.mouseEntered);
				MouseLeave += new System.EventHandler(this.mouseExited);
				MouseDown += new System.Windows.Forms.MouseEventHandler(this.mousePressed);
				MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseReleased);
			}
			
			protected internal int topMargin = 2;
			protected internal int leftMargin = 2;
			protected internal int numRows;
			protected internal int numCols;
			protected internal int dx = 1;
			
			protected override void  OnPaint(System.Windows.Forms.PaintEventArgs g_EventArg)
			{
				System.Drawing.Graphics g = null;
				if (g_EventArg != null)
					g = g_EventArg.Graphics;
				
				//Debug.trace("TickLabcmdel.paint(" + numTicks + "/" + maxTicks + ")");
				//Debug.trace("  width=" + getWidth() + " height=" + getHeight());
				if (maxTicks > 0)
				{
					// prefered width is 10
					// min width before resize is 6
					int displayWidth = Width - 2;
					
					dx = System.Math.Min(displayWidth / maxTicks, 10);
					// if dx < 2, we have a problem
					
					numRows = dx < 3?3:(dx < 6?2:1);
					
					dx = System.Math.Min(displayWidth * numRows / maxTicks, System.Math.Min(Height / numRows, 10));
					if (dx < 1)
						dx = 1;
					
					numCols = (maxTicks + numRows - 1) / numRows;
					
					int dy = dx;
					
					topMargin = (Height - dy * numRows + 2) / 2;
					
					int tick = 0;
					int row, col;
					if (dx > 4)
					{
						for (; tick < maxTicks; ++tick)
						{
							row = tick / numCols;
							col = tick % numCols;
							g.setColor(Color.BLACK);
							g.DrawRectangle(SupportClass.GraphicsManager.manager.GetPen(g), leftMargin + col * dx, topMargin + row * dy, dx - 3, dy - 3);
							g.setColor(tick < numTicks?Color.BLACK:Color.WHITE);
							g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), leftMargin + 1 + col * dx, topMargin + 1 + row * dy, dx - 4, dy - 4);
						}
					}
					else
					{
						g.setColor(Color.GRAY);
						g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), 0, topMargin - 2, numCols * dx + leftMargin * 2, numRows * dy + 4);
						for (; tick < maxTicks; ++tick)
						{
							row = tick / numCols;
							col = tick % numCols;
							g.setColor(tick < numTicks?Color.BLACK:Color.WHITE);
							g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), leftMargin + col * dx, topMargin + row * dy, dx - 1, dy - 1);
						}
					}
				}
			}
			
			public virtual void  mouseClicked(System.Object event_sender, System.EventArgs event_Renamed)
			{
				
				//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.isMetaDown' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
				if ((event_Renamed.isMetaDown() || (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift)) && panelType != VassalSharp.counters.PropertySheet.TICKS_VALMAX)
				{
					new EditTickLabelValueDialog(this, this);
					return ;
				}
				//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.getX' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventgetX'"
				int col = System.Math.Min((event_Renamed.getX() - leftMargin + 1) / dx, numCols - 1);
				//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.getY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventgetY'"
				int row = System.Math.Min((event_Renamed.getY() - topMargin + 1) / dx, numRows - 1);
				int num = row * numCols + col + 1;
				
				// Checkbox behavior; toggle the box clicked on
				// numTicks = num > numTicks ? num : num - 1;
				
				// Slider behavior; set the box clicked on and all to left and clear all boxes to right,
				// UNLESS user clicked on last set box (which would do nothing), in this case, toggle the
				// box clicked on.  This is the only way the user can clear the first box.
				
				numTicks = (num == numTicks)?num - 1:num;
				fireActionEvent();
				
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				Refresh();
			}
			
			public virtual void  fireActionEvent()
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(ActionListener l: actionListeners)
				{
					l.actionPerformed(new System.EventArgs());
				}
			}
			
			
			public virtual void  set_Renamed(int newNumTicks, int newMaxTicks)
			{
				//Debug.trace("TickLabel.set( " + newNumTicks + "," + newMaxTicks + " ) was " + numTicks + "/" + maxTicks);
				numTicks = newNumTicks;
				maxTicks = newMaxTicks;
				
				System.String tip = numTicks + "/" + maxTicks;
				
				if (panelType != VassalSharp.counters.PropertySheet.TICKS_VALMAX)
				{
					tip += " (right-click to edit)";
				}
				
				SupportClass.ToolTipSupport.setToolTipText(this, tip);
				
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				Refresh();
			}
			
			public virtual void  mouseEntered(System.Object event_sender, System.EventArgs event_Renamed)
			{
			}
			
			public virtual void  mouseExited(System.Object event_sender, System.EventArgs event_Renamed)
			{
			}
			
			public virtual void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs event_Renamed)
			{
			}
			
			public virtual void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs event_Renamed)
			{
			}
			
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'EditTickLabelValueDialog' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			//UPGRADE_TODO: Interface 'javax.swing.event.DocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			[Serializable]
			public class EditTickLabelValueDialog:System.Windows.Forms.Panel, DocumentListener
			{
				private void  InitBlock(TickLabel enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private TickLabel enclosingInstance;
				public TickLabel Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				private const long serialVersionUID = 1L;
				
				internal TickLabel theTickLabel;
				//UPGRADE_TODO: Class 'javax.swing.JLayeredPane' was converted to 'System.Windows.Forms.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				internal System.Windows.Forms.Panel editorParent;
				internal System.Windows.Forms.TextBox valueField;
				
				public EditTickLabelValueDialog(TickLabel enclosingInstance, TickLabel owner):base()
				{
					InitBlock(enclosingInstance);
					//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
					new BorderLayout();
					//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
					
					theTickLabel = owner;
					
					// Find containing dialog
					System.Windows.Forms.Control theDialog = theTickLabel.Parent;
					while (!(theDialog is System.Windows.Forms.Form) && theDialog != null)
					{
						theDialog = theDialog.Parent;
					}
					
					if (theDialog != null)
					{
						//UPGRADE_ISSUE: Method 'javax.swing.JDialog.getLayeredPane' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJDialoggetLayeredPane'"
						editorParent = ((System.Windows.Forms.Form) theDialog).getLayeredPane();
						//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.convertRectangle' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
						System.Drawing.Rectangle newBounds = SwingUtilities.convertRectangle(theTickLabel.Parent, theTickLabel.Bounds, editorParent);
						//UPGRADE_TODO: Method 'java.awt.Component.setBounds' was converted to 'System.Windows.Forms.Control.Bounds' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetBounds_javaawtRectangle'"
						Bounds = newBounds;
						
						System.Windows.Forms.Button okButton = SupportClass.ButtonSupport.CreateStandardButton("Ok");
						
						switch (Enclosing_Instance.panelType)
						{
							
							case VassalSharp.counters.PropertySheet.TICKS_VAL: 
								System.Windows.Forms.TextBox temp_text_box;
								temp_text_box = new System.Windows.Forms.TextBox();
								temp_text_box.Text = System.Convert.ToString(owner.MaxTicks);
								valueField = temp_text_box;
								SupportClass.ToolTipSupport.setToolTipText(valueField, "max value");
								break;
							
							case VassalSharp.counters.PropertySheet.TICKS_MAX: 
								System.Windows.Forms.TextBox temp_text_box2;
								temp_text_box2 = new System.Windows.Forms.TextBox();
								temp_text_box2.Text = System.Convert.ToString(owner.NumTicks);
								valueField = temp_text_box2;
								SupportClass.ToolTipSupport.setToolTipText(valueField, "current value");
								break;
							
							case VassalSharp.counters.PropertySheet.TICKS: 
							default: 
								System.Windows.Forms.TextBox temp_text_box3;
								temp_text_box3 = new System.Windows.Forms.TextBox();
								temp_text_box3.Text = owner.numTicks + "/" + owner.maxTicks;
								valueField = temp_text_box3;
								SupportClass.ToolTipSupport.setToolTipText(valueField, "current value / max value");
								break;
							}
						
						//UPGRADE_TODO: Method 'javax.swing.JTextField.addActionListener' was converted to 'System.Windows.Forms.TextBox.KeyPress' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldaddActionListener_javaawteventActionListener'"
						valueField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.actionPerformed);
						valueField.Enter += new System.EventHandler(this.focusGained);
						valueField.Leave += new System.EventHandler(this.focusLost);
						//UPGRADE_ISSUE: Method 'javax.swing.text.Document.addDocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDocument'"
						//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.getDocument' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentgetDocument'"
						((System.String) valueField.Text).addDocumentListener(this);
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
						Controls.Add(valueField);
						valueField.Dock = System.Windows.Forms.DockStyle.Fill;
						valueField.BringToFront();
						
						okButton.Click += new System.EventHandler(this.actionPerformed);
						SupportClass.CommandManager.CheckCommand(okButton);
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
						Controls.Add(okButton);
						okButton.Dock = System.Windows.Forms.DockStyle.Right;
						okButton.BringToFront();
						
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
						editorParent.Controls.Add(this);
						this.Dock = new System.Windows.Forms.DockStyle();
						this.BringToFront();
						
						Visible = true;
						valueField.Focus();
					}
				}
				
				public virtual void  storeValues()
				{
					System.String value_Renamed = valueField.Text;
					switch (Enclosing_Instance.panelType)
					{
						
						case VassalSharp.counters.PropertySheet.TICKS_VAL: 
							theTickLabel.set_Renamed(theTickLabel.NumTicks, Enclosing_Instance.Enclosing_Instance.atoi(value_Renamed));
							break;
						
						case VassalSharp.counters.PropertySheet.TICKS_MAX: 
							theTickLabel.set_Renamed(Enclosing_Instance.Enclosing_Instance.atoi(value_Renamed), theTickLabel.MaxTicks);
							break;
						
						case VassalSharp.counters.PropertySheet.TICKS: 
						default: 
							theTickLabel.set_Renamed(Enclosing_Instance.Enclosing_Instance.atoi(value_Renamed), Enclosing_Instance.Enclosing_Instance.atoiRight(value_Renamed));
							break;
						}
					theTickLabel.fireActionEvent();
				}
				
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs event_Renamed)
				{
					storeValues();
					editorParent.Controls.Remove(this);
				}
				
				//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
				public virtual void  changedUpdate(DocumentEvent event_Renamed)
				{
					theTickLabel.fireActionEvent();
				}
				
				//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
				public virtual void  insertUpdate(DocumentEvent event_Renamed)
				{
					theTickLabel.fireActionEvent();
				}
				
				//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
				public virtual void  removeUpdate(DocumentEvent event_Renamed)
				{
					theTickLabel.fireActionEvent();
				}
				
				public virtual void  focusGained(System.Object event_sender, System.EventArgs event_Renamed)
				{
				}
				
				public virtual void  focusLost(System.Object event_sender, System.EventArgs event_Renamed)
				{
					storeValues();
				}
			}
		}
	}
}