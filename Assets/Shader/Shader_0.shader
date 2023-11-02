тя	Shader "Standard" {
Properties {
 _Color ("Color", Color) = (1,1,1,1)
 _MainTex ("Albedo", 2D) = "white" { }
 _Cutoff ("Alpha Cutoff", Range(0,1)) = 0.5
 _Glossiness ("Smoothness", Range(0,1)) = 0.5
[Gamma]  _Metallic ("Metallic", Range(0,1)) = 0
 _MetallicGlossMap ("Metallic", 2D) = "white" { }
 _BumpScale ("Scale", Float) = 1
 _BumpMap ("Normal Map", 2D) = "bump" { }
 _Parallax ("Height Scale", Range(0.005,0.08)) = 0.02
 _ParallaxMap ("Height Map", 2D) = "black" { }
 _OcclusionStrength ("Strength", Range(0,1)) = 1
 _OcclusionMap ("Occlusion", 2D) = "white" { }
 _EmissionColor ("Color", Color) = (0,0,0,1)
 _EmissionMap ("Emission", 2D) = "white" { }
 _DetailMask ("Detail Mask", 2D) = "white" { }
 _DetailAlbedoMap ("Detail Albedo x2", 2D) = "grey" { }
 _DetailNormalMapScale ("Scale", Float) = 1
 _DetailNormalMap ("Normal Map", 2D) = "bump" { }
[Enum(UV0,0,UV1,1)]  _UVSec ("UV Set for secondary textures", Float) = 0
[HideInInspector]  _Mode ("__mode", Float) = 0
[HideInInspector]  _SrcBlend ("__src", Float) = 1
[HideInInspector]  _DstBlend ("__dst", Float) = 0
[HideInInspector]  _ZWrite ("__zw", Float) = 1
}
SubShader { 
 LOD 300
 Tags { "RenderType"="Opaque" "PerformanceChecks"="False" }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardBase" "SHADOWSUPPORT"="true" "RenderType"="Opaque" "PerformanceChecks"="False" }
  ZWrite [_ZWrite]
  Blend [_SrcBlend] [_DstBlend]
  GpuProgramID 19283
Program "vp" {
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 16 [_DetailAlbedoMap_ST]
Vector 15 [_MainTex_ST]
Float 17 [_UVSec]
Vector 10 [_WorldSpaceCameraPos]
Vector 13 [unity_SHBb]
Vector 12 [unity_SHBg]
Vector 11 [unity_SHBr]
Vector 14 [unity_SHC]
"vs_3_0
def c18, 0, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord8 o7.xyz
dp4 o0.x, c0, v0
dp4 o0.y, c1, v0
dp4 o0.z, c2, v0
dp4 o0.w, c3, v0
mad o1.xy, v2, c15, c15.zwzw
abs r0.x, c17.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c16.xyxy, c16
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c10
mov o7.xyz, r0
mul r0.xyz, c8, v1.y
mad r0.xyz, c7, v1.x, r0
mad r0.xyz, c9, v1.z, r0
nrm r1.xyz, r0
mul r0.x, r1.y, r1.y
mad r0.x, r1.x, r1.x, -r0.x
mul r2, r1.yzzx, r1.xyzz
mov o5.xyz, r1
dp4 r1.x, c11, r2
dp4 r1.y, c12, r2
dp4 r1.z, c13, r2
mad o6.xyz, c14, r0.x, r1
mov o3, c18.x
mov o4, c18.x
mov o5.w, c18.x
mov o6.w, c18.x

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "texcoord2" TexCoord2
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 13 [_DetailAlbedoMap_ST]
Vector 12 [_MainTex_ST]
Float 14 [_UVSec]
Vector 10 [_WorldSpaceCameraPos]
Vector 11 [unity_DynamicLightmapST]
"vs_3_0
def c15, 0, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_texcoord2 v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord8 o7.xyz
dp4 o0.x, c0, v0
dp4 o0.y, c1, v0
dp4 o0.z, c2, v0
dp4 o0.w, c3, v0
mad o1.xy, v2, c12, c12.zwzw
abs r0.x, c14.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c13.xyxy, c13
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c10
mov o7.xyz, r0
mul r0.xyz, c8, v1.y
mad r0.xyz, c7, v1.x, r0
mad r0.xyz, c9, v1.z, r0
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul o5.xyz, r0.w, r0
mad o6.zw, v4.xyxy, c11.xyxy, c11
mov o3, c15.x
mov o4, c15.x
mov o5.w, c15.x
mov o6.xy, c15.x

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 18 [_DetailAlbedoMap_ST]
Vector 17 [_MainTex_ST]
Vector 11 [_ProjectionParams]
Vector 12 [_ScreenParams]
Float 19 [_UVSec]
Vector 10 [_WorldSpaceCameraPos]
Vector 15 [unity_SHBb]
Vector 14 [unity_SHBg]
Vector 13 [unity_SHBr]
Vector 16 [unity_SHC]
"vs_3_0
def c20, 0.5, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord6 o7
dcl_texcoord8 o8.xyz
mad o1.xy, v2, c17, c17.zwzw
abs r0.x, c19.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c18.xyxy, c18
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c10
mov o8.xyz, r0
dp4 r0.y, c1, v0
mul r1.x, r0.y, c11.x
mul r1.w, r1.x, c20.x
dp4 r0.x, c0, v0
dp4 r0.w, c3, v0
mul r1.xz, r0.xyww, c20.x
mad o7.xy, r1.z, c12.zwzw, r1.xwzw
mul r1.xyz, c8, v1.y
mad r1.xyz, c7, v1.x, r1
mad r1.xyz, c9, v1.z, r1
nrm r2.xyz, r1
mul r1.x, r2.y, r2.y
mad r1.x, r2.x, r2.x, -r1.x
mul r3, r2.yzzx, r2.xyzz
mov o5.xyz, r2
dp4 r2.x, c13, r3
dp4 r2.y, c14, r3
dp4 r2.z, c15, r3
mad o6.xyz, c16, r1.x, r2
dp4 r0.z, c2, v0
mov o0, r0
mov o7.zw, r0
mov o3, c20.y
mov o4, c20.y
mov o5.w, c20.y
mov o6.w, c20.y

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "texcoord2" TexCoord2
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 15 [_DetailAlbedoMap_ST]
Vector 14 [_MainTex_ST]
Vector 11 [_ProjectionParams]
Vector 12 [_ScreenParams]
Float 16 [_UVSec]
Vector 10 [_WorldSpaceCameraPos]
Vector 13 [unity_DynamicLightmapST]
"vs_3_0
def c17, 0.5, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_texcoord2 v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord6 o7
dcl_texcoord8 o8.xyz
mad o1.xy, v2, c14, c14.zwzw
abs r0.x, c16.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c15.xyxy, c15
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c10
mov o8.xyz, r0
mul r0.xyz, c8, v1.y
mad r0.xyz, c7, v1.x, r0
mad r0.xyz, c9, v1.z, r0
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul o5.xyz, r0.w, r0
dp4 r0.y, c1, v0
mul r1.x, r0.y, c11.x
mul r1.w, r1.x, c17.x
dp4 r0.x, c0, v0
dp4 r0.w, c3, v0
mul r1.xz, r0.xyww, c17.x
mad o7.xy, r1.z, c12.zwzw, r1.xwzw
mad o6.zw, v4.xyxy, c13.xyxy, c13
dp4 r0.z, c2, v0
mov o0, r0
mov o7.zw, r0
mov o3, c17.y
mov o4, c17.y
mov o5.w, c17.y
mov o6.xy, c17.y

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 8 [_Object2World] 3
Matrix 11 [_World2Object] 3
Matrix 4 [glstate_matrix_mvp]
Vector 24 [_DetailAlbedoMap_ST]
Vector 23 [_MainTex_ST]
Float 25 [_UVSec]
Vector 14 [_WorldSpaceCameraPos]
Vector 18 [unity_4LightAtten0]
Vector 15 [unity_4LightPosX0]
Vector 16 [unity_4LightPosY0]
Vector 17 [unity_4LightPosZ0]
Vector 0 [unity_LightColor0]
Vector 1 [unity_LightColor1]
Vector 2 [unity_LightColor2]
Vector 3 [unity_LightColor3]
Vector 21 [unity_SHBb]
Vector 20 [unity_SHBg]
Vector 19 [unity_SHBr]
Vector 22 [unity_SHC]
"vs_3_0
def c26, 0, 1, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord8 o7.xyz
dp4 o0.x, c4, v0
dp4 o0.y, c5, v0
dp4 o0.z, c6, v0
dp4 o0.w, c7, v0
mad o1.xy, v2, c23, c23.zwzw
abs r0.x, c25.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c24.xyxy, c24
dp4 r0.x, c8, v0
dp4 r0.y, c9, v0
dp4 r0.z, c10, v0
add o2.xyz, r0, -c14
add r1, -r0.z, c17
mov o7.xyz, r0
add r2, -r0.x, c15
add r0, -r0.y, c16
mul r3.xyz, c12, v1.y
mad r3.xyz, c11, v1.x, r3
mad r3.xyz, c13, v1.z, r3
nrm r4.xyz, r3
mul r3, r0, r4.y
mul r0, r0, r0
mad r0, r2, r2, r0
mad r2, r2, r4.x, r3
mad r2, r1, r4.z, r2
mad r0, r1, r1, r0
rsq r1.x, r0.x
rsq r1.y, r0.y
rsq r1.z, r0.z
rsq r1.w, r0.w
mov r3.y, c26.y
mad r0, r0, c18, r3.y
mul r1, r1, r2
max r1, r1, c26.x
rcp r2.x, r0.x
rcp r2.y, r0.y
rcp r2.z, r0.z
rcp r2.w, r0.w
mul r0, r1, r2
mul r1.xyz, r0.y, c1
mad r1.xyz, c0, r0.x, r1
mad r0.xyz, c2, r0.z, r1
mad r0.xyz, c3, r0.w, r0
mul r0.w, r4.y, r4.y
mad r0.w, r4.x, r4.x, -r0.w
mul r1, r4.yzzx, r4.xyzz
mov o5.xyz, r4
dp4 r2.x, c19, r1
dp4 r2.y, c20, r1
dp4 r2.z, c21, r1
mad r1.xyz, c22, r0.w, r2
add o6.xyz, r0, r1
mov o3, c26.x
mov o4, c26.x
mov o5.w, c26.x
mov o6.w, c26.x

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "texcoord2" TexCoord2
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 13 [_DetailAlbedoMap_ST]
Vector 12 [_MainTex_ST]
Float 14 [_UVSec]
Vector 10 [_WorldSpaceCameraPos]
Vector 11 [unity_DynamicLightmapST]
"vs_3_0
def c15, 0, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_texcoord2 v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord8 o7.xyz
dp4 o0.x, c0, v0
dp4 o0.y, c1, v0
dp4 o0.z, c2, v0
dp4 o0.w, c3, v0
mad o1.xy, v2, c12, c12.zwzw
abs r0.x, c14.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c13.xyxy, c13
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c10
mov o7.xyz, r0
mul r0.xyz, c8, v1.y
mad r0.xyz, c7, v1.x, r0
mad r0.xyz, c9, v1.z, r0
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul o5.xyz, r0.w, r0
mad o6.zw, v4.xyxy, c11.xyxy, c11
mov o3, c15.x
mov o4, c15.x
mov o5.w, c15.x
mov o6.xy, c15.x

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 8 [_Object2World] 3
Matrix 11 [_World2Object] 3
Matrix 4 [glstate_matrix_mvp]
Vector 26 [_DetailAlbedoMap_ST]
Vector 25 [_MainTex_ST]
Vector 15 [_ProjectionParams]
Vector 16 [_ScreenParams]
Float 27 [_UVSec]
Vector 14 [_WorldSpaceCameraPos]
Vector 20 [unity_4LightAtten0]
Vector 17 [unity_4LightPosX0]
Vector 18 [unity_4LightPosY0]
Vector 19 [unity_4LightPosZ0]
Vector 0 [unity_LightColor0]
Vector 1 [unity_LightColor1]
Vector 2 [unity_LightColor2]
Vector 3 [unity_LightColor3]
Vector 23 [unity_SHBb]
Vector 22 [unity_SHBg]
Vector 21 [unity_SHBr]
Vector 24 [unity_SHC]
"vs_3_0
def c28, 0.5, 0, 1, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord6 o7
dcl_texcoord8 o8.xyz
mad o1.xy, v2, c25, c25.zwzw
abs r0.x, c27.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c26.xyxy, c26
dp4 r0.x, c8, v0
dp4 r0.y, c9, v0
dp4 r0.z, c10, v0
add o2.xyz, r0, -c14
dp4 r1.y, c5, v0
mul r0.w, r1.y, c15.x
mul r2.w, r0.w, c28.x
dp4 r1.x, c4, v0
dp4 r1.w, c7, v0
mul r2.xz, r1.xyww, c28.x
mad o7.xy, r2.z, c16.zwzw, r2.xwzw
add r2, -r0.z, c19
mov o8.xyz, r0
add r3, -r0.x, c17
add r0, -r0.y, c18
mul r4.xyz, c12, v1.y
mad r4.xyz, c11, v1.x, r4
mad r4.xyz, c13, v1.z, r4
nrm r5.xyz, r4
mul r4, r0, r5.y
mul r0, r0, r0
mad r0, r3, r3, r0
mad r3, r3, r5.x, r4
mad r3, r2, r5.z, r3
mad r0, r2, r2, r0
rsq r2.x, r0.x
rsq r2.y, r0.y
rsq r2.z, r0.z
rsq r2.w, r0.w
mov r4.z, c28.z
mad r0, r0, c20, r4.z
mul r2, r2, r3
max r2, r2, c28.y
rcp r3.x, r0.x
rcp r3.y, r0.y
rcp r3.z, r0.z
rcp r3.w, r0.w
mul r0, r2, r3
mul r2.xyz, r0.y, c1
mad r2.xyz, c0, r0.x, r2
mad r0.xyz, c2, r0.z, r2
mad r0.xyz, c3, r0.w, r0
mul r0.w, r5.y, r5.y
mad r0.w, r5.x, r5.x, -r0.w
mul r2, r5.yzzx, r5.xyzz
mov o5.xyz, r5
dp4 r3.x, c21, r2
dp4 r3.y, c22, r2
dp4 r3.z, c23, r2
mad r2.xyz, c24, r0.w, r3
add o6.xyz, r0, r2
dp4 r1.z, c6, v0
mov o0, r1
mov o7.zw, r1
mov o3, c28.y
mov o4, c28.y
mov o5.w, c28.y
mov o6.w, c28.y

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "texcoord2" TexCoord2
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 15 [_DetailAlbedoMap_ST]
Vector 14 [_MainTex_ST]
Vector 11 [_ProjectionParams]
Vector 12 [_ScreenParams]
Float 16 [_UVSec]
Vector 10 [_WorldSpaceCameraPos]
Vector 13 [unity_DynamicLightmapST]
"vs_3_0
def c17, 0.5, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_texcoord2 v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord6 o7
dcl_texcoord8 o8.xyz
mad o1.xy, v2, c14, c14.zwzw
abs r0.x, c16.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c15.xyxy, c15
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c10
mov o8.xyz, r0
mul r0.xyz, c8, v1.y
mad r0.xyz, c7, v1.x, r0
mad r0.xyz, c9, v1.z, r0
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul o5.xyz, r0.w, r0
dp4 r0.y, c1, v0
mul r1.x, r0.y, c11.x
mul r1.w, r1.x, c17.x
dp4 r0.x, c0, v0
dp4 r0.w, c3, v0
mul r1.xz, r0.xyww, c17.x
mad o7.xy, r1.z, c12.zwzw, r1.xwzw
mad o6.zw, v4.xyxy, c13.xyxy, c13
dp4 r0.z, c2, v0
mov o0, r0
mov o7.zw, r0
mov o3, c17.y
mov o4, c17.y
mov o5.w, c17.y
mov o6.xy, c17.y

"
}
}
Program "fp" {
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
Vector 15 [_Color]
Float 17 [_Glossiness]
Vector 13 [_LightColor0]
Float 16 [_Metallic]
Float 18 [_OcclusionStrength]
Vector 0 [_WorldSpaceLightPos0]
Vector 12 [unity_ColorSpaceDielectricSpec]
Vector 14 [unity_LightGammaCorrectionConsts]
Vector 3 [unity_SHAb]
Vector 2 [unity_SHAg]
Vector 1 [unity_SHAr]
Vector 4 [unity_SpecCube0_BoxMax]
Vector 5 [unity_SpecCube0_BoxMin]
Vector 7 [unity_SpecCube0_HDR]
Vector 6 [unity_SpecCube0_ProbePosition]
Vector 8 [unity_SpecCube1_BoxMax]
Vector 9 [unity_SpecCube1_BoxMin]
Vector 11 [unity_SpecCube1_HDR]
Vector 10 [unity_SpecCube1_ProbePosition]
SetTexture 0 [unity_SpecCube0] CUBE 0
SetTexture 1 [unity_SpecCube1] CUBE 1
SetTexture 2 [_MainTex] 2D 2
SetTexture 3 [_OcclusionMap] 2D 3
"ps_3_0
def c19, 7, 0.999989986, 0.00100000005, 31.622776
def c20, 9.99999975e-005, 0.967999995, 0.0299999993, 10
def c21, 0, 1, 0.5, 0.75
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord4_pp v2.xyz
dcl_texcoord5_pp v3.xyz
dcl_texcoord8_pp v4.xyz
dcl_cube s0
dcl_cube s1
dcl_2d s2
dcl_2d s3
nrm_pp r0.xyz, v2
dp3_pp r1.x, v1, v1
rsq_pp r1.x, r1.x
mul_pp r1.yzw, r1.x, v1.xxyz
texld r2, v0, s2
mul_pp r3.xyz, r2, c15
mov r4, c12
mad_pp r2.xyz, c15, r2, -r4
mad_pp r2.xyz, c16.x, r2, r4
mad_pp r2.w, c16.x, -r4.w, r4.w
mul_pp r3.xyz, r2.w, r3
dp3_pp r3.w, r0, c0
max_pp r4.x, r3.w, c21.x
texld_pp r5, v0, s3
mov r6.xyz, c21
add_pp r3.w, r6.y, -c18.x
mad_pp r3.w, r5.y, c18.x, r3.w
mov r0.w, c21.y
dp4_pp r5.x, c1, r0
dp4_pp r5.y, c2, r0
dp4_pp r5.z, c3, r0
add_pp r4.yzw, r5.xxyz, v3.xxyz
dp3 r0.w, r1.yzww, r0
add r0.w, r0.w, r0.w
mad_pp r5.xyz, r0, -r0.w, r1.yzww
if_lt -c6.w, r6.x
nrm_pp r7.xyz, r5
add r8.xyz, c4, -v4
rcp r9.x, r7.x
rcp r9.y, r7.y
rcp r9.z, r7.z
mul_pp r8.xyz, r8, r9
add r10.xyz, c5, -v4
mul_pp r9.xyz, r9, r10
cmp_pp r8.xyz, -r7, r9, r8
min_pp r0.w, r8.y, r8.x
min_pp r5.w, r8.z, r0.w
mov r8.xyz, c5
add r8.xyz, r8, c4
mad r9.xyz, r8, r6.z, -c6
add r9.xyz, r9, v4
mad r7.xyz, r7, r5.w, r9
mad_pp r7.xyz, r8, -c21.z, r7
else
mov_pp r7.xyz, r5
endif
add_pp r0.w, r6.y, -c17.x
pow_pp r5.w, r0.w, c21.w
mul_pp r7.w, r5.w, c19.x
texldl_pp r8, r7, s0
pow_pp r5.w, r8.w, c7.y
mul_pp r5.w, r5.w, c7.x
mul_pp r9.xyz, r8, r5.w
mov r6.w, c5.w
if_lt r6.w, c19.y
if_lt -c10.w, r6.x
nrm_pp r10.xyz, r5
add r11.xyz, c8, -v4
rcp r12.x, r10.x
rcp r12.y, r10.y
rcp r12.z, r10.z
mul_pp r11.xyz, r11, r12
add r13.xyz, c9, -v4
mul_pp r12.xyz, r12, r13
cmp_pp r11.xyz, -r10, r12, r11
min_pp r6.x, r11.y, r11.x
min_pp r8.w, r11.z, r6.x
mov r11.xyz, c8
add r11.xyz, r11, c9
mad r6.xzw, r11.xyyz, r6.z, -c10.xyyz
add r6.xzw, r6, v4.xyyz
mad r6.xzw, r10.xyyz, r8.w, r6
mad_pp r7.xyz, r11, -c21.z, r6.xzww
else
mov_pp r7.xyz, r5
endif
texldl_pp r7, r7, s1
pow_pp r5.x, r7.w, c11.y
mul_pp r5.x, r5.x, c11.x
mul_pp r5.xyz, r7, r5.x
mad r6.xzw, r5.w, r8.xyyz, -r5.xyyz
mad_pp r9.xyz, c5.w, r6.xzww, r5
endif
mul_pp r5.xyz, r3.w, r9
mad_pp r6.xzw, v1.xyyz, -r1.x, c0.xyyz
dp3_pp r1.x, r6.xzww, r6.xzww
add r5.w, -r1.x, c19.z
rsq_pp r1.x, r1.x
cmp_pp r1.x, r5.w, c19.w, r1.x
mul_pp r6.xzw, r1.x, r6
dp3_pp r1.x, r0, r6.xzww
max_pp r5.w, r1.x, c21.x
dp3_pp r0.x, r0, -r1.yzww
max_pp r1.x, r0.x, c21.x
dp3_pp r0.x, c0, r6.xzww
max_pp r1.y, r0.x, c21.x
mul_pp r0.x, r0.w, r0.w
mul_pp r0.y, r0.x, c14.w
mad_pp r0.x, r0.x, -c14.w, r6.y
mad_pp r0.z, r4.x, r0.x, r0.y
mad_pp r0.x, r1.x, r0.x, r0.y
mad r0.x, r0.z, r0.x, c20.x
rcp_pp r0.x, r0.x
add_pp r0.y, -r0.w, c21.y
mad_pp r0.y, r0.y, c20.y, c20.z
log_pp r0.y, r0.y
rcp r0.y, r0.y
mul_pp r0.y, r0.y, c20.w
mul_pp r0.z, r0.y, r0.y
mad_pp r0.y, r0.y, r0.y, c21.y
mul_pp r0.y, r0.y, c14.y
pow_pp r1.z, r5.w, r0.z
mul_pp r0.y, r0.y, r1.z
add_pp r0.z, -r4.x, c21.y
mul_pp r1.z, r0.z, r0.z
mul_pp r1.z, r1.z, r1.z
mul_pp r0.z, r0.z, r1.z
add_pp r1.x, -r1.x, c21.y
mul_pp r1.z, r1.x, r1.x
mul_pp r1.z, r1.z, r1.z
mul_pp r1.x, r1.x, r1.z
mul_pp r1.z, r1.y, r1.y
dp2add_pp r0.w, r1.z, r0.w, -c21.z
mad_pp r0.z, r0.w, r0.z, c21.y
mad_pp r0.w, r0.w, r1.x, c21.y
mul_pp r0.xz, r0.yyww, r0
mul_pp r0.xy, r4.x, r0.xzzw
mul_pp r0.x, r0.x, c14.x
add_pp r0.z, -r2.w, c21.y
add_sat_pp r0.z, r0.z, c17.x
mul_pp r6.xyz, r0.y, c13
mad_pp r4.xyz, r4.yzww, r3.w, r6
mul_pp r6.xyz, r0.x, c13
cmp_pp r0.xyw, r0.x, r6.xyzz, c21.x
add_pp r1.y, -r1.y, c21.y
mul_pp r1.z, r1.y, r1.y
mul_pp r1.z, r1.z, r1.z
mul_pp r1.y, r1.y, r1.z
lrp_pp r6.xyz, r1.y, c21.y, r2
mul_pp r0.xyw, r0, r6.xyzz
mad_pp r0.xyw, r3.xyzz, r4.xyzz, r0
lrp_pp r3.xyz, r1.x, r0.z, r2
mad_pp oC0.xyz, r5, r3, r0.xyww
mov_pp oC0.w, c21.y

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" }
Vector 13 [_Color]
Float 15 [_Glossiness]
Vector 11 [_LightColor0]
Float 14 [_Metallic]
Float 16 [_OcclusionStrength]
Vector 0 [_WorldSpaceLightPos0]
Vector 9 [unity_ColorSpaceDielectricSpec]
Vector 10 [unity_DynamicLightmap_HDR]
Vector 12 [unity_LightGammaCorrectionConsts]
Vector 1 [unity_SpecCube0_BoxMax]
Vector 2 [unity_SpecCube0_BoxMin]
Vector 4 [unity_SpecCube0_HDR]
Vector 3 [unity_SpecCube0_ProbePosition]
Vector 5 [unity_SpecCube1_BoxMax]
Vector 6 [unity_SpecCube1_BoxMin]
Vector 8 [unity_SpecCube1_HDR]
Vector 7 [unity_SpecCube1_ProbePosition]
SetTexture 0 [unity_DynamicLightmap] 2D 0
SetTexture 1 [unity_SpecCube0] CUBE 1
SetTexture 2 [unity_SpecCube1] CUBE 2
SetTexture 3 [_MainTex] 2D 3
SetTexture 4 [_OcclusionMap] 2D 4
"ps_3_0
def c17, 7, 0.999989986, 0.00100000005, 31.622776
def c18, 9.99999975e-005, 0.967999995, 0.0299999993, 10
def c19, 0, 1, 0.5, 0.75
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord4_pp v2.xyz
dcl_texcoord5_pp v3.zw
dcl_texcoord8_pp v4.xyz
dcl_2d s0
dcl_cube s1
dcl_cube s2
dcl_2d s3
dcl_2d s4
nrm_pp r0.xyz, v2
dp3_pp r0.w, v1, v1
rsq_pp r0.w, r0.w
mul_pp r1.xyz, r0.w, v1
texld r2, v0, s3
mul_pp r3.xyz, r2, c13
mov r4, c9
mad_pp r2.xyz, c13, r2, -r4
mad_pp r2.xyz, c14.x, r2, r4
mad_pp r1.w, c14.x, -r4.w, r4.w
mul_pp r3.xyz, r1.w, r3
dp3_pp r2.w, r0, c0
max_pp r3.w, r2.w, c19.x
texld_pp r4, v0, s4
mov r5.xyz, c19
add_pp r2.w, r5.y, -c16.x
mad_pp r2.w, r4.y, c16.x, r2.w
texld_pp r4, v3.zwzw, s0
mul_pp r4.w, r4.w, c10.x
mul_pp r4.xyz, r4, r4.w
log_pp r6.x, r4.x
log_pp r6.y, r4.y
log_pp r6.z, r4.z
mul_pp r4.xyz, r6, c10.y
exp_pp r6.x, r4.x
exp_pp r6.y, r4.y
exp_pp r6.z, r4.z
dp3 r4.x, r1, r0
add r4.x, r4.x, r4.x
mad_pp r4.xyz, r0, -r4.x, r1
if_lt -c3.w, r5.x
nrm_pp r7.xyz, r4
add r8.xyz, c1, -v4
rcp r9.x, r7.x
rcp r9.y, r7.y
rcp r9.z, r7.z
mul_pp r8.xyz, r8, r9
add r10.xyz, c2, -v4
mul_pp r9.xyz, r9, r10
cmp_pp r8.xyz, -r7, r9, r8
min_pp r4.w, r8.y, r8.x
min_pp r5.w, r8.z, r4.w
mov r8.xyz, c2
add r8.xyz, r8, c1
mad r9.xyz, r8, r5.z, -c3
add r9.xyz, r9, v4
mad r7.xyz, r7, r5.w, r9
mad_pp r7.xyz, r8, -c19.z, r7
else
mov_pp r7.xyz, r4
endif
add_pp r4.w, r5.y, -c15.x
pow_pp r5.w, r4.w, c19.w
mul_pp r7.w, r5.w, c17.x
texldl_pp r8, r7, s1
pow_pp r5.w, r8.w, c4.y
mul_pp r5.w, r5.w, c4.x
mul_pp r9.xyz, r8, r5.w
mov r6.w, c2.w
if_lt r6.w, c17.y
if_lt -c7.w, r5.x
nrm_pp r10.xyz, r4
add r11.xyz, c5, -v4
rcp r12.x, r10.x
rcp r12.y, r10.y
rcp r12.z, r10.z
mul_pp r11.xyz, r11, r12
add r13.xyz, c6, -v4
mul_pp r12.xyz, r12, r13
cmp_pp r11.xyz, -r10, r12, r11
min_pp r5.x, r11.y, r11.x
min_pp r6.w, r11.z, r5.x
mov r11.xyz, c5
add r11.xyz, r11, c6
mad r12.xyz, r11, r5.z, -c7
add r12.xyz, r12, v4
mad r10.xyz, r10, r6.w, r12
mad_pp r7.xyz, r11, -c19.z, r10
else
mov_pp r7.xyz, r4
endif
texldl_pp r7, r7, s2
pow_pp r4.x, r7.w, c8.y
mul_pp r4.x, r4.x, c8.x
mul_pp r4.xyz, r7, r4.x
mad r5.xzw, r5.w, r8.xyyz, -r4.xyyz
mad_pp r9.xyz, c2.w, r5.xzww, r4
endif
mul_pp r4.xyz, r2.w, r9
mad_pp r5.xzw, v1.xyyz, -r0.w, c0.xyyz
dp3_pp r0.w, r5.xzww, r5.xzww
add r6.w, -r0.w, c17.z
rsq_pp r0.w, r0.w
cmp_pp r0.w, r6.w, c17.w, r0.w
mul_pp r5.xzw, r0.w, r5
dp3_pp r0.w, r0, r5.xzww
max_pp r6.w, r0.w, c19.x
dp3_pp r0.x, r0, -r1
max_pp r1.x, r0.x, c19.x
dp3_pp r0.x, c0, r5.xzww
max_pp r1.y, r0.x, c19.x
mul_pp r0.x, r4.w, r4.w
mul_pp r0.y, r0.x, c12.w
mad_pp r0.x, r0.x, -c12.w, r5.y
mad_pp r0.z, r3.w, r0.x, r0.y
mad_pp r0.x, r1.x, r0.x, r0.y
mad r0.x, r0.z, r0.x, c18.x
rcp_pp r0.x, r0.x
add_pp r0.y, -r4.w, c19.y
mad_pp r0.y, r0.y, c18.y, c18.z
log_pp r0.y, r0.y
rcp r0.y, r0.y
mul_pp r0.y, r0.y, c18.w
mul_pp r0.z, r0.y, r0.y
mad_pp r0.y, r0.y, r0.y, c19.y
mul_pp r0.y, r0.y, c12.y
pow_pp r1.z, r6.w, r0.z
add_pp r0.z, -r3.w, c19.y
mul_pp r0.w, r0.z, r0.z
mul_pp r0.w, r0.w, r0.w
mul_pp r0.z, r0.z, r0.w
add_pp r0.w, -r1.x, c19.y
mul_pp r1.x, r0.w, r0.w
mul_pp r1.x, r1.x, r1.x
mul_pp r0.yw, r0, r1.xzzx
mul_pp r1.x, r1.y, r1.y
dp2add_pp r1.x, r1.x, r4.w, -c19.z
mad_pp r0.z, r1.x, r0.z, c19.y
mad_pp r1.x, r1.x, r0.w, c19.y
mul_pp r0.z, r0.z, r1.x
mul_pp r0.x, r0.y, r0.x
mul_pp r0.xy, r3.w, r0.xzzw
mul_pp r0.x, r0.x, c12.x
add_pp r0.z, -r1.w, c19.y
add_sat_pp r0.z, r0.z, c15.x
mul_pp r1.xzw, r0.y, c11.xyyz
mad_pp r1.xzw, r6.xyyz, r2.w, r1
mul_pp r5.xyz, r0.x, c11
cmp_pp r5.xyz, r0.x, r5, c19.x
add_pp r0.x, -r1.y, c19.y
mul_pp r0.y, r0.x, r0.x
mul_pp r0.y, r0.y, r0.y
mul_pp r0.x, r0.x, r0.y
lrp_pp r6.xyz, r0.x, c19.y, r2
mul_pp r5.xyz, r5, r6
mad_pp r1.xyz, r3, r1.xzww, r5
lrp_pp r3.xyz, r0.w, r0.z, r2
mad_pp oC0.xyz, r4, r3, r1
mov_pp oC0.w, c19.y

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
Vector 15 [_Color]
Float 17 [_Glossiness]
Vector 13 [_LightColor0]
Float 16 [_Metallic]
Float 18 [_OcclusionStrength]
Vector 0 [_WorldSpaceLightPos0]
Vector 12 [unity_ColorSpaceDielectricSpec]
Vector 14 [unity_LightGammaCorrectionConsts]
Vector 3 [unity_SHAb]
Vector 2 [unity_SHAg]
Vector 1 [unity_SHAr]
Vector 4 [unity_SpecCube0_BoxMax]
Vector 5 [unity_SpecCube0_BoxMin]
Vector 7 [unity_SpecCube0_HDR]
Vector 6 [unity_SpecCube0_ProbePosition]
Vector 8 [unity_SpecCube1_BoxMax]
Vector 9 [unity_SpecCube1_BoxMin]
Vector 11 [unity_SpecCube1_HDR]
Vector 10 [unity_SpecCube1_ProbePosition]
SetTexture 0 [unity_SpecCube0] CUBE 0
SetTexture 1 [unity_SpecCube1] CUBE 1
SetTexture 2 [_MainTex] 2D 2
SetTexture 3 [_OcclusionMap] 2D 3
SetTexture 4 [_ShadowMapTexture] 2D 4
"ps_3_0
def c19, 7, 0.999989986, 0.00100000005, 31.622776
def c20, 9.99999975e-005, 0.967999995, 0.0299999993, 10
def c21, 0, 1, 0.5, 0.75
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord4_pp v2.xyz
dcl_texcoord5_pp v3.xyz
dcl_texcoord6 v4
dcl_texcoord8_pp v5.xyz
dcl_cube s0
dcl_cube s1
dcl_2d s2
dcl_2d s3
dcl_2d s4
nrm_pp r0.xyz, v2
dp3_pp r1.x, v1, v1
rsq_pp r1.x, r1.x
mul_pp r1.yzw, r1.x, v1.xxyz
texld r2, v0, s2
mul_pp r3.xyz, r2, c15
mov r4, c12
mad_pp r2.xyz, c15, r2, -r4
mad_pp r2.xyz, c16.x, r2, r4
mad_pp r2.w, c16.x, -r4.w, r4.w
mul_pp r3.xyz, r2.w, r3
dp3_pp r3.w, r0, c0
max_pp r4.x, r3.w, c21.x
texldp_pp r5, v4, s4
texld_pp r6, v0, s3
mov r7.xyz, c21
add_pp r3.w, r7.y, -c18.x
mad_pp r3.w, r6.y, c18.x, r3.w
mov r0.w, c21.y
dp4_pp r6.x, c1, r0
dp4_pp r6.y, c2, r0
dp4_pp r6.z, c3, r0
add_pp r4.yzw, r6.xxyz, v3.xxyz
mul_pp r5.xyz, r5.x, c13
dp3 r0.w, r1.yzww, r0
add r0.w, r0.w, r0.w
mad_pp r6.xyz, r0, -r0.w, r1.yzww
if_lt -c6.w, r7.x
nrm_pp r8.xyz, r6
add r9.xyz, c4, -v5
rcp r10.x, r8.x
rcp r10.y, r8.y
rcp r10.z, r8.z
mul_pp r9.xyz, r9, r10
add r11.xyz, c5, -v5
mul_pp r10.xyz, r10, r11
cmp_pp r9.xyz, -r8, r10, r9
min_pp r0.w, r9.y, r9.x
min_pp r5.w, r9.z, r0.w
mov r9.xyz, c5
add r9.xyz, r9, c4
mad r10.xyz, r9, r7.z, -c6
add r10.xyz, r10, v5
mad r8.xyz, r8, r5.w, r10
mad_pp r8.xyz, r9, -c21.z, r8
else
mov_pp r8.xyz, r6
endif
add_pp r0.w, r7.y, -c17.x
pow_pp r5.w, r0.w, c21.w
mul_pp r8.w, r5.w, c19.x
texldl_pp r9, r8, s0
pow_pp r5.w, r9.w, c7.y
mul_pp r5.w, r5.w, c7.x
mul_pp r10.xyz, r9, r5.w
mov r6.w, c5.w
if_lt r6.w, c19.y
if_lt -c10.w, r7.x
nrm_pp r11.xyz, r6
add r12.xyz, c8, -v5
rcp r13.x, r11.x
rcp r13.y, r11.y
rcp r13.z, r11.z
mul_pp r12.xyz, r12, r13
add r14.xyz, c9, -v5
mul_pp r13.xyz, r13, r14
cmp_pp r12.xyz, -r11, r13, r12
min_pp r6.w, r12.y, r12.x
min_pp r7.x, r12.z, r6.w
mov r12.xyz, c8
add r12.xyz, r12, c9
mad r13.xyz, r12, r7.z, -c10
add r13.xyz, r13, v5
mad r7.xzw, r11.xyyz, r7.x, r13.xyyz
mad_pp r8.xyz, r12, -c21.z, r7.xzww
else
mov_pp r8.xyz, r6
endif
texldl_pp r6, r8, s1
pow_pp r7.x, r6.w, c11.y
mul_pp r6.w, r7.x, c11.x
mul_pp r6.xyz, r6, r6.w
mad r7.xzw, r5.w, r9.xyyz, -r6.xyyz
mad_pp r10.xyz, c5.w, r7.xzww, r6
endif
mul_pp r6.xyz, r3.w, r10
mad_pp r7.xzw, v1.xyyz, -r1.x, c0.xyyz
dp3_pp r1.x, r7.xzww, r7.xzww
add r5.w, -r1.x, c19.z
rsq_pp r1.x, r1.x
cmp_pp r1.x, r5.w, c19.w, r1.x
mul_pp r7.xzw, r1.x, r7
dp3_pp r1.x, r0, r7.xzww
max_pp r5.w, r1.x, c21.x
dp3_pp r0.x, r0, -r1.yzww
max_pp r1.x, r0.x, c21.x
dp3_pp r0.x, c0, r7.xzww
max_pp r1.y, r0.x, c21.x
mul_pp r0.x, r0.w, r0.w
mul_pp r0.y, r0.x, c14.w
mad_pp r0.x, r0.x, -c14.w, r7.y
mad_pp r0.z, r4.x, r0.x, r0.y
mad_pp r0.x, r1.x, r0.x, r0.y
mad r0.x, r0.z, r0.x, c20.x
rcp_pp r0.x, r0.x
add_pp r0.y, -r0.w, c21.y
mad_pp r0.y, r0.y, c20.y, c20.z
log_pp r0.y, r0.y
rcp r0.y, r0.y
mul_pp r0.y, r0.y, c20.w
mul_pp r0.z, r0.y, r0.y
mad_pp r0.y, r0.y, r0.y, c21.y
mul_pp r0.y, r0.y, c14.y
pow_pp r1.z, r5.w, r0.z
mul_pp r0.y, r0.y, r1.z
add_pp r0.z, -r4.x, c21.y
mul_pp r1.z, r0.z, r0.z
mul_pp r1.z, r1.z, r1.z
mul_pp r0.z, r0.z, r1.z
add_pp r1.x, -r1.x, c21.y
mul_pp r1.z, r1.x, r1.x
mul_pp r1.z, r1.z, r1.z
mul_pp r1.x, r1.x, r1.z
mul_pp r1.z, r1.y, r1.y
dp2add_pp r0.w, r1.z, r0.w, -c21.z
mad_pp r0.z, r0.w, r0.z, c21.y
mad_pp r0.w, r0.w, r1.x, c21.y
mul_pp r0.xz, r0.yyww, r0
mul_pp r0.x, r4.x, r0.x
mul_pp r0.x, r0.x, c14.x
max_pp r1.z, r0.x, c21.x
mul_pp r0.x, r4.x, r0.z
add_pp r0.y, -r2.w, c21.y
add_sat_pp r0.y, r0.y, c17.x
mul_pp r0.xzw, r0.x, r5.xyyz
mad_pp r0.xzw, r4.yyzw, r3.w, r0
mul_pp r4.xyz, r5, r1.z
add_pp r1.y, -r1.y, c21.y
mul_pp r1.z, r1.y, r1.y
mul_pp r1.z, r1.z, r1.z
mul_pp r1.y, r1.y, r1.z
lrp_pp r5.xyz, r1.y, c21.y, r2
mul_pp r1.yzw, r4.xxyz, r5.xxyz
mad_pp r0.xzw, r3.xyyz, r0, r1.yyzw
lrp_pp r3.xyz, r1.x, r0.y, r2
mad_pp oC0.xyz, r6, r3, r0.xzww
mov_pp oC0.w, c21.y

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" }
Vector 13 [_Color]
Float 15 [_Glossiness]
Vector 11 [_LightColor0]
Float 14 [_Metallic]
Float 16 [_OcclusionStrength]
Vector 0 [_WorldSpaceLightPos0]
Vector 9 [unity_ColorSpaceDielectricSpec]
Vector 10 [unity_DynamicLightmap_HDR]
Vector 12 [unity_LightGammaCorrectionConsts]
Vector 1 [unity_SpecCube0_BoxMax]
Vector 2 [unity_SpecCube0_BoxMin]
Vector 4 [unity_SpecCube0_HDR]
Vector 3 [unity_SpecCube0_ProbePosition]
Vector 5 [unity_SpecCube1_BoxMax]
Vector 6 [unity_SpecCube1_BoxMin]
Vector 8 [unity_SpecCube1_HDR]
Vector 7 [unity_SpecCube1_ProbePosition]
SetTexture 0 [unity_DynamicLightmap] 2D 0
SetTexture 1 [unity_SpecCube0] CUBE 1
SetTexture 2 [unity_SpecCube1] CUBE 2
SetTexture 3 [_MainTex] 2D 3
SetTexture 4 [_OcclusionMap] 2D 4
SetTexture 5 [_ShadowMapTexture] 2D 5
"ps_3_0
def c17, 7, 0.999989986, 0.00100000005, 31.622776
def c18, 9.99999975e-005, 0.967999995, 0.0299999993, 10
def c19, 0, 1, 0.5, 0.75
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord4_pp v2.xyz
dcl_texcoord5_pp v3.zw
dcl_texcoord6 v4
dcl_texcoord8_pp v5.xyz
dcl_2d s0
dcl_cube s1
dcl_cube s2
dcl_2d s3
dcl_2d s4
dcl_2d s5
nrm_pp r0.xyz, v2
dp3_pp r0.w, v1, v1
rsq_pp r0.w, r0.w
mul_pp r1.xyz, r0.w, v1
texld r2, v0, s3
mul_pp r3.xyz, r2, c13
mov r4, c9
mad_pp r2.xyz, c13, r2, -r4
mad_pp r2.xyz, c14.x, r2, r4
mad_pp r1.w, c14.x, -r4.w, r4.w
mul_pp r3.xyz, r1.w, r3
dp3_pp r2.w, r0, c0
max_pp r3.w, r2.w, c19.x
texldp_pp r4, v4, s5
texld_pp r5, v0, s4
mov r6.xyz, c19
add_pp r2.w, r6.y, -c16.x
mad_pp r2.w, r5.y, c16.x, r2.w
mul_pp r4.xyz, r4.x, c11
texld_pp r5, v3.zwzw, s0
mul_pp r4.w, r5.w, c10.x
mul_pp r5.xyz, r5, r4.w
log_pp r7.x, r5.x
log_pp r7.y, r5.y
log_pp r7.z, r5.z
mul_pp r5.xyz, r7, c10.y
exp_pp r7.x, r5.x
exp_pp r7.y, r5.y
exp_pp r7.z, r5.z
dp3 r4.w, r1, r0
add r4.w, r4.w, r4.w
mad_pp r5.xyz, r0, -r4.w, r1
if_lt -c3.w, r6.x
nrm_pp r8.xyz, r5
add r9.xyz, c1, -v5
rcp r10.x, r8.x
rcp r10.y, r8.y
rcp r10.z, r8.z
mul_pp r9.xyz, r9, r10
add r11.xyz, c2, -v5
mul_pp r10.xyz, r10, r11
cmp_pp r9.xyz, -r8, r10, r9
min_pp r4.w, r9.y, r9.x
min_pp r5.w, r9.z, r4.w
mov r9.xyz, c2
add r9.xyz, r9, c1
mad r10.xyz, r9, r6.z, -c3
add r10.xyz, r10, v5
mad r8.xyz, r8, r5.w, r10
mad_pp r8.xyz, r9, -c19.z, r8
else
mov_pp r8.xyz, r5
endif
add_pp r4.w, r6.y, -c15.x
pow_pp r5.w, r4.w, c19.w
mul_pp r8.w, r5.w, c17.x
texldl_pp r9, r8, s1
pow_pp r5.w, r9.w, c4.y
mul_pp r5.w, r5.w, c4.x
mul_pp r10.xyz, r9, r5.w
mov r6.w, c2.w
if_lt r6.w, c17.y
if_lt -c7.w, r6.x
nrm_pp r11.xyz, r5
add r12.xyz, c5, -v5
rcp r13.x, r11.x
rcp r13.y, r11.y
rcp r13.z, r11.z
mul_pp r12.xyz, r12, r13
add r14.xyz, c6, -v5
mul_pp r13.xyz, r13, r14
cmp_pp r12.xyz, -r11, r13, r12
min_pp r6.x, r12.y, r12.x
min_pp r7.w, r12.z, r6.x
mov r12.xyz, c5
add r12.xyz, r12, c6
mad r6.xzw, r12.xyyz, r6.z, -c7.xyyz
add r6.xzw, r6, v5.xyyz
mad r6.xzw, r11.xyyz, r7.w, r6
mad_pp r8.xyz, r12, -c19.z, r6.xzww
else
mov_pp r8.xyz, r5
endif
texldl_pp r8, r8, s2
pow_pp r5.x, r8.w, c8.y
mul_pp r5.x, r5.x, c8.x
mul_pp r5.xyz, r8, r5.x
mad r6.xzw, r5.w, r9.xyyz, -r5.xyyz
mad_pp r10.xyz, c2.w, r6.xzww, r5
endif
mul_pp r5.xyz, r2.w, r10
mad_pp r6.xzw, v1.xyyz, -r0.w, c0.xyyz
dp3_pp r0.w, r6.xzww, r6.xzww
add r5.w, -r0.w, c17.z
rsq_pp r0.w, r0.w
cmp_pp r0.w, r5.w, c17.w, r0.w
mul_pp r6.xzw, r0.w, r6
dp3_pp r0.w, r0, r6.xzww
max_pp r5.w, r0.w, c19.x
dp3_pp r0.x, r0, -r1
max_pp r1.x, r0.x, c19.x
dp3_pp r0.x, c0, r6.xzww
max_pp r1.y, r0.x, c19.x
mul_pp r0.x, r4.w, r4.w
mul_pp r0.y, r0.x, c12.w
mad_pp r0.x, r0.x, -c12.w, r6.y
mad_pp r0.z, r3.w, r0.x, r0.y
mad_pp r0.x, r1.x, r0.x, r0.y
mad r0.x, r0.z, r0.x, c18.x
rcp_pp r0.x, r0.x
add_pp r0.y, -r4.w, c19.y
mad_pp r0.y, r0.y, c18.y, c18.z
log_pp r0.y, r0.y
rcp r0.y, r0.y
mul_pp r0.y, r0.y, c18.w
mul_pp r0.z, r0.y, r0.y
mad_pp r0.y, r0.y, r0.y, c19.y
mul_pp r0.y, r0.y, c12.y
pow_pp r1.z, r5.w, r0.z
add_pp r0.z, -r3.w, c19.y
mul_pp r0.w, r0.z, r0.z
mul_pp r0.w, r0.w, r0.w
mul_pp r0.z, r0.z, r0.w
add_pp r0.w, -r1.x, c19.y
mul_pp r1.x, r0.w, r0.w
mul_pp r1.x, r1.x, r1.x
mul_pp r0.yw, r0, r1.xzzx
mul_pp r1.x, r1.y, r1.y
dp2add_pp r1.x, r1.x, r4.w, -c19.z
mad_pp r0.z, r1.x, r0.z, c19.y
mad_pp r1.x, r1.x, r0.w, c19.y
mul_pp r0.z, r0.z, r1.x
mul_pp r0.x, r0.y, r0.x
mul_pp r0.x, r3.w, r0.x
mul_pp r0.x, r0.x, c12.x
max_pp r1.x, r0.x, c19.x
mul_pp r0.x, r3.w, r0.z
add_pp r0.y, -r1.w, c19.y
add_sat_pp r0.y, r0.y, c15.x
mul_pp r6.xyz, r0.x, r4
mad_pp r6.xyz, r7, r2.w, r6
mul_pp r1.xzw, r4.xyyz, r1.x
add_pp r0.x, -r1.y, c19.y
mul_pp r0.z, r0.x, r0.x
mul_pp r0.z, r0.z, r0.z
mul_pp r0.x, r0.x, r0.z
lrp_pp r4.xyz, r0.x, c19.y, r2
mul_pp r1.xyz, r1.xzww, r4
mad_pp r1.xyz, r3, r6, r1
lrp_pp r3.xyz, r0.w, r0.y, r2
mad_pp oC0.xyz, r5, r3, r1
mov_pp oC0.w, c19.y

"
}
}
 }
 Pass {
  Name "FORWARD_DELTA"
  Tags { "LIGHTMODE"="ForwardAdd" "SHADOWSUPPORT"="true" "RenderType"="Opaque" "PerformanceChecks"="False" }
  ZWrite Off
  Fog {
   Color (0,0,0,0)
  }
  Blend [_SrcBlend] One
  GpuProgramID 115835
Program "vp" {
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 11 [_LightMatrix0] 3
Matrix 4 [_Object2World]
Matrix 8 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 17 [_DetailAlbedoMap_ST]
Vector 16 [_MainTex_ST]
Float 18 [_UVSec]
Vector 14 [_WorldSpaceCameraPos]
Vector 15 [_WorldSpaceLightPos0]
"vs_3_0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6.xyz
dp4 o0.x, c0, v0
dp4 o0.y, c1, v0
dp4 o0.z, c2, v0
dp4 o0.w, c3, v0
mad o1.xy, v2, c16, c16.zwzw
abs r0.x, c18.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c17.xyxy, c17
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c14
mul r1.xyz, c9, v1.y
mad r1.xyz, c8, v1.x, r1
mad r1.xyz, c10, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c4, v4
dp3 r1.y, c5, v4
dp3 r1.z, c6, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov o5.xyz, r2
mov o3.xyz, r3
mul o4.xyz, r1, v4.w
dp4 r0.w, c7, v0
dp4 o6.x, c11, r0
dp4 o6.y, c12, r0
dp4 o6.z, c13, r0
mad r0.xyz, r0, -c15.w, c15
mov o3.w, r0.x
mov o4.w, r0.y
mov o5.w, r0.z

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 13 [_DetailAlbedoMap_ST]
Vector 12 [_MainTex_ST]
Float 14 [_UVSec]
Vector 10 [_WorldSpaceCameraPos]
Vector 11 [_WorldSpaceLightPos0]
"vs_3_0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dp4 o0.x, c0, v0
dp4 o0.y, c1, v0
dp4 o0.z, c2, v0
dp4 o0.w, c3, v0
mad o1.xy, v2, c12, c12.zwzw
abs r0.x, c14.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c13.xyxy, c13
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c10
mad r0.xyz, r0, -c11.w, c11
mul r1.xyz, c8, v1.y
mad r1.xyz, c7, v1.x, r1
mad r1.xyz, c9, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c4, v4
dp3 r1.y, c5, v4
dp3 r1.z, c6, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov o5.xyz, r2
mov o3.xyz, r3
mul o4.xyz, r1, v4.w
mov o3.w, r0.x
mov o4.w, r0.y
mov o5.w, r0.z

"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 8 [_LightMatrix0]
Matrix 4 [_Object2World]
Matrix 12 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 18 [_DetailAlbedoMap_ST]
Vector 17 [_MainTex_ST]
Float 19 [_UVSec]
Vector 15 [_WorldSpaceCameraPos]
Vector 16 [_WorldSpaceLightPos0]
"vs_3_0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dp4 o0.x, c0, v0
dp4 o0.y, c1, v0
dp4 o0.z, c2, v0
dp4 o0.w, c3, v0
mad o1.xy, v2, c17, c17.zwzw
abs r0.x, c19.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c18.xyxy, c18
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c15
mul r1.xyz, c13, v1.y
mad r1.xyz, c12, v1.x, r1
mad r1.xyz, c14, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c4, v4
dp3 r1.y, c5, v4
dp3 r1.z, c6, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov o5.xyz, r2
mov o3.xyz, r3
mul o4.xyz, r1, v4.w
dp4 r0.w, c7, v0
dp4 o6.x, c8, r0
dp4 o6.y, c9, r0
dp4 o6.z, c10, r0
dp4 o6.w, c11, r0
mad r0.xyz, r0, -c16.w, c16
mov o3.w, r0.x
mov o4.w, r0.y
mov o5.w, r0.z

"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 11 [_LightMatrix0] 3
Matrix 4 [_Object2World]
Matrix 8 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 17 [_DetailAlbedoMap_ST]
Vector 16 [_MainTex_ST]
Float 18 [_UVSec]
Vector 14 [_WorldSpaceCameraPos]
Vector 15 [_WorldSpaceLightPos0]
"vs_3_0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6.xyz
dp4 o0.x, c0, v0
dp4 o0.y, c1, v0
dp4 o0.z, c2, v0
dp4 o0.w, c3, v0
mad o1.xy, v2, c16, c16.zwzw
abs r0.x, c18.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c17.xyxy, c17
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c14
mul r1.xyz, c9, v1.y
mad r1.xyz, c8, v1.x, r1
mad r1.xyz, c10, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c4, v4
dp3 r1.y, c5, v4
dp3 r1.z, c6, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov o5.xyz, r2
mov o3.xyz, r3
mul o4.xyz, r1, v4.w
dp4 r0.w, c7, v0
dp4 o6.x, c11, r0
dp4 o6.y, c12, r0
dp4 o6.z, c13, r0
mad r0.xyz, r0, -c15.w, c15
mov o3.w, r0.x
mov o4.w, r0.y
mov o5.w, r0.z

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 11 [_LightMatrix0] 2
Matrix 4 [_Object2World]
Matrix 8 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 16 [_DetailAlbedoMap_ST]
Vector 15 [_MainTex_ST]
Float 17 [_UVSec]
Vector 13 [_WorldSpaceCameraPos]
Vector 14 [_WorldSpaceLightPos0]
"vs_3_0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6.xy
dp4 o0.x, c0, v0
dp4 o0.y, c1, v0
dp4 o0.z, c2, v0
dp4 o0.w, c3, v0
mad o1.xy, v2, c15, c15.zwzw
abs r0.x, c17.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c16.xyxy, c16
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c13
mul r1.xyz, c9, v1.y
mad r1.xyz, c8, v1.x, r1
mad r1.xyz, c10, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c4, v4
dp3 r1.y, c5, v4
dp3 r1.z, c6, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov o5.xyz, r2
mov o3.xyz, r3
mul o4.xyz, r1, v4.w
dp4 r0.w, c7, v0
dp4 o6.x, c11, r0
dp4 o6.y, c12, r0
mad r0.xyz, r0, -c14.w, c14
mov o3.w, r0.x
mov o4.w, r0.y
mov o5.w, r0.z

"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_NATIVE" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 12 [_LightMatrix0]
Matrix 8 [_Object2World]
Matrix 16 [_World2Object] 3
Matrix 4 [glstate_matrix_mvp]
Matrix 0 [unity_World2Shadow0]
Vector 22 [_DetailAlbedoMap_ST]
Vector 21 [_MainTex_ST]
Float 23 [_UVSec]
Vector 19 [_WorldSpaceCameraPos]
Vector 20 [_WorldSpaceLightPos0]
"vs_3_0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord6 o7
dp4 o0.x, c4, v0
dp4 o0.y, c5, v0
dp4 o0.z, c6, v0
dp4 o0.w, c7, v0
mad o1.xy, v2, c21, c21.zwzw
abs r0.x, c23.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c22.xyxy, c22
dp4 r0.x, c8, v0
dp4 r0.y, c9, v0
dp4 r0.z, c10, v0
add o2.xyz, r0, -c19
mul r1.xyz, c17, v1.y
mad r1.xyz, c16, v1.x, r1
mad r1.xyz, c18, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c8, v4
dp3 r1.y, c9, v4
dp3 r1.z, c10, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov o5.xyz, r2
mov o3.xyz, r3
mul o4.xyz, r1, v4.w
dp4 r0.w, c11, v0
dp4 o6.x, c12, r0
dp4 o6.y, c13, r0
dp4 o6.z, c14, r0
dp4 o6.w, c15, r0
dp4 o7.x, c0, r0
dp4 o7.y, c1, r0
dp4 o7.z, c2, r0
dp4 o7.w, c3, r0
mad r0.xyz, r0, -c20.w, c20
mov o3.w, r0.x
mov o4.w, r0.y
mov o5.w, r0.z

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 15 [_DetailAlbedoMap_ST]
Vector 14 [_MainTex_ST]
Vector 11 [_ProjectionParams]
Vector 12 [_ScreenParams]
Float 16 [_UVSec]
Vector 10 [_WorldSpaceCameraPos]
Vector 13 [_WorldSpaceLightPos0]
"vs_3_0
def c17, 0.5, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
mad o1.xy, v2, c14, c14.zwzw
abs r0.x, c16.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c15.xyxy, c15
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c10
mad r0.xyz, r0, -c13.w, c13
mul r1.xyz, c8, v1.y
mad r1.xyz, c7, v1.x, r1
mad r1.xyz, c9, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c4, v4
dp3 r1.y, c5, v4
dp3 r1.z, c6, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov o5.xyz, r2
mov o3.xyz, r3
mul o4.xyz, r1, v4.w
dp4 r1.y, c1, v0
mul r0.w, r1.y, c11.x
mul r2.w, r0.w, c17.x
dp4 r1.x, c0, v0
dp4 r1.w, c3, v0
mul r2.xz, r1.xyww, c17.x
mad o6.xy, r2.z, c12.zwzw, r2.xwzw
dp4 r1.z, c2, v0
mov o0, r1
mov o6.zw, r1
mov o3.w, r0.x
mov o4.w, r0.y
mov o5.w, r0.z

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 11 [_LightMatrix0] 2
Matrix 4 [_Object2World]
Matrix 8 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 18 [_DetailAlbedoMap_ST]
Vector 17 [_MainTex_ST]
Vector 14 [_ProjectionParams]
Vector 15 [_ScreenParams]
Float 19 [_UVSec]
Vector 13 [_WorldSpaceCameraPos]
Vector 16 [_WorldSpaceLightPos0]
"vs_3_0
def c20, 0.5, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6.xy
dcl_texcoord6 o7
mad o1.xy, v2, c17, c17.zwzw
abs r0.x, c19.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c18.xyxy, c18
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c13
mul r1.xyz, c9, v1.y
mad r1.xyz, c8, v1.x, r1
mad r1.xyz, c10, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c4, v4
dp3 r1.y, c5, v4
dp3 r1.z, c6, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov o5.xyz, r2
mov o3.xyz, r3
mul o4.xyz, r1, v4.w
dp4 r0.w, c7, v0
dp4 o6.x, c11, r0
dp4 o6.y, c12, r0
mad r0.xyz, r0, -c16.w, c16
dp4 r1.y, c1, v0
mul r0.w, r1.y, c14.x
mul r2.w, r0.w, c20.x
dp4 r1.x, c0, v0
dp4 r1.w, c3, v0
mul r2.xz, r1.xyww, c20.x
mad o7.xy, r2.z, c15.zwzw, r2.xwzw
dp4 r1.z, c2, v0
mov o0, r1
mov o7.zw, r1
mov o3.w, r0.x
mov o4.w, r0.y
mov o5.w, r0.z

"
}
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_CUBE" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 11 [_LightMatrix0] 3
Matrix 4 [_Object2World]
Matrix 8 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 18 [_DetailAlbedoMap_ST]
Vector 16 [_LightPositionRange]
Vector 17 [_MainTex_ST]
Float 19 [_UVSec]
Vector 14 [_WorldSpaceCameraPos]
Vector 15 [_WorldSpaceLightPos0]
"vs_3_0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6.xyz
dcl_texcoord6 o7.xyz
dp4 o0.x, c0, v0
dp4 o0.y, c1, v0
dp4 o0.z, c2, v0
dp4 o0.w, c3, v0
mad o1.xy, v2, c17, c17.zwzw
abs r0.x, c19.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c18.xyxy, c18
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c14
mul r1.xyz, c9, v1.y
mad r1.xyz, c8, v1.x, r1
mad r1.xyz, c10, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c4, v4
dp3 r1.y, c5, v4
dp3 r1.z, c6, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov o5.xyz, r2
mov o3.xyz, r3
mul o4.xyz, r1, v4.w
dp4 r0.w, c7, v0
dp4 o6.x, c11, r0
dp4 o6.y, c12, r0
dp4 o6.z, c13, r0
add o7.xyz, r0, -c16
mad r0.xyz, r0, -c15.w, c15
mov o3.w, r0.x
mov o4.w, r0.y
mov o5.w, r0.z

"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 11 [_LightMatrix0] 3
Matrix 4 [_Object2World]
Matrix 8 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 18 [_DetailAlbedoMap_ST]
Vector 16 [_LightPositionRange]
Vector 17 [_MainTex_ST]
Float 19 [_UVSec]
Vector 14 [_WorldSpaceCameraPos]
Vector 15 [_WorldSpaceLightPos0]
"vs_3_0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6.xyz
dcl_texcoord6 o7.xyz
dp4 o0.x, c0, v0
dp4 o0.y, c1, v0
dp4 o0.z, c2, v0
dp4 o0.w, c3, v0
mad o1.xy, v2, c17, c17.zwzw
abs r0.x, c19.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c18.xyxy, c18
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c14
mul r1.xyz, c9, v1.y
mad r1.xyz, c8, v1.x, r1
mad r1.xyz, c10, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c4, v4
dp3 r1.y, c5, v4
dp3 r1.z, c6, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov o5.xyz, r2
mov o3.xyz, r3
mul o4.xyz, r1, v4.w
dp4 r0.w, c7, v0
dp4 o6.x, c11, r0
dp4 o6.y, c12, r0
dp4 o6.z, c13, r0
add o7.xyz, r0, -c16
mad r0.xyz, r0, -c15.w, c15
mov o3.w, r0.x
mov o4.w, r0.y
mov o5.w, r0.z

"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_SOFT" "SHADOWS_NATIVE" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 12 [_LightMatrix0]
Matrix 8 [_Object2World]
Matrix 16 [_World2Object] 3
Matrix 4 [glstate_matrix_mvp]
Matrix 0 [unity_World2Shadow0]
Vector 22 [_DetailAlbedoMap_ST]
Vector 21 [_MainTex_ST]
Float 23 [_UVSec]
Vector 19 [_WorldSpaceCameraPos]
Vector 20 [_WorldSpaceLightPos0]
"vs_3_0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord6 o7
dp4 o0.x, c4, v0
dp4 o0.y, c5, v0
dp4 o0.z, c6, v0
dp4 o0.w, c7, v0
mad o1.xy, v2, c21, c21.zwzw
abs r0.x, c23.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c22.xyxy, c22
dp4 r0.x, c8, v0
dp4 r0.y, c9, v0
dp4 r0.z, c10, v0
add o2.xyz, r0, -c19
mul r1.xyz, c17, v1.y
mad r1.xyz, c16, v1.x, r1
mad r1.xyz, c18, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c8, v4
dp3 r1.y, c9, v4
dp3 r1.z, c10, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov o5.xyz, r2
mov o3.xyz, r3
mul o4.xyz, r1, v4.w
dp4 r0.w, c11, v0
dp4 o6.x, c12, r0
dp4 o6.y, c13, r0
dp4 o6.z, c14, r0
dp4 o6.w, c15, r0
dp4 o7.x, c0, r0
dp4 o7.y, c1, r0
dp4 o7.z, c2, r0
dp4 o7.w, c3, r0
mad r0.xyz, r0, -c20.w, c20
mov o3.w, r0.x
mov o4.w, r0.y
mov o5.w, r0.z

"
}
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_CUBE" "SHADOWS_SOFT" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 11 [_LightMatrix0] 3
Matrix 4 [_Object2World]
Matrix 8 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 18 [_DetailAlbedoMap_ST]
Vector 16 [_LightPositionRange]
Vector 17 [_MainTex_ST]
Float 19 [_UVSec]
Vector 14 [_WorldSpaceCameraPos]
Vector 15 [_WorldSpaceLightPos0]
"vs_3_0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6.xyz
dcl_texcoord6 o7.xyz
dp4 o0.x, c0, v0
dp4 o0.y, c1, v0
dp4 o0.z, c2, v0
dp4 o0.w, c3, v0
mad o1.xy, v2, c17, c17.zwzw
abs r0.x, c19.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c18.xyxy, c18
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c14
mul r1.xyz, c9, v1.y
mad r1.xyz, c8, v1.x, r1
mad r1.xyz, c10, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c4, v4
dp3 r1.y, c5, v4
dp3 r1.z, c6, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov o5.xyz, r2
mov o3.xyz, r3
mul o4.xyz, r1, v4.w
dp4 r0.w, c7, v0
dp4 o6.x, c11, r0
dp4 o6.y, c12, r0
dp4 o6.z, c13, r0
add o7.xyz, r0, -c16
mad r0.xyz, r0, -c15.w, c15
mov o3.w, r0.x
mov o4.w, r0.y
mov o5.w, r0.z

