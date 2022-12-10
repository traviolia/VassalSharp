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
using AbstractConfigurable = VassalSharp.build.AbstractConfigurable;
using BadDataReport = VassalSharp.build.BadDataReport;
using Buildable = VassalSharp.build.Buildable;
using GameModule = VassalSharp.build.GameModule;
using GameState = VassalSharp.build.module.GameState;
using Map = VassalSharp.build.module.Map;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using AddPiece = VassalSharp.command.AddPiece;
using Command = VassalSharp.command.Command;
using MoveTracker = VassalSharp.command.MoveTracker;
using NullCommand = VassalSharp.command.NullCommand;
using ColorConfigurer = VassalSharp.configure.ColorConfigurer;
using HotKeyConfigurer = VassalSharp.configure.HotKeyConfigurer;
using VisibilityCondition = VassalSharp.configure.VisibilityCondition;
using BasicPiece = VassalSharp.counters.BasicPiece;
using GamePiece = VassalSharp.counters.GamePiece;
using Highlighter = VassalSharp.counters.Highlighter;
using PieceFilter = VassalSharp.counters.PieceFilter;
using PieceIterator = VassalSharp.counters.PieceIterator;
using Properties = VassalSharp.counters.Properties;
using Stack = VassalSharp.counters.Stack;
using Resources = VassalSharp.i18n.Resources;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
namespace VassalSharp.build.module.map
{
	
