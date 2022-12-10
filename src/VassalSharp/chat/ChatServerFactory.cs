/*
* $Id$
*
* Copyright (c) 2000-2006 by Rodney Kinney
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
namespace VassalSharp.chat
{
	
	/// <summary> A Factory for building {@link ChatServerConnection} instances
	/// 
	/// </summary>
	/// <author>  rkinney
	/// </author>
	public abstract class ChatServerFactory
	{
		public const System.String TYPE_KEY = "type"; //$NON-NLS-1$
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private static Map < String, ChatServerFactory > factories = 
		new HashMap < String, ChatServerFactory >();
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public abstract ChatServerConnection buildServer(System.Collections.Specialized.NameValueCollection param);
		
		public static void  register(System.String key, ChatServerFactory instance)
		{
			factories.put(key, instance);
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public static ChatServerConnection build(System.Collections.Specialized.NameValueCollection param)
		{
			System.String type = param.Get(TYPE_KEY);
			ChatServerFactory factory = factories.get_Renamed(type);
			return factory.buildServer(param);
		}
	}
}