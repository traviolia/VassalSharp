/*
* $Id$
*
* Copyright (c) 2000-2009 by Rodney Kinney, Brent Easton
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
using NullCommand = VassalSharp.command.NullCommand;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using FormattedExpressionConfigurer = VassalSharp.configure.FormattedExpressionConfigurer;
using FormattedStringConfigurer = VassalSharp.configure.FormattedStringConfigurer;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using NamedKeyStrokeArrayConfigurer = VassalSharp.configure.NamedKeyStrokeArrayConfigurer;
using PropertyExpression = VassalSharp.configure.PropertyExpression;
using PropertyExpressionConfigurer = VassalSharp.configure.PropertyExpressionConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using StringEnumConfigurer = VassalSharp.configure.StringEnumConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using Resources = VassalSharp.i18n.Resources;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using FormattedString = VassalSharp.tools.FormattedString;
using LoopControl = VassalSharp.tools.LoopControl;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using RecursionLimitException = VassalSharp.tools.RecursionLimitException;
using RecursionLimiter = VassalSharp.tools.RecursionLimiter;
using Loopable = VassalSharp.tools.RecursionLimiter.Loopable;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> Macro Execute a series of Keystrokes against this same piece - Triggered by
	/// own KeyCommand or list of keystrokes - Match against an optional Property
	/// Filter
	/// 
	/// </summary>
	public class TriggerAction:Decorator, TranslatablePiece, RecursionLimiter.Loopable
	{
		private void  InitBlock()
		{
			key = NamedKeyStroke.NULL_KEYSTROKE;
			preLoopKey = NamedKeyStroke.NULL_KEYSTROKE;
			postLoopKey = NamedKeyStroke.NULL_KEYSTROKE;
			loopType = LoopControl.LOOP_COUNTED;
			if (Index)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final ArrayList < String > l = new ArrayList < String >();
				l.add(indexProperty);
				return l;
			}
			else
			{
				return base.getPropertyNames();
			}
		}
		virtual protected internal bool Index
		{
			get
			{
				return loop && index && indexProperty != null && indexProperty.Length > 0;
			}
			
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
				System.String s = Resources.getString("Editor.TriggerAction.component_type"); //$NON-NLS-1$
				if (name.Length > 0)
				{
					s += (" - " + name); //$NON-NLS-1$
				}
				return s;
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("TriggerAction.htm"); //$NON-NLS-1$
			}
			
		}
		virtual public System.String PropertyMatch
		{
			// Setters for JUnit testing
			
			set
			{
				propertyMatch.Expression = value;
			}
			
		}
		virtual public System.String CommandName
		{
			set
			{
				command = value;
			}
			
		}
		virtual public NamedKeyStroke Key
		{
			set
			{
				key = value;
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
		
		public const System.String ID = "macro;"; //$NON-NLS-1$
		
		protected internal System.String name = ""; //$NON-NLS-1$
		protected internal System.String command = ""; //$NON-NLS-1$
		//UPGRADE_NOTE: The initialization of  'key' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal NamedKeyStroke key;
		protected internal PropertyExpression propertyMatch = new PropertyExpression();
		protected internal NamedKeyStroke[] watchKeys = new NamedKeyStroke[0];
		protected internal NamedKeyStroke[] actionKeys = new NamedKeyStroke[0];
		protected internal bool loop = false;
		//UPGRADE_NOTE: The initialization of  'preLoopKey' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal NamedKeyStroke preLoopKey;
		//UPGRADE_NOTE: The initialization of  'postLoopKey' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal NamedKeyStroke postLoopKey;
		//UPGRADE_NOTE: The initialization of  'loopType' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal System.String loopType;
		protected internal PropertyExpression whileExpression = new PropertyExpression();
		protected internal PropertyExpression untilExpression = new PropertyExpression();
		protected internal FormattedString loopCount = new FormattedString("1"); //$NON-NLS-1$
		protected internal bool index = false;
		protected internal System.String indexProperty = ""; //$NON-NLS-1$
		protected internal FormattedString indexStart = new FormattedString("1");
		protected internal FormattedString indexStep = new FormattedString("1");
		protected internal int indexValue = 0;
		protected internal GamePiece outer;
		
		public TriggerAction():this(ID, null)
		{
		}
		
		public TriggerAction(System.String type, GamePiece inner)
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
			if (command.Length > 0 && key != null)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				KeyCommand c = new KeyCommand(command, key, Decorator.getOutermost(this), matchesFilter());
				if (getMap() == null)
				{
					c.setEnabled(false);
				}
				return new KeyCommand[]{c};
			}
			else
			{
				return new KeyCommand[0];
			}
		}
		
		public override System.String myGetState()
		{
			return ""; //$NON-NLS-1$
		}
		
		public override System.String myGetType()
		{
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(name).append(command).append(key).append(propertyMatch.Expression).append(NamedKeyStrokeArrayConfigurer.encode(watchKeys)).append(NamedKeyStrokeArrayConfigurer.encode(actionKeys)).append(loop).append(preLoopKey).append(postLoopKey).append(loopType).append(whileExpression.Expression).append(untilExpression.Expression).append(loopCount.Format).append(index).append(indexProperty).append(indexStart.Format).append(indexStep.Format);
			
			return ID + se.Value;
		}
		
		/// <summary> Apply key commands to inner pieces first
		/// 
		/// </summary>
		/// <param name="stroke">
		/// </param>
		/// <returns>
		/// </returns>
		public override Command keyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			Command c = piece.keyEvent(stroke);
			return c == null?myKeyEvent(stroke):c.append(myKeyEvent(stroke));
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			/*
			* 1. Are we interested in this key command? Is it our command key? Does it
			* match one of our watching keystrokes?
			*/
			bool seen = false;
			if (key.Equals(stroke))
			{
				seen = true;
			}
			
			for (int i = 0; i < watchKeys.Length && !seen; i++)
			{
				if (watchKeys[i].Equals(stroke))
				{
					seen = true;
				}
			}
			
			if (!seen)
			{
				return null;
			}
			
			// 2. Check the Property Filter if it exists.
			if (!matchesFilter())
			{
				return null;
			}
			
			// 3. Initialise
			outer = Decorator.getOutermost(this);
			Command c = new NullCommand();
			
			// 4. Handle non-looping case
			if (!loop)
			{
				try
				{
					doLoopOnce(c);
				}
				catch (RecursionLimitException e)
				{
					RecursionLimiter.infiniteLoop(e);
				}
				return c;
			}
			
			// 5. Looping
			
			// Set up Index Property
			indexValue = parse("Index Property Start Value", indexStart, outer);
			//UPGRADE_NOTE: Final was removed from the declaration of 'step '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int step = parse("Index Property increment value", indexStep, outer);
			
			// Issue the Pre-loop key
			executeKey(c, preLoopKey);
			
			// Loop
			
			// Set up counters for a counted loop
			int loopCounter = 0;
			int loopCountLimit = 0;
			if (LoopControl.LOOP_COUNTED.Equals(loopType))
			{
				loopCountLimit = loopCount.getTextAsInt(outer, Resources.getString("Editor.LoopControl.loop_count"), this); //$NON-NLS-1$
			}
			RecursionLimitException loopException = null;
			
			for (; ; )
			{
				
				// While loop - test condition is still true before actions
				if (LoopControl.LOOP_WHILE.Equals(loopType))
				{
					if (!whileExpression.accept(outer))
					{
						break;
					}
				}
				
				// Execute the actions and catch and looping. Save any
				// loop Exception to be thrown after the post-loop code
				// to ensure post-loop key is executed.
				try
				{
					doLoopOnce(c);
				}
				catch (RecursionLimitException ex)
				{
					loopException = ex;
					break;
				}
				
				// Until loop - test condition is not false after loop
				if (LoopControl.LOOP_UNTIL.Equals(loopType))
				{
					if (untilExpression.accept(outer))
					{
						break;
					}
				}
				
				// Check for infinite looping. Save any
				// loop Exception to be thrown after the post-loop code
				// to ensure post-loop key is executed.
				if (loopCounter++ >= LoopControl.LOOP_LIMIT)
				{
					loopException = new RecursionLimitException(this);
					break;
				}
				
				// Counted loop - Check if looped enough times
				if (LoopControl.LOOP_COUNTED.Equals(loopType))
				{
					if (loopCounter >= loopCountLimit)
					{
						break;
					}
				}
				
				// Increment the Index Variable
				indexValue += step;
			}
			
			// Issue the Post-loop key
			executeKey(c, postLoopKey);
			
			// Report any loop exceptions
			if (loopException != null)
			{
				RecursionLimiter.infiniteLoop(loopException);
			}
			
			return c;
		}
		
		private int parse(System.String desc, FormattedString s, GamePiece outer)
		{
			int i = 0;
			System.String val = s.getText(outer, "0");
			try
			{
				i = System.Int32.Parse(val);
			}
			catch (System.FormatException e)
			{
				reportDataError(this, Resources.getString("Error.non_number_error"), s.debugInfo(val, desc), e);
			}
			return i;
		}
		
		public override System.Object getProperty(System.Object key)
		{
			if (Index && indexProperty.Equals(key))
			{
				return System.Convert.ToString(indexValue);
			}
			return base.getProperty(key);
		}
		
		public override System.Object getLocalizedProperty(System.Object key)
		{
			if (Index && indexProperty.Equals(key))
			{
				return System.Convert.ToString(indexValue);
			}
			return base.getLocalizedProperty(key);
		}
		
		protected internal virtual void  doLoopOnce(Command c)
		{
			try
			{
				RecursionLimiter.startExecution(this);
				for (int i = 0; i < actionKeys.Length && getMap() != null; i++)
				{
					c.append(outer.keyEvent(actionKeys[i].KeyStroke));
				}
			}
			finally
			{
				RecursionLimiter.endExecution();
			}
		}
		
		protected internal virtual void  executeKey(Command c, NamedKeyStroke key)
		{
			if (key.Null || getMap() == null)
			{
				return ;
			}
			
			try
			{
				RecursionLimiter.startExecution(this);
				c.append(outer.keyEvent(key.KeyStroke));
			}
			catch (RecursionLimitException e)
			{
				RecursionLimiter.infiniteLoop(e);
			}
			finally
			{
				RecursionLimiter.endExecution();
			}
		}
		
		protected internal virtual bool matchesFilter()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'outer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GamePiece outer = Decorator.getOutermost(this);
			if (!propertyMatch.Null)
			{
				if (!propertyMatch.accept(outer))
				{
					return false;
				}
			}
			return true;
		}
		
		public override void  mySetState(System.String newState)
		{
		}
		
		public virtual void  mySetType(System.String type)
		{
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			st.nextToken();
			name = st.nextToken(""); //$NON-NLS-1$
			command = st.nextToken("Trigger"); //$NON-NLS-1$
			key = st.nextNamedKeyStroke('T');
			propertyMatch.Expression = st.nextToken(""); //$NON-NLS-1$
			
			System.String keys = st.nextToken(""); //$NON-NLS-1$
			if (keys.IndexOf(',') > 0)
			{
				watchKeys = NamedKeyStrokeArrayConfigurer.decode(keys);
			}
			else
			{
				watchKeys = new NamedKeyStroke[keys.Length];
				for (int i = 0; i < watchKeys.Length; i++)
				{
					watchKeys[i] = NamedKeyStroke.getNamedKeyStroke(keys[i], (int) System.Windows.Forms.Keys.Control);
				}
			}
			
			keys = st.nextToken(""); //$NON-NLS-1$
			if (keys.IndexOf(',') > 0)
			{
				actionKeys = NamedKeyStrokeArrayConfigurer.decode(keys);
			}
			else
			{
				actionKeys = new NamedKeyStroke[keys.Length];
				for (int i = 0; i < actionKeys.Length; i++)
				{
					actionKeys[i] = NamedKeyStroke.getNamedKeyStroke(keys[i], (int) System.Windows.Forms.Keys.Control);
				}
			}
			loop = st.nextBoolean(false);
			preLoopKey = st.nextNamedKeyStroke();
			postLoopKey = st.nextNamedKeyStroke();
			loopType = st.nextToken(LoopControl.LOOP_COUNTED);
			whileExpression.Expression = st.nextToken(""); //$NON-NLS-1$
			untilExpression.Expression = st.nextToken(""); //$NON-NLS-1$
			loopCount.Format = st.nextToken(""); //$NON-NLS-1$
			index = st.nextBoolean(false);
			indexProperty = st.nextToken(""); //$NON-NLS-1$
			indexStart.Format = st.nextToken("1");
			indexStep.Format = st.nextToken("1");
		}
		
		/// <summary> Return Property names exposed by this trait</summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getPropertyNames()
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		public override PieceI18nData getI18nData()
		{
			return getI18nData(command, getCommandDescription(name, "Trigger command")); //$NON-NLS-1$
		}
		
		public class Ed : PieceEditor
		{
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
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs arg0)
				{
					Enclosing_Instance.updateVisibility();
				}
			}
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
					return ""; //$NON-NLS-1$
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					SequenceEncoder se = new SequenceEncoder(';');
					se.append(name.ValueString).append(command.ValueString).append(key.ValueString).append(propertyMatch.ValueString).append(watchKeys.ValueString).append(actionKeys.ValueString).append(loopConfig.ValueString).append(preLoopKeyConfig.ValueString).append(postLoopKeyConfig.ValueString).append(LoopControl.loopDescToType(loopTypeConfig.ValueString)).append(whileExpressionConfig.ValueString).append(untilExpressionConfig.ValueString).append(loopCountConfig.ValueString).append(indexConfig.ValueString).append(indexPropertyConfig.ValueString).append(indexStartConfig.ValueString).append(indexStepConfig.ValueString);
					
					return VassalSharp.counters.TriggerAction.ID + se.Value;
				}
				
			}
			
			private StringConfigurer name;
			private StringConfigurer command;
			private NamedHotKeyConfigurer key;
			private PropertyExpressionConfigurer propertyMatch;
			private NamedKeyStrokeArrayConfigurer watchKeys;
			private NamedKeyStrokeArrayConfigurer actionKeys;
			private System.Windows.Forms.Panel box;
			private BooleanConfigurer loopConfig;
			private NamedHotKeyConfigurer preLoopKeyConfig;
			private NamedHotKeyConfigurer postLoopKeyConfig;
			private StringEnumConfigurer loopTypeConfig;
			private PropertyExpressionConfigurer whileExpressionConfig;
			private PropertyExpressionConfigurer untilExpressionConfig;
			private FormattedStringConfigurer loopCountConfig;
			private BooleanConfigurer indexConfig;
			private StringConfigurer indexPropertyConfig;
			private FormattedStringConfigurer indexStartConfig;
			private FormattedStringConfigurer indexStepConfig;
			
			public Ed(TriggerAction piece)
			{
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'updateListener '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				PropertyChangeListener updateListener = new AnonymousClassPropertyChangeListener(this);
				
				box = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				box.setLayout(new BoxLayout(box, BoxLayout.Y_AXIS));
				
				name = new StringConfigurer(null, Resources.getString("Editor.description_label"), piece.name); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(name.Controls);
				
				propertyMatch = new PropertyExpressionConfigurer(null, Resources.getString("Editor.TriggerAction.trigger_when_properties"), piece.propertyMatch, Decorator.getOutermost(piece));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(propertyMatch.Controls);
				
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box commandBox = Box.createHorizontalBox();
				command = new StringConfigurer(null, Resources.getString("Editor.menu_command"), piece.command); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				commandBox.Controls.Add(command.Controls);
				key = new NamedHotKeyConfigurer(null, Resources.getString("Editor.keyboard_command"), piece.key); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				commandBox.Controls.Add(key.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(commandBox);
				
				watchKeys = new NamedKeyStrokeArrayConfigurer(null, Resources.getString("Editor.TriggerAction.watch_for"), piece.watchKeys); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(watchKeys.Controls);
				
				actionKeys = new NamedKeyStrokeArrayConfigurer(null, Resources.getString("Editor.TriggerAction.perform_keystrokes"), piece.actionKeys); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(actionKeys.Controls);
				
				loopConfig = new BooleanConfigurer(null, Resources.getString("Editor.TriggerAction.repeat_this"), piece.loop); //$NON-NLS-1$
				loopConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(updateListener.propertyChange);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(loopConfig.Controls);
				
				loopTypeConfig = new StringEnumConfigurer(null, Resources.getString("Editor.LoopControl.type_of_loop"), LoopControl.LOOP_TYPE_DESCS);
				loopTypeConfig.setValue(LoopControl.loopTypeToDesc(piece.loopType));
				
				loopTypeConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(updateListener.propertyChange);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(loopTypeConfig.Controls);
				
				loopCountConfig = new FormattedExpressionConfigurer(null, Resources.getString("Editor.LoopControl.loop_how_many"), piece.loopCount.Format, piece); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(loopCountConfig.Controls);
				
				whileExpressionConfig = new PropertyExpressionConfigurer(null, Resources.getString("Editor.TriggerAction.looping_continues"), piece.whileExpression); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(whileExpressionConfig.Controls);
				
				untilExpressionConfig = new PropertyExpressionConfigurer(null, Resources.getString("Editor.TriggerAction.looping_ends"), piece.untilExpression); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(untilExpressionConfig.Controls);
				
				preLoopKeyConfig = new NamedHotKeyConfigurer(null, Resources.getString("Editor.TriggerAction.keystroke_before"), piece.preLoopKey);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(preLoopKeyConfig.Controls);
				
				postLoopKeyConfig = new NamedHotKeyConfigurer(null, Resources.getString("Editor.TriggerAction.keystroke_after"), piece.postLoopKey);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(postLoopKeyConfig.Controls);
				
				indexConfig = new BooleanConfigurer(null, Resources.getString("Editor.LoopControl.loop_index"), piece.index); //$NON-NLS-1$
				indexConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(updateListener.propertyChange);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(indexConfig.Controls);
				
				indexPropertyConfig = new StringConfigurer(null, Resources.getString("Editor.LoopControl.index_name"), piece.indexProperty); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(indexPropertyConfig.Controls);
				
				indexStartConfig = new FormattedExpressionConfigurer(null, Resources.getString("Editor.LoopControl.index_start"), piece.indexStart.Format, piece); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(indexStartConfig.Controls);
				
				indexStepConfig = new FormattedExpressionConfigurer(null, Resources.getString("Editor.LoopControl.index_step"), piece.indexStep.Format, piece); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(indexStepConfig.Controls);
				
				updateVisibility();
			}
			
			private void  updateVisibility()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'isLoop '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				bool isLoop = loopConfig.booleanValue();
				//UPGRADE_NOTE: Final was removed from the declaration of 'isIndex '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				bool isIndex = indexConfig.booleanValue();
				//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String type = LoopControl.loopDescToType(loopTypeConfig.ValueString);
				
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(loopTypeConfig.Controls, "Visible", isLoop);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(loopCountConfig.Controls, "Visible", isLoop && type.Equals(LoopControl.LOOP_COUNTED));
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(whileExpressionConfig.Controls, "Visible", isLoop && type.Equals(LoopControl.LOOP_WHILE));
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(untilExpressionConfig.Controls, "Visible", isLoop && type.Equals(LoopControl.LOOP_UNTIL));
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(preLoopKeyConfig.Controls, "Visible", isLoop);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(postLoopKeyConfig.Controls, "Visible", isLoop);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(indexConfig.Controls, "Visible", isLoop);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(indexPropertyConfig.Controls, "Visible", isLoop && isIndex);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(indexStartConfig.Controls, "Visible", isLoop && isIndex);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(indexStepConfig.Controls, "Visible", isLoop && isIndex);
				
				//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
				System.Windows.Forms.Form w = (System.Windows.Forms.Form) box.Parent;
				if (w != null)
				{
					//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
					w.pack();
				}
			}
		}
	}
}