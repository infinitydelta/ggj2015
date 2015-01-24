Shader "Custom/BRDF" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Ramp2D ("BRDF Ramp", 2D) = "gray" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Ramp
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Ramp2D;

		struct Input {
			float2 uv_MainTex;
		};
		
		half4 LightingRamp(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten){
			float NormDotLight = dot(s.Normal, lightDir);
			float NormDotView = dot(s.Normal, viewDir);
			
			float diff = ((NormDotLight * .5) + .5)*(atten);
			float2 brdfUV = float2(NormDotView, diff);
			float3 BRDF = tex2D(_Ramp2D, brdfUV.xy).rgb*(atten);
			
			float4 c;
			c.rgb = BRDF;
			c.a = s.Alpha;
			return c;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = float4(.5,.5,.5,1);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "VertexLit"
}
