Shader "VertExmotion/Bumped Diffuse 2 sided" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_BumpMap ("Normalmap", 2D) = "bump" {}
}

SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 300
	Cull Off

CGPROGRAM
#pragma target 3.0
#pragma surface surf Lambert vertex:vert addshadow
#include "../VertExmotion.cginc"
void vert (inout appdata_full v) {VertExmotion( v );}

sampler2D _MainTex;
sampler2D _BumpMap;
fixed4 _Color;

struct Input {
	float2 uv_MainTex;
	float2 uv_BumpMap;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	o.Albedo = c.rgb;
	o.Alpha = c.a;
	o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
}
ENDCG  
}

FallBack "Diffuse"
}
