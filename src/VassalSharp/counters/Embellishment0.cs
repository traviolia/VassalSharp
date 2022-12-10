/*
* $Id: Embellishment0.java 8282 2012-08-21 00:05:59Z swampwallaby $
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
using GameModule = VassalSharp.build.GameModule;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using ChangeTracker = VassalSharp.command.ChangeTracker;
using Command = VassalSharp.command.Command;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using HotKeyConfigurer = VassalSharp.configure.HotKeyConfigurer;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using KeyModifiersConfigurer = VassalSharp.configure.KeyModifiersConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using Resources = VassalSharp.i18n.Resources;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using FormattedString = VassalSharp.tools.FormattedString;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
using ImageUtils = VassalSharp.tools.image.ImageUtils;
//UPGRADE_TODO: The type 'VassalSharp.tools.imageop.ImageOp' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ImageOp = VassalSharp.tools.imageop.ImageOp;
using ScaledImagePainter = VassalSharp.tools.imageop.ScaledImagePainter;
namespace VassalSharp.counters
{
	
	/// <summary> 
	/// Embellishment has been extensively re-written for Vassal 3.2 changing
	/// both the behavior and the visual look of the configurer. A version
	/// number has been added to distinguish between the two versions.
	/// Note, there is also a much older Embellishment trait with a type of emb
	/// 
	/// When editing a module, the old versions will be automatically converted to
	/// the new version. One feature (multiple keystrokes for a command) cannot
	/// be converted.
	/// 
	/// This class contains the complete code of the original version 0
	/// emb2 Embellishment to support editing of unconverted version 0 Embellishments
	/// and run-time support for modules containing unconverted traits.
	/// 
	/// This is essentially the latest 3.1 version of Embellishment
	/// </summary>
	public class Embellishment0:Decorator, TranslatablePiece
	{
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
				if (value >= nValues)
				{
					throw new System.ArgumentException();
				}
				value_Renamed = value_Renamed > 0?value + 1:- value - 1;
			}
			
		}
		override public System.String LocalizedName
		{
			get
			{
				return getName(true);
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
		protected internal System.Windows.Forms.KeyEventArgs resetKey;
		
		protected internal bool followProperty;
		protected internal System.String propertyName = "";
		protected internal int firstLevelValue;
		
		// random layers
		// protected KeyCommand rndCommand;
		protected internal System.Windows.Forms.KeyEventArgs rndKey;
		private System.String rndText = "";
		// end random layers
		
		// Index of the image to draw. Negative if inactive. 0 is not a valid value.
		protected internal int value_Renamed = - 1;
		
		protected internal System.String activationStatus = "";
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
		
		public Embellishment0():this(ID + "Activate", null)
		{
		}
		
		public Embellishment0(System.String type, GamePiece d)
		{
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
				activateCommand = st.nextToken("Activate");
				activateModifiers = st.nextInt((int) System.Windows.Forms.Keys.Control);
				activateKey = st.nextToken("A");
				upCommand = st.nextToken("Increase");
				upModifiers = st.nextInt((int) System.Windows.Forms.Keys.Control);
				upKey = st.nextToken("]");
				downCommand = st.nextToken("Decrease");
				downModifiers = st.nextInt((int) System.Windows.Forms.Keys.Control);
				downKey = st.nextToken("[");
				resetCommand = st.nextToken("Reset");
				resetKey = st.nextKeyStroke('R');
				resetLevel = new FormattedString(st.nextToken("1"));
				drawUnderneathWhenSelected = st.nextBoolean(false);
				xOff = st.nextInt(0);
				yOff = st.nextInt(0);
				imageName = st.nextStringArray(0);
				commonName = st.nextStringArray(imageName.Length);
				loopLevels = st.nextBoolean(true);
				name = st.nextToken("");
				
				// random layers
				rndKey = st.nextKeyStroke(null);
				rndText = st.nextToken("");
				// end random layers
				
				// Follow property value
				followProperty = st.nextBoolean(false);
				propertyName = st.nextToken("");
				firstLevelValue = st.nextInt(1);
				
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
			activateModifiers = (int) System.Windows.Forms.Keys.Control;
			if (st2.hasMoreTokens())
			{
				resetCommand = st2.nextToken();
				resetKey = st2.nextKeyStroke(null);
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
			value_Renamed = st.nextInt(1);
			activationStatus = st.nextToken(value_Renamed < 0?"":activateKey);
		}
		
		public override System.String myGetType()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(activateCommand).append(activateModifiers).append(activateKey).append(upCommand).append(upModifiers).append(upKey).append(downCommand).append(downModifiers).append(downKey).append(resetCommand).append(resetKey).append(resetLevel.Format).append(drawUnderneathWhenSelected).append(xOff).append(yOff).append(imageName).append(commonName).append(loopLevels).append(name).append(rndKey).append(rndText).append(followProperty).append(propertyName).append(firstLevelValue);
			
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
			return se.append(System.Convert.ToString(value_Renamed)).append(activationStatus).Value;
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			piece.draw(g, x, y, obs, zoom);
			
			checkPropertyLevel();
			
			if (!Active)
			{
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
			
			if (drawUnderneathWhenSelected && true.Equals(getProperty(VassalSharp.counters.Properties_Fields.SELECTED)))
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
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'propertyValue '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Object propertyValue = Decorator.getOutermost(this).getProperty(propertyName);
			//UPGRADE_NOTE: Final was removed from the declaration of 'val '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String val = propertyValue == null?System.Convert.ToString(firstLevelValue):System.Convert.ToString(propertyValue);
			
			try
			{
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
				if (activateCommand.Length > 0 && activateKey.Length > 0)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'k '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					KeyCommand k = new KeyCommand(activateCommand, new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) (activateKey[0] | activateModifiers)), outer, this);
					k.setEnabled(nValues > 0);
					l.add(k);
				}
				if (upCommand.Length > 0 && upKey.Length > 0 && nValues > 1 && !followProperty)
				{
					up = new KeyCommand(upCommand, new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) (upKey[0] | upModifiers)), outer, this);
					l.add(up);
				}
				if (downCommand.Length > 0 && downKey.Length > 0 && nValues > 1 && !followProperty)
				{
					down = new KeyCommand(downCommand, new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) (downKey[0] | downModifiers)), outer, this);
					l.add(down);
				}
				if (resetKey != null && resetCommand.Length > 0 && !followProperty)
				{
					l.add(new KeyCommand(resetCommand, resetKey, outer, this));
				}
				// random layers
				if (rndKey != null && rndText.Length > 0 && !followProperty)
				{
					l.add(new KeyCommand(rndText, rndKey, outer, this));
				}
				// end random layers
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
			char strokeChar = getMatchingActivationChar(stroke);
			ChangeTracker tracker = null;
			if (strokeChar != 0 && nValues > 0)
			{
				// Do not Activate if no levels defined
				tracker = new ChangeTracker(this);
				int index = activationStatus.IndexOf((System.Char) strokeChar);
				if (index < 0)
				{
					activationStatus += strokeChar;
				}
				else
				{
					System.String before = activationStatus.Substring(0, (index) - (0));
					System.String after = activationStatus.Substring(index + 1);
					activationStatus = before + after;
				}
				if (activationStatus.Length == activateKey.Length)
				{
					value_Renamed = System.Math.Abs(value_Renamed);
				}
				else
				{
					value_Renamed = - System.Math.Abs(value_Renamed);
				}
			}
			if (!followProperty)
			{
				for (int i = 0; i < upKey.Length; ++i)
				{
					if (new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) (upKey[i] | upModifiers)).Equals(stroke))
					{
						if (tracker == null)
						{
							tracker = new ChangeTracker(this);
						}
						int val = System.Math.Abs(value_Renamed);
						if (++val > nValues)
						{
							val = loopLevels?1:nValues;
						}
						value_Renamed = value_Renamed > 0?val:- val;
						break;
					}
				}
				for (int i = 0; i < downKey.Length; ++i)
				{
					if (new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) (downKey[i] | downModifiers)).Equals(stroke))
					{
						if (tracker == null)
						{
							tracker = new ChangeTracker(this);
						}
						int val = System.Math.Abs(value_Renamed);
						if (--val < 1)
						{
							val = loopLevels?nValues:1;
						}
						value_Renamed = value_Renamed > 0?val:- val;
						break;
					}
				}
				if (resetKey != null && resetKey.Equals(stroke))
				{
					if (tracker == null)
					{
						tracker = new ChangeTracker(this);
					}
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
					if (tracker == null)
					{
						tracker = new ChangeTracker(this);
					}
					int val = 0;
					val = GameModule.getGameModule().getRNG().nextInt(nValues) + 1;
					value_Renamed = value_Renamed > 0?val:- val;
				}
			}
			// end random layers
			return tracker != null?tracker.ChangeCommand:null;
		}
		
		private char getMatchingActivationChar(System.Windows.Forms.KeyEventArgs stroke)
		{
			for (int i = 0; i < activateKey.Length; ++i)
			{
				if (stroke != null && stroke.Equals(new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) (activateKey[i] | activateModifiers))))
				{
					return activateKey[i];
				}
			}
			return (char) 0;
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
				System.String s = System.Convert.ToString(base.getProperty(key));
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
		
		private class Ed : PieceEditor
		{
			static private System.Int32 state499;
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassItemListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassItemListener
			{
				public AnonymousClassItemListener(Ed enclosingInstance)
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
				public virtual void  itemStateChanged(System.Object event_sender, System.EventArgs evt)
				{
					if (event_sender is System.Windows.Forms.MenuItem)
						((System.Windows.Forms.MenuItem) event_sender).Checked = !((System.Windows.Forms.MenuItem) event_sender).Checked;
					if (Enclosing_Instance.alwaysActive.Checked)
					{
						//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
						Enclosing_Instance.activateCommand.Text = "";
						//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
						Enclosing_Instance.activateKeyInput.Text = "";
						Enclosing_Instance.activateCommand.Enabled = false;
						Enclosing_Instance.activateKeyInput.Enabled = false;
					}
					else
					{
						//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
						Enclosing_Instance.activateCommand.Text = "Activate";
						//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
						Enclosing_Instance.activateKeyInput.Text = "A";
						Enclosing_Instance.activateCommand.Enabled = true;
						Enclosing_Instance.activateKeyInput.Enabled = true;
					}
				}
			}
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
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
				{
					Enclosing_Instance.showHideFields();
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
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
				{
					if (Enclosing_Instance.prefix.Checked)
					{
						Enclosing_Instance.suffix.Checked = false;
					}
					Enclosing_Instance.changeLevelName();
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
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
				{
					if (Enclosing_Instance.suffix.Checked)
					{
						Enclosing_Instance.prefix.Checked = false;
					}
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
					names.add(null);
					isPrefix.add(null);
					Enclosing_Instance.images.addEntry();
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
				public virtual void  valueChanged(System.Object event_sender, System.EventArgs evt)
				{
					Enclosing_Instance.updateLevelName();
				}
			}
			private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
			{
				state499 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
			}
			private void  InitBlock()
			{
				System.Windows.Forms.TextBox temp_text_box;
				temp_text_box = new System.Windows.Forms.TextBox();
				temp_text_box.Text = "A";
				activateKeyInput = temp_text_box;
				System.Windows.Forms.TextBox temp_text_box2;
				temp_text_box2 = new System.Windows.Forms.TextBox();
				temp_text_box2.Text = "]";
				upKeyInput = temp_text_box2;
				System.Windows.Forms.TextBox temp_text_box3;
				temp_text_box3 = new System.Windows.Forms.TextBox();
				temp_text_box3.Text = "[";
				downKeyInput = temp_text_box3;
				System.Windows.Forms.TextBox temp_text_box4;
				temp_text_box4 = new System.Windows.Forms.TextBox();
				temp_text_box4.Text = "Activate";
				activateCommand = temp_text_box4;
				System.Windows.Forms.TextBox temp_text_box5;
				temp_text_box5 = new System.Windows.Forms.TextBox();
				temp_text_box5.Text = "Increase";
				upCommand = temp_text_box5;
				System.Windows.Forms.TextBox temp_text_box6;
				temp_text_box6 = new System.Windows.Forms.TextBox();
				temp_text_box6.Text = "Decrease";
				downCommand = temp_text_box6;
				System.Windows.Forms.RadioButton temp_radiobutton;
				temp_radiobutton = new System.Windows.Forms.RadioButton();
				temp_radiobutton.Text = "is prefix";
				prefix = temp_radiobutton;
				System.Windows.Forms.RadioButton temp_radiobutton2;
				temp_radiobutton2 = new System.Windows.Forms.RadioButton();
				temp_radiobutton2.Text = "is suffix";
				suffix = temp_radiobutton2;
			}
			virtual public System.String State
			{
				get
				{
					return alwaysActive.Checked?"1":"-1";
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
						// TODO use IntConfigurer
						//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
						xOffInput.Text = "0";
					}
					
					try
					{
						System.Int32.Parse(yOffInput.Text);
					}
					catch (System.FormatException yNAN)
					{
						// TODO use IntConfigurer
						//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
						yOffInput.Text = "0";
					}
					
					se.append(activateCommand.Text).append(activateModifiers.ValueString).append(activateKeyInput.Text).append(upCommand.Text).append(upModifiers.ValueString).append(upKeyInput.Text).append(downCommand.Text).append(downModifiers.ValueString).append(downKeyInput.Text).append(resetCommand.Text).append((System.Windows.Forms.KeyEventArgs) resetKey.getValue()).append(resetLevel.Text).append(drawUnderneath.Checked).append(xOffInput.Text).append(yOffInput.Text).append(imageNames.toArray(new System.String[imageNames.size()])).append(commonNames.toArray(new System.String[commonNames.size()])).append(loop.Checked).append(name.Text).append((System.Windows.Forms.KeyEventArgs) rndKeyConfig.getValue()).append(rndCommand.Text == null?"":rndCommand.Text.Trim()).append(followConfig.ValueString).append(propertyConfig.ValueString).append(firstLevelConfig.ValueString);
					
					return VassalSharp.counters.Embellishment0.ID + se.Value;
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
			//UPGRADE_NOTE: The initialization of  'activateKeyInput' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private System.Windows.Forms.TextBox activateKeyInput;
			//UPGRADE_NOTE: The initialization of  'upKeyInput' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private System.Windows.Forms.TextBox upKeyInput;
			//UPGRADE_NOTE: The initialization of  'downKeyInput' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private System.Windows.Forms.TextBox downKeyInput;
			//UPGRADE_NOTE: The initialization of  'activateCommand' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private System.Windows.Forms.TextBox activateCommand;
			private KeyModifiersConfigurer activateModifiers = new KeyModifiersConfigurer(null, "  key:  ");
			//UPGRADE_NOTE: The initialization of  'upCommand' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private System.Windows.Forms.TextBox upCommand;
			private KeyModifiersConfigurer upModifiers = new KeyModifiersConfigurer(null, "  key:  ");
			//UPGRADE_NOTE: The initialization of  'downCommand' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private System.Windows.Forms.TextBox downCommand;
			private KeyModifiersConfigurer downModifiers = new KeyModifiersConfigurer(null, "  key:  ");
			// random layers
			//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
			private System.Windows.Forms.TextBox rndCommand = new System.Windows.Forms.TextBox();
			// random layers
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
			private System.Windows.Forms.CheckBox alwaysActive = SupportClass.CheckBoxSupport.CreateCheckBox("Always active?");
			private System.Windows.Forms.CheckBox drawUnderneath = SupportClass.CheckBoxSupport.CreateCheckBox("Underneath when highlighted?");
			//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
			private System.Windows.Forms.TextBox resetLevel = new System.Windows.Forms.TextBox();
			//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
			private System.Windows.Forms.TextBox resetCommand = new System.Windows.Forms.TextBox();
			private System.Windows.Forms.CheckBox loop = SupportClass.CheckBoxSupport.CreateCheckBox("Loop through levels?");
			private HotKeyConfigurer resetKey = new HotKeyConfigurer(null, "  Keyboard:  ");
			//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
			private System.Windows.Forms.TextBox name = new System.Windows.Forms.TextBox();
			
			private System.Windows.Forms.Panel controls;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private List < String > names;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private List < Integer > isPrefix;
			//UPGRADE_NOTE: Final was removed from the declaration of 'NEITHER '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private static readonly System.Int32 NEITHER = Integer.valueOf(0);
			//UPGRADE_NOTE: Final was removed from the declaration of 'PREFIX '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private static readonly System.Int32 PREFIX = Integer.valueOf(1);
			//UPGRADE_NOTE: Final was removed from the declaration of 'SUFFIX '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private static readonly System.Int32 SUFFIX = Integer.valueOf(2);
			// random layers
			private HotKeyConfigurer rndKeyConfig;
			
			private BooleanConfigurer followConfig;
			private StringConfigurer propertyConfig;
			private IntConfigurer firstLevelConfig;
			
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			private Box reset1Controls, reset2Controls;
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			private Box rnd1Controls, rnd2Controls;
			
			public Ed(Embellishment0 e)
			{
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box box;
				
				controls = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				controls.setLayout(new BoxLayout(controls, BoxLayout.Y_AXIS));
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'nameControls '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box nameControls = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = "Name:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				nameControls.Controls.Add(temp_Control);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				nameControls.Controls.Add(name);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(nameControls);
				//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Panel p = new System.Windows.Forms.Panel();
				//UPGRADE_TODO: Constructor 'java.awt.GridLayout.GridLayout' was converted to 'System.Drawing.Rectangle.Rectangle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGridLayoutGridLayout_int_int'"
				//UPGRADE_TODO: Class 'java.awt.GridLayout' was converted to 'System.Drawing.Rectangle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGridLayout'"
				p.Tag = new System.Drawing.Rectangle(5, 3, 0, 0);
				p.Layout += new System.Windows.Forms.LayoutEventHandler(SupportClass.GridLayoutResize);
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(resetKey.Controls);
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
				activateCommand.setMaximumSize(activateCommand.Size);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(activateCommand);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(activateModifiers.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(activateKeyInput);
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
				upCommand.setMaximumSize(upCommand.Size);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(upCommand);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(upModifiers.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(upKeyInput);
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
				downCommand.setMaximumSize(downCommand.Size);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(downCommand);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(downModifiers.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(downKeyInput);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				reset1Controls = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label4;
				temp_label4 = new System.Windows.Forms.Label();
				temp_label4.Text = "Reset to level:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = temp_label4;
				reset1Controls.Controls.Add(temp_Control2);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				reset1Controls.Controls.Add(resetLevel);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(reset1Controls);
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				reset2Controls = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label6;
				temp_label6 = new System.Windows.Forms.Label();
				temp_label6.Text = "  Command:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control3;
				temp_Control3 = temp_label6;
				reset2Controls.Controls.Add(temp_Control3);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				reset2Controls.Controls.Add(resetCommand);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(reset2Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(resetKey.Controls);
				
				// random layer
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				rnd1Controls = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label8;
				temp_label8 = new System.Windows.Forms.Label();
				temp_label8.Text = "Randomize:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control4;
				temp_Control4 = temp_label8;
				rnd1Controls.Controls.Add(temp_Control4);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(rnd1Controls);
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				rnd2Controls = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label10;
				temp_label10 = new System.Windows.Forms.Label();
				temp_label10.Text = "  Command:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control5;
				temp_Control5 = temp_label10;
				rnd2Controls.Controls.Add(temp_Control5);
				//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
				rndCommand = new System.Windows.Forms.TextBox();
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
				rndCommand.setMaximumSize(rndCommand.Size);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				rndCommand.Text = e.rndText;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				rnd2Controls.Controls.Add(rndCommand);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(rnd2Controls);
				rndKeyConfig = new HotKeyConfigurer(null, "  Keyboard:  ", e.rndKey);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(rndKeyConfig.Controls);
				// end random layer
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				box = Box.createVerticalBox();
				alwaysActive.CheckedChanged += new System.EventHandler(new AnonymousClassItemListener(this).itemStateChanged);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'checkBoxes '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Panel checkBoxes = new System.Windows.Forms.Panel();
				//UPGRADE_TODO: Constructor 'java.awt.GridLayout.GridLayout' was converted to 'System.Drawing.Rectangle.Rectangle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGridLayoutGridLayout_int_int'"
				//UPGRADE_TODO: Class 'java.awt.GridLayout' was converted to 'System.Drawing.Rectangle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGridLayout'"
				checkBoxes.Tag = new System.Drawing.Rectangle(3, 2, 0, 0);
				checkBoxes.Layout += new System.Windows.Forms.LayoutEventHandler(SupportClass.GridLayoutResize);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				checkBoxes.Controls.Add(alwaysActive);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				checkBoxes.Controls.Add(drawUnderneath);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				checkBoxes.Controls.Add(loop);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(checkBoxes);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(p);
				
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
				System.Windows.Forms.Label temp_label12;
				temp_label12 = new System.Windows.Forms.Label();
				temp_label12.Text = "Offset: ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control6;
				temp_Control6 = temp_label12;
				offsetControls.Controls.Add(temp_Control6);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				offsetControls.Controls.Add(xOffInput);
				System.Windows.Forms.Label temp_label14;
				temp_label14 = new System.Windows.Forms.Label();
				temp_label14.Text = ",";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control7;
				temp_Control7 = temp_label14;
				offsetControls.Controls.Add(temp_Control7);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				offsetControls.Controls.Add(yOffInput);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				checkBoxes.Controls.Add(offsetControls);
				
				followConfig = new BooleanConfigurer(null, "Levels follow Property Value?");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				checkBoxes.Controls.Add(followConfig.Controls);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'levelBox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box levelBox = Box.createHorizontalBox();
				propertyConfig = new StringConfigurer(null, "Property Name:  ");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				levelBox.Controls.Add(propertyConfig.Controls);
				firstLevelConfig = new IntConfigurer(null, " Level 1 = ", e.firstLevelValue);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				levelBox.Controls.Add(firstLevelConfig.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				checkBoxes.Controls.Add(levelBox);
				followConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(box);
				
				images = new MultiImagePicker();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(images);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'p2 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Panel p2 = new System.Windows.Forms.Panel();
				//UPGRADE_TODO: Constructor 'java.awt.GridLayout.GridLayout' was converted to 'System.Drawing.Rectangle.Rectangle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGridLayoutGridLayout_int_int'"
				//UPGRADE_TODO: Class 'java.awt.GridLayout' was converted to 'System.Drawing.Rectangle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGridLayout'"
				p2.Tag = new System.Drawing.Rectangle(2, 2, 0, 0);
				p2.Layout += new System.Windows.Forms.LayoutEventHandler(SupportClass.GridLayoutResize);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				box = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label16;
				temp_label16 = new System.Windows.Forms.Label();
				temp_label16.Text = "Level Name:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control8;
				temp_Control8 = temp_label16;
				box.Controls.Add(temp_Control8);
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
				levelNameInput.setMaximumSize(levelNameInput.Size);
				levelNameInput.KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.counters.Embellishment0.Ed.keyDown);
				levelNameInput.KeyUp += new System.Windows.Forms.KeyEventHandler(new AnonymousClassKeyAdapter(this).keyReleased);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(levelNameInput);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p2.Controls.Add(box);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				box = Box.createHorizontalBox();
				prefix.CheckedChanged += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(prefix);
				suffix.CheckedChanged += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(suffix);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(prefix);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(suffix);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p2.Controls.Add(box);
				
				System.Windows.Forms.Button b = SupportClass.ButtonSupport.CreateStandardButton("Add Level");
				b.Click += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(b);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p2.Controls.Add(b);
				b = SupportClass.ButtonSupport.CreateStandardButton("Remove Level");
				b.Click += new System.EventHandler(new AnonymousClassActionListener3(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(b);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p2.Controls.Add(b);
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(p2);
				
				images.List.SelectedValueChanged += new System.EventHandler(new AnonymousClassListSelectionListener(this).valueChanged);
				
				reset(e);
			}
			
			/*
			* Change visibility of fields depending on the Follow Property setting
			*/
			protected internal virtual void  showHideFields()
			{
				bool show = !followConfig.booleanValue();
				loop.Enabled = show;
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(propertyConfig.Controls, "Visible", !show);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(firstLevelConfig.Controls, "Visible", !show);
				reset1Controls.Visible = show;
				reset2Controls.Visible = show;
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(resetKey.Controls, "Visible", show);
				rnd1Controls.Visible = show;
				rnd2Controls.Visible = show;
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(rndKeyConfig.Controls, "Visible", show);
				upCommand.Visible = show;
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(upModifiers.Controls, "Visible", show);
				upKeyInput.Visible = show;
				downCommand.Visible = show;
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(downModifiers.Controls, "Visible", show);
				downKeyInput.Visible = show;
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
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Deprecated
			public virtual System.String oldgetType()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'imageList '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SequenceEncoder imageList = new SequenceEncoder(';');
				int i = 0;
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(String imageName: images.getImageNameList())
				{
					System.String commonName = names.get_Renamed(i);
					if (names.get_Renamed(i) != null && commonName != null && commonName.Length > 0)
					{
						SequenceEncoder sub = new SequenceEncoder(Enclosing_Instance.imageName, ',');
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
						imageList.append(sub.append(commonName).Value);
					}
					else
					{
						imageList.append(Enclosing_Instance.imageName);
					}
					i++;
				}
				
				try
				{
					System.Int32.Parse(xOffInput.Text);
				}
				catch (System.FormatException xNAN)
				{
					// TODO use IntConfigurer
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
					xOffInput.Text = "0";
				}
				try
				{
					System.Int32.Parse(yOffInput.Text);
				}
				catch (System.FormatException yNAN)
				{
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
					yOffInput.Text = "0";
				}
				System.String command = activateCommand.Text;
				if (drawUnderneath.Checked)
				{
					command = "_" + command;
				}
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'se2 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SequenceEncoder se2 = new SequenceEncoder(activateKeyInput.Text, ';');
				se2.append(resetCommand.Text).append((System.Windows.Forms.KeyEventArgs) resetKey.getValue()).append(resetLevel.Text);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SequenceEncoder se = new SequenceEncoder(null, ';');
				se.append(se2.Value).append(command).append(upKeyInput.Text).append(upCommand.Text).append(downKeyInput.Text).append(downCommand.Text).append(xOffInput.Text).append(yOffInput.Text);
				
				System.String type = VassalSharp.counters.Embellishment0.ID + se.Value + ';' + (imageList.Value == null?"":imageList.Value);
				return type;
			}
			
			public virtual void  reset(Embellishment0 e)
			{
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				name.Text = e.name;
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
				
				alwaysActive.Checked = e.activateKey.Length == 0;
				drawUnderneath.Checked = e.drawUnderneathWhenSelected;
				loop.Checked = e.loopLevels;
				
				images.clear();
				
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				activateKeyInput.Text = e.activateKey;
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				activateCommand.Text = e.activateCommand;
				activateModifiers.setValue(Integer.valueOf(e.activateModifiers));
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				upKeyInput.Text = e.upKey;
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				upCommand.Text = e.upCommand;
				upModifiers.setValue(Integer.valueOf(e.upModifiers));
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				downKeyInput.Text = e.downKey;
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				downCommand.Text = e.downCommand;
				downModifiers.setValue(Integer.valueOf(e.downModifiers));
				resetKey.setValue(e.resetKey);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				resetCommand.Text = e.resetCommand;
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				resetLevel.Text = e.resetLevel.Format;
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