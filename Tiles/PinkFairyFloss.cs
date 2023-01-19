using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TheConfectionRebirth.Dusts;

namespace TheConfectionRebirth.Tiles
{
	public class PinkFairyFloss : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			TheConfectionRebirth.tileMerge[Type, Mod.Find<ModTile>("CreamGrass").Type] = true;
			TheConfectionRebirth.tileMerge[Type, Mod.Find<ModTile>("Creamstone").Type] = true;
			TheConfectionRebirth.tileMerge[Type, Mod.Find<ModTile>("CreamWood").Type] = true;
			TheConfectionRebirth.tileMerge[Type, Mod.Find<ModTile>("PurpleFairyFloss").Type] = true;
			TheConfectionRebirth.tileMerge[Type, Mod.Find<ModTile>("BlueFairyFloss").Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			ItemDrop = ModContent.ItemType<Items.Placeable.PinkFairyFloss>();
			AddMapEntry(new Color(253, 142, 250));
			DustType = ModContent.DustType<FairyFlossDust1>();
		}
	}
}