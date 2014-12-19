// �����ܼ�  �i�� �D�{�� �ϥ�  EffectParameter �]�w
float4x4 view;  // �[���x�}
float4x4 projection; // ��v�x�}
float viewportHeight;
texture modelTexture;  // ���z��

// ���˾�
sampler ModelTextureSampler = sampler_state
{
    Texture = <modelTexture>; // �]�w���@�i ���z��
    
    //����z��覡  �Y�p�� �ϥ� �u�ʿz�� 
    MinFilter = Linear;  // Anisotropic; //Point; 
    MagFilter = Linear;  // ��j�� �ϥ� �u�ʿz��
    MipFilter = Linear;  // MipMap �ϥ� �u�ʿz��

    //�w�}�Ҧ� Clamp;  Mirror; Border; Wrap;
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

