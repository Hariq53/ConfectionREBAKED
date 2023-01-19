using Terraria;
using Terraria.ModLoader;

namespace TheConfectionRebirth.Buffs
{
	internal class GoneBananas : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
		}
	}
}
