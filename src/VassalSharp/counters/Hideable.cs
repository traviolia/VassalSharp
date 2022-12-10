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
using ChangeTracker = VassalSharp.command.ChangeTracker;
using Command = VassalSharp.command.Command;
using ColorConfigurer = VassalSharp.configure.ColorConfigurer;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using PieceAccessConfigurer = VassalSharp.configure.PieceAccessConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	public class Hideable:Decorator, TranslatablePiece
	{
		private void  InitBlock()
		{
			access = PlayerAccess.Instance;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < String > l = new ArrayList < String >();
			l.add(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_OTHERS);
			return l;
		}
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		override public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				if (invisibleToMe())
				{
					return new System.Drawing.Rectangle();
				}
				else
				{
					return piece.Shape;
				}
			}
			
		}
		virtual public System.String Description
		{
			get
			{
				return "Invisible";
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("Hideable.htm");
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
		
		public const System.String ID = "hide;";
		public const System.String HIDDEN_BY = "hiddenBy";
		public const System.String TRANSPARENCY = "transparency";
		
		protected internal System.String hiddenBy;
		protected internal NamedKeyStroke hideKey;
		protected internal System.String command = "Invisible";
		//UPGRADE_NOTE: The initialization of  'access' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal PieceAccess access;
		
		protected internal float transparency = 0.3f;
		protected internal System.Drawing.Color bgColor;
		
		protected internal KeyCommand[] commands;
		protected internal KeyCommand hideCommand;
		
		public override void  setProperty(System.Object key, System.Object val)
		{
			if (HIDDEN_BY.Equals(key))
			{
				hiddenBy = ((System.String) val);
			}
			else
			{
				base.setProperty(key, val);
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.Object getLocalizedProperty(System.Object key)
		{
			if (invisibleToMe())
			{
				return ((BasicPiece) Decorator.getInnermost(this)).getLocalizedPublicProperty(key);
			}
			else if (HIDDEN_BY.Equals(key))
			{
				return hiddenBy;
			}
			else if (VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME.Equals(key))
			{
				return invisibleToMe()?true:false;
			}
			else if (VassalSharp.counters.Properties_Fields.INVISIBLE_TO_OTHERS.Equals(key))
			{
				return invisibleToOthers()?true:false;
			}
			else if (VassalSharp.counters.Properties_Fields.VISIBLE_STATE.Equals(key))
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return System.Convert.ToString(invisibleToOthers()) + invisibleToMe() + piece.getProperty(key);
			}
			else
			{
				return base.getLocalizedProperty(key);
			}
		}
		
		public override System.Object getProperty(System.Object key)
		{
			if (HIDDEN_BY.Equals(key))
			{
				return hiddenBy;
			}
			else if (VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME.Equals(key))
			{
				return invisibleToMe()?true:false;
			}
			else if (VassalSharp.counters.Properties_Fields.INVISIBLE_TO_OTHERS.Equals(key))
			{
				return invisibleToOthers()?true:false;
			}
			else if (VassalSharp.counters.Properties_Fields.VISIBLE_STATE.Equals(key))
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return System.Convert.ToString(invisibleToOthers()) + invisibleToMe() + piece.getProperty(key);
			}
			else
			{
				return base.getProperty(key);
			}
		}
		
		public Hideable():this(ID + "I", null)
		{
		}
		
		public Hideable(System.String type, GamePiece p)
		{
			InitBlock();
			setInner(p);
			mySetType(type);
		}
		
		public virtual void  mySetType(System.String type)
		{
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			st.nextToken();
			hideKey = st.nextNamedKeyStroke('I');
			command = st.nextToken("Invisible");
			System.Drawing.Color tempAux = System.Drawing.Color.Empty;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			bgColor = st.nextColor(ref tempAux);
			access = PieceAccessConfigurer.decode(st.nextToken(null));
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			transparency = st.hasMoreTokens()?(float) st.nextDouble(0.3):0.3f;
			commands = null;
		}
		
		public override void  mySetState(System.String in_Renamed)
		{
			hiddenBy = "null".Equals(in_Renamed)?null:in_Renamed;
		}
		
		public override System.String myGetType()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder se = new SequenceEncoder(';');
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			se.append(hideKey).append(command).append(ref bgColor).append(PieceAccessConfigurer.encode(access)).append(transparency);
			return ID + se.Value;
		}
		
		public override System.String myGetState()
		{
			return hiddenBy == null?"null":hiddenBy;
		}
		
		public virtual bool invisibleToMe()
		{
			return !access.currentPlayerHasAccess(hiddenBy);
		}
		
		public virtual bool invisibleToOthers()
		{
			return hiddenBy != null;
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			if (invisibleToMe())
			{
				return new System.Drawing.Rectangle();
			}
			else
			{
				return piece.boundingBox();
			}
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			if (invisibleToMe())
			{
				return ;
			}
			else if (invisibleToOthers())
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'g2d '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Graphics g2d = (System.Drawing.Graphics) g;
				
				if (!bgColor.IsEmpty)
				{
					SupportClass.GraphicsManager.manager.SetColor(g, bgColor);
					//UPGRADE_NOTE: Final was removed from the declaration of 't '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Drawing2D.Matrix temp_Matrix;
					temp_Matrix = new System.Drawing.Drawing2D.Matrix();
					temp_Matrix.Scale((float) zoom, (float) zoom);
					System.Drawing.Drawing2D.Matrix t = temp_Matrix;
					t.Translate((System.Single) (x / zoom), (System.Single) (y / zoom));
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.fill' was converted to 'System.Drawing.Graphics.FillPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Dfill_javaawtShape'"
					//UPGRADE_ISSUE: Method 'java.awt.geom.AffineTransform.createTransformedShape' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomAffineTransformcreateTransformedShape_javaawtShape'"
					g2d.FillPath(t.createTransformedShape(piece.Shape));
				}
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'oldComposite '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Interface 'java.awt.Composite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComposite'"
				//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.getComposite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DgetComposite'"
				Composite oldComposite = g2d.getComposite();
				//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setComposite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetComposite_javaawtComposite'"
				//UPGRADE_ISSUE: Method 'java.awt.AlphaComposite.getInstance' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAlphaComposite'"
				//UPGRADE_ISSUE: Field 'java.awt.AlphaComposite.SRC_OVER' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAlphaComposite'"
				g2d.setComposite(AlphaComposite.getInstance(AlphaComposite.SRC_OVER, transparency));
				piece.draw(g, x, y, obs, zoom);
				//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setComposite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetComposite_javaawtComposite'"
				g2d.setComposite(oldComposite);
			}
			else
			{
				piece.draw(g, x, y, obs, zoom);
			}
		}
		
		public override System.String getName()
		{
			if (invisibleToMe())
			{
				return "";
			}
			else if (invisibleToOthers())
			{
				return piece.getName() + "(" + command + ")";
			}
			else
			{
				return piece.getName();
			}
		}
		
		public override KeyCommand[] myGetKeyCommands()
		{
			if (commands == null)
			{
				hideCommand = new KeyCommand(command, hideKey, Decorator.getOutermost(this), this);
				if (command.Length > 0 && hideKey != null && !hideKey.Null)
				{
					commands = new KeyCommand[]{hideCommand};
				}
				else
				{
					commands = new KeyCommand[0];
				}
			}
			hideCommand.setEnabled(access.currentPlayerCanModify(hiddenBy));
			return commands;
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			myGetKeyCommands();
			if (hideCommand.matches(stroke))
			{
				ChangeTracker tracker = new ChangeTracker(this);
				if (invisibleToOthers())
				{
					hiddenBy = null;
				}
				else if (!invisibleToMe())
				{
					hiddenBy = access.CurrentPlayerId;
				}
				return tracker.ChangeCommand;
			}
			return null;
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		/// <summary> If true, then all hidden pieces are considered invisible to all players.
		/// Used to temporarily draw pieces as they appear to other players
		/// 
		/// </summary>
		/// <param name="allHidden">
		/// </param>
		/// <deprecated>
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		
		public override PieceI18nData getI18nData()
		{
			return getI18nData(command, "Hide command");
		}
		
		/// <summary> Return Property names exposed by this trait</summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getPropertyNames()
		
		protected internal class Ed : PieceEditor
		{
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
					se.append(hideKeyInput.ValueString).append(hideCommandInput.Text).append(colorConfig.getValue() == null?"":colorConfig.ValueString).append(accessConfig.ValueString).append(transpConfig.getIntValue(30) / 100.0f);
					return VassalSharp.counters.Hideable.ID + se.Value;
				}
				
			}
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return controls;
				}
				
			}
			protected internal NamedHotKeyConfigurer hideKeyInput;
			protected internal System.Windows.Forms.TextBox hideCommandInput;
			protected internal ColorConfigurer colorConfig;
			protected internal IntConfigurer transpConfig;
			protected internal PieceAccessConfigurer accessConfig;
			protected internal System.Windows.Forms.Panel controls;
			
			public Ed(Hideable p)
			{
				controls = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				controls.setLayout(new BoxLayout(controls, BoxLayout.Y_AXIS));
				
				hideKeyInput = new NamedHotKeyConfigurer(null, "Keyboard command:  ", p.hideKey);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(hideKeyInput.Controls);
				
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box b = Box.createHorizontalBox();
				//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
				hideCommandInput = new System.Windows.Forms.TextBox();
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
				hideCommandInput.setMaximumSize(hideCommandInput.Size);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				hideCommandInput.Text = p.command;
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = "Menu Text:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				b.Controls.Add(temp_Control);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(hideCommandInput);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(b);
				
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				colorConfig = new ColorConfigurer(null, "Background color:  ", ref p.bgColor);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(colorConfig.Controls);
				
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				transpConfig = new IntConfigurer(null, "Transparency (%):  ", (int) (p.transparency * 100));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(transpConfig.Controls);
				
				accessConfig = new PieceAccessConfigurer(null, "Can by hidden by:  ", p.access);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(accessConfig.Controls);
			}
		}
	}
}