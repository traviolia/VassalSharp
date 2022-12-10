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
using GameModule = VassalSharp.build.GameModule;
using PropertySource = VassalSharp.build.module.properties.PropertySource;
using EvalError = bsh.EvalError;
using NameSpace = bsh.NameSpace;
namespace VassalSharp.script
{
	
	[Serializable]
	public class ScriptInterpreter:AbstractInterpreter
	{
		
		private const long serialVersionUID = 1L;
		
		//UPGRADE_ISSUE: Class 'java.lang.ClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
		public ScriptInterpreter(ClassLoader loader):base()
		{
			ClassLoader = loader;
			
			myNameSpace = new NameSpace(ClassManager, "script");
			
			NameSpace = myNameSpace;
			NameSpace.importClass("VassalSharp.build.module.properties.PropertySource");
			NameSpace.importClass("VassalSharp.script.ExpressionInterpreter");
			NameSpace.importClass("VassalSharp.script.ScriptInterpreter");
			
			setVar(THIS, this);
		}
		
		public virtual System.Object evaluate(System.String statement)
		{
			setVar(SOURCE, (PropertySource) GameModule.getGameModule());
			return base.eval(statement);
		}
		
		public virtual System.Object evaluate(System.String statement, PropertySource source)
		{
			setVar(SOURCE, source);
			return base.eval(statement);
		}
		protected ScriptInterpreter(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context):base(info, context)
		{
		}
		//UPGRADE_NOTE: A parameterless constructor was added for a serializable class to avoid compile errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1268'"
		public ScriptInterpreter()
		{
		}
		public override void  GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}