using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using TheConfectionRebirth.Biomes;
using TheConfectionRebirth.Items.Banners;

namespace TheConfectionRebirth.NPCs
{

	public class Hunger : ModNPC
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 6;
		}

		public override void SetDefaults()
		{
			NPC.width = 20;
			NPC.height = 20;
			NPC.damage = 80;
			NPC.defense = 22;
			NPC.lifeMax = 500;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath6;
			NPC.value = 60f;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 25;
			AIType = NPCID.PresentMimic;
			AnimationType = NPCID.PresentMimic;
			Banner = NPC.type;
			BannerItem = ModContent.ItemType<HungerBanner>();
			SpawnModBiomes = new int[1] { ModContent.GetInstance<SandConfectionSurfaceBiome>().Type };
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				new FlavorTextBestiaryInfoElement("Mods.TheConfectionRebirth.Bestiary.Hunger")
			});
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (Main.netMode == NetmodeID.Server)
				return;

			if (NPC.life <= 0)
			{
				for (int i = 0; i < 3; i++)
					Utilities.SpawnDeathGore(NPC, 13, 12, 11);

				for (int j = 0; j < 2; j++)
					Utilities.SpawnDeathGore(NPC, Mod, "HungerGore");
			}
		}
	}
}