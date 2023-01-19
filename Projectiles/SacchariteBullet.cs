using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace TheConfectionRebirth.Projectiles
{
	public class SacchariteBullet : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
		}

		public override void AI()
		{
			Projectile.ai[0] += 1f;
			Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;
			Projectile.rotation = Projectile.velocity.ToRotation();
			if (Projectile.velocity.Y > 21f)
			{
				Projectile.velocity.Y = 21f;
			}
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation += MathHelper.Pi;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Projectile.ai[0] += 0.1f;
			Projectile.velocity *= 0.75f;
		}

		public override void Kill(int timeLeft)
		{
			if (Main.myPlayer != Projectile.owner)
			{
				return;
			}
			for (int i = 0; i < 2; i++)
			{
				Projectile.NewProjectile(new EntitySource_Misc("Rock candy shard from saccharite bullet"), Projectile.Center.X, Projectile.Center.Y, -8 + Main.rand.Next(0, 17), -8 + Main.rand.Next(0, 17), ModContent.ProjectileType<RockCandyShard>(), 24, 1f, Main.myPlayer, 0f, 0f);
			}
		}
	}
}