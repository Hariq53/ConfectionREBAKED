using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TheConfectionRebirth.Dusts;

namespace TheConfectionRebirth.Projectiles
{

	public class ChocolateChip : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(24);
			AIType = 24;
			Projectile.timeLeft = 60;
			Projectile.timeLeft = 200;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			fallThrough = false;
			return true;
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<ChipDust>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
			}
			if (Main.myPlayer != Projectile.owner)
			{
				return;
			}
		}
	}
}