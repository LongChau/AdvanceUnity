Shader "Custom/LinePattern"
{
    // chiếu đến đâu vẽ đến đó
    // với hình tròn
    // và có smoothFeather
    // Feather -> trong photoshop
    Properties
    {
        _MyColor("Tint Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _Speed("Speed", Range(0, 1)) = 0.0
        _MainTex("Main Texture", 2D) = "white" {}
        _SinValue("Sin Value", Int) = 1

        _Center("Center", vector) = (0.5, 0.5, 0, 0)
        _Radius("Radius", float) = 1.0

        _SmoothFeather("Feather", Range(0.001, 0.05)) = 0.02

        _BlinkSpeed("BlinkSpeed", float) = 0.0
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
            uniform float2 _Center;
            uniform float _Radius;

            uniform float _SmoothFeather;

            uniform float _BlinkSpeed;

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

            half DrawBlinkCircleAlphaFade(float2 uv, float2 center, float radius, float _feather)
            {
                //float distanceSq = pow(uv.x - center.x, );
                float squareDistance = pow(uv.x - center.x, 2) + pow(uv.y - center.y, 2);
                float squareRadius = pow(radius, 2);
                float fadeValue = abs(sin(_Time.y * _BlinkSpeed));

                // Fade từ hình tròn ra hình vuông
                // return smoothstep(squareRadius, squareRadius - _feather, squareDistance * fadeValue);

                // fade in fade out dần dần
                return smoothstep(squareRadius, squareRadius - _feather, squareDistance) * fadeValue;
            }

            half4 fragFunc(VertexOutput interpolatedVertex) : COLOR
            {
                // // sample texture and return it
                fixed4 col = tex2D(_MainTex, interpolatedVertex.uv);

                // col.a = DrawCircleAlpha(interpolatedVertex.uv, _Center, _Radius);

                col.a = DrawBlinkCircleAlphaFade(interpolatedVertex.uv, _Center, _Radius, _SmoothFeather);

                return col;
            }

            ENDCG
        }
    }
}
