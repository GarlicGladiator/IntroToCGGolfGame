# ContraActivityForBonus

SHADERS for Contra:

-Jason's shaders
Water shader: 
Made to create a foam effect around terrain. The reason for this new water shader is because it creates a more realistic and dynamic water effect. This water has a scrolling texture on it and a wavy effect that makes it look like the waves are actually moving. Having more dynamic water (if kept at a reasonable amplitude and speed) makes the scene feel more alive and interesting to play in than a flat water effect that Contra had. 

Terrain shader: 
Next I made a terrain shader that applies a simple tiling texture that makes things look like it was painted with a brush. This texture is grayscaled so I used vertex colors to make it actually grass and rock colored. I used vertex painting in blender to get the vertex colors. After applying the vertex colors over the texture, I also applied a rocky normal map to make the terrain look less flat. The terrain is still flat to keep the simple blockiness of Contra, but the extra rocky texture enhances the style of the terrain. The rocky texture helps sell that the player is standing on terrain and for areas where terrain overlap, there is some contrast between the grass and the rocky wall behind the player in terms of texture (adding a sense of depth). The way the rocky texture is by using vertex colors as a mask. Because I knew the green areas should be less rocky, I took the green channel of vertex colors and masked the rocky normal map and cracks. this helps to save on space as well because its reusing the data from vertex colors. 