	/// <summary> Encapsulates information on how to draw expanded and unexpanded
	/// views of a stack
	/// </summary>
	public class StackMetrics:AbstractConfigurable
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPieceFilter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPieceFilter : PieceFilter
		{
			public AnonymousClassPieceFilter(StackMetrics enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(StackMetrics enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private StackMetrics enclosingInstance;
			public StackMetrics Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual bool accept(GamePiece piece)
			{
				return !true.Equals(piece.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME)) && !true.Equals(piece.getProperty(VassalSharp.counters.Properties_Fields.SELECTED));
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPieceFilter1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPieceFilter1 : PieceFilter
		{
			public AnonymousClassPieceFilter1(StackMetrics enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(StackMetrics enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private StackMetrics enclosingInstance;
			public StackMetrics Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual bool accept(GamePiece piece)
			{
				return !true.Equals(piece.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME)) && true.Equals(piece.getProperty(VassalSharp.counters.Properties_Fields.SELECTED));
			}
		}
		private void  InitBlock()
		{
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > [0];
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > []
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				Boolean.
			}
		}
		/// <summary> The color used to draw boxes representing counters beneath the top one in a stack.
		/// A value of null indicates that the counters should be drawn fully
		/// </summary>
		/// <returns>
		/// </returns>
		virtual public System.Drawing.Color BlankColor
		{
			get
			{
				return blankColor;
			}
			
		}
		virtual public bool StackingEnabled
		{
			get
			{
				return !disabled;
			}
			
		}
		public static System.String ConfigureTypeName
		{
			get
			{
				return Resources.getString("Editor.Stacking.component_type"); //$NON-NLS-1$
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[]{DISABLED, EXSEP_X, EXSEP_Y, UNEXSEP_X, UNEXSEP_Y, COLOR, TOP_KEY, BOTTOM_KEY, UP_KEY, DOWN_KEY};
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[]{Resources.getString("Editor.Stacking.disable"), Resources.getString("Editor.Stacking.h_expand"), Resources.getString("Editor.Stacking.v_expand"), Resources.getString("Editor.Stacking.hnon_expand"), Resources.getString("Editor.Stacking.vnon_expand"), Resources.getString("Editor.Stacking.color_nonexpand")};
			}
			
		}
		protected internal int exSepX, exSepY;
		protected internal int unexSepX, unexSepY;
		protected internal bool disabled = false;
		
		protected internal System.Windows.Forms.KeyEventArgs topKey = new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) ((int) System.Windows.Forms.Keys.Up | 0));
		protected internal System.Windows.Forms.KeyEventArgs bottomKey = new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) ((int) System.Windows.Forms.Keys.Down | 0));
		protected internal System.Windows.Forms.KeyEventArgs upKey = new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) ((int) System.Windows.Forms.Keys.Right | 0));
		protected internal System.Windows.Forms.KeyEventArgs downKey = new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) ((int) System.Windows.Forms.Keys.Left | 0));
		
		protected internal PieceFilter unselectedVisible;
		protected internal PieceFilter selectedVisible;
		
		protected internal System.Drawing.Color blankColor;
		
		public const System.String EXSEP_X = "exSepX";
		public const System.String EXSEP_Y = "exSepY";
		public const System.String UNEXSEP_X = "unexSepX";
		public const System.String UNEXSEP_Y = "unexSepY";
		public const System.String DISABLED = "disabled";
		public const System.String TOP_KEY = "top";
		public const System.String BOTTOM_KEY = "bottom";
		public const System.String UP_KEY = "up";
		public const System.String DOWN_KEY = "down";
		public const System.String COLOR = "color";
		
		public static int DEFAULT_EXSEP_X = 6;
		public static int DEFAULT_EXSEP_Y = 18;
		public static int DEFAULT_UNEXSEP_X = 2;
		public static int DEFAULT_UNEXSEP_Y = 4;
		
		protected internal Map map;
		
		public override void  setAttribute(System.String name, System.Object value_Renamed)
		{
			if (EXSEP_X.Equals(name))
			{
				if (value_Renamed is System.String)
				{
					try
					{
						exSepX = System.Int32.Parse((System.String) value_Renamed);
					}
					catch (System.FormatException NaN)
					{
						exSepX = DEFAULT_EXSEP_X;
						ErrorDialog.dataError(new BadDataReport(Resources.getString("Error.bad_preference", EXSEP_X, "StackMetrics"), (System.String) value_Renamed, NaN));
					}
				}
				else if (value_Renamed != null)
				{
					exSepX = ((System.Int32) value_Renamed);
				}
			}
			else if (EXSEP_Y.Equals(name))
			{
				if (value_Renamed is System.String)
				{
					try
					{
						exSepY = System.Int32.Parse((System.String) value_Renamed);
					}
					catch (System.FormatException NaN)
					{
						exSepY = DEFAULT_EXSEP_Y;
						ErrorDialog.dataError(new BadDataReport(Resources.getString("Error.bad_preference", EXSEP_Y, "StackMetrics"), (System.String) value_Renamed, NaN));
					}
				}
				else if (value_Renamed != null)
				{
					exSepY = ((System.Int32) value_Renamed);
				}
			}
			else if (UNEXSEP_X.Equals(name))
			{
				if (value_Renamed is System.String)
				{
					try
					{
						unexSepX = System.Int32.Parse((System.String) value_Renamed);
					}
					catch (System.FormatException NaN)
					{
						unexSepX = DEFAULT_UNEXSEP_X;
						ErrorDialog.dataError(new BadDataReport(Resources.getString("Error.bad_preference", UNEXSEP_X, "StackMetrics"), (System.String) value_Renamed, NaN));
					}
				}
				else if (value_Renamed != null)
				{
					unexSepX = ((System.Int32) value_Renamed);
				}
			}
			else if (UNEXSEP_Y.Equals(name))
			{
				if (value_Renamed is System.String)
				{
					try
					{
						unexSepY = System.Int32.Parse((System.String) value_Renamed);
					}
					catch (System.FormatException NaN)
					{
						unexSepY = DEFAULT_UNEXSEP_Y;
						ErrorDialog.dataError(new BadDataReport(Resources.getString("Error.bad_preference", UNEXSEP_Y, "StackMetrics"), (System.String) value_Renamed, NaN));
					}
				}
				else if (value_Renamed != null)
				{
					unexSepY = ((System.Int32) value_Renamed);
				}
			}
			else if (DISABLED.Equals(name))
			{
				if (value_Renamed is System.String)
				{
					//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.lang.Boolean.valueOf' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
					value_Renamed = System.Boolean.Parse((System.String) value_Renamed);
				}
				disabled = ((System.Boolean) value_Renamed);
			}
			else if (TOP_KEY.Equals(name))
			{
				topKey = HotKeyConfigurer.decode((System.String) value_Renamed);
			}
			else if (BOTTOM_KEY.Equals(name))
			{
				bottomKey = HotKeyConfigurer.decode((System.String) value_Renamed);
			}
			else if (UP_KEY.Equals(name))
			{
				upKey = HotKeyConfigurer.decode((System.String) value_Renamed);
			}
			else if (DOWN_KEY.Equals(name))
			{
				downKey = HotKeyConfigurer.decode((System.String) value_Renamed);
			}
			else if (COLOR.Equals(name))
			{
				if (value_Renamed is System.String)
				{
					value_Renamed = ColorConfigurer.stringToColor((System.String) value_Renamed);
				}
				blankColor = (System.Drawing.Color) value_Renamed;
			}
		}
		
		public override System.String getAttributeValueString(System.String name)
		{
			if (EXSEP_X.Equals(name))
			{
				return System.Convert.ToString(exSepX);
			}
			else if (EXSEP_Y.Equals(name))
			{
				return System.Convert.ToString(exSepY);
			}
			else if (UNEXSEP_X.Equals(name))
			{
				return System.Convert.ToString(unexSepX);
			}
			else if (UNEXSEP_Y.Equals(name))
			{
				return System.Convert.ToString(unexSepY);
			}
			else if (DISABLED.Equals(name))
			{
				return System.Convert.ToString(disabled);
			}
			else if (TOP_KEY.Equals(name))
			{
				return HotKeyConfigurer.encode(topKey);
			}
			else if (BOTTOM_KEY.Equals(name))
			{
				return HotKeyConfigurer.encode(bottomKey);
			}
			else if (UP_KEY.Equals(name))
			{
				return HotKeyConfigurer.encode(upKey);
			}
			else if (DOWN_KEY.Equals(name))
			{
				return HotKeyConfigurer.encode(downKey);
			}
			else if (COLOR.Equals(name))
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return blankColor.IsEmpty?null:ColorConfigurer.colorToString(ref blankColor);
			}
			return null;
		}
		
		public override void  addTo(Buildable b)
		{
			map = (Map) b;
			map.StackMetrics = this;
		}
		
		public StackMetrics():this(false, DEFAULT_EXSEP_X, DEFAULT_EXSEP_Y, DEFAULT_UNEXSEP_X, DEFAULT_UNEXSEP_Y)
		{
		}
		
		public StackMetrics(bool dis, int exSx, int exSy, int unexSx, int unexSy)
		{
			InitBlock();
			disabled = dis;
			exSepX = exSx;
			exSepY = exSy;
			unexSepX = unexSx;
			unexSepY = unexSy;
			
			unselectedVisible = new AnonymousClassPieceFilter(this);
			selectedVisible = new AnonymousClassPieceFilter1(this);
		}
		
		/// <summary> Different instances of StackMetrics may render a {@link Stack}
		/// in different ways.  The default algorithm is: If not expanded,
		/// all but the top visible GamePiece is drawn as a white square
		/// with size given by {@link GamePiece#getShape}.  The
		/// separation between GamePieces is given by {@link
		/// #relativePosition}
		/// 
		/// If expanded, all GamePieces are drawn with separation given by
		/// {@link #relativePosition}.  GamePiece that are selected are
		/// drawn in front of other GamePieces, even those above them in
		/// the stack.
		/// </summary>
		public virtual void  draw(Stack stack, System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			Highlighter highlighter = stack.getMap() == null?BasicPiece.Highlighter:stack.getMap().getHighlighter();
			System.Drawing.Point[] positions = new System.Drawing.Point[stack.PieceCount];
			getContents(stack, positions, null, null, x, y);
			
			for (PieceIterator e = new PieceIterator(stack.getPiecesIterator(), unselectedVisible); e.hasMoreElements(); )
			{
				GamePiece next = e.nextPiece();
				int index = stack.indexOf(next);
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				int nextX = x + (int) (zoom * (positions[index].X - x));
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				int nextY = y + (int) (zoom * (positions[index].Y - y));
				if (stack.isExpanded() || !e.hasMoreElements())
				{
					next.draw(g, nextX, nextY, obs, zoom);
				}
				else
				{
					drawUnexpanded(next, g, nextX, nextY, obs, zoom);
				}
			}
			
			for (PieceIterator e = new PieceIterator(stack.getPiecesIterator(), selectedVisible); e.hasMoreElements(); )
			{
				GamePiece next = e.nextPiece();
				int index = stack.indexOf(next);
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				int nextX = x + (int) (zoom * (positions[index].X - x));
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				int nextY = y + (int) (zoom * (positions[index].Y - y));
				next.draw(g, nextX, nextY, obs, zoom);
				highlighter.draw(next, g, nextX, nextY, obs, zoom);
			}
		}
		
		/// <summary> Draw only those pieces in the target stack whose boundingBoxes fall within the given visibleRect
		/// This method is considerably faster than the other draw method.
		/// </summary>
		/// <param name="stack">
		/// </param>
		/// <param name="g">
		/// </param>
		/// <param name="location">the location of the stack in component coordinates
		/// </param>
		/// <param name="zoom">
		/// </param>
		/// <param name="visibleRect">the visible rectangle in component coordinates
		/// </param>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual void  draw(Stack stack, ref System.Drawing.Point location, System.Drawing.Graphics g, Map map, double zoom, ref System.Drawing.Rectangle visibleRect)
		{
			Highlighter highlighter = map.getHighlighter();
			System.Drawing.Point mapLocation = map.mapCoordinates(location);
			System.Drawing.Rectangle region = visibleRect.IsEmpty?null:map.mapRectangle(visibleRect);
			System.Drawing.Point[] positions = new System.Drawing.Point[stack.PieceCount];
			System.Drawing.Rectangle[] bounds = region.IsEmpty?null:new System.Drawing.Rectangle[stack.PieceCount];
			getContents(stack, positions, null, bounds, mapLocation.X, mapLocation.Y);
			
			for (PieceIterator e = new PieceIterator(stack.getPiecesIterator(), unselectedVisible); e.hasMoreElements(); )
			{
				GamePiece next = e.nextPiece();
				int index = stack.indexOf(next);
				System.Drawing.Point pt = map.componentCoordinates(positions[index]);
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				if (bounds == null || isVisible(ref region, ref bounds[index]))
				{
					if (stack.isExpanded() || !e.hasMoreElements())
					{
						next.draw(g, pt.X, pt.Y, map.getView(), zoom);
					}
					else
					{
						drawUnexpanded(next, g, pt.X, pt.Y, map.getView(), zoom);
					}
				}
			}
			
			for (PieceIterator e = new PieceIterator(stack.getPiecesIterator(), selectedVisible); e.hasMoreElements(); )
			{
				GamePiece next = e.nextPiece();
				int index = stack.indexOf(next);
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				if (bounds == null || isVisible(ref region, ref bounds[index]))
				{
					System.Drawing.Point pt = map.componentCoordinates(positions[index]);
					next.draw(g, pt.X, pt.Y, map.getView(), zoom);
					highlighter.draw(next, g, pt.X, pt.Y, map.getView(), zoom);
				}
			}
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		private bool isVisible(ref System.Drawing.Rectangle region, ref System.Drawing.Rectangle bounds)
		{
			bool visible = true;
			if (!region.IsEmpty)
			{
				visible = region.IntersectsWith(bounds);
			}
			return visible;
		}
		
		/// <summary> Draw a {@link GamePiece} that is not the top piece in an unexpanded {@link Stack}
		/// 
		/// Default implementation is a white square with a black border
		/// </summary>
		protected internal virtual void  drawUnexpanded(GamePiece p, System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			if (blankColor.IsEmpty)
			{
				p.draw(g, x, y, obs, zoom);
			}
			else
			{
				System.Drawing.Graphics g2d = (System.Drawing.Graphics) g;
				SupportClass.GraphicsManager.manager.SetColor(g, blankColor);
				//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.Drawing.Drawing2D.GraphicsPath s = p.Shape;
				System.Drawing.Drawing2D.Matrix temp_Matrix;
				temp_Matrix = new System.Drawing.Drawing2D.Matrix();
				temp_Matrix.Scale((float) zoom, (float) zoom);
				System.Drawing.Drawing2D.Matrix t = temp_Matrix;
				t.Translate((System.Single) (x / zoom), (System.Single) (y / zoom));
				//UPGRADE_ISSUE: Method 'java.awt.geom.AffineTransform.createTransformedShape' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomAffineTransformcreateTransformedShape_javaawtShape'"
				s = t.createTransformedShape(s);
				//UPGRADE_TODO: Method 'java.awt.Graphics2D.fill' was converted to 'System.Drawing.Graphics.FillPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Dfill_javaawtShape'"
				g2d.FillPath(s);
				SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.Black);
				//UPGRADE_TODO: Method 'java.awt.Graphics2D.draw' was converted to 'System.Drawing.Graphics.DrawPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Ddraw_javaawtShape'"
				g2d.DrawPath(SupportClass.GraphicsManager.manager.GetPen(g2d), s);
			}
		}
		
		/// <summary> Fill the argument arrays with the positions, selection bounds and bounding boxes of the pieces in the argument stack</summary>
		/// <param name="parent">The parent Stack
		/// </param>
		/// <param name="positions">If non-null will contain a {@link Point} giving the position of each piece in <code>parent</code>
		/// </param>
		/// <param name="shapes">If non-null will contain a {@link Shape} giving the shape of for each piece in <code>parent</code>
		/// </param>
		/// <param name="boundingBoxes">If non-null will contain a {@link Rectangle} giving the bounding box for each piece in <code>parent</code>
		/// </param>
		/// <param name="x">the x-location of the parent
		/// </param>
		/// <param name="y">the y-location of the parent
		/// </param>
		/// <returns> the number of pieces processed in the stack
		/// </returns>
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public virtual int getContents(Stack parent, System.Drawing.Point[] positions, System.Drawing.Drawing2D.GraphicsPath[] shapes, System.Drawing.Rectangle[] boundingBoxes, int x, int y)
		{
			int count = parent.MaximumVisiblePieceCount;
			if (positions != null)
			{
				count = System.Math.Min(count, positions.Length);
			}
			if (boundingBoxes != null)
			{
				count = System.Math.Min(count, boundingBoxes.Length);
			}
			if (shapes != null)
			{
				count = System.Math.Min(count, shapes.Length);
			}
			int dx = parent.isExpanded()?exSepX:unexSepX;
			int dy = parent.isExpanded()?exSepY:unexSepY;
			System.Drawing.Point currentPos = System.Drawing.Point.Empty, nextPos = System.Drawing.Point.Empty;
			System.Drawing.Rectangle currentSelBounds = System.Drawing.Rectangle.Empty, nextSelBounds = System.Drawing.Rectangle.Empty;
			for (int index = 0; index < count; ++index)
			{
				GamePiece child = parent.getPieceAt(index);
				if (true.Equals(child.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME)))
				{
					System.Drawing.Rectangle blank = new System.Drawing.Rectangle(x, y, 0, 0);
					if (positions != null)
					{
						positions[index] = blank.Location;
					}
					if (boundingBoxes != null)
					{
						boundingBoxes[index] = blank;
					}
					if (shapes != null)
					{
						shapes[index] = blank;
					}
				}
				else
				{
					child.setProperty(VassalSharp.counters.Properties_Fields.USE_UNROTATED_SHAPE, (System.Object) true);
					nextSelBounds = System.Drawing.Rectangle.Truncate(child.Shape.GetBounds());
					child.setProperty(VassalSharp.counters.Properties_Fields.USE_UNROTATED_SHAPE, (System.Object) false);
					nextPos = new System.Drawing.Point(0, 0);
					if (currentPos.IsEmpty)
					{
						currentSelBounds = nextSelBounds;
						currentSelBounds.Offset(x, y);
						currentPos = new System.Drawing.Point(x, y);
						nextPos = currentPos;
					}
					else
					{
						//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
						nextPosition(ref currentPos, ref currentSelBounds, ref nextPos, ref nextSelBounds, dx, dy);
					}
					if (positions != null)
					{
						positions[index] = nextPos;
					}
					if (boundingBoxes != null)
					{
						System.Drawing.Rectangle bbox = child.boundingBox();
						bbox.Offset(nextPos.X, nextPos.Y);
						boundingBoxes[index] = bbox;
					}
					if (shapes != null)
					{
						//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
						System.Drawing.Drawing2D.GraphicsPath s = child.Shape;
						//UPGRADE_ISSUE: Method 'java.awt.geom.AffineTransform.createTransformedShape' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomAffineTransformcreateTransformedShape_javaawtShape'"
						System.Drawing.Drawing2D.Matrix temp_Matrix;
						temp_Matrix = new System.Drawing.Drawing2D.Matrix();
						temp_Matrix.Translate((float) nextPos.X, (float) nextPos.Y);
						s = temp_Matrix.createTransformedShape(s);
						shapes[index] = s;
					}
					currentPos = nextPos;
					currentSelBounds = nextSelBounds;
				}
			}
			return count;
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		protected internal virtual void  nextPosition(ref System.Drawing.Point currentPos, ref System.Drawing.Rectangle currentBounds, ref System.Drawing.Point nextPos, ref System.Drawing.Rectangle nextBounds, int dx, int dy)
		{
			int deltaX, deltaY;
			if (dx > 0)
			{
				deltaX = currentBounds.X + dx - nextBounds.X;
			}
			else if (dx < 0)
			{
				deltaX = currentBounds.X + currentBounds.Width - nextBounds.Width + dx - nextBounds.X;
			}
			else
			{
				deltaX = currentPos.X - nextPos.X;
			}
			if (dy > 0)
			{
				deltaY = currentBounds.Y + currentBounds.Height - nextBounds.Height - nextBounds.Y - dy;
			}
			else if (dy < 0)
			{
				deltaY = currentBounds.Y - dy - nextBounds.Y;
			}
			else
			{
				deltaY = currentPos.Y - nextPos.Y;
			}
			nextBounds.Offset(deltaX, deltaY);
			nextPos.Offset(deltaX, deltaY);
		}
		
		public virtual System.Drawing.Point relativePosition(Stack parent, GamePiece c)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'index '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int index = System.Math.Min(parent.indexOf(c), parent.MaximumVisiblePieceCount - 1);
			
			if (index < 0)
			{
				return new System.Drawing.Point(0, 0);
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'pos '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Point[] pos = new System.Drawing.Point[parent.MaximumVisiblePieceCount];
			getContents(parent, pos, null, null, 0, 0);
			return pos[index];
		}
		
		public override void  removeFrom(Buildable parent)
		{
		}
		
		public override System.String getConfigureName()
		{
			return null;
		}
		
		public override HelpFile getHelpFile()
		{
			return HelpFile.getReferenceManualPage("Map.htm", "StackingOptions");
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAllowableConfigureComponents()
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Integer.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Integer.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Integer.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Integer.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Color.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private VisibilityCondition cond = new VisibilityCondition()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean shouldBeVisible()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return !disabled;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public VisibilityCondition getAttributeVisibility(String name)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(name.equals(EXSEP_X) 
		|| name.equals(EXSEP_Y) 
		|| name.equals(UNEXSEP_X) 
		|| name.equals(UNEXSEP_Y) 
		|| name.equals(COLOR))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return cond;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Stack createStack(GamePiece p)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return createStack(p, false);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Stack createStack(GamePiece p, boolean force)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return isStackingEnabled() || force ? new Stack(p): null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public KeyStroke getMoveUpKey()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return upKey;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public KeyStroke getMoveDownKey()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return downKey;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public KeyStroke getMoveTopKey()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return topKey;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public KeyStroke getMoveBottomKey()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return bottomKey;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Merge the two pieces if stacking is enabled.
	/// If stacking is disabled, place the moving piece at the same location as the fixed piece
	/// </summary>
	/// <param name="fixed">
	/// </param>
	/// <param name="moving">
	/// </param>
	/// <returns> a Command that accomplishes this task
	/// </returns>
	/// <seealso cref="merge">
	/// </seealso>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Command placeOrMerge(GamePiece fixed, GamePiece moving)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(disabled)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return fixed.getMap().placeAt(moving, fixed.getPosition());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return merge(fixed, moving);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Place a GamePiece on top of another GamePiece
	/// Create/remove stacks as necessary, even if stacking is disabled for this instance
	/// </summary>
	/// <param name="moving">the GamePiece that will be merged into the stack
	/// </param>
	/// <param name="fixed">the GamePiece defining the location and contents of the existing stack
	/// </param>
	/// <returns> a Command that accomplishes this task
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Command merge(GamePiece fixed, GamePiece moving)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		Command comm;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(fixed instanceof Stack &&((Stack) fixed).topPiece() != null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		comm = merge(((Stack) fixed).topPiece(), moving);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final MoveTracker tracker = new MoveTracker(moving);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	comm = new NullCommand();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Stack fixedParent = fixed.getParent();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int index = fixedParent == null ? 0: fixedParent.indexOf(fixed) + 1;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(moving != fixed && moving != fixedParent)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final GameState gs = GameModule.getGameModule().getGameState();
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final boolean isNewPiece = gs.getPieceForId(moving.getId()) == null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(fixedParent == null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(fixed instanceof Stack)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		fixedParent =(Stack) fixed;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	index = fixedParent.getPieceCount();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		fixedParent = createStack(fixed, true);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	comm = comm.append(fixedParent.getMap().placeAt(
	fixedParent, fixedParent.getPosition()));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	index = 1;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	if(isNewPiece)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		gs.addPiece(moving);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	comm = comm.append(new AddPiece(moving));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	if(moving instanceof Stack)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		for(Iterator < GamePiece > i =((Stack) moving).getPiecesIterator();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i.hasNext();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final GamePiece p = i.next();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final MoveTracker t = new MoveTracker(p);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	fixedParent.insertChild(p, index ++);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	comm = comm.append(t.getMoveCommand());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(moving.getParent() == fixedParent && fixedParent != null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		index --;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	fixedParent.insert(moving, index);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	comm = comm.append(tracker.getMoveCommand());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return comm;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}