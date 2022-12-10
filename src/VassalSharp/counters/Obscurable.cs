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
using GameModule = VassalSharp.build.GameModule;
using ObscurableOptions = VassalSharp.build.module.ObscurableOptions;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using ChangeTracker = VassalSharp.command.ChangeTracker;
using Command = VassalSharp.command.Command;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using PieceAccessConfigurer = VassalSharp.configure.PieceAccessConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using StringEnumConfigurer = VassalSharp.configure.StringEnumConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using ArrayUtils = VassalSharp.tools.ArrayUtils;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	public class Obscurable:Decorator, TranslatablePiece
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(Obscurable enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Obscurable enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Obscurable enclosingInstance;
			public Obscurable Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				KeyBuffer.Buffer.remove(Decorator.getOutermost(Enclosing_Instance));
			}
		}
		private void  InitBlock()
		{
			access = PlayerAccess.Instance;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final ArrayList < String > l = new ArrayList < String >();
			l.add(VassalSharp.counters.Properties_Fields.OBSCURED_TO_OTHERS);
			l.add(VassalSharp.counters.Properties_Fields.OBSCURED_TO_ME);
			return l;
		}
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		override public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				if (obscuredToMe())
				{
					return obscuredToMeView.Shape;
				}
				else if (obscuredToOthers())
				{
					switch (displayStyle)
					{
						
						case BACKGROUND: 
							return obscuredToMeView.Shape;
						
						case INSET: 
							return piece.Shape;
						
						case PEEK: 
							if (peeking && true.Equals(getProperty(VassalSharp.counters.Properties_Fields.SELECTED)))
							{
								return piece.Shape;
							}
							else
							{
								return obscuredToMeView.Shape;
							}
							goto case IMAGE;
						
						case IMAGE: 
							//UPGRADE_NOTE: Final was removed from the declaration of 'area '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							System.Drawing.Region area = new System.Drawing.Region(obscuredToOthersView.Shape);
							//UPGRADE_NOTE: Final was removed from the declaration of 'innerShape '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
							System.Drawing.Drawing2D.GraphicsPath innerShape = piece.Shape;
							if (innerShape is System.Drawing.Region)
							{
								//UPGRADE_TODO: Method 'java.awt.geom.Area.add' was converted to 'System.Drawing.Region.Union' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtgeomAreaadd_javaawtgeomArea'"
								area.Union((System.Drawing.Region) innerShape);
							}
							else
							{
								//UPGRADE_TODO: Method 'java.awt.geom.Area.add' was converted to 'System.Drawing.Region.Union' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtgeomAreaadd_javaawtgeomArea'"
								area.Union(new System.Drawing.Region(innerShape));
							}
							return area;
						
						default: 
							return piece.Shape;
						
					}
				}
				else
				{
					return piece.Shape;
				}
			}
			
		}
		/// <summary>Return true if the piece is currently being "peeked at" </summary>
		virtual public bool Peeking
		{
			get
			{
				return peeking;
			}
			
		}
		override public System.String LocalizedName
		{
			get
			{
				System.String maskedName = maskName == null?"?":maskName;
				maskedName = getTranslation(maskedName);
				return getName(maskedName, true);
			}
			
		}
		/// <summary> Return true if this piece can be masked/unmasked by the current player</summary>
		virtual public bool Maskable
		{
			get
			{
				// Check if piece is owned by us. Returns true if we own the piece, or if it
				// is not currently masked
				if (access.currentPlayerCanModify(obscuredBy))
				{
					return true;
				}
				
				// Check ObscurableOptions in play when piece was Obscured
				if (obscuredOptions != null)
				{
					return obscuredOptions.isUnmaskable(obscuredBy);
				}
				
				// Fall-back, use current global ObscurableOptions
				return ObscurableOptions.Instance.isUnmaskable(obscuredBy);
			}
			
		}
		virtual public System.String Description
		{
			get
			{
				return "Mask";
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("Mask.htm");
			}
			
		}
		public static bool AllHidden
		{
			set
			{
				if (value)
				{
					VassalSharp.counters.GlobalAccess.hideAll();
				}
				else
				{
					VassalSharp.counters.GlobalAccess.revertAll();
				}
			}
			
		}
		public const System.String ID = "obs;";
		protected internal const char INSET = 'I';
		protected internal const char BACKGROUND = 'B';
		protected internal const char PEEK = 'P';
		protected internal const char IMAGE = 'G';
		protected internal const System.String DEFAULT_PEEK_COMMAND = "Peek";
		
		protected internal char obscureKey;
		protected internal NamedKeyStroke keyCommand;
		protected internal NamedKeyStroke peekKey;
		protected internal System.String imageName;
		protected internal System.String obscuredToOthersImage;
		protected internal System.String obscuredBy;
		protected internal ObscurableOptions obscuredOptions;
		protected internal System.String hideCommand = "Mask";
		protected internal System.String peekCommand = DEFAULT_PEEK_COMMAND;
		protected internal GamePiece obscuredToMeView;
		protected internal GamePiece obscuredToOthersView;
		protected internal bool peeking;
		protected internal char displayStyle = INSET; // I = inset, B = background
		protected internal System.String maskName = "?";
		//UPGRADE_NOTE: The initialization of  'access' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal PieceAccess access;
		
		protected internal KeyCommand[] commandsWithPeek;
		protected internal KeyCommand[] commandsWithoutPeek;
		protected internal KeyCommand hide;
		protected internal KeyCommand peek;
		
		public Obscurable():this(ID + "M;", null)
		{
		}
		
		public Obscurable(System.String type, GamePiece d)
		{
			InitBlock();
			mySetType(type);
			setInner(d);
		}
		
		public override void  mySetState(System.String in_Renamed)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'sd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(in_Renamed, ';');
			System.String token = sd.nextToken("null");
			obscuredBy = "null".Equals(token)?null:token;
			token = sd.nextToken("");
			obscuredOptions = (obscuredBy == null?null:new ObscurableOptions(token));
		}
		
		public virtual void  mySetType(System.String in_Renamed)
		{
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(in_Renamed, ';');
			st.nextToken();
			keyCommand = st.nextNamedKeyStroke(null);
			imageName = st.nextToken();
			obscuredToMeView = GameModule.getGameModule().createPiece(BasicPiece.ID + ";;" + imageName + ";;");
			hideCommand = st.nextToken(hideCommand);
			if (st.hasMoreTokens())
			{
				System.String s = st.nextToken(System.Convert.ToString(INSET));
				displayStyle = s[0];
				switch (displayStyle)
				{
					
					case PEEK: 
						if (s.Length > 1)
						{
							if (s.Length == 2)
							{
								peekKey = NamedKeyStroke.getNamedKeyStroke(s[1], (int) System.Windows.Forms.Keys.Control);
							}
							else
							{
								peekKey = NamedHotKeyConfigurer.decode(s.Substring(1));
							}
							peeking = false;
						}
						else
						{
							peekKey = null;
							peeking = true;
						}
						break;
					
					case IMAGE: 
						if (s.Length > 1)
						{
							obscuredToOthersImage = s.Substring(1);
							obscuredToOthersView = GameModule.getGameModule().createPiece(BasicPiece.ID + ";;" + obscuredToOthersImage + ";;");
							obscuredToMeView.Position = new System.Drawing.Point(0, 0);
						}
						break;
					}
			}
			maskName = st.nextToken(maskName);
			access = PieceAccessConfigurer.decode(st.nextToken(null));
			peekCommand = st.nextToken(DEFAULT_PEEK_COMMAND);
			commandsWithPeek = null;
			hide = null;
			peek = null;
		}
		
		public override System.String myGetType()
		{
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(keyCommand).append(imageName).append(hideCommand);
			switch (displayStyle)
			{
				
				case PEEK: 
					if (peekKey == null)
					{
						se.append(displayStyle);
					}
					else
					{
						se.append(displayStyle + NamedHotKeyConfigurer.encode(peekKey));
					}
					break;
				
				case IMAGE: 
					se.append(displayStyle + obscuredToOthersImage);
					break;
				
				default: 
					se.append(displayStyle);
					break;
				
			}
			se.append(maskName);
			se.append(PieceAccessConfigurer.encode(access));
			se.append(peekCommand);
			return ID + se.Value;
		}
		
		public override System.String myGetState()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(obscuredBy == null?"null":obscuredBy);
			se.append((obscuredBy == null || obscuredOptions == null)?"":obscuredOptions.encodeOptions());
			return se.Value;
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			if (obscuredToMe())
			{
				return bBoxObscuredToMe();
			}
			else if (obscuredToOthers())
			{
				return bBoxObscuredToOthers();
			}
			else
			{
				return piece.boundingBox();
			}
		}
		
		public virtual bool obscuredToMe()
		{
			return !access.currentPlayerHasAccess(obscuredBy);
		}
		
		public virtual bool obscuredToOthers()
		{
			return obscuredBy != null;
		}
		
		public override void  setProperty(System.Object key, System.Object val)
		{
			if (ID.Equals(key))
			{
				if (val is System.String || val == null)
				{
					obscuredBy = ((System.String) val);
					if ("null".Equals(obscuredBy))
					{
						obscuredBy = null;
						obscuredOptions = null;
					}
				}
			}
			else if (VassalSharp.counters.Properties_Fields.SELECTED.Equals(key))
			{
				if (!true.Equals(val) && peekKey != null)
				{
					peeking = false;
				}
				base.setProperty(key, val);
			}
			else if (VassalSharp.counters.Properties_Fields.OBSCURED_TO_OTHERS.Equals(key))
			{
				System.String owner = null;
				if (true.Equals(val))
				{
					owner = access.CurrentPlayerId;
				}
				obscuredBy = owner;
				obscuredOptions = new ObscurableOptions(ObscurableOptions.Instance.encodeOptions());
			}
			else
			{
				base.setProperty(key, val);
			}
		}
		
		public override System.Object getProperty(System.Object key)
		{
			if (VassalSharp.counters.Properties_Fields.OBSCURED_TO_ME.Equals(key))
			{
				return Boolean.valueOf(obscuredToMe());
			}
			else if (VassalSharp.counters.Properties_Fields.OBSCURED_TO_OTHERS.Equals(key))
			{
				return Boolean.valueOf(obscuredToOthers());
			}
			else if (ID.Equals(key))
			{
				return obscuredBy;
			}
			else if (VassalSharp.counters.Properties_Fields.VISIBLE_STATE.Equals(key))
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return myGetState() + Peeking + piece.getProperty(key);
			}
			// FIXME: Access to Obscured properties
			// If piece is obscured to me, then mask any properties returned by
			// traits between this one and the innermost BasicPiece. Return directly
			// any properties normally handled by Decorator.getproperty()
			// Global Key Commands acting on Decks over-ride the masking by calling
			// setExposeMaskedProperties()
			//    else if (obscuredToMe() && ! exposeMaskedProperties) {
			//      if (Properties.KEY_COMMANDS.equals(key)) {
			//        return getKeyCommands();
			//      }
			//      else if (Properties.INNER.equals(key)) {
			//        return piece;
			//      }
			//      else if (Properties.OUTER.equals(key)) {
			//        return getOuter();
			//      }
			//      else if (Properties.VISIBLE_STATE.equals(key)) {
			//        return myGetState();
			//      }
			//      else {
			//        return ((BasicPiece) Decorator.getInnermost(this)).getPublicProperty(key);
			//      }
			//    }
			else
			{
				return base.getProperty(key);
			}
		}
		
		public override System.Object getLocalizedProperty(System.Object key)
		{
			if (obscuredToMe())
			{
				return ((BasicPiece) Decorator.getInnermost(this)).getLocalizedPublicProperty(key);
			}
			else if (VassalSharp.counters.Properties_Fields.OBSCURED_TO_ME.Equals(key))
			{
				return Boolean.valueOf(obscuredToMe());
			}
			else if (VassalSharp.counters.Properties_Fields.OBSCURED_TO_OTHERS.Equals(key))
			{
				return Boolean.valueOf(obscuredToOthers());
			}
			else if (ID.Equals(key))
			{
				return obscuredBy;
			}
			else if (VassalSharp.counters.Properties_Fields.VISIBLE_STATE.Equals(key))
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return myGetState() + Peeking + piece.getProperty(key);
			}
			else
			{
				return base.getLocalizedProperty(key);
			}
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			if (obscuredToMe())
			{
				drawObscuredToMe(g, x, y, obs, zoom);
			}
			else if (obscuredToOthers())
			{
				drawObscuredToOthers(g, x, y, obs, zoom);
			}
			else
			{
				piece.draw(g, x, y, obs, zoom);
			}
		}
		
		protected internal virtual void  drawObscuredToMe(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			obscuredToMeView.draw(g, x, y, obs, zoom);
		}
		
		protected internal virtual void  drawObscuredToOthers(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			switch (displayStyle)
			{
				
				case BACKGROUND: 
					obscuredToMeView.draw(g, x, y, obs, zoom);
					piece.draw(g, x, y, obs, zoom * .5);
					break;
				
				case INSET: 
					piece.draw(g, x, y, obs, zoom);
					System.Drawing.Rectangle bounds = System.Drawing.Rectangle.Truncate(piece.Shape.GetBounds());
					System.Drawing.Rectangle obsBounds = System.Drawing.Rectangle.Truncate(obscuredToMeView.Shape.GetBounds());
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					obscuredToMeView.draw(g, x - (int) (zoom * bounds.Width / 2 - .5 * zoom * obsBounds.Width / 2), y - (int) (zoom * bounds.Height / 2 - .5 * zoom * obsBounds.Height / 2), obs, zoom * 0.5);
					break;
				
				case PEEK: 
					if (peeking && true.Equals(getProperty(VassalSharp.counters.Properties_Fields.SELECTED)))
					{
						piece.draw(g, x, y, obs, zoom);
					}
					else
					{
						obscuredToMeView.draw(g, x, y, obs, zoom);
					}
					break;
				
				case IMAGE: 
					piece.draw(g, x, y, obs, zoom);
					obscuredToOthersView.draw(g, x, y, obs, zoom);
					break;
				}
		}
		
		protected internal virtual System.Drawing.Rectangle bBoxObscuredToMe()
		{
			return obscuredToMeView.boundingBox();
		}
		
		protected internal virtual System.Drawing.Rectangle bBoxObscuredToOthers()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Rectangle r;
			switch (displayStyle)
			{
				
				case BACKGROUND: 
					r = bBoxObscuredToMe();
					break;
				
				case IMAGE: 
					r = piece.boundingBox();
					SupportClass.RectangleSupport.AddRectangleToRectangle(ref r, obscuredToOthersView.boundingBox());
					break;
				
				default: 
					r = piece.boundingBox();
					break;
				
			}
			return r;
		}
		
		public override System.String getName()
		{
			System.String maskedName = maskName == null?"?":maskName;
			return getName(maskedName, false);
		}
		
		protected internal virtual System.String getName(System.String maskedName, bool localized)
		{
			if (obscuredToMe())
			{
				return maskedName;
			}
			else if (obscuredToOthers())
			{
				return (localized?piece.LocalizedName:piece.getName()) + "(" + maskedName + ")";
			}
			else
			{
				return (localized?piece.LocalizedName:piece.getName());
			}
		}
		
		public override KeyCommand[] getKeyCommands()
		{
			if (obscuredToMe())
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'myC '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				KeyCommand[] myC = myGetKeyCommands();
				//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				KeyCommand[] c = (KeyCommand[]) Decorator.getInnermost(this).getProperty(VassalSharp.counters.Properties_Fields.KEY_COMMANDS);
				
				if (c == null)
					return myC;
				else
					return ArrayUtils.append(typeof(KeyCommand[]), myC, c);
			}
			else
			{
				return base.getKeyCommands();
			}
		}
		
		public override KeyCommand[] myGetKeyCommands()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < KeyCommand > l = new ArrayList < KeyCommand >();
			GamePiece outer = Decorator.getOutermost(this);
			
			// Hide Command
			if (keyCommand == null)
			{
				// Backwards compatibility with VASL classes
				keyCommand = NamedKeyStroke.getNamedKeyStroke(obscureKey, (int) System.Windows.Forms.Keys.Control);
			}
			
			hide = new KeyCommand(hideCommand, keyCommand, outer, this);
			if (hideCommand.Length > 0 && Maskable)
			{
				l.add(hide);
				commandsWithoutPeek = new KeyCommand[]{hide};
			}
			else
			{
				commandsWithoutPeek = new KeyCommand[0];
			}
			
			// Peek Command
			peek = new KeyCommand(peekCommand, peekKey, outer, this);
			if (displayStyle == PEEK && peekKey != null && peekCommand.Length > 0)
			{
				l.add(peek);
			}
			
			commandsWithPeek = l.toArray(new KeyCommand[l.size()]);
			
			return obscuredToOthers() && Maskable && displayStyle == PEEK && peekKey != null?commandsWithPeek:commandsWithoutPeek;
		}
		
		/// <summary> Return true if this piece can be masked/unmasked by the current player</summary>
		/// <param name="id">ignored
		/// </param>
		/// <deprecated>
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		public virtual bool isMaskableBy(System.String id)
		{
			return Maskable;
		}
		
		public override Command keyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			if (!obscuredToMe())
			{
				return base.keyEvent(stroke);
			}
			else if (Maskable)
			{
				return myKeyEvent(stroke);
			}
			else
			{
				return null;
			}
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			Command retVal = null;
			myGetKeyCommands();
			
			if (hide.matches(stroke))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				ChangeTracker c = new ChangeTracker(this);
				if (obscuredToOthers() || obscuredToMe())
				{
					obscuredBy = null;
					obscuredOptions = null;
				}
				else if (!obscuredToMe())
				{
					obscuredBy = access.CurrentPlayerId;
					obscuredOptions = new ObscurableOptions(ObscurableOptions.Instance.encodeOptions());
				}
				
				retVal = c.ChangeCommand;
			}
			else if (peek.matches(stroke))
			{
				if (obscuredToOthers() && true.Equals(getProperty(VassalSharp.counters.Properties_Fields.SELECTED)))
				{
					peeking = true;
				}
			}
			
			// For the "peek" display style with no key command (i.e. appears
			// face-up whenever selected).
			//
			// It looks funny if we turn something face down but we can still see it.
			// Therefore, un-select the piece if turning it face down
			if (retVal != null && PEEK == displayStyle && peekKey == null && obscuredToOthers())
			{
				// FIXME: This probably causes a race condition. Can we do this directly?
				IThreadRunnable runnable = new AnonymousClassRunnable(this);
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				SwingUtilities.invokeLater(runnable);
			}
			return retVal;
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		/// <summary> If true, then all masked pieces are considered masked to all players.
		/// Used to temporarily draw pieces as they appear to other players
		/// </summary>
		/// <param name="allHidden">
		/// </param>
		/// <deprecated>
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		
		public override PieceI18nData getI18nData()
		{
			return getI18nData(new System.String[]{hideCommand, maskName, peekCommand}, new System.String[]{"Mask command", "Name when masked", "Peek command"});
		}
		
		/// <summary> Return Property names exposed by this trait</summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getPropertyNames()
		
		private class Ed : PieceEditor
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassJPanel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			[Serializable]
			private class AnonymousClassJPanel:System.Windows.Forms.Panel
			{
				public AnonymousClassJPanel(Ed enclosingInstance)
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
				private const long serialVersionUID = 1L;
				
				//UPGRADE_NOTE: The equivalent of method 'javax.swing.JComponent.getMinimumSize' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public System.Drawing.Size getMinimumSize()
				{
					return new System.Drawing.Size(60, 60);
				}
				
				//UPGRADE_NOTE: The equivalent of method 'javax.swing.JComponent.getPreferredSize' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public System.Drawing.Size getPreferredSize()
				{
					return new System.Drawing.Size(60, 60);
				}
				
				protected override void  OnPaint(System.Windows.Forms.PaintEventArgs g_EventArg)
				{
					System.Drawing.Graphics g = null;
					if (g_EventArg != null)
						g = g_EventArg.Graphics;
					g.FillRegion(new System.Drawing.SolidBrush(SupportClass.GraphicsManager.manager.GetBackColor(g)), new System.Drawing.Region(new System.Drawing.Rectangle(0, 0, Width, Height)));
					switch (Enclosing_Instance.displayOption.ValueString[0])
					{
						
						case VassalSharp.counters.Obscurable.BACKGROUND: 
							SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.Black);
							g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), 0, 0, 60, 60);
							SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.White);
							g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), 15, 15, 30, 30);
							break;
						
						case VassalSharp.counters.Obscurable.INSET: 
							SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.White);
							g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), 0, 0, 60, 60);
							SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.Black);
							g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), 0, 0, 30, 30);
							break;
						
						case VassalSharp.counters.Obscurable.PEEK: 
							SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.Black);
							g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), 0, 0, 60, 60);
							break;
						}
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener
			{
				public AnonymousClassPropertyChangeListener(System.Windows.Forms.Panel showDisplayOption, Ed enclosingInstance)
				{
					InitBlock(showDisplayOption, enclosingInstance);
				}
				private void  InitBlock(System.Windows.Forms.Panel showDisplayOption, Ed enclosingInstance)
				{
					this.showDisplayOption = showDisplayOption;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable showDisplayOption was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private System.Windows.Forms.Panel showDisplayOption;
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
					//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
					showDisplayOption.Refresh();
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(Enclosing_Instance.peekKeyInput.Controls, "Visible", Enclosing_Instance.optionNames[1].Equals(evt.NewValue));
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(Enclosing_Instance.peekCommandInput.Controls, "Visible", Enclosing_Instance.optionNames[1].Equals(evt.NewValue));
					Enclosing_Instance.imagePicker.Visible = Enclosing_Instance.optionNames[3].Equals(evt.NewValue);
					//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
					System.Windows.Forms.Form w = (System.Windows.Forms.Form) Enclosing_Instance.controls.Parent;
					if (w != null)
					{
						//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
						w.pack();
					}
				}
			}
			private void  InitBlock()
			{
				optionChars = new char[]{VassalSharp.counters.Obscurable.BACKGROUND, VassalSharp.counters.Obscurable.PEEK, VassalSharp.counters.Obscurable.INSET, VassalSharp.counters.Obscurable.IMAGE};
			}
			virtual public System.String State
			{
				get
				{
					return "null";
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					SequenceEncoder se = new SequenceEncoder(';');
					se.append(obscureKeyInput.ValueString).append(picker.getImageName()).append(obscureCommandInput.ValueString);
					char optionChar = VassalSharp.counters.Obscurable.INSET;
					for (int i = 0; i < optionNames.Length; ++i)
					{
						if (optionNames[i].Equals(displayOption.ValueString))
						{
							optionChar = optionChars[i];
							break;
						}
					}
					switch (optionChar)
					{
						
						case VassalSharp.counters.Obscurable.PEEK: 
							System.String valueString = peekKeyInput.ValueString;
							if (valueString != null)
							{
								se.append(optionChar + valueString);
							}
							else
							{
								se.append(optionChar);
							}
							break;
						
						case VassalSharp.counters.Obscurable.IMAGE: 
							se.append(optionChar + imagePicker.getImageName());
							break;
						
						default: 
							se.append(optionChar);
							break;
						
					}
					se.append(maskNameInput.ValueString);
					se.append(accessConfig.ValueString);
					se.append(peekCommandInput.ValueString);
					return VassalSharp.counters.Obscurable.ID + se.Value;
				}
				
			}
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return controls;
				}
				
			}
			private ImagePicker picker;
			private NamedHotKeyConfigurer obscureKeyInput;
			private StringConfigurer obscureCommandInput, maskNameInput;
			private StringEnumConfigurer displayOption;
			private NamedHotKeyConfigurer peekKeyInput;
			private StringConfigurer peekCommandInput;
			private System.Windows.Forms.Panel controls = new System.Windows.Forms.Panel();
			private System.String[] optionNames = new System.String[]{"Background", "Plain", "Inset", "Use Image"};
			//UPGRADE_NOTE: The initialization of  'optionChars' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private char[] optionChars;
			private ImagePicker imagePicker;
			private PieceAccessConfigurer accessConfig;
			
			public Ed(Obscurable p)
			{
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				controls.setLayout(new BoxLayout(controls, BoxLayout.Y_AXIS));
				
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box box = Box.createHorizontalBox();
				obscureCommandInput = new StringConfigurer(null, "Mask Command:  ", p.hideCommand);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(obscureCommandInput.Controls);
				obscureKeyInput = new NamedHotKeyConfigurer(null, "  Keyboard Command:  ", p.keyCommand);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(obscureKeyInput.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(box);
				
				accessConfig = new PieceAccessConfigurer(null, "Can be masked by:  ", p.access);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(accessConfig.Controls);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				box = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = "View when masked: ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				box.Controls.Add(temp_Control);
				picker = new ImagePicker();
				picker.setImageName(p.imageName);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(picker);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(box);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				box = Box.createHorizontalBox();
				maskNameInput = new StringConfigurer(null, "Name when masked:  ", p.maskName);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(maskNameInput.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(box);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				box = Box.createHorizontalBox();
				displayOption = new StringEnumConfigurer(null, "Display style:  ", optionNames);
				for (int i = 0; i < optionNames.Length; ++i)
				{
					if (p.displayStyle == optionChars[i])
					{
						displayOption.setValue(optionNames[i]);
						break;
					}
				}
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(displayOption.Controls);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'showDisplayOption '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Panel showDisplayOption = new AnonymousClassJPanel(this);
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(showDisplayOption);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(box);
				
				peekKeyInput = new NamedHotKeyConfigurer(null, "Peek Key:  ", p.peekKey);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(peekKeyInput.Controls, "Visible", p.displayStyle == VassalSharp.counters.Obscurable.PEEK);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(peekKeyInput.Controls);
				
				peekCommandInput = new StringConfigurer(null, "Peek Command:  ", p.peekCommand);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(peekCommandInput.Controls, "Visible", p.displayStyle == VassalSharp.counters.Obscurable.PEEK);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(peekCommandInput.Controls);
				
				imagePicker = new ImagePicker();
				imagePicker.setImageName(p.obscuredToOthersImage);
				imagePicker.Visible = p.displayStyle == VassalSharp.counters.Obscurable.IMAGE;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(imagePicker);
				
				displayOption.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(showDisplayOption, this).propertyChange);
			}
		}
	}
}