/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney
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
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Localization = VassalSharp.i18n.Localization;
namespace VassalSharp.build
{
	
	/// <summary> A general-purpose configurable GUI container
	/// Widgets are {@link Configurable} objects that represent
	/// AWT components.  Adding a Widget to another Widget during
	/// a {@link Buildable#build} operation will add the corresponding
	/// AWT component of the child to the component of the parent
	/// </summary>
	public abstract class Widget:AbstractConfigurable
	{
		private void  InitBlock()
		{
			return parent.AllowableConfigureComponents;
		}
		virtual public Widget Parent
		{
			get
			{
				return parent;
			}
			
		}
		override public Configurable[] ConfigureComponents
		{
			get
			{
				rebuild();
				return base.ConfigureComponents;
			}
			
		}
		/// <returns> the Component for this widget.  For efficiency, the
		/// Component may be initialized lazily instead of being created
		/// in the {@link Buildable#build} method
		/// </returns>
		public abstract System.Windows.Forms.Control Component{get;}
		public const System.String NAME = "entryName";
		public const System.String WIDTH = "width";
		public const System.String HEIGHT = "height";
		protected internal System.Xml.XmlElement buildElement;
		
		protected internal Widget parent;
		
		protected internal Widget()
		{
			InitBlock();
		}
		
		/// <summary> For memory efficiency reasons, a Widget is initialized lazily.
		/// This method only stores the element from which the build the Widget.
		/// The Widget is built from the stored element by invoking {@link #rebuild}.
		/// Subclasses should invoke {@link #rebuild} before invoking {@link #getComponent}
		/// </summary>
		public override void  build(System.Xml.XmlElement el)
		{
			buildElement = el;
			if (el != null)
			{
				System.Xml.XmlNamedNodeMap n = (System.Xml.XmlAttributeCollection) el.Attributes;
				for (int i = 0; i < n.Count; ++i)
				{
					System.Xml.XmlAttribute att = (System.Xml.XmlAttribute) n.Item(i);
					setAttribute(att.Name, att.Value);
					Localization.Instance.saveTranslatableAttribute(this, att.Name, att.Value);
				}
			}
		}
		
		/// <summary> Perform the build of this {@link Buildable} component using the element
		/// stored from when the the {@link #build} method was invoked
		/// </summary>
		protected internal virtual void  rebuild()
		{
			if (buildElement != null)
			{
				Builder.build(buildElement, this);
				buildElement = null;
			}
		}
		
		public override System.Xml.XmlElement getBuildElement(System.Xml.XmlDocument doc)
		{
			rebuild();
			return base.getBuildElement(doc);
		}
		
		/// <summary> The allowable Configurable components of a Widget are the same
		/// as its parent
		/// </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAllowableConfigureComponents()
		
		public override void  addTo(Buildable b)
		{
			parent = (Widget) b;
		}
		
		public override void  removeFrom(Buildable b)
		{
		}
		
		public override HelpFile getHelpFile()
		{
			return null;
		}
		
		//UPGRADE_ISSUE: Class 'javax.swing.DefaultListCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultListCellRenderer'"
		[Serializable]
		public class MyCellRenderer:javax.swing.DefaultListCellRenderer
		{
			private const long serialVersionUID = 1L;
			
			public System.Windows.Forms.Control getListCellRendererComponent(System.Windows.Forms.ListBox list, System.Object value_Renamed, int index, bool isSelected, bool cellHasFocus)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.DefaultListCellRenderer.getListCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultListCellRenderer'"
				base.getListCellRendererComponent(list, value_Renamed, index, isSelected, cellHasFocus);
				if (value_Renamed is Configurable)
					Text = ((Configurable) value_Renamed).getConfigureName();
				return this;
			}
		}
	}
}