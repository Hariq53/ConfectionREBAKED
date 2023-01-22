using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

/// <summary>
/// Confection Drawcode Shortener
/// </summary>
namespace TheConfectionRebirth.NPCs
{
	public struct DS
	{
		public static Vector2 DrawPos(Vector2 center) => center - Main.screenPosition;

		public static Vector2 DrawOrigin(Texture2D tex) => new(tex.Width / 2, tex.Height / 2);

		public static Rectangle DrawFrame(Texture2D tex, int x = 0, int y = 0)
			=> new(x, y, tex.Width, tex.Height);

		
		public static SpriteEffects FlipTex(int direction, bool left = false)
			=> direction == (left ? -1 : 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
		public static void DrawNPC(NPC NPC, Texture2D texture, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor, bool left = false)
		{
			Vector2 pos = NPC.Center - screenPos;
			pos.Y += NPC.gfxOffY - 6f;
			spriteBatch.Draw
			(
				texture,
				pos,
				sourceRectangle: NPC.frame,
				drawColor,
				rotation: NPC.rotation,
				origin: NPC.frame.Size() * 0.5f,
				scale: NPC.scale,
				effects: FlipTex(NPC.direction, left),
				layerDepth: 0f
			);
		}
	}
	public struct Utilities
	{
		public static int Round(float f) => (int)Math.Round(f);
		public static int LerpRound(float f1, float f2, float scale)
			=> Round(MathHelper.Lerp(f1, f2, scale));

		public static int DamageLiteral(int i)
		{
			float divident = Main.expertMode ? 0.4f : 0.5f;
			return (int)Math.Round(i * divident);
		}

		public static Color LerpColor(Color c1, Color c2, float lerp)
			=> new(LerpRound(c1.R, c2.R, lerp), LerpRound(c1.G, c2.G, lerp), LerpRound(c1.B, c2.B, lerp), LerpRound(c1.A, c2.A, lerp));

		/// <summary>
		/// Helper method to add many entries to an npc's loot table without having to write
		/// npcLoot.Add multiple times
		/// </summary>
		/// <param name="npcLoot"></param>
		/// <param name="entries"></param>
		public static void NPCLootAddRange(NPCLoot npcLoot, params IItemDropRule[] entries)
		{
			foreach (var entry in entries)
				npcLoot.Add(entry);
		}

		public static void SpawnDeathGore(NPC npc) => SpawnDeathGore(npc, 13, 12, 11);

		public static void SpawnDeathGore(NPC npc, Vector2 velocity, params int[] types)
		{
			var entitySource = npc.GetSource_Death();

			foreach (var goreType in types)
			{
				Gore.NewGore
				(
					entitySource,
					npc.position,
					velocity,
					goreType
				);
			}
		}

		public static void SpawnDeathGore(NPC npc, params int[] types)
			=> SpawnDeathGore(npc, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), types);

		public static void SpawnDeathGore(NPC npc, Mod mod, Vector2 velocity, params string[] modGoreNames)
		{
			var entitySource = npc.GetSource_Death();

			foreach (var name in modGoreNames)
			{
				Gore.NewGore
				(
					entitySource,
					npc.position,
					velocity,
					mod.Find<ModGore>(name).Type
				);
			}
		}

		public static void SpawnDeathGore(NPC npc, Mod mod, params string[] modGoreNames)
			=> SpawnDeathGore(npc, mod, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), modGoreNames);
	}

}