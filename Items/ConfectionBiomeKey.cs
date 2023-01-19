using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TheConfectionRebirth.Items
{
	internal class ConfectionBiomeKey : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 14;
			Item.height = 20;
			Item.maxStack = 9999;
			Item.rare = ItemRarityID.Yellow;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (!NPC.downedPlantBoss)
				return;

			try
			{
				tooltips[tooltips.FindIndex(x => x.Mod is "Terraria" && x.Name.Equals("Tooltip0"))].Text = Language.GetTextValue("LegacyTooltip.59");
			}
			catch
			{
			}
		}
	}
}