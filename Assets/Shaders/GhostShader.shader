Shader "Ghost" {
	Properties{
		_MainTex("Diffuse", 2D) = "white" {}
		_MatCap("MatCap", 2D) = "black" {}
		_ShakeDisplacement("Displacement", Range(0, 1.0)) = 1.0
		_ShakeTime("Shake Time", Range(0, 1.0)) = 1.0
		_ShakeWindspeed("Shake Windspeed", Range(0, 1.0)) = 1.0
		_ShakeBending("Shake Bending", Range(0, 1.0)) = 1.0

	}

		SubShader{
			ZTest LEqual
		Tags{ "Lightmode" = "ForwardBase" }
		LOD 200
		Pass{
		ZWrite On

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma target 2.0
#include "UnityCG.cginc"

	uniform sampler2D _MainTex;
	uniform sampler2D _MatCap;

	float _ShakeDisplacement;
	float _ShakeTime;
	float _ShakeWindspeed;
	float _ShakeBending;

	struct vertexInput {
		half4 vertex : POSITION;
		half3 normal : NORMAL;
		half4 texcoord : TEXCOORD0;
		half4 tangent : TANGENT;
		fixed4 color : COLOR;
	};

	struct vertexOutput {
		half4 vertex : SV_POSITION;
		half4 uv : TEXCOORD0;
		half4 posWorld : TEXCOORD1;
		half3 normalWorld : TEXCOORD2;
		half3 tangentWorld : TEXCOORD3;
		half3 binormalWorld : TEXCOORD4;
		half4 screenPos : TEXCOORD5;
		fixed4 color : COLOR;
	};

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

	vertexOutput vert(vertexInput v)
	{
		vertexOutput o;
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

		//matcap stuff
		o.normalWorld = normalize ( mul( half4( v.normal, 0.0 ), _World2Object ).xyz );
		o.tangentWorld = normalize ( mul( _Object2World, v.tangent).xyz );
		o.binormalWorld = normalize ( cross (o.normalWorld, o.tangentWorld) * v.tangent.w);
		
		//o.posWorld = mul(_Object2World, v.vertex);
		//o.vertex = mul( UNITY_MATRIX_MVP, v.vertex);
		o.screenPos = ComputeScreenPos(o.vertex);
		o.uv = v.texcoord;

		return o;
	}

	fixed4 frag(vertexOutput i) : COLOR{
		half3 viewDirection = normalize ( _WorldSpaceCameraPos.xyz - i.posWorld.xyz );
		
		//UnpackNormal function
		half3 localCoords = half3(1- half2(1.0,1.0), 0.0);
		localCoords.z = 2;
		
		//Convert Normals to ViewSpace
		half3x3 local2WorldTranspose = half3x3(
			i.normalWorld,
			i.binormalWorld,
			i.normalWorld
		);

		half4 color = tex2D( _MainTex, i.uv.xy);
		half3 normalDirection = normalize( mul(localCoords, local2WorldTranspose));
		half3 viewNormals= normalize(mul((half3x3)UNITY_MATRIX_V, normalDirection));

		//lighting
		half3 matCap = tex2D (_MatCap, viewNormals.xy *0.5 +0.5);

		return half4(color *matCap,0.5);
	}
		ENDCG
	}
	}
}
