/*
* $Id$
*
* Copyright (c) 2000-2012 by Brent Easton, Rodney Kinney
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
//UPGRADE_TODO: The type 'net.miginfocom.swing.MigLayout' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MigLayout = net.miginfocom.swing.MigLayout;
using GameModule = VassalSharp.build.GameModule;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using ChangeTracker = VassalSharp.command.ChangeTracker;
using Command = VassalSharp.command.Command;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using FormattedExpressionConfigurer = VassalSharp.configure.FormattedExpressionConfigurer;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using PropertyNameExpressionConfigurer = VassalSharp.configure.PropertyNameExpressionConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using Resources = VassalSharp.i18n.Resources;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using Expression = VassalSharp.script.expression.Expression;
using ExpressionException = VassalSharp.script.expression.ExpressionException;
using FormattedString = VassalSharp.tools.FormattedString;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
using IconFactory = VassalSharp.tools.icon.IconFactory;
using IconFamily = VassalSharp.tools.icon.IconFamily;
using ImageUtils = VassalSharp.tools.image.ImageUtils;
//UPGRADE_TODO: The type 'VassalSharp.tools.imageop.ImageOp' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ImageOp = VassalSharp.tools.imageop.ImageOp;
using ScaledImagePainter = VassalSharp.tools.imageop.ScaledImagePainter;
namespace VassalSharp.counters
{
	
	/// <summary> The "Layer" trait. Contains a list of images that the user may cycle through.
	/// The current image is superimposed over the inner piece. The entire layer may
	/// be activated or deactivated.
	/// 
	/// Changes to support NamedKeyStrokes:
	/// - Random and reset command changed directly to Name Key Strokes.
	/// - Disentangle alwaysActive flag from length of activateKey field. Make a
	/// separate field and save in type
	/// - Add a Version field to type to enable conversion of Activate/Increase/Decrease
	/// commands. Note commands with more than 1 target keycode cannot be converted
	/// - Simplify code. Removed Version 0 (3.1) code to a separate class Embellishment0. The BasicCommandEncoder
	/// replaces this class with an Embellishment0 if Embellishment(type, inner) returns
	/// a version 0 Embellishment trait.
	/// </summary>
	public class Embellishment:Decorator, TranslatablePiece
	{
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < String > l = new ArrayList < String >();
			l.add(name + IMAGE);
			l.add(name + LEVEL);
			l.add(name + ACTIVE);
			l.add(name + NAME);
			return l;
		}
		virtual public bool Active
		{
			get
			{
				return value_Renamed > 0;
			}
			
			set
			{
				value_Renamed = value?System.Math.Abs(value_Renamed):- System.Math.Abs(value_Renamed);
			}
			
		}
		/// <summary> Set the current level - First level = 0 Does not change the active status
		/// 
		/// </summary>
		/// <param name="val">
		/// </param>
		virtual public int Value
		{
			get
			{
				return System.Math.Abs(value_Renamed) - 1;
			}
			
			set
			{
				int theVal = value;
				if (value >= nValues)
				{
					reportDataError(this, Resources.getString("Error.bad_layer"), "Layer=" + value);
					theVal = nValues;
				}
				value_Renamed = value_Renamed > 0?theVal + 1:- theVal - 1;
			}
			
		}
		override public System.String LocalizedName
		{
			get
			{
				return getName(true);
			}
			
		}
		/// <summary> Return raw Embellishment name</summary>
		/// <returns> Embellishment name
		/// </returns>
		virtual public System.String LayerName
		{
			get
			{
				return name == null?"":name;
			}
			
		}
		virtual protected internal System.Drawing.Image CurrentImage
		{
			get
			{
				// nonpositive value means that layer is inactive
				// null or empty imageName[value-1] means that this layer has no image
				if (value_Renamed <= 0 || imageName[value_Renamed - 1] == null || imageName[value_Renamed - 1].Length == 0 || imagePainter[value_Renamed - 1] == null || imagePainter[value_Renamed - 1].Source == null)
					return null;
				
				return imagePainter[value_Renamed - 1].Source.getImage();
			}
			
		}
		virtual public System.Drawing.Rectangle CurrentImageBounds
		{
			get
			{
				if (value_Renamed > 0)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'i '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int i = value_Renamed - 1;
					
					if (i >= size.Length)
					{
						// Occurs when adding a layer with a name, but no image
						return new System.Drawing.Rectangle();
					}
					
					if (size[i].IsEmpty)
					{
						if (imagePainter[i] != null)
						{
							System.Drawing.Size tempAux = imagePainter[i].ImageSize;
							//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
							size[i] = ImageUtils.getBounds(ref tempAux);
							size[i].Offset(xOff, yOff);
						}
						else
						{
							size[i] = new System.Drawing.Rectangle();
						}
					}
					
					return size[i];
				}
				else
				{
					return new System.Drawing.Rectangle();
				}
			}
			
		}
		/// <summary> Return the Shape of the counter by adding the shape of this layer to the shape of all inner traits.
		/// Minimize generation of new Area objects.
		/// </summary>
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		override public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'innerShape '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.Drawing.Drawing2D.GraphicsPath innerShape = piece.Shape;
				
				if (value_Renamed > 0 && !drawUnderneathWhenSelected)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Rectangle r = CurrentImageBounds;
					
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
						
						// Cache the Area object generated. Only recreate if the layer position or size has changed
						if (!r.Equals(lastBounds))
						{
							lastShape = new System.Drawing.Region(r);
							System.Drawing.Rectangle temp_Rectangle;
							temp_Rectangle = r;
							lastBounds = new System.Drawing.Rectangle(temp_Rectangle.Location, temp_Rectangle.Size);
						}
						
						//UPGRADE_TODO: Method 'java.awt.geom.Area.add' was converted to 'System.Drawing.Region.Union' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtgeomAreaadd_javaawtgeomArea'"
						a.Union(lastShape);
						return a;
					}
				}
				else
				{
					return innerShape;
				}
			}
			
		}
		virtual public System.String Description
		{
			get
			{
				System.String displayName = name;
				if (name == null || name.Length == 0)
				{
					if (imageName.Length > 0 && imageName[0] != null && imageName[0].Length > 0)
					{
						displayName = imageName[0];
					}
				}
				if (displayName == null || displayName.Length == 0)
				{
					return "Layer";
				}
				else
				{
					return "Layer - " + displayName;
				}
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("Layer.htm");
			}
			
		}
		virtual public int Version
		{
			get
			{
				return version;
			}
			
		}
		public const System.String OLD_ID = "emb;";
		public const System.String ID = "emb2;"; // New type encoding
		
		public const System.String IMAGE = "_Image";
		public const System.String NAME = "_Name";
		public const System.String LEVEL = "_Level";
		public const System.String ACTIVE = "_Active";
		
		protected internal System.String activateKey = "";
		protected internal System.String upKey, downKey;
		protected internal int activateModifiers, upModifiers, downModifiers;
		protected internal System.String upCommand, downCommand, activateCommand;
		protected internal System.String resetCommand;
		protected internal FormattedString resetLevel = new FormattedString("1");
		protected internal bool loopLevels;
		protected internal NamedKeyStroke resetKey;
		
		protected internal bool followProperty;
		protected internal System.String propertyName = "";
		protected internal Expression followPropertyExpression;
		protected internal int firstLevelValue;
		
		// random layers
		// protected KeyCommand rndCommand;
		protected internal NamedKeyStroke rndKey;
		private System.String rndText = "";
		// end random layers
		
		// Index of the image to draw. Negative if inactive. 0 is not a valid value.
		protected internal int value_Renamed = - 1;
		
		protected internal int nValues;
		protected internal int xOff, yOff;
		protected internal System.String[] imageName;
		protected internal System.String[] commonName;
		protected internal System.Drawing.Rectangle[] size;
		protected internal ScaledImagePainter[] imagePainter;
		protected internal bool drawUnderneathWhenSelected = false;
		
		protected internal System.String name = "";
		
		protected internal KeyCommand[] commands;
		protected internal KeyCommand up = null;
		protected internal KeyCommand down = null;
		
		// Shape cache
		protected internal System.Drawing.Rectangle lastBounds = System.Drawing.Rectangle.Empty;
		protected internal System.Drawing.Region lastShape = null;
		
		// Version control
		// Version 0 = Original multi-keystroke support for Activate/Increase/Decrease
		// Version 1 = NamedKeyStrokes for Activate/Increase/Decrease
		public const int BASE_VERSION = 0;
		public const int CURRENT_VERSION = 1;
		protected internal int version;
		
		// NamedKeyStroke support
		protected internal bool alwaysActive;
		protected internal NamedKeyStroke activateKeyStroke;
		protected internal NamedKeyStroke increaseKeyStroke;
		protected internal NamedKeyStroke decreaseKeyStroke;
		
		public Embellishment():this(ID + "Activate", null)
		{
		}
		
		public Embellishment(System.String type, GamePiece d)
		{
			InitBlock();
			mySetType(type);
			setInner(d);
		}
		
		public virtual void  mySetType(System.String s)
		{
			if (!s.StartsWith(ID))
			{
				originalSetType(s);
			}
			else
			{
				s = s.Substring(ID.Length);
				SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(s, ';');
				activateCommand = st.nextToken("");
				activateModifiers = st.nextInt((int) System.Windows.Forms.Keys.Control);
				activateKey = st.nextToken("A");
				upCommand = st.nextToken("");
				upModifiers = st.nextInt((int) System.Windows.Forms.Keys.Control);
				upKey = st.nextToken("");
				downCommand = st.nextToken("");
				downModifiers = st.nextInt((int) System.Windows.Forms.Keys.Control);
				downKey = st.nextToken("");
				resetCommand = st.nextToken("");
				resetKey = st.nextNamedKeyStroke();
				resetLevel = new FormattedString(st.nextToken("1"));
				drawUnderneathWhenSelected = st.nextBoolean(false);
				xOff = st.nextInt(0);
				yOff = st.nextInt(0);
				imageName = st.nextStringArray(0);
				commonName = st.nextStringArray(imageName.Length);
				loopLevels = st.nextBoolean(true);
				name = st.nextToken("");
				
				// random layers
				rndKey = st.nextNamedKeyStroke(null);
				rndText = st.nextToken("");
				// end random layers
				
				// Follow property value
				followProperty = st.nextBoolean(false);
				propertyName = st.nextToken("");
				firstLevelValue = st.nextInt(1);
				
				version = st.nextInt(0);
				alwaysActive = st.nextBoolean(false);
				activateKeyStroke = st.nextNamedKeyStroke();
				increaseKeyStroke = st.nextNamedKeyStroke();
				decreaseKeyStroke = st.nextNamedKeyStroke();
				
				// Conversion?
				if (version == BASE_VERSION)
				{
					alwaysActive = activateKey.Length == 0;
					
					// Cannot convert if activate, up or down has more than 1 char specified
					if (activateKey.Length <= 1 && upKey.Length <= 1 && downKey.Length <= 1)
					{
						if (activateKey.Length == 0)
						{
							activateKeyStroke = NamedKeyStroke.NULL_KEYSTROKE;
						}
						else
						{
							activateKeyStroke = new NamedKeyStroke(activateKey[0], activateModifiers);
						}
						
						if (upKey.Length == 0)
						{
							increaseKeyStroke = NamedKeyStroke.NULL_KEYSTROKE;
						}
						else
						{
							increaseKeyStroke = new NamedKeyStroke(upKey[0], upModifiers);
						}
						
						if (downKey.Length == 0)
						{
							decreaseKeyStroke = NamedKeyStroke.NULL_KEYSTROKE;
						}
						else
						{
							decreaseKeyStroke = new NamedKeyStroke(downKey[0], downModifiers);
						}
						version = CURRENT_VERSION;
					}
				}
				
				value_Renamed = activateKey.Length > 0?- 1:1;
				nValues = imageName.Length;
				size = new System.Drawing.Rectangle[imageName.Length];
				imagePainter = new ScaledImagePainter[imageName.Length];
				
				for (int i = 0; i < imageName.Length; ++i)
				{
					imagePainter[i] = new ScaledImagePainter();
					imagePainter[i].ImageName = imageName[i];
				}
			}
			
			commands = null;
		}
		
		/// <summary> This original way of representing the type causes problems because it's not
		/// extensible
		/// 
		/// </summary>
		/// <param name="s">
		/// </param>
		private void  originalSetType(System.String s)
		{
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(s, ';');
			
			st.nextToken();
			//UPGRADE_NOTE: Final was removed from the declaration of 'st2 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder.Decoder st2 = new SequenceEncoder.Decoder(st.nextToken(), ';');
			activateKey = st2.nextToken().ToUpper();
			if (activateKey != null && activateKey.Length > 0)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.KeyStroke.getKeyStroke' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingKeyStrokegetKeyStroke_javalangString'"
				activateKeyStroke = new NamedKeyStroke(KeyStroke.getKeyStroke(activateKey));
			}
			activateModifiers = (int) System.Windows.Forms.Keys.Control;
			if (st2.hasMoreTokens())
			{
				resetCommand = st2.nextToken();
				resetKey = st2.nextNamedKeyStroke(null);
				resetLevel.Format = st2.nextToken("0");
			}
			else
			{
				resetKey = null;
				resetCommand = "";
				resetLevel.Format = "0";
			}
			
			activateCommand = st.nextToken();
			drawUnderneathWhenSelected = activateCommand.StartsWith("_");
			if (drawUnderneathWhenSelected)
			{
				activateCommand = activateCommand.Substring(1);
			}
			
			value_Renamed = activateKey.Length > 0?- 1:1;
			
			upKey = st.nextToken().ToUpper();
			upCommand = st.nextToken();
			upModifiers = (int) System.Windows.Forms.Keys.Control;
			
			downKey = st.nextToken().ToUpper();
			downCommand = st.nextToken();
			downModifiers = (int) System.Windows.Forms.Keys.Control;
			
			xOff = st.nextInt(0);
			yOff = st.nextInt(0);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final ArrayList < String > l = new ArrayList < String >();
			while (st.hasMoreTokens())
			{
				l.add(st.nextToken());
			}
			
			nValues = l.size();
			imageName = new System.String[l.size()];
			commonName = new System.String[l.size()];
			size = new System.Drawing.Rectangle[imageName.Length];
			imagePainter = new ScaledImagePainter[imageName.Length];
			
			for (int i = 0; i < imageName.Length; ++i)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'sub '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String sub = l.get_Renamed(i);
				//UPGRADE_NOTE: Final was removed from the declaration of 'subSt '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SequenceEncoder.Decoder subSt = new SequenceEncoder.Decoder(sub, ',');
				imageName[i] = subSt.nextToken();
				imagePainter[i] = new ScaledImagePainter();
				imagePainter[i].ImageName = imageName[i];
				if (subSt.hasMoreTokens())
				{
					commonName[i] = subSt.nextToken();
				}
			}
			loopLevels = true;
			
			alwaysActive = activateKey.Length == 0;
			if (activateKey.Length == 0)
			{
				activateKeyStroke = NamedKeyStroke.NULL_KEYSTROKE;
			}
			else
			{
				activateKeyStroke = new NamedKeyStroke(activateKey[0], activateModifiers);
			}
			
			if (upKey.Length == 0)
			{
				increaseKeyStroke = NamedKeyStroke.NULL_KEYSTROKE;
			}
			else
			{
				increaseKeyStroke = new NamedKeyStroke(upKey[0], upModifiers);
			}
			
			if (downKey.Length == 0)
			{
				decreaseKeyStroke = NamedKeyStroke.NULL_KEYSTROKE;
			}
			else
			{
				decreaseKeyStroke = new NamedKeyStroke(downKey[0], downModifiers);
			}
			version = CURRENT_VERSION;
		}
		
		public override System.String getName()
		{
			return getName(false);
		}
		
		public virtual System.String getName(bool localized)
		{
			checkPropertyLevel(); // Name Change?
			System.String name = null;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'cname '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String cname = 0 < value_Renamed && value_Renamed - 1 < commonName.Length?getCommonName(localized, value_Renamed - 1):null;
			
			if (cname != null && cname.Length > 0)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(cname, '+');
				//UPGRADE_NOTE: Final was removed from the declaration of 'first '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String first = st.nextToken();
				if (st.hasMoreTokens())
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'second '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String second = st.nextToken();
					if (first.Length == 0)
					{
						name = (localized?piece.LocalizedName:piece.getName()) + second;
					}
					else
					{
						name = first + (localized?piece.LocalizedName:piece.getName());
					}
				}
				else
				{
					name = first;
				}
			}
			else
			{
				name = (localized?piece.LocalizedName:piece.getName());
			}
			
			return name;
		}
		
		public override void  mySetState(System.String s)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(s, ';');
			value_Renamed = st.nextInt(- 1);
		}
		
		public override System.String myGetType()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(activateCommand).append(activateModifiers).append(activateKey).append(upCommand).append(upModifiers).append(upKey).append(downCommand).append(downModifiers).append(downKey).append(resetCommand).append(resetKey).append(resetLevel.Format).append(drawUnderneathWhenSelected).append(xOff).append(yOff).append(imageName).append(commonName).append(loopLevels).append(name).append(rndKey).append(rndText).append(followProperty).append(propertyName).append(firstLevelValue).append(version).append(alwaysActive).append(activateKeyStroke).append(increaseKeyStroke).append(decreaseKeyStroke);
			
			return ID + se.Value;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		public virtual System.String oldGetType()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder se = new SequenceEncoder(';');
			//UPGRADE_NOTE: Final was removed from the declaration of 'se2 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder se2 = new SequenceEncoder(activateKey, ';');
			
			se2.append(resetCommand).append(resetKey).append(System.Convert.ToString(resetLevel));
			
			se.append(se2.Value).append(drawUnderneathWhenSelected?"_" + activateCommand:activateCommand).append(upKey).append(upCommand).append(downKey).append(downCommand).append(xOff).append(yOff);
			
			for (int i = 0; i < nValues; ++i)
			{
				if (commonName[i] != null)
				{
					SequenceEncoder sub = new SequenceEncoder(imageName[i], ',');
					se.append(sub.append(commonName[i]).Value);
				}
				else
				{
					se.append(imageName[i]);
				}
			}
			return ID + se.Value;
		}
		
		public override System.String myGetState()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder se = new SequenceEncoder(';');
			
			/*
			* Fix for Bug 9700 is to strip back the encoding of State to the simplest case.
			* Both Activation status and level is determined by the value parameter.
			*/
			return se.append(System.Convert.ToString(value_Renamed)).Value;
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'drawUnder '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			bool drawUnder = drawUnderneathWhenSelected && true.Equals(getProperty(VassalSharp.counters.Properties_Fields.SELECTED));
			
			if (!drawUnder)
			{
				piece.draw(g, x, y, obs, zoom);
			}
			
			checkPropertyLevel();
			
			if (!Active)
			{
				if (drawUnder)
				{
					piece.draw(g, x, y, obs, zoom);
				}
				return ;
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'i '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int i = value_Renamed - 1;
			
			if (i < imagePainter.Length && imagePainter[i] != null)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Rectangle r = CurrentImageBounds;
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				imagePainter[i].draw(g, x + (int) (zoom * r.X), y + (int) (zoom * r.Y), zoom, obs);
			}
			
			if (drawUnder)
			{
				piece.draw(g, x, y, obs, zoom);
			}
		}
		
		/*
		* Calculate the new level to display based on a property?
		*/
		protected internal virtual void  checkPropertyLevel()
		{
			if (!followProperty || propertyName.Length == 0)
				return ;
			
			if (followPropertyExpression == null)
			{
				followPropertyExpression = Expression.createSimplePropertyExpression(propertyName);
			}
			
			System.String val = "";
			try
			{
				
				val = followPropertyExpression.evaluate(Decorator.getOutermost(this));
				if (val == null || val.Length == 0)
					val = System.Convert.ToString(firstLevelValue);
				
				int v = System.Int32.Parse(val) - firstLevelValue + 1;
				if (v <= 0)
					v = 1;
				if (v > nValues)
					v = nValues;
				
				value_Renamed = Active?v:- v;
			}
			catch (System.FormatException e)
			{
				reportDataError(this, Resources.getString("Error.non_number_error"), "followProperty[" + propertyName + "]=" + val, e);
			}
			catch (ExpressionException e)
			{
				reportDataError(this, Resources.getString("Error.expression_error"), "followProperty[" + propertyName + "]", e);
			}
			return ;
		}
		
		public override KeyCommand[] myGetKeyCommands()
		{
			if (commands == null)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final ArrayList < KeyCommand > l = new ArrayList < KeyCommand >();
				//UPGRADE_NOTE: Final was removed from the declaration of 'outer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GamePiece outer = Decorator.getOutermost(this);
				
				if (activateCommand != null && activateCommand.Length > 0 && !alwaysActive)
				{
					KeyCommand k;
					k = new KeyCommand(activateCommand, activateKeyStroke, outer, this);
					k.setEnabled(nValues > 0);
					l.add(k);
				}
				
				if (!followProperty)
				{
					if (nValues > 1)
					{
						if (upCommand != null && upCommand.Length > 0 && increaseKeyStroke != null && !increaseKeyStroke.Null)
						{
							up = new KeyCommand(upCommand, increaseKeyStroke, outer, this);
							l.add(up);
						}
						
						if (downCommand != null && downCommand.Length > 0 && decreaseKeyStroke != null && !decreaseKeyStroke.Null)
						{
							down = new KeyCommand(downCommand, decreaseKeyStroke, outer, this);
							l.add(down);
						}
					}
					
					if (resetKey != null && !resetKey.Null && resetCommand.Length > 0)
					{
						l.add(new KeyCommand(resetCommand, resetKey, outer, this));
					}
					
					// random layers
					if (rndKey != null && !rndKey.Null && rndText.Length > 0)
					{
						l.add(new KeyCommand(rndText, rndKey, outer, this));
					}
					// end random layers
				}
				
				commands = l.toArray(new KeyCommand[l.size()]);
			}
			
			if (up != null)
			{
				up.setEnabled(loopLevels || System.Math.Abs(value_Renamed) < imageName.Length);
			}
			
			if (down != null)
			{
				down.setEnabled(loopLevels || System.Math.Abs(value_Renamed) > 1);
			}
			
			return commands;
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'tracker '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			ChangeTracker tracker = new ChangeTracker(this);
			
			if (activateKeyStroke.Equals(stroke) && nValues > 0 && !alwaysActive)
			{
				value_Renamed = - value_Renamed;
				//      activated = ! activated;
				//      if (activated) {
				//        value = Math.abs(value);
				//      }
				//      else {
				//        value = -Math.abs(value);
				//      }
			}
			
			if (!followProperty)
			{
				
				if (increaseKeyStroke.Equals(stroke))
				{
					doIncrease();
				}
				if (decreaseKeyStroke.Equals(stroke))
				{
					doDecrease();
				}
				
				if (resetKey != null && resetKey.Equals(stroke))
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'outer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					GamePiece outer = Decorator.getOutermost(this);
					//UPGRADE_NOTE: Final was removed from the declaration of 'levelText '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String levelText = resetLevel.getText(outer);
					try
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'level '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						int level = System.Int32.Parse(levelText);
						Value = System.Math.Abs(level) - 1;
						Active = level > 0;
					}
					catch (System.FormatException e)
					{
						reportDataError(this, Resources.getString("Error.non_number_error"), resetLevel.debugInfo(levelText, "resetLevel"), e);
					}
				}
				// random layers
				if (rndKey != null && rndKey.Equals(stroke))
				{
					int val = 0;
					val = GameModule.getGameModule().getRNG().nextInt(nValues) + 1;
					value_Renamed = value_Renamed > 0?val:- val;
				}
			}
			// end random layers
			return tracker.Changed?tracker.ChangeCommand:null;
		}
		
		protected internal virtual void  doIncrease()
		{
			int val = System.Math.Abs(value_Renamed);
			if (++val > nValues)
			{
				val = loopLevels?1:nValues;
			}
			value_Renamed = value_Renamed > 0?val:- val;
			return ;
		}
		
		protected internal virtual void  doDecrease()
		{
			int val = System.Math.Abs(value_Renamed);
			if (--val < 1)
			{
				val = loopLevels?nValues:1;
			}
			value_Renamed = value_Renamed > 0?val:- val;
			return ;
		}
		
		/// <deprecated> Use {@link ImageOp.getImage} instead. 
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		
		public override System.Drawing.Rectangle boundingBox()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Rectangle r = piece.boundingBox();
			if (value_Renamed > 0)
				SupportClass.RectangleSupport.AddRectangleToRectangle(ref r, CurrentImageBounds);
			return r;
		}
		
		public override System.Object getProperty(System.Object key)
		{
			if (key.Equals(name + IMAGE))
			{
				checkPropertyLevel();
				if (value_Renamed > 0)
				{
					return imageName[System.Math.Abs(value_Renamed) - 1];
				}
				else
					return "";
			}
			else if (key.Equals(name + NAME))
			{
				checkPropertyLevel();
				if (value_Renamed > 0)
				{
					return strip(commonName[System.Math.Abs(value_Renamed) - 1]);
				}
				else
					return "";
			}
			else if (key.Equals(name + LEVEL))
			{
				checkPropertyLevel();
				return System.Convert.ToString(value_Renamed);
			}
			else if (key.Equals(name + ACTIVE))
			{
				return System.Convert.ToString(Active);
			}
			else if (key.Equals(VassalSharp.counters.Properties_Fields.VISIBLE_STATE))
			{
				checkPropertyLevel();
				System.String s = System.Convert.ToString(base.getProperty(key));
				s += value_Renamed;
				if (drawUnderneathWhenSelected)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					s += getProperty(VassalSharp.counters.Properties_Fields.SELECTED);
				}
				return s;
			}
			return base.getProperty(key);
		}
		
		public override System.Object getLocalizedProperty(System.Object key)
		{
			if (key.Equals(name + IMAGE) || key.Equals(name + LEVEL) || key.Equals(name + ACTIVE) || key.Equals(VassalSharp.counters.Properties_Fields.VISIBLE_STATE))
			{
				return getProperty(key);
			}
			else if (key.Equals(name + NAME))
			{
				
				checkPropertyLevel();
				if (value_Renamed > 0)
				{
					return strip(getLocalizedCommonName(System.Math.Abs(value_Renamed) - 1));
				}
				else
					return "";
			}
			return base.getLocalizedProperty(key);
		}
		
		protected internal virtual System.String strip(System.String s)
		{
			if (s == null)
			{
				return null;
			}
			if (s.StartsWith("+"))
			{
				return s.Substring(1);
			}
			if (s.EndsWith("+"))
			{
				return s.Substring(0, (s.Length - 1) - (0));
			}
			return s;
		}
		
		/// <summary>Get the name of this level (alone) </summary>
		protected internal virtual System.String getCommonName(bool localized, int i)
		{
			return localized?getLocalizedCommonName(i):commonName[i];
		}
		
		/// <summary>Get the localized name of this level (alone) </summary>
		protected internal virtual System.String getLocalizedCommonName(int i)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String name = commonName[i];
			if (name == null)
				return null;
			//UPGRADE_NOTE: Final was removed from the declaration of 'translation '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String translation = getTranslation(strip(name));
			if (name.StartsWith("+"))
			{
				return "+" + translation;
			}
			if (name.EndsWith("+"))
			{
				return translation + "+";
			}
			return translation;
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		/// <summary> If the argument GamePiece contains a Layer whose "activate" command matches
		/// the given keystroke, and whose active status matches the boolean argument,
		/// return that Layer
		/// </summary>
		public static Embellishment getLayerWithMatchingActivateCommand(GamePiece piece, System.Windows.Forms.KeyEventArgs stroke, bool active)
		{
			for (Embellishment layer = (Embellishment) Decorator.getDecorator(piece, typeof(Embellishment)); layer != null; layer = (Embellishment) Decorator.getDecorator(layer.piece, typeof(Embellishment)))
			{
				for (int i = 0; i < layer.activateKey.Length; ++i)
				{
					if (stroke.Equals(new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) (layer.activateKey[i] | layer.activateModifiers))))
					{
						if (active && layer.Active)
						{
							return layer;
						}
						else if (!active && !layer.Active)
						{
							return layer;
						}
						break;
					}
				}
			}
			return null;
		}
		
		public static Embellishment getLayerWithMatchingActivateCommand(GamePiece piece, NamedKeyStroke stroke, bool active)
		{
			return getLayerWithMatchingActivateCommand(piece, stroke.KeyStroke, active);
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getPropertyNames()
		
		/// <summary> Return Property names exposed by this trait</summary>
		protected internal class Ed : PieceEditor
		{
			static private System.Int32 state494;
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener
			{
				public AnonymousClassPropertyChangeListener(Ed enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Ed enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Ed enclosingInstance;
				public Ed Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
				{
					Enclosing_Instance.showHideFields();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener1
			{
				public AnonymousClassPropertyChangeListener1(Ed enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Ed enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Ed enclosingInstance;
				public Ed Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
				{
					Enclosing_Instance.showHideFields();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassListSelectionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassListSelectionListener
			{
				public AnonymousClassListSelectionListener(Ed enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Ed enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Ed enclosingInstance;
				public Ed Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  valueChanged(System.Object event_sender, System.EventArgs e)
				{
					Enclosing_Instance.setUpDownEnabled();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener
			{
				public AnonymousClassActionListener(Ed enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Ed enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Ed enclosingInstance;
				public Ed Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					Enclosing_Instance.moveSelectedUp();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener1
			{
				public AnonymousClassActionListener1(Ed enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Ed enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Ed enclosingInstance;
				public Ed Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					Enclosing_Instance.moveSelectedDown();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassKeyAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassKeyAdapter
			{
				public AnonymousClassKeyAdapter(Ed enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Ed enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Ed enclosingInstance;
				public Ed Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs evt)
				{
					Enclosing_Instance.changeLevelName();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener2
			{
				public AnonymousClassActionListener2(Ed enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Ed enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Ed enclosingInstance;
				public Ed Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
				{
					if (Enclosing_Instance.prefix.Checked)
					{
						Enclosing_Instance.suffix.Checked = false;
					}
					Enclosing_Instance.changeLevelName();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener3
			{
				public AnonymousClassActionListener3(Ed enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Ed enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Ed enclosingInstance;
				public Ed Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
				{
					if (Enclosing_Instance.suffix.Checked)
					{
						Enclosing_Instance.prefix.Checked = false;
					}
					Enclosing_Instance.changeLevelName();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener4' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener4
			{
				public AnonymousClassActionListener4(Ed enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Ed enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Ed enclosingInstance;
				public Ed Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
				{
					names.add(null);
					isPrefix.add(null);
					Enclosing_Instance.images.addEntry();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener5' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener5
			{
				public AnonymousClassActionListener5(Ed enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Ed enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Ed enclosingInstance;
				public Ed Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'index '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int index = Enclosing_Instance.images.List.SelectedIndex;
					if (index >= 0)
					{
						names.remove(index);
						isPrefix.remove(index);
						Enclosing_Instance.images.removeEntryAt(index);
					}
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassListSelectionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassListSelectionListener1
			{
				public AnonymousClassListSelectionListener1(Ed enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Ed enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Ed enclosingInstance;
				public Ed Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  valueChanged(System.Object event_sender, System.EventArgs evt)
				{
					Enclosing_Instance.updateLevelName();
				}
			}
			private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
			{
				state494 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
			}
			private void  InitBlock()
			{
				System.Windows.Forms.RadioButton temp_radiobutton;
				temp_radiobutton = new System.Windows.Forms.RadioButton();
				temp_radiobutton.Text = "is prefix";
				prefix = temp_radiobutton;
				System.Windows.Forms.RadioButton temp_radiobutton2;
				temp_radiobutton2 = new System.Windows.Forms.RadioButton();
				temp_radiobutton2.Text = "is suffix";
				suffix = temp_radiobutton2;
			}
			virtual protected internal MultiImagePicker ImagePicker
			{
				get
				{
					return new MultiImagePicker();
				}
				
			}
			virtual public System.String State
			{
				get
				{
					return alwaysActiveConfig.ValueBoolean?"1":"-1";
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					SequenceEncoder se = new SequenceEncoder(';');
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					final ArrayList < String > imageNames = new ArrayList < String >();
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					final ArrayList < String > commonNames = new ArrayList < String >();
					int i = 0;
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(String n: images.getImageNameList())
					{
						imageNames.add(n);
						System.String commonName = names.get_Renamed(i);
						if (commonName != null && commonName.Length > 0)
						{
							if (PREFIX.Equals(isPrefix.get_Renamed(i)))
							{
								commonName = new SequenceEncoder(commonName, '+').append("").Value;
							}
							else if (SUFFIX.Equals(isPrefix.get_Renamed(i)))
							{
								commonName = new SequenceEncoder("", '+').append(commonName).Value;
							}
							else
							{
								commonName = new SequenceEncoder(commonName, '+').Value;
							}
						}
						commonNames.add(commonName);
						i++;
					}
					
					try
					{
						System.Int32.Parse(xOffInput.Text);
					}
					catch (System.FormatException xNAN)
					{
						// TODO use IntConfigurer NB Deprecated code - don't worry
						//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
						xOffInput.Text = "0";
					}
					
					try
					{
						System.Int32.Parse(yOffInput.Text);
					}
					catch (System.FormatException yNAN)
					{
						// TODO use IntConfigurer NB Deprecated code - don't worry
						//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
						yOffInput.Text = "0";
					}
					
					se.append(activateCommand.ValueString).append("").append("").append(upCommand.ValueString).append("").append("").append(downCommand.ValueString).append("").append("").append(resetCommand.ValueString).append(resetConfig.ValueString).append(resetLevel.ValueString).append(drawUnderneath.Checked).append(xOffInput.Text).append(yOffInput.Text).append(imageNames.toArray(new System.String[imageNames.size()])).append(commonNames.toArray(new System.String[commonNames.size()])).append(loop.Checked).append(nameConfig.ValueString).append(rndKeyConfig.ValueString).append(rndCommand.ValueString == null?"":rndCommand.ValueString.Trim()).append(followConfig.ValueString).append(propertyConfig.ValueString).append(firstLevelConfig.ValueString).append(version).append(alwaysActiveConfig.ValueString).append(activateConfig.ValueString).append(increaseConfig.ValueString).append(decreaseConfig.ValueString);
					
					return VassalSharp.counters.Embellishment.ID + se.Value;
				}
				
			}
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return controls;
				}
				
			}
			private MultiImagePicker images;
			private StringConfigurer activateCommand;
			private StringConfigurer upCommand;
			private StringConfigurer downCommand;
			private StringConfigurer rndCommand;
			
			//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
			private System.Windows.Forms.TextBox xOffInput = new System.Windows.Forms.TextBox();
			//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
			private System.Windows.Forms.TextBox yOffInput = new System.Windows.Forms.TextBox();
			//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
			private System.Windows.Forms.TextBox levelNameInput = new System.Windows.Forms.TextBox();
			//UPGRADE_NOTE: The initialization of  'prefix' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private System.Windows.Forms.RadioButton prefix;
			//UPGRADE_NOTE: The initialization of  'suffix' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private System.Windows.Forms.RadioButton suffix;
			private System.Windows.Forms.CheckBox drawUnderneath = SupportClass.CheckBoxSupport.CreateCheckBox("Underneath when highlighted?");
			private FormattedExpressionConfigurer resetLevel = new FormattedExpressionConfigurer(null, "Reset to level:  ");
			private StringConfigurer resetCommand;
			private System.Windows.Forms.CheckBox loop = SupportClass.CheckBoxSupport.CreateCheckBox("Loop through levels?");
			
			private System.Windows.Forms.Panel controls;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private List < String > names;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private List < Integer > isPrefix;
			//UPGRADE_NOTE: Final was removed from the declaration of 'NEITHER'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private static readonly System.Int32 NEITHER = 0;
			//UPGRADE_NOTE: Final was removed from the declaration of 'PREFIX'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private static readonly System.Int32 PREFIX = 1;
			//UPGRADE_NOTE: Final was removed from the declaration of 'SUFFIX'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private static readonly System.Int32 SUFFIX = 2;
			
			private BooleanConfigurer followConfig;
			private PropertyNameExpressionConfigurer propertyConfig;
			private IntConfigurer firstLevelConfig;
			private StringConfigurer nameConfig;
			
			private System.Windows.Forms.Button up, down;
			private int version;
			private BooleanConfigurer alwaysActiveConfig;
			private NamedHotKeyConfigurer activateConfig;
			private NamedHotKeyConfigurer increaseConfig;
			private NamedHotKeyConfigurer decreaseConfig;
			private NamedHotKeyConfigurer resetConfig;
			private NamedHotKeyConfigurer rndKeyConfig;
			
			private System.Windows.Forms.Label activateLabel;
			private System.Windows.Forms.Label increaseLabel;
			private System.Windows.Forms.Label decreaseLabel;
			private System.Windows.Forms.Label resetLabel;
			private System.Windows.Forms.Label rndLabel;
			
			private System.Windows.Forms.Label actionLabel;
			private System.Windows.Forms.Label menuLabel;
			private System.Windows.Forms.Label keyLabel;
			private System.Windows.Forms.Label optionLabel;
			
			public Ed(Embellishment e)
			{
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box box;
				version = e.version;
				
				controls = new System.Windows.Forms.Panel();
				controls.setLayout(new MigLayout("hidemode 2,fillx", "[]rel[]rel[]rel[]"));
				
				nameConfig = new StringConfigurer(null, "Name: ", e.getName());
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(nameConfig.Controls);
				nameConfig.Controls.Dock = new System.Windows.Forms.DockStyle();
				nameConfig.Controls.BringToFront();
				
				alwaysActiveConfig = new BooleanConfigurer(null, "Always active?", e.alwaysActive);
				alwaysActiveConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(alwaysActiveConfig.Controls);
				alwaysActiveConfig.Controls.Dock = new System.Windows.Forms.DockStyle();
				alwaysActiveConfig.Controls.BringToFront();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(drawUnderneath);
				drawUnderneath.Dock = new System.Windows.Forms.DockStyle();
				drawUnderneath.BringToFront();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(loop);
				loop.Dock = new System.Windows.Forms.DockStyle();
				loop.BringToFront();
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'offsetControls '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box offsetControls = Box.createHorizontalBox();
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
				xOffInput.setMaximumSize(xOffInput.Size);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				xOffInput.Text = "0";
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
				yOffInput.setMaximumSize(xOffInput.Size);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				yOffInput.Text = "0";
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = "Offset: ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				offsetControls.Controls.Add(temp_Control);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				offsetControls.Controls.Add(xOffInput);
				System.Windows.Forms.Label temp_label4;
				temp_label4 = new System.Windows.Forms.Label();
				temp_label4.Text = ",";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = temp_label4;
				offsetControls.Controls.Add(temp_Control2);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				offsetControls.Controls.Add(yOffInput);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(offsetControls);
				offsetControls.Dock = new System.Windows.Forms.DockStyle();
				offsetControls.BringToFront();
				
				followConfig = new BooleanConfigurer(null, "Levels follow expression value?");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(followConfig.Controls);
				followConfig.Controls.Dock = new System.Windows.Forms.DockStyle();
				followConfig.Controls.BringToFront();
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'levelBox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box levelBox = Box.createHorizontalBox();
				propertyConfig = new PropertyNameExpressionConfigurer(null, "Follow Expression:  ");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				levelBox.Controls.Add(propertyConfig.Controls);
				firstLevelConfig = new IntConfigurer(null, " Level 1 = ", e.firstLevelValue);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				levelBox.Controls.Add(firstLevelConfig.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(levelBox);
				levelBox.Dock = new System.Windows.Forms.DockStyle();
				levelBox.BringToFront();
				
				followConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener1(this).propertyChange);
				
				System.Windows.Forms.Label temp_label5;
				temp_label5 = new System.Windows.Forms.Label();
				temp_label5.Text = "Action";
				actionLabel = temp_label5;
				//UPGRADE_NOTE: Final was removed from the declaration of 'defaultFont '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Font defaultFont = actionLabel.Font;
				//UPGRADE_NOTE: Final was removed from the declaration of 'boldFont '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
				System.Drawing.Font boldFont = new System.Drawing.Font(defaultFont.FontFamily.Name, (int) defaultFont.Size, (System.Drawing.FontStyle) System.Drawing.FontStyle.Bold);
				actionLabel.Font = boldFont;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(actionLabel);
				
				System.Windows.Forms.Label temp_label6;
				temp_label6 = new System.Windows.Forms.Label();
				temp_label6.Text = "Menu Command";
				menuLabel = temp_label6;
				menuLabel.Font = boldFont;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(menuLabel);
				menuLabel.Dock = new System.Windows.Forms.DockStyle();
				menuLabel.BringToFront();
				
				System.Windows.Forms.Label temp_label7;
				temp_label7 = new System.Windows.Forms.Label();
				temp_label7.Text = "Key";
				keyLabel = temp_label7;
				keyLabel.Font = boldFont;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(keyLabel);
				keyLabel.Dock = new System.Windows.Forms.DockStyle();
				keyLabel.BringToFront();
				
				System.Windows.Forms.Label temp_label8;
				temp_label8 = new System.Windows.Forms.Label();
				temp_label8.Text = "Option";
				optionLabel = temp_label8;
				optionLabel.Font = boldFont;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(optionLabel);
				optionLabel.Dock = new System.Windows.Forms.DockStyle();
				optionLabel.BringToFront();
				
				activateConfig = new NamedHotKeyConfigurer(null, "", e.activateKeyStroke);
				increaseConfig = new NamedHotKeyConfigurer(null, "", e.increaseKeyStroke);
				decreaseConfig = new NamedHotKeyConfigurer(null, "", e.decreaseKeyStroke);
				resetConfig = new NamedHotKeyConfigurer(null, "", e.resetKey);
				rndKeyConfig = new NamedHotKeyConfigurer(null, "", e.rndKey);
				
				System.Windows.Forms.Label temp_label9;
				temp_label9 = new System.Windows.Forms.Label();
				temp_label9.Text = "Activate Layer";
				activateLabel = temp_label9;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(activateLabel);
				activateCommand = new StringConfigurer(null, "", e.activateCommand);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(activateCommand.Controls);
				activateCommand.Controls.Dock = new System.Windows.Forms.DockStyle();
				activateCommand.Controls.BringToFront();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(activateConfig.Controls);
				activateConfig.Controls.Dock = new System.Windows.Forms.DockStyle();
				activateConfig.Controls.BringToFront();
				
				System.Windows.Forms.Label temp_label10;
				temp_label10 = new System.Windows.Forms.Label();
				temp_label10.Text = "Increase Level";
				increaseLabel = temp_label10;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(increaseLabel);
				upCommand = new StringConfigurer(null, "", e.upCommand);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(upCommand.Controls);
				upCommand.Controls.Dock = new System.Windows.Forms.DockStyle();
				upCommand.Controls.BringToFront();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(increaseConfig.Controls);
				increaseConfig.Controls.Dock = new System.Windows.Forms.DockStyle();
				increaseConfig.Controls.BringToFront();
				
				System.Windows.Forms.Label temp_label11;
				temp_label11 = new System.Windows.Forms.Label();
				temp_label11.Text = "Decrease Level";
				decreaseLabel = temp_label11;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(decreaseLabel);
				downCommand = new StringConfigurer(null, "", e.downCommand);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(downCommand.Controls);
				downCommand.Controls.Dock = new System.Windows.Forms.DockStyle();
				downCommand.Controls.BringToFront();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(decreaseConfig.Controls);
				decreaseConfig.Controls.Dock = new System.Windows.Forms.DockStyle();
				decreaseConfig.Controls.BringToFront();
				
				System.Windows.Forms.Label temp_label12;
				temp_label12 = new System.Windows.Forms.Label();
				temp_label12.Text = "Reset to Level";
				resetLabel = temp_label12;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(resetLabel);
				resetCommand = new StringConfigurer(null, "", e.resetCommand);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(resetCommand.Controls);
				resetCommand.Controls.Dock = new System.Windows.Forms.DockStyle();
				resetCommand.Controls.BringToFront();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(resetConfig.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(resetLevel.Controls);
				resetLevel.Controls.Dock = new System.Windows.Forms.DockStyle();
				resetLevel.Controls.BringToFront();
				
				System.Windows.Forms.Label temp_label13;
				temp_label13 = new System.Windows.Forms.Label();
				temp_label13.Text = "Randomize";
				rndLabel = temp_label13;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(rndLabel);
				rndCommand = new StringConfigurer(null, "", e.rndText);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(rndCommand.Controls);
				rndCommand.Controls.Dock = new System.Windows.Forms.DockStyle();
				rndCommand.Controls.BringToFront();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(rndKeyConfig.Controls);
				rndKeyConfig.Controls.Dock = new System.Windows.Forms.DockStyle();
				rndKeyConfig.Controls.BringToFront();
				
				images = ImagePicker;
				images.addListSelectionListener(new AnonymousClassListSelectionListener(this));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(images);
				images.Dock = new System.Windows.Forms.DockStyle();
				images.BringToFront();
				
				up = SupportClass.ButtonSupport.CreateStandardButton(IconFactory.getIcon("go-up", IconFamily.XSMALL));
				up.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(up);
				
				down = SupportClass.ButtonSupport.CreateStandardButton(IconFactory.getIcon("go-down", IconFamily.XSMALL));
				down.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(down);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'upDownPanel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box upDownPanel = Box.createVerticalBox();
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalGlue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control3;
				temp_Control3 = Box.createVerticalGlue();
				upDownPanel.Controls.Add(temp_Control3);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				upDownPanel.Controls.Add(up);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				upDownPanel.Controls.Add(down);
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalGlue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control4;
				temp_Control4 = Box.createVerticalGlue();
				upDownPanel.Controls.Add(temp_Control4);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(upDownPanel);
				upDownPanel.Dock = new System.Windows.Forms.DockStyle();
				upDownPanel.BringToFront();
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				box = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label15;
				temp_label15 = new System.Windows.Forms.Label();
				temp_label15.Text = "Level Name:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control5;
				temp_Control5 = temp_label15;
				box.Controls.Add(temp_Control5);
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
				levelNameInput.setMaximumSize(levelNameInput.Size);
				levelNameInput.KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.counters.Embellishment.Ed.keyDown);
				levelNameInput.KeyUp += new System.Windows.Forms.KeyEventHandler(new AnonymousClassKeyAdapter(this).keyReleased);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(levelNameInput);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(box);
				box.Dock = new System.Windows.Forms.DockStyle();
				box.BringToFront();
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				box = Box.createHorizontalBox();
				prefix.CheckedChanged += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(prefix);
				suffix.CheckedChanged += new System.EventHandler(new AnonymousClassActionListener3(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(suffix);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(prefix);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(suffix);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(box);
				box.Dock = new System.Windows.Forms.DockStyle();
				box.BringToFront();
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'buttonPanel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Panel buttonPanel = new JPanel(new MigLayout("ins 0", "[grow 1]rel[grow 1]"));
				System.Windows.Forms.Button b = SupportClass.ButtonSupport.CreateStandardButton("Add Level");
				b.Click += new System.EventHandler(new AnonymousClassActionListener4(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(b);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				buttonPanel.Controls.Add(b);
				b.Dock = new System.Windows.Forms.DockStyle();
				b.BringToFront();
				
				b = SupportClass.ButtonSupport.CreateStandardButton("Remove Level");
				b.Click += new System.EventHandler(new AnonymousClassActionListener5(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(b);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				buttonPanel.Controls.Add(b);
				b.Dock = new System.Windows.Forms.DockStyle();
				b.BringToFront();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				controls.Controls.Add(buttonPanel);
				buttonPanel.Dock = new System.Windows.Forms.DockStyle();
				buttonPanel.BringToFront();
				
				images.List.SelectedValueChanged += new System.EventHandler(new AnonymousClassListSelectionListener1(this).valueChanged);
				
				showHideFields();
				
				reset(e);
			}
			
			protected internal virtual void  moveSelectedUp()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'selected '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int selected = images.List.SelectedIndex;
				//UPGRADE_NOTE: Final was removed from the declaration of 'count '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int count = images.List.Items.Count;
				if (count > 1 && selected > 0)
				{
					swap(selected, selected - 1);
				}
			}
			
			protected internal virtual void  moveSelectedDown()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'selected '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int selected = images.List.SelectedIndex;
				//UPGRADE_NOTE: Final was removed from the declaration of 'count '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int count = images.List.Items.Count;
				if (count > 1 && selected < (count - 1))
				{
					swap(selected, selected + 1);
				}
			}
			
			protected internal virtual void  swap(int index1, int index2)
			{
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String name = names.get_Renamed(index1);
				names.set_Renamed(index1, names.get_Renamed(index2));
				names.set_Renamed(index2, name);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'prefix '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Int32 prefix = isPrefix.get_Renamed(index1);
				isPrefix.set_Renamed(index1, isPrefix.get_Renamed(index2));
				isPrefix.set_Renamed(index2, prefix);
				
				images.swap(index1, index2);
			}
			
			protected internal virtual void  setUpDownEnabled()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'selected '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int selected = images.List.SelectedIndex;
				//UPGRADE_NOTE: Final was removed from the declaration of 'count '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int count = images.List.Items.Count;
				up.Enabled = count > 1 && selected > 0;
				down.Enabled = count > 1 && selected < (count - 1);
			}
			
			/*
			* Change visibility of fields depending on the Follow Property  and Always Active settings
			*/
			protected internal virtual void  showHideFields()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'alwaysActive '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				bool alwaysActive = alwaysActiveConfig.ValueBoolean;
				if (alwaysActive)
				{
					activateLabel.Visible = false;
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(activateCommand.Controls, "Visible", false);
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(activateConfig.Controls, "Visible", false);
				}
				else
				{
					activateLabel.Visible = true;
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(activateCommand.Controls, "Visible", true);
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(activateConfig.Controls, "Visible", true);
				}
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'controlled '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				bool controlled = !followConfig.booleanValue();
				loop.Enabled = controlled;
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(propertyConfig.Controls, "Visible", !controlled);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(firstLevelConfig.Controls, "Visible", !controlled);
				
				increaseLabel.Visible = controlled;
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(upCommand.Controls, "Visible", controlled);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(increaseConfig.Controls, "Visible", controlled);
				
				decreaseLabel.Visible = controlled;
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(downCommand.Controls, "Visible", controlled);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(decreaseConfig.Controls, "Visible", controlled);
				
				resetLabel.Visible = controlled;
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(resetCommand.Controls, "Visible", controlled);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(resetConfig.Controls, "Visible", controlled);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(resetLevel.Controls, "Visible", controlled);
				
				rndLabel.Visible = controlled;
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(rndCommand.Controls, "Visible", controlled);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(rndKeyConfig.Controls, "Visible", controlled);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'labelsVisible '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				bool labelsVisible = ((!alwaysActive) || controlled);
				actionLabel.Visible = labelsVisible;
				menuLabel.Visible = labelsVisible;
				keyLabel.Visible = labelsVisible;
				optionLabel.Visible = labelsVisible;
				
				Decorator.repack(controls);
			}
			
			private void  updateLevelName()
			{
				int index = images.List.SelectedIndex;
				if (index < 0)
				{
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
					levelNameInput.Text = null;
				}
				else
				{
					levelNameInput.setText(names.get_Renamed(index));
					prefix.Checked = PREFIX.Equals(isPrefix.get_Renamed(index));
					suffix.Checked = SUFFIX.Equals(isPrefix.get_Renamed(index));
				}
			}
			
			private void  changeLevelName()
			{
				int index = images.List.SelectedIndex;
				if (index >= 0)
				{
					System.String s = levelNameInput.Text;
					names.set_Renamed(index, s);
					if (prefix.Checked)
					{
						isPrefix.set_Renamed(index, PREFIX);
					}
					else if (suffix.Checked)
					{
						isPrefix.set_Renamed(index, SUFFIX);
					}
					else
					{
						isPrefix.set_Renamed(index, NEITHER);
					}
				}
				else if (index == 0)
				{
					names.set_Renamed(index, null);
					isPrefix.set_Renamed(index, NEITHER);
				}
			}
			
			public virtual void  reset(Embellishment e)
			{
				nameConfig.setValue(e.name);
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				names = new ArrayList < String >();
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				isPrefix = new ArrayList < Integer >();
				for (int i = 0; i < e.commonName.Length; ++i)
				{
					System.String s = e.commonName[i];
					System.Int32 is_Renamed = NEITHER;
					if (s != null && s.Length > 0)
					{
						SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(s, '+');
						System.String first = st.nextToken();
						if (st.hasMoreTokens())
						{
							System.String second = st.nextToken();
							if (first.Length == 0)
							{
								s = second;
								is_Renamed = SUFFIX;
							}
							else
							{
								s = first;
								is_Renamed = PREFIX;
							}
						}
						else
						{
							s = first;
						}
					}
					names.add(s);
					isPrefix.add(is_Renamed);
				}
				
				alwaysActiveConfig.setValue(Boolean.valueOf(e.alwaysActive));
				drawUnderneath.Checked = e.drawUnderneathWhenSelected;
				loop.Checked = e.loopLevels;
				
				images.clear();
				
				activateCommand.setValue(e.activateCommand);
				upCommand.setValue(e.upCommand);
				downCommand.setValue(e.downCommand);
				resetConfig.setValue(e.resetKey);
				resetCommand.setValue(e.resetCommand);
				resetLevel.setValue(e.resetLevel.Format);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				xOffInput.Text = System.Convert.ToString(e.xOff);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				yOffInput.Text = System.Convert.ToString(e.yOff);
				images.ImageList = e.imageName;
				
				followConfig.setValue(Boolean.valueOf(e.followProperty));
				propertyConfig.setValue(e.propertyName);
				
				// Add at least one level if none defined
				if (images.getImageNameList().isEmpty())
				{
					names.add(null);
					isPrefix.add(null);
					images.addEntry();
				}
				
				updateLevelName();
				
				showHideFields();
			}
		}
		
		public override PieceI18nData getI18nData()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'data '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			PieceI18nData data = new PieceI18nData(this);
			//UPGRADE_NOTE: Final was removed from the declaration of 'prefix '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String prefix = name.Length > 0?name + ": ":"";
			if (activateKey.Length > 0)
			{
				data.add(activateCommand, prefix + "Activate command");
			}
			if (!followProperty)
			{
				data.add(upCommand, prefix + "Increase command");
				data.add(downCommand, prefix + "Decrease command");
				data.add(resetCommand, prefix + "Reset command");
				data.add(rndText, prefix + "Random command");
			}
			// Strip off prefix/suffix marker
			for (int i = 0; i < commonName.Length; i++)
			{
				data.add(strip(commonName[i]), prefix + "Level " + (i + 1) + " name");
			}
			return data;
		}
	}
}