/*
* $Id$
*
* Copyright (c) 2003 by Rodney Kinney
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
using Info = VassalSharp.Info;
namespace VassalSharp.tools
{
	
	/// <summary> Provides support for hidden panels. Use the split methods to create an instance of {@link SplitPane}, which can then
	/// be manipulated to show and hide the panel
	/// </summary>
	public class ComponentSplitter
	{
		private class AnonymousClassWindowAdapter
		{
			public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs e)
			{
				e.Cancel = true;
				System.Environment.Exit(0);
			}
		}
		[Serializable]
		private class AnonymousClassAbstractAction:SupportClass.ActionSupport
		{
			private void  InitBlock(VassalSharp.tools.ComponentSplitter.SplitPane splitLeft)
			{
				this.splitLeft = splitLeft;
			}
			//UPGRADE_NOTE: Final variable splitLeft was copied into class AnonymousClassAbstractAction. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.tools.ComponentSplitter.SplitPane splitLeft;
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction(VassalSharp.tools.ComponentSplitter.SplitPane splitLeft, System.String Param1):base(Param1)
			{
				InitBlock(splitLeft);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				splitLeft.toggleVisibility();
			}
		}
		[Serializable]
		private class AnonymousClassAbstractAction1:SupportClass.ActionSupport
		{
			private void  InitBlock(VassalSharp.tools.ComponentSplitter.SplitPane splitRight)
			{
				this.splitRight = splitRight;
			}
			//UPGRADE_NOTE: Final variable splitRight was copied into class AnonymousClassAbstractAction1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.tools.ComponentSplitter.SplitPane splitRight;
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction1(VassalSharp.tools.ComponentSplitter.SplitPane splitRight, System.String Param1):base(Param1)
			{
				InitBlock(splitRight);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				splitRight.toggleVisibility();
			}
		}
		[Serializable]
		private class AnonymousClassAbstractAction2:SupportClass.ActionSupport
		{
			private void  InitBlock(VassalSharp.tools.ComponentSplitter.SplitPane splitBottom)
			{
				this.splitBottom = splitBottom;
			}
			//UPGRADE_NOTE: Final variable splitBottom was copied into class AnonymousClassAbstractAction2. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.tools.ComponentSplitter.SplitPane splitBottom;
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction2(VassalSharp.tools.ComponentSplitter.SplitPane splitBottom, System.String Param1):base(Param1)
			{
				InitBlock(splitBottom);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				splitBottom.toggleVisibility();
			}
		}
		/// <summary> Create a new hideable panel to the right of the base component. The base component is replaced by a
		/// {@link SplitPane}
		/// 
		/// </summary>
		/// <param name="base">the base component
		/// </param>
		/// <param name="hideableComponent">the hideable component
		/// </param>
		/// <param name="resizeOnVisibilityChange">If true, the containing window will expand or shrink to an appropriate size when the hideable component is
		/// shown or hidden
		/// </param>
		/// <returns> the {@link SplitPane} containing the two components
		/// </returns>
		public virtual SplitPane splitRight(System.Windows.Forms.Control base_Renamed, System.Windows.Forms.Control hideableComponent, bool resizeOnVisibilityChange)
		{
			return split(base_Renamed, hideableComponent, SplitPane.HIDE_RIGHT, resizeOnVisibilityChange);
		}
		
		/// <summary> Create a new hideable panel to the left of the base component. The base component is replaced by a
		/// {@link SplitPane}
		/// 
		/// </summary>
		/// <param name="base">the base component
		/// </param>
		/// <param name="hideableComponent">the hideable component
		/// </param>
		/// <param name="resizeOnVisibilityChange">If true, the containing window will expand or shrink to an appropriate size when the hideable component is
		/// shown or hidden
		/// </param>
		/// <returns> the {@link SplitPane} containing the two components
		/// </returns>
		public virtual SplitPane splitLeft(System.Windows.Forms.Control base_Renamed, System.Windows.Forms.Control hideableComponent, bool resizeOnVisibilityChange)
		{
			return split(base_Renamed, hideableComponent, SplitPane.HIDE_LEFT, resizeOnVisibilityChange);
		}
		
		/// <summary> Create a new hideable panel to the bottom of the base component. The base component is replaced by a
		/// {@link SplitPane}
		/// 
		/// </summary>
		/// <param name="base">the base component
		/// </param>
		/// <param name="hideableComponent">the hideable component
		/// </param>
		/// <param name="resizeOnVisibilityChange">If true, the containing window will expand or shrink to an appropriate size when the hideable component is
		/// shown or hidden
		/// </param>
		/// <returns> the {@link SplitPane} containing the two components
		/// </returns>
		public virtual SplitPane splitBottom(System.Windows.Forms.Control base_Renamed, System.Windows.Forms.Control hideableComponent, bool resizeOnVisibilityChange)
		{
			return split(base_Renamed, hideableComponent, SplitPane.HIDE_BOTTOM, resizeOnVisibilityChange);
		}
		
		/// <summary> Create a new hideable panel to the top of the base component. The base component is replaced by a {@link SplitPane}
		/// 
		/// </summary>
		/// <param name="base">the base component
		/// </param>
		/// <param name="hideableComponent">the hideable component
		/// </param>
		/// <param name="resizeOnVisibilityChange">If true, the containing window will expand or shrink to an appropriate size when the hideable component is
		/// shown or hidden
		/// </param>
		/// <returns> the {@link SplitPane} containing the two components
		/// </returns>
		public virtual SplitPane splitTop(System.Windows.Forms.Control base_Renamed, System.Windows.Forms.Control hideableComponent, bool resizeOnVisibilityChange)
		{
			return split(base_Renamed, hideableComponent, SplitPane.HIDE_TOP, resizeOnVisibilityChange);
		}
		
		/// <summary> Search the containment hierarchy for the index-th {@link SplitPane} ancestor of a target component
		/// 
		/// </summary>
		/// <param name="c">the target component
		/// </param>
		/// <param name="index">
		/// If -1, return the last {@link SplitPane} ancestor
		/// </param>
		/// <returns> the {@link SplitPane} ancestor, or the original component if none is found
		/// </returns>
		public virtual System.Windows.Forms.Control getSplitAncestor(System.Windows.Forms.Control c, int index)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.getAncestorOfClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
			System.Windows.Forms.Control next = SwingUtilities.getAncestorOfClass(typeof(SplitPane), c);
			int count = - 1;
			while (next != null && (index < 0 || count++ < index))
			{
				c = next;
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.getAncestorOfClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				next = SwingUtilities.getAncestorOfClass(typeof(SplitPane), c);
			}
			return c;
		}
		
		private SplitPane split(System.Windows.Forms.Control base_Renamed, System.Windows.Forms.Control newComponent, int hideablePosition, bool resize)
		{
			int index = - 1;
			System.Windows.Forms.Control parent = base_Renamed.Parent;
			if (base_Renamed.Parent != null)
			{
				for (int i = 0, n = (int) base_Renamed.Parent.Controls.Count; i < n; ++i)
				{
					if (base_Renamed == base_Renamed.Parent.Controls[i])
					{
						index = i;
						break;
					}
				}
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'split '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SplitPane split = new SplitPane(newComponent, base_Renamed, hideablePosition, resize);
			if (index >= 0)
			{
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_int'"
				parent.Controls.Add(split);
				if (index != -1)
					parent.Controls.SetChildIndex(split, index);
			}
			return split;
		}
		/// <summary> Contains methods to automatically show/hide one of its components (the "hideable" component) while the other (the
		/// "base" component) remains always visible. Can optionally change the size of its top level ancestorExtension when
		/// the component is shown/hidden. The hideable component is initially hidden
		/// </summary>
		//UPGRADE_TODO: Class 'javax.swing.JSplitPane' was converted to 'SupportClass.SplitterPanelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		[Serializable]
		public class SplitPane:SupportClass.SplitterPanelSupport
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassRunnable : IThreadRunnable
			{
				public AnonymousClassRunnable(double divPos, SplitPane enclosingInstance)
				{
					InitBlock(divPos, enclosingInstance);
				}
				private void  InitBlock(double divPos, SplitPane enclosingInstance)
				{
					this.divPos = divPos;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable divPos was copied into class AnonymousClassRunnable. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private double divPos;
				private SplitPane enclosingInstance;
				public SplitPane Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  Run()
				{
					Enclosing_Instance.SetLocationProportional(divPos);
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassRunnable1 : IThreadRunnable
			{
				public AnonymousClassRunnable1(SplitPane enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(SplitPane enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private SplitPane enclosingInstance;
				public SplitPane Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  Run()
				{
					Enclosing_Instance.SplitterLocation = Enclosing_Instance.PreferredDividerLocation;
				}
			}
			/// <returns> the Component that can be shown/hidden
			/// </returns>
			virtual public System.Windows.Forms.Control HideableComponent
			{
				get
				{
					System.Windows.Forms.Control c = null;
					switch (hideablePosition)
					{
						
						case HIDE_LEFT: 
							c = FirstControl;
							break;
						
						case HIDE_RIGHT: 
							c = SecondControl;
							break;
						
						case HIDE_TOP: 
							c = FirstControl;
							break;
						
						case HIDE_BOTTOM: 
							c = SecondControl;
							break;
						}
					return c;
				}
				
			}
			/// <returns> the Component that remains always visible
			/// </returns>
			virtual public System.Windows.Forms.Control BaseComponent
			{
				get
				{
					System.Windows.Forms.Control c = null;
					switch (hideablePosition)
					{
						
						case HIDE_LEFT: 
							c = SecondControl;
							break;
						
						case HIDE_RIGHT: 
							c = FirstControl;
							break;
						
						case HIDE_TOP: 
							c = SecondControl;
							break;
						
						case HIDE_BOTTOM: 
							c = FirstControl;
							break;
						}
					return c;
				}
				
			}
			/// <returns> the size of the base component along the axis of orientation
			/// </returns>
			virtual protected internal int BaseComponentSize
			{
				get
				{
					int size = - 1;
					switch (SplitterOrientation)
					{
						
						case (int) System.Windows.Forms.Orientation.Vertical: 
							size = BaseComponent.Size.Height;
							break;
						
						case (int) System.Windows.Forms.Orientation.Horizontal: 
							size = BaseComponent.Size.Width;
							break;
						}
					return size;
				}
				
			}
			/// <summary> </summary>
			/// <returns> the size of the hideable component along the axis of orientation
			/// </returns>
			virtual protected internal int HideableComponentSize
			{
				get
				{
					int size = - 1;
					switch (SplitterOrientation)
					{
						
						case (int) System.Windows.Forms.Orientation.Vertical: 
							size = HideableComponent.Size.Height;
							break;
						
						case (int) System.Windows.Forms.Orientation.Horizontal: 
							size = HideableComponent.Size.Width;
							break;
						}
					return size;
				}
				
			}
			/// <returns> the preferred size of the base component along the orientation axis
			/// </returns>
			virtual protected internal int PreferredBaseComponentSize
			{
				get
				{
					int size = transverseHiddenSize;
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(SplitPane split: showingTransverseComponents)
					{
						switch (SplitterOrientation)
						{
							
							case (int) System.Windows.Forms.Orientation.Vertical: 
								size = Math.max(size, split.HideableComponent.getPreferredSize().height);
								break;
							
							case (int) System.Windows.Forms.Orientation.Horizontal: 
								size = Math.max(size, split.HideableComponent.getPreferredSize().width);
								break;
							}
					}
					return size;
				}
				
			}
			/// <summary> Return the preferred size of the top-level container in the
			/// direction transverse to this SplitPane's orientation.
			/// Depends on which ancestors have been shown using
			/// {@link #showTransverseComponent}.
			/// </summary>
			virtual protected internal System.Drawing.Size TransverseSize
			{
				get
				{
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
					System.Drawing.Size newSize = getTopLevelAncestor().Size;
					switch (SplitterOrientation)
					{
						
						case (int) System.Windows.Forms.Orientation.Vertical: 
							newSize.Height += PreferredBaseComponentSize - BaseComponentSize;
							break;
						
						case (int) System.Windows.Forms.Orientation.Horizontal: 
							newSize.Width += PreferredBaseComponentSize - BaseComponentSize;
							break;
						}
					return newSize;
				}
				
			}
			/// <returns> the preferred location of the divider when the hideable component is visible
			/// </returns>
			virtual protected internal int PreferredDividerLocation
			{
				get
				{
					int loc = 0;
					switch (hideablePosition)
					{
						
						case HIDE_LEFT: 
							//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Component.getPreferredSize' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
							loc = SupportClass.GetInsets(this)[1] + FirstControl.Size.Width;
							break;
						
						case HIDE_RIGHT: 
							//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Component.getPreferredSize' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
							loc = Size.Width - SupportClass.GetInsets(this)[2] - SplitterSize - SecondControl.Size.Width;
							break;
						
						case HIDE_TOP: 
							//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Component.getPreferredSize' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
							loc = SupportClass.GetInsets(this)[0] + FirstControl.Size.Height;
							break;
						
						case HIDE_BOTTOM: 
							//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Component.getPreferredSize' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
							loc = Size.Height - SupportClass.GetInsets(this)[3] - SplitterSize - SecondControl.Size.Height;
							break;
						}
					return loc;
				}
				
			}
			/// <summary> Return the first SplitPane ancestor with a different orientation from this SplitPane
			/// 
			/// </summary>
			/// <returns>
			/// </returns>
			virtual public SplitPane TransverseSplit
			{
				get
				{
					SplitPane split = null;
					for (System.Windows.Forms.Control c = Parent; c != null; c = c.Parent)
					{
						if (c is SplitPane)
						{
							SplitPane p = (SplitPane) c;
							//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.isDescendingFrom' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
							if (p.SplitterOrientation != SplitterOrientation && SwingUtilities.isDescendingFrom(this, p.BaseComponent))
							{
								split = p;
								break;
							}
						}
					}
					return split;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			private bool resizeOnVisibilityChange;
			private int hideablePosition;
			public const int HIDE_TOP = 0;
			public const int HIDE_BOTTOM = 1;
			public const int HIDE_LEFT = 2;
			public const int HIDE_RIGHT = 3;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private List < SplitPane > showingTransverseComponents = 
			new ArrayList < SplitPane >();
			private int transverseHiddenSize;
			
			/// <summary> Initialize the SplitPane with the two component
			/// 
			/// </summary>
			/// <param name="hideableComponent">
			/// </param>
			/// <param name="baseComponent">
			/// </param>
			/// <param name="hideablePosition">
			/// one of {@link #HIDE_TOP}, {@link #HIDE_BOTTOM}, {@link #HIDE_LEFT} or {@link #HIDE_RIGHT}
			/// </param>
			/// <param name="resizeOnVisibilityChange">If true, resize the top-level ancestor when the hideable component is shown/hidden
			/// </param>
			public SplitPane(System.Windows.Forms.Control hideableComponent, System.Windows.Forms.Control baseComponent, int hideablePosition, bool resizeOnVisibilityChange):base(HIDE_TOP == hideablePosition || HIDE_BOTTOM == hideablePosition?(int) System.Windows.Forms.Orientation.Vertical:(int) System.Windows.Forms.Orientation.Horizontal)
			{
				this.resizeOnVisibilityChange = resizeOnVisibilityChange;
				this.hideablePosition = hideablePosition;
				if (hideableComponent is System.Windows.Forms.Control)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMinimumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMinimumSize_javaawtDimension'"
					((System.Windows.Forms.Control) hideableComponent).setMinimumSize(new System.Drawing.Size(0, 0));
				}
				switch (hideablePosition)
				{
					
					case HIDE_LEFT: 
						FirstControl = hideableComponent;
						SecondControl = baseComponent;
						break;
					
					case HIDE_RIGHT: 
						SecondControl = hideableComponent;
						FirstControl = baseComponent;
						break;
					
					case HIDE_TOP: 
						FirstControl = hideableComponent;
						SecondControl = baseComponent;
						break;
					
					case HIDE_BOTTOM: 
						SecondControl = hideableComponent;
						FirstControl = baseComponent;
						break;
					}
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
				System.Windows.Forms.ControlPaint.DrawBorder3D(CreateGraphics(), 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.Flat);
				//UPGRADE_ISSUE: Method 'javax.swing.JSplitPane.setResizeWeight' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJSplitPanesetResizeWeight_double'"
				setResizeWeight(HIDE_LEFT == hideablePosition || HIDE_TOP == hideablePosition?0.0:1.0);
				hideComponent();
			}
			
			/// <summary>Toggle the visibility of the hideable component </summary>
			public virtual void  toggleVisibility()
			{
				//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
				if (HideableComponent.Visible)
				{
					hideComponent();
				}
				else
				{
					showComponent();
				}
			}
			
			/// <summary>Hide the hideable component </summary>
			public virtual void  hideComponent()
			{
				//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
				if (HideableComponent.Visible)
				{
					if (resizeOnVisibilityChange)
					{
						//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
						System.Windows.Forms.Control ancestor = getTopLevelAncestor();
						if (ancestor != null)
						{
							switch (hideablePosition)
							{
								
								case HIDE_LEFT: 
								case HIDE_RIGHT: 
									//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_javaawtDimension'"
									ancestor.Size = new System.Drawing.Size(ancestor.Size.Width - HideableComponent.Size.Width, ancestor.Size.Height - SplitterSize);
									break;
								
								case HIDE_TOP: 
								case HIDE_BOTTOM: 
									//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_javaawtDimension'"
									ancestor.Size = new System.Drawing.Size(ancestor.Size.Width, ancestor.Size.Height - HideableComponent.Size.Height - SplitterSize);
									break;
								}
							//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
							SupportClass.InvokeMethodAsVirtual(ancestor, "Invalidate", new System.Object[]{});
						}
					}
					// Running later causes race conditions in the Module Manager
					//Runnable runnable = new Runnable() {
					//  public void run() {
					//UPGRADE_TODO: Method 'javax.swing.plaf.basic.BasicSplitPaneUI.getDivider' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					//UPGRADE_ISSUE: Method 'javax.swing.JSplitPane.getUI' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJSplitPanegetUI'"
					//UPGRADE_TODO: Class 'javax.swing.plaf.basic.BasicSplitPaneUI' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					((BasicSplitPaneUI) getUI()).getDivider().Visible = false;
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(HideableComponent, "Visible", false);
					switch (hideablePosition)
					{
						
						case HIDE_LEFT: 
						case HIDE_TOP: 
							SetLocationProportional(0.0);
							break;
						
						case HIDE_RIGHT: 
						case HIDE_BOTTOM: 
							SetLocationProportional(1.0);
							break;
						}
					//  }
					//};
					//SwingUtilities.invokeLater(runnable);
					SplitPane split = TransverseSplit;
					if (split != null)
					{
						split.hideTransverseComponent(this);
					}
				}
			}
			
			/// <summary> Set the divider location and/or the top-level ancestor size to be large enough to display the argument
			/// {@link SplitPane}'s hideable component
			/// 
			/// </summary>
			/// <param name="split">
			/// </param>
			protected internal virtual void  showTransverseComponent(SplitPane split)
			{
				if (showingTransverseComponents.isEmpty())
				{
					transverseHiddenSize = BaseComponentSize;
				}
				showingTransverseComponents.add(split);
				resizeBaseComponent();
			}
			
			/// <summary> Set the base component size to be large enough to accomodate all descendant SplitPane's showing components</summary>
			protected internal virtual void  resizeBaseComponent()
			{
				//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
				if (HideableComponent.Visible)
				{
					switch (hideablePosition)
					{
						
						case HIDE_BOTTOM: 
						case HIDE_RIGHT: 
							SplitterLocation = PreferredBaseComponentSize;
							break;
						
						case HIDE_TOP: 
							SplitterLocation = Size.Height - PreferredBaseComponentSize;
							break;
						
						case HIDE_LEFT: 
							SplitterLocation = Size.Width - PreferredBaseComponentSize;
							break;
						}
				}
				else
				{
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
					if (resizeOnVisibilityChange && getTopLevelAncestor() != null)
					{
						//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_javaawtDimension'"
						//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
						getTopLevelAncestor().Size = TransverseSize;
						//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
						//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
						SupportClass.InvokeMethodAsVirtual(getTopLevelAncestor(), "Invalidate", new System.Object[]{});
					}
				}
			}
			
			/// <summary> Set the divider location and/or the top-level ancestor size to
			/// the preferred transverse size.
			/// 
			/// </summary>
			/// <param name="split">
			/// </param>
			protected internal virtual void  hideTransverseComponent(SplitPane split)
			{
				showingTransverseComponents.remove(split);
				resizeBaseComponent();
			}
			
			/// <summary> Show the hideable component</summary>
			public virtual void  showComponent()
			{
				//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
				if (HideableComponent.Visible)
				{
					return ;
				}
				
				if (resizeOnVisibilityChange)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'ancestor '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
					System.Windows.Forms.Control ancestor = getTopLevelAncestor();
					if (ancestor == null)
					{
						return ;
					}
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'screenBounds '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Rectangle screenBounds = Info.getScreenBounds(ancestor);
					//UPGRADE_NOTE: Final was removed from the declaration of 'ancestorPos '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Point ancestorPos = ancestor.Location;
					//UPGRADE_NOTE: Final was removed from the declaration of 'ancestorSize '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Size ancestorSize = ancestor.Size;
					//UPGRADE_NOTE: Final was removed from the declaration of 'prefHSize '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Component.getPreferredSize' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.Drawing.Size prefHSize = HideableComponent.Size;
					//UPGRADE_NOTE: Final was removed from the declaration of 'prefBSize '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Component.getPreferredSize' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.Drawing.Size prefBSize = BaseComponent.Size;
					
					double div = 0.0;
					int w = 0, h = 0;
					switch (SplitterOrientation)
					{
						
						case (int) System.Windows.Forms.Orientation.Horizontal: 
							w = System.Math.Min(ancestorSize.Width + prefHSize.Width, screenBounds.Width - ancestorPos.X);
							h = ancestorSize.Height;
							div = prefBSize.Width / (double) (prefBSize.Width + prefHSize.Width);
							break;
						
						case (int) System.Windows.Forms.Orientation.Vertical: 
							w = ancestorSize.Width;
							h = System.Math.Min(ancestorSize.Height + prefHSize.Height, screenBounds.Height - ancestorPos.Y);
							div = prefBSize.Height / (double) (prefBSize.Height + prefHSize.Height);
							break;
						}
					
					//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
					ancestor.Size = new System.Drawing.Size(w, h);
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.InvokeMethodAsVirtual(ancestor, "Invalidate", new System.Object[]{});
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(HideableComponent, "Visible", true);
					//UPGRADE_TODO: Method 'javax.swing.plaf.basic.BasicSplitPaneUI.getDivider' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					//UPGRADE_ISSUE: Method 'javax.swing.JSplitPane.getUI' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJSplitPanegetUI'"
					//UPGRADE_TODO: Class 'javax.swing.plaf.basic.BasicSplitPaneUI' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					((BasicSplitPaneUI) getUI()).getDivider().Visible = true;
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'divPos '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					double divPos = div;
					//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
					SwingUtilities.invokeLater(new AnonymousClassRunnable(divPos, this));
				}
				else
				{
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(HideableComponent, "Visible", true);
					//UPGRADE_TODO: Method 'javax.swing.plaf.basic.BasicSplitPaneUI.getDivider' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					//UPGRADE_ISSUE: Method 'javax.swing.JSplitPane.getUI' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJSplitPanegetUI'"
					//UPGRADE_TODO: Class 'javax.swing.plaf.basic.BasicSplitPaneUI' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					((BasicSplitPaneUI) getUI()).getDivider().Visible = true;
					
					//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
					SwingUtilities.invokeLater(new AnonymousClassRunnable1(this));
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'split '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					SplitPane split = TransverseSplit;
					if (split != null)
					{
						split.showTransverseComponent(this);
					}
				}
			}
			
			/// <summary> If the hideable component is not visible, use the base component's preferred size</summary>
			//UPGRADE_NOTE: The equivalent of method 'javax.swing.JComponent.getPreferredSize' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public System.Drawing.Size getPreferredSize()
			{
				System.Drawing.Size d = System.Drawing.Size.Empty;
				//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
				if (HideableComponent == null || HideableComponent.Visible)
				{
					//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					d = base.Size;
				}
				else
				{
					switch (hideablePosition)
					{
						
						case HIDE_LEFT: 
							//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Component.getPreferredSize' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
							d = SecondControl.Size;
							break;
						
						case HIDE_RIGHT: 
							//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Component.getPreferredSize' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
							d = FirstControl.Size;
							break;
						
						case HIDE_TOP: 
							//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Component.getPreferredSize' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
							d = SecondControl.Size;
							break;
						
						case HIDE_BOTTOM: 
							//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Component.getPreferredSize' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
							d = FirstControl.Size;
							break;
						}
				}
				return d;
			}
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			System.Windows.Forms.Form f = new System.Windows.Forms.Form();
			f.Closing += new System.ComponentModel.CancelEventHandler(ComponentSplitter.ComponentSplitter_Closing_DO_NOTHING_ON_CLOSE);
			//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			f.Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter().windowClosing);
			System.Windows.Forms.TextBox temp_text_box;
			temp_text_box = new System.Windows.Forms.TextBox();
			temp_text_box.Text = "status";
			System.Windows.Forms.TextBox status = temp_text_box;
			status.ReadOnly = !false;
			//UPGRADE_ISSUE: Method 'javax.swing.JFrame.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJFramesetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			/*
			f.setLayout(new BorderLayout());*/
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box box = Box.createVerticalBox();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			box.Controls.Add(status);
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			new BorderLayout();
			//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
			System.Windows.Forms.Panel main = new System.Windows.Forms.Panel();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			f.Controls.Add(main);
			main.Dock = System.Windows.Forms.DockStyle.Fill;
			main.BringToFront();
			System.Windows.Forms.ToolBar temp_ToolBar;
			System.Windows.Forms.ImageList temp_ImageList;
			temp_ImageList = new System.Windows.Forms.ImageList();
			temp_ToolBar = new System.Windows.Forms.ToolBar();
			temp_ToolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(SupportClass.ToolBarButtonClicked);
			temp_ToolBar.ImageList = temp_ImageList;
			System.Windows.Forms.ToolBar toolbar = temp_ToolBar;
			//UPGRADE_ISSUE: Method 'javax.swing.JToolBar.setFloatable' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJToolBarsetFloatable_boolean'"
			toolbar.setFloatable(false);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentX' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentX_float'"
			toolbar.setAlignmentX(0.0F);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			box.Controls.Add(toolbar);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			f.Controls.Add(box);
			box.Dock = System.Windows.Forms.DockStyle.Top;
			box.SendToBack();
			//UPGRADE_NOTE: Final was removed from the declaration of 'smallLeft '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label temp_label;
			//UPGRADE_TODO: Constructor 'javax.swing.ImageIcon.ImageIcon' was converted to 'System.Drawing.Image' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingImageIconImageIcon_javalangString'"
			temp_label = new System.Windows.Forms.Label();
			temp_label.Image = System.Drawing.Image.FromFile("small.gif");
			System.Windows.Forms.Label smallLeft = temp_label;
			//UPGRADE_NOTE: Final was removed from the declaration of 'smallRight '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label temp_label2;
			//UPGRADE_TODO: Constructor 'javax.swing.ImageIcon.ImageIcon' was converted to 'System.Drawing.Image' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingImageIconImageIcon_javalangString'"
			temp_label2 = new System.Windows.Forms.Label();
			temp_label2.Image = System.Drawing.Image.FromFile("smallRight.gif");
			System.Windows.Forms.Label smallRight = temp_label2;
			//UPGRADE_NOTE: Final was removed from the declaration of 'large '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label temp_label3;
			//UPGRADE_TODO: Constructor 'javax.swing.ImageIcon.ImageIcon' was converted to 'System.Drawing.Image' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingImageIconImageIcon_javalangString'"
			temp_label3 = new System.Windows.Forms.Label();
			temp_label3.Image = System.Drawing.Image.FromFile("large.jpg");
			System.Windows.Forms.Label large = temp_label3;
			System.Windows.Forms.Panel text = new System.Windows.Forms.Panel();
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			text.setLayout(new BoxLayout(text, BoxLayout.Y_AXIS));
			System.Windows.Forms.TextBox temp_TextBox2;
			temp_TextBox2 = new System.Windows.Forms.TextBox();
			temp_TextBox2.Multiline = true;
			temp_TextBox2.WordWrap = false;
			temp_TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = new ScrollPane(temp_TextBox2);
			text.Controls.Add(temp_Control);
			//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
			System.Windows.Forms.TextBox input = new System.Windows.Forms.TextBox();
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetMaximumSize'"
			input.setMaximumSize(new System.Drawing.Size(input.getMaximumSize().Width, input.Size.Height));
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			text.Controls.Add(input);
			ComponentSplitter splitter = new ComponentSplitter();
			//UPGRADE_NOTE: Final was removed from the declaration of 'splitRight '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SplitPane splitRight = splitter.splitRight(main, smallRight, false);
			//UPGRADE_NOTE: Final was removed from the declaration of 'splitLeft '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SplitPane splitLeft = splitter.splitLeft(main, smallLeft, false);
			//UPGRADE_NOTE: Final was removed from the declaration of 'splitBottom '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SplitPane splitBottom = splitter.splitBottom(splitter.getSplitAncestor(main, - 1), new ScrollPane(large), true);
			//UPGRADE_ISSUE: Method 'javax.swing.JSplitPane.setResizeWeight' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJSplitPanesetResizeWeight_double'"
			splitBottom.setResizeWeight(0.0);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			main.Controls.Add(text);
			text.Dock = System.Windows.Forms.DockStyle.Fill;
			text.BringToFront();
			System.Windows.Forms.ToolBarButton temp_ToolBarButton;
			AnonymousClassAbstractAction temp_Action;
			temp_Action = new AnonymousClassAbstractAction(splitLeft, "Left");
			System.Windows.Forms.Button temp_Button;
			temp_Button = SupportClass.ButtonSupport.CreateStandardButton(temp_Action.Description, temp_Action.Icon);
			temp_Button.Click += new System.EventHandler(temp_Action.actionPerformed);
			temp_ToolBarButton = new System.Windows.Forms.ToolBarButton(temp_Button.Text);
			toolbar.Buttons.Add(temp_ToolBarButton);
			if (temp_Button.Image != null)
			{
				toolbar.ImageList.Images.Add(temp_Button.Image);
				temp_ToolBarButton.ImageIndex = toolbar.ImageList.Images.Count - 1;
			}
			temp_ToolBarButton.Tag = temp_Button;
			temp_Button.Tag = temp_ToolBarButton;
			System.Windows.Forms.ToolBarButton temp_ToolBarButton2;
			AnonymousClassAbstractAction1 temp_Action2;
			temp_Action2 = new AnonymousClassAbstractAction1(splitRight, "Right");
			System.Windows.Forms.Button temp_Button2;
			temp_Button2 = SupportClass.ButtonSupport.CreateStandardButton(temp_Action2.Description, temp_Action2.Icon);
			temp_Button2.Click += new System.EventHandler(temp_Action2.actionPerformed);
			temp_ToolBarButton2 = new System.Windows.Forms.ToolBarButton(temp_Button2.Text);
			toolbar.Buttons.Add(temp_ToolBarButton2);
			if (temp_Button2.Image != null)
			{
				toolbar.ImageList.Images.Add(temp_Button2.Image);
				temp_ToolBarButton2.ImageIndex = toolbar.ImageList.Images.Count - 1;
			}
			temp_ToolBarButton2.Tag = temp_Button2;
			temp_Button2.Tag = temp_ToolBarButton2;
			System.Windows.Forms.ToolBarButton temp_ToolBarButton3;
			AnonymousClassAbstractAction2 temp_Action3;
			temp_Action3 = new AnonymousClassAbstractAction2(splitBottom, "Bottom");
			System.Windows.Forms.Button temp_Button3;
			temp_Button3 = SupportClass.ButtonSupport.CreateStandardButton(temp_Action3.Description, temp_Action3.Icon);
			temp_Button3.Click += new System.EventHandler(temp_Action3.actionPerformed);
			temp_ToolBarButton3 = new System.Windows.Forms.ToolBarButton(temp_Button3.Text);
			toolbar.Buttons.Add(temp_ToolBarButton3);
			if (temp_Button3.Image != null)
			{
				toolbar.ImageList.Images.Add(temp_Button3.Image);
				temp_ToolBarButton3.ImageIndex = toolbar.ImageList.Images.Count - 1;
			}
			temp_ToolBarButton3.Tag = temp_Button3;
			temp_Button3.Tag = temp_ToolBarButton3;
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			f.pack();
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			f.Visible = true;
		}
		private static void  ComponentSplitter_Closing_DO_NOTHING_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 0);
		}
	}
}