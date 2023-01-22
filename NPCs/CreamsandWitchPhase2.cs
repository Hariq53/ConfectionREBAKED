using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TheConfectionRebirth.Biomes;
using TheConfectionRebirth.Items;
using TheConfectionRebirth.Items.Armor;
using TheConfectionRebirth.Items.Banners;
using TheConfectionRebirth.Items.Placeable;
using TheConfectionRebirth.Pets.CreamsandWitchPet;

namespace TheConfectionRebirth.NPCs
{
	public class CreamsandWitchPhase2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 16;
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, new(0)
			{
				Velocity = 0.5f
			});
		}

		public override void SetDefaults()
		{
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 85;
			NPC.defense = 15;
			NPC.lifeMax = 2000;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath6;
			NPC.value = 60f;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 3;
			AIType = NPCID.ChaosElemental;
			AnimationType = NPCID.Mummy;
			Banner = NPC.type;
			BannerItem = ModContent.ItemType<CreamsandWitchBanner>();
			SpawnModBiomes = new int[1] { ModContent.GetInstance<SandConfectionSurfaceBiome>().Type };
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				new FlavorTextBestiaryInfoElement("Mods.TheConfectionRebirth.Bestiary.CreamsandWitch")
			});
		}

		public override void AI()
		{
			NPC.ai[0] += 1f;
			if (Main.rand.NextBool(500) && NPC.CountNPCS(ModContent.NPCType<Hunger>()) < 25)
				SpawnAlly(ModContent.NPCType<Hunger>());
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

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			Utilities.NPCLootAddRange(npcLoot,
				ItemDropRule.OneFromOptionsNotScalingWithLuck
				(
					chanceDenominator: 5,
					ModContent.ItemType<CreamHat>(),
					ModContent.ItemType<CookieCorset>(),
					ModContent.ItemType<CakeDress>()
				),
				ItemDropRule.Common
				(
					ModContent.ItemType<Creamsand>(),
					chanceDenominator: 1,
					minimumDropped: 30,
					maximumDropped: 50
				),
				ItemDropRule.Common
				(
					ModContent.ItemType<PixieStick>(),
					chanceDenominator: 10
				),
				ItemDropRule.Food
				(
					ModContent.ItemType<CreamySandwhich>(),
					chanceDenominator: 10
				),
				ItemDropRule.Food
				(
					ModContent.ItemType<Brownie>(),
					chanceDenominator: 150
				)
			);
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
					"CreamsandWitchGore1",
					"CreamsandWitchGore2"
				);
			}
		}
	}
}
