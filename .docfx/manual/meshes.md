# Meshes

The Graphics Utils package comes with a few custom cube meshes.

- `Cube-3.mesh`: cube mesh with 3 submeshes (one for each axis)
- `Cube-6.mesh`: cube mesh with 6 submeshes (one for each face)
- `Cube-Inverted.mesh`: cube mesh with inverted normals and triangles (inside-out)
- `Cube-Tiling.mesh` cube mesh designed specifically for [Auto Tiling](tiling.md)

There are also 3 different scripts to generate cube meshes at runtime:

- [CubeMesh](xref:Zigurous.Graphics.CubeMesh)
- [CubeMesh3](xref:Zigurous.Graphics.CubeMesh3)
- [CubeMesh6](xref:Zigurous.Graphics.CubeMesh6)

### Saving Meshes

Often when generating meshes at runtime, you may want to save that mesh as an asset for future use so you do not need to regenerate them over and over. The Graphics Utils package comes with a [SaveMesh](xref:Zigurous.Graphics.SaveMesh) script that will save a mesh as an asset at runtime.

### Inverting Meshes

Sometimes it is useful to invert a mesh so it renders inside out. This is especially useful for cubes. Inverting a mesh flips the triangles and the normals. The Graphics Utils package comes with an [InvertMesh](xref:Zigurous.Graphics.InvertMesh) script that handles this automatically.

You can also manually invert the normals and triangles of a mesh by using extension methods:

```csharp
mesh.InvertNormals();
mesh.InvertTriangles();

// Returns the inverted values without changing the actual mesh
Vector3[] normals = mesh.InvertedNormals();
int[] triangles = mesh.InvertedTriangles();
```
