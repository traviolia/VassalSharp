/*
* $Id$
*
* Copyright (c) 2000-2012 by Rodney Kinney, Brent Easton
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
using BadDataReport = VassalSharp.build.BadDataReport;
using Map = VassalSharp.build.module.Map;
using Board = VassalSharp.build.module.map.boardPicker.Board;
using Zone = VassalSharp.build.module.map.boardPicker.board.mapgrid.Zone;
using PropertyNameSource = VassalSharp.build.module.properties.PropertyNameSource;
using Command = VassalSharp.command.Command;
using Localization = VassalSharp.i18n.Localization;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using ArrayUtils = VassalSharp.tools.ArrayUtils;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> The abstract class describing a generic 'trait' of a GamePiece.  Follows the Decorator design pattern
	/// of wrapping around another instance of GamePiece (the 'inner' piece) and delegating some of the GamePiece methods to it
	/// </summary>
	public abstract class Decorator : GamePiece, StateMergeable, PropertyNameSource
	{
		private void  InitBlock()
		{
			while (p is Decorator)
			{
				if (type.isInstance(p))
				{
					return p;
				}
				p = ((Decorator) p).piece;
			}
			return null;
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new ArrayList < String >(0);
		}
		virtual public Stack Parent
		{
			get
			{
				return piece.Parent;
			}
			
			set
			{
				piece.Parent = value;
			}
			
		}
		virtual public Decorator Outer
		{
			/*
			* getOuter() required by Obscurable to handle masking of getProperty calls
			*/
			
			get
			{
				return dec;
			}
			
		}
		virtual public System.Drawing.Point Position
		{
			get
			{
				return piece.Position;
			}
			
			set
			{
				piece.Position = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary>The state of a Decorator is a composition of {@link #myGetState} and the inner piece's state </summary>
		/// <summary> Extract the string describing this trait's state and forward the remaining string to the inner piece</summary>
		/// <param name="newState">the new state of this trait and all inner pieces
		/// </param>
		virtual public System.String State
		{
			get
			{
				SequenceEncoder se = new SequenceEncoder(myGetState(), '\t');
				se.append(piece.State);
				return se.Value;
			}
			
			set
			{
				SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(value, '\t');
				mySetState(st.nextToken());
				try
				{
					piece.State = st.nextToken();
				}
				catch (System.ArgumentOutOfRangeException e)
				{
					throw new System.SystemException("No state for Decorator=" + myGetType());
				}
			}
			
		}
		/// <summary> The type of a Decorator is a composition of {@link #myGetType} and the type of its inner piece</summary>
		/// <returns> the combined type of this trait and its inner piece
		/// </returns>
		virtual public System.String Type
		{
			get
			{
				SequenceEncoder se = new SequenceEncoder(myGetType(), '\t');
				se.append(piece.Type);
				return se.Value;
			}
			
		}
		virtual public System.String Id
		{
			get
			{
				return piece.Id;
			}
			
			set
			{
				piece.Id = value;
			}
			
		}
		/// <summary> Return the translated name for this piece. Most pieces do not have
		/// translatable elements, so just return the standard name
		/// </summary>
		virtual public System.String LocalizedName
		{
			get
			{
				return piece.LocalizedName;
			}
			
		}
		/// <summary> Support Selection status locally</summary>
		virtual protected internal bool Selected
		{
			get
			{
				return selected;
			}
			
			
			
			set
			{
				selected = value;
			}
			
		}
		public abstract System.String Name{get;}
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public abstract System.Drawing.Drawing2D.GraphicsPath Shape{get;}
		protected internal GamePiece piece;
		private Decorator dec;
		private bool selected = false;
		
		public Decorator()
		{
			InitBlock();
		}
		
		/// <summary>Set the inner GamePiece </summary>
		public virtual void  setInner(GamePiece p)
		{
			piece = p;
			if (p != null)
			{
				p.setProperty(VassalSharp.counters.Properties_Fields.OUTER, this);
			}
		}
		
		public virtual void  setMap(Map m)
		{
			piece.setMap(m);
		}
		
		/// <returns> the piece decorated by this Decorator
		/// </returns>
		public virtual GamePiece getInner()
		{
			return piece;
		}
		
		public virtual Map getMap()
		{
			return piece.getMap();
		}
		
		public virtual System.Object getProperty(System.Object key)
		{
			if (VassalSharp.counters.Properties_Fields.KEY_COMMANDS.Equals(key))
			{
				return getKeyCommands();
			}
			else if (VassalSharp.counters.Properties_Fields.INNER.Equals(key))
			{
				return piece;
			}
			else if (VassalSharp.counters.Properties_Fields.OUTER.Equals(key))
			{
				return dec;
			}
			else if (VassalSharp.counters.Properties_Fields.VISIBLE_STATE.Equals(key))
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return myGetState() + piece.getProperty(key);
			}
			else if (VassalSharp.counters.Properties_Fields.SELECTED.Equals(key))
			{
				return selected;
			}
			else
			{
				return piece.getProperty(key);
			}
		}
		
		public virtual System.Object getLocalizedProperty(System.Object key)
		{
			if (VassalSharp.counters.Properties_Fields.KEY_COMMANDS.Equals(key))
			{
				return getProperty(key);
			}
			else if (VassalSharp.counters.Properties_Fields.INNER.Equals(key))
			{
				return getProperty(key);
			}
			else if (VassalSharp.counters.Properties_Fields.OUTER.Equals(key))
			{
				return getProperty(key);
			}
			else if (VassalSharp.counters.Properties_Fields.VISIBLE_STATE.Equals(key))
			{
				return getProperty(key);
			}
			/**
			* Return local cached copy of Selection Status
			*/
			else if (VassalSharp.counters.Properties_Fields.SELECTED.Equals(key))
			{
				return Selected;
			}
			else
			{
				return piece.getLocalizedProperty(key);
			}
		}
		
		public virtual void  setProperty(System.Object key, System.Object val)
		{
			if (VassalSharp.counters.Properties_Fields.INNER.Equals(key))
			{
				setInner((GamePiece) val);
			}
			else if (VassalSharp.counters.Properties_Fields.OUTER.Equals(key))
			{
				dec = (Decorator) val;
			}
			/**
			* Cache Selection status and pass it on to all inner traits.
			*/
			else if (VassalSharp.counters.Properties_Fields.SELECTED.Equals(key))
			{
				if (val is System.Boolean)
				{
					Selected = ((System.Boolean) val);
				}
				else
				{
					Selected = false;
				}
				piece.setProperty(key, val);
			}
			else
			{
				piece.setProperty(key, val);
			}
		}
		
		/// <summary>Set just the state of this trait</summary>
		/// <seealso cref="myGetState">
		/// </seealso>
		public abstract void  mySetState(System.String newState);
		
		/// <summary> Compute the difference between <code>newState</code> and <code>oldState</code>
		/// and appy that difference to the current state
		/// </summary>
		/// <param name="newState">
		/// </param>
		/// <param name="oldState">
		/// </param>
		public virtual void  mergeState(System.String newState, System.String oldState)
		{
			SequenceEncoder.Decoder stNew = new SequenceEncoder.Decoder(newState, '\t');
			System.String myNewState = stNew.nextToken();
			System.String innerNewState = stNew.nextToken();
			SequenceEncoder.Decoder stOld = new SequenceEncoder.Decoder(oldState, '\t');
			System.String myOldState = stOld.nextToken();
			System.String innerOldState = stOld.nextToken();
			if (!myOldState.Equals(myNewState))
			{
				mySetState(myNewState);
			}
			if (piece is StateMergeable)
			{
				((StateMergeable) piece).mergeState(innerNewState, innerOldState);
			}
			else
			{
				piece.State = innerNewState;
			}
		}
		
		/// <summary> </summary>
		/// <returns> the state of this trait alone
		/// </returns>
		/// <seealso cref="getState">
		/// </seealso>
		public abstract System.String myGetState();
		
		/// <summary> </summary>
		/// <returns> the type of this trait alone
		/// </returns>
		/// <seealso cref="getType">
		/// </seealso>
		public abstract System.String myGetType();
		
		/// <summary> </summary>
		/// <returns> the commands for this trait alone
		/// </returns>
		/// <seealso cref="getKeyCommands">
		/// </seealso>
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public abstract KeyCommand[] myGetKeyCommands();
		
		/// <summary> The set of key commands that will populate the piece's right-click menu.
		/// The key commands are accessible through the {@link Properties#KEY_COMMANDS} property.
		/// The commands for a Decorator are a composite of {@link #myGetKeyCommands} and the
		/// commands of its inner piece.
		/// </summary>
		/// <returns> the commands for this piece and its inner piece
		/// </returns>
		//UPGRADE_NOTE: Access modifiers of method 'getKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public virtual KeyCommand[] getKeyCommands()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'myC '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			KeyCommand[] myC = myGetKeyCommands();
			//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			KeyCommand[] c = (KeyCommand[]) piece.getProperty(VassalSharp.counters.Properties_Fields.KEY_COMMANDS);
			
			if (c == null)
				return myC;
			else if (myC == null)
				return c;
			else
				return ArrayUtils.append(typeof(KeyCommand[]), myC, c);
		}
		
		/// <summary> The response of this trait alone to the given KeyStroke</summary>
		/// <param name="stroke">
		/// </param>
		/// <returns> null if no effect
		/// </returns>
		/// <seealso cref="keyEvent">
		/// </seealso>
		public abstract Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke);
		
		/// <summary> Append the command returned by {@link #myKeyEvent} with the command returned
		/// by the inner piece's {@link GamePiece#keyEvent} method.
		/// </summary>
		/// <param name="stroke">
		/// </param>
		/// <returns>
		/// </returns>
		public virtual Command keyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			Command c = myKeyEvent(stroke);
			return c == null?piece.keyEvent(stroke):c.append(piece.keyEvent(stroke));
		}
		
		/// <param name="p">
		/// </param>
		/// <returns> the outermost Decorator instance of this piece, i.e. the entire piece with all traits
		/// </returns>
		public static GamePiece getOutermost(GamePiece p)
		{
			while (p.getProperty(VassalSharp.counters.Properties_Fields.OUTER) != null)
			{
				p = (GamePiece) p.getProperty(VassalSharp.counters.Properties_Fields.OUTER);
			}
			return p;
		}
		
		/// <summary> </summary>
		/// <param name="p">
		/// </param>
		/// <returns> the innermost GamePiece of this piece.  In most cases, an instance of {@link BasicPiece}
		/// </returns>
		public static GamePiece getInnermost(GamePiece p)
		{
			while (p is Decorator)
			{
				p = ((Decorator) p).piece;
			}
			return p;
		}
		
		/// <returns> the first Decorator within the given GamePiece
		/// that is an instance of the given Class
		/// </returns>
		public static GamePiece getDecorator;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(GamePiece p, Class < ? > type)
		
		public virtual PieceEditor getEditor()
		{
			return new SimplePieceEditor(this);
		}
		
		public override System.String ToString()
		{
			if (piece == null)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return base.ToString();
			}
			else
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return base.ToString() + "[name=" + getName() + ",type=" + Type + ",state=" + State + "]";
			}
		}
		
		/// <summary> Return I18n data for this piece</summary>
		/// <returns>
		/// </returns>
		public virtual PieceI18nData getI18nData()
		{
			return new PieceI18nData(this);
		}
		
		protected internal virtual PieceI18nData getI18nData(System.String command, System.String description)
		{
			PieceI18nData data = new PieceI18nData(this);
			data.add(command, description);
			return data;
		}
		
		protected internal virtual PieceI18nData getI18nData(System.String[] commands, System.String[] descriptions)
		{
			PieceI18nData data = new PieceI18nData(this);
			for (int i = 0; i < commands.Length; i++)
			{
				data.add(commands[i], descriptions[i]);
			}
			return data;
		}
		
		protected internal virtual System.String getCommandDescription(System.String description, System.String command)
		{
			System.String s = "";
			if (description != null && description.Length > 0)
			{
				s += (description + ": ");
			}
			return s + command;
		}
		
		protected internal virtual System.String getTranslation(System.String key)
		{
			System.String fullKey = VassalSharp.i18n.TranslatablePiece_Fields.PREFIX + key;
			return Localization.Instance.translate(fullKey, key);
		}
		
		/// <summary> Report a Data Error detected by a trait</summary>
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		protected internal static void  reportDataError(EditablePiece piece, System.String message, System.String data, System.Exception e)
		{
			ErrorDialog.dataError(new BadDataReport(piece, message, data, e));
		}
		
		protected internal static void  reportDataError(EditablePiece piece, System.String message, System.String data)
		{
			ErrorDialog.dataError(new BadDataReport(piece, message, data));
		}
		
		protected internal static void  reportDataError(EditablePiece piece, System.String message)
		{
			ErrorDialog.dataError(new BadDataReport(piece, message));
		}
		
		/// <summary> Default Property Name Source</summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getPropertyNames()
		
		/// <summary> Set the Oldxxxx properties related to movement</summary>
		/// <param name="p">
		/// </param>
		public static void  setOldProperties(GamePiece p)
		{
			System.String mapName = ""; //$NON-NLS-1$
			System.String boardName = ""; //$NON-NLS-1$
			System.String zoneName = ""; //$NON-NLS-1$
			System.String locationName = ""; //$NON-NLS-1$
			//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Map m = p.getMap();
			//UPGRADE_NOTE: Final was removed from the declaration of 'pos '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Point pos = p.Position;
			
			if (m != null)
			{
				mapName = m.getConfigureName();
				//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				Board b = m.findBoard(ref pos);
				if (b != null)
				{
					boardName = b.Name;
				}
				//UPGRADE_NOTE: Final was removed from the declaration of 'z '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				Zone z = m.findZone(ref pos);
				if (z != null)
				{
					zoneName = z.Name;
				}
				locationName = m.locationName(pos);
			}
			
			p.setProperty(BasicPiece.OLD_X, System.Convert.ToString(pos.X));
			p.setProperty(BasicPiece.OLD_Y, System.Convert.ToString(pos.Y));
			p.setProperty(BasicPiece.OLD_MAP, mapName);
			p.setProperty(BasicPiece.OLD_BOARD, boardName);
			p.setProperty(BasicPiece.OLD_ZONE, zoneName);
			p.setProperty(BasicPiece.OLD_LOCATION_NAME, locationName);
		}
		
		public virtual void  setOldProperties()
		{
			setOldProperties(this);
		}
		
		/// <summary> 
		/// Utility method to allow Decorator Editors to repack themselves. c must be one of the
		/// components that make up the Decorator's controls.
		/// </summary>
		public static void  repack(System.Windows.Forms.Control c)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
			System.Windows.Forms.Form w = (System.Windows.Forms.Form) c.Parent;
			if (w != null)
			{
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				w.pack();
			}
		}
		public abstract System.Drawing.Rectangle boundingBox();
		public abstract void  draw(System.Drawing.Graphics param1, int param2, int param3, System.Windows.Forms.Control param4, double param5);
	}
}