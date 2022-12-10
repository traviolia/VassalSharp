/*
* $Id$
*
* Copyright (c) 2007 Joel Uckelman
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
//UPGRADE_TODO: The type 'java.net.URI' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using URI = java.net.URI;
//UPGRADE_TODO: The type 'java.net.URISyntaxException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using URISyntaxException = java.net.URISyntaxException;
using DieRoll = VassalSharp.build.module.DieRoll;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using FormattedString = VassalSharp.tools.FormattedString;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.build.module.dice
{
	
	public class BonesDiceServer:DieServer
	{
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final Iterator < String > line = results.iterator();
			
			for (int i = 0; i < rollSet.dieRolls.length; ++i)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SupportClass.Tokenizer st = new StringTokenizer(line.next(), " ");
				
				for (int j = 0; j < rollSet.dieRolls[i].getNumDice(); ++j)
				{
					rollSet.dieRolls[i].setResult(j, System.Int32.Parse(st.NextToken()));
				}
			}
		}
		
		public BonesDiceServer()
		{
			InitBlock();
			name = "Bones";
			description = "Bones Dice Server";
			emailOnly = false;
			maxRolls = 0;
			maxEmails = 0;
			serverURL = "http://dice.nomic.net/cgi-bin/randroll.pl";
			passwdRequired = false;
			canDoSeparateDice = true;
		}
		
		public override System.String[] buildInternetRollString(RollSet toss)
		{
			DieRoll[] rolls = toss.DieRolls;
			StringBuilder query = new StringBuilder("req=");
			
			// format is "{{ xdy + n }}"
			for (int i = 0; i < rolls.Length; ++i)
			{
				query.append("{{").append(rolls[i].NumDice).append("D").append(rolls[i].NumSides);
				
				if (rolls[i].Plus != 0)
				{
					query.append("+").append(rolls[i].Plus);
				}
				
				query.append("}}\n");
			}
			
			try
			{
				return new System.String[]{new URI("http", "dice.nomic.net", "/cgi-bin/randroll.pl", query.toString(), null).toURL().toString()};
				
				//      return new String[] {  URLEncoder.encode(query.toString(), "UTF-8") };
			}
			catch (System.UriFormatException e)
			{
				// should never happen
				ErrorDialog.bug(e);
			}
			catch (URISyntaxException e)
			{
				// should never happen
				ErrorDialog.bug(e);
			}
			
			return null;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void parseInternetRollString(RollSet rollSet, Vector < String > results)
		
		public override void  roll(RollSet mr, FormattedString format)
		{
			base.doInternetRoll(mr, format);
		}
		
		public virtual void  doIRoll(RollSet toss)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'rollString '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String[] rollString = buildInternetRollString(toss);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final Vector < String > returnString = new Vector < String >();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'url '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'java.net.URL' was converted to a 'System.Uri' which does not throw an exception if a URL specifies an unknown protocol. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1132'"
			System.Uri url = new System.Uri(rollString[0]);
			//UPGRADE_NOTE: Final was removed from the declaration of 'connection '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Net.HttpWebRequest connection = (System.Net.HttpWebRequest) System.Net.WebRequest.Create(url);
			connection.Method = "GET";
			//UPGRADE_ISSUE: Method 'java.net.URLConnection.connect' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetURLConnectionconnect'"
			connection.connect();
			
			System.IO.StreamReader in_Renamed = null;
			try
			{
				//UPGRADE_TODO: The differences in the expected value  of parameters for constructor 'java.io.BufferedReader.BufferedReader'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
				//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
				in_Renamed = new System.IO.StreamReader(new System.IO.StreamReader(connection.GetResponse().GetResponseStream(), System.Text.Encoding.Default).BaseStream, new System.IO.StreamReader(connection.GetResponse().GetResponseStream(), System.Text.Encoding.Default).CurrentEncoding);
				
				System.String line;
				while ((line = in_Renamed.ReadLine()) != null)
					returnString.add(line);
				
				in_Renamed.Close();
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
			
			parseInternetRollString(toss, returnString);
		}
	}
}