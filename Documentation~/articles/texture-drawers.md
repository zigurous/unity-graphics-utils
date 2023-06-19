---
slug: "/manual/texture-drawers"
---

# Texture Drawers

The **Graphics Utils** package includes a base class for drawing textures at runtime. It provides the boilerplate code for creating and drawing new textures programmatically.

Create a new class that inherits from [TextureDrawer](/api/Zigurous.Graphics/TextureDrawer) and override the function `SetPixels(Texture2D)` to complete the implementation.

Since [TextureDrawer](/api/Zigurous.Graphics/TextureDrawer) is a ScriptableObject, you'll want to add the `[CreateAssetMenu]` attribute to your class so you can save an instance of the class as an asset in your project using Unity's asset menu.

```csharp
[CreateAssetMenu]
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

The **Graphics Utils** package comes with a script to quickly render the result of a [TextureDrawer](/api/Zigurous.Graphics/TextureDrawer). This is an optional, but useful script to preview the texture without having to write any other code to manually assign the texture to a material.

Add the [TextureDrawerRenderer](/api/Zigurous.Graphics/TextureDrawerRenderer) script to any game object that contains any type of Renderer component. Assign the texture drawer you want to use and customize any other options you'd like. You can even render the texture in the editor without having to run the game.

<hr/>

## 🏁 Checkerboard

The **Graphics Utils** package includes a script [CheckerboardTextureDrawer](/api/Zigurous.Graphics/CheckerboardTextureDrawer) as a sample implementation. It is, of course, a fully functional script that draws a checkerboard pattern. This can be used however you desire, and there are even a number of customization options available.

To create a new checkerboard pattern texture, use the asset menu `Create > Zigurous > Graphics > Checkerboard Texture Drawer`.
