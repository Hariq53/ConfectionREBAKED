using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TheConfectionRebirth.Dusts;

namespace TheConfectionRebirth.Tiles
{
	public class PurpleFairyFloss : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			TheConfectionRebirth.tileMerge[Type, Mod.Find<ModTile>("CreamGrass").Type] = true;
			TheConfectionRebirth.tileMerge[Type, Mod.Find<ModTile>("Creamstone").Type] = true;
			TheConfectionRebirth.tileMerge[Type, Mod.Find<ModTile>("CreamWood").Type] = true;
			TheConfectionRebirth.tileMerge[Type, Mod.Find<ModTile>("PinkFairyFloss").Type] = true;
			TheConfectionRebirth.tileMerge[Type, Mod.Find<ModTile>("BlueFairyFloss").Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			ItemDrop = ModContent.ItemType<Items.Placeable.PurpleFairyFloss>();
			AddMapEntry(new Color(210, 90, 250));
			DustType = ModContent.DustType<FairyFlossDust2>();
		}
	}
}