Shader "AlphaSelfIllum" {
Properties {
 _Color ("Color Tint", Color) = (1,1,1,1)
 _MainTex ("SelfIllum Color (RGB) Alpha (A)", 2D) = "white" { }
}
SubShader { 
 Tags { "QUEUE"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" }
  Lighting On
  Material {
   Emission [_Color]
  }
  ZWrite Off
  Blend SrcAlpha OneMinusSrcAlpha
  SetTexture [_MainTex] { combine texture * primary, texture alpha * primary alpha }
 }
}
}