/*
* $Id$
*
* Copyright (c) 2008-2012 by Brent Easton
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
//UPGRADE_TODO: The type 'java.util.regex.Pattern' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Pattern = java.util.regex.Pattern;
//UPGRADE_TODO: The type 'org.jdesktop.swingx.JXTreeTable' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using JXTreeTable = org.jdesktop.swingx.JXTreeTable;
//UPGRADE_TODO: The type 'org.jdesktop.swingx.treetable.DefaultMutableTreeTableNode' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using DefaultMutableTreeTableNode = org.jdesktop.swingx.treetable.DefaultMutableTreeTableNode;
//UPGRADE_TODO: The type 'org.jdesktop.swingx.treetable.DefaultTreeTableModel' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using DefaultTreeTableModel = org.jdesktop.swingx.treetable.DefaultTreeTableModel;
using Configurable = VassalSharp.build.Configurable;
using GameModule = VassalSharp.build.GameModule;
using PrototypeDefinition = VassalSharp.build.module.PrototypeDefinition;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using CardSlot = VassalSharp.build.widget.CardSlot;
using PieceSlot = VassalSharp.build.widget.PieceSlot;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using ConfigureTree = VassalSharp.configure.ConfigureTree;
using DirectoryConfigurer = VassalSharp.configure.DirectoryConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using StringEnumConfigurer = VassalSharp.configure.StringEnumConfigurer;
using Resources = VassalSharp.i18n.Resources;
using BrowserSupport = VassalSharp.tools.BrowserSupport;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
using ImageUtils = VassalSharp.tools.image.ImageUtils;
using Dialogs = VassalSharp.tools.swing.Dialogs;
namespace VassalSharp.counters
{
	
	/// <summary> Class to load a directory full of images and create counters
	/// 
	/// </summary>
	public class MassPieceLoader
	{
		
		protected internal const int DESC_COL = 0;
		protected internal const int NAME_COL = 2;
		protected internal const int IMAGE_COL = 1;
		protected internal const int SKIP_COL = 3;
		protected internal const int COPIES_COL = 4;
		protected internal const int COLUMN_COUNT = 5;
		//UPGRADE_NOTE: Final was removed from the declaration of 'EDITABLE_COLOR '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal static readonly System.Drawing.Color EDITABLE_COLOR = System.Drawing.Color.Blue;
		
		protected internal Configurable target;
		protected internal ConfigureTree configureTree;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected ArrayList < String > imageNames = new ArrayList < String >();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected ArrayList < String > baseImages = new ArrayList < String >();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected ArrayList < String > levelImages = new ArrayList < String >();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected HashMap < String, PieceInfo > pieceInfo = new HashMap < String, PieceInfo >();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected ArrayList < Emb > layers = new ArrayList < Emb >();
		protected internal MassLoaderDialog dialog;
		private static DirectoryConfigurer dirConfig = new DirectoryConfigurer(null, "Image Directory: ");
		System.Boolean tempAux = false;
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		private static BooleanConfigurer basicConfig = new BooleanConfigurer(null, "Do not load images into Basic Piece traits?", ref tempAux);
		private static MassPieceDefiner definer = new MassPieceDefiner();
		
		public MassPieceLoader(ConfigureTree tree, Configurable target)
		{
			this.target = target;
			this.configureTree = tree;
		}
		
		// The Dialog does all the work.
		public virtual void  load()
		{
			dialog = new MassLoaderDialog(this);
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			dialog.Visible = true;
			if (!dialog.Cancelled)
			{
				dialog.load();
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MassLoaderDialog' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Mass Piece Loader dialog</summary>
		//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
		[Serializable]
		protected internal class MassLoaderDialog:System.Windows.Forms.Form
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener
			{
				public AnonymousClassPropertyChangeListener(MassLoaderDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(MassLoaderDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private MassLoaderDialog enclosingInstance;
				public MassLoaderDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
				{
					if (e.NewValue != null)
					{
						Enclosing_Instance.buildTree((System.IO.FileInfo) e.NewValue);
					}
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener1
			{
				public AnonymousClassPropertyChangeListener1(MassLoaderDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(MassLoaderDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private MassLoaderDialog enclosingInstance;
				public MassLoaderDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
				{
					if (e.NewValue != null)
					{
						Enclosing_Instance.buildTree(VassalSharp.counters.MassPieceLoader.dirConfig.FileValue);
					}
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener
			{
				public AnonymousClassActionListener(MassLoaderDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(MassLoaderDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private MassLoaderDialog enclosingInstance;
				public MassLoaderDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'savePiece '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					GamePiece savePiece = VassalSharp.counters.MassPieceLoader.definer.getPiece();
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					Enclosing_Instance.defineDialog.Visible = true;
					if (Enclosing_Instance.defineDialog.Cancelled)
					{
						VassalSharp.counters.MassPieceLoader.definer.setPiece(savePiece);
					}
					else
					{
						Enclosing_Instance.buildTree(VassalSharp.counters.MassPieceLoader.dirConfig.FileValue);
					}
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener1
			{
				public AnonymousClassActionListener1(MassLoaderDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(MassLoaderDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private MassLoaderDialog enclosingInstance;
				public MassLoaderDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					Enclosing_Instance.save();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener2
			{
				public AnonymousClassActionListener2(MassLoaderDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(MassLoaderDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private MassLoaderDialog enclosingInstance;
				public MassLoaderDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					Enclosing_Instance.cancel();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener3
			{
				public AnonymousClassActionListener3(MassLoaderDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(MassLoaderDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private MassLoaderDialog enclosingInstance;
				public MassLoaderDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'h '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					HelpFile h = HelpFile.getReferenceManualPage("MassPieceLoader.htm");
					BrowserSupport.openURL(h.Contents.ToString());
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassWindowAdapter
			{
				public AnonymousClassWindowAdapter(MassLoaderDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(MassLoaderDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private MassLoaderDialog enclosingInstance;
				public MassLoaderDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs we)
				{
					we.Cancel = true;
					Enclosing_Instance.cancel();
				}
			}
			private void  InitBlock(MassPieceLoader enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MassPieceLoader enclosingInstance;
			virtual public bool Cancelled
			{
				get
				{
					return cancelled;
				}
				
			}
			virtual public System.IO.FileInfo Directory
			{
				get
				{
					return VassalSharp.counters.MassPieceLoader.dirConfig.FileValue;
				}
				
			}
			public MassPieceLoader Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			protected internal bool cancelled = false;
			protected internal DefineDialog defineDialog;
			protected internal MyTreeTable tree;
			protected internal MyTreeTableModel model;
			protected internal BasicNode root;
			protected internal System.IO.FileInfo loadDirectory;
			
			public MassLoaderDialog(MassPieceLoader enclosingInstance):base()
			{
				InitBlock(enclosingInstance);
				SupportClass.DialogSupport.SetDialog(this, Enclosing_Instance.configureTree.Frame);
				//UPGRADE_ISSUE: Method 'java.awt.Dialog.setModal' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtDialogsetModal_boolean'"
				setModal(true);
				Text = "Load Multiple Pieces";
				//UPGRADE_ISSUE: Method 'javax.swing.JDialog.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJDialogsetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				setLayout(new BoxLayout(((System.Windows.Forms.ContainerControl) this), BoxLayout.Y_AXIS));
				setPreferredSize(new System.Drawing.Size(800, 600));
				
				VassalSharp.counters.MassPieceLoader.dirConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(VassalSharp.counters.MassPieceLoader.dirConfig.Controls);
				
				VassalSharp.counters.MassPieceLoader.basicConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener1(this).propertyChange);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(VassalSharp.counters.MassPieceLoader.basicConfig.Controls);
				
				defineDialog = new DefineDialog(enclosingInstance, this);
				//UPGRADE_NOTE: Final was removed from the declaration of 'defineButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Button defineButton = SupportClass.ButtonSupport.CreateStandardButton("Edit Piece Template");
				defineButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(defineButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(defineButton);
				
				tree = new MyTreeTable(enclosingInstance);
				buildTree(VassalSharp.counters.MassPieceLoader.dirConfig.FileValue);
				//UPGRADE_NOTE: Final was removed from the declaration of 'scrollPane '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.Windows.Forms.ScrollableControl scrollPane = new JScrollPane(tree);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(scrollPane);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'buttonBox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box buttonBox = Box.createHorizontalBox();
				//UPGRADE_NOTE: Final was removed from the declaration of 'okButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Button okButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.OK));
				okButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(okButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				buttonBox.Controls.Add(okButton);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'cancelButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Button cancelButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.CANCEL));
				cancelButton.Click += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(cancelButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				buttonBox.Controls.Add(cancelButton);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'helpButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Button helpButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.HELP));
				helpButton.Click += new System.EventHandler(new AnonymousClassActionListener3(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(helpButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				buttonBox.Controls.Add(helpButton);
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(buttonBox);
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				pack();
				System.Windows.Forms.Control generatedAux13 = Parent;
				//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
				StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
				Closing += new System.ComponentModel.CancelEventHandler(this.MassLoaderDialog_Closing_DO_NOTHING_ON_CLOSE);
				//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
				Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(this).windowClosing);
			}
			
			public virtual void  cancel()
			{
				cancelled = true;
				Dispose();
			}
			
			public virtual void  save()
			{
				// Count the pieces to be loaded
				int pieceCount = 0;
				for (int i = 0; i < root.getChildCount(); i++)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'node '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					PieceNode node = (PieceNode) root.getChildAt(i);
					if (!node.isSkip())
					{
						pieceCount += node.Copies;
					}
				}
				
				// Do they really want to do this?
				if (pieceCount > 0)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'message '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String message = "This will load " + pieceCount + " piece" + (pieceCount > 1?"s":"") + " into component " + Enclosing_Instance.target.getConfigureName() + ". There is no UNDO option for this process. " + "Are you sure you wish to continue with the load?";
					//UPGRADE_NOTE: Final was removed from the declaration of 'result '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int result = Dialogs.showConfirmDialog(null, "Confirm Load", "Confirm Load", message, (int) System.Windows.Forms.MessageBoxIcon.Exclamation, (int) System.Windows.Forms.MessageBoxButtons.YesNoCancel);
					if (result == 1)
					{
						cancel();
						return ;
					}
					else if (result == 2)
					{
						return ;
					}
				}
				
				cancelled = false;
				Dispose();
			}
			
			/// <summary> Build a tree representing the Game Pieces, Layers, Levels and images to
			/// be loaded. This tree is used as a model for the JxTreeTable, and also as
			/// the guide to load the counters.
			/// 
			/// </summary>
			/// <param name="dir">Directory containing images
			/// </param>
			protected internal virtual void  buildTree(System.IO.FileInfo dir)
			{
				
				loadDirectory = dir;
				
				// Make a list of the Layer traits in the template
				layers.clear();
				GamePiece piece = VassalSharp.counters.MassPieceLoader.definer.getPiece();
				while (piece is Decorator)
				{
					piece = Decorator.getDecorator(piece, typeof(Emb));
					if (piece is Emb)
					{
						layers.add(0, (Emb) piece);
						piece = ((Emb) piece).getInner();
					}
				}
				
				// Find all of the images in the target directory
				loadImageNames(dir);
				
				// Check each image in the target directory to see if it matches the
				// level specification in any of the Embellishments in the template.
				// The remaining images that do not match any Level are our baseImages
				baseImages.clear();
				levelImages.clear();
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(String imageName: imageNames)
				{
					bool match = false;
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(Emb emb: layers)
					{
						match = emb.matchLayer(imageName);
						if (match)
						{
							break;
						}
					}
					
					if (match)
					{
						levelImages.add(imageName);
					}
					else
					{
						baseImages.add(imageName);
					}
				}
				
				// Generate a table node for each base Image.
				// Create a child Layer Node for each layer that has at least one image
				root = new BasicNode(enclosingInstance);
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(String baseImage: baseImages)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'pieceNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					BasicNode pieceNode = new PieceNode(baseImage);
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(Emb emb: layers)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'newLayer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						Emb newLayer = new Emb(emb.getType(), null);
						if (newLayer.buildLayers(baseImage, levelImages))
						{
							//UPGRADE_NOTE: Final was removed from the declaration of 'layerNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							BasicNode layerNode = new LayerNode(enclosingInstance, newLayer.LayerName);
							for (int i = 0; i < newLayer.ImageNames.Length; i++)
							{
								//UPGRADE_NOTE: Final was removed from the declaration of 'levelName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
								System.String levelName = newLayer.LevelNames[i];
								//UPGRADE_NOTE: Final was removed from the declaration of 'levelNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
								BasicNode levelNode = new LevelNode(enclosingInstance, levelName == null?"":levelName, newLayer.ImageNames[i], i);
								layerNode.add(levelNode);
							}
							pieceNode.add(layerNode);
						}
					}
					root.add(pieceNode);
				}
				
				// Set the tree
				model = new MyTreeTableModel(enclosingInstance, root);
				tree.setTreeTableModel(model);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'tcm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Interface 'javax.swing.table.TableColumnModel' was converted to 'System.Data.DataColumnCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.Data.DataColumnCollection tcm = tree.getColumnModel();
				//UPGRADE_ISSUE: Method 'javax.swing.table.TableColumn.setPreferredWidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableTableColumnsetPreferredWidth_int'"
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.table.TableColumnModel.getColumn' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				tcm[VassalSharp.counters.MassPieceLoader.DESC_COL].setPreferredWidth(100);
				//UPGRADE_TODO: Method 'javax.swing.table.TableColumn.setCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.table.TableColumnModel.getColumn' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				tcm[VassalSharp.counters.MassPieceLoader.DESC_COL].setCellRenderer(new ImageNameRenderer(this));
				//UPGRADE_ISSUE: Method 'javax.swing.table.TableColumn.setPreferredWidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableTableColumnsetPreferredWidth_int'"
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.table.TableColumnModel.getColumn' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				tcm[VassalSharp.counters.MassPieceLoader.NAME_COL].setPreferredWidth(200);
				//UPGRADE_TODO: Method 'javax.swing.table.TableColumn.setCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.table.TableColumnModel.getColumn' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				tcm[VassalSharp.counters.MassPieceLoader.NAME_COL].setCellRenderer(new NameRenderer(this));
				//UPGRADE_TODO: Method 'javax.swing.table.TableColumn.setCellEditor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.table.TableColumnModel.getColumn' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				tcm[VassalSharp.counters.MassPieceLoader.NAME_COL].setCellEditor(new NameEditor(this, new System.Windows.Forms.TextBox()));
				//UPGRADE_ISSUE: Method 'javax.swing.table.TableColumn.setPreferredWidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableTableColumnsetPreferredWidth_int'"
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.table.TableColumnModel.getColumn' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				tcm[VassalSharp.counters.MassPieceLoader.IMAGE_COL].setPreferredWidth(200);
				//UPGRADE_TODO: Method 'javax.swing.table.TableColumn.setCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.table.TableColumnModel.getColumn' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				tcm[VassalSharp.counters.MassPieceLoader.IMAGE_COL].setCellRenderer(new ImageNameRenderer(this));
				//UPGRADE_ISSUE: Method 'javax.swing.table.TableColumn.setPreferredWidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableTableColumnsetPreferredWidth_int'"
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.table.TableColumnModel.getColumn' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				tcm[VassalSharp.counters.MassPieceLoader.SKIP_COL].setPreferredWidth(50);
				//UPGRADE_ISSUE: Method 'javax.swing.table.TableColumn.setMaxWidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableTableColumnsetMaxWidth_int'"
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.table.TableColumnModel.getColumn' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				tcm[VassalSharp.counters.MassPieceLoader.SKIP_COL].setMaxWidth(50);
				//UPGRADE_ISSUE: Method 'javax.swing.table.TableColumn.setPreferredWidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableTableColumnsetPreferredWidth_int'"
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.table.TableColumnModel.getColumn' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				tcm[VassalSharp.counters.MassPieceLoader.COPIES_COL].setPreferredWidth(50);
				//UPGRADE_ISSUE: Method 'javax.swing.table.TableColumn.setMaxWidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableTableColumnsetMaxWidth_int'"
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.table.TableColumnModel.getColumn' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				tcm[VassalSharp.counters.MassPieceLoader.COPIES_COL].setMaxWidth(50);
				//UPGRADE_TODO: Method 'javax.swing.table.TableColumn.setCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.table.TableColumnModel.getColumn' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				tcm[VassalSharp.counters.MassPieceLoader.COPIES_COL].setCellRenderer(new CopiesRenderer(this));
			}
			
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'NameRenderer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			//UPGRADE_ISSUE: Class 'javax.swing.table.DefaultTableCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableDefaultTableCellRenderer'"
			[Serializable]
			internal class NameRenderer:DefaultTableCellRenderer
			{
				public NameRenderer(MassLoaderDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(MassLoaderDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private MassLoaderDialog enclosingInstance;
				public MassLoaderDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				private const long serialVersionUID = 1L;
				
				//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				public System.Windows.Forms.Control getTableCellRendererComponent(System.Windows.Forms.DataGrid table, System.Object value_Renamed, bool isSelected, bool hasFocus, int row, int col)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.table.DefaultTableCellRenderer.getTableCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableDefaultTableCellRenderer'"
					System.Windows.Forms.Control c = base.getTableCellRendererComponent(table, value_Renamed, isSelected, hasFocus, row, col);
					//UPGRADE_NOTE: Final was removed from the declaration of 'node '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					BasicNode node = (BasicNode) Enclosing_Instance.tree.getPathForRow(row).getLastPathComponent();
					c.Enabled = !node.isSkip();
					c.ForeColor = VassalSharp.counters.MassPieceLoader.EDITABLE_COLOR;
					return c;
				}
			}
			
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'CopiesRenderer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			//UPGRADE_ISSUE: Class 'javax.swing.table.DefaultTableCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableDefaultTableCellRenderer'"
			[Serializable]
			internal class CopiesRenderer:DefaultTableCellRenderer
			{
				public CopiesRenderer(MassLoaderDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(MassLoaderDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private MassLoaderDialog enclosingInstance;
				public MassLoaderDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				private const long serialVersionUID = 1L;
				
				//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				public System.Windows.Forms.Control getTableCellRendererComponent(System.Windows.Forms.DataGrid table, System.Object value_Renamed, bool isSelected, bool hasFocus, int row, int column)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.table.DefaultTableCellRenderer.getTableCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableDefaultTableCellRenderer'"
					System.Windows.Forms.Label renderedLabel = (System.Windows.Forms.Label) base.getTableCellRendererComponent(table, value_Renamed, isSelected, hasFocus, row, column);
					//UPGRADE_NOTE: Final was removed from the declaration of 'node '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					BasicNode node = (BasicNode) Enclosing_Instance.tree.getPathForRow(row).getLastPathComponent();
					//UPGRADE_TODO: Method 'javax.swing.JLabel.setHorizontalAlignment' was converted to 'System.Windows.Forms.Label.ImageAlign' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJLabelsetHorizontalAlignment_int'"
					renderedLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
					renderedLabel.Enabled = !node.isSkip();
					renderedLabel.ForeColor = VassalSharp.counters.MassPieceLoader.EDITABLE_COLOR;
					return renderedLabel;
				}
			}
			
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'ImageNameRenderer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			//UPGRADE_ISSUE: Class 'javax.swing.table.DefaultTableCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableDefaultTableCellRenderer'"
			[Serializable]
			internal class ImageNameRenderer:DefaultTableCellRenderer
			{
				public ImageNameRenderer(MassLoaderDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(MassLoaderDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private MassLoaderDialog enclosingInstance;
				public MassLoaderDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				private const long serialVersionUID = 1L;
				
				//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				public System.Windows.Forms.Control getTableCellRendererComponent(System.Windows.Forms.DataGrid table, System.Object value_Renamed, bool isSelected, bool hasFocus, int row, int col)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_ISSUE: Class 'javax.swing.table.DefaultTableCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableDefaultTableCellRenderer'"
					//UPGRADE_ISSUE: Method 'javax.swing.table.DefaultTableCellRenderer.getTableCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableDefaultTableCellRenderer'"
					DefaultTableCellRenderer c = (DefaultTableCellRenderer) base.getTableCellRendererComponent(table, value_Renamed, isSelected, hasFocus, row, col);
					//UPGRADE_NOTE: Final was removed from the declaration of 'node '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					BasicNode node = (BasicNode) Enclosing_Instance.tree.getPathForRow(row).getLastPathComponent();
					c.Enabled = !node.isSkip();
					if (node is PieceNode)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'image '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.String image = ((PieceNode) node).getImageName();
						//UPGRADE_NOTE: Final was removed from the declaration of 'i '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.String i = "<html><img src=\"file:/" + Enclosing_Instance.loadDirectory.FullName + "/" + image + "\"></html>";
						SupportClass.ToolTipSupport.setToolTipText(c, i);
					}
					return c;
				}
			}
			
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'NameEditor' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			//UPGRADE_ISSUE: Class 'javax.swing.DefaultCellEditor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultCellEditor'"
			[Serializable]
			internal class NameEditor:DefaultCellEditor
			{
				private void  InitBlock(MassLoaderDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private MassLoaderDialog enclosingInstance;
				public MassLoaderDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				private const long serialVersionUID = 1L;
				
				//UPGRADE_ISSUE: Constructor 'javax.swing.DefaultCellEditor.DefaultCellEditor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultCellEditor'"
				public NameEditor(MassLoaderDialog enclosingInstance, System.Windows.Forms.TextBox textField):base(textField)
				{
					InitBlock(enclosingInstance);
				}
				
				//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				public System.Windows.Forms.Control getTableCellEditorComponent(System.Windows.Forms.DataGrid table, System.Object value_Renamed, bool isSelected, int row, int column)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.DefaultCellEditor.getTableCellEditorComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultCellEditor'"
					System.Windows.Forms.Control c = base.getTableCellEditorComponent(table, value_Renamed, isSelected, row, column);
					c.ForeColor = System.Drawing.Color.Blue;
					return c;
				}
			}
			
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'SkipRenderer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			//UPGRADE_ISSUE: Class 'javax.swing.table.DefaultTableCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableDefaultTableCellRenderer'"
			[Serializable]
			internal class SkipRenderer:DefaultTableCellRenderer
			{
				public SkipRenderer(MassLoaderDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(MassLoaderDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private MassLoaderDialog enclosingInstance;
				public MassLoaderDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				private const long serialVersionUID = 1L;
				
				//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				public System.Windows.Forms.Control getTableCellRendererComponent(System.Windows.Forms.DataGrid table, System.Object value_Renamed, bool isSelected, bool hasFocus, int row, int col)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.table.DefaultTableCellRenderer.getTableCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableDefaultTableCellRenderer'"
					System.Windows.Forms.Control c = base.getTableCellRendererComponent(table, value_Renamed, isSelected, hasFocus, row, col);
					if (!(Enclosing_Instance.tree.getPathForRow(row).getLastPathComponent() is PieceNode))
					{
						System.Windows.Forms.Label temp_label;
						temp_label = new System.Windows.Forms.Label();
						temp_label.Text = "";
						return temp_label;
					}
					return c;
				}
			}
			
			/// <summary> Load all image names in the target directory
			/// 
			/// </summary>
			/// <param name="dir">
			/// </param>
			protected internal virtual void  loadImageNames(System.IO.FileInfo dir)
			{
				imageNames.clear();
				if (dir != null && System.IO.Directory.Exists(dir.FullName))
				{
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(File file: dir.listFiles())
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'imageName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.String imageName = file.getName();
						if (ImageUtils.hasImageSuffix(imageName))
						{
							imageNames.add(imageName);
						}
					}
				}
			}
			
			/// <summary> Load the Pieces based on the Node Tree built while user was editing</summary>
			public virtual void  load()
			{
				// Check a Directory has been entered
				//UPGRADE_NOTE: Final was removed from the declaration of 'dir '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.IO.FileInfo dir = Enclosing_Instance.dialog.Directory;
				if (dir == null)
				{
					return ;
				}
				
				// For each PieceNode, load the required images into the module and
				// generate the piece
				for (int i = 0; i < root.getChildCount(); i++)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'pieceNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					PieceNode pieceNode = (PieceNode) root.getChildAt(i);
					if (!pieceNode.isSkip())
					{
						load(pieceNode);
					}
				}
			}
			
			/// <summary> Load a specific piece and and all referenced images.
			/// 
			/// </summary>
			/// <param name="pieceNode">Sub-tree representing piece
			/// </param>
			public virtual void  load(PieceNode pieceNode)
			{
				
				// Add the Base Image to the module
				//UPGRADE_NOTE: Final was removed from the declaration of 'baseImage '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String baseImage = pieceNode.getImageName();
				Enclosing_Instance.addImageToModule(baseImage);
				
				// Create the BasicPiece
				//UPGRADE_NOTE: Final was removed from the declaration of 'basicType '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String basicType = new SequenceEncoder("", ';').append("").append(baseImage).append(pieceNode.getName()).Value;
				//UPGRADE_NOTE: Final was removed from the declaration of 'basic '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				BasicPiece basic = (BasicPiece) GameModule.getGameModule().createPiece(BasicPiece.ID + basicType);
				
				
				// Build the piece from the template
				GamePiece template = VassalSharp.counters.MassPieceLoader.definer.getPiece();
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				ArrayList < Decorator > traits = new ArrayList < Decorator >();
				
				// Reverse the order of the traits to innermost out
				while (template != null && template is Decorator)
				{
					traits.add(0, (Decorator) template);
					template = ((Decorator) template).getInner();
				}
				
				for (int count = 0; count < pieceNode.Copies; count++)
				{
					GamePiece piece = basic;
					// Build the new piece. Note special Handling for Embellishment templates
					// that will
					// have actual images added for references to matching images. If an
					// Embellishment
					// has no matching images at all, do not add it to the new counter.
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(Decorator trait: traits)
					{
						if (trait is Emb)
						{
							Emb newLayer = new Emb(trait.getType(), null);
							if (newLayer.buildLayers(baseImage, levelImages))
							{
								//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
								for(String image: newLayer.getBuiltImageList())
								{
									addImageToModule(image);
								}
								newLayer.setInner(piece);
								//UPGRADE_NOTE: Final was removed from the declaration of 'saveState '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
								System.String saveState = newLayer.State;
								piece = GameModule.getGameModule().createPiece(newLayer.Type);
								piece.State = saveState;
							}
						}
						else
						{
							//UPGRADE_NOTE: Final was removed from the declaration of 'newTrait '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							Decorator newTrait = (Decorator) GameModule.getGameModule().createPiece(trait.getType());
							newTrait.setState(trait.getState());
							newTrait.setInner(piece);
							//UPGRADE_NOTE: Final was removed from the declaration of 'saveState '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							System.String saveState = newTrait.State;
							piece = GameModule.getGameModule().createPiece(newTrait.Type);
							piece.State = saveState;
						}
					}
					
					// Create the PieceSlot for the new piece
					PieceSlot slot = null;
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					final Class < ? > [] c = target.getAllowableConfigureComponents();
					for (int i = 0; i < c.length && slot == null; i++)
					{
						if (c[i].equals(typeof(CardSlot)))
						{
							slot = new CardSlot();
							slot.Piece = piece;
						}
					}
					if (slot == null)
					{
						slot = new PieceSlot(piece);
					}
					
					// Generate a gpid
					Enclosing_Instance.configureTree.updateGpIds(slot);
					
					// Add the new piece to the tree
					Enclosing_Instance.configureTree.externalInsert(Enclosing_Instance.target, slot);
				}
			}
			private void  MassLoaderDialog_Closing_DO_NOTHING_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
			{
				e.Cancel = true;
				SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 0);
			}
		}
		
		/// <summary> Add the named image to the module
		/// 
		/// </summary>
		/// <param name="name">Image name
		/// </param>
		protected internal virtual void  addImageToModule(System.String name)
		{
			if (name != null && name.Length > 0)
			{
				try
				{
					GameModule.getGameModule().getArchiveWriter().addImage(new System.IO.FileInfo(dirConfig.FileValue.FullName + "\\" + name).FullName, name);
				}
				catch (System.IO.IOException e)
				{
					// FIXME: Log error properly
					// ErrorLog.log()
				}
			}
		}
		
		/// <summary> Maintain a record of all names changed by the user for image basenames.
		/// Default name is image name with image suffix stripped.
		/// 
		/// </summary>
		/// <param name="baseName">Image name
		/// </param>
		/// <returns> user modified name
		/// </returns>
		protected internal virtual System.String getPieceName(System.String baseName)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'info '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			PieceInfo info = pieceInfo.get_Renamed(baseName);
			return info == null?ImageUtils.stripImageSuffix(baseName):info.Name;
		}
		
		/// <summary> 
		/// A custom piece definer based on the Prototype piece definer
		/// 
		/// </summary>
		internal class MassPieceDefiner:PrototypeDefinition.Config.Definer
		{
			private const long serialVersionUID = 1L;
			
			//UPGRADE_TODO: Class 'javax.swing.DefaultListModel' was converted to 'System.Windows.Forms.ListBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingDefaultListModel'"
			protected internal static System.Windows.Forms.ListBox.ObjectCollection newModel;
			
			public MassPieceDefiner():base(GameModule.getGameModule().getGpIdSupport())
			{
				
				// Build a replacement model that uses a modified Embellishment trait
				// with a replacement ImagePicker.
				if (newModel == null)
				{
					//UPGRADE_TODO: Class 'javax.swing.DefaultListModel' was converted to 'System.Windows.Forms.ListBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingDefaultListModel'"
					newModel = new System.Windows.Forms.ListBox.ObjectCollection(new System.Windows.Forms.ListBox());
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(Enumeration < ? > e = availableModel.elements();
					e.hasMoreElements();
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					)
					{
						System.Object o = e.nextElement();
						if (o is Embellishment)
						{
							newModel.Add(new MassPieceLoader.Emb());
						}
						else
						{
							newModel.Add(o);
						}
					}
				}
				
				availableList.setModel(newModel);
				setPiece(null);
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'DefineDialog' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> 
		/// Dialog to hold the PieceDefiner used to specify the multi-load piece
		/// template.
		/// 
		/// </summary>
		//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
		[Serializable]
		protected internal class DefineDialog:System.Windows.Forms.Form
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener
			{
				public AnonymousClassActionListener(DefineDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(DefineDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private DefineDialog enclosingInstance;
				public DefineDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					Enclosing_Instance.save();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener1
			{
				public AnonymousClassActionListener1(DefineDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(DefineDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private DefineDialog enclosingInstance;
				public DefineDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					Enclosing_Instance.cancel();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassWindowAdapter
			{
				public AnonymousClassWindowAdapter(DefineDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(DefineDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private DefineDialog enclosingInstance;
				public DefineDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs we)
				{
					we.Cancel = true;
					Enclosing_Instance.cancel();
				}
			}
			private void  InitBlock(MassPieceLoader enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MassPieceLoader enclosingInstance;
			virtual public bool Cancelled
			{
				get
				{
					return cancelled;
				}
				
			}
			public MassPieceLoader Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			protected internal bool cancelled = false;
			
			public DefineDialog(MassPieceLoader enclosingInstance, System.Windows.Forms.Form owner)
			{
				InitBlock(enclosingInstance);
				//UPGRADE_ISSUE: Constructor 'javax.swing.JDialog.JDialog' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJDialogJDialog'"
				SupportClass.DialogSupport.SetDialog(this, );
				//UPGRADE_ISSUE: Method 'java.awt.Dialog.setModal' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtDialogsetModal_boolean'"
				setModal(true);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				Visible = false;
				Text = "Multiple Piece Template";
				//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
				StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'box '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box box = Box.createVerticalBox();
				box.add(VassalSharp.counters.MassPieceLoader.definer);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'saveButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Button saveButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.SAVE));
				saveButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(saveButton);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'canButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Button canButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.CANCEL));
				canButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(canButton);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'bbox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box bbox = Box.createHorizontalBox();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				bbox.Controls.Add(saveButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				bbox.Controls.Add(canButton);
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(bbox);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(box);
				
				Closing += new System.ComponentModel.CancelEventHandler(this.DefineDialog_Closing_DO_NOTHING_ON_CLOSE);
				//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
				Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(this).windowClosing);
				
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				pack();
			}
			
			protected internal virtual void  save()
			{
				cancelled = false;
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				Visible = false;
			}
			
			protected internal virtual void  cancel()
			{
				cancelled = true;
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				Visible = false;
			}
			private void  DefineDialog_Closing_DO_NOTHING_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
			{
				e.Cancel = true;
				SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 0);
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MyTreeTableModel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> *************************************************************************
		/// Custom Tree table model:- - Return column count - Return column headings
		/// </summary>
		//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
		protected internal class MyTreeTableModel:DefaultTreeTableModel
		{
			private void  InitBlock(MassPieceLoader enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				if (column == VassalSharp.counters.MassPieceLoader.SKIP_COL)
				{
					return typeof(System.Boolean);
				}
				else if (column == VassalSharp.counters.MassPieceLoader.COPIES_COL)
				{
					return typeof(System.Int32);
				}
				else
				{
					return typeof(System.String);
				}
			}
			private MassPieceLoader enclosingInstance;
			virtual public int ColumnCount
			{
				get
				{
					return VassalSharp.counters.MassPieceLoader.COLUMN_COUNT;
				}
				
			}
			public MassPieceLoader Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			public MyTreeTableModel(MassPieceLoader enclosingInstance, BasicNode rootNode):base(rootNode)
			{
				InitBlock(enclosingInstance);
			}
			
			public virtual System.String getColumnName(int col)
			{
				switch (col)
				{
					
					case VassalSharp.counters.MassPieceLoader.DESC_COL: 
						return "Item";
					
					case VassalSharp.counters.MassPieceLoader.NAME_COL: 
						return "Piece Name";
					
					case VassalSharp.counters.MassPieceLoader.IMAGE_COL: 
						return "Image File";
					
					case VassalSharp.counters.MassPieceLoader.SKIP_COL: 
						return "Skip?";
					
					case VassalSharp.counters.MassPieceLoader.COPIES_COL: 
						return "Copies";
					}
				return "";
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public Class < ? > getColumnClass(int column)
			
			public virtual bool isCellEditable(System.Object node, int column)
			{
				if (node is PieceNode)
				{
					if (column == VassalSharp.counters.MassPieceLoader.NAME_COL || column == VassalSharp.counters.MassPieceLoader.COPIES_COL)
					{
						return !((PieceNode) node).isSkip();
					}
					else if (column == VassalSharp.counters.MassPieceLoader.SKIP_COL)
					{
						return true;
					}
				}
				return false;
			}
			
			public virtual System.Object getValueAt(System.Object node, int column)
			{
				return ((BasicNode) node).getValueAt(column);
			}
			
			public virtual void  setValueAt(System.Object value_Renamed, System.Object node, int column)
			{
				if (node is PieceNode)
				{
					if (column == VassalSharp.counters.MassPieceLoader.NAME_COL)
					{
						((PieceNode) node).setName((System.String) value_Renamed);
					}
					else if (column == VassalSharp.counters.MassPieceLoader.SKIP_COL)
					{
						((PieceNode) node).setSkip(((System.Boolean) value_Renamed));
					}
					else if (column == VassalSharp.counters.MassPieceLoader.COPIES_COL)
					{
						int val = value_Renamed == null?1:((System.Int32) value_Renamed);
						if (val < 1)
							val = 1;
						((PieceNode) node).Copies = val;
					}
				}
				else
				{
					base.setValueAt(value_Renamed, node, column);
				}
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MyTreeTable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Custom implementation of JXTreeTable Fix for bug on startup generating
		/// illegal column numbers
		/// 
		/// </summary>
		//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
		protected internal class MyTreeTable:JXTreeTable
		{
			private void  InitBlock(MassPieceLoader enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MassPieceLoader enclosingInstance;
			public MassPieceLoader Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			private const long serialVersionUID = 1L;
			
			public MyTreeTable(MassPieceLoader enclosingInstance):base()
			{
				InitBlock(enclosingInstance);
				setRootVisible(false);
			}
			
			//
			// public String getToolTipText(MouseEvent event) {
			// if (getComponentAt(event.getPoint().x, event.getPoint().y) == null)
			// return null;
			// return super.getToolTipText(event);
			// }
			
			// Hide the Skip checkbox on rows other than Piece rows
			//UPGRADE_ISSUE: Interface 'javax.swing.table.TableCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableTableCellRenderer'"
			public virtual TableCellRenderer getCellRenderer(int row, int column)
			{
				if (column == VassalSharp.counters.MassPieceLoader.SKIP_COL || column == VassalSharp.counters.MassPieceLoader.COPIES_COL)
				{
					if (!(this.getPathForRow(row).getLastPathComponent() is PieceNode))
					{
						return new NullRenderer(enclosingInstance);
					}
				}
				return base.getCellRenderer(row, column);
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'NullRenderer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Blank Cell Renderer</summary>
		//UPGRADE_ISSUE: Interface 'javax.swing.table.TableCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableTableCellRenderer'"
		internal class NullRenderer : TableCellRenderer
		{
			public NullRenderer(MassPieceLoader enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(MassPieceLoader enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MassPieceLoader enclosingInstance;
			public MassPieceLoader Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			public virtual System.Windows.Forms.Control getTableCellRendererComponent(System.Windows.Forms.DataGrid arg0, System.Object arg1, bool arg2, bool arg3, int arg4, int arg5)
			{
				System.Windows.Forms.Label temp_label;
				temp_label = new System.Windows.Forms.Label();
				temp_label.Text = "";
				return temp_label;
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'BasicNode' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> *************************************************************************
		/// Custom TreeTable Node
		/// </summary>
		//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
		protected internal class BasicNode:DefaultMutableTreeTableNode
		{
			private void  InitBlock(MassPieceLoader enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MassPieceLoader enclosingInstance;
			virtual public int Copies
			{
				get
				{
					return copies;
				}
				
				set
				{
					copies = value;
				}
				
			}
			virtual public System.String Description
			{
				get
				{
					return "Root";
				}
				
			}
			public MassPieceLoader Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			protected internal System.String name;
			protected internal System.String imageName;
			protected internal bool skip;
			protected internal int copies;
			
			public BasicNode(MassPieceLoader enclosingInstance):this(enclosingInstance, "", "")
			{
			}
			
			public BasicNode(MassPieceLoader enclosingInstance, System.String name, System.String imageName)
			{
				InitBlock(enclosingInstance);
				this.name = name;
				this.imageName = imageName;
				this.skip = false;
				this.copies = 1;
			}
			
			public virtual System.String getName()
			{
				return name;
			}
			
			public virtual void  setName(System.String name)
			{
				this.name = name;
			}
			
			public virtual System.String getImageName()
			{
				return imageName;
			}
			
			public virtual void  setImageName(System.String imageName)
			{
				this.imageName = imageName;
			}
			
			public virtual bool isSkip()
			{
				return skip;
			}
			
			public virtual void  setSkip(bool b)
			{
				skip = b;
			}
			
			public virtual System.Object getValueAt(int col)
			{
				switch (col)
				{
					
					case VassalSharp.counters.MassPieceLoader.DESC_COL: 
						return Description;
					
					case VassalSharp.counters.MassPieceLoader.NAME_COL: 
						return getName();
					
					case VassalSharp.counters.MassPieceLoader.IMAGE_COL: 
						return getImageName();
					
					case VassalSharp.counters.MassPieceLoader.SKIP_COL: 
						return Boolean.valueOf(isSkip());
					
					case VassalSharp.counters.MassPieceLoader.COPIES_COL: 
						return Integer.valueOf(Copies);
					}
				return "";
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'PieceNode' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Node representing a GamePiece to be loaded. imageName is the name of the
		/// Basic Piece image
		/// 
		/// </summary>
		//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
		public class PieceNode:BasicNode
		{
			private void  InitBlock(MassPieceLoader enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MassPieceLoader enclosingInstance;
			override public System.String Description
			{
				get
				{
					return "Piece";
				}
				
			}
			public new MassPieceLoader Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			internal bool noBasicPieceImage;
			
			public PieceNode(MassPieceLoader enclosingInstance, System.String imageName):base(enclosingInstance)
			{
				InitBlock(enclosingInstance);
				setImageName(imageName);
				//UPGRADE_NOTE: Final was removed from the declaration of 'info '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				PieceInfo info = pieceInfo.get_Renamed(imageName);
				if (info == null)
				{
					setName(ImageUtils.stripImageSuffix(imageName));
					setSkip(false);
				}
				else
				{
					setName(info.Name);
					setSkip(info.Skip);
				}
			}
			
			public override System.String getImageName()
			{
				if (VassalSharp.counters.MassPieceLoader.basicConfig.booleanValue())
				{
					return "";
				}
				else
				{
					return base.getImageName();
				}
			}
			
			public override void  setName(System.String name)
			{
				base.setName(name);
				pieceInfo.put(getImageName(), new PieceInfo(enclosingInstance, name, isSkip()));
			}
			
			public override void  setSkip(bool skip)
			{
				base.setSkip(skip);
				pieceInfo.put(getImageName(), new PieceInfo(enclosingInstance, getName(), skip));
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'LayerNode' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Node representing a Layer trait of a GamePiece</summary>
		internal class LayerNode:BasicNode
		{
			private void  InitBlock(MassPieceLoader enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MassPieceLoader enclosingInstance;
			override public System.String Description
			{
				get
				{
					return "Layer" + (name.Length > 0?" [" + name + "]":"");
				}
				
			}
			public new MassPieceLoader Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public LayerNode(MassPieceLoader enclosingInstance, System.String name):base(enclosingInstance, name, "")
			{
				InitBlock(enclosingInstance);
			}
			
			public override System.String getName()
			{
				return "";
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'LevelNode' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Node representing an individual Image Level within a Layer trait
		/// 
		/// </summary>
		internal class LevelNode:BasicNode
		{
			private void  InitBlock(MassPieceLoader enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MassPieceLoader enclosingInstance;
			override public System.String Description
			{
				get
				{
					return "Level " + (levelNumber + 1) + (name.Length > 0?" [" + name + "]":"");
				}
				
			}
			public new MassPieceLoader Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			internal int levelNumber;
			
			public LevelNode(MassPieceLoader enclosingInstance, System.String name, System.String imageName, int level):base(enclosingInstance, name, imageName)
			{
				InitBlock(enclosingInstance);
				levelNumber = level;
			}
			
			public override System.String getName()
			{
				return "";
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'PieceInfo' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Utility class to hold user changes about pieces - Updated piece name - Skip
		/// load flag
		/// 
		/// </summary>
		internal class PieceInfo
		{
			private void  InitBlock(MassPieceLoader enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MassPieceLoader enclosingInstance;
			virtual public System.String Name
			{
				get
				{
					return name;
				}
				
				set
				{
					this.name = value;
				}
				
			}
			virtual public bool Skip
			{
				get
				{
					return skip;
				}
				
				set
				{
					skip = value;
				}
				
			}
			public MassPieceLoader Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			protected internal System.String name;
			protected internal bool skip;
			
			public PieceInfo(MassPieceLoader enclosingInstance, System.String name, bool skip)
			{
				InitBlock(enclosingInstance);
				this.name = name;
				this.skip = skip;
			}
		}
		
		/// <summary> Subclass of Embellishment to allow us to directly manipulate its members</summary>
		internal class Emb:Embellishment
		{
			private void  InitBlock()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'base '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String base_Renamed = baseImage.split("\\.")[0];
				int count = 0;
				builtImages.clear();
				
				for (int i = 0; i < imageName.Length; i++)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'imageTemplate '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String imageTemplate = imageName[i];
					System.String thisImage = null;
					if (imageTemplate[0] == VassalSharp.counters.MassPieceLoader.BASE_IMAGE[0])
					{
						thisImage = baseImage;
					}
					else
					{
						//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
					}
					imageName[i] = thisImage;
					if (thisImage != null)
					{
						count++;
						builtImages.add(thisImage);
					}
				}
				return count > 0;
				return builtImages;
			}
			virtual public System.String[] ImageNames
			{
				get
				{
					return imageName;
				}
				
			}
			virtual public System.String[] LevelNames
			{
				get
				{
					return commonName;
				}
				
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private ArrayList < String > builtImages = new ArrayList < String >();
			
			public Emb():base()
			{
			}
			
			public Emb(System.String state, GamePiece p):base(state, p)
			{
			}
			
			// Return true if the specified file name matches the level
			// definition of any level in this layer
			public virtual bool matchLayer(System.String s)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(String levelName: imageName)
				{
					if (match(s.split("\\.")[0], levelName))
					{
						return true;
					}
				}
				return false;
			}
			
			protected internal virtual bool match(System.String s, System.String levelName)
			{
				if (levelName != null && levelName.Length > 1)
				{
					// Check 1 to skip
					// command char
					if (levelName[0] == VassalSharp.counters.MassPieceLoader.BASE_IMAGE[0])
					{
						return false;
					}
					else if (levelName[0] == VassalSharp.counters.MassPieceLoader.ENDS_WITH[0])
					{
						return s.EndsWith(levelName.Substring(1));
					}
					else if (levelName[0] == VassalSharp.counters.MassPieceLoader.INCLUDES[0])
					{
						return s.IndexOf(levelName.Substring(1)) >= 0;
					}
					else if (levelName[0] == VassalSharp.counters.MassPieceLoader.EQUALS[0])
					{
						return s.Equals(levelName.Substring(1));
					}
					else
					{
						try
						{
							return Pattern.matches(levelName.Substring(1), s);
						}
						catch (System.Exception ex)
						{
							// Invalid pattern
						}
					}
				}
				return false;
			}
			
			/// <summary> Set the actual layer images based on a base image and the layer image
			/// template specification.
			/// 
			/// </summary>
			/// <param name="baseImage">base Image name
			/// </param>
			/// <param name="levelImages">List of available level images
			/// </param>
			/// <returns> true if at least one layer built
			/// </returns>
			public bool buildLayers;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			(String baseImage, ArrayList < String > levelImages)
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public List < String > getBuiltImageList()
			
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'LoaderEd' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			public class LoaderEd:Embellishment.Ed
			{
				private void  InitBlock(Emb enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Emb enclosingInstance;
				override protected internal MultiImagePicker ImagePicker
				{
					get
					{
						return new LoaderImagePicker();
					}
					
				}
				public Emb Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public LoaderEd(Emb enclosingInstance, Embellishment e):base(e)
				{
					InitBlock(enclosingInstance);
				}
			}
			
			public override PieceEditor getEditor()
			{
				return new LoaderEd(this, this);
			}
		}
		
		/// <summary> Replacement class for the MultiImagePicker to specify image match strings</summary>
		[Serializable]
		internal class LoaderImagePicker:MultiImagePicker
		{
			public LoaderImagePicker()
			{
				InitBlock();
			}
			private void  InitBlock()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'size '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int size = imageListElements.Count;
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final ArrayList < String > names = new ArrayList < String >(size);
				for (int i = 0; i < size; ++i)
				{
					//UPGRADE_TODO: Method 'java.awt.Container.getComponent' was converted to 'System.Windows.Forms.Control.Controls' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContainergetComponent_int'"
					names.add((((Entry) multiPanel.Controls[i]).ToString()));
				}
				return names;
			}
			override public System.String[] ImageList
			{
				set
				{
					while (value.Length > (int) multiPanel.Controls.Count)
					{
						addEntry();
					}
					for (int i = 0; i < value.Length; ++i)
					{
						//UPGRADE_TODO: Method 'java.awt.Container.getComponent' was converted to 'System.Windows.Forms.Control.Controls' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContainergetComponent_int'"
						((Entry) multiPanel.Controls[i]).ImageName = value[i];
					}
				}
				
			}
			private const long serialVersionUID = 1L;
			
			public override void  addEntry()
			{
				System.String entry = "Image " + (imageListElements.Count + 1);
				imageListElements.Add(entry);
				Entry pick = new Entry();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javalangString_javaawtComponent'"
				multiPanel.Controls.Add(pick);
				//UPGRADE_TODO: Method 'javax.swing.JList.setSelectedIndex' was converted to 'System.Windows.Forms.ListBox.SelectedIndex' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListsetSelectedIndex_int'"
				imageList.SelectedIndex = imageListElements.Count - 1;
				//UPGRADE_ISSUE: Method 'java.awt.CardLayout.show' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
				cl.show(multiPanel, (System.String) imageList.SelectedItem);
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public List < String > getImageNameList()
			
			public override void  clear()
			{
				multiPanel.Controls.Clear();
				imageListElements.Clear();
			}
		}
		
		protected internal const System.String ENDS_WITH = "ends with";
		protected internal const System.String INCLUDES = "includes";
		protected internal const System.String MATCHES = "matches";
		protected internal const System.String EQUALS = "same as";
		protected internal const System.String BASE_IMAGE = "use Base Image";
		
		[Serializable]
		internal class Entry:System.Windows.Forms.Panel
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener
			{
				public AnonymousClassPropertyChangeListener(Entry enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Entry enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Entry enclosingInstance;
				public Entry Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
				{
					Enclosing_Instance.updateVisibility();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener1
			{
				public AnonymousClassPropertyChangeListener1(Entry enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Entry enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Entry enclosingInstance;
				public Entry Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
				{
					Enclosing_Instance.updateVisibility();
				}
			}
			private void  InitBlock()
			{
				System.Windows.Forms.Label temp_label;
				temp_label = new System.Windows.Forms.Label();
				temp_label.Text = "Warning - Image suffix included";
				warning = temp_label;
			}
			virtual public System.String ImageName
			{
				set
				{
					switch (value[0])
					{
						
						case 'e': 
							typeConfig.setValue(VassalSharp.counters.MassPieceLoader.ENDS_WITH);
							break;
						
						case 'i': 
							typeConfig.setValue(VassalSharp.counters.MassPieceLoader.INCLUDES);
							break;
						
						case 'm': 
							typeConfig.setValue(VassalSharp.counters.MassPieceLoader.MATCHES);
							break;
						
						case 's': 
							typeConfig.setValue(VassalSharp.counters.MassPieceLoader.EQUALS);
							break;
						
						case 'u': 
							typeConfig.setValue(VassalSharp.counters.MassPieceLoader.BASE_IMAGE);
							break;
						}
					
					nameConfig.setValue(value.Substring(1));
				}
				
			}
			private const long serialVersionUID = 1L;
			private StringEnumConfigurer typeConfig;
			private StringConfigurer nameConfig;
			//UPGRADE_NOTE: The initialization of  'warning' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private System.Windows.Forms.Label warning;
			
			public Entry()
			{
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = "Do NOT include image suffix (eg. .gif, .png) in level match strings";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				Controls.Add(temp_Control);
				
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box entry = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label4;
				temp_label4 = new System.Windows.Forms.Label();
				temp_label4.Text = "Image name ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = temp_label4;
				entry.Controls.Add(temp_Control2);
				typeConfig = new StringEnumConfigurer(null, "", new System.String[]{VassalSharp.counters.MassPieceLoader.ENDS_WITH, VassalSharp.counters.MassPieceLoader.INCLUDES, VassalSharp.counters.MassPieceLoader.MATCHES, VassalSharp.counters.MassPieceLoader.EQUALS, VassalSharp.counters.MassPieceLoader.BASE_IMAGE});
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				entry.Controls.Add(typeConfig.Controls);
				nameConfig = new StringConfigurer(null, "");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				entry.Controls.Add(nameConfig.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(entry);
				
				warning.Visible = false;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(warning);
				
				typeConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
				
				nameConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener1(this).propertyChange);
			}
			
			protected internal virtual void  updateVisibility()
			{
				warning.Visible = ImageUtils.hasImageSuffix(nameConfig.ValueString);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(nameConfig.Controls, "Visible", !typeConfig.ValueString.Equals(VassalSharp.counters.MassPieceLoader.BASE_IMAGE));
			}
			
			public override System.String ToString()
			{
				return typeConfig.ValueString[0] + nameConfig.ValueString;
			}
		}
	}
}