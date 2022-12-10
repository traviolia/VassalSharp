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
using Map = VassalSharp.build.module.Map;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using MassKeyCommand = VassalSharp.build.module.map.MassKeyCommand;
using Command = VassalSharp.command.Command;
using NullCommand = VassalSharp.command.NullCommand;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using PropertyExpression = VassalSharp.configure.PropertyExpression;
using PropertyExpressionConfigurer = VassalSharp.configure.PropertyExpressionConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using Resources = VassalSharp.i18n.Resources;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using RecursionLimiter = VassalSharp.tools.RecursionLimiter;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> Adds a menu item that applies a {@link GlobalCommand} to other pieces</summary>
	public class CounterGlobalKeyCommand:Decorator, TranslatablePiece, RecursionLimiter.Loopable
	{
		private void  InitBlock()
		{
			globalCommand = new GlobalCommand(this);
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
				System.String d = "Global Key Command";
				if (description.Length > 0)
				{
					d += (" - " + description);
				}
				return d;
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("GlobalKeyCommand.htm");
			}
			
		}
		virtual public System.String ComponentName
		{
			// Implement Loopable
			
			get
			{
				// Use inner name to prevent recursive looping when reporting errors.
				return piece.getName();
			}
			
		}
		virtual public System.String ComponentTypeName
		{
			get
			{
				return Description;
			}
			
		}
		public const System.String ID = "globalkey;";
		protected internal KeyCommand[] command;
		protected internal System.String commandName;
		protected internal NamedKeyStroke key;
		protected internal NamedKeyStroke globalKey;
		//UPGRADE_NOTE: The initialization of  'globalCommand' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal GlobalCommand globalCommand;
		protected internal PropertyExpression propertiesFilter = new PropertyExpression();
		protected internal bool restrictRange;
		protected internal bool fixedRange = true;
		protected internal int range;
		protected internal System.String rangeProperty = "";
		private KeyCommand myCommand;
		protected internal System.String description;
		
		public CounterGlobalKeyCommand():this(ID, null)
		{
		}
		
		public CounterGlobalKeyCommand(System.String type, GamePiece inner)
		{
			InitBlock();
			mySetType(type);
			setInner(inner);
		}
		
		public virtual void  mySetType(System.String type)
		{
			type = type.Substring(ID.Length);
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			commandName = st.nextToken("Global Command");
			key = st.nextNamedKeyStroke('G');
			globalKey = st.nextNamedKeyStroke('K');
			propertiesFilter.Expression = st.nextToken("");
			restrictRange = st.nextBoolean(false);
			range = st.nextInt(1);
			globalCommand.ReportSingle = st.nextBoolean(true);
			globalCommand.setKeyStroke(globalKey);
			fixedRange = st.nextBoolean(true);
			rangeProperty = st.nextToken("");
			description = st.nextToken("");
			globalCommand.SelectFromDeck = st.nextInt(- 1);
			command = null;
		}
		
		public override System.String myGetType()
		{
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(commandName).append(key).append(globalKey).append(propertiesFilter.Expression).append(restrictRange).append(range).append(globalCommand.ReportSingle).append(fixedRange).append(rangeProperty).append(description).append(globalCommand.SelectFromDeck);
			return ID + se.Value;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			if (command == null)
			{
				myCommand = new KeyCommand(commandName, key, Decorator.getOutermost(this), this);
				if (commandName.Length > 0 && key != null && !key.Null)
				{
					command = new KeyCommand[]{myCommand};
				}
				else
				{
					command = new KeyCommand[0];
				}
			}
			if (command.Length > 0)
			{
				command[0].setEnabled(getMap() != null);
			}
			return command;
		}
		
		public override System.String myGetState()
		{
			return "";
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			myGetKeyCommands();
			return myCommand.matches(stroke)?apply():null;
		}
		
		public override void  mySetState(System.String newState)
		{
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			return piece.boundingBox();
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			piece.draw(g, x, y, obs, zoom);
		}
		
		public override System.String getName()
		{
			return piece.getName();
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		public virtual Command apply()
		{
			PieceFilter filter = propertiesFilter.getFilter(Decorator.getOutermost(this));
			Command c = new NullCommand();
			if (restrictRange)
			{
				int r = range;
				if (!fixedRange)
				{
					System.String rangeValue = (System.String) Decorator.getOutermost(this).getProperty(rangeProperty);
					try
					{
						r = System.Int32.Parse(rangeValue);
					}
					catch (System.FormatException e)
					{
						reportDataError(this, Resources.getString("Error.non_number_error"), "range[" + rangeProperty + "]=" + rangeValue, e);
					}
				}
				System.Drawing.Point tempAux = Position;
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				filter = new BooleanAndPieceFilter(filter, new RangeFilter(getMap(), ref tempAux, r));
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Map m: Map.getMapList())
			{
				c = c.append(globalCommand.apply(m, filter));
			}
			
			return c;
		}
		
		public override PieceI18nData getI18nData()
		{
			return getI18nData(commandName, getCommandDescription(description, "Command name"));
		}
		
		public class Ed : PieceEditor
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener
			{
				public AnonymousClassPropertyChangeListener(Ed enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Ed enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Ed enclosingInstance;
				public Ed Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
				{
					
					bool isRange = true.Equals(Enclosing_Instance.restrictRange.getValue());
					bool isFixed = true.Equals(Enclosing_Instance.fixedRange.getValue());
					
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(Enclosing_Instance.range.Controls, "Visible", isRange && isFixed);
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(Enclosing_Instance.fixedRange.Controls, "Visible", isRange);
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(Enclosing_Instance.rangeProperty.Controls, "Visible", isRange && !isFixed);
					
					//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
					System.Windows.Forms.Form w = (System.Windows.Forms.Form) Enclosing_Instance.range.Controls.Parent;
					if (w != null)
					{
						//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
						w.pack();
					}
				}
			}
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return controls;
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					SequenceEncoder se = new SequenceEncoder(';');
					se.append(nameInput.ValueString).append(keyInput.ValueString).append(globalKey.ValueString).append(propertyMatch.ValueString).append(restrictRange.ValueString).append(range.ValueString).append(suppress.booleanValue()).append(fixedRange.booleanValue()).append(rangeProperty.ValueString).append(descInput.ValueString).append(deckPolicy.getIntValue());
					return VassalSharp.counters.CounterGlobalKeyCommand.ID + se.Value;
				}
				
			}
			virtual public System.String State
			{
				get
				{
					return "";
				}
				
			}
			protected internal StringConfigurer nameInput;
			protected internal NamedHotKeyConfigurer keyInput;
			protected internal NamedHotKeyConfigurer globalKey;
			protected internal PropertyExpressionConfigurer propertyMatch;
			protected internal MassKeyCommand.DeckPolicyConfig deckPolicy;
			protected internal BooleanConfigurer suppress;
			protected internal BooleanConfigurer restrictRange;
			protected internal BooleanConfigurer fixedRange;
			protected internal IntConfigurer range;
			protected internal StringConfigurer rangeProperty;
			protected internal StringConfigurer descInput;
			protected internal System.Windows.Forms.Panel controls;
			
			public Ed(CounterGlobalKeyCommand p)
			{
				
				//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				PropertyChangeListener pl = new AnonymousClassPropertyChangeListener(this);
				
				controls = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				controls.setLayout(new BoxLayout(controls, BoxLayout.Y_AXIS));
				
				descInput = new StringConfigurer(null, "Description:  ", p.description);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(descInput.Controls);
				
				nameInput = new StringConfigurer(null, "Command name:  ", p.commandName);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(nameInput.Controls);
				
				keyInput = new NamedHotKeyConfigurer(null, "Keyboard Command:  ", p.key);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(keyInput.Controls);
				
				globalKey = new NamedHotKeyConfigurer(null, "Global Key Command:  ", p.globalKey);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(globalKey.Controls);
				
				propertyMatch = new PropertyExpressionConfigurer(null, "Matching Properties:  ", p.propertiesFilter);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(propertyMatch.Controls);
				
				deckPolicy = new MassKeyCommand.DeckPolicyConfig();
				deckPolicy.setValue(p.globalCommand.SelectFromDeck);
				controls.add(deckPolicy.Controls);
				
				restrictRange = new BooleanConfigurer(null, "Restrict Range?", p.restrictRange);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(restrictRange.Controls);
				restrictRange.PropertyChange += new SupportClass.PropertyChangeEventHandler(pl.propertyChange);
				
				fixedRange = new BooleanConfigurer(null, "Fixed Range?", p.fixedRange);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(fixedRange.Controls);
				fixedRange.PropertyChange += new SupportClass.PropertyChangeEventHandler(pl.propertyChange);
				
				range = new IntConfigurer(null, "Range:  ", p.range);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(range.Controls);
				
				rangeProperty = new StringConfigurer(null, "Range Property:  ", p.rangeProperty);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(rangeProperty.Controls);
				
				suppress = new BooleanConfigurer(null, "Suppress individual reports?", p.globalCommand.ReportSingle);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(suppress.Controls);
				
				//UPGRADE_TODO: Method 'java.beans.PropertyChangeListener.propertyChange' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				pl.propertyChange(null);
			}
		}
	}
}