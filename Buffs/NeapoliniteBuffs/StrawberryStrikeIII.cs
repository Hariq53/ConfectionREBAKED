using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TheConfectionRebirth.Projectiles;

namespace TheConfectionRebirth.Buffs.NeapoliniteBuffs
{
	public class StrawberryStrikeIII : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<StrawberryStrike>()] < 3)
			{
				Projectile.NewProjectile(new EntitySource_Misc("Strawberry Strike III 1"), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<StrawberryStrike>(), 50, 8f, player.whoAmI);
				Projectile.NewProjectile(new EntitySource_Misc("Strawberry Strike III 2"), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<StrawberryStrike>(), 50, 8f, player.whoAmI);
				Projectile.NewProjectile(new EntitySource_Misc("Strawberry Strike III 3"), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<StrawberryStrike>(), 50, 8f, player.whoAmI);
			}
		}
	}
}