"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "SHADOWS_SOFT" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 11 [_LightMatrix0] 3
Matrix 4 [_Object2World]
Matrix 8 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 18 [_DetailAlbedoMap_ST]
Vector 16 [_LightPositionRange]
Vector 17 [_MainTex_ST]
Float 19 [_UVSec]
Vector 14 [_WorldSpaceCameraPos]
Vector 15 [_WorldSpaceLightPos0]
"vs_3_0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6.xyz
dcl_texcoord6 o7.xyz
dp4 o0.x, c0, v0
dp4 o0.y, c1, v0
dp4 o0.z, c2, v0
dp4 o0.w, c3, v0
mad o1.xy, v2, c17, c17.zwzw
abs r0.x, c19.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c18.xyxy, c18
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c14
mul r1.xyz, c9, v1.y
mad r1.xyz, c8, v1.x, r1
mad r1.xyz, c10, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c4, v4
dp3 r1.y, c5, v4
dp3 r1.z, c6, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov o5.xyz, r2
mov o3.xyz, r3
mul o4.xyz, r1, v4.w
dp4 r0.w, c7, v0
dp4 o6.x, c11, r0
dp4 o6.y, c12, r0
dp4 o6.z, c13, r0
add o7.xyz, r0, -c16
mad r0.xyz, r0, -c15.w, c15
mov o3.w, r0.x
mov o4.w, r0.y
mov o5.w, r0.z

