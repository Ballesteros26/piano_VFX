using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000142 RID: 322
	[Serializable]
	public class BoolScalableSetting : ScalableSetting<bool>
	{
		// Token: 0x0600095A RID: 2394 RVA: 0x0004BF61 File Offset: 0x0004A161
		public BoolScalableSetting(bool[] values, ScalableSettingSchemaId schemaId)
			: base(values, schemaId)
		{
		}
	}
}
