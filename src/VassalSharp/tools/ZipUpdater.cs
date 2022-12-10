/*
* $Id$
*
* Copyright (c) 2003 by Rodney Kinney
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
namespace VassalSharp.tools
{
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import static VassalSharp.tools.IterableEnumeration.iterate;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.BufferedInputStream;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.BufferedOutputStream;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.BufferedReader;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.ByteArrayInputStream;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.ByteArrayOutputStream;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.File;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.FileOutputStream;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.IOException;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.InputStream;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.InputStreamReader;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.Enumeration;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.Properties;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.jar.JarOutputStream;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.zip.CRC32;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.zip.ZipEntry;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.zip.ZipFile;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.zip.ZipOutputStream;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import javax.swing.JOptionPane;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import javax.swing.SwingUtilities;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.slf4j.Logger;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.slf4j.LoggerFactory;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.tools.io.IOUtils;
	
	/// <summary> Automatically builds a .jar file that will update a Zip archive.
	/// Usage:  java VassalSharp.tools.ZipUpdater <oldArchiveName> <newArchiveName>
	/// will create a file named update<oldArchiveName>.jar Executing this jar (by double-clicking or
	/// typing "java -jar update<oldArchiveName>.jar") will update the old archive so that its contents are identical to
	/// the new archive.
	/// User: rkinney
	/// Date: Oct 23, 2003
	/// </summary>
	public class ZipUpdater : IThreadRunnable
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.tools.ZipUpdater'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		public const System.String CHECKSUM_RESOURCE = "checksums";
		public const System.String TARGET_ARCHIVE = "target";
		public const System.String UPDATED_ARCHIVE_NAME = "finalName";
		public const System.String ENTRIES_DIR = "entries/";
		private File oldFile;
		private ZipFile oldZipFile;
		private Properties checkSums;
		
		public ZipUpdater(File input)
		{
			this.oldFile = input;
			if (!oldFile.exists())
			{
				throw new IOException("Could not find file " + input.getPath());
			}
		}
		
		private long getCrc(ZipFile file, ZipEntry entry)
		{
			long crc = - 1;
			if (entry != null)
			{
				crc = entry.getCrc();
				if (crc < 0)
				{
					CRC32 checksum = new CRC32();
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'in '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					InputStream in_Renamed = file.getInputStream(entry);
					try
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'buffer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						sbyte[] buffer = new sbyte[1024];
						int count;
						while ((count = in_Renamed.read(buffer)) >= 0)
						{
							checksum.update(buffer, 0, count);
						}
						in_Renamed.close();
						crc = checksum.getValue();
					}
					finally
					{
						IOUtils.closeQuietly(in_Renamed);
					}
				}
			}
			return crc;
		}
		
		private long copyEntry(ZipOutputStream output, ZipEntry newEntry)
		{
			return writeEntry(oldZipFile.getInputStream(new ZipEntry(newEntry.getName())), output, newEntry);
		}
		
		private long replaceEntry(ZipOutputStream output, ZipEntry newEntry)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'newContents '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'java.lang.Class.getResourceAsStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetResourceAsStream_javalangString'"
			InputStream newContents = GetType().getResourceAsStream("/" + ENTRIES_DIR + newEntry.getName());
			if (newContents == null)
			{
				throw new IOException("This updater was created with an original that differs from the file you're trying to update.\nLocal entry does not match original:  " + newEntry.getName());
			}
			
			BufferedInputStream in_Renamed = null;
			try
			{
				in_Renamed = new BufferedInputStream(newContents);
				long cs = writeEntry(newContents, output, newEntry);
				in_Renamed.close();
				return cs;
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
		}
		
		
		private long writeEntry(InputStream zis, ZipOutputStream output, ZipEntry newEntry)
		{
			// FIXME: is there a better way to do this, so that the whole input
			// stream isn't in memory at once?
			//UPGRADE_NOTE: Final was removed from the declaration of 'contents '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] contents = IOUtils.toByteArray(zis);
			//UPGRADE_NOTE: Final was removed from the declaration of 'checksum '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			CRC32 checksum = new CRC32();
			checksum.update(contents);
			if (newEntry.getMethod() == ZipEntry.STORED)
			{
				newEntry.setSize(contents.Length);
				newEntry.setCrc(checksum.getValue());
			}
			output.putNextEntry(newEntry);
			output.write(contents, 0, contents.Length);
			return checksum.getValue();
		}
		
		public virtual void  write(File destination)
		{
			checkSums = new Properties();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'rin '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'java.lang.Class.getResourceAsStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetResourceAsStream_javalangString'"
			InputStream rin = typeof(ZipUpdater).getResourceAsStream("/" + CHECKSUM_RESOURCE);
			if (rin == null)
				throw new IOException("Resource not found: " + CHECKSUM_RESOURCE);
			
			BufferedInputStream in_Renamed = null;
			try
			{
				in_Renamed = new BufferedInputStream(rin);
				checkSums.load(in_Renamed);
				in_Renamed.close();
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'tempFile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			File tempFile = File.createTempFile("VSL", ".zip");
			
			oldZipFile = new ZipFile(oldFile.getPath());
			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'output '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				ZipOutputStream output = new ZipOutputStream(new FileOutputStream(tempFile));
				try
				{
					// FIXME: reinstate when we move to 1.6+.
					//        for (String entryName : checkSums.stringPropertyNames()) {
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				}
				finally
				{
					try
					{
						output.close();
					}
					// FIXME: review error message
					catch (IOException e)
					{
						logger.error("", e);
					}
				}
			}
			finally
			{
				oldZipFile.close();
			}
			
			if (destination.getName().equals(oldFile.getName()))
			{
				System.String updatedName = destination.getName();
				int index = updatedName.LastIndexOf('.');
				System.String backup = index < 0 || index == updatedName.Length - 1?updatedName + "Backup":updatedName.Substring(0, (index) - (0)) + "Backup" + updatedName.Substring(index);
				if (!oldFile.renameTo(new File(backup)))
				{
					throw new IOException("Unable to create backup file " + backup + ".\nUpdated file is in " + tempFile.getPath());
				}
			}
			if (!tempFile.renameTo(destination))
			{
				throw new IOException("Unable to write to file " + destination.getPath() + ".\nUpdated file is in " + tempFile.getPath());
			}
		}
		
		public virtual void  createUpdater(File newFile)
		{
			System.String inputArchiveName = oldFile.getName();
			int index = inputArchiveName.IndexOf('.');
			System.String jarName;
			if (index >= 0)
			{
				jarName = "update" + inputArchiveName.Substring(0, (index) - (0)) + ".jar";
			}
			else
			{
				jarName = "update" + inputArchiveName;
			}
			createUpdater(newFile, new File(jarName));
		}
		
		public virtual void  createUpdater(File newFile, File updaterFile)
		{
			if (!updaterFile.getName().endsWith(".jar"))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'newName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String newName = updaterFile.getName().replace('.', '_') + ".jar";
				updaterFile = new File(updaterFile.getParentFile(), newName);
			}
			checkSums = new Properties();
			
			try
			{
				oldZipFile = new ZipFile(oldFile);
				//UPGRADE_NOTE: Final was removed from the declaration of 'inputArchiveName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String inputArchiveName = oldFile.getName();
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'goal '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				ZipFile goal = new ZipFile(newFile);
				try
				{
					JarOutputStream out_Renamed = null;
					try
					{
						out_Renamed = new JarOutputStream(new BufferedOutputStream(new FileOutputStream(updaterFile)));
						//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
						for(ZipEntry entry: iterate(goal.entries()))
						{
							//UPGRADE_NOTE: Final was removed from the declaration of 'goalCrc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							long goalCrc = getCrc(goal, entry);
							//UPGRADE_NOTE: Final was removed from the declaration of 'inputCrc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							long inputCrc = getCrc(oldZipFile, oldZipFile.getEntry(entry.getName()));
							if (goalCrc != inputCrc)
							{
								//UPGRADE_NOTE: Final was removed from the declaration of 'outputEntry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
								ZipEntry outputEntry = new ZipEntry(ENTRIES_DIR + entry.getName());
								outputEntry.setMethod(entry.getMethod());
								
								InputStream gis = null;
								try
								{
									gis = new BufferedInputStream(goal.getInputStream(entry));
									writeEntry(gis, out_Renamed, outputEntry);
									gis.close();
								}
								finally
								{
									IOUtils.closeQuietly(gis);
								}
							}
							checkSums.put(entry.getName(), goalCrc + "");
						}
						
						//UPGRADE_NOTE: Final was removed from the declaration of 'manifestEntry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						ZipEntry manifestEntry = new ZipEntry("META-INF/MANIFEST.MF");
						manifestEntry.setMethod(ZipEntry.DEFLATED);
						//UPGRADE_NOTE: Final was removed from the declaration of 'buffer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						StringBuilder buffer = new StringBuilder();
						buffer.append("Manifest-Version: 1.0\n").append("Main-Class: VassalSharp.tools.ZipUpdater\n");
						writeEntry(new ByteArrayInputStream(buffer.toString().getBytes("UTF-8")), out_Renamed, manifestEntry);
						
						//UPGRADE_NOTE: Final was removed from the declaration of 'nameEntry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						ZipEntry nameEntry = new ZipEntry(TARGET_ARCHIVE);
						nameEntry.setMethod(ZipEntry.DEFLATED);
						//UPGRADE_TODO: Method 'java.lang.String.getBytes' was converted to 'System.Text.Encoding.GetEncoding(string).GetBytes(string)' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangStringgetBytes_javalangString'"
						writeEntry(new ByteArrayInputStream(SupportClass.ToSByteArray(System.Text.Encoding.GetEncoding("UTF-8").GetBytes(inputArchiveName))), out_Renamed, nameEntry);
						
						//UPGRADE_NOTE: Final was removed from the declaration of 'updatedEntry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						ZipEntry updatedEntry = new ZipEntry(UPDATED_ARCHIVE_NAME);
						updatedEntry.setMethod(ZipEntry.DEFLATED);
						writeEntry(new ByteArrayInputStream(newFile.getName().getBytes("UTF-8")), out_Renamed, updatedEntry);
						
						//UPGRADE_NOTE: Final was removed from the declaration of 'byteOut '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						ByteArrayOutputStream byteOut = new ByteArrayOutputStream();
						checkSums.store(byteOut, null);
						//UPGRADE_NOTE: Final was removed from the declaration of 'sumEntry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						ZipEntry sumEntry = new ZipEntry(CHECKSUM_RESOURCE);
						sumEntry.setMethod(ZipEntry.DEFLATED);
						writeEntry(new ByteArrayInputStream(byteOut.toByteArray()), out_Renamed, sumEntry);
						
						//UPGRADE_NOTE: Final was removed from the declaration of 'className '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.String className = GetType().FullName.Replace('.', '/') + ".class";
						//UPGRADE_NOTE: Final was removed from the declaration of 'classEntry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						ZipEntry classEntry = new ZipEntry(className);
						classEntry.setMethod(ZipEntry.DEFLATED);
						
						//UPGRADE_NOTE: Final was removed from the declaration of 'is '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						//UPGRADE_ISSUE: Method 'java.lang.Class.getResourceAsStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetResourceAsStream_javalangString'"
						InputStream is_Renamed = GetType().getResourceAsStream("/" + className);
						if (is_Renamed == null)
							throw new IOException("Resource not found: " + className);
						
						BufferedInputStream in_Renamed = null;
						try
						{
							in_Renamed = new BufferedInputStream(is_Renamed);
							writeEntry(is_Renamed, out_Renamed, classEntry);
							in_Renamed.close();
						}
						finally
						{
							IOUtils.closeQuietly(in_Renamed);
						}
						
						out_Renamed.close();
					}
					finally
					{
						IOUtils.closeQuietly(out_Renamed);
					}
					
					goal.close();
				}
				finally
				{
					IOUtils.closeQuietly(goal);
				}
				
				oldZipFile.close();
			}
			finally
			{
				IOUtils.closeQuietly(oldZipFile);
			}
		}
		
		private System.String fileName;
		private System.Exception error;
		
		private ZipUpdater(System.String fileName, System.Exception error)
		{
			this.fileName = fileName;
			this.error = error;
		}
		
		public virtual void  Run()
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			JOptionPane.showMessageDialog(null, "Unable to update " + fileName + ".\n" + error.Message, "Update failed", JOptionPane.ERROR_MESSAGE);
			System.Environment.Exit(0);
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			System.String oldArchiveName = "<unknown>";
			try
			{
				if (args.Length > 1)
				{
					oldArchiveName = args[0];
					System.String goal = args[1];
					ZipUpdater updater = new ZipUpdater(new File(oldArchiveName));
					updater.createUpdater(new File(goal));
				}
				else
				{
					BufferedReader r = null;
					try
					{
						//UPGRADE_ISSUE: Method 'java.lang.Class.getResourceAsStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetResourceAsStream_javalangString'"
						r = new BufferedReader(new InputStreamReader(typeof(ZipUpdater).getResourceAsStream("/" + TARGET_ARCHIVE)));
						oldArchiveName = r.readLine();
						r.close();
					}
					finally
					{
						if (r != null)
						{
							try
							{
								r.close();
							}
							catch (IOException e)
							{
								e.printStackTrace();
							}
						}
					}
					
					try
					{
						//UPGRADE_ISSUE: Method 'java.lang.Class.getResourceAsStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetResourceAsStream_javalangString'"
						r = new BufferedReader(new InputStreamReader(typeof(ZipUpdater).getResourceAsStream("/" + UPDATED_ARCHIVE_NAME)));
						//UPGRADE_NOTE: Final was removed from the declaration of 'newArchiveName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.String newArchiveName = r.readLine();
						//UPGRADE_NOTE: Final was removed from the declaration of 'updater '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						ZipUpdater updater = new ZipUpdater(new File(oldArchiveName));
						updater.write(new File(newArchiveName));
						r.close();
					}
					finally
					{
						if (r != null)
						{
							try
							{
								r.close();
							}
							catch (IOException e)
							{
								e.printStackTrace();
							}
						}
					}
				}
			}
			catch (IOException e)
			{
				e.printStackTrace();
				try
				{
					SwingUtilities.invokeAndWait(new ZipUpdater(oldArchiveName, e));
				}
				catch (System.Exception e1)
				{
					if (e1 is bsh.TargetError)
						((bsh.TargetError) e1).printStackTrace();
					else
						SupportClass.WriteStackTrace(e1, Console.Error);
				}
			}
		}
		static ZipUpdater()
		{
			logger = LoggerFactory.getLogger(typeof(ZipUpdater));
		}
	}
}