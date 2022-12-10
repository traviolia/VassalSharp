using System;
namespace VassalSharp.tools
{
	
	/// <summary> Stores components in a dummy toolbar, then transfers them to another toolbar
	/// component when it becomes available. Used to get around lazy creation of
	/// toolbars in ToolBarComponents
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class TemporaryToolBar : ToolBarComponent
	{
		public TemporaryToolBar()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			System.Windows.Forms.ToolBar temp_ToolBar;
			System.Windows.Forms.ImageList temp_ImageList;
			temp_ImageList = new System.Windows.Forms.ImageList();
			temp_ToolBar = new System.Windows.Forms.ToolBar();
			temp_ToolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(SupportClass.ToolBarButtonClicked);
			temp_ToolBar.ImageList = temp_ImageList;
			tempToolBar = temp_ToolBar;
		}
		virtual public System.Windows.Forms.ToolBar ToolBar
		{
			get
			{
				return tempToolBar != null?tempToolBar:delegate_Renamed.ToolBar;
			}
			
		}
		virtual public ToolBarComponent Delegate
		{
			set
			{
				if (tempToolBar != null)
				{
					while ((int) tempToolBar.Controls.Count > 0)
					{
						System.Windows.Forms.Control c = tempToolBar.Controls[0];
						tempToolBar.Buttons.Remove((System.Windows.Forms.ToolBarButton) c.Tag);
						System.Windows.Forms.ToolBarButton temp_ToolBarButton;
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
						value.ToolBar.Controls.Add(c);
					}
				}
				tempToolBar = null;
				this.delegate_Renamed = value;
			}
			
		}
		//UPGRADE_NOTE: The initialization of  'tempToolBar' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private System.Windows.Forms.ToolBar tempToolBar;
		private ToolBarComponent delegate_Renamed;
	}
}