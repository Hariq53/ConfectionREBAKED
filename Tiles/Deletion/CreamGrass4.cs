using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Localization;
using TheConfectionRebirth.Dusts;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using TheConfectionRebirth.Items;

namespace TheConfectionRebirth.Tiles.Deletion
{
    public class CreamGrass4 : ModTile
    {
		public override void SetStaticDefaults()
		{
			Main.tileCut[Type] = true;
			Main.tileSolid[Type] = false;
			Main.tileNoFail[Type] = true;
			Main.tileWaterDeath[Type] = true;
			Main.tileSolidTop[Type] = false;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileTable[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileID.Sets.SwaysInWindBasic[Type] = true;
			DustType = ModContent.DustType<CreamDust>();
			SoundStyle = 1;
			SoundType = 6;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.addTile(Type);
		}
	
		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}
		
		public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
		{
			Tile tileBelow = Framing.GetTileSafely(i, j + 1);
			int type = -1;
			if (tileBelow.HasTile && !tileBelow.BottomSlope)
			{
				type = tileBelow.TileType;
			}
			if (type == ModContent.TileType<SherbetTorch>() || type == Type)
			{
				return true;
			}
			WorldGen.KillTile(i, j);
			return true;
		}
	}
}
