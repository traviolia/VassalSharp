/*
* Copyright (c) 2006 Amazon.com, Inc.
* All rights reserved.
*/
using System;
namespace VassalSharp.configure
{
	
	public class PasswordConfigurer:StringConfigurer
	{
		
		public PasswordConfigurer(System.String key, System.String name, System.String val):base(key, name, val)
		{
		}
		
		public PasswordConfigurer(System.String key, System.String name):base(key, name)
		{
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected internal override System.Windows.Forms.TextBox buildTextField()
		{
			System.Windows.Forms.TextBox temp_TextBox;
			temp_TextBox = new System.Windows.Forms.TextBox();
			temp_TextBox.PasswordChar = '*';
			return temp_TextBox;
		}
	}
}