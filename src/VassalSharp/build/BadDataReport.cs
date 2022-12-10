/*
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
using ConfigureTree = VassalSharp.configure.ConfigureTree;
using Decorator = VassalSharp.counters.Decorator;
using EditablePiece = VassalSharp.counters.EditablePiece;
namespace VassalSharp.build
{
	
	/// <summary> General-purpose condition indicating that VASSAL has encountered data that's inconsistent with the current module.
	/// A typical example would be failing to find a map/board/image/prototype from the supplied name.  Covers a variety of
	/// situations where the most likely cause is a module version compatibility issue.
	/// 
	/// This is for recoverable errors that occur during game play, as opposed to {@link IllegalBuildException},
	/// which covers errors when building a module
	/// </summary>
	/// <seealso cref="ErrorDialog.dataError()">
	/// </seealso>
	/// <author>  rodneykinney
	/// 
	/// </author>
	public class BadDataReport
	{
		virtual public System.String Message
		{
			get
			{
				return message;
			}
			
		}
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		virtual public System.Exception Cause
		{
			get
			{
				return cause;
			}
			
		}
		virtual public System.String Data
		{
			get
			{
				return data;
			}
			
		}
		private System.String message;
		private System.String data;
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		private System.Exception cause;
		
		public BadDataReport()
		{
		}
		
		/// <summary> Basic Bad Data Report
		/// 
		/// </summary>
		/// <param name="message">Message to display
		/// </param>
		/// <param name="data">Data causing error
		/// </param>
		/// <param name="cause">Throwable that generated error
		/// </param>
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public BadDataReport(System.String message, System.String data, System.Exception cause):this(null, null, message, data, cause)
		{
		}
		
		public BadDataReport(System.String message, System.String data):this(message, data, null)
		{
		}
		
		/// <summary> Expanded Bad Data Report called by Traits.
		/// Display additional information to aid debugging
		/// 
		/// NB. Note use of piece.getLocalizedName() rather than
		/// Decorator.getOuterMost().getLocalizedName() which can result in infinite
		/// BadData reporting loops.
		/// </summary>
		/// <param name="piece">Trait that generated the error
		/// </param>
		/// <param name="message">Resource message key to display
		/// </param>
		/// <param name="data">Data causing error
		/// </param>
		/// <param name="cause">Throwable that generated error
		/// </param>
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public BadDataReport(EditablePiece piece, System.String message, System.String data, System.Exception cause):this(getPieceName(piece), piece.Description, message, data, cause)
		{
		}
		
		public BadDataReport(EditablePiece piece, System.String message, System.String data):this(getPieceName(piece), piece.Description, message, data, null)
		{
		}
		
		public BadDataReport(EditablePiece piece, System.String message):this(getPieceName(piece), piece.Description, message, "", null)
		{
		}
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public BadDataReport(System.String pieceName, System.String traitDesc, System.String message, System.String data, System.Exception cause)
		{
			System.String m = ((pieceName != null && pieceName.Length > 0)?pieceName + " ":"");
			m += ((traitDesc != null && traitDesc.Length > 0)?"[" + traitDesc + "] ":"");
			m += (m.Length > 0?"- ":"");
			m += message;
			this.message = m;
			this.cause = cause;
			this.data = data;
		}
		
		/// <summary> Return the name of the piece. For Decorators, return the name of the inner piece as
		/// the Bad Data Report may have been generated by the call to get the name of the Decorator
		/// in the first place.
		/// 
		/// </summary>
		/// <param name="piece">
		/// </param>
		/// <returns>
		/// </returns>
		protected internal static System.String getPieceName(EditablePiece piece)
		{
			if (piece is Decorator)
			{
				return ((Decorator) piece).getInner().LocalizedName;
			}
			else
			{
				return piece.LocalizedName;
			}
		}
		
		/// <summary> Expanded Bad Data Report for AbstractConfigurables.
		/// Display the name and type of the Configurable
		/// 
		/// </summary>
		/// <param name="c">AbstractConfigurable that generated the error
		/// </param>
		/// <param name="message">Resource message key to display
		/// </param>
		/// <param name="data">Data causing error
		/// </param>
		/// <param name="cause">Throwable that generated error
		/// </param>
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public BadDataReport(AbstractConfigurable c, System.String message, System.String data, System.Exception cause)
		{
			this.message = c.getConfigureName() + "[" + ConfigureTree.getConfigureName(c.GetType()) + "]: " + message;
			this.cause = cause;
			this.data = data;
		}
		
		public BadDataReport(AbstractConfigurable c, System.String messageKey, System.String data):this(c, messageKey, data, null)
		{
		}
	}
}