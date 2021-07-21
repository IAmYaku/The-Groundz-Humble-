# Shaders

	There are four types dissolve effects. 

	TypeA is Unlit Simple Dissolve(PixelDissolve/PixelDissolveSimple).

	TypeB is Lighting Simple Dissolve(PixelDissolve/PixelDissolveSimpleLighting).

	TypeC is Unlit Reveal Dissolve(PixelDissolve/PixelDissolveReveal). 

	TypeD is Lighting Reveal Dissolve(PixelDissolve/PixelDissolveRevealLighting).

	The shader properties of TypeA and TypeB is the same.

	The shdaer properties of TypeC and TypeD is the same.

# Shader Properties

	## TypeA and TypeB

		* Main Texture: Albedo texture
		* Main Color: Color tint albedo texture
		* Main Ratio: How percent albedo color to dissolve

		* Dissolve Noise Texture: Perlin Noise texture
		* Dissolve Color: color of dissolve
		* Dissolve Ratio: How percent albedo color to dissolve
		* Dissolve Direction: direction of dissolve in object space

		* Wind: How wind strongly affect dissolve
		* Speed: Wind speed
		* Start: Dissolve start position
		* End: Dissolve end position
		* Pixel Level: pixelate of dissolve

	## TypeC and TypeD

		* Main Texture: Albedo texture
		* Main Color: Color tint albedo texture
		* Main Ratio: How percent albedo color to dissolve

		* Dissolve Noise Texture: Perlin Noise texture
		* Dissolve Color: color of dissolve
		* Dissolve Level: edge smoothness
		* Dissolve Intensity: How dissolve strongly
		* Dissolve Thickness: How dissolve strongly
		* Dissolve Thickness2: How dissolve strongly
		* Pixel Level: pixelate of dissolve

		* Wind: How wind strongly affect dissolve
		* Speed: Wind speed

# How to get a bloom effect like key image

	* Make sure HDR is on.
	* Give dissolve a hdr color, greater than RGB(1,1,1).
	* Import Post Processing Stack(https://assetstore.unity.com/packages/essentials/post-processing-stack-83912)
	* Setup Post Processing Stack on the camera
	* Turn on Bloom
	* You will get a bloom effect like key image