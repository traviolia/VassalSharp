using System;
using ImageUtils = VassalSharp.tools.image.ImageUtils;
namespace VassalSharp.tools
{
	
	[Serializable]
	public class IconButton:System.Windows.Forms.Button
	{
		private const long serialVersionUID = 1L;
		
		public const int PLUS_ICON = 0;
		public const int MINUS_ICON = 1;
		public const int TICK_ICON = 2;
		public const int CROSS_ICON = 3;
		
		public IconButton(int type):this(type, 22)
		{
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public IconButton(int type, int size):this(type, size, ref getDefaultColor(type), 2.0f)
		{
		}
		
		public static System.Drawing.Color getDefaultColor(int type)
		{
			switch (type)
			{
				
				case TICK_ICON: 
					return System.Drawing.Color.Green;
				
				case CROSS_ICON: 
					return System.Drawing.Color.Red;
				
				default: 
					return System.Drawing.Color.Black;
				
			}
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public IconButton(int type, int size, ref System.Drawing.Color color, float width):base()
		{
			
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMinimumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMinimumSize_javaawtDimension'"
			setMinimumSize(new System.Drawing.Size(size, size));
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			Size = new System.Drawing.Size(size, size);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'image '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Bitmap image = ImageUtils.createCompatibleTranslucentImage(size, size);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
			//UPGRADE_TODO: Method 'java.awt.Graphics2D.setStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DsetStroke_javaawtStroke'"
			//UPGRADE_TODO: Constructor 'java.awt.BasicStroke.BasicStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtBasicStrokeBasicStroke_float'"
			SupportClass.GraphicsManager.manager.SetPen(g, new System.Drawing.Pen(System.Drawing.Brushes.Black, width));
			SupportClass.GraphicsManager.manager.SetColor(g, color);
			
			switch (type)
			{
				
				case PLUS_ICON: 
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), 5, size / 2, size - 5, size / 2);
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), size / 2, 5, size / 2, size - 5);
					break;
				
				case MINUS_ICON: 
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), 5, size / 2, size - 5, size / 2);
					break;
				
				case TICK_ICON: 
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), 5, size / 2, size / 2, size - 5);
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), size / 2, size - 5, 5, size - 5);
					break;
				
				case CROSS_ICON: 
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), 5, 5, size - 5, size - 5);
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), 5, size - 5, size - 5, 5);
					break;
				}
			Image = (System.Drawing.Image) image.Clone();
		}
	}
}