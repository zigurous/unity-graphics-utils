---
slug: "/manual/procedural-generation"
---

# Procedural Generation

The **Graphics Utils** package provides a utility class to generate procedural meshes at runtime. For example, use the [MeshGenerator](/api/Zigurous.Graphics/MeshGenerator) class to create a new procedural mesh that forms a grid of points.

```csharp
Mesh mesh = MeshGenerator.Create(64, 64);
```

This is a useful starting point to create more complex meshes, such as procedural terrain. The [MeshGenerator](/api/Zigurous.Graphics/MeshGenerator) class allows you to provide your own custom vertex generation function to create these more complex meshes.

```csharp
void GenerateTerrain()
{
    Mesh terrain = MeshGenerator.Create(64, 64, VertexGenerator);
}

void VertexGenerator(int x, int y, float u, float v)
{
    // sample terrain height using a noise function
    float height = Mathf.PerlinNoise(x, y);
    return new Vector3(x, height, y);
}
```

<hr/>

## 💎 Triangulation

Sometimes it is useful to split a polygon into triangles in order to generate a custom mesh for the polygon. The [Triangulator](/api/Zigurous.Graphics/Triangulator) class provides this utility. Pass in the points that form the polygon, and the indices of the triangles will be returned.

```csharp
Vector2[] polygon; // array of points that form a polygon
int[] triangles = Triangulator.Triangulate(polygon);
```
