/*
* Copyright (c) 2008 by Michael Kiefte
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
//UPGRADE_TODO: The type 'javax.imageio.ImageIO' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ImageIO = javax.imageio.ImageIO;
//UPGRADE_TODO: The type 'org.apache.commons.io.FileUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using FileUtils = org.apache.commons.io.FileUtils;
using Info = VassalSharp.Info;
using AbstractConfigurable = VassalSharp.build.AbstractConfigurable;
using GameModule = VassalSharp.build.GameModule;
using GlobalOptions = VassalSharp.build.module.GlobalOptions;
using Inventory = VassalSharp.build.module.Inventory;
using Map = VassalSharp.build.module.Map;
using PrototypeDefinition = VassalSharp.build.module.PrototypeDefinition;
using PrototypesContainer = VassalSharp.build.module.PrototypesContainer;
using ToolbarMenu = VassalSharp.build.module.ToolbarMenu;
using BoardPicker = VassalSharp.build.module.map.BoardPicker;
using LayerControl = VassalSharp.build.module.map.LayerControl;
using LayeredPieceCollection = VassalSharp.build.module.map.LayeredPieceCollection;
using SetupStack = VassalSharp.build.module.map.SetupStack;
using Zoomer = VassalSharp.build.module.map.Zoomer;
using Board = VassalSharp.build.module.map.boardPicker.Board;
using HexGrid = VassalSharp.build.module.map.boardPicker.board.HexGrid;
using MapGrid = VassalSharp.build.module.map.boardPicker.board.MapGrid;
using BadCoords = VassalSharp.build.module.map.boardPicker.board.MapGrid.BadCoords;
using SquareGrid = VassalSharp.build.module.map.boardPicker.board.SquareGrid;
using ZonedGrid = VassalSharp.build.module.map.boardPicker.board.ZonedGrid;
using HexGridNumbering = VassalSharp.build.module.map.boardPicker.board.mapgrid.HexGridNumbering;
using RegularGridNumbering = VassalSharp.build.module.map.boardPicker.board.mapgrid.RegularGridNumbering;
using SquareGridNumbering = VassalSharp.build.module.map.boardPicker.board.mapgrid.SquareGridNumbering;
using Zone = VassalSharp.build.module.map.boardPicker.board.mapgrid.Zone;
using PieceSlot = VassalSharp.build.widget.PieceSlot;
using StringArrayConfigurer = VassalSharp.configure.StringArrayConfigurer;
using BasicPiece = VassalSharp.counters.BasicPiece;
using GamePiece = VassalSharp.counters.GamePiece;
using Immobilized = VassalSharp.counters.Immobilized;
using Marker = VassalSharp.counters.Marker;
using UsePrototype = VassalSharp.counters.UsePrototype;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
using ExtensionFileFilter = VassalSharp.tools.filechooser.ExtensionFileFilter;
using FileFormatException = VassalSharp.tools.imports.FileFormatException;
using Importer = VassalSharp.tools.imports.Importer;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.tools.imports.adc2
{
	
	/// <summary> The map board itself.
	/// 
	/// </summary>
	/// <author>  Michael Kiefte
	/// 
	/// </author>
	public class MapBoard:Importer
	{
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			DASH_DOT(new float []
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				12.0f, 8.0f, 4.0f, 8.0f
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			), 
			DASH_DOT_DOT(new float []
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				12.f, 4.0f, 4.0f, 4.0f, 4.0f, 4.0f
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			), 
			DASHED(new float []
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				12.0f, 8.0f
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			), 
			DOTTED(new float []
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				4.0f, 4.0f
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			), 
			SOLID(null);
		}
		
		private const System.String PLACE_NAME = "Location Names";
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MapLayer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		protected internal class MapLayer
		{
			public MapLayer(MapBoard enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(MapBoard enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				this.elements = elements;
				this.name = name;
				this.switchable = switchable;
			}
			private MapBoard enclosingInstance;
			//UPGRADE_ISSUE: Class 'java.awt.AlphaComposite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAlphaComposite'"
			virtual protected internal AlphaComposite Composite
			{
				get
				{
					return AlphaComposite.SrcAtop;
				}
				
			}
			virtual internal System.Drawing.Bitmap LayerImage
			{
				get
				{
					System.Drawing.Size d = getLayout().getBoardSize();
					System.Drawing.Bitmap image = new System.Drawing.Bitmap(d.Width, d.Height, (System.Drawing.Imaging.PixelFormat) System.Drawing.Imaging.PixelFormat.Format32bppArgb);
					System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
					if (draw(g))
					{
						if (layers != null)
						{
							//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setComposite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetComposite_javaawtComposite'"
							g.setComposite(Composite);
							//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
							for(MapLayer l: layers)
							{
								l.draw(g);
							}
						}
					}
					else
					{
						image = null;
					}
					return image;
				}
				
			}
			virtual internal bool Switchable
			{
				get
				{
					return switchable;
				}
				
			}
			virtual internal System.String Name
			{
				get
				{
					return name;
				}
				
			}
			public MapBoard Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private final ArrayList < ? extends MapDrawable > elements;
			//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private System.String name;
			//UPGRADE_NOTE: Final was removed from the declaration of 'switchable '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private bool switchable;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			protected ArrayList < MapLayer > layers = null;
			protected internal System.String imageName;
			internal bool shouldDraw = true;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			MapLayer(ArrayList < ? extends MapDrawable > elements, String name, boolean switchable)
			
			internal virtual void  writeToArchive()
			{
				// write piece
				System.Drawing.Rectangle r = writeImageToArchive();
				if (imageName != null && !r.IsEmpty && r.Width > 0 && r.Height > 0)
				{
					SequenceEncoder se = new SequenceEncoder(';');
					se.append("").append("").append(imageName).append(Name);
					GamePiece gp = new BasicPiece(BasicPiece.ID + se.Value);
					gp = new Marker(Marker.ID + "Layer", gp);
					gp.setProperty("Layer", Name);
					gp = new Marker(Marker.ID + "Type", gp);
					gp.setProperty("Type", "Layer");
					gp = new Immobilized(gp, Immobilized.ID + "n;V");
					
					// create layer
					LayeredPieceCollection l = Enclosing_Instance.LayeredPieceCollection;
					System.String order = l.getAttributeValueString(LayeredPieceCollection.LAYER_ORDER);
					if (order.Equals(""))
					{
						order = Name;
					}
					else
					{
						order = order + "," + Name;
					}
					l.setAttribute(LayeredPieceCollection.LAYER_ORDER, order);
					
					Map mainMap = Enclosing_Instance.MainMap;
					Board board = getBoard();
					SetupStack stack = new SetupStack();
					VassalSharp.tools.imports.Importer.insertComponent(stack, mainMap);
					System.Drawing.Point p = new System.Drawing.Point(r.X + r.Width / 2, r.Y + r.Height / 2);
					stack.setAttribute(SetupStack.NAME, Name);
					stack.setAttribute(SetupStack.OWNING_BOARD, board.getConfigureName());
					stack.setAttribute(SetupStack.X_POSITION, System.Convert.ToString(p.X));
					stack.setAttribute(SetupStack.Y_POSITION, System.Convert.ToString(p.Y));
					
					PieceSlot slot = new PieceSlot(gp);
					VassalSharp.tools.imports.Importer.insertComponent(slot, stack);
					
					if (Switchable)
					{
						// TODO: initial state of layer visibility
						// add stack layer control
						LayerControl control = new LayerControl();
						VassalSharp.tools.imports.Importer.insertComponent(control, l);
						control.setAttribute(LayerControl.BUTTON_TEXT, Name);
						control.setAttribute(LayerControl.TOOLTIP, "Toggle " + Name.ToLower() + " visibility");
						control.setAttribute(LayerControl.COMMAND, LayerControl.CMD_TOGGLE);
						control.setAttribute(LayerControl.LAYERS, Name);
						
						// one toolbar menu to control all mapboard elements.
						ToolbarMenu menu = getToolbarMenu();
						System.String entries = menu.getAttributeValueString(ToolbarMenu.MENU_ITEMS);
						if (entries.Equals(""))
						{
							entries = Name;
						}
						else
						{
							entries = entries + "," + new SequenceEncoder(Name, ',').Value;
						}
						menu.setAttribute(ToolbarMenu.MENU_ITEMS, entries);
					}
				}
			}
			
			/// <throws>  IOException </throws>
			protected internal virtual System.Drawing.Rectangle writeImageToArchive()
			{
				// write image to archive
				//UPGRADE_NOTE: Final was removed from the declaration of 'image '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Bitmap image = LayerImage;
				if (image == null)
				{
					return System.Drawing.Rectangle.Empty;
				}
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Rectangle r = getCropRectangle(image);
				if (r.Width == 0 || r.Height == 0)
				{
					return System.Drawing.Rectangle.Empty;
				}
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Method 'java.io.File.createTempFile' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioFilecreateTempFile_javalangString_javalangString_javaioFile'"
				System.IO.FileInfo f = File.createTempFile("map", ".png", Info.TempDir);
				try
				{
					ImageIO.write((System.Drawing.Bitmap) image.Clone(new System.Drawing.Rectangle(r.X, r.Y, r.Width, r.Height), image.PixelFormat), "png", f);
					imageName = getUniqueImageFileName(Name, ".png");
					GameModule.getGameModule().getArchiveWriter().addImage(f.FullName, imageName);
					return r;
				}
				finally
				{
					FileUtils.forceDelete(f);
				}
			}
			
			protected internal virtual System.Drawing.Rectangle getCropRectangle(System.Drawing.Bitmap image)
			{
				System.Drawing.Rectangle r = new Rectangle(getLayout().getBoardSize());
				while (true)
				{
					for (int i = r.Y; i < r.Y + r.Height; ++i)
					{
						if (image.GetPixel(r.X, i).ToArgb() != 0)
						{
							//UPGRADE_NOTE: Labeled break statement was changed to a goto statement. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1012'"
							goto leftside_brk;
						}
					}
					++r.X;
					--r.Width;
					if (r.Width == 0)
					{
						r.Height = 0;
						return r;
					}
				}
				//UPGRADE_NOTE: Label 'leftside_brk' was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1011'"
leftside_brk: ;
				
				while (true)
				{
					for (int i = r.X; i < r.X + r.Width; ++i)
					{
						if (image.GetPixel(i, r.Y).ToArgb() != 0)
						{
							//UPGRADE_NOTE: Labeled break statement was changed to a goto statement. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1012'"
							goto topside_brk;
						}
					}
					++r.Y;
					--r.Height;
				}
				//UPGRADE_NOTE: Label 'topside_brk' was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1011'"
topside_brk: ;
				
				while (true)
				{
					for (int i = r.Y; i < r.Y + r.Height; ++i)
					{
						if (image.GetPixel(r.X + r.Width - 1, i).ToArgb() != 0)
						{
							//UPGRADE_NOTE: Labeled break statement was changed to a goto statement. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1012'"
							goto rightside_brk;
						}
					}
					--r.Width;
				}
				//UPGRADE_NOTE: Label 'rightside_brk' was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1011'"
rightside_brk: ;
				
				while (true)
				{
					for (int i = r.X; i < r.X + r.Width; ++i)
					{
						if (image.GetPixel(i, r.Y + r.Height - 1).ToArgb() != 0)
						{
							//UPGRADE_NOTE: Labeled break statement was changed to a goto statement. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1012'"
							goto bottomside_brk;
						}
					}
					--r.Height;
				}
				//UPGRADE_NOTE: Label 'bottomside_brk' was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1011'"
bottomside_brk: ;
				
				return r;
			}
			
			internal virtual void  overlay(MapLayer layer)
			{
				if (layers == null)
				{
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					layers = new ArrayList < MapLayer >();
				}
				layers.add(layer);
			}
			
			internal virtual bool draw(System.Drawing.Graphics g)
			{
				if (shouldDraw)
				{
					shouldDraw = false;
					//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setRenderingHint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetRenderingHint_javaawtRenderingHintsKey_javalangObject'"
					//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_ANTIALIASING' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_ANTIALIASING_f'"
					//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.VALUE_ANTIALIAS_OFF' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsVALUE_ANTIALIAS_OFF_f'"
					g.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_OFF);
					//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setRenderingHint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetRenderingHint_javaawtRenderingHintsKey_javalangObject'"
					//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_RENDERING' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_RENDERING_f'"
					g.setRenderingHint(RenderingHints.KEY_RENDERING, (System.Object) System.Drawing.Drawing2D.CompositingQuality.HighQuality);
					//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setRenderingHint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetRenderingHint_javaawtRenderingHintsKey_javalangObject'"
					//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_STROKE_CONTROL' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_STROKE_CONTROL_f'"
					//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.VALUE_STROKE_PURE' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsVALUE_STROKE_PURE_f'"
					g.setRenderingHint(RenderingHints.KEY_STROKE_CONTROL, RenderingHints.VALUE_STROKE_PURE);
					
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(MapDrawable m: elements)
					{
						if (m.draw(g))
						{
							shouldDraw = true;
						}
					}
				}
				return shouldDraw;
			}
			
			internal virtual bool hasElements()
			{
				return !elements.isEmpty();
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'BaseLayer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		internal class BaseLayer:MapLayer
		{
			private void  InitBlock(MapBoard enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				// set background color
				g.setBackground(tableColor);
				System.Drawing.Size d = getLayout().getBoardSize();
				g.clearRect(0, 0, d.Width, d.Height);
				
				// See if map image file exists
				System.IO.FileInfo sml = Enclosing_Instance.action.getCaseInsensitiveFile(new File(forceExtension(path, "sml")), null, false, null);
				if (sml != null)
				{
					try
					{
						readScannedMapLayoutFile(sml, g);
					}
					// FIXME: review error message
					catch (System.IO.IOException e)
					{
					}
				}
				else if (getSet().underlay != null)
				{
					// If sml file doesn't exist, see if there is a single-sheet underlay image
					g.drawImage(getSet().underlay, null, 0, 0);
				}
				return true;
			}
			private MapBoard enclosingInstance;
			//UPGRADE_ISSUE: Class 'java.awt.AlphaComposite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAlphaComposite'"
			override protected internal AlphaComposite Composite
			{
				get
				{
					//UPGRADE_ISSUE: Field 'java.awt.AlphaComposite.SrcOver' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAlphaComposite'"
					return AlphaComposite.SrcOver;
				}
				
			}
			public new MapBoard Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			internal BaseLayer(MapBoard enclosingInstance):base(enclosingInstance, null, "Base Layer", false)
			{
				InitBlock(enclosingInstance);
			}
			
			internal virtual bool hasBaseMap()
			{
				System.IO.FileInfo underlay = Enclosing_Instance.action.getCaseInsensitiveFile(new File(forceExtension(path, "sml")), null, false, null);
				if (underlay == null)
				{
					underlay = Enclosing_Instance.action.getCaseInsensitiveFile(new System.IO.FileInfo(stripExtension(path) + "-Z" + (zoomLevel + 1) + ".bmp"), null, false, null);
				}
				return underlay != null;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override 
			boolean draw(Graphics2D g)
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			internal override void  writeToArchive()
			{
				// write the underlay map image
				writeImageToArchive();
				assert(imageName != null);
				Board board = getBoard();
				board.setAttribute(Board.IMAGE, imageName);
				board.setConfigureName(baseName);
				
				// so we can get hex labels
				Enclosing_Instance.MainMap.setBoards(new SupportClass.HashSetSupport(new System.Object[]{board}));
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			protected internal override System.Drawing.Rectangle getCropRectangle(System.Drawing.Bitmap image)
			{
				return new Rectangle(getLayout().getBoardSize());
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'GridLayout' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> A layout consisting of squares in a checkerboard pattern (<it>i.e.</it> each
		/// square has four neighbours).
		/// 
		/// </summary>
		/// <author>  Michael Kiefte
		/// 
		/// </author>
		protected internal class GridLayout:Layout
		{
			private void  InitBlock(MapBoard enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				return HexSize; return HexSize;
				return 4;
			}
			private MapBoard enclosingInstance;
			public MapBoard Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			internal GridLayout(MapBoard enclosingInstance, int size, int columns, int rows):base(size, columns, rows)
			{
				InitBlock(enclosingInstance);
			}
			
			internal Override Point;
			internal coordinatesToPosition(MapBoard enclosingInstance, int x, int y, bool nullIfOffBoard)
			{
				InitBlock(enclosingInstance);
				if (!nullIfOffBoard || isOnMapBoard(x, y))
				{
					int xx = DeltaX * x;
					int yy = DeltaY * y;
					return new System.Drawing.Point(xx, yy);
				}
				else
				{
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				}
			}
			
			internal Override Dimension;
			internal getBoardSize(MapBoard enclosingInstance)
			{
				InitBlock(enclosingInstance);
				System.Drawing.Size d = new System.Drawing.Size(0, 0);
				d.Width = DeltaX * nColumns;
				d.Height = DeltaY * nRows;
				return d;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override 
			int getDeltaX()
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override 
			int getDeltaY()
			
			internal Override Point;
			internal getOrigin(MapBoard enclosingInstance)
			{
				InitBlock(enclosingInstance);
				return new System.Drawing.Point(HexSize / 2, HexSize / 2);
			}
			
			internal Override SquareGrid;
			internal getGeometricGrid(MapBoard enclosingInstance)
			{
				InitBlock(enclosingInstance);
				SquareGrid grid = new SquareGrid();
				System.Drawing.Point tempAux = getOrigin();
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				grid.setOrigin(ref tempAux);
				grid.Dx = DeltaX;
				grid.Dy = DeltaY;
				
				return grid;
			}
			
			internal Override Rectangle;
			internal getRectangle(MapBoard enclosingInstance, MapSheet map)
			{
				InitBlock(enclosingInstance);
				System.Drawing.Rectangle r = map.Field;
				
				System.Drawing.Point upperLeft = coordinatesToPosition(r.X, r.Y, false);
				System.Drawing.Point lowerRight = coordinatesToPosition(r.X + r.Width - 1, r.Y + r.Height - 1, false);
				
				// get lower right-hand corner of lower right-hand square
				lowerRight.X += HexSize - 1;
				lowerRight.Y += HexSize - 1;
				
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				constrainRectangle(ref upperLeft, ref lowerRight);
				
				return new System.Drawing.Rectangle(upperLeft.X, upperLeft.Y, lowerRight.X - upperLeft.X + 1, lowerRight.Y - upperLeft.Y + 1);
			}
			
			internal Override RegularGridNumbering;
			internal getGridNumbering(MapBoard enclosingInstance)
			{
				InitBlock(enclosingInstance);
				return new SquareGridNumbering();
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override 
			int getNFaces()
		}
		
		/// <summary> Redundant information about each hex. So far only used for determining
		/// the default order of line definitions for hex sides and hex lines.
		/// </summary>
		private class Hex
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < Line > hexLines = new ArrayList < Line >();
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < Line > hexSides = new ArrayList < Line >();
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'HexData' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Mapboard element based on terrain symbol from <code>SymbolSet</code>. Not necessarily hexagonal but can also be square.</summary>
		protected internal class HexData:MapDrawable
		{
			private void  InitBlock(MapBoard enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				System.Drawing.Point p = getPosition();
				if (symbol != null && !symbol.Transparent)
				{
					g.drawImage(symbol.getImage(), null, p.X, p.Y);
					return true;
				}
				else
				{
					return false;
				}
			}
			private MapBoard enclosingInstance;
			public MapBoard Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'symbol '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			internal SymbolSet.SymbolData symbol;
			
			internal HexData(MapBoard enclosingInstance, int index, SymbolSet.SymbolData symbol):base(index)
			{
				InitBlock(enclosingInstance);
				assert(symbol != null);
				this.symbol = symbol;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override 
			boolean draw(Graphics2D g)
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MapBoardOverlay' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Symbol that is placed in every hex.</summary>
		protected internal class MapBoardOverlay:HexData
		{
			private void  InitBlock(MapBoard enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				if (symbol != null)
				{
					for (int y = 0; y < getNRows(); ++y)
					{
						for (int x = 0; x < getNColumns(); ++x)
						{
							System.Drawing.Point p = coordinatesToPosition(x, y);
							g.drawImage(symbol.getImage(), null, p.X, p.Y);
						}
					}
					return true;
				}
				else
				{
					return false;
				}
			}
			private MapBoard enclosingInstance;
			public new MapBoard Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override 
			boolean draw(Graphics2D g)
			
			// doesn't have an index
			internal MapBoardOverlay(MapBoard enclosingInstance, SymbolSet.SymbolData symbol):base(enclosingInstance, - 1, symbol)
			{
				InitBlock(enclosingInstance);
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'HexLine' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> A line from a hex edge to the centre as in the spoke of a wheel. Typically used for terrain
		/// features such as roads etc.
		/// </summary>
		protected internal class HexLine:Line
		{
			private void  InitBlock(MapBoard enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				if (o1 == null && o2 == null)
					return 0;
				else if (o1 == null)
					return 1;
				else if (o2 == null)
					return - 1;
				int priority = o1.getHexLineDrawPriority() - o2.getHexLineDrawPriority();
				if (priority != 0)
					return priority;
				else
					return base.compare(o1, o2);
				LineDefinition l = getLine();
				bool result = false;
				
				if (l != null)
				{
					result = true;
					System.Drawing.Point pos = getPosition();
					int size = getLayout().getHexSize();
					pos.Offset(size / 2, size / 2);
					Layout lo = getLayout();
					
					// if ((direction & 0x1) > 0) // horizontal north west
					if ((direction & 0x6) > 0)
					{
						// north west; 0x4 = version 1
						System.Drawing.Point nw = lo.getNorthWest(hexIndex);
						nw.Offset(size / 2, size / 2);
						l.addLine(pos.X, pos.Y, (pos.X + nw.X) / 2.0f, (pos.Y + nw.Y) / 2.0f);
					}
					if ((direction & 0x8) > 0)
					{
						// west
						System.Drawing.Point w = lo.getWest(hexIndex);
						w.Offset(size / 2, size / 2);
						l.addLine(pos.X, pos.Y, (pos.X + w.X) / 2.0f, (pos.Y + w.Y) / 2.0f);
					}
					if ((direction & 0x30) > 0)
					{
						// south west; 0x10 = version 1
						System.Drawing.Point sw = lo.getSouthWest(hexIndex);
						sw.Offset(size / 2, size / 2);
						l.addLine(pos.X, pos.Y, (pos.X + sw.X) / 2.0f, (pos.Y + sw.Y) / 2.0f);
					}
					// if ((direction & 0x40) > 0) // horizontal south west
					if ((direction & 0x80) > 0)
					{
						// south
						System.Drawing.Point s = lo.getSouth(hexIndex);
						s.Offset(size / 2, size / 2);
						l.addLine(pos.X, pos.Y, (pos.X + s.X) / 2.0f, (pos.Y + s.Y) / 2.0f);
					}
					if ((direction & 0x100) > 0)
					{
						// north
						System.Drawing.Point n = lo.getNorth(hexIndex);
						n.Offset(size / 2, size / 2);
						l.addLine(pos.X, pos.Y, (pos.X + n.X) / 2.0f, (pos.Y + n.Y) / 2.0f);
					}
					// if ((direction & 0x200) > 0) // horizontal north east
					if ((direction & 0xC00) > 0)
					{
						// north east; 0x800 = version 1
						System.Drawing.Point ne = lo.getNorthEast(hexIndex);
						ne.Offset(size / 2, size / 2);
						l.addLine(pos.X, pos.Y, (pos.X + ne.X) / 2.0f, (pos.Y + ne.Y) / 2.0f);
					}
					if ((direction & 0x1000) > 0)
					{
						// east
						System.Drawing.Point e = lo.getEast(hexIndex);
						e.Offset(size / 2, size / 2);
						l.addLine(pos.X, pos.Y, (pos.X + e.X) / 2.0f, (pos.Y + e.Y) / 2.0f);
					}
					if ((direction & 0x6000) > 0)
					{
						// south east; 0x2000 = version 1
						System.Drawing.Point se = lo.getSouthEast(hexIndex);
						se.Offset(size / 2, size / 2);
						l.addLine(pos.X, pos.Y, (pos.X + se.X) / 2.0f, (pos.Y + se.Y) / 2.0f);
					}
					// if ((direction & 0x8000) > 0) // horizontal south east
				}
				
				// if this is the last one, draw all of the compiled lines.
				if (this == hexLines.get_Renamed(hexLines.size() - 1))
				{
					drawLines(g, (int) System.Drawing.Drawing2D.LineCap.Flat);
				}
				
				return result;
				return h.hexLines;
			}
			private MapBoard enclosingInstance;
			public MapBoard Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'direction '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private int direction;
			
			internal HexLine(MapBoard enclosingInstance, int index, int line, int direction):base(index, line)
			{
				InitBlock(enclosingInstance);
				this.direction = direction;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override 
			int compare(LineDefinition o1, LineDefinition o2)
			
			/*
			* Under normal circumstances, map board elements get drawn one at a time. Hex sides and hex lines are
			* actually continuous from one hex or square to the next. Although ADC2 draws each segment separately
			* anyway, this creates poor-looking corner and edge effects.  Instead of that, the importer composes longer
			* lines made up of many continuous smaller segments and then draws a single line in one go.  To do this,
			* instead of drawing itself as it's called, each segment adds itself to a line and the last segment in the
			* list calls the draw method for all of the lines thus created.
			*/
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override 
			boolean draw(Graphics2D g)
			
			internal Override ArrayList;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< Line > getLineList(Hex h)
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'HexSide' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> The edges of a hex or square.</summary>
		protected internal class HexSide:Line
		{
			private void  InitBlock(MapBoard enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				if (o1 == null && o2 == null)
					return 0;
				else if (o1 == null)
					return 1;
				else if (o2 == null)
					return - 1;
				int priority = o1.getHexSideDrawPriority() - o2.getHexSideDrawPriority();
				if (priority != 0)
					return priority;
				else
					return base.compare(o1, o2);
				
				LineDefinition l = getLine();
				bool result = false;
				
				if (l != null)
				{
					result = true;
					System.Drawing.Point p = getPosition();
					int size = getLayout().getHexSize();
					int dX = getLayout().getDeltaX();
					int dY = getLayout().getDeltaY();
					
					if ((side & 0x1) > 0)
					{
						// vertical SW
						System.Drawing.Point sw = getLayout().getSouthWest(hexIndex);
						sw.Offset(dX, 0);
						System.Drawing.Point s = getLayout().getSouth(hexIndex);
						l.addLine(p.X, sw.Y, p.X + (size / 5), s.Y);
					}
					if ((side & 0x2) > 0)
					{
						// vertical NW
						System.Drawing.Point sw = getLayout().getSouthWest(hexIndex);
						sw.Offset(dX, 0);
						l.addLine(p.X, sw.Y, p.X + (size / 5), p.Y);
					}
					if ((side & 0x4) > 0)
					{
						// vertical N
						l.addLine(p.X + (size / 5), p.Y, p.X + dX, p.Y);
					}
					if ((side & 0x8) > 0)
					{
						// horizontal SW
						System.Drawing.Point se = getLayout().getSouthEast(hexIndex);
						l.addLine(p.X, p.Y + dY, se.X, p.Y + dY + (size / 5));
					}
					if ((side & 0x10) > 0)
					{
						// horizontal W
						l.addLine(p.X, p.Y + (size / 5), p.X, p.Y + dY);
					}
					if ((side & 0x20) > 0)
					{
						// horizontal NW
						System.Drawing.Point ne = getLayout().getNorthEast(hexIndex);
						l.addLine(p.X, p.Y + (size / 5), ne.X, p.Y);
					}
					if ((side & 0x40) > 0)
					{
						// square left
						l.addLine(p.X, p.Y, p.X, p.Y + dY);
					}
					if ((side & 0x80) > 0)
					{
						// square top
						l.addLine(p.X, p.Y, p.X + dX, p.Y);
					}
				}
				
				// if this is the last one, draw all the lines.
				if (this == hexSides.get_Renamed(hexSides.size() - 1))
				{
					drawLines(g, (int) System.Drawing.Drawing2D.LineCap.Round);
				}
				
				return result;
				return h.hexSides;
			}
			private MapBoard enclosingInstance;
			public MapBoard Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			// flags indicating which side to draw.
			//UPGRADE_NOTE: Final was removed from the declaration of 'side '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private int side;
			
			internal HexSide(MapBoard enclosingInstance, int index, int line, int side):base(index, line)
			{
				InitBlock(enclosingInstance);
				this.side = side;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override 
			int compare(LineDefinition o1, LineDefinition o2)
			
			// see the comments for HexLine.draw(Graphics2D).
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override 
			boolean draw(Graphics2D g)
			
			internal Override ArrayList;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< Line > getLineList(Hex h)
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'HorizontalHexLayout' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Hexes aligned along rows.</summary>
		protected internal class HorizontalHexLayout:HorizontalLayout
		{
			private void  InitBlock(MapBoard enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				return HexSize - (isPreV208Layout()?2:0); return HexSize * 4 / 5 - 1;
			}
			private MapBoard enclosingInstance;
			public MapBoard Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			internal HorizontalHexLayout(MapBoard enclosingInstance, int size, int columns, int rows):base(size, columns, rows)
			{
				InitBlock(enclosingInstance);
			}
			
			internal Override Dimension;
			internal getBoardSize(MapBoard enclosingInstance)
			{
				InitBlock(enclosingInstance);
				System.Drawing.Size d = new System.Drawing.Size(0, 0);
				d.Width = DeltaX * nColumns + HexSize / 2;
				d.Height = DeltaY * nRows + HexSize / 5 + 1;
				return d;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override 
			int getDeltaX()
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override 
			int getDeltaY()
			
			new internal Override Point;
			internal getOrigin(MapBoard enclosingInstance)
			{
				InitBlock(enclosingInstance);
				return new System.Drawing.Point(HexSize / 2, HexSize / 2 - (isPreV208Layout()?1:0));
			}
			
			internal Override HexGrid;
			internal getGeometricGrid(MapBoard enclosingInstance)
			{
				InitBlock(enclosingInstance);
				HexGrid mg = new HexGrid();
				
				mg.Sideways = true;
				
				// VASSAL defines these sideways. Height always refers to the major
				// dimension, and Dy always refers to height whether they're sideways or not.
				System.Drawing.Point tempAux = getOrigin();
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				mg.setOrigin(ref tempAux);
				mg.Dy = DeltaX;
				mg.Dx = DeltaY;
				
				return mg;
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'GridOffsetRowLayout' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> A layout consisting of squares in which every second row is shifted to the
		/// right by one half-width. Used to approximate hexagons as each square has
		/// six neighbours.
		/// 
		/// </summary>
		/// <author>  Michael Kiefte
		/// 
		/// </author>
		protected internal class GridOffsetRowLayout:HorizontalLayout
		{
			private void  InitBlock(MapBoard enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				return HexSize; return HexSize;
			}
			private MapBoard enclosingInstance;
			public MapBoard Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			internal GridOffsetRowLayout(MapBoard enclosingInstance, int size, int columns, int rows):base(size, columns, rows)
			{
				InitBlock(enclosingInstance);
			}
			
			internal Override Dimension;
			internal getBoardSize(MapBoard enclosingInstance)
			{
				InitBlock(enclosingInstance);
				System.Drawing.Size d = new System.Drawing.Size(0, 0);
				d.Height = DeltaY * nRows + 1;
				d.Width = DeltaX * nColumns + HexSize / 2 + 1;
				return d;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override 
			int getDeltaX()
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override 
			int getDeltaY()
			
			new internal Override Point;
			internal getOrigin(MapBoard enclosingInstance)
			{
				InitBlock(enclosingInstance);
				return new System.Drawing.Point(HexSize * 7 / 12, HexSize / 2);
			}
			
			internal Override AbstractConfigurable;
			internal getGeometricGrid(MapBoard enclosingInstance)
			{
				InitBlock(enclosingInstance);
				HexGrid mg = new HexGrid();
				
				mg.Sideways = true;
				
				System.Drawing.Point tempAux = getOrigin();
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				mg.setOrigin(ref tempAux);
				mg.Dx = DeltaY;
				mg.Dy = DeltaX;
				
				return mg;
			}
		}
		
		/// <summary> A layout in which every second row is offset by one-half hex or square.</summary>
		protected internal abstract class HorizontalLayout:Layout
		{
			private void  InitBlock()
			{
				return 6;
			}
			
			internal HorizontalLayout(int size, int columns, int rows):base(size, columns, rows)
			{
				InitBlock();
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override 
			int getNFaces()
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			internal override void  setGridNumberingOffsets(RegularGridNumbering numbering, MapSheet sheet)
			{
				System.Drawing.Point position = coordinatesToPosition(sheet.Field.X, sheet.Field.Y, true);
				position.Offset(DeltaX / 2, DeltaY / 2);
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				int rowOffset = numbering.getColumn(ref position);
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				int colOffset = numbering.getRow(ref position);
				
				rowOffset = - rowOffset + sheet.TopLeftRow;
				colOffset = - colOffset + sheet.TopLeftCol;
				
				numbering.setAttribute(RegularGridNumbering.H_OFF, rowOffset);
				numbering.setAttribute(RegularGridNumbering.V_OFF, colOffset);
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			internal override void  initGridNumbering(RegularGridNumbering numbering, MapSheet sheet)
			{
				base.initGridNumbering(numbering, sheet);
				bool stagger = false;
				if (sheet.firstHexRight() && (sheet.Field.Y & 1) == 1)
					stagger = true;
				else if (sheet.firstHexLeft() && sheet.Field.Y % 2 == 0)
					stagger = true;
				numbering.setAttribute(HexGridNumbering.STAGGER, stagger);
				numbering.setAttribute(RegularGridNumbering.FIRST, sheet.rowsAndCols()?"H":"V");
				numbering.setAttribute(RegularGridNumbering.H_TYPE, sheet.numericRows()?"N":"A");
				numbering.setAttribute(RegularGridNumbering.V_TYPE, sheet.numericCols()?"N":"A");
				numbering.setAttribute(RegularGridNumbering.H_LEADING, sheet.NRowChars - 1);
				numbering.setAttribute(RegularGridNumbering.V_LEADING, sheet.NColChars - 1);
			}
			
			internal Override HexGridNumbering;
			internal getGridNumbering()
			{
				InitBlock();
				return new HexGridNumbering();
			}
			
			internal Override Point;
			internal coordinatesToPosition(int x, int y, bool nullIfOffBoard)
			{
				InitBlock();
				if (!nullIfOffBoard || isOnMapBoard(x, y))
				{
					int xx = DeltaX * x + (y % 2) * DeltaX / 2;
					int yy = DeltaY * y;
					return new System.Drawing.Point(xx, yy);
				}
				else
				{
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				}
			}
			
			internal Override Point;
			internal getNorthEast(int index)
			{
				InitBlock();
				int row = getRow(index);
				int col = getCol(index) + System.Math.Abs(row) % 2;
				--row;
				return coordinatesToPosition(col, row, false);
			}
			
			internal Override Point;
			internal getNorthWest(int index)
			{
				InitBlock();
				int row = getRow(index) - 1;
				int col = getCol(index) - System.Math.Abs(row) % 2;
				return coordinatesToPosition(col, row, false);
			}
			
			internal Override Rectangle;
			internal getRectangle(MapSheet map)
			{
				InitBlock();
				System.Drawing.Rectangle r = map.Field;
				
				System.Drawing.Point upperLeft = coordinatesToPosition(r.X, r.Y, false);
				System.Drawing.Point lowerRight = coordinatesToPosition(r.X + r.Width - 1, r.Y + r.Height - 1, false);
				
				// adjust for staggering of hexes
				if (map.firstHexLeft())
				// next one down is to the left
					upperLeft.X -= HexSize / 2;
				
				// adjust x of bottom right-hand corner
				if (r.Y % 2 == (r.Y + r.Height - 1) % 2)
				{
					// both even or both odd
					if (map.firstHexRight())
						lowerRight.X += HexSize / 2;
					// check to see if lower right-hand corner is on the wrong
					// square
				}
				else if ((r.Y & 1) == 1)
				{
					// top is odd and bottom is even
					if (map.firstHexLeft())
						lowerRight.X += HexSize / 2;
					else
						lowerRight.X += HexSize;
				}
				else if (map.firstHexLeft() && r.Y % 2 == 0)
				// top is even and bottom is odd
					lowerRight.X -= HexSize / 2;
				
				// get lower right corner of lower right hex
				lowerRight.X += HexSize - 1;
				lowerRight.Y += HexSize - 1;
				
				// adjust so that we don't overlap the centres of hexes that don't
				// belong to this sheet
				upperLeft.X += HexSize / 5;
				lowerRight.X -= HexSize / 5;
				
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				constrainRectangle(ref upperLeft, ref lowerRight);
				
				return new System.Drawing.Rectangle(upperLeft.X, upperLeft.Y, lowerRight.X - upperLeft.X + 1, lowerRight.Y - upperLeft.Y + 1);
			}
			
			internal Override Point;
			internal getSouthEast(int index)
			{
				InitBlock();
				int row = getRow(index);
				int col = getCol(index) + System.Math.Abs(row) % 2;
				++row;
				return coordinatesToPosition(col, row, false);
			}
			
			internal Override Point;
			internal getSouthWest(int index)
			{
				InitBlock();
				int row = getRow(index) + 1;
				int col = getCol(index) - System.Math.Abs(row) % 2;
				return coordinatesToPosition(col, row, false);
			}
		}
		
		/// <summary> A drawable line such as a river, border, or road.</summary>
		protected internal abstract class Line:MapDrawable
		{
			
			// index of line definition. don't know the actual line definitions until later
			//UPGRADE_NOTE: Final was removed from the declaration of 'line '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private int line;
			
			internal Line(int index, int line):base(index)
			{
				this.line = line;
				if (hexes == null)
					hexes = new Hex[getNColumns() * getNRows()];
				if (hexes[index] == null)
					hexes[index] = new Hex();
				getLineList(hexes[index]).add(this);
			}
			
			/// <returns> The <code>LineDefinition</code> for this line.
			/// </returns>
			internal virtual LineDefinition getLine()
			{
				return getLineDefinition(line);
			}
			
			/// <returns> The list of lines by hex.
			/// </returns>
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			abstract ArrayList < Line > getLineList(Hex h);
			
			// I no longer remember how this works, but I do remember it took a long time to figure out.
			internal virtual int compare(LineDefinition o1, LineDefinition o2)
			{
				if (o1 == null && o2 == null)
					return 0;
				else if (o1 == null)
					return 1;
				else if (o2 == null)
					return - 1;
				// go through all the hexes
				// and determine file order for lines
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Hex h: hexes)
				{
					if (h == null)
						continue;
					bool index1 = false;
					bool index2 = false;
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(Line hl: getLineList(h))
					{
						if (hl.getLine() == o1)
						{
							if (index2)
								return 1;
							index1 = true;
						}
						else if (hl.getLine() == o2)
						{
							if (index1)
								return - 1;
							index2 = true;
						}
					}
				}
				return 0;
			}
			
			internal virtual void  drawLines(System.Drawing.Graphics g, int cap)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final ArrayList < LineDefinition > lds = 
				new ArrayList < LineDefinition >(Arrays.asList(lineDefinitions));
				
				// find the next line in priority
				while (lds.size() > 0)
				{
					LineDefinition lowest = null;
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(LineDefinition ld: lds)
					{
						if (ld == null)
							continue;
						else if (lowest == null || compare(ld, lowest) < 0)
							lowest = ld;
					}
					if (lowest == null)
						break;
					else
					{
						lowest.draw(g, cap);
						lowest.clearPoints();
						lds.remove(lowest);
					}
				}
			}
		}
		
		/// <summary> Line styles for hex sides and hex lines.</summary>
		protected internal class LineDefinition
		{
			virtual internal System.Drawing.Color Color
			{
				get
				{
					return color;
				}
				
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'color '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private System.Drawing.Color color;
			
			private int hexLineDrawPriority;
			
			private int hexSideDrawPriority;
			
			// using floats because we really want to aim for the centre pixel, not necessarily
			// the space between pixels--only important for aliasing effects.
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private ArrayList < ArrayList < Point2D.Float >> points = new ArrayList < ArrayList < Point2D.Float >>();
			
			// line width
			//UPGRADE_NOTE: Final was removed from the declaration of 'size '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private int size;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'style '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private LineStyle style;
			
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			internal LineDefinition(ref System.Drawing.Color color, int size, MapBoard.LineStyle style)
			{
				this.color = color;
				this.size = size;
				this.style = style;
			}
			
			private void  setHexLineDrawPriority(int priority)
			{
				// only change the priority if it hasn't already been set.
				if (hexLineDrawPriority == 0)
					hexLineDrawPriority = priority;
			}
			
			private void  setHexSideDrawPriority(int priority)
			{
				if (hexSideDrawPriority == 0)
					hexSideDrawPriority = priority;
			}
			
			internal virtual System.Drawing.Pen getStroke(int cap)
			{
				if (size <= 0 || style == null)
					return null;
				return style.getStroke(size, cap);
			}
			
			internal virtual void  addLine(float x1, float y1, float x2, float y2)
			{
				System.Drawing.PointF tempAux = new System.Drawing.PointF(x1, y1);
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				System.Drawing.PointF tempAux2 = new System.Drawing.PointF(x2, y2);
				addLine(ref tempAux, ref tempAux2);
			}
			
			internal virtual void  addLine(int x1, int y1, float x2, float y2)
			{
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				System.Drawing.PointF tempAux = new System.Drawing.PointF((float) x1, (float) y1);
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				System.Drawing.PointF tempAux2 = new System.Drawing.PointF(x2, y2);
				addLine(ref tempAux, ref tempAux2);
			}
			
			internal virtual void  addLine(int x1, int y1, int x2, int y2)
			{
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				System.Drawing.PointF tempAux = new System.Drawing.PointF((float) x1, (float) y1);
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				System.Drawing.PointF tempAux2 = new System.Drawing.PointF((float) x2, (float) y2);
				addLine(ref tempAux, ref tempAux2);
			}
			
			/// <summary> Add a line to the line list for later processing. Attach it to already existing line if possible.
			/// Otherwise start a new one.
			/// </summary>
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			internal virtual void  addLine(ref System.Drawing.PointF a, ref System.Drawing.PointF b)
			{
				// find out if this line is attached to any other line in the list.
				// if not create a line.
				for (int i = 0; i < points.size(); ++i)
				{
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
					if (a.Equals(lineA.get_Renamed(0)))
					{
						// a at the start of lineA
						// repeated segment?
						if (b.Equals(lineA.get_Renamed(1)))
							return ;
						// find out if this segment joins two lines already in
						// existance
						for (int j = 0; j < points.size(); ++j)
						{
							if (i == j)
								continue;
							//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
							if (b.Equals(lineB.get_Renamed(0)))
							{
								// point A at start of lineA and point B at start of lineB
								if (lineA.size() < lineB.size())
								{
									// insert A before B
									for (int k = 0; k < lineA.size(); ++k)
										lineB.add(0, lineA.get_Renamed(k));
									points.remove(i);
								}
								else
								{
									// insert B before A
									for (int k = 0; k < lineB.size(); ++k)
										lineA.add(0, lineB.get_Renamed(k));
									points.remove(j);
								}
								return ;
							}
							else if (b.Equals(lineB.get_Renamed(lineB.size() - 1)))
							{
								// point A at start of lineA and point B at end of lineB
								lineB.addAll(lineA);
								points.remove(i);
								return ;
							}
						}
						// point A at start of lineA and point B is open
						lineA.add(0, b);
						return ;
					}
					else if (a.Equals(lineA.get_Renamed(lineA.size() - 1)))
					{
						// Point A is at end of line A
						if (b.Equals(lineA.get_Renamed(lineA.size() - 2)))
						// repeated segment?
							return ;
						for (int j = 0; j < points.size(); ++j)
						{
							if (i == j)
							// skip closed loops
								continue;
							//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
							if (b.Equals(lineB.get_Renamed(0)))
							{
								// point A at end of line A and point B at start of lineB
								lineA.addAll(lineB);
								points.remove(j);
								return ;
							}
							else if (b.Equals(lineB.get_Renamed(lineB.size() - 1)))
							{
								// point A at end of lineA and point B at end of lineB
								if (lineA.size() < lineB.size())
								{
									// add line A to B
									for (int k = lineA.size() - 1; k >= 0; --k)
										lineB.add(lineA.get_Renamed(k));
									points.remove(i);
								}
								else
								{
									// add line B to A
									for (int k = lineB.size() - 1; k >= 0; --k)
										lineA.add(lineB.get_Renamed(k));
									points.remove(j);
								}
								return ;
							}
						}
						// point A at the end of lineA and point B is open
						lineA.add(b);
						return ;
					}
					// find out if the segment already exists
					for (int j = 1; j < lineA.size() - 1; ++j)
						if (a.Equals(lineA.get_Renamed(j)) && (b.Equals(lineA.get_Renamed(j - 1)) || b.Equals(lineA.get_Renamed(j + 1))))
							return ;
				}
				
				// point A is open (not attached)
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(ArrayList < Point2D.Float > line: points)
				{
					if (b.Equals(line.get_Renamed(0)))
					{
						// B at the start of the line
						// repeated segment?
						if (a.Equals(line.get_Renamed(1)))
							return ;
						line.add(0, a);
						return ;
					}
					else if (b.Equals(line.get_Renamed(line.size() - 1)))
					{
						// B at the end of the line
						if (a.Equals(line.get_Renamed(line.size() - 2)))
							return ;
						line.add(a);
						return ;
					}
				}
				
				// both A and B are open
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				ArrayList < Point2D.Float > newLine = new ArrayList < Point2D.Float >(2);
				newLine.add(a);
				newLine.add(b);
				points.add(newLine);
			}
			
			/// <summary> start fresh.</summary>
			internal virtual void  clearPoints()
			{
				points.clear();
			}
			
			internal virtual void  draw(System.Drawing.Graphics g, int cap)
			{
				//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setRenderingHint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetRenderingHint_javaawtRenderingHintsKey_javalangObject'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_ANTIALIASING' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_ANTIALIASING_f'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.VALUE_ANTIALIAS_ON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsVALUE_ANTIALIAS_ON_f'"
				g.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
				System.Drawing.Pen stroke = getStroke(cap);
				if (stroke == null)
					return ;
				//UPGRADE_TODO: Method 'java.awt.Graphics2D.setStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DsetStroke_javaawtStroke'"
				SupportClass.GraphicsManager.manager.SetPen(g, stroke);
				SupportClass.GraphicsManager.manager.SetColor(g, Color);
				System.Drawing.Drawing2D.GraphicsPath temp_GraphicsPath;
				temp_GraphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
				temp_GraphicsPath.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
				System.Drawing.Drawing2D.GraphicsPath gp = temp_GraphicsPath;
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(ArrayList < Point2D.Float > line: points)
				{
					gp.moveTo(line.get_Renamed(0).x, line.get_Renamed(0).y);
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(Point2D.Float p: line)
					{
						if (!p.equals(line.get_Renamed(0)))
							gp.lineTo(p.x, p.y);
						else if (p != line.get_Renamed(0))
							gp.CloseFigure();
					}
				}
				//UPGRADE_TODO: Method 'java.awt.Graphics2D.draw' was converted to 'System.Drawing.Graphics.DrawPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Ddraw_javaawtShape'"
				g.DrawPath(SupportClass.GraphicsManager.manager.GetPen(g), gp);
				//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setRenderingHint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetRenderingHint_javaawtRenderingHintsKey_javalangObject'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_ANTIALIASING' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_ANTIALIASING_f'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.VALUE_ANTIALIAS_OFF' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsVALUE_ANTIALIAS_OFF_f'"
				g.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_OFF);
			}
			
			internal virtual int getHexLineDrawPriority()
			{
				return hexLineDrawPriority;
			}
			
			internal virtual int getHexSideDrawPriority()
			{
				return hexSideDrawPriority;
			}
		}
		
		/// <summary> line patter such as dashed or dotted or solid</summary>
		protected internal enum_Renamed LineStyle_Renamed_Field;
		
		private float[] dash;
		
		internal LineStyle(float[] dash)
		{
			InitBlock();
			this.dash = dash;
		}
		
		internal virtual System.Drawing.Pen getStroke(int size, int cap)
		{
			if (dash == null)
			// nice effect if it's a solid line
			{
				//UPGRADE_TODO: Constructor 'java.awt.BasicStroke.BasicStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtBasicStrokeBasicStroke_float_int_int'"
				return SupportClass.StrokeConsSupport.CreatePenInstance(size, cap, (int) System.Drawing.Drawing2D.LineJoin.Round);
			}
			else
			{
				//UPGRADE_TODO: Constructor 'java.awt.BasicStroke.BasicStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtBasicStrokeBasicStroke_float_int_int_float_float[]_float'"
				return SupportClass.StrokeConsSupport.CreatePenInstance(size, (int) System.Drawing.Drawing2D.LineCap.Flat, (int) System.Drawing.Drawing2D.LineJoin.Round, 0.0f, dash, 0.0f);
			}
		}
	}
	
	/// <summary> Anything that can be drawn on the map and is associated with a particular hex.</summary>
	protected internal abstract class MapDrawable
	{
		virtual internal int HexIndex
		{
			get
			{
				return hexIndex;
			}
			
		}
		/// <returns> Enclosing rectangle for hex or square.
		/// </returns>
		virtual internal System.Drawing.Rectangle Rectangle
		{
			get
			{
				System.Drawing.Point temp_Point;
				temp_Point = getPosition();
				System.Drawing.Rectangle r = new System.Drawing.Rectangle(temp_Point.X, temp_Point.Y, 0, 0);
				int width = getLayout().getHexSize();
				r.Width = width;
				r.Height = width;
				return r;
			}
			
		}
		
		// hex index in row-major order: determines location on map
		//UPGRADE_NOTE: Final was removed from the declaration of 'hexIndex '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int hexIndex;
		
		internal MapDrawable(int index)
		{
			this.hexIndex = index;
		}
		
		/// <summary> Draw the element to the graphics context. Return <code>true</code> if an element was actually drawn.</summary>
		internal abstract bool draw(System.Drawing.Graphics g);
		
		/// <returns> Upper left-hand corner of hex or square.
		/// </returns>
		internal virtual System.Drawing.Point getPosition()
		{
			return indexToPosition(hexIndex);
		}
	}
	
	/// <summary> Defines the numbering system for an ADC2 mapboard.</summary>
	protected internal class MapSheet
	{
		/// <returns> A rectangle giving the bounds of the hex coordinates.
		/// </returns>
		virtual internal System.Drawing.Rectangle Field
		{
			get
			{
				return field;
			}
			
		}
		/// <returns> The sheet name.
		/// </returns>
		virtual internal System.String Name
		{
			get
			{
				return name;
			}
			
		}
		/// <returns> Number of characters in the column label.
		/// </returns>
		virtual internal int NColChars
		{
			get
			{
				return nColChars;
			}
			
		}
		/// <returns> Number of characters in the row label.
		/// </returns>
		virtual internal int NRowChars
		{
			get
			{
				return nRowChars;
			}
			
		}
		/// <summary> Used by the grid numbering system in VassalSharp.</summary>
		virtual internal System.String RectangleAsString
		{
			get
			{
				System.Drawing.Rectangle r = getLayout().getRectangle(this);
				if (r.IsEmpty)
					return null;
				return r.X + "," + r.Y + ";" + (r.X + r.Width - 1) + "," + r.Y + ";" + (r.X + r.Width - 1) + "," + (r.Y + r.Height - 1) + ";" + r.X + "," + (r.Y + r.Height - 1);
			}
			
		}
		virtual internal RegularGridNumbering GridNumbering
		{
			get
			{
				// numbering system
				RegularGridNumbering gn = getLayout().GridNumbering;
				getLayout().initGridNumbering(gn, this);
				return gn;
			}
			
		}
		/// <summary> Creates a VASSAL <code>Zone</code> if not already created and initializes settings.</summary>
		virtual internal Zone Zone
		{
			get
			{
				if (zone == null)
				{
					zone = new Zone();
					zone.setConfigureName(Name);
					
					System.String rect = RectangleAsString;
					if (rect == null)
						return null;
					zone.setAttribute(Zone.PATH, rect);
					zone.setAttribute(Zone.LOCATION_FORMAT, "$name$ $gridLocation$");
					AbstractConfigurable mg = getLayout().getGeometricGrid();
					
					// add numbering system to grid
					RegularGridNumbering gn = GridNumbering;
					insertComponent(gn, mg);
					
					// add grid to zone
					insertComponent(mg, zone);
					
					getLayout().setGridNumberingOffsets(gn, this);
				}
				
				return zone;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <returns> Index of the top left column on the map sheet.
		/// </returns>
		/// <summary> Sets the top left column index of the map sheet.</summary>
		virtual internal int TopLeftCol
		{
			get
			{
				return topLeftCol;
			}
			
			set
			{
				this.topLeftCol = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <returns> Index of the top left row of the map sheet.
		/// </returns>
		/// <summary> Sets the top left row index of the map sheet.</summary>
		virtual internal int TopLeftRow
		{
			get
			{
				return topLeftRow;
			}
			
			set
			{
				this.topLeftRow = value;
			}
			
		}
		
		private int topLeftCol;
		
		private int topLeftRow;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'field '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Drawing.Rectangle field;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String name;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'nColChars '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int nColChars;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'nRowChars '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int nRowChars;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'style '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int style;
		
		private Zone zone;
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		internal MapSheet(System.String name, ref System.Drawing.Rectangle playingFieldPosition, int style, int nColChars, int nRowChars)
		{
			this.name = name;
			this.field = playingFieldPosition;
			this.style = style;
			this.nColChars = nColChars;
			this.nRowChars = nRowChars;
		}
		
		/// <returns> <code>true</code> if column labels are alphabetic.
		/// </returns>
		internal virtual bool alphaCols()
		{
			return !numericCols();
		}
		
		/// <returns> <code>true</code> if row labels are alphabetic.
		/// </returns>
		internal virtual bool alphaRows()
		{
			return !numericRows();
		}
		
		/// <returns> <code>true</code> if columns are first in coordinate label.
		/// </returns>
		internal virtual bool colsAndRows()
		{
			return (style & 0x2) > 0;
		}
		
		/// <returns> <code>true</code> if column labels increase going left.
		/// </returns>
		internal virtual bool colsIncreaseLeft()
		{
			return !colsIncreaseRight();
		}
		
		/// <returns> <code>true</code> if column labels increase going right.
		/// </returns>
		internal virtual bool colsIncreaseRight()
		{
			return (style & 0x10) > 0;
		}
		
		/// <returns> <code>true</code> if the row label of odd-numbered columns is shifted down.
		/// </returns>
		internal virtual bool firstHexDown()
		{
			return (style & 0x40) > 0 && getLayout() is VerticalLayout;
		}
		
		/// <returns> <code>true</code> if the row label of odd-numbered rows is shifted left.
		/// </returns>
		internal virtual bool firstHexLeft()
		{
			return (style & 0x40) > 0 && getLayout() is HorizontalLayout;
		}
		
		/// <returns> <code>true</code> if the row label of odd-numbered rows is shifted right.
		/// </returns>
		internal virtual bool firstHexRight()
		{
			return (style & 0x40) == 0 && getLayout() is HorizontalLayout;
		}
		
		/// <returns> <code>true</code> if the row label of odd-numbered columns is shifted down.
		/// </returns>
		internal virtual bool firstHexUp()
		{
			return (style & 0x40) == 0 && getLayout() is VerticalLayout;
		}
		
		/// <returns> <code>true</code> if column labels are numeric.
		/// </returns>
		internal virtual bool numericCols()
		{
			return (style & 0x4) > 0;
		}
		
		/// <returns> <code>true</code> if row labels are numeric.
		/// </returns>
		internal virtual bool numericRows()
		{
			return (style & 0x8) > 0;
		}
		
		/// <summary> Rows before columns?</summary>
		internal virtual bool rowsAndCols()
		{
			return !colsAndRows();
		}
		
		/// <returns> <code>true</code> if row labels increase downward.
		/// </returns>
		internal virtual bool rowsIncreaseDown()
		{
			return (style & 0x20) > 0;
		}
		
		/// <returns> <code>true</code> if row labels increase upward.
		/// </returns>
		internal virtual bool rowsIncreaseUp()
		{
			return !rowsIncreaseDown();
		}
	}
	
	/// <summary> Place name element which includes not only the name itself, but the font and style that it
	/// should be drawn with.
	/// </summary>
	protected internal class PlaceName:MapDrawable
	{
		private void  InitBlock()
		{
			if (Size != 0)
			{
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_TEXT_ANTIALIASING' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_TEXT_ANTIALIASING_f'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.VALUE_TEXT_ANTIALIAS_ON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsVALUE_TEXT_ANTIALIAS_ON_f'"
				g.setRenderingHint(RenderingHints.KEY_TEXT_ANTIALIASING, RenderingHints.VALUE_TEXT_ANTIALIAS_ON);
				g.Font = Font;
				g.setColor(color);
				System.Drawing.Point p = getPosition(g);
				g.drawString(text, p.X, p.Y);
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_TEXT_ANTIALIASING' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_TEXT_ANTIALIASING_f'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.VALUE_TEXT_ANTIALIAS_OFF' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsVALUE_TEXT_ANTIALIAS_OFF_f'"
				g.setRenderingHint(RenderingHints.KEY_TEXT_ANTIALIASING, RenderingHints.VALUE_TEXT_ANTIALIAS_OFF);
				return true;
			}
			else
			{
				return false;
			}
		}
		virtual internal System.Drawing.Font Font
		{
			get
			{
				int size = Size;
				return size == 0?null:getDefaultFont(Size, font);
			}
			
		}
		virtual internal int Size
		{
			// scale the size more appropriately--for some reason ADC2 font sizes
			// don't correspond to anything else.
			
			get
			{
				return size <= 5?0:(size + 1) * 4 / 3 - 1;
			}
			
		}
		virtual internal System.String Text
		{
			get
			{
				return text;
			}
			
		}
		
		// text colour
		//UPGRADE_NOTE: Final was removed from the declaration of 'color '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Drawing.Color color;
		
		// bit flags
		//UPGRADE_NOTE: Final was removed from the declaration of 'font '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int font;
		
		// position relative to the hex. not really orientation. e.g., can't
		// have vertical text.
		//UPGRADE_NOTE: Final was removed from the declaration of 'orientation '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private PlaceNameOrientation orientation;
		
		// font size
		//UPGRADE_NOTE: Final was removed from the declaration of 'size '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int size;
		
		// the actual name
		//UPGRADE_NOTE: Final was removed from the declaration of 'text '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String text;
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		internal PlaceName(int index, System.String text, ref System.Drawing.Color color, PlaceNameOrientation orientation, int size, int font):base(index)
		{
			InitBlock();
			this.text = text;
			assert(!color.IsEmpty);
			this.color = color;
			assert(orientation != null);
			this.orientation = orientation;
			//      assert (size > 0);
			this.size = size;
			font &= 0x7f;
			int fontIndex = font & 0xf;
			if (fontIndex < 1 || fontIndex > 9)
			{
				fontIndex = 9;
				font = font & 0xf0 | fontIndex;
			}
			this.font = font;
		}
		
		/// <summary> Get the position based on the hex index, font and orientation.</summary>
		internal virtual System.Drawing.Point getPosition(System.Drawing.Graphics g)
		{
			System.Drawing.Point p = getPosition();
			if (Size == 0)
				return p;
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Graphics.getFont' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			assert(SupportClass.GraphicsManager.manager.GetFont(g) == Font);
			System.Drawing.Font fm = SupportClass.GraphicsManager.manager.GetFont(g);
			int size = getLayout().getHexSize();
			
			switch (orientation)
			{
				
				case LOWER_CENTER: 
				case UPPER_CENTER: 
				case LOWER_RIGHT: 
				case UPPER_RIGHT: 
				case UPPER_LEFT: 
				case LOWER_LEFT: 
				case HEX_CENTER: 
					p.X += size / 2; // middle of the hex.
					break;
				
				case CENTER_RIGHT: 
					p.X += size; // right of hex
					break;
				
				case CENTER_LEFT: 
					break;
				}
			switch (orientation)
			{
				
				case LOWER_CENTER: 
				case UPPER_CENTER: 
				case HEX_CENTER: 
					// text centered
					//UPGRADE_ISSUE: Method 'java.awt.FontMetrics.charsWidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtFontMetricscharsWidth_char[]_int_int'"
					p.X -= fm.charsWidth(text.ToCharArray(), 0, text.Length) / 2;
					break;
				
				case UPPER_LEFT: 
				case LOWER_LEFT: 
				case CENTER_LEFT: 
					// right justified
					//UPGRADE_ISSUE: Method 'java.awt.FontMetrics.charsWidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtFontMetricscharsWidth_char[]_int_int'"
					p.X -= fm.charsWidth(text.ToCharArray(), 0, text.Length);
					break;
				
				case LOWER_RIGHT: 
				case UPPER_RIGHT: 
				case CENTER_RIGHT: 
					break;
				}
			switch (orientation)
			{
				
				case LOWER_CENTER: 
				case LOWER_RIGHT: 
				case LOWER_LEFT: 
					//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.FontMetrics.getAscent' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					p.Y += size + SupportClass.GetAscent(fm);
					break;
				
				case UPPER_CENTER: 
				case UPPER_RIGHT: 
				case UPPER_LEFT: 
					//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.FontMetrics.getDescent' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					p.Y -= SupportClass.GetDescent(fm);
					break;
				
				case CENTER_LEFT: 
				case CENTER_RIGHT: 
				case HEX_CENTER: 
					//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.FontMetrics.getHeight' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.FontMetrics.getDescent' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					p.Y += size / 2 + (int) fm.GetHeight() / 2 - SupportClass.GetDescent(fm);
					break;
				}
			return p;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override 
		boolean draw(Graphics2D g)
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected enum PlaceNameOrientation
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		CENTER_LEFT, CENTER_RIGHT, HEX_CENTER, LOWER_CENTER, LOWER_LEFT, LOWER_RIGHT, UPPER_CENTER, UPPER_LEFT, UPPER_RIGHT;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> A layout consisting of squares in which every second column is shifted
	/// downward by one half width.  This is done to approximate hexagons as each
	/// square as six neighbours.
	/// 
	/// </summary>
	/// <author>  Michael Kiefte
	/// 
	/// </author>
	protected internal class GridOffsetColumnLayout:VerticalLayout
	{
		private void  InitBlock()
		{
			return HexSize; return HexSize;
		}
		
		internal GridOffsetColumnLayout(int size, int columns, int rows):base(size, columns, rows)
		{
			InitBlock();
		}
		
		internal Override Dimension;
		internal getBoardSize()
		{
			InitBlock();
			System.Drawing.Size d = new System.Drawing.Size(0, 0);
			d.Width = DeltaX * nColumns + 1;
			d.Height = DeltaY * nRows + HexSize / 2 + 1;
			return d;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override 
		int getDeltaX()
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override 
		int getDeltaY()
		
		new internal Override Point;
		internal getOrigin()
		{
			InitBlock();
			return new System.Drawing.Point(HexSize * 7 / 12, HexSize / 2);
		}
		
		internal Override AbstractConfigurable;
		internal getGeometricGrid()
		{
			InitBlock();
			HexGrid mg = new HexGrid();
			
			System.Drawing.Point tempAux = getOrigin();
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			mg.setOrigin(ref tempAux);
			mg.Dx = DeltaX;
			mg.Dy = DeltaY;
			
			return mg;
		}
	}
	
	/// <summary> Hexes in columns.</summary>
	protected internal class VerticalHexLayout:VerticalLayout
	{
		private void  InitBlock()
		{
			return HexSize * 4 / 5 - (isPreV208Layout()?1:0); return HexSize - (isPreV208Layout()?2:1);
		}
		
		internal VerticalHexLayout(int size, int columns, int rows):base(size, columns, rows)
		{
			InitBlock();
		}
		
		internal Override Dimension;
		internal getBoardSize()
		{
			InitBlock();
			System.Drawing.Size d = new System.Drawing.Size(0, 0);
			d.Width = DeltaX * nColumns + HexSize / 5 + 1;
			d.Height = DeltaY * nRows + HexSize / 2 + 1;
			return d;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override 
		int getDeltaX()
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override 
		int getDeltaY()
		
		new internal Override Point;
		internal getOrigin()
		{
			InitBlock();
			return new System.Drawing.Point(HexSize / 2, HexSize / 2 - (isPreV208Layout()?1:0));
		}
		
		internal Override HexGrid;
		internal getGeometricGrid()
		{
			InitBlock();
			HexGrid mg = new HexGrid();
			
			System.Drawing.Point tempAux = getOrigin();
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			mg.setOrigin(ref tempAux);
			mg.Dx = DeltaX;
			mg.Dy = DeltaY;
			
			return mg;
		}
	}
	
	/// <summary> A layout in which every second column is offset--either hexes or squares.</summary>
	protected internal abstract class VerticalLayout:Layout
	{
		private void  InitBlock()
		{
			return 6;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override 
		int getNFaces()
		
		internal VerticalLayout(int size, int columns, int rows):base(size, columns, rows)
		{
			InitBlock();
		}
		
		internal Override HexGridNumbering;
		internal getGridNumbering()
		{
			InitBlock();
			return new HexGridNumbering();
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		internal override void  initGridNumbering(RegularGridNumbering numbering, MapSheet sheet)
		{
			bool stagger = false;
			if (sheet.firstHexDown() && (sheet.Field.X & 1) == 1)
				stagger = true;
			else if (sheet.firstHexUp() && sheet.Field.X % 2 == 0)
				stagger = true;
			numbering.setAttribute(HexGridNumbering.STAGGER, stagger);
			base.initGridNumbering(numbering, sheet);
		}
		
		internal Override Point;
		internal coordinatesToPosition(int x, int y, bool nullIfOffBoard)
		{
			InitBlock();
			if (!nullIfOffBoard || isOnMapBoard(x, y))
			{
				int xx = DeltaX * x;
				int yy = DeltaY * y + x % 2 * DeltaY / 2;
				return new System.Drawing.Point(xx, yy);
			}
			else
			{
				//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			}
		}
		
		internal Override Point;
		internal getNorthEast(int index)
		{
			InitBlock();
			int col = getCol(index) + 1;
			int row = getRow(index) - System.Math.Abs(col) % 2;
			return coordinatesToPosition(col, row, false);
		}
		
		internal Override Point;
		internal getNorthWest(int index)
		{
			InitBlock();
			int col = getCol(index) - 1;
			int row = getRow(index) - System.Math.Abs(col) % 2;
			return coordinatesToPosition(col, row, false);
		}
		
		internal Override Rectangle;
		internal getRectangle(MapSheet map)
		{
			InitBlock();
			System.Drawing.Rectangle r = map.Field;
			
			if (r.Width <= 0 || r.Height <= 0)
			{
				//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			}
			
			System.Drawing.Point upperLeft = coordinatesToPosition(r.X, r.Y, false);
			System.Drawing.Point lowerRight = coordinatesToPosition(r.X + r.Width - 1, r.Y + r.Height - 1, false);
			
			// adjust for staggering of hexes
			if (map.firstHexUp())
			// next one over is above
				upperLeft.Y -= HexSize / 2;
			
			// adjust y of bottom right-hand corner
			if (r.X % 2 == (r.X + r.Width - 1) % 2)
			{
				// both even or both odd
				if (map.firstHexDown())
					lowerRight.Y += HexSize / 2;
				// check to see if lower right-hand corner is on the wrong
				// square
			}
			else if ((r.X & 1) == 1)
			{
				// left is odd and right is even
				if (map.firstHexDown())
					lowerRight.Y += HexSize;
				else
					lowerRight.Y += HexSize / 2;
			}
			else if (map.firstHexUp() && r.X % 2 == 0)
			{
				// left is even and right is odd
				lowerRight.Y -= HexSize / 2;
			}
			
			// get lower right corner of lower right hex
			lowerRight.X += HexSize - 1;
			lowerRight.Y += HexSize - 1;
			
			// adjust so that we don't overlap the centres of hexes that don't
			// belong to this sheet
			upperLeft.Y += HexSize / 5;
			lowerRight.Y -= HexSize / 5;
			
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			constrainRectangle(ref upperLeft, ref lowerRight);
			
			return new System.Drawing.Rectangle(upperLeft.X, upperLeft.Y, lowerRight.X - upperLeft.X + 1, lowerRight.Y - upperLeft.Y + 1);
		}
		
		internal Override Point;
		internal getSouthEast(int index)
		{
			InitBlock();
			int col = getCol(index);
			int row = getRow(index) + System.Math.Abs(col) % 2;
			++col;
			return coordinatesToPosition(col, row, false);
		}
		
		internal Override Point;
		internal getSouthWest(int index)
		{
			InitBlock();
			int col = getCol(index);
			int row = getRow(index) + System.Math.Abs(col) % 2;
			--col;
			return coordinatesToPosition(col, row, false);
		}
	}
	
	/// <summary> How the hexes or squares are organized on the map board.</summary>
	protected internal abstract class Layout
	{
		/// <returns> number of flat sides. <i>e.g.</i>, four for squares, six for hexes.
		/// </returns>
		internal abstract int NFaces{get;}
		/// <returns> the distance in pixels to the next square on the right.
		/// </returns>
		internal abstract int DeltaX{get;}
		/// <returns> the distance in pixels ot the next square below
		/// </returns>
		internal abstract int DeltaY{get;}
		/// <returns> the size of the hexes or squares in pixels.
		/// </returns>
		virtual internal int HexSize
		{
			get
			{
				return size;
			}
			
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'nColumns '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int nColumns;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'nRows '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int nRows;
		
		// Size of the hexes or squares.
		//UPGRADE_NOTE: Final was removed from the declaration of 'size '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int size;
		
		internal Layout(int size, int columns, int rows)
		{
			this.size = size;
			this.nColumns = columns;
			this.nRows = rows;
		}
		
		protected internal virtual int getRow(int index)
		{
			return index / nColumns;
		}
		
		protected internal virtual int getCol(int index)
		{
			return index % nColumns;
		}
		
		/// <summary> Move the upper left and lower-right points to just within the map board.</summary>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		internal virtual void  constrainRectangle(ref System.Drawing.Point upperLeft, ref System.Drawing.Point lowerRight)
		{
			if (upperLeft.X < 0)
				upperLeft.X = 0;
			if (upperLeft.Y < 0)
				upperLeft.Y = 0;
			System.Drawing.Size d = getBoardSize();
			
			if (lowerRight.X >= d.Width)
				lowerRight.X = d.Width - 1;
			if (lowerRight.Y >= d.Height)
				lowerRight.Y = d.Height - 1;
		}
		
		/// <summary> Set attributes of the <code>GridNumbering</code> object based on map board parameters.</summary>
		internal virtual void  initGridNumbering(RegularGridNumbering numbering, MapSheet sheet)
		{
			numbering.setAttribute(RegularGridNumbering.FIRST, sheet.colsAndRows()?"H":"V");
			numbering.setAttribute(RegularGridNumbering.H_TYPE, sheet.numericCols()?"N":"A");
			numbering.setAttribute(RegularGridNumbering.H_LEADING, sheet.NColChars - 1);
			numbering.setAttribute(RegularGridNumbering.H_DESCEND, sheet.colsIncreaseLeft());
			numbering.setAttribute(RegularGridNumbering.H_DESCEND, sheet.colsIncreaseLeft());
			numbering.setAttribute(RegularGridNumbering.V_TYPE, sheet.numericRows()?"N":"A");
			numbering.setAttribute(RegularGridNumbering.V_LEADING, sheet.NRowChars - 1);
			numbering.setAttribute(RegularGridNumbering.V_DESCEND, sheet.rowsIncreaseUp());
		}
		
		/// <summary> Set the offset in the grid numbering system according to the specified map sheet.</summary>
		internal virtual void  setGridNumberingOffsets(RegularGridNumbering numbering, MapSheet sheet)
		{
			System.Drawing.Point position = coordinatesToPosition(sheet.Field.X, sheet.Field.Y, true);
			// shift to the middle of the hex
			position.Offset(DeltaX / 2, DeltaY / 2);
			// use the numbering system to find out where we are
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			int rowOffset = numbering.getRow(ref position);
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			int colOffset = numbering.getColumn(ref position);
			
			rowOffset = - rowOffset + sheet.TopLeftRow;
			colOffset = - colOffset + sheet.TopLeftCol;
			
			numbering.setAttribute(RegularGridNumbering.H_OFF, colOffset);
			numbering.setAttribute(RegularGridNumbering.V_OFF, rowOffset);
		}
		
		/// <returns> an uninitialized grid numbering system appropriate for this layout
		/// </returns>
		internal abstract RegularGridNumbering getGridNumbering();
		
		/// <summary> Returns a point corresponding the the upper-left corner of the square
		/// specified by the coordinates.
		/// 
		/// </summary>
		/// <param name="x">column
		/// </param>
		/// <param name="y">row
		/// </param>
		/// <param name="nullIfOffBoard">return null if not on the board. Otherwise the point
		/// may not be valid.
		/// </param>
		/// <returns> the point corresponding to the upper-left-hand corner of the square.
		/// </returns>
		internal abstract System.Drawing.Point coordinatesToPosition(int x, int y, bool nullIfOffBoard);
		
		/// <returns> board image size dimensions in pixels.
		/// </returns>
		internal abstract System.Drawing.Size getBoardSize();
		
		/// <summary> Returns the location of the hex or square to the East.
		/// 
		/// </summary>
		/// <param name="index">raw index (columns increasing fastest).
		/// </param>
		/// <returns> the position in pixels of the next hex or square to the East.
		/// </returns>
		internal virtual System.Drawing.Point getEast(int index)
		{
			int row = getRow(index);
			int col = getCol(index) + 1;
			return coordinatesToPosition(col, row, false);
		}
		
		/// <returns> an initialized VASSAL hex grid appropriate for the current layout
		/// </returns>
		internal abstract AbstractConfigurable getGeometricGrid();
		
		/// <summary> Returns the location of the hex or square to the North.
		/// 
		/// </summary>
		/// <param name="index">raw index (columns increasing fastest).
		/// </param>
		/// <returns> the position in pixels of the next hex or square to the North.
		/// </returns>
		internal virtual System.Drawing.Point getNorth(int index)
		{
			int row = getRow(index) - 1;
			int col = getCol(index);
			return coordinatesToPosition(col, row, false);
		}
		
		/// <summary> Returns the location of the hex or square to the NorthEast.
		/// 
		/// </summary>
		/// <param name="index">raw index (columns increasing fastest).
		/// </param>
		/// <returns> the position in pixels of the next hex or square to the NorthEast.
		/// </returns>
		internal virtual System.Drawing.Point getNorthEast(int index)
		{
			int row = getRow(index) - 1;
			int col = getCol(index) + 1;
			return coordinatesToPosition(col, row, false);
		}
		
		/// <summary> Returns the location of the hex or square to the NorthWest.
		/// 
		/// </summary>
		/// <param name="index">raw index (columns increasing fastest).
		/// </param>
		/// <returns> the position in pixels of the next hex or square to the NorthWest.
		/// </returns>
		internal virtual System.Drawing.Point getNorthWest(int index)
		{
			int row = getRow(index) - 1;
			int col = getCol(index) - 1;
			return coordinatesToPosition(col, row, false);
		}
		
		/// <returns> the centre in pixels of a square or hex relative to the top-left corner.
		/// </returns>
		internal abstract System.Drawing.Point getOrigin();
		
		/// <summary> Returns a rectangle in pixels that encloses the given <code>MapSheet</code>.
		/// Returns null if <code>MapSheet</code> has a negative size.
		/// </summary>
		internal abstract System.Drawing.Rectangle getRectangle(MapSheet map);
		
		/// <summary> Returns the location of the hex or square to the South.
		/// 
		/// </summary>
		/// <param name="index">raw index (columns increasing fastest).
		/// </param>
		/// <returns> the position in pixels of the next hex or square to the South.
		/// </returns>
		internal virtual System.Drawing.Point getSouth(int index)
		{
			int row = getRow(index) + 1;
			int col = getCol(index);
			return coordinatesToPosition(col, row, false);
		}
		
		/// <summary> Returns the location of the hex or square to the SouthEast.
		/// 
		/// </summary>
		/// <param name="index">raw index (columns increasing fastest).
		/// </param>
		/// <returns> the position in pixels of the next hex or square to the SouthEast.
		/// </returns>
		internal virtual System.Drawing.Point getSouthEast(int index)
		{
			int row = getRow(index) + 1;
			int col = getCol(index) + 1;
			return getLayout().coordinatesToPosition(col, row, false);
		}
		
		/// <summary> Returns the location of the hex or square to the SouthWest.
		/// 
		/// </summary>
		/// <param name="index">raw index (columns increasing fastest).
		/// </param>
		/// <returns> the position in pixels of the next hex or square to the SouthWest.
		/// </returns>
		internal virtual System.Drawing.Point getSouthWest(int index)
		{
			int row = getRow(index) + 1;
			int col = getCol(index) - 1;
			return coordinatesToPosition(col, row, false);
		}
		
		/// <summary> Returns the location of the hex or square to the West.
		/// 
		/// </summary>
		/// <param name="index">raw index (columns increasing fastest).
		/// </param>
		/// <returns> the position in pixels of the next hex or square to the West.
		/// </returns>
		internal virtual System.Drawing.Point getWest(int index)
		{
			int row = getRow(index);
			int col = getCol(index) - 1;
			return coordinatesToPosition(col, row, false);
		}
	}
	
	// Archive of fonts used for placenames. makes reuse possible and is
	// probably faster as most of the place names use only one of a very few fonts.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private final static HashMap < Integer, Font > defaultFonts = new HashMap < Integer, Font >();
	
	// which level to import
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private static final int zoomLevel = 2;
	
	// fonts available to ADC
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private static final String [] defaultFontNames =
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ Courier, Fixedsys, 
		MS Sans Serif, MS Serif, Impact, Brush Script MT, 
		System, Times New Roman, Arial
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private static final String PLACE_NAMES = Place Names;
	
	/// <summary> Get a font based on size and font index. If this font has not already been created, then it will be generated.
	/// Can be reused later if the same font was already created.
	/// 
	/// </summary>
	/// <param name="size">Font size.
	/// </param>
	/// <param name="font">Font index. See MapBoard.java for format.
	/// </param>
	
	/* Binary format for fonts:
	*
	*             00000000
	*                 ||||_ Font name index (between 1 and 9).
	*                |_____ Bold flag.
	*               |______ Italics flag.
	*              |_______ Underline flag.
	*/
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static Font getDefaultFont(int size, int font)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final Integer key = Integer.valueOf((size << 8) + font);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Font f = defaultFonts.get(key);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(f == null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		int fontIndex = font & f;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	assert(fontIndex >= 1 && fontIndex <= 9);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	boolean isBold =(font & 0010) > 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	boolean isItalic =(font & 0020) > 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	boolean isUnderline =(font & 0040) > 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	String fontName = defaultFontNames [fontIndex - 1];
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int fontStyle = Font.PLAIN;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(isItalic) 
	fontStyle |= Font.ITALIC;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(isBold) 
	fontStyle |= Font.BOLD;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	f = new Font(fontName, fontStyle, size);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(isUnderline)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// TODO: why doesn't underlining doesn't work? Why why why?
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Hashtable < TextAttribute, Object > hash = new Hashtable < TextAttribute, Object >();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	hash.put(TextAttribute.UNDERLINE, TextAttribute.UNDERLINE_ON);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	f = f.deriveFont(hash);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	defaultFonts.put(key, f);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return f;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	// tertiary symbols.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private final ArrayList < HexData > attributes = new ArrayList < HexData >();
	
	// name of the map board is derived from the file name
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private String baseName;
	
	// hex data organized by index
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private Hex [] hexes;
	
	// hexline data
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private final ArrayList < HexLine > hexLines = new ArrayList < HexLine >();
	
	// hexside data
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private final ArrayList < HexSide > hexSides = new ArrayList < HexSide >();
	
	// layout of the hexes or squares
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private Layout layout;
	
	// line definitions needed for hex sides and lines
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private LineDefinition [] lineDefinitions;
	
	// organizes all the drawable elements in order of drawing priority
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private ArrayList < MapLayer > mapElements = new ArrayList < MapLayer >();
	
	// grid numbering systems
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private final ArrayList < MapSheet > mapSheets = new ArrayList < MapSheet >();
	
	// labels; not necessary actual places corresponding to a hex, although
	// that's how it's described by ADC2
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private final ArrayList < PlaceName > placeNames = new ArrayList < PlaceName >();
	
	// optional place symbol in addition to primary and secondary mapboard
	// symbol
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private final ArrayList < HexData > placeSymbols = new ArrayList < HexData >();
	
	// primary mapboard symbols. Every hex must have one even if it's null.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private final ArrayList < HexData > primaryMapBoardSymbols = new ArrayList < HexData >();
	
	// and secondary mapboard symbols (typically a lot fewer)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private final ArrayList < HexData > secondaryMapBoardSymbols = new ArrayList < HexData >();
	
	// overlay symbol. there's only one, but we make it an ArrayList<> for consistency
	// with other drawing objects
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private final ArrayList < MapBoardOverlay > overlaySymbol = new ArrayList < MapBoardOverlay >();
	
	// symbol set associated with this map -- needed for mapboard symbols
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private SymbolSet set;
	
	// How many hex columns in the map.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private int columns;
	
	// How many hex rows in the map.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private int rows;
	
	// background map color when hexes are not drawn.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private Color tableColor;
	
	// version information needed for rendering hexes and determining hex dimensions
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private boolean isPreV208 = true;
	
	// map file path
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private String path;
	
	// The VASSAL BoardPicker object which is the tree parent of Board.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private BoardPicker boardPicker;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private byte [] drawingPriorities =
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	
	// initialize the drawing elements which must all be ArrayList<>'s.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public MapBoard()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		mapElements.add(new MapLayer(primaryMapBoardSymbols, Primary MapBoard Symbols, false));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	mapElements.add(new MapLayer(secondaryMapBoardSymbols, Secondary MapBoard Symbols, false));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	mapElements.add(new MapLayer(hexSides, Hex Sides, true));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	mapElements.add(new MapLayer(hexLines, Hex Lines, true));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	mapElements.add(new MapLayer(placeSymbols, Place Symbols, false));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	mapElements.add(new MapLayer(attributes, Attributes, false));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	mapElements.add(new MapLayer(overlaySymbol, Overlay Symbol, true));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	mapElements.add(new MapLayer(placeNames, Place Names, true));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Many maps are actually just scanned images held in a separate file. The images are often broken up into
	/// sections. An extra file describes how the images are pasted together. This function pieces the images
	/// together using the <code>Graphics2D</code> object <code>g</code>.
	/// </summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void readScannedMapLayoutFile(File f, Graphics2D g) throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		DataInputStream in = null;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	try
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		in = new DataInputStream(new BufferedInputStream(new FileInputStream(f)));
	// how many image sections
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int nSheets = ADC2Utils.readBase250Word(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < nSheets;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// image file name
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	String name = stripExtension(readWindowsFileName(in));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	File file = action.getCaseInsensitiveFile(new File(name + -L +(zoomLevel + 1) + .bmp), new File(path), true, null);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(file == null) 
	throw new FileNotFoundException(Unable to find map image.);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	BufferedImage img = ImageIO.read(file);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int x = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int y = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int j = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	j < 3;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ j)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		int tempx = ADC2Utils.readBase250Integer(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int tempy = ADC2Utils.readBase250Integer(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(j == zoomLevel)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		x = tempx;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	y = tempy;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	g.drawImage(img, null, x, y);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	in.close();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	finally
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		IOUtils.closeQuietly(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Read symbol that is drawn on all hexes.</summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void readMapBoardOverlaySymbolBlock(DataInputStream in) throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ADC2Utils.readBlockHeader(in, MapBoard Overlay Symbol);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	SymbolSet.SymbolData overlaySymbol = getSet().getMapBoardSymbol(ADC2Utils.readBase250Word(in));
	// the reason we use an ArrayList<> here is so that it is treated like all other drawn elements
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(overlaySymbol != null) 
	this.overlaySymbol.add(new MapBoardOverlay(overlaySymbol));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Block of flags to indicate which elements are actually displayed.</summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void readMapItemDrawFlagBlock(DataInputStream in) throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ADC2Utils.readBlockHeader(in, Map Item Draw Flag);
	
	// obviously, element types can't be sorted before we do this.
	// TODO: check this! If they're turned off in the map, can they be turned on
	// again in the player?
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final ArrayList < MapLayer > elements = new ArrayList < MapLayer >(mapElements);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(in.readByte() == 0) 
	mapElements.remove(elements.get(drawingPriorities [0]));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(in.readByte() == 0) 
	mapElements.remove(elements.get(drawingPriorities [1]));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(in.readByte() == 0) 
	mapElements.remove(elements.get(drawingPriorities [2]));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(in.readByte() == 0) 
	mapElements.remove(elements.get(drawingPriorities [3]));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(in.readByte() == 0) 
	mapElements.remove(elements.get(drawingPriorities [4]));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(in.readByte() == 0) 
	mapElements.remove(elements.get(drawingPriorities [7]));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(in.readByte() == 0) 
	mapElements.remove(elements.get(drawingPriorities [5]));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Attributes are tertiary symbols, any number of which can be attached to a specific hex.  Otherwise, they
	/// function the same as primary or secondary hex symbols.
	/// </summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void readAttributeBlock(DataInputStream in) throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ADC2Utils.readBlockHeader(in, Attribute Symbol);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int nAttributes = ADC2Utils.readBase250Word(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < nAttributes;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		int index = ADC2Utils.readBase250Word(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	SymbolSet.SymbolData symbol = set.getMapBoardSymbol(ADC2Utils.readBase250Word(in));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(isOnMapBoard(index) && symbol != null) 
	attributes.add(new HexData(index, symbol));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Read primary and secondary symbol information. Each hex may only have one of each. Additional symbols must
	/// be tertiary attributes.
	/// </summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void readHexDataBlock(DataInputStream in) throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ADC2Utils.readBlockHeader(in, Hex Data);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int count = getNColumns() * getNRows();
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < count;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// primary symbol
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int symbolIndex = ADC2Utils.readBase250Word(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	SymbolSet.SymbolData symbol = getSet().getMapBoardSymbol(symbolIndex);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(symbol != null) 
	primaryMapBoardSymbols.add(new HexData(i, symbol));
	
	// secondary symbol
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	symbolIndex = ADC2Utils.readBase250Word(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	symbol = getSet().getMapBoardSymbol(symbolIndex);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(symbol != null) 
	secondaryMapBoardSymbols.add(new HexData(i, symbol));
	
	/* int elevation = */
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	ADC2Utils.readBase250Word(in);
	
	// flags for hexsides, lines, and placenames: completely ignored
	/* int additionalInformation = */
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	in.readUnsignedByte();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Hex lines are like spokes of the hex and are typically used for things like roads or other elements that
	/// traverse from hex to hex.  The direction of each spoke is encoded as bit flags, and while ADC2 could encode
	/// each hex with only one record, modules typically have a separate record for every spoke resulting in
	/// data inflation.
	/// </summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void readHexLineBlock(DataInputStream in) throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ADC2Utils.readBlockHeader(in, Hex Line);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int nHexLines = ADC2Utils.readBase250Word(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < nHexLines;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		int index = ADC2Utils.readBase250Word(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int line = in.readUnsignedByte();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int direction = in.readUnsignedShort();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(isOnMapBoard(index)) 
	hexLines.add(new HexLine(index, line, direction));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Information about hex sides which are used for things like rivers, etc. is read in.</summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void readHexSideBlock(DataInputStream in) throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ADC2Utils.readBlockHeader(in, Hex Side);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int nHexSides = ADC2Utils.readBase250Word(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < nHexSides;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		int index = ADC2Utils.readBase250Word(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int line = in.readUnsignedByte();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int side = in.readUnsignedByte();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(isOnMapBoard(index)) 
	hexSides.add(new HexSide(index, line, side));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Information about the width, colour and style of hex sides and hex lines is read in.</summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void readLineDefinitionBlock(DataInputStream in) throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ADC2Utils.readBlockHeader(in, Line Definition);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int nLineDefinitions = in.readUnsignedByte();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	lineDefinitions = new LineDefinition [nLineDefinitions];
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < nLineDefinitions;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		int colorIndex = in.readUnsignedByte();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Color color = ADC2Utils.getColorFromIndex(colorIndex);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int size = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int j = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	j < 3;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ j)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		int s = in.readByte();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(j == zoomLevel) 
	size = s;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	// only used when editing ADC2 maps within ADC2.
	/* String name = */
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	readNullTerminatedString(in, 25);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int styleByte = in.readUnsignedByte();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	LineStyle style;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	switch(styleByte)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	case 2: 
	style = LineStyle.DOTTED;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	break;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	case 3: 
	style = LineStyle.DASH_DOT;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	break;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	case 4: 
	style = LineStyle.DASHED;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	break;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	case 5: 
	style = LineStyle.DASH_DOT_DOT;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	break;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	default: 
	style = LineStyle.SOLID;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	if(size > 0) 
	lineDefinitions [i] = new LineDefinition(color, size, style);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	else 
	lineDefinitions [i] = null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Read what order to draw the lines in.</summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void readLineDrawPriorityBlock(DataInputStream in) throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ADC2Utils.readBlockHeader(in, Line Draw Priority);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	in.readByte(); // unused.
	
	// there can only be 10 line definitions. however drawning priorities for hex sides and hex lines
	// are completely independent
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 1;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i <= 10;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		int index = in.readUnsignedByte();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(index < lineDefinitions.length && lineDefinitions [index] != null) 
	lineDefinitions [index].setHexLineDrawPriority(i);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	for(int i = 1;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i <= 10;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		int index = in.readUnsignedByte();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(index < lineDefinitions.length && lineDefinitions [index] != null) 
	lineDefinitions [index].setHexSideDrawPriority(i);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Read information on hex numbering.</summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void readMapSheetBlock(DataInputStream in) throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ADC2Utils.readBlockHeader(in, Map Sheet);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int nMapSheets = ADC2Utils.readBase250Word(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < nMapSheets;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		int x1 = ADC2Utils.readBase250Word(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int y1 = ADC2Utils.readBase250Word(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int x2 = ADC2Utils.readBase250Word(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int y2 = ADC2Utils.readBase250Word(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Rectangle r = new Rectangle(x1, y1, x2 - x1 + 1, y2 - y1 + 1);
	// must be exactly 9 bytes or 10 if there's a terminating null at the end
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	String name = readNullTerminatedString(in, 10);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(name.length() < 9) 
	in.readFully(new byte [9 - name.length()]);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int style = in.readUnsignedByte();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	in.readFully(new byte [2]);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int nColChars = in.readUnsignedByte();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int nRowChars = in.readUnsignedByte();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(i < nMapSheets - 1) // the last one is always ignored.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	mapSheets.add(new MapSheet(name, r, style, nColChars, nRowChars));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Read in information on hex sheets which define the hex numbering systems. This represents supplemental
	/// information--some map sheet info occurs earlier in the file.  Only the coordinates of the top-left corner
	/// are read in here.
	/// </summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void readHexNumberingBlock(DataInputStream in) throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ADC2Utils.readBlockHeader(in, Hex Numbering);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < mapSheets.size() + 1;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// rare case when integers are not base-250. However, they are big-endian despite being a
	// Windows application.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int col = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int j = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	j < 4;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ j)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		col <<= 8;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	col += in.readUnsignedByte();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	int row = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int j = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	j < 4;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ j)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		row <<= 8;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	row += in.readUnsignedByte();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	if(i < mapSheets.size())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		MapSheet ms = mapSheets.get(i);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	ms.setTopLeftCol(col);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	ms.setTopLeftRow(row);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Read and set the order of the drawn element types.</summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void readMapItemDrawingOrderBlock(DataInputStream in) throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ADC2Utils.readBlockHeader(in, Map Item Drawing Order);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	byte [] priority = new byte [10];
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	in.readFully(priority);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	ArrayList < MapLayer > items = new ArrayList < MapLayer >(mapElements.size());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < mapElements.size();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	
	// invalid index: abort reordering and switch back to default
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(priority [i] >= mapElements.size() || priority [i] < 0) 
	return;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(i > 0)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// abort reordering and switch back to default if any indeces are repeated
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int j = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	j < i;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ j)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(priority [j] == priority [i]) 
		return;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	items.add(mapElements.get(priority [i]));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	// find out where it moved
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < mapElements.size();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		drawingPriorities [priority [i]] =(byte) i;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	// swap default order with specified order
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	mapElements = items;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Crude version information.  Comes near the end of the file!  Actually it's just a flag to indicate whether
	/// the version is < 2.08.  In version 2.08, the hexes are abutted slightly differently.
	/// </summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void readVersionBlock(DataInputStream in) throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ADC2Utils.readBlockHeader(in, File Format Version);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int version = in.readByte();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	isPreV208 = version != 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> The colour to fill before any elements are drawn. The fast-scroll flag is also read.</summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void readTableColorBlock(DataInputStream in) throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ADC2Utils.readBlockHeader(in, Table Color);
	
	/* int fastScrollFlag = */
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	in.readByte();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	tableColor = ADC2Utils.getColorFromIndex(in.readUnsignedByte());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Optional labels that can be added to hexes.  Can also include a symbol that can be added with the label.</summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void readPlaceNameBlock(DataInputStream in) throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ADC2Utils.readBlockHeader(in, Place Name);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int nNames = ADC2Utils.readBase250Word(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < nNames;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		int index = ADC2Utils.readBase250Word(in);
	// extra hex symbol
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	SymbolSet.SymbolData symbol = getSet().getMapBoardSymbol(ADC2Utils.readBase250Word(in));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(symbol != null && isOnMapBoard(index)) 
	placeSymbols.add(new HexData(index, symbol));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	String text = readNullTerminatedString(in, 25);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Color color = ADC2Utils.getColorFromIndex(in.readUnsignedByte());
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int size = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int z = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	z < 3;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ z)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		int b = in.readUnsignedByte();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(z == zoomLevel) 
	size = b;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	PlaceNameOrientation orientation = null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int z = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	z < 3;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ z)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		int o = in.readByte();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(z == zoomLevel)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		switch(o)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	case 1: 
	orientation = PlaceNameOrientation.LOWER_CENTER;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	break;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	case 2: 
	orientation = PlaceNameOrientation.UPPER_CENTER;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	break;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	case 3: 
	orientation = PlaceNameOrientation.LOWER_RIGHT;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	break;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	case 4: 
	orientation = PlaceNameOrientation.UPPER_RIGHT;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	break;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	case 5: 
	orientation = PlaceNameOrientation.UPPER_LEFT;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	break;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	case 6: 
	orientation = PlaceNameOrientation.LOWER_LEFT;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	break;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	case 7: 
	orientation = PlaceNameOrientation.CENTER_LEFT;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	break;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	case 8: 
	orientation = PlaceNameOrientation.CENTER_RIGHT;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	break;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	case 9: 
	orientation = PlaceNameOrientation.HEX_CENTER;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	break;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	int font = in.readUnsignedByte();
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(!isOnMapBoard(index) || text.length() == 0 || orientation == null) 
	continue;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	placeNames.add(new PlaceName(index, text, color, orientation, size, font));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <param name="index">Index of hex or square in row-major order starting with the upper-left-hand corner (0-based).
	/// </param>
	/// <returns> <code>true</code> if <code>index</code> is valid, <code>false</code> otherwise.
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	boolean isOnMapBoard(int index)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return isOnMapBoard(index % columns, index / columns);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <param name="x">Hex or square column.
	/// </param>
	/// <param name="y">Hex or square row.
	/// </param>
	/// <returns> <code>true</code> if <code>x</code> and <code>y</code> are within the bounds of the board
	/// <code>false</code> otherwise.
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	boolean isOnMapBoard(int x, int y)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return x >= 0 && x < columns && y >= 0 && y < rows;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <returns> The Layout object corresponding to this imported map.
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected Layout getLayout()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return layout;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Returns the LineDefinition object corresponding to the given index.</summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected LineDefinition getLineDefinition(int index)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(index < 0 | index >= lineDefinitions.length) 
		return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	else 
	return lineDefinitions [index];
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <returns> Number of columns in the map.
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int getNColumns()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return columns;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <returns> Number of rows in the map.
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int getNRows()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return rows;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <returns> The <code>SymbolSet</code> needed by this map to render terrain and attribute elements.
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	SymbolSet getSet()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return set;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	@ Override
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void load(File f) throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		super.load(f);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	DataInputStream in = null;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	try
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		in = new DataInputStream(new BufferedInputStream(new FileInputStream(f)));
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	baseName = stripExtension(f.getName());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	path = f.getPath();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int header = in.readByte();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(header != - 3) 
	throw new FileFormatException(Invalid Mapboard File Header);
	
	// don't know what these do.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	in.readFully(new byte [2]);
	
	// get the symbol set
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	String s = readWindowsFileName(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	String symbolSetFileName = forceExtension(s, set);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	set = new SymbolSet();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	File setFile = action.getCaseInsensitiveFile(new File(symbolSetFileName), f, true, 
	new ExtensionFileFilter(ADC2Utils.SET_DESCRIPTION, new String []
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ ADC2Utils.SET_EXTENSION
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(setFile == null) 
	throw new FileNotFoundException(Unable to find symbol set file.);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	set.importFile(action, setFile);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	in.readByte(); // ignored
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	columns = ADC2Utils.readBase250Word(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	rows = ADC2Utils.readBase250Word(in);
	// presumably, they're all the same size (and they're square)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int hexSize = set.getMapBoardSymbolSize();
	
	// each block read separately
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	readHexDataBlock(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	readPlaceNameBlock(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	readHexSideBlock(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	readLineDefinitionBlock(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	readAttributeBlock(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	readMapSheetBlock(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	readHexLineBlock(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	readLineDrawPriorityBlock(in);
	// end of data blocks
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int orientation = in.read();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	switch(orientation)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	case 0:
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	case 1: // vertical hex orientation or grid offset column
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(set.getMapBoardSymbolShape() == SymbolSet.Shape.SQUARE) 
	layout = new GridOffsetColumnLayout(hexSize, columns, rows);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	else 
	layout = new VerticalHexLayout(hexSize, columns, rows);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	break;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	case 2: // horizontal hex orientation or grid offset row
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(set.getMapBoardSymbolShape() == SymbolSet.Shape.SQUARE) 
	layout = new GridOffsetRowLayout(hexSize, columns, rows);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	else 
	layout = new HorizontalHexLayout(hexSize, columns, rows);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	break;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	default: // square grid -- no offset
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	layout = new GridLayout(hexSize, columns, rows);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/* int saveMapPosition = */
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	in.readByte();
	
	/* int mapViewingPosition = */
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	in.readShort(); // probably base-250
	
	/* int mapViewingZoomLevel = */
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	in.readShort();
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	in.readByte(); // totally unknown
	
	// strangely, more blocks
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	readTableColorBlock(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	readHexNumberingBlock(in);
	
	// TODO: default map item drawing order appears to be different for different maps.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	try
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ // optional blocks
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	readMapBoardOverlaySymbolBlock(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	readVersionBlock(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	readMapItemDrawingOrderBlock(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	readMapItemDrawFlagBlock(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	catch(ADC2Utils.NoMoreBlocksException e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	in.close();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	finally
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		IOUtils.closeQuietly(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <returns> How many sides does each hex (6) or square (4) have?
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	int getNFaces()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return getLayout().getNFaces();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <returns> The point corresponding to the hex centre relative to the upper-left-hand corner.
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Point getCenterOffset()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return getLayout().getOrigin();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Given a row and column for a hex, return the point corresponding to the upper left-hand pixel.</summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Point coordinatesToPosition(int x, int y)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return getLayout().coordinatesToPosition(x, y, true);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <param name="index">Hex index in row-major order starting with the upper-left-hand corner (0-based).
	/// </param>
	/// <returns> upper-left-hand point of the hex or square. Returns <code>null</code> if the index is not valid.
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Point indexToPosition(int index)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return getLayout().coordinatesToPosition(index % columns, index / columns, true);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <param name="index">hex index in row-major order starting with the upper-left-hand corner (0-based).
	/// </param>
	/// <param name="nullIfOffBoard">return <code>null</code> if not a valid index. If <code>false</code> will
	/// return the point corresponding to the index if it were valid.
	/// </param>
	/// <returns> Point corresponding to the upper left hand corner of the hex or square.
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Point indexToPosition(int index, boolean nullIfOffBoard)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return getLayout().coordinatesToPosition(index % columns, index / columns, 
		nullIfOffBoard);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <param name="index">The hex index in row major order starting with the upper-left-hand corner (0-based).
	/// </param>
	/// <returns> <code>Point</code> corresponding to the centre of that hex.
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Point indexToCenterPosition(int index)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// get upper-left-hand corner of the hex
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Point p = indexToPosition(index);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(p == null) 
	return p;
	// shift to the centre
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	p.translate(getLayout().getDeltaX() / 2, getLayout().getDeltaY() / 2);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return p;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	@ Override
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void writeToArchive() throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		
		GameModule module = GameModule.getGameModule();
	
	// merge layers that can and should be merged.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	MapLayer base = new BaseLayer();
	// if there is no base map image, avoid creating a lot of layers.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(!((BaseLayer) base).hasBaseMap())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		Iterator < MapLayer > iter = mapElements.iterator();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	while(iter.hasNext())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		base.overlay(iter.next());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	iter.remove();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	mapElements.add(base);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		mapElements.add(0, base);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Iterator < MapLayer > iter = mapElements.iterator();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	iter.next();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	while(iter.hasNext())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		MapLayer next = iter.next();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(!next.isSwitchable())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		Iterator < MapLayer > iter2 = mapElements.iterator();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	MapLayer under = iter2.next();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	while(under != next)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		under.overlay(next);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	under = iter2.next();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	iter.remove();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	mapElements.add(0, base);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	for(MapLayer layer: mapElements)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		layer.writeToArchive();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	// map options: log formats
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	getMainMap().setAttribute(Map.MOVE_WITHIN_FORMAT, $pieceName$ moving from [$previousLocation$] to [$location$]);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	getMainMap().setAttribute(Map.MOVE_TO_FORMAT, $pieceName$ moving from [$previousLocation$] to [$location$]);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	getMainMap().setAttribute(Map.CREATE_FORMAT, $pieceName$ Added to [$location$]);
	
	// default grid
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	AbstractConfigurable ac = getLayout().getGeometricGrid();
	
	// TODO: set default grid numbering for maps that have no sheets (e.g., Air Assault on Crete).
	
	// ensure that we don't have a singleton null
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(mapSheets.size() == 1 && mapSheets.get(0) == null) 
	mapSheets.remove(0);
	
	// setup grids defined by ADC module
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Board board = getBoard();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(mapSheets.size() > 0)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ZonedGrid zg = new ZonedGrid();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(MapSheet ms: mapSheets)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(ms == null) // the last one is always null
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	break;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Zone z = ms.getZone();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(z != null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		insertComponent(z, zg);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	// add default grid
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	insertComponent(ac, zg);
	
	// add zoned grid to board
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	insertComponent(zg, board);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// add the default grid to the board
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	insertComponent(ac, board);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/* global properties */
	
	// for testing purposes
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	GlobalOptions options = module.getAllDescendantComponentsOf(GlobalOptions.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	class).toArray(new GlobalOptions [0]) [0];
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	options.setAttribute(GlobalOptions.AUTO_REPORT, GlobalOptions.ALWAYS);
	
	// add zoom capability
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(zoomLevel > 0)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		Zoomer zoom = new Zoomer();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	String [] s = new String [3];
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < 3;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i) 
	s [i] = Double.toString(set.getZoomFactor(i));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	zoom.setAttribute(zoomLevels, StringArrayConfigurer.arrayToString(s));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	insertComponent(zoom, getMainMap());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	// add place name capability
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(placeNames.size() > 0)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		writePlaceNames(module);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	// set up inventory button
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final Inventory inv = new Inventory();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	insertComponent(inv, module);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	inv.setAttribute(Inventory.BUTTON_TEXT, Search);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	inv.setAttribute(Inventory.TOOLTIP, Find place by name);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	inv.setAttribute(Inventory.FILTER, CurrentMap = Main Map && Type != Layer);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	inv.setAttribute(Inventory.ICON, );
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	inv.setAttribute(Inventory.GROUP_BY, Type);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Write out place name information as non-stackable pieces which can be searched via
	/// the piece inventory.
	/// 
	/// </summary>
	/// <param name="module">- Game module to write to.
	/// </param>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void writePlaceNames(GameModule module)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// write prototype
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	PrototypesContainer container = module.getAllDescendantComponentsOf(PrototypesContainer.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	class).iterator().next();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	PrototypeDefinition def = new PrototypeDefinition();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	insertComponent(def, container);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	def.setConfigureName(PLACE_NAMES);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	GamePiece gp = new BasicPiece();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	SequenceEncoder se = new SequenceEncoder(,);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	se.append(ADC2Utils.TYPE);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	gp = new Marker(Marker.ID + se.getValue(), gp);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	gp.setProperty(ADC2Utils.TYPE, PLACE_NAME);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	gp = new Immobilized(gp, Immobilized.ID + n;V);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	def.setPiece(gp);
	
	// write place names as pieces with no image.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	getMainMap();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final Point offset = getCenterOffset();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final HashSet < String > set = new HashSet < String >();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final Board board = getBoard();
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(PlaceName pn: placeNames)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		String name = pn.getText();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Point p = pn.getPosition();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(p == null) 
	continue;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(set.contains(name)) 
	continue;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	set.add(name);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	SetupStack stack = new SetupStack();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	insertComponent(stack, getMainMap());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	p.translate(offset.x, offset.y);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	String location = getMainMap().locationName(p);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	stack.setAttribute(SetupStack.NAME, name);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	stack.setAttribute(SetupStack.OWNING_BOARD, board.getConfigureName());
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	MapGrid mg = board.getGrid();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Zone z = null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(mg instanceof ZonedGrid) 
	z =((ZonedGrid) mg).findZone(p);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	stack.setAttribute(SetupStack.X_POSITION, Integer.toString(p.x));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	stack.setAttribute(SetupStack.Y_POSITION, Integer.toString(p.y));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(z != null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	try
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(mg.getLocation(location) != null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		assert(mg.locationName(mg.getLocation(location)).equals(location));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	stack.setAttribute(SetupStack.USE_GRID_LOCATION, true);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	stack.setAttribute(SetupStack.LOCATION, location);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	catch(BadCoords e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	BasicPiece bp = new BasicPiece();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	se = new SequenceEncoder(BasicPiece.ID, ;);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	se.append().append().append().append(name);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	bp.mySetType(se.getValue());
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	se = new SequenceEncoder(UsePrototype.ID.replaceAll(;, ), ;);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	se.append(PLACE_NAMES);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	gp = new UsePrototype(se.getValue(), bp);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	PieceSlot ps = new PieceSlot(gp);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	insertComponent(ps, stack);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Does this map board use old-style hex spacing?
	/// 
	/// </summary>
	/// <returns> <code>true</code> if this board is pre version 2.08, <code>false</code> if V2.08 or later.
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	boolean isPreV208Layout()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return isPreV208;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <returns> The VASSAL board object corresponding to the imported map.
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Board getBoard()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		BoardPicker picker = getBoardPicker();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	String boards [] = picker.getAllowableBoardNames();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	assert(boards.length <= 1);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Board board = null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(boards.length == 0)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		board = new Board();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	insertComponent(board, picker);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		board = picker.getBoard(boards [0]);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return board;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private ToolbarMenu getToolbarMenu()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		List < ToolbarMenu > list = getMainMap().getComponentsOf(ToolbarMenu.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	class);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	ToolbarMenu menu = null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(list.size() == 0)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		menu = new ToolbarMenu();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	insertComponent(menu, getMainMap());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	menu.setAttribute(ToolbarMenu.BUTTON_TEXT, View);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	menu.setAttribute(ToolbarMenu.TOOLTIP, Toggle visibility of map elements);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		assert(list.size() == 1);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	menu = list.get(0);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return menu;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <returns> The map background colour.
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Color getTableColor()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return tableColor;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <returns> The VASSAL BoardPicker object corresponding to this imported map.
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	BoardPicker getBoardPicker()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(boardPicker == null) 
		boardPicker = getMainMap().getAllDescendantComponentsOf(BoardPicker.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	class).toArray(new BoardPicker [0]) [0];
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return boardPicker;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	@ Override
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean isValidImportFile(File f) throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		DataInputStream in = null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	try
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		in = new DataInputStream(new FileInputStream(f));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	boolean valid = in.readByte() == - 3;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	in.close();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return valid;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	finally
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		IOUtils.closeQuietly(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}