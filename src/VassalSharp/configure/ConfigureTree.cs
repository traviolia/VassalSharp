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
using Buildable = VassalSharp.build.Buildable;
using Builder = VassalSharp.build.Builder;
using Configurable = VassalSharp.build.Configurable;
using GameModule = VassalSharp.build.GameModule;
using IllegalBuildException = VassalSharp.build.IllegalBuildException;
using Plugin = VassalSharp.build.module.Plugin;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using HelpWindow = VassalSharp.build.module.documentation.HelpWindow;
using Zone = VassalSharp.build.module.map.boardPicker.board.mapgrid.Zone;
using GlobalProperties = VassalSharp.build.module.properties.GlobalProperties;
using GlobalProperty = VassalSharp.build.module.properties.GlobalProperty;
using ZoneProperty = VassalSharp.build.module.properties.ZoneProperty;
using CardSlot = VassalSharp.build.widget.CardSlot;
using PieceSlot = VassalSharp.build.widget.PieceSlot;
using MassPieceLoader = VassalSharp.counters.MassPieceLoader;
using Resources = VassalSharp.i18n.Resources;
using TranslateAction = VassalSharp.i18n.TranslateAction;
using EditorWindow = VassalSharp.launch.EditorWindow;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using ReflectionUtils = VassalSharp.tools.ReflectionUtils;
using MenuManager = VassalSharp.tools.menu.MenuManager;
namespace VassalSharp.configure
{
	
