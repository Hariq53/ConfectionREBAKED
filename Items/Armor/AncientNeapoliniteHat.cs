using Terraria.ID;
using Terraria.ModLoader;

namespace TheConfectionRebirth.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class AncientNeapoliniteHat : NeapoliniteHat
	{
		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Items.Placeable.NeapoliniteBar>(), 12).AddTile(TileID.DemonAltar).Register();
		}
	}
}