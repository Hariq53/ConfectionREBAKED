using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using TheConfectionRebirth.Biomes;
using TheConfectionRebirth.Items.Banners;

namespace TheConfectionRebirth.NPCs.Critters
{
	internal class ChocolateBunny : ModNPC
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Bunny];
			Main.npcCatchable[NPC.type] = true;
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, new(0)
			{
				Velocity = 1f
			});
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Bunny);
			NPC.catchItem = (short)ModContent.ItemType<ChocolateBunnyItem>();
			NPC.aiStyle = 7;
			NPC.friendly = true;
			AIType = NPCID.Bunny;
			AnimationType = NPCID.Bunny;
			Banner = NPC.type;
			BannerItem = ModContent.ItemType<ChocolateBunnyBanner>();
			SpawnModBiomes = new int[1] { ModContent.GetInstance<ConfectionBiomeSurface>().Type };
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				new FlavorTextBestiaryInfoElement("Mods.TheConfectionRebirth.Bestiary.ChocolateBunny")
			});
		}

		public override bool? CanBeHitByItem(Player player, Item item)
		{
			return true;
		}

		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			return true;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (Main.netMode == NetmodeID.Server)
			{
				return;
			}

			if (NPC.life <= 0)
			{
				var entitySource = NPC.GetSource_Death();

				for (int i = 0; i < 1; i++)
				{
					Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("ChocolateBunnyGore1").Type);
					Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("ChocolateBunnyGore2").Type);
				}
			}
		}

		/*public virtual void OnCatchNPC(Player player, Item item)
        {
            item.stack = 1;

            try
            {
                var npcCenter = NPC.Center.ToTileCoordinates();
            }
            catch
            {
                return;
            }
        }*/

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.ZoneOverworldHeight && Main.dayTime && !spawnInfo.Player.ZoneDesert && spawnInfo.Player.InModBiome(ModContent.GetInstance<ConfectionBiomeSurface>()))
			{
				return 1f;
			}
			return 0f;
		}
	}

	internal class ChocolateBunnyItem : ModItem
	{
		public override void SetStaticDefaults() => SacrificeTotal = 5;

		public override void SetDefaults()
		{
			Item.useStyle = 1;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 12;
			Item.height = 12;
			Item.makeNPC = 360;
			Item.noUseGraphic = true;

			Item.makeNPC = (short)ModContent.NPCType<ChocolateBunny>();
		}
	}
}
