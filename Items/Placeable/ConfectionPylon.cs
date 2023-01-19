using Terraria.Enums;
using Terraria.ModLoader;
using TheConfectionRebirth.Tiles.Pylon;

namespace TheConfectionRebirth.Items.Placeable
{
	public class ConfectionPylon : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
		}

		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<ConfectionPylonTile>());

			Item.SetShopValues(ItemRarityColor.Blue1, Terraria.Item.buyPrice(gold: 10));
		}
	}
}