"
}
}
Program "fp" {
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_OFF" }
Vector 3 [_Color]
Float 5 [_Glossiness]
Vector 1 [_LightColor0]
Float 4 [_Metallic]
Vector 0 [unity_ColorSpaceDielectricSpec]
Vector 2 [unity_LightGammaCorrectionConsts]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_LightTexture0] 2D 1
"ps_3_0
def c6, 0, 1, 0.00100000005, 31.622776
def c7, 9.99999975e-005, 0.967999995, 0.0299999993, 10
def c8, -0.5, 0, 0, 0
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord2_pp v2.w
dcl_texcoord3_pp v3.w
dcl_texcoord4_pp v4
dcl_texcoord5 v5.xyz
dcl_2d s0
dcl_2d s1
mov r0.y, c6.y
add_pp r0.x, r0.y, -c5.x
add_pp r0.z, -r0.x, c6.y
mad_pp r0.z, r0.z, c7.y, c7.z
log_pp r0.z, r0.z
rcp r0.z, r0.z
mul_pp r0.z, r0.z, c7.w
mad_pp r0.w, r0.z, r0.z, c6.y
mul_pp r0.z, r0.z, r0.z
mul_pp r0.w, r0.w, c2.y
nrm_pp r1.xyz, v1
mov_pp r2.x, v2.w
mov_pp r2.y, v3.w
mov_pp r2.z, v4.w
dp3_pp r1.w, r2, r2
rsq_pp r1.w, r1.w
mad_pp r3.xyz, r2, r1.w, -r1
mul_pp r2.xyz, r1.w, r2
dp3_pp r1.w, r3, r3
add r2.w, -r1.w, c6.z
rsq_pp r1.w, r1.w
cmp_pp r1.w, r2.w, c6.w, r1.w
mul_pp r3.xyz, r1.w, r3
nrm_pp r4.xyz, v4
dp3_pp r1.w, r4, r3
dp3_pp r2.w, r2, r3
dp3_pp r2.x, r4, r2
dp3_pp r1.x, r4, -r1
max_pp r2.y, r1.x, c6.x
max_pp r1.x, r2.x, c6.x
max_pp r1.y, r2.w, c6.x
max_pp r2.x, r1.w, c6.x
pow_pp r1.z, r2.x, r0.z
mul_pp r0.z, r0.w, r1.z
mul_pp r0.w, r0.x, r0.x
mul_pp r1.z, r0.w, c2.w
mad_pp r0.y, r0.w, -c2.w, r0.y
mad_pp r0.w, r2.y, r0.y, r1.z
add_pp r1.w, -r2.y, c6.y
mad_pp r0.y, r1.x, r0.y, r1.z
mad r0.y, r0.y, r0.w, c7.x
rcp_pp r0.y, r0.y
mul_pp r0.y, r0.z, r0.y
mul_pp r0.y, r1.x, r0.y
mul_pp r0.y, r0.y, c2.x
max_pp r1.z, r0.y, c6.x
dp3 r0.y, v5, v5
texld_pp r2, r0.y, s1
mul_pp r0.yzw, r2.x, c1.xxyz
mul_pp r2.xyz, r0.yzww, r1.z
add_pp r1.z, -r1.y, c6.y
mul_pp r1.y, r1.y, r1.y
dp2add_pp r0.x, r1.y, r0.x, c8.x
mul_pp r1.y, r1.z, r1.z
mul_pp r1.y, r1.y, r1.y
mul_pp r1.y, r1.z, r1.y
texld r3, v0, s0
mov r4, c0
mad_pp r5.xyz, c3, r3, -r4
mul_pp r3.xyz, r3, c3
mad_pp r4.xyz, c4.x, r5, r4
lrp_pp r5.xyz, r1.y, c6.y, r4
mul_pp r2.xyz, r2, r5
mul_pp r1.y, r1.w, r1.w
mul_pp r1.y, r1.y, r1.y
mul_pp r1.y, r1.w, r1.y
mad_pp r1.y, r0.x, r1.y, c6.y
add_pp r1.z, -r1.x, c6.y
mul_pp r1.w, r1.z, r1.z
mul_pp r1.w, r1.w, r1.w
mul_pp r1.z, r1.z, r1.w
mad_pp r0.x, r0.x, r1.z, c6.y
mul_pp r0.x, r1.y, r0.x
mul_pp r0.x, r1.x, r0.x
mul_pp r0.xyz, r0.x, r0.yzww
mad_pp r0.w, c4.x, -r4.w, r4.w
mul_pp r1.xyz, r0.w, r3
mad_pp oC0.xyz, r1, r0, r2
mov_pp oC0.w, c6.y

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" }
Vector 3 [_Color]
Float 5 [_Glossiness]
Vector 1 [_LightColor0]
Float 4 [_Metallic]
Vector 0 [unity_ColorSpaceDielectricSpec]
Vector 2 [unity_LightGammaCorrectionConsts]
SetTexture 0 [_MainTex] 2D 0
"ps_3_0
def c6, 0, 1, 0.00100000005, 31.622776
def c7, 9.99999975e-005, 0.967999995, 0.0299999993, 10
def c8, -0.5, 0, 0, 0
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord2_pp v2.w
dcl_texcoord3_pp v3.w
dcl_texcoord4_pp v4
dcl_2d s0
mov r0.y, c6.y
add_pp r0.x, r0.y, -c5.x
add_pp r0.z, -r0.x, c6.y
mad_pp r0.z, r0.z, c7.y, c7.z
log_pp r0.z, r0.z
rcp r0.z, r0.z
mul_pp r0.z, r0.z, c7.w
mad_pp r0.w, r0.z, r0.z, c6.y
mul_pp r0.z, r0.z, r0.z
mul_pp r0.w, r0.w, c2.y
dp3_pp r1.x, v1, v1
rsq_pp r1.x, r1.x
mov_pp r2.x, v2.w
mov_pp r2.y, v3.w
mov_pp r2.z, v4.w
mad_pp r1.yzw, v1.xxyz, -r1.x, r2.xxyz
mul_pp r3.xyz, r1.x, v1
dp3_pp r1.x, r1.yzww, r1.yzww
add r2.w, -r1.x, c6.z
rsq_pp r1.x, r1.x
cmp_pp r1.x, r2.w, c6.w, r1.x
mul_pp r1.xyz, r1.x, r1.yzww
nrm_pp r4.xyz, v4
dp3_pp r1.w, r4, r1
dp3_pp r1.x, r2, r1
dp3_pp r1.y, r4, r2
dp3_pp r1.z, r4, -r3
max_pp r2.x, r1.z, c6.x
max_pp r2.y, r1.y, c6.x
max_pp r2.z, r1.x, c6.x
max_pp r2.w, r1.w, c6.x
pow_pp r1.x, r2.w, r0.z
mul_pp r0.z, r0.w, r1.x
mul_pp r0.w, r0.x, r0.x
mul_pp r1.x, r0.w, c2.w
mad_pp r0.y, r0.w, -c2.w, r0.y
mad_pp r0.w, r2.x, r0.y, r1.x
mad_pp r0.y, r2.y, r0.y, r1.x
mad r0.y, r0.y, r0.w, c7.x
rcp_pp r0.y, r0.y
mul_pp r0.y, r0.z, r0.y
mul_pp r0.y, r2.y, r0.y
mul_pp r0.y, r0.y, c2.x
mul_pp r1.xzw, r0.y, c1.xyyz
cmp_pp r0.yzw, r0.y, r1.xxzw, c6.x
add_pp r1.xy, -r2.zxzw, c6.y
mul_pp r1.z, r2.z, r2.z
dp2add_pp r0.x, r1.z, r0.x, c8.x
mul_pp r1.z, r1.x, r1.x
mul_pp r1.z, r1.z, r1.z
mul_pp r1.x, r1.x, r1.z
texld r3, v0, s0
mov r4, c0
mad_pp r2.xzw, c3.xyyz, r3.xyyz, -r4.xyyz
mul_pp r3.xyz, r3, c3
mad_pp r2.xzw, c4.x, r2, r4.xyyz
lrp_pp r4.xyz, r1.x, c6.y, r2.xzww
mul_pp r0.yzw, r0, r4.xxyz
mul_pp r1.x, r1.y, r1.y
mul_pp r1.x, r1.x, r1.x
mul_pp r1.x, r1.y, r1.x
mad_pp r1.x, r0.x, r1.x, c6.y
add_pp r1.y, -r2.y, c6.y
mul_pp r1.z, r1.y, r1.y
mul_pp r1.z, r1.z, r1.z
mul_pp r1.y, r1.y, r1.z
mad_pp r0.x, r0.x, r1.y, c6.y
mul_pp r0.x, r1.x, r0.x
mul_pp r0.x, r2.y, r0.x
mul_pp r1.xyz, r0.x, c1
mad_pp r0.x, c4.x, -r4.w, r4.w
mul_pp r2.xyz, r0.x, r3
mad_pp oC0.xyz, r2, r1, r0.yzww
mov_pp oC0.w, c6.y

"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_OFF" }
Vector 3 [_Color]
Float 5 [_Glossiness]
Vector 1 [_LightColor0]
Float 4 [_Metallic]
Vector 0 [unity_ColorSpaceDielectricSpec]
Vector 2 [unity_LightGammaCorrectionConsts]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_LightTexture0] 2D 1
SetTexture 2 [_LightTextureB0] 2D 2
"ps_3_0
def c6, 0.5, 0, 1, 0.00100000005
def c7, 31.622776, 9.99999975e-005, 0.967999995, 0.0299999993
def c8, 10, 0, 0, 0
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord2_pp v2.w
dcl_texcoord3_pp v3.w
dcl_texcoord4_pp v4
dcl_texcoord5 v5
dcl_2d s0
dcl_2d s1
dcl_2d s2
mov r0.z, c6.z
add_pp r0.x, r0.z, -c5.x
add_pp r0.y, -r0.x, c6.z
mad_pp r0.y, r0.y, c7.z, c7.w
log_pp r0.y, r0.y
rcp r0.y, r0.y
mul_pp r0.y, r0.y, c8.x
mad_pp r0.w, r0.y, r0.y, c6.z
mul_pp r0.y, r0.y, r0.y
mul_pp r0.w, r0.w, c2.y
nrm_pp r1.xyz, v1
mov_pp r2.x, v2.w
mov_pp r2.y, v3.w
mov_pp r2.z, v4.w
dp3_pp r1.w, r2, r2
rsq_pp r1.w, r1.w
mad_pp r3.xyz, r2, r1.w, -r1
mul_pp r2.xyz, r1.w, r2
dp3_pp r1.w, r3, r3
add r2.w, -r1.w, c6.w
rsq_pp r1.w, r1.w
cmp_pp r1.w, r2.w, c7.x, r1.w
mul_pp r3.xyz, r1.w, r3
nrm_pp r4.xyz, v4
dp3_pp r1.w, r4, r3
dp3_pp r2.w, r2, r3
dp3_pp r2.x, r4, r2
dp3_pp r1.x, r4, -r1
max_pp r2.y, r1.x, c6.y
max_pp r1.x, r2.x, c6.y
max_pp r1.y, r2.w, c6.y
max_pp r2.x, r1.w, c6.y
pow_pp r1.z, r2.x, r0.y
mul_pp r0.y, r0.w, r1.z
mul_pp r0.w, r0.x, r0.x
mul_pp r1.z, r0.w, c2.w
mad_pp r0.z, r0.w, -c2.w, r0.z
mad_pp r0.w, r2.y, r0.z, r1.z
add_pp r1.w, -r2.y, c6.z
mad_pp r0.z, r1.x, r0.z, r1.z
mad r0.z, r0.z, r0.w, c7.y
rcp_pp r0.z, r0.z
mul_pp r0.y, r0.y, r0.z
mul_pp r0.y, r1.x, r0.y
mul_pp r0.y, r0.y, c2.x
max_pp r1.z, r0.y, c6.y
rcp r0.y, v5.w
mad r0.yz, v5.xxyw, r0.y, c6.x
texld_pp r2, r0.yzzw, s1
dp3 r0.y, v5, v5
texld_pp r3, r0.y, s2
mul r0.y, r2.w, r3.x
mul_pp r0.yzw, r0.y, c1.xxyz
cmp_pp r0.yzw, -v5.z, c6.y, r0
mul_pp r2.xyz, r0.yzww, r1.z
add_pp r1.z, -r1.y, c6.z
mul_pp r1.y, r1.y, r1.y
dp2add_pp r0.x, r1.y, r0.x, -c6.x
mul_pp r1.y, r1.z, r1.z
mul_pp r1.y, r1.y, r1.y
mul_pp r1.y, r1.z, r1.y
texld r3, v0, s0
mov r4, c0
mad_pp r5.xyz, c3, r3, -r4
mul_pp r3.xyz, r3, c3
mad_pp r4.xyz, c4.x, r5, r4
lrp_pp r5.xyz, r1.y, c6.z, r4
mul_pp r2.xyz, r2, r5
mul_pp r1.y, r1.w, r1.w
mul_pp r1.y, r1.y, r1.y
mul_pp r1.y, r1.w, r1.y
mad_pp r1.y, r0.x, r1.y, c6.z
add_pp r1.z, -r1.x, c6.z
mul_pp r1.w, r1.z, r1.z
mul_pp r1.w, r1.w, r1.w
mul_pp r1.z, r1.z, r1.w
mad_pp r0.x, r0.x, r1.z, c6.z
mul_pp r0.x, r1.y, r0.x
mul_pp r0.x, r1.x, r0.x
mul_pp r0.xyz, r0.x, r0.yzww
mad_pp r0.w, c4.x, -r4.w, r4.w
mul_pp r1.xyz, r0.w, r3
mad_pp oC0.xyz, r1, r0, r2
mov_pp oC0.w, c6.z

"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_OFF" }
Vector 3 [_Color]
Float 5 [_Glossiness]
Vector 1 [_LightColor0]
Float 4 [_Metallic]
Vector 0 [unity_ColorSpaceDielectricSpec]
Vector 2 [unity_LightGammaCorrectionConsts]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_LightTexture0] CUBE 1
SetTexture 2 [_LightTextureB0] 2D 2
"ps_3_0
def c6, 0, 1, 0.00100000005, 31.622776
def c7, 9.99999975e-005, 0.967999995, 0.0299999993, 10
def c8, -0.5, 0, 0, 0
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord2_pp v2.w
dcl_texcoord3_pp v3.w
dcl_texcoord4_pp v4
dcl_texcoord5 v5.xyz
dcl_2d s0
dcl_cube s1
dcl_2d s2
mov r0.y, c6.y
add_pp r0.x, r0.y, -c5.x
add_pp r0.z, -r0.x, c6.y
mad_pp r0.z, r0.z, c7.y, c7.z
log_pp r0.z, r0.z
rcp r0.z, r0.z
mul_pp r0.z, r0.z, c7.w
mad_pp r0.w, r0.z, r0.z, c6.y
mul_pp r0.z, r0.z, r0.z
mul_pp r0.w, r0.w, c2.y
nrm_pp r1.xyz, v1
mov_pp r2.x, v2.w
mov_pp r2.y, v3.w
mov_pp r2.z, v4.w
dp3_pp r1.w, r2, r2
rsq_pp r1.w, r1.w
mad_pp r3.xyz, r2, r1.w, -r1
mul_pp r2.xyz, r1.w, r2
dp3_pp r1.w, r3, r3
add r2.w, -r1.w, c6.z
rsq_pp r1.w, r1.w
cmp_pp r1.w, r2.w, c6.w, r1.w
mul_pp r3.xyz, r1.w, r3
nrm_pp r4.xyz, v4
dp3_pp r1.w, r4, r3
dp3_pp r2.w, r2, r3
dp3_pp r2.x, r4, r2
dp3_pp r1.x, r4, -r1
max_pp r2.y, r1.x, c6.x
max_pp r1.x, r2.x, c6.x
max_pp r1.y, r2.w, c6.x
max_pp r2.x, r1.w, c6.x
pow_pp r1.z, r2.x, r0.z
mul_pp r0.z, r0.w, r1.z
mul_pp r0.w, r0.x, r0.x
mul_pp r1.z, r0.w, c2.w
mad_pp r0.y, r0.w, -c2.w, r0.y
mad_pp r0.w, r2.y, r0.y, r1.z
add_pp r1.w, -r2.y, c6.y
mad_pp r0.y, r1.x, r0.y, r1.z
mad r0.y, r0.y, r0.w, c7.x
rcp_pp r0.y, r0.y
mul_pp r0.y, r0.z, r0.y
mul_pp r0.y, r1.x, r0.y
mul_pp r0.y, r0.y, c2.x
max_pp r1.z, r0.y, c6.x
dp3 r0.y, v5, v5
texld r2, r0.y, s2
texld r3, v5, s1
mul_pp r0.y, r2.x, r3.w
mul_pp r0.yzw, r0.y, c1.xxyz
mul_pp r2.xyz, r0.yzww, r1.z
add_pp r1.z, -r1.y, c6.y
mul_pp r1.y, r1.y, r1.y
dp2add_pp r0.x, r1.y, r0.x, c8.x
mul_pp r1.y, r1.z, r1.z
mul_pp r1.y, r1.y, r1.y
mul_pp r1.y, r1.z, r1.y
texld r3, v0, s0
mov r4, c0
mad_pp r5.xyz, c3, r3, -r4
mul_pp r3.xyz, r3, c3
mad_pp r4.xyz, c4.x, r5, r4
lrp_pp r5.xyz, r1.y, c6.y, r4
mul_pp r2.xyz, r2, r5
mul_pp r1.y, r1.w, r1.w
mul_pp r1.y, r1.y, r1.y
mul_pp r1.y, r1.w, r1.y
mad_pp r1.y, r0.x, r1.y, c6.y
add_pp r1.z, -r1.x, c6.y
mul_pp r1.w, r1.z, r1.z
mul_pp r1.w, r1.w, r1.w
mul_pp r1.z, r1.z, r1.w
mad_pp r0.x, r0.x, r1.z, c6.y
mul_pp r0.x, r1.y, r0.x
mul_pp r0.x, r1.x, r0.x
mul_pp r0.xyz, r0.x, r0.yzww
mad_pp r0.w, c4.x, -r4.w, r4.w
mul_pp r1.xyz, r0.w, r3
mad_pp oC0.xyz, r1, r0, r2
mov_pp oC0.w, c6.y

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_OFF" }
Vector 3 [_Color]
Float 5 [_Glossiness]
Vector 1 [_LightColor0]
Float 4 [_Metallic]
Vector 0 [unity_ColorSpaceDielectricSpec]
Vector 2 [unity_LightGammaCorrectionConsts]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_LightTexture0] 2D 1
"ps_3_0
def c6, 0, 1, 0.00100000005, 31.622776
def c7, 9.99999975e-005, 0.967999995, 0.0299999993, 10
def c8, -0.5, 0, 0, 0
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord2_pp v2.w
dcl_texcoord3_pp v3.w
dcl_texcoord4_pp v4
dcl_texcoord5 v5.xy
dcl_2d s0
dcl_2d s1
mov r0.y, c6.y
add_pp r0.x, r0.y, -c5.x
add_pp r0.z, -r0.x, c6.y
mad_pp r0.z, r0.z, c7.y, c7.z
log_pp r0.z, r0.z
rcp r0.z, r0.z
mul_pp r0.z, r0.z, c7.w
mad_pp r0.w, r0.z, r0.z, c6.y
mul_pp r0.z, r0.z, r0.z
mul_pp r0.w, r0.w, c2.y
dp3_pp r1.x, v1, v1
rsq_pp r1.x, r1.x
mov_pp r2.x, v2.w
mov_pp r2.y, v3.w
mov_pp r2.z, v4.w
mad_pp r1.yzw, v1.xxyz, -r1.x, r2.xxyz
mul_pp r3.xyz, r1.x, v1
dp3_pp r1.x, r1.yzww, r1.yzww
add r2.w, -r1.x, c6.z
rsq_pp r1.x, r1.x
cmp_pp r1.x, r2.w, c6.w, r1.x
mul_pp r1.xyz, r1.x, r1.yzww
nrm_pp r4.xyz, v4
dp3_pp r1.w, r4, r1
dp3_pp r1.x, r2, r1
dp3_pp r1.y, r4, r2
dp3_pp r1.z, r4, -r3
max_pp r2.x, r1.z, c6.x
max_pp r2.y, r1.y, c6.x
max_pp r2.z, r1.x, c6.x
max_pp r2.w, r1.w, c6.x
pow_pp r1.x, r2.w, r0.z
mul_pp r0.z, r0.w, r1.x
mul_pp r0.w, r0.x, r0.x
mul_pp r1.x, r0.w, c2.w
mad_pp r0.y, r0.w, -c2.w, r0.y
mad_pp r0.w, r2.x, r0.y, r1.x
add_pp r1.y, -r2.x, c6.y
mad_pp r0.y, r2.y, r0.y, r1.x
mad r0.y, r0.y, r0.w, c7.x
rcp_pp r0.y, r0.y
mul_pp r0.y, r0.z, r0.y
mul_pp r0.y, r2.y, r0.y
mul_pp r0.y, r0.y, c2.x
max_pp r1.x, r0.y, c6.x
texld_pp r3, v5, s1
mul_pp r0.yzw, r3.w, c1.xxyz
mul_pp r1.xzw, r0.yyzw, r1.x
add_pp r2.x, -r2.z, c6.y
mul_pp r2.z, r2.z, r2.z
dp2add_pp r0.x, r2.z, r0.x, c8.x
mul_pp r2.z, r2.x, r2.x
mul_pp r2.z, r2.z, r2.z
mul_pp r2.x, r2.x, r2.z
texld r3, v0, s0
mov r4, c0
mad_pp r5.xyz, c3, r3, -r4
mul_pp r3.xyz, r3, c3
mad_pp r4.xyz, c4.x, r5, r4
lrp_pp r5.xyz, r2.x, c6.y, r4
mul_pp r1.xzw, r1, r5.xyyz
mul_pp r2.x, r1.y, r1.y
mul_pp r2.x, r2.x, r2.x
mul_pp r1.y, r1.y, r2.x
mad_pp r1.y, r0.x, r1.y, c6.y
add_pp r2.x, -r2.y, c6.y
mul_pp r2.z, r2.x, r2.x
mul_pp r2.z, r2.z, r2.z
mul_pp r2.x, r2.x, r2.z
mad_pp r0.x, r0.x, r2.x, c6.y
mul_pp r0.x, r1.y, r0.x
mul_pp r0.x, r2.y, r0.x
mul_pp r0.xyz, r0.x, r0.yzww
mad_pp r0.w, c4.x, -r4.w, r4.w
mul_pp r2.xyz, r0.w, r3
mad_pp oC0.xyz, r2, r0, r1.xzww
mov_pp oC0.w, c6.y

