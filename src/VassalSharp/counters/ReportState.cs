/*
* $Id$
*
* Copyright (c) 2000-2013 by Rodney Kinney, Brent Easton
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
/*
* Created by IntelliJ IDEA.
* User: rkinney
* Date: Oct 2, 2002
* Time: 6:30:35 AM
* To change template for new class use
* Code Style | Class Templates options (Tools | IDE Options).
*/
using System;
using GameModule = VassalSharp.build.GameModule;
using Chatter = VassalSharp.build.module.Chatter;
using Map = VassalSharp.build.module.Map;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using PropertySource = VassalSharp.build.module.properties.PropertySource;
using ChangeTracker = VassalSharp.command.ChangeTracker;
using Command = VassalSharp.command.Command;
using NamedKeyStrokeArrayConfigurer = VassalSharp.configure.NamedKeyStrokeArrayConfigurer;
using PlayerIdFormattedStringConfigurer = VassalSharp.configure.PlayerIdFormattedStringConfigurer;
using StringArrayConfigurer = VassalSharp.configure.StringArrayConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using Resources = VassalSharp.i18n.Resources;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using ArrayUtils = VassalSharp.tools.ArrayUtils;
using FormattedString = VassalSharp.tools.FormattedString;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> A GamePiece with this trait will echo the piece's current name when any of a given key commands are pressed
	/// (and after they take effect)
	/// </summary>
	public class ReportState:Decorator, TranslatablePiece
	{
		virtual protected internal System.String PieceName
		{
			get
			{
				
				System.String name = "";
				
				VassalSharp.counters.GlobalAccess.hideAll();
				
				name = getOutermost(this).getName();
				
				VassalSharp.counters.GlobalAccess.revertAll();
				
				return name;
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
				System.String d = "Report Action";
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
				return HelpFile.getReferenceManualPage("ReportChanges.htm");
			}
			
		}
		public const System.String ID = "report;";
		protected internal NamedKeyStroke[] keys;
		protected internal FormattedString format = new FormattedString();
		protected internal System.String reportFormat;
		protected internal System.String[] cycleReportFormat;
		protected internal NamedKeyStroke[] cycleDownKeys;
		protected internal int cycleIndex = - 1;
		protected internal System.String description;
		
		public ReportState():this(ID, null)
		{
		}
		
		public ReportState(System.String type, GamePiece inner)
		{
			mySetType(type);
			setInner(inner);
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
		
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			return new KeyCommand[0];
		}
		
		public override System.String myGetState()
		{
			return cycleIndex + "";
		}
		
		public override System.String myGetType()
		{
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(NamedKeyStrokeArrayConfigurer.encode(keys)).append(reportFormat).append(NamedKeyStrokeArrayConfigurer.encode(cycleDownKeys)).append(StringArrayConfigurer.arrayToString(cycleReportFormat)).append(description);
			return ID + se.Value;
		}
		
		// We perform the inner commands first so that their effects will be reported
		public override Command keyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			Command c = piece.keyEvent(stroke);
			return c == null?myKeyEvent(stroke):c.append(myKeyEvent(stroke));
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			GamePiece outer = getOutermost(this);
			
			// Retrieve the name, location and visibilty of the unit prior to the
			// trait being executed if it is outside this one.
			
			format.setProperty(MAP_NAME, getMap() == null?null:getMap().getConfigureName());
			format.setProperty(LOCATION_NAME, getMap() == null?null:getMap().locationName(Position));
			format.setProperty(OLD_MAP_NAME, (System.String) getProperty(BasicPiece.OLD_MAP));
			format.setProperty(OLD_LOCATION_NAME, (System.String) getProperty(BasicPiece.OLD_LOCATION_NAME));
			
			Command c = null;
			
			GamePiece oldPiece = (GamePiece) getProperty(VassalSharp.counters.Properties_Fields.SNAPSHOT);
			
			bool wasVisible = oldPiece != null && !true.Equals(oldPiece.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_OTHERS));
			bool isVisible = !true.Equals(outer.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_OTHERS));
			
			VassalSharp.counters.GlobalAccess.hideAll();
			System.String oldUnitName = oldPiece == null?null:oldPiece.LocalizedName;
			format.setProperty(OLD_UNIT_NAME, oldUnitName);
			System.String newUnitName = outer.LocalizedName;
			format.setProperty(NEW_UNIT_NAME, newUnitName);
			VassalSharp.counters.GlobalAccess.revertAll();
			
			// Only make a report if:
			//  1. It's not part of a global command with Single Reporting on
			//  2. The piece is visible to all players either before or after the trait
			//     command was executed.
			
			if (isVisible || wasVisible)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'allKeys '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				NamedKeyStroke[] allKeys = ArrayUtils.append(keys, cycleDownKeys);
				
				for (int i = 0; i < allKeys.Length; ++i)
				{
					if (stroke != null && stroke.Equals(allKeys[i].KeyStroke))
					{
						
						//
						// Find the Command Name
						//
						System.String commandName = "";
						KeyCommand[] k = ((Decorator) outer).getKeyCommands();
						for (int j = 0; j < k.Length; j++)
						{
							System.Windows.Forms.KeyEventArgs commandKey = k[j].KeyStroke;
							if (stroke.Equals(commandKey))
							{
								commandName = k[j].Name;
							}
						}
						
						ChangeTracker tracker = new ChangeTracker(this);
						
						format.setProperty(COMMAND_NAME, commandName);
						
						System.String theFormat = reportFormat;
						if (cycleIndex >= 0 && cycleReportFormat.Length > 0)
						{
							if (i < keys.Length)
							{
								theFormat = cycleReportFormat[cycleIndex];
								cycleIndex = (cycleIndex + 1) % cycleReportFormat.Length;
							}
							else
							{
								cycleIndex = (cycleIndex + cycleReportFormat.Length - 1) % cycleReportFormat.Length;
								theFormat = cycleReportFormat[(cycleIndex + cycleReportFormat.Length - 1) % cycleReportFormat.Length];
							}
						}
						format.Format = getTranslation(theFormat);
						
						OldAndNewPieceProperties properties = new OldAndNewPieceProperties(oldPiece, outer);
						
						System.String reportText = format.getLocalizedText(properties);
						
						if (getMap() != null)
						{
							format.setFormat(getMap().getChangeFormat());
						}
						else if (!Map.ChangeReportingEnabled)
						{
							format.Format = "";
						}
						else
						{
							format.Format = "$" + Map.MESSAGE + "$";
						}
						format.setProperty(Map.MESSAGE, reportText);
						reportText = format.getLocalizedText(properties);
						
						if (reportText.Length > 0)
						{
							Command display = new Chatter.DisplayText(GameModule.getGameModule().getChatter(), "* " + reportText);
							display.execute();
							c = display;
						}
						c = tracker.ChangeCommand.append(c);
						break;
					}
				}
			}
			
			return c;
		}
		
		public override void  mySetState(System.String newState)
		{
			if (newState.Length > 0)
			{
				try
				{
					cycleIndex = System.Int32.Parse(newState);
				}
				catch (System.FormatException e)
				{
					cycleIndex = - 1;
					reportDataError(this, Resources.getString("Error.non_number_error"), "Trying to init Message Index to " + newState);
				}
			}
			else
			{
				cycleIndex = - 1;
			}
		}
		
		public virtual void  mySetType(System.String type)
		{
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			st.nextToken();
			System.String encodedKeys = st.nextToken("");
			if (encodedKeys.IndexOf(',') > 0)
			{
				keys = NamedKeyStrokeArrayConfigurer.decode(encodedKeys);
			}
			else
			{
				keys = new NamedKeyStroke[encodedKeys.Length];
				for (int i = 0; i < keys.Length; i++)
				{
					keys[i] = NamedKeyStroke.getNamedKeyStroke(encodedKeys[i], (int) System.Windows.Forms.Keys.Control);
				}
			}
			reportFormat = st.nextToken("$" + LOCATION_NAME + "$: $" + NEW_UNIT_NAME + "$ *");
			System.String encodedCycleDownKeys = st.nextToken("");
			if (encodedCycleDownKeys.IndexOf(',') > 0)
			{
				cycleDownKeys = NamedKeyStrokeArrayConfigurer.decode(encodedCycleDownKeys);
			}
			else
			{
				cycleDownKeys = new NamedKeyStroke[encodedCycleDownKeys.Length];
				for (int i = 0; i < cycleDownKeys.Length; i++)
				{
					cycleDownKeys[i] = NamedKeyStroke.getNamedKeyStroke(encodedCycleDownKeys[i], (int) System.Windows.Forms.Keys.Control);
				}
			}
			cycleReportFormat = StringArrayConfigurer.stringToArray(st.nextToken(""));
			description = st.nextToken("");
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		public override PieceI18nData getI18nData()
		{
			int c = cycleReportFormat == null?0:cycleReportFormat.Length;
			System.String[] formats = new System.String[c + 1];
			System.String[] descriptions = new System.String[c + 1];
			formats[0] = reportFormat;
			descriptions[0] = getCommandDescription(description, "Report Format");
			int j = 1;
			for (int i = 0; i < c; i++)
			{
				formats[j] = cycleReportFormat[i];
				descriptions[j] = getCommandDescription(description, "Report Format " + j);
				j++;
			}
			return getI18nData(formats, descriptions);
		}
		
		public const System.String OLD_UNIT_NAME = "oldPieceName";
		public const System.String NEW_UNIT_NAME = "newPieceName";
		public const System.String MAP_NAME = "mapName";
		public const System.String OLD_MAP_NAME = "oldMapName";
		public const System.String LOCATION_NAME = "location";
		public const System.String OLD_LOCATION_NAME = "oldLocation";
		public const System.String COMMAND_NAME = "menuCommand";
		
		public class Ed : PieceEditor
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassItemListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassItemListener
			{
				public AnonymousClassItemListener(Ed enclosingInstance)
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
				public virtual void  itemStateChanged(System.Object event_sender, System.EventArgs e)
				{
					if (event_sender is System.Windows.Forms.MenuItem)
						((System.Windows.Forms.MenuItem) event_sender).Checked = !((System.Windows.Forms.MenuItem) event_sender).Checked;
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(Enclosing_Instance.format.Controls, "Visible", !Enclosing_Instance.cycle.Checked);
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(Enclosing_Instance.cycleFormat.Controls, "Visible", Enclosing_Instance.cycle.Checked);
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(Enclosing_Instance.cycleDownKeys.Controls, "Visible", Enclosing_Instance.cycle.Checked);
					//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
					System.Windows.Forms.Form w = (System.Windows.Forms.Form) Enclosing_Instance.box.Parent;
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
					return box;
				}
				
			}
			virtual public System.String State
			{
				get
				{
					return cycle.Checked?"0":"-1";
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					SequenceEncoder se = new SequenceEncoder(';');
					if (cycle.Checked && cycleFormat.StringArray.Length > 0)
					{
						se.append(keys.ValueString).append("").append(cycleDownKeys.ValueString).append(cycleFormat.ValueString);
					}
					else
					{
						se.append(keys.ValueString).append(format.ValueString).append("").append("");
					}
					se.append(descInput.ValueString);
					return VassalSharp.counters.ReportState.ID + se.Value;
				}
				
			}
			
			private NamedKeyStrokeArrayConfigurer keys;
			private StringConfigurer format;
			private System.Windows.Forms.CheckBox cycle;
			private StringArrayConfigurer cycleFormat;
			private NamedKeyStrokeArrayConfigurer cycleDownKeys;
			protected internal StringConfigurer descInput;
			private System.Windows.Forms.Panel box;
			
			public Ed(ReportState piece)
			{
				
				box = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				box.setLayout(new BoxLayout(box, BoxLayout.Y_AXIS));
				descInput = new StringConfigurer(null, "Description:  ", piece.description);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(descInput.Controls);
				keys = new NamedKeyStrokeArrayConfigurer(null, "Report on these keystrokes:  ", piece.keys);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(keys.Controls);
				cycle = SupportClass.CheckBoxSupport.CreateCheckBox("Cycle through different messages?");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(cycle);
				format = new PlayerIdFormattedStringConfigurer(null, "Report format:  ", new System.String[]{VassalSharp.counters.ReportState.COMMAND_NAME, VassalSharp.counters.ReportState.OLD_UNIT_NAME, VassalSharp.counters.ReportState.NEW_UNIT_NAME, VassalSharp.counters.ReportState.MAP_NAME, VassalSharp.counters.ReportState.OLD_MAP_NAME, VassalSharp.counters.ReportState.LOCATION_NAME, VassalSharp.counters.ReportState.OLD_LOCATION_NAME});
				format.setValue(piece.reportFormat);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(format.Controls);
				cycleFormat = new StringArrayConfigurer(null, "Message formats", piece.cycleReportFormat);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(cycleFormat.Controls);
				cycleDownKeys = new NamedKeyStrokeArrayConfigurer(null, "Report previous message on these keystrokes:  ", piece.cycleDownKeys);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(cycleDownKeys.Controls);
				//UPGRADE_TODO: Interface 'java.awt.event.ItemListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				ItemListener l = new AnonymousClassItemListener(this);
				//UPGRADE_TODO: Method 'java.awt.event.ItemListener.itemStateChanged' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				l.itemStateChanged(null);
				cycle.CheckedChanged += new System.EventHandler(l.itemStateChanged);
				cycle.Checked = piece.cycleReportFormat.Length > 0;
			}
		}
		
		/// <summary> Looks in both the new and old piece for property values.
		/// Any properties with names of the format "oldXyz" are changed
		/// to "xyz" and applied to the old piece.
		/// </summary>
		/// <author>  rkinney
		/// 
		/// </author>
		private class OldAndNewPieceProperties : PropertySource
		{
			private GamePiece oldPiece;
			private GamePiece newPiece;
			public OldAndNewPieceProperties(GamePiece oldPiece, GamePiece newPiece):base()
			{
				this.oldPiece = oldPiece;
				this.newPiece = newPiece;
			}
			public virtual System.Object getProperty(System.Object key)
			{
				System.Object value_Renamed = null;
				if (key != null)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.String name = key.ToString();
					if (name.StartsWith("old") && name.Length >= 4)
					{
						name = name.Substring(3);
						value_Renamed = oldPiece.getProperty(name);
					}
					else
					{
						value_Renamed = newPiece.getProperty(key);
					}
				}
				return value_Renamed;
			}
			
			public virtual System.Object getLocalizedProperty(System.Object key)
			{
				return getProperty(key);
			}
		}
	}
}