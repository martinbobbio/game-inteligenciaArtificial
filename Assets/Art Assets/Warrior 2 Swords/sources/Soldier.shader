Shader "Custom/Soldier" 
{
	Properties {
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_TeamMask ("Team Mask (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_HitAmount ( "Hit amount",Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _TeamMask;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed _HitAmount;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			fixed mask = tex2D (_TeamMask, IN.uv_MainTex).r;
			o.Albedo = lerp(c, c * _Color, mask) + (fixed4(1,0,0,0) * _HitAmount);
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
