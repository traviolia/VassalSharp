/*
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
using Info = VassalSharp.Info;
using GameModule = VassalSharp.build.GameModule;
using ModuleExtension = VassalSharp.build.module.ModuleExtension;
using Resources = VassalSharp.i18n.Resources;
using ArchiveWriter = VassalSharp.tools.ArchiveWriter;
namespace VassalSharp.launch
{
	
	/// <summary> Creates a new module extension and opens an extension edit window</summary>
	/// <author>  rodneykinney
	/// 
	/// </author>
	[Serializable]
	public class NewExtensionAction:GameModuleAction
	{
		private const long serialVersionUID = 1L;
		
		public NewExtensionAction(System.Windows.Forms.Control comp):base(Resources.getString("Editor.new_extension"), comp)
		{
		}
		
		public override void  performAction(System.Object event_sender, System.EventArgs e)
		{
			ModuleExtension ext = new ModuleExtension(new ArchiveWriter((System.String) null));
			ext.build();
			System.Windows.Forms.Form frame = GameModule.getGameModule().getFrame();
			ExtensionEditorWindow w = new ExtensionEditorWindow(GameModule.getGameModule(), ext);
			//UPGRADE_TODO: Method 'java.awt.Component.setLocation' was converted to 'System.Windows.Forms.Control.Location' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetLocation_int_int'"
			w.Location = new System.Drawing.Point(0, frame.Location.Y + frame.Height);
			//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
			w.Size = new System.Drawing.Size(Info.getScreenBounds(frame).Width / 2, w.Height);
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			w.Visible = true;
		}
	}
}