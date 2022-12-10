/*
* $Id$
*
* Copyright (c) 2004-2009 by Rodney Kinney, Joel Uckelman
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
//UPGRADE_TODO: The type 'java.awt.dnd.DragSourceMotionListener' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using DragSourceMotionListener = java.awt.dnd.DragSourceMotionListener;
using AbstractConfigurable = VassalSharp.build.AbstractConfigurable;
using AutoConfigurable = VassalSharp.build.AutoConfigurable;
using BadDataReport = VassalSharp.build.BadDataReport;
using Buildable = VassalSharp.build.Buildable;
using Configurable = VassalSharp.build.Configurable;
using GameModule = VassalSharp.build.GameModule;
using GameComponent = VassalSharp.build.module.GameComponent;
using Map = VassalSharp.build.module.Map;
using NewGameIndicator = VassalSharp.build.module.NewGameIndicator;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Board = VassalSharp.build.module.map.boardPicker.Board;
using MapGrid = VassalSharp.build.module.map.boardPicker.board.MapGrid;
using BadCoords = VassalSharp.build.module.map.boardPicker.board.MapGrid.BadCoords;
using PieceSlot = VassalSharp.build.widget.PieceSlot;
using Command = VassalSharp.command.Command;
using AutoConfigurer = VassalSharp.configure.AutoConfigurer;
using Configurer = VassalSharp.configure.Configurer;
using StringEnum = VassalSharp.configure.StringEnum;
using ValidationReport = VassalSharp.configure.ValidationReport;
using VisibilityCondition = VassalSharp.configure.VisibilityCondition;
using GamePiece = VassalSharp.counters.GamePiece;
using PieceCloner = VassalSharp.counters.PieceCloner;
using Properties = VassalSharp.counters.Properties;
using Stack = VassalSharp.counters.Stack;
using ComponentI18nData = VassalSharp.i18n.ComponentI18nData;
using Resources = VassalSharp.i18n.Resources;
using AdjustableSpeedScrollPane = VassalSharp.tools.AdjustableSpeedScrollPane;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using UniqueIdManager = VassalSharp.tools.UniqueIdManager;
using ImageUtils = VassalSharp.tools.image.ImageUtils;
using MenuManager = VassalSharp.tools.menu.MenuManager;
namespace VassalSharp.build.module.map
{
	
	/// <summary> This is the "At-Start Stack" component, which initializes a Map or Board with a specified stack.
	/// Because it uses a regular stack, this component is better suited for limited-force-pool collections
	/// of counters than a {@link DrawPile}
	/// 
	/// </summary>
	public class SetupStack:AbstractConfigurable, GameComponent, UniqueIdManager.Identifyable
	{
		public SetupStack()
		{
			InitBlock();
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassVisibilityCondition' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassVisibilityCondition : VisibilityCondition
		{
			public AnonymousClassVisibilityCondition(SetupStack enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SetupStack enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SetupStack enclosingInstance;
			public SetupStack Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual bool shouldBeVisible()
			{
				Board b = getConfigureBoard();
				if (b == null)
					return false;
				else
					return b.getGrid() != null;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassVisibilityCondition1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassVisibilityCondition1 : VisibilityCondition
		{
			public AnonymousClassVisibilityCondition1(SetupStack enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SetupStack enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SetupStack enclosingInstance;
			public SetupStack Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual bool shouldBeVisible()
			{
				return Enclosing_Instance.UseGridLocation;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassVisibilityCondition2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassVisibilityCondition2 : VisibilityCondition
		{
			public AnonymousClassVisibilityCondition2(SetupStack enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SetupStack enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SetupStack enclosingInstance;
			public SetupStack Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual bool shouldBeVisible()
			{
				return !Enclosing_Instance.UseGridLocation;
			}
		}
		private void  InitBlock()
		{
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > []
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				String.
			}
		}
		virtual protected internal bool UseGridLocation
		{
			// must have a useable board with a grid
			
			get
			{
				if (!useGridLocation)
					return false;
				Board b = getConfigureBoard();
				if (b == null)
					return false;
				MapGrid g = b.getGrid();
				if (g == null)
					return false;
				else
					return true;
			}
			
		}
		virtual public Command RestoreCommand
		{
			get
			{
				return null;
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[]{Resources.getString(Resources.NAME_LABEL), Resources.getString("Editor.StartStack.board"), Resources.getString("Editor.StartStack.grid"), Resources.getString("Editor.StartStack.location"), Resources.getString("Editor.StartStack.position_x"), Resources.getString("Editor.StartStack.position_y")};
			}
			
		}
		private static UniqueIdManager idMgr = new UniqueIdManager("SetupStack");
		public const System.String COMMAND_PREFIX = "SETUP_STACK\t";
		protected internal System.Drawing.Point pos = new System.Drawing.Point(0, 0);
		public const System.String OWNING_BOARD = "owningBoard";
		public const System.String X_POSITION = "x";
		public const System.String Y_POSITION = "y";
		protected internal Map map;
		protected internal System.String owningBoardName;
		protected internal System.String id;
		public const System.String NAME = "name";
		protected internal static NewGameIndicator indicator;
		
		protected internal StackConfigurer stackConfigurer;
		protected internal System.Windows.Forms.Button configureButton;
		protected internal System.String location;
		protected internal bool useGridLocation;
		public const System.String LOCATION = "location";
		public const System.String USE_GRID_LOCATION = "useGridLocation";
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override VisibilityCondition getAttributeVisibility(System.String name)
		{
			if (USE_GRID_LOCATION.Equals(name))
			{
				return new AnonymousClassVisibilityCondition(this);
			}
			else if (LOCATION.Equals(name))
			{
				return new AnonymousClassVisibilityCondition1(this);
			}
			else if (X_POSITION.Equals(name) || Y_POSITION.Equals(name))
			{
				return new AnonymousClassVisibilityCondition2(this);
			}
			else
				return base.getAttributeVisibility(name);
		}
		
		// only update the position if we're using the location name
		protected internal virtual void  updatePosition()
		{
			if (UseGridLocation && location != null && !location.Equals(""))
			{
				try
				{
					pos = getConfigureBoard().getGrid().getLocation(location);
				}
				catch (BadCoords e)
				{
					ErrorDialog.dataError(new BadDataReport(this, "Error.setup_stack_position_error", location, e));
				}
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override void  validate(Buildable target, ValidationReport report)
		{
			if (UseGridLocation)
			{
				if (location == null)
				{
					report.addWarning(getConfigureName() + Resources.getString("SetupStack.null_location"));
				}
				else
				{
					try
					{
						getConfigureBoard().getGrid().getLocation(location);
					}
					catch (BadCoords e)
					{
						System.String msg = "Bad location name " + location + " in " + getConfigureName();
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						if (e.Message != null)
						{
							//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
							msg += (":  " + e.Message);
						}
						report.addWarning(msg);
					}
				}
			}
			
			base.validate(target, report);
		}
		
		protected internal virtual void  updateLocation()
		{
			Board b = getConfigureBoard();
			if (b != null)
			{
				MapGrid g = b.getGrid();
				if (g != null)
				{
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					location = g.locationName(ref pos);
				}
			}
		}
		
		public virtual void  setup(bool gameStarting)
		{
			if (gameStarting && indicator.NewGame && isOwningBoardActive())
			{
				Stack s = initializeContents();
				updatePosition();
				System.Drawing.Point p = new System.Drawing.Point(new System.Drawing.Size(pos));
				if (owningBoardName != null)
				{
					System.Drawing.Rectangle r = map.getBoardByName(owningBoardName).bounds();
					p.Offset(r.X, r.Y);
				}
				if (placeNonStackingSeparately())
				{
					for (int i = 0; i < s.PieceCount; ++i)
					{
						GamePiece piece = s.getPieceAt(i);
						if (true.Equals(piece.getProperty(VassalSharp.counters.Properties_Fields.NO_STACK)))
						{
							s.remove(piece);
							piece.Parent = null;
							map.placeAt(piece, p);
							i--;
						}
					}
				}
				map.placeAt(s, p);
			}
		}
		
		protected internal virtual bool placeNonStackingSeparately()
		{
			return true;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		OwningBoardPrompt.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Boolean.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		String.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Integer.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Integer.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String [] getAttributeNames()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new String []
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		NAME, 
		OWNING_BOARD, 
		USE_GRID_LOCATION, 
		LOCATION, 
		X_POSITION, 
		Y_POSITION
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String getAttributeValueString(String key)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(NAME.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return getConfigureName();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(OWNING_BOARD.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return owningBoardName;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(USE_GRID_LOCATION.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return Boolean.toString(useGridLocation);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(LOCATION.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return location;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(X_POSITION.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(pos.x);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(Y_POSITION.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(pos.y);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setAttribute(String key, Object value)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(NAME.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		setConfigureName((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(OWNING_BOARD.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(OwningBoardPrompt.ANY.equals(value))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		owningBoardName = null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		owningBoardName =(String) value;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	updateConfigureButton();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(USE_GRID_LOCATION.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		value = Boolean.valueOf((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	useGridLocation =((Boolean) value).booleanValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(LOCATION.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		location =(String) value;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(X_POSITION.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		value = Integer.valueOf((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	pos.x =((Integer) value).intValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(Y_POSITION.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		value = Integer.valueOf((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	pos.y =((Integer) value).intValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void add(Buildable child)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		super.add(child);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	updateConfigureButton();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void addTo(Buildable parent)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(indicator == null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		indicator = new NewGameIndicator(COMMAND_PREFIX);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	map =(Map) parent;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	idMgr.add(this);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	GameModule.getGameModule().getGameState().addGameComponent(this);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	setAttributeTranslatable(NAME, false);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Class < ? > [] getAllowableConfigureComponents()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new Class < ? > []
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ PieceSlot.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	class
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public HelpFile getHelpFile()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return HelpFile.getReferenceManualPage(SetupStack.htm);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public static String getConfigureTypeName()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return Resources.getString(Editor.StartStack.component_type); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void removeFrom(Buildable parent)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		idMgr.remove(this);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	GameModule.getGameModule().getGameState().removeGameComponent(this);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected boolean isOwningBoardActive()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		boolean active = false;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(owningBoardName == null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		active = true;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(map.getBoardByName(owningBoardName) != null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		active = true;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return active;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected Stack initializeContents()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		Stack s = createStack();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Configurable [] c = getConfigureComponents();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < c.length;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(c [i] instanceof PieceSlot)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		PieceSlot slot =(PieceSlot) c [i];
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	GamePiece p = slot.getPiece();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	p = PieceCloner.getInstance().clonePiece(p);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	GameModule.getGameModule().getGameState().addPiece(p);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	s.add(p);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	GameModule.getGameModule().getGameState().addPiece(s);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return s;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected Stack createStack()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new Stack();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setId(String id)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		this.id = id;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String getId()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return id;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//  public static class GridPrompt extends StringEnum {
	//    public static final String NONE = "<none>";
	//    public static final String ZONE = "(Zone)";
	//    public static final String BOARD = "(Board)";
	//
	//    public GridPrompt() {
	//    }
	//
	//  @Override
	//  public String[] getValidValues(AutoConfigurable target) {
	//    ArrayList<String> values = new ArrayList<String>();
	//    values.add(NONE);
	//    if (target instanceof SetupStack) {
	//      SetupStack stack = (SetupStack) target;
	//      BoardPicker bp = stack.map.getBoardPicker();
	//      if (stack.owningBoardName != null) {
	//        Board b = bp.getBoard(stack.owningBoardName);
	//        MapGrid grid = b.getGrid();
	//        if (grid != null) {
	//          GridNumbering gn = grid.getGridNumbering();
	//          if (gn != null)
	//            values.add(BOARD + " " + b.getName());
	//          if (grid instanceof ZonedGrid) {
	//            ZonedGrid zg = (ZonedGrid) grid;
	//            for (Iterator i = zg.getZones(); i.hasNext(); ) {
	//              Zone z = (Zone) i.next();
	//              if (!z.isUseParentGrid() && z.getGrid() != null && z.getGrid().getGridNumbering() != null)
	//                values.add(ZONE + " " + z.getName());
	//            }
	//          }
	//        }
	//      }
	//    }
	//    return values.toArray(new String[values.size()]);
	//  }
	//  }
	//
	public class OwningBoardPrompt:StringEnum
	{
		public const System.String ANY = "<any>";
		
		public OwningBoardPrompt()
		{
		}
		
		public override System.String[] getValidValues(AutoConfigurable target)
		{
			System.String[] values;
			if (target is SetupStack)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				ArrayList < String > l = new ArrayList < String >();
				l.add(ANY);
				Map m = ((SetupStack) target).map;
				if (m != null)
				{
					l.addAll(Arrays.asList(m.BoardPicker.getAllowableBoardNames()));
				}
				else
				{
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(Map m2: Map.getMapList())
					{
						l.addAll(Arrays.asList(m2.getBoardPicker().getAllowableBoardNames()));
					}
				}
				values = l.toArray(new System.String[l.size()]);
			}
			else
			{
				values = new System.String[]{ANY};
			}
			return values;
		}
	}
	
	
	/*
	*  GUI Stack Placement Configurer
	*/
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected Configurer xConfig, yConfig, locationConfig;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Configurer getConfigurer()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		config = null; // Don't cache the Configurer so that the list of available boards won't go stale
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Configurer c = super.getConfigurer();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	xConfig =((AutoConfigurer) c).getConfigurer(X_POSITION);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	yConfig =((AutoConfigurer) c).getConfigurer(Y_POSITION);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	locationConfig =((AutoConfigurer) c).getConfigurer(LOCATION);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	updateConfigureButton();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	((Container) c.getControls()).add(configureButton);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return c;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void updateConfigureButton()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(configureButton == null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		configureButton = new JButton(Reposition Stack);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	configureButton.addActionListener(new ActionListener()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void actionPerformed(ActionEvent e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		configureStack();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	});
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	configureButton.setEnabled(getConfigureBoard() != null && buildComponents.size() > 0);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void configureStack()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		stackConfigurer = new StackConfigurer(this);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	stackConfigurer.init();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	stackConfigurer.setVisible(true);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected PieceSlot getTopPiece()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		Iterator < PieceSlot > i = 
		getAllDescendantComponentsOf(PieceSlot.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	class).iterator();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return i.hasNext() ? i.next(): null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/*
	* Return a board to configure the stack on.
	*/
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected Board getConfigureBoard()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		
		Board board = null;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(map != null && !OwningBoardPrompt.ANY.equals(owningBoardName))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		board = map.getBoardPicker().getBoard(owningBoardName);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	if(board == null && map != null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		String [] allBoards = map.getBoardPicker().getAllowableBoardNames();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(allBoards.length > 0)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		board = map.getBoardPicker().getBoard(allBoards [0]);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	return board;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final Dimension DEFAULT_SIZE = new Dimension(800, 600);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final int DELTA = 1;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final int FAST = 10;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final int FASTER = 5;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final int DEFAULT_DUMMY_SIZE = 50;
	
	[Serializable]
	public class StackConfigurer:System.Windows.Forms.Form
	{
		static private System.Int32 state221;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(StackConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(StackConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private StackConfigurer enclosingInstance;
			public StackConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs e)
			{
				e.Cancel = true;
				Enclosing_Instance.cancel();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(StackConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(StackConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private StackConfigurer enclosingInstance;
			public StackConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.snap();
				Enclosing_Instance.view.Focus();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(StackConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(StackConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private StackConfigurer enclosingInstance;
			public StackConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				Enclosing_Instance.Visible = false;
				// Update the Component configurer to reflect the change
				xConfig.setValue(System.Convert.ToString(Enclosing_Instance.myStack.pos.X));
				yConfig.setValue(System.Convert.ToString(Enclosing_Instance.myStack.pos.Y));
				if (locationConfig != null)
				{
					// DrawPile's do not have a location
					updateLocation();
					locationConfig.setValue(location);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener2
		{
			public AnonymousClassActionListener2(StackConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(StackConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private StackConfigurer enclosingInstance;
			public StackConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.cancel();
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				Enclosing_Instance.Visible = false;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPopupMenuListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPopupMenuListener
		{
			public AnonymousClassPopupMenuListener(StackConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(StackConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private StackConfigurer enclosingInstance;
			public StackConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  popupMenuCanceled(System.Object event_sender, System.EventArgs evt)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				Enclosing_Instance.view.Refresh();
			}
			public virtual void  popupMenuWillBecomeInvisible(System.Object event_sender, System.EventArgs evt)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				Enclosing_Instance.view.Refresh();
			}
			public virtual void  popupMenuWillBecomeVisible(System.Object event_sender, System.EventArgs evt)
			{
			}
		}
		private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			state221 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			state221 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		virtual public System.Drawing.Bitmap DummyImage
		{
			/*
			* If the piece to be displayed does not have an Image, then we
			* need to supply a dummy one.
			*/
			
			get
			{
				if (dummyImage == null)
				{
					dummyImage = ImageUtils.createCompatibleTranslucentImage(dummySize.Width * 2, dummySize.Height * 2);
					//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dummyImage);
					SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.White);
					g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), 0, 0, dummySize.Width, dummySize.Height);
					SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.Black);
					g.DrawRectangle(SupportClass.GraphicsManager.manager.GetPen(g), 0, 0, dummySize.Width, dummySize.Height);
					g.Dispose();
				}
				return dummyImage;
			}
			
		}
		virtual public System.Drawing.Rectangle PieceBoundingBox
		{
			get
			{
				System.Drawing.Rectangle r = myPiece == null?new System.Drawing.Rectangle():System.Drawing.Rectangle.Truncate(myPiece.Shape.GetBounds());
				if (r.IsEmpty || r.Width == 0 || r.Height == 0)
				{
					r.X = 0 - dummySize.Width / 2;
					r.Y = 0 - dummySize.Height / 2;
					r.Width = dummySize.Width;
					r.Height = dummySize.Height;
				}
				return r;
			}
			
		}
		
		private const long serialVersionUID = 1L;
		
		protected internal Board board;
		protected internal View view;
		//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		protected internal System.Windows.Forms.ScrollableControl scroll;
		protected internal SetupStack myStack;
		protected internal PieceSlot mySlot;
		protected internal GamePiece myPiece;
		protected internal System.Drawing.Point savePosition
		{
			get
			{
				return savePosition_Renamed;
			}
			
			set
			{
				savePosition_Renamed = value;
			}
			
		}
		protected internal System.Drawing.Point savePosition_Renamed;
		protected internal System.Drawing.Size dummySize
		{
			get
			{
				return dummySize_Renamed;
			}
			
			set
			{
				dummySize_Renamed = value;
			}
			
		}
		protected internal System.Drawing.Size dummySize_Renamed;
		protected internal System.Drawing.Bitmap dummyImage;
		
		public StackConfigurer(SetupStack stack):base()
		{
			this.Text = "Adjust At-Start Stack";
			Menu = MenuManager.Instance.getMenuBarFor(this);
			
			myStack = stack;
			mySlot = stack.getTopPiece();
			if (mySlot != null)
			{
				myPiece = mySlot.Piece;
			}
			
			myStack.updatePosition();
			savePosition = new System.Drawing.Point(new System.Drawing.Size(myStack.pos));
			
			if (stack is DrawPile)
			{
				dummySize = new Dimension(((DrawPile) stack).getSize());
			}
			else
			{
				dummySize = new Dimension(DEFAULT_DUMMY_SIZE, DEFAULT_DUMMY_SIZE);
			}
			
			//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(this).windowClosing);
		}
		
		// Main Entry Point
		protected internal virtual void  init()
		{
			
			board = getConfigureBoard();
			
			view = new View(board, myStack);
			
			view.KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.build.module.map.StackConfigurer.keyDown);
			view.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyPressed);
			view.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyReleased);
			view.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyTyped);
			view.MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.build.module.map.StackConfigurer.mouseDown);
			view.Click += new System.EventHandler(this.mouseClicked);
			view.MouseEnter += new System.EventHandler(this.mouseEntered);
			view.MouseLeave += new System.EventHandler(this.mouseExited);
			view.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mousePressed);
			view.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseReleased);
			view.setFocusable(true);
			
			
			//UPGRADE_ISSUE: Field 'javax.swing.ScrollPaneConstants.VERTICAL_SCROLLBAR_ALWAYS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingScrollPaneConstants'"
			//UPGRADE_ISSUE: Field 'javax.swing.ScrollPaneConstants.HORIZONTAL_SCROLLBAR_ALWAYS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingScrollPaneConstants'"
			scroll = new AdjustableSpeedScrollPane(view, JScrollPane.VERTICAL_SCROLLBAR_ALWAYS, JScrollPane.HORIZONTAL_SCROLLBAR_ALWAYS);
			
			scroll.setPreferredSize(DEFAULT_SIZE);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(scroll);
			scroll.Dock = System.Windows.Forms.DockStyle.Fill;
			scroll.BringToFront();
			
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box textPanel = Box.createVerticalBox();
			System.Windows.Forms.Label temp_label2;
			temp_label2 = new System.Windows.Forms.Label();
			temp_label2.Text = "Arrow Keys - Move Stack";
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = temp_label2;
			textPanel.Controls.Add(temp_Control);
			System.Windows.Forms.Label temp_label4;
			temp_label4 = new System.Windows.Forms.Label();
			temp_label4.Text = "Ctrl/Shift Keys - Move Stack Faster  ";
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control2;
			temp_Control2 = temp_label4;
			textPanel.Controls.Add(temp_Control2);
			
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box displayPanel = Box.createHorizontalBox();
			
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box buttonPanel = Box.createHorizontalBox();
			System.Windows.Forms.Button snapButton = SupportClass.ButtonSupport.CreateStandardButton("Snap to grid");
			snapButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(snapButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonPanel.Controls.Add(snapButton);
			
			System.Windows.Forms.Button okButton = SupportClass.ButtonSupport.CreateStandardButton("Ok");
			okButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(okButton);
			System.Windows.Forms.Panel okPanel = new System.Windows.Forms.Panel();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			okPanel.Controls.Add(okButton);
			
			System.Windows.Forms.Button canButton = SupportClass.ButtonSupport.CreateStandardButton("Cancel");
			canButton.Click += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(canButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			okPanel.Controls.Add(canButton);
			
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box controlPanel = Box.createHorizontalBox();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			controlPanel.Controls.Add(textPanel);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			controlPanel.Controls.Add(displayPanel);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			controlPanel.Controls.Add(buttonPanel);
			
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box mainPanel = Box.createVerticalBox();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			mainPanel.Controls.Add(controlPanel);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			mainPanel.Controls.Add(okPanel);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(mainPanel);
			mainPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			mainPanel.SendToBack();
			
			scroll.Invalidate();
			updateDisplay();
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			Refresh();
		}
		
		protected internal virtual void  cancel()
		{
			myStack.pos.X = savePosition.X;
			myStack.pos.Y = savePosition.Y;
		}
		
		public virtual void  updateDisplay()
		{
			//UPGRADE_TODO: Method 'javax.swing.JComponent.getVisibleRect' was converted to 'System.Windows.Forms.Control.DisplayRectangle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentgetVisibleRect'"
			if (!view.DisplayRectangle.Contains(myStack.pos))
			{
				System.Drawing.Point tempAux = new System.Drawing.Point(myStack.pos.X, myStack.pos.Y);
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				view.center(ref tempAux);
			}
		}
		
		protected internal virtual void  snap()
		{
			MapGrid grid = board.getGrid();
			if (grid != null)
			{
				System.Drawing.Point snapTo = grid.snapTo(pos);
				pos.x = snapTo.X;
				pos.y = snapTo.Y;
				updateDisplay();
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				Refresh();
			}
		}
		
		//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public virtual System.Windows.Forms.ScrollableControl getScroll()
		{
			return scroll;
		}
		
		public virtual void  drawDummyImage(System.Drawing.Graphics g, int x, int y)
		{
			drawDummyImage(g, x - dummySize.Width / 2, y - dummySize.Height / 2, null, 1.0);
		}
		
		public virtual void  drawDummyImage(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
			g.DrawImage(DummyImage, x, y);
		}
		
		public virtual void  drawImage(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			System.Drawing.Rectangle r = myPiece == null?System.Drawing.Rectangle.Empty:myPiece.boundingBox();
			if (r.IsEmpty || r.Width == 0 || r.Height == 0)
			{
				drawDummyImage(g, x, y);
			}
			else
			{
				myPiece.draw(g, x, y, obs, zoom);
			}
		}
		
		public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			
		}
		
		public virtual void  keyPressed(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			
			//UPGRADE_TODO: Method 'java.awt.event.KeyEvent.getKeyCode' was converted to 'System.Windows.Forms.KeyEventArgs.KeyValue' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventKeyEventgetKeyCode'"
			switch (e.KeyValue)
			{
				
				case (int) System.Windows.Forms.Keys.Up: 
					adjustY(event_sender, - 1, e);
					break;
				
				case (int) System.Windows.Forms.Keys.Down: 
					adjustY(event_sender, 1, e);
					break;
				
				case (int) System.Windows.Forms.Keys.Left: 
					adjustX(event_sender, - 1, e);
					break;
				
				case (int) System.Windows.Forms.Keys.Right: 
					adjustX(event_sender, 1, e);
					break;
				
				default: 
					if (myPiece != null)
					{
						//UPGRADE_ISSUE: Method 'javax.swing.KeyStroke.getKeyStrokeForEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingKeyStrokegetKeyStrokeForEvent_javaawteventKeyEvent'"
						myPiece.keyEvent(KeyStroke.getKeyStrokeForEvent(event_sender, e));
					}
					break;
				
			}
			updateDisplay();
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			Refresh();
			e.Handled = true;
		}
		
		//UPGRADE_TODO: The equivalent of method adjustX needs to be overloaded, to match all the event parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1142'"
		protected internal virtual void  adjustX(System.Object event_sender, int direction, System.Windows.Forms.KeyEventArgs e)
		{
			int delta = direction * DELTA;
			if ((System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift))
			{
				delta *= FAST;
			}
			if ((System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control))
			{
				delta *= FASTER;
			}
			int newX = myStack.pos.X + delta;
			if (newX < 0)
				newX = 0;
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Dimension.getWidth' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			if (newX >= (System.Int32) board.Size.Width)
			{
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Dimension.getWidth' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				newX = (int) board.Size.Width - 1;
			}
			myStack.pos.X = newX;
		}
		
		//UPGRADE_TODO: The equivalent of method adjustY needs to be overloaded, to match all the event parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1142'"
		protected internal virtual void  adjustY(System.Object event_sender, int direction, System.Windows.Forms.KeyEventArgs e)
		{
			int delta = direction * DELTA;
			if ((System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift))
			{
				delta *= FAST;
			}
			if ((System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control))
			{
				delta *= FASTER;
			}
			int newY = myStack.pos.Y + delta;
			if (newY < 0)
				newY = 0;
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Dimension.getHeight' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			if (newY >= (System.Int32) board.Size.Height)
			{
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Dimension.getHeight' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				newY = (int) board.Size.Height - 1;
			}
			myStack.pos.Y = newY;
		}
		
		public virtual void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			
		}
		
		public virtual void  keyTyped(System.Object event_sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			
		}
		
		public virtual void  mouseClicked(System.Object event_sender, System.EventArgs e)
		{
			
		}
		
		public virtual void  mouseEntered(System.Object event_sender, System.EventArgs e)
		{
			
		}
		
		public virtual void  mouseExited(System.Object event_sender, System.EventArgs e)
		{
			
		}
		
		public virtual void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			System.Drawing.Rectangle r = PieceBoundingBox;
			r.translate(pos.x, pos.y);
			//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.isMetaDown' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
			if (myPiece != null && e.isMetaDown() && r.Contains(new System.Drawing.Point(e.X, e.Y)))
			{
				//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				System.Windows.Forms.ContextMenu popup = MenuDisplayer.createPopup(myPiece);
				//UPGRADE_NOTE: Some methods of the 'javax.swing.event.PopupMenuListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
				popup.Popup += new System.EventHandler(new AnonymousClassPopupMenuListener(this).popupMenuWillBecomeVisible);
				if (view.Visible)
				{
					//UPGRADE_TODO: Method 'javax.swing.JPopupMenu.show' was converted to 'System.Windows.Forms.ContextMenu.Show' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPopupMenushow_javaawtComponent_int_int'"
					popup.Show(view, new System.Drawing.Point(e.X, e.Y));
				}
			}
		}
		
		public virtual void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			
			
		}
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public ComponentI18nData getI18nData()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ComponentI18nData myI18nData = super.getI18nData();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	myI18nData.setAttributeTranslatable(LOCATION, false);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return myI18nData;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	// FIXME: check for duplication with PieceMover
	
	[Serializable]
	public class View:System.Windows.Forms.Panel, DragSourceMotionListener
	{
		
		private const long serialVersionUID = 1L;
		protected internal const int CURSOR_ALPHA = 127;
		protected internal const int EXTRA_BORDER = 4;
		protected internal Board myBoard;
		protected internal MapGrid myGrid;
		protected internal SetupStack myStack;
		protected internal GamePiece myPiece;
		protected internal PieceSlot slot;
		//UPGRADE_ISSUE: Class 'java.awt.dnd.DragSource' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000'"
		//UPGRADE_ISSUE: Method 'java.awt.dnd.DragSource.getDefaultDragSource' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000'"
		protected internal DragSource ds = DragSource.getDefaultDragSource();
		protected internal bool isDragging = false;
		protected internal System.Windows.Forms.Label dragCursor;
		//UPGRADE_TODO: Class 'javax.swing.JLayeredPane' was converted to 'System.Windows.Forms.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		protected internal System.Windows.Forms.Panel drawWin;
		protected internal System.Drawing.Point drawOffset
		{
			get
			{
				return drawOffset_Renamed;
			}
			
			set
			{
				drawOffset_Renamed = value;
			}
			
		}
		protected internal System.Drawing.Point drawOffset_Renamed = new System.Drawing.Point(0, 0);
		protected internal System.Drawing.Rectangle boundingBox
		{
			get
			{
				return boundingBox_Renamed;
			}
			
			set
			{
				boundingBox_Renamed = value;
			}
			
		}
		protected internal System.Drawing.Rectangle boundingBox_Renamed;
		protected internal int currentPieceOffsetX;
		protected internal int currentPieceOffsetY;
		protected internal int originalPieceOffsetX;
		protected internal int originalPieceOffsetY;
		protected internal System.Drawing.Point lastDragLocation
		{
			get
			{
				return lastDragLocation_Renamed;
			}
			
			set
			{
				lastDragLocation_Renamed = value;
			}
			
		}
		protected internal System.Drawing.Point lastDragLocation_Renamed = new System.Drawing.Point(0, 0);
		
		public View(Board b, SetupStack s)
		{
			myBoard = b;
			myGrid = b.getGrid();
			myStack = s;
			slot = myStack.getTopPiece();
			if (slot != null)
			{
				myPiece = slot.Piece;
			}
			this.AllowDrop = true;
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.dragEnter_renamed);
			this.DragOver += new System.Windows.Forms.DragEventHandler(this.dragOver_renamed);
			this.DragLeave += new System.EventHandler(this.dragExit_renamed);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.drop_renamed);
			//UPGRADE_ISSUE: Constructor 'java.awt.dnd.DropTarget.DropTarget' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDropTarget'"
			new DropTarget(this, (int) System.Windows.Forms.DragDropEffects.Move, this);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dragGestureRecognized);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.dragEnter);
			this.DragOver += new System.Windows.Forms.DragEventHandler(this.dragOver);
			this.DragLeave += new System.EventHandler(this.dragExit);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.dragDropEnd);
			setFocusTraversalKeysEnabled(false);
		}
		
		protected override void  OnPaint(System.Windows.Forms.PaintEventArgs g_EventArg)
		{
			System.Drawing.Graphics g = null;
			if (g_EventArg != null)
				g = g_EventArg.Graphics;
			myBoard.draw(g, 0, 0, 1.0, this);
			System.Drawing.Rectangle bounds = new Rectangle(new System.Drawing.Point(0, 0), myBoard.bounds().getSize());
			if (myGrid != null)
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				myGrid.draw(g, ref bounds, ref bounds, 1.0, false);
			}
			int x = myStack.pos.X;
			int y = myStack.pos.Y;
			myStack.stackConfigurer.drawImage(g, x, y, this, 1.0);
		}
		
		//UPGRADE_WARNING: The equivalent to the 'javax.swing.JComponent.update' method has fewer parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1265'"
		//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
		new virtual public void  Update()
		{
			// To avoid flicker, don't clear the display first *
			OnPaint(new System.Windows.Forms.PaintEventArgs(g, Bounds));
		}
		
		//UPGRADE_NOTE: The equivalent of method 'javax.swing.JComponent.getPreferredSize' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public System.Drawing.Size getPreferredSize()
		{
			return new Dimension(myBoard.bounds().width, myBoard.bounds().height);
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual void  center(ref System.Drawing.Point p)
		{
			//UPGRADE_TODO: Method 'javax.swing.JComponent.getVisibleRect' was converted to 'System.Windows.Forms.Control.DisplayRectangle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentgetVisibleRect'"
			System.Drawing.Rectangle r = this.DisplayRectangle;
			if (r.Width == 0)
			{
				r.Width = DEFAULT_SIZE.width;
				r.Height = DEFAULT_SIZE.height;
			}
			int x = p.X - r.Width / 2;
			int y = p.Y - r.Height / 2;
			if (x < 0)
				x = 0;
			if (y < 0)
				y = 0;
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.scrollRectToVisible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentscrollRectToVisible_javaawtRectangle'"
			scrollRectToVisible(new System.Drawing.Rectangle(x, y, r.Width, r.Height));
		}
		
		public virtual void  dragEnter_renamed(System.Object event_sender, System.Windows.Forms.DragEventArgs arg0)
		{
			return ;
		}
		
		public virtual void  dragOver_renamed(System.Object event_sender, System.Windows.Forms.DragEventArgs e)
		{
			System.Drawing.Point tempAux = SupportClass.GetLocation(e);
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			scrollAtEdge(ref tempAux, 15);
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual void  scrollAtEdge(ref System.Drawing.Point evtPt, int dist)
		{
			//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.Windows.Forms.ScrollableControl scroll = myStack.stackConfigurer.getScroll();
			
			//UPGRADE_ISSUE: Method 'javax.swing.JViewport.getViewPosition' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJViewport'"
			//UPGRADE_ISSUE: Method 'javax.swing.JScrollPane.getViewport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanegetViewport'"
			System.Drawing.Point p = new System.Drawing.Point(evtPt.X - scroll.getViewport().getViewPosition().X, evtPt.Y - scroll.getViewport().getViewPosition().Y);
			int dx = 0, dy = 0;
			if (p.X < dist && p.X >= 0)
				dx = - 1;
			//UPGRADE_ISSUE: Method 'javax.swing.JScrollPane.getViewport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanegetViewport'"
			if (p.X >= scroll.getViewport().Size.Width - dist && p.X < scroll.getViewport().Size.Width)
				dx = 1;
			if (p.Y < dist && p.Y >= 0)
				dy = - 1;
			//UPGRADE_ISSUE: Method 'javax.swing.JScrollPane.getViewport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanegetViewport'"
			if (p.Y >= scroll.getViewport().Size.Height - dist && p.Y < scroll.getViewport().Size.Height)
				dy = 1;
			
			if (dx != 0 || dy != 0)
			{
				System.Drawing.Rectangle temp_Rectangle;
				//UPGRADE_ISSUE: Method 'javax.swing.JViewport.getViewRect' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJViewport'"
				//UPGRADE_ISSUE: Method 'javax.swing.JScrollPane.getViewport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanegetViewport'"
				temp_Rectangle = scroll.getViewport().getViewRect();
				System.Drawing.Rectangle r = new System.Drawing.Rectangle(temp_Rectangle.Location, temp_Rectangle.Size);
				r.Offset(2 * dist * dx, 2 * dist * dy);
				//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Rectangle.intersection' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				r = System.Drawing.Rectangle.Intersect(r, new System.Drawing.Rectangle(new System.Drawing.Point(0, 0), getPreferredSize()));
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.scrollRectToVisible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentscrollRectToVisible_javaawtRectangle'"
				scrollRectToVisible(r);
			}
		}
		
		public virtual void  dropActionChanged(System.Object event_sender, System.Windows.Forms.DragEventArgs arg0)
		{
			return ;
		}
		
		public virtual void  drop_renamed(System.Object event_sender, System.Windows.Forms.DragEventArgs event_Renamed)
		{
			removeDragCursor();
			System.Drawing.Point pos = SupportClass.GetLocation(event_Renamed);
			pos.Offset(currentPieceOffsetX, currentPieceOffsetY);
			myStack.pos.X = pos.X;
			myStack.pos.Y = pos.Y;
			myStack.stackConfigurer.updateDisplay();
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			Refresh();
			return ;
		}
		
		//UPGRADE_ISSUE: Class 'java.awt.dnd.DropTargetEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDropTargetEvent'"
		public virtual void  dragExit_renamed(System.Object event_sender, System.EventArgs arg0)
		{
			return ;
		}
		
		public virtual void  dragEnter(System.Object event_sender, System.Windows.Forms.DragEventArgs arg0)
		{
			return ;
		}
		
		public virtual void  dragOver(System.Object event_sender, System.Windows.Forms.DragEventArgs arg0)
		{
			return ;
		}
		
		public virtual void  dropActionChanged(System.Object event_sender, System.Windows.Forms.DragEventArgs arg0)
		{
			return ;
		}
		
		public virtual void  dragDropEnd(System.Object event_sender, System.Windows.Forms.DragEventArgs arg0)
		{
			removeDragCursor();
			return ;
		}
		
		public virtual void  dragExit(System.Object event_sender, System.EventArgs arg0)
		{
			return ;
		}
		
		public virtual void  dragGestureRecognized(System.Object event_sender, System.Windows.Forms.MouseEventArgs dge)
		{
			
			System.Drawing.Point mousePosition = new System.Drawing.Point(dge.X, dge.Y);
			System.Drawing.Point piecePosition = new System.Drawing.Point(new System.Drawing.Size(myStack.pos));
			
			// Check drag starts inside piece
			System.Drawing.Rectangle r = myStack.stackConfigurer.PieceBoundingBox;
			r.Offset(piecePosition.X, piecePosition.Y);
			if (!r.Contains(mousePosition))
			{
				return ;
			}
			
			originalPieceOffsetX = piecePosition.X - mousePosition.X;
			originalPieceOffsetY = piecePosition.Y - mousePosition.Y;
			
			drawWin = null;
			
			makeDragCursor();
			setDragCursor();
			
			SupportClass.SwingUtilsSupport.PointToScreen(ref mousePosition, drawWin);
			moveDragCursor(mousePosition.X, mousePosition.Y);
			
			// begin dragging
			try
			{
				((System.Windows.Forms.Control) event_sender).DoDragDrop(new System.Windows.Forms.DataObject(""), System.Windows.Forms.DragDropEffects.All); // DEBUG
				//UPGRADE_ISSUE: Method 'java.awt.dnd.DragGestureEvent.getDragSource' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDragGestureEventgetDragSource'"
				dge.getDragSource().addDragSourceMotionListener(this);
			}
			catch (System.Exception e)
			{
				ErrorDialog.bug(e);
			}
		}
		
		protected internal virtual void  setDragCursor()
		{
			//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getRootPane' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetRootPane_javaawtComponent'"
			System.Windows.Forms.ContainerControl rootWin = (System.Windows.Forms.ContainerControl) this.Parent;
			if (rootWin != null)
			{
				// remove cursor from old window
				if (dragCursor.Parent != null)
				{
					dragCursor.Parent.Controls.Remove(dragCursor);
				}
				//UPGRADE_ISSUE: Method 'javax.swing.JRootPane.getLayeredPane' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJRootPanegetLayeredPane'"
				drawWin = rootWin.getLayeredPane();
				
				calcDrawOffset();
				dragCursor.Visible = true;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				drawWin.Controls.Add(dragCursor);
				dragCursor.Dock = new System.Windows.Forms.DockStyle();
				dragCursor.BringToFront();
			}
		}
		
		/// <summary>Moves the drag cursor on the current draw window </summary>
		protected internal virtual void  moveDragCursor(int dragX, int dragY)
		{
			if (drawWin != null)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setLocation' was converted to 'System.Windows.Forms.Control.Location' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetLocation_int_int'"
				dragCursor.Location = new System.Drawing.Point(dragX - drawOffset.X, dragY - drawOffset.Y);
			}
		}
		
		private void  removeDragCursor()
		{
			if (drawWin != null)
			{
				if (dragCursor != null)
				{
					dragCursor.Visible = false;
					drawWin.Controls.Remove(dragCursor);
				}
				drawWin = null;
			}
		}
		
		/// <summary>calculates the offset between cursor dragCursor positions </summary>
		private void  calcDrawOffset()
		{
			if (drawWin != null)
			{
				// drawOffset is the offset between the mouse location during a drag
				// and the upper-left corner of the cursor
				// accounts for difference betwen event point (screen coords)
				// and Layered Pane position, boundingBox and off-center drag
				drawOffset.X = - boundingBox.X - currentPieceOffsetX + EXTRA_BORDER;
				drawOffset.Y = - boundingBox.Y - currentPieceOffsetY + EXTRA_BORDER;
				SupportClass.SwingUtilsSupport.PointToScreen(ref drawOffset, drawWin);
			}
		}
		
		private System.Drawing.Bitmap featherDragImage(System.Drawing.Bitmap src, int w, int h, int b)
		{
			// FIXME: duplicated from PieceMover!
			//UPGRADE_NOTE: Final was removed from the declaration of 'dst '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Bitmap dst = ImageUtils.createCompatibleTranslucentImage(w, h);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dst);
			//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setRenderingHint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetRenderingHint_javaawtRenderingHintsKey_javalangObject'"
			//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_ANTIALIASING' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_ANTIALIASING_f'"
			//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.VALUE_ANTIALIAS_ON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsVALUE_ANTIALIAS_ON_f'"
			g.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
			
			// paint the rectangle occupied by the piece at specified alpha
			SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.FromArgb(CURSOR_ALPHA, 0xff, 0xff, 0xff));
			g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), 0, 0, w, h);
			
			// feather outwards
			for (int f = 0; f < b; ++f)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'alpha '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int alpha = CURSOR_ALPHA * (f + 1) / b;
				SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.FromArgb(alpha, 0xff, 0xff, 0xff));
				g.DrawRectangle(SupportClass.GraphicsManager.manager.GetPen(g), f, f, w - 2 * f, h - 2 * f);
			}
			
			// paint in the source image
			//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setComposite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetComposite_javaawtComposite'"
			//UPGRADE_ISSUE: Method 'java.awt.AlphaComposite.getInstance' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAlphaComposite'"
			//UPGRADE_ISSUE: Field 'java.awt.AlphaComposite.SRC_IN' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAlphaComposite'"
			g.setComposite(AlphaComposite.getInstance(AlphaComposite.SRC_IN));
			//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
			g.DrawImage(src, 0, 0);
			g.Dispose();
			
			return dst;
		}
		
		private void  makeDragCursor()
		{
			//double zoom = 1.0;
			// create the cursor if necessary
			if (dragCursor == null)
			{
				dragCursor = new System.Windows.Forms.Label();
				dragCursor.Visible = false;
			}
			
			//dragCursorZoom = zoom;
			currentPieceOffsetX = originalPieceOffsetX;
			currentPieceOffsetY = originalPieceOffsetY;
			
			// Record sizing info and resize our cursor
			boundingBox = myStack.stackConfigurer.PieceBoundingBox;
			calcDrawOffset();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int w = boundingBox.Width + EXTRA_BORDER * 2;
			//UPGRADE_NOTE: Final was removed from the declaration of 'h '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int h = boundingBox.Height + EXTRA_BORDER * 2;
			
			System.Drawing.Bitmap cursorImage = ImageUtils.createCompatibleTranslucentImage(w, h);
			//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(cursorImage);
			
			myStack.stackConfigurer.drawImage(g, EXTRA_BORDER - boundingBox.X, EXTRA_BORDER - boundingBox.Y, dragCursor, 1.0);
			
			g.Dispose();
			
			//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
			dragCursor.Size = new System.Drawing.Size(w, h);
			
			cursorImage = featherDragImage(cursorImage, w, h, EXTRA_BORDER);
			
			// store the bitmap in the cursor
			dragCursor.Image = (System.Drawing.Image) cursorImage.Clone();
		}
		
		public virtual void  dragMouseMoved(System.Object event_sender, System.Windows.Forms.DragEventArgs event_Renamed)
		{
			if (!event_Renamed.getLocation().equals(lastDragLocation))
			{
				lastDragLocation = event_Renamed.getLocation();
				moveDragCursor(event_Renamed.getX(), event_Renamed.getY());
				//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
				if (dragCursor != null && !dragCursor.Visible)
				{
					dragCursor.Visible = true;
				}
			}
		}
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}