using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheConfectionRebirth.Items.Archived
{
	public class TrueDeathsRaze : ModItem
	{
		public override void SetStaticDefaults()
		{
			Terraria.ID.ItemID.Sets.Deprecated[Type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 85;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 15;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 500000;
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item1;
			//Item.shoot = Mod.Find<ModProjectile>("TrueIchorBolt").Type;
			Item.shootSpeed = 10f;
		}
	}
}