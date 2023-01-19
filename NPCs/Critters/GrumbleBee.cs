using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using TheConfectionRebirth.Biomes;
using TheConfectionRebirth.Dusts;

namespace TheConfectionRebirth.NPCs.Critters
{
	internal class GrumbleBee : ModNPC
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.GoldButterfly];
			Main.npcCatchable[NPC.type] = true;
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, new(0)
			{
				Position = new(0, 6f),
				Velocity = 0.05f
			});
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Butterfly);
			NPC.catchItem = (short)ModContent.ItemType<GrumbleBeeItem>();
			NPC.aiStyle = 65;
			NPC.friendly = true;
			AIType = NPCID.GoldButterfly;
			AnimationType = NPCID.GoldButterfly;
			SpawnModBiomes = new int[1] { ModContent.GetInstance<ConfectionBiomeSurface>().Type };
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				new FlavorTextBestiaryInfoElement("Mods.TheConfectionRebirth.Bestiary.GrumbleBee")
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
				for (int i = 0; i < 10; i++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<CritterBlood>());
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

	internal class GrumbleBeeItem : ModItem
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
			Item.bait = 40;

			Item.makeNPC = (short)ModContent.NPCType<GrumbleBee>();
		}
	}
}
