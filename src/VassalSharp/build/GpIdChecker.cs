/*
* Copyright (c) 2011-2016 by Brent Easton
*
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License (LGPL) as published by the Free Software Foundation.
*
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
using System;
using System.Collections.Generic;
//using PieceSlot = VassalSharp.build.widget.PieceSlot;
//using BasicPiece = VassalSharp.counters.BasicPiece;
//using Decorator = VassalSharp.counters.Decorator;
//using GamePiece = VassalSharp.counters.GamePiece;
//using PieceCloner = VassalSharp.counters.PieceCloner;
//using PlaceMarker = VassalSharp.counters.PlaceMarker;
//using Properties = VassalSharp.counters.Properties;
namespace VassalSharp.build
{
	
	/// <summary> Build a cross-reference of all GpId-able elements in a module or ModuleExtension,
	/// Check for missing, duplicate or illegal GamePieceId's
	/// Update if necessary
	/// 
	/// </summary>
	public class GpIdChecker
	{
		
		protected internal GpIdSupport gpIdSupport;
		protected internal int maxId;
		protected internal bool useName = false;
		protected internal bool extensionsLoaded = false;
        ////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
        //final HashMap < String, SlotElement > goodSlots = new HashMap < String, SlotElement >();
        ////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
        List<SlotElement> errorSlots = new List<SlotElement>();

        public GpIdChecker():this(null)
		{
		}
		
		public GpIdChecker(GpIdSupport gpIdSupport)
		{
			this.gpIdSupport = gpIdSupport;
			maxId = - 1;
		}
		
		// This constructor is used by the GameRefresher to refresh a game with extensions possibly loaded
		public GpIdChecker(bool useName):this()
		{
			this.useName = useName;
			this.extensionsLoaded = true;
		}
		
		///// <summary> Add a PieceSlot to our cross-reference and any PlaceMarker
		///// traits it contains.
		///// 
		///// </summary>
		///// <param name="pieceSlot">
		///// </param>
		//public virtual void  add(PieceSlot pieceSlot)
		//{
		//	testGpId(pieceSlot.GpId, new SlotElement(this, pieceSlot));
			
		//	// PlaceMarker traits within the PieceSlot definition also contain GpId's.
		//	GamePiece gp = pieceSlot.Piece;
		//	checkTrait(gp, pieceSlot);
		//}
		
		///// <summary> Check for PlaceMarker traits in a GamePiece and add them to
		///// the cross-reference
		///// 
		///// </summary>
		///// <param name="gp">
		///// </param>
		///// <param name="slot">
		///// </param>
		//protected internal virtual void  checkTrait(GamePiece gp, PieceSlot slot)
		//{
		//	if (gp == null || gp is BasicPiece)
		//	{
		//		return ;
		//	}
			
		//	if (gp is PlaceMarker)
		//	{
		//		//UPGRADE_NOTE: Final was removed from the declaration of 'pm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//		PlaceMarker pm = (PlaceMarker) gp;
		//		testGpId(pm.GpId, new SlotElement(this, pm, slot));
		//	}
			
		//	checkTrait(((Decorator) gp).getInner(), slot);
		//}
		
		/// <summary> Validate a GamePieceId.
		/// - non-null
		/// - Integer
		/// - Not a duplicate of any other GpId
		/// Keep a list of the good Slots and the slots with errors.
		/// Also track the maximum GpId
		/// 
		/// </summary>
		/// <param name="id">
		/// </param>
		/// <param name="element">
		/// </param>
		protected internal virtual void  testGpId(System.String id, SlotElement element)
		{
			///*
			//*  If this has been called from a ModuleExtension, the GpId is prefixed with
			//*  the Extension Id. Remove the Extension Id and just process the numeric part.
			//*  
			//*  NOTE: If GpIdChecker is being used by the GameRefesher, then there may be 
			//*  extensions loaded, so retain the extension prefix to ensure a correct
			//*  unique slot id check.
			//*/
			//if (!extensionsLoaded)
			//{
			//	if (id.contains(":"))
			//	{
			//		id = id.split(":")[1];
			//	}
			//}
			
			//if (id == null || id.Length == 0)
			//{
			//	// gpid not generated yet?
			//	errorSlots.add(element);
			//}
			//else
			//{
			//	if (goodSlots.get_Renamed(id) != null)
			//	{
			//		// duplicate gpid?
			//		errorSlots.add(element);
			//	}
			//	try
			//	{
			//		if (extensionsLoaded)
			//		{
			//			goodSlots.put(id, element);
			//			System.Console.Out.WriteLine("Add Id " + id);
			//		}
			//		else
			//		{
			//			//UPGRADE_NOTE: Final was removed from the declaration of 'iid '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//			int iid = System.Int32.Parse(id);
			//			goodSlots.put(id, element); // gpid is good.
			//			if (iid >= maxId)
			//			{
			//				maxId = iid + 1;
			//			}
			//		}
			//	}
			//	catch (System.Exception e)
			//	{
			//		errorSlots.add(element); // non-numeric gpid?
			//	}
			//}
		}
		
		/// <summary> Where any errors found?</summary>
		/// <returns>
		/// </returns>
		public virtual bool hasErrors()
		{
			return errorSlots.Count > 0;
		}
		
		/// <summary> Repair any errors
		/// - Update the next GpId in the module if necessary
		/// - Generate new GpId's for slots with errors.
		/// </summary>
		public virtual void  fixErrors()
		{
			//if (maxId >= gpIdSupport.NextGpId)
			//{
			//	gpIdSupport.NextGpId = maxId + 1;
			//}
			////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			//for(SlotElement slotElement: errorSlots)
			//{
			//	slotElement.updateGpId();
			//}
		}
		
		///// <summary> Locate the SlotElement that matches oldPiece and return a new GamePiece
		///// created from that Slot.
		///// 
		///// 
		///// 
		///// </summary>
		///// <param name="oldPiece">
		///// </param>
		///// <returns>
		///// </returns>
		//public virtual GamePiece createUpdatedPiece(GamePiece oldPiece)
		//{
		//	// Find a slot with a matching gpid
		//	//UPGRADE_NOTE: Final was removed from the declaration of 'gpid '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//	System.String gpid = (System.String) oldPiece.getProperty(VassalSharp.counters.Properties_Fields.PIECE_ID);
		//	if (gpid != null && gpid.Length > 0)
		//	{
		//		//UPGRADE_NOTE: Final was removed from the declaration of 'element '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//		SlotElement element = goodSlots.get_Renamed(gpid);
		//		if (element != null)
		//		{
		//			return element.createPiece(oldPiece);
		//		}
		//	}
			
		//	// Failed to find a slot by gpid, try by matching piece name if option selected
		//	if (useName)
		//	{
		//		//UPGRADE_NOTE: Final was removed from the declaration of 'oldPieceName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//		System.String oldPieceName = Decorator.getInnermost(oldPiece).getName();
		//		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//		for(SlotElement el: goodSlots.values())
		//		{
		//			//UPGRADE_NOTE: Final was removed from the declaration of 'newPiece '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//			GamePiece newPiece = el.getPiece();
		//			//UPGRADE_NOTE: Final was removed from the declaration of 'newPieceName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//			System.String newPieceName = Decorator.getInnermost(newPiece).getName();
		//			if (oldPieceName.Equals(newPieceName))
		//			{
		//				return el.createPiece(oldPiece);
		//			}
		//		}
		//	}
			
		//	return oldPiece;
		//}
		
		
		//public virtual bool findUpdatedPiece(GamePiece oldPiece)
		//{
		//	// Find a slot with a matching gpid
		//	//UPGRADE_NOTE: Final was removed from the declaration of 'gpid '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//	System.String gpid = (System.String) oldPiece.getProperty(VassalSharp.counters.Properties_Fields.PIECE_ID);
		//	if (gpid != null && gpid.Length > 0)
		//	{
		//		//UPGRADE_NOTE: Final was removed from the declaration of 'element '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//		SlotElement element = goodSlots.get_Renamed(gpid);
		//		if (element != null)
		//		{
		//			return true;
		//		}
		//	}
			
		//	// Failed to find a slot by gpid, try by matching piece name if option selected
		//	if (useName)
		//	{
		//		//UPGRADE_NOTE: Final was removed from the declaration of 'oldPieceName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//		System.String oldPieceName = Decorator.getInnermost(oldPiece).getName();
		//		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//		for(SlotElement el: goodSlots.values())
		//		{
		//			//UPGRADE_NOTE: Final was removed from the declaration of 'newPiece '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//			GamePiece newPiece = el.getPiece();
		//			//UPGRADE_NOTE: Final was removed from the declaration of 'newPieceName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//			System.String newPieceName = Decorator.getInnermost(newPiece).getName();
		//			if (oldPieceName.Equals(newPieceName))
		//			{
		//				return true;
		//			}
		//		}
		//	}
			
		//	return false;
		//}
		
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'SlotElement' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Wrapper class for components that contain a GpId - They will all be either
		/// PieceSlot components or PlaceMarker Decorator's.
		/// Ideally we would add an interface to these components, but this
		/// will break any custom code based on PlaceMarker
		/// 
		/// </summary>
		//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
		protected internal class SlotElement
		{
			private void  InitBlock(GpIdChecker enclosingInstance)
			{
				//this.enclosingInstance = enclosingInstance;
				
				//GamePiece p = oldPiece;
				//while (p != null && !(p is BasicPiece))
				//{
				//	//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//	Decorator d = (Decorator) Decorator.getDecorator(p, classToFind);
				//	if (d != null)
				//	{
				//		if (d.GetType().Equals(classToFind))
				//		{
				//			if (d.myGetType().Equals(typeToFind))
				//			{
				//				return d.myGetState();
				//			}
				//		}
				//		p = d.getInner();
				//	}
				//	else
				//		p = null;
				//}
				//return null;
			}
			private GpIdChecker enclosingInstance;
			virtual public System.String GpId
			{
				get
				{
					return id;
				}
				
			}
			//virtual public GamePiece Piece
			//{
			//	get
			//	{
			//		if (slot == null)
			//		{
			//			return marker;
			//		}
			//		else
			//		{
			//			return slot.Piece;
			//		}
			//	}
				
			//}
			public GpIdChecker Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			//private PieceSlot slot;
			//private PlaceMarker marker;
			private System.String id;
			
			public SlotElement(GpIdChecker enclosingInstance)
			{
				InitBlock(enclosingInstance);
				//slot = null;
				//marker = null;
			}
			
			//public SlotElement(GpIdChecker enclosingInstance, PieceSlot ps):this(enclosingInstance)
			//{
			//	slot = ps;
			//	id = ps.GpId;
			//}
			
			//public SlotElement(GpIdChecker enclosingInstance, PlaceMarker pm, PieceSlot ps):this(enclosingInstance)
			//{
			//	marker = pm;
			//	slot = ps;
			//	id = pm.GpId;
			//}
			
			public virtual void  updateGpId()
			{
				//if (marker == null)
				//{
				//	slot.updateGpId();
				//}
				//else
				//{
				//	marker.updateGpId();
				//}
			}
			
			///// <summary> Create a new GamePiece based on this Slot Element. Use oldPiece
			///// to copy state information over to the new piece.
			///// 
			///// </summary>
			///// <param name="oldPiece">Old Piece for state information
			///// </param>
			///// <returns> New Piece
			///// </returns>
			//public virtual GamePiece createPiece(GamePiece oldPiece)
			//{
			//	GamePiece newPiece = (slot != null)?slot.Piece:marker.createMarker();
			//	// The following two steps create a complete new GamePiece with all
			//	// prototypes expanded
			//	newPiece = PieceCloner.Instance.clonePiece(newPiece);
			//	copyState(oldPiece, newPiece);
			//	newPiece.setProperty(VassalSharp.counters.Properties_Fields.PIECE_ID, GpId);
			//	return newPiece;
			//}
			
			///// <summary> Copy as much state information as possible from the old
			///// piece to the new piece
			///// 
			///// </summary>
			///// <param name="oldPiece">Piece to copy state from
			///// </param>
			///// <param name="newPiece">Piece to copy state to
			///// </param>
			//protected internal virtual void  copyState(GamePiece oldPiece, GamePiece newPiece)
			//{
			//	GamePiece p = newPiece;
			//	while (p != null)
			//	{
			//		if (p is BasicPiece)
			//		{
			//			((BasicPiece) p).State = ((BasicPiece) Decorator.getInnermost(oldPiece)).State;
			//			p = null;
			//		}
			//		else
			//		{
			//			//UPGRADE_NOTE: Final was removed from the declaration of 'decorator '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//			Decorator decorator = (Decorator) p;
			//			//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//			System.String type = decorator.myGetType();
			//			//UPGRADE_NOTE: Final was removed from the declaration of 'newState '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//			System.String newState = findStateFromType(oldPiece, type, p.GetType());
			//			if (newState != null && newState.Length > 0)
			//			{
			//				decorator.mySetState(newState);
			//			}
			//			p = decorator.getInner();
			//		}
			//	}
			//}
			
			///// <summary> Locate a Decorator in the old piece that has the exact same
			///// type as the new Decorator and return it's state
			///// 
			///// </summary>
			///// <param name="oldPiece">Old piece to search
			///// </param>
			///// <param name="typeToFind">Type to match
			///// </param>
			///// <param name="classToFind">Class to match
			///// </param>
			///// <returns>
			///// </returns>
			//protected internal System.String findStateFromType;
			////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			//(GamePiece oldPiece, String typeToFind, Class < ? extends GamePiece > classToFind)
		}
	}
}