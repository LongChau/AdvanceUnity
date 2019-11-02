Shader "Custom/LinePattern"
{
    // chiếu đến đâu vẽ đến đó
    Properties
    {
        _MyColor("Tint Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _Speed("Speed", Range(0, 1)) = 0.0
        _MainTex("Main Texture", 2D) = "white" {}
        _SinValue("Sin Value", Int) = 1

        _Start("Start", float) = 1.0
        _Width("Width", float) = 1.0
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

            uniform int _SinValue;
            uniform float _Start;
            uniform float _Width;

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

            half DrawLineAlpha(float2 uv, float start, float end)
            {
                if ((uv.x > start && uv.x < end) && (uv.y > start && uv.y < end))
                // if ((uv.x > start && uv.x < end) || (uv.y > start && uv.y < end))
                    return 1;
                return 0;
            }

            half4 fragFunc(VertexOutput interpolatedVertex) : COLOR
            {
                // // sample texture and return it
                fixed4 col = tex2D(_MainTex, interpolatedVertex.uv);

                col.a = DrawLineAlpha(interpolatedVertex.uv, _Start, _Start + _Width);

                return col;
            }

            ENDCG
        }
    }
}
