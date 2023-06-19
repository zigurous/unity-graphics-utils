---
slug: "/manual/custom-meshes"
---

# Custom Meshes

The **Graphics Utils** package includes a few custom cube meshes.

- `Cube-3.mesh`: cube mesh with 3 submeshes (one for each axis)
- `Cube-6.mesh`: cube mesh with 6 submeshes (one for each face)
- `Cube-Inverted.mesh`: cube mesh with inverted normals and triangles (inside-out)
- `Cube-Tiling.mesh` cube mesh designed specifically for [Material Tiling](/manual/material-tiling)

There are also 3 different scripts to generate these cube meshes at runtime:

- [CubeMesh](/api/Zigurous.Graphics/CubeMesh)
- [CubeMesh3](/api/Zigurous.Graphics/CubeMesh3)
- [CubeMesh6](/api/Zigurous.Graphics/CubeMesh6)

<hr/>

## ⭕ Inverting Meshes

Sometimes it is useful to invert a mesh so it renders inside out. This is especially useful for cubes. Inverting a mesh flips the triangles and the normals. The **Graphics Utils** package comes with an [InvertMesh](/api/Zigurous.Graphics/InvertMesh) script that handles this automatically.

You can also manually invert the normals and triangles of a mesh using extension methods:

```csharp
mesh.InvertNormals();
mesh.InvertTriangles();

// Returns the inverted values without changing the actual mesh
Vector3[] normals = mesh.InvertedNormals();
int[] triangles = mesh.InvertedTriangles();
```

<hr/>

## 🔰 Combining Meshes

The **Graphics Utils** package includes a script to combine multiple meshes into a single mesh. This can be used to improve rendering performance, or as a way to create custom meshes and turn them into assets. Add the [CombineChildrenMeshes](/api/Zigurous.Graphics/CombineChildrenMeshes) script to the parent game object of children meshes. The combined mesh will be assigned to the mesh filter of the parent object, and the child game objects will either be destroyed or disabled.

You can also manually combine meshes through an extension method:

```csharp
MeshFilter[] filters = GetComponentsInChildren<MeshFilter>();
Mesh combinedMesh = filters.CombineMesh();
```

<hr/>

## 💾 Saving Meshes

Often when generating meshes at runtime, you may want to save that mesh as an asset for future use so you don't need to regenerate them again. The **Graphics Utils** package comes with a [SaveMesh](/api/Zigurous.Graphics/SaveMesh) script that will save a mesh as an asset at runtime.

You can also manually save a mesh through extension methods:

```csharp
mesh.Save("Custom");
meshFilter.SaveMesh("Custom");
```
