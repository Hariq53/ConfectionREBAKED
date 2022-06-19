using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using TheConfectionRebirth.Buffs.NeapoliniteBuffs;

namespace TheConfectionRebirth.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class NeapoliniteHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% Increased Ranged Damage"
                    + "\n4% Increased Ranged Critical Strike Chance");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 6;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<NeapoliniteConeMail>() && legs.type == ModContent.ItemType<NeapoliniteGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Every 10mph you go damage is increased by 4% and critical strike chance is increased by 2% which will last for 5 seconds. This stacks up to 5 times";
            int rank;
            float len = player.velocity.Length();
            if (len >= 11f)
                rank = 4;
            else
            {
                float speed = len / 2.2f;
                rank = (int)(speed - 1);
            }
            if (rank >= 0)
                StackableBuffData.ChocolateCharge.AscendBuff(player, rank, 300);
        }
    }
}