"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_NATIVE" }
Vector 4 [_Color]
Float 6 [_Glossiness]
Vector 2 [_LightColor0]
Vector 0 [_LightShadowData]
Float 5 [_Metallic]
Vector 1 [unity_ColorSpaceDielectricSpec]
Vector 3 [unity_LightGammaCorrectionConsts]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_ShadowMapTexture] 2D 1
SetTexture 2 [_LightTexture0] 2D 2
SetTexture 3 [_LightTextureB0] 2D 3
"ps_3_0
def c7, 0.5, 0, 1, 0.00100000005
def c8, 31.622776, 9.99999975e-005, 0.967999995, 0.0299999993
def c9, 10, 0, 0, 0
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord2_pp v2.w
dcl_texcoord3_pp v3.w
dcl_texcoord4_pp v4
dcl_texcoord5 v5
dcl_texcoord6 v6
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
rcp r0.x, v5.w
mad r0.xy, v5, r0.x, c7.x
texld_pp r0, r0, s2
dp3 r0.x, v5, v5
texld_pp r1, r0.x, s3
mul r0.x, r0.w, r1.x
cmp r0.x, -v5.z, c7.y, r0.x
texldp_pp r1, v6, s1
mov r0.z, c7.z
lrp_pp r2.x, r1.x, r0.z, c0.x
mul_pp r0.x, r0.x, r2.x
mul_pp r0.xyw, r0.x, c2.xyzz
add_pp r1.x, r0.z, -c6.x
add_pp r1.y, -r1.x, c7.z
mad_pp r1.y, r1.y, c8.z, c8.w
log_pp r1.y, r1.y
rcp r1.y, r1.y
mul_pp r1.y, r1.y, c9.x
mad_pp r1.z, r1.y, r1.y, c7.z
mul_pp r1.y, r1.y, r1.y
mul_pp r1.z, r1.z, c3.y
nrm_pp r2.xyz, v1
mov_pp r3.x, v2.w
mov_pp r3.y, v3.w
mov_pp r3.z, v4.w
dp3_pp r1.w, r3, r3
rsq_pp r1.w, r1.w
mad_pp r4.xyz, r3, r1.w, -r2
mul_pp r3.xyz, r1.w, r3
dp3_pp r1.w, r4, r4
add r2.w, -r1.w, c7.w
rsq_pp r1.w, r1.w
cmp_pp r1.w, r2.w, c8.x, r1.w
mul_pp r4.xyz, r1.w, r4
nrm_pp r5.xyz, v4
dp3_pp r1.w, r5, r4
dp3_pp r2.w, r3, r4
dp3_pp r3.x, r5, r3
dp3_pp r2.x, r5, -r2
max_pp r3.y, r2.x, c7.y
max_pp r2.x, r3.x, c7.y
max_pp r3.x, r2.w, c7.y
max_pp r2.y, r1.w, c7.y
pow_pp r3.z, r2.y, r1.y
mul_pp r1.y, r1.z, r3.z
mul_pp r1.z, r1.x, r1.x
mul_pp r1.w, r1.z, c3.w
mad_pp r0.z, r1.z, -c3.w, r0.z
mad_pp r1.z, r3.y, r0.z, r1.w
add_pp r2.y, -r3.y, c7.z
mad_pp r0.z, r2.x, r0.z, r1.w
mad r0.z, r0.z, r1.z, c8.y
rcp_pp r0.z, r0.z
mul_pp r0.z, r1.y, r0.z
mul_pp r0.z, r2.x, r0.z
mul_pp r0.z, r0.z, c3.x
max_pp r1.y, r0.z, c7.y
mul_pp r1.yzw, r0.xxyw, r1.y
add_pp r0.z, -r3.x, c7.z
mul_pp r2.z, r3.x, r3.x
dp2add_pp r1.x, r2.z, r1.x, -c7.x
mul_pp r2.z, r0.z, r0.z
mul_pp r2.z, r2.z, r2.z
mul_pp r0.z, r0.z, r2.z
texld r3, v0, s0
mov r4, c1
mad_pp r5.xyz, c4, r3, -r4
mul_pp r3.xyz, r3, c4
mad_pp r4.xyz, c5.x, r5, r4
lrp_pp r5.xyz, r0.z, c7.z, r4
mul_pp r1.yzw, r1, r5.xxyz
mul_pp r0.z, r2.y, r2.y
mul_pp r0.z, r0.z, r0.z
mul_pp r0.z, r2.y, r0.z
mad_pp r0.z, r1.x, r0.z, c7.z
add_pp r2.y, -r2.x, c7.z
mul_pp r2.z, r2.y, r2.y
mul_pp r2.z, r2.z, r2.z
mul_pp r2.y, r2.y, r2.z
mad_pp r1.x, r1.x, r2.y, c7.z
mul_pp r0.z, r0.z, r1.x
mul_pp r0.z, r2.x, r0.z
mul_pp r0.xyz, r0.z, r0.xyww
mad_pp r0.w, c5.x, -r4.w, r4.w
mul_pp r2.xyz, r0.w, r3
mad_pp oC0.xyz, r2, r0, r1.yzww
mov_pp oC0.w, c7.z

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
Vector 3 [_Color]
Float 5 [_Glossiness]
Vector 1 [_LightColor0]
Float 4 [_Metallic]
Vector 0 [unity_ColorSpaceDielectricSpec]
Vector 2 [unity_LightGammaCorrectionConsts]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_ShadowMapTexture] 2D 1
"ps_3_0
def c6, 0, 1, 0.00100000005, 31.622776
def c7, 9.99999975e-005, 0.967999995, 0.0299999993, 10
def c8, -0.5, 0, 0, 0
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord2_pp v2.w
dcl_texcoord3_pp v3.w
dcl_texcoord4_pp v4
dcl_texcoord5 v5
dcl_2d s0
dcl_2d s1
mov r0.y, c6.y
add_pp r0.x, r0.y, -c5.x
add_pp r0.z, -r0.x, c6.y
mad_pp r0.z, r0.z, c7.y, c7.z
log_pp r0.z, r0.z
rcp r0.z, r0.z
mul_pp r0.z, r0.z, c7.w
mad_pp r0.w, r0.z, r0.z, c6.y
mul_pp r0.z, r0.z, r0.z
mul_pp r0.w, r0.w, c2.y
dp3_pp r1.x, v1, v1
rsq_pp r1.x, r1.x
mov_pp r2.x, v2.w
mov_pp r2.y, v3.w
mov_pp r2.z, v4.w
mad_pp r1.yzw, v1.xxyz, -r1.x, r2.xxyz
mul_pp r3.xyz, r1.x, v1
dp3_pp r1.x, r1.yzww, r1.yzww
add r2.w, -r1.x, c6.z
rsq_pp r1.x, r1.x
cmp_pp r1.x, r2.w, c6.w, r1.x
mul_pp r1.xyz, r1.x, r1.yzww
nrm_pp r4.xyz, v4
dp3_pp r1.w, r4, r1
dp3_pp r1.x, r2, r1
dp3_pp r1.y, r4, r2
dp3_pp r1.z, r4, -r3
max_pp r2.x, r1.z, c6.x
max_pp r2.y, r1.y, c6.x
max_pp r2.z, r1.x, c6.x
max_pp r2.w, r1.w, c6.x
pow_pp r1.x, r2.w, r0.z
mul_pp r0.z, r0.w, r1.x
mul_pp r0.w, r0.x, r0.x
mul_pp r1.x, r0.w, c2.w
mad_pp r0.y, r0.w, -c2.w, r0.y
mad_pp r0.w, r2.x, r0.y, r1.x
add_pp r1.y, -r2.x, c6.y
mad_pp r0.y, r2.y, r0.y, r1.x
mad r0.y, r0.y, r0.w, c7.x
rcp_pp r0.y, r0.y
mul_pp r0.y, r0.z, r0.y
mul_pp r0.y, r2.y, r0.y
mul_pp r0.y, r0.y, c2.x
max_pp r1.x, r0.y, c6.x
texldp_pp r3, v5, s1
mul_pp r0.yzw, r3.x, c1.xxyz
mul_pp r1.xzw, r0.yyzw, r1.x
add_pp r2.x, -r2.z, c6.y
mul_pp r2.z, r2.z, r2.z
dp2add_pp r0.x, r2.z, r0.x, c8.x
mul_pp r2.z, r2.x, r2.x
mul_pp r2.z, r2.z, r2.z
mul_pp r2.x, r2.x, r2.z
texld r3, v0, s0
mov r4, c0
mad_pp r5.xyz, c3, r3, -r4
mul_pp r3.xyz, r3, c3
mad_pp r4.xyz, c4.x, r5, r4
lrp_pp r5.xyz, r2.x, c6.y, r4
mul_pp r1.xzw, r1, r5.xyyz
mul_pp r2.x, r1.y, r1.y
mul_pp r2.x, r2.x, r2.x
mul_pp r1.y, r1.y, r2.x
mad_pp r1.y, r0.x, r1.y, c6.y
add_pp r2.x, -r2.y, c6.y
mul_pp r2.z, r2.x, r2.x
mul_pp r2.z, r2.z, r2.z
mul_pp r2.x, r2.x, r2.z
mad_pp r0.x, r0.x, r2.x, c6.y
mul_pp r0.x, r1.y, r0.x
mul_pp r0.x, r2.y, r0.x
mul_pp r0.xyz, r0.x, r0.yzww
mad_pp r0.w, c4.x, -r4.w, r4.w
mul_pp r2.xyz, r0.w, r3
mad_pp oC0.xyz, r2, r0, r1.xzww
mov_pp oC0.w, c6.y

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
Vector 3 [_Color]
Float 5 [_Glossiness]
Vector 1 [_LightColor0]
Float 4 [_Metallic]
Vector 0 [unity_ColorSpaceDielectricSpec]
Vector 2 [unity_LightGammaCorrectionConsts]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_ShadowMapTexture] 2D 1
SetTexture 2 [_LightTexture0] 2D 2
"ps_3_0
def c6, 0, 1, 0.00100000005, 31.622776
def c7, 9.99999975e-005, 0.967999995, 0.0299999993, 10
def c8, -0.5, 0, 0, 0
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord2_pp v2.w
dcl_texcoord3_pp v3.w
dcl_texcoord4_pp v4
dcl_texcoord5 v5.xy
dcl_texcoord6 v6
dcl_2d s0
dcl_2d s1
dcl_2d s2
mov r0.y, c6.y
add_pp r0.x, r0.y, -c5.x
add_pp r0.z, -r0.x, c6.y
mad_pp r0.z, r0.z, c7.y, c7.z
log_pp r0.z, r0.z
rcp r0.z, r0.z
mul_pp r0.z, r0.z, c7.w
mad_pp r0.w, r0.z, r0.z, c6.y
mul_pp r0.z, r0.z, r0.z
mul_pp r0.w, r0.w, c2.y
dp3_pp r1.x, v1, v1
rsq_pp r1.x, r1.x
mov_pp r2.x, v2.w
mov_pp r2.y, v3.w
mov_pp r2.z, v4.w
mad_pp r1.yzw, v1.xxyz, -r1.x, r2.xxyz
mul_pp r3.xyz, r1.x, v1
dp3_pp r1.x, r1.yzww, r1.yzww
add r2.w, -r1.x, c6.z
rsq_pp r1.x, r1.x
cmp_pp r1.x, r2.w, c6.w, r1.x
mul_pp r1.xyz, r1.x, r1.yzww
nrm_pp r4.xyz, v4
dp3_pp r1.w, r4, r1
dp3_pp r1.x, r2, r1
dp3_pp r1.y, r4, r2
dp3_pp r1.z, r4, -r3
max_pp r2.x, r1.z, c6.x
max_pp r2.y, r1.y, c6.x
max_pp r2.z, r1.x, c6.x
max_pp r2.w, r1.w, c6.x
pow_pp r1.x, r2.w, r0.z
mul_pp r0.z, r0.w, r1.x
mul_pp r0.w, r0.x, r0.x
mul_pp r1.x, r0.w, c2.w
mad_pp r0.y, r0.w, -c2.w, r0.y
mad_pp r0.w, r2.x, r0.y, r1.x
add_pp r1.y, -r2.x, c6.y
mad_pp r0.y, r2.y, r0.y, r1.x
mad r0.y, r0.y, r0.w, c7.x
rcp_pp r0.y, r0.y
mul_pp r0.y, r0.z, r0.y
mul_pp r0.y, r2.y, r0.y
mul_pp r0.y, r0.y, c2.x
max_pp r1.x, r0.y, c6.x
texld r3, v5, s2
texldp_pp r4, v6, s1
mul_pp r0.y, r3.w, r4.x
mul_pp r0.yzw, r0.y, c1.xxyz
mul_pp r1.xzw, r0.yyzw, r1.x
add_pp r2.x, -r2.z, c6.y
mul_pp r2.z, r2.z, r2.z
dp2add_pp r0.x, r2.z, r0.x, c8.x
mul_pp r2.z, r2.x, r2.x
mul_pp r2.z, r2.z, r2.z
mul_pp r2.x, r2.x, r2.z
texld r3, v0, s0
mov r4, c0
mad_pp r5.xyz, c3, r3, -r4
mul_pp r3.xyz, r3, c3
mad_pp r4.xyz, c4.x, r5, r4
lrp_pp r5.xyz, r2.x, c6.y, r4
mul_pp r1.xzw, r1, r5.xyyz
mul_pp r2.x, r1.y, r1.y
mul_pp r2.x, r2.x, r2.x
mul_pp r1.y, r1.y, r2.x
mad_pp r1.y, r0.x, r1.y, c6.y
add_pp r2.x, -r2.y, c6.y
mul_pp r2.z, r2.x, r2.x
mul_pp r2.z, r2.z, r2.z
mul_pp r2.x, r2.x, r2.z
mad_pp r0.x, r0.x, r2.x, c6.y
mul_pp r0.x, r1.y, r0.x
mul_pp r0.x, r2.y, r0.x
mul_pp r0.xyz, r0.x, r0.yzww
mad_pp r0.w, c4.x, -r4.w, r4.w
mul_pp r2.xyz, r0.w, r3
mad_pp oC0.xyz, r2, r0, r1.xzww
mov_pp oC0.w, c6.y

"
}
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_CUBE" }
Vector 5 [_Color]
Float 7 [_Glossiness]
Vector 3 [_LightColor0]
Vector 0 [_LightPositionRange]
Vector 1 [_LightShadowData]
Float 6 [_Metallic]
Vector 2 [unity_ColorSpaceDielectricSpec]
Vector 4 [unity_LightGammaCorrectionConsts]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_ShadowMapTexture] CUBE 1
SetTexture 2 [_LightTexture0] 2D 2
"ps_3_0
def c8, 0.970000029, 1, 0, 0.00100000005
def c9, 31.622776, 9.99999975e-005, 0.967999995, 0.0299999993
def c10, 10, -0.5, 0, 0
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord2_pp v2.w
dcl_texcoord3_pp v3.w
dcl_texcoord4_pp v4
dcl_texcoord5 v5.xyz
dcl_texcoord6 v6.xyz
dcl_2d s0
dcl_cube s1
dcl_2d s2
mov r0.y, c8.y
add_pp r0.x, r0.y, -c7.x
add_pp r0.z, -r0.x, c8.y
mad_pp r0.z, r0.z, c9.z, c9.w
log_pp r0.z, r0.z
rcp r0.z, r0.z
mul_pp r0.z, r0.z, c10.x
mad_pp r0.w, r0.z, r0.z, c8.y
mul_pp r0.z, r0.z, r0.z
mul_pp r0.w, r0.w, c4.y
nrm_pp r1.xyz, v1
mov_pp r2.x, v2.w
mov_pp r2.y, v3.w
mov_pp r2.z, v4.w
dp3_pp r1.w, r2, r2
rsq_pp r1.w, r1.w
mad_pp r3.xyz, r2, r1.w, -r1
mul_pp r2.xyz, r1.w, r2
dp3_pp r1.w, r3, r3
add r2.w, -r1.w, c8.w
rsq_pp r1.w, r1.w
cmp_pp r1.w, r2.w, c9.x, r1.w
mul_pp r3.xyz, r1.w, r3
nrm_pp r4.xyz, v4
dp3_pp r1.w, r4, r3
dp3_pp r2.w, r2, r3
dp3_pp r2.x, r4, r2
dp3_pp r1.x, r4, -r1
max_pp r2.y, r1.x, c8.z
max_pp r1.x, r2.x, c8.z
max_pp r1.y, r2.w, c8.z
max_pp r2.x, r1.w, c8.z
pow_pp r1.z, r2.x, r0.z
mul_pp r0.z, r0.w, r1.z
mul_pp r0.w, r0.x, r0.x
mul_pp r1.z, r0.w, c4.w
mad_pp r0.w, r0.w, -c4.w, r0.y
mad_pp r1.w, r2.y, r0.w, r1.z
add_pp r2.x, -r2.y, c8.y
mad_pp r0.w, r1.x, r0.w, r1.z
mad r0.w, r0.w, r1.w, c9.y
rcp_pp r0.w, r0.w
mul_pp r0.z, r0.z, r0.w
mul_pp r0.z, r1.x, r0.z
mul_pp r0.z, r0.z, c4.x
max_pp r1.z, r0.z, c8.z
dp3 r0.z, v6, v6
rsq r0.z, r0.z
rcp r0.z, r0.z
mul r0.z, r0.z, c0.w
texld r3, v6, s1
mad r0.z, r0.z, -c8.x, r3.x
cmp_pp r0.y, r0.z, r0.y, c1.x
dp3 r0.z, v5, v5
texld r3, r0.z, s2
mul_pp r0.y, r0.y, r3.x
mul_pp r0.yzw, r0.y, c3.xxyz
mul_pp r2.yzw, r0, r1.z
add_pp r1.z, -r1.y, c8.y
mul_pp r1.y, r1.y, r1.y
dp2add_pp r0.x, r1.y, r0.x, c10.y
mul_pp r1.y, r1.z, r1.z
mul_pp r1.y, r1.y, r1.y
mul_pp r1.y, r1.z, r1.y
texld r3, v0, s0
mov r4, c2
mad_pp r5.xyz, c5, r3, -r4
mul_pp r3.xyz, r3, c5
mad_pp r4.xyz, c6.x, r5, r4
lrp_pp r5.xyz, r1.y, c8.y, r4
mul_pp r1.yzw, r2, r5.xxyz
mul_pp r2.y, r2.x, r2.x
mul_pp r2.y, r2.y, r2.y
mul_pp r2.x, r2.x, r2.y
mad_pp r2.x, r0.x, r2.x, c8.y
add_pp r2.y, -r1.x, c8.y
mul_pp r2.z, r2.y, r2.y
mul_pp r2.z, r2.z, r2.z
mul_pp r2.y, r2.y, r2.z
mad_pp r0.x, r0.x, r2.y, c8.y
mul_pp r0.x, r2.x, r0.x
mul_pp r0.x, r1.x, r0.x
mul_pp r0.xyz, r0.x, r0.yzww
mad_pp r0.w, c6.x, -r4.w, r4.w
mul_pp r2.xyz, r0.w, r3
mad_pp oC0.xyz, r2, r0, r1.yzww
mov_pp oC0.w, c8.y

"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
Vector 5 [_Color]
Float 7 [_Glossiness]
Vector 3 [_LightColor0]
Vector 0 [_LightPositionRange]
Vector 1 [_LightShadowData]
Float 6 [_Metallic]
Vector 2 [unity_ColorSpaceDielectricSpec]
Vector 4 [unity_LightGammaCorrectionConsts]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_ShadowMapTexture] CUBE 1
SetTexture 2 [_LightTexture0] CUBE 2
SetTexture 3 [_LightTextureB0] 2D 3
"ps_3_0
def c8, 0.970000029, 1, 0, 0.00100000005
def c9, 31.622776, 9.99999975e-005, 0.967999995, 0.0299999993
def c10, 10, -0.5, 0, 0
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord2_pp v2.w
dcl_texcoord3_pp v3.w
dcl_texcoord4_pp v4
dcl_texcoord5 v5.xyz
dcl_texcoord6 v6.xyz
dcl_2d s0
dcl_cube s1
dcl_cube s2
dcl_2d s3
dp3 r0.x, v6, v6
rsq r0.x, r0.x
rcp r0.x, r0.x
mul r0.x, r0.x, c0.w
texld r1, v6, s1
mad r0.x, r0.x, -c8.x, r1.x
mov r0.y, c8.y
cmp_pp r0.x, r0.x, r0.y, c1.x
dp3 r0.z, v5, v5
texld r1, r0.z, s3
texld r2, v5, s2
mul r0.z, r1.x, r2.w
mul_pp r0.x, r0.x, r0.z
mul_pp r0.xzw, r0.x, c3.xyyz
add_pp r1.x, r0.y, -c7.x
add_pp r1.y, -r1.x, c8.y
mad_pp r1.y, r1.y, c9.z, c9.w
log_pp r1.y, r1.y
rcp r1.y, r1.y
mul_pp r1.y, r1.y, c10.x
mad_pp r1.z, r1.y, r1.y, c8.y
mul_pp r1.y, r1.y, r1.y
mul_pp r1.z, r1.z, c4.y
nrm_pp r2.xyz, v1
mov_pp r3.x, v2.w
mov_pp r3.y, v3.w
mov_pp r3.z, v4.w
dp3_pp r1.w, r3, r3
rsq_pp r1.w, r1.w
mad_pp r4.xyz, r3, r1.w, -r2
mul_pp r3.xyz, r1.w, r3
dp3_pp r1.w, r4, r4
add r2.w, -r1.w, c8.w
rsq_pp r1.w, r1.w
cmp_pp r1.w, r2.w, c9.x, r1.w
mul_pp r4.xyz, r1.w, r4
nrm_pp r5.xyz, v4
dp3_pp r1.w, r5, r4
dp3_pp r2.w, r3, r4
dp3_pp r3.x, r5, r3
dp3_pp r2.x, r5, -r2
max_pp r3.y, r2.x, c8.z
max_pp r2.x, r3.x, c8.z
max_pp r3.x, r2.w, c8.z
max_pp r2.y, r1.w, c8.z
pow_pp r3.z, r2.y, r1.y
mul_pp r1.y, r1.z, r3.z
mul_pp r1.z, r1.x, r1.x
mul_pp r1.w, r1.z, c4.w
mad_pp r0.y, r1.z, -c4.w, r0.y
mad_pp r1.z, r3.y, r0.y, r1.w
add_pp r2.y, -r3.y, c8.y
mad_pp r0.y, r2.x, r0.y, r1.w
mad r0.y, r0.y, r1.z, c9.y
rcp_pp r0.y, r0.y
mul_pp r0.y, r1.y, r0.y
mul_pp r0.y, r2.x, r0.y
mul_pp r0.y, r0.y, c4.x
max_pp r1.y, r0.y, c8.z
mul_pp r1.yzw, r0.xxzw, r1.y
add_pp r0.y, -r3.x, c8.y
mul_pp r2.z, r3.x, r3.x
dp2add_pp r1.x, r2.z, r1.x, c10.y
mul_pp r2.z, r0.y, r0.y
mul_pp r2.z, r2.z, r2.z
mul_pp r0.y, r0.y, r2.z
texld r3, v0, s0
mov r4, c2
mad_pp r5.xyz, c5, r3, -r4
mul_pp r3.xyz, r3, c5
mad_pp r4.xyz, c6.x, r5, r4
lrp_pp r5.xyz, r0.y, c8.y, r4
mul_pp r1.yzw, r1, r5.xxyz
mul_pp r0.y, r2.y, r2.y
mul_pp r0.y, r0.y, r0.y
mul_pp r0.y, r2.y, r0.y
mad_pp r0.y, r1.x, r0.y, c8.y
add_pp r2.y, -r2.x, c8.y
mul_pp r2.z, r2.y, r2.y
mul_pp r2.z, r2.z, r2.z
mul_pp r2.y, r2.y, r2.z
mad_pp r1.x, r1.x, r2.y, c8.y
mul_pp r0.y, r0.y, r1.x
mul_pp r0.y, r2.x, r0.y
mul_pp r0.xyz, r0.y, r0.xzww
mad_pp r0.w, c6.x, -r4.w, r4.w
mul_pp r2.xyz, r0.w, r3
mad_pp oC0.xyz, r2, r0, r1.yzww
mov_pp oC0.w, c8.y

"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_SOFT" "SHADOWS_NATIVE" }
Vector 8 [_Color]
Float 10 [_Glossiness]
Vector 6 [_LightColor0]
Vector 4 [_LightShadowData]
Float 9 [_Metallic]
Vector 0 [_ShadowOffsets0]
Vector 1 [_ShadowOffsets1]
Vector 2 [_ShadowOffsets2]
Vector 3 [_ShadowOffsets3]
Vector 5 [unity_ColorSpaceDielectricSpec]
Vector 7 [unity_LightGammaCorrectionConsts]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_ShadowMapTexture] 2D 1
SetTexture 2 [_LightTexture0] 2D 2
SetTexture 3 [_LightTextureB0] 2D 3
"ps_3_0
def c11, 0.5, 0, 1, 0.25
def c12, 0.00100000005, 31.622776, 9.99999975e-005, 10
def c13, 0.967999995, 0.0299999993, 0, 0
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord2_pp v2.w
dcl_texcoord3_pp v3.w
dcl_texcoord4_pp v4
dcl_texcoord5 v5
dcl_texcoord6 v6
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
mov r0.z, c11.z
rcp r0.x, v6.w
mad r1, v6, r0.x, c0
texldp_pp r1, r1, s1
mad r2, v6, r0.x, c1
texldp_pp r2, r2, s1
mov_pp r1.y, r2.x
mad r2, v6, r0.x, c2
mad r3, v6, r0.x, c3
texldp_pp r3, r3, s1
mov_pp r1.w, r3.x
texldp_pp r2, r2, s1
mov_pp r1.z, r2.x
lrp_pp r2, r1, r0.z, c4.x
dp4_pp r0.x, r2, c11.w
rcp r0.y, v5.w
mad r0.yw, v5.xxzy, r0.y, c11.x
texld_pp r1, r0.ywzw, s2
dp3 r0.y, v5, v5
texld_pp r2, r0.y, s3
mul r0.y, r1.w, r2.x
cmp r0.y, -v5.z, c11.y, r0.y
mul_pp r0.x, r0.x, r0.y
mul_pp r0.xyw, r0.x, c6.xyzz
add_pp r1.x, r0.z, -c10.x
add_pp r1.y, -r1.x, c11.z
mad_pp r1.y, r1.y, c13.x, c13.y
log_pp r1.y, r1.y
rcp r1.y, r1.y
mul_pp r1.y, r1.y, c12.w
mad_pp r1.z, r1.y, r1.y, c11.z
mul_pp r1.y, r1.y, r1.y
mul_pp r1.z, r1.z, c7.y
nrm_pp r2.xyz, v1
mov_pp r3.x, v2.w
mov_pp r3.y, v3.w
mov_pp r3.z, v4.w
dp3_pp r1.w, r3, r3
rsq_pp r1.w, r1.w
mad_pp r4.xyz, r3, r1.w, -r2
mul_pp r3.xyz, r1.w, r3
dp3_pp r1.w, r4, r4
add r2.w, -r1.w, c12.x
rsq_pp r1.w, r1.w
cmp_pp r1.w, r2.w, c12.y, r1.w
mul_pp r4.xyz, r1.w, r4
nrm_pp r5.xyz, v4
dp3_pp r1.w, r5, r4
dp3_pp r2.w, r3, r4
dp3_pp r3.x, r5, r3
dp3_pp r2.x, r5, -r2
max_pp r3.y, r2.x, c11.y
max_pp r2.x, r3.x, c11.y
max_pp r3.x, r2.w, c11.y
max_pp r2.y, r1.w, c11.y
pow_pp r3.z, r2.y, r1.y
mul_pp r1.y, r1.z, r3.z
mul_pp r1.z, r1.x, r1.x
mul_pp r1.w, r1.z, c7.w
mad_pp r0.z, r1.z, -c7.w, r0.z
mad_pp r1.z, r3.y, r0.z, r1.w
add_pp r2.y, -r3.y, c11.z
mad_pp r0.z, r2.x, r0.z, r1.w
mad r0.z, r0.z, r1.z, c12.z
rcp_pp r0.z, r0.z
mul_pp r0.z, r1.y, r0.z
mul_pp r0.z, r2.x, r0.z
mul_pp r0.z, r0.z, c7.x
max_pp r1.y, r0.z, c11.y
mul_pp r1.yzw, r0.xxyw, r1.y
add_pp r0.z, -r3.x, c11.z
mul_pp r2.z, r3.x, r3.x
dp2add_pp r1.x, r2.z, r1.x, -c11.x
mul_pp r2.z, r0.z, r0.z
mul_pp r2.z, r2.z, r2.z
mul_pp r0.z, r0.z, r2.z
texld r3, v0, s0
mov r4, c5
mad_pp r5.xyz, c8, r3, -r4
mul_pp r3.xyz, r3, c8
mad_pp r4.xyz, c9.x, r5, r4
lrp_pp r5.xyz, r0.z, c11.z, r4
mul_pp r1.yzw, r1, r5.xxyz
mul_pp r0.z, r2.y, r2.y
mul_pp r0.z, r0.z, r0.z
mul_pp r0.z, r2.y, r0.z
mad_pp r0.z, r1.x, r0.z, c11.z
add_pp r2.y, -r2.x, c11.z
mul_pp r2.z, r2.y, r2.y
mul_pp r2.z, r2.z, r2.z
mul_pp r2.y, r2.y, r2.z
mad_pp r1.x, r1.x, r2.y, c11.z
mul_pp r0.z, r0.z, r1.x
mul_pp r0.z, r2.x, r0.z
mul_pp r0.xyz, r0.z, r0.xyww
mad_pp r0.w, c9.x, -r4.w, r4.w
mul_pp r2.xyz, r0.w, r3
mad_pp oC0.xyz, r2, r0, r1.yzww
mov_pp oC0.w, c11.z

"
}
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_CUBE" "SHADOWS_SOFT" }
Vector 5 [_Color]
Float 7 [_Glossiness]
Vector 3 [_LightColor0]
Vector 0 [_LightPositionRange]
Vector 1 [_LightShadowData]
Float 6 [_Metallic]
Vector 2 [unity_ColorSpaceDielectricSpec]
Vector 4 [unity_LightGammaCorrectionConsts]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_ShadowMapTexture] CUBE 1
SetTexture 2 [_LightTexture0] 2D 2
"ps_3_0
def c8, 0.0078125, -0.0078125, 0.970000029, 1
def c9, 0.25, 0, 0.00100000005, 31.622776
def c10, 9.99999975e-005, 0.967999995, 0.0299999993, 10
def c11, -0.5, 0, 0, 0
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord2_pp v2.w
dcl_texcoord3_pp v3.w
dcl_texcoord4_pp v4
dcl_texcoord5 v5.xyz
dcl_texcoord6 v6.xyz
dcl_2d s0
dcl_cube s1
dcl_2d s2
dp3 r0.x, v6, v6
rsq r0.x, r0.x
rcp r0.x, r0.x
mul r0.x, r0.x, c0.w
add r0.yzw, c8.x, v6.xxyz
texld r1, r0.yzww, s1
add r0.yzw, c8.xyyx, v6.xxyz
texld r2, r0.yzww, s1
mov r1.y, r2.x
add r0.yzw, c8.xyxy, v6.xxyz
texld r2, r0.yzww, s1
mov r1.z, r2.x
add r0.yzw, c8.xxyy, v6.xxyz
texld r2, r0.yzww, s1
mov r1.w, r2.x
mad r0, r0.x, -c8.z, r1
mov r1.w, c8.w
cmp_pp r0, r0, r1.w, c1.x
dp4_pp r0.x, r0, c9.x
dp3 r0.y, v5, v5
texld r2, r0.y, s2
mul_pp r0.x, r0.x, r2.x
mul_pp r0.xyz, r0.x, c3
add_pp r0.w, r1.w, -c7.x
add_pp r1.x, -r0.w, c8.w
mad_pp r1.x, r1.x, c10.y, c10.z
log_pp r1.x, r1.x
rcp r1.x, r1.x
mul_pp r1.x, r1.x, c10.w
mad_pp r1.y, r1.x, r1.x, c8.w
mul_pp r1.x, r1.x, r1.x
mul_pp r1.y, r1.y, c4.y
nrm_pp r2.xyz, v1
mov_pp r3.x, v2.w
mov_pp r3.y, v3.w
mov_pp r3.z, v4.w
dp3_pp r1.z, r3, r3
rsq_pp r1.z, r1.z
mad_pp r4.xyz, r3, r1.z, -r2
mul_pp r3.xyz, r1.z, r3
dp3_pp r1.z, r4, r4
add r2.w, -r1.z, c9.z
rsq_pp r1.z, r1.z
cmp_pp r1.z, r2.w, c9.w, r1.z
mul_pp r4.xyz, r1.z, r4
nrm_pp r5.xyz, v4
dp3_pp r1.z, r5, r4
dp3_pp r2.w, r3, r4
dp3_pp r3.x, r5, r3
dp3_pp r2.x, r5, -r2
max_pp r3.y, r2.x, c9.y
max_pp r2.x, r3.x, c9.y
max_pp r3.x, r2.w, c9.y
max_pp r2.y, r1.z, c9.y
pow_pp r3.z, r2.y, r1.x
mul_pp r1.x, r1.y, r3.z
mul_pp r1.y, r0.w, r0.w
mul_pp r1.z, r1.y, c4.w
mad_pp r1.y, r1.y, -c4.w, r1.w
mad_pp r1.w, r3.y, r1.y, r1.z
add_pp r2.y, -r3.y, c8.w
mad_pp r1.y, r2.x, r1.y, r1.z
mad r1.y, r1.y, r1.w, c10.x
rcp_pp r1.y, r1.y
mul_pp r1.x, r1.x, r1.y
mul_pp r1.x, r2.x, r1.x
mul_pp r1.x, r1.x, c4.x
max_pp r2.z, r1.x, c9.y
mul_pp r1.xyz, r0, r2.z
add_pp r1.w, -r3.x, c8.w
mul_pp r2.z, r3.x, r3.x
dp2add_pp r0.w, r2.z, r0.w, c11.x
mul_pp r2.z, r1.w, r1.w
mul_pp r2.z, r2.z, r2.z
mul_pp r1.w, r1.w, r2.z
texld r3, v0, s0
mov r4, c2
mad_pp r5.xyz, c5, r3, -r4
mul_pp r3.xyz, r3, c5
mad_pp r4.xyz, c6.x, r5, r4
lrp_pp r5.xyz, r1.w, c8.w, r4
mul_pp r1.xyz, r1, r5
mul_pp r1.w, r2.y, r2.y
mul_pp r1.w, r1.w, r1.w
mul_pp r1.w, r2.y, r1.w
mad_pp r1.w, r0.w, r1.w, c8.w
add_pp r2.y, -r2.x, c8.w
mul_pp r2.z, r2.y, r2.y
mul_pp r2.z, r2.z, r2.z
mul_pp r2.y, r2.y, r2.z
mad_pp r0.w, r0.w, r2.y, c8.w
mul_pp r0.w, r1.w, r0.w
mul_pp r0.w, r2.x, r0.w
mul_pp r0.xyz, r0.w, r0
mad_pp r0.w, c6.x, -r4.w, r4.w
mul_pp r2.xyz, r0.w, r3
mad_pp oC0.xyz, r2, r0, r1
mov_pp oC0.w, c8.w

