/*
* $Id$
*
* Copyright (c) 2000-2009 by Rodney Kinney, Joel Uckelman, Brent Easton
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
////////////////////////////////////////////////////////
// These imports are used in deprecated methods only. //
////////////////////////////////////////////////////////

#if NEVER_DEFINED
using System;
using System.Collections.Generic;
////UPGRADE_TODO: The type 'java.io.Closeable' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using Closeable = java.io.Closeable;
//using ImageUtils = VassalSharp.tools.image.ImageUtils;
//using SVGImageUtils = VassalSharp.tools.image.svg.SVGImageUtils;
//using SVGRenderer = VassalSharp.tools.image.svg.SVGRenderer;
////UPGRADE_TODO: The type 'VassalSharp.tools.imageop.ImageOp' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using ImageOp = VassalSharp.tools.imageop.ImageOp;
//using Op = VassalSharp.tools.imageop.Op;
//using RotateScaleOp = VassalSharp.tools.imageop.RotateScaleOp;
//using ScaleOp = VassalSharp.tools.imageop.ScaleOp;
using FileArchive = VassalSharp.tools.io.FileArchive;
//using IOUtils = VassalSharp.tools.io.IOUtils;
//using ZipArchive = VassalSharp.tools.io.ZipArchive;
#else
using System;
using System.Collections.Generic;
//using Closeable = java.io.Closeable;
//using ImageUtils = VassalSharp.tools.image.ImageUtils;
//using SVGImageUtils = VassalSharp.tools.image.svg.SVGImageUtils;
//using SVGRenderer = VassalSharp.tools.image.svg.SVGRenderer;
//using ImageOp = VassalSharp.tools.imageop.ImageOp;
//using Op = VassalSharp.tools.imageop.Op;
//using RotateScaleOp = VassalSharp.tools.imageop.RotateScaleOp;
//using ScaleOp = VassalSharp.tools.imageop.ScaleOp;
using FileArchive = VassalSharp.tools.io.FileArchive;
using IOUtils = VassalSharp.tools.io.IOUtils;
using ZipArchive = VassalSharp.tools.io.ZipArchive;
#endif

namespace VassalSharp.tools
{

#if NEVER_DEFINED
	/// <summary> Wrapper around a Zip archive with methods to cache images</summary>
	//UPGRADE_ISSUE: Class 'java.security.SecureClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javasecuritySecureClassLoader'"
	public class DataArchive //:SecureClassLoader, Closeable
	{
		protected FileArchive archive;

		protected List<DataArchive> extensions = new List<DataArchive>();

		//private void  InitBlock()
		//{
		//	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//	final TreeSet < String > s = new TreeSet < String >();
		//	getImageNamesRecursively(s);
		//	return s;
		//	if (localImages == null)
		//		localImages = getLocalImageNames();
		//	s.addAll(localImages);

		//	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//	for(DataArchive ext: extensions)
		//	{
		//		ext.getImageNamesRecursively(s);
		//	}
		//	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//	final TreeSet < String > s = new TreeSet < String >();

		//	if (archive != null)
		//	{
		//		try
		//		{
		//			//        for (String filename : archive.getFiles(imageDir)) {
		//			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//			for(String filename: archive.getFiles(images))
		//			{
		//				s.add(filename.substring(imageDir.Length));
		//			}
		//		}
		//		catch (System.IO.IOException e)
		//		{
		//			// FIXME: don't swallow this exception!
		//			SupportClass.WriteStackTrace(e, Console.Error);
		//		}
		//	}

		//	return s;
		//	// FIXME: why is this method this way?
		//	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//	Class < ? > c;
		//	try
		//	{
		//		//      c = findSystemClass(name);
		//		c = Class.forName(name);
		//	}
		//	//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		//	catch (System.Exception e)
		//	{
		//		c = findLoadedClass(name);
		//	}

		//	if (c == null)
		//	{
		//		return findClass(name);
		//	}
		//	if (resolve)
		//	{
		//		resolveClass(c);
		//	}
		//	return c; //UPGRADE_NOTE: Final was removed from the declaration of 'slashname '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//	string slashname = name.replace('.', '/');
		//	sbyte[] data = null;

		//	System.IO.Stream stream = null;
		//	try
		//	{
		//		stream = getInputStream(slashname + ".class");
		//		data = IOUtils.toByteArray(stream);
		//	}
		//	catch (System.IO.IOException e)
		//	{
		//		//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		//		throw new System.Exception("Unable to load class " + name, e);
		//	}
		//	finally
		//	{
		//		IOUtils.closeQuietly(stream);
		//	}

		//	//UPGRADE_NOTE: Final was removed from the declaration of 'minor '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//	int minor = (data[4] << 8) | data[5];
		//	//UPGRADE_NOTE: Final was removed from the declaration of 'major '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//	int major = (data[6] << 8) | data[7];

		//	if (major > 49 || (major == 49 && minor != 0))
		//	{
		//		ProblemDialog.showDisableable((int) System.Windows.Forms.MessageBoxIcon.Exclamation, null, null, cs, "Incompatible Custom Code", "The Custom Code In This Module Should Be Recompiled", "This module contains custom Java code (" + name + ") which was not compiled to be Java 5 compatible. As a result, this module will not run on all versions of Java which VASSAL itself supports.\n\nPlease check whether there is an updated version of this module. If not, please contact the maintainer of this module and request that it be fixed.");
		//	}

		//	return defineClass(name, data, 0, data.Length, cs);
		//	return getImageNameSet();
		//}

		virtual public string Name
		{
			get
			{
				//return archive == null?"data archive":archive.Name;
                return null;
			}
			
		}
		//virtual public FileArchive Archive
		//{
		//	get
		//	{
		//		return archive;
		//	}
			
		//}
		virtual public string ImagePrefix
		{
			get
			{
				return imageDir;
			}
			
		}
		virtual public string[] ImageNames
		{
			get
			{
                ////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
                //final SortedSet < String > s = getImageNameSet();
                //return s.toArray(new string[s.size()]);
                return new string[0];
			}
			
		}
		/// <summary> Return the writeable instance of DataArchive, either this or one
		/// of its extensions. (At most one archive should be edited at a time.)
		/// 
		/// </summary>
		/// <returns>
		/// </returns>
		virtual public ArchiveWriter Writer
		{
			get
			{
				if (this is ArchiveWriter)
					return (ArchiveWriter) this;
				
				////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				//for(DataArchive ext: extensions)
				//{
				//	//UPGRADE_NOTE: Final was removed from the declaration of 'writer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//	ArchiveWriter writer = ext.Writer;
				//	if (writer != null)
				//		return writer;
				//}
				
				return null;
			}
			
		}
		virtual protected internal bool NameCacheStale
		{
			get
			{
				return true;
			}
			
		}
		virtual public string ArchiveURL
		{
			get
			{
                //return archive != null?"jar:file://" + archive.Name + "!/":"";
                return null;
			}
			
		}
		
		//protected internal FileArchive archive;
		
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//protected List < DataArchive > extensions = new ArrayList < DataArchive >();
		
		//// FIXME: these should go into a cache, like images have
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//private final Map < String, AudioClip > soundCache = 
		//new HashMap < String, AudioClip >();
		
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//protected SortedSet < String > localImages = null;
		
		public const string IMAGE_DIR = "images/";
		protected internal string imageDir = IMAGE_DIR;
		
		public const string SOUND_DIR = "sounds/";
		protected internal string soundDir = SOUND_DIR;
		
		public const string ICON_DIR = "icons/";
		
		//UPGRADE_ISSUE: Constructor 'java.security.SecureClassLoader.SecureClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javasecuritySecureClassLoader'"
		//UPGRADE_ISSUE: Method 'java.lang.Class.getClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetClassLoader'"
		protected internal DataArchive() // :base(typeof(DataArchive).getClassLoader())
		{
			//InitBlock();
		}
		
		public DataArchive(string zipName, string imageDir):this()
		{
			//archive = new ZipArchive(zipName);
			//this.imageDir = imageDir;
		}
		
		public DataArchive(string zipName):this(zipName, IMAGE_DIR)
		{
		}
		
		////UPGRADE_ISSUE: Interface 'java.applet.AudioClip' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaappletAudioClip'"
		//public virtual AudioClip getCachedAudioClip(string name)
		//{
		//	//UPGRADE_NOTE: Final was removed from the declaration of 'path '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//	string path = soundDir + name;
		//	//UPGRADE_ISSUE: Interface 'java.applet.AudioClip' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaappletAudioClip'"
		//	AudioClip clip = soundCache.get_Renamed(path);
		//	if (clip == null)
		//	{
		//		if (name.ToLower().EndsWith(".mp3"))
		//		{
		//			clip = new Mp3AudioClip(path);
		//		}
		//		else
		//		{
		//			System.IO.Stream stream = null;
		//			try
		//			{
		//				stream = getInputStream(path);
		//				clip = new AppletAudioClip(IOUtils.toByteArray(stream));
		//				soundCache.put(path, clip);
		//			}
		//			finally
		//			{
		//				IOUtils.closeQuietly(stream);
		//			}
		//		}
		//	}
		//	return clip;
		//}
		
		/// <summary> Get an {@link InputStream} for the given image file in the archive.
		/// 
		/// </summary>
		/// <param name="fileName">the name of the image file
		/// </param>
		/// <returns> an <code>InputStream</code> which contains the image file
		/// </returns>
		/// <throws>  IOException if there is a problem reading the image file </throws>
		/// <throws>  FileNotFoundException if the image file doesn't exist </throws>
		/// <deprecated> Use {@link #getInputStream(String)} instead.
		/// </deprecated>
		public virtual System.IO.Stream getImageInputStream(string fileName)
		{
			//// FIXME: We should give notice that we're going to stop searching for
			//// GIFs by appending ".gif" to them. In general, a way of marking obsolete
			//// features would be good---something which pops up a dialog alerting the
			//// user when a module calls a deprecated method, maybe.
			
			//if (fileName.StartsWith("/"))
			//{
			//	//UPGRADE_NOTE: Final was removed from the declaration of 'in '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//	//UPGRADE_ISSUE: Method 'java.lang.Class.getResourceAsStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetResourceAsStream_javalangString'"
			//	System.IO.Stream in_Renamed = GetType().getResourceAsStream(fileName);
			//	if (in_Renamed != null)
			//		return in_Renamed;
			//	throw new System.IO.FileNotFoundException("Resource not found: " + fileName);
			//}
			
			//try
			//{
			//	return getInputStream(imageDir + fileName);
			//}
			//catch (System.IO.FileNotFoundException e)
			//{
			//}
			
			//////////////////////
			//// FIXME: Appending .gif should be considered deprecated behavior.
			////
			//try
			//{
			//	return getInputStream(imageDir + fileName + ".gif");
			//}
			//catch (System.IO.FileNotFoundException e)
			//{
			//}
			
			////UPGRADE_NOTE: Final was removed from the declaration of 'in '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			////UPGRADE_ISSUE: Method 'java.lang.Class.getResourceAsStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetResourceAsStream_javalangString'"
			//System.IO.Stream in_Renamed2 = GetType().getResourceAsStream("/" + imageDir + fileName + ".gif");
			//if (in_Renamed2 != null)
			//	return in_Renamed2;
			////
			/////////////////////
			
			throw new System.IO.FileNotFoundException("\'" + imageDir + fileName + "\' not found in " + Name);
		}
		
		/// <summary> Get an {@link InputStream} for the given file in the archive.
		/// 
		/// </summary>
		/// <param name="fileName">the name of the file
		/// </param>
		/// <returns> an <code>InputStream</code> which contains the file
		/// </returns>
		/// <throws>  IOException if there is a problem reading the file </throws>
		/// <throws>  FileNotFoundException if the file doesn't exist </throws>
		public virtual System.IO.Stream getInputStream(string fileName)
		{
			//// requested file is a resource, try our JARs
			//if (fileName.StartsWith("/"))
			//{
			//	//UPGRADE_NOTE: Final was removed from the declaration of 'in '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//	//UPGRADE_ISSUE: Method 'java.lang.Class.getResourceAsStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetResourceAsStream_javalangString'"
			//	System.IO.Stream in_Renamed = GetType().getResourceAsStream(fileName);
			//	if (in_Renamed != null)
			//	{
			//		return in_Renamed;
			//	}
			//	throw new System.IO.FileNotFoundException("Resource not found: " + fileName);
			//}
			
			//// Look in this archive and its extensions
			//System.IO.Stream in_Renamed2 = getInputStreamImpl(fileName);
			//if (in_Renamed2 != null)
			//{
			//	return in_Renamed2;
			//}
			
			////
			//// Ridiculous crap we have to check for backwards compatibility
			////
			
			//// Maybe it's a resource missing its initial slash. Aauugh!
			////UPGRADE_ISSUE: Method 'java.lang.Class.getResourceAsStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetResourceAsStream_javalangString'"
			//in_Renamed2 = GetType().getResourceAsStream("/" + fileName);
			//if (in_Renamed2 != null)
			//{
			//	return in_Renamed2;
			//}
			
			//// Maybe it's an extensionless GIF? Aauugh!
			//in_Renamed2 = getInputStreamImpl(fileName + ".gif");
			//if (in_Renamed2 != null)
			//{
			//	return in_Renamed2;
			//}
			
			//// Maybe it's an extensionless GIF resource. Aauugh!
			////UPGRADE_ISSUE: Method 'java.lang.Class.getResourceAsStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetResourceAsStream_javalangString'"
			//in_Renamed2 = GetType().getResourceAsStream("/" + fileName + ".gif");
			//if (in_Renamed2 != null)
			//{
			//	return in_Renamed2;
			//}
			
			////
			//// End of ridiculous crap
			////
			
			throw new System.IO.FileNotFoundException("\'" + fileName + "\' not found in " + Name);
		}
		
		private System.IO.Stream getInputStreamImpl(string fileName)
		{
			//// requested file is in this archive
			//if (archive != null && archive.contains(fileName))
			//{
			//	return archive.getInputStream(fileName);
			//}
			
			//// we don't have it, try our extensions
			////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			//for(DataArchive ext: extensions)
			//{
			//	try
			//	{
			//		return ext.getInputStream(fileName);
			//	}
			//	catch (System.IO.FileNotFoundException e)
			//	{
			//		// not found in this extension, try the next
			//	}
			//}
			
			return null;
		}
		
		/// <summary> Returns a URL pointing to the archive.
		/// 
		/// </summary>
		/// <returns> a URL corresponding to this archive
		/// </returns>
		/// <throws>  IOException if the archive has not yet been saved </throws>
		public virtual System.Uri getURL()
		{
            //if (archive == null)
            //{
            //	throw new System.IO.IOException("Must save before accessing contents");
            //}

            //return URLUtils.toJarURL(archive.Name);
            return null;
		}
		
		/// <summary> Returns a URL pointing to the named file.
		/// 
		/// </summary>
		/// <param name="fileName">the name of the file
		/// </param>
		/// <returns> a URL corresponding to the file
		/// </returns>
		/// <throws>  FileNotFoundException if the file doesn't exist </throws>
		/// <throws>  IOException if some other problem occurs </throws>
		public virtual System.Uri getURL(string fileName)
		{
			//// requested file is a resource
			//if (fileName.StartsWith("/"))
			//{
			//	GetType();
			//	//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			//	return new System.Uri(System.IO.Path.GetFullPath(fileName));
			//}
			
			//if (archive == null)
			//{
			//	throw new System.IO.IOException("Must save before accessing contents");
			//}
			
			//if (archive.contains(fileName))
			//{
			//	//UPGRADE_TODO: Class 'java.net.URL' was converted to a 'System.Uri' which does not throw an exception if a URL specifies an unknown protocol. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1132'"
			//	return new System.Uri(getURL(), fileName);
			//}
			
			////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			//for(DataArchive ext: extensions)
			//{
			//	try
			//	{
			//		return ext.getURL(fileName);
			//	}
			//	catch (System.IO.FileNotFoundException e)
			//	{
			//		// not found in this extension, try the next
			//	}
			//}
			
			throw new System.IO.FileNotFoundException("\'" + fileName + "\' not found in " + Name);
		}
		
		/// <summary> Returns a URL pointing to the named image file.
		/// 
		/// </summary>
		/// <param name="fileName">the name of the image file
		/// </param>
		/// <returns> a URL corresponding to the image file
		/// </returns>
		/// <throws>  FileNotFoundException if the file doesn't exist </throws>
		/// <throws>  IOException if some other problem occurs </throws>
		/// <deprecated> Use {@link #getURL(String)} instead.
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public virtual System.Uri getImageURL(string fileName)
		{
			return getURL(fileName);
		}
		
		public virtual bool contains(string fileName)
		{
            //if (archive == null)
            //	return false;
            //return archive.contains(fileName);
            return false;
		}
		
		public virtual void  close()
		{
			//if (archive != null)
			//{
			//	archive.revert(); // ensure that we don't modify the archive
			//	archive.close();
			//}
		}

		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//public SortedSet < String > getImageNameSet()

		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//protected
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//void getImageNamesRecursively(SortedSet < String > s)

		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//protected SortedSet < String > getLocalImageNames()

		///// <summary> DataArchives can extend other archives. The extensions will be
		///// searched for data if not found in the parent archive.
		///// 
		///// </summary>
		///// <param name="ext">the extension
		///// </param>
		//public virtual void  addExtension(DataArchive ext)
		//{
		//	extensions.add(ext);
		//}

		///////////////////////////////////////////////////////////////////////
		//// Methods overridden from SecureClassLoader
		///////////////////////////////////////////////////////////////////////
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Override
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//public synchronized Class < ? > loadClass(String name, boolean resolve) 
		//throws ClassNotFoundException
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Override
		////UPGRADE_ISSUE: Class 'java.security.CodeSource' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javasecurityCodeSource'"
		//protected internal System.Security.PermissionSet getPermissions(CodeSource codesource)
		//{
		//	//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//	//UPGRADE_ISSUE: Method 'java.security.SecureClassLoader.getPermissions' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javasecuritySecureClassLoader'"
		//	System.Security.PermissionSet p = base.getPermissions(codesource);
		//	//UPGRADE_TODO: Constructor 'java.security.AllPermission.AllPermission' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		//	p.AddPermission((System.Security.IPermission) new AllPermission());
		//	return p;
		//}

		////UPGRADE_NOTE: Final was removed from the declaration of 'cs '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		////UPGRADE_ISSUE: Class 'java.security.CodeSource' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javasecurityCodeSource'"
		////UPGRADE_ISSUE: Constructor 'java.security.CodeSource.CodeSource' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javasecurityCodeSource'"
		//private static readonly CodeSource cs = new CodeSource((System.Uri) null, (System.Security.Cryptography.X509Certificates.X509Certificate[]) null);
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Override
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//protected Class < ? > findClass(String name) throws ClassNotFoundException

		///////////////////////////////////////////////////////////////////////
		//// All methods deprecated below this point.
		///////////////////////////////////////////////////////////////////////
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		////UPGRADE_NOTE: Final was removed from the declaration of 'SOUNDS_DIR '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//public static readonly string SOUNDS_DIR = SOUND_DIR;
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//protected internal string soundsDir = SOUND_DIR;
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//private final Map < String, ImageSource > imageSources = 
		//new HashMap < String, ImageSource >();

		///// <summary> Add an ImageSource under the given name, but only if no source is
		///// yet registered under this name.
		///// 
		///// </summary>
		///// <param name="name">
		///// </param>
		///// <param name="src">
		///// </param>
		///// <returns> true if the ImageSource was added, false if it existed already
		///// </returns>
		///// <deprecated>
		///// </deprecated>
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public virtual bool addImageSource(string name, ImageSource src)
		//{
		//	if (!imageSources.containsKey(name))
		//	{
		//		imageSources.put(name, src);
		//		localImages = null;
		//		return true;
		//	}
		//	return false;
		//}
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public virtual void  removeImageSource(string name)
		//{
		//	imageSources.remove(name);
		//	localImages = null;
		//}

		///// <summary> Get the size of an image without loading and decoding it.
		///// 
		///// </summary>
		///// <param name="name">filename of the image
		///// </param>
		///// <returns> the size of the image
		///// </returns>
		///// <deprecated> Use {@link ImageUtils.getImageSize} or
		///// {@link SVGImageUtils.getImageSize} instead.
		///// </deprecated>
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public virtual System.Drawing.Size getImageSize(string name)
		//{
		//	//UPGRADE_NOTE: Final was removed from the declaration of 'src '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//	ImageSource src;

		//	if (name.StartsWith("/"))
		//	{
		//		if (name.ToLower().EndsWith(".svg"))
		//			return SVGImageUtils.getImageSize(name, getImageInputStream(name));
		//		else
		//			return ImageUtils.getImageSize(name, getImageInputStream(name));
		//	}
		//	else if ((src = imageSources.get_Renamed(name)) != null)
		//	{
		//		//UPGRADE_NOTE: Final was removed from the declaration of 'image '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//		System.Drawing.Image image = src.Image;
		//		return image != null?new System.Drawing.Size(image.Width, image.Height):new System.Drawing.Size(0, 0);
		//	}
		//	else if (name.ToLower().EndsWith(".svg"))
		//	{
		//		return SVGImageUtils.getImageSize(name, getImageInputStream(name));
		//	}
		//	else
		//	{
		//		return ImageUtils.getImageSize(name, getImageInputStream(name));
		//	}
		//}

		///// <summary> Returns an {@link Image} from the archive.
		///// 
		///// </summary>
		///// <param name="name">the name of the image file
		///// </param>
		///// <returns> the <code>Image</code> contained in the image file
		///// </returns>
		///// <throws>  IOException if there is a problem reading the image file </throws>
		///// <deprecated> Use {@link ImageUtils.getImage} or
		///// {@link SVGImageUtils.getImage} instead.
		///// </deprecated>
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public virtual System.Drawing.Bitmap getImage(string name)
		//{
		//	//UPGRADE_NOTE: Final was removed from the declaration of 'src '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//	ImageSource src;

		//	if (name.StartsWith("/"))
		//	{
		//		if (name.ToLower().EndsWith(".svg"))
		//		{
		//			return new SVGRenderer(getURL(name), getImageInputStream(name)).render();
		//		}
		//		else
		//		{
		//			return ImageUtils.getImage(name, getImageInputStream(name));
		//		}
		//	}
		//	else if ((src = imageSources.get_Renamed(name)) != null)
		//	{
		//		return ImageUtils.toBufferedImage(src.Image);
		//	}
		//	else if (name.ToLower().EndsWith(".svg"))
		//	{
		//		return new SVGRenderer(getURL(name), getImageInputStream(name)).render();
		//	}
		//	else
		//	{
		//		return ImageUtils.getImage(name, getImageInputStream(name));
		//	}
		//}

		///// <summary> Does the actual work of transforming an image.</summary>
		///*
		//@Deprecated
		//protected Image createTransformedInstance(Image im, double zoom,
		//double theta) {
		//// get smoothing preferences
		//if (smoothPrefs == null) {
		//smoothPrefs = (BooleanConfigurer) GameModule.getGameModule()
		//.getPrefs().getOption(GlobalOptions.SCALER_ALGORITHM);
		//if (smoothPrefs == null) {
		//smoothPrefs = new BooleanConfigurer(null, null, Boolean.FALSE);
		//}
		//smoothPrefs.addPropertyChangeListener(new PropertyChangeListener() {
		//public void propertyChange(PropertyChangeEvent evt) {
		//clearTransformedImageCache();
		//}
		//});
		//}

		//final boolean smooth = Boolean.TRUE.equals(smoothPrefs.getValue());
		//return new RotateScaleOp(new ImageSourceOp(im), theta, zoom).getImage(null);
		//}*/
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//protected internal string[] imageNames;
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated

		///// <returns> the names of the image files stored in this DataArchive
		///// and its extensions
		///// </returns>
		///// <deprecated> Use {@link #getImageNameSet()} instead.
		///// </deprecated>
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//protected SortedSet < String > setOfImageNames()

		//// FIXME: hook these up to ImageOp methods
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public virtual void  unCacheImage(string file)
		//{
		//}
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public virtual void  unCacheImage(System.Drawing.Image im)
		//{
		//}
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public virtual void  clearTransformedImageCache()
		//{
		//}
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public virtual void  clearScaledImageCache()
		//{
		//}

		///// <summary> Find an image from the archive
		///// Once an image is found, cache it in our HashMap.
		///// </summary>
		///// <deprecated> Use {@link ImageOp}s instead.
		///// </deprecated>
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public virtual System.Drawing.Image getCachedImage(string name)
		//{
		//	// An ugly hack, but nothing should be using this method anyway.
		//	return Op.load(name).getImage();
		//}

		///// <summary> Return a transformed instance of the image.
		///// The image will be retrieved from the cache if available, and cached
		///// after retrieval if not.
		///// </summary>
		///// <param name="base">the untransformed Image
		///// </param>
		///// <param name="scale">the scaling factor
		///// </param>
		///// <param name="theta">the angle of rotation (in degrees) about the Image center
		///// </param>
		///// <deprecated> Use {@link RotateScaleOp} instead.
		///// </deprecated>
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public virtual System.Drawing.Image getTransformedImage(System.Drawing.Image base_Renamed, double scale, double theta)
		//{
		//	// An ugly hack, but nothing should be using this method anyway.
		//	return Op.rotateScale(Op.load(ImageUtils.toBufferedImage(base_Renamed)), theta, scale).getImage();
		//}

		///// <deprecated> Use {@link RotateScaleOp} instead.
		///// </deprecated>
		///// <param name="base">
		///// </param>
		///// <param name="scale">
		///// </param>
		///// <param name="theta">
		///// </param>
		///// <param name="forceSmoothing">
		///// </param>
		///// <returns>
		///// </returns>
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public virtual System.Drawing.Image getTransformedImage(System.Drawing.Image base_Renamed, double scale, double theta, bool forceSmoothing)
		//{
		//	return getTransformedImage(base_Renamed, scale, theta);
		//}

		///// <deprecated> Use {@link ScaleOp} instead.
		///// The image will be retrieved from cache if available, cached otherwise
		///// </deprecated>
		///// <param name="base">
		///// </param>
		///// <param name="scale">
		///// </param>
		///// <param name="reversed">
		///// </param>
		///// <param name="forceSmoothing">If true, force smoothing.
		///// This usually yields better results, but can be slow for large images
		///// </param>
		///// <returns>
		///// </returns>
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public virtual System.Drawing.Image getScaledImage(System.Drawing.Image base_Renamed, double scale, bool reversed, bool forceSmoothing)
		//{
		//	return getTransformedImage(base_Renamed, scale, reversed?180.0:0.0);
		//}

		///// <summary> Return a scaled instance of the image.
		///// The image will be retrieved from cache if available, cached otherwise
		///// </summary>
		///// <param name="base">
		///// </param>
		///// <param name="scale">
		///// </param>
		///// <returns>
		///// </returns>
		///// <deprecated> Use {@link ScaleOp} instead.
		///// </deprecated>
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public virtual System.Drawing.Image getScaledImage(System.Drawing.Image base_Renamed, double scale)
		//{
		//	return getTransformedImage(base_Renamed, scale, 0.0, true);
		//}

		///// <deprecated> Use {@link #getImage} instead.
		///// </deprecated>
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public static System.Drawing.Image findImage(System.IO.FileInfo zip, string file)
		//{
		//	return getImage(getFileStream(zip, file));
		//}

		///// <deprecated> Use {@link #getImage} instead.
		///// </deprecated>
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public static System.Drawing.Image findImage(System.IO.FileInfo dir, string zip, string file)
		//{
		//	/*
		//	** Looks for entry "file" in ZipFile "zip" in directory "dir"
		//	** If no such zipfile, look for "file" in "dir"
		//	*/
		//	bool tmpBool;
		//	if (System.IO.File.Exists((new System.IO.FileInfo(dir.FullName + "\\" + zip)).FullName))
		//		tmpBool = true;
		//	else
		//		tmpBool = System.IO.Directory.Exists((new System.IO.FileInfo(dir.FullName + "\\" + zip)).FullName);
		//	if (tmpBool)
		//	{
		//		return getImage(getFileStream(dir, zip, file));
		//	}
		//	else
		//	{
		//		bool tmpBool2;
		//		if (System.IO.File.Exists((new System.IO.FileInfo(dir.FullName + "\\" + file)).FullName))
		//			tmpBool2 = true;
		//		else
		//			tmpBool2 = System.IO.Directory.Exists((new System.IO.FileInfo(dir.FullName + "\\" + file)).FullName);
		//		if (tmpBool2)
		//		{
		//			//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDefaultToolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
		//			Toolkit.getDefaultToolkit();
		//			return System.Drawing.Image.FromFile(dir.FullName + System.IO.Path.DirectorySeparatorChar + file);
		//		}
		//		else
		//		{
		//			throw new System.IO.IOException("Image " + file + " not found in " + dir + System.IO.Path.DirectorySeparatorChar.ToString() + zip);
		//		}
		//	}
		//}

		///// <deprecated> Use {@link #getFileStream(String)} instead.
		///// </deprecated>
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public static System.IO.Stream getFileStream(System.IO.FileInfo zip, string file)
		//{
		//	try
		//	{
		//		//UPGRADE_NOTE: Final was removed from the declaration of 'z '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//		//UPGRADE_ISSUE: Class 'java.util.zip.ZipFile' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
		//		//UPGRADE_ISSUE: Constructor 'java.util.zip.ZipFile.ZipFile' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
		//		ZipFile z = new ZipFile(zip);
		//		//UPGRADE_ISSUE: Method 'java.util.zip.ZipFile.getInputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
		//		//UPGRADE_ISSUE: Method 'java.util.zip.ZipFile.getEntry' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
		//		return z.getInputStream(z.getEntry(file));
		//	}
		//	catch (System.Exception e)
		//	{
		//		//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
		//		throw new System.IO.IOException("Couldn't locate " + file + " in " + zip.Name + ": " + e.Message);
		//	}
		//}

		///// <deprecated> Use {@link #getFileStream(String)} instead.
		///// </deprecated>
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public static System.IO.Stream getFileStream(System.IO.FileInfo dir, string zipName, string file)
		//{
		//	try
		//	{
		//		bool tmpBool;
		//		if (System.IO.File.Exists((new System.IO.FileInfo(dir.FullName + "\\" + zipName)).FullName))
		//			tmpBool = true;
		//		else
		//			tmpBool = System.IO.Directory.Exists((new System.IO.FileInfo(dir.FullName + "\\" + zipName)).FullName);
		//		if (tmpBool)
		//		{
		//			//UPGRADE_NOTE: Final was removed from the declaration of 'zip '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//			//UPGRADE_ISSUE: Class 'java.util.zip.ZipFile' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
		//			//UPGRADE_ISSUE: Constructor 'java.util.zip.ZipFile.ZipFile' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
		//			ZipFile zip = new ZipFile(new System.IO.FileInfo(dir.FullName + "\\" + zipName));
		//			//UPGRADE_ISSUE: Method 'java.util.zip.ZipFile.getInputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
		//			//UPGRADE_ISSUE: Method 'java.util.zip.ZipFile.getEntry' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
		//			return zip.getInputStream(zip.getEntry(file));
		//		}
		//		else
		//		{
		//			//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javaioFile'"
		//			return new System.IO.FileStream(new System.IO.FileInfo(dir.FullName + "\\" + file).FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
		//		}
		//	}
		//	catch (System.IO.IOException e)
		//	{
		//		return null;
		//	}
		//}

		/// <summary> Get an {@link InputStream} for the given filename in the archive.
		/// 
		/// </summary>
		/// <deprecated> Use {@link #getInputStream(String)} instead.
		/// </deprecated>
		public virtual System.IO.Stream getFileStream(string fileName)
		{
			return getInputStream(fileName);
		}

		///// <summary>Use {@link ImageUtils.getImage(InputStream)} instead. </summary>
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public static System.Drawing.Image getImage(System.IO.Stream in_Renamed)
		//{
		//	return ImageUtils.getImage(in_Renamed);
		//}

		///// <deprecated> Use {@link getURL()} instead. 
		///// </deprecated>
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated

		///// <summary> Read all available bytes from the given InputStream.</summary>
		///// <deprecated> Use {@link IOUtils.toBytesArray(InputStream)} instead.
		///// </deprecated>
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public static sbyte[] getBytes(System.IO.Stream in_Renamed)
		//{
		//	return IOUtils.toByteArray(in_Renamed);
		//}

		///// <summary> Place the names of the image files stored in this DataArchive into
		///// the argument Collection
		///// </summary>
		///// <param name="l">
		///// </param>
		///// <deprecated> Use {@link #listImageNames()} instead.
		///// </deprecated>
		//internal Deprecated SuppressWarnings;
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//(unchecked)
		//protected internal virtual void  listImageNames(System.Collections.ICollection l)
		//{
		//	l.addAll(setOfImageNames());
		//}

		///// <summary> </summary>
		///// <param name="im">
		///// </param>
		///// <returns> the boundaries of this image,
		///// where (0,0) is the center of the image
		///// </returns>
		///// <deprecated> Use {@link ImageUtils.getBounds(BufferedImage)} instead.
		///// </deprecated>
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public static System.Drawing.Rectangle getImageBounds(System.Drawing.Image im)
		//{
		//	//UPGRADE_TODO: Class 'javax.swing.ImageIcon' was converted to 'System.Drawing.Image' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingImageIcon'"
		//	System.Drawing.Image icon = (System.Drawing.Image) im.Clone();
		//	//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
		//	return new System.Drawing.Rectangle((- (int) SupportClass.GetPropertyAsVirtual(icon, "Width")) / 2, (- (int) SupportClass.GetPropertyAsVirtual(icon, "Height")) / 2, (int) SupportClass.GetPropertyAsVirtual(icon, "Width"), (int) SupportClass.GetPropertyAsVirtual(icon, "Height"));
		//}

		///// <deprecated> Don't use this. We've switched to Lanczos scaling.
		///// </deprecated>
		////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//Deprecated
		//public virtual System.Drawing.Image improvedScaling(System.Drawing.Image img, int width, int height)
		//{
		//	System.Object filter;

		//	filter = new ImprovedAveragingScaleFilter(img.Width, img.Height, width, height);

		//	System.Drawing.Image prod;
		//	prod = SupportClass.ImageSupport.FilterImage(img, filter);
		//	//UPGRADE_ISSUE: Method 'java.awt.Toolkit.createImage' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
		//	//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDefaultToolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
		//	return Toolkit.getDefaultToolkit().createImage(prod);
		//}
	}

