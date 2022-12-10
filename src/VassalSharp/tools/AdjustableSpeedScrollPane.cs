/*
* $Id$
*
* Copyright (c) 2006 by Joel Uckelman
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
using GameModule = VassalSharp.build.GameModule;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using Resources = VassalSharp.i18n.Resources;
using Prefs = VassalSharp.preferences.Prefs;
namespace VassalSharp.tools
{
	
	/// <summary>  AdjustableSpeedScrollPane extends {@link ScrollPane} by making the
	/// scroll speed user-configurable. Use AdjustableScrollPane instead
	/// of ScrollPane wherever a scrollpane for large images is needed.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <seealso cref="VassalSharp.tools.ScrollPane">
	/// </seealso>
	/// <seealso cref="javax.swing.JScrollPane">
	/// </seealso>
	[Serializable]
	public class AdjustableSpeedScrollPane:ScrollPane
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(AdjustableSpeedScrollPane enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(AdjustableSpeedScrollPane enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private AdjustableSpeedScrollPane enclosingInstance;
			public AdjustableSpeedScrollPane Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
			{
				if (VassalSharp.tools.AdjustableSpeedScrollPane.SCROLL_SPEED.Equals(e.PropertyName))
					Enclosing_Instance.Speed = ((System.Int32) e.NewValue);
			}
		}
		private int Speed
		{
			set
			{
				//UPGRADE_WARNING: Method javax.swing.JScrollbar.setUnitIncrement was converted to 'System.Windows.Forms.RadioButton.SmallChange' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
				//UPGRADE_ISSUE: Field 'javax.swing.JScrollPane.verticalScrollBar' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPaneverticalScrollBar_f'"
				verticalScrollBar.SmallChange = value;
				//UPGRADE_WARNING: Method javax.swing.JScrollbar.setUnitIncrement was converted to 'System.Windows.Forms.RadioButton.SmallChange' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
				//UPGRADE_ISSUE: Field 'javax.swing.JScrollPane.horizontalScrollBar' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanehorizontalScrollBar_f'"
				horizontalScrollBar.SmallChange = value;
			}
			
		}
		private const long serialVersionUID = 1L;
		private const System.String SCROLL_SPEED = "scrollSpeed"; //$NON-NLS-1$
		private const int defaultSpeed = 50;
		
		/// <summary> Creates an AdjustableSpeedScrollPane that displays the contents of the
		/// specified component, where both horizontal and vertical scrollbars
		/// appear whenever the component's contents are larger than the view.
		/// 
		/// </summary>
		/// <param name="view">the component to display in the scrollpane's viewport
		/// </param>
		//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.VERTICAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsVERTICAL_SCROLLBAR_AS_NEEDED_f'"
		//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.HORIZONTAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsHORIZONTAL_SCROLLBAR_AS_NEEDED_f'"
		public AdjustableSpeedScrollPane(System.Windows.Forms.Control view):this(view, true, true)
		{
		}
		
		/// <summary> Creates an AdjustableSpeedScrollPane that displays the view component
		/// in a viewport with the specified scrollbar policies. The available
		/// policy settings are listed at
		/// {@link JScrollPane#setVerticalScrollBarPolicy} and
		/// {@link JScrollPane#setHorizontalScrollBarPolicy}.
		/// 
		/// </summary>
		/// <param name="view">the component to display in the scrollpane's viewport
		/// </param>
		/// <param name="vsbPolicy">an integer that specifies the vertical scrollbar policy
		/// </param>
		/// <param name="hsbPolicy">an integer that specifies the horizontal scrollbar
		/// policy
		/// </param>
		public AdjustableSpeedScrollPane(System.Windows.Forms.Control view, int vsbPolicy, int hsbPolicy):base(view, vsbPolicy, hsbPolicy)
		{
			
			// set configurer
			//UPGRADE_NOTE: Final was removed from the declaration of 'config '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			IntConfigurer config = new IntConfigurer(SCROLL_SPEED, Resources.getString("AdjustableSpeedScrollPane.scroll_increment"), defaultSpeed);
			
			config.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GameModule g = GameModule.getGameModule();
			if (g == null)
			{
				Speed = defaultSpeed;
			}
			else
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'prefs '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Prefs prefs = g.getPrefs();
				prefs.addOption(Resources.getString("Prefs.general_tab"), config); //$NON-NLS-1$
				Speed = ((System.Int32) prefs.getValue(SCROLL_SPEED));
			}
		}
	}
}