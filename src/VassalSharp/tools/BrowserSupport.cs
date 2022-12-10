/*
* $Id$
*
* Copyright (c) 2000-2011 by Rodney Kinney, Joel Uckelman
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
//UPGRADE_TODO: The type 'java.awt.Desktop' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Desktop = java.awt.Desktop;
//UPGRADE_TODO: The type 'java.net.URI' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using URI = java.net.URI;
//UPGRADE_TODO: The type 'java.net.URISyntaxException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using URISyntaxException = java.net.URISyntaxException;
//UPGRADE_TODO: The type 'edu.stanford.ejalbert.BrowserLauncher' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using BrowserLauncher = edu.stanford.ejalbert.BrowserLauncher;
//UPGRADE_TODO: The type 'edu.stanford.ejalbert.exception.BrowserLaunchingInitializingException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using BrowserLaunchingInitializingException = edu.stanford.ejalbert.exception.BrowserLaunchingInitializingException;
//UPGRADE_TODO: The type 'edu.stanford.ejalbert.exception.UnsupportedOperatingSystemException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using UnsupportedOperatingSystemException = edu.stanford.ejalbert.exception.UnsupportedOperatingSystemException;
//UPGRADE_TODO: The type 'org.apache.commons.lang.SystemUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SystemUtils = org.apache.commons.lang.SystemUtils;
namespace VassalSharp.tools
{
	
	// FIXME: Remove BrowserLauncher when we move to Java 1.6+.
	
	/// <summary> Utility class for displaying an external browser window.
	/// 
	/// </summary>
	/// <author>  rkinney
	/// </author>
	public class BrowserSupport
	{
		internal class AnonymousClassHyperlinkListener
		{
			public virtual void  hyperlinkUpdate(System.Object event_sender, System.Windows.Forms.LinkClickedEventArgs e)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.event.HyperlinkEvent.getEventType' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventHyperlinkEventgetEventType'"
				//UPGRADE_ISSUE: Field 'javax.swing.event.HyperlinkEvent.EventType.ACTIVATED' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventHyperlinkEventEventType'"
				if (e.getEventType() == HyperlinkEvent.EventType.ACTIVATED)
				{
					VassalSharp.tools.BrowserSupport.openURL(new System.Uri(e.LinkText).ToString());
				}
			}
		}
		//UPGRADE_TODO: Interface 'javax.swing.event.HyperlinkListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public static HyperlinkListener Listener
		{
			get
			{
				return listener;
			}
			
		}
		
		private static BrowserLauncher browserLauncher;
		
		public static void  openURL(System.String url)
		{
			//
			// This method is irritatingly complex, because:
			// * There is no java.awt.Desktop in Java 1.5.
			// * java.awt.Desktop seems not to work sometimes on Windows.
			// * BrowserLauncher failes sometimes on Linux, and isn't supported
			//   anymore.
			//
			if (!SystemUtils.IS_JAVA_1_5)
			{
				if (Desktop.isDesktopSupported())
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'desktop '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Desktop desktop = Desktop.getDesktop();
					if (desktop.isSupported(Desktop.Action.BROWSE))
					{
						try
						{
							desktop.browse(new URI(url));
							return ;
						}
						catch (System.IO.IOException e)
						{
							// We ignore this on Windows, because Desktop seems flaky
							if (!SystemUtils.IS_OS_WINDOWS)
							{
								ReadErrorDialog.error(e, url);
								return ;
							}
						}
						catch (System.ArgumentException e)
						{
							ErrorDialog.bug(e);
							return ;
						}
						catch (URISyntaxException e)
						{
							ErrorDialog.bug(e);
							return ;
						}
					}
				}
			}
			
			if (browserLauncher != null)
			{
				browserLauncher.openURLinBrowser(url);
			}
			else
			{
				ErrorDialog.show("BrowserSupport.unable_to_launch", url);
			}
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'listener '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Interface 'javax.swing.event.HyperlinkListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		//UPGRADE_NOTE: The initialization of  'listener' was moved to static method 'VassalSharp.tools.BrowserSupport'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly HyperlinkListener listener;
		static BrowserSupport()
		{
			{
				BrowserLauncher bl = null;
				
				if (SystemUtils.IS_JAVA_1_5)
				{
					try
					{
						bl = new BrowserLauncher();
					}
					catch (BrowserLaunchingInitializingException e)
					{
						ErrorDialog.bug(e);
					}
					catch (UnsupportedOperatingSystemException e)
					{
						ErrorDialog.bug(e);
					}
				}
				
				browserLauncher = bl;
			}
			listener = new AnonymousClassHyperlinkListener();
		}
	}
}