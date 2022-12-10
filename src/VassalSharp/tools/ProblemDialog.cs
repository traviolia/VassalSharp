/*
* $Id$
*
* Copyright (c) 2008 by Joel Uckelman
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
using GameModule = VassalSharp.build.GameModule;
using Resources = VassalSharp.i18n.Resources;
using DetailsDialog = VassalSharp.tools.swing.DetailsDialog;
using Dialogs = VassalSharp.tools.swing.Dialogs;
namespace VassalSharp.tools
{
	
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class ProblemDialog
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(ProblemDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ProblemDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ProblemDialog enclosingInstance;
			public ProblemDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				Dialogs.showMessageDialog(parent, title, heading, message, messageType);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable1 : IThreadRunnable
		{
			public AnonymousClassRunnable1(ProblemDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ProblemDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ProblemDialog enclosingInstance;
			public ProblemDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				Dialogs.showMessageDialog(parent, title, heading, message, messageType, key, Resources.getString("Dialogs.disable"));
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable2 : IThreadRunnable
		{
			public AnonymousClassRunnable2(ProblemDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ProblemDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ProblemDialog enclosingInstance;
			public ProblemDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				Dialogs.showMessageDialog(VassalSharp.tools.ProblemDialog.Frame, title, heading, message, messageType, key, "Don't show this again");
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable3 : IThreadRunnable
		{
			public AnonymousClassRunnable3(ProblemDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ProblemDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ProblemDialog enclosingInstance;
			public ProblemDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				DetailsDialog.showDialog(parent, title, heading, message, details, Resources.getString("Dialogs.disable"), Resources.getString("Dialogs.show_details"), Resources.getString("Dialogs.hide_details"), messageType, null);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable4' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable4 : IThreadRunnable
		{
			public AnonymousClassRunnable4(ProblemDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ProblemDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ProblemDialog enclosingInstance;
			public ProblemDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				DetailsDialog.showDialog(parent, title, heading, message, details, Resources.getString("Dialogs.disable"), Resources.getString("Dialogs.show_details"), Resources.getString("Dialogs.hide_details"), messageType, key);
			}
		}
		private void  InitBlock()
		{
			return show(messageType, Frame, null, messageKey, args);
			return show(messageType, parent, null, messageKey, args);
			return show(messageType, Frame, thrown, messageKey, args);
			return show(messageType, parent, thrown, Resources.getString(messageKey + "_title"), Resources.getString(messageKey + "_heading"), Resources.getString(messageKey + "_message", args));
			if (thrown != null)
				logger.error("", thrown);
			
			return DialogUtils.enqueue(new AnonymousClassRunnable(this));
			return showDisableable(messageType, Frame, null, key, messageKey, args);
			return showDisableable(messageType, parent, null, key, messageKey, args);
			return showDisableable(messageType, Frame, thrown, key, messageKey, args);
			return showDisableable(messageType, parent, thrown, key, Resources.getString(messageKey + "_title"), Resources.getString(messageKey + "_heading"), Resources.getString(messageKey + "_message", args));
			if (thrown != null)
				logger.error("", thrown);
			
			return DialogUtils.enqueue(new AnonymousClassRunnable1(this));
			if (thrown != null)
				logger.error("", thrown);
			
			return DialogUtils.enqueue(new AnonymousClassRunnable2(this));
			return showDetails(messageType, Frame, null, details, messageKey, args);
			return showDetails(messageType, parent, null, details, messageKey, args);
			return showDetails(messageType, Frame, thrown, details, messageKey, args);
			return showDetails(messageType, parent, thrown, details, Resources.getString(messageKey + "_title"), Resources.getString(messageKey + "_heading"), Resources.getString(messageKey + "_message", args));
			if (thrown != null)
				logger.error("", thrown);
			
			return DialogUtils.enqueue(new AnonymousClassRunnable3(this));
			return showDetailsDisableable(messageType, Frame, null, details, key, messageKey, args);
			return showDetailsDisableable(messageType, parent, null, details, key, messageKey, args);
			return showDetailsDisableable(messageType, Frame, thrown, details, key, messageKey, args);
			return showDetailsDisableable(messageType, parent, thrown, details, key, Resources.getString(messageKey + "_title"), Resources.getString(messageKey + "_heading"), Resources.getString(messageKey + "_message", args));
			if (thrown != null)
				logger.error("", thrown);
			
			return DialogUtils.enqueue(new AnonymousClassRunnable4(this));
		}
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		private static System.Windows.Forms.Form Frame
		{
			get
			{
				return GameModule.getGameModule() == null?null:GameModule.getGameModule().Frame;
			}
			
		}
		private ProblemDialog()
		{
			InitBlock();
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.tools.ProblemDialog'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > show(
		int messageType, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > show(
		int messageType, 
		Component parent, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > show(
		int messageType, 
		Throwable thrown, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > show(
		int messageType, 
		Component parent, 
		Throwable thrown, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > show(
		final int messageType, 
		final Component parent, 
		final Throwable thrown, 
		final String title, 
		final String heading, 
		final String message)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDisableable(
		int messageType, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDisableable(
		int messageType, 
		Component parent, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDisableable(
		int messageType, 
		Throwable thrown, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDisableable(
		int messageType, 
		Component parent, 
		Throwable thrown, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDisableable(
		final int messageType, 
		final Component parent, 
		final Throwable thrown, 
		final Object key, 
		final String title, 
		final String heading, 
		final String message)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDisableableNoI18N(
		final int messageType, 
		final Throwable thrown, 
		final Object key, 
		final String title, 
		final String heading, 
		final String message)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetails(
		int messageType, 
		String details, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetails(
		int messageType, 
		Component parent, 
		String details, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetails(
		int messageType, 
		Throwable thrown, 
		String details, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetails(
		int messageType, 
		Component parent, 
		Throwable thrown, 
		String details, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetails(
		final int messageType, 
		final Component parent, 
		final Throwable thrown, 
		final String details, 
		final String title, 
		final String heading, 
		final String message)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetailsDisableable(
		int messageType, 
		String details, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetailsDisableable(
		int messageType, 
		Component parent, 
		String details, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetailsDisableable(
		int messageType, 
		Throwable thrown, 
		String details, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetailsDisableable(
		int messageType, 
		Component parent, 
		Throwable thrown, 
		String details, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetailsDisableable(
		final int messageType, 
		final Component parent, 
		final Throwable thrown, 
		final String details, 
		final Object key, 
		final String title, 
		final String heading, 
		final String message)
		static ProblemDialog()
		{
			logger = LoggerFactory.getLogger(typeof(ProblemDialog));
		}
	}
}