#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
float Radius;
float2 Dimensions;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 scale;
    if (Dimensions.x > Dimensions.y)
        scale = float2(1, Dimensions.y / Dimensions.x);
	else
        scale = float2(Dimensions.x / Dimensions.y, 1);
    float distance = length((input.TextureCoordinates - float2(0.5, 0.5)) * scale);
    float fadeEnd = (1 + Radius) * length(scale) * input.Color.a / 2;
    float fadeStart = fadeEnd - Radius;
	if(distance < fadeStart)
    {
        return tex2D(SpriteTextureSampler, input.TextureCoordinates);
	}
    else if (distance < fadeEnd)
    {
        float ratio = (distance - fadeStart) / (fadeEnd - fadeStart);
        return lerp(tex2D(SpriteTextureSampler, input.TextureCoordinates), float4(input.Color.rgb, 1), ratio);

    }
	else
        return float4(input.Color.rgb, 1);
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};