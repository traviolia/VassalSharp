/*
* $Id$
*
* Copyright (c) 2008-2009 Brent Easton
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
using GameModule = VassalSharp.build.GameModule;
using Map = VassalSharp.build.module.Map;
using PlayerRoster = VassalSharp.build.module.PlayerRoster;
using PrivateMap = VassalSharp.build.module.PrivateMap;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using WarningDialog = VassalSharp.tools.WarningDialog;
using EvalError = bsh.EvalError;
using Interpreter = bsh.Interpreter;
using NameSpace = bsh.NameSpace;
using UtilEvalError = bsh.UtilEvalError;
namespace VassalSharp.script
{
	
	[Serializable]
	public abstract class AbstractInterpreter:Interpreter
	{
		private void  InitBlock()
		{
			try
			{
				NameSpace.setTypedVariable(name, cl, value_Renamed, null);
			}
			catch (UtilEvalError e)
			{
				// FIXME: Error message
				WarningDialog.show(e, "");
			}
			if (m.getMapName().equals(mapName) && isAccessible(m))
			{
				map = m;
				break;
			}
		}
		
		protected internal const System.String THIS = "_interp";
		protected internal const System.String SOURCE = "_source";
		private const long serialVersionUID = 1L;
		
		protected internal NameSpace myNameSpace;
		
		public AbstractInterpreter():base()
		{
			InitBlock();
		}
		
		/// <summary> Set a variable and handle exceptions
		/// 
		/// </summary>
		/// <param name="name">Variable name
		/// </param>
		/// <param name="value">Variable value
		/// </param>
		protected internal virtual void  setVar(System.String name, System.Object value_Renamed)
		{
			try
			{
				set_Renamed(name, value_Renamed);
			}
			catch (EvalError e)
			{
				// FIXME: Error message
				WarningDialog.show(e, "");
			}
		}
		
		
		protected internal virtual void  setVar(System.String name, int value_Renamed)
		{
			try
			{
				set_Renamed(name, value_Renamed);
			}
			catch (EvalError e)
			{
				// FIXME: Error message
				WarningDialog.show(e, "");
			}
		}
		
		protected internal virtual void  setVar(System.String name, float value_Renamed)
		{
			try
			{
				set_Renamed(name, value_Renamed);
			}
			catch (EvalError e)
			{
				// FIXME: Error message
				WarningDialog.show(e, "");
			}
		}
		
		protected internal virtual void  setVar(System.String name, bool value_Renamed)
		{
			try
			{
				set_Renamed(name, value_Renamed);
			}
			catch (EvalError e)
			{
				// FIXME: Error message
				WarningDialog.show(e, "");
			}
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void setVar(String name, Class < ? > cl, Object value)
		
		/// <summary>************************************************************
		/// Call-backs from BeanShell Scripts
		/// </summary>
		
		/// <summary> Alert(message) Display a message in a dialog box/
		/// 
		/// </summary>
		/// <param name="message">
		/// </param>
		public virtual System.Object alert(System.String message)
		{
			SupportClass.OptionPaneSupport.ShowMessageDialog(null, message);
			return "";
		}
		
		/// <summary> Get a module level property value.
		/// 
		/// </summary>
		/// <param name="name">Property Name
		/// </param>
		/// <returns> Property value
		/// </returns>
		public virtual System.Object getModuleProperty(System.String name)
		{
			return BeanShell.wrap(GameModule.getGameModule().getProperty(name).toString());
		}
		
		/// <summary> Set the value of a module level property
		/// 
		/// </summary>
		/// <param name="name">Property name
		/// </param>
		/// <param name="value">new value
		/// </param>
		public virtual void  setModuleProperty(System.String name, System.String value_Renamed)
		{
			GameModule.getGameModule().getMutableProperty(name).setPropertyValue(value_Renamed);
		}
		
		/// <summary> Return a proxy reference to the named map as long as it is accessible to
		/// us.
		/// 
		/// </summary>
		/// <param name="mapName">Map Name
		/// </param>
		/// <returns> Map proxy
		/// </returns>
		public virtual VassalSharp.script.proxy.Map findMap(System.String mapName)
		{
			Map map = null;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Map m: GameModule.getGameModule().getAllDescendantComponentsOf(
			Map.
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class))
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		return map == null ? null: new VassalSharp.script.proxy.Map(map);
		protected AbstractInterpreter(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context):base(info, context)
		{
		}
		public override void  GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
	
	/// <summary> Is a map accessible to us?
	/// 
	/// </summary>
	/// <param name="m">Map
	/// </param>
	/// <returns> true if accessible
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected boolean isAccessible(Map m)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(m instanceof PrivateMap)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		String mySide = PlayerRoster.getMySide();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(mySide == null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return true;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return((PrivateMap) m).isAccessibleTo(mySide);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return true;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Fire off a Global Hot Key
	/// 
	/// </summary>
	/// <param name="stroke">Keystroke
	/// </param>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void globalHotKey(KeyStroke stroke)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		GameModule.getGameModule().fireKeyStroke(new NamedKeyStroke(stroke));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}