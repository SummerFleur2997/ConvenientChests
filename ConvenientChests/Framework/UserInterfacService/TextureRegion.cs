using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;

namespace ConvenientChests.Framework.UserInterfacService;

internal class TextureRegion
{
    public readonly Texture2D Texture;
    public readonly Rectangle Region;
    public readonly bool Zoom;

    public TextureRegion(Texture2D texture, Rectangle region)
        : this(texture, region, false)
    {
    }

    public TextureRegion(Texture2D texture, Rectangle region, bool zoom)
    {
        Texture = texture;
        Region = region;
        Zoom = zoom;
    }

    public int Width => Region.Width * (Zoom ? Game1.pixelZoom : 1);
    public int Height => Region.Height * (Zoom ? Game1.pixelZoom : 1);
}