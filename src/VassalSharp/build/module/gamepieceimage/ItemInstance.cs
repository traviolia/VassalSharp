/*
* $Id$
*
* Copyright (c) 2005 by Rodney Kinney, Brent Easton
*
* This library is free software; you can redistribute it and/or modify it under
* the terms of the GNU Library General Public License (LGPL) as published by
* the Free Software Foundation.
*
* This library is distributed in the hope that it will be useful, but WITHOUT
* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
* FOR A PARTICULAR PURPOSE. See the GNU Library General Public License for more
* details.
*
* You should have received a copy of the GNU Library General Public License
* along with this library; if not, copies are available at
* http://www.opensource.org.
*/
using System;
using AbstractConfigurable = VassalSharp.build.AbstractConfigurable;
using Buildable = VassalSharp.build.Buildable;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
namespace VassalSharp.build.module.gamepieceimage
{
	
	/// <summary> Individual override parameters for an instance of an {@link Item}. The XXXItemInstance classes ({@link TextItemInstance},{@link ImageItemInstance},
	/// etc.) draw themselves optionally using values for color, text value, etc., from the corresponding XXXItem classes ({@link TextItem},{@link ImageItem}, etc.) can make use of when drawing themselves
	/// </summary>
	public abstract class ItemInstance:AbstractConfigurable
	{
		private void  InitBlock()
		{
			bgColor = ColorSwatch.getClear();
			fgColor = ColorSwatch.getBlack();
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > [0];
			return null;
		}
		virtual public InstanceConfigurer Config
		{
			set
			{
				myConfig = value;
			}
			
		}
		virtual public System.String Name
		{
			get
			{
				return name;
			}
			
			set
			{
				this.name = value;
			}
			
		}
		virtual public System.String Type
		{
			get
			{
				return type;
			}
			
			set
			{
				this.type = value;
			}
			
		}
		virtual public System.String Location
		{
			get
			{
				return location;
			}
			
			set
			{
				this.location = value;
			}
			
		}
		virtual public ColorSwatch BgColor
		{
			get
			{
				return bgColor;
			}
			
			set
			{
				this.bgColor = value;
			}
			
		}
		virtual public ColorSwatch FgColor
		{
			get
			{
				return fgColor;
			}
			
			set
			{
				this.fgColor = value;
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[0];
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[0];
			}
			
		}
		virtual public System.String Suffix
		{
			get
			{
				return ""; //$NON-NLS-1$
			}
			
		}
		
		public const System.String FG_COLOR = "fgColor"; //$NON-NLS-1$
		public const System.String BG_COLOR = "bgColor"; //$NON-NLS-1$
		
		protected internal System.String type = ""; //$NON-NLS-1$
		protected internal System.String location = ""; //$NON-NLS-1$
		//UPGRADE_NOTE: The initialization of  'bgColor' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal ColorSwatch bgColor;
		//UPGRADE_NOTE: The initialization of  'fgColor' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal ColorSwatch fgColor;
		protected internal GamePieceImage defn;
		protected internal Item item;
		protected internal InstanceConfigurer myConfig = null;
		
		public ItemInstance(System.String nam, System.String typ, System.String loc)
		{
			InitBlock();
			Name = nam;
			Type = typ;
			Location = loc;
		}
		
		public ItemInstance():this("", "", "")
		{ //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
		}
		
		public ItemInstance(GamePieceImage defn):this()
		{
			this.defn = defn;
		}
		
		/*
		* Generate a copy of the instance for use by the Generic trait.
		*/
		
		protected internal virtual void  setItem()
		{
			if (defn != null)
			{
				GamePieceLayout layout = defn.getLayout();
				if (layout != null)
				{
					item = layout.getItem(name);
				}
			}
		}
		
		public virtual Item getItem()
		{
			if (item == null)
			{
				setItem();
			}
			return item;
		}
		
		public abstract System.String encode();
		
		public static ItemInstance newDefaultInstance(System.String name, System.String type, System.String location)
		{
			
			if (type.Equals(SymbolItem.TYPE))
			{
				return new SymbolItemInstance(name, type, location, Symbol.NatoUnitSymbolSet.SZ_DIVISION, Symbol.NatoUnitSymbolSet.INFANTRY, Symbol.NatoUnitSymbolSet.NONE);
			}
			else if (type.Equals(TextItem.TYPE))
			{
				return new TextItemInstance(name, type, location, null);
			}
			else if (type.Equals(TextBoxItem.TYPE))
			{
				return new TextBoxItemInstance(name, type, location);
			}
			else if (type.Equals(ShapeItem.TYPE))
			{
				return new ShapeItemInstance(name, type, location);
			}
			else if (type.Equals(ImageItem.TYPE))
			{
				return new ImageItemInstance(name, type, location);
			}
			return null;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		
		public override void  setAttribute(System.String key, System.Object value_Renamed)
		{
			
		}
		
		public override System.String getAttributeValueString(System.String key)
		{
			return null;
		}
		
		public override void  removeFrom(Buildable parent)
		{
			
		}
		
		public override HelpFile getHelpFile()
		{
			return null;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAllowableConfigureComponents()
		
		public override void  addTo(Buildable parent)
		{
			if (parent is GamePieceImage)
			{
				defn = (GamePieceImage) parent;
			}
		}
		
		public virtual System.String formatName(System.String name)
		{
			return name;
		}
	}
}