"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "SHADOWS_SOFT" }
Vector 5 [_Color]
Float 7 [_Glossiness]
Vector 3 [_LightColor0]
Vector 0 [_LightPositionRange]
Vector 1 [_LightShadowData]
Float 6 [_Metallic]
Vector 2 [unity_ColorSpaceDielectricSpec]
Vector 4 [unity_LightGammaCorrectionConsts]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_ShadowMapTexture] CUBE 1
SetTexture 2 [_LightTexture0] CUBE 2
SetTexture 3 [_LightTextureB0] 2D 3
"ps_3_0
def c8, 0.0078125, -0.0078125, 0.970000029, 1
def c9, 0.25, 0, 0.00100000005, 31.622776
def c10, 9.99999975e-005, 0.967999995, 0.0299999993, 10
def c11, -0.5, 0, 0, 0
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord2_pp v2.w
dcl_texcoord3_pp v3.w
dcl_texcoord4_pp v4
dcl_texcoord5 v5.xyz
dcl_texcoord6 v6.xyz
dcl_2d s0
dcl_cube s1
dcl_cube s2
dcl_2d s3
dp3 r0.x, v6, v6
rsq r0.x, r0.x
rcp r0.x, r0.x
mul r0.x, r0.x, c0.w
add r0.yzw, c8.x, v6.xxyz
texld r1, r0.yzww, s1
add r0.yzw, c8.xyyx, v6.xxyz
texld r2, r0.yzww, s1
mov r1.y, r2.x
add r0.yzw, c8.xyxy, v6.xxyz
texld r2, r0.yzww, s1
mov r1.z, r2.x
add r0.yzw, c8.xxyy, v6.xxyz
texld r2, r0.yzww, s1
mov r1.w, r2.x
mad r0, r0.x, -c8.z, r1
mov r1.w, c8.w
cmp_pp r0, r0, r1.w, c1.x
dp4_pp r0.x, r0, c9.x
dp3 r0.y, v5, v5
texld r2, r0.y, s3
texld r3, v5, s2
mul r0.y, r2.x, r3.w
mul_pp r0.x, r0.x, r0.y
mul_pp r0.xyz, r0.x, c3
add_pp r0.w, r1.w, -c7.x
add_pp r1.x, -r0.w, c8.w
mad_pp r1.x, r1.x, c10.y, c10.z
log_pp r1.x, r1.x
rcp r1.x, r1.x
mul_pp r1.x, r1.x, c10.w
mad_pp r1.y, r1.x, r1.x, c8.w
mul_pp r1.x, r1.x, r1.x
mul_pp r1.y, r1.y, c4.y
nrm_pp r2.xyz, v1
mov_pp r3.x, v2.w
mov_pp r3.y, v3.w
mov_pp r3.z, v4.w
dp3_pp r1.z, r3, r3
rsq_pp r1.z, r1.z
mad_pp r4.xyz, r3, r1.z, -r2
mul_pp r3.xyz, r1.z, r3
dp3_pp r1.z, r4, r4
add r2.w, -r1.z, c9.z
rsq_pp r1.z, r1.z
cmp_pp r1.z, r2.w, c9.w, r1.z
mul_pp r4.xyz, r1.z, r4
nrm_pp r5.xyz, v4
dp3_pp r1.z, r5, r4
dp3_pp r2.w, r3, r4
dp3_pp r3.x, r5, r3
dp3_pp r2.x, r5, -r2
max_pp r3.y, r2.x, c9.y
max_pp r2.x, r3.x, c9.y
max_pp r3.x, r2.w, c9.y
max_pp r2.y, r1.z, c9.y
pow_pp r3.z, r2.y, r1.x
mul_pp r1.x, r1.y, r3.z
mul_pp r1.y, r0.w, r0.w
mul_pp r1.z, r1.y, c4.w
mad_pp r1.y, r1.y, -c4.w, r1.w
mad_pp r1.w, r3.y, r1.y, r1.z
add_pp r2.y, -r3.y, c8.w
mad_pp r1.y, r2.x, r1.y, r1.z
mad r1.y, r1.y, r1.w, c10.x
rcp_pp r1.y, r1.y
mul_pp r1.x, r1.x, r1.y
mul_pp r1.x, r2.x, r1.x
mul_pp r1.x, r1.x, c4.x
max_pp r2.z, r1.x, c9.y
mul_pp r1.xyz, r0, r2.z
add_pp r1.w, -r3.x, c8.w
mul_pp r2.z, r3.x, r3.x
dp2add_pp r0.w, r2.z, r0.w, c11.x
mul_pp r2.z, r1.w, r1.w
mul_pp r2.z, r2.z, r2.z
mul_pp r1.w, r1.w, r2.z
texld r3, v0, s0
mov r4, c2
mad_pp r5.xyz, c5, r3, -r4
mul_pp r3.xyz, r3, c5
mad_pp r4.xyz, c6.x, r5, r4
lrp_pp r5.xyz, r1.w, c8.w, r4
mul_pp r1.xyz, r1, r5
mul_pp r1.w, r2.y, r2.y
mul_pp r1.w, r1.w, r1.w
mul_pp r1.w, r2.y, r1.w
mad_pp r1.w, r0.w, r1.w, c8.w
add_pp r2.y, -r2.x, c8.w
mul_pp r2.z, r2.y, r2.y
mul_pp r2.z, r2.z, r2.z
mul_pp r2.y, r2.y, r2.z
mad_pp r0.w, r0.w, r2.y, c8.w
mul_pp r0.w, r1.w, r0.w
mul_pp r0.w, r2.x, r0.w
mul_pp r0.xyz, r0.w, r0
mad_pp r0.w, c6.x, -r4.w, r4.w
mul_pp r2.xyz, r0.w, r3
mad_pp oC0.xyz, r2, r0, r1
mov_pp oC0.w, c8.w

"
}
}
 }
 Pass {
  Name "SHADOWCASTER"
  Tags { "LIGHTMODE"="SHADOWCASTER" "SHADOWSUPPORT"="true" "RenderType"="Opaque" "PerformanceChecks"="False" }
  GpuProgramID 133839
Program "vp" {
SubProgram "d3d9 " {
Keywords { "SHADOWS_DEPTH" }
Bind "vertex" Vertex
Bind "normal" Normal
Matrix 8 [_Object2World] 3
Matrix 11 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [unity_MatrixVP]
Vector 14 [_WorldSpaceLightPos0]
Vector 15 [unity_LightShadowBias]
"vs_3_0
def c16, 1, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord o0
dcl_position o1
abs r0.x, c15.z
slt r0.x, -r0.x, r0.x
mul r0.yzw, c12.xxyz, v1.y
mad r0.yzw, c11.xxyz, v1.x, r0
mad r0.yzw, c13.xxyz, v1.z, r0
nrm r1.xyz, r0.yzww
mad r2, v0.xyzx, c16.xxxy, c16.yyyx
dp4 r3.x, c8, r2
dp4 r3.y, c9, r2
dp4 r3.z, c10, r2
mad r0.yzw, r3.xxyz, -c14.w, c14.xxyz
nrm r4.xyz, r0.yzww
dp3 r0.y, r1, r4
mad r0.y, r0.y, -r0.y, c16.x
rsq r0.y, r0.y
rcp r0.y, r0.y
mul r0.y, r0.y, c15.z
mad r1.xyz, r1, -r0.y, r3
mov r1.w, c16.x
dp4 r3.x, c4, r1
dp4 r3.y, c5, r1
dp4 r3.z, c6, r1
dp4 r3.w, c7, r1
dp4 r1.x, c0, r2
dp4 r1.y, c1, r2
dp4 r1.z, c2, r2
dp4 r1.w, c3, r2
lrp r2, r0.x, r3, r1
rcp r0.x, r2.w
mul_sat r0.x, r0.x, c15.x
add r0.x, r0.x, r2.z
max r0.y, r0.x, c16.y
lrp r2.z, c15.y, r0.y, r0.x
mov o0, r2
mov o1, r2

"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_CUBE" }
Bind "vertex" Vertex
Matrix 4 [_Object2World] 3
Matrix 0 [glstate_matrix_mvp]
Vector 7 [_LightPositionRange]
"vs_3_0
dcl_position v0
dcl_texcoord o0.xyz
dcl_position o1
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o0.xyz, r0, -c7
dp4 o1.x, c0, v0
dp4 o1.y, c1, v0
dp4 o1.z, c2, v0
dp4 o1.w, c3, v0

"
}
}
Program "fp" {
SubProgram "d3d9 " {
Keywords { "SHADOWS_DEPTH" }
"ps_3_0
dcl_texcoord v0.zw
rcp r0.x, v0.w
mul_pp oC0, r0.x, v0.z

"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_CUBE" }
Vector 0 [_LightPositionRange]
"ps_3_0
dcl_texcoord v0.xyz
dp3 r0.x, v0, v0
rsq r0.x, r0.x
rcp r0.x, r0.x
mul_pp oC0, r0.x, c0.w

"
}
}
 }
 Pass {
  Name "DEFERRED"
  Tags { "LIGHTMODE"="Deferred" "RenderType"="Opaque" "PerformanceChecks"="False" }
  GpuProgramID 200435
Program "vp" {
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 16 [_DetailAlbedoMap_ST]
Vector 15 [_MainTex_ST]
Float 17 [_UVSec]
Vector 10 [_WorldSpaceCameraPos]
Vector 13 [unity_SHBb]
Vector 12 [unity_SHBg]
Vector 11 [unity_SHBr]
Vector 14 [unity_SHC]
"vs_3_0
def c18, 0, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord6 o7.xyz
dp4 o0.x, c0, v0
dp4 o0.y, c1, v0
dp4 o0.z, c2, v0
dp4 o0.w, c3, v0
mad o1.xy, v2, c15, c15.zwzw
abs r0.x, c17.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c16.xyxy, c16
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c10
mov o7.xyz, r0
mul r0.xyz, c8, v1.y
mad r0.xyz, c7, v1.x, r0
mad r0.xyz, c9, v1.z, r0
nrm r1.xyz, r0
mul r0.x, r1.y, r1.y
mad r0.x, r1.x, r1.x, -r0.x
mul r2, r1.yzzx, r1.xyzz
mov o5.xyz, r1
dp4 r1.x, c11, r2
dp4 r1.y, c12, r2
dp4 r1.z, c13, r2
mad o6.xyz, c14, r0.x, r1
mov o3, c18.x
mov o4, c18.x
mov o5.w, c18.x
mov o6.w, c18.x

"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "texcoord2" TexCoord2
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 13 [_DetailAlbedoMap_ST]
Vector 12 [_MainTex_ST]
Float 14 [_UVSec]
Vector 10 [_WorldSpaceCameraPos]
Vector 11 [unity_DynamicLightmapST]
"vs_3_0
def c15, 0, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_texcoord2 v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord6 o7.xyz
dp4 o0.x, c0, v0
dp4 o0.y, c1, v0
dp4 o0.z, c2, v0
dp4 o0.w, c3, v0
mad o1.xy, v2, c12, c12.zwzw
abs r0.x, c14.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c13.xyxy, c13
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c10
mov o7.xyz, r0
mul r0.xyz, c8, v1.y
mad r0.xyz, c7, v1.x, r0
mad r0.xyz, c9, v1.z, r0
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul o5.xyz, r0.w, r0
mad o6.zw, v4.xyxy, c11.xyxy, c11
mov o3, c15.x
mov o4, c15.x
mov o5.w, c15.x
mov o6.xy, c15.x

"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "UNITY_HDR_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 16 [_DetailAlbedoMap_ST]
Vector 15 [_MainTex_ST]
Float 17 [_UVSec]
Vector 10 [_WorldSpaceCameraPos]
Vector 13 [unity_SHBb]
Vector 12 [unity_SHBg]
Vector 11 [unity_SHBr]
Vector 14 [unity_SHC]
"vs_3_0
def c18, 0, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord6 o7.xyz
dp4 o0.x, c0, v0
dp4 o0.y, c1, v0
dp4 o0.z, c2, v0
dp4 o0.w, c3, v0
mad o1.xy, v2, c15, c15.zwzw
abs r0.x, c17.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c16.xyxy, c16
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c10
mov o7.xyz, r0
mul r0.xyz, c8, v1.y
mad r0.xyz, c7, v1.x, r0
mad r0.xyz, c9, v1.z, r0
nrm r1.xyz, r0
mul r0.x, r1.y, r1.y
mad r0.x, r1.x, r1.x, -r0.x
mul r2, r1.yzzx, r1.xyzz
mov o5.xyz, r1
dp4 r1.x, c11, r2
dp4 r1.y, c12, r2
dp4 r1.z, c13, r2
mad o6.xyz, c14, r0.x, r1
mov o3, c18.x
mov o4, c18.x
mov o5.w, c18.x
mov o6.w, c18.x

"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" "UNITY_HDR_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "texcoord2" TexCoord2
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 13 [_DetailAlbedoMap_ST]
Vector 12 [_MainTex_ST]
Float 14 [_UVSec]
Vector 10 [_WorldSpaceCameraPos]
Vector 11 [unity_DynamicLightmapST]
"vs_3_0
def c15, 0, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_texcoord2 v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord6 o7.xyz
dp4 o0.x, c0, v0
dp4 o0.y, c1, v0
dp4 o0.z, c2, v0
dp4 o0.w, c3, v0
mad o1.xy, v2, c12, c12.zwzw
abs r0.x, c14.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c13.xyxy, c13
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c10
mov o7.xyz, r0
mul r0.xyz, c8, v1.y
mad r0.xyz, c7, v1.x, r0
mad r0.xyz, c9, v1.z, r0
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul o5.xyz, r0.w, r0
mad o6.zw, v4.xyxy, c11.xyxy, c11
mov o3, c15.x
mov o4, c15.x
mov o5.w, c15.x
mov o6.xy, c15.x

"
}
}
Program "fp" {
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
Vector 12 [_Color]
Float 14 [_Glossiness]
Float 13 [_Metallic]
Float 15 [_OcclusionStrength]
Vector 11 [unity_ColorSpaceDielectricSpec]
Vector 2 [unity_SHAb]
Vector 1 [unity_SHAg]
Vector 0 [unity_SHAr]
Vector 3 [unity_SpecCube0_BoxMax]
Vector 4 [unity_SpecCube0_BoxMin]
Vector 6 [unity_SpecCube0_HDR]
Vector 5 [unity_SpecCube0_ProbePosition]
Vector 7 [unity_SpecCube1_BoxMax]
Vector 8 [unity_SpecCube1_BoxMin]
Vector 10 [unity_SpecCube1_HDR]
Vector 9 [unity_SpecCube1_ProbePosition]
SetTexture 0 [unity_SpecCube0] CUBE 0
SetTexture 1 [unity_SpecCube1] CUBE 1
SetTexture 2 [_MainTex] 2D 2
SetTexture 3 [_OcclusionMap] 2D 3
"ps_3_0
def c16, 7, 0.999989986, 0, 0
def c17, 1, 0, 0.5, 0.75
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord4_pp v2.xyz
dcl_texcoord5_pp v3.xyz
dcl_texcoord6_pp v4.xyz
dcl_cube s0
dcl_cube s1
dcl_2d s2
dcl_2d s3
nrm_pp r0.xyz, v2
nrm_pp r1.xyz, v1
texld r2, v0, s2
mul_pp r3.xyz, r2, c12
mov r4, c11
mad_pp r2.xyz, c12, r2, -r4
mad_pp r2.xyz, c13.x, r2, r4
mad_pp r1.w, c13.x, -r4.w, r4.w
mul_pp r3.xyz, r1.w, r3
texld_pp r4, v0, s3
mov r5.xyz, c17
add_pp r2.w, r5.x, -c15.x
mad_pp r3.w, r4.y, c15.x, r2.w
mov r0.w, c17.x
dp4_pp r4.x, c0, r0
dp4_pp r4.y, c1, r0
dp4_pp r4.z, c2, r0
add_pp r4.xyz, r4, v3
mul_pp r4.xyz, r3.w, r4
dp3 r2.w, r1, r0
add r2.w, r2.w, r2.w
mad_pp r6.xyz, r0, -r2.w, r1
if_lt -c5.w, r5.y
nrm_pp r7.xyz, r6
add r8.xyz, c3, -v4
rcp r9.x, r7.x
rcp r9.y, r7.y
rcp r9.z, r7.z
mul_pp r8.xyz, r8, r9
add r10.xyz, c4, -v4
mul_pp r9.xyz, r9, r10
cmp_pp r8.xyz, -r7, r9, r8
min_pp r2.w, r8.y, r8.x
min_pp r4.w, r8.z, r2.w
mov r8.xyz, c4
add r8.xyz, r8, c3
mad r9.xyz, r8, r5.z, -c5
add r9.xyz, r9, v4
mad r7.xyz, r7, r4.w, r9
mad_pp r7.xyz, r8, -c17.z, r7
else
mov_pp r7.xyz, r6
endif
add_pp r2.w, r5.x, -c14.x
pow_pp r4.w, r2.w, c17.w
mul_pp r7.w, r4.w, c16.x
texldl_pp r8, r7, s0
pow_pp r2.w, r8.w, c6.y
mul_pp r2.w, r2.w, c6.x
mul_pp r9.xyz, r8, r2.w
mov r4.w, c4.w
if_lt r4.w, c16.y
if_lt -c9.w, r5.y
nrm_pp r10.xyz, r6
add r5.xyw, c7.xyzz, -v4.xyzz
rcp r11.x, r10.x
rcp r11.y, r10.y
rcp r11.z, r10.z
mul_pp r5.xyw, r5, r11.xyzz
add r12.xyz, c8, -v4
mul_pp r11.xyz, r11, r12
cmp_pp r5.xyw, -r10.xyzz, r11.xyzz, r5
min_pp r4.w, r5.y, r5.x
min_pp r6.w, r5.w, r4.w
mov r11.xyz, c7
add r5.xyw, r11.xyzz, c8.xyzz
mad r11.xyz, r5.xyww, r5.z, -c9
add r11.xyz, r11, v4
mad r10.xyz, r10, r6.w, r11
mad_pp r7.xyz, r5.xyww, -c17.z, r10
else
mov_pp r7.xyz, r6
endif
texldl_pp r5, r7, s1
pow_pp r4.w, r5.w, c10.y
mul_pp r4.w, r4.w, c10.x
mul_pp r5.xyz, r5, r4.w
mad r6.xyz, r2.w, r8, -r5
mad_pp r9.xyz, c4.w, r6, r5
endif
mul_pp r5.xyz, r3.w, r9
dp3_pp r1.x, r0, -r1
add_pp r1.yz, -r1.xwxw, c17.x
add_sat_pp r1.y, r1.y, c14.x
cmp_pp r1.x, r1.x, r1.z, c17.x
mul_pp r1.z, r1.x, r1.x
mul_pp r1.z, r1.z, r1.z
mul_pp r1.x, r1.x, r1.z
lrp_pp r6.xyz, r1.x, r1.y, r2
mul_pp r1.xyz, r5, r6
mad_pp r1.xyz, r3, r4, r1
exp_pp oC3.x, -r1.x
exp_pp oC3.y, -r1.y
exp_pp oC3.z, -r1.z
mov_pp oC0, r3
mov_pp oC1.w, c14.x
mov_pp oC1.xyz, r2
mad_pp oC2, r0, c17.zzzx, c17.zzzy
mov_pp oC3.w, c17.x

"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" }
Vector 10 [_Color]
Float 12 [_Glossiness]
Float 11 [_Metallic]
Float 13 [_OcclusionStrength]
Vector 8 [unity_ColorSpaceDielectricSpec]
Vector 9 [unity_DynamicLightmap_HDR]
Vector 0 [unity_SpecCube0_BoxMax]
Vector 1 [unity_SpecCube0_BoxMin]
Vector 3 [unity_SpecCube0_HDR]
Vector 2 [unity_SpecCube0_ProbePosition]
Vector 4 [unity_SpecCube1_BoxMax]
Vector 5 [unity_SpecCube1_BoxMin]
Vector 7 [unity_SpecCube1_HDR]
Vector 6 [unity_SpecCube1_ProbePosition]
SetTexture 0 [unity_DynamicLightmap] 2D 0
SetTexture 1 [unity_SpecCube0] CUBE 1
SetTexture 2 [unity_SpecCube1] CUBE 2
SetTexture 3 [_MainTex] 2D 3
SetTexture 4 [_OcclusionMap] 2D 4
"ps_3_0
def c14, 7, 0.999989986, 0, 0
def c15, 1, 0, 0.5, 0.75
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord4_pp v2.xyz
dcl_texcoord5_pp v3.zw
dcl_texcoord6_pp v4.xyz
dcl_2d s0
dcl_cube s1
dcl_cube s2
dcl_2d s3
dcl_2d s4
nrm_pp r0.xyz, v2
nrm_pp r1.xyz, v1
texld r2, v0, s3
mul_pp r3.xyz, r2, c10
mov r4, c8
mad_pp r2.xyz, c10, r2, -r4
mad_pp r2.xyz, c11.x, r2, r4
mad_pp r0.w, c11.x, -r4.w, r4.w
mul_pp r3.xyz, r0.w, r3
texld_pp r4, v0, s4
mov r5.xyz, c15
add_pp r1.w, r5.x, -c13.x
mad_pp r3.w, r4.y, c13.x, r1.w
texld_pp r4, v3.zwzw, s0
mul_pp r1.w, r4.w, c9.x
mul_pp r4.xyz, r4, r1.w
log_pp r6.x, r4.x
log_pp r6.y, r4.y
log_pp r6.z, r4.z
mul_pp r4.xyz, r6, c9.y
exp_pp r6.x, r4.x
exp_pp r6.y, r4.y
exp_pp r6.z, r4.z
mul_pp r4.xyz, r3.w, r6
dp3 r1.w, r1, r0
add r1.w, r1.w, r1.w
mad_pp r6.xyz, r0, -r1.w, r1
if_lt -c2.w, r5.y
nrm_pp r7.xyz, r6
add r8.xyz, c0, -v4
rcp r9.x, r7.x
rcp r9.y, r7.y
rcp r9.z, r7.z
mul_pp r8.xyz, r8, r9
add r10.xyz, c1, -v4
mul_pp r9.xyz, r9, r10
cmp_pp r8.xyz, -r7, r9, r8
min_pp r1.w, r8.y, r8.x
min_pp r2.w, r8.z, r1.w
mov r8.xyz, c1
add r8.xyz, r8, c0
mad r9.xyz, r8, r5.z, -c2
add r9.xyz, r9, v4
mad r7.xyz, r7, r2.w, r9
mad_pp r7.xyz, r8, -c15.z, r7
else
mov_pp r7.xyz, r6
endif
add_pp r1.w, r5.x, -c12.x
pow_pp r2.w, r1.w, c15.w
mul_pp r7.w, r2.w, c14.x
texldl_pp r8, r7, s1
pow_pp r1.w, r8.w, c3.y
mul_pp r1.w, r1.w, c3.x
mul_pp r9.xyz, r8, r1.w
mov r2.w, c1.w
if_lt r2.w, c14.y
if_lt -c6.w, r5.y
nrm_pp r10.xyz, r6
add r5.xyw, c4.xyzz, -v4.xyzz
rcp r11.x, r10.x
rcp r11.y, r10.y
rcp r11.z, r10.z
mul_pp r5.xyw, r5, r11.xyzz
add r12.xyz, c5, -v4
mul_pp r11.xyz, r11, r12
cmp_pp r5.xyw, -r10.xyzz, r11.xyzz, r5
min_pp r2.w, r5.y, r5.x
min_pp r4.w, r5.w, r2.w
mov r11.xyz, c4
add r5.xyw, r11.xyzz, c5.xyzz
mad r11.xyz, r5.xyww, r5.z, -c6
add r11.xyz, r11, v4
mad r10.xyz, r10, r4.w, r11
mad_pp r7.xyz, r5.xyww, -c15.z, r10
else
mov_pp r7.xyz, r6
endif
texldl_pp r5, r7, s2
pow_pp r2.w, r5.w, c7.y
mul_pp r2.w, r2.w, c7.x
mul_pp r5.xyz, r5, r2.w
mad r6.xyz, r1.w, r8, -r5
mad_pp r9.xyz, c1.w, r6, r5
endif
mul_pp r5.xyz, r3.w, r9
dp3_pp r1.x, r0, -r1
add_pp r0.w, -r0.w, c15.x
add_sat_pp r0.w, r0.w, c12.x
add_pp r1.y, -r1.x, c15.x
cmp_pp r1.x, r1.x, r1.y, c15.x
mul_pp r1.y, r1.x, r1.x
mul_pp r1.y, r1.y, r1.y
mul_pp r1.x, r1.x, r1.y
lrp_pp r6.xyz, r1.x, r0.w, r2
mul_pp r1.xyz, r5, r6
mad_pp r1.xyz, r3, r4, r1
exp_pp oC3.x, -r1.x
exp_pp oC3.y, -r1.y
exp_pp oC3.z, -r1.z
mad_pp oC2.xyz, r0, c15.z, c15.z
mov_pp oC0, r3
mov_pp oC1.w, c12.x
mov_pp oC1.xyz, r2
mov_pp oC2.w, c15.x
mov_pp oC3.w, c15.x

"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "UNITY_HDR_ON" }
Vector 12 [_Color]
Float 14 [_Glossiness]
Float 13 [_Metallic]
Float 15 [_OcclusionStrength]
Vector 11 [unity_ColorSpaceDielectricSpec]
Vector 2 [unity_SHAb]
Vector 1 [unity_SHAg]
Vector 0 [unity_SHAr]
Vector 3 [unity_SpecCube0_BoxMax]
Vector 4 [unity_SpecCube0_BoxMin]
Vector 6 [unity_SpecCube0_HDR]
Vector 5 [unity_SpecCube0_ProbePosition]
Vector 7 [unity_SpecCube1_BoxMax]
Vector 8 [unity_SpecCube1_BoxMin]
Vector 10 [unity_SpecCube1_HDR]
Vector 9 [unity_SpecCube1_ProbePosition]
SetTexture 0 [unity_SpecCube0] CUBE 0
SetTexture 1 [unity_SpecCube1] CUBE 1
SetTexture 2 [_MainTex] 2D 2
SetTexture 3 [_OcclusionMap] 2D 3
"ps_3_0
def c16, 7, 0.999989986, 0, 0
def c17, 1, 0, 0.5, 0.75
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord4_pp v2.xyz
dcl_texcoord5_pp v3.xyz
dcl_texcoord6_pp v4.xyz
dcl_cube s0
dcl_cube s1
dcl_2d s2
dcl_2d s3
nrm_pp r0.xyz, v2
nrm_pp r1.xyz, v1
texld r2, v0, s2
mul_pp r3.xyz, r2, c12
mov r4, c11
mad_pp r2.xyz, c12, r2, -r4
mad_pp r2.xyz, c13.x, r2, r4
mad_pp r1.w, c13.x, -r4.w, r4.w
mul_pp r3.xyz, r1.w, r3
texld_pp r4, v0, s3
mov r5.xyz, c17
add_pp r2.w, r5.x, -c15.x
mad_pp r3.w, r4.y, c15.x, r2.w
mov r0.w, c17.x
dp4_pp r4.x, c0, r0
dp4_pp r4.y, c1, r0
dp4_pp r4.z, c2, r0
add_pp r4.xyz, r4, v3
mul_pp r4.xyz, r3.w, r4
dp3 r2.w, r1, r0
add r2.w, r2.w, r2.w
mad_pp r6.xyz, r0, -r2.w, r1
if_lt -c5.w, r5.y
nrm_pp r7.xyz, r6
add r8.xyz, c3, -v4
rcp r9.x, r7.x
rcp r9.y, r7.y
rcp r9.z, r7.z
mul_pp r8.xyz, r8, r9
add r10.xyz, c4, -v4
mul_pp r9.xyz, r9, r10
cmp_pp r8.xyz, -r7, r9, r8
min_pp r2.w, r8.y, r8.x
min_pp r4.w, r8.z, r2.w
mov r8.xyz, c4
add r8.xyz, r8, c3
mad r9.xyz, r8, r5.z, -c5
add r9.xyz, r9, v4
mad r7.xyz, r7, r4.w, r9
mad_pp r7.xyz, r8, -c17.z, r7
else
mov_pp r7.xyz, r6
endif
add_pp r2.w, r5.x, -c14.x
pow_pp r4.w, r2.w, c17.w
mul_pp r7.w, r4.w, c16.x
texldl_pp r8, r7, s0
pow_pp r2.w, r8.w, c6.y
mul_pp r2.w, r2.w, c6.x
mul_pp r9.xyz, r8, r2.w
mov r4.w, c4.w
if_lt r4.w, c16.y
if_lt -c9.w, r5.y
nrm_pp r10.xyz, r6
add r5.xyw, c7.xyzz, -v4.xyzz
rcp r11.x, r10.x
rcp r11.y, r10.y
rcp r11.z, r10.z
mul_pp r5.xyw, r5, r11.xyzz
add r12.xyz, c8, -v4
mul_pp r11.xyz, r11, r12
cmp_pp r5.xyw, -r10.xyzz, r11.xyzz, r5
min_pp r4.w, r5.y, r5.x
min_pp r6.w, r5.w, r4.w
mov r11.xyz, c7
add r5.xyw, r11.xyzz, c8.xyzz
mad r11.xyz, r5.xyww, r5.z, -c9
add r11.xyz, r11, v4
mad r10.xyz, r10, r6.w, r11
mad_pp r7.xyz, r5.xyww, -c17.z, r10
else
mov_pp r7.xyz, r6
endif
texldl_pp r5, r7, s1
pow_pp r4.w, r5.w, c10.y
mul_pp r4.w, r4.w, c10.x
mul_pp r5.xyz, r5, r4.w
mad r6.xyz, r2.w, r8, -r5
mad_pp r9.xyz, c4.w, r6, r5
endif
mul_pp r5.xyz, r3.w, r9
dp3_pp r1.x, r0, -r1
add_pp r1.yz, -r1.xwxw, c17.x
add_sat_pp r1.y, r1.y, c14.x
cmp_pp r1.x, r1.x, r1.z, c17.x
mul_pp r1.z, r1.x, r1.x
mul_pp r1.z, r1.z, r1.z
mul_pp r1.x, r1.x, r1.z
lrp_pp r6.xyz, r1.x, r1.y, r2
mul_pp r1.xyz, r5, r6
mad_pp oC3.xyz, r3, r4, r1
mov_pp oC0, r3
mov_pp oC1.w, c14.x
mov_pp oC1.xyz, r2
mad_pp oC2, r0, c17.zzzx, c17.zzzy
mov_pp oC3.w, c17.x

