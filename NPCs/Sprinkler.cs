using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using TheConfectionRebirth.Biomes;
using TheConfectionRebirth.Items.Banners;

namespace TheConfectionRebirth.NPCs
{

	public class Sprinkler : ModNPC
	{
		private Player? TargetPlayer => NPC.target == -1 ? null : Main.player[NPC.target];

		private int _variationIndex;

		public static Asset<Texture2D>[,] Assets { get; private set; } = new Asset<Texture2D>[0, 0];

		public override void Load()
		{
			Asset<Texture2D> textureAsset = ModContent.Request<Texture2D>(Texture);
			VariationsManager<Sprinkler>.AddVariationRange
			(
				new NPCVariation
				(
					name: "Normal",
					textureAsset
				),
				new NPCVariation
				(
					name: "Corn",
					textureAsset,
					() => false && Main.halloween
				),
				new NPCVariation
				(
					name: "Eye",
					textureAsset,
					() => Main.halloween
				),
				new NPCVariation
				(
					name: "Gift",
					textureAsset,
					() => Main.xMas
				)
			);

			if (Main.dedServ)
				return;

			Assets = new Asset<Texture2D>[VariationsManager<Sprinkler>.VariationsCount, 2];

			for (int i = 0; i < Assets.GetLength(0); i++)
				for (int j = 0; j < 2; j++)
					Assets[i, j] = ModContent.Request<Texture2D>($"TheConfectionRebirth/NPCs/Sprinkler/Sprinkler_{i}_{j}");
		}

		public override void Unload()
		{
			VariationsManager<Sprinkler>.ClearVariations();
			Assets = null!;
		}

		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 2;

			NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}

		public override void SetDefaults()
		{
			NPC.width = 42;
			NPC.height = 30;
			NPC.damage = 70;
			NPC.defense = 22;
			NPC.lifeMax = 120;
			NPC.HitSound = SoundID.NPCHit5;
			NPC.DeathSound = SoundID.NPCDeath7;
			NPC.value = 60f;
			// npc.noGravity = false;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = 0;
			AIType = 0;
			AnimationType = NPCID.BlueSlime;
			Banner = NPC.type;
			BannerItem = ModContent.ItemType<SprinklingBanner>();
			SpawnModBiomes = new int[1] { ModContent.GetInstance<ConfectionBiomeSurface>().Type };
			_variationIndex = -1;
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				new FlavorTextBestiaryInfoElement("Mods.TheConfectionRebirth.Bestiary.Sprinkler")
			});
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			NPC.damage = (int)(NPC.damage * 0.2f);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (NPC.life <= 0)
			{
				int index =
					NPC.NewNPC
					(
						source: NPC.GetSource_FromAI(),
						X: (int)NPC.Center.X,
						Y: (int)(NPC.Center.Y + NPC.height / 2f),
						ModContent.NPCType<Sprinkling>()
					);

				(Main.npc[index].ModNPC as Sprinkling)!.Index = _variationIndex;

				if (Main.netMode == NetmodeID.Server)
					NetMessage.SendData(MessageID.SyncNPC, number: index);
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.InModBiome(ModContent.GetInstance<ConfectionBiomeSurface>())
				&& !spawnInfo.AnyInvasionActive())
			{
				return 1f;
			}
			return 0f;
		}

		public override bool PreAI()
		{
			if (_variationIndex == -1)
			{
				VariationsManager<Sprinkler>.GetRandomVariation(out _variationIndex);

				if (Main.netMode == NetmodeID.Server)
					NetMessage.SendData(MessageID.SyncNPC, number: NPC.whoAmI);
			}

			return true;
		}

		public override void AI()
		{
			NPC.TargetClosest(false);

			if (TargetPlayer == null || !Collision.CanHit(NPC, TargetPlayer) || --NPC.ai[1] > 0f)
				return;

			Shoot();
		}

		private void Shoot()
		{
			if (Main.netMode == NetmodeID.MultiplayerClient)
				return;

			int type = Mod.Find<ModProjectile>("SprinklingBall").Type;
			Vector2 velocity = TargetPlayer!.Center - NPC.Center;
			float magnitude = MathF.Sqrt((velocity.X * velocity.X) + (velocity.Y * velocity.Y));

			if (magnitude > 0f)
				velocity *= 5f / magnitude;

			var proj = Projectile.NewProjectileDirect
			(
				NPC.GetSource_FromAI(),
				NPC.Center,
				velocity,
				type,
				NPC.damage, 
				knockback: 2f
			);
			// Change frame based on current variation
			proj.frame = _variationIndex;

			NPC.ai[1] = 200f;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Texture2D texture;
			Rectangle frame = NPC.frame;
			Vector2 pos = NPC.Center - screenPos;
			pos.Y += NPC.gfxOffY + 4f;

			int index = Utils.Clamp(_variationIndex, 0, 4);

			if (index == 4)
				index = 0;

			int frameYOffset = NPC.frame.Y != 0f ? 2 : 0;
			Texture2D front = Assets[index, 1].Value;
			texture = Assets[index, 0].Value;

			spriteBatch.Draw
			(
				texture,
				position: pos + new Vector2(0f, frameYOffset),
				sourceRectangle: new(0, 0, 42, 24),
				drawColor,
				NPC.rotation,
				origin: frame.Size() * 0.5f,
				NPC.scale,
				effects: 0,
				layerDepth: 0f
			);
			spriteBatch.Draw
			(
				texture: front,
				pos,
				sourceRectangle: frame,
				drawColor,
				NPC.rotation,
				origin: frame.Size() * 0.5f,
				NPC.scale,
				effects: 0,
				layerDepth: 0f
			);

			return false;
		}

		public override void SendExtraAI(BinaryWriter writer)
			=> writer.Write(_variationIndex);

		public override void ReceiveExtraAI(BinaryReader reader)
			=> _variationIndex = reader.ReadInt32();
	}
}