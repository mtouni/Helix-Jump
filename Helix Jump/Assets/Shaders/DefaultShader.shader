Shader "Custom/Default_Material" {
	//属性
	Properties
       {
			_MainColor("主颜色",Color) = (1,1,1,0)	//四值（RGBA）
			_MidColor("中间颜色",Color) = (1,1,1,0)	//四值（RGBA）
			_SpecColor("反射高光",Color) = (1,1,1,0) //
			_Shininess("光泽度",Range(0.1,2)) = 0.7
			_Transparency("透明效果",Range(0,1)) = 0.5
			_AOFactor("",Range(0.1,2)) = 1
			//_Emission("自发光",Color) = (0,0,0,0)
			//_MainTex("主纹理",2D) = "white"{}
       }
	//子着色器
    SubShader
    {

	//通道
      Pass
      {
		 //材质块被用于定义对象的材质属性
         Material
         {
			//漫反射颜色构成。这是对象的基本颜色。
            Diffuse[_MainColor]
			//半阴影 Semishadow Material Color
			//Shaders[_MidColor]
			//反射高光
			Specular[_SpecColor]
			//光泽度
			Shininess[_Shininess]
			//透明效果
			//Transparency[_Transparency]
			//环境
			Ambient[_AOFactor]
			
			//自发光
			//Emission[_Emission]
         }
		 //开启光照，也就是定义材质块中的设定是否有效。想要有效的话必须使用Lighting On命令开启光照，而颜色则通过Color命令直接给出。
         Lighting On
		 //开启独立镜面反射。这个命令会添加高光光照到着色器通道的末尾，因此贴图对高光没有影响。只在光照开启时有效。
		 //SeparateSpecular On | Off 
		//使用每顶点的颜色替代材质中的颜色集。AmbientAndDiffuse 替代材质的阴影光和漫反射值;Emission 替代材质中的光发射值。
		//ColorMaterial AmbientAndDiffuse | Emission 
      }
    }

   //备胎设为Unity自带的普通漫反射  
    Fallback" Diffuse "  
}