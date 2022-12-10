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
using GameModule = VassalSharp.build.GameModule;
using SaveAction = VassalSharp.configure.SaveAction;
using ValidationReport = VassalSharp.configure.ValidationReport;
using ValidationReportDialog = VassalSharp.configure.ValidationReportDialog;
namespace VassalSharp.launch
{
	
	[Serializable]
	public class SaveModuleAction:SaveAction
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassCallBack' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassCallBack : ValidationReportDialog.CallBack
		{
			public AnonymousClassCallBack(SaveModuleAction enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SaveModuleAction enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SaveModuleAction enclosingInstance;
			public SaveModuleAction Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  ok()
			{
				Enclosing_Instance.save();
			}
			
			public virtual void  cancel()
			{
			}
		}
		private const long serialVersionUID = 1L;
		
		public SaveModuleAction()
		{
		}
		
		public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'report '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			ValidationReport report = new ValidationReport();
			GameModule.getGameModule().validate(GameModule.getGameModule(), report);
			if (report.getWarnings().size() == 0)
			{
				save();
			}
			else
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				new ValidationReportDialog(report, new AnonymousClassCallBack(this)).Visible = true;
			}
		}
		
		protected internal virtual void  save()
		{
			GameModule.getGameModule().save();
		}
	}
}