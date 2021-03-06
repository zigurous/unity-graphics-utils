# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.3.0] - 2021/11/14

### Added

- Extension method for combining meshes
- Extension methods for saving meshes as assets
- Option to merge submeshes in `CombineChildrenMeshes` script

## [0.2.0] - 2021/07/17

### Added

- Data structure `AnimatedShaderProperty`
- Abstract class `TextureDrawer`
- Behavior `CheckerboardTextureDrawer`
- Behavior `CombineChildrenMeshes`

### Fixed

- `ShaderProperty` ids were not changing when the name was changed in the editor

## [0.1.0] - 2021/07/05

### Added

#### Meshes

- Cube-3
- Cube-6
- Cube-Inverted
- Cube-Tiling

#### Behaviors

- AutoTile
- CubeMesh
- CubeMesh3
- CubeMesh6
- InvertMesh
- SaveMesh

#### Other

- Identifier
- MaterialExtensions
- MeshExtensions
- RenderingMode
- ShaderProperty
- Triangle