	/// <summary> This is the Configuration Tree that appears in the Configuration window
	/// when editing a VASSAL module. Each node in the tree structure is a
	/// {@link VassalSharp.build.Configurable} object, whose child nodes are obtained
	/// via {@link VassalSharp.build.Configurable#getConfigureComponents}.
	/// </summary>
	//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
	[Serializable]
	public class ConfigureTree:System.Windows.Forms.TreeView
	{
		static private System.Int32 state428;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction:SupportClass.ActionSupport
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener
			{
				public AnonymousClassActionListener(System.Windows.Forms.ComboBox select, int currentIndex, System.Windows.Forms.TreeNode targetNode, VassalSharp.build.Configurable target, System.Windows.Forms.Form d, AnonymousClassAbstractAction enclosingInstance)
				{
					InitBlock(select, currentIndex, targetNode, target, d, enclosingInstance);
				}
				private void  InitBlock(System.Windows.Forms.ComboBox select, int currentIndex, System.Windows.Forms.TreeNode targetNode, VassalSharp.build.Configurable target, System.Windows.Forms.Form d, AnonymousClassAbstractAction enclosingInstance)
				{
					this.select = select;
					this.currentIndex = currentIndex;
					this.targetNode = targetNode;
					this.target = target;
					this.d = d;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable select was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private System.Windows.Forms.ComboBox select;
				//UPGRADE_NOTE: Final variable currentIndex was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private int currentIndex;
				//UPGRADE_NOTE: Final variable targetNode was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private System.Windows.Forms.TreeNode targetNode;
				//UPGRADE_NOTE: Final variable target was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private VassalSharp.build.Configurable target;
				//UPGRADE_NOTE: Final variable d was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private System.Windows.Forms.Form d;
				private AnonymousClassAbstractAction enclosingInstance;
				public AnonymousClassAbstractAction Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					int index = select.SelectedIndex;
					if (currentIndex != index)
					{
						Configurable parent = Enclosing_Instance.Enclosing_Instance.getParent(targetNode);
						if (Enclosing_Instance.Enclosing_Instance.remove(parent, target))
						{
							Enclosing_Instance.Enclosing_Instance.insert(parent, target, index);
						}
					}
					d.Dispose();
				}
			}
			private void  InitBlock(VassalSharp.build.Configurable target, ConfigureTree enclosingInstance)
			{
				this.target = target;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable target was copied into class AnonymousClassAbstractAction. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.build.Configurable target;
			private ConfigureTree enclosingInstance;
			public ConfigureTree Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction(VassalSharp.build.Configurable target, ConfigureTree enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(target, enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.CreateDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtFrame_boolean'"
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.getAncestorOfClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
				System.Windows.Forms.Form d = SupportClass.DialogSupport.CreateDialog((System.Windows.Forms.Form) SwingUtilities.getAncestorOfClass(typeof(System.Windows.Forms.Form), Enclosing_Instance));
				d.Text = target.getConfigureName() == null?Enclosing_Instance.moveCmd:Enclosing_Instance.moveCmd + " " + target.getConfigureName();
				//UPGRADE_ISSUE: Method 'javax.swing.JDialog.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJDialogsetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				d.setLayout(new BoxLayout(((System.Windows.Forms.ContainerControl) d), BoxLayout.Y_AXIS));
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box box = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = "Move to position";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				box.Controls.Add(temp_Control);
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalStrut' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = Box.createHorizontalStrut(10);
				box.Controls.Add(temp_Control2);
				//UPGRADE_NOTE: Final was removed from the declaration of 'select '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.ComboBox select = new System.Windows.Forms.ComboBox();
				System.Windows.Forms.TreeNode parentNode = Enclosing_Instance.getTreeNode(target).Parent;
				for (int i = 0; i < parentNode.Nodes.Count; ++i)
				{
					Configurable c = (Configurable) ((System.Windows.Forms.TreeNode) parentNode.Nodes[i]).Tag;
					System.String name = (c.getConfigureName() != null?c.getConfigureName():"") + " [" + getConfigureName(c.GetType()) + "]";
					select.Items.Add((i + 1) + ":  " + name);
				}
				//UPGRADE_NOTE: Final was removed from the declaration of 'targetNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.TreeNode targetNode = Enclosing_Instance.getTreeNode(target);
				//UPGRADE_NOTE: Final was removed from the declaration of 'currentIndex '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int currentIndex = targetNode.Parent.Nodes.IndexOf(targetNode);
				select.SelectedIndex = currentIndex;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(select);
				System.Windows.Forms.Button ok = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.OK));
				ok.Click += new System.EventHandler(new AnonymousClassActionListener(select, currentIndex, targetNode, target, d, this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(ok);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				d.Controls.Add(box);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				d.Controls.Add(ok);
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				d.pack();
				System.Windows.Forms.Control generatedAux6 = d.Parent;
				//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
				d.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(d, "Visible", true);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction1:SupportClass.ActionSupport
		{
			private void  InitBlock(VassalSharp.build.Configurable target, ConfigureTree enclosingInstance)
			{
				this.target = target;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable target was copied into class AnonymousClassAbstractAction1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.build.Configurable target;
			private ConfigureTree enclosingInstance;
			public ConfigureTree Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction1(VassalSharp.build.Configurable target, ConfigureTree enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(target, enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.cutData = Enclosing_Instance.getTreeNode(target);
				Enclosing_Instance.copyData = null;
				Enclosing_Instance.updateEditMenu();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction2:SupportClass.ActionSupport
		{
			private void  InitBlock(VassalSharp.build.Configurable target, ConfigureTree enclosingInstance)
			{
				this.target = target;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable target was copied into class AnonymousClassAbstractAction2. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.build.Configurable target;
			private ConfigureTree enclosingInstance;
			public ConfigureTree Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction2(VassalSharp.build.Configurable target, ConfigureTree enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(target, enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.copyData = Enclosing_Instance.getTreeNode(target);
				Enclosing_Instance.cutData = null;
				Enclosing_Instance.updateEditMenu();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction3:SupportClass.ActionSupport
		{
			private void  InitBlock(VassalSharp.build.Configurable target, ConfigureTree enclosingInstance)
			{
				this.target = target;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable target was copied into class AnonymousClassAbstractAction3. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.build.Configurable target;
			private ConfigureTree enclosingInstance;
			public ConfigureTree Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction3(VassalSharp.build.Configurable target, ConfigureTree enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(target, enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				if (Enclosing_Instance.cutData != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'targetNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Windows.Forms.TreeNode targetNode = Enclosing_Instance.getTreeNode(target);
					//UPGRADE_NOTE: Final was removed from the declaration of 'cutObj '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Configurable cutObj = (Configurable) Enclosing_Instance.cutData.Tag;
					//UPGRADE_NOTE: Final was removed from the declaration of 'convertedCutObj '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Configurable convertedCutObj = Enclosing_Instance.convertChild(target, cutObj);
					if (Enclosing_Instance.remove(Enclosing_Instance.getParent(Enclosing_Instance.cutData), cutObj))
					{
						Enclosing_Instance.insert(target, convertedCutObj, targetNode.Nodes.Count);
					}
					Enclosing_Instance.copyData = Enclosing_Instance.getTreeNode(convertedCutObj);
				}
				else if (Enclosing_Instance.copyData != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'copyBase '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Configurable copyBase = (Configurable) Enclosing_Instance.copyData.Tag;
					Configurable clone = null;
					try
					{
						clone = convertChild(target, copyBase.GetType().getConstructor().newInstance());
					}
					//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
					catch (System.Exception t)
					{
						ReflectionUtils.handleNewInstanceFailure(t, copyBase.GetType());
					}
					
					if (clone != null)
					{
						clone.build(copyBase.getBuildElement(Builder.createNewDocument()));
						Enclosing_Instance.insert(target, clone, Enclosing_Instance.getTreeNode(target).Nodes.Count);
						Enclosing_Instance.updateGpIds(clone);
					}
				}
				Enclosing_Instance.cutData = null;
				Enclosing_Instance.updateEditMenu();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction4' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction4:SupportClass.ActionSupport
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertiesWindow' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertiesWindow:PropertiesWindow
			{
				private void  InitBlock(VassalSharp.build.Configurable c, VassalSharp.build.Configurable child, AnonymousClassAbstractAction4 enclosingInstance)
				{
					this.c = c;
					this.child = child;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable c was copied into class AnonymousClassPropertiesWindow. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private VassalSharp.build.Configurable c;
				//UPGRADE_NOTE: Final variable child was copied into class AnonymousClassPropertiesWindow. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private VassalSharp.build.Configurable child;
				private AnonymousClassAbstractAction4 enclosingInstance;
				public AnonymousClassAbstractAction4 Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
				internal AnonymousClassPropertiesWindow(VassalSharp.build.Configurable c, VassalSharp.build.Configurable child, AnonymousClassAbstractAction4 enclosingInstance, System.Windows.Forms.Form Param1, bool Param2, VassalSharp.build.Configurable Param3, VassalSharp.build.module.documentation.HelpWindow Param4):base(Param1, Param2, Param3, Param4)
				{
					InitBlock(c, child, enclosingInstance);
				}
				private const long serialVersionUID = 1L;
				
				public override void  save()
				{
					base.save();
					Enclosing_Instance.Enclosing_Instance.insert(c, child, Enclosing_Instance.Enclosing_Instance.getTreeNode(c).Nodes.Count);
				}
				
				public override void  cancel()
				{
					Dispose();
				}
			}
			private void  InitBlock(VassalSharp.build.Configurable target, ConfigureTree enclosingInstance)
			{
				this.target = target;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable target was copied into class AnonymousClassAbstractAction4. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.build.Configurable target;
			private ConfigureTree enclosingInstance;
			public ConfigureTree Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction4(VassalSharp.build.Configurable target, ConfigureTree enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(target, enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'child '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Configurable child = Enclosing_Instance.importConfigurable();
				if (child != null)
				{
					try
					{
						child.build(null);
						//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						Configurable c = target;
						if (child.Configurer != null)
						{
							//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.getAncestorOfClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
							//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
							PropertiesWindow w = new AnonymousClassPropertiesWindow(c, child, this, (System.Windows.Forms.Form) SwingUtilities.getAncestorOfClass(typeof(System.Windows.Forms.Form), Enclosing_Instance), false, child, Enclosing_Instance.helpWindow);
							//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
							//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
							w.Visible = true;
						}
						else
						{
							Enclosing_Instance.insert(c, child, Enclosing_Instance.getTreeNode(c).Nodes.Count);
						}
					}
					// FIXME: review error message
					catch (System.Exception ex)
					{
						//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						SupportClass.OptionPaneSupport.ShowMessageDialog(Enclosing_Instance.getTopLevelAncestor(), "Error adding " + VassalSharp.configure.ConfigureTree.getConfigureName(child) + " to " + VassalSharp.configure.ConfigureTree.getConfigureName(target) + "\n" + ex.Message, "Illegal configuration", (int) System.Windows.Forms.MessageBoxIcon.Error);
					}
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction5' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassAbstractAction5:SupportClass.ActionSupport
		{
			private void  InitBlock(VassalSharp.configure.ConfigureTree tree, VassalSharp.build.Configurable target, ConfigureTree enclosingInstance)
			{
				this.tree = tree;
				this.target = target;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable tree was copied into class AnonymousClassAbstractAction5. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.ConfigureTree tree;
			//UPGRADE_NOTE: Final variable target was copied into class AnonymousClassAbstractAction5. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.build.Configurable target;
			private ConfigureTree enclosingInstance;
			public ConfigureTree Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction5(VassalSharp.configure.ConfigureTree tree, VassalSharp.build.Configurable target, ConfigureTree enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(tree, target, enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				new MassPieceLoader(tree, target).load();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction6' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction6:SupportClass.ActionSupport
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertiesWindow' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertiesWindow:PropertiesWindow
			{
				private void  InitBlock(VassalSharp.build.Configurable c, VassalSharp.build.Configurable child, AnonymousClassAbstractAction6 enclosingInstance)
				{
					this.c = c;
					this.child = child;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable c was copied into class AnonymousClassPropertiesWindow. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private VassalSharp.build.Configurable c;
				//UPGRADE_NOTE: Final variable child was copied into class AnonymousClassPropertiesWindow. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private VassalSharp.build.Configurable child;
				private AnonymousClassAbstractAction6 enclosingInstance;
				public AnonymousClassAbstractAction6 Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
				internal AnonymousClassPropertiesWindow(VassalSharp.build.Configurable c, VassalSharp.build.Configurable child, AnonymousClassAbstractAction6 enclosingInstance, System.Windows.Forms.Form Param1, bool Param2, VassalSharp.build.Configurable Param3, VassalSharp.build.module.documentation.HelpWindow Param4):base(Param1, Param2, Param3, Param4)
				{
					InitBlock(c, child, enclosingInstance);
				}
				private const long serialVersionUID = 1L;
				
				public override void  save()
				{
					base.save();
				}
				
				public override void  cancel()
				{
					Enclosing_Instance.Enclosing_Instance.remove(c, child);
					Dispose();
				}
			}
			private void  InitBlock(ConfigureTree enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ConfigureTree enclosingInstance;
			public ConfigureTree Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction6(ConfigureTree enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				Configurable ch = null;
				try
				{
					ch = (Configurable) newConfig.getConstructor().newInstance();
				}
				//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				catch (System.Exception t)
				{
					ReflectionUtils.handleNewInstanceFailure(t, newConfig);
				}
				
				if (ch != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'child '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Configurable child = ch;
					child.build(null);
					
					if (child is PieceSlot)
					{
						((PieceSlot) child).updateGpId(GameModule.getGameModule());
					}
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Configurable c = target;
					if (child.Configurer != null)
					{
						if (insert(target, child, getTreeNode(target).getChildCount()))
						{
							//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.getAncestorOfClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
							//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
							PropertiesWindow w = new AnonymousClassPropertiesWindow(c, child, this, (System.Windows.Forms.Form) SwingUtilities.getAncestorOfClass(typeof(System.Windows.Forms.Form), Enclosing_Instance), false, child, Enclosing_Instance.helpWindow);
							//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
							//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
							w.Visible = true;
						}
					}
					else
					{
						Enclosing_Instance.insert(c, child, Enclosing_Instance.getTreeNode(c).Nodes.Count);
					}
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction7' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction7:SupportClass.ActionSupport
		{
			private void  InitBlock(VassalSharp.build.Configurable target, System.Windows.Forms.TreeNode targetNode, ConfigureTree enclosingInstance)
			{
				this.target = target;
				this.targetNode = targetNode;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable target was copied into class AnonymousClassAbstractAction7. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.build.Configurable target;
			//UPGRADE_NOTE: Final variable targetNode was copied into class AnonymousClassAbstractAction7. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.TreeNode targetNode;
			private ConfigureTree enclosingInstance;
			public ConfigureTree Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction7(VassalSharp.build.Configurable target, System.Windows.Forms.TreeNode targetNode, ConfigureTree enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(target, targetNode, enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				Configurable clone = null;
				try
				{
					clone = target.GetType().getConstructor().newInstance();
				}
				//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				catch (System.Exception t)
				{
					ReflectionUtils.handleNewInstanceFailure(t, target.GetType());
				}
				
				if (clone != null)
				{
					clone.build(target.getBuildElement(Builder.createNewDocument()));
					Enclosing_Instance.insert(Enclosing_Instance.getParent(targetNode), clone, targetNode.Parent.Nodes.IndexOf(targetNode) + 1);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction8' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction8:SupportClass.ActionSupport
		{
			private void  InitBlock(VassalSharp.build.Configurable parent, VassalSharp.build.Configurable target, ConfigureTree enclosingInstance)
			{
				this.parent = parent;
				this.target = target;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable parent was copied into class AnonymousClassAbstractAction8. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.build.Configurable parent;
			//UPGRADE_NOTE: Final variable target was copied into class AnonymousClassAbstractAction8. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.build.Configurable target;
			private ConfigureTree enclosingInstance;
			public ConfigureTree Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction8(VassalSharp.build.Configurable parent, VassalSharp.build.Configurable target, ConfigureTree enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(parent, target, enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				int row = Enclosing_Instance.selectedRow;
				Enclosing_Instance.remove(parent, target);
				//UPGRADE_ISSUE: Method 'javax.swing.JTree.getRowCount' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreegetRowCount'"
				if (row < Enclosing_Instance.getRowCount())
				{
					//UPGRADE_ISSUE: Method 'javax.swing.JTree.setSelectionRow' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreesetSelectionRow_int'"
					Enclosing_Instance.setSelectionRow(row);
				}
				else
				{
					//UPGRADE_ISSUE: Method 'javax.swing.JTree.setSelectionRow' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreesetSelectionRow_int'"
					Enclosing_Instance.setSelectionRow(row - 1);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPopupMenuListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPopupMenuListener
		{
			public AnonymousClassPopupMenuListener(ConfigureTree enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ConfigureTree enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ConfigureTree enclosingInstance;
			public ConfigureTree Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  popupMenuCanceled(System.Object event_sender, System.EventArgs evt)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				Enclosing_Instance.Refresh();
			}
			
			public virtual void  popupMenuWillBecomeInvisible(System.Object event_sender, System.EventArgs evt)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				Enclosing_Instance.Refresh();
			}
			
			public virtual void  popupMenuWillBecomeVisible(System.Object event_sender, System.EventArgs evt)
			{
			}
		}
		private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			state428 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		private void  InitBlock()
		{
			bool empty = true;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Action a: l)
			{
				if (a != null)
				{
					menu.add(a).setFont(POPUP_MENU_FONT);
					empty = false;
				}
			}
			if (!empty)
			{
				menu.addSeparator();
			}
			l.clear();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final Class < ? > [] allowableClasses = parent.getAllowableConfigureComponents();
			for (int i = 0; i < allowableClasses.length; i++)
			{
				if (allowableClasses[i] == childClass)
				{
					return true;
				}
			}
			return false;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Class < ? > c: parent.getAllowableConfigureComponents())
			{
				if (c.equals(childClass))
				{
					return true;
				}
			}
			return false;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final ArrayList < Action > l = new ArrayList < Action >();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Class < ? extends Buildable > newConfig: 
			target.getAllowableConfigureComponents())
			{
				l.add(buildAddAction(target, newConfig));
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(AdditionalComponent add: additionalComponents)
			{
				if (target.getClass().equals(add.getParent()))
				{
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					final Class < ? extends Buildable > newConfig = add.getChild();
					l.add(buildAddAction(target, newConfig));
				}
			}
			return l;
			return Collections.enumeration(buildAddActionsFor(target));
			SupportClass.ActionSupport action = new AnonymousClassAbstractAction6(this, "Add " + getConfigureName(newConfig));
			return action;
			try
			{
				return (System.String) c.getMethod("getConfigureTypeName").invoke(null);
			}
			catch (System.MethodAccessException e)
			{
				// Ignore. This is normal, since some classes won't have this method.
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
			catch (System.NullReferenceException e)
			{
				ErrorDialog.bug(e);
			}
			//UPGRADE_NOTE: Exception 'java.lang.ExceptionInInitializerError' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			catch (System.Exception e)
			{
				ErrorDialog.bug(e);
			}
			
			return c.getName().substring(c.getName().lastIndexOf(".") + 1);
			additionalComponents.add(new AdditionalComponent(parent, child));
		}
		virtual public System.Windows.Forms.Form Frame
		{
			get
			{
				return editorWindow;
			}
			
		}
		virtual public SupportClass.ActionSupport HelpAction
		{
			get
			{
				return helpAction;
			}
			
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected Map < Configurable, DefaultMutableTreeNode > nodes = 
		new HashMap < Configurable, DefaultMutableTreeNode >();
		protected internal System.Windows.Forms.TreeNode copyData;
		protected internal System.Windows.Forms.TreeNode cutData;
		protected internal HelpWindow helpWindow;
		protected internal EditorWindow editorWindow;
		protected internal Configurable selected;
		protected internal int selectedRow;
		protected internal System.String moveCmd;
		protected internal System.String deleteCmd;
		protected internal System.String pasteCmd;
		protected internal System.String copyCmd;
		protected internal System.String cutCmd;
		protected internal System.String helpCmd;
		protected internal System.String propertiesCmd;
		protected internal System.String translateCmd;
		protected internal System.Windows.Forms.KeyEventArgs cutKey;
		protected internal System.Windows.Forms.KeyEventArgs copyKey;
		protected internal System.Windows.Forms.KeyEventArgs pasteKey;
		protected internal System.Windows.Forms.KeyEventArgs deleteKey;
		protected internal System.Windows.Forms.KeyEventArgs moveKey;
		protected internal System.Windows.Forms.KeyEventArgs helpKey;
		protected internal System.Windows.Forms.KeyEventArgs propertiesKey;
		protected internal System.Windows.Forms.KeyEventArgs translateKey;
		protected internal SupportClass.ActionSupport cutAction;
		protected internal SupportClass.ActionSupport copyAction;
		protected internal SupportClass.ActionSupport pasteAction;
		protected internal SupportClass.ActionSupport deleteAction;
		protected internal SupportClass.ActionSupport moveAction;
		protected internal SupportClass.ActionSupport propertiesAction;
		protected internal SupportClass.ActionSupport translateAction;
		protected internal SupportClass.ActionSupport helpAction;
		
		//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
		public static System.Drawing.Font POPUP_MENU_FONT = new System.Drawing.Font("Dialog", 11, System.Drawing.FontStyle.Regular);
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected static List < AdditionalComponent > additionalComponents = 
		new ArrayList < AdditionalComponent >();
		
		/// <summary>Creates new ConfigureTree </summary>
		public ConfigureTree(Configurable root, HelpWindow helpWindow):this(root, helpWindow, null)
		{
		}
		
		public ConfigureTree(Configurable root, HelpWindow helpWindow, EditorWindow editorWindow)
		{
			InitBlock();
			//UPGRADE_ISSUE: Field 'javax.swing.JTree.toggleClickCount' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreetoggleClickCount_f'"
			toggleClickCount = 3;
			this.helpWindow = helpWindow;
			this.editorWindow = editorWindow;
			ShowRootLines = true;
			//UPGRADE_TODO: Constructor 'javax.swing.tree.DefaultTreeModel.DefaultTreeModel' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtreeDefaultTreeModelDefaultTreeModel_javaxswingtreeTreeNode'"
			SupportClass.TreeSupport.SetModel(this, buildTreeNode(root));
			//UPGRADE_ISSUE: Method 'javax.swing.JTree.setCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreesetCellRenderer_javaxswingtreeTreeCellRenderer'"
			setCellRenderer(buildRenderer());
			MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.configure.ConfigureTree.mouseDown);
			Click += new System.EventHandler(this.mouseClicked);
			MouseEnter += new System.EventHandler(this.mouseEntered);
			MouseLeave += new System.EventHandler(this.mouseExited);
			MouseDown += new System.Windows.Forms.MouseEventHandler(this.mousePressed);
			MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseReleased);
			MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouseMoved);
			AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.valueChanged);
			moveCmd = Resources.getString("Editor.move"); //$NON-NLS-1$
			deleteCmd = Resources.getString("Editor.delete"); //$NON-NLS-1$
			pasteCmd = Resources.getString("Editor.paste"); //$NON-NLS-1$
			copyCmd = Resources.getString("Editor.copy"); //$NON-NLS-1$
			cutCmd = Resources.getString("Editor.cut"); //$NON-NLS-1$
			propertiesCmd = Resources.getString("Editor.ModuleEditor.properties"); //$NON-NLS-1$
			translateCmd = Resources.getString("Editor.ModuleEditor.translate"); //$NON-NLS-1$
			helpCmd = Resources.getString("Editor.ModuleEditor.component_help"); //$NON-NLS-1$
			//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getMenuShortcutKeyMask' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
			//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDefaultToolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
			int mask = Toolkit.getDefaultToolkit().getMenuShortcutKeyMask();
			cutKey = new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) ((int) System.Windows.Forms.Keys.X | mask));
			copyKey = new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) ((int) System.Windows.Forms.Keys.C | mask));
			pasteKey = new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) ((int) System.Windows.Forms.Keys.V | mask));
			deleteKey = new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) ((int) System.Windows.Forms.Keys.Delete | 0));
			moveKey = new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) ((int) System.Windows.Forms.Keys.M | mask));
			propertiesKey = new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) ((int) System.Windows.Forms.Keys.P | mask));
			translateKey = new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) ((int) System.Windows.Forms.Keys.T | mask));
			helpKey = new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) ((int) System.Windows.Forms.Keys.F1 | 0));
			copyAction = new KeyAction(this, copyCmd, copyKey);
			pasteAction = new KeyAction(this, pasteCmd, pasteKey);
			cutAction = new KeyAction(this, cutCmd, cutKey);
			deleteAction = new KeyAction(this, deleteCmd, deleteKey);
			moveAction = new KeyAction(this, moveCmd, moveKey);
			propertiesAction = new KeyAction(this, propertiesCmd, propertiesKey);
			translateAction = new KeyAction(this, translateCmd, translateKey);
			helpAction = new KeyAction(this, helpCmd, helpKey);
			/*
			* Cut, Copy and Paste will not work unless I add them to the JTree input and action maps. Why??? All the others
			* work fine.
			*/
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getInputMap' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetInputMap'"
			getInputMap().Add((System.Object) cutKey, cutCmd);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getInputMap' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetInputMap'"
			getInputMap().Add((System.Object) copyKey, copyCmd);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getInputMap' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetInputMap'"
			getInputMap().Add((System.Object) pasteKey, pasteCmd);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getInputMap' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetInputMap'"
			getInputMap().Add((System.Object) deleteKey, deleteCmd);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getActionMap' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetActionMap'"
			getActionMap().Add(cutCmd, (System.Object) cutAction);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getActionMap' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetActionMap'"
			getActionMap().Add(copyCmd, (System.Object) copyAction);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getActionMap' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetActionMap'"
			getActionMap().Add(pasteCmd, (System.Object) pasteAction);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getActionMap' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetActionMap'"
			getActionMap().Add(deleteCmd, (System.Object) deleteAction);
			//UPGRADE_ISSUE: Method 'javax.swing.tree.TreeSelectionModel.setSelectionMode' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreeSelectionModel'"
			//UPGRADE_ISSUE: Method 'javax.swing.JTree.getSelectionModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreegetSelectionModel'"
			//UPGRADE_ISSUE: Field 'javax.swing.tree.TreeSelectionModel.SINGLE_TREE_SELECTION' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreeSelectionModel'"
			this.getSelectionModel().setSelectionMode(TreeSelectionModel.SINGLE_TREE_SELECTION);
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'KeyAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		internal class KeyAction:SupportClass.ActionSupport
		{
			private void  InitBlock(ConfigureTree enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ConfigureTree enclosingInstance;
			public ConfigureTree Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			protected internal System.String actionName;
			
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			public KeyAction(ConfigureTree enclosingInstance, System.String name, System.Windows.Forms.KeyEventArgs key):base(name)
			{
				InitBlock(enclosingInstance);
				actionName = name;
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
				//UPGRADE_ISSUE: Field 'javax.swing.Action.ACCELERATOR_KEY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionACCELERATOR_KEY_f'"
				putValue(Action.ACCELERATOR_KEY, key);
			}
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.doKeyAction(actionName);
			}
		}
		
		protected internal virtual Renderer buildRenderer()
		{
			return new Renderer();
		}
		
		/// <summary> Tell our enclosing EditorWindow that we are now clean
		/// or dirty.
		/// 
		/// </summary>
		/// <param name="changed">true = state is not dirty
		/// </param>
		protected internal virtual void  notifyStateChanged(bool changed)
		{
			if (editorWindow != null)
			{
				editorWindow.treeStateChanged(changed);
			}
		}
		
		protected internal virtual Configurable getTarget(int x, int y)
		{
			//UPGRADE_ISSUE: Class 'javax.swing.tree.TreePath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
			//UPGRADE_ISSUE: Method 'javax.swing.JTree.getPathForLocation' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreegetPathForLocation_int_int'"
			TreePath path = getPathForLocation(x, y);
			Configurable target = null;
			if (path != null)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.tree.TreePath.getLastPathComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
				target = (Configurable) ((System.Windows.Forms.TreeNode) path.getLastPathComponent()).Tag;
			}
			return target;
		}
		
		protected internal virtual System.Windows.Forms.TreeNode buildTreeNode(Configurable c)
		{
			c.PropertyChange += new SupportClass.PropertyChangeEventHandler(this.propertyChange);
			//UPGRADE_NOTE: Final was removed from the declaration of 'node '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode(c.ToString());
			//UPGRADE_NOTE: Final was removed from the declaration of 'children '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Configurable[] children = c.ConfigureComponents;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Configurable child: children)
			{
				if (!(child is Plugin))
				{
					// Hide Plug-ins
					node.add(buildTreeNode(child));
				}
			}
			nodes.put(c, node);
			return node;
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		protected internal virtual void  addAction(System.Windows.Forms.ContextMenu menu, SupportClass.ActionSupport a)
		{
			if (a != null)
			{
				System.Windows.Forms.MenuItem temp_MenuItem;
				temp_MenuItem = new System.Windows.Forms.MenuItem();
				temp_MenuItem.Click += new System.EventHandler(a.actionPerformed);
				menu.MenuItems.Add(temp_MenuItem);
				temp_MenuItem.Font = POPUP_MENU_FONT;
			}
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void addActionGroup(JPopupMenu menu, ArrayList < Action > l)
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		protected internal virtual System.Windows.Forms.ContextMenu buildPopupMenu(Configurable target)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'popup '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Windows.Forms.ContextMenu popup = new System.Windows.Forms.ContextMenu();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final ArrayList < Action > l = new ArrayList < Action >();
			l.add(buildEditAction(target));
			l.add(buildEditPiecesAction(target));
			addActionGroup(popup, l);
			l.add(buildTranslateAction(target));
			addActionGroup(popup, l);
			l.add(buildHelpAction(target));
			addActionGroup(popup, l);
			l.add(buildDeleteAction(target));
			l.add(buildCutAction(target));
			l.add(buildCopyAction(target));
			l.add(buildPasteAction(target));
			l.add(buildMoveAction(target));
			addActionGroup(popup, l);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Action a: buildAddActionsFor(target))
			{
				addAction(popup, a);
			}
			if (hasChild(target, typeof(PieceSlot)) || hasChild(target, typeof(CardSlot)))
			{
				addAction(popup, buildMassPieceLoaderAction(target));
			}
			addAction(popup, buildImportAction(target));
			return popup;
		}
		
		protected internal virtual SupportClass.ActionSupport buildMoveAction(Configurable target)
		{
			SupportClass.ActionSupport a = null;
			if (getTreeNode(target).Parent != null)
			{
				a = new AnonymousClassAbstractAction(target, this, moveCmd);
			}
			return a;
		}
		
		protected internal virtual SupportClass.ActionSupport buildCutAction(Configurable target)
		{
			SupportClass.ActionSupport a = null;
			if (getTreeNode(target).Parent != null)
			{
				a = new AnonymousClassAbstractAction1(target, this, cutCmd);
			}
			return a;
		}
		
		protected internal virtual SupportClass.ActionSupport buildCopyAction(Configurable target)
		{
			SupportClass.ActionSupport a = null;
			if (getTreeNode(target).Parent != null)
			{
				a = new AnonymousClassAbstractAction2(target, this, copyCmd);
			}
			return a;
		}
		
		protected internal virtual SupportClass.ActionSupport buildPasteAction(Configurable target)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'a '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SupportClass.ActionSupport a = new AnonymousClassAbstractAction3(target, this, pasteCmd);
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			a.setEnabled(isValidPasteTarget(target));
			return a;
		}
		
		protected internal virtual bool isValidPasteTarget(Configurable target)
		{
			return (cutData != null && isValidParent(target, (Configurable) cutData.Tag)) || (copyData != null && isValidParent(target, (Configurable) copyData.Tag));
		}
		
		/// <summary> Some components need to be converted to a new type before insertion.
		/// 
		/// Currently this is used to allow cut and paste of CardSlots and PieceSlots
		/// between Decks and GamePiece Palette components.
		/// 
		/// </summary>
		/// <param name="parent">
		/// </param>
		/// <param name="child">
		/// </param>
		/// <returns> new Child
		/// </returns>
		protected internal virtual Configurable convertChild(Configurable parent, Configurable child)
		{
			if (child.GetType() == typeof(PieceSlot) && isAllowedChildClass(parent, typeof(CardSlot)))
			{
				return new CardSlot((PieceSlot) child);
			}
			else if (child.GetType() == typeof(CardSlot) && isAllowedChildClass(parent, typeof(PieceSlot)))
			{
				return new PieceSlot((CardSlot) child);
			}
			else
			{
				return child;
			}
		}
		
		protected internal bool isAllowedChildClass;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(Configurable parent, Class < ? > childClass)
		
		/// <summary> Allocate new PieceSlot Id's to any PieceSlot sub-components
		/// 
		/// </summary>
		/// <param name="c">Configurable to update
		/// </param>
		public virtual void  updateGpIds(Configurable c)
		{
			if (c is PieceSlot)
			{
				((PieceSlot) c).updateGpId(GameModule.getGameModule());
			}
			else
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Configurable comp: c.getConfigureComponents()) updateGpIds(comp);
			}
		}
		
		protected internal virtual SupportClass.ActionSupport buildImportAction(Configurable target)
		{
			SupportClass.ActionSupport a = new AnonymousClassAbstractAction4(target, this, "Add Imported Class");
			return a;
		}
		
		
		protected internal virtual SupportClass.ActionSupport buildMassPieceLoaderAction(Configurable target)
		{
			SupportClass.ActionSupport a = null;
			//UPGRADE_NOTE: Final was removed from the declaration of 'tree '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			ConfigureTree tree = this;
			if (getTreeNode(target).Parent != null)
			{
				System.String desc = "Add Multiple " + (hasChild(target, typeof(CardSlot))?"Cards":"Pieces");
				a = new AnonymousClassAbstractAction5(tree, target, this, desc);
			}
			return a;
		}
		
		protected internal bool hasChild;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(Configurable parent, Class < ? > childClass)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < Action > buildAddActionsFor(final Configurable target)
		
		/// <deprecated> Use {@link #buildAddActionsFor(final Configurable)} instead.
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected Enumeration < Action > buildAddActions(final Configurable target)
		
		protected internal SupportClass.ActionSupport buildAddAction;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(final Configurable target, final Class < ? extends Buildable > newConfig)
		
		protected internal virtual SupportClass.ActionSupport buildHelpAction(Configurable target)
		{
			SupportClass.ActionSupport showHelp;
			HelpFile helpFile = target.getHelpFile();
			if (helpFile == null)
			{
				showHelp = new ShowHelpAction(null, null);
				//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
				showHelp.setEnabled(false);
			}
			else
			{
				showHelp = new ShowHelpAction(helpFile.Contents, null);
			}
			return showHelp;
		}
		
		protected internal virtual SupportClass.ActionSupport buildCloneAction(Configurable target)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'targetNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.TreeNode targetNode = getTreeNode(target);
			if (targetNode.Parent != null)
			{
				return new AnonymousClassAbstractAction7(target, targetNode, this, "Clone");
			}
			else
			{
				return null;
			}
		}
		
		protected internal virtual Configurable getParent(System.Windows.Forms.TreeNode targetNode)
		{
			System.Windows.Forms.TreeNode parentNode = (System.Windows.Forms.TreeNode) targetNode.Parent;
			return parentNode == null?null:(Configurable) parentNode.Tag;
		}
		
		protected internal virtual SupportClass.ActionSupport buildDeleteAction(Configurable target)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'targetNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.TreeNode targetNode = getTreeNode(target);
			//UPGRADE_NOTE: Final was removed from the declaration of 'parent '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Configurable parent = getParent(targetNode);
			if (targetNode.Parent != null)
			{
				return new AnonymousClassAbstractAction8(parent, target, this, deleteCmd);
			}
			else
			{
				return null;
			}
		}
		
		protected internal virtual SupportClass.ActionSupport buildEditPiecesAction(Configurable target)
		{
			if (canContainGamePiece(target))
			{
				return new EditContainedPiecesAction(target);
			}
			else
			{
				return null;
			}
		}
		
		protected internal virtual SupportClass.ActionSupport buildEditAction(Configurable target)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.getAncestorOfClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
			//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
			return new EditPropertiesAction(target, helpWindow, (System.Windows.Forms.Form) SwingUtilities.getAncestorOfClass(typeof(System.Windows.Forms.Form), this), this);
		}
		
		protected internal virtual SupportClass.ActionSupport buildTranslateAction(Configurable target)
		{
			SupportClass.ActionSupport a = new TranslateAction(target, helpWindow, this);
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			a.setEnabled(target.I18nData.Translatable);
			return a;
		}
		
		public virtual bool canContainGamePiece(Configurable target)
		{
			bool canContainPiece = false;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Class < ? > c: target.getAllowableConfigureComponents())
			{
				if (typeof(VassalSharp.build.widget.PieceSlot).isAssignableFrom(c))
				{
					canContainPiece = true;
					break;
				}
			}
			return canContainPiece;
		}
		
		protected internal virtual bool remove(Configurable parent, Configurable child)
		{
			try
			{
				child.removeFrom(parent);
				parent.remove(child);
				//UPGRADE_TODO: Method 'javax.swing.tree.DefaultTreeModel.removeNodeFromParent' was converted to 'System.Windows.Forms.TreeNode.Nodes.Remove' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtreeDefaultTreeModelremoveNodeFromParent_javaxswingtreeMutableTreeNode'"
				//UPGRADE_TODO: Method 'javax.swing.JTree.getModel' was converted to 'SupportClass.TreeSupport.GetModel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTreegetModel'"
				//UPGRADE_TODO: Class 'javax.swing.tree.DefaultTreeModel' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				((System.Windows.Forms.TreeNode) SupportClass.TreeSupport.GetModel(this)).Nodes.Remove(getTreeNode(child));
				notifyStateChanged(true);
				return true;
			}
			// FIXME: review error message
			catch (IllegalBuildException err)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				SupportClass.OptionPaneSupport.ShowMessageDialog(getTopLevelAncestor(), "Cannot delete " + getConfigureName(child) + " from " + getConfigureName(parent) + "\n" + err.Message, "Illegal configuration", (int) System.Windows.Forms.MessageBoxIcon.Error);
				return false;
			}
		}
		
		protected internal virtual bool insert(Configurable parent, Configurable child, int index)
		{
			Configurable theChild = child;
			// Convert subclasses of GlobalProperty to an actual GlobalProperty before inserting into the GlobalProperties container
			if (parent.GetType() == typeof(GlobalProperties) && child.GetType() == typeof(ZoneProperty))
			{
				theChild = new GlobalProperty((GlobalProperty) child);
			}
			if (parent.GetType() == typeof(Zone) && child.GetType() == typeof(GlobalProperty))
			{
				theChild = new ZoneProperty((GlobalProperty) child);
			}
			System.Windows.Forms.TreeNode childNode = buildTreeNode(theChild);
			System.Windows.Forms.TreeNode parentNode = getTreeNode(parent);
			Configurable[] oldContents = parent.ConfigureComponents;
			bool succeeded = true;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < Configurable > moveToBack = new ArrayList < Configurable >();
			for (int i = index; i < oldContents.Length; ++i)
			{
				try
				{
					oldContents[i].removeFrom(parent);
					parent.remove(oldContents[i]);
				}
				// FIXME: review error message
				catch (IllegalBuildException err)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
					SupportClass.OptionPaneSupport.ShowMessageDialog(getTopLevelAncestor(), "Can't insert " + getConfigureName(theChild) + " before " + getConfigureName(oldContents[i]), "Illegal configuration", (int) System.Windows.Forms.MessageBoxIcon.Error);
					for (int j = index; j < i; ++j)
					{
						parent.add(oldContents[j]);
						oldContents[j].addTo(parent);
					}
					return false;
				}
				moveToBack.add(oldContents[i]);
			}
			try
			{
				theChild.addTo(parent);
				parent.add(theChild);
				parentNode.Nodes.Insert(index, childNode);
				int[] childI = new int[1];
				childI[0] = index;
				//UPGRADE_ISSUE: Method 'javax.swing.tree.DefaultTreeModel.nodesWereInserted' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultTreeModelnodesWereInserted_javaxswingtreeTreeNode_int[]'"
				//UPGRADE_TODO: Method 'javax.swing.JTree.getModel' was converted to 'SupportClass.TreeSupport.GetModel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTreegetModel'"
				//UPGRADE_TODO: Class 'javax.swing.tree.DefaultTreeModel' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				((System.Windows.Forms.TreeNode) SupportClass.TreeSupport.GetModel(this)).nodesWereInserted(parentNode, childI);
			}
			// FIXME: review error message
			catch (IllegalBuildException err)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				SupportClass.OptionPaneSupport.ShowMessageDialog(getTopLevelAncestor(), "Can't add " + getConfigureName(child) + "\n" + err.Message, "Illegal configuration", (int) System.Windows.Forms.MessageBoxIcon.Error);
				succeeded = false;
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Configurable c: moveToBack)
			{
				parent.add(c);
				c.addTo(parent);
			}
			
			notifyStateChanged(true);
			return succeeded;
		}
		
		public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
		{
			System.Windows.Forms.TreeNode newValue = getTreeNode((Configurable) event_sender);
			//UPGRADE_ISSUE: Method 'javax.swing.tree.DefaultTreeModel.nodeChanged' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultTreeModelnodeChanged_javaxswingtreeTreeNode'"
			//UPGRADE_TODO: Method 'javax.swing.JTree.getModel' was converted to 'SupportClass.TreeSupport.GetModel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTreegetModel'"
			//UPGRADE_TODO: Class 'javax.swing.tree.DefaultTreeModel' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			((System.Windows.Forms.TreeNode) SupportClass.TreeSupport.GetModel(this)).nodeChanged(newValue);
		}
		
		//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
		//UPGRADE_ISSUE: Class 'javax.swing.tree.DefaultTreeCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultTreeCellRenderer'"
		[Serializable]
		protected internal class Renderer:javax.swing.tree.DefaultTreeCellRenderer
		{
			private const long serialVersionUID = 1L;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			public System.Windows.Forms.Control getTreeCellRendererComponent(System.Windows.Forms.TreeView tree, System.Object value_Renamed, bool sel, bool expanded, bool leaf, int row, bool hasFocus)
			{
				if (value_Renamed is System.Windows.Forms.TreeNode)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Configurable c = (Configurable) ((System.Windows.Forms.TreeNode) value_Renamed).Tag;
					if (c != null)
					{
						leaf = c.AllowableConfigureComponents.Length == 0;
						value_Renamed = (c.getConfigureName() != null?c.getConfigureName():"") + " [" + getConfigureName(c.GetType()) + "]";
					}
				}
				
				//UPGRADE_ISSUE: Method 'javax.swing.tree.DefaultTreeCellRenderer.getTreeCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultTreeCellRenderer'"
				return base.getTreeCellRendererComponent(tree, value_Renamed, sel, expanded, leaf, row, hasFocus);
			}
		}
		
		/// <summary> Returns the name of the class for display purposes. Reflection is
		/// used to call <code>getConfigureTypeName()</code>, which should be
		/// a static method if it exists in the given class. (This is necessary
		/// because static methods are not permitted in interfaces.)
		/// 
		/// </summary>
		/// <param name="the">class whose configure name will be returned
		/// </param>
		/// <returns> the configure name of the class
		/// </returns>
		public static System.String getConfigureName_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(Class < ? > c)
		
		public static System.String getConfigureName(Configurable c)
		{
			if (c.getConfigureName() != null && c.getConfigureName().Length > 0)
			{
				return c.getConfigureName();
			}
			else
			{
				return getConfigureName(c.GetType());
			}
		}
		
		protected internal virtual Configurable importConfigurable()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'className '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'javax.swing.JOptionPane.showInputDialog' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJOptionPane'"
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
			System.String className = JOptionPane.showInputDialog(getTopLevelAncestor(), "Enter fully-qualified name of Java class to import");
			
			if (className == null)
				return null;
			
			System.Object o = null;
			try
			{
				o = GameModule.getGameModule().getDataArchive().loadClass(className).getConstructor().newInstance();
			}
			//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			catch (System.Exception t)
			{
				ReflectionUtils.handleImportClassFailure(t, className);
			}
			
			if (o == null)
				return null;
			
			if (o is Configurable)
				return (Configurable) o;
			
			ErrorDialog.show("Error.not_a_configurable", className);
			return null;
		}
		
		public virtual void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
		}
		
		public virtual void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			Configurable target = getTarget(e.X, e.Y);
			if (target != null)
			{
				//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.isMetaDown' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
				if (e.Clicks == 2 && !e.isMetaDown())
				{
					if (target.Configurer != null)
					{
						SupportClass.ActionSupport a = buildEditAction(target);
						if (a != null)
						{
							//UPGRADE_TODO: Method 'java.awt.event.ActionListener.actionPerformed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
							System.Object generatedAux = event_sender;
							a.actionPerformed(new System.EventArgs());
						}
					}
				}
				else
				{
					//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.isMetaDown' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
					if (e.isMetaDown())
					{
						//UPGRADE_ISSUE: Method 'javax.swing.JTree.setSelectionRow' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreesetSelectionRow_int'"
						//UPGRADE_ISSUE: Method 'javax.swing.JTree.getClosestRowForLocation' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreegetClosestRowForLocation_int_int'"
						setSelectionRow(getClosestRowForLocation(e.X, e.Y));
						//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
						System.Windows.Forms.ContextMenu popup = buildPopupMenu(target);
						//UPGRADE_TODO: Method 'javax.swing.JPopupMenu.show' was converted to 'System.Windows.Forms.ContextMenu.Show' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPopupMenushow_javaawtComponent_int_int'"
						popup.Show(this, new System.Drawing.Point(e.X, e.Y));
						//UPGRADE_NOTE: Some methods of the 'javax.swing.event.PopupMenuListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
						popup.Popup += new System.EventHandler(new AnonymousClassPopupMenuListener(this).popupMenuWillBecomeVisible);
					}
				}
			}
		}
		
		/*
		* protected void performDrop(Configurable target) { DefaultMutableTreeNode dragNode = getTreeNode(dragging);
		* DefaultMutableTreeNode targetNode = getTreeNode(target); Configurable parent = null; int index = 0; if
		* (isValidParent(target, dragging)) { parent = target; index = targetNode.getChildCount(); if (dragNode.getParent() ==
		* targetNode) { index--; } } else if (targetNode.getParent() != null && isValidParent(getParent(targetNode),
		* dragging)) { parent = (Configurable) ((DefaultMutableTreeNode) targetNode.getParent()).getUserObject(); index =
		* targetNode.getParent().getIndex(targetNode); } if (parent != null) { remove(getParent(dragNode), dragging);
		* insert(parent, dragging, index); } dragging = null; }
		*/
		public virtual System.Windows.Forms.TreeNode getTreeNode(Configurable target)
		{
			return nodes.get_Renamed(target);
		}
		
		public virtual void  mouseDragged(System.Object event_sender, System.Windows.Forms.MouseEventArgs evt)
		{
		}
		
		
		protected internal virtual bool isValidParent(Configurable parent, Configurable child)
		{
			if (parent != null && child != null)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final Class < ? > c [] = parent.getAllowableConfigureComponents();
				for (int i = 0; i < c.length; ++i)
				{
					if (c[i].isAssignableFrom(child.GetType()) || ((c[i] == typeof(CardSlot)) && (child.GetType() == typeof(PieceSlot))) || ((c[i] == typeof(ZoneProperty)) && (child.GetType() == typeof(GlobalProperty))))
					{
						return true;
					}
				}
			}
			return false;
		}
		
		public virtual void  mouseClicked(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mouseEntered(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mouseExited(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mouseMoved(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
		}
		
		/*
		* Refresh the display of a node
		*/
		public virtual void  nodeUpdated(Configurable target)
		{
			System.Windows.Forms.TreeNode node = getTreeNode(target);
			Configurable parent = getParent(node);
			if (remove(parent, target))
			{
				insert(parent, target, 0);
			}
		}
		
		/// <summary> Configurers that add or remove their own children directly should implement the Mutable interface so that
		/// ConfigureTree can refresh the changed node.
		/// </summary>
		public interface Mutable
		{
		}
		
		/// <summary> Build an AddAction and execute it to request a new component from the user
		/// 
		/// </summary>
		/// <param name="parent">Target Parent
		/// </param>
		/// <param name="type">Type to add
		/// </param>
		public virtual void  externalInsert(Configurable parent, Configurable child)
		{
			insert(parent, child, getTreeNode(parent).Nodes.Count);
		}
		
		public virtual void  populateEditMenu(EditorWindow ew)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'mm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuManager mm = MenuManager.Instance;
			
			mm.addAction("Editor.delete", deleteAction);
			mm.addAction("Editor.cut", cutAction);
			mm.addAction("Editor.copy", copyAction);
			mm.addAction("Editor.paste", pasteAction);
			mm.addAction("Editor.move", moveAction);
			mm.addAction("Editor.ModuleEditor.properties", propertiesAction);
			mm.addAction("Editor.ModuleEditor.translate", translateAction);
			
			updateEditMenu();
		}
		
		/// <summary> Handle main Edit menu selections/accelerators
		/// 
		/// </summary>
		/// <param name="action">Edit command name
		/// </param>
		protected internal virtual void  doKeyAction(System.String action)
		{
			System.Windows.Forms.TreeNode targetNode = (System.Windows.Forms.TreeNode) this.SelectedNode;
			if (targetNode != null)
			{
				Configurable target = (Configurable) targetNode.Tag;
				SupportClass.ActionSupport a = null;
				if (cutCmd.Equals(action))
				{
					a = buildCutAction(target);
				}
				else if (copyCmd.Equals(action))
				{
					a = buildCopyAction(target);
				}
				else if (pasteCmd.Equals(action) || action.equals((char) pasteKey.KeyValue))
				{
					a = buildPasteAction(target);
				}
				else if (deleteCmd.Equals(action))
				{
					a = buildDeleteAction(target);
				}
				else if (moveCmd.Equals(action))
				{
					a = buildMoveAction(target);
				}
				else if (propertiesCmd.Equals(action))
				{
					a = buildEditAction(target);
				}
				else if (translateCmd.Equals(action))
				{
					a = buildTranslateAction(target);
				}
				else if (helpCmd.Equals(action))
				{
					a = buildHelpAction(target);
				}
				if (a != null)
				{
					//UPGRADE_TODO: Method 'java.awt.event.ActionListener.actionPerformed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					a.actionPerformed(null);
				}
			}
		}
		
		/// <summary> Tree selection changed, record info about the currently selected component</summary>
		public virtual void  valueChanged(System.Object event_sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			selected = null;
			//UPGRADE_ISSUE: Class 'javax.swing.tree.TreePath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
			//UPGRADE_ISSUE: Method 'javax.swing.event.TreeSelectionEvent.getPath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventTreeSelectionEventgetPath'"
			TreePath path = e.getPath();
			if (path != null)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.tree.TreePath.getLastPathComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
				selected = (Configurable) ((System.Windows.Forms.TreeNode) path.getLastPathComponent()).Tag;
				//UPGRADE_ISSUE: Method 'javax.swing.JTree.getRowForPath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreegetRowForPath_javaxswingtreeTreePath'"
				selectedRow = getRowForPath(path);
				updateEditMenu();
			}
		}
		
		protected internal virtual void  updateEditMenu()
		{
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			deleteAction.setEnabled(selected != null);
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			cutAction.setEnabled(selected != null);
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			copyAction.setEnabled(selected != null);
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			pasteAction.setEnabled(selected != null && isValidPasteTarget(selected));
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			moveAction.setEnabled(selected != null);
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			propertiesAction.setEnabled(selected != null && selected.Configurer != null);
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			translateAction.setEnabled(selected != null);
		}
		
		/// <summary> Find the parent Configurable of a specified Configurable
		/// 
		/// </summary>
		/// <param name="target">
		/// </param>
		/// <returns> parent
		/// </returns>
		protected internal virtual Configurable getParent(Configurable target)
		{
			System.Windows.Forms.TreeNode parentNode = (System.Windows.Forms.TreeNode) getTreeNode(target).Parent;
			return (Configurable) parentNode.Tag;
		}
		
		/// <summary> Record additional available components to add to the popup menu.
		/// 
		/// </summary>
		/// <param name="parent">
		/// </param>
		/// <param name="child">
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void addAdditionalComponent(Class < ? extends Buildable > parent, Class < ? extends Buildable > child)
		
		protected internal class AdditionalComponent
		{
			public AdditionalComponent()
			{
				InitBlock();
			}
			private void  InitBlock()
			{
				parent = p;
				child = c;
				return parent;
				return child;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Class < ? extends Buildable > parent;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Class < ? extends Buildable > child;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public AdditionalComponent(Class < ? extends Buildable > p, Class < ? extends Buildable > c)
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public Class < ? extends Buildable > getParent()
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public Class < ? extends Buildable > getChild()
		}
	}
}