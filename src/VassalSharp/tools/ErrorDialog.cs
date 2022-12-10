/*
* $Id$
*
* Copyright (c) 2008-2011 by Joel Uckelman
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
//UPGRADE_TODO: The type 'java.util.concurrent.Future' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Future = java.util.concurrent.Future;
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
using BadDataReport = VassalSharp.build.BadDataReport;
using GameModule = VassalSharp.build.GameModule;
using Resources = VassalSharp.i18n.Resources;
using Bug2694Handler = VassalSharp.tools.bug.Bug2694Handler;
using BugHandler = VassalSharp.tools.bug.BugHandler;
namespace VassalSharp.tools
{
	
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class ErrorDialog
	{
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(System.Windows.Forms.Form frame, System.Exception thrown)
			{
				InitBlock(frame, thrown);
			}
			private void  InitBlock(System.Windows.Forms.Form frame, System.Exception thrown)
			{
				this.frame = frame;
				this.thrown = thrown;
			}
			//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
			//UPGRADE_NOTE: Final variable frame was copied into class AnonymousClassRunnable. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.Form frame;
			//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			//UPGRADE_NOTE: Final variable thrown was copied into class AnonymousClassRunnable. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Exception thrown;
			public virtual void  Run()
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
				//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				new BugDialog(frame, thrown).Visible = true;
			}
		}
		private void  InitBlock()
		{
			return ProblemDialog.show((int) System.Windows.Forms.MessageBoxIcon.Error, messageKey, args);
			return ProblemDialog.show((int) System.Windows.Forms.MessageBoxIcon.Error, parent, messageKey, args);
			return ProblemDialog.show((int) System.Windows.Forms.MessageBoxIcon.Error, thrown, messageKey, args);
			return ProblemDialog.show((int) System.Windows.Forms.MessageBoxIcon.Error, parent, thrown, messageKey, args);
			return ProblemDialog.show((int) System.Windows.Forms.MessageBoxIcon.Error, parent, thrown, title, heading, message);
			return ProblemDialog.showDisableable((int) System.Windows.Forms.MessageBoxIcon.Error, key, messageKey, args);
			return ProblemDialog.showDisableable((int) System.Windows.Forms.MessageBoxIcon.Error, parent, key, messageKey, args);
			return ProblemDialog.showDisableable((int) System.Windows.Forms.MessageBoxIcon.Error, thrown, key, messageKey, args);
			return ProblemDialog.showDisableable((int) System.Windows.Forms.MessageBoxIcon.Error, parent, thrown, key, messageKey, args);
			return ProblemDialog.showDisableable((int) System.Windows.Forms.MessageBoxIcon.Error, parent, thrown, key, title, heading, message);
			return ProblemDialog.showDisableableNoI18N((int) System.Windows.Forms.MessageBoxIcon.Error, thrown, key, title, heading, message);
			return ProblemDialog.showDetails((int) System.Windows.Forms.MessageBoxIcon.Error, details, messageKey, args);
			return ProblemDialog.showDetails((int) System.Windows.Forms.MessageBoxIcon.Error, parent, details, messageKey, args);
			return ProblemDialog.showDetails((int) System.Windows.Forms.MessageBoxIcon.Error, thrown, details, messageKey, args);
			return ProblemDialog.showDetails((int) System.Windows.Forms.MessageBoxIcon.Error, parent, thrown, details, messageKey, args);
			return ProblemDialog.showDetails((int) System.Windows.Forms.MessageBoxIcon.Error, parent, thrown, details, title, heading, message);
			return ProblemDialog.showDetailsDisableable((int) System.Windows.Forms.MessageBoxIcon.Error, details, key, messageKey, args);
			return ProblemDialog.showDetailsDisableable((int) System.Windows.Forms.MessageBoxIcon.Error, parent, details, key, messageKey, args);
			return ProblemDialog.showDetailsDisableable((int) System.Windows.Forms.MessageBoxIcon.Error, thrown, details, key, messageKey, args);
			return ProblemDialog.showDetailsDisableable((int) System.Windows.Forms.MessageBoxIcon.Error, parent, thrown, details, key, messageKey, args);
			return ProblemDialog.showDetailsDisableable((int) System.Windows.Forms.MessageBoxIcon.Error, parent, thrown, details, key, title, heading, message);
		}
		private ErrorDialog()
		{
			InitBlock();
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.tools.ErrorDialog'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private static final List < BugHandler > bughandlers = 
		Collections.synchronizedList(new ArrayList < BugHandler >(
		Arrays.asList(new Bug2694Handler())
		));
		
		public static void  addBugHandler(BugHandler bh)
		{
			bughandlers.add(bh);
		}
		
		// FIXME: make method which takes Throwable but doesn't use it for details
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public static void  bug(System.Exception thrown)
		{
			// determine whether an OutOfMemoryError is in our causal chain
			//UPGRADE_NOTE: Final was removed from the declaration of 'oom '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.OutOfMemoryException oom = ThrowableUtils.getRecent(typeof(System.OutOfMemoryException), thrown);
			if (oom != null)
			{
				logger.error("", thrown);
				show("Error.out_of_memory");
				return ;
			}
			
			// use a bug handler if one matches
			lock (bughandlers)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(BugHandler bh: bughandlers)
				{
					if (bh.accept(thrown))
					{
						bh.handle(thrown);
						return ;
					}
				}
			}
			
			// show a bug report dialog if one has not been shown before
			if (!DialogUtils.setDisabled(typeof(BugDialog), true))
			{
				logger.error("", thrown);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'frame '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
				System.Windows.Forms.Form frame = GameModule.getGameModule() == null?null:GameModule.getGameModule().getFrame();
				
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				SwingUtilities.invokeLater(new AnonymousClassRunnable(frame, thrown));
			}
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > show(
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > show(
		Component parent, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > show(
		Throwable thrown, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > show(
		final Component parent, 
		final Throwable thrown, 
		final String messageKey, 
		final Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > show(
		final Component parent, 
		final Throwable thrown, 
		final String title, 
		final String heading, 
		final String message)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDisableable(
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDisableable(
		Component parent, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDisableable(
		Throwable thrown, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDisableable(
		Component parent, 
		Throwable thrown, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDisableable(
		Component parent, 
		Throwable thrown, 
		Object key, 
		String title, 
		String heading, 
		String message)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDisableableNoI18N(
		Throwable thrown, 
		Object key, 
		String title, 
		String heading, 
		String message)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetails(
		String details, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetails(
		Component parent, 
		String details, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetails(
		Throwable thrown, 
		String details, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetails(
		Component parent, 
		Throwable thrown, 
		String details, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetails(
		Component parent, 
		Throwable thrown, 
		String details, 
		String title, 
		String heading, 
		String message)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetailsDisableable(
		String details, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetailsDisableable(
		Component parent, 
		String details, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetailsDisableable(
		Throwable thrown, 
		String details, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetailsDisableable(
		Component parent, 
		Throwable thrown, 
		String details, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetailsDisableable(
		Component parent, 
		Throwable thrown, 
		String details, 
		Object key, 
		String title, 
		String heading, 
		String message)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private static final Set < String > reportedDataErrors = 
		Collections.synchronizedSet(new HashSet < String >());
		
		public static void  dataError(BadDataReport e)
		{
			logger.warn(e.Message + ": " + e.Data);
			if (e.Cause != null)
				logger.error("", e.Cause);
			
			if (!reportedDataErrors.contains(e.Data))
			{
				reportedDataErrors.add(e.Data);
				
				// send a warning to the controls window
				GameModule.getGameModule().warn(Resources.getString("Error.data_error_message", e.Message, e.Data));
			}
		}
		
		///////////////////
		
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'loremIpsum '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n\nLorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
			
			while (!DialogUtils.isDisabled(0))
			{
				showDisableable(null, null, 0, "Oh Shit!", "Oh Shit!", loremIpsum);
				//UPGRADE_TODO: Method 'java.lang.Thread.sleep' was converted to 'System.Threading.Thread.Sleep' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangThreadsleep_long'"
				System.Threading.Thread.Sleep(new System.TimeSpan((System.Int64) 10000 * 1000));
			}
			
			System.Environment.Exit(0);
		}
		static ErrorDialog()
		{
			logger = LoggerFactory.getLogger(typeof(ErrorDialog));
		}
	}
}