using Terraria;
using Terraria.ModLoader;

namespace TheConfectionRebirth.Items.Placeable
{
	public class SherbetBricks : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 100;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 16;
			Item.maxStack = 9999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.value = Item.buyPrice(0, 0, 0);
			Item.consumable = true;
			Item.createTile = ModContent.TileType<Tiles.SherbetBricks>();
		}
	}
}
