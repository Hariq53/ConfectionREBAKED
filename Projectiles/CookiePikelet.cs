﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheConfectionRebirth.Dusts;

namespace TheConfectionRebirth.Projectiles
{
	public class CookiePikelet : ModProjectile
	{
		protected virtual float HoldoutRangeMin => 98f;
		protected virtual float HoldoutRangeMax => 200f;

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Spear);
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			int duration = player.itemAnimationMax;

			player.heldProj = Projectile.whoAmI;

			if (Projectile.timeLeft > duration)
			{
				Projectile.timeLeft = duration;
			}

			Projectile.velocity = Vector2.Normalize(Projectile.velocity);

			float halfDuration = duration * 0.5f;
			float progress;

			if (Projectile.timeLeft < halfDuration)
			{
				progress = Projectile.timeLeft / halfDuration;
			}
			else
			{
				progress = (duration - Projectile.timeLeft) / halfDuration;
			}

			Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);

			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation += MathHelper.ToRadians(45f);
			}
			else
			{
				Projectile.rotation += MathHelper.ToRadians(135f);
			}

			if (!Main.dedServ)
			{
				if (Main.rand.NextBool(3))
				{
					Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<CookieCrumbs>(), Projectile.velocity.X * 2f, Projectile.velocity.Y * 2f, Alpha: 128, Scale: 1.2f);
				}

				if (Main.rand.NextBool(4))
				{
					Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<CookieCrumbs>(), Alpha: 128, Scale: 0.3f);
				}
			}

			return false;
		}
	}
}