"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" "UNITY_HDR_ON" }
Vector 10 [_Color]
Float 12 [_Glossiness]
Float 11 [_Metallic]
Float 13 [_OcclusionStrength]
Vector 8 [unity_ColorSpaceDielectricSpec]
Vector 9 [unity_DynamicLightmap_HDR]
Vector 0 [unity_SpecCube0_BoxMax]
Vector 1 [unity_SpecCube0_BoxMin]
Vector 3 [unity_SpecCube0_HDR]
Vector 2 [unity_SpecCube0_ProbePosition]
Vector 4 [unity_SpecCube1_BoxMax]
Vector 5 [unity_SpecCube1_BoxMin]
Vector 7 [unity_SpecCube1_HDR]
Vector 6 [unity_SpecCube1_ProbePosition]
SetTexture 0 [unity_DynamicLightmap] 2D 0
SetTexture 1 [unity_SpecCube0] CUBE 1
SetTexture 2 [unity_SpecCube1] CUBE 2
SetTexture 3 [_MainTex] 2D 3
SetTexture 4 [_OcclusionMap] 2D 4
"ps_3_0
def c14, 7, 0.999989986, 0, 0
def c15, 1, 0, 0.5, 0.75
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord4_pp v2.xyz
dcl_texcoord5_pp v3.zw
dcl_texcoord6_pp v4.xyz
dcl_2d s0
dcl_cube s1
dcl_cube s2
dcl_2d s3
dcl_2d s4
nrm_pp r0.xyz, v2
nrm_pp r1.xyz, v1
texld r2, v0, s3
mul_pp r3.xyz, r2, c10
mov r4, c8
mad_pp r2.xyz, c10, r2, -r4
mad_pp r2.xyz, c11.x, r2, r4
mad_pp r0.w, c11.x, -r4.w, r4.w
mul_pp r3.xyz, r0.w, r3
texld_pp r4, v0, s4
mov r5.xyz, c15
add_pp r1.w, r5.x, -c13.x
mad_pp r3.w, r4.y, c13.x, r1.w
texld_pp r4, v3.zwzw, s0
mul_pp r1.w, r4.w, c9.x
mul_pp r4.xyz, r4, r1.w
log_pp r6.x, r4.x
log_pp r6.y, r4.y
log_pp r6.z, r4.z
mul_pp r4.xyz, r6, c9.y
exp_pp r6.x, r4.x
exp_pp r6.y, r4.y
exp_pp r6.z, r4.z
mul_pp r4.xyz, r3.w, r6
dp3 r1.w, r1, r0
add r1.w, r1.w, r1.w
mad_pp r6.xyz, r0, -r1.w, r1
if_lt -c2.w, r5.y
nrm_pp r7.xyz, r6
add r8.xyz, c0, -v4
rcp r9.x, r7.x
rcp r9.y, r7.y
rcp r9.z, r7.z
mul_pp r8.xyz, r8, r9
add r10.xyz, c1, -v4
mul_pp r9.xyz, r9, r10
cmp_pp r8.xyz, -r7, r9, r8
min_pp r1.w, r8.y, r8.x
min_pp r2.w, r8.z, r1.w
mov r8.xyz, c1
add r8.xyz, r8, c0
mad r9.xyz, r8, r5.z, -c2
add r9.xyz, r9, v4
mad r7.xyz, r7, r2.w, r9
mad_pp r7.xyz, r8, -c15.z, r7
else
mov_pp r7.xyz, r6
endif
add_pp r1.w, r5.x, -c12.x
pow_pp r2.w, r1.w, c15.w
mul_pp r7.w, r2.w, c14.x
texldl_pp r8, r7, s1
pow_pp r1.w, r8.w, c3.y
mul_pp r1.w, r1.w, c3.x
mul_pp r9.xyz, r8, r1.w
mov r2.w, c1.w
if_lt r2.w, c14.y
if_lt -c6.w, r5.y
nrm_pp r10.xyz, r6
add r5.xyw, c4.xyzz, -v4.xyzz
rcp r11.x, r10.x
rcp r11.y, r10.y
rcp r11.z, r10.z
mul_pp r5.xyw, r5, r11.xyzz
add r12.xyz, c5, -v4
mul_pp r11.xyz, r11, r12
cmp_pp r5.xyw, -r10.xyzz, r11.xyzz, r5
min_pp r2.w, r5.y, r5.x
min_pp r4.w, r5.w, r2.w
mov r11.xyz, c4
add r5.xyw, r11.xyzz, c5.xyzz
mad r11.xyz, r5.xyww, r5.z, -c6
add r11.xyz, r11, v4
mad r10.xyz, r10, r4.w, r11
mad_pp r7.xyz, r5.xyww, -c15.z, r10
else
mov_pp r7.xyz, r6
endif
texldl_pp r5, r7, s2
pow_pp r2.w, r5.w, c7.y
mul_pp r2.w, r2.w, c7.x
mul_pp r5.xyz, r5, r2.w
mad r6.xyz, r1.w, r8, -r5
mad_pp r9.xyz, c1.w, r6, r5
endif
mul_pp r5.xyz, r3.w, r9
dp3_pp r1.x, r0, -r1
add_pp r0.w, -r0.w, c15.x
add_sat_pp r0.w, r0.w, c12.x
add_pp r1.y, -r1.x, c15.x
cmp_pp r1.x, r1.x, r1.y, c15.x
mul_pp r1.y, r1.x, r1.x
mul_pp r1.y, r1.y, r1.y
mul_pp r1.x, r1.x, r1.y
lrp_pp r6.xyz, r1.x, r0.w, r2
mul_pp r1.xyz, r5, r6
mad_pp oC3.xyz, r3, r4, r1
mad_pp oC2.xyz, r0, c15.z, c15.z
mov_pp oC0, r3
mov_pp oC1.w, c12.x
mov_pp oC1.xyz, r2
mov_pp oC2.w, c15.x
mov_pp oC3.w, c15.x

