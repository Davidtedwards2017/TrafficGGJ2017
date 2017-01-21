// unlit, vertex colour, alpha blended
// cull off
Shader "tk2d/BlendVertexColor_Billboard" 
{
    Properties 
    {
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
        _ScreenOffset ("Screen Offset (xy)", Vector) = (0, 0, 0, 0)
        _WorldOffset ("World Offset (xyz)", Vector) = (0, 0, 0, 0)
        _Cutoff ("Alpha cutoff", Float) = 0.1
        _DepthOffset ("Depth offset", Float) = 0
    }
    
    SubShader
    {
        // note that batching is disabled - that's because this shader modifies vertex positions,
        // and if batching is enabled unity might combine multiple similar sprites with the same
        // texture into one mesh.  if that happens then the billboarding part of this shader won't 
        // work right.
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "DisableBatching"="True"}
        ZWrite On Lighting Off Cull Off Fog { Mode Off } Blend SrcAlpha OneMinusSrcAlpha
        LOD 110
        
        Pass 
        {
            CGPROGRAM
            #pragma vertex vert_vct
            #pragma fragment frag_mult 
            #pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _ScreenOffset;
            float4 _WorldOffset;
            float _Cutoff, _DepthOffset;
            struct vin_vct 
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };
            struct v2f_vct
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                float2 depth : TEXCOORD1;
            };
            struct frag_out {
                fixed4 color : SV_Target;
                float depth : SV_Depth;
            };
            v2f_vct vert_vct(vin_vct v)
            {
                v2f_vct o;
                o.color = v.color;
                o.texcoord = v.texcoord;
                
                float4 normalPos = mul(UNITY_MATRIX_MVP, v.vertex);
                // calculate the depth for the vert using it's un-billboarded z value.
                // this gets the depth buffer value from the linear z value
                // http://answers.unity3d.com/questions/142958/compute-distance-from-depth-value.html
                // (substitute your camera's zNear/zFar if not using the defaults)
                float zNear = 0.3;
                float zFar = 1000.0;
                float a = zFar / (zFar - zNear);
                float b = zFar * zNear / (zNear - zFar);
                // use the x component of a texcoord to send the depth value to the fragment shader
                o.depth.x = a + b / (normalPos.z + _DepthOffset);
                // now get the billboarded pos
                // https://en.wikibooks.org/wiki/Cg_Programming/Unity/Billboards
                float4 billboardPos = mul(UNITY_MATRIX_P, 
                        mul(UNITY_MATRIX_MV, float4(0.0, 0.0, 0.0, 1.0))
                            + float4(v.vertex.x, v.vertex.y, 0.0, 0.0));
                o.vertex = billboardPos;
                
                // now add an offset for any post-billboard fudging that needs to happen (as per request)
                // in world units (independent of the camera transform) or screen pixels 
                o.vertex += mul(UNITY_MATRIX_P, _WorldOffset.xyz);
                o.vertex += float4(_ScreenOffset.xy * (1 / _ScreenParams.xy * 2) * o.vertex.w, 0, 0);
                
                return o;
            }
            frag_out frag_mult(v2f_vct i)
            {
                frag_out fout;
                fout.color = tex2D(_MainTex, i.texcoord) * i.color;
                fout.depth = i.depth.x;
                clip(fout.color.a - _Cutoff);
                return fout;
            }
            
            ENDCG
        }
    }
}