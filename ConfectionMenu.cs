using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using TheConfectionRebirth.Backgrounds.MenuBackgrounds;

namespace TheConfectionRebirth
{
	public class ConfectionMenu : ModMenu
	{
		private const string menuAssetPath = "TheConfectionRebirth/Assets";

		public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>($"{menuAssetPath}/Logo");

		public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Confection");

		public override ModSurfaceBackgroundStyle MenuBackgroundStyle
		{
			get
			{
				if (Main.dayTime)
				{
					return ModContent.GetInstance<ConfectionMenuBackground>();
				}
				return ModContent.GetInstance<ConfectionMenuBackgroundNight>();
			}
		}

		public override string DisplayName => "Confection Menu";
	}
}
