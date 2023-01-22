using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TheConfectionRebirth.Biomes;
using TheConfectionRebirth.Dusts;
using TheConfectionRebirth.Items;
using TheConfectionRebirth.Items.Banners;
using TheConfectionRebirth.Pets.DudlingPet;

namespace TheConfectionRebirth.NPCs
{
	public class Dudley : ModNPC
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 4;
		}

		public override void SetDefaults()
		{
			NPC.width = 26;
			NPC.height = 24;
			NPC.damage = 48;
			NPC.defense = 22;
			NPC.lifeMax = 320;
			NPC.HitSound = SoundID.NPCHit13;
			NPC.DeathSound = SoundID.NPCDeath12;
			NPC.value = 650f;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 67;
			AIType = 360;
			Banner = NPC.type;
			BannerItem = ModContent.ItemType<DudleyBanner>();
			SpawnModBiomes = new int[1] { ModContent.GetInstance<SandConfectionUndergroundBiome>().Type };
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				new FlavorTextBestiaryInfoElement("Mods.TheConfectionRebirth.Bestiary.Dudley")
			});
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TaffyApple>(), 50));
			npcLoot.Add(ItemDropRule.Food(ModContent.ItemType<Brownie>(), 125));
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			int num = NPC.life > 0 ? 1 : 5;
			for (int k = 0; k < num; k++)
				Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<CritterBlood>());
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.InModBiome(ModContent.GetInstance<SandConfectionUndergroundBiome>())
				&& !spawnInfo.AnyInvasionActive())
			{
				return 1f;
			}
			return 0f;
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter > 8.0)
			{
				NPC.frameCounter = 0.0;

				NPC.frame.Y += frameHeight;

				if (NPC.frame.Y > frameHeight * 2)
					NPC.frame.Y = 0;
			}
		}
	}
}
