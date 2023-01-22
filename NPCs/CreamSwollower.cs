using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TheConfectionRebirth.Biomes;
using TheConfectionRebirth.Items.Banners;

namespace TheConfectionRebirth.NPCs
{
	public class CreamSwollower : ModNPC
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 4;
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, new(0)
			{
				Position = new(35f, 10f),
				PortraitPositionXOverride = 0f
			});
		}

		public override void SetDefaults()
		{
			NPC.width = 40;
			NPC.height = 40;
			NPC.damage = 50;
			NPC.defense = 10;
			NPC.lifeMax = 360;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath6;
			NPC.value = 60f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.behindTiles = true;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 103;
			AIType = NPCID.SandShark;
			AnimationType = NPCID.SandShark;
			Banner = Type;
			BannerItem = ModContent.ItemType<CreamSwollowerBanner>();
			SpawnModBiomes = new int[1] { ModContent.GetInstance<SandConfectionSurfaceBiome>().Type };
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Sandstorm,

				new FlavorTextBestiaryInfoElement("Mods.TheConfectionRebirth.Bestiary.CreamSwollower")
			});
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			Utilities.NPCLootAddRange(npcLoot,
				ItemDropRule.Common
				(
					ItemID.SharkFin,
					chanceDenominator: 8
				),
				ItemDropRule.Food
				(
					ItemID.Nachos,
					chanceDenominator: 30
				),
				ItemDropRule.Common
				(
					ItemID.LightShard,
					chanceDenominator: 25
				),
				ItemDropRule.ByCondition
				(
					new Conditions.WindyEnoughForKiteDrops(),
					ItemID.KiteSandShark,
					chanceDenominator: 25
				)
			);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.ZoneSandstorm
				&& spawnInfo.Player.InModBiome(ModContent.GetInstance<SandConfectionSurfaceBiome>())
				&& !spawnInfo.AnyInvasionActive())
			{
				return 0.5f;
			}
			return 0f;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (Main.netMode == NetmodeID.Server)
			{
				return;
			}

			if (NPC.life <= 0)
			{
				Utilities.SpawnDeathGore(NPC, Mod,
					"CreamSwollowerGore1",
					"CreamSwollowerGore2",
					"CreamSwollowerGore3",
					"CreamSwollowerGore4"
				);
			}
		}
	}
}
