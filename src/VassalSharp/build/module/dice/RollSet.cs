using System;
using DieRoll = VassalSharp.build.module.DieRoll;
namespace VassalSharp.build.module.dice
{
	
	/// <summary>Describes a set of {@link DieRoll}s </summary>
	public class RollSet
	{
		virtual public System.String Description
		{
			get
			{
				return description;
			}
			
			set
			{
				this.description = value;
			}
			
		}
		virtual public DieRoll[] DieRolls
		{
			get
			{
				return dieRolls;
			}
			
		}
		virtual public int MaxDescLength
		{
			get
			{
				int len = 0;
				for (int i = 0; i < dieRolls.Length; i++)
				{
					len = System.Math.Max(len, dieRolls[i].Description.Length);
				}
				return len;
			}
			
		}
		public System.String description;
		public DieRoll[] dieRolls;
		
		public RollSet(System.String description, DieRoll[] rolls)
		{
			this.description = description;
			this.dieRolls = rolls;
		}
	}
}