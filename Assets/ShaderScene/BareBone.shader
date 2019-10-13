Shader "Custom/BareBone"
{
    Properties
    {
        _IDNumber("Id number", Int) = 1
        _ScaleNumber("Scale", Float) = 1.0
        _MyVector4("Direction", Vector) = (0.0, 0.0, 0.0, 0.0)
        _MyColor("Tint Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _Speed("Speed", Range(0, 1)) = 0.0
        _MainTex("Main Texture", 2D) = "white" {}
        // _BlendAlpha("_BlendAlpha", UnityEngine.Rendering.BlendMode) = OneMinusSrcAlpha
    }
    SubShader
    {
        Tags 
        {
            "Queue" = "Transparent"
        }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha // lấy alpha nguồn trộn. 

            CGPROGRAM
            #pragma vertex vertFunc
            #pragma fragment fragFunc

            // uniform sử dụng chung giữa các shader
            uniform half4 _MyColor;
            // texture we will sample
            uniform sampler2D _MainTex;
            uniform float4 _MainTex_ST;

            struct VertexInput
            {
                float4 vertex : POSITION;   // Position của cái model.
                float2 uv : TEXCOORD0; // texture coordinate
            };

            struct VertexOutput
            {
                float4 pos : SV_POSITION;   // Space view position. Là position của card đồ họa.
                float2 uv : TEXCOORD0; // texture coordinate
            };

            VertexOutput vertFunc(VertexInput vertexInput)
            {
                VertexOutput vertexOutput;
                vertexOutput.pos = UnityObjectToClipPos(vertexInput.vertex);
                vertexOutput.uv = vertexInput.uv * _MainTex_ST.xy + _MainTex_ST.zw;

                return vertexOutput;
            }

            half4 fragFunc(VertexOutput interpolatedVertex) : COLOR
            {
                // half4 outputColor = _MyColor;
                // outputColor.r = interpolatedVertex.pos.z;

                // // sample texture and return it
                fixed4 col = tex2D(_MainTex, interpolatedVertex.uv);
                col.a = interpolatedVertex.uv.x * _MyColor.a;

                return col;
            }

            ENDCG
        }
    }
}
