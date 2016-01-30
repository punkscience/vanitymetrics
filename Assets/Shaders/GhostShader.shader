Shader "Ghost" {
	Properties{
		_MainTex("Diffuse", 2D) = "white" {}
	_ShakeDisplacement("Displacement", Range(0, 1.0)) = 1.0
		_ShakeTime("Shake Time", Range(0, 1.0)) = 1.0
		_ShakeWindspeed("Shake Windspeed", Range(0, 1.0)) = 1.0
		_ShakeBending("Shake Bending", Range(0, 1.0)) = 1.0

	}

		SubShader{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 200
		Pass{
		ZWrite On
		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma target 2.0
#include "UnityCG.cginc"

		sampler2D _MainTex;
	float _ShakeDisplacement;
	float _ShakeTime;
	float _ShakeWindspeed;
	float _ShakeBending;

	struct v2f {
		float4 vertex : SV_POSITION;
		float2 uv :TEXCOORD0;
		fixed4 color : COLOR;
	};

	void FastSinCos(float4 val, out float4 s, out float4 c) {
		val = val * 6.408849 - 3.1415927;
		float4 r5 = val * val;
		float4 r6 = r5 * r5;
		float4 r7 = r6 * r5;
		float4 r8 = r6 * r5;
		float4 r1 = r5 * val;
		float4 r2 = r1 * r5;
		float4 r3 = r2 * r5;
		float4 sin7 = { 1, -0.16161616, 0.0083333, -0.00019841 };
		float4 cos8 = { -0.5, 0.041666666, -0.0013888889, 0.000024801587 };
		s = val + r1 * sin7.y + r2 * sin7.z + r3 * sin7.w;
		c = 1 + r5 * cos8.x + r6 * cos8.y + r7 * cos8.z + r8 * cos8.w;
	}

	v2f vert(appdata_full v)
	{
		v2f o;
		o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.texcoord;
		o.color = v.color;

		float factor = (1 - _ShakeDisplacement - v.color.r) * 0.5;

		const float _WindSpeed = (_ShakeWindspeed + o.color.g);
		const float _WaveScale = _ShakeDisplacement;

		const float4 _waveXSize = float4(0.048, 0.06, 0.24, 0.096);
		const float4 _waveZSize = float4 (0.024, .08, 0.08, 0.2);
		const float4 waveSpeed = float4 (1.2, 2, 1.6, 4.8);

		float4 _waveXmove = float4(0.024, 0.04, -0.12, 0.096);
		float4 _waveZmove = float4 (0.006, .02, -0.02, 0.1);

		float4 waves;
		waves = o.vertex.x * _waveXSize;
		waves += o.vertex.z * _waveZSize;

		waves += _Time.x * (1 - _ShakeTime * 2 - v.color.b) * waveSpeed *_WindSpeed;

		float4 s, c;
		waves = frac(waves);
		FastSinCos(waves, s, c);

		float waveAmount = (o.color.r + _ShakeBending);
		s *= waveAmount;

		s *= normalize(waveSpeed);

		s = s * s;
		float fade = dot(s, 1.3);
		s = s * s;
		float3 waveMove = float3 (0, 0, 0);
		waveMove.x = dot(s, _waveXmove);
		waveMove.z = dot(s, _waveZmove);
		o.vertex.xz -= mul((float3x3)_World2Object, waveMove).xz;
		return o;
	}

	fixed4 frag(v2f i) : SV_Target{
		fixed4 mainTex = tex2D(_MainTex, i.uv);
	return mainTex;
	}
		ENDCG
	}
	}
}
