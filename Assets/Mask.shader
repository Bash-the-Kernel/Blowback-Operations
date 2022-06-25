Shader "Custom/Mask"
{
	SubShader
	{
		Tags{"Queue" = "Transparent+1"}

		Pass
		{
			Blend zero one
		}
	}
}
