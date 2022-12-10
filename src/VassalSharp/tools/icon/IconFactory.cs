/*
* $Id$
*
* Copyright (c) 2008 Brent Easton
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
//UPGRADE_TODO: The type 'java.util.concurrent.ConcurrentHashMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ConcurrentHashMap = java.util.concurrent.ConcurrentHashMap;
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
using IllegalBuildException = VassalSharp.build.IllegalBuildException;
using Resources = VassalSharp.i18n.Resources;
using ApplicationIcons = VassalSharp.tools.ApplicationIcons;
using DataArchive = VassalSharp.tools.DataArchive;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using JarArchive = VassalSharp.tools.JarArchive;
using ReadErrorDialog = VassalSharp.tools.ReadErrorDialog;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.tools.icon
{
	
	/// <summary> Manage and Provide Icons in standard sizes.
	/// 
	/// </summary>
	public sealed class IconFactory
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(IconFactory enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(IconFactory enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private IconFactory enclosingInstance;
			public IconFactory Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  Run()
			{
				lock (VassalSharp.tools.icon.IconFactory.preloadLock)
				{
					try
					{
						Enclosing_Instance.initVassalIconFamilys();
					}
					catch (IllegalBuildException e)
					{
						ErrorDialog.bug(e);
					}
				}
			}
		}
		private void  InitBlock()
		{
			return Instance.getIconFamilyList();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final ArrayList < String > names = new ArrayList < String >();
			lock (preloadLock)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(String s: iconFamilies.keySet())
				{
					names.add(s);
				}
			}
			Collections.sort(names);
			return names;
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Return the Singleton instance.</summary>
		/// <summary> Set the Singleton instance</summary>
		/// <param name="i">
		/// </param>
		internal static IconFactory Instance
		{
			get
			{
				if (instance == null)
				{
					throw new System.SystemException("No IconFactory instance"); //$NON-NLS-1$
				}
				return instance;
			}
			
			set
			{
				instance = value;
			}
			
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.tools.icon.IconFactory'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		internal const System.String FILE = "file:"; //$NON-NLS-1$
		internal const System.String JAR = "jar:"; //$NON-NLS-1$
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'jar '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly JarArchive jar = new JarArchive();
		
		private static IconFactory instance = new IconFactory();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private Map < String, IconFamily > iconFamilies = new ConcurrentHashMap < String, IconFamily >();
		//UPGRADE_NOTE: Final was removed from the declaration of 'preloadLock '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly System.Object preloadLock = new System.Object();
		private SupportClass.ThreadClass preloadThread;
		
		/// <summary> Create a new IconFactory.</summary>
		public IconFactory()
		{
			InitBlock();
			
			// FIXME: Maybe send this off to an executor?
			// FIXME: preloadThread is never set to null, cannot be gc'd
			// Find all available Icon Familys within Vassal.
			// May take a little while, so run it on a background thread
			preloadThread = new SupportClass.ThreadClass(new System.Threading.ThreadStart(new AnonymousClassRunnable(this).Run), "IconFactory-preload"); //$NON-NLS-1$
			preloadThread.Start();
		}
		
		/// <summary> Return an Icon of the specified size. Standard sizes are defined in IconFamily
		/// 
		/// </summary>
		/// <param name="IconFamilyName">Name of Icon family
		/// </param>
		/// <param name="size">Size (See IconFamily)
		/// </param>
		/// <returns> Sized Icon
		/// </returns>
		public static System.Drawing.Image getIcon(System.String iconFamilyName, int size)
		{
			IconFamily family = Instance.getFamily(iconFamilyName);
			if (family == null)
			{
				throw new IllegalStateException(Resources.getString("Error.not_found", IconFamily.ConfigureTypeName + " " + iconFamilyName)); //$NON-NLS-1$
			}
			return family.getIcon(size);
		}
		
		/// <summary>Return an Icon of the specified size as an Image. Standard sizes are defined in IconFamily
		/// 
		/// </summary>
		/// <param name="IconFamilyName">Name of Icon family
		/// </param>
		/// <param name="size">Size (See IconFamily)
		/// </param>
		/// <returns> Sized Image
		/// </returns>
		public static System.Drawing.Bitmap getImage(System.String iconFamilyName, int size)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'family '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			IconFamily family = Instance.getFamily(iconFamilyName);
			if (family == null)
			{
				throw new IllegalStateException(Resources.getString("Error.not_found", IconFamily.ConfigureTypeName + " " + iconFamilyName)); //$NON-NLS-1$
			}
			return family.getImage(size);
		}
		
		/// <summary> Return a sorted list of all Icon Family names.</summary>
		/// <returns>
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static List < String > getIconFamilyNames()
		
		/// <summary> Add a new IconFamily</summary>
		/// <param name="family">Icon Family Name
		/// </param>
		public static void  addIconFamily(IconFamily family)
		{
			Instance.add(family);
		}
		
		/// <summary> Remove an IconFamily</summary>
		/// <param name="name">Icon Family Name
		/// </param>
		public static void  removeIconFamily(IconFamily family)
		{
			Instance.remove(family);
		}
		
		/// <summary> Rename an IconFamily
		/// 
		/// </summary>
		/// <param name="oldName">Old Icon Family Name
		/// </param>
		/// <param name="newName">New Icon Family Name
		/// </param>
		public static void  renameIconFamily(System.String oldName, IconFamily family)
		{
			Instance.rename(oldName, family);
		}
		
		/// <summary> Return an Icon Family</summary>
		/// <param name="name">
		/// </param>
		/// <returns>
		/// </returns>
		public static IconFamily getIconFamily(System.String name)
		{
			return Instance.getFamily(name);
		}
		
		/// <summary> Add an Icon Family. Don't overwrite an existing Icon Family
		/// of the same name.
		/// 
		/// </summary>
		/// <param name="family">
		/// </param>
		private void  add(IconFamily family)
		{
			if (iconFamilies.get_Renamed(family.Name) == null)
			{
				iconFamilies.put(family.Name, family);
			}
		}
		
		/// <summary> Remove an Icon Family. Ensure that the family to be removed is
		/// the same as the one on the list.
		/// </summary>
		/// <param name="family">
		/// </param>
		private void  remove(IconFamily family)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'old '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			IconFamily old = iconFamilies.get_Renamed(family.Name);
			if (old != null && old == family)
			{
				iconFamilies.remove(family.Name);
			}
		}
		
		/// <summary> Rename an IconFamily. Do not affect existing families with the
		/// same name.
		/// 
		/// </summary>
		/// <param name="oldFamilyName">
		/// </param>
		/// <param name="iconFamily">
		/// </param>
		private void  rename(System.String oldFamilyName, IconFamily iconFamily)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'oldFamily '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			IconFamily oldFamily = iconFamilies.get_Renamed(oldFamilyName);
			if (oldFamily != null && oldFamily == iconFamily)
			{
				iconFamilies.remove(oldFamilyName);
			}
			add(iconFamily);
		}
		
		
		/// <summary> Return an individual named IconFamily.
		/// Ensure the Vassal icon prescan has completed first.
		/// 
		/// </summary>
		/// <param name="iconFamilyName">
		/// </param>
		/// <returns> Icon Family
		/// </returns>
		internal IconFamily getFamily(System.String iconFamilyName)
		{
			try
			{
				
				// FIXME: This is bad---we should wait on a Future instead.
				// Ensure preload is complete
				if (preloadThread.IsAlive)
				{
					try
					{
						preloadThread.Join();
					}
					catch (System.Threading.ThreadInterruptedException e)
					{
					}
				}
				
				return iconFamilies.get_Renamed(iconFamilyName);
			}
			catch (System.SystemException e)
			{
				ErrorDialog.bug(e);
			}
			return null;
		}
		
		/// <summary> Return a sorted list of all available IconFamily names.
		/// 
		/// </summary>
		/// <returns> Icon Family name list
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < String > getIconFamilyList()
		
		/// <summary>-------------------------------------------------------------------
		/// Inspect the Jar file (for a standard installation) or the local file
		/// system (Vassal running under a debugger) and determine all available
		/// Icons of all sizes and collect them into named Icon Familys.
		/// 
		/// This is essentially a cross-reference of all available Icons to ensure
		/// fast processing of requests for Icons. No Icons are created at this
		/// stage.
		/// </summary>
		private void  initVassalIconFamilys()
		{
			System.Uri imageUrl = null;
			try
			{
				//Build a URL to the Vassal images folder. It is guaranteed to exist
				// in any version of Vassal
				// Bug 9670 BUT there may also be an images folder in another Jar file in the Classloader path before the Vengine.jar
				// so look for an Icon we know must exist there.
				// imageUrl = jar.getURL(DataArchive.IMAGE_DIR
				imageUrl = jar.getURL(DataArchive.IMAGE_DIR + ApplicationIcons.VASSAL_ICON_LARGE);
				//UPGRADE_TODO: Class 'java.net.URL' was converted to a 'System.Uri' which does not throw an exception if a URL specifies an unknown protocol. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1132'"
				imageUrl = new System.Uri(imageUrl.ToString().Substring(0, (imageUrl.ToString().Length - ApplicationIcons.VASSAL_ICON_LARGE.Length) - (0)));
				logger.info("VASSAL images folder found at " + imageUrl);
				// Determine if we are running locally under a debugger, or
				// from an installation package. If running an installed version
				// of Vassal, the images URL will start with "jar:".
				if (imageUrl.ToString().StartsWith(FILE))
				{
					findLocalScalableIcons();
					for (int size = 0; size < IconFamily.SIZE_DIRS.Length; size++)
					{
						findLocalSizedIcons(size);
					}
				}
				else if (imageUrl.ToString().StartsWith(JAR))
				{
					findJarIcons();
				}
				else
				{
					throw new IllegalBuildException("Unknown Vassal Image source type: " + imageUrl.ToString()); //$NON-NLS-1$
				}
			}
			catch (System.IO.IOException e)
			{
				ReadErrorDialog.error(e, imageUrl.ToString());
			}
		}
		
		/// <summary> Record all icons of the specified size found in the local file system
		/// NB. Vassal is not running from a bundled Jar file
		/// 
		/// </summary>
		/// <param name="size">
		/// </param>
		/// <throws>  IOException </throws>
		internal void  findLocalSizedIcons(int size)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'path '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String path = DataArchive.ICON_DIR + IconFamily.SIZE_DIRS[size];
			//UPGRADE_NOTE: Final was removed from the declaration of 'sizeURL '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Uri sizeURL = jar.getURL(path);
			System.IO.StreamReader br = null;
			try
			{
				//UPGRADE_TODO: The differences in the expected value  of parameters for constructor 'java.io.BufferedReader.BufferedReader'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
				//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
				br = new System.IO.StreamReader(new System.IO.StreamReader(System.Net.WebRequest.Create(sizeURL).GetResponse().GetResponseStream(), System.Text.Encoding.Default).BaseStream, new System.IO.StreamReader(System.Net.WebRequest.Create(sizeURL).GetResponse().GetResponseStream(), System.Text.Encoding.Default).CurrentEncoding);
				System.String imageName = ""; //$NON-NLS-1$
				while (imageName != null)
				{
					imageName = br.ReadLine();
					if (imageName != null)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'familyName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.String familyName = imageName.split("\\.")[0]; //$NON-NLS-1$
						IconFamily family = iconFamilies.get_Renamed(familyName);
						if (family == null)
						{
							family = new IconFamily(familyName);
							logger.info("Icon family " + familyName + " created for " + imageName);
						}
						family.setSizeIconPath(size, "/" + path + imageName); //$NON-NLS-1$ //$NON-NLS-2$
						iconFamilies.put(familyName, family);
					}
				}
			}
			finally
			{
				IOUtils.closeQuietly(br);
			}
		}
		
		/// <summary> Record all scalable icons found on the local file system.
		/// NB. Vassal is not running from a bundled Jar file
		/// 
		/// </summary>
		/// <throws>  IOException </throws>
		private void  findLocalScalableIcons()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'scalablePath '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String scalablePath = DataArchive.ICON_DIR + IconFamily.SCALABLE_DIR;
			//UPGRADE_NOTE: Final was removed from the declaration of 'url '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Uri url = jar.getURL(scalablePath);
			System.IO.StreamReader br = null;
			try
			{
				//UPGRADE_TODO: The differences in the expected value  of parameters for constructor 'java.io.BufferedReader.BufferedReader'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
				//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
				br = new System.IO.StreamReader(new System.IO.StreamReader(System.Net.WebRequest.Create(url).GetResponse().GetResponseStream(), System.Text.Encoding.Default).BaseStream, new System.IO.StreamReader(System.Net.WebRequest.Create(url).GetResponse().GetResponseStream(), System.Text.Encoding.Default).CurrentEncoding);
				System.String imageName = ""; //$NON-NLS-1$
				while (imageName != null)
				{
					imageName = br.ReadLine();
					if (imageName != null)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'familyName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.String familyName = imageName.split("\\.")[0]; //$NON-NLS-1$
						IconFamily family = iconFamilies.get_Renamed(familyName);
						if (family == null)
						{
							family = new IconFamily(familyName);
							logger.info("Icon family " + familyName + " created for " + imageName);
						}
						family.ScalableIconPath = "/" + scalablePath + imageName; //$NON-NLS-1$ //$NON-NLS-2$
						iconFamilies.put(familyName, family);
					}
				}
			}
			finally
			{
				IOUtils.closeQuietly(br);
			}
		}
		
		/// <summary> Process the installed Vassal JarFile to find contained Icons</summary>
		/// <throws>  IOException </throws>
		private void  findJarIcons()
		{
			
			// Path to scalable icons
			//UPGRADE_NOTE: Final was removed from the declaration of 'scalablePath '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String scalablePath = DataArchive.ICON_DIR + IconFamily.SCALABLE_DIR;
			// Path to sized icons
			//UPGRADE_NOTE: Final was removed from the declaration of 'sizePaths '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String[] sizePaths = new System.String[IconFamily.SIZE_COUNT];
			for (int size = 0; size < IconFamily.SIZE_COUNT; size++)
			{
				sizePaths[size] = DataArchive.ICON_DIR + IconFamily.SIZE_DIRS[size];
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'j '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'java.net.JarURLConnection' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetJarURLConnection'"
			JarURLConnection j = (JarURLConnection) System.Net.WebRequest.Create(jar.getURL(DataArchive.IMAGE_DIR));
			//UPGRADE_NOTE: Final was removed from the declaration of 'vengine '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'java.util.jar.JarFile' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautiljarJarFile'"
			//UPGRADE_ISSUE: Method 'java.net.JarURLConnection.getJarFile' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetJarURLConnection'"
			JarFile vengine = j.getJarFile();
			
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
		}
		static IconFactory()
		{
			logger = LoggerFactory.getLogger(typeof(IconFactory));
		}
	}
}