using System;
namespace bsh
{
	
	/// <author>  Pat Niemeyer (pat@pat.net)
	/// </author>
	/*
	Note: which of these things should be checked at parse time vs. run time?*/
	[Serializable]
	public class Modifiers
	{
		public const int CLASS = 0;
		public const int METHOD = 1;
		public const int FIELD = 2;
		internal System.Collections.Hashtable modifiers;
		
		/// <param name="context">is METHOD or FIELD
		/// </param>
		public virtual void  addModifier(int context, System.String name)
		{
			if (modifiers == null)
				modifiers = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
			
			System.Object tempObject;
			tempObject = modifiers[name];
			modifiers[name] = System.Type.GetType("System.Void");
			System.Object existing = tempObject;
			if (existing != null)
				throw new System.SystemException("Duplicate modifier: " + name);
			
			int count = 0;
			if (hasModifier("private"))
				++count;
			if (hasModifier("protected"))
				++count;
			if (hasModifier("public"))
				++count;
			if (count > 1)
				throw new System.SystemException("public/private/protected cannot be used in combination.");
			
			switch (context)
			{
				
				case CLASS: 
					validateForClass();
					break;
				
				case METHOD: 
					validateForMethod();
					break;
				
				case FIELD: 
					validateForField();
					break;
				}
		}
		
		public virtual bool hasModifier(System.String name)
		{
			if (modifiers == null)
				modifiers = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
			return modifiers[name] != null;
		}
		
		// could refactor these a bit
		private void  validateForMethod()
		{
			insureNo("volatile", "Method");
			insureNo("transient", "Method");
		}
		private void  validateForField()
		{
			insureNo("synchronized", "Variable");
			insureNo("native", "Variable");
			insureNo("abstract", "Variable");
		}
		private void  validateForClass()
		{
			validateForMethod(); // volatile, transient
			insureNo("native", "Class");
			insureNo("synchronized", "Class");
		}
		
		private void  insureNo(System.String modifier, System.String context)
		{
			if (hasModifier(modifier))
				throw new System.SystemException(context + " cannot be declared '" + modifier + "'");
		}
		
		public override System.String ToString()
		{
			return "Modifiers: " + SupportClass.CollectionToString(modifiers);
		}
	}
}