## Penumbra UWP

This is my fork of the original penumbra uwp project by Nixxs:
https://github.com/Nixxs/PenumbraUWP

## My 2 cents (status)
- Penumbra tuned on(to) Monigame.Framwork v3.7.xx.xx
- HelloPenumbraUWP sample project research and minimal "re-dev"
- Platformer2D sample project init. 


The aim of this for is to create a build of penumbra that will work with MonoGame UWP projects. Instructions for adding to your UWP project are below in the "Using Penumbra UWP" section.

Otherwise, here is a video of how to use this fork: http://youtu.be/HE3Xmy3a5Ok

## What is this sorcery?

Penumbra allows users to easily add 2D lighting with shadowing effects to their games.

> Note that this project is no longer in development. I do try to fix any bugs though!



### Building source and samples

The following is required to successfully compile the Penumbra MonoGame solution:

- Visual studio 2015+ (due to C# 6 syntax)
- MonoGame 3.6+
- [DirectX End-User Runtimes (June 2010)](http://www.microsoft.com/en-us/download/details.aspx?id=8109) (to compile effect shaders)

### Using Penumbra UWP

This is a fork of the original Penumbra project by discosultan, the original project only supported WindowsDX and OpenGL monogame projects. This fork only supports UWP MonoGame projects, so if you want the WindowsDX or OpenGL versions please go to the original project:

https://github.com/discosultan/penumbra

To get started: 
- clone this project and build it using visual studio, this will build the required files:
    - Monogame.Penumbra.UWP.dll
    - Content\PenumbraHull.fx
    - Content\PenumbraLight.fx
    - Content\PenumbraShadow.fx
    - Content\PenumbraTexture.fx
    - Content\Macros.fxh
- In your MonoGame UWP project, add a reference to the built Monogame.Penumbra.UWP.dll you just built
- Next, open up your content pipeline tool for your MonoGame UWP project. You need to add in Penumbra's fx files and the Macros.fxh file
    - Edit -> Add -> Existing Item
    - Select all the fx files and the Macros.fxh file you built earlier. When prompted choose to make a copy of the files.
    - Exclude the Macros.fxh file from the content pipeline tool (don't delete it just exclude it). This is required by the fx files but it doesn't need to be built by the content pipeline tool.
    - Run a build in the content pipeline tool to make sure everything is setup correctly
- Start using Penumbra

Here is a video of how to use this fork: http://youtu.be/HE3Xmy3a5Ok


In the game constructor, create the Penumbra component and add to components:
```cs
PenumbraComponent penumbra;

public Game1()
{
  // ...
  penumbra = new PenumbraComponent(this);
  Components.Add(penumbra);
}
```

In the game's `Draw` method, make sure to call `BeginDraw` before any other drawing takes place:

```cs
protected override void Draw(GameTime gameTime)
{
  penumbra.BeginDraw();
  GraphicsDevice.Clear(Color.CornflowerBlue);
  // Rest of the drawing calls to be affected by Penumbra ...
}
...
```

This will swap the render target to a custom texture so that the generated lightmap can be blended atop of it once `PenumbraComponent` is drawn.

By default, Penumbra operates in the same coordinate space as `SpriteBatch`. Custom coordinate space can be configured by setting:

```cs
penumbra.SpriteBatchTransformEnabled = false;
```

 Custom transform matrix is set through the `Transform` property.

### Working with lights

Penumbra supports three types of lights: `PointLight`, `Spotlight`, `TexturedLight`

![PointLight](Documentation/PointLight.png)
![Spotlight](Documentation/Spotlight.png)
![TexturedLight](Documentation/TexturedLight.png)

While `PointLight` and `Spotlight` are generated on the shader, `TexturedLight` allows for more customization by requiring a custom texture used for lighting.

Lights provide three types of shadowing schemes: `ShadowType.Solid`, `ShadowType.Occluded`, `ShadowType.Illuminated`

![Solid](Documentation/Solid.png)
![Occluded](Documentation/Occluded.png)
![Illuminated](Documentation/Illuminated.png)

To add a light:

```cs
penumbra.Lights.Add(light);
```

### Working with shadow hulls

Hulls are polygons from which shadows are cast. They are usually created using the same geometry as the scene and can be ordered both clockwise or counter-clockwise. Hull points can be manipulated through the `hull.Points` property.

For a hull to be valid and included in the shadow mask generation, it must conform to the following rules:

- Contain at least 3 points
- Points must form a simple polygon (polygon where no two edges intersect)

Hull validity can be checked through the `hull.Valid` property.

To add a hull:

```cs
penumbra.Hulls.Add(hull);
```

## Assemblies

- **MonoGame.Penumbra**: The core project for the lighting system.

## Samples

- **HelloPenumbra**: Simple sample which sets up bare basics of Penumbra with a single light source and shadow hull.
- **Platformer2D**: Penumbra lighting applied to [MonoGame Platformer2D samples game](https://github.com/MonoGame/MonoGame.Samples). Draft.


## ..
As is. No support. RnD only. DIY.

## .
[m][e] 2024

