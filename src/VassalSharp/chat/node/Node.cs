/*
* $Id$
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
/*
* Copyright (c) 2003 by Rodney Kinney.  All rights reserved.
* Date: May 29, 2003
*/
using System;
//UPGRADE_TODO: The type 'java.util.logging.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = java.util.logging.Logger;
using PropertiesEncoder = VassalSharp.tools.PropertiesEncoder;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.chat.node
{
	
	/// <summary> Base class for the hierarchical server model.
	/// Each node has a name, a list of children, and arbitrary extra information
	/// encoded in the {@link #getInfo} string. Each node can be identified
	/// globally by a path name. Messages sent to a node generally broadcast to
	/// all descendents of the node.
	/// </summary>
	public class Node : MsgSender
	{
		private void  InitBlock()
		{
			if (base_Renamed.Leaf)
			{
				l.add(base_Renamed);
			}
			else
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Node n: base.getChildren())
				{
					addLeaves(n, l);
				}
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < Node > path = new ArrayList < Node >();
			for (Node n = this; n != null && n.Id != null; n = n.Parent)
			{
				path.add(n);
			}
			return path;
		}
		virtual public System.String Id
		{
			get
			{
				return id;
			}
			
		}
		virtual public Node Parent
		{
			get
			{
				return parent;
			}
			
			set
			{
				this.parent = value;
			}
			
		}
		virtual public Node[] LeafDescendants
		{
			get
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				ArrayList < Node > l = new ArrayList < Node >();
				addLeaves(this, l);
				return l.toArray(new Node[l.size()]);
			}
			
		}
		virtual public bool Leaf
		{
			get
			{
				return false;
			}
			
		}
		virtual public Node[] Children
		{
			get
			{
				lock (children)
				{
					return children.toArray(new Node[children.size()]);
				}
			}
			
		}
		virtual public System.String Path
		{
			get
			{
				lock (children)
				{
					SequenceEncoder se = new SequenceEncoder('/');
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
					return se.Value;
				}
			}
			
		}
		/// <summary> Return a string in the format parentId=parentInfo/childId=childInfo/...</summary>
		/// <returns>
		/// </returns>
		virtual public System.String PathAndInfo
		{
			get
			{
				lock (children)
				{
					SequenceEncoder se = new SequenceEncoder('/');
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
					return se.Value;
				}
			}
			
		}
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.chat.node.Node'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static Logger logger;
		private System.String id;
		private System.String info;
		private Node parent;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < Node > children = new ArrayList < Node >();
		
		public Node(Node parent, System.String id, System.String info)
		{
			InitBlock();
			this.parent = parent;
			this.id = id;
			this.info = info;
		}
		
		public virtual System.String getInfo()
		{
			return info;
		}
		
		public virtual System.String getInfoProperty(System.String propName)
		{
			try
			{
				return new PropertiesEncoder(info).Properties.Get(propName);
			}
			catch (System.IO.IOException e)
			{
				return null;
			}
		}
		
		public virtual void  setInfo(System.String info)
		{
			this.info = info;
		}
		
		public virtual void  remove(Node child)
		{
			logger.finer("Removing " + child + " from " + this); //$NON-NLS-1$ //$NON-NLS-2$
			children.remove(child);
		}
		
		public virtual void  add(Node child)
		{
			// FIXME: added this to find out what is calling add(null)
			if (child == null)
			{
				throw new System.NullReferenceException("child == null");
			}
			
			if (child.parent != null)
			{
				child.parent.remove(child);
			}
			logger.finer("Adding " + child + " to " + this); //$NON-NLS-1$ //$NON-NLS-2$
			children.add(child);
			child.Parent = this;
		}
		
		public  override bool Equals(System.Object o)
		{
			if (this == o)
				return true;
			if (!(o is Node))
				return false;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'node '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Node node = (Node) o;
			
			if (id != null?!id.Equals(node.id):node.id != null)
				return false;
			
			return true;
		}
		
		public override int GetHashCode()
		{
			return (id != null?id.GetHashCode():0);
		}
		
		public override System.String ToString()
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			return base.ToString() + "[id=" + id + "]"; //$NON-NLS-1$ //$NON-NLS-2$
		}
		
		/// <summary> return the child node with the given id, or null if no match</summary>
		/// <param name="id">
		/// </param>
		/// <returns>
		/// </returns>
		public virtual Node getChild(System.String id)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Node n: getChildren())
			{
				if (id.Equals(n.Id))
				{
					return n;
				}
			}
			return null;
		}
		
		/// <summary> Return the descendant node with the given path relative to this node</summary>
		/// <param name="path">
		/// </param>
		/// <returns>
		/// </returns>
		public virtual Node getDescendant(System.String path)
		{
			Node n = this;
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(path, '/');
			while (st.hasMoreTokens() && n != null)
			{
				System.String id = st.nextToken();
				n = n.getChild(id);
			}
			return n;
		}
		
		public virtual void  send(System.String msg)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Node n: getChildren())
			{
				n.send(msg);
			}
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void addLeaves(Node base, List < Node > l)
		
		/// <summary> Constructs from a path name.
		/// Instantiates parent nodes with appropriate names as necessary.
		/// </summary>
		/// <param name="base">the top-level ancestor of the node to be built.
		/// Its name is not included in the path name
		/// </param>
		/// <param name="path">
		/// </param>
		/// <returns>
		/// </returns>
		public static Node build(Node base_Renamed, System.String path)
		{
			Node node = null;
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(path, '/');
			while (st.hasMoreTokens())
			{
				System.String childId = st.nextToken();
				node = base_Renamed.getChild(childId);
				if (node == null)
				{
					node = new Node(base_Renamed, childId, null);
					base_Renamed.add(node);
				}
				base_Renamed = node;
			}
			return node;
		}
		
		/// <summary> Builds a Node from a pathAndInfo string</summary>
		/// <seealso cref="getPathAndInfo">
		/// </seealso>
		/// <param name="base">
		/// </param>
		/// <param name="pathAndInfo">
		/// </param>
		/// <returns>
		/// </returns>
		public virtual Node buildWithInfo(Node base_Renamed, System.String pathAndInfo)
		{
			Node node = null;
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(pathAndInfo, '/');
			while (st.hasMoreTokens())
			{
				SequenceEncoder.Decoder st2 = new SequenceEncoder.Decoder(st.nextToken(), '=');
				System.String childId = st2.nextToken();
				System.String childInfo = st2.nextToken();
				node = base_Renamed.getChild(childId);
				if (node == null)
				{
					node = new Node(base_Renamed, childId, null);
					base_Renamed.add(node);
				}
				node.setInfo(childInfo);
				base_Renamed = node;
			}
			return node;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < Node > getPathList()
		static Node()
		{
			logger = Logger.getLogger(typeof(MsgSender).FullName);
		}
	}
}