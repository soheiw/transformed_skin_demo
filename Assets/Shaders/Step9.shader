Shader "Slime/Step9"
{
    Properties {}
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent" // 透過できるようにする
        }

        Pass
        {
            ZWrite On // 深度を書き込む
            Blend SrcAlpha OneMinusSrcAlpha // 透過できるようにする

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct input
            {
                float4 vertex : POSITION; // 頂点座標
            };

            struct v2f
            {
                float4 pos : POSITION1; // ピクセルワールド座標
                float4 vertex : SV_POSITION; // 頂点座標
            };

            struct output
            {
                float4 col : SV_Target; // ピクセル色
                float depth : SV_Depth; // 深度
            };

            v2f vert(const input v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.pos = mul(unity_ObjectToWorld, v.vertex); // ローカル座標をワールド座標に変換
                return o;
            }

            float sphereDistanceFunction(float4 sphere, float3 pos)
            {
                return length(sphere.xyz - pos) - sphere.w;
            }

            inline float getDepth(float3 pos)
            {
                const float4 vpPos = mul(UNITY_MATRIX_VP, float4(pos, 1.0));
                float z = vpPos.z / vpPos.w;
                #if defined(SHADER_API_GLCORE) || \
                    defined(SHADER_API_OPENGL) || \
                    defined(SHADER_API_GLES) || \
                    defined(SHADER_API_GLES3)
                return z * 0.5 + 0.5;
                #else
                return z;
                #endif
            }

            #define MAX_SPHERE_COUNT 256 // 最大の球の個数
            float4 _Spheres[MAX_SPHERE_COUNT]; // 球の座標・半径を格納した配列
            fixed3 _Colors[MAX_SPHERE_COUNT]; // 球の色を格納した配列
            int _SphereCount; // 処理する球の個数

            float smoothMin(float x1, float x2, float k)
            {
                return -log(exp(-k * x1) + exp(-k * x2)) / k;
            }

            float getDistance(float3 pos)
            {
                float dist = 100000;
                for (int i = 0; i < _SphereCount; i++)
                {
                    dist = smoothMin(dist, sphereDistanceFunction(_Spheres[i], pos), 2.5 / _Spheres[i].w); // 球の半径に基づいてk値を計算
                }
                return dist;
            }

            fixed3 getColor(float3 pos)
            {
                fixed3 color = fixed3(0, 0, 0);
                float weight = 0.01;
                float d;
                float t;
                for (int i = 0; i < _SphereCount; i++)
                {
                    d = length(_Spheres[i].xyz - pos) - _Spheres[i].w;
                    const float distinctness = 0.7;
                    float x = clamp(d * distinctness, 0, 1);
                    t = 1.0 - x * x * (3.0 - 2.0 * x);
                    color += t * _Colors[i];
                    weight += t;
                }
                color /= weight;
                return float4(color, 1);
            }

            float3 getNormal(float3 pos)
            {
                float d = 0.0001;
                return normalize(float3(
                    getDistance(pos + float3(d, 0.0, 0.0)) - getDistance(pos + float3(-d, 0.0, 0.0)),
                    getDistance(pos + float3(0.0, d, 0.0)) - getDistance(pos + float3(0.0, -d, 0.0)),
                    getDistance(pos + float3(0.0, 0.0, d)) - getDistance(pos + float3(0.0, 0.0, -d))
                ));
            }

            output frag(const v2f i)
            {
                output o;

                float3 pos = i.pos.xyz;
                const float3 rayDir = normalize(pos - _WorldSpaceCameraPos); 
                const half3 halfDir = normalize(_WorldSpaceLightPos0.xyz - rayDir);

                for (int i = 0; i < 10; i++)
                {
                    float dist = getDistance(pos);

                    if (dist < 0.01)
                    {
                        fixed3 norm = getNormal(pos); 
                        fixed3 baseColor = getColor(pos);

                        const float rimPower = 2;
                        const float rimRate = pow(1 - abs(dot(norm, rayDir)), rimPower);
                        const fixed3 rimColor = fixed3(1.5, 1.5, 1.5);

                        float highlight = dot(norm, halfDir) > 0.99 ? 1 : 0; 
                        fixed3 color = clamp(lerp(baseColor, rimColor, rimRate) + highlight, 0, 1); 
                        float alpha = clamp(lerp(0.2, 4, rimRate) + highlight, 0, 1); 

                        o.col = fixed4(color, alpha); 
                        o.depth = getDepth(pos); 
                        return o;
                    }

                    pos += dist * rayDir;
                }

                o.col = 0;
                o.depth = 0;
                return o;
            }
            ENDCG
        }
    }
}
