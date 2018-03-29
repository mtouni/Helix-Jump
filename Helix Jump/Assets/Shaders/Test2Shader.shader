Shader "Custom/Lighting_Material2" {
	Properties
       {
          _ColorMain("主颜色",Color) = (15,.1,.5,1)
       }
    SubShader
    {

      Pass
      {
         Material
         {
            Diffuse[_ColorMain]
            Ambient[_ColorMain]
         }
         Lighting On
      }
    }

   //备胎设为Unity自带的普通漫反射  
       Fallback" Diffuse "  
}