"
}
}
 }
 Pass {
  Name "META"
  Tags { "LIGHTMODE"="Meta" "RenderType"="Opaque" "PerformanceChecks"="False" }
  Cull Off
  GpuProgramID 273865
Program "vp" {
SubProgram "d3d9 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "texcoord2" TexCoord2
Matrix 0 [glstate_matrix_mvp]
Vector 7 [_DetailAlbedoMap_ST]
Vector 6 [_MainTex_ST]
Float 8 [_UVSec]
Vector 5 [unity_DynamicLightmapST]
Vector 4 [unity_LightmapST]
Vector 9 [unity_MetaVertexControl]
"vs_2_0
def c10, 0, 9.99999975e-005, 0, 0
dcl_position v0
dcl_texcoord v1
dcl_texcoord1 v2
dcl_texcoord2 v3
slt r0.x, c10.x, v0.z
mul r0.z, r0.x, c10.y
mad r0.xy, v2, c4, c4.zwzw
lrp r1.xyz, c9.x, r0, v0
slt r0.x, c10.x, r1.z
mul r0.z, r0.x, c10.y
mad r0.xy, v3, c5, c5.zwzw
lrp r2.xyz, c9.y, r0, r1
mov r2.w, v0.w
dp4 oPos.x, c0, r2
dp4 oPos.y, c1, r2
dp4 oPos.z, c2, r2
dp4 oPos.w, c3, r2
mad oT0.xy, v1, c6, c6.zwzw
mul r0.x, c8.x, c8.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
add r0.yz, -r1.xxyw, v1.xxyw
mad r0.xy, r0.x, r0.yzzw, v2
mad oT0.zw, r0.xyxy, c7.xyxy, c7

"
}
}
Program "fp" {
SubProgram "d3d9 " {
Vector 1 [_Color]
Float 3 [_Glossiness]
Float 2 [_Metallic]
Vector 0 [unity_ColorSpaceDielectricSpec]
Float 6 [unity_MaxOutputValue]
Vector 4 [unity_MetaFragmentControl]
Float 5 [unity_OneOverOutputBoost]
SetTexture 0 [_MainTex] 2D 0
"ps_2_0
def c7, 1, 0.5, 0, 0
def c8, 0, 0, 0, 0.0199999996
dcl t0.xy
dcl_2d s0
texld r0, t0, s0
mov r1.xz, c7
add_pp r0.w, r1.x, -c3.x
mul_pp r0.w, r0.w, r0.w
mov r2, c0
mad_pp r3.xyz, c1, r0, -r2
mul_pp r0.xyz, r0, c1
mad_pp r2.xyz, c2.x, r3, r2
mul_pp r2.xyz, r0.w, r2
mul_pp r2.xyz, r2, c7.y
mad_pp r0.w, c2.x, -r2.w, r2.w
mad_pp r0.xyz, r0, r0.w, r2
log r2.x, r0.x
log r2.y, r0.y
log r2.z, r0.z
mov_sat r2.w, c5.x
mul r0.xyz, r2, r2.w
exp_pp r2.x, r0.x
exp_pp r2.y, r0.y
exp_pp r2.z, r0.z
min_pp r0.xyz, c6.x, r2
cmp_pp r0.xyz, -c4.x, r1.z, r0
mov r0.w, c4.x
mov r1.y, c4.y
cmp_pp r0, -r1.y, r0, c8
mov_pp oC0, r0

"
}
}
 }
}
SubShader { 
 LOD 150
 Tags { "RenderType"="Opaque" "PerformanceChecks"="False" }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardBase" "SHADOWSUPPORT"="true" "RenderType"="Opaque" "PerformanceChecks"="False" }
  ZWrite [_ZWrite]
  Blend [_SrcBlend] [_DstBlend]
  GpuProgramID 374778
Program "vp" {
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 19 [_DetailAlbedoMap_ST]
Vector 18 [_MainTex_ST]
Float 20 [_UVSec]
Vector 10 [_WorldSpaceCameraPos]
Vector 13 [unity_SHAb]
Vector 12 [unity_SHAg]
Vector 11 [unity_SHAr]
Vector 16 [unity_SHBb]
Vector 15 [unity_SHBg]
Vector 14 [unity_SHBr]
Vector 17 [unity_SHC]
"vs_2_0
def c21, 1, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dp4 oPos.x, c0, v0
dp4 oPos.y, c1, v0
dp4 oPos.z, c2, v0
dp4 oPos.w, c3, v0
mad oT0.xy, v2, c18, c18.zwzw
mul r0.x, c20.x, c20.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad oT0.zw, r2.xyxy, c19.xyxy, c19
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add r0.xyz, r0, -c10
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul oT1.xyz, r0.w, r0
mul r0.xyz, v1.y, c8
mad r0.xyz, c7, v1.x, r0
mad r0.xyz, c9, v1.z, r0
nrm r1.xyz, r0
mul r0.x, r1.y, r1.y
mad r0.x, r1.x, r1.x, -r0.x
mul r2, r1.yzzx, r1.xyzz
dp4 r3.x, c14, r2
dp4 r3.y, c15, r2
dp4 r3.z, c16, r2
mad r0.xyz, c17, r0.x, r3
mov r1.w, c21.x
dp4 r2.x, c11, r1
dp4 r2.y, c12, r1
dp4 r2.z, c13, r1
mov oT4.xyz, r1
add oT5.xyz, r0, r2
mov oT2, c21.y
mov oT3, c21.y
mov oT4.w, c21.y
mov oT5.w, c21.y

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "texcoord2" TexCoord2
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 13 [_DetailAlbedoMap_ST]
Vector 12 [_MainTex_ST]
Float 14 [_UVSec]
Vector 10 [_WorldSpaceCameraPos]
Vector 11 [unity_DynamicLightmapST]
"vs_3_0
def c15, 0, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_texcoord2 v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord8 o7.xyz
dp4 o0.x, c0, v0
dp4 o0.y, c1, v0
dp4 o0.z, c2, v0
dp4 o0.w, c3, v0
mad o1.xy, v2, c12, c12.zwzw
abs r0.x, c14.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c13.xyxy, c13
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c10
mov o7.xyz, r0
mul r0.xyz, c8, v1.y
mad r0.xyz, c7, v1.x, r0
mad r0.xyz, c9, v1.z, r0
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul o5.xyz, r0.w, r0
mad o6.zw, v4.xyxy, c11.xyxy, c11
mov o3, c15.x
mov o4, c15.x
mov o5.w, c15.x
mov o6.xy, c15.x

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 21 [_DetailAlbedoMap_ST]
Vector 20 [_MainTex_ST]
Vector 11 [_ProjectionParams]
Vector 12 [_ScreenParams]
Float 22 [_UVSec]
Vector 10 [_WorldSpaceCameraPos]
Vector 15 [unity_SHAb]
Vector 14 [unity_SHAg]
Vector 13 [unity_SHAr]
Vector 18 [unity_SHBb]
Vector 17 [unity_SHBg]
Vector 16 [unity_SHBr]
Vector 19 [unity_SHC]
"vs_2_0
def c23, 0.5, 1, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
mad oT0.xy, v2, c20, c20.zwzw
mul r0.x, c22.x, c22.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad oT0.zw, r2.xyxy, c21.xyxy, c21
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add r0.xyz, r0, -c10
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul oT1.xyz, r0.w, r0
dp4 r0.y, c1, v0
mul r1.x, r0.y, c11.x
mul r1.w, r1.x, c23.x
dp4 r0.x, c0, v0
dp4 r0.w, c3, v0
mul r1.xz, r0.xyww, c23.x
mad oT6.xy, r1.z, c12.zwzw, r1.xwzw
mul r1.xyz, v1.y, c8
mad r1.xyz, c7, v1.x, r1
mad r1.xyz, c9, v1.z, r1
nrm r2.xyz, r1
mul r1.x, r2.y, r2.y
mad r1.x, r2.x, r2.x, -r1.x
mul r3, r2.yzzx, r2.xyzz
dp4 r4.x, c16, r3
dp4 r4.y, c17, r3
dp4 r4.z, c18, r3
mad r1.xyz, c19, r1.x, r4
mov r2.w, c23.y
dp4 r3.x, c13, r2
dp4 r3.y, c14, r2
dp4 r3.z, c15, r2
mov oT4.xyz, r2
add oT5.xyz, r1, r3
dp4 r0.z, c2, v0
mov oPos, r0
mov oT6.zw, r0
mov oT2, c23.z
mov oT3, c23.z
mov oT4.w, c23.z
mov oT5.w, c23.z

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "texcoord2" TexCoord2
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 15 [_DetailAlbedoMap_ST]
Vector 14 [_MainTex_ST]
Vector 11 [_ProjectionParams]
Vector 12 [_ScreenParams]
Float 16 [_UVSec]
Vector 10 [_WorldSpaceCameraPos]
Vector 13 [unity_DynamicLightmapST]
"vs_3_0
def c17, 0.5, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_texcoord2 v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord6 o7
dcl_texcoord8 o8.xyz
mad o1.xy, v2, c14, c14.zwzw
abs r0.x, c16.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c15.xyxy, c15
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c10
mov o8.xyz, r0
mul r0.xyz, c8, v1.y
mad r0.xyz, c7, v1.x, r0
mad r0.xyz, c9, v1.z, r0
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul o5.xyz, r0.w, r0
dp4 r0.y, c1, v0
mul r1.x, r0.y, c11.x
mul r1.w, r1.x, c17.x
dp4 r0.x, c0, v0
dp4 r0.w, c3, v0
mul r1.xz, r0.xyww, c17.x
mad o7.xy, r1.z, c12.zwzw, r1.xwzw
mad o6.zw, v4.xyxy, c13.xyxy, c13
dp4 r0.z, c2, v0
mov o0, r0
mov o7.zw, r0
mov o3, c17.y
mov o4, c17.y
mov o5.w, c17.y
mov o6.xy, c17.y

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 8 [_Object2World] 3
Matrix 11 [_World2Object] 3
Matrix 4 [glstate_matrix_mvp]
Vector 27 [_DetailAlbedoMap_ST]
Vector 26 [_MainTex_ST]
Float 28 [_UVSec]
Vector 14 [_WorldSpaceCameraPos]
Vector 18 [unity_4LightAtten0]
Vector 15 [unity_4LightPosX0]
Vector 16 [unity_4LightPosY0]
Vector 17 [unity_4LightPosZ0]
Vector 0 [unity_LightColor0]
Vector 1 [unity_LightColor1]
Vector 2 [unity_LightColor2]
Vector 3 [unity_LightColor3]
Vector 21 [unity_SHAb]
Vector 20 [unity_SHAg]
Vector 19 [unity_SHAr]
Vector 24 [unity_SHBb]
Vector 23 [unity_SHBg]
Vector 22 [unity_SHBr]
Vector 25 [unity_SHC]
"vs_2_0
def c29, 1, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dp4 oPos.x, c4, v0
dp4 oPos.y, c5, v0
dp4 oPos.z, c6, v0
dp4 oPos.w, c7, v0
mad oT0.xy, v2, c26, c26.zwzw
mul r0.x, c28.x, c28.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad oT0.zw, r2.xyxy, c27.xyxy, c27
dp4 r0.x, c8, v0
dp4 r0.y, c9, v0
dp4 r0.z, c10, v0
add r1.xyz, r0, -c14
add r2, -r0.x, c15
add r3, -r0.y, c16
add r0, -r0.z, c17
dp3 r1.w, r1, r1
rsq r1.w, r1.w
mul oT1.xyz, r1.w, r1
mul r1, r3, r3
mad r1, r2, r2, r1
mad r1, r0, r0, r1
rsq r4.x, r1.x
rsq r4.y, r1.y
rsq r4.z, r1.z
rsq r4.w, r1.w
mov r5.x, c29.x
mad r1, r1, c18, r5.x
mul r5.xyz, v1.y, c12
mad r5.xyz, c11, v1.x, r5
mad r5.xyz, c13, v1.z, r5
nrm r6.xyz, r5
mul r3, r3, r6.y
mad r2, r2, r6.x, r3
mad r0, r0, r6.z, r2
mul r0, r4, r0
max r0, r0, c29.y
rcp r2.x, r1.x
rcp r2.y, r1.y
rcp r2.z, r1.z
rcp r2.w, r1.w
mul r0, r0, r2
mul r1.xyz, r0.y, c1
mad r1.xyz, c0, r0.x, r1
mad r0.xyz, c2, r0.z, r1
mad r0.xyz, c3, r0.w, r0
mul r0.w, r6.y, r6.y
mad r0.w, r6.x, r6.x, -r0.w
mul r1, r6.yzzx, r6.xyzz
dp4 r2.x, c22, r1
dp4 r2.y, c23, r1
dp4 r2.z, c24, r1
mad r1.xyz, c25, r0.w, r2
mov r6.w, c29.x
dp4 r2.x, c19, r6
dp4 r2.y, c20, r6
dp4 r2.z, c21, r6
mov oT4.xyz, r6
add r1.xyz, r1, r2
add oT5.xyz, r0, r1
mov oT2, c29.y
mov oT3, c29.y
mov oT4.w, c29.y
mov oT5.w, c29.y

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "texcoord2" TexCoord2
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 13 [_DetailAlbedoMap_ST]
Vector 12 [_MainTex_ST]
Float 14 [_UVSec]
Vector 10 [_WorldSpaceCameraPos]
Vector 11 [unity_DynamicLightmapST]
"vs_3_0
def c15, 0, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_texcoord2 v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord8 o7.xyz
dp4 o0.x, c0, v0
dp4 o0.y, c1, v0
dp4 o0.z, c2, v0
dp4 o0.w, c3, v0
mad o1.xy, v2, c12, c12.zwzw
abs r0.x, c14.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c13.xyxy, c13
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c10
mov o7.xyz, r0
mul r0.xyz, c8, v1.y
mad r0.xyz, c7, v1.x, r0
mad r0.xyz, c9, v1.z, r0
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul o5.xyz, r0.w, r0
mad o6.zw, v4.xyxy, c11.xyxy, c11
mov o3, c15.x
mov o4, c15.x
mov o5.w, c15.x
mov o6.xy, c15.x

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 8 [_Object2World] 3
Matrix 11 [_World2Object] 3
Matrix 4 [glstate_matrix_mvp]
Vector 29 [_DetailAlbedoMap_ST]
Vector 28 [_MainTex_ST]
Vector 15 [_ProjectionParams]
Vector 16 [_ScreenParams]
Float 30 [_UVSec]
Vector 14 [_WorldSpaceCameraPos]
Vector 20 [unity_4LightAtten0]
Vector 17 [unity_4LightPosX0]
Vector 18 [unity_4LightPosY0]
Vector 19 [unity_4LightPosZ0]
Vector 0 [unity_LightColor0]
Vector 1 [unity_LightColor1]
Vector 2 [unity_LightColor2]
Vector 3 [unity_LightColor3]
Vector 23 [unity_SHAb]
Vector 22 [unity_SHAg]
Vector 21 [unity_SHAr]
Vector 26 [unity_SHBb]
Vector 25 [unity_SHBg]
Vector 24 [unity_SHBr]
Vector 27 [unity_SHC]
"vs_2_0
def c31, 0.5, 1, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
mad oT0.xy, v2, c28, c28.zwzw
mul r0.x, c30.x, c30.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad oT0.zw, r2.xyxy, c29.xyxy, c29
dp4 r0.x, c8, v0
dp4 r0.y, c9, v0
dp4 r0.z, c10, v0
add r1.xyz, r0, -c14
add r2, -r0.x, c17
add r3, -r0.y, c18
add r0, -r0.z, c19
dp3 r1.w, r1, r1
rsq r1.w, r1.w
mul oT1.xyz, r1.w, r1
dp4 r1.y, c5, v0
mul r4.x, r1.y, c15.x
mul r4.w, r4.x, c31.x
dp4 r1.x, c4, v0
dp4 r1.w, c7, v0
mul r4.xz, r1.xyww, c31.x
mad oT6.xy, r4.z, c16.zwzw, r4.xwzw
mul r4, r3, r3
mad r4, r2, r2, r4
mad r4, r0, r0, r4
rsq r5.x, r4.x
rsq r5.y, r4.y
rsq r5.z, r4.z
rsq r5.w, r4.w
mov r6.y, c31.y
mad r4, r4, c20, r6.y
mul r6.xyz, v1.y, c12
mad r6.xyz, c11, v1.x, r6
mad r6.xyz, c13, v1.z, r6
nrm r7.xyz, r6
mul r3, r3, r7.y
mad r2, r2, r7.x, r3
mad r0, r0, r7.z, r2
mul r0, r5, r0
max r0, r0, c31.z
rcp r2.x, r4.x
rcp r2.y, r4.y
rcp r2.z, r4.z
rcp r2.w, r4.w
mul r0, r0, r2
mul r2.xyz, r0.y, c1
mad r2.xyz, c0, r0.x, r2
mad r0.xyz, c2, r0.z, r2
mad r0.xyz, c3, r0.w, r0
mul r0.w, r7.y, r7.y
mad r0.w, r7.x, r7.x, -r0.w
mul r2, r7.yzzx, r7.xyzz
dp4 r3.x, c24, r2
dp4 r3.y, c25, r2
dp4 r3.z, c26, r2
mad r2.xyz, c27, r0.w, r3
mov r7.w, c31.y
dp4 r3.x, c21, r7
dp4 r3.y, c22, r7
dp4 r3.z, c23, r7
mov oT4.xyz, r7
add r2.xyz, r2, r3
add oT5.xyz, r0, r2
dp4 r1.z, c6, v0
mov oPos, r1
mov oT6.zw, r1
mov oT2, c31.z
mov oT3, c31.z
mov oT4.w, c31.z
mov oT5.w, c31.z

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "texcoord2" TexCoord2
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 15 [_DetailAlbedoMap_ST]
Vector 14 [_MainTex_ST]
Vector 11 [_ProjectionParams]
Vector 12 [_ScreenParams]
Float 16 [_UVSec]
Vector 10 [_WorldSpaceCameraPos]
Vector 13 [unity_DynamicLightmapST]
"vs_3_0
def c17, 0.5, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_texcoord2 v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2.xyz
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord6 o7
dcl_texcoord8 o8.xyz
mad o1.xy, v2, c14, c14.zwzw
abs r0.x, c16.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad o1.zw, r2.xyxy, c15.xyxy, c15
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add o2.xyz, r0, -c10
mov o8.xyz, r0
mul r0.xyz, c8, v1.y
mad r0.xyz, c7, v1.x, r0
mad r0.xyz, c9, v1.z, r0
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul o5.xyz, r0.w, r0
dp4 r0.y, c1, v0
mul r1.x, r0.y, c11.x
mul r1.w, r1.x, c17.x
dp4 r0.x, c0, v0
dp4 r0.w, c3, v0
mul r1.xz, r0.xyww, c17.x
mad o7.xy, r1.z, c12.zwzw, r1.xwzw
mad o6.zw, v4.xyxy, c13.xyxy, c13
dp4 r0.z, c2, v0
mov o0, r0
mov o7.zw, r0
mov o3, c17.y
mov o4, c17.y
mov o5.w, c17.y
mov o6.xy, c17.y

"
}
}
Program "fp" {
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
Vector 4 [_Color]
Float 6 [_Glossiness]
Vector 3 [_LightColor0]
Float 5 [_Metallic]
Vector 0 [_WorldSpaceLightPos0]
Vector 2 [unity_ColorSpaceDielectricSpec]
Vector 1 [unity_SpecCube0_HDR]
SetTexture 0 [unity_SpecCube0] CUBE 0
SetTexture 1 [unity_NHxRoughness] 2D 1
SetTexture 2 [_MainTex] 2D 2
SetTexture 3 [_OcclusionMap] 2D 3
"ps_2_0
def c7, -7, 7, 1, 16
dcl t0.xy
dcl_pp t1.xyz
dcl_pp t4.xyz
dcl_pp t5.xyz
dcl_cube s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
nrm_pp r0.xyz, t4
dp3_pp r0.w, -t1, r0
add_pp r1.w, r0.w, r0.w
mov_sat_pp r0.w, r0.w
add_pp r1.y, -r0.w, c7.z
mad_pp r2.xyz, r0, -r1.w, -t1
dp3_pp r1.x, r2, c0
mul_pp r1.xy, r1, r1
mul_pp r1.xy, r1, r1
mov r2.xyz, c7
add_pp r1.z, r2.z, -c6.x
mov_pp r3.x, r1.x
mov_pp r3.y, r1.z
dp3 r0.w, t1, r0
add r0.w, r0.w, r0.w
mad_pp r4.xyz, r0, -r0.w, t1
mad_pp r4.w, c6.x, r2.x, r2.y
texld r2, r3, s1
texld r3, t0, s2
texld_pp r5, t0, s3
texldb_pp r4, r4, s0
mul_pp r0.w, r2.x, c7.w
mov r2, c2
mad_pp r6.xyz, c4, r3, -r2
mul_pp r3.xyz, r3, c4
mad_pp r2.xyz, c5.x, r6, r2
mad_pp r2.w, c5.x, -r2.w, r2.w
mul_pp r3.xyz, r2.w, r3
add_pp r2.w, -r2.w, c6.x
add_sat_pp r2.w, r2.w, c7.z
lrp_pp r6.xyz, r1.y, r2.w, r2
mad_pp r1.xyz, r0.w, r2, r3
mul_pp r1.xyz, r1, c3
mul_pp r2.xyz, r5.y, t5
mul_pp r2.xyz, r3, r2
dp3_sat_pp r1.w, r0, c0
mad_pp r0.xyz, r1, r1.w, r2
pow_pp r0.w, r4.w, c1.y
mul_pp r0.w, r0.w, c1.x
mul_pp r1.xyz, r4, r0.w
mul_pp r1.xyz, r5.y, r1
mad_pp r0.xyz, r1, r6, r0
mov_pp r0.w, c7.z
mov_pp oC0, r0

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" }
Vector 13 [_Color]
Float 15 [_Glossiness]
Vector 11 [_LightColor0]
Float 14 [_Metallic]
Float 16 [_OcclusionStrength]
Vector 0 [_WorldSpaceLightPos0]
Vector 9 [unity_ColorSpaceDielectricSpec]
Vector 10 [unity_DynamicLightmap_HDR]
Vector 12 [unity_LightGammaCorrectionConsts]
Vector 1 [unity_SpecCube0_BoxMax]
Vector 2 [unity_SpecCube0_BoxMin]
Vector 4 [unity_SpecCube0_HDR]
Vector 3 [unity_SpecCube0_ProbePosition]
Vector 5 [unity_SpecCube1_BoxMax]
Vector 6 [unity_SpecCube1_BoxMin]
Vector 8 [unity_SpecCube1_HDR]
Vector 7 [unity_SpecCube1_ProbePosition]
SetTexture 0 [unity_DynamicLightmap] 2D 0
SetTexture 1 [unity_SpecCube0] CUBE 1
SetTexture 2 [unity_SpecCube1] CUBE 2
SetTexture 3 [_MainTex] 2D 3
SetTexture 4 [_OcclusionMap] 2D 4
"ps_3_0
def c17, 7, 0.999989986, 0.00100000005, 31.622776
def c18, 9.99999975e-005, 0.967999995, 0.0299999993, 10
def c19, 0, 1, 0.5, 0.75
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord4_pp v2.xyz
dcl_texcoord5_pp v3.zw
dcl_texcoord8_pp v4.xyz
dcl_2d s0
dcl_cube s1
dcl_cube s2
dcl_2d s3
dcl_2d s4
nrm_pp r0.xyz, v2
dp3_pp r0.w, v1, v1
rsq_pp r0.w, r0.w
mul_pp r1.xyz, r0.w, v1
texld r2, v0, s3
mul_pp r3.xyz, r2, c13
mov r4, c9
mad_pp r2.xyz, c13, r2, -r4
mad_pp r2.xyz, c14.x, r2, r4
mad_pp r1.w, c14.x, -r4.w, r4.w
mul_pp r3.xyz, r1.w, r3
dp3_pp r2.w, r0, c0
max_pp r3.w, r2.w, c19.x
texld_pp r4, v0, s4
mov r5.xyz, c19
add_pp r2.w, r5.y, -c16.x
mad_pp r2.w, r4.y, c16.x, r2.w
texld_pp r4, v3.zwzw, s0
mul_pp r4.w, r4.w, c10.x
mul_pp r4.xyz, r4, r4.w
log_pp r6.x, r4.x
log_pp r6.y, r4.y
log_pp r6.z, r4.z
mul_pp r4.xyz, r6, c10.y
exp_pp r6.x, r4.x
exp_pp r6.y, r4.y
exp_pp r6.z, r4.z
dp3 r4.x, r1, r0
add r4.x, r4.x, r4.x
mad_pp r4.xyz, r0, -r4.x, r1
if_lt -c3.w, r5.x
nrm_pp r7.xyz, r4
add r8.xyz, c1, -v4
rcp r9.x, r7.x
rcp r9.y, r7.y
rcp r9.z, r7.z
mul_pp r8.xyz, r8, r9
add r10.xyz, c2, -v4
mul_pp r9.xyz, r9, r10
cmp_pp r8.xyz, -r7, r9, r8
min_pp r4.w, r8.y, r8.x
min_pp r5.w, r8.z, r4.w
mov r8.xyz, c2
add r8.xyz, r8, c1
mad r9.xyz, r8, r5.z, -c3
add r9.xyz, r9, v4
mad r7.xyz, r7, r5.w, r9
mad_pp r7.xyz, r8, -c19.z, r7
else
mov_pp r7.xyz, r4
endif
add_pp r4.w, r5.y, -c15.x
pow_pp r5.w, r4.w, c19.w
mul_pp r7.w, r5.w, c17.x
texldl_pp r8, r7, s1
pow_pp r5.w, r8.w, c4.y
mul_pp r5.w, r5.w, c4.x
mul_pp r9.xyz, r8, r5.w
mov r6.w, c2.w
if_lt r6.w, c17.y
if_lt -c7.w, r5.x
nrm_pp r10.xyz, r4
add r11.xyz, c5, -v4
rcp r12.x, r10.x
rcp r12.y, r10.y
rcp r12.z, r10.z
mul_pp r11.xyz, r11, r12
add r13.xyz, c6, -v4
mul_pp r12.xyz, r12, r13
cmp_pp r11.xyz, -r10, r12, r11
min_pp r5.x, r11.y, r11.x
min_pp r6.w, r11.z, r5.x
mov r11.xyz, c5
add r11.xyz, r11, c6
mad r12.xyz, r11, r5.z, -c7
add r12.xyz, r12, v4
mad r10.xyz, r10, r6.w, r12
mad_pp r7.xyz, r11, -c19.z, r10
else
mov_pp r7.xyz, r4
endif
texldl_pp r7, r7, s2
pow_pp r4.x, r7.w, c8.y
mul_pp r4.x, r4.x, c8.x
mul_pp r4.xyz, r7, r4.x
mad r5.xzw, r5.w, r8.xyyz, -r4.xyyz
mad_pp r9.xyz, c2.w, r5.xzww, r4
endif
mul_pp r4.xyz, r2.w, r9
mad_pp r5.xzw, v1.xyyz, -r0.w, c0.xyyz
dp3_pp r0.w, r5.xzww, r5.xzww
add r6.w, -r0.w, c17.z
rsq_pp r0.w, r0.w
cmp_pp r0.w, r6.w, c17.w, r0.w
mul_pp r5.xzw, r0.w, r5
dp3_pp r0.w, r0, r5.xzww
max_pp r6.w, r0.w, c19.x
dp3_pp r0.x, r0, -r1
max_pp r1.x, r0.x, c19.x
dp3_pp r0.x, c0, r5.xzww
max_pp r1.y, r0.x, c19.x
mul_pp r0.x, r4.w, r4.w
mul_pp r0.y, r0.x, c12.w
mad_pp r0.x, r0.x, -c12.w, r5.y
mad_pp r0.z, r3.w, r0.x, r0.y
mad_pp r0.x, r1.x, r0.x, r0.y
mad r0.x, r0.z, r0.x, c18.x
rcp_pp r0.x, r0.x
add_pp r0.y, -r4.w, c19.y
mad_pp r0.y, r0.y, c18.y, c18.z
log_pp r0.y, r0.y
rcp r0.y, r0.y
mul_pp r0.y, r0.y, c18.w
mul_pp r0.z, r0.y, r0.y
mad_pp r0.y, r0.y, r0.y, c19.y
mul_pp r0.y, r0.y, c12.y
pow_pp r1.z, r6.w, r0.z
add_pp r0.z, -r3.w, c19.y
mul_pp r0.w, r0.z, r0.z
mul_pp r0.w, r0.w, r0.w
mul_pp r0.z, r0.z, r0.w
add_pp r0.w, -r1.x, c19.y
mul_pp r1.x, r0.w, r0.w
mul_pp r1.x, r1.x, r1.x
mul_pp r0.yw, r0, r1.xzzx
mul_pp r1.x, r1.y, r1.y
dp2add_pp r1.x, r1.x, r4.w, -c19.z
mad_pp r0.z, r1.x, r0.z, c19.y
mad_pp r1.x, r1.x, r0.w, c19.y
mul_pp r0.z, r0.z, r1.x
mul_pp r0.x, r0.y, r0.x
mul_pp r0.xy, r3.w, r0.xzzw
mul_pp r0.x, r0.x, c12.x
add_pp r0.z, -r1.w, c19.y
add_sat_pp r0.z, r0.z, c15.x
mul_pp r1.xzw, r0.y, c11.xyyz
mad_pp r1.xzw, r6.xyyz, r2.w, r1
mul_pp r5.xyz, r0.x, c11
cmp_pp r5.xyz, r0.x, r5, c19.x
add_pp r0.x, -r1.y, c19.y
mul_pp r0.y, r0.x, r0.x
mul_pp r0.y, r0.y, r0.y
mul_pp r0.x, r0.x, r0.y
lrp_pp r6.xyz, r0.x, c19.y, r2
mul_pp r5.xyz, r5, r6
mad_pp r1.xyz, r3, r1.xzww, r5
lrp_pp r3.xyz, r0.w, r0.z, r2
mad_pp oC0.xyz, r4, r3, r1
mov_pp oC0.w, c19.y

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
Vector 4 [_Color]
Float 6 [_Glossiness]
Vector 3 [_LightColor0]
Float 5 [_Metallic]
Vector 0 [_WorldSpaceLightPos0]
Vector 2 [unity_ColorSpaceDielectricSpec]
Vector 1 [unity_SpecCube0_HDR]
SetTexture 0 [unity_SpecCube0] CUBE 0
SetTexture 1 [unity_NHxRoughness] 2D 1
SetTexture 2 [_MainTex] 2D 2
SetTexture 3 [_OcclusionMap] 2D 3
SetTexture 4 [_ShadowMapTexture] 2D 4
"ps_2_0
def c7, -7, 7, 1, 16
dcl t0.xy
dcl_pp t1.xyz
dcl_pp t4.xyz
dcl_pp t5.xyz
dcl_pp t6
dcl_cube s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
dcl_2d s4
nrm_pp r0.xyz, t4
dp3_pp r0.w, -t1, r0
add_pp r1.w, r0.w, r0.w
mov_sat_pp r0.w, r0.w
add_pp r1.y, -r0.w, c7.z
mad_pp r2.xyz, r0, -r1.w, -t1
dp3_pp r1.x, r2, c0
mul_pp r1.xy, r1, r1
mul_pp r1.xy, r1, r1
mov r2.xyz, c7
add_pp r1.z, r2.z, -c6.x
mov_pp r3.x, r1.x
mov_pp r3.y, r1.z
dp3 r0.w, t1, r0
add r0.w, r0.w, r0.w
mad_pp r4.xyz, r0, -r0.w, t1
mad_pp r4.w, c6.x, r2.x, r2.y
texldp_pp r2, t6, s4
texld r3, r3, s1
texld r5, t0, s2
texld_pp r6, t0, s3
texldb_pp r4, r4, s0
mul_pp r2.xyz, r2.x, c3
mul_pp r0.w, r3.x, c7.w
mov r3, c2
mad_pp r7.xyz, c4, r5, -r3
mul_pp r5.xyz, r5, c4
mad_pp r3.xyz, c5.x, r7, r3
mad_pp r2.w, c5.x, -r3.w, r3.w
mul_pp r5.xyz, r2.w, r5
add_pp r2.w, -r2.w, c6.x
add_sat_pp r2.w, r2.w, c7.z
lrp_pp r7.xyz, r1.y, r2.w, r3
mad_pp r1.xyz, r0.w, r3, r5
mul_pp r1.xyz, r2, r1
mul_pp r2.xyz, r6.y, t5
mul_pp r2.xyz, r5, r2
dp3_sat_pp r1.w, r0, c0
mad_pp r0.xyz, r1, r1.w, r2
pow_pp r0.w, r4.w, c1.y
mul_pp r0.w, r0.w, c1.x
mul_pp r1.xyz, r4, r0.w
mul_pp r1.xyz, r6.y, r1
mad_pp r0.xyz, r1, r7, r0
mov_pp r0.w, c7.z
mov_pp oC0, r0

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" }
Vector 13 [_Color]
Float 15 [_Glossiness]
Vector 11 [_LightColor0]
Float 14 [_Metallic]
Float 16 [_OcclusionStrength]
Vector 0 [_WorldSpaceLightPos0]
Vector 9 [unity_ColorSpaceDielectricSpec]
Vector 10 [unity_DynamicLightmap_HDR]
Vector 12 [unity_LightGammaCorrectionConsts]
Vector 1 [unity_SpecCube0_BoxMax]
Vector 2 [unity_SpecCube0_BoxMin]
Vector 4 [unity_SpecCube0_HDR]
Vector 3 [unity_SpecCube0_ProbePosition]
Vector 5 [unity_SpecCube1_BoxMax]
Vector 6 [unity_SpecCube1_BoxMin]
Vector 8 [unity_SpecCube1_HDR]
Vector 7 [unity_SpecCube1_ProbePosition]
SetTexture 0 [unity_DynamicLightmap] 2D 0
SetTexture 1 [unity_SpecCube0] CUBE 1
SetTexture 2 [unity_SpecCube1] CUBE 2
SetTexture 3 [_MainTex] 2D 3
SetTexture 4 [_OcclusionMap] 2D 4
SetTexture 5 [_ShadowMapTexture] 2D 5
"ps_3_0
def c17, 7, 0.999989986, 0.00100000005, 31.622776
def c18, 9.99999975e-005, 0.967999995, 0.0299999993, 10
def c19, 0, 1, 0.5, 0.75
dcl_texcoord v0.xy
dcl_texcoord1_pp v1.xyz
dcl_texcoord4_pp v2.xyz
dcl_texcoord5_pp v3.zw
dcl_texcoord6 v4
dcl_texcoord8_pp v5.xyz
dcl_2d s0
dcl_cube s1
dcl_cube s2
dcl_2d s3
dcl_2d s4
dcl_2d s5
nrm_pp r0.xyz, v2
dp3_pp r0.w, v1, v1
rsq_pp r0.w, r0.w
mul_pp r1.xyz, r0.w, v1
texld r2, v0, s3
mul_pp r3.xyz, r2, c13
mov r4, c9
mad_pp r2.xyz, c13, r2, -r4
mad_pp r2.xyz, c14.x, r2, r4
mad_pp r1.w, c14.x, -r4.w, r4.w
mul_pp r3.xyz, r1.w, r3
dp3_pp r2.w, r0, c0
max_pp r3.w, r2.w, c19.x
texldp_pp r4, v4, s5
texld_pp r5, v0, s4
mov r6.xyz, c19
add_pp r2.w, r6.y, -c16.x
mad_pp r2.w, r5.y, c16.x, r2.w
mul_pp r4.xyz, r4.x, c11
texld_pp r5, v3.zwzw, s0
mul_pp r4.w, r5.w, c10.x
mul_pp r5.xyz, r5, r4.w
log_pp r7.x, r5.x
log_pp r7.y, r5.y
log_pp r7.z, r5.z
mul_pp r5.xyz, r7, c10.y
exp_pp r7.x, r5.x
exp_pp r7.y, r5.y
exp_pp r7.z, r5.z
dp3 r4.w, r1, r0
add r4.w, r4.w, r4.w
mad_pp r5.xyz, r0, -r4.w, r1
if_lt -c3.w, r6.x
nrm_pp r8.xyz, r5
add r9.xyz, c1, -v5
rcp r10.x, r8.x
rcp r10.y, r8.y
rcp r10.z, r8.z
mul_pp r9.xyz, r9, r10
add r11.xyz, c2, -v5
mul_pp r10.xyz, r10, r11
cmp_pp r9.xyz, -r8, r10, r9
min_pp r4.w, r9.y, r9.x
min_pp r5.w, r9.z, r4.w
mov r9.xyz, c2
add r9.xyz, r9, c1
mad r10.xyz, r9, r6.z, -c3
add r10.xyz, r10, v5
mad r8.xyz, r8, r5.w, r10
mad_pp r8.xyz, r9, -c19.z, r8
else
mov_pp r8.xyz, r5
endif
add_pp r4.w, r6.y, -c15.x
pow_pp r5.w, r4.w, c19.w
mul_pp r8.w, r5.w, c17.x
texldl_pp r9, r8, s1
pow_pp r5.w, r9.w, c4.y
mul_pp r5.w, r5.w, c4.x
mul_pp r10.xyz, r9, r5.w
mov r6.w, c2.w
if_lt r6.w, c17.y
if_lt -c7.w, r6.x
nrm_pp r11.xyz, r5
add r12.xyz, c5, -v5
rcp r13.x, r11.x
rcp r13.y, r11.y
rcp r13.z, r11.z
mul_pp r12.xyz, r12, r13
add r14.xyz, c6, -v5
mul_pp r13.xyz, r13, r14
cmp_pp r12.xyz, -r11, r13, r12
min_pp r6.x, r12.y, r12.x
min_pp r7.w, r12.z, r6.x
mov r12.xyz, c5
add r12.xyz, r12, c6
mad r6.xzw, r12.xyyz, r6.z, -c7.xyyz
add r6.xzw, r6, v5.xyyz
mad r6.xzw, r11.xyyz, r7.w, r6
mad_pp r8.xyz, r12, -c19.z, r6.xzww
else
mov_pp r8.xyz, r5
endif
texldl_pp r8, r8, s2
pow_pp r5.x, r8.w, c8.y
mul_pp r5.x, r5.x, c8.x
mul_pp r5.xyz, r8, r5.x
mad r6.xzw, r5.w, r9.xyyz, -r5.xyyz
mad_pp r10.xyz, c2.w, r6.xzww, r5
endif
mul_pp r5.xyz, r2.w, r10
mad_pp r6.xzw, v1.xyyz, -r0.w, c0.xyyz
dp3_pp r0.w, r6.xzww, r6.xzww
add r5.w, -r0.w, c17.z
rsq_pp r0.w, r0.w
cmp_pp r0.w, r5.w, c17.w, r0.w
mul_pp r6.xzw, r0.w, r6
dp3_pp r0.w, r0, r6.xzww
max_pp r5.w, r0.w, c19.x
dp3_pp r0.x, r0, -r1
max_pp r1.x, r0.x, c19.x
dp3_pp r0.x, c0, r6.xzww
max_pp r1.y, r0.x, c19.x
mul_pp r0.x, r4.w, r4.w
mul_pp r0.y, r0.x, c12.w
mad_pp r0.x, r0.x, -c12.w, r6.y
mad_pp r0.z, r3.w, r0.x, r0.y
mad_pp r0.x, r1.x, r0.x, r0.y
mad r0.x, r0.z, r0.x, c18.x
rcp_pp r0.x, r0.x
add_pp r0.y, -r4.w, c19.y
mad_pp r0.y, r0.y, c18.y, c18.z
log_pp r0.y, r0.y
rcp r0.y, r0.y
mul_pp r0.y, r0.y, c18.w
mul_pp r0.z, r0.y, r0.y
mad_pp r0.y, r0.y, r0.y, c19.y
mul_pp r0.y, r0.y, c12.y
pow_pp r1.z, r5.w, r0.z
add_pp r0.z, -r3.w, c19.y
mul_pp r0.w, r0.z, r0.z
mul_pp r0.w, r0.w, r0.w
mul_pp r0.z, r0.z, r0.w
add_pp r0.w, -r1.x, c19.y
mul_pp r1.x, r0.w, r0.w
mul_pp r1.x, r1.x, r1.x
mul_pp r0.yw, r0, r1.xzzx
mul_pp r1.x, r1.y, r1.y
dp2add_pp r1.x, r1.x, r4.w, -c19.z
mad_pp r0.z, r1.x, r0.z, c19.y
mad_pp r1.x, r1.x, r0.w, c19.y
mul_pp r0.z, r0.z, r1.x
mul_pp r0.x, r0.y, r0.x
mul_pp r0.x, r3.w, r0.x
mul_pp r0.x, r0.x, c12.x
max_pp r1.x, r0.x, c19.x
mul_pp r0.x, r3.w, r0.z
add_pp r0.y, -r1.w, c19.y
add_sat_pp r0.y, r0.y, c15.x
mul_pp r6.xyz, r0.x, r4
mad_pp r6.xyz, r7, r2.w, r6
mul_pp r1.xzw, r4.xyyz, r1.x
add_pp r0.x, -r1.y, c19.y
mul_pp r0.z, r0.x, r0.x
mul_pp r0.z, r0.z, r0.z
mul_pp r0.x, r0.x, r0.z
lrp_pp r4.xyz, r0.x, c19.y, r2
mul_pp r1.xyz, r1.xzww, r4
mad_pp r1.xyz, r3, r6, r1
lrp_pp r3.xyz, r0.w, r0.y, r2
mad_pp oC0.xyz, r5, r3, r1
mov_pp oC0.w, c19.y

"
}
}
 }
 Pass {
  Name "FORWARD_DELTA"
  Tags { "LIGHTMODE"="ForwardAdd" "SHADOWSUPPORT"="true" "RenderType"="Opaque" "PerformanceChecks"="False" }
  ZWrite Off
  Fog {
   Color (0,0,0,0)
  }
  Blend [_SrcBlend] One
  GpuProgramID 456985
Program "vp" {
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 11 [_LightMatrix0] 3
Matrix 4 [_Object2World]
Matrix 8 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 17 [_DetailAlbedoMap_ST]
Vector 16 [_MainTex_ST]
Float 18 [_UVSec]
Vector 14 [_WorldSpaceCameraPos]
Vector 15 [_WorldSpaceLightPos0]
"vs_2_0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dp4 oPos.x, c0, v0
dp4 oPos.y, c1, v0
dp4 oPos.z, c2, v0
dp4 oPos.w, c3, v0
mad oT0.xy, v2, c16, c16.zwzw
mul r0.x, c18.x, c18.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad oT0.zw, r2.xyxy, c17.xyxy, c17
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add r1.xyz, r0, -c14
dp3 r1.w, r1, r1
rsq r1.w, r1.w
mul oT1.xyz, r1.w, r1
mul r1.xyz, v1.y, c9
mad r1.xyz, c8, v1.x, r1
mad r1.xyz, c10, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c4, v4
dp3 r1.y, c5, v4
dp3 r1.z, c6, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov oT4.xyz, r2
mov oT2.xyz, r3
mul oT3.xyz, r1, v4.w
dp4 r0.w, c7, v0
dp4 oT5.x, c11, r0
dp4 oT5.y, c12, r0
dp4 oT5.z, c13, r0
mad r0.xyz, r0, -c15.w, c15
nrm r1.xyz, r0
mov oT2.w, r1.x
mov oT3.w, r1.y
mov oT4.w, r1.z

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 13 [_DetailAlbedoMap_ST]
Vector 12 [_MainTex_ST]
Float 14 [_UVSec]
Vector 10 [_WorldSpaceCameraPos]
Vector 11 [_WorldSpaceLightPos0]
"vs_2_0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dp4 oPos.x, c0, v0
dp4 oPos.y, c1, v0
dp4 oPos.z, c2, v0
dp4 oPos.w, c3, v0
mad oT0.xy, v2, c12, c12.zwzw
mul r0.x, c14.x, c14.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad oT0.zw, r2.xyxy, c13.xyxy, c13
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add r1.xyz, r0, -c10
mad r0.xyz, r0, -c11.w, c11
dp3 r0.w, r1, r1
rsq r0.w, r0.w
mul oT1.xyz, r0.w, r1
mul r1.xyz, v1.y, c8
mad r1.xyz, c7, v1.x, r1
mad r1.xyz, c9, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c4, v4
dp3 r1.y, c5, v4
dp3 r1.z, c6, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov oT4.xyz, r2
mov oT2.xyz, r3
mul oT3.xyz, r1, v4.w
mov oT2.w, r0.x
mov oT3.w, r0.y
mov oT4.w, r0.z

"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 8 [_LightMatrix0]
Matrix 4 [_Object2World]
Matrix 12 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 18 [_DetailAlbedoMap_ST]
Vector 17 [_MainTex_ST]
Float 19 [_UVSec]
Vector 15 [_WorldSpaceCameraPos]
Vector 16 [_WorldSpaceLightPos0]
"vs_2_0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dp4 oPos.x, c0, v0
dp4 oPos.y, c1, v0
dp4 oPos.z, c2, v0
dp4 oPos.w, c3, v0
mad oT0.xy, v2, c17, c17.zwzw
mul r0.x, c19.x, c19.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad oT0.zw, r2.xyxy, c18.xyxy, c18
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add r1.xyz, r0, -c15
dp3 r1.w, r1, r1
rsq r1.w, r1.w
mul oT1.xyz, r1.w, r1
mul r1.xyz, v1.y, c13
mad r1.xyz, c12, v1.x, r1
mad r1.xyz, c14, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c4, v4
dp3 r1.y, c5, v4
dp3 r1.z, c6, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov oT4.xyz, r2
mov oT2.xyz, r3
mul oT3.xyz, r1, v4.w
dp4 r0.w, c7, v0
dp4 oT5.x, c8, r0
dp4 oT5.y, c9, r0
dp4 oT5.z, c10, r0
dp4 oT5.w, c11, r0
mad r0.xyz, r0, -c16.w, c16
nrm r1.xyz, r0
mov oT2.w, r1.x
mov oT3.w, r1.y
mov oT4.w, r1.z

"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 11 [_LightMatrix0] 3
Matrix 4 [_Object2World]
Matrix 8 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 17 [_DetailAlbedoMap_ST]
Vector 16 [_MainTex_ST]
Float 18 [_UVSec]
Vector 14 [_WorldSpaceCameraPos]
Vector 15 [_WorldSpaceLightPos0]
"vs_2_0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dp4 oPos.x, c0, v0
dp4 oPos.y, c1, v0
dp4 oPos.z, c2, v0
dp4 oPos.w, c3, v0
mad oT0.xy, v2, c16, c16.zwzw
mul r0.x, c18.x, c18.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad oT0.zw, r2.xyxy, c17.xyxy, c17
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add r1.xyz, r0, -c14
dp3 r1.w, r1, r1
rsq r1.w, r1.w
mul oT1.xyz, r1.w, r1
mul r1.xyz, v1.y, c9
mad r1.xyz, c8, v1.x, r1
mad r1.xyz, c10, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c4, v4
dp3 r1.y, c5, v4
dp3 r1.z, c6, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov oT4.xyz, r2
mov oT2.xyz, r3
mul oT3.xyz, r1, v4.w
dp4 r0.w, c7, v0
dp4 oT5.x, c11, r0
dp4 oT5.y, c12, r0
dp4 oT5.z, c13, r0
mad r0.xyz, r0, -c15.w, c15
nrm r1.xyz, r0
mov oT2.w, r1.x
mov oT3.w, r1.y
mov oT4.w, r1.z

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 11 [_LightMatrix0] 2
Matrix 4 [_Object2World]
Matrix 8 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 16 [_DetailAlbedoMap_ST]
Vector 15 [_MainTex_ST]
Float 17 [_UVSec]
Vector 13 [_WorldSpaceCameraPos]
Vector 14 [_WorldSpaceLightPos0]
"vs_2_0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dp4 oPos.x, c0, v0
dp4 oPos.y, c1, v0
dp4 oPos.z, c2, v0
dp4 oPos.w, c3, v0
mad oT0.xy, v2, c15, c15.zwzw
mul r0.x, c17.x, c17.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad oT0.zw, r2.xyxy, c16.xyxy, c16
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add r1.xyz, r0, -c13
dp3 r1.w, r1, r1
rsq r1.w, r1.w
mul oT1.xyz, r1.w, r1
mul r1.xyz, v1.y, c9
mad r1.xyz, c8, v1.x, r1
mad r1.xyz, c10, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c4, v4
dp3 r1.y, c5, v4
dp3 r1.z, c6, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov oT4.xyz, r2
mov oT2.xyz, r3
mul oT3.xyz, r1, v4.w
dp4 r0.w, c7, v0
dp4 oT5.x, c11, r0
dp4 oT5.y, c12, r0
mad r0.xyz, r0, -c14.w, c14
mov oT2.w, r0.x
mov oT3.w, r0.y
mov oT4.w, r0.z

"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_NATIVE" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 12 [_LightMatrix0]
Matrix 8 [_Object2World]
Matrix 16 [_World2Object] 3
Matrix 4 [glstate_matrix_mvp]
Matrix 0 [unity_World2Shadow0]
Vector 22 [_DetailAlbedoMap_ST]
Vector 21 [_MainTex_ST]
Float 23 [_UVSec]
Vector 19 [_WorldSpaceCameraPos]
Vector 20 [_WorldSpaceLightPos0]
"vs_2_0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dp4 oPos.x, c4, v0
dp4 oPos.y, c5, v0
dp4 oPos.z, c6, v0
dp4 oPos.w, c7, v0
mad oT0.xy, v2, c21, c21.zwzw
mul r0.x, c23.x, c23.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad oT0.zw, r2.xyxy, c22.xyxy, c22
dp4 r0.x, c8, v0
dp4 r0.y, c9, v0
dp4 r0.z, c10, v0
add r1.xyz, r0, -c19
dp3 r1.w, r1, r1
rsq r1.w, r1.w
mul oT1.xyz, r1.w, r1
mul r1.xyz, v1.y, c17
mad r1.xyz, c16, v1.x, r1
mad r1.xyz, c18, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c8, v4
dp3 r1.y, c9, v4
dp3 r1.z, c10, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov oT4.xyz, r2
mov oT2.xyz, r3
mul oT3.xyz, r1, v4.w
dp4 r0.w, c11, v0
dp4 oT5.x, c12, r0
dp4 oT5.y, c13, r0
dp4 oT5.z, c14, r0
dp4 oT5.w, c15, r0
dp4 oT6.x, c0, r0
dp4 oT6.y, c1, r0
dp4 oT6.z, c2, r0
dp4 oT6.w, c3, r0
mad r0.xyz, r0, -c20.w, c20
nrm r1.xyz, r0
mov oT2.w, r1.x
mov oT3.w, r1.y
mov oT4.w, r1.z

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 15 [_DetailAlbedoMap_ST]
Vector 14 [_MainTex_ST]
Vector 11 [_ProjectionParams]
Vector 12 [_ScreenParams]
Float 16 [_UVSec]
Vector 10 [_WorldSpaceCameraPos]
Vector 13 [_WorldSpaceLightPos0]
"vs_2_0
def c17, 0.5, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
mad oT0.xy, v2, c14, c14.zwzw
mul r0.x, c16.x, c16.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad oT0.zw, r2.xyxy, c15.xyxy, c15
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add r1.xyz, r0, -c10
mad r0.xyz, r0, -c13.w, c13
dp3 r0.w, r1, r1
rsq r0.w, r0.w
mul oT1.xyz, r0.w, r1
mul r1.xyz, v1.y, c8
mad r1.xyz, c7, v1.x, r1
mad r1.xyz, c9, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c4, v4
dp3 r1.y, c5, v4
dp3 r1.z, c6, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov oT4.xyz, r2
mov oT2.xyz, r3
mul oT3.xyz, r1, v4.w
dp4 r1.y, c1, v0
mul r0.w, r1.y, c11.x
mul r2.w, r0.w, c17.x
dp4 r1.x, c0, v0
dp4 r1.w, c3, v0
mul r2.xz, r1.xyww, c17.x
mad oT5.xy, r2.z, c12.zwzw, r2.xwzw
dp4 r1.z, c2, v0
mov oPos, r1
mov oT5.zw, r1
mov oT2.w, r0.x
mov oT3.w, r0.y
mov oT4.w, r0.z

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 11 [_LightMatrix0] 2
Matrix 4 [_Object2World]
Matrix 8 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 18 [_DetailAlbedoMap_ST]
Vector 17 [_MainTex_ST]
Vector 14 [_ProjectionParams]
Vector 15 [_ScreenParams]
Float 19 [_UVSec]
Vector 13 [_WorldSpaceCameraPos]
Vector 16 [_WorldSpaceLightPos0]
"vs_2_0
def c20, 0.5, 0, 0, 0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
mad oT0.xy, v2, c17, c17.zwzw
mul r0.x, c19.x, c19.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad oT0.zw, r2.xyxy, c18.xyxy, c18
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add r1.xyz, r0, -c13
dp3 r1.w, r1, r1
rsq r1.w, r1.w
mul oT1.xyz, r1.w, r1
mul r1.xyz, v1.y, c9
mad r1.xyz, c8, v1.x, r1
mad r1.xyz, c10, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c4, v4
dp3 r1.y, c5, v4
dp3 r1.z, c6, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov oT4.xyz, r2
mov oT2.xyz, r3
mul oT3.xyz, r1, v4.w
dp4 r0.w, c7, v0
dp4 oT5.x, c11, r0
dp4 oT5.y, c12, r0
mad r0.xyz, r0, -c16.w, c16
dp4 r1.y, c1, v0
mul r0.w, r1.y, c14.x
mul r2.w, r0.w, c20.x
dp4 r1.x, c0, v0
dp4 r1.w, c3, v0
mul r2.xz, r1.xyww, c20.x
mad oT6.xy, r2.z, c15.zwzw, r2.xwzw
dp4 r1.z, c2, v0
mov oPos, r1
mov oT6.zw, r1
mov oT2.w, r0.x
mov oT3.w, r0.y
mov oT4.w, r0.z

"
}
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_CUBE" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 11 [_LightMatrix0] 3
Matrix 4 [_Object2World]
Matrix 8 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 18 [_DetailAlbedoMap_ST]
Vector 16 [_LightPositionRange]
Vector 17 [_MainTex_ST]
Float 19 [_UVSec]
Vector 14 [_WorldSpaceCameraPos]
Vector 15 [_WorldSpaceLightPos0]
"vs_2_0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dp4 oPos.x, c0, v0
dp4 oPos.y, c1, v0
dp4 oPos.z, c2, v0
dp4 oPos.w, c3, v0
mad oT0.xy, v2, c17, c17.zwzw
mul r0.x, c19.x, c19.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad oT0.zw, r2.xyxy, c18.xyxy, c18
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add r1.xyz, r0, -c14
dp3 r1.w, r1, r1
rsq r1.w, r1.w
mul oT1.xyz, r1.w, r1
mul r1.xyz, v1.y, c9
mad r1.xyz, c8, v1.x, r1
mad r1.xyz, c10, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c4, v4
dp3 r1.y, c5, v4
dp3 r1.z, c6, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov oT4.xyz, r2
mov oT2.xyz, r3
mul oT3.xyz, r1, v4.w
dp4 r0.w, c7, v0
dp4 oT5.x, c11, r0
dp4 oT5.y, c12, r0
dp4 oT5.z, c13, r0
add oT6.xyz, r0, -c16
mad r0.xyz, r0, -c15.w, c15
nrm r1.xyz, r0
mov oT2.w, r1.x
mov oT3.w, r1.y
mov oT4.w, r1.z

"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "tangent" TexCoord4
Matrix 11 [_LightMatrix0] 3
Matrix 4 [_Object2World]
Matrix 8 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 18 [_DetailAlbedoMap_ST]
Vector 16 [_LightPositionRange]
Vector 17 [_MainTex_ST]
Float 19 [_UVSec]
Vector 14 [_WorldSpaceCameraPos]
Vector 15 [_WorldSpaceLightPos0]
"vs_2_0
dcl_position v0
dcl_normal v1
dcl_texcoord v2
dcl_texcoord1 v3
dcl_tangent v4
dp4 oPos.x, c0, v0
dp4 oPos.y, c1, v0
dp4 oPos.z, c2, v0
dp4 oPos.w, c3, v0
mad oT0.xy, v2, c17, c17.zwzw
mul r0.x, c19.x, c19.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
lrp r2.xy, r0.x, r1, v3
mad oT0.zw, r2.xyxy, c18.xyxy, c18
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add r1.xyz, r0, -c14
dp3 r1.w, r1, r1
rsq r1.w, r1.w
mul oT1.xyz, r1.w, r1
mul r1.xyz, v1.y, c9
mad r1.xyz, c8, v1.x, r1
mad r1.xyz, c10, v1.z, r1
nrm r2.xyz, r1
dp3 r1.x, c4, v4
dp3 r1.y, c5, v4
dp3 r1.z, c6, v4
nrm r3.xyz, r1
mul r1.xyz, r2.zxyw, r3.yzxw
mad r1.xyz, r2.yzxw, r3.zxyw, -r1
mov oT4.xyz, r2
mov oT2.xyz, r3
mul oT3.xyz, r1, v4.w
dp4 r0.w, c7, v0
dp4 oT5.x, c11, r0
dp4 oT5.y, c12, r0
dp4 oT5.z, c13, r0
add oT6.xyz, r0, -c16
mad r0.xyz, r0, -c15.w, c15
nrm r1.xyz, r0
mov oT2.w, r1.x
mov oT3.w, r1.y
mov oT4.w, r1.z

"
}
}
Program "fp" {
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_OFF" }
Vector 2 [_Color]
Float 4 [_Glossiness]
Vector 1 [_LightColor0]
Float 3 [_Metallic]
Vector 0 [unity_ColorSpaceDielectricSpec]
SetTexture 0 [unity_NHxRoughness] 2D 0
SetTexture 1 [_MainTex] 2D 1
SetTexture 2 [_LightTexture0] 2D 2
"ps_2_0
def c5, 1, 16, 0, 0
dcl t0.xy
dcl_pp t1.xyz
dcl_pp t2
dcl_pp t3
dcl_pp t4
dcl t5.xyz
dcl_2d s0
dcl_2d s1
dcl_2d s2
dp3 r0.w, t5, t5
mov r0.xy, r0.w
nrm_pp r1.xyz, t4
dp3_pp r1.w, -t1, r1
add_pp r1.w, r1.w, r1.w
mad_pp r2.xyz, r1, -r1.w, -t1
mov_pp r3.x, t2.w
mov_pp r3.y, t3.w
mov_pp r3.z, t4.w
dp3_pp r1.w, r2, r3
mul_pp r1.w, r1.w, r1.w
mul_pp r2.x, r1.w, r1.w
mov r1.w, c5.x
add_pp r2.y, r1.w, -c4.x
texld_pp r0, r0, s2
texld r2, r2, s0
texld r4, t0, s1
mul_pp r0.xyz, r0.x, c1
dp3_sat_pp r0.w, r1, r3
mul_pp r4.w, r2.x, c5.y
mov r1, c0
mad_pp r2.xyz, c2, r4, -r1
mul_pp r3.xyz, r4, c2
mad_pp r1.xyz, c3.x, r2, r1
mul_pp r1.xyz, r1, r4.w
mad_pp r1.w, c3.x, -r1.w, r1.w
mad_pp r1.xyz, r3, r1.w, r1
mul_pp r0.xyz, r0, r1
mul_pp r0.xyz, r0.w, r0
mov_pp r0.w, c5.x
mov_pp oC0, r0

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" }
Vector 2 [_Color]
Float 4 [_Glossiness]
Vector 1 [_LightColor0]
Float 3 [_Metallic]
Vector 0 [unity_ColorSpaceDielectricSpec]
SetTexture 0 [unity_NHxRoughness] 2D 0
SetTexture 1 [_MainTex] 2D 1
"ps_2_0
def c5, 1, 16, 0, 0
dcl t0.xy
dcl_pp t1.xyz
dcl_pp t2
dcl_pp t3
dcl_pp t4
dcl_2d s0
dcl_2d s1
nrm_pp r0.xyz, t4
dp3_pp r0.w, -t1, r0
add_pp r0.w, r0.w, r0.w
mad_pp r1.xyz, r0, -r0.w, -t1
mov_pp r2.x, t2.w
mov_pp r2.y, t3.w
mov_pp r2.z, t4.w
dp3_pp r0.w, r1, r2
dp3_sat_pp r0.x, r0, r2
mul_pp r0.y, r0.w, r0.w
mul_pp r1.x, r0.y, r0.y
mov r2.x, c5.x
add_pp r1.y, r2.x, -c4.x
texld r1, r1, s0
texld r2, t0, s1
mul_pp r2.w, r1.x, c5.y
mov r1, c0
mad_pp r0.yzw, c2.wzyx, r2.wzyx, -r1.wzyx
mul_pp r2.xyz, r2, c2
mad_pp r0.yzw, c3.x, r0, r1.wzyx
mul_pp r0.yzw, r0, r2.w
mad_pp r2.w, c3.x, -r1.w, r1.w
mad_pp r0.yzw, r2.wzyx, r2.w, r0
mul_pp r0.yzw, r0, c1.wzyx
mul_pp r0.xyz, r0.x, r0.wzyx
mov_pp r0.w, c5.x
mov_pp oC0, r0

"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_OFF" }
Vector 2 [_Color]
Float 4 [_Glossiness]
Vector 1 [_LightColor0]
Float 3 [_Metallic]
Vector 0 [unity_ColorSpaceDielectricSpec]
SetTexture 0 [unity_NHxRoughness] 2D 0
SetTexture 1 [_MainTex] 2D 1
SetTexture 2 [_LightTexture0] 2D 2
SetTexture 3 [_LightTextureB0] 2D 3
"ps_2_0
def c5, 0.5, 0, 1, 16
dcl t0.xy
dcl_pp t1.xyz
dcl_pp t2
dcl_pp t3
dcl_pp t4
dcl_pp t5
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
rcp r0.w, t5.w
mad_pp r0.xy, t5, r0.w, c5.x
dp3 r1.w, t5, t5
mov r1.xy, r1.w
nrm_pp r2.xyz, t4
dp3_pp r2.w, -t1, r2
add_pp r2.w, r2.w, r2.w
mad_pp r3.xyz, r2, -r2.w, -t1
mov_pp r4.x, t2.w
mov_pp r4.y, t3.w
mov_pp r4.z, t4.w
dp3_pp r2.w, r3, r4
mul_pp r2.w, r2.w, r2.w
mul_pp r3.x, r2.w, r2.w
mov r2.w, c5.z
add_pp r3.y, r2.w, -c4.x
texld_pp r0, r0, s2
texld_pp r1, r1, s3
texld r3, r3, s0
texld r5, t0, s1
mul r2.w, r0.w, r1.x
mul_pp r0.xyz, r2.w, c1
cmp_pp r0.xyz, -t5.z, c5.y, r0
dp3_sat_pp r0.w, r2, r4
mul_pp r5.w, r3.x, c5.w
mov r1, c0
mad_pp r2.xyz, c2, r5, -r1
mul_pp r3.xyz, r5, c2
mad_pp r1.xyz, c3.x, r2, r1
mul_pp r1.xyz, r1, r5.w
mad_pp r1.w, c3.x, -r1.w, r1.w
mad_pp r1.xyz, r3, r1.w, r1
mul_pp r0.xyz, r0, r1
mul_pp r0.xyz, r0.w, r0
mov r0.w, c5.z
mov_pp oC0, r0

