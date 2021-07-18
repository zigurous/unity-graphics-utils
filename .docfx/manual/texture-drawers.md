# Texture Drawers

The Graphics Utils package includes a base class for drawing textures at runtime. It provides the boilerplate code for creating and drawing new textures programmatically. Create a new class that inherits from [TextureDrawer](xref:Zigurous.Graphics.TextureDrawer) and override the function `SetPixels(Texture2D)` to complete the implementation.

### Rendering

The [TextureDrawer](xref:Zigurous.Graphics.TextureDrawer) script will automatically apply your texture to the renderer's main material on the object if a Renderer component is available. This is not required, but often is useful. There are additional customization options available in regards to the rendering, such as which shader property the texture is set to.

### Checkerboard

The Graphics Utils package includes a script [CheckerboardTextureDrawer](xref:Zigurous.Graphics.CheckerboardTextureDrawer) as a sample implementation. It is, of course, a fully functional script that draws a checkerboard pattern. This can be used however you desire, and there are even a number of customization options available.
