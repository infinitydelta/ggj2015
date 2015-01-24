Shader "Custom/Fullbright_Shadow" {
	Properties {
        _Color ("Main Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _MainTex ("Color (RGBA)", 2D) = "white" {}
    }
 
    SubShader {
        Tags {"Queue" = "Geometry" "RenderType" = "Opaque" "LightMode" = "Always" "LightMode" = "ForwardBase"}
       
        Pass {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fwdbase
                #pragma fragmentoption ARB_precision_hint_fastest
               
                #include "UnityCG.cginc"
                #include "AutoLight.cginc"
               
               fixed4 _Color;
               
                struct appdata {
                    float4 vertex   :   POSITION;
                };
 
                struct v2f
                {
                    float4  pos     :   SV_POSITION;
                    float4  color   :   TEXCOORD0;
                    LIGHTING_COORDS(1, 2) 
                };
 
                v2f vert (appdata v)
                {
                    v2f o;
                    o.pos = mul( UNITY_MATRIX_MVP, v.vertex);
                    o.color = _Color;
                    TRANSFER_VERTEX_TO_FRAGMENT(o)
                    return o;
                }
 
                fixed4 frag(v2f i) : COLOR
                {
                   
                    fixed atten = LIGHT_ATTENUATION(i);
                   
                    fixed4 c = i.color;
                    c.rgb *= atten;
                    return c;
                }
            ENDCG
        }
    }
	FallBack "VertexLit"
}
