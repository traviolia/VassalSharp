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
using Configurable = VassalSharp.build.Configurable;
using MassPieceDefiner = VassalSharp.counters.MassPieceDefiner;
namespace VassalSharp.configure
{
	
	/// <summary> Action to edit all {@link VassalSharp.counters.GamePiece}'s within a given component</summary>
	[Serializable]
	public class EditContainedPiecesAction:SupportClass.ActionSupport
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassConfigurer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassConfigurer:Configurer
		{
			private void  InitBlock(VassalSharp.counters.MassPieceDefiner mass, EditContainedPiecesAction enclosingInstance)
			{
				this.mass = mass;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable mass was copied into class AnonymousClassConfigurer. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.counters.MassPieceDefiner mass;
			private EditContainedPiecesAction enclosingInstance;
			override public System.Windows.Forms.Control Controls
			{
				get
				{
					return mass;
				}
				
			}
			override public System.String ValueString
			{
				get
				{
					return "";
				}
				
			}
			public EditContainedPiecesAction Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			internal AnonymousClassConfigurer(VassalSharp.counters.MassPieceDefiner mass, EditContainedPiecesAction enclosingInstance, System.String Param1, System.String Param2):base(Param1, Param2)
			{
				InitBlock(mass, enclosingInstance);
			}
			public override void  setValue(System.String s)
			{
			}
		}
		private const long serialVersionUID = 1L;
		
		private Configurable target;
		
		//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
		public EditContainedPiecesAction(Configurable target):base("Edit All Contained Pieces")
		{
			this.target = target;
		}
		
		public override void  actionPerformed(System.Object event_sender, System.EventArgs evt)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'mass '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MassPieceDefiner mass = new MassPieceDefiner(target);
			Configurer c = new AnonymousClassConfigurer(mass, this, "", "");
			//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			ConfigurerWindow w = new ConfigurerWindow(c);
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			w.Visible = true;
			if (!w.Cancelled && mass.Changed)
			{
				mass.save();
			}
		}
	}
}