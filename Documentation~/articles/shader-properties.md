---
slug: "/manual/shader-properties"
---

# Shader Properties

When setting shader properties on materials, it is more efficient to use property ids instead of strings. The **Graphics Utils** package comes with a static class [Identifier](/api/Zigurous.Graphics/Identifier) with predefined ids for common shader properties.

```csharp
private void Start()
{
    Material material = GetComponent<MeshRenderer>().material;
    material.SetFloat(Identifier.Glossiness, 1f);
}
```

<hr/>

## 🔖 Automatic Property Ids

The [ShaderProperty](/api/Zigurous.Graphics/ShaderProperty) struct included in the **Graphics Utils** package automatically creates a property id for a given shader property name. Anywhere you might declare a variable for a custom shader property name use `ShaderProperty` instead. It will still be serialized as a string in the editor, but you can use the id when getting or setting a shader property on a material.

```csharp
public ShaderProperty property = "_Custom";

private void Start()
{
    Material material = GetComponent<MeshRenderer>().material;
    material.SetFloat(property.id, 1f);
}
```

<hr/>

## 🌠 Animated Properties

Sometimes you want to animate a shader property over time. The **Graphics Utils** package includes a few data structures to accomplish this. You can declare any of the following:

- [AnimatedShaderIntProperty](/api/Zigurous.Graphics/AnimatedShaderIntProperty)
- [AnimatedShaderFloatProperty](/api/Zigurous.Graphics/AnimatedShaderFloatProperty)
- [AnimatedShaderColorProperty](/api/Zigurous.Graphics/AnimatedShaderColorProperty)

These structs provide properties to set the animated values (usually done in the editor). At this point your script can call `Animate(material, time)` to evaluate the value at the given time and apply it to the provided material.

```csharp
public AnimatedShaderFloatProperty property;

private void Update()
{
    property.Animate(material, time);
}
```
