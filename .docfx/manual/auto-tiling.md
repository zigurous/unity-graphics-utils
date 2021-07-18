# Auto Tiling

One of the most powerful features included in the Graphics Utils package is the ability to auto tile materials based on the object's scale. In doing so, new materials are created that are unique to the object. This makes the workflow of creating materials for tiled objects effortless. Without this feature, you often end up creating dozens of variants of a material just to change the tiling values for different objects.

Add the [AutoTile](xref:Zigurous.Graphics.AutoTile) script to the object you want to tile. The main properties that are usually edited are the `submeshes`. Each element in the array indicates how a submesh of the mesh is tiled, such as which axis the object is tiled on, the unit scale of the object, the texture offset, etc. For example, a plane is usually tiled around the Y+ axis and has a unit scale of 10. See the [Scripting API](xref:Zigurous.Graphics.AutoTile) for more information.

### Cube Tiling

When tiling cubes, the script gives the best results when used with the `Cube-Tiling.mesh` asset instead of Unity's default cube mesh. This mesh asset is split into 3 separate submeshes so you can tile each axis independently from the others. The mesh also has custom UV coordinates so the materials are tiled from the center of each axis.
