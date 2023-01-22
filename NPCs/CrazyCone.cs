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

	public class CrazyCone : ModNPC
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 6;
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, new(0)
			{
				Position = new Vector2(-2f, 6f),
				Velocity = 1f,
				PortraitPositionXOverride = -8f,
				PortraitPositionYOverride = 2f
			});
		}

		public override void SetDefaults()
		{
			NPC.width = 36;
			NPC.height = 36;
			NPC.damage = 80;
			NPC.defense = 18;
			NPC.lifeMax = 200;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath6;
			NPC.value = 60f;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 23;
			AIType = NPCID.EnchantedSword;
			AnimationType = NPCID.EnchantedSword;
			Banner = NPC.type;
			BannerItem = ModContent.ItemType<CrazyConeBanner>();
			SpawnModBiomes = new int[1] { ModContent.GetInstance<ConfectionUndergroundBiome>().Type };
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				new FlavorTextBestiaryInfoElement("Mods.TheConfectionRebirth.Bestiary.CrazyCone")
			});
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			Utilities.NPCLootAddRange(npcLoot,
				ItemDropRule.NormalvsExpert
				(
					ItemID.Nazar,
					chanceDenominatorInNormal: 100,
					chanceDenominatorInExpert: 50
				)
			);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.InModBiome(ModContent.GetInstance<ConfectionUndergroundBiome>())
				&& !spawnInfo.AnyInvasionActive())
			{
				return 0.08f;
			}
			return 0f;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (Main.netMode == NetmodeID.Server)
				return;

			if (NPC.life <= 0)
			{
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