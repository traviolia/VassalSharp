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
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
using Buildable = VassalSharp.build.Buildable;
using Builder = VassalSharp.build.Builder;
using GameModule = VassalSharp.build.GameModule;
using AddPiece = VassalSharp.command.AddPiece;
using ChangePiece = VassalSharp.command.ChangePiece;
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
using MovePiece = VassalSharp.command.MovePiece;
using NullCommand = VassalSharp.command.NullCommand;
using PlayAudioClipCommand = VassalSharp.command.PlayAudioClipCommand;
using RemovePiece = VassalSharp.command.RemovePiece;
using ActionButton = VassalSharp.counters.ActionButton;
using AreaOfEffect = VassalSharp.counters.AreaOfEffect;
using BasicPiece = VassalSharp.counters.BasicPiece;
using CalculatedProperty = VassalSharp.counters.CalculatedProperty;
using Clone = VassalSharp.counters.Clone;
using CounterGlobalKeyCommand = VassalSharp.counters.CounterGlobalKeyCommand;
using Deck = VassalSharp.counters.Deck;
using Decorator = VassalSharp.counters.Decorator;
using Delete = VassalSharp.counters.Delete;
using DynamicProperty = VassalSharp.counters.DynamicProperty;
using Embellishment = VassalSharp.counters.Embellishment;
using Embellishment0 = VassalSharp.counters.Embellishment0;
using Footprint = VassalSharp.counters.Footprint;
using FreeRotator = VassalSharp.counters.FreeRotator;
using GamePiece = VassalSharp.counters.GamePiece;
using GlobalHotKey = VassalSharp.counters.GlobalHotKey;
using Hideable = VassalSharp.counters.Hideable;
using Immobilized = VassalSharp.counters.Immobilized;
using Labeler = VassalSharp.counters.Labeler;
using Marker = VassalSharp.counters.Marker;
using MovementMarkable = VassalSharp.counters.MovementMarkable;
using NonRectangular = VassalSharp.counters.NonRectangular;
using Obscurable = VassalSharp.counters.Obscurable;
using Pivot = VassalSharp.counters.Pivot;
using PlaceMarker = VassalSharp.counters.PlaceMarker;
using PlaySound = VassalSharp.counters.PlaySound;
using PropertySheet = VassalSharp.counters.PropertySheet;
using Replace = VassalSharp.counters.Replace;
using ReportState = VassalSharp.counters.ReportState;
using RestrictCommands = VassalSharp.counters.RestrictCommands;
using Restricted = VassalSharp.counters.Restricted;
using ReturnToDeck = VassalSharp.counters.ReturnToDeck;
using SendToLocation = VassalSharp.counters.SendToLocation;
using SetGlobalProperty = VassalSharp.counters.SetGlobalProperty;
using Stack = VassalSharp.counters.Stack;
using SubMenu = VassalSharp.counters.SubMenu;
using TableInfo = VassalSharp.counters.TableInfo;
using Translate = VassalSharp.counters.Translate;
using TriggerAction = VassalSharp.counters.TriggerAction;
using UsePrototype = VassalSharp.counters.UsePrototype;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.build.module
{
	
	/// <summary> A {@link CommandEncoder} that handles the basic commands: {@link AddPiece},
	/// {@link RemovePiece}, {@link ChangePiece}, {@link MovePiece}. If a module
	/// defines custom {@link GamePiece} classes, then this class may be overriden
	/// and imported into the module. Subclasses should override the
	/// {@link #createDecorator} method or, less often, the {@link #createBasic} or
	/// {@link #createPiece} methods to allow instantiation of the custom
	/// {@link GamePiece} classes.
	/// </summary>
	public class BasicCommandEncoder : CommandEncoder, Buildable
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassBasicPieceFactory' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassBasicPieceFactory : BasicCommandEncoder.BasicPieceFactory
		{
			public AnonymousClassBasicPieceFactory(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual GamePiece createBasicPiece(System.String type)
			{
				return new Stack();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassBasicPieceFactory1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassBasicPieceFactory1 : BasicCommandEncoder.BasicPieceFactory
		{
			public AnonymousClassBasicPieceFactory1(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual GamePiece createBasicPiece(System.String type)
			{
				return new BasicPiece(type);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassBasicPieceFactory2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassBasicPieceFactory2 : BasicCommandEncoder.BasicPieceFactory
		{
			public AnonymousClassBasicPieceFactory2(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual GamePiece createBasicPiece(System.String type)
			{
				return new Deck(type);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new Immobilized(inner, type);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory1 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory1(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'e '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Embellishment e = new Embellishment(type, inner);
				if (e.Version == Embellishment.BASE_VERSION)
				{
					return new Embellishment0(type, inner);
				}
				return e;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory2 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory2(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new Embellishment(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory3 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory3(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new Hideable(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory4' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory4 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory4(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new Obscurable(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory5' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory5 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory5(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new Labeler(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory6' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory6 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory6(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new TableInfo(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory7' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory7 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory7(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new PropertySheet(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory8' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory8 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory8(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new FreeRotator(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory9' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory9 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory9(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new Pivot(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory10' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory10 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory10(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new NonRectangular(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory11' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory11 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory11(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new Marker(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory12' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory12 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory12(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new Restricted(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory13' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory13 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory13(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new PlaceMarker(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory14' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory14 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory14(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new Replace(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory15' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory15 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory15(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new ReportState(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory16' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory16 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory16(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new MovementMarkable(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory17' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory17 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory17(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new Footprint(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory18' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory18 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory18(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new ReturnToDeck(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory19' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory19 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory19(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new SendToLocation(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory20' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory20 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory20(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new UsePrototype(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory21' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory21 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory21(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new Clone(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory22' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory22 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory22(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new Delete(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory23' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory23 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory23(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new SubMenu(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory24' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory24 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory24(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new Translate(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory25' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory25 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory25(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new AreaOfEffect(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory26' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory26 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory26(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new CounterGlobalKeyCommand(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory27' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory27 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory27(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new TriggerAction(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory28' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory28 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory28(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new DynamicProperty(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory29' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory29 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory29(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new CalculatedProperty(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory30' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory30 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory30(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new SetGlobalProperty(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory31' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory31 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory31(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new RestrictCommands(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory32' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory32 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory32(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new PlaySound(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory33' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory33 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory33(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new ActionButton(type, inner);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDecoratorFactory34' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDecoratorFactory34 : BasicCommandEncoder.DecoratorFactory
		{
			public AnonymousClassDecoratorFactory34(BasicCommandEncoder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicCommandEncoder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicCommandEncoder enclosingInstance;
			public BasicCommandEncoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Decorator createDecorator(System.String type, GamePiece inner)
			{
				return new GlobalHotKey(type, inner);
			}
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.build.module.BasicCommandEncoder'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private Map < String, BasicPieceFactory > basicFactories = 
		new HashMap < String, BasicPieceFactory >();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private Map < String, DecoratorFactory > decoratorFactories = 
		new HashMap < String, DecoratorFactory >();
		
		public BasicCommandEncoder()
		{
			basicFactories.put(Stack.TYPE, new AnonymousClassBasicPieceFactory(this));
			basicFactories.put(BasicPiece.ID, new AnonymousClassBasicPieceFactory1(this));
			basicFactories.put(Deck.ID, new AnonymousClassBasicPieceFactory2(this));
			decoratorFactories.put(Immobilized.ID, new AnonymousClassDecoratorFactory(this));
			decoratorFactories.put(Embellishment.ID, new AnonymousClassDecoratorFactory1(this));
			decoratorFactories.put(Embellishment.OLD_ID, new AnonymousClassDecoratorFactory2(this));
			decoratorFactories.put(Hideable.ID, new AnonymousClassDecoratorFactory3(this));
			decoratorFactories.put(Obscurable.ID, new AnonymousClassDecoratorFactory4(this));
			decoratorFactories.put(Labeler.ID, new AnonymousClassDecoratorFactory5(this));
			decoratorFactories.put(TableInfo.ID, new AnonymousClassDecoratorFactory6(this));
			decoratorFactories.put(PropertySheet.ID, new AnonymousClassDecoratorFactory7(this));
			decoratorFactories.put(FreeRotator.ID, new AnonymousClassDecoratorFactory8(this));
			decoratorFactories.put(Pivot.ID, new AnonymousClassDecoratorFactory9(this));
			decoratorFactories.put(NonRectangular.ID, new AnonymousClassDecoratorFactory10(this));
			decoratorFactories.put(Marker.ID, new AnonymousClassDecoratorFactory11(this));
			decoratorFactories.put(Restricted.ID, new AnonymousClassDecoratorFactory12(this));
			decoratorFactories.put(PlaceMarker.ID, new AnonymousClassDecoratorFactory13(this));
			decoratorFactories.put(Replace.ID, new AnonymousClassDecoratorFactory14(this));
			decoratorFactories.put(ReportState.ID, new AnonymousClassDecoratorFactory15(this));
			decoratorFactories.put(MovementMarkable.ID, new AnonymousClassDecoratorFactory16(this));
			decoratorFactories.put(Footprint.ID, new AnonymousClassDecoratorFactory17(this));
			decoratorFactories.put(ReturnToDeck.ID, new AnonymousClassDecoratorFactory18(this));
			decoratorFactories.put(SendToLocation.ID, new AnonymousClassDecoratorFactory19(this));
			decoratorFactories.put(UsePrototype.ID, new AnonymousClassDecoratorFactory20(this));
			decoratorFactories.put(Clone.ID, new AnonymousClassDecoratorFactory21(this));
			decoratorFactories.put(Delete.ID, new AnonymousClassDecoratorFactory22(this));
			decoratorFactories.put(SubMenu.ID, new AnonymousClassDecoratorFactory23(this));
			decoratorFactories.put(Translate.ID, new AnonymousClassDecoratorFactory24(this));
			decoratorFactories.put(AreaOfEffect.ID, new AnonymousClassDecoratorFactory25(this));
			decoratorFactories.put(CounterGlobalKeyCommand.ID, new AnonymousClassDecoratorFactory26(this));
			decoratorFactories.put(TriggerAction.ID, new AnonymousClassDecoratorFactory27(this));
			decoratorFactories.put(DynamicProperty.ID, new AnonymousClassDecoratorFactory28(this));
			decoratorFactories.put(CalculatedProperty.ID, new AnonymousClassDecoratorFactory29(this));
			decoratorFactories.put(SetGlobalProperty.ID, new AnonymousClassDecoratorFactory30(this));
			decoratorFactories.put(RestrictCommands.ID, new AnonymousClassDecoratorFactory31(this));
			decoratorFactories.put(PlaySound.ID, new AnonymousClassDecoratorFactory32(this));
			decoratorFactories.put(ActionButton.ID, new AnonymousClassDecoratorFactory33(this));
			decoratorFactories.put(GlobalHotKey.ID, new AnonymousClassDecoratorFactory34(this));
		}
		
		/// <summary> Creates a {@link Decorator} instance
		/// 
		/// </summary>
		/// <param name="type">the type of the Decorator to be created. Once created, the
		/// Decorator should return this value from its
		/// {@link Decorator#myGetType} method.
		/// 
		/// </param>
		/// <param name="inner">the inner piece of the Decorator
		/// </param>
		/// <seealso cref="Decorator">
		/// </seealso>
		public virtual Decorator createDecorator(System.String type, GamePiece inner)
		{
			Decorator d = null;
			System.String prefix = type.Substring(0, (type.IndexOf(';') + 1) - (0));
			if (prefix.Length == 0)
			{
				prefix = type;
			}
			BasicCommandEncoder.DecoratorFactory f = decoratorFactories.get_Renamed(prefix);
			if (f != null)
			{
				d = f.createDecorator(type, inner);
			}
			else
			{
				System.Console.Error.WriteLine("Unknown type " + type); //$NON-NLS-1$
				d = new Marker(Marker.ID, inner);
			}
			return d;
		}
		
		/// <summary> Create a GamePiece instance that is not a Decorator
		/// 
		/// </summary>
		/// <param name="type">the type of the GamePiece. The created piece should return this
		/// value from its {@link GamePiece#getType} method
		/// </param>
		protected internal virtual GamePiece createBasic(System.String type)
		{
			GamePiece p = null;
			System.String prefix = type.Substring(0, (type.IndexOf(';') + 1) - (0));
			if (prefix.Length == 0)
			{
				prefix = type;
			}
			BasicCommandEncoder.BasicPieceFactory f = basicFactories.get_Renamed(prefix);
			if (f != null)
			{
				p = f.createBasicPiece(type);
			}
			return p;
		}
		
		/// <summary> Creates a GamePiece instance from the given type information. Determines
		/// from the type whether the represented piece is a {@link Decorator} or not
		/// and forwards to {@link #createDecorator} or {@link #createBasic}. This
		/// method should generally not need to be overridden. Instead, override
		/// createDecorator or createBasic
		/// </summary>
		public virtual GamePiece createPiece(System.String type)
		{
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, '\t');
			type = st.nextToken();
			System.String innerType = st.hasMoreTokens()?st.nextToken():null;
			
			if (innerType != null)
			{
				GamePiece inner = createPiece(innerType);
				if (inner == null)
				{
					GameModule.getGameModule().getChatter().send("Invalid piece type - see Error Log for details"); //$NON-NLS-1$
					logger.warn("Could not create piece with type " + innerType);
					inner = new BasicPiece();
				}
				Decorator d = createDecorator(type, inner);
				return d != null?d:inner;
			}
			else
			{
				return createBasic(type);
			}
		}
		
		public virtual void  build(System.Xml.XmlElement e)
		{
			Builder.build(e, this);
		}
		
		public virtual void  addTo(Buildable parent)
		{
			((GameModule) parent).addCommandEncoder(this);
		}
		
		public virtual void  add(Buildable b)
		{
		}
		
		public virtual System.Xml.XmlElement getBuildElement(System.Xml.XmlDocument doc)
		{
			return doc.CreateElement(GetType().FullName);
		}
		
		private const char PARAM_SEPARATOR = '/';
		//UPGRADE_NOTE: Final was removed from the declaration of 'ADD '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly System.String ADD = "+" + PARAM_SEPARATOR; //$NON-NLS-1$
		//UPGRADE_NOTE: Final was removed from the declaration of 'REMOVE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly System.String REMOVE = "-" + PARAM_SEPARATOR; //$NON-NLS-1$
		//UPGRADE_NOTE: Final was removed from the declaration of 'CHANGE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly System.String CHANGE = "D" + PARAM_SEPARATOR; //$NON-NLS-1$
		//UPGRADE_NOTE: Final was removed from the declaration of 'MOVE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly System.String MOVE = "M" + PARAM_SEPARATOR; //$NON-NLS-1$
		
		public virtual Command decode(System.String command)
		{
			if (command.Length == 0)
			{
				return new NullCommand();
			}
			SequenceEncoder.Decoder st;
			if (command.StartsWith(ADD))
			{
				command = command.Substring(ADD.Length);
				st = new SequenceEncoder.Decoder(command, PARAM_SEPARATOR);
				System.String id = unwrapNull(st.nextToken());
				System.String type = st.nextToken();
				System.String state = st.nextToken();
				GamePiece p = createPiece(type);
				if (p == null)
				{
					return null;
				}
				else
				{
					p.Id = id;
					return new AddPiece(p, state);
				}
			}
			else if (command.StartsWith(REMOVE))
			{
				System.String id = command.Substring(REMOVE.Length);
				GamePiece target = GameModule.getGameModule().getGameState().getPieceForId(id);
				if (target == null)
				{
					return new RemovePiece(id);
				}
				else
				{
					return new RemovePiece(target);
				}
			}
			else if (command.StartsWith(CHANGE))
			{
				command = command.Substring(CHANGE.Length);
				st = new SequenceEncoder.Decoder(command, PARAM_SEPARATOR);
				System.String id = st.nextToken();
				System.String newState = st.nextToken();
				System.String oldState = st.hasMoreTokens()?st.nextToken():null;
				return new ChangePiece(id, oldState, newState);
			}
			else if (command.StartsWith(MOVE))
			{
				command = command.Substring(MOVE.Length);
				st = new SequenceEncoder.Decoder(command, PARAM_SEPARATOR);
				System.String id = unwrapNull(st.nextToken());
				System.String newMapId = unwrapNull(st.nextToken());
				int newX = System.Int32.Parse(st.nextToken());
				int newY = System.Int32.Parse(st.nextToken());
				System.String newUnderId = unwrapNull(st.nextToken());
				System.String oldMapId = unwrapNull(st.nextToken());
				int oldX = System.Int32.Parse(st.nextToken());
				int oldY = System.Int32.Parse(st.nextToken());
				System.String oldUnderId = unwrapNull(st.nextToken());
				System.String playerid = st.nextToken(GameModule.getUserId());
				System.Drawing.Point tempAux = new System.Drawing.Point(newX, newY);
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				System.Drawing.Point tempAux2 = new System.Drawing.Point(oldX, oldY);
				return new MovePiece(id, newMapId, ref tempAux, newUnderId, oldMapId, ref tempAux2, oldUnderId, playerid);
			}
			else
			{
				return PlayAudioClipCommand.decode(command);
			}
		}
		
		private System.String wrapNull(System.String s)
		{
			return s == null?"null":s; //$NON-NLS-1$
		}
		
		private System.String unwrapNull(System.String s)
		{
			return "null".Equals(s)?null:s; //$NON-NLS-1$
		}
		
		public virtual System.String encode(Command c)
		{
			SequenceEncoder se = new SequenceEncoder(PARAM_SEPARATOR);
			if (c is AddPiece)
			{
				AddPiece a = (AddPiece) c;
				return ADD + se.append(wrapNull(a.Target.Id)).append(a.Target.Type).append(a.State).Value;
			}
			else if (c is RemovePiece)
			{
				return REMOVE + ((RemovePiece) c).Id;
			}
			else if (c is ChangePiece)
			{
				ChangePiece cp = (ChangePiece) c;
				se.append(cp.Id).append(cp.NewState);
				if (cp.OldState != null)
				{
					se.append(cp.OldState);
				}
				return CHANGE + se.Value;
			}
			else if (c is MovePiece)
			{
				MovePiece mp = (MovePiece) c;
				se.append(mp.Id).append(wrapNull(mp.NewMapId)).append(mp.NewPosition.X + "").append(mp.NewPosition.Y + "").append(wrapNull(mp.NewUnderneathId)).append(wrapNull(mp.OldMapId)).append(mp.OldPosition.X + "").append(mp.OldPosition.Y + "").append(wrapNull(mp.OldUnderneathId)).append(mp.PlayerId);
				return MOVE + se.Value;
			}
			else if (c is NullCommand)
			{
				return ""; //$NON-NLS-1$
			}
			else if (c is PlayAudioClipCommand)
			{
				return ((PlayAudioClipCommand) c).encode();
			}
			else
			{
				return null;
			}
		}
		
		public interface DecoratorFactory
		{
			Decorator createDecorator(System.String type, GamePiece inner);
		}
		
		public interface BasicPieceFactory
		{
			GamePiece createBasicPiece(System.String type);
		}
		static BasicCommandEncoder()
		{
			logger = LoggerFactory.getLogger(typeof(BasicCommandEncoder));
		}
	}
}