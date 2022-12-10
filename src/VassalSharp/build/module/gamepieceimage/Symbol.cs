/*
* $Id$
*
* Copyright (c) 2005 by Rodney Kinney, Brent Easton
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
using ImageUtils = VassalSharp.tools.image.ImageUtils;
namespace VassalSharp.build.module.gamepieceimage
{
	
	public class Symbol
	{
		
		protected internal const System.String NATO = "NATO Unit Symbols";
		//UPGRADE_NOTE: Final was removed from the declaration of 'SYMBOL_SETS '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal static readonly System.String[] SYMBOL_SETS = new System.String[]{NATO};
		
		//  public static void draw(Graphics g, Rectangle r, Color fg, Color bg, String
		// symbolSet, String symbolName) {
		//
		//    if (symbolSet.equals(NATO_SIZE_SET)) {
		//
		//    }
		//    else if (symbolSet.equals(NATO_UNIT_SET)) {
		//      NatoUnitSymbolSet.draw(g, r, fg, bg, symbolName);
		//    }
		//
		//  }
		
		protected internal System.String symbolSetName;
		protected internal System.String symbolName1;
		protected internal System.String symbolName2;
		protected internal System.String symbolSize;
		
		public Symbol(System.String setName, System.String name1, System.String name2, System.String size)
		{
			symbolSetName = setName;
			symbolName1 = name1;
			symbolName2 = name2;
			symbolSize = size;
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual void  draw(System.Drawing.Graphics g, ref System.Drawing.Rectangle bounds, ref System.Drawing.Color fg, ref System.Drawing.Color bg, ref System.Drawing.Color sz, float lineWidth)
		{
			
			if (symbolSetName.Equals(NATO))
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				NatoUnitSymbolSet.draw(symbolName1, symbolName2, g, ref bounds, ref fg, ref bg, ref sz, lineWidth, symbolSize);
			}
		}
		
		public class NatoUnitSymbolSet
		{
			protected internal static System.String[] SymbolNames
			{
				get
				{
					return new System.String[]{NONE, INFANTRY, RECON, ARMORED, ARTILLERY, ENGINEERS, AIRBORNE, AIR_DEFENCE, AIR_FORCE, ANTI_TANK, ARMY_AVIATION, COMMANDO, GLIDER, GUERILLA, MOUNTAIN, NAVY};
				}
				
			}
			protected internal static System.String[] SymbolSizes
			{
				get
				{
					if (sizeNames == null)
					{
						sizeNames = new System.String[SIZES.Length];
						for (int i = 0; i < SIZES.Length; i++)
						{
							sizeNames[i] = SIZES[i].Name;
						}
					}
					return sizeNames;
				}
				
			}
			
			protected internal const System.String SZ_NONE = "None";
			protected internal const System.String SZ_INSTALLATION = "Installation";
			protected internal const System.String SZ_TEAM = "Team";
			protected internal const System.String SZ_SQUAD = "Squad";
			protected internal const System.String SZ_SECTION = "Section";
			protected internal const System.String SZ_PLATOON = "Platoon";
			protected internal const System.String SZ_ECHELON = "Echelon";
			protected internal const System.String SZ_COMPANY = "Company";
			protected internal const System.String SZ_BATTALION = "Battalion";
			protected internal const System.String SZ_REGIMENT = "Regiment";
			protected internal const System.String SZ_BRIGADE = "Brigade";
			protected internal const System.String SZ_DIVISION = "Division";
			protected internal const System.String SZ_CORPS = "Corps";
			protected internal const System.String SZ_ARMY = "Army";
			protected internal const System.String SZ_ARMY_GROUP = "Army Group";
			protected internal const System.String SZ_REGION = "Region";
			
			protected internal const System.String NONE = "None";
			protected internal const System.String AIRBORNE = "Airborne";
			protected internal const System.String AIR_DEFENCE = "Air Defence";
			protected internal const System.String AIR_FORCE = "Air Force";
			//    protected static final String AIR_MOBILE = "Air Mobile";
			//    protected static final String AMPHIBIOUS = "Amphibious";
			protected internal const System.String ANTI_TANK = "Anti Tank";
			protected internal const System.String ARMORED = "Armored";
			protected internal const System.String ARMY_AVIATION = "Army Aviation";
			protected internal const System.String ARTILLERY = "Artillery";
			//    protected static final String BRIDGING = "Bridging";
			//    protected static final String COMBAT_SERVICE_SUPPORT = "";
			protected internal const System.String COMMANDO = "Commando";
			//    protected static final String ELECTRONIC_RANGING = "";
			//    protected static final String ELECTRONIC_WARFARE = "";
			protected internal const System.String ENGINEERS = "Engineers";
			protected internal const System.String GLIDER = "Glider-Borne";
			protected internal const System.String GUERILLA = "Guerilla";
			//    protected static final String HEADQUARTERS_SUPPORT = "";
			protected internal const System.String INFANTRY = "Infantry";
			//    protected static final String LABOR_RESOURCES = "";
			//    protected static final String MAINTENANCE = "";
			protected internal const System.String MARINES = "Marines";
			//    protected static final String METEOROLOGICAL = "";
			//    protected static final String MILITARY_CIVIL = "";
			//    protected static final String MP = "";
			//    protected static final String MISSILE = "";
			protected internal const System.String MOUNTAIN = "Mountain";
			protected internal const System.String NAVY = "";
			//    protected static final String NBC = "";
			//    protected static final String ORDNANCE = "";
			//    protected static final String PARACHUTE = "";
			//    protected static final String PAY_FINANCE = "";
			//    protected static final String PERSONNEL = "";
			//    protected static final String PIPELINE = "";
			//    protected static final String POSTAL = "";
			//    protected static final String PSYCH = "";
			//    protected static final String QUARTERMASTER = "";
			protected internal const System.String RECON = "Cavalry/Recon";
			
			//    protected static final String REPLACEMENT = "";
			//    protected static final String SERVICE = "";
			//    protected static final String SIGNAL = "";
			//    protected static final String SOUND_RANGING = "";
			//    protected static final String SUPPLY = "";
			//    protected static final String TRANSPORT = "";
			//    protected static final String TOPO = "";
			//    protected static final String UNMANNED_AIR = "";
			//    protected static final String VET = "";
			
			protected internal const System.String INSTALLATION_SYMBOL = "m"; //$NON-NLS-1$
			protected internal const System.String TEAM_SYMBOL = "o"; //$NON-NLS-1$
			protected internal const System.String SQUAD_SYMBOL = "s"; //$NON-NLS-1$
			protected internal const System.String COMPANY_SYMBOL = "i"; //$NON-NLS-1$
			protected internal const System.String BRIGADE_SYMBOL = "x"; //$NON-NLS-1$
			
			protected internal static System.String[] sizeNames;
			
			protected internal static SizeOption findSize(System.String name)
			{
				for (int i = 0; i < SIZES.Length; i++)
				{
					if (name.Equals(SIZES[i].Name))
					{
						return SIZES[i];
					}
				}
				return SIZES[0];
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'SIZES '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			protected internal static readonly SizeOption[] SIZES = new SizeOption[]{new SizeOption(SZ_NONE, 0, ""), new SizeOption(SZ_INSTALLATION, 1, INSTALLATION_SYMBOL), new SizeOption(SZ_TEAM, 1, TEAM_SYMBOL), new SizeOption(SZ_SQUAD, 1, SQUAD_SYMBOL), new SizeOption(SZ_SECTION, 2, SQUAD_SYMBOL), new SizeOption(SZ_PLATOON, 3, SQUAD_SYMBOL), new SizeOption(SZ_ECHELON, 4, SQUAD_SYMBOL), new SizeOption(SZ_COMPANY, 1, COMPANY_SYMBOL), new SizeOption(SZ_BATTALION, 2, COMPANY_SYMBOL), new SizeOption(SZ_REGIMENT, 3, COMPANY_SYMBOL), new SizeOption(SZ_BRIGADE, 1, BRIGADE_SYMBOL), new SizeOption(SZ_DIVISION, 2, BRIGADE_SYMBOL), new SizeOption(SZ_CORPS, 3, BRIGADE_SYMBOL), new SizeOption(SZ_ARMY, 4, BRIGADE_SYMBOL), new SizeOption(SZ_ARMY_GROUP, 5, BRIGADE_SYMBOL), new SizeOption(SZ_REGION, 6, BRIGADE_SYMBOL)};
			
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			protected internal static void  draw(System.String name1, System.String name2, System.Drawing.Graphics g, ref System.Drawing.Rectangle bounds, ref System.Drawing.Color fg, ref System.Drawing.Color bg, ref System.Drawing.Color sz, float lineWidth, System.String size)
			{
				
				if (!bg.IsEmpty)
				{
					SupportClass.GraphicsManager.manager.SetColor(g, bg);
					g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), bounds.X, bounds.Y, bounds.Width, bounds.Height);
				}
				
				SupportClass.GraphicsManager.manager.SetColor(g, fg);
				//UPGRADE_TODO: Constructor 'java.awt.BasicStroke.BasicStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtBasicStrokeBasicStroke_float_int_int'"
				System.Drawing.Pen stroke = SupportClass.StrokeConsSupport.CreatePenInstance(lineWidth, (int) System.Drawing.Drawing2D.LineCap.Round, (int) System.Drawing.Drawing2D.LineJoin.Round);
				System.Drawing.Graphics g2 = ((System.Drawing.Graphics) g);
				//UPGRADE_TODO: Method 'java.awt.Graphics2D.setStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DsetStroke_javaawtStroke'"
				SupportClass.GraphicsManager.manager.SetPen(g2, stroke);
				//g2.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
				
				g.DrawRectangle(SupportClass.GraphicsManager.manager.GetPen(g), bounds.X, bounds.Y, bounds.Width, bounds.Height);
				
				SupportClass.GraphicsManager.manager.SetColor(g, sz);
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				drawSize(g, size, ref bounds);
				
				SupportClass.GraphicsManager.manager.SetColor(g, fg);
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				draw(g, lineWidth, name1, ref bounds, false);
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				draw(g, lineWidth, name2, ref bounds, true);
			}
			
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			protected internal static void  draw(System.Drawing.Graphics g, float lineWidth, System.String name, ref System.Drawing.Rectangle bounds, bool drawLow)
			{
				
				System.Drawing.Graphics g2 = (System.Drawing.Graphics) g;
				//UPGRADE_TODO: Constructor 'java.awt.BasicStroke.BasicStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtBasicStrokeBasicStroke_float_int_int'"
				System.Drawing.Pen stroke = SupportClass.StrokeConsSupport.CreatePenInstance(lineWidth, (int) System.Drawing.Drawing2D.LineCap.Round, (int) System.Drawing.Drawing2D.LineJoin.Round);
				//UPGRADE_TODO: Method 'java.awt.Graphics2D.setStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DsetStroke_javaawtStroke'"
				SupportClass.GraphicsManager.manager.SetPen(g2, stroke);
				
				int x_left = bounds.X;
				int x_center = bounds.X + bounds.Width / 2 + 1;
				int x_right = bounds.X + bounds.Width;
				
				int y_top = bounds.Y;
				int y_center = bounds.Y + bounds.Height / 2;
				int y_bottom = bounds.Y + bounds.Height;
				
				if (name.Equals(NONE))
				{
					
				}
				else if (name.Equals(AIRBORNE))
				{
					int x1 = x_center - bounds.Width / 4;
					int y1 = y_top + bounds.Height * 4 / 5 + 1;
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.draw' was converted to 'System.Drawing.Graphics.DrawPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Ddraw_javaawtShape'"
					g2.DrawPath(SupportClass.GraphicsManager.manager.GetPen(g2), SupportClass.Arc2DSupport.CreateArc2D((float) x1, (float) y1, (float) (bounds.Width / 4), (float) (bounds.Height / 4), (float) 0, (float) 180, SupportClass.Arc2DSupport.OPEN));
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.draw' was converted to 'System.Drawing.Graphics.DrawPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Ddraw_javaawtShape'"
					g2.DrawPath(SupportClass.GraphicsManager.manager.GetPen(g2), SupportClass.Arc2DSupport.CreateArc2D((float) x_center, (float) y1, (float) (bounds.Width / 4), (float) (bounds.Height / 4), (float) 0, (float) 180, SupportClass.Arc2DSupport.OPEN));
				}
				else if (name.Equals(AIR_DEFENCE))
				{
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.draw' was converted to 'System.Drawing.Graphics.DrawPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Ddraw_javaawtShape'"
					g2.DrawPath(SupportClass.GraphicsManager.manager.GetPen(g2), SupportClass.Arc2DSupport.CreateArc2D((float) x_left, (float) (y_top + bounds.Height / 4), (float) bounds.Width, (float) (bounds.Height * 1.5), (float) 0, (float) 180, SupportClass.Arc2DSupport.OPEN));
				}
				else if (name.Equals(AIR_FORCE))
				{
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					int xoff1 = (int) (bounds.Width * 0.15);
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					int xoff2 = (int) (bounds.Width * 0.2);
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					int yoff = (int) (bounds.Height * 0.35);
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), x_center - xoff2, y_top + yoff, x_center + xoff2, y_bottom - yoff);
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), x_center + xoff2, y_top + yoff, x_center - xoff2, y_bottom - yoff);
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.draw' was converted to 'System.Drawing.Graphics.DrawPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Ddraw_javaawtShape'"
					g2.DrawPath(SupportClass.GraphicsManager.manager.GetPen(g2), SupportClass.Arc2DSupport.CreateArc2D((float) (x_center - xoff2 - xoff1), (float) (y_top + yoff), (float) (xoff1 * 2), (float) (bounds.Height - (2 * yoff)), (float) 90, (float) 180, SupportClass.Arc2DSupport.OPEN));
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.draw' was converted to 'System.Drawing.Graphics.DrawPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Ddraw_javaawtShape'"
					g2.DrawPath(SupportClass.GraphicsManager.manager.GetPen(g2), SupportClass.Arc2DSupport.CreateArc2D((float) (x_center + xoff2 - xoff1), (float) (y_top + yoff), (float) (xoff1 * 2), (float) (bounds.Height - (2 * yoff)), (float) 270, (float) 180, SupportClass.Arc2DSupport.OPEN));
				}
				else if (name.Equals(ANTI_TANK))
				{
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), x_left, y_bottom, x_center, y_top);
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), x_center, y_top, x_right, y_bottom);
				}
				else if (name.Equals(ARMORED))
				{
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					int yoff = (int) (bounds.Height * .25);
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					int xoff1 = (int) (bounds.Width * .15);
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					int xoff2 = (int) (bounds.Width * .20);
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), x_left + xoff1 + xoff2, y_top + yoff, x_right - xoff1 - xoff2, y_top + yoff);
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), x_left + xoff1 + xoff2, y_bottom - yoff, x_right - xoff1 - xoff2, y_bottom - yoff);
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.draw' was converted to 'System.Drawing.Graphics.DrawPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Ddraw_javaawtShape'"
					g2.DrawPath(SupportClass.GraphicsManager.manager.GetPen(g2), SupportClass.Arc2DSupport.CreateArc2D((float) (x_left + xoff1), (float) (y_top + yoff), (float) (xoff2 * 2), (float) (bounds.Height - (yoff * 2)), (float) 90, (float) 180, SupportClass.Arc2DSupport.OPEN));
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.draw' was converted to 'System.Drawing.Graphics.DrawPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Ddraw_javaawtShape'"
					g2.DrawPath(SupportClass.GraphicsManager.manager.GetPen(g2), SupportClass.Arc2DSupport.CreateArc2D((float) (x_right - xoff1 - (2 * xoff2)), (float) (y_top + yoff), (float) (xoff2 * 2), (float) (bounds.Height - (yoff * 2)), (float) 270, (float) 180, SupportClass.Arc2DSupport.OPEN));
				}
				else if (name.Equals(ARMY_AVIATION))
				{
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					int xoff = (int) (bounds.Height * 0.25);
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					int yoff = (int) (bounds.Height * 0.33);
					System.Drawing.Drawing2D.GraphicsPath temp_GraphicsPath;
					temp_GraphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
					temp_GraphicsPath.FillMode = System.Drawing.Drawing2D.FillMode.Winding;
					System.Drawing.Drawing2D.GraphicsPath p = temp_GraphicsPath;
					//UPGRADE_TODO: Method 'java.awt.geom.GeneralPath.moveTo' was converted to 'System.Drawing.Drawing2D.GraphicsPath.AddLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtgeomGeneralPathmoveTo_float_float'"
					p.AddLine(x_left + xoff, y_top + yoff, x_left + xoff, y_top + yoff);
					p.AddLine(p.PathPoints[p.PathPoints.Length - 1].X, p.PathPoints[p.PathPoints.Length - 1].Y, x_right - yoff, y_bottom - yoff);
					p.AddLine(p.PathPoints[p.PathPoints.Length - 1].X, p.PathPoints[p.PathPoints.Length - 1].Y, x_right - yoff, y_top + yoff);
					p.AddLine(p.PathPoints[p.PathPoints.Length - 1].X, p.PathPoints[p.PathPoints.Length - 1].Y, x_left + xoff, y_bottom - yoff);
					p.CloseFigure();
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.draw' was converted to 'System.Drawing.Graphics.DrawPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Ddraw_javaawtShape'"
					g2.DrawPath(SupportClass.GraphicsManager.manager.GetPen(g2), p);
				}
				else if (name.Equals(ARTILLERY))
				{
					int radius = bounds.Height / 5;
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					int yoff = (drawLow?(int) (bounds.Height * .2):0);
					g.FillEllipse(SupportClass.GraphicsManager.manager.GetPaint(g), x_center - radius, y_center - radius + yoff, radius * 2, radius * 2);
				}
				else if (name.Equals(COMMANDO))
				{
					
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), x_left, y_top, x_right, y_bottom);
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), x_left, y_bottom, x_right, y_top);
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					int x1 = (int) (bounds.Width / 2.5);
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					int y1 = (int) (bounds.Height / 2.5);
					
					System.Drawing.Drawing2D.GraphicsPath temp_GraphicsPath2;
					temp_GraphicsPath2 = new System.Drawing.Drawing2D.GraphicsPath();
					temp_GraphicsPath2.FillMode = System.Drawing.Drawing2D.FillMode.Winding;
					System.Drawing.Drawing2D.GraphicsPath p = temp_GraphicsPath2;
					//UPGRADE_TODO: Method 'java.awt.geom.GeneralPath.moveTo' was converted to 'System.Drawing.Drawing2D.GraphicsPath.AddLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtgeomGeneralPathmoveTo_float_float'"
					p.AddLine(x_left, y_top, x_left, y_top);
					p.AddLine(p.PathPoints[p.PathPoints.Length - 1].X, p.PathPoints[p.PathPoints.Length - 1].Y, x_left + x1, y_top);
					p.AddLine(p.PathPoints[p.PathPoints.Length - 1].X, p.PathPoints[p.PathPoints.Length - 1].Y, x_left + x1, y_top + y1);
					p.AddLine(p.PathPoints[p.PathPoints.Length - 1].X, p.PathPoints[p.PathPoints.Length - 1].Y, x_left, y_top);
					//UPGRADE_TODO: Method 'java.awt.geom.GeneralPath.moveTo' was converted to 'System.Drawing.Drawing2D.GraphicsPath.AddLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtgeomGeneralPathmoveTo_float_float'"
					p.AddLine(x_right, y_top, x_right, y_top);
					p.AddLine(p.PathPoints[p.PathPoints.Length - 1].X, p.PathPoints[p.PathPoints.Length - 1].Y, x_right - x1, y_top);
					p.AddLine(p.PathPoints[p.PathPoints.Length - 1].X, p.PathPoints[p.PathPoints.Length - 1].Y, x_right - x1, y_top + y1);
					p.AddLine(p.PathPoints[p.PathPoints.Length - 1].X, p.PathPoints[p.PathPoints.Length - 1].Y, x_right, y_top);
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.fill' was converted to 'System.Drawing.Graphics.FillPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Dfill_javaawtShape'"
					g2.FillPath(p);
				}
				else if (name.Equals(ENGINEERS))
				{
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.getStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DgetStroke'"
					System.Drawing.Pen oldStroke = (System.Drawing.Pen) SupportClass.GraphicsManager.manager.GetPen(g2);
					//UPGRADE_TODO: Constructor 'java.awt.BasicStroke.BasicStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtBasicStrokeBasicStroke_float_int_int'"
					System.Drawing.Pen estroke = SupportClass.StrokeConsSupport.CreatePenInstance(oldStroke.Width * 1.2f, (int) System.Drawing.Drawing2D.LineCap.Round, (int) System.Drawing.Drawing2D.LineJoin.Round);
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.setStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DsetStroke_javaawtStroke'"
					SupportClass.GraphicsManager.manager.SetPen(g2, estroke);
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					int yh = (int) (bounds.Height * 0.2);
					int y1 = drawLow?y_bottom - yh - 1:y_top + (bounds.Height - yh) / 2;
					int y2 = y1 + yh;
					int x1 = x_center - bounds.Width / 5;
					int x2 = x_center + bounds.Width / 5;
					
					System.Drawing.Drawing2D.GraphicsPath temp_GraphicsPath3;
					temp_GraphicsPath3 = new System.Drawing.Drawing2D.GraphicsPath();
					temp_GraphicsPath3.FillMode = System.Drawing.Drawing2D.FillMode.Winding;
					System.Drawing.Drawing2D.GraphicsPath p = temp_GraphicsPath3;
					//UPGRADE_TODO: Method 'java.awt.geom.GeneralPath.moveTo' was converted to 'System.Drawing.Drawing2D.GraphicsPath.AddLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtgeomGeneralPathmoveTo_float_float'"
					p.AddLine(x1, y2, x1, y2);
					p.AddLine(p.PathPoints[p.PathPoints.Length - 1].X, p.PathPoints[p.PathPoints.Length - 1].Y, x1, y1);
					p.AddLine(p.PathPoints[p.PathPoints.Length - 1].X, p.PathPoints[p.PathPoints.Length - 1].Y, x2, y1);
					p.AddLine(p.PathPoints[p.PathPoints.Length - 1].X, p.PathPoints[p.PathPoints.Length - 1].Y, x2, y2);
					//UPGRADE_TODO: Method 'java.awt.geom.GeneralPath.moveTo' was converted to 'System.Drawing.Drawing2D.GraphicsPath.AddLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtgeomGeneralPathmoveTo_float_float'"
					p.AddLine(x_center, y2, x_center, y2);
					p.AddLine(p.PathPoints[p.PathPoints.Length - 1].X, p.PathPoints[p.PathPoints.Length - 1].Y, x_center, y1);
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.draw' was converted to 'System.Drawing.Graphics.DrawPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Ddraw_javaawtShape'"
					g2.DrawPath(SupportClass.GraphicsManager.manager.GetPen(g2), p);
				}
				else if (name.Equals(GLIDER))
				{
					
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), x_left + (x_center - x_left) / 3, y_center, x_right - (x_center - x_left) / 3, y_center);
				}
				else if (name.Equals(GUERILLA))
				{
					
					System.Drawing.Drawing2D.GraphicsPath temp_GraphicsPath4;
					temp_GraphicsPath4 = new System.Drawing.Drawing2D.GraphicsPath();
					temp_GraphicsPath4.FillMode = System.Drawing.Drawing2D.FillMode.Winding;
					System.Drawing.Drawing2D.GraphicsPath p = temp_GraphicsPath4;
					//UPGRADE_TODO: Method 'java.awt.geom.GeneralPath.moveTo' was converted to 'System.Drawing.Drawing2D.GraphicsPath.AddLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtgeomGeneralPathmoveTo_float_float'"
					p.AddLine(x_left, y_top, x_left, y_top);
					p.AddLine(p.PathPoints[p.PathPoints.Length - 1].X, p.PathPoints[p.PathPoints.Length - 1].Y, x_right, y_bottom);
					p.AddLine(p.PathPoints[p.PathPoints.Length - 1].X, p.PathPoints[p.PathPoints.Length - 1].Y, x_right, y_top);
					p.AddLine(p.PathPoints[p.PathPoints.Length - 1].X, p.PathPoints[p.PathPoints.Length - 1].Y, x_left, y_bottom);
					p.AddLine(p.PathPoints[p.PathPoints.Length - 1].X, p.PathPoints[p.PathPoints.Length - 1].Y, x_left, y_top);
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.fill' was converted to 'System.Drawing.Graphics.FillPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Dfill_javaawtShape'"
					g2.FillPath(p);
				}
				else if (name.Equals(INFANTRY))
				{
					
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), x_left, y_top, x_right, y_bottom);
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), x_left, y_bottom, x_right, y_top);
				}
				else if (name.Equals(MARINES))
				{
					
				}
				else if (name.Equals(MOUNTAIN))
				{
					int x_off = bounds.Width / 6;
					System.Drawing.Drawing2D.GraphicsPath temp_GraphicsPath5;
					temp_GraphicsPath5 = new System.Drawing.Drawing2D.GraphicsPath();
					temp_GraphicsPath5.FillMode = System.Drawing.Drawing2D.FillMode.Winding;
					System.Drawing.Drawing2D.GraphicsPath p = temp_GraphicsPath5;
					//UPGRADE_TODO: Method 'java.awt.geom.GeneralPath.moveTo' was converted to 'System.Drawing.Drawing2D.GraphicsPath.AddLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtgeomGeneralPathmoveTo_float_float'"
					p.AddLine(x_center, y_center, x_center, y_center);
					p.AddLine(p.PathPoints[p.PathPoints.Length - 1].X, p.PathPoints[p.PathPoints.Length - 1].Y, x_center + x_off, y_bottom);
					p.AddLine(p.PathPoints[p.PathPoints.Length - 1].X, p.PathPoints[p.PathPoints.Length - 1].Y, x_center - x_off, y_bottom);
					p.CloseFigure();
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.fill' was converted to 'System.Drawing.Graphics.FillPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Dfill_javaawtShape'"
					g2.FillPath(p);
				}
				else if (name.Equals(NAVY))
				{
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					int yoff1 = (int) (bounds.Height * 0.20);
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					int yoff2 = (int) (bounds.Height * 0.15);
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					int xoff1 = (int) (bounds.Width * 0.15);
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					int xoff2 = (int) (bounds.Width * 0.30);
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), x_center, y_top + yoff1, x_center, y_bottom - yoff1);
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), x_center - xoff1, y_top + yoff1 + yoff2, x_center + xoff1, y_top + yoff1 + yoff2);
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.draw' was converted to 'System.Drawing.Graphics.DrawPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Ddraw_javaawtShape'"
					g2.DrawPath(SupportClass.GraphicsManager.manager.GetPen(g2), SupportClass.Arc2DSupport.CreateArc2D((float) (x_center - xoff2), (float) (y_top + yoff1), (float) (xoff2 * 2), (float) (bounds.Height - (2 * yoff1)), (float) 225, (float) 90, SupportClass.Arc2DSupport.OPEN));
				}
				else if (name.Equals(RECON))
				{
					g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), bounds.X, bounds.Y + bounds.Height, bounds.X + bounds.Width, bounds.Y);
				}
			}
			
			/// <summary> </summary>
			/// <param name="g">      Grahics
			/// </param>
			/// <param name="size">   Name of size symbol
			/// </param>
			/// <param name="bounds"> Size of the unit symbol
			/// </param>
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			protected internal static void  drawSize(System.Drawing.Graphics g, System.String size, ref System.Drawing.Rectangle bounds)
			{
				
				if (size.Equals(SZ_NONE) || size.Equals(""))
				{
					//$NON-NLS-1$
					return ;
				}
				
				SizeOption option = findSize(size);
				System.String type = option.Type;
				int count = option.Count;
				
				int sym_w;
				int sym_h = bounds.Height / 3;
				if (count <= 4)
				{
					sym_w = bounds.Width / 5;
				}
				else
				{
					sym_w = bounds.Width / 7;
				}
				int gap = bounds.Width / 15;
				
				System.Drawing.Bitmap bi = buildSizeImage(g, count, type, sym_w, sym_h, gap);
				
				int xpos = bounds.X + (bounds.Width / 2) - (bi.Width / 2) + gap; // + (gap/2) - (bi.getWidth()/2);
				int ypos = bounds.Y - sym_h - 1;
				//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
				g.DrawImage(bi, xpos, ypos);
			}
			
			public static System.Drawing.Bitmap buildSizeImage(System.String size, int sym_w, int sym_h, int gap)
			{
				
				SizeOption option = findSize(size);
				System.String type = option.Type;
				int count = option.Count;
				
				System.Drawing.Bitmap bi = createImage(count, sym_w, sym_h, gap);
				System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bi);
				System.Drawing.Color tempAux = System.Drawing.Color.Empty;
				SupportClass.GraphicsManager.manager.SetColor(g, tempAux);
				g.setColor(Color.BLACK);
				return buildSizeImage(g, count, type, sym_w, sym_h, gap);
			}
			
			protected internal static System.Drawing.Bitmap createImage(int count, int sym_w, int sym_h, int gap)
			{
				int w = sym_w * count + gap * (count - 1) + 1;
				if (w < 1)
					w = sym_w;
				return ImageUtils.createCompatibleTranslucentImage(w, sym_h + 1);
			}
			
			public static System.Drawing.Bitmap buildSizeImage(System.Drawing.Graphics g, int count, System.String type, int sym_w, int sym_h, int gap)
			{
				
				System.Drawing.Graphics g2 = (System.Drawing.Graphics) g;
				System.Drawing.Bitmap bi;
				
				if (type.Equals(INSTALLATION_SYMBOL))
				{
					bi = createImage(count, sym_w * 3, sym_h, gap);
				}
				else
				{
					bi = createImage(count, sym_w, sym_h, gap);
				}
				System.Drawing.Graphics big = System.Drawing.Graphics.FromImage(bi);
				SupportClass.GraphicsManager.manager.SetColor(big, SupportClass.GraphicsManager.manager.GetColor(g2));
				System.Drawing.Color tempAux = System.Drawing.Color.Empty;
				SupportClass.GraphicsManager.manager.SetColor(big, tempAux);
				
				// Force Size symbol to be drawn 1 pixel wide with anti-aliasing to ensure readability
				//UPGRADE_TODO: Method 'java.awt.Graphics2D.setStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DsetStroke_javaawtStroke'"
				//UPGRADE_TODO: Constructor 'java.awt.BasicStroke.BasicStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtBasicStrokeBasicStroke_float_int_int'"
				SupportClass.GraphicsManager.manager.SetPen(big, SupportClass.StrokeConsSupport.CreatePenInstance(1.0f, (int) System.Drawing.Drawing2D.LineCap.Round, (int) System.Drawing.Drawing2D.LineJoin.Round));
				//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setRenderingHint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetRenderingHint_javaawtRenderingHintsKey_javalangObject'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_ANTIALIASING' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_ANTIALIASING_f'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.VALUE_ANTIALIAS_ON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsVALUE_ANTIALIAS_ON_f'"
				big.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
				
				int x_pos = 0;
				for (int i = 0; i < count; i++)
				{
					if (type.Equals(TEAM_SYMBOL))
					{
						int radius = sym_w / 2;
						big.DrawEllipse(SupportClass.GraphicsManager.manager.GetPen(big), x_pos, sym_h / 3, radius * 2, radius * 2);
						big.DrawLine(SupportClass.GraphicsManager.manager.GetPen(big), x_pos, sym_h, x_pos + sym_w, 0);
					}
					else if (type.Equals(SQUAD_SYMBOL))
					{
						int radius = sym_w / 2;
						big.FillEllipse(SupportClass.GraphicsManager.manager.GetPaint(big), x_pos, sym_h / 3, radius * 2, radius * 2);
					}
					else if (type.Equals(COMPANY_SYMBOL))
					{
						big.DrawLine(SupportClass.GraphicsManager.manager.GetPen(big), x_pos + sym_w / 2, 0, x_pos + sym_w / 2, sym_h);
					}
					else if (type.Equals(BRIGADE_SYMBOL))
					{
						big.DrawLine(SupportClass.GraphicsManager.manager.GetPen(big), x_pos, 0, x_pos + sym_w, sym_h);
						big.DrawLine(SupportClass.GraphicsManager.manager.GetPen(big), x_pos, sym_h, x_pos + sym_w, 0);
					}
					else if (type.Equals(INSTALLATION_SYMBOL))
					{
						big.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(big), x_pos, sym_h / 2, x_pos + 3 * sym_w, sym_h);
					}
					x_pos += sym_w + gap;
				}
				
				big.Dispose();
				
				return bi;
			}
		}
	}
	
	//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
	public class SizeOption
	{
		virtual public System.String Name
		{
			get
			{
				return name;
			}
			
		}
		virtual public System.String Type
		{
			get
			{
				return type;
			}
			
		}
		virtual public int Count
		{
			get
			{
				return count;
			}
			
		}
		internal System.String name;
		internal System.String type;
		internal int count;
		
		public SizeOption(System.String n, int c, System.String t)
		{
			name = n;
			type = t;
			count = c;
		}
	}
}