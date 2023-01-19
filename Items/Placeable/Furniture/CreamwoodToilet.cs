using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheConfectionRebirth.Items.Placeable.Furniture
{
	public class CreamwoodToilet : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 12;
			Item.height = 30;
			Item.maxStack = 9999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 0;
			Item.createTile = ModContent.TileType<Tiles.Furniture.CreamwoodToilet>();
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Items.Placeable.CreamWood>(), 6).AddTile(TileID.Sawmill).Register();
		}
	}
}