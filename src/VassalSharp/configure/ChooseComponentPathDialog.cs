/*
* $Id$
*
* Copyright (c) 2005-2011 by Rodney Kinney, Brent Easton
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
using Configurable = VassalSharp.build.Configurable;
using ArrayUtils = VassalSharp.tools.ArrayUtils;
namespace VassalSharp.configure
{
	
	/// <summary> Widget for selecting the full path of a Component in the Buildable hierarchy</summary>
	[Serializable]
	public class ChooseComponentPathDialog:ChooseComponentDialog
	{
		public ChooseComponentPathDialog()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			base(owner, targetClass);
		}
		virtual public Configurable[] Path
		{
			get
			{
				return path;
			}
			
		}
		private const long serialVersionUID = 1L;
		
		private Configurable[] path;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public ChooseComponentPathDialog(Frame owner, 
		Class < ? extends Buildable > targetClass)
		
		public override void  valueChanged(System.Object event_sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			base.valueChanged(e);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'javax.swing.tree.TreePath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
			//UPGRADE_ISSUE: Method 'javax.swing.event.TreeSelectionEvent.getPath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventTreeSelectionEventgetPath'"
			TreePath p = e.getPath();
			if (p != null)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'node '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Method 'javax.swing.tree.TreePath.getLastPathComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
				System.Windows.Forms.TreeNode node = (System.Windows.Forms.TreeNode) p.getLastPathComponent();
				
				//UPGRADE_ISSUE: Method 'javax.swing.tree.DefaultMutableTreeNode.getUserObjectPath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultMutableTreeNodegetUserObjectPath'"
				System.Object[] x = node.getUserObjectPath();
				Configurable[] userObjectPath = new Configurable[x.Length];
				
				for (int i = 0; i < x.Length; i++)
				{
					userObjectPath[i] = (Configurable) x[i];
				}
				
				path = ArrayUtils.copyOfRange(userObjectPath, 1, userObjectPath.Length);
			}
			else
			{
				path = null;
			}
		}
	}
}