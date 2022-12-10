/*
* $Id$
*
* Copyright (c) 2000-2012 by Rodney Kinney, Joel Uckelman, Brent Easton
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
//UPGRADE_TODO: The type 'org.apache.commons.lang.builder.HashCodeBuilder' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using HashCodeBuilder = org.apache.commons.lang.builder.HashCodeBuilder;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using ChangeTracker = VassalSharp.command.ChangeTracker;
using Command = VassalSharp.command.Command;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using ColorConfigurer = VassalSharp.configure.ColorConfigurer;
using FormattedStringConfigurer = VassalSharp.configure.FormattedStringConfigurer;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using FormattedString = VassalSharp.tools.FormattedString;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using RecursionLimitException = VassalSharp.tools.RecursionLimitException;
using RecursionLimiter = VassalSharp.tools.RecursionLimiter;
using Loopable = VassalSharp.tools.RecursionLimiter.Loopable;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
using ImageUtils = VassalSharp.tools.image.ImageUtils;
using AbstractTileOpImpl = VassalSharp.tools.imageop.AbstractTileOpImpl;
using ScaledImagePainter = VassalSharp.tools.imageop.ScaledImagePainter;
namespace VassalSharp.counters
{
	
	/// <summary> Displays a text label, with content specified by the user at runtime.</summary>
	public class Labeler:Decorator, TranslatablePiece, RecursionLimiter.Loopable
	{
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < String > l = new ArrayList < String >();
			if (propertyName.Length > 0)
			{
				l.add(propertyName);
			}
			return l;
		}
		override public System.String LocalizedName
		{
			get
			{
				if (label.Length == 0)
				{
					return piece.LocalizedName;
				}
				else
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					FormattedString f = new FormattedString(getTranslation(nameFormat.Format));
					f.setProperty(PIECE_NAME, piece.LocalizedName);
					f.setProperty(LABEL, LocalizedLabel);
					return f.getLocalizedText(Decorator.getOutermost(this));
				}
			}
			
		}
		/// <summary> Return the relative position of the upper-left corner of the label,
		/// for a piece at position (0,0). Cache the position of the label once the label
		/// image has been generated.
		/// </summary>
		private System.Drawing.Point LabelPosition
		{
			get
			{
				if (!position.IsEmpty)
				{
					return position;
				}
				int x = horizontalOffset;
				int y = verticalOffset;
				
				updateCachedImage(); // ensure that the LabelOp is set
				//UPGRADE_NOTE: Final was removed from the declaration of 'lblSize '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Size lblSize = imagePainter.ImageSize;
				//UPGRADE_NOTE: Final was removed from the declaration of 'selBnds '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Rectangle selBnds = System.Drawing.Rectangle.Truncate(piece.Shape.GetBounds());
				
				switch (verticalPos)
				{
					
					case 't': 
						y += selBnds.Y;
						break;
					
					case 'b': 
						y += selBnds.Y + selBnds.Height;
						break;
					}
				
				switch (horizontalPos)
				{
					
					case 'l': 
						x += selBnds.X;
						break;
					
					case 'r': 
						x += selBnds.X + selBnds.Width;
						break;
					}
				
				switch (verticalJust)
				{
					
					case 'b': 
						y -= lblSize.Height;
						break;
					
					case 'c': 
						y -= lblSize.Height / 2;
						break;
					}
				
				switch (horizontalJust)
				{
					
					case 'c': 
						x -= lblSize.Width / 2;
						break;
					
					case 'r': 
						x -= lblSize.Width;
						break;
					}
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'result '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point result = new System.Drawing.Point(x, y);
				
				// Cache the position once the label image has been generated
				if (lblSize.Height > 0 && lblSize.Width > 0)
				{
					position = result;
				}
				
				return result;
			}
			
		}
		virtual public System.String Label
		{
			get
			{
				return labelFormat.getText(Decorator.getOutermost(this));
			}
			
			set
			{
				if (value == null)
					value = "";
				
				position = System.Drawing.Point.Empty; // clear position cache
				
				int index = value.IndexOf("$" + propertyName + "$");
				while (index >= 0)
				{
					value = value.Substring(0, (index) - (0)) + value.Substring(index + propertyName.Length + 2);
					index = value.IndexOf("$" + propertyName + "$");
				}
				label = value;
				// prevent recursive references from this label
				// to piece name (which may contain this label)
				labelFormat.setProperty(BasicPiece.PIECE_NAME, piece.getName());
				labelFormat.Format = label;
				
				if (getMap() != null && label != null && label.Length > 0)
				{
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					imagePainter.setSource(new LabelOp(LocalizedLabel, font, ref textFg, ref textBg));
				}
				else
				{
					imagePainter.Source = null;
				}
			}
			
		}
		virtual public System.Drawing.Color Background
		{
			set
			{
				this.textBg = value;
			}
			
		}
		virtual public System.Drawing.Color Foreground
		{
			set
			{
				this.textFg = value;
			}
			
		}
		virtual public System.String LocalizedLabel
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				FormattedString f = new FormattedString(getTranslation(labelFormat.Format));
				return f.getLocalizedText(Decorator.getOutermost(this));
			}
			
		}
		/// <summary> Return the Shape of the counter by adding the shape of this label to the shape of all inner traits.
		/// Minimize generation of new Area objects.
		/// </summary>
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		override public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.Drawing.Drawing2D.GraphicsPath innerShape = piece.Shape;
				
				// If the label has a Control key, then the image of the label is NOT included in the selectable area of the
				// counter
				if (!labelKey.Null)
				{
					return innerShape;
				}
				else
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Rectangle r = new System.Drawing.Rectangle(LabelPosition, imagePainter.ImageSize);
					
					// If the label is completely enclosed in the current counter shape, then we can just return
					// the current shape
					//UPGRADE_ISSUE: Method 'java.awt.Shape.contains' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtShapecontains_double_double_double_double'"
					if (innerShape.contains(r.X, r.Y, r.Width, r.Height))
					{
						return innerShape;
					}
					else
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'a '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.Drawing.Region a = new System.Drawing.Region(innerShape);
						
						// Cache the Area object generated. Only recreate if the label position or size has changed
						if (!r.Equals(lastRect))
						{
							lastShape = new System.Drawing.Region(r);
							System.Drawing.Rectangle temp_Rectangle;
							temp_Rectangle = r;
							lastRect = new System.Drawing.Rectangle(temp_Rectangle.Location, temp_Rectangle.Size);
						}
						//UPGRADE_TODO: Method 'java.awt.geom.Area.add' was converted to 'System.Drawing.Region.Union' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtgeomAreaadd_javaawtgeomArea'"
						a.Union(lastShape);
						return a;
					}
				}
			}
			
		}
		virtual public System.String Description
		{
			get
			{
				return "Text Label" + (description.Length > 0?(" - " + description):"");
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("Label.htm");
			}
			
		}
		virtual public System.String ComponentTypeName
		{
			get
			{
				// Use inner name to prevent recursive looping when reporting errors.
				return piece.getName();
			}
			
		}
		virtual public System.String ComponentName
		{
			get
			{
				return Description;
			}
			
		}
		public const System.String ID = "label;";
		protected internal System.Drawing.Color textBg = System.Drawing.Color.Black;
		protected internal System.Drawing.Color textFg = System.Drawing.Color.White;
		
		public const int CENTER = 0;
		public const int RIGHT = 1;
		public const int LEFT = 2;
		public const int TOP = 3;
		public const int BOTTOM = 4;
		
		public static int HORIZONTAL_ALIGNMENT = CENTER;
		public static int VERTICAL_ALIGNMENT = TOP;
		
		private System.String label = "";
		private System.String lastCachedLabel;
		private NamedKeyStroke labelKey;
		private System.String menuCommand = "Change Label";
		//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
		private System.Drawing.Font font = new System.Drawing.Font("Dialog", 10, System.Drawing.FontStyle.Regular);
		private KeyCommand[] commands;
		private FormattedString nameFormat = new FormattedString("$" + PIECE_NAME + "$ ($" + LABEL + "$)");
		private FormattedString labelFormat = new FormattedString("");
		private const System.String PIECE_NAME = "pieceName";
		private const System.String BAD_PIECE_NAME = "PieceName";
		private const System.String LABEL = "label";
		
		protected internal ScaledImagePainter imagePainter = new ScaledImagePainter();
		
		private char verticalJust = 'b';
		private char horizontalJust = 'c';
		private char verticalPos = 't';
		private char horizontalPos = 'c';
		private int verticalOffset = 0;
		private int horizontalOffset = 0;
		protected internal int rotateDegrees;
		protected internal System.String propertyName;
		protected internal KeyCommand menuKeyCommand;
		protected internal System.String description;
		
		private System.Drawing.Point position = System.Drawing.Point.Empty; // Label position cache
		
		public Labeler():this(ID, null)
		{
		}
		
		public Labeler(System.String s, GamePiece d)
		{
			InitBlock();
			mySetType(s);
			setInner(d);
		}
		
		public virtual void  mySetType(System.String type)
		{
			commands = null;
			//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			st.nextToken();
			labelKey = st.nextNamedKeyStroke(null);
			menuCommand = st.nextToken("Change Label");
			//UPGRADE_NOTE: Final was removed from the declaration of 'fontSize '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int fontSize = st.nextInt(10);
			System.Drawing.Color tempAux = System.Drawing.Color.Empty;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			textBg = st.nextColor(ref tempAux);
			System.Drawing.Color tempAux2 = System.Drawing.Color.Black;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			textFg = st.nextColor(ref tempAux2);
			verticalPos = st.nextChar('t');
			verticalOffset = st.nextInt(0);
			horizontalPos = st.nextChar('c');
			horizontalOffset = st.nextInt(0);
			verticalJust = st.nextChar('b');
			horizontalJust = st.nextChar('c');
			nameFormat.Format = clean(st.nextToken("$" + PIECE_NAME + "$ ($" + LABEL + "$)"));
			//UPGRADE_NOTE: Final was removed from the declaration of 'fontFamily '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String fontFamily = st.nextToken("Dialog");
			//UPGRADE_NOTE: Final was removed from the declaration of 'fontStyle '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Field 'java.awt.Font.PLAIN' was converted to 'System.Drawing.FontStyle.Regular' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFontPLAIN_f'"
			int fontStyle = st.nextInt((int) System.Drawing.FontStyle.Regular);
			//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
			font = new System.Drawing.Font(fontFamily, fontSize, (System.Drawing.FontStyle) fontStyle);
			rotateDegrees = st.nextInt(0);
			propertyName = st.nextToken("TextLabel");
			description = st.nextToken("");
		}
		
		/*
		* Clean up any property names that will cause an infinite loop when used in a label name
		*/
		protected internal virtual System.String clean(System.String s)
		{
			// Cannot use $PieceName$ in a label format, must use $pieceName$
			return s.replaceAll("$" + BAD_PIECE_NAME + "$", "$" + PIECE_NAME + "$");
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.Object getLocalizedProperty(System.Object key)
		{
			if (key.Equals(propertyName))
			{
				return LocalizedLabel;
			}
			else if (VassalSharp.counters.Properties_Fields.VISIBLE_STATE.Equals(key))
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return LocalizedLabel + piece.getProperty(key);
			}
			else
			{
				return base.getLocalizedProperty(key);
			}
		}
		
		public override System.Object getProperty(System.Object key)
		{
			if (key.Equals(propertyName))
			{
				return Label;
			}
			else if (VassalSharp.counters.Properties_Fields.VISIBLE_STATE.Equals(key))
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return Label + piece.getProperty(key);
			}
			else
			{
				return base.getProperty(key);
			}
		}
		
		public override System.String myGetType()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(labelKey).append(menuCommand).append((int) font.Size);
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			System.String s = ColorConfigurer.colorToString(ref textBg);
			se.append(s == null?"":s);
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			s = ColorConfigurer.colorToString(ref textFg);
			se.append(s == null?"":s).append(System.Convert.ToString(verticalPos)).append(System.Convert.ToString(verticalOffset)).append(System.Convert.ToString(horizontalPos)).append(System.Convert.ToString(horizontalOffset)).append(System.Convert.ToString(verticalJust)).append(System.Convert.ToString(horizontalJust)).append(nameFormat.Format).append(font.FontFamily.Name).append((int) font.Style).append(System.Convert.ToString(rotateDegrees)).append(propertyName).append(description);
			return ID + se.Value;
		}
		
		public override System.String myGetState()
		{
			return label;
		}
		
		public override void  mySetState(System.String s)
		{
			Label = s.Trim();
		}
		
		public override System.String getName()
		{
			System.String result = "";
			if (label.Length == 0)
			{
				result = piece.getName();
			}
			else
			{
				nameFormat.setProperty(PIECE_NAME, piece.getName());
				//
				// Bug 9483
				// Don't evaluate the label while reporting an infinite loop
				// Can cause further looping so that the infinite loop report 
				// never finishes before a StackOverflow occurs
				//
				if (!RecursionLimiter.ReportingInfiniteLoop)
				{
					nameFormat.setProperty(LABEL, Label);
				}
				try
				{
					RecursionLimiter.startExecution(this);
					result = nameFormat.getText(Decorator.getOutermost(this));
				}
				catch (RecursionLimitException e)
				{
					SupportClass.WriteStackTrace(e, Console.Error);
				}
				finally
				{
					RecursionLimiter.endExecution();
				}
			}
			return result;
		}
		
		
		// FIXME: This doesn't belong here. Should be in ImageUtils instead?
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public static void  drawLabel(System.Drawing.Graphics g, System.String text, int x, int y, int hAlign, int vAlign, ref System.Drawing.Color fgColor, ref System.Drawing.Color bgColor)
		{
			//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
			//UPGRADE_TODO: Field 'java.awt.Font.PLAIN' was converted to 'System.Drawing.FontStyle.Regular' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFontPLAIN_f'"
			System.Drawing.Color tempAux = System.Drawing.Color.Empty;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			drawLabel(g, text, x, y, new System.Drawing.Font("Dialog", 10, System.Drawing.FontStyle.Regular), hAlign, vAlign, ref fgColor, ref bgColor, ref tempAux);
		}
		
		// FIXME: This doesn't belong here. Should be in ImageUtils instead?
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public static void  drawLabel(System.Drawing.Graphics g, System.String text, int x, int y, System.Drawing.Font f, int hAlign, int vAlign, ref System.Drawing.Color fgColor, ref System.Drawing.Color bgColor, ref System.Drawing.Color borderColor)
		{
			SupportClass.GraphicsManager.manager.SetFont(g, f);
			//UPGRADE_NOTE: Final was removed from the declaration of 'width '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'java.awt.FontMetrics.stringWidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtFontMetricsstringWidth_javalangString'"
			int width = SupportClass.GraphicsManager.manager.GetFont(g).stringWidth(text + "  ");
			//UPGRADE_NOTE: Final was removed from the declaration of 'height '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.FontMetrics.getHeight' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			int height = (int) SupportClass.GraphicsManager.manager.GetFont(g).GetHeight();
			int x0 = x;
			int y0 = y;
			switch (hAlign)
			{
				
				case CENTER: 
					x0 = x - width / 2;
					break;
				
				case LEFT: 
					x0 = x - width;
					break;
				}
			switch (vAlign)
			{
				
				case CENTER: 
					y0 = y - height / 2;
					break;
				
				case BOTTOM: 
					y0 = y - height;
					break;
				}
			if (!bgColor.IsEmpty)
			{
				SupportClass.GraphicsManager.manager.SetColor(g, bgColor);
				g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), x0, y0, width, height);
			}
			if (!borderColor.IsEmpty)
			{
				SupportClass.GraphicsManager.manager.SetColor(g, borderColor);
				g.DrawRectangle(SupportClass.GraphicsManager.manager.GetPen(g), x0, y0, width, height);
			}
			SupportClass.GraphicsManager.manager.SetColor(g, fgColor);
			//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setRenderingHint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetRenderingHint_javaawtRenderingHintsKey_javalangObject'"
			//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_ANTIALIASING' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_ANTIALIASING_f'"
			//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.VALUE_ANTIALIAS_ON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsVALUE_ANTIALIAS_ON_f'"
			((System.Drawing.Graphics) g).setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
			//UPGRADE_TODO: Method 'java.awt.Graphics.drawString' was converted to 'System.Drawing.Graphics.DrawString' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphicsdrawString_javalangString_int_int'"
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.FontMetrics.getHeight' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.FontMetrics.getDescent' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			g.DrawString(" " + text + " ", SupportClass.GraphicsManager.manager.GetFont(g), SupportClass.GraphicsManager.manager.GetBrush(g), x0, y0 + (int) SupportClass.GraphicsManager.manager.GetFont(g).GetHeight() - SupportClass.GetDescent(SupportClass.GraphicsManager.manager.GetFont(g)) - SupportClass.GraphicsManager.manager.GetFont(g).GetHeight());
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			updateCachedImage();
			piece.draw(g, x, y, obs, zoom);
			
			// FIXME: We should be drawing the text at the right size, not scaling it!
			//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Point p = LabelPosition;
			//UPGRADE_NOTE: Final was removed from the declaration of 'labelX '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int labelX = x + (int) (zoom * p.X);
			//UPGRADE_NOTE: Final was removed from the declaration of 'labelY '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int labelY = y + (int) (zoom * p.Y);
			
			System.Drawing.Drawing2D.Matrix saveXForm = null;
			//UPGRADE_NOTE: Final was removed from the declaration of 'g2d '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Graphics g2d = (System.Drawing.Graphics) g;
			
			if (rotateDegrees != 0)
			{
				saveXForm = g2d.Transform;
				//UPGRADE_NOTE: Final was removed from the declaration of 'newXForm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Drawing2D.Matrix temp_Matrix;
				temp_Matrix = new System.Drawing.Drawing2D.Matrix();
				temp_Matrix.RotateAt((float) (SupportClass.DegreesToRadians(rotateDegrees) * (180 / System.Math.PI)), new System.Drawing.PointF((float) x, (float) y));
				System.Drawing.Drawing2D.Matrix newXForm = temp_Matrix;
				//UPGRADE_TODO: Method 'java.awt.Graphics2D.transform' was converted to 'System.Drawing.Graphics.Transform' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Dtransform_javaawtgeomAffineTransform'"
				g2d.Transform = newXForm;
			}
			
			imagePainter.draw(g, labelX, labelY, zoom, obs);
			
			if (rotateDegrees != 0)
			{
				g2d.Transform = saveXForm;
			}
		}
		
		protected internal virtual void  updateCachedImage()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'label '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String label = LocalizedLabel;
			if (label != null && !label.Equals(lastCachedLabel))
			{
				imagePainter.Source = null;
				lastCachedLabel = null;
				position = System.Drawing.Point.Empty;
			}
			
			if (imagePainter.Source == null && label != null && label.Length > 0)
			{
				lastCachedLabel = label;
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				imagePainter.setSource(new LabelOp(lastCachedLabel, font, ref textFg, ref textBg));
			}
		}
		
		protected internal class LabelOp:AbstractTileOpImpl
		{
			private void  InitBlock()
			{
				return Collections.emptyList();
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'txt '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private System.String txt;
			//UPGRADE_NOTE: Final was removed from the declaration of 'font '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private System.Drawing.Font font;
			//UPGRADE_NOTE: Final was removed from the declaration of 'fg '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private System.Drawing.Color fg;
			//UPGRADE_NOTE: Final was removed from the declaration of 'bg '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private System.Drawing.Color bg;
			//UPGRADE_NOTE: Final was removed from the declaration of 'hash '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private int hash;
			
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			public LabelOp(System.String txt, System.Drawing.Font font, ref System.Drawing.Color fg, ref System.Drawing.Color bg)
			{
				this.txt = txt;
				this.font = font;
				this.fg = fg;
				this.bg = bg;
				hash = new HashCodeBuilder().append(txt).append(font).append(fg).append(bg).toHashCode();
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public List < VassalSharp.tools.opcache.Op < ? >> getSources()
			
			public override System.Drawing.Bitmap eval()
			{
				// determine whether we are HTML and fix our size
				//UPGRADE_NOTE: Final was removed from the declaration of 'label '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Label label = buildDimensions();
				
				// draw nothing if our size is zero
				if (size.Width <= 0 || size.Height <= 0)
					return ImageUtils.NULL_IMAGE;
				
				// prepare the target image
				//UPGRADE_NOTE: Final was removed from the declaration of 'im '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Method 'java.awt.Color.getTransparency' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtColorgetTransparency'"
				//UPGRADE_ISSUE: Field 'java.awt.Transparency.OPAQUE' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtTransparency'"
				System.Drawing.Bitmap im = ImageUtils.createCompatibleImage(size.Width, size.Height, bg.IsEmpty || bg.getTransparency() != Color.OPAQUE);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(im);
				//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setRenderingHint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetRenderingHint_javaawtRenderingHintsKey_javalangObject'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_ANTIALIASING' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_ANTIALIASING_f'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.VALUE_ANTIALIAS_ON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsVALUE_ANTIALIAS_ON_f'"
				g.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
				
				// paint the background
				if (!bg.IsEmpty)
				{
					SupportClass.GraphicsManager.manager.SetColor(g, bg);
					g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), 0, 0, size.Width, size.Height);
				}
				
				// paint the foreground
				if (!fg.IsEmpty)
				{
					if (label != null)
					{
						if ((label is VassalSharp.tools.icon.IconFamily.IconImageConfigurer.AnonymousClassJPanel || label is VassalSharp.counters.PropertySheet.TickLabel || label is VassalSharp.counters.Obscurable.Ed.AnonymousClassJPanel || label is VassalSharp.counters.NonRectangular.Ed.AnonymousClassJPanel || label is VassalSharp.configure.IconConfigurer.AnonymousClassJPanel || label is VassalSharp.build.widget.PieceSlot.Panel || label is VassalSharp.build.module.map.View || label is VassalSharp.build.module.map.boardPicker.board.Config.View || label is VassalSharp.build.module.map.boardPicker.board.mapgrid.SquareGridNumbering.AnonymousClassJPanel || label is VassalSharp.build.module.map.boardPicker.board.mapgrid.PolygonEditor || label is VassalSharp.build.module.map.boardPicker.board.mapgrid.HexGridNumbering.TestPanel.AnonymousClassJPanel1 || label is VassalSharp.build.module.map.boardPicker.board.mapgrid.HexGridNumbering.AnonymousClassJPanel || label is VassalSharp.build.module.map.boardPicker.board.GridEditor.GridPanel || label is VassalSharp.build.module.gamepieceimage.Visualizer.AnonymousClassJPanel || label is VassalSharp.build.module.gamepieceimage.NewColorConfigurer.Panel))
							SupportClass.InvokeMethodAsVirtual(label, "paint", new System.Object[]{g});
						else
							label.OnPaint(new System.Windows.Forms.PaintEventArgs(g, label.Bounds));
					}
					else
					{
						SupportClass.GraphicsManager.manager.SetColor(g, fg);
						SupportClass.GraphicsManager.manager.SetFont(g, font);
						
						//UPGRADE_NOTE: Final was removed from the declaration of 'fm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.Drawing.Font fm = font;
						//UPGRADE_TODO: Method 'java.awt.Graphics2D.drawString' was converted to 'System.Drawing.Graphics.DrawString' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DdrawString_javalangString_int_int'"
						//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.FontMetrics.getDescent' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						g.DrawString(txt, SupportClass.GraphicsManager.manager.GetFont(g), SupportClass.GraphicsManager.manager.GetPaint(g), 0, size.Height - SupportClass.GetDescent(fm) - SupportClass.GraphicsManager.manager.GetFont(g).GetHeight());
					}
				}
				
				g.Dispose();
				return im;
			}
			
			private System.Windows.Forms.Label buildDimensions()
			{
				// Build and return a JLabel if we need to render HTML,
				// then determine the dimensions of the label.
				//UPGRADE_NOTE: Final was removed from the declaration of 'label '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Label label;
				
				//UPGRADE_TODO: Method 'javax.swing.plaf.basic.BasicHTML.isHTMLString' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				if (BasicHTML.isHTMLString(txt))
				{
					System.Windows.Forms.Label temp_label;
					temp_label = new System.Windows.Forms.Label();
					temp_label.Text = txt;
					label = temp_label;
					label.ForeColor = fg;
					label.Font = font;
					//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					size = label.Size;
					//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_javaawtDimension'"
					label.Size = size;
				}
				else
				{
					label = null;
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(ImageUtils.NULL_IMAGE);
					//UPGRADE_NOTE: Final was removed from the declaration of 'fm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Font fm = font;
					//UPGRADE_ISSUE: Method 'java.awt.FontMetrics.stringWidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtFontMetricsstringWidth_javalangString'"
					//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.FontMetrics.getHeight' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					size = new System.Drawing.Size(fm.stringWidth(txt), (int) fm.GetHeight());
					g.Dispose();
				}
				
				return label;
			}
			
			protected internal override void  fixSize()
			{
				if ((size = SizeFromCache).IsEmpty)
				{
					buildDimensions();
					
					// ensure that our area is nonempty
					if (size.Width <= 0 || size.Height <= 0)
					{
						size.Width = size.Height = 1;
					}
				}
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public  override bool Equals(System.Object o)
			{
				if (this == o)
					return true;
				if (!(o is LabelOp))
					return false;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'lop '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				LabelOp lop = (LabelOp) o;
				return (txt == null?lop.txt == null:txt.Equals(lop.txt)) && (font == null?lop.font == null:font.Equals(lop.font)) && (fg.IsEmpty?lop.fg.IsEmpty:fg.Equals(lop.fg)) && (bg.IsEmpty?lop.bg.IsEmpty:bg.Equals(lop.bg));
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public override int GetHashCode()
			{
				return hash;
			}
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Rectangle r = piece.boundingBox();
			SupportClass.RectangleSupport.AddRectangleToRectangle(ref r, new System.Drawing.Rectangle(LabelPosition, imagePainter.ImageSize));
			return r;
		}
		
		protected internal System.Drawing.Rectangle lastRect = System.Drawing.Rectangle.Empty;
		protected internal System.Drawing.Region lastShape = null;
		
		public override KeyCommand[] myGetKeyCommands()
		{
			if (commands == null)
			{
				menuKeyCommand = new KeyCommand(menuCommand, labelKey, Decorator.getOutermost(this), this);
				if (labelKey == null || labelKey.Null || menuCommand == null || menuCommand.Length == 0)
				{
					commands = new KeyCommand[0];
				}
				else
				{
					commands = new KeyCommand[]{menuKeyCommand};
				}
			}
			return commands;
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			myGetKeyCommands();
			Command c = null;
			if (menuKeyCommand.matches(stroke))
			{
				ChangeTracker tracker = new ChangeTracker(this);
				//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String s = (System.String) JOptionPane.showInputDialog(getMap() == null?null:getMap().getView().getTopLevelAncestor(), menuKeyCommand.Name, null, (int) System.Windows.Forms.MessageBoxIcon.Question, null, null, label);
				if (s == null)
				{
					tracker = null;
				}
				else
				{
					Label = s;
					c = tracker.ChangeCommand;
				}
			}
			return c;
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		/// <summary> Return Property names exposed by this trait</summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getPropertyNames()
		
		private class Ed : PieceEditor
		{
			virtual public System.String State
			{
				get
				{
					return initialValue.ValueString;
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					SequenceEncoder se = new SequenceEncoder(';');
					se.append(labelKeyInput.ValueString).append(command.ValueString);
					
					System.Int32 i = (System.Int32) fontSize.getValue();
					//UPGRADE_TODO: The 'System.Int32' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
					if (i == null || i <= 0)
					{
						i = 10;
					}
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					se.append(i.ToString()).append(bg.ValueString).append(fg.ValueString).append(vPos.SelectedItem.ToString());
					i = (System.Int32) vOff.getValue();
					//UPGRADE_TODO: The 'System.Int32' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
					if (i == null)
						i = 0;
					
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					se.append(i.ToString()).append(hPos.SelectedItem.ToString());
					i = (System.Int32) hOff.getValue();
					//UPGRADE_TODO: The 'System.Int32' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
					if (i == null)
						i = 0;
					
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					se.append(i.ToString()).append(vJust.SelectedItem.ToString()).append(hJust.SelectedItem.ToString()).append(format.ValueString).append(fontFamily.SelectedItem.ToString());
					//UPGRADE_NOTE: Final was removed from the declaration of 'style '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_TODO: Field 'java.awt.Font.PLAIN' was converted to 'System.Drawing.FontStyle.Regular' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFontPLAIN_f'"
					int style = (int) System.Drawing.FontStyle.Regular + (bold.booleanValue()?(int) System.Drawing.FontStyle.Bold:0) + (italic.booleanValue()?(int) System.Drawing.FontStyle.Italic:0);
					se.append(style + "");
					i = (System.Int32) rotate.getValue();
					//UPGRADE_TODO: The 'System.Int32' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
					if (i == null)
						i = 0;
					
					se.append(i.ToString()).append(propertyNameConfig.ValueString).append(descConfig.ValueString);
					
					return VassalSharp.counters.Labeler.ID + se.Value;
				}
				
			}
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return controls;
				}
				
			}
			private NamedHotKeyConfigurer labelKeyInput;
			private System.Windows.Forms.Panel controls = new System.Windows.Forms.Panel();
			private StringConfigurer command;
			private StringConfigurer initialValue;
			private ColorConfigurer fg, bg;
			private System.Windows.Forms.ComboBox hPos, vPos, hJust, vJust;
			private IntConfigurer hOff, vOff, fontSize;
			//UPGRADE_ISSUE: Interface 'javax.swing.ListCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingListCellRenderer'"
			private ListCellRenderer renderer;
			private FormattedStringConfigurer format;
			private System.Windows.Forms.ComboBox fontFamily;
			private IntConfigurer rotate;
			private BooleanConfigurer bold, italic;
			private StringConfigurer propertyNameConfig;
			private StringConfigurer descConfig;
			
			public Ed(Labeler l)
			{
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				controls.setLayout(new BoxLayout(controls, BoxLayout.Y_AXIS));
				
				descConfig = new StringConfigurer(null, "Description:  ", l.description);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(descConfig.Controls);
				
				initialValue = new StringConfigurer(null, "Text:  ", l.label);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(initialValue.Controls);
				
				format = new FormattedStringConfigurer(null, "Name format:  ", new System.String[]{VassalSharp.counters.Labeler.PIECE_NAME, VassalSharp.counters.Labeler.LABEL});
				format.setValue(l.nameFormat.Format);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(format.Controls);
				
				command = new StringConfigurer(null, "Menu Command:  ", l.menuCommand);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(command.Controls);
				
				labelKeyInput = new NamedHotKeyConfigurer(null, "Keyboard Command:  ", l.labelKey);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(labelKeyInput.Controls);
				
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box b = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = "Font:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				b.Controls.Add(temp_Control);
				fontFamily = new System.Windows.Forms.ComboBox();
				//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String[] s = new System.String[]{"Serif", "SansSerif", "Monospaced", "Dialog", "DialogInput"};
				for (int i = 0; i < s.Length; ++i)
				{
					fontFamily.Items.Add(s[i]);
				}
				fontFamily.SelectedItem = l.font.FontFamily.Name;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(fontFamily);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(b);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				b = Box.createHorizontalBox();
				fontSize = new IntConfigurer(null, "Font size:  ", (int) l.font.Size);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(fontSize.Controls);
				System.Windows.Forms.Label temp_label4;
				temp_label4 = new System.Windows.Forms.Label();
				temp_label4.Text = "  Bold?";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = temp_label4;
				b.Controls.Add(temp_Control2);
				//UPGRADE_NOTE: Final was removed from the declaration of 'fontStyle '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int fontStyle = (int) l.font.Style;
				//UPGRADE_TODO: Field 'java.awt.Font.PLAIN' was converted to 'System.Drawing.FontStyle.Regular' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFontPLAIN_f'"
				bold = new BooleanConfigurer(null, "", Boolean.valueOf(fontStyle != (int) System.Drawing.FontStyle.Regular && fontStyle != (int) System.Drawing.FontStyle.Italic));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(bold.Controls);
				System.Windows.Forms.Label temp_label6;
				temp_label6 = new System.Windows.Forms.Label();
				temp_label6.Text = "  Italic?";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control3;
				temp_Control3 = temp_label6;
				b.Controls.Add(temp_Control3);
				//UPGRADE_TODO: Field 'java.awt.Font.PLAIN' was converted to 'System.Drawing.FontStyle.Regular' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFontPLAIN_f'"
				italic = new BooleanConfigurer(null, "", Boolean.valueOf(fontStyle != (int) System.Drawing.FontStyle.Regular && fontStyle != (int) System.Drawing.FontStyle.Bold));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(italic.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(b);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				b = Box.createHorizontalBox();
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				fg = new ColorConfigurer(null, "Text Color:  ", ref l.textFg);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(fg.Controls);
				
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				bg = new ColorConfigurer(null, "  Background Color:  ", ref l.textBg);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(bg.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(b);
				
				renderer = new MyRenderer();
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'rightLeft '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Char[] rightLeft = new System.Char[]{'c', 'r', 'l'};
				//UPGRADE_NOTE: Final was removed from the declaration of 'topBottom '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Char[] topBottom = new System.Char[]{'c', 't', 'b'};
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				b = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label8;
				temp_label8 = new System.Windows.Forms.Label();
				temp_label8.Text = "Vertical position:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control4;
				temp_Control4 = temp_label8;
				b.Controls.Add(temp_Control4);
				vPos = SupportClass.ComboBoxSupport.CreateComboBox(topBottom);
				//UPGRADE_ISSUE: Method 'javax.swing.JComboBox.setRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComboBoxsetRenderer_javaxswingListCellRenderer'"
				vPos.setRenderer(renderer);
				vPos.setSelectedItem(l.verticalPos);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(vPos);
				vOff = new IntConfigurer(null, "  Offset:  ", l.verticalOffset);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(vOff.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(b);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				b = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label10;
				temp_label10 = new System.Windows.Forms.Label();
				temp_label10.Text = "Horizontal position:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control5;
				temp_Control5 = temp_label10;
				b.Controls.Add(temp_Control5);
				hPos = SupportClass.ComboBoxSupport.CreateComboBox(rightLeft);
				//UPGRADE_ISSUE: Method 'javax.swing.JComboBox.setRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComboBoxsetRenderer_javaxswingListCellRenderer'"
				hPos.setRenderer(renderer);
				hPos.setSelectedItem(l.horizontalPos);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(hPos);
				hOff = new IntConfigurer(null, "  Offset:  ", l.horizontalOffset);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(hOff.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(b);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				b = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label12;
				temp_label12 = new System.Windows.Forms.Label();
				temp_label12.Text = "Vertical text justification:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control6;
				temp_Control6 = temp_label12;
				b.Controls.Add(temp_Control6);
				vJust = SupportClass.ComboBoxSupport.CreateComboBox(topBottom);
				//UPGRADE_ISSUE: Method 'javax.swing.JComboBox.setRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComboBoxsetRenderer_javaxswingListCellRenderer'"
				vJust.setRenderer(renderer);
				vJust.setSelectedItem(l.verticalJust);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(vJust);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(b);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				b = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label14;
				temp_label14 = new System.Windows.Forms.Label();
				temp_label14.Text = "Horizontal text justification:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control7;
				temp_Control7 = temp_label14;
				b.Controls.Add(temp_Control7);
				hJust = SupportClass.ComboBoxSupport.CreateComboBox(rightLeft);
				//UPGRADE_ISSUE: Method 'javax.swing.JComboBox.setRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComboBoxsetRenderer_javaxswingListCellRenderer'"
				hJust.setRenderer(renderer);
				hJust.setSelectedItem(l.horizontalJust);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(hJust);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(b);
				
				rotate = new IntConfigurer(null, "Rotate Text (Degrees):  ", l.rotateDegrees);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(rotate.Controls);
				
				propertyNameConfig = new StringConfigurer(null, "Property Name:  ", l.propertyName);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(propertyNameConfig.Controls);
			}
			
			//UPGRADE_ISSUE: Class 'javax.swing.DefaultListCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultListCellRenderer'"
			[Serializable]
			private class MyRenderer:DefaultListCellRenderer
			{
				private const long serialVersionUID = 1L;
				
				public System.Windows.Forms.Control getListCellRendererComponent(System.Windows.Forms.ListBox list, System.Object value_Renamed, int index, bool sel, bool focus)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.DefaultListCellRenderer.getListCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultListCellRenderer'"
					base.getListCellRendererComponent(list, value_Renamed, index, sel, focus);
					switch (((System.Char) value_Renamed))
					{
						
						case 't': 
							Text = "Top";
							break;
						
						case 'b': 
							Text = "Bottom";
							break;
						
						case 'c': 
							Text = "Center";
							break;
						
						case 'l': 
							Text = "Left";
							break;
						
						case 'r': 
							Text = "Right";
							break;
						}
					return this;
				}
			}
		}
		
		public override PieceI18nData getI18nData()
		{
			return getI18nData(new System.String[]{labelFormat.Format, nameFormat.Format, menuCommand}, new System.String[]{"Label Text", "Label Format", "Change Label Command"});
		}
	}
}