/*
* $Id$
*
* Copyright (c) 2008-2009 by Brent Easton
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
using Configurable = VassalSharp.build.Configurable;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using PropertySource = VassalSharp.build.module.properties.PropertySource;
using Configurer = VassalSharp.configure.Configurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using TextConfigurer = VassalSharp.configure.TextConfigurer;
using ValidationReport = VassalSharp.configure.ValidationReport;
using ValidityChecker = VassalSharp.configure.ValidityChecker;
using UniqueIdManager = VassalSharp.tools.UniqueIdManager;
namespace VassalSharp.script
{
	
	/// <summary> 
	/// 
	/// </summary>
	public class GeneralScript:AbstractScript, UniqueIdManager.Identifyable, ValidityChecker
	{
		public static System.String ConfigureTypeName
		{
			get
			{
				return "General Script";
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
		virtual public System.String FullScript
		{
			get
			{
				return buildHeaderLine() + "\n" + Script + "\n}";
			}
			
		}
		override public Configurer Configurer
		{
			get
			{
				return new ScriptConfigurer(this, this);
			}
			
		}
		
		private static UniqueIdManager idMgr = new UniqueIdManager("General-");
		
		public GeneralScript():base()
		{
		}
		
		public virtual System.String evaluate(PropertySource target)
		{
			return "";
		}
		
		protected internal virtual System.String buildHeaderLine()
		{
			return "void " + getConfigureName() + "() {";
		}
		
		public override CompileResult compile()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'fullScript '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String fullScript = FullScript;
			return BeanShell.Instance.compile(new System.IO.StringReader(fullScript));
		}
		
		public override HelpFile getHelpFile()
		{
			return HelpFile.getReferenceManualPage("Script.htm"); //$NON-NLS-1$
		}
		
		public override void  removeFrom(Buildable parent)
		{
			idMgr.remove(this);
		}
		
		public override void  addTo(Buildable parent)
		{
			idMgr.add(this);
		}
		
		public override void  validate(Buildable target, ValidationReport report)
		{
			idMgr.validate(this, report);
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'ScriptConfigurer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Configure a Script</summary>
		internal class ScriptConfigurer:Configurer
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener
			{
				public AnonymousClassPropertyChangeListener(ScriptConfigurer enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(ScriptConfigurer enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private ScriptConfigurer enclosingInstance;
				public ScriptConfigurer Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
				{
					if (VassalSharp.build.Configurable_Fields.NAME_PROPERTY.Equals(evt.PropertyName))
					{
						Enclosing_Instance.setName((System.String) evt.NewValue);
					}
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener1
			{
				public AnonymousClassPropertyChangeListener1(ScriptConfigurer enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(ScriptConfigurer enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private ScriptConfigurer enclosingInstance;
				public ScriptConfigurer Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
				{
					Enclosing_Instance.script.setAttribute(VassalSharp.script.AbstractScript.NAME, e.NewValue);
					Enclosing_Instance.updateHeader();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener2
			{
				public AnonymousClassPropertyChangeListener2(ScriptConfigurer enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(ScriptConfigurer enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private ScriptConfigurer enclosingInstance;
				public ScriptConfigurer Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
				{
					Enclosing_Instance.script.setAttribute(VassalSharp.script.AbstractScript.DESC, e.NewValue);
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener3
			{
				public AnonymousClassPropertyChangeListener3(ScriptConfigurer enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(ScriptConfigurer enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private ScriptConfigurer enclosingInstance;
				public ScriptConfigurer Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
				{
					Enclosing_Instance.script.setAttribute(VassalSharp.script.AbstractScript.SCRIPT, e.NewValue);
				}
			}
			private void  InitBlock(GeneralScript enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private GeneralScript enclosingInstance;
			virtual public GeneralScript Script
			{
				get
				{
					return script;
				}
				
			}
			override public System.Windows.Forms.Control Controls
			{
				get
				{
					return panel;
				}
				
			}
			override public System.String ValueString
			{
				get
				{
					return script.getConfigureName();
				}
				
			}
			public GeneralScript Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			protected internal GeneralScript script;
			protected internal System.Windows.Forms.Panel panel;
			protected internal JavaNameConfigurer nameConfig;
			protected internal StringConfigurer descConfig;
			protected internal TextConfigurer scriptConfig;
			protected internal System.Windows.Forms.Button compileButton;
			protected internal System.Windows.Forms.Label error = new System.Windows.Forms.Label();
			protected internal System.Windows.Forms.Label headerLine = new System.Windows.Forms.Label();
			
			public ScriptConfigurer(GeneralScript enclosingInstance, GeneralScript s):base(null, s.getConfigureName())
			{
				InitBlock(enclosingInstance);
				script = s;
				setValue(script);
				script.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
				
				panel = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				panel.setLayout(new BoxLayout(panel, BoxLayout.Y_AXIS));
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				panel.Size = new System.Drawing.Size(800, 600);
				
				nameConfig = new JavaNameConfigurer(VassalSharp.script.AbstractScript.NAME, "Name:  ", script.getConfigureName());
				nameConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener1(this).propertyChange);
				
				descConfig = new StringConfigurer(VassalSharp.script.AbstractScript.DESC, "Description:  ", script.Description);
				descConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener2(this).propertyChange);
				
				headerLine.Text = Enclosing_Instance.buildHeaderLine();
				
				scriptConfig = new TextConfigurer(VassalSharp.script.AbstractScript.SCRIPT, "Script:  ", script.Script);
				scriptConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener3(this).propertyChange);
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(nameConfig.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(descConfig.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(headerLine);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(scriptConfig.Controls);
				
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box compileBox = Box.createHorizontalBox();
				compileButton = SupportClass.ButtonSupport.CreateStandardButton("Compile");
				compileButton.Click += new System.EventHandler(this.actionPerformed);
				SupportClass.CommandManager.CheckCommand(compileButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				compileBox.Controls.Add(compileButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				compileBox.Controls.Add(error);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(compileBox);
			}
			
			public virtual void  updateHeader()
			{
				headerLine.Text = Enclosing_Instance.buildHeaderLine();
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public override void  setValue(System.String s)
			{
				throw new System.SystemException("Can't set ScriptConfigurable from String");
			}
			
			/// <summary> Compile the script and report errors</summary>
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				CompileResult r = script.compile();
				if (r.Success)
				{
					error.Text = "";
				}
				else
				{
					error.Text = r.Message;
				}
			}
		}
	}
}