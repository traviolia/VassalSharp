/*
*
* Copyright (c) 2000-2007 by Rodney Kinney
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
using Player = VassalSharp.chat.Player;
using Room = VassalSharp.chat.Room;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.chat.ui
{
	
	/// <summary> JTree component displaying chat rooms on the server</summary>
	/// <author>  rkinney
	/// 
	/// </author>
	//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
	[Serializable]
	public class RoomTree:System.Windows.Forms.TreeView
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDefaultTreeModel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_TODO: Class 'javax.swing.tree.DefaultTreeModel' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		[Serializable]
		private class AnonymousClassDefaultTreeModel:System.Windows.Forms.TreeNode
		{
			private void  InitBlock(RoomTree enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private RoomTree enclosingInstance;
			public RoomTree Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.tree.DefaultTreeModel.DefaultTreeModel' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtreeDefaultTreeModelDefaultTreeModel_javaxswingtreeTreeNode'"
			internal AnonymousClassDefaultTreeModel(RoomTree enclosingInstance, System.Windows.Forms.TreeNode Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			//UPGRADE_NOTE: The equivalent of method 'javax.swing.tree.DefaultTreeModel.isLeaf' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public bool isLeaf(System.Object node)
			{
				return ((System.Windows.Forms.TreeNode) node).Tag is Player;
			}
		}
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'setRooms'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		virtual public Room[] Rooms
		{
			set
			{
				lock (this)
				{
					if (value == null)
					{
						value = new Room[0];
					}
					
					// Remove rooms no longer present
					for (int i = 0; i < root.Nodes.Count; ++i)
					{
						Room r = roomAt(i);
						int j = 0;
						for (j = 0; j < value.Length; ++j)
						{
							if (value[j] == r)
							{
								break;
							}
							else if (value[j].Equals(r))
							{
								//UPGRADE_ISSUE: Method 'javax.swing.tree.DefaultTreeModel.valueForPathChanged' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultTreeModelvalueForPathChanged_javaxswingtreeTreePath_javalangObject'"
								//UPGRADE_ISSUE: Constructor 'javax.swing.tree.TreePath.TreePath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
								//UPGRADE_TODO: Method 'javax.swing.tree.DefaultMutableTreeNode.getPath' was converted to 'System.Windows.Forms.TreeNode.FullPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtreeDefaultMutableTreeNodegetPath'"
								model.valueForPathChanged(new TreePath((System.String) ((System.Windows.Forms.TreeNode) root.Nodes[i]).FullPath), value[j]);
								break;
							}
						}
						if (j >= value.Length)
						{
							// No match
							//UPGRADE_TODO: Method 'javax.swing.tree.DefaultTreeModel.removeNodeFromParent' was converted to 'System.Windows.Forms.TreeNode.Nodes.Remove' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtreeDefaultTreeModelremoveNodeFromParent_javaxswingtreeMutableTreeNode'"
							model.Nodes.Remove((System.Windows.Forms.TreeNode) root.Nodes[i--]);
						}
					}
					// Add new rooms
					for (int i = 0; i < value.Length; ++i)
					{
						int j;
						for (j = 0; j < root.Nodes.Count; ++j)
						{
							if (roomAt(j) == value[i])
							{
								break;
							}
						}
						if (j >= root.Nodes.Count)
						{
							root.Nodes.Insert(i, new System.Windows.Forms.TreeNode(value[i].ToString()));
						}
					}
					// Update players
					for (int i = 0; i < root.Nodes.Count; ++i)
					{
						System.Windows.Forms.TreeNode node = (System.Windows.Forms.TreeNode) root.Nodes[i];
						//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
						while (p.size() < node.Nodes.Count)
						{
							//UPGRADE_TODO: Method 'javax.swing.tree.DefaultTreeModel.removeNodeFromParent' was converted to 'System.Windows.Forms.TreeNode.Nodes.Remove' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtreeDefaultTreeModelremoveNodeFromParent_javaxswingtreeMutableTreeNode'"
							model.Nodes.Remove((System.Windows.Forms.TreeNode) node.Nodes[0]);
						}
						while (p.size() > node.Nodes.Count)
						{
							node.Nodes.Insert(0, new System.Windows.Forms.TreeNode(value[i].ToString()));
						}
						for (int j = 0; j < node.Nodes.Count; ++j)
						{
							//UPGRADE_ISSUE: Method 'javax.swing.tree.DefaultTreeModel.valueForPathChanged' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultTreeModelvalueForPathChanged_javaxswingtreeTreePath_javalangObject'"
							//UPGRADE_ISSUE: Constructor 'javax.swing.tree.TreePath.TreePath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
							//UPGRADE_TODO: Method 'javax.swing.tree.DefaultMutableTreeNode.getPath' was converted to 'System.Windows.Forms.TreeNode.FullPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtreeDefaultMutableTreeNodegetPath'"
							model.valueForPathChanged(new TreePath((System.String) ((System.Windows.Forms.TreeNode) node.Nodes[j]).FullPath), p.get_Renamed(j));
						}
					}
				}
			}
			
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_TODO: Class 'javax.swing.tree.DefaultTreeModel' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		protected internal System.Windows.Forms.TreeNode model;
		protected internal System.Windows.Forms.TreeNode root;
		
		public RoomTree()
		{
			//UPGRADE_ISSUE: Method 'javax.swing.JTree.setRootVisible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreesetRootVisible_boolean'"
			setRootVisible(false);
			ShowRootLines = true;
			root = new System.Windows.Forms.TreeNode(Resources.getString("Chat.server").ToString()); //$NON-NLS-1$
			model = new AnonymousClassDefaultTreeModel(this, root);
			SupportClass.TreeSupport.SetModel(this, model);
			//UPGRADE_ISSUE: Method 'javax.swing.JTree.setCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreesetCellRenderer_javaxswingtreeTreeCellRenderer'"
			setCellRenderer(new RoomTreeRenderer());
		}
		
		protected internal virtual Room roomAt(int index)
		{
			return (Room) ((System.Windows.Forms.TreeNode) root.Nodes[index]).Tag;
		}
	}
}