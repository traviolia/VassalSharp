using System;
namespace VassalSharp.tools
{
	
	/// <summary> Indicates a component with a toolbar</summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public interface ToolBarComponent
	{
		System.Windows.Forms.ToolStrip ToolBar
		{
			get;
			
		}
	}
}