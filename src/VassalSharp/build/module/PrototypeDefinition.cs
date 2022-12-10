/*
* $Id$
*
* Copyright (c) 2004 by Rodney Kinney
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
using AbstractConfigurable = VassalSharp.build.AbstractConfigurable;
using BadDataReport = VassalSharp.build.BadDataReport;
using Buildable = VassalSharp.build.Buildable;
using Builder = VassalSharp.build.Builder;
using Configurable = VassalSharp.build.Configurable;
using GameModule = VassalSharp.build.GameModule;
using GpIdSupport = VassalSharp.build.GpIdSupport;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using PropertySource = VassalSharp.build.module.properties.PropertySource;
using AddPiece = VassalSharp.command.AddPiece;
using Configurer = VassalSharp.configure.Configurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using ValidationReport = VassalSharp.configure.ValidationReport;
using ValidityChecker = VassalSharp.configure.ValidityChecker;
using BasicPiece = VassalSharp.counters.BasicPiece;
using Decorator = VassalSharp.counters.Decorator;
using GamePiece = VassalSharp.counters.GamePiece;
using PieceDefiner = VassalSharp.counters.PieceDefiner;
using PieceEditor = VassalSharp.counters.PieceEditor;
using Properties = VassalSharp.counters.Properties;
using ComponentI18nData = VassalSharp.i18n.ComponentI18nData;
using Resources = VassalSharp.i18n.Resources;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using FormattedString = VassalSharp.tools.FormattedString;
using UniqueIdManager = VassalSharp.tools.UniqueIdManager;
namespace VassalSharp.build.module
{
	
	public class PrototypeDefinition:AbstractConfigurable, UniqueIdManager.Identifyable, ValidityChecker
	{
		public PrototypeDefinition()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: Constructor 'java.beans.PropertyChangeSupport.PropertyChangeSupport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
			propSupport = new PropertyChangeSupport(this);
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > [0];
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > [0];
		}
		[System.ComponentModel.Browsable(true)]
		public new  event SupportClass.PropertyChangeEventHandler PropertyChange;
		override public Configurable[] ConfigureComponents
		{
			get
			{
				return new Configurable[0];
			}
			
		}
		override public Configurer Configurer
		{
			get
			{
				return new Config(this);
			}
			
		}
		virtual public System.String Id
		{
			get
			{
				return null;
			}
			
			set
			{
			}
			
		}
		public static System.String ConfigureTypeName
		{
			get
			{
				return Resources.getString("Editor.Prototype.component_type"); //$NON-NLS-1$
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
		override public ComponentI18nData I18nData
		{
			get
			{
				/*
				* Prototype definition may change due to editing, so no caching
				*/
				return new ComponentI18nData(this, getPiece());
			}
			
		}
		new private System.String name = "Prototype"; //$NON-NLS-1$
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private java.util.Map < String, GamePiece > pieces = 
		new HashMap < String, GamePiece >();
		private System.String pieceDefinition;
		private static UniqueIdManager idMgr = new UniqueIdManager("prototype-"); //$NON-NLS-1$
		//UPGRADE_ISSUE: Class 'java.beans.PropertyChangeSupport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
		//UPGRADE_NOTE: The initialization of  'propSupport' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private PropertyChangeSupport propSupport;
		
		//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public override void  addPropertyChangeListener(PropertyChangeListener l)
		{
			//UPGRADE_ISSUE: Method 'java.beans.PropertyChangeSupport.addPropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
			propSupport.addPropertyChangeListener(l);
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAllowableConfigureComponents()
		
		public override System.String getConfigureName()
		{
			return name;
		}
		
		public override void  setConfigureName(System.String s)
		{
			System.String oldName = name;
			this.name = s;
			SupportClass.PropertyChangingEventArgs me3 = new SupportClass.PropertyChangingEventArgs(VassalSharp.build.Configurable_Fields.NAME_PROPERTY, oldName, name);
			if (PropertyChange != null)
				PropertyChange(this, me3);
		}
		
		public override HelpFile getHelpFile()
		{
			return HelpFile.getReferenceManualPage("GameModule.htm", "Definition"); //$NON-NLS-1$ //$NON-NLS-2$
		}
		
		public override void  remove(Buildable child)
		{
			idMgr.remove(this);
		}
		
		public override void  removeFrom(Buildable parent)
		{
		}
		
		public override void  add(Buildable child)
		{
		}
		
		public override void  validate(Buildable target, ValidationReport report)
		{
			idMgr.validate(this, report);
		}
		
		public override void  addTo(Buildable parent)
		{
			idMgr.add(this);
		}
		
		public virtual GamePiece getPiece()
		{
			return getPiece(pieceDefinition);
		}
		
		/// <summary> For the case when the piece definition is a Message Format, expand the definition using the given properties
		/// 
		/// </summary>
		/// <param name="props">
		/// </param>
		/// <returns>
		/// </returns>
		public virtual GamePiece getPiece(PropertySource props)
		{
			System.String def = props == null?pieceDefinition:new FormattedString(pieceDefinition).getText(props);
			return getPiece(def);
		}
		
		protected internal virtual GamePiece getPiece(System.String def)
		{
			GamePiece piece = pieces.get_Renamed(def);
			if (piece == null && def != null)
			{
				try
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'comm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					AddPiece comm = (AddPiece) GameModule.getGameModule().decode(def);
					if (comm == null)
					{
						ErrorDialog.dataError(new BadDataReport("Couldn't build piece ", def, null)); //$NON-NLS-1$
					}
					else
					{
						piece = comm.Target;
						piece.State = comm.State;
					}
				}
				catch (System.SystemException e)
				{
					ErrorDialog.dataError(new BadDataReport("Couldn't build piece", def, e));
				}
			}
			return piece;
		}
		
		public virtual void  setPiece(GamePiece p)
		{
			pieceDefinition = p == null?null:GameModule.getGameModule().encode(new AddPiece(p));
			pieces.clear();
		}
		
		public override void  build(System.Xml.XmlElement e)
		{
			if (e != null)
			{
				setConfigureName(e.GetAttribute(VassalSharp.build.Configurable_Fields.NAME_PROPERTY));
				pieceDefinition = Builder.getText(e);
			}
		}
		
		public override System.Xml.XmlElement getBuildElement(System.Xml.XmlDocument doc)
		{
			System.Xml.XmlElement el = doc.CreateElement(GetType().FullName);
			//UPGRADE_TODO: Method 'org.w3c.dom.Element.setAttribute' was converted to 'System.Xml.XmlElement.SetAttribute' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_orgw3cdomElementsetAttribute_javalangString_javalangString'"
			el.SetAttribute(VassalSharp.build.Configurable_Fields.NAME_PROPERTY, name);
			el.AppendChild(doc.CreateTextNode(pieceDefinition));
			return el;
		}
		
		public class Config:Configurer
		{
			override public System.Windows.Forms.Control Controls
			{
				get
				{
					return box;
				}
				
			}
			override public System.String ValueString
			{
				get
				{
					return null;
				}
				
			}
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			private Box box;
			private PieceDefiner pieceDefiner;
			new private StringConfigurer name;
			private PrototypeDefinition def;
			
			public Config(PrototypeDefinition def):base(null, null, def)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				box = Box.createVerticalBox();
				name = new StringConfigurer(null, Resources.getString(Resources.NAME_LABEL), def.name);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(name.Controls);
				pieceDefiner = new Definer(GameModule.getGameModule().getGpIdSupport());
				pieceDefiner.setPiece(def.getPiece());
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(pieceDefiner);
				this.def = def;
			}
			
			public override System.Object getValue()
			{
				if (def != null)
				{
					def.setPiece(pieceDefiner.getPiece());
					def.setConfigureName(name.ValueString);
				}
				return def;
			}
			
			public override void  setValue(System.String s)
			{
			}
			[Serializable]
			public class Definer:PieceDefiner
			{
				private const long serialVersionUID = 1L;
				
				public Definer(GpIdSupport s):base(s)
				{
				}
				
				public override void  setPiece(GamePiece piece)
				{
					if (piece != null)
					{
						GamePiece inner = Decorator.getInnermost(piece);
						if (!(inner is Plain))
						{
							Plain plain = new Plain();
							System.Object outer = inner.getProperty(VassalSharp.counters.Properties_Fields.OUTER);
							if (outer is Decorator)
							{
								((Decorator) outer).setInner(plain);
							}
							piece = Decorator.getOutermost(plain);
						}
					}
					else
					{
						piece = new Plain();
					}
					base.setPiece(piece);
				}
				
				protected internal override void  removeTrait(int index)
				{
					if (index > 0)
					{
						base.removeTrait(index);
					}
				}
				
				private class Plain:BasicPiece
				{
					override public System.String Description
					{
						get
						{
							return ""; //$NON-NLS-1$
						}
						
					}
					public Plain():base(ID + ";;;;")
					{ //$NON-NLS-1$
					}
					
					public override PieceEditor getEditor()
					{
						return null;
					}
				}
			}
		}
		
		/*
		* Implement Translatable - Since PrototypeDefinition implements its
		* own configurer, methods below here will only ever be called by the
		* translation subsystem.
		*/
		
		public override void  setAttribute(System.String attr, System.Object value_Renamed)
		{
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		
		/*
		* Redirect getAttributeValueString() to return the attribute
		* values for the enclosed pieces
		*/
		public override System.String getAttributeValueString(System.String attr)
		{
			return I18nData.getLocalUntranslatedValue(attr);
		}
	}
}