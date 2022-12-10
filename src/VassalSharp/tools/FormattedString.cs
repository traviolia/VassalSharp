/// <summary> FormattedString.java
/// 
/// A String that can include options of the form $optionName$. Option values
/// are maintained in a property list and getText returns the string will all
/// options replaced by their value
/// 
/// 
/// </summary>
using System;
using AbstractConfigurable = VassalSharp.build.AbstractConfigurable;
using BadDataReport = VassalSharp.build.BadDataReport;
using GameModule = VassalSharp.build.GameModule;
using PropertySource = VassalSharp.build.module.properties.PropertySource;
//using EditablePiece = VassalSharp.counters.EditablePiece;
//using GamePiece = VassalSharp.counters.GamePiece;
using Resources = VassalSharp.i18n.Resources;
//using Expression = VassalSharp.script.expression.Expression;
//using ExpressionException = VassalSharp.script.expression.ExpressionException;
namespace VassalSharp.tools
{
	
	public class FormattedString
	{
		virtual public System.String Format
		{
			get
			{
				return formatString;
			}
			
			set
			{
				formatString = value;
				format = Expression.createExpression(value);
			}
			
		}
		virtual public PropertySource DefaultProperties
		{
			get
			{
				return defaultProperties;
			}
			
			set
			{
				this.defaultProperties = value;
			}
			
		}
		
		// The actual string for display purposes
		protected internal System.String formatString;
		
		// An efficiently evaluable representation of the string
		protected internal Expression format;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected Map < String, String > props = new HashMap < String, String >();
		protected internal PropertySource defaultProperties;
		
		public FormattedString():this("")
		{
		}
		
		public FormattedString(System.String s):this(s, GameModule.getGameModule())
		{
		}
		
		public FormattedString(PropertySource defaultProperties):this("", defaultProperties)
		{
		}
		
		public FormattedString(System.String formatString, PropertySource defaultProperties)
		{
			Format = formatString;
			this.defaultProperties = defaultProperties;
		}
		
		public virtual void  setProperty(System.String name, System.String value_Renamed)
		{
			props.put(name, value_Renamed);
		}
		
		public virtual void  clearProperties()
		{
			props.clear();
		}
		
		/// <summary> Return the resulting string after substituting properties</summary>
		/// <returns>
		/// </returns>
		public virtual System.String getText()
		{
			return getText(defaultProperties, false);
		}
		
		public virtual System.String getLocalizedText()
		{
			return getText(defaultProperties, true);
		}
		
		/// <summary> Return the resulting string after substituting properties
		/// Also, if any property keys match a property in the given GamePiece,
		/// substitute the value of that property
		/// </summary>
		/// <seealso cref="GamePiece.getProperty">
		/// </seealso>
		/// <param name="ps">
		/// </param>
		/// <returns>
		/// </returns>
		public virtual System.String getText(PropertySource ps)
		{
			return getText(ps, false);
		}
		
		/// <summary> Return the resulting string after substituting properties
		/// Also, if any property keys match a property in the given GamePiece,
		/// substitute the value of that property. If the resulting string is
		/// empty, then the default is returned.
		/// </summary>
		/// <seealso cref="GamePiece.getProperty">
		/// </seealso>
		/// <param name="ps">
		/// </param>
		/// <param name="def">the default if the result is otherwise empty
		/// </param>
		/// <returns>
		/// </returns>
		public virtual System.String getText(PropertySource ps, System.String def)
		{
			System.String s = getText(ps, false);
			if (s == null || s.Length == 0)
			{
				s = def;
			}
			return s;
		}
		
		public virtual System.String getLocalizedText(PropertySource ps)
		{
			return getText(ps, true);
		}
		
		protected internal virtual System.String getText(PropertySource ps, bool localized)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'source '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			PropertySource source = (ps == null)?defaultProperties:ps;
			try
			{
				return format.evaluate(source, props, localized);
			}
			catch (ExpressionException e)
			{
				if (source is EditablePiece)
				{
					ErrorDialog.dataError(new BadDataReport((EditablePiece) source, Resources.getString("Error.expression_error"), format.getExpression(), e));
				}
				else if (source is AbstractConfigurable)
				{
					ErrorDialog.dataError(new BadDataReport((AbstractConfigurable) source, Resources.getString("Error.expression_error"), format.getExpression(), e));
				}
				else
				{
					ErrorDialog.dataError(new BadDataReport(Resources.getString("Error.expression_error"), format.getExpression(), e));
				}
				return "";
			}
		}
		
		/// <summary> Expand a FormattedString using the supplied propertySource and parse it as
		/// an integer. If the expanded string is not an integer, generate a Bad Data Report
		/// with debugging information and return a value of 0
		/// 
		/// </summary>
		public virtual int getTextAsInt(PropertySource ps, System.String description, EditablePiece source)
		{
			int result = 0;
			//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String value_Renamed = getText(ps, "0");
			try
			{
				result = System.Int32.Parse(value_Renamed);
			}
			catch (System.FormatException e)
			{
				ErrorDialog.dataError(new BadDataReport(source, Resources.getString("Error.non_number_error"), debugInfo(this, value_Renamed, description), e));
			}
			return result;
		}
		
		public virtual int getTextAsInt(PropertySource ps, System.String description, AbstractConfigurable source)
		{
			int result = 0;
			//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String value_Renamed = getText(ps, "0");
			try
			{
				result = System.Int32.Parse(value_Renamed);
			}
			catch (System.FormatException e)
			{
				ErrorDialog.dataError(new BadDataReport(source, Resources.getString("Error.non_number_error"), debugInfo(this, value_Renamed, description), e));
			}
			return result;
		}
		
		/// <summary> Format a standard debug message for use in Decorator bad data reports.
		/// 
		/// description=value
		/// description[format]=value
		/// 
		/// Use format 1 if the generated value is the same as the format
		/// Use format 2 if the formated contains an expression that has been expanded.
		/// 
		/// </summary>
		/// <param name="format">Formatted String
		/// </param>
		/// <param name="description">Description of the String
		/// </param>
		/// <param name="value">Value generated by the formatted string
		/// </param>
		/// <returns> error message
		/// </returns>
		public static System.String debugInfo(FormattedString format, System.String value_Renamed, System.String description)
		{
			return description + (value_Renamed.Equals(format.Format)?"":"[" + format.Format + "]") + "=" + value_Renamed;
		}
		
		public virtual System.String debugInfo(System.String value_Renamed, System.String description)
		{
			return debugInfo(this, value_Renamed, description);
		}

		public override int GetHashCode()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'prime '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int prime = 31;
			int result = 1;
			result = prime * result + ((formatString == null)?0:formatString.GetHashCode());
			return result;
		}

		public  override bool Equals(System.Object obj)
		{
			if (this == obj)
				return true;
			if (obj == null)
				return false;
			if (GetType() != obj.GetType())
				return false;
			FormattedString other = (FormattedString) obj;
			if (formatString == null)
			{
				if (other.formatString != null)
					return false;
			}
			else if (!formatString.Equals(other.formatString))
				return false;
			return true;
		}
	}
}