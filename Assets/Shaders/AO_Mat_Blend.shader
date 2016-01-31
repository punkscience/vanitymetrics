//works from all angels

Shader "AO-MatCap-Blender" {
	Properties{
		_MainTex ("Diffuse", 2D) = "white" {}
		_MatCapBlend0 ("MatCap0", 2D) = "black" {}
		_MatCapBlend1 ("MatCap1", 2D) = "black" {}
		_MatCapBlend2 ("MatCap1", 2D) = "black" {}
		_Blender("Blender", Range(0, 1.0)) = 0

	}
	Subshader{
		ZTest LEqual
		Pass{		// one light
			Tags{ "Lightmode" = "ForwardBase"}

			CGPROGRAM
			
			//pragmas
			#pragma vertex vert
			#pragma fragment frag
			#pragma exclude_renderers flash
			#include "UnityCG.cginc"
			
			//user defined variables
			uniform sampler2D _MainTex;
			uniform sampler2D _MatCapBlend0;
			uniform sampler2D _MatCapBlend1;
			uniform sampler2D _MatCapBlend2;
			uniform half _Blender;

				
			//structs
			struct vertexInput{
				half4 vertex : POSITION;
				half3 normal : NORMAL;
				half4 texcoord0 : TEXCOORD0;
				half4 tangent : TANGENT;
			};
			struct vertexOutput{
				half4 pos : SV_POSITION;
				half4 uv0 : TEXCOORD0;
				half4 posWorld : TEXCOORD1;
				half3 normalWorld : TEXCOORD2;
				half3 tangentWorld : TEXCOORD3;
				half3 binormalWorld : TEXCOORD4;
				half4 screenPos : TEXCOORD5;
			};
			
			//vertex funcntion
			vertexOutput vert(vertexInput v){
				vertexOutput o;
				
				o.normalWorld = normalize ( mul( half4( v.normal, 0.0 ), _World2Object ).xyz );
				o.tangentWorld = normalize ( mul( _Object2World, v.tangent).xyz );
				o.binormalWorld = normalize ( cross (o.normalWorld, o.tangentWorld) * v.tangent.w);
				
				o.posWorld = mul(_Object2World, v.vertex);
				o.pos = mul( UNITY_MATRIX_MVP, v.vertex);
				o.screenPos = ComputeScreenPos(o.pos);
				o.uv0 = v.texcoord0;
								
				return o;
			}
			
			//fragment function
			half4 frag(vertexOutput i) : COLOR{
				
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

				half4 color = tex2D( _MainTex, i.uv0.xy);
				half3 normalDirection = normalize( mul(localCoords, local2WorldTranspose));
				half3 viewNormals= normalize(mul((half3x3)UNITY_MATRIX_V, normalDirection));

				//lighting
				half3 matCap0 = tex2D (_MatCapBlend0, viewNormals.xy *0.5 +0.5);
				half3 matCap1 = tex2D (_MatCapBlend1, viewNormals.xy *0.5 +0.5);
				half3 matCap2 = tex2D (_MatCapBlend2, viewNormals.xy *0.5 +0.5);
				half3 matCapResult = 	(matCap0 * clamp((_Blender * -2 +1) , 0 , 1))  +
										(matCap1 * (1 - abs((_Blender -0.5) * 2) ))+ 
										(matCap2 * clamp( ((_Blender -0.5) * 2) , 0 , 1) );


				return half4(color * matCapResult,0.5);

			}		
			ENDCG
		}

	}
	// Fallback "Specular"
}
