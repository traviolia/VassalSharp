using System;
using DieRoll = VassalSharp.build.module.DieRoll;
using FormattedString = VassalSharp.tools.FormattedString;
namespace VassalSharp.build.module.dice
{
	
	/// <summary> 
	/// ShadowDice Dice Server
	/// 
	/// </summary>
	public class ShadowDiceDieServer:DieServer
	{
		private void  InitBlock()
		{
			
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			
			// Initialise and search for start line
			System.String line = e.nextElement();
			while (e.hasMoreElements() && !line.StartsWith("! " + ROLL_MARKER))
				line = e.nextElement();
			
			// Skip description line
			line = e.nextElement();
			
			// And process the results, 1 per roll in the multiroll
			DieRoll[] rolls = rollSet.getDieRolls();
			for (int i = 0; i < rolls.Length; i++)
			{
				
				line = e.nextElement();
				
				int firsthash = line.IndexOf('#') - 1;
				SupportClass.Tokenizer st = new SupportClass.Tokenizer(line.Substring(firsthash), " ");
				
				for (int j = 0; j < rollSet.dieRolls[i].getNumDice(); j++)
				{
					st.NextToken();
					System.String result = st.NextToken();
					int res = System.Int32.Parse(result);
					rollSet.dieRolls[i].setResult(j, res);
				}
			}
		}
		public const System.String ROLL_MARKER = "VASSAL auto-generated dice roll";
		
		public ShadowDiceDieServer()
		{
			InitBlock();
			
			name = "ShadowDice";
			description = "ShadowDice Dice Server";
			emailOnly = false;
			maxRolls = 0;
			maxEmails = 0;
			serverURL = "http://www.gamerz.net/shadowdice/shadowdice.cgi";
			passwdRequired = false;
			canDoSeparateDice = true;
		}
		
		public override System.String[] buildInternetRollString(RollSet toss)
		{
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'CRLF '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String CRLF = "%0D%0A"; // CRLF
			//UPGRADE_NOTE: Final was removed from the declaration of 'LSQUARE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String LSQUARE = "%5B"; // '['
			//UPGRADE_NOTE: Final was removed from the declaration of 'RSQUARE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String RSQUARE = "%5D"; // ']'
			//UPGRADE_NOTE: Final was removed from the declaration of 'HASH '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String HASH = "%23";
			//      final String PLUS = "%2B";
			
			System.String desc, s, pEmail = "", sEmail = "";
			
			if (UseEmail)
			{
				pEmail = extractEmail(PrimaryEmail);
				sEmail = extractEmail(SecondaryEmail);
			}
			
			desc = hexify(toss.description);
			
			s = "mto=" + pEmail + "&mcc=" + sEmail + "&yem=" + pEmail;
			s += ("&sbj=" + desc);
			s += ("&msg=" + ROLL_MARKER + CRLF + desc + CRLF);
			
			int mLen = toss.MaxDescLength;
			
			DieRoll[] rolls = toss.DieRolls;
			for (int i = 0; i < rolls.Length; i++)
			{
				s += hexify(rolls[i].Description);
				for (int j = 0; j < mLen - rolls[i].Description.Length; j++)
				{
					s += ' ';
				}
				s += (' ' + HASH);
				int nd = rolls[i].NumDice;
				int ns = rolls[i].NumSides;
				//        int p = rolls[i].getPlus();
				for (int j = 0; j < nd; j++)
				{
					s += (LSQUARE + "1d" + ns + RSQUARE);
				}
				s += CRLF;
			}
			
			s += "&todo=Action%21&hid=1";
			s = s.Replace(' ', '+'); // No spaces allowed, use '+' instead.
			
			return new System.String[]{s};
		}
		
		/*
		* The Irony server requires most of the non-alphanumerics to be
		* converted to a hex escape code %nn. '*-_.' excepted.
		* '#' characters interfere with the output parsing and are stripped out.
		*/
		public virtual System.String hexify(System.String s)
		{
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'hexyChars '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String hexyChars = "~!$%^&()+`={}[]|:;'<>,?/\\\"";
			//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder b = new StringBuilder();
			
			for (int i = 0; i < s.Length; i++)
			{
				char c = s[i];
				
				if (c == '#')
				{
					b.append('.');
				}
				else if (hexyChars.IndexOf((System.Char) c) >= 0)
				{
					b.append("%" + System.Convert.ToString(c, 16).ToUpper());
				}
				else
				{
					b.append(c);
				}
			}
			return b.toString();
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void parseInternetRollString(RollSet rollSet, Vector < String > results)
		
		public override void  roll(RollSet mr, FormattedString format)
		{
			base.doInternetRoll(mr, format);
		}
	}
}