using System;
namespace VassalSharp.tools.image.tilecache
{
	
	public class ZipFileImageTilerState
	{
		private ZipFileImageTilerState()
		{
		}
		
		public const sbyte STARTING_IMAGE = 1;
		public const sbyte TILE_WRITTEN = 2;
		public const sbyte TILING_FINISHED = 3;
	}
}