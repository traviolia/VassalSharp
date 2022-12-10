/* $Id$
*
* Copyright (c) 2008 by Brent Easton and Joel Uckelman
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
using GameModule = VassalSharp.build.GameModule;
using GameState = VassalSharp.build.module.GameState;
using ImportAction = VassalSharp.tools.imports.ImportAction;
using IOUtils = VassalSharp.tools.io.IOUtils;
#else
using Microsoft.Extensions.Logging;
using System;
#endif

namespace VassalSharp.build.module.metadata
{
#if NEVER_DEFINED
	public class MetaDataFactory
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.build.module.metadata.MetaDataFactory'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;

		protected internal const string BUILDFILE_MODULE_ELEMENT1 = "VassalSharp.launch.BasicModule";
		protected internal const string BUILDFILE_MODULE_ELEMENT2 = "VassalSharp.build.GameModule";
		protected internal const string BUILDFILE_EXTENSION_ELEMENT = "VassalSharp.build.module.ModuleExtension";

		/// <summary> Factory method to build and return an appropriate MetaData class based on
		/// the contents of the file. Return null if the file is not a Zip archive, or
		/// it is not a VASSAL Module, Extension or Save Game.
		/// 
		/// </summary>
		/// <param name="file">metadata file
		/// </param>
		/// <returns> MetaData object
		/// </returns>
		public static AbstractMetaData buildMetaData(System.IO.FileInfo file)
		{

			// Check the file exists and is a file
			bool tmpBool;
			if (System.IO.File.Exists(file.FullName))
				tmpBool = true;
			else
				tmpBool = System.IO.Directory.Exists(file.FullName);
			if (file == null || !tmpBool || !System.IO.File.Exists(file.FullName))
				return null;

			//UPGRADE_ISSUE: Class 'java.util.zip.ZipFile' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
			ZipFile zip = null;
			try
			{
				// Check it is a Zip file
				//UPGRADE_ISSUE: Constructor 'java.util.zip.ZipFile.ZipFile' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
				zip = new ZipFile(file);

				// Check if it is a Save Game file
				//UPGRADE_ISSUE: Class 'java.util.zip.ZipEntry' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipEntry'"
				//UPGRADE_ISSUE: Method 'java.util.zip.ZipFile.getEntry' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
				ZipEntry entry = zip.getEntry(GameState.SAVEFILE_ZIP_ENTRY);
				if (entry != null)
				{
					return new SaveMetaData(zip);
				}

				// Check if it has a buildFile
				//UPGRADE_ISSUE: Class 'java.util.zip.ZipEntry' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipEntry'"
				//UPGRADE_ISSUE: Method 'java.util.zip.ZipFile.getEntry' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
				ZipEntry buildFileEntry = zip.getEntry(GameModule.BUILDFILE);
				if (buildFileEntry == null)
				{
					return null;
				}

				// It's either a module or an Extension, check for existence of metadata
				//UPGRADE_ISSUE: Method 'java.util.zip.ZipFile.getEntry' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
				entry = zip.getEntry(ExtensionMetaData.ZIP_ENTRY_NAME);
				if (entry != null)
				{
					return new ExtensionMetaData(zip);
				}

				//UPGRADE_ISSUE: Method 'java.util.zip.ZipFile.getEntry' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
				entry = zip.getEntry(ModuleMetaData.ZIP_ENTRY_NAME);
				if (entry != null)
				{
					return new ModuleMetaData(zip);
				}

				// read the first few lines of the buildFile
				System.IO.StreamReader br = null;
				try
				{
					//UPGRADE_TODO: The differences in the expected value  of parameters for constructor 'java.io.BufferedReader.BufferedReader'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
					//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
					//UPGRADE_ISSUE: Method 'java.util.zip.ZipFile.getInputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
					br = new System.IO.StreamReader(new System.IO.StreamReader(zip.getInputStream(buildFileEntry), System.Text.Encoding.Default).BaseStream, new System.IO.StreamReader(zip.getInputStream(buildFileEntry), System.Text.Encoding.Default).CurrentEncoding);
					for (int i = 0; i < 10; i++)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						string s = br.ReadLine();
						if (s.IndexOf(BUILDFILE_MODULE_ELEMENT1) > 0 || s.IndexOf(BUILDFILE_MODULE_ELEMENT2) > 0)
						{
							br.Close();
							return new ModuleMetaData(zip);
						}
						else if (s.IndexOf(BUILDFILE_EXTENSION_ELEMENT) > 0)
						{
							br.Close();
							return new ExtensionMetaData(zip);
						}
					}
					br.Close();
				}
				finally
				{
					IOUtils.closeQuietly(br);
				}

				//UPGRADE_ISSUE: Method 'java.util.zip.ZipFile.close' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
				zip.close();
			}
			//UPGRADE_NOTE: Exception ?1 was converted to ?2 which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			catch (System.IO.IOException e)
			{
				// It is not a Zip file, check for an Importable file
				return ImportAction.buildMetaData(file);
			}
			catch (System.IO.IOException e)
			{
				logger.error("", e);
			}
			finally
			{
				IOUtils.closeQuietly(zip);
			}

			return null;
		}
		static MetaDataFactory()
		{
			logger = LoggerFactory.getLogger(typeof(MetaDataFactory));
		}
	}
#else
	public class MetaDataFactory
	{
		/// <summary> Factory method to build and return an appropriate MetaData class based on
		/// the contents of the file. Return null if the file is not a Zip archive, or
		/// it is not a VASSAL Module, Extension or Save Game.
		/// 
		/// </summary>
		/// <param name="file">metadata file
		/// </param>
		/// <returns> MetaData object
		/// </returns>
		public static AbstractMetaData buildMetaData(System.IO.FileInfo file) { return null; }

	}
#endif
}