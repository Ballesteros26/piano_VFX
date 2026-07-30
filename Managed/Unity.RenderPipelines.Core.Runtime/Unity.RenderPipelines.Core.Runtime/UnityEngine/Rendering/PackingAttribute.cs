using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000045 RID: 69
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class PackingAttribute : Attribute
	{
		// Token: 0x06000186 RID: 390 RVA: 0x00007A2C File Offset: 0x00005C2C
		public PackingAttribute(string[] displayNames, FieldPacking packingScheme = FieldPacking.NoPacking, int bitSize = 32, int offsetInSource = 0, float minValue = 0f, float maxValue = 1f, bool isDirection = false, bool sRGBDisplay = false)
		{
			this.displayNames = displayNames;
			this.packingScheme = packingScheme;
			this.offsetInSource = offsetInSource;
			this.isDirection = isDirection;
			this.sRGBDisplay = sRGBDisplay;
			this.sizeInBits = bitSize;
			this.range = new float[] { minValue, maxValue };
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007A84 File Offset: 0x00005C84
		public PackingAttribute(string displayName = "", FieldPacking packingScheme = FieldPacking.NoPacking, int bitSize = 0, int offsetInSource = 0, float minValue = 0f, float maxValue = 1f, bool isDirection = false, bool sRGBDisplay = false)
		{
			this.displayNames = new string[1];
			this.displayNames[0] = displayName;
			this.packingScheme = packingScheme;
			this.offsetInSource = offsetInSource;
			this.isDirection = isDirection;
			this.sRGBDisplay = sRGBDisplay;
			this.sizeInBits = bitSize;
			this.range = new float[] { minValue, maxValue };
		}

		// Token: 0x04000124 RID: 292
		public string[] displayNames;

		// Token: 0x04000125 RID: 293
		public float[] range;

		// Token: 0x04000126 RID: 294
		public FieldPacking packingScheme;

		// Token: 0x04000127 RID: 295
		public int offsetInSource;

		// Token: 0x04000128 RID: 296
		public int sizeInBits;

		// Token: 0x04000129 RID: 297
		public bool isDirection;

		// Token: 0x0400012A RID: 298
		public bool sRGBDisplay;
	}
}
