using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using TheConfectionRebirth.Biomes;
using TheConfectionRebirth.Items.Banners;

namespace TheConfectionRebirth.NPCs
{
	public class CrookedCookie : ModNPC
	{
		public override void SetStaticDefaults()
		{
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, new(0)
			{
				Position = new(0, -5f),
				PortraitPositionYOverride = -20f
			});
		}

		public override void SetDefaults()
		{
			NPC.width = 18;
			NPC.height = 18;
			NPC.damage = 60;
			NPC.defense = 24;
			NPC.lifeMax = 140;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath6;
			NPC.value = 60f;
			// npc.noGravity = false;
			// npc.noTileCollide = false;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 26;
			AIType = NPCID.Unicorn;
			Banner = NPC.type;
			BannerItem = ModContent.ItemType<CrookedCookieBanner>();
			SpawnModBiomes = new int[1] { ModContent.GetInstance<SandConfectionSurfaceBiome>().Type };
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				new FlavorTextBestiaryInfoElement("Mods.TheConfectionRebirth.Bestiary.CrookedCookie")
			});
		}

		public override void AI()
		{
			NPC.rotation += NPC.velocity.X * 0.05f;
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
					"CrookedCookieGore1",
					"CrookedCookieGore2"
				);
			}
		}
	}
}
