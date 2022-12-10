/*
*
* Copyright (c) 2000-2007 by Rodney Kinney
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
	
	/// <author>  rkinney
	/// </author>
	public class DynamicClientFactory:ChatServerFactory
	{
		public const System.String DYNAMIC_TYPE = "dynamic"; //$NON-NLS-1$
		public const System.String URL = "url"; //$NON-NLS-1$
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public override ChatServerConnection buildServer(System.Collections.Specialized.NameValueCollection param)
		{
			System.String url = param.Get(URL);
			DynamicClient client = url == null?new DynamicClient():new DynamicClient(url);
			if (SupportClass.ContainsKeySupport(param, DYNAMIC_TYPE))
			{
				//param = new Properties(param);
				// Force the type of the dynamically created server proxy
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				param[ChatServerFactory.TYPE_KEY] = param.Get(DYNAMIC_TYPE);
			}
			client.setOverrides(param);
			return client;
		}
	}
}