using System;
using Microsoft.Xna.Framework;

namespace ConvenientChests.Framework.UserInterfacService;

/// <summary>
/// A simple clickable widget.
/// </summary>
internal abstract class Button : Widget
{
    public event Action OnPress;

    public override bool ReceiveLeftClick(Point point)
    {
        OnPress?.Invoke();
        return true;
    }
}