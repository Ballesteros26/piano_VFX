using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000043 RID: 67
	[AttributeUsage(AttributeTargets.Field)]
	public class SurfaceDataAttributes : Attribute
	{
		// Token: 0x06000183 RID: 387 RVA: 0x000079BD File Offset: 0x00005BBD
		public SurfaceDataAttributes(string displayName = "", bool isDirection = false, bool sRGBDisplay = false, FieldPrecision precision = FieldPrecision.Default)
		{
			this.displayNames = new string[1];
			this.displayNames[0] = displayName;
			this.isDirection = isDirection;
			this.sRGBDisplay = sRGBDisplay;
			this.precision = precision;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000079F0 File Offset: 0x00005BF0
		public SurfaceDataAttributes(string[] displayNames, bool isDirection = false, bool sRGBDisplay = false, FieldPrecision precision = FieldPrecision.Default)
		{
			this.displayNames = displayNames;
			this.isDirection = isDirection;
			this.sRGBDisplay = sRGBDisplay;
			this.precision = precision;
		}

		// Token: 0x0400011E RID: 286
		public string[] displayNames;

		// Token: 0x0400011F RID: 287
		public bool isDirection;

		// Token: 0x04000120 RID: 288
		public bool sRGBDisplay;

		// Token: 0x04000121 RID: 289
		public FieldPrecision precision;
	}
}
