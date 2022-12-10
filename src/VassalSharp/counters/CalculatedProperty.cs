/*
* $Id$
*
* Copyright (c) 2008-2009 by Brent Easton
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
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Command = VassalSharp.command.Command;
using BeanShellExpressionConfigurer = VassalSharp.configure.BeanShellExpressionConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using Resources = VassalSharp.i18n.Resources;
using BeanShellExpression = VassalSharp.script.expression.BeanShellExpression;
using Expression = VassalSharp.script.expression.Expression;
using ExpressionException = VassalSharp.script.expression.ExpressionException;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using RecursionLimitException = VassalSharp.tools.RecursionLimitException;
using RecursionLimiter = VassalSharp.tools.RecursionLimiter;
using Loopable = VassalSharp.tools.RecursionLimiter.Loopable;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> Conditional Marker
	/// A marker with a variable value depending on conditions.
	/// 
	/// </summary>
	public class CalculatedProperty:Decorator, EditablePiece, RecursionLimiter.Loopable
	{
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < String > l = new ArrayList < String >();
			l.add(name);
			return l;
		}
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		override public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				return piece.Shape;
			}
			
		}
		virtual public System.String Description
		{
			get
			{
				System.String desc = "Calculated Property";
				if (name != null && name.Length > 0)
				{
					desc += (" - " + name);
				}
				return desc;
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("CalculatedProperty.htm");
			}
			
		}
		virtual protected internal System.String Expression
		{
			get
			{
				return expression.getExpression();
			}
			
		}
		virtual public System.String ComponentName
		{
			// Implement Loopable
			
			get
			{
				// Use inner name to prevent recursive looping when reporting errors.
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
		
		public const System.String ID = "calcProp;";
		
		protected internal static int counter = 0;
		
		protected internal System.String name = "";
		protected internal Expression expression;
		
		public CalculatedProperty():this(ID, null)
		{
		}
		
		public CalculatedProperty(System.String type, GamePiece inner)
		{
			InitBlock();
			mySetType(type);
			setInner(inner);
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			return piece.boundingBox();
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			piece.draw(g, x, y, obs, zoom);
		}
		
		public override System.String getName()
		{
			return piece.getName();
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			return new KeyCommand[0];
		}
		
		public override System.String myGetState()
		{
			return "";
		}
		
		public override System.String myGetType()
		{
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(name).append(Expression);
			return ID + se.Value;
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			return null;
		}
		
		public override void  mySetState(System.String newState)
		{
		}
		
		public virtual void  mySetType(System.String type)
		{
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			st.nextToken();
			name = st.nextToken("");
			expression = BeanShellExpression.createExpression(st.nextToken(""));
		}
		
		/// <summary> Return the value of this trait's property.
		/// Evaluating Expressions can lead to infinite loops and
		/// eventually a Stack Overflow. Trap and report this
		/// </summary>
		public override System.Object getProperty(System.Object key)
		{
			System.Object result = "";
			if (name.Length > 0 && name.Equals(key))
			{
				try
				{
					RecursionLimiter.startExecution(this);
					result = evaluate();
					return result;
				}
				catch (RecursionLimitException e)
				{
					RecursionLimiter.infiniteLoop(e);
				}
				finally
				{
					RecursionLimiter.endExecution();
				}
				
				return result;
			}
			return base.getProperty(key);
		}
		
		public override System.Object getLocalizedProperty(System.Object key)
		{
			if (name.Length > 0 && name.Equals(key))
			{
				return getProperty(key);
			}
			return base.getLocalizedProperty(key);
		}
		
		/// <summary> Evaluate the calculated property. Do not call Decorator.reportDataError as this will probably
		/// cause an infinite reporting loop.
		/// 
		/// </summary>
		/// <returns> value
		/// </returns>
		protected internal virtual System.String evaluate()
		{
			try
			{
				return expression.evaluate(Decorator.getOutermost(this));
			}
			catch (ExpressionException e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				ErrorDialog.dataError(new BadDataReport(Resources.getString("Error.expression_error"), piece.getProperty(BasicPiece.BASIC_NAME) + "-Calculated Property[" + name + "]=" + Expression + ", Error=" + e.Error, e));
				return "";
			}
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		/// <summary> Trait Editor implementation
		/// 
		/// </summary>
		public class Ed : PieceEditor
		{
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return box;
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
					SequenceEncoder se = new SequenceEncoder(';');
					se.append(nameConfig.ValueString).append(expressionConfig.ValueString);
					return VassalSharp.counters.CalculatedProperty.ID + se.Value;
				}
				
			}
			
			protected internal StringConfigurer nameConfig;
			protected internal BeanShellExpressionConfigurer expressionConfig;
			protected internal StringConfigurer defaultValueConfig;
			protected internal System.Windows.Forms.Panel box;
			
			public Ed(CalculatedProperty piece)
			{
				
				box = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				box.setLayout(new BoxLayout(box, BoxLayout.Y_AXIS));
				
				nameConfig = new StringConfigurer(null, "Property Name:  ", piece.name);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(nameConfig.Controls);
				
				expressionConfig = new BeanShellExpressionConfigurer(null, "Expression:  ", piece.Expression, Decorator.getOutermost(piece));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(expressionConfig.Controls);
			}
		}
		
		/// <summary> Return Property names exposed by this trait</summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getPropertyNames()
	}
}