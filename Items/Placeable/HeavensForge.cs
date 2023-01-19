using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheConfectionRebirth.Items.Weapons;
using TheConfectionRebirth.Tiles;

namespace TheConfectionRebirth.Items.Placeable
{
	public class HeavensForge : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 50;
			Item.height = 26;
			Item.maxStack = 9999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 150;
			Item.createTile = ModContent.TileType<Tiles.HeavensForgeTile>();
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heavenly Forge");
			Tooltip.SetDefault("Allows you to convert hallowed materials into their confection alternatives and vice versa\n" +
				"'A forge created from the both the lands of rainbows and candy'");
			SacrificeTotal = 1;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.CrystalShard, 10)
				.AddIngredient(ItemID.PearlstoneBlock, 30)
				.AddIngredient(ItemID.SoulofLight, 8)
				.AddTile(TileID.DemonAltar)
				.Register();
			CreateRecipe()
				.AddIngredient<Saccharite>(10)
				.AddIngredient<Creamstone>(30)
				.AddIngredient<SoulofDelight>(8)
				.AddTile(TileID.DemonAltar)
				.Register();

			AddAndReplace<Saccharite>(ItemID.CrystalShard);
			AddAndReplace<Sprinkles>(ItemID.PixieDust);
			AddAndReplace<SoulofDelight>(ItemID.SoulofLight);
			AddAndReplace<KeyofDelight>(ItemID.LightKey);
			AddAndReplace<CookieDough>(ItemID.UnicornHorn);
			AddAndReplace<NeapoliniteBar>(ItemID.HallowedBar);
			AddAndReplace<HallowedOre, NeapoliniteOre>();
			AddAndReplace<HallowedBrick, NeapoliniteBrick>();
			AddAndReplace<HallowedBrickWall, NeapoliniteBrickWall>();
			AddAndReplace<ConfectionBiomeKey>(ItemID.HallowedKey);
			AddAndReplace<SherbetBricks>(ItemID.RainbowBrick);
			AddAndReplace<SherbetWall>(ItemID.RainbowBrickWall);
			AddAndReplace<SherbetTorch>(ItemID.RainbowTorch);
			AddAndReplace<CreamWood>(ItemID.Pearlwood);
			AddAndReplace<Cakekite>(ItemID.Prismite);
			AddAndReplace<CookieCarp>(ItemID.PrincessFish);
			AddAndReplace<GrandSlammer>(ItemID.Pwnhammer);
			AddAndReplace<Creamstone>(ItemID.PearlstoneBlock);
			AddAndReplace<CreamBeans>(ItemID.HallowedSeeds);
			AddAndReplace<Creamsand>(ItemID.PearlsandBlock);
			AddAndReplace<Creamsandstone>(ItemID.HallowSandstone);
			AddAndReplace<HardenedCreamsand>(ItemID.HallowHardenedSand);
			AddAndReplace<OrangeIce>(ItemID.PinkIceBlock);
		}

		private static void AddAndReplace<TConf>(int hall) where TConf : ModItem
		{
			Recipe recipe = Recipe.Create(hall);
			recipe.AddIngredient(ContentInstance<TConf>.Instance.Type);
			recipe.AddTile(ModContent.TileType<HeavensForgeTile>());
			recipe.Register();
			recipe = Recipe.Create(ContentInstance<TConf>.Instance.Type);
			recipe.AddIngredient(hall);
			recipe.AddTile(ModContent.TileType<HeavensForgeTile>());
			recipe.Register();
		}

		private static void AddAndReplace<THall, TConf>() where TConf : ModItem where THall : ModItem
		{
			int ht = ContentInstance<THall>.Instance.Type;
			int ct = ContentInstance<TConf>.Instance.Type;

			Recipe recipe = Recipe.Create(ht);
			recipe.AddIngredient(ct);
			recipe.AddTile(ModContent.TileType<HeavensForgeTile>());
			recipe.Register();
			recipe = Recipe.Create(ct);
			recipe.AddIngredient(ht);
			recipe.AddTile(ModContent.TileType<HeavensForgeTile>());
			recipe.Register();
		}
	}
}