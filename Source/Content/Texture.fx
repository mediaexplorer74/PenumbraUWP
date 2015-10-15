﻿#include "Macros.fxh"

Texture2D Texture : register(t0);
SamplerState TextureSampler;

struct VertexIn
{
	float2 Position : SV_POSITION0;
	float2 TexCoord : TEXCOORD0;
};

struct VertexOut
{
	float4 Position : SV_POSITION;
	float2 TexCoord : TEXCOORD0;
};

VertexOut VS(VertexIn vin)
{
	VertexOut vout;

	vout.Position = float4(vin.Position.x, vin.Position.y, 0.0, 1.0);
	vout.TexCoord = vin.TexCoord;

	return vout;
}

float4 PS(VertexOut pin) : SV_TARGET
{
	return Texture.Sample(TextureSampler, pin.TexCoord);
}

TECHNIQUE(Main, VS, PS);
