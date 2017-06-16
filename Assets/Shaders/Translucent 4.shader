Shader "Translucent 4"
{
	Properties
	{
		
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo", 2D) = "white" {}
		
		_Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5

		_Glossiness("Smoothness", Range(0.0, 1.0)) = 0.5
		_GlossMapScale("Smoothness Factor", Range(0.0, 1.0)) = 1.0
		[Enum(Specular Alpha,0,Albedo Alpha,1)] _SmoothnessTextureChannel ("Smoothness texture channel", Float) = 0
		_Shininess ("Shininess", Range (0.03, 1)) = 0.078125
		_SpecColor("Specular", Color) = (0.2,0.2,0.2)
		_SpecGlossMap("Specular", 2D) = "white" {}
		[ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
		[ToggleOff] _GlossyReflections("Glossy Reflections", Float) = 1.0

		_BumpScale("Scale", Float) = 1.0
		_BumpMap("Normal Map", 2D) = "bump" {}

		_Parallax ("Height Scale", Range (0.005, 0.08)) = 0.02
		_ParallaxMap ("Height Map", 2D) = "black" {}

		_OcclusionStrength("Strength", Range(0.0, 1.0)) = 1.0
		_OcclusionMap("Occlusion", 2D) = "white" {}

		//_EmissionColor("Color", Color) = (0,0,0)
		//_EmissionMap("Emission", 2D) = "white" {}
		
		_DetailMask("Detail Mask", 2D) = "white" {}

		_DetailAlbedoMap("Detail Albedo x2", 2D) = "grey" {}
		_DetailNormalMapScale("Scale", Float) = 1.0
		_DetailNormalMap("Normal Map", 2D) = "bump" {}

		[Enum(UV0,0,UV1,1)] _UVSec ("UV Set for secondary textures", Float) = 0


		// Blending state
		[HideInInspector] _Mode ("__mode", Float) = 0.0
		[HideInInspector] _SrcBlend ("__src", Float) = 1.0
		[HideInInspector] _DstBlend ("__dst", Float) = 0.0
		[HideInInspector] _ZWrite ("__zw", Float) = 1.0
		_Thickness ("Thickness (R)", 2D) = "bump" {}
		_Power ("Subsurface Power", Float) = 1.0
		_Distortion ("Subsurface Distortion", Float) = 0.0
		_Scale ("Subsurface Scale", Float) = 0.5
		_SubColor ("Subsurface Color", Color) = (1.0, 1.0, 1.0, 1.0)
	}

	CGINCLUDE
		#define UNITY_SETUP_BRDF_INPUT SpecularSetup
	ENDCG

	SubShader
	{
		Tags { "RenderType"="Opaque" "PerformanceChecks"="False" }
		LOD 300
	

		// ------------------------------------------------------------------
		//  Base forward pass (directional light, emission, lightmaps, ...)
		Pass
		{
			Name "FORWARD" 
			Tags { "LightMode" = "ForwardBase" }

			Blend One One
			Cull Back
			ZWrite On
			AlphaToMask On
			CGPROGRAM
			//#pragma target 3.0
			//#pragma surface surf Lambert alpha

        	#pragma target 4.0
			// -------------------------------------

			#pragma shader_feature _NORMALMAP
			#pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
			#pragma shader_feature _EMISSION
			#pragma shader_feature _SPECGLOSSMAP
			#pragma shader_feature ___ _DETAIL_MULX2
			#pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
			#pragma shader_feature _ _SPECULARHIGHLIGHTS_OFF
			#pragma shader_feature _ _GLOSSYREFLECTIONS_OFF
			#pragma shader_feature _PARALLAXMAP

			#pragma multi_compile_fwdbase
			#pragma multi_compile_fog

			#pragma vertex vertBase
			#pragma fragment fragBase
			#include "UnityStandardCoreForward.cginc"

			ENDCG
		}
		// ------------------------------------------------------------------
		//  Additive forward pass (one light per pass)
		Pass
		{
			Name "FORWARD_DELTA"
			Tags { "LightMode" = "ForwardAdd" }
			Blend One One
			//ZWrite On
			//Ztest LEqual
			Cull Off
			//AlphaToMask On
			Fog { Color (0,0,0,0) } // in additive pass fog should be black
			//ZWrite On
			//ZTest LEqual

			CGPROGRAM
			#pragma target 3.0

			// -------------------------------------

			#pragma shader_feature _NORMALMAP
			#pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
			#pragma shader_feature _SPECGLOSSMAP
			#pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
			#pragma shader_feature _ _SPECULARHIGHLIGHTS_OFF
			#pragma shader_feature ___ _DETAIL_MULX2
			#pragma shader_feature _PARALLAXMAP

			#pragma multi_compile_fwdadd_fullshadows
			#pragma multi_compile_fog

			#pragma vertex vertAdd
			#pragma fragment fragAdd
			#include "UnityStandardCoreForward.cginc"

			ENDCG
		}
		// ------------------------------------------------------------------
		//  Shadow rendering pass
		Pass {
			Name "ShadowCaster"
			Tags { "LightMode" = "ShadowCaster" }

			ZWrite On ZTest Equal

			CGPROGRAM
			#pragma target 3.0

			// -------------------------------------


			#pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
			#pragma multi_compile_shadowcaster

			#pragma vertex vertShadowCaster
			#pragma fragment fragShadowCaster

			#include "UnityStandardShadow.cginc"

			ENDCG
		}

				Pass
		{
			Name "META" 
			Tags { "LightMode"="Meta" }
			//AlphaToMask On
			Cull Off

			CGPROGRAM
			#pragma vertex vert_meta
			#pragma fragment frag_meta

			#pragma shader_feature _EMISSION
			#pragma shader_feature _SPECGLOSSMAP
			#pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
			#pragma shader_feature ___ _DETAIL_MULX2

			#include "UnityStandardMeta.cginc"
			ENDCG
		}
		Tags { "RenderType"="Transpatent"}
		LOD 200
		Blend One OneMinusSrcAlpha
		ZWrite On
		Ztest Less
		//Cull Off
		//AlphaToMask On
		Cull Back
		//SeparateSpecular On
		//Lighting On
		CGPROGRAM
		#pragma surface surf Translucent
		//#pragma exclude_renderers flash
		//#pragma surface surf Lambert alpha
        //#pragma target 4.0     
            // ZTest Greater               // here the check is for the pixel being greater or closer to the camera, in which
             //Cull Off                  // case the model is behind something, so this pass runs
            // ZWrite On
		sampler2D _MainTex, _BumpMap, _Thickness;
		float _Scale, _Power, _Distortion;
		fixed4 _Color, _SubColor;
		half _Shininess;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = tex.rgb * _Color.rgb;
			o.Alpha = tex2D(_Thickness, IN.uv_MainTex).r*_Color.a;
			o.Alpha = tex.a * _Color.a;
			o.Gloss = _SpecColor.a;
			o.Specular = _Shininess;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
		}

		inline fixed4 LightingTranslucent (SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten)
		{		
			// You can remove these two lines,
			// to save some instructions. They're just
			// here for visual fidelity.
			viewDir = normalize ( viewDir );
			lightDir = normalize ( lightDir );

			// Translucency.
			half3 transLightDir = lightDir + s.Normal * _Distortion;
			float transDot = pow ( max (0, dot ( viewDir, -transLightDir ) ), _Power ) * _Scale;
			fixed3 transLight = (atten * 2) * ( transDot ) * s.Alpha * _SubColor.rgb;
			fixed3 transAlbedo = s.Albedo * _LightColor0.rgb * transLight;

			// Regular BlinnPhong.
			half3 h = normalize (lightDir + viewDir);
			fixed diff = max (0, dot (s.Normal, lightDir));
			float nh = max (0, dot (s.Normal, h));
			float spec = pow (nh, s.Specular*128.0) * s.Gloss;
			fixed3 diffAlbedo = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * _SpecColor.rgb * spec) * (atten * 2);

			// Add the two together.
			fixed4 c;
			c.rgb = diffAlbedo + transAlbedo;
			c.a = _LightColor0.a * _SpecColor.a ;
			return c;
		}

		ENDCG


	}


	FallBack "Bumped Diffuse"
	//CustomEditor "StandardShaderGUI"
}
