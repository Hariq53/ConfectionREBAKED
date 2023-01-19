using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheConfectionRebirth.Projectiles;

namespace TheConfectionRebirth.Items
{
	internal class DimensionSplit : ModItem
	{
		public virtual float ai1 => 0f;

		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
		}
		public override void SetDefaults()
		{
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.width = 30;
			Item.height = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(0, 50, 0, 0);
			Item.rare = ItemRarityID.Lime;
			Item.shoot = ModContent.ProjectileType<DimWarp>();
			Item.UseSound = SoundID.Item8;
			Item.autoReuse = true;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			ConfectionPlayer modPlayer = player.GetModPlayer<ConfectionPlayer>();
			int tileX = (int)((Main.mouseX + Main.screenPosition.X) / 16);
			int tileY = (int)((Main.mouseY + Main.screenPosition.Y) / 16);
			if (modPlayer.DimensionalWarp == null && (!Main.tile[tileX, tileY].HasTile || !Main.tileSolid[Main.tile[tileX, tileY].TileType]))
			{
				Projectile.NewProjectile(source, Main.MouseWorld, velocity, type, damage, knockback, player.whoAmI, ai1: ai1);
			}
			else if (modPlayer.DimensionalWarp != null && player.ownedProjectileCounts[ModContent.ProjectileType<DimWarp2>()] == 0)
			{
				Projectile.NewProjectile(source, Main.MouseWorld, velocity, ModContent.ProjectileType<DimWarp2>(), 1, knockback, player.whoAmI, ai1: ai1);
			}
			return false;
		}
	}
}
