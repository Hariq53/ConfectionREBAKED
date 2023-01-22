using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TheConfectionRebirth.Biomes;
using TheConfectionRebirth.Items;
using TheConfectionRebirth.Items.Weapons;

namespace TheConfectionRebirth.NPCs
{
	public class BigMimicConfection : ModNPC
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 14;
		}

		public override void SetDefaults()
		{
			NPC.width = 30;
			NPC.height = 40;
			NPC.damage = 180;
			NPC.defense = 34;
			NPC.lifeMax = 3500;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 60f;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 87;
			AIType = NPCID.BigMimicHallow;
			AnimationType = NPCID.BigMimicHallow;
			SpawnModBiomes = new int[1] { ModContent.GetInstance<ConfectionUndergroundBiome>().Type };
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("Mods.TheConfectionRebirth.Bestiary.BigMimicConfection")
			});
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.InModBiome(ModContent.GetInstance<ConfectionUndergroundBiome>())
			    && !spawnInfo.AnyInvasionActive())
			{
				return 0.01f;
			}
			return 0f;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			Utilities.NPCLootAddRange(npcLoot,
				ItemDropRule.OneFromOptionsNotScalingWithLuck
				(
					chanceDenominator: 1,
					ModContent.ItemType<CookieCrumbler>(),
					ModContent.ItemType<SweetTooth>(),
					ModContent.ItemType<SweetHook>(),
					ModContent.ItemType<CreamSpray>()
				),
				ItemDropRule.Common
				(
					ItemID.GreaterHealingPotion,
					chanceDenominator: 1,
					minimumDropped: 5,
					maximumDropped: 10
				),
				ItemDropRule.Common
				(
					ItemID.GreaterManaPotion,
					chanceDenominator: 1,
					minimumDropped: 5,
					maximumDropped: 15
				)
			);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (Main.netMode == NetmodeID.Server)
				return;

			if (NPC.life <= 0)
			{
				// Repeated code
				var entitySource = NPC.GetSource_Death();

				for (int i = 0; i < 3; i++)
				{
					Gore.NewGore
					(
						entitySource,
						NPC.position,
						Velocity: new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)),
						Type: 13
					 );

					Gore.NewGore
					(
						entitySource,
						NPC.position,
						Velocity: new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)),
						Type: 12
					 );

					Gore.NewGore
					(
						entitySource,
						NPC.position,
						Velocity: new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)),
						Type: 11
					);
				}
			}
		}
	}
}