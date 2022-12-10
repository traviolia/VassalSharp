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
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.chat
{
	
	public abstract class Compressor
	{
		private class AnonymousClassActionListener
		{
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				try
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String s = SupportClass.CommandManager.GetCommand(event_sender);
					System.Console.Error.WriteLine("Input (" + s.Length + ") = " + s); //$NON-NLS-1$ //$NON-NLS-2$
					//UPGRADE_NOTE: Final was removed from the declaration of 'comp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String comp = new System.String(SupportClass.ToCharArray(SupportClass.ToByteArray(VassalSharp.chat.Compressor.compress(SupportClass.ToSByteArray(SupportClass.ToByteArray(s))))));
					System.Console.Error.WriteLine("Compressed (" + comp.Length + ") = " + comp); //$NON-NLS-1$ //$NON-NLS-2$
					//UPGRADE_NOTE: Final was removed from the declaration of 'decomp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String decomp = new System.String(SupportClass.ToCharArray(SupportClass.ToByteArray(VassalSharp.chat.Compressor.decompress(SupportClass.ToSByteArray(SupportClass.ToByteArray(comp))))));
					System.Console.Error.WriteLine("Decompressed (" + decomp.Length + ") = " + decomp); //$NON-NLS-1$ //$NON-NLS-2$
				}
				// FIXME: review error message
				catch (System.IO.IOException ex)
				{
					SupportClass.WriteStackTrace(ex, Console.Error);
				}
			}
		}
		public static sbyte[] compress(sbyte[] in_Renamed)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'byteOut '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.MemoryStream byteOut = new System.IO.MemoryStream();
			//UPGRADE_NOTE: Final was removed from the declaration of 'zipOut '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'java.util.zip.ZipOutputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipOutputStream'"
			//UPGRADE_ISSUE: Constructor 'java.util.zip.ZipOutputStream.ZipOutputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipOutputStream'"
			ZipOutputStream zipOut = new ZipOutputStream(byteOut);
			try
			{
				//UPGRADE_ISSUE: Method 'java.util.zip.ZipOutputStream.putNextEntry' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipOutputStream'"
				//UPGRADE_ISSUE: Constructor 'java.util.zip.ZipEntry.ZipEntry' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipEntry'"
				zipOut.putNextEntry(new ZipEntry("Dummy")); //$NON-NLS-1$
				zipOut.Write(SupportClass.ToByteArray(in_Renamed));
			}
			finally
			{
				try
				{
					//UPGRADE_ISSUE: Method 'java.util.zip.ZipOutputStream.close' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipOutputStream'"
					zipOut.close();
				}
				// FIXME: review error message
				catch (System.IO.IOException e)
				{
					SupportClass.WriteStackTrace(e, Console.Error);
				}
			}
			return SupportClass.ToSByteArray(byteOut.ToArray());
		}
		
		public static sbyte[] decompress(sbyte[] in_Renamed)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'zipIn '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'java.util.zip.ZipInputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipInputStream'"
			//UPGRADE_ISSUE: Constructor 'java.util.zip.ZipInputStream.ZipInputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipInputStream'"
			ZipInputStream zipIn = new ZipInputStream(new System.IO.MemoryStream(SupportClass.ToByteArray(in_Renamed)));
			try
			{
				//UPGRADE_ISSUE: Method 'java.util.zip.ZipInputStream.getNextEntry' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipInputStream'"
				zipIn.getNextEntry();
				return IOUtils.toByteArray(zipIn);
			}
			finally
			{
				try
				{
					//UPGRADE_ISSUE: Method 'java.util.zip.ZipInputStream.close' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipInputStream'"
					zipIn.close();
				}
				// FIXME: review error message
				catch (System.IO.IOException e)
				{
					SupportClass.WriteStackTrace(e, Console.Error);
				}
			}
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			if (args.Length == 0)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
				System.Windows.Forms.Form f = new System.Windows.Forms.Form();
				//UPGRADE_NOTE: Final was removed from the declaration of 'tf '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Constructor 'java.awt.TextField.TextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtTextFieldTextField_int'"
				System.Windows.Forms.TextBox tf = new System.Windows.Forms.TextBox();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				f.Controls.Add(tf);
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				f.pack();
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				f.Visible = true;
				//UPGRADE_TODO: Method 'java.awt.TextField.addActionListener' was converted to 'System.Windows.Forms.TextBox.KeyPress' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtTextFieldaddActionListener_javaawteventActionListener'"
				tf.KeyPress += new System.Windows.Forms.KeyPressEventHandler(new AnonymousClassActionListener().actionPerformed);
			}
			else
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'byteOut '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.IO.MemoryStream byteOut = new System.IO.MemoryStream();
				//UPGRADE_NOTE: Final was removed from the declaration of 'file '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javalangString'"
				System.IO.FileStream file = new System.IO.FileStream(args[0], System.IO.FileMode.Open, System.IO.FileAccess.Read);
				try
				{
					IOUtils.copy(file, byteOut);
				}
				finally
				{
					try
					{
						file.Close();
					}
					// FIXME: review error message
					catch (System.IO.IOException e)
					{
						SupportClass.WriteStackTrace(e, Console.Error);
					}
				}
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'contents '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				sbyte[] contents = SupportClass.ToSByteArray(byteOut.ToArray());
				if (contents[0] == 'P' && contents[1] == 'K')
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'uncompressed '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					sbyte[] uncompressed = Compressor.decompress(contents);
					//UPGRADE_NOTE: Final was removed from the declaration of 'out '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_TODO: Constructor 'java.io.FileOutputStream.FileOutputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileOutputStreamFileOutputStream_javalangString'"
					System.IO.FileStream out_Renamed = new System.IO.FileStream(args[0] + ".uncompressed", System.IO.FileMode.Create); //$NON-NLS-1$
					try
					{
						SupportClass.WriteOutput(out_Renamed, uncompressed);
					}
					finally
					{
						try
						{
							out_Renamed.Close();
						}
						// FIXME: review error message
						catch (System.IO.IOException e)
						{
							SupportClass.WriteStackTrace(e, Console.Error);
						}
					}
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'recompressed '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					sbyte[] recompressed = Compressor.compress(uncompressed);
					if (!SupportClass.ArraySupport.Equals(SupportClass.ToByteArray(recompressed), SupportClass.ToByteArray(contents)))
					{
						// FIXME: don't throw unchecked exception
						throw new System.SystemException("Compression failed"); //$NON-NLS-1$
					}
				}
				else
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'compressed '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					sbyte[] compressed = Compressor.compress(contents);
					//UPGRADE_NOTE: Final was removed from the declaration of 'out '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_TODO: Constructor 'java.io.FileOutputStream.FileOutputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileOutputStreamFileOutputStream_javalangString'"
					System.IO.FileStream out_Renamed = new System.IO.FileStream(args[0] + ".compressed", System.IO.FileMode.Create); //$NON-NLS-1$
					try
					{
						SupportClass.WriteOutput(out_Renamed, compressed);
					}
					finally
					{
						try
						{
							out_Renamed.Close();
						}
						// FIXME: review error message
						catch (System.IO.IOException e)
						{
							SupportClass.WriteStackTrace(e, Console.Error);
						}
					}
					
					if (!SupportClass.ArraySupport.Equals(SupportClass.ToByteArray(Compressor.decompress(compressed)), SupportClass.ToByteArray(contents)))
					{
						// FIXME: don't throw unchecked exception
						throw new System.SystemException("Compression failed"); //$NON-NLS-1$
					}
				}
			}
		}
	}
}