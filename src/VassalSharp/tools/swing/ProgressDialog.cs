/*
* $Id$
*
* Copyright (c) 2008-2010 by Joel Uckelman
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
//UPGRADE_TODO: The type 'java.util.concurrent.Callable' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Callable = java.util.concurrent.Callable;
//UPGRADE_TODO: The type 'java.util.concurrent.CancellationException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using CancellationException = java.util.concurrent.CancellationException;
//UPGRADE_TODO: The type 'java.util.concurrent.ExecutionException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ExecutionException = java.util.concurrent.ExecutionException;
//UPGRADE_TODO: The type 'java.util.concurrent.Future' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Future = java.util.concurrent.Future;
//UPGRADE_TODO: The type 'net.miginfocom.swing.MigLayout' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MigLayout = net.miginfocom.swing.MigLayout;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.tools.swing
{
	
	/// <summary> A cancellable progress dialog.
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	[Serializable]
	public class ProgressDialog:System.Windows.Forms.Form
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(ProgressDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ProgressDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ProgressDialog enclosingInstance;
			public ProgressDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs e)
			{
				e.Cancel = true;
				Enclosing_Instance.fireCancelledEvent(new System.EventArgs());
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(ProgressDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ProgressDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ProgressDialog enclosingInstance;
			public ProgressDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.OnCancelled(event_sender, e);
			}
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Gets the text label shown beneath the progress bar.
		/// 
		/// </summary>
		/// <returns> the text label
		/// </returns>
		/// <summary> Sets the text label shown beneath the progress bar.
		/// 
		/// </summary>
		/// <param name="text">the text label
		/// </param>
		virtual public System.String Label
		{
			get
			{
				return label.Text;
			}
			
			set
			{
				label.Text = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Gets whether the progress bar is indeterminate.
		/// 
		/// </summary>
		/// <returns> whether the progress bar is indeterminate
		/// </returns>
		/// <summary> Sets whether the progress bar should be indeterminate.
		/// 
		/// </summary>
		/// <param name="indet">whether the progress bar should beindeterminate
		/// </param>
		virtual public bool Indeterminate
		{
			get
			{
				return progbar.Indeterminate;
			}
			
			set
			{
				progbar.Indeterminate = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Gets the percentage displayed by the progress bar.
		/// 
		/// </summary>
		/// <returns> the percentage completed
		/// </returns>
		/// <summary> Sets the percentage for the progress bar.
		/// 
		/// </summary>
		/// <param name="percent">the percentage completed
		/// </param>
		virtual public int Progress
		{
			get
			{
				return progbar.Value;
			}
			
			set
			{
				progbar.Value = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Gets whether the progress bar contains a progress string.
		/// 
		/// </summary>
		/// <returns> whether the progress bar contains a progress string
		/// </returns>
		/// <summary> Sets whether the progress bar should contain a progress string.
		/// 
		/// </summary>
		/// <param name="painted">whether the progress bar should contain a progress string
		/// </param>
		virtual public bool StringPainted
		{
			get
			{
				//UPGRADE_ISSUE: Method 'javax.swing.JProgressBar.isStringPainted' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJProgressBarisStringPainted'"
				return progbar.isStringPainted();
			}
			
			set
			{
				//UPGRADE_TODO: Method 'javax.swing.JProgressBar.setStringPainted' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				progbar.setStringPainted(value);
			}
			
		}
		/// <summary> Gets the list of cancellation listeners.
		/// 
		/// </summary>
		/// <returns> the action listeners
		/// </returns>
		//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		virtual public ActionListener[] ActionListeners
		{
			get
			{
				//UPGRADE_ISSUE: Method 'javax.swing.event.EventListenerList.getListeners' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventEventListenerListgetListeners_javalangClass'"
				//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				return listeners.getListeners(typeof(ActionListener));
			}
			
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'label '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal System.Windows.Forms.Label label;
		//UPGRADE_NOTE: Final was removed from the declaration of 'progbar '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal System.Windows.Forms.ProgressBar progbar;
		//UPGRADE_NOTE: Final was removed from the declaration of 'cancel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal System.Windows.Forms.Button cancel;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'listeners '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal System.Collections.Hashtable listeners = new System.Collections.Hashtable();
		
		/// <summary> Creates a progress dialog.
		/// 
		/// </summary>
		/// <param name="parent">the parent frame
		/// </param>
		/// <param name="title">the dialog title
		/// </param>
		/// <param name="text">the text beneath the progress bar
		/// </param>
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		public ProgressDialog(System.Windows.Forms.Form parent, System.String title, System.String text):base()
		{
			//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.SetDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtFrame_javalangString_boolean'"
			SupportClass.DialogSupport.SetDialog(this, parent, title);
			
			// set up the components
			System.Windows.Forms.Label temp_label;
			temp_label = new System.Windows.Forms.Label();
			temp_label.Text = text;
			label = temp_label;
			
			//    Font f = label.getFont();
			//    label.setFont(f.deriveFont(f.getSize2D()*4f/3f));
			//    label.setFont(f.deriveFont(14f));
			
			progbar = SupportClass.ProgressBarSupport.CreateProgress(0, 100);
			//UPGRADE_TODO: Method 'javax.swing.JProgressBar.setStringPainted' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			progbar.setStringPainted(true);
			progbar.Value = 0;
			
			// AUGH! This is retarded that we can't set a default font size...
			//    f = progbar.getFont();
			//    progbar.setFont(f.deriveFont(14f));
			
			cancel = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.CANCEL));
			
			//    f = cancel.getFont();
			//    cancel.setFont(f.deriveFont(14f));
			
			// forward clicks on the close decoration to cancellation listeners
			//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(this).windowClosing);
			
			// forward clicks on the close button to the cancellation listeners
			cancel.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(cancel);
			
			// create the layout
			//UPGRADE_NOTE: Final was removed from the declaration of 'panel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel panel = new JPanel(new MigLayout("insets dialog, fill", "", "unrelated:push[]related[]unrelated:push[]"));
			
			// NB: It's necessary to set the minimum width for the label,
			// otherwise if the label text is set to a string which is too long,
			// the label will overflow the container instead of showing ellipses.
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(progbar);
			progbar.Dock = new System.Windows.Forms.DockStyle();
			progbar.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(label);
			label.Dock = new System.Windows.Forms.DockStyle();
			label.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(cancel);
			cancel.Dock = new System.Windows.Forms.DockStyle();
			cancel.BringToFront();
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(panel);
			
			// pack to find the minimum height
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
			
			// set minimum size
			setMinimumSize(new System.Drawing.Size(300, Height));
			
			// pack again to ensure that we respect the minimum size
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
		}
		
		//UPGRADE_NOTE: This method is no longer necessary and it can be commented or removed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1271'"
		protected internal virtual void  fireCancelledEvent(System.Object event_sender, System.EventArgs e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'larr '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.event.EventListenerList.getListenerList' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.Object[] larr = listeners;
			
			// Process the listeners last to first, notifying
			// those that are interested in this event.
			for (int i = larr.Length - 2; i >= 0; i -= 2)
			{
				//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				if (larr[i] == typeof(ActionListener))
				{
					//UPGRADE_TODO: Method 'java.awt.event.ActionListener.actionPerformed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					((ActionListener) larr[i + 1]).actionPerformed(e);
				}
			}
		}
		
		/// <summary> Adds a cancellation listener.
		/// 
		/// </summary>
		/// <param name="l">the action listener
		/// </param>
		//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public virtual void  addActionListener(ActionListener l)
		{
			//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			listeners.Add(l, typeof(ActionListener));
		}
		
		/// <summary> Removes cancellation listener.
		/// 
		/// </summary>
		/// <param name="l">the action listener
		/// </param>
		//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public virtual void  removeActionListener(ActionListener l)
		{
			listeners.Remove(l);
		}
		
		/// <summary> Creates a progress dialog on the EDT.
		/// 
		/// This is a convenience method to be used when a non-EDT thread needs to
		/// create a progress dialog in order to attach listeners to it.
		/// 
		/// </summary>
		/// <param name="parent">the parent frame
		/// </param>
		/// <param name="title">the dialog title
		/// </param>
		/// <param name="text">the text beneath the progress bar
		/// </param>
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		public static ProgressDialog createOnEDT(System.Windows.Forms.Form parent, System.String title, System.String text)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final Future < ProgressDialog > f = EDT.submit(new Callable < ProgressDialog >()
			{
			}
		}
		public virtual ProgressDialog call()
		{
			return new ProgressDialog(parent, title, text);
		}
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	try
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return f.get();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	catch(CancellationException e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// this should never happen
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	throw new IllegalStateException(e);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	catch(InterruptedException e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// this should never happen
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	throw new IllegalStateException(e);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	catch(ExecutionException e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		throw new RuntimeException(e);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}