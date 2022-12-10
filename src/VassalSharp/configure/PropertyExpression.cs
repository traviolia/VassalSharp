using System;
using BadDataReport = VassalSharp.build.BadDataReport;
using PropertySource = VassalSharp.build.module.properties.PropertySource;
using GamePiece = VassalSharp.counters.GamePiece;
using PieceFilter = VassalSharp.counters.PieceFilter;
using Resources = VassalSharp.i18n.Resources;
using Expression = VassalSharp.script.expression.Expression;
using ExpressionException = VassalSharp.script.expression.ExpressionException;
using NullExpression = VassalSharp.script.expression.NullExpression;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
namespace VassalSharp.configure
{
	
	/*
	* Class encapsulating a Property Match Expression
	* A PropertyExpression is it's own PieceFilter.
	*/
	public class PropertyExpression : PieceFilter
	{
		virtual public System.String Expression
		{
			get
			{
				return expression.getExpression();
			}
			
			set
			{
				expression = Expression.createPropertyExpression(value);
			}
			
		}
		virtual public bool Null
		{
			get
			{
				return expression == null || expression is NullExpression;
			}
			
		}
		
		protected internal Expression expression = new NullExpression();
		
		public PropertyExpression()
		{
		}
		
		public PropertyExpression(System.String s)
		{
			Expression = s;
		}
		
		public virtual PieceFilter getFilter(PropertySource source)
		{
			return expression.getFilter(source);
		}
		
		public virtual PieceFilter getFilter()
		{
			return expression.getFilter();
		}
		
		public virtual bool accept(GamePiece piece)
		{
			return accept(piece, piece);
		}
		
		public virtual bool accept(GamePiece source, GamePiece piece)
		{
			return getFilter(source).accept(piece);
		}
		
		public  override bool Equals(System.Object o)
		{
			if (o is PropertyExpression)
			{
				return Expression.Equals(((PropertyExpression) o).Expression);
			}
			return false;
		}
		
		/// <summary> Evaluate the Property Expression as true/false using
		/// a supplied property source
		/// 
		/// </summary>
		/// <param name="ps">Property Source   *
		/// </param>
		/// <returns> boolean result
		/// </returns>
		public virtual bool isTrue(PropertySource ps)
		{
			System.String result = null;
			try
			{
				result = expression.evaluate(ps);
			}
			catch (ExpressionException e)
			{
				ErrorDialog.dataError(new BadDataReport(Resources.getString("Error.expression_error"), "Expression=" + Expression + ", Error=" + e.Error, e));
			}
			return "true".Equals(result);
		}
		//UPGRADE_NOTE: The following method implementation was automatically added to preserve functionality. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1306'"
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}