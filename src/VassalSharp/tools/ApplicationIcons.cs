/*
* $Id$
*
* Copyright (c) 2009 by Joel Uckelman
*
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License (LGPL) as published by the Free Software Foundation.
*
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
using System;
//UPGRADE_TODO: The type 'org.apache.commons.lang.SystemUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SystemUtils = org.apache.commons.lang.SystemUtils;
using ImageIOException = VassalSharp.tools.image.ImageIOException;
using ImageUtils = VassalSharp.tools.image.ImageUtils;
namespace VassalSharp.tools
{
	
	
	// FIXME: wizard doesn't get the right icons
	// FIXME: check that parentless dialogs get the right icons
	
	public class ApplicationIcons
	{
		private static System.Windows.Forms.Form IconImages
		{
			set
			{
				try
				{
					setIconImages_Renamed_Field.invoke(value, icons);
				}
				catch (System.UnauthorizedAccessException e)
				{
					ErrorDialog.bug(e);
				}
				catch (System.ArgumentException e)
				{
					ErrorDialog.bug(e);
				}
				catch (System.Reflection.TargetInvocationException e)
				{
					ErrorDialog.bug(e);
				}
				//UPGRADE_NOTE: Exception 'java.lang.ExceptionInInitializerError' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				catch (System.Exception e)
				{
					ErrorDialog.bug(e);
				}
			}
			
		}
		
		public const System.String VASSAL_ICON_LARGE = "VASSAL-256x256.png";
		
		private ApplicationIcons()
		{
		}
		
		//
		// Set up app icons for non-Mac systems
		//
		// The only decent way to set app icons in a variety of sizes is to
		// use Window.setIconImages(), which unforunately is a method new in
		// Java 1.6. We check here whether the method exists and use it if
		// we can. Otherwise, we use the crappy Window.setIconImage() and set
		// a single 16x16 icon. It should not generally be necessary to use
		// setIconImage(), as almost all non-Mac users should be using a Java
		// 6 JRE at this point.
		//
		// FIXME: Use setIconImages() directly on move to Java 1.6 or better
		//
		
		private static System.Reflection.MethodInfo setIconImages_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private static final List < BufferedImage > icons;
		
		public static void  setFor(System.Windows.Forms.Form w)
		{
			if (icons == null)
				return ;
			
			if (setIconImages_Renamed_Field != null)
			{
				IconImages = w;
			}
			else
			{
				// Load a single image as a fallback and watch in horror as it
				// gets scaled to ridiculous sizes.
				w.setIconImage(icons.get_Renamed(0));
			}
		}
		
		public static void  setFor(System.Windows.Forms.Form w)
		{
			if (icons == null)
				return ;
			
			if (setIconImages_Renamed_Field != null)
			{
				IconImages = w;
			}
			else
			{
				// Give up. JDialog has no setIconImage() method.
			}
		}
		static ApplicationIcons()
		{
			{
				if (SystemUtils.IS_OS_MAC_OSX)
				{
					setIconImages_Renamed_Field = null;
					icons = null;
				}
				else
				{
					// get Window.setIconImages() by reflection
					System.Reflection.MethodInfo m = null;
					try
					{
						m = typeof(System.Windows.Forms.Form).getMethod("setIconImages", typeof(System.Collections.IList));
					}
					catch (System.MethodAccessException e)
					{
						// This means we are using Java 1.5.
					}
					
					setIconImages_Renamed_Field = m;
					
					// load the icons
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
					try
					{
						if (setIconImages_Renamed_Field != null)
						{
							if (SystemUtils.IS_OS_WINDOWS)
							{
								// Windows uses 24x24 instead of 22x22
								//UPGRADE_NOTE: Final was removed from the declaration of 'src '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
								System.Drawing.Bitmap src = ImageUtils.getImageResource("/icons/22x22/VassalSharp.png");
								//UPGRADE_NOTE: Final was removed from the declaration of 'dst '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
								System.Drawing.Bitmap dst = ImageUtils.createCompatibleTranslucentImage(24, 24);
								//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
								System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dst);
								//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
								g.DrawImage(src, 1, 1);
								g.Dispose();
								
								l = Arrays.asList(ImageUtils.getImageResource("/icons/16x16/VassalSharp.png"), dst, ImageUtils.getImageResource("/icons/32x32/VassalSharp.png"), ImageUtils.getImageResource("/icons/48x48/VassalSharp.png"), ImageUtils.getImageResource("/images/" + VASSAL_ICON_LARGE));
							}
							else
							{
								// load the standard Tango sizes
								l = Arrays.asList(ImageUtils.getImageResource("/icons/16x16/VassalSharp.png"), ImageUtils.getImageResource("/icons/22x22/VassalSharp.png"), ImageUtils.getImageResource("/icons/32x32/VassalSharp.png"), ImageUtils.getImageResource("/icons/48x48/VassalSharp.png"));
							}
						}
						else
						{
							// we are using Java 1.5, so we can load but a single humble icon
							l = System.Collections.ArrayList.ReadOnly(new System.Collections.ArrayList(new System.Object[]{ImageUtils.getImageResource("/icons/16x16/VassalSharp.png")}));
						}
					}
					catch (ImageIOException e)
					{
						ReadErrorDialog.error(e, e.File);
					}
					icons = l;
				}
			}
		}
	}
}