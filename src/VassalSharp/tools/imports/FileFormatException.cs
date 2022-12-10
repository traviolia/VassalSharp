using System;
namespace VassalSharp.tools.imports
{
	
	/// <summary> File cannot be interpreted.  Either the file is not what VASSAL thinks it is
	/// or it is currupted in some way.
	/// </summary>
	[Serializable]
	public class FileFormatException:System.IO.IOException
	{
		
		private const long serialVersionUID = 0L;
		
		internal FileFormatException()
		{
		}
		
		public FileFormatException(System.String s):base(s)
		{
		}
	}
}