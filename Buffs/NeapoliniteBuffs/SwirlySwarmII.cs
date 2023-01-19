using Terraria;
using Terraria.ModLoader;

namespace TheConfectionRebirth.Buffs.NeapoliniteBuffs
{
	public class SwirlySwarmII : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) += 0.20f;
		}
	}
}
