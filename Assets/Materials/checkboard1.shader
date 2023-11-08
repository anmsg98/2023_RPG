Shader "Unlit/Checkerboard1"
{
	SubShader
	{
		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;			//A-1. 텍스쳐 UV 전달 변수 선언
			};

			v2f vert (float3 vertex : POSITION,
				float2 uv : TEXCOORD0			//A-2. 전달 받을 uv 파라미터 변수 선언
				)
			{
				v2f o;
				o.pos = UnityObjectToClipPos (vertex);
				o.uv = uv;						//A-3. uv좌표를 fragment로 전달.
				return o;
			}

			float4 frag (v2f i) : SV_Target
			{
				float lineCount = 10;			//1. 10개의 라인갯수

				float2 c = i.uv * lineCount;	//2. uv값을  소수점이 있는 0,0f ~ 50.0f으로 늘림 ()
				c = floor (c);					//3. 0~50까지 1단위로 변경
				c = c / 2;						//4. 0~25까지 0.5단위로 변경, 소수점은 0또는 0.5다.
				float check = c.x + c.y;		//5. x값과 y값을 더함 (경우의 수:0(검), 0.5(흰), 1.0(검));
				check = frac (check);			//6. 경우의 수를 0, 0.5로 나오도록 변경
				check *= 2;						//6. 0또는 1값으로만 표현되도록 변경

				return check;
			}
			
			ENDCG
		}
	}
}