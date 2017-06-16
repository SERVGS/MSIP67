Shader "Custom/Translucent3" 
{
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		//_Colour ("Transparency (A only)", Color) = (0.5, 0.5, 0.5, 1)
		_BumpMap ("Normal (Normal)", 2D) = "bump" {}
		_Color ("Main Color", Color) = (1,1,1,1)
		_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_Shininess ("Shininess", Range (0.03, 1)) = 0.078125
		_SpecGlossMap("Specular", 2D) = "white" {}
		//_Thickness = Thickness texture (invert normals, bake AO).
		//_Power = "Sharpness" of translucent glow.
		//_Distortion = Subsurface distortion, shifts surface normal, effectively a refractive index.
		//_Scale = Multiplier for translucent glow - should be per-light, really.
		//_SubColor = Subsurface colour.
		_Thickness ("Thickness (R)", 2D) = "bump" {}
		_Power ("Subsurface Power", Float) = 1.0
		_Distortion ("Subsurface Distortion", Float) = 0.0
		_Scale ("Subsurface Scale", Float) = 0.5
		_SubColor ("Subsurface Color", Color) = (1.0, 1.0, 1.0, 1.0)
		//[Enum(Specular Alpha,0,Albedo Alpha,1)] _SmoothnessTextureChannel ("Smoothness texture channel", Float) = 0
	}
	CGINCLUDE
		#define UNITY_SETUP_BRDF_INPUT SpecularSetup
	ENDCG
	SubShader 
	{
		//Tags { "RenderType"="Transpatent" "Queue"="Transpatent" }
		Tags { "RenderType"="Transpatent"}
		LOD 200

		ZWrite On
		Ztest Less
		//AlphaToMask On
		//Cull Back
		//SeparateSpecular On
		Lighting Off
		//Cull Back
		Pass 
		{

		Name "FORWARD" 
			Tags { "LightMode" = "ForwardBase" }

			Blend SrcAlpha OneMinusSrcAlpha
			//ZWrite On
			//ZTest LEqual
			//AlphaToMask On
			
			CGPROGRAM
			//#pragma target 3.0
			//#pragma surface surf Lambert alpha
        	#pragma target 4.0
			// -------------------------------------

			#pragma shader_feature _BumpMap
			#pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
			//#pragma shader_feature _EMISSION
			#pragma shader_feature _SPECGLOSSMAP
			//#pragma shader_feature ___ _DETAIL_MULX2
			#pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
			#pragma shader_feature _ _SPECULARHIGHLIGHTS_ON
			#pragma shader_feature _ _GLOSSYREFLECTIONS_ON
			//#pragma shader_feature _PARALLAXMAP

			#pragma multi_compile_fwdbase
			#pragma multi_compile_fog
			#pragma multi_compile LIGHTMAP_ON
			#pragma multi_compile DIRLIGHTMAP_ON DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
			#pragma multi_compile DYNAMICLIGHTMAP_ON
			#pragma vertex vertBase
			#pragma fragment fragBase
			#include "UnityStandardCoreForward.cginc"

			ENDCG

		}
		Pass
		{
			Name "FORWARD_DELTA"
			Tags { "LightMode" = "ForwardAdd" }
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite On
			//AlphaToMask On
			Cull Back
			Fog { Color (0,0,0,0) } // in additive pass fog should be black
			//ZWrite Off
			ZTest LEqual

			CGPROGRAM
			#pragma target 2.0

			// -------------------------------------

			#pragma shader_feature _BumpMap
			#pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
			//#pragma shader_feature _EMISSION
			#pragma shader_feature _SPECGLOSSMAP
			//#pragma shader_feature ___ _DETAIL_MULX2
			#pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
			#pragma shader_feature _ _SPECULARHIGHLIGHTS_ON
			#pragma shader_feature _ _GLOSSYREFLECTIONS_OFF
			//#pragma shader_feature _PARALLAXMAP

			#pragma multi_compile_fwdbase
			#pragma multi_compile_fog

			#pragma vertex vertBase
			#pragma fragment fragBase
			#include "UnityStandardCoreForward.cginc"

			ENDCG
		}
				
		// ------------------------------------------------------------------
		// Extracts information for lightmapping, GI (emission, albedo, ...)
		// This pass it not used during regular rendering.

		Tags { "RenderType"="Transpatent"}
		LOD 200
		Blend SrcAlpha OneMinusSrcAlpha
		//ZWrite On
		Ztest LEqual
		//AlphaToMask On
		//Cull Back
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
			//o.Alpha = tex.a * _Color.a;
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
}