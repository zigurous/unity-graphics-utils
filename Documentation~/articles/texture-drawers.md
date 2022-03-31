---
slug: "/manual/texture-drawers"
---

# Texture Drawers

The **Graphics Utils** package includes a base class for drawing textures at runtime. It provides the boilerplate code for creating and drawing new textures programmatically. Create a new class that inherits from [TextureDrawer](/api/Zigurous.Graphics/TextureDrawer) and override the function `SetPixels(Texture2D)` to complete the implementation.

```csharp
public class CustomTextureDrawer : TextureDrawer
{
    public override void SetPixels(Texture2D texture)
    {
        // Handle setting the pixel colors here...
    }
}
```

<hr/>

## 🖼️ Rendering

The [TextureDrawer](/api/Zigurous.Graphics/TextureDrawer) script will automatically apply your texture to the renderer's main material on the object if a Renderer component is available. This is not required, but often is useful. There are additional customization options available in regards to the rendering, such as which shader property the texture is set to.

<hr/>

## 🏁 Checkerboard

The **Graphics Utils** package includes a script [CheckerboardTextureDrawer](/api/Zigurous.Graphics/CheckerboardTextureDrawer) as a sample implementation. It is, of course, a fully functional script that draws a checkerboard pattern. This can be used however you desire, and there are even a number of customization options available.
