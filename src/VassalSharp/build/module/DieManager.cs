/*
* $Id$
*
* Copyright (c) 2000-2003 by Brent Easton
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
using AbstractConfigurable = VassalSharp.build.AbstractConfigurable;
using Buildable = VassalSharp.build.Buildable;
using GameModule = VassalSharp.build.GameModule;
using BonesDiceServer = VassalSharp.build.module.dice.BonesDiceServer;
using DieServer = VassalSharp.build.module.dice.DieServer;
using RollSet = VassalSharp.build.module.dice.RollSet;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Command = VassalSharp.command.Command;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using StringArrayConfigurer = VassalSharp.configure.StringArrayConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using StringEnumConfigurer = VassalSharp.configure.StringEnumConfigurer;
using Resources = VassalSharp.i18n.Resources;
using Prefs = VassalSharp.preferences.Prefs;
using FormattedString = VassalSharp.tools.FormattedString;
namespace VassalSharp.build.module
{
	
	/// <author>  Brent Easton
	/// 
	/// Internet Die Roller Manager. Includes all the smarts to interface to web-based
	/// Die Servers
	/// </author>
	
	public class DieManager:AbstractConfigurable
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassListDataListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_TODO: Interface 'javax.swing.event.ListDataListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		private class AnonymousClassListDataListener : ListDataListener
		{
			public AnonymousClassListDataListener(DieManager enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(DieManager enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private DieManager enclosingInstance;
			public DieManager Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			//UPGRADE_ISSUE: Class 'javax.swing.event.ListDataEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventListDataEvent'"
			public virtual void  contentsChanged(System.Object event_sender, ListDataEvent arg0)
			{
				Enclosing_Instance.setSemailValues();
			}
			
			//UPGRADE_ISSUE: Class 'javax.swing.event.ListDataEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventListDataEvent'"
			public virtual void  intervalAdded(System.Object event_sender, ListDataEvent arg0)
			{
				Enclosing_Instance.setSemailValues();
			}
			
			//UPGRADE_ISSUE: Class 'javax.swing.event.ListDataEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventListDataEvent'"
			public virtual void  intervalRemoved(System.Object event_sender, ListDataEvent arg0)
			{
				Enclosing_Instance.setSemailValues();
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
		virtual public System.String[] Names
		{
			// Return names of all known Dice Servers
			
			get
			{
				// FIXME: better to return zero-length array
				if (servers == null)
				{
					return null;
				}
				else
				{
					return servers.keySet().toArray(new System.String[servers.size()]);
				}
			}
			
		}
		virtual public System.String[] Descriptions
		{
			// Return descriptions of all known dice servers
			
			get
			{
				// FIXME: better to return zero-length array
				if (servers == null)
				{
					return null;
				}
				else
				{
					System.String[] s = new System.String[servers.size()];
					int i = 0;
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(DieServer d: servers.values())
					{
						s[i++] = d.getDescription();
					}
					return s;
				}
			}
			
		}
		virtual public DieServer Server
		{
			get
			{
				getPrefs();
				return server;
			}
			
		}
		virtual public System.String ServerDescription
		{
			get
			{
				return Server.Description;
			}
			
		}
		virtual public System.String ServerName
		{
			get
			{
				return Server.Name;
			}
			
		}
		virtual public int DfltNDice
		{
			get
			{
				return defaultNDice;
			}
			
		}
		virtual public int DfltNSides
		{
			get
			{
				return defaultNSides;
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[]{Resources.getString("Editor.DieManager.description"), Resources.getString("Editor.DieManager.ndice"), Resources.getString("Editor.DieManager.nsides")};
			}
			
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private Map < String, DieServer > servers;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < InternetDiceButton > dieButtons = 
		new ArrayList < InternetDiceButton >();
		private System.String desc = "Die Manager";
		private bool useMultiRoll;
		private int defaultNDice = 2;
		private int defaultNSides = 6;
		
		private DieServer server;
		private System.String lastServerName = "";
		private MultiRoll myMultiRoll;
		//UPGRADE_NOTE: Final was removed from the declaration of 'semail '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		internal StringEnumConfigurer semail;
		
		public const System.String USE_INTERNET_DICE = "useinternetdice";
		public const System.String DICE_SERVER = "diceserver";
		public const System.String SERVER_PW = "serverpw";
		public const System.String USE_EMAIL = "useemail";
		public const System.String PRIMARY_EMAIL = "primaryemail";
		public const System.String SECONDARY_EMAIL = "secondaryemail";
		public const System.String ADDRESS_BOOK = "addressbook";
		public const System.String MULTI_ROLL = "multiroll";
		public const System.String DIE_MANAGER = "Internet Die Roller";
		
		public const System.String DESC = "description";
		public const System.String DFLT_NSIDES = "dfltnsides";
		public const System.String DFLT_NDICE = "dfltndice";
		
		public DieManager()
		{
			InitBlock();
			
			DieServer d;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			servers = new HashMap < String, DieServer >();
			
			/*
			* Create the Internet Dice Servers we know about
			*/
			//    d = new InbuiltDieServer();
			//    servers.put(d.getName(), d);
			//    server = d; // Set the default Internet Server
			
			//        d = new IronyDieServer();
			//        servers.put(d.getName(), d);
			//
			//        d = new InternetGamesDieServer();
			//        servers.put(d.getName(), d);
			
			//    d = new ShadowDiceDieServer();
			//    servers.put(d.getName(), d);
			
			d = new BonesDiceServer();
			servers.put(d.Name, d);
			
			server = d;
			
			/*
			* The Dice Manager needs some preferences
			*/
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'dieserver '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringEnumConfigurer dieserver = new StringEnumConfigurer(DICE_SERVER, "Internet Dice Server", Descriptions);
			dieserver.setValue(server.Description);
			//UPGRADE_NOTE: Final was removed from the declaration of 'serverpw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringConfigurer serverpw = new StringConfigurer(SERVER_PW, "Dice Server Password");
			//UPGRADE_NOTE: Final was removed from the declaration of 'useemail '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			BooleanConfigurer useemail = new BooleanConfigurer(USE_EMAIL, "Email results?");
			//UPGRADE_NOTE: Final was removed from the declaration of 'pemail '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringConfigurer pemail = new StringConfigurer(PRIMARY_EMAIL, "Primary Email");
			//UPGRADE_NOTE: Final was removed from the declaration of 'abook '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringArrayConfigurer abook = new StringArrayConfigurer(ADDRESS_BOOK, "Address Book");
			//UPGRADE_NOTE: Final was removed from the declaration of 'multiroll '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			BooleanConfigurer multiroll = new BooleanConfigurer(MULTI_ROLL, "Put multiple rolls into single email");
			
			GameModule.getGameModule().getPrefs().addOption(null, dieserver);
			GameModule.getGameModule().getPrefs().addOption(null, serverpw);
			GameModule.getGameModule().getPrefs().addOption(DIE_MANAGER, useemail);
			
			GameModule.getGameModule().getPrefs().addOption(DIE_MANAGER, abook);
			System.String[] addressList = (System.String[]) GameModule.getGameModule().getPrefs().getValue(ADDRESS_BOOK);
			semail = new StringEnumConfigurer(SECONDARY_EMAIL, "Secondary Email", addressList);
			
			GameModule.getGameModule().getPrefs().addOption(DIE_MANAGER, pemail);
			GameModule.getGameModule().getPrefs().addOption(DIE_MANAGER, semail);
			GameModule.getGameModule().getPrefs().addOption(DIE_MANAGER, multiroll);
			
			setSemailValues();
			
			//UPGRADE_TODO: Class 'javax.swing.DefaultListModel' was converted to 'System.Windows.Forms.ListBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingDefaultListModel'"
			System.Windows.Forms.ListBox.ObjectCollection m = abook.Model;
			//UPGRADE_TODO: Interface 'javax.swing.event.ListDataListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			ListDataListener ldl = new AnonymousClassListDataListener(this);
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractListModel.addListDataListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractListModeladdListDataListener_javaxswingeventListDataListener'"
			m.addListDataListener(ldl);
		}
		
		public virtual void  setSemailValues()
		{
			System.String currentSemail = (System.String) GameModule.getGameModule().getPrefs().getValue(SECONDARY_EMAIL);
			System.String[] addressBook = (System.String[]) GameModule.getGameModule().getPrefs().getValue(ADDRESS_BOOK);
			semail.setValidValues(addressBook);
			semail.setValue(currentSemail);
		}
		
		// Return server matching Name
		public virtual DieServer getServerForName(System.String name)
		{
			return servers.get_Renamed(name);
		}
		
		// Return server matching Description
		public virtual DieServer getServerFromDescription(System.String de)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(DieServer d: servers.values())
			{
				if (de.Equals(d.getDescription()))
				{
					return d;
				}
			}
			return null;
		}
		
		public virtual MultiRoll getMultiRoll(int nDice, int nSides)
		{
			System.String serverName = Server.Name;
			if (myMultiRoll == null || !serverName.Equals(lastServerName))
			{
				myMultiRoll = new MultiRoll(this, nDice, nSides);
			}
			lastServerName = serverName;
			
			return myMultiRoll;
		}
		
		public virtual void  roll(int nDice, int nSides, int plus, bool reportTotal, System.String description, FormattedString format)
		{
			MultiRoll mroll = getMultiRoll(nDice, nSides);
			getPrefs();
			
			RollSet rollSet;
			
			System.String desc = GameModule.getGameModule().getChatter().getInputField().getText();
			if (desc != null && desc.Length > 0)
			{
				mroll.Description = desc;
			}
			
			// Do we want full multi-roll capabilities? If required, pop-up the multi-roll
			// cofigurer to get the details
			if (useMultiRoll)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				mroll.Visible = true;
				
				if (mroll.wasCancelled())
				{
					return ;
				}
				rollSet = mroll.RollSet;
				desc = rollSet.Description;
			}
			// Multi Roll preference not selected, so build a dummy MultiRoll object
			else
			{
				DieRoll[] rolls = new DieRoll[]{new DieRoll(description, nDice, nSides, plus, reportTotal)};
				rollSet = new RollSet(description, rolls);
				desc = "";
			}
			
			Command chatCommand = new Chatter.DisplayText(GameModule.getGameModule().getChatter(), " - Roll sent to " + server.Description);
			
			if (desc == null || desc.Length == 0)
			{
				desc = GameModule.getGameModule().getChatter().getInputField().getText();
			}
			if (server.UseEmail)
			{
				if (desc == null || desc.Length == 0)
				{
					chatCommand.append(new Chatter.DisplayText(GameModule.getGameModule().getChatter(), " - Emailing " + server.SecondaryEmail + " (no subject line)"));
					chatCommand.append(new Chatter.DisplayText(GameModule.getGameModule().getChatter(), " - Leave text in the chat input area to provide a subject line"));
				}
				else
				{
					chatCommand.append(new Chatter.DisplayText(GameModule.getGameModule().getChatter(), " - Emailing " + server.SecondaryEmail + " (Subject:  " + desc + ")"));
				}
			}
			chatCommand.execute();
			GameModule.getGameModule().sendAndLog(chatCommand);
			
			GameModule.getGameModule().getChatter().getInputField().setText("");
			rollSet.Description = desc;
			
			server.roll(rollSet, format);
		}
		
		/*
		* Retrieve the Dice Manager preferences and update the current Server
		* Preferences may change at ANY time!
		*/
		private void  getPrefs()
		{
			
			Prefs prefs = GameModule.getGameModule().getPrefs();
			
			// Get the correct server
			System.String serverName = ((System.String) prefs.getValue(DICE_SERVER));
			server = getServerFromDescription(serverName);
			
			// And tell it the prefs it will need
			server.Passwd = ((System.String) prefs.getValue(SERVER_PW));
			server.UseEmail = ((System.Boolean) prefs.getValue(USE_EMAIL));
			server.PrimaryEmail = ((System.String) prefs.getValue(PRIMARY_EMAIL));
			server.SecondaryEmail = ((System.String) prefs.getValue(SECONDARY_EMAIL));
			
			useMultiRoll = ((System.Boolean) prefs.getValue(MULTI_ROLL));
		}
		
		/*
		public void addDie(SpecialDie d) {
		specialDice.add(d);
		}
		
		public void removeDie(SpecialDie d) {
		specialDice.remove(d);
		}
		*/
		
		public virtual void  addDieButton(InternetDiceButton d)
		{
			dieButtons.add(d);
		}
		
		public virtual void  removeDieButton(InternetDiceButton d)
		{
			dieButtons.remove(d);
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
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
		DESC, 
		DFLT_NDICE, 
		DFLT_NSIDES
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setAttribute(String key, Object value)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(DESC.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		desc =(String) value;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(DFLT_NDICE.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		value = Integer.valueOf((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	defaultNDice =((Integer) value).intValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(DFLT_NSIDES.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		value = Integer.valueOf((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	defaultNSides =((Integer) value).intValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String getAttributeValueString(String key)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(DESC.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return desc;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(DFLT_NDICE.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return defaultNDice + ;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(DFLT_NSIDES.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return defaultNSides + ;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else 
	return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void removeFrom(Buildable parent)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public HelpFile getHelpFile()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Class < ? > [] getAllowableConfigureComponents()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new Class < ? > []
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ InternetDiceButton.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	class
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void addTo(Buildable parent)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public static String getConfigureTypeName()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return Resources.getString(Editor.DieManager.component_type); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setSecondaryEmail(String email)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		GameModule.getGameModule().getPrefs().setValue(SECONDARY_EMAIL, email);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	server.setSecondaryEmail(email);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}