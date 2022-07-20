using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace TheConfectionRebirth.Backgrounds
{
    public class ConfectionSnowSurfaceBackgroundStyle : ModSurfaceBackgroundStyle, IBackground
    {
        // Use this to keep far Backgrounds like the mountains.
        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            for (int i = 0; i < fades.Length; i++)
            {
                if (i == Slot)
                {
                    fades[i] += transitionSpeed;
                    if (fades[i] > 1f)
                    {
                        fades[i] = 1f;
                    }
                }
                else
                {
                    fades[i] -= transitionSpeed;
                    if (fades[i] < 0f)
                    {
                        fades[i] = 0f;
                    }
                }
            }
        }

        public override int ChooseFarTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot("TheConfectionRebirth/Backgrounds/ConfectionSnowSurfaceFar");
        }

        public override int ChooseMiddleTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot("TheConfectionRebirth/Backgrounds/ConfectionSnowSurfaceMid0");
        }

        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return BackgroundTextureLoader.GetBackgroundSlot("TheConfectionRebirth/Backgrounds/ConfectionSnowSurfaceClose");
        }

		public Asset<Texture2D> GetFarTexture(int i)
		{
			throw new System.NotImplementedException();
		}

		public Asset<Texture2D> GetCloseTexture(int i)
		{
			throw new System.NotImplementedException();
		}

		public Asset<Texture2D> GetMidTexture(int i)
		{
			throw new System.NotImplementedException();
		}

		public Asset<Texture2D> GetUltraFarTexture(int i)
		{
			throw new System.NotImplementedException();
		}
	}
}