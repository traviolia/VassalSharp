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
using Buildable = VassalSharp.build.Buildable;
using Builder = VassalSharp.build.Builder;
using Configurable = VassalSharp.build.Configurable;
using GameModule = VassalSharp.build.GameModule;
using ComponentPathBuilder = VassalSharp.tools.ComponentPathBuilder;
namespace VassalSharp.build.module
{
	
	/// <summary> An element of a {@link ModuleExtension} that extends an
	/// individual {@link VassalSharp.build.Buildable} component of the
	/// {@link VassalSharp.build.GameModule}.
	/// </summary>
	public class ExtensionElement : Buildable
	{
		virtual public Buildable Extension
		{
			get
			{
				return extension;
			}
			
		}
		virtual public Configurable[] TargetPath
		{
			get
			{
				return targetPath;
			}
			
		}
		/// <summary> An identifier for the component to be extended</summary>
		public const System.String TARGET = "target"; //$NON-NLS-1$
		private Buildable extension;
		private Configurable[] targetPath;
		
		public ExtensionElement()
		{
		}
		
		public ExtensionElement(Buildable extension, Configurable[] targetPath)
		{
			this.extension = extension;
			this.targetPath = targetPath;
		}
		
		public virtual void  add(Buildable child)
		{
			extension = child;
		}
		
		public virtual void  build(System.Xml.XmlElement e)
		{
			try
			{
				targetPath = ComponentPathBuilder.Instance.getPath(e.GetAttribute(TARGET));
			}
			catch (ComponentPathBuilder.PathFormatException e1)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new ExtensionsLoader.LoadExtensionException(e1.Message);
			}
			
			// find and build first child which is an element
			for (System.Xml.XmlNode n = e.FirstChild; n != null; n = n.NextSibling)
			{
				if (System.Convert.ToInt16(n.NodeType) == (short) System.Xml.XmlNodeType.Element)
				{
					extension = Builder.create((System.Xml.XmlElement) n);
					break;
				}
			}
		}
		
		public virtual System.Xml.XmlElement getBuildElement(System.Xml.XmlDocument doc)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'el '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Xml.XmlElement el = doc.CreateElement(GetType().FullName);
			//UPGRADE_TODO: Method 'org.w3c.dom.Element.setAttribute' was converted to 'System.Xml.XmlElement.SetAttribute' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_orgw3cdomElementsetAttribute_javalangString_javalangString'"
			el.SetAttribute(TARGET, ComponentPathBuilder.Instance.getId(targetPath));
			el.AppendChild(extension.getBuildElement(doc));
			return el;
		}
		
		public virtual void  addTo(Buildable parent)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'target '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Configurable target = targetPath.Length == 0?GameModule.getGameModule():targetPath[targetPath.Length - 1];
			extension.addTo(target);
			target.add(extension);
		}
	}
}