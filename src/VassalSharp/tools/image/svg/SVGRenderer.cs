/*
* $Id$
*
* Copyright (c) 2007-2008 by Joel Uckelman
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
//UPGRADE_TODO: The type 'org.apache.batik.bridge.BridgeContext' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using BridgeContext = org.apache.batik.bridge.BridgeContext;
//UPGRADE_TODO: The type 'org.apache.batik.bridge.BridgeException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using BridgeException = org.apache.batik.bridge.BridgeException;
//UPGRADE_TODO: The type 'org.apache.batik.bridge.DocumentLoader' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using DocumentLoader = org.apache.batik.bridge.DocumentLoader;
//UPGRADE_TODO: The type 'org.apache.batik.bridge.UserAgent' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using UserAgent = org.apache.batik.bridge.UserAgent;
//UPGRADE_TODO: The type 'org.apache.batik.dom.svg.SAXSVGDocumentFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SAXSVGDocumentFactory = org.apache.batik.dom.svg.SAXSVGDocumentFactory;
//UPGRADE_TODO: The type 'org.apache.batik.dom.svg.SVGDOMImplementation' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SVGDOMImplementation = org.apache.batik.dom.svg.SVGDOMImplementation;
//UPGRADE_TODO: The type 'org.apache.batik.ext.awt.image.GraphicsUtil' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using GraphicsUtil = org.apache.batik.ext.awt.image.GraphicsUtil;
//UPGRADE_TODO: The type 'org.apache.batik.gvt.renderer.ConcreteImageRendererFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ConcreteImageRendererFactory = org.apache.batik.gvt.renderer.ConcreteImageRendererFactory;
//UPGRADE_TODO: The type 'org.apache.batik.gvt.renderer.ImageRenderer' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ImageRenderer = org.apache.batik.gvt.renderer.ImageRenderer;
//UPGRADE_TODO: The type 'org.apache.batik.gvt.renderer.ImageRendererFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ImageRendererFactory = org.apache.batik.gvt.renderer.ImageRendererFactory;
//UPGRADE_TODO: The type 'org.apache.batik.transcoder.SVGAbstractTranscoder' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SVGAbstractTranscoder = org.apache.batik.transcoder.SVGAbstractTranscoder;
//UPGRADE_TODO: The type 'org.apache.batik.transcoder.TranscoderException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using TranscoderException = org.apache.batik.transcoder.TranscoderException;
//UPGRADE_TODO: The type 'org.apache.batik.transcoder.TranscoderInput' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using TranscoderInput = org.apache.batik.transcoder.TranscoderInput;
//UPGRADE_TODO: The type 'org.apache.batik.transcoder.TranscoderOutput' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using TranscoderOutput = org.apache.batik.transcoder.TranscoderOutput;
//UPGRADE_TODO: The type 'org.apache.batik.transcoder.TranscodingHints' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using TranscodingHints = org.apache.batik.transcoder.TranscodingHints;
//UPGRADE_TODO: The type 'org.apache.batik.transcoder.keys.BooleanKey' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using BooleanKey = org.apache.batik.transcoder.keys.BooleanKey;
//UPGRADE_TODO: The type 'org.apache.batik.transcoder.keys.PaintKey' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using PaintKey = org.apache.batik.transcoder.keys.PaintKey;
//UPGRADE_TODO: The type 'org.apache.batik.util.XMLResourceDescriptor' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using XMLResourceDescriptor = org.apache.batik.util.XMLResourceDescriptor;
//UPGRADE_TODO: The type 'org.apache.commons.lang.SystemUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SystemUtils = org.apache.commons.lang.SystemUtils;
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
using GameModule = VassalSharp.build.GameModule;
using ImageUtils = VassalSharp.tools.image.ImageUtils;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.tools.image.svg
{
	
	/// <summary> Render an SVG image to a {@link BufferedImage}.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class SVGRenderer
	{
		private void  InitBlock()
		{
			r = new Rasterizer();
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.tools.image.svg.SVGRenderer'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'docFactory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly SAXSVGDocumentFactory docFactory = new SAXSVGDocumentFactory(XMLResourceDescriptor.getXMLParserClassName());
		//UPGRADE_NOTE: Final was removed from the declaration of 'rendFactory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly ImageRendererFactory rendFactory = new ConcreteImageRendererFactory();
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'doc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Xml.XmlDocument doc;
		//UPGRADE_NOTE: Final was removed from the declaration of 'defaultW '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: Final was removed from the declaration of 'defaultH '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private float defaultW;
		private float defaultH;
		//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'r' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private Rasterizer r;
		
		public SVGRenderer(System.Uri file, System.IO.Stream in_Renamed):this(file.ToString(), in_Renamed)
		{
		}
		
		public SVGRenderer(System.String file, System.IO.Stream in_Renamed)
		{
			InitBlock();
			// load the SVG
			try
			{
				// We synchronize on docFactory becuase it does internal caching
				// of the Documents it produces. This ensures that a Document is
				// being modified on one thread only.
				lock (docFactory)
				{
					doc = docFactory.createDocument(file, in_Renamed);
				}
				in_Renamed.Close();
			}
			//UPGRADE_TODO: Class 'org.w3c.dom.DOMException' was converted to 'System.Exceptiont' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			catch (System.Exception e)
			{
				throw (System.IO.IOException) new System.IO.IOException().initCause(e);
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
			
			// get the default image size
			//UPGRADE_NOTE: Final was removed from the declaration of 'root '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Xml.XmlElement root = (System.Xml.XmlElement) doc.DocumentElement;
			
			defaultW = Float.parseFloat(root.GetAttribute("width", null).replaceFirst("px", ""));
			defaultH = Float.parseFloat(root.GetAttribute("height", null).replaceFirst("px", ""));
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'DEGTORAD '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly double DEGTORAD = System.Math.PI / 180.0;
		
		public virtual System.Drawing.Bitmap render()
		{
			return render(0.0, 1.0);
		}
		
		public virtual System.Drawing.Bitmap render(double angle, double scale)
		{
			// The renderer needs the bounds unscaled---scaling comes from the
			// width and height hints.
			System.Drawing.Drawing2D.Matrix temp_Matrix;
			temp_Matrix = new System.Drawing.Drawing2D.Matrix();
			temp_Matrix.RotateAt((float) (angle * DEGTORAD * (180 / System.Math.PI)), new System.Drawing.PointF((float) (defaultW / 2.0), (float) (defaultH / 2.0)));
			System.Drawing.Drawing2D.Matrix px = temp_Matrix;
			r.Transform = px;
			
			px = new System.Drawing.Drawing2D.Matrix((float) px.Elements.GetValue(0), (float) px.Elements.GetValue(1), (float) px.Elements.GetValue(2), (float) px.Elements.GetValue(3), (float) px.Elements.GetValue(4), (float) px.Elements.GetValue(5));
			px.Scale((System.Single) scale, (System.Single) scale);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'rect '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.RectangleF rect = new System.Drawing.RectangleF(0, 0, defaultW, defaultH);
			//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'java.awt.geom.AffineTransform.createTransformedShape' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomAffineTransformcreateTransformedShape_javaawtShape'"
			System.Drawing.RectangleF b = px.createTransformedShape(rect).GetBounds();
			
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			r.addTranscodingHint(Rasterizer.KEY_WIDTH, (float) b.Width);
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			r.addTranscodingHint(Rasterizer.KEY_HEIGHT, (float) b.Height);
			
			try
			{
				r.transcode(new TranscoderInput(doc), null);
				return r.BufferedImage;
			}
			// FIXME: review error message
			catch (BridgeException e)
			{
				logger.error("", e);
			}
			catch (TranscoderException e)
			{
				logger.error("", e);
			}
			
			return null;
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual System.Drawing.Bitmap render(double angle, double scale, ref System.Drawing.RectangleF aoi)
		{
			// The renderer needs the bounds unscaled---scaling comes from the
			// width and height hints.
			System.Drawing.Drawing2D.Matrix temp_Matrix;
			temp_Matrix = new System.Drawing.Drawing2D.Matrix();
			temp_Matrix.RotateAt((float) (angle * DEGTORAD * (180 / System.Math.PI)), new System.Drawing.PointF((float) (defaultW / 2.0), (float) (defaultH / 2.0)));
			System.Drawing.Drawing2D.Matrix px = temp_Matrix;
			r.Transform = px;
			
			px = new System.Drawing.Drawing2D.Matrix((float) px.Elements.GetValue(0), (float) px.Elements.GetValue(1), (float) px.Elements.GetValue(2), (float) px.Elements.GetValue(3), (float) px.Elements.GetValue(4), (float) px.Elements.GetValue(5));
			px.Scale((System.Single) scale, (System.Single) scale);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'rect '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.RectangleF rect = new System.Drawing.RectangleF(0, 0, defaultW, defaultH);
			
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			r.addTranscodingHint(Rasterizer.KEY_WIDTH, (float) aoi.Width);
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			r.addTranscodingHint(Rasterizer.KEY_HEIGHT, (float) aoi.Height);
			r.addTranscodingHint(Rasterizer.KEY_AOI, aoi);
			
			try
			{
				r.transcode(new TranscoderInput(doc), null);
				return r.BufferedImage;
			}
			// FIXME: review error message
			catch (BridgeException e)
			{
				logger.error("", e);
			}
			catch (TranscoderException e)
			{
				logger.error("", e);
			}
			
			return null;
		}
		
		private class DataArchiveDocumentLoader:DocumentLoader
		{
			public DataArchiveDocumentLoader(UserAgent userAgent):base(userAgent)
			{
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public virtual System.Xml.XmlDocument loadDocument(System.String uri)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'file '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Class 'java.net.URL' was converted to a 'System.Uri' which does not throw an exception if a URL specifies an unknown protocol. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1132'"
				System.String file = new System.IO.FileInfo((new System.Uri(uri)).AbsolutePath).Name;
				
				System.IO.BufferedStream in_Renamed = null;
				try
				{
					in_Renamed = new BufferedInputStream(GameModule.getGameModule().getDataArchive().getInputStream(file));
					//UPGRADE_NOTE: Final was removed from the declaration of 'doc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Xml.XmlDocument doc = loadDocument(uri, in_Renamed);
					in_Renamed.Close();
					return doc;
				}
				//UPGRADE_TODO: Class 'org.w3c.dom.DOMException' was converted to 'System.Exceptiont' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				catch (System.Exception e)
				{
					throw (System.IO.IOException) new System.IO.IOException().initCause(e);
				}
				finally
				{
					IOUtils.closeQuietly(in_Renamed);
				}
			}
		}
		
		private class Rasterizer:SVGAbstractTranscoder
		{
			virtual public System.Drawing.Bitmap BufferedImage
			{
				get
				{
					return image;
				}
				
			}
			virtual public System.Drawing.Drawing2D.Matrix Transform
			{
				set
				{
					xform = value;
				}
				
			}
			private DocumentLoader docLoader;
			private System.Drawing.Bitmap image;
			private System.Drawing.Drawing2D.Matrix xform;
			
			public Rasterizer()
			{
				docLoader = new DataArchiveDocumentLoader(userAgent);
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			protected internal virtual BridgeContext createBridgeContext()
			{
				return new BridgeContext(userAgent, docLoader);
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			protected internal virtual void  transcode(System.Xml.XmlDocument document, System.String uri, TranscoderOutput output)
			{
				if (SystemUtils.IS_OS_MAC_OSX)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Xml.XmlElement g = document.createElementNS(SVGDOMImplementation.SVG_NAMESPACE_URI, "g");
					g.SetAttribute("transform", null, "rotate(0.000001)");
					
					// interpose this <g> element between <svg> and its children
					//UPGRADE_NOTE: Final was removed from the declaration of 'svg '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Xml.XmlElement svg = (System.Xml.XmlElement) document.DocumentElement;
					System.Xml.XmlNode n = null;
					while ((n = svg.FirstChild) != null)
					{
						g.AppendChild(n);
					}
					
					svg.AppendChild(g);
				}
				
				// Sets up root, curTxf & curAoi
				base.transcode(document, uri, output);
				
				// prepare the image to be painted
				int w = (int) (width + 0.5);
				int h = (int) (height + 0.5);
				
				// paint the SVG document using the bridge package
				// create the appropriate renderer
				ImageRenderer renderer = VassalSharp.tools.image.svg.SVGRenderer.rendFactory.createStaticImageRenderer();
				renderer.updateOffScreen(w, h);
				if (xform != null)
					curTxf.concatenate(xform);
				renderer.Transform = curTxf;
				renderer.setTree(this.root);
				this.root = null; // We're done with it...
				
				// now we are sure that the aoi is the image size
				//UPGRADE_NOTE: Final was removed from the declaration of 'raoi '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.Drawing.Drawing2D.GraphicsPath raoi = new Rectangle2D.Float(0, 0, width, height);
				// Warning: the renderer's AOI must be in user space
				try
				{
					renderer.repaint(curTxf.createInverse().createTransformedShape(raoi));
				}
				catch (System.Exception e)
				{
					throw new TranscoderException(e);
				}
				
				// FIXME: is this the image we want to use?
				System.Drawing.Bitmap rend = renderer.getOffScreen();
				renderer = null; // We're done with it...
				
				// produce an opaque image if our background color is set
				//UPGRADE_NOTE: Final was removed from the declaration of 'dest '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Bitmap dest = ImageUtils.createCompatibleImage(w, h, !hints.containsKey(VassalSharp.tools.image.svg.SVGRenderer.KEY_BACKGROUND_COLOR));
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'g2d '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Graphics g2d = GraphicsUtil.createGraphics(dest);
				if (hints.containsKey(VassalSharp.tools.image.svg.SVGRenderer.KEY_BACKGROUND_COLOR))
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'bgcolor '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Brush bgcolor = (System.Drawing.Brush) hints.get_Renamed(VassalSharp.tools.image.svg.SVGRenderer.KEY_BACKGROUND_COLOR);
					//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setComposite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetComposite_javaawtComposite'"
					//UPGRADE_ISSUE: Field 'java.awt.AlphaComposite.SrcOver' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAlphaComposite'"
					g2d.setComposite(AlphaComposite.SrcOver);
					SupportClass.GraphicsManager.manager.SetPaint(g2d, bgcolor);
					g2d.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g2d), 0, 0, w, h);
				}
				
				if (rend != null)
				{
					// might be null if the svg document is empty
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.drawRenderedImage' was converted to 'System.Drawing.Graphics.DrawImage' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DdrawRenderedImage_javaawtimageRenderedImage_javaawtgeomAffineTransform'"
					g2d.Transform = new System.Drawing.Drawing2D.Matrix();
					g2d.DrawImage(rend, 0, 0);
				}
				g2d.Dispose();
				rend = null; // We're done with it...
				
				writeImage(dest, output);
			}
			
			private void  writeImage(System.Drawing.Bitmap image, TranscoderOutput output)
			{
				this.image = image;
			}
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'KEY_BACKGROUND_COLOR '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly TranscodingHints.Key KEY_BACKGROUND_COLOR = new PaintKey();
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'KEY_FORCE_TRANSPARENT_WHITE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly TranscodingHints.Key KEY_FORCE_TRANSPARENT_WHITE = new BooleanKey();
		static SVGRenderer()
		{
			logger = LoggerFactory.getLogger(typeof(SVGRenderer));
		}
	}
}