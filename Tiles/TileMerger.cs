using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace TheConfectionRebirth
{
	public class TileMergeGroup
	{
		private List<int> tilesToMerge = new();

		public static TileMergeGroup GroundGroup;
		public class TileMergeGroup_Loader : ILoadable
		{
			public void Load(Mod mod)
			{
				GroundGroup = new();
			}

			public void Unload()
			{
				GroundGroup = null;
			}
		}

		public TileMergeGroup(params int[] vanillaBlocks)
		{
			tilesToMerge.AddRange(vanillaBlocks);
		}
		public void MergeTile(int newTile)
		{
			tilesToMerge.ForEach(i =>
			{
				Main.tileMerge[i][newTile] = true;
				Main.tileMerge[newTile][i] = true;
			});
			tilesToMerge.Add(newTile);
		}
	}
}
