using System;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x020003C1 RID: 961
	[UsedByNativeCode]
	public struct LightDataGI
	{
		// Token: 0x0600216E RID: 8558 RVA: 0x000382EC File Offset: 0x000364EC
		public void Init(ref DirectionalLight light, ref Cookie cookie)
		{
			this.instanceID = light.instanceID;
			this.cookieID = cookie.instanceID;
			this.cookieScale = cookie.scale;
			this.color = light.color;
			this.indirectColor = light.indirectColor;
			this.orientation = light.orientation;
			this.position = light.position;
			this.range = 0f;
			this.coneAngle = cookie.sizes.x;
			this.innerConeAngle = cookie.sizes.y;
			this.shape0 = light.penumbraWidthRadian;
			this.shape1 = 0f;
			this.type = LightType.Directional;
			this.mode = light.mode;
			this.shadow = (light.shadow ? 1 : 0);
			this.falloff = FalloffType.Undefined;
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x000383C0 File Offset: 0x000365C0
		public void Init(ref PointLight light, ref Cookie cookie)
		{
			this.instanceID = light.instanceID;
			this.cookieID = cookie.instanceID;
			this.cookieScale = cookie.scale;
			this.color = light.color;
			this.indirectColor = light.indirectColor;
			this.orientation = Quaternion.identity;
			this.position = light.position;
			this.range = light.range;
			this.coneAngle = 0f;
			this.innerConeAngle = 0f;
			this.shape0 = light.sphereRadius;
			this.shape1 = 0f;
			this.type = LightType.Point;
			this.mode = light.mode;
			this.shadow = (light.shadow ? 1 : 0);
			this.falloff = light.falloff;
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x0003848C File Offset: 0x0003668C
		public void Init(ref SpotLight light, ref Cookie cookie)
		{
			this.instanceID = light.instanceID;
			this.cookieID = cookie.instanceID;
			this.cookieScale = cookie.scale;
			this.color = light.color;
			this.indirectColor = light.indirectColor;
			this.orientation = light.orientation;
			this.position = light.position;
			this.range = light.range;
			this.coneAngle = light.coneAngle;
			this.innerConeAngle = light.innerConeAngle;
			this.shape0 = light.sphereRadius;
			this.shape1 = (float)light.angularFalloff;
			this.type = LightType.Spot;
			this.mode = light.mode;
			this.shadow = (light.shadow ? 1 : 0);
			this.falloff = light.falloff;
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x00038560 File Offset: 0x00036760
		public void Init(ref RectangleLight light, ref Cookie cookie)
		{
			this.instanceID = light.instanceID;
			this.cookieID = cookie.instanceID;
			this.cookieScale = cookie.scale;
			this.color = light.color;
			this.indirectColor = light.indirectColor;
			this.orientation = light.orientation;
			this.position = light.position;
			this.range = light.range;
			this.coneAngle = 0f;
			this.innerConeAngle = 0f;
			this.shape0 = light.width;
			this.shape1 = light.height;
			this.type = LightType.Rectangle;
			this.mode = light.mode;
			this.shadow = (light.shadow ? 1 : 0);
			this.falloff = light.falloff;
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x00038630 File Offset: 0x00036830
		public void Init(ref DiscLight light, ref Cookie cookie)
		{
			this.instanceID = light.instanceID;
			this.cookieID = cookie.instanceID;
			this.cookieScale = cookie.scale;
			this.color = light.color;
			this.indirectColor = light.indirectColor;
			this.orientation = light.orientation;
			this.position = light.position;
			this.range = light.range;
			this.coneAngle = 0f;
			this.innerConeAngle = 0f;
			this.shape0 = light.radius;
			this.shape1 = 0f;
			this.type = LightType.Disc;
			this.mode = light.mode;
			this.shadow = (light.shadow ? 1 : 0);
			this.falloff = light.falloff;
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x00038700 File Offset: 0x00036900
		public void Init(ref SpotLightBoxShape light, ref Cookie cookie)
		{
			this.instanceID = light.instanceID;
			this.cookieID = cookie.instanceID;
			this.cookieScale = cookie.scale;
			this.color = light.color;
			this.indirectColor = light.indirectColor;
			this.orientation = light.orientation;
			this.position = light.position;
			this.range = light.range;
			this.coneAngle = 0f;
			this.innerConeAngle = 0f;
			this.shape0 = light.width;
			this.shape1 = light.height;
			this.type = LightType.SpotBoxShape;
			this.mode = light.mode;
			this.shadow = (light.shadow ? 1 : 0);
			this.falloff = FalloffType.Undefined;
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x000387CC File Offset: 0x000369CC
		public void Init(ref SpotLightPyramidShape light, ref Cookie cookie)
		{
			this.instanceID = light.instanceID;
			this.cookieID = cookie.instanceID;
			this.cookieScale = cookie.scale;
			this.color = light.color;
			this.indirectColor = light.indirectColor;
			this.orientation = light.orientation;
			this.position = light.position;
			this.range = light.range;
			this.coneAngle = light.angle;
			this.innerConeAngle = 0f;
			this.shape0 = light.aspectRatio;
			this.shape1 = 0f;
			this.type = LightType.SpotPyramidShape;
			this.mode = light.mode;
			this.shadow = (light.shadow ? 1 : 0);
			this.falloff = light.falloff;
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x0003889C File Offset: 0x00036A9C
		public void Init(ref DirectionalLight light)
		{
			Cookie cookie = Cookie.Defaults();
			this.Init(ref light, ref cookie);
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x000388BC File Offset: 0x00036ABC
		public void Init(ref PointLight light)
		{
			Cookie cookie = Cookie.Defaults();
			this.Init(ref light, ref cookie);
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x000388DC File Offset: 0x00036ADC
		public void Init(ref SpotLight light)
		{
			Cookie cookie = Cookie.Defaults();
			this.Init(ref light, ref cookie);
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x000388FC File Offset: 0x00036AFC
		public void Init(ref RectangleLight light)
		{
			Cookie cookie = Cookie.Defaults();
			this.Init(ref light, ref cookie);
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x0003891C File Offset: 0x00036B1C
		public void Init(ref DiscLight light)
		{
			Cookie cookie = Cookie.Defaults();
			this.Init(ref light, ref cookie);
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x0003893C File Offset: 0x00036B3C
		public void Init(ref SpotLightBoxShape light)
		{
			Cookie cookie = Cookie.Defaults();
			this.Init(ref light, ref cookie);
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x0003895C File Offset: 0x00036B5C
		public void Init(ref SpotLightPyramidShape light)
		{
			Cookie cookie = Cookie.Defaults();
			this.Init(ref light, ref cookie);
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x0003897A File Offset: 0x00036B7A
		public void InitNoBake(int lightInstanceID)
		{
			this.instanceID = lightInstanceID;
			this.mode = LightMode.Unknown;
		}

		// Token: 0x04000C30 RID: 3120
		public int instanceID;

		// Token: 0x04000C31 RID: 3121
		public int cookieID;

		// Token: 0x04000C32 RID: 3122
		public float cookieScale;

		// Token: 0x04000C33 RID: 3123
		public LinearColor color;

		// Token: 0x04000C34 RID: 3124
		public LinearColor indirectColor;

		// Token: 0x04000C35 RID: 3125
		public Quaternion orientation;

		// Token: 0x04000C36 RID: 3126
		public Vector3 position;

		// Token: 0x04000C37 RID: 3127
		public float range;

		// Token: 0x04000C38 RID: 3128
		public float coneAngle;

		// Token: 0x04000C39 RID: 3129
		public float innerConeAngle;

		// Token: 0x04000C3A RID: 3130
		public float shape0;

		// Token: 0x04000C3B RID: 3131
		public float shape1;

		// Token: 0x04000C3C RID: 3132
		public LightType type;

		// Token: 0x04000C3D RID: 3133
		public LightMode mode;

		// Token: 0x04000C3E RID: 3134
		public byte shadow;

		// Token: 0x04000C3F RID: 3135
		public FalloffType falloff;
	}
}
