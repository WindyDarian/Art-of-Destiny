// 全域變數  可由 主程式 使用  EffectParameter 設定
float4x4 view;  // 觀測矩陣
float4x4 projection; // 投影矩陣
float viewportHeight;
texture modelTexture;  // 紋理圖

// 取樣器
sampler ModelTextureSampler = sampler_state
{
    Texture = <modelTexture>; // 設定那一張 紋理圖
    
    //材質篩選方式  縮小時 使用 線性篩選 
    MinFilter = Linear;  // Anisotropic; //Point; 
    MagFilter = Linear;  // 放大時 使用 線性篩選
    MipFilter = Linear;  // MipMap 使用 線性篩選

    //定址模式 Clamp;  Mirror; Border; Wrap;
    AddressU = mirror;
    AddressV = mirror;
};


struct VertexShaderInput 
{
  float4 Position : POSITION;
  float4 Color    : COLOR0;
  float1 Size     : PSIZE;
};

struct VertexShaderOutput
{
    float4 Position	: POSITION0;
    float1 Size 	: PSIZE;
    float4 Color    : COLOR0;
};

struct PixelShaderInput
{
    float4 Color: COLOR;
	float2 TexCoord : TEXCOORD0;
};

//VertexShaderOutput VertexShader(VertexShaderInput input)
VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
     
	float4x4 vp = mul(view, projection);
	
	output.Position = mul(input.Position, vp);
    
    output.Size = input.Size * projection._m11 / output.Position.w * viewportHeight / 2;
    
    output.Color = input.Color;
    
    return output;
}

//float4 PixelShader(PixelShaderInput input): COLOR
float4 PixelShaderFunction(PixelShaderInput input): COLOR
{   
    float4 textureColor = tex2D(ModelTextureSampler, input.TexCoord);
    float4 outputColor;
    outputColor = input.Color * textureColor;
    return outputColor;
}


technique PointSprites
{
	pass Pass0
	{   
		//PointSpriteEnable = true;
		VertexShader = compile vs_2_0 VertexShaderFunction();
		PixelShader  = compile ps_2_0 PixelShaderFunction();
	}
}

