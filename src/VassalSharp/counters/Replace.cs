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
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Command = VassalSharp.command.Command;
using RemovePiece = VassalSharp.command.RemovePiece;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.counters
{
	
	/// <summary> GamePiece trait that replaces a GamePiece with another one</summary>
	public class Replace:PlaceMarker
	{
		override public System.String Description
		{
			get
			{
				System.String d = "Replace with Other";
				if (description.Length > 0)
				{
					d += (" - " + description);
				}
				return d;
			}
			
		}
		override public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("Replace.htm");
			}
			
		}
		new public const System.String ID = "replace;";
		
		public Replace():this(ID + "Replace;R;null", null)
		{
		}
		
		public Replace(System.String type, GamePiece inner):base(type, inner)
		{
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			Command c = null;
			if (command.matches(stroke))
			{
				c = replacePiece();
			}
			return c;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'replacePiece' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public virtual Command replacePiece()
		{
			Command c;
			c = placeMarker();
			if (c == null)
			{
				reportDataError(this, Resources.getString("Error.bad_replace"));
			}
			else
			{
				Command remove = new RemovePiece(Decorator.getOutermost(this));
				remove.execute();
				c.append(remove);
			}
			return c;
		}
		
		protected internal override void  selectMarker(GamePiece marker)
		{
			KeyBuffer.Buffer.add(marker);
		}
		
		public override System.String myGetType()
		{
			return ID + base.myGetType().Substring(PlaceMarker.ID.Length);
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		public override GamePiece createMarker()
		{
			GamePiece marker = base.createMarker();
			if (marker != null && matchRotation)
			{
				if (above)
				{
					matchTraits(this, marker);
				}
				else
				{
					matchTraits(Decorator.getOutermost(this), marker);
				}
			}
			return marker;
		}
		
		protected internal virtual void  matchTraits(GamePiece base_Renamed, GamePiece marker)
		{
			if (!(base_Renamed is Decorator) || !(marker is Decorator))
			{
				return ;
			}
			Decorator currentTrait = (Decorator) base_Renamed;
			Decorator lastMatch = (Decorator) marker;
			while (currentTrait != null)
			{
				Decorator candidate = lastMatch;
				while (candidate != null)
				{
					candidate = (Decorator) Decorator.getDecorator(candidate, currentTrait.GetType());
					if (candidate != null)
					{
						if (candidate.myGetType().Equals(currentTrait.myGetType()))
						{
							candidate.mySetState(currentTrait.myGetState());
							lastMatch = candidate;
							candidate = null;
						}
						else
						{
							GamePiece inner = candidate.getInner();
							if (inner is Decorator)
							{
								candidate = (Decorator) inner;
							}
							else
							{
								candidate = null;
							}
						}
					}
				}
				if (currentTrait.getInner() is Decorator)
				{
					currentTrait = (Decorator) currentTrait.getInner();
				}
				else
				{
					currentTrait = null;
				}
			}
		}
		
		public override PieceI18nData getI18nData()
		{
			return getI18nData(command.Name, getCommandDescription(description, "Replace command"));
		}
		
		new protected internal class Ed:PlaceMarker.Ed
		{
			override public System.String Type
			{
				get
				{
					return VassalSharp.counters.Replace.ID + base.Type.Substring(PlaceMarker.ID.Length);
				}
				
			}
			
			public Ed(Replace piece):base(piece)
			{
				defineButton.Text = "Define Replacement";
			}
			
			protected internal override BooleanConfigurer createMatchRotationConfig()
			{
				return new BooleanConfigurer(null, "Match Current State?");
			}
			
			protected internal override BooleanConfigurer createAboveConfig()
			{
				return new BooleanConfigurer(null, "Only match states above this trait?");
			}
		}
	}
}