# Auto Tiling

One of the most powerful features included in the Graphics Utils package is the ability to auto tile materials based on the object's scale. In doing so, new materials are created that are unique to the object. This makes the workflow of creating materials for tiled objects effortless. Without this feature, you often end up creating dozens of variants of a material just to change the tiling values for different objects.

The [AutoTile](xref:Zigurous.Graphics.AutoTile) script is intended to be used with the `Cube-Tiling.mesh` asset. This mesh asset is split into 3 separate submeshes so you can tile each axis independently from the others. The mesh also has custom UV coordinates so the materials are tiled from the center of each axis. That said, the script has several properties to customize how you want to tile the object, so it is possible to use your own meshes.
