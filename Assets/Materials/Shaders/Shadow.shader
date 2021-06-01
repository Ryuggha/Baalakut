// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Shadow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Shadow("Shadow", Vector) = (1, 0, 0, 0.5)
		[NoScaleOffset] _NormalMap("Normals", 2D) = "bump" {}
		[Gamma] _Metallic("Metallic", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				float dist : DISTANCE;
            };

            sampler2D _MainTex;
			sampler2D _NormalMap;
            float4 _MainTex_ST;
			float3 _Shadow;

            v2f vert (appdata v)
            {

                v2f o;

				
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
				o.normal = v.normal;
				//o.normal = tex2D(_NormalMap, i.uv).xyz;

				float4 pos = mul(unity_ObjectToWorld, v.vertex);
				float d = length(pos.xyz - _Shadow.xyz);
				o.dist = d;
				
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
				//i.normal = UnpackNormal(tex2D(_NormalMap, i.uv));
				//i.normal = normalize(i.normal.xzy);
				//i.normal = i.normal.xzy;
				
				float3 normal = i.normal;
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
				

				float dis = i.dist;
				if (dis <=3.5 ){
					col = float4(0, 0, 0, 0);
				}
				else {
					//col = float4(0.4, 0.4, 0.4, 0);
				}
				
				
                return col;
            }
            ENDCG
        }
    }
}