#else
	/// <summary> Wrapper around a Zip archive with methods to cache images</summary>
	public class DataArchive //:SecureClassLoader, Closeable
	{
		protected FileArchive archive;

		protected List<DataArchive> extensions = new List<DataArchive>();

		virtual public string Name
		{
			get
			{
                return null;
			}
		}

        virtual public FileArchive Archive
        {
            get
            {
				return null;
            }
        }

        virtual public string ImagePrefix
		{
			get
			{
				return imageDir;
			}
		}

		virtual public string[] ImageNames
		{
			get
			{
                return new string[0];
			}
		}

		/// <summary> Return the writeable instance of DataArchive, either this or one
		/// of its extensions. (At most one archive should be edited at a time.)
		/// 
		/// </summary>
		/// <returns>
		/// </returns>
		virtual public ArchiveWriter Writer
		{
			get
			{
				return null;
			}			
		}

		virtual protected internal bool NameCacheStale
		{
			get
			{
				return true;
			}		
		}

		virtual public string ArchiveURL
		{
			get
			{
                return null;
			}
		}
		
		public const string IMAGE_DIR = "images/";
		protected internal string imageDir = IMAGE_DIR;
		
		public const string SOUND_DIR = "sounds/";
		protected internal string soundDir = SOUND_DIR;
		
		public const string ICON_DIR = "icons/";
		
		public DataArchive(string zipName, string imageDir)
		{
		}
		
		public DataArchive(string zipName) : this(zipName, IMAGE_DIR)
		{
		}
		
		////UPGRADE_ISSUE: Interface 'java.applet.AudioClip' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaappletAudioClip'"
		//public virtual AudioClip getCachedAudioClip(string name)
		//{
		//	//UPGRADE_NOTE: Final was removed from the declaration of 'path '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//	string path = soundDir + name;
		//	//UPGRADE_ISSUE: Interface 'java.applet.AudioClip' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaappletAudioClip'"
		//	AudioClip clip = soundCache.get_Renamed(path);
		//	if (clip == null)
		//	{
		//		if (name.ToLower().EndsWith(".mp3"))
		//		{
		//			clip = new Mp3AudioClip(path);
		//		}
		//		else
		//		{
		//			System.IO.Stream stream = null;
		//			try
		//			{
		//				stream = getInputStream(path);
		//				clip = new AppletAudioClip(IOUtils.toByteArray(stream));
		//				soundCache.put(path, clip);
		//			}
		//			finally
		//			{
		//				IOUtils.closeQuietly(stream);
		//			}
		//		}
		//	}
		//	return clip;
		//}
		
		/// <summary> Get an {@link InputStream} for the given image file in the archive.
		/// 
		/// </summary>
		/// <param name="fileName">the name of the image file
		/// </param>
		/// <returns> an <code>InputStream</code> which contains the image file
		/// </returns>
		/// <throws>  IOException if there is a problem reading the image file </throws>
		/// <throws>  FileNotFoundException if the image file doesn't exist </throws>
		/// <deprecated> Use {@link #getInputStream(String)} instead.
		/// </deprecated>
		public virtual System.IO.Stream getImageInputStream(string fileName)
		{
			return null;
		}
		
		/// <summary> Get an {@link InputStream} for the given file in the archive.
		/// 
		/// </summary>
		/// <param name="fileName">the name of the file
		/// </param>
		/// <returns> an <code>InputStream</code> which contains the file
		/// </returns>
		/// <throws>  IOException if there is a problem reading the file </throws>
		/// <throws>  FileNotFoundException if the file doesn't exist </throws>
		public virtual System.IO.Stream getInputStream(string fileName)
		{
			return null;
		}

		private System.IO.Stream getInputStreamImpl(string fileName)
		{
			return null;
		}
		
		/// <summary> Returns a URL pointing to the archive.
		/// 
		/// </summary>
		/// <returns> a URL corresponding to this archive
		/// </returns>
		/// <throws>  IOException if the archive has not yet been saved </throws>
		public virtual System.Uri getURL()
		{
            return null;
		}
		
		/// <summary> Returns a URL pointing to the named file.
		/// 
		/// </summary>
		/// <param name="fileName">the name of the file
		/// </param>
		/// <returns> a URL corresponding to the file
		/// </returns>
		/// <throws>  FileNotFoundException if the file doesn't exist </throws>
		/// <throws>  IOException if some other problem occurs </throws>
		public virtual System.Uri getURL(string fileName)
		{
			return null;
		}

		/// <summary> Returns a URL pointing to the named image file.
		/// 
		/// </summary>
		/// <param name="fileName">the name of the image file
		/// </param>
		/// <returns> a URL corresponding to the image file
		/// </returns>
		/// <throws>  FileNotFoundException if the file doesn't exist </throws>
		/// <throws>  IOException if some other problem occurs </throws>
		/// <deprecated> Use {@link #getURL(String)} instead.
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public virtual System.Uri getImageURL(string fileName)
		{
			return getURL(fileName);
		}
		
		public virtual bool contains(string fileName)
		{
            return false;
		}
		
		public virtual void  close()
		{
		}

        public SortedSet<String> getImageNameSet() { return null; }

        /// <summary> DataArchives can extend other archives. The extensions will be
        /// searched for data if not found in the parent archive.
        /// 
        /// </summary>
        /// <param name="ext">the extension
        /// </param>
        public virtual void addExtension(DataArchive ext)
        {
        }

        /////////////////////////////////////////////////////////////////////
        // All methods deprecated below this point.
        /////////////////////////////////////////////////////////////////////
        public static readonly string SOUNDS_DIR = SOUND_DIR;

        /// <summary> Add an ImageSource under the given name, but only if no source is
        /// yet registered under this name.
        /// 
        /// </summary>
        /// <param name="name">
        /// </param>
        /// <param name="src">
        /// </param>
        /// <returns> true if the ImageSource was added, false if it existed already
        /// </returns>
        /// <deprecated>
        /// </deprecated>
        public virtual bool addImageSource(string name, ImageSource src)
        {
            return false;
        }

        public virtual void removeImageSource(string name)
        {
        }

        /// <summary> Get the size of an image without loading and decoding it.
        /// 
        /// </summary>
        /// <param name="name">filename of the image
        /// </param>
        /// <returns> the size of the image
        /// </returns>
        /// <deprecated> Use {@link ImageUtils.getImageSize} or
        /// {@link SVGImageUtils.getImageSize} instead.
        /// </deprecated>
        public virtual System.Drawing.Size getImageSize(string name)
        {
			return default(System.Drawing.Size);
		}

		/// <summary> Returns an {@link Image} from the archive.
		/// 
		/// </summary>
		/// <param name="name">the name of the image file
		/// </param>
		/// <returns> the <code>Image</code> contained in the image file
		/// </returns>
		/// <throws>  IOException if there is a problem reading the image file </throws>
		/// <deprecated> Use {@link ImageUtils.getImage} or
		/// {@link SVGImageUtils.getImage} instead.
		/// </deprecated>
		public virtual System.Drawing.Bitmap getImage(string name)
        {
			return null;
		}

		public virtual void unCacheImage(string file)
        {
        }

        public virtual void unCacheImage(System.Drawing.Image im)
        {
        }

        public virtual void clearTransformedImageCache()
        {
        }

        public virtual void clearScaledImageCache()
        {
        }

        /// <summary> Find an image from the archive
        /// Once an image is found, cache it in our HashMap.
        /// </summary>
        /// <deprecated> Use {@link ImageOp}s instead.
        /// </deprecated>
        public virtual System.Drawing.Image getCachedImage(string name)
        {
			return null;
		}

		/// <summary> Return a transformed instance of the image.
		/// The image will be retrieved from the cache if available, and cached
		/// after retrieval if not.
		/// </summary>
		/// <param name="base">the untransformed Image
		/// </param>
		/// <param name="scale">the scaling factor
		/// </param>
		/// <param name="theta">the angle of rotation (in degrees) about the Image center
		/// </param>
		/// <deprecated> Use {@link RotateScaleOp} instead.
		/// </deprecated>
		public virtual System.Drawing.Image getTransformedImage(System.Drawing.Image base_Renamed, double scale, double theta)
        {
			return null;
		}

		/// <deprecated> Use {@link RotateScaleOp} instead.
		/// </deprecated>
		/// <param name="base">
		/// </param>
		/// <param name="scale">
		/// </param>
		/// <param name="theta">
		/// </param>
		/// <param name="forceSmoothing">
		/// </param>
		/// <returns>
		/// </returns>
		public virtual System.Drawing.Image getTransformedImage(System.Drawing.Image base_Renamed, double scale, double theta, bool forceSmoothing)
        {
			return null;
		}

		/// <deprecated> Use {@link ScaleOp} instead.
		/// The image will be retrieved from cache if available, cached otherwise
		/// </deprecated>
		/// <param name="base">
		/// </param>
		/// <param name="scale">
		/// </param>
		/// <param name="reversed">
		/// </param>
		/// <param name="forceSmoothing">If true, force smoothing.
		/// This usually yields better results, but can be slow for large images
		/// </param>
		/// <returns>
		/// </returns>
		public virtual System.Drawing.Image getScaledImage(System.Drawing.Image base_Renamed, double scale, bool reversed, bool forceSmoothing)
        {
			return null;
		}

		/// <summary> Return a scaled instance of the image.
		/// The image will be retrieved from cache if available, cached otherwise
		/// </summary>
		/// <param name="base">
		/// </param>
		/// <param name="scale">
		/// </param>
		/// <returns>
		/// </returns>
		/// <deprecated> Use {@link ScaleOp} instead.
		/// </deprecated>
		public virtual System.Drawing.Image getScaledImage(System.Drawing.Image base_Renamed, double scale)
        {
			return null;
		}

		/// <deprecated> Use {@link #getImage} instead.
		/// </deprecated>
		public static System.Drawing.Image findImage(System.IO.FileInfo zip, string file)
        {
			return null;
		}

		/// <deprecated> Use {@link #getImage} instead.
		/// </deprecated>
		public static System.Drawing.Image findImage(System.IO.FileInfo dir, string zip, string file)
        {
			return null;
		}

		/// <deprecated> Use {@link #getFileStream(String)} instead.
		/// </deprecated>
		public static System.IO.Stream getFileStream(System.IO.FileInfo zip, string file)
		{
			return null;
		}

		/// <deprecated> Use {@link #getFileStream(String)} instead.
		/// </deprecated>
		public static System.IO.Stream getFileStream(System.IO.FileInfo dir, string zipName, string file)
        {
			return null;
		}

		public virtual System.IO.Stream getFileStream(string fileName)
        {
			return null;
		}

		/// <summary>Use {@link ImageUtils.getImage(InputStream)} instead. </summary>
		public static System.Drawing.Image getImage(System.IO.Stream in_Renamed)
        {
			return null;
		}

		/// <summary> Read all available bytes from the given InputStream.</summary>
		/// <deprecated> Use {@link IOUtils.toBytesArray(InputStream)} instead.
		/// </deprecated>
		public static sbyte[] getBytes(System.IO.Stream in_Renamed)
        {
			return null;
		}


		/// <summary> </summary>
		/// <param name="im">
		/// </param>
		/// <returns> the boundaries of this image,
		/// where (0,0) is the center of the image
		/// </returns>
		/// <deprecated> Use {@link ImageUtils.getBounds(BufferedImage)} instead.
		/// </deprecated>
		public static System.Drawing.Rectangle getImageBounds(System.Drawing.Image im)
        {
			return default(System.Drawing.Rectangle);
		}

		/// <deprecated> Don't use this. We've switched to Lanczos scaling.
		/// </deprecated>
		public virtual System.Drawing.Image improvedScaling(System.Drawing.Image img, int width, int height)
        {
			return null;
		}
    }
#endif
}