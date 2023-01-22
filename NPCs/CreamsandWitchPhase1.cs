using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheConfectionRebirth.Biomes;

namespace TheConfectionRebirth.NPCs
{
	public class CreamsandWitchPhase1 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}

		public override void SetDefaults()
		{
			NPC.width = 40;
			NPC.height = 40;
			NPC.damage = 85;
			NPC.defense = 15;
			NPC.lifeMax = 1200;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath6;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 22;
			AIType = NPCID.FloatyGross;
			SpawnModBiomes = new int[1] { ModContent.GetInstance<SandConfectionSurfaceBiome>().Type };
		}

		public override void FindFrame(int frameHeight) => NPC.spriteDirection = NPC.direction;

		public override void AI()
		{
			NPC.ai[0] += 1f;

			if (Main.rand.NextBool(1000) && NPC.CountNPCS(ModContent.NPCType<CrookedCookie>()) < 25)
				SpawnAlly(ModContent.NPCType<CrookedCookie>());

			NPC.ai[0] += 2f;

			if (Main.rand.NextBool(1000) && NPC.CountNPCS(ModContent.NPCType<MintJr>()) < 25)
				SpawnAlly(ModContent.NPCType<MintJr>());
		}

		private void SpawnAlly(int type)
		{
			NPC.ai[0] = 0f;
			var ally =
				NPC.NewNPCDirect
				(
					source: NPC.GetSource_FromAI(),
					(int)NPC.Center.X,
					(int)NPC.Center.Y,
					type,
					ai0: NPC.whoAmI
				);

			ally.velocity.X = Main.rand.NextFloat(-0.4f, 0.4f);
			ally.velocity.Y = Main.rand.NextFloat(-0.5f, -0.05f);

			if (Main.netMode == NetmodeID.MultiplayerClient)
				NetMessage.SendData(MessageID.SyncNPC, number: ally.whoAmI);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.InModBiome(ModContent.GetInstance<SandConfectionSurfaceBiome>())
				&& !spawnInfo.Player.ZoneOldOneArmy
				&& !spawnInfo.Player.ZoneTowerNebula
				&& !spawnInfo.Player.ZoneTowerSolar
				&& !spawnInfo.Player.ZoneTowerStardust
				&& !spawnInfo.Player.ZoneTowerVortex
				&& !spawnInfo.Invasion)
			{
				return 0.01f;
			}
			return 0f;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (Main.netMode == NetmodeID.Server)
				return;

			if (NPC.life <= 0)
				SpawnPhase2NPC();
		}

		private void SpawnPhase2NPC()
		{
			Vector2 spawnPos = NPC.Center + new Vector2(0f, NPC.height / 2f);

			NPC.NewNPC
			(
				source: NPC.GetSource_FromAI(),
				(int)spawnPos.X,
				(int)spawnPos.Y,
				ModContent.NPCType<CreamsandWitchPhase2>()
			);

			Utilities.SpawnDeathGore(NPC, Mod,
				"CreamsandWitchBroomGore1",
				"CreamsandWitchBroomGore2"
			);
		}
	}
}
