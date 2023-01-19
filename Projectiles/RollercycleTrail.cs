using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TheConfectionRebirth.Projectiles
{
	public class RollercycleTrail : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 24;
			Projectile.height = 24;
			Projectile.friendly = true;
			//Projectile.alpha = byte.MaxValue;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 20;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}

		public override void AI()
		{
			Projectile.ai[0] += 1f;
			Projectile.alpha = (int)Projectile.ai[0] * 2;

			if (Projectile.localAI[0] > 130f)
			{
				Projectile.Kill();
			}

		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0) * Projectile.Opacity;
		}
	}
}