"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_OFF" }
Vector 2 [_Color]
Float 4 [_Glossiness]
Vector 1 [_LightColor0]
Float 3 [_Metallic]
Vector 0 [unity_ColorSpaceDielectricSpec]
SetTexture 0 [unity_NHxRoughness] 2D 0
SetTexture 1 [_MainTex] 2D 1
SetTexture 2 [_LightTexture0] CUBE 2
SetTexture 3 [_LightTextureB0] 2D 3
"ps_2_0
def c5, 1, 16, 0, 0
dcl t0.xy
dcl_pp t1.xyz
dcl_pp t2
dcl_pp t3
dcl_pp t4
dcl t5.xyz
dcl_2d s0
dcl_2d s1
dcl_cube s2
dcl_2d s3
dp3 r0.w, t5, t5
mov r0.xy, r0.w
nrm_pp r1.xyz, t4
dp3_pp r1.w, -t1, r1
add_pp r1.w, r1.w, r1.w
mad_pp r2.xyz, r1, -r1.w, -t1
mov_pp r3.x, t2.w
mov_pp r3.y, t3.w
mov_pp r3.z, t4.w
dp3_pp r1.w, r2, r3
mul_pp r1.w, r1.w, r1.w
mul_pp r2.x, r1.w, r1.w
mov r1.w, c5.x
add_pp r2.y, r1.w, -c4.x
texld r0, r0, s3
texld r4, t5, s2
texld r2, r2, s0
texld r5, t0, s1
mul_pp r1.w, r0.x, r4.w
mul_pp r0.xyz, r1.w, c1
dp3_sat_pp r0.w, r1, r3
mul_pp r5.w, r2.x, c5.y
mov r1, c0
mad_pp r2.xyz, c2, r5, -r1
mul_pp r3.xyz, r5, c2
mad_pp r1.xyz, c3.x, r2, r1
mul_pp r1.xyz, r1, r5.w
mad_pp r1.w, c3.x, -r1.w, r1.w
mad_pp r1.xyz, r3, r1.w, r1
mul_pp r0.xyz, r0, r1
mul_pp r0.xyz, r0.w, r0
mov_pp r0.w, c5.x
mov_pp oC0, r0

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_OFF" }
Vector 2 [_Color]
Float 4 [_Glossiness]
Vector 1 [_LightColor0]
Float 3 [_Metallic]
Vector 0 [unity_ColorSpaceDielectricSpec]
SetTexture 0 [unity_NHxRoughness] 2D 0
SetTexture 1 [_MainTex] 2D 1
SetTexture 2 [_LightTexture0] 2D 2
"ps_2_0
def c5, 1, 16, 0, 0
dcl t0.xy
dcl_pp t1.xyz
dcl_pp t2
dcl_pp t3
dcl_pp t4
dcl t5.xy
dcl_2d s0
dcl_2d s1
dcl_2d s2
nrm_pp r0.xyz, t4
dp3_pp r0.w, -t1, r0
add_pp r0.w, r0.w, r0.w
mad_pp r1.xyz, r0, -r0.w, -t1
mov_pp r2.x, t2.w
mov_pp r2.y, t3.w
mov_pp r2.z, t4.w
dp3_pp r0.w, r1, r2
dp3_sat_pp r0.x, r0, r2
mul_pp r0.y, r0.w, r0.w
mul_pp r1.x, r0.y, r0.y
mov r2.x, c5.x
add_pp r1.y, r2.x, -c4.x
texld r1, r1, s0
texld r2, t0, s1
texld_pp r3, t5, s2
mul_pp r2.w, r1.x, c5.y
mov r1, c0
mad_pp r0.yzw, c2.wzyx, r2.wzyx, -r1.wzyx
mul_pp r2.xyz, r2, c2
mad_pp r0.yzw, c3.x, r0, r1.wzyx
mul_pp r0.yzw, r0, r2.w
mad_pp r2.w, c3.x, -r1.w, r1.w
mad_pp r0.yzw, r2.wzyx, r2.w, r0
mul_pp r1.xyz, r3.w, c1
mul_pp r0.yzw, r0, r1.wzyx
mul_pp r0.xyz, r0.x, r0.wzyx
mov_pp r0.w, c5.x
mov_pp oC0, r0

"
}
SubProgram "d3d9 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_NATIVE" }
Vector 3 [_Color]
Float 5 [_Glossiness]
Vector 2 [_LightColor0]
Vector 0 [_LightShadowData]
Float 4 [_Metallic]
Vector 1 [unity_ColorSpaceDielectricSpec]
SetTexture 0 [unity_NHxRoughness] 2D 0
SetTexture 1 [_MainTex] 2D 1
SetTexture 2 [_ShadowMapTexture] 2D 2
SetTexture 3 [_LightTexture0] 2D 3
SetTexture 4 [_LightTextureB0] 2D 4
"ps_2_0
def c6, 0.5, 0, 1, 16
dcl t0.xy
dcl_pp t1.xyz
dcl_pp t2
dcl_pp t3
dcl_pp t4
dcl_pp t5
dcl t6
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
dcl_2d s4
rcp r0.w, t5.w
mad_pp r0.xy, t5, r0.w, c6.x
dp3_pp r1.w, t5, t5
mov_pp r1.xy, r1.w
nrm_pp r2.xyz, t4
dp3_pp r2.w, -t1, r2
add_pp r2.w, r2.w, r2.w
mad_pp r3.xyz, r2, -r2.w, -t1
mov_pp r4.x, t2.w
mov_pp r4.y, t3.w
mov_pp r4.z, t4.w
dp3_pp r2.w, r3, r4
mul_pp r2.w, r2.w, r2.w
mul_pp r3.x, r2.w, r2.w
mov r2.w, c6.z
add_pp r3.y, r2.w, -c5.x
texld_pp r0, r0, s3
texld_pp r1, r1, s4
texldp_pp r5, t6, s2
texld r3, r3, s0
texld r6, t0, s1
mul r4.w, r0.w, r1.x
cmp r4.w, -t5.z, c6.y, r4.w
lrp_pp r6.w, r5.x, r2.w, c0.x
mul_pp r2.w, r4.w, r6.w
mul_pp r0.xyz, r2.w, c2
dp3_sat_pp r0.w, r2, r4
mul_pp r6.w, r3.x, c6.w
mov r1, c1
mad_pp r2.xyz, c3, r6, -r1
mul_pp r3.xyz, r6, c3
mad_pp r1.xyz, c4.x, r2, r1
mul_pp r1.xyz, r1, r6.w
mad_pp r1.w, c4.x, -r1.w, r1.w
mad_pp r1.xyz, r3, r1.w, r1
mul_pp r0.xyz, r0, r1
mul_pp r0.xyz, r0.w, r0
mov r0.w, c6.z
mov_pp oC0, r0

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
Vector 2 [_Color]
Float 4 [_Glossiness]
Vector 1 [_LightColor0]
Float 3 [_Metallic]
Vector 0 [unity_ColorSpaceDielectricSpec]
SetTexture 0 [unity_NHxRoughness] 2D 0
SetTexture 1 [_MainTex] 2D 1
SetTexture 2 [_ShadowMapTexture] 2D 2
"ps_2_0
def c5, 1, 16, 0, 0
dcl t0.xy
dcl_pp t1.xyz
dcl_pp t2
dcl_pp t3
dcl_pp t4
dcl_pp t5
dcl_2d s0
dcl_2d s1
dcl_2d s2
nrm_pp r0.xyz, t4
dp3_pp r0.w, -t1, r0
add_pp r0.w, r0.w, r0.w
mad_pp r1.xyz, r0, -r0.w, -t1
mov_pp r2.x, t2.w
mov_pp r2.y, t3.w
mov_pp r2.z, t4.w
dp3_pp r0.w, r1, r2
dp3_sat_pp r0.x, r0, r2
mul_pp r0.y, r0.w, r0.w
mul_pp r1.x, r0.y, r0.y
mov r2.x, c5.x
add_pp r1.y, r2.x, -c4.x
texld r1, r1, s0
texld r2, t0, s1
texldp_pp r3, t5, s2
mul_pp r2.w, r1.x, c5.y
mov r1, c0
mad_pp r0.yzw, c2.wzyx, r2.wzyx, -r1.wzyx
mul_pp r2.xyz, r2, c2
mad_pp r0.yzw, c3.x, r0, r1.wzyx
mul_pp r0.yzw, r0, r2.w
mad_pp r2.w, c3.x, -r1.w, r1.w
mad_pp r0.yzw, r2.wzyx, r2.w, r0
mul_pp r1.xyz, r3.x, c1
mul_pp r0.yzw, r0, r1.wzyx
mul_pp r0.xyz, r0.x, r0.wzyx
mov_pp r0.w, c5.x
mov_pp oC0, r0

"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
Vector 2 [_Color]
Float 4 [_Glossiness]
Vector 1 [_LightColor0]
Float 3 [_Metallic]
Vector 0 [unity_ColorSpaceDielectricSpec]
SetTexture 0 [unity_NHxRoughness] 2D 0
SetTexture 1 [_MainTex] 2D 1
SetTexture 2 [_ShadowMapTexture] 2D 2
SetTexture 3 [_LightTexture0] 2D 3
"ps_2_0
def c5, 1, 16, 0, 0
dcl t0.xy
dcl_pp t1.xyz
dcl_pp t2
dcl_pp t3
dcl_pp t4
dcl t5.xy
dcl_pp t6
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
nrm_pp r0.xyz, t4
dp3_pp r0.w, -t1, r0
add_pp r0.w, r0.w, r0.w
mad_pp r1.xyz, r0, -r0.w, -t1
mov_pp r2.x, t2.w
mov_pp r2.y, t3.w
mov_pp r2.z, t4.w
dp3_pp r0.w, r1, r2
mul_pp r0.w, r0.w, r0.w
mul_pp r1.x, r0.w, r0.w
mov r0.w, c5.x
add_pp r1.y, r0.w, -c4.x
texld r3, t5, s3
texldp_pp r4, t6, s2
texld r1, r1, s0
texld r5, t0, s1
mul_pp r0.w, r3.w, r4.x
mul_pp r1.yzw, r0.w, c1.wzyx
dp3_sat_pp r5.w, r0, r2
mul_pp r0.x, r1.x, c5.y
mov r2, c0
mad_pp r0.yzw, c2.wzyx, r5.wzyx, -r2.wzyx
mul_pp r3.xyz, r5, c2
mad_pp r0.yzw, c3.x, r0, r2.wzyx
mul_pp r0.xyz, r0.wzyx, r0.x
mad_pp r0.w, c3.x, -r2.w, r2.w
mad_pp r0.xyz, r3, r0.w, r0
mul_pp r0.xyz, r1.wzyx, r0
mul_pp r0.xyz, r5.w, r0
mov_pp r0.w, c5.x
mov_pp oC0, r0

"
}
SubProgram "d3d9 " {
Keywords { "POINT" "SHADOWS_CUBE" }
Vector 4 [_Color]
Float 6 [_Glossiness]
Vector 3 [_LightColor0]
Vector 0 [_LightPositionRange]
Vector 1 [_LightShadowData]
Float 5 [_Metallic]
Vector 2 [unity_ColorSpaceDielectricSpec]
SetTexture 0 [unity_NHxRoughness] 2D 0
SetTexture 1 [_MainTex] 2D 1
SetTexture 2 [_ShadowMapTexture] CUBE 2
SetTexture 3 [_LightTexture0] 2D 3
"ps_2_0
def c7, 0.970000029, 1, 16, 0
dcl t0.xy
dcl_pp t1.xyz
dcl_pp t2
dcl_pp t3
dcl_pp t4
dcl t5.xyz
dcl t6.xyz
dcl_2d s0
dcl_2d s1
dcl_cube s2
dcl_2d s3
dp3 r0.w, t5, t5
mov r0.xy, r0.w
nrm_pp r1.xyz, t4
dp3_pp r1.w, -t1, r1
add_pp r1.w, r1.w, r1.w
mad_pp r2.xyz, r1, -r1.w, -t1
mov_pp r3.x, t2.w
mov_pp r3.y, t3.w
mov_pp r3.z, t4.w
dp3_pp r1.w, r2, r3
mul_pp r1.w, r1.w, r1.w
mul_pp r2.x, r1.w, r1.w
mov r1.w, c7.y
add_pp r2.y, r1.w, -c6.x
texld r4, t6, s2
texld r0, r0, s3
texld r2, r2, s0
texld r5, t0, s1
dp3 r3.w, t6, t6
rsq r3.w, r3.w
rcp r3.w, r3.w
mul r3.w, r3.w, c0.w
mad r3.w, r3.w, -c7.x, r4.x
cmp_pp r1.w, r3.w, r1.w, c1.x
mul_pp r1.w, r0.x, r1.w
mul_pp r0.xyz, r1.w, c3
dp3_sat_pp r0.w, r1, r3
mul_pp r5.w, r2.x, c7.z
mov r1, c2
mad_pp r2.xyz, c4, r5, -r1
mul_pp r3.xyz, r5, c4
mad_pp r1.xyz, c5.x, r2, r1
mul_pp r1.xyz, r1, r5.w
mad_pp r1.w, c5.x, -r1.w, r1.w
mad_pp r1.xyz, r3, r1.w, r1
mul_pp r0.xyz, r0, r1
mul_pp r0.xyz, r0.w, r0
mov_pp r0.w, c7.y
mov_pp oC0, r0

"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
Vector 4 [_Color]
Float 6 [_Glossiness]
Vector 3 [_LightColor0]
Vector 0 [_LightPositionRange]
Vector 1 [_LightShadowData]
Float 5 [_Metallic]
Vector 2 [unity_ColorSpaceDielectricSpec]
SetTexture 0 [unity_NHxRoughness] 2D 0
SetTexture 1 [_MainTex] 2D 1
SetTexture 2 [_ShadowMapTexture] CUBE 2
SetTexture 3 [_LightTexture0] CUBE 3
SetTexture 4 [_LightTextureB0] 2D 4
"ps_2_0
def c7, 0.970000029, 1, 16, 0
dcl t0.xy
dcl_pp t1.xyz
dcl_pp t2
dcl_pp t3
dcl_pp t4
dcl t5.xyz
dcl t6.xyz
dcl_2d s0
dcl_2d s1
dcl_cube s2
dcl_cube s3
dcl_2d s4
texld r0, t6, s2
texld r1, t5, s3
texld r2, t0, s1
dp3 r2.w, t6, t6
rsq r2.w, r2.w
rcp r2.w, r2.w
mul r2.w, r2.w, c0.w
mad r2.w, r2.w, -c7.x, r0.x
mov r0.y, c7.y
cmp_pp r2.w, r2.w, r0.y, c1.x
dp3 r1.xy, t5, t5
nrm_pp r3.xyz, t4
dp3_pp r3.w, -t1, r3
add_pp r3.w, r3.w, r3.w
mad_pp r4.xyz, r3, -r3.w, -t1
mov_pp r5.x, t2.w
mov_pp r5.y, t3.w
mov_pp r5.z, t4.w
dp3_pp r3.w, r4, r5
mul_pp r3.w, r3.w, r3.w
mul_pp r4.x, r3.w, r3.w
add_pp r4.y, r0.y, -c6.x
texld r0, r1, s4
texld r4, r4, s0
mul r3.w, r1.w, r0.x
mul_pp r2.w, r2.w, r3.w
mul_pp r0.xyz, r2.w, c3
dp3_sat_pp r0.w, r3, r5
mul_pp r2.w, r4.x, c7.z
mov r1, c2
mad_pp r3.xyz, c4, r2, -r1
mul_pp r2.xyz, r2, c4
mad_pp r1.xyz, c5.x, r3, r1
mul_pp r1.xyz, r1, r2.w
mad_pp r1.w, c5.x, -r1.w, r1.w
mad_pp r1.xyz, r2, r1.w, r1
mul_pp r0.xyz, r0, r1
mul_pp r0.xyz, r0.w, r0
mov_pp r0.w, c7.y
mov_pp oC0, r0

"
}
}
 }
 Pass {
  Name "SHADOWCASTER"
  Tags { "LIGHTMODE"="SHADOWCASTER" "SHADOWSUPPORT"="true" "RenderType"="Opaque" "PerformanceChecks"="False" }
  GpuProgramID 502614
Program "vp" {
SubProgram "d3d9 " {
Keywords { "SHADOWS_DEPTH" }
Bind "vertex" Vertex
Bind "normal" Normal
Matrix 8 [_Object2World] 3
Matrix 11 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [unity_MatrixVP]
Vector 14 [_WorldSpaceLightPos0]
Vector 15 [unity_LightShadowBias]
"vs_2_0
def c16, 1, 0, 0, 0
dcl_position v0
dcl_normal v1
mul r0.x, c15.z, c15.z
slt r0.x, -r0.x, r0.x
mul r0.yzw, v1.y, c12.xxyz
mad r0.yzw, c11.xxyz, v1.x, r0
mad r0.yzw, c13.xxyz, v1.z, r0
nrm r1.xyz, r0.yzww
mad r2, v0.xyzx, c16.xxxy, c16.yyyx
dp4 r3.x, c8, r2
dp4 r3.y, c9, r2
dp4 r3.z, c10, r2
mad r0.yzw, r3.xxyz, -c14.w, c14.xxyz
nrm r4.xyz, r0.yzww
dp3 r0.y, r1, r4
mad r0.y, r0.y, -r0.y, c16.x
rsq r0.y, r0.y
rcp r0.y, r0.y
mul r0.y, r0.y, c15.z
mad r1.xyz, r1, -r0.y, r3
mov r1.w, c16.x
dp4 r3.x, c4, r1
dp4 r3.y, c5, r1
dp4 r3.z, c6, r1
dp4 r3.w, c7, r1
dp4 r1.x, c0, r2
dp4 r1.y, c1, r2
dp4 r1.z, c2, r2
dp4 r1.w, c3, r2
lrp r2, r0.x, r3, r1
rcp r0.x, r2.w
mul r0.x, r0.x, c15.x
max r0.x, r0.x, c16.y
min r0.x, r0.x, c16.x
add r0.x, r0.x, r2.z
max r0.y, r0.x, c16.y
lrp r2.z, c15.y, r0.y, r0.x
mov oT0, r2
mov oPos, r2

"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_CUBE" }
Bind "vertex" Vertex
Matrix 4 [_Object2World] 3
Matrix 0 [glstate_matrix_mvp]
Vector 7 [_LightPositionRange]
"vs_2_0
dcl_position v0
dp4 r0.x, c4, v0
dp4 r0.y, c5, v0
dp4 r0.z, c6, v0
add oT0.xyz, r0, -c7
dp4 oPos.x, c0, v0
dp4 oPos.y, c1, v0
dp4 oPos.z, c2, v0
dp4 oPos.w, c3, v0

"
}
}
Program "fp" {
SubProgram "d3d9 " {
Keywords { "SHADOWS_DEPTH" }
"ps_2_0
dcl t0
rcp r0.w, t0.w
mul_pp r0, r0.w, t0.z
mov_pp oC0, r0

"
}
SubProgram "d3d9 " {
Keywords { "SHADOWS_CUBE" }
Vector 0 [_LightPositionRange]
"ps_2_0
dcl t0.xyz
dp3 r0.w, t0, t0
rsq r0.x, r0.w
rcp r0.x, r0.x
mul_pp r0, r0.x, c0.w
mov_pp oC0, r0

"
}
}
 }
 Pass {
  Name "META"
  Tags { "LIGHTMODE"="Meta" "RenderType"="Opaque" "PerformanceChecks"="False" }
  Cull Off
  GpuProgramID 560566
Program "vp" {
SubProgram "d3d9 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "texcoord2" TexCoord2
Matrix 0 [glstate_matrix_mvp]
Vector 7 [_DetailAlbedoMap_ST]
Vector 6 [_MainTex_ST]
Float 8 [_UVSec]
Vector 5 [unity_DynamicLightmapST]
Vector 4 [unity_LightmapST]
Vector 9 [unity_MetaVertexControl]
"vs_2_0
def c10, 0, 9.99999975e-005, 0, 0
dcl_position v0
dcl_texcoord v1
dcl_texcoord1 v2
dcl_texcoord2 v3
slt r0.x, c10.x, v0.z
mul r0.z, r0.x, c10.y
mad r0.xy, v2, c4, c4.zwzw
lrp r1.xyz, c9.x, r0, v0
slt r0.x, c10.x, r1.z
mul r0.z, r0.x, c10.y
mad r0.xy, v3, c5, c5.zwzw
lrp r2.xyz, c9.y, r0, r1
mov r2.w, v0.w
dp4 oPos.x, c0, r2
dp4 oPos.y, c1, r2
dp4 oPos.z, c2, r2
dp4 oPos.w, c3, r2
mad oT0.xy, v1, c6, c6.zwzw
mul r0.x, c8.x, c8.x
sge r0.x, -r0.x, r0.x
mov r1.xy, v2
add r0.yz, -r1.xxyw, v1.xxyw
mad r0.xy, r0.x, r0.yzzw, v2
mad oT0.zw, r0.xyxy, c7.xyxy, c7

"
}
}
Program "fp" {
SubProgram "d3d9 " {
Vector 1 [_Color]
Float 3 [_Glossiness]
Float 2 [_Metallic]
Vector 0 [unity_ColorSpaceDielectricSpec]
Float 6 [unity_MaxOutputValue]
Vector 4 [unity_MetaFragmentControl]
Float 5 [unity_OneOverOutputBoost]
SetTexture 0 [_MainTex] 2D 0
"ps_2_0
def c7, 1, 0.5, 0, 0
def c8, 0, 0, 0, 0.0199999996
dcl t0.xy
dcl_2d s0
texld r0, t0, s0
mov r1.xz, c7
add_pp r0.w, r1.x, -c3.x
mul_pp r0.w, r0.w, r0.w
mov r2, c0
mad_pp r3.xyz, c1, r0, -r2
mul_pp r0.xyz, r0, c1
mad_pp r2.xyz, c2.x, r3, r2
mul_pp r2.xyz, r0.w, r2
mul_pp r2.xyz, r2, c7.y
mad_pp r0.w, c2.x, -r2.w, r2.w
mad_pp r0.xyz, r0, r0.w, r2
log r2.x, r0.x
log r2.y, r0.y
log r2.z, r0.z
mov_sat r2.w, c5.x
mul r0.xyz, r2, r2.w
exp_pp r2.x, r0.x
exp_pp r2.y, r0.y
exp_pp r2.z, r0.z
min_pp r0.xyz, c6.x, r2
cmp_pp r0.xyz, -c4.x, r1.z, r0
mov r0.w, c4.x
mov r1.y, c4.y
cmp_pp r0, -r1.y, r0, c8
mov_pp oC0, r0

"
}
}
 }
}
Fallback "VertexLit"
}