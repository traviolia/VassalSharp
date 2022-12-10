/*
* $Id$
*
* Copyright (c) 2000-2009 by Rodney Kinney, Brent Easton
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

#if NEVER_DEFINED
using System;
using AbstractConfigurable = VassalSharp.build.AbstractConfigurable;
using Buildable = VassalSharp.build.Buildable;
using GameModule = VassalSharp.build.GameModule;
using Documentation = VassalSharp.build.module.documentation;
using DataArchive = VassalSharp.tools.DataArchive;
//using ErrorDialog = VassalSharp.tools.ErrorDialog;
using ReadErrorDialog = VassalSharp.tools.ReadErrorDialog;
using URLUtils = VassalSharp.tools.URLUtils;
using MenuItemProxy = VassalSharp.tools.menu.MenuItemProxy;
using MenuManager = VassalSharp.tools.menu.MenuManager;
#else
using System;
using AbstractConfigurable = VassalSharp.build.AbstractConfigurable;
using Buildable = VassalSharp.build.Buildable;
using GameModule = VassalSharp.build.GameModule;
using Documentation = VassalSharp.build.module.documentation;
using DataArchive = VassalSharp.tools.DataArchive;
//using ErrorDialog = VassalSharp.tools.ErrorDialog;
using ReadErrorDialog = VassalSharp.tools.ReadErrorDialog;
//using URLUtils = VassalSharp.tools.URLUtils;
//using MenuItemProxy = VassalSharp.tools.menu.MenuItemProxy;
//using MenuManager = VassalSharp.tools.menu.MenuManager;
#endif

namespace VassalSharp.build.module.documentation
{
#if NEVER_DEFINED

	/// <summary> Places an entry in the <code>Help</code> menu.  Selecting the entry
	/// displays a window with stored text on it.
	/// </summary>
	public class HelpFile : AbstractConfigurable
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction:SupportClass.ActionSupport
		{
			public AnonymousClassAbstractAction(HelpFile enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(HelpFile enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private HelpFile enclosingInstance;
			public HelpFile Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//Enclosing_Instance.showWindow();
			}
		}

		private void  InitBlock()
		{
			//return ;
			////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			//new Class < ? > []
			//{
			//	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			//	String.
			//}
		}

		public override Type[] getAttributeTypes()
		{
			throw new NotImplementedException();
		}

		public override void removeFrom(Buildable param1)
		{
			throw new NotImplementedException();
		}

		public override void setAttribute(string key, object value_Renamed)
		{
			throw new NotImplementedException();
		}

		public override string getAttributeValueString(string key)
		{
			throw new NotImplementedException();
		}

		public override void addTo(Buildable param1)
		{
			throw new NotImplementedException();
		}

		public static string ConfigureTypeName
		{
			get
			{
				return "Plain Text Help File";
			}
			
		}

		public override string[] AttributeDescriptions => throw new NotImplementedException();

		public override Type[] AllowableConfigureComponents => throw new NotImplementedException();

		public override string[] AttributeNames => throw new NotImplementedException();

#if NEVER_DEFINED
		virtual protected internal HelpWindow HelpWindow
		{
			get
			{
				if (frame == null)
				{
					frame = new HelpWindow(title, Contents);
				}
				return frame;
			}
			
		}
		virtual public System.Uri Contents
		{
			get
			{
				if (contents != null || fileName == null)
					return contents;
				
				if (ARCHIVE_ENTRY.Equals(fileType))
				{
					try
					{
						contents = GameModule.getGameModule().getDataArchive().getURL(fileName);
					}
					catch (System.IO.IOException e)
					{
						ReadErrorDialog.error(e, fileName);
					}
				}
				else if (RESOURCE.Equals(fileType))
				{
					GetType();
					//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
					contents = new System.Uri(System.IO.Path.GetFullPath(fileName));
				}
				else if (LOCAL_FILE.Equals(fileType))
				{
					System.IO.FileInfo f = new System.IO.FileInfo(fileName);
					if (fileName.StartsWith("docs/"))
					{
						//$NON-NLS-1$
						f = new System.IO.FileInfo(Documentation.DocumentationBaseDir.FullName + "\\" + fileName.Substring("docs/".Length)); //$NON-NLS-1$
					}
					
					try
					{
						contents = URLUtils.toURL(f);
					}
					catch (System.UriFormatException e)
					{
						ErrorDialog.bug(e);
					}
				}
				
				return contents;
			}
			
		}
		/// <summary> The attributes of a HelpFile are:
		/// 
		/// <code>TITLE</code> the text of the menu entry in the Help menu
		/// <code>FILE</code> the name of an text file in the {@link
		/// DataArchive}.  The text is displayed in a window with the same title
		/// </summary>
		override public string[] AttributeNames
		{
			get
			{
				return new string[]{TITLE, FILE, IMAGE, TYPE};
			}
			
		}
		override public string[] AttributeDescriptions
		{
			get
			{
				return new string[]{"Menu Entry:  ", "Text File:  "};
			}
			
		}
		public const string TITLE = "title"; //$NON-NLS-1$
		public const string FILE = "fileName"; //$NON-NLS-1$
		public const string TYPE = "fileType"; //$NON-NLS-1$
		private const string IMAGE = "image"; //$NON-NLS-1$
		
		public const string ARCHIVE_ENTRY = "archive"; //$NON-NLS-1$
		public const string RESOURCE = "resource"; //$NON-NLS-1$
		public const string LOCAL_FILE = "file"; //$NON-NLS-1$
		
		protected internal HelpWindow frame;
		protected internal DialogHelpWindow dialog;
		protected internal System.Uri contents;
		protected internal string title;
		protected internal string fileName;
		protected internal SupportClass.ActionSupport launch;
		protected internal string fileType = ARCHIVE_ENTRY;
		
		public HelpFile():this("help", (System.Uri) null)
		{
		}
		
		//UPGRADE_TODO: Class 'java.net.URL' was converted to a 'System.Uri' which does not throw an exception if a URL specifies an unknown protocol. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1132'"
		public HelpFile(string title, System.IO.FileInfo contents, string ref_Renamed):this(title, new System.Uri(URLUtils.toURL(contents), ref_Renamed))
		{
		}
		
		public HelpFile(string title, System.IO.FileInfo contents):this(title, URLUtils.toURL(contents))
		{
		}
		
		public HelpFile(string title, System.Uri contents)
		{
			InitBlock();
			this.title = title;
			this.contents = contents;
			setConfigureName(title);
			
			launch = new AnonymousClassAbstractAction(this);
			
			//UPGRADE_ISSUE: Method 'javax.swing.Action.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionputValue_javalangString_javalangObject'"
			//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
			launch.putValue(Action.NAME, getConfigureName());
		}
		
		/// <summary> Create and display a new HelpWindow with the contents of this HelpFile</summary>
		public virtual void  showWindow()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			HelpWindow w = HelpWindow;
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			w.Visible = true;
			w.BringToFront();
		}
		
		/// <summary> Create and display a new HelpWindow as a Dialog
		/// with the contents of this HelpFile
		/// </summary>
		public virtual void  showWindow(System.Windows.Forms.Form owner)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			DialogHelpWindow w = getDialogHelpWindow(owner);
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			w.Visible = true;
			w.BringToFront();
		}
		
		protected internal virtual DialogHelpWindow getDialogHelpWindow(System.Windows.Forms.Form d)
		{
			if (dialog == null)
			{
				dialog = new DialogHelpWindow(title, Contents, d);
			}
			return dialog;
		}
		
		/// <deprecated> Use {@link URLUtils.toURL(File)} instead. 
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		public static System.Uri toURL(System.IO.FileInfo f)
		{
			return URLUtils.toURL(f);
		}
		
		public override HelpFile getHelpFile()
		{
			System.IO.FileInfo dir = VassalSharp.build.module.Documentation.DocumentationBaseDir;
			dir = new System.IO.FileInfo(dir.FullName + "\\" + "ReferenceManual"); //$NON-NLS-1$
			try
			{
				return new HelpFile(null, new System.IO.FileInfo(dir.FullName + "\\" + "HelpMenu.htm"), "#HelpFile"); //$NON-NLS-1$ //$NON-NLS-2$
			}
			catch (System.UriFormatException e)
			{
				ErrorDialog.bug(e);
				return null;
			}
		}
		
		public override string getAttributeValueString(string key)
		{
			if (TITLE.Equals(key))
			{
				return title;
			}
			else if (FILE.Equals(key))
			{
				return fileName;
			}
			else if (TYPE.Equals(key))
			{
				return fileType;
			}
			return null;
		}
		
		public override void  setAttribute(string key, System.Object val)
		{
			if (TITLE.Equals(key))
			{
				title = ((string) val);
				setConfigureName(title);
				//UPGRADE_ISSUE: Method 'javax.swing.Action.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionputValue_javalangString_javalangObject'"
				//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
				launch.putValue(Action.NAME, title);
			}
			else if (FILE.Equals(key))
			{
				if (val is System.IO.FileInfo)
				{
					val = ((System.IO.FileInfo) val).Name;
					fileType = ARCHIVE_ENTRY;
				}
				fileName = ((string) val);
				if ("Intro.txt".Equals(key))
				{
					//$NON-NLS-1$
					fileType = RESOURCE;
				}
			}
			else if (TYPE.Equals(key))
			{
				fileType = ((string) val);
			}
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		File.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Class < ? > [] getAllowableConfigureComponents()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new Class < ? > [0];
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected MenuItemProxy launchItem;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void addTo(Buildable b)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		launchItem = new MenuItemProxy(launch);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	MenuManager.getInstance().addToSection(Documentation.Module, launchItem);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	launch.setEnabled(true);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void removeFrom(Buildable b)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		MenuManager.getInstance()
		.removeFromSection(Documentation.Module, launchItem);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	launch.setEnabled(false);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public static HelpFile getReferenceManualPage(String page)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return getReferenceManualPage(page, null);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public static HelpFile getReferenceManualPage(String page, String anchor)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(anchor != null && !anchor.startsWith(#))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	anchor = # + anchor; //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	File dir = VassalSharp.build.module.Documentation.getDocumentationBaseDir();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	dir = new File(dir, ReferenceManual); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	try
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return anchor == null ? new HelpFile(null, new File(dir, page)): 
		new HelpFile(null, new File(dir, page), anchor);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	catch(MalformedURLException ex)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ErrorDialog.bug(ex);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
#endif
	}
#else
	/// <summary> Places an entry in the <code>Help</code> menu.  Selecting the entry
	/// displays a window with stored text on it.
	/// </summary>
	public class HelpFile : AbstractConfigurable
	{
		private class AnonymousClassAbstractAction : SupportClass.ActionSupport
		{
			public AnonymousClassAbstractAction(HelpFile enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void InitBlock(HelpFile enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private HelpFile enclosingInstance;
			public HelpFile Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}

			}
			private const long serialVersionUID = 1L;

			public override void actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//Enclosing_Instance.showWindow();
			}
		}

		public static string ConfigureTypeName
		{
			get
			{
				return "Plain Text Help File";
			}

		}

		virtual protected internal HelpWindow HelpWindow
		{
			get
			{
				return null;
			}
		}

		virtual public System.Uri Contents
		{
			get
			{
				return null;
			}

		}

		/// <summary> The attributes of a HelpFile are:
		/// 
		/// <code>TITLE</code> the text of the menu entry in the Help menu
		/// <code>FILE</code> the name of an text file in the {@link
		/// DataArchive}.  The text is displayed in a window with the same title
		/// </summary>
		override public string[] AttributeNames
		{
			get
			{
				return new string[] { TITLE, FILE, IMAGE, TYPE };
			}

		}
		override public string[] AttributeDescriptions
		{
			get
			{
				return new string[] { "Menu Entry:  ", "Text File:  " };
			}

		}

        public override Type[] AllowableConfigureComponents => throw new NotImplementedException();

        public const string TITLE = "title"; //$NON-NLS-1$
		public const string FILE = "fileName"; //$NON-NLS-1$
		public const string TYPE = "fileType"; //$NON-NLS-1$
		private const string IMAGE = "image"; //$NON-NLS-1$

		public const string ARCHIVE_ENTRY = "archive"; //$NON-NLS-1$
		public const string RESOURCE = "resource"; //$NON-NLS-1$
		public const string LOCAL_FILE = "file"; //$NON-NLS-1$

		public HelpFile() : this("help", (System.Uri)null)
		{
		}

		public HelpFile(string title, System.Uri contents, string ref_Renamed) : this(title, new Uri(contents, ref_Renamed))
		{
		}

		public HelpFile(string title, string contents) : this(title, new Uri(contents))
		{
		}

		public HelpFile(string title, System.Uri contents)
		{
		}

		/// <summary> Create and display a new HelpWindow with the contents of this HelpFile</summary>
		public virtual void showWindow()
		{
		}

		/// <summary> Create and display a new HelpWindow as a Dialog
		/// with the contents of this HelpFile
		/// </summary>
		public virtual void showWindow(System.Windows.Forms.Form owner)
		{
		}

		protected internal virtual DialogHelpWindow getDialogHelpWindow(System.Windows.Forms.Form d)
		{
			return null;
		}

		/// <deprecated> Use {@link URLUtils.toURL(File)} instead. 
		/// </deprecated>
		public static System.Uri toURL(System.IO.FileInfo f)
		{
			return null;
		}

		public override HelpFile getHelpFile ()
		{
			 return null; 
		}

		public override string getAttributeValueString(string key)
		{
			return null;
		}

		public override void SetAttribute(string key, System.Object val)
		{
		}

		public override System.Type[] getAttributeTypes()
		{
			return null;
		}

		public System.Type[] getAllowableConfigureComponents()
		{
			return Array.Empty<System.Type>();
		}

		public override void addTo(Buildable b)
		{
		}

		public override void removeFrom(Buildable b)
		{
		}

		public static HelpFile getReferenceManualPage(String page)
		{
			return getReferenceManualPage(page, null);
		}

		public static HelpFile getReferenceManualPage(String page, String anchor)
		{
			return null;
		}
	}

#endif
}