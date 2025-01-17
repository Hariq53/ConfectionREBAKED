using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TheConfectionRebirth.Projectiles;

namespace TheConfectionRebirth.Mounts
{
	public class Rollercycle : ModMount
	{
		protected class RollercycleSpecificData
		{
			internal static float[] offsets = new float[] { 0, 14, -14 };
		}

		public override void SetStaticDefaults()
		{
			MountData.jumpHeight = 20;
			MountData.acceleration = 0.06f;
			MountData.jumpSpeed = 4f;
			MountData.blockExtraJumps = true;
			MountData.constantJump = true;
			MountData.heightBoost = 20;
			MountData.fallDamage = 0f;
			MountData.runSpeed = 15f;
			MountData.dashSpeed = 10f;
			MountData.flightTimeMax = 0;

			MountData.fatigueMax = 0;
			MountData.buff = ModContent.BuffType<Buffs.RollercycleBuff>();

			MountData.spawnDust = ModContent.DustType<Dusts.NeapoliniteDust>();

			MountData.totalFrames = 5;
			MountData.playerYOffsets = Enumerable.Repeat(20, MountData.totalFrames).ToArray();
			MountData.xOffset = 13;
			MountData.yOffset = 0;
			MountData.playerHeadOffset = 22;
			MountData.bodyFrame = 3;
			MountData.standingFrameCount = 1;
			MountData.standingFrameDelay = 1;
			MountData.standingFrameStart = 1;
			MountData.runningFrameCount = 4;
			MountData.runningFrameDelay = 12;
			MountData.runningFrameStart = 1;
			MountData.flyingFrameCount = 0;
			MountData.flyingFrameDelay = 0;
			MountData.flyingFrameStart = 0;
			MountData.inAirFrameCount = 0;
			MountData.inAirFrameDelay = 0;
			MountData.inAirFrameStart = 0;
			MountData.idleFrameCount = 1;
			MountData.idleFrameDelay = 1;
			MountData.idleFrameStart = 1;
			MountData.idleFrameLoop = true;
			MountData.swimFrameCount = MountData.inAirFrameCount;
			MountData.swimFrameDelay = MountData.inAirFrameDelay;
			MountData.swimFrameStart = MountData.inAirFrameStart;

			if (!Main.dedServ)
			{
				MountData.textureWidth = MountData.backTexture.Width() + 20;
				MountData.textureHeight = MountData.backTexture.Height();
			}
		}

		public override void UpdateEffects(Player player)
		{
			if (Math.Abs(player.velocity.X) > 14f)
			{
				Rectangle rect = player.getRect();

				var entitySource = new EntitySource_Misc("");

				Projectile.NewProjectile(entitySource, player.Center.X - 4f, player.Center.Y - -20f, 0f, 0f, ModContent.ProjectileType<RollercycleTrail>(), 0, 0f, player.whoAmI, 0f);
			}
		}
	}
}