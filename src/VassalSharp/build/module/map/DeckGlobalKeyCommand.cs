/*
* $Id$
*
* Copyright (c) 2005 by Rodney Kinney
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
using Buildable = VassalSharp.build.Buildable;
using GameModule = VassalSharp.build.GameModule;
using Chatter = VassalSharp.build.module.Chatter;
using Map = VassalSharp.build.module.Map;
using PropertySource = VassalSharp.build.module.properties.PropertySource;
using Command = VassalSharp.command.Command;
using NullCommand = VassalSharp.command.NullCommand;
using PropertyExpression = VassalSharp.configure.PropertyExpression;
using Deck = VassalSharp.counters.Deck;
using DeckVisitorDispatcher = VassalSharp.counters.DeckVisitorDispatcher;
using GlobalCommand = VassalSharp.counters.GlobalCommand;
using KeyCommand = VassalSharp.counters.KeyCommand;
using PieceFilter = VassalSharp.counters.PieceFilter;
using Resources = VassalSharp.i18n.Resources;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using Loopable = VassalSharp.tools.RecursionLimiter.Loopable;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.build.module.map
{
	
	/// <summary> This version of {@link MassKeyCommand} is added directly to a
	/// {@link VassalSharp.build.GameModule} and applies to all maps
	/// </summary>
	public class DeckGlobalKeyCommand:MassKeyCommand
	{
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
		public static System.String ConfigureTypeName
		{
			get
			{
				return Resources.getString("Editor.DeckGlobalKeyCommand.component_type"); //$NON-NLS-1$
			}
			
		}
		/// <summary> Since we also limit application of a Deck Global Key command to a specified number of pieces in the
		/// Deck, a null match expression should match all pieces, not reject them all.
		/// </summary>
		virtual public PieceFilter Filter
		{
			get
			{
				if (propertiesFilter == null || propertiesFilter.Expression == null || propertiesFilter.Expression.Length == 0)
				{
					return null;
				}
				return base.Filter;
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[]{Resources.getString(Resources.NAME_LABEL), Resources.getString("Editor.DeckGlobalKeyCommand.command"), Resources.getString("Editor.DeckGlobalKeyCommand.matching_properties"), Resources.getString("Editor.DeckGlobalKeyCommand.affects"), Resources.getString("Editor.report_format")};
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[]{NAME, KEY_COMMAND, PROPERTIES_FILTER, DECK_COUNT, REPORT_FORMAT};
			}
			
		}
		
		public DeckGlobalKeyCommand()
		{
			InitBlock();
			globalCommand = new DeckGlobalCommand(this);
			setConfigureName("");
		}
		
		public DeckGlobalKeyCommand(System.String code):this()
		{
			decode(code);
		}
		
		public DeckGlobalKeyCommand(System.String code, PropertySource source):this(code)
		{
			propertySource = source;
			globalCommand.PropertySource = source;
		}
		
		public override void  addTo(Buildable parent)
		{
			if (parent is Map)
			{
				map = (Map) parent;
			}
			if (parent is PropertySource)
			{
				propertySource = (PropertySource) parent;
			}
			((DrawPile) parent).addGlobalKeyCommand(this);
			globalCommand.PropertySource = propertySource;
		}
		
		public override void  removeFrom(Buildable parent)
		{
			((DrawPile) parent).removeGlobalKeyCommand(this);
		}
		
		public virtual KeyCommand getKeyCommand(Deck deck)
		{
			return new DeckKeyCommand(this, LocalizedConfigureName, null, deck);
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'DeckKeyCommand' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		internal class DeckKeyCommand:KeyCommand
		{
			private void  InitBlock(DeckGlobalKeyCommand enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private DeckGlobalKeyCommand enclosingInstance;
			public DeckGlobalKeyCommand Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			protected internal Deck deck;
			public DeckKeyCommand(DeckGlobalKeyCommand enclosingInstance, System.String name, System.Windows.Forms.KeyEventArgs key, Deck deck):base(name, key, deck)
			{
				InitBlock(enclosingInstance);
				this.deck = deck;
			}
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.apply(deck);
			}
		}
		
		public virtual void  apply(Deck deck)
		{
			GameModule.getGameModule().sendAndLog(((DeckGlobalCommand) globalCommand).apply(deck, Filter));
		}
		
		public virtual System.String encode()
		{
			SequenceEncoder se = new SequenceEncoder('|');
			se.append(getConfigureName()).append(getAttributeValueString(KEY_COMMAND)).append(getAttributeValueString(PROPERTIES_FILTER)).append(getAttributeValueString(DECK_COUNT)).append(getAttributeValueString(REPORT_FORMAT)).append(LocalizedConfigureName);
			return se.Value;
		}
		
		public virtual void  decode(System.String s)
		{
			SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(s, '|');
			setConfigureName(sd.nextToken(""));
			setAttribute(KEY_COMMAND, sd.nextNamedKeyStroke('A'));
			setAttribute(PROPERTIES_FILTER, sd.nextToken(null));
			setAttribute(DECK_COUNT, sd.nextInt(0));
			setAttribute(REPORT_FORMAT, sd.nextToken(""));
			localizedName = sd.nextToken(getConfigureName());
		}
		
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		NamedKeyStroke.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		PropertyExpression.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		DeckPolicyConfig2.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		ReportFormatConfig.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	public class DeckPolicyConfig2:DeckPolicyConfig
	{
		public DeckPolicyConfig2():base()
		{
			typeConfig.ValidValues = new System.String[]{ALL, FIXED};
			prompt.Text = "Affects:  ";
		}
	}
	
	public class DeckGlobalCommand:GlobalCommand
	{
		
		public DeckGlobalCommand(RecursionLimiter.Loopable l):base(l)
		{
		}
		
		public virtual Command apply(Deck d, PieceFilter filter)
		{
			System.String reportText = reportFormat.getText(source);
			Command c;
			if (reportText.Length > 0)
			{
				c = new Chatter.DisplayText(GameModule.getGameModule().getChatter(), "*" + reportText);
				c.execute();
			}
			else
			{
				c = new NullCommand();
			}
			Visitor visitor = new Visitor(this, c, filter, keyStroke);
			DeckVisitorDispatcher dispatcher = new DeckVisitorDispatcher(visitor);
			
			dispatcher.accept(d);
			visitor.Tracker.repaint();
			
			c = visitor.Command;
			return c;
		}
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}