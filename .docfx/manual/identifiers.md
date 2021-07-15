# Identifiers

When setting shader properties on materials, it is more efficient to use property ids instead of strings. The Graphics Utils package comes with a static class [Identifier](xref:Zigurous.Graphics.Identifier) with predefined ids for many common shader properties. To learn more, see https://docs.unity3d.com/ScriptReference/Shader.PropertyToID.html.

```csharp
private void Start()
{
    Material material = GetComponent<MeshRenderer>().material;
    material.SetFloat(Identifier.Glossiness, 1.0f);
}
```

### [Shader Property](xref:Zigurous.Graphics.ShaderProperty)

The `ShaderProperty` struct included in the Graphics Utils package automatically creates a property id for a given shader property name. Anywhere you might declare a variable for a custom shader property name use `ShaderProperty` instead. It will still be serialized in the editor as a string, but you can use the id when getting or setting a shader property on a material.

```csharp
public ShaderProperty property = "_Custom";
public float propertyValue;

private void Start()
{
    Material material = GetComponent<MeshRenderer>().material;
    material.SetFloat(property.id, propertyValue);
}
```
