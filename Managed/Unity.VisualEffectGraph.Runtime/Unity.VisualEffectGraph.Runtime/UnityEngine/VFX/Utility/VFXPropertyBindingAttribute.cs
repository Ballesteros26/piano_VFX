using System;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x02000025 RID: 37
	[AttributeUsage(AttributeTargets.Field)]
	public class VFXPropertyBindingAttribute : PropertyAttribute
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00004A02 File Offset: 0x00002C02
		public VFXPropertyBindingAttribute(params string[] editorTypes)
		{
			this.EditorTypes = editorTypes;
		}

		// Token: 0x04000093 RID: 147
		public string[] EditorTypes;
	}
}
