/*
* $Id$
*
* Copyright (c) 2004 by Rodney Kinney
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
using PrototypeDefinition = VassalSharp.build.module.PrototypeDefinition;
using PrototypesContainer = VassalSharp.build.module.PrototypesContainer;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using PropertySource = VassalSharp.build.module.properties.PropertySource;
using Command = VassalSharp.command.Command;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using RecursionLimitException = VassalSharp.tools.RecursionLimitException;
using RecursionLimiter = VassalSharp.tools.RecursionLimiter;
using Loopable = VassalSharp.tools.RecursionLimiter.Loopable;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> This trait is a placeholder for a pre-defined series of traits specified in a
	/// {@link VassalSharp.build.module.PrototypeDefinition} object. When a piece that uses a prototype is defined in a module, it
	/// is simply assigned the name of a particular prototype definition. When that piece is during a game, the UsePrototype
	/// trait is substituted for the list of traits in the prototype definition. From that point on, the piece has no record
	/// that those traits were defined in a prototype instead of assigned to piece directly. This is necessary so that
	/// subsequent changes to a prototype definition don't invalidate games that were saved using previous versions of the
	/// module.
	/// 
	/// </summary>
	public class UsePrototype:Decorator, EditablePiece, RecursionLimiter.Loopable
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertySource' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertySource : PropertySource
		{
			public AnonymousClassPropertySource(System.Collections.Specialized.NameValueCollection p, UsePrototype enclosingInstance)
			{
				InitBlock(p, enclosingInstance);
			}
			private void  InitBlock(System.Collections.Specialized.NameValueCollection p, UsePrototype enclosingInstance)
			{
				this.p = p;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			//UPGRADE_NOTE: Final variable p was copied into class AnonymousClassPropertySource. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Collections.Specialized.NameValueCollection p;
			private UsePrototype enclosingInstance;
			public UsePrototype Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual System.Object getProperty(System.Object key)
			{
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				return p.Get(System.Convert.ToString(key));
			}
			
			public virtual System.Object getLocalizedProperty(System.Object key)
			{
				return getProperty(key);
			}
		}
		virtual public System.String Description
		{
			get
			{
				return prototypeName != null && prototypeName.Length > 0?"Prototype - " + prototypeName:"Prototype";
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("UsePrototype.htm");
			}
			
		}
		/// <summary> Build a new GamePiece instance based on the traits in the referenced {@link PrototypeDefinition}. Substitute the
		/// new instance for {@link #getInner} and return it. If the referenced definition does not exist, return the default
		/// inner piece.
		/// 
		/// </summary>
		/// <returns> the new instance
		/// </returns>
		virtual public GamePiece ExpandedInner
		{
			get
			{
				buildPrototype();
				return prototype != null?prototype:piece;
			}
			
		}
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		override public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				return ExpandedInner.Shape;
			}
			
		}
		virtual public System.String PrototypeName
		{
			get
			{
				return prototypeName;
			}
			
		}
		virtual public System.String ComponentName
		{
			// Implement Loopable
			
			get
			{
				return piece.getName();
			}
			
		}
		virtual public System.String ComponentTypeName
		{
			get
			{
				return Description;
			}
			
		}
		public const System.String ID = "prototype;";
		private System.String prototypeName;
		private System.String lastCachedPrototype;
		private GamePiece prototype;
		private PropertySource properties;
		private System.String type;
		
		public UsePrototype():this(ID, null)
		{
		}
		
		public UsePrototype(System.String type, GamePiece inner)
		{
			mySetType(type);
			setInner(inner);
		}
		
		public virtual void  mySetType(System.String type)
		{
			this.type = type;
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type.Substring(ID.Length), ';');
			prototypeName = st.nextToken("");
			if (st.hasMoreTokens())
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
				System.Collections.Specialized.NameValueCollection p = new System.Collections.Specialized.NameValueCollection();
				SequenceEncoder.Decoder st2 = new SequenceEncoder.Decoder(st.nextToken(), ',');
				while (st2.hasMoreTokens())
				{
					SequenceEncoder.Decoder st3 = new SequenceEncoder.Decoder(st2.nextToken(), '=');
					if (st3.hasMoreTokens())
					{
						System.String key = st3.nextToken();
						if (st3.hasMoreTokens())
						{
							System.String value_Renamed = st3.nextToken();
							//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
							p[key] = value_Renamed;
						}
					}
				}
				properties = new AnonymousClassPropertySource(p, this);
			}
			lastCachedPrototype = null;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			return new KeyCommand[0];
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'getKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] getKeyCommands()
		{
			return (KeyCommand[]) ExpandedInner.getProperty(VassalSharp.counters.Properties_Fields.KEY_COMMANDS);
		}
		
		public override void  setInner(GamePiece p)
		{
			base.setInner(p);
			lastCachedPrototype = null;
		}
		
		protected internal virtual void  buildPrototype()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'def '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			PrototypeDefinition def = PrototypesContainer.getPrototype(prototypeName);
			if (def != null)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'expandedPrototype '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GamePiece expandedPrototype = def.getPiece(properties);
				
				// Check to see if prototype definition has changed
				//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String type = expandedPrototype.Type;
				if (!type.Equals(lastCachedPrototype))
				{
					lastCachedPrototype = type;
					try
					{
						RecursionLimiter.startExecution(this);
						
						prototype = PieceCloner.Instance.clonePiece(expandedPrototype);
						//UPGRADE_NOTE: Final was removed from the declaration of 'outer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						Decorator outer = (Decorator) Decorator.getInnermost(prototype).getProperty(VassalSharp.counters.Properties_Fields.OUTER);
						if (outer != null)
						{
							// Will be null for an empty prototype
							outer.setInner(piece);
							prototype.setProperty(VassalSharp.counters.Properties_Fields.OUTER, this);
						}
						else
						{
							prototype = null;
						}
					}
					catch (RecursionLimitException e)
					{
						RecursionLimiter.infiniteLoop(e);
						prototype = null;
					}
					finally
					{
						RecursionLimiter.endExecution();
					}
				}
			}
			else
			{
				prototype = null;
			}
		}
		
		public override System.String myGetState()
		{
			return "";
		}
		
		public override System.String myGetType()
		{
			return type;
		}
		
		public override Command keyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			return ExpandedInner.keyEvent(stroke);
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			return null;
		}
		
		public override void  mySetState(System.String newState)
		{
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			return ExpandedInner.boundingBox();
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			ExpandedInner.draw(g, x, y, obs, zoom);
		}
		
		public override System.String getName()
		{
			return ExpandedInner.getName();
		}
		
		public override PieceEditor getEditor()
		{
			return new Editor(this);
		}
		public class Editor : PieceEditor
		{
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return nameConfig.Controls;
				}
				
			}
			virtual public System.String State
			{
				get
				{
					return "";
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					return VassalSharp.counters.UsePrototype.ID + nameConfig.ValueString;
				}
				
			}
			private StringConfigurer nameConfig;
			
			public Editor(UsePrototype up)
			{
				nameConfig = new StringConfigurer(null, "Prototype name:  ", up.type.Substring(VassalSharp.counters.UsePrototype.ID.Length));
			}
		}
	}
}