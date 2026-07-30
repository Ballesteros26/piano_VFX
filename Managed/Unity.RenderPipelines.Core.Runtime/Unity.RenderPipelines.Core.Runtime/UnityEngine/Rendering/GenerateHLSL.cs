using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000042 RID: 66
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum)]
	public class GenerateHLSL : Attribute
	{
		// Token: 0x06000182 RID: 386 RVA: 0x00007980 File Offset: 0x00005B80
		public GenerateHLSL(PackingRules rules = PackingRules.Exact, bool needAccessors = true, bool needSetters = false, bool needParamDebug = false, int paramDefinesStart = 1, bool omitStructDeclaration = false, bool containsPackedFields = false)
		{
			this.packingRules = rules;
			this.needAccessors = needAccessors;
			this.needSetters = needSetters;
			this.needParamDebug = needParamDebug;
			this.paramDefinesStart = paramDefinesStart;
			this.omitStructDeclaration = omitStructDeclaration;
			this.containsPackedFields = containsPackedFields;
		}

		// Token: 0x04000117 RID: 279
		public PackingRules packingRules;

		// Token: 0x04000118 RID: 280
		public bool containsPackedFields;

		// Token: 0x04000119 RID: 281
		public bool needAccessors;

		// Token: 0x0400011A RID: 282
		public bool needSetters;

		// Token: 0x0400011B RID: 283
		public bool needParamDebug;

		// Token: 0x0400011C RID: 284
		public int paramDefinesStart;

		// Token: 0x0400011D RID: 285
		public bool omitStructDeclaration;
	}
}
