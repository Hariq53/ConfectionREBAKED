using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using TheConfectionRebirth.Items.Placeable;

namespace TheConfectionRebirth.Items.Weapons
{
	public class NeapoliniteJoustingLance : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
		}

		public override void SetDefaults()
		{
			Item.DefaultToSpear(ModContent.ProjectileType<Projectiles.NeapoliniteJoustingLanceProjectile>(), 1f, 24);

			Item.DamageType = DamageClass.MeleeNoSpeed;
			Item.SetWeaponValues(80, 12f, 0);
			Item.SetShopValues(ItemRarityColor.LightRed4, Item.buyPrice(0, 6));
			Item.channel = true;
			Item.StopAnimationOnHurt = true;
		}

		public override bool MeleePrefix() => true;

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<NeapoliniteBar>(12)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}