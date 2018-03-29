Shader "Custom/Lighting_Material" {
	Properties
       {
          _MainColor("主颜色",Color) = (1,.1,.5,1)
       }
    SubShader
    {

      Pass
      {
         Material
         {
            Diffuse[_MainColor]
            Ambient[_MainColor]
         }
         Lighting On
      }
    }

   //备胎设为Unity自带的普通漫反射  
       Fallback" Diffuse "  
}