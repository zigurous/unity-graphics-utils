# Shader Properties

When setting shader properties on materials, it is more efficient to use property ids instead of strings. The Graphics Utils package comes with a static class [Identifier](xref:Zigurous.Graphics.Identifier) with predefined ids for common shader properties.

```csharp
private void Start()
{
    Material material = GetComponent<MeshRenderer>().material;
    material.SetFloat(Identifier.Glossiness, 1.0f);
}
```

### Automatic Property Ids

The [ShaderProperty](xref:Zigurous.Graphics.ShaderProperty) struct included in the Graphics Utils package automatically creates a property id for a given shader property name. Anywhere you might declare a variable for a custom shader property name use `ShaderProperty` instead. It will still be serialized as a string in the editor, but you can use the id when getting or setting a shader property on a material.

To learn more, see https://docs.unity3d.com/ScriptReference/Shader.PropertyToID.html.

```csharp
public ShaderProperty property = "_Custom";
public float propertyValue;

private void Start()
{
    Material material = GetComponent<MeshRenderer>().material;
    material.SetFloat(property.id, propertyValue);
}
```

### Animated Properties

Sometimes you want to animate a shader property over time. The Graphics Utils package includes a few data structures to accomplish this. You can declare any one of the following in a script:

- [AnimatedShaderIntProperty](xref:Zigurous.Graphics.AnimatedShaderIntProperty)
- [AnimatedShaderFloatProperty](xref:Zigurous.Graphics.AnimatedShaderFloatProperty)
- [AnimatedShaderColorProperty](xref:Zigurous.Graphics.AnimatedShaderColorProperty)

The structs provide a `valueOverTime` or `colorOverTime` variable that you can use to set the animated values. At this point your script can call `Animate(material, time)` to evaluate the value at the given time and apply it to the provided material.
