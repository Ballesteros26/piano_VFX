using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200003E RID: 62
	public abstract class ParameterOverride
	{
		// Token: 0x060000B9 RID: 185
		internal abstract void Interp(ParameterOverride from, ParameterOverride to, float t);

		// Token: 0x060000BA RID: 186
		public abstract int GetHash();

		// Token: 0x060000BB RID: 187 RVA: 0x00009307 File Offset: 0x00007507
		public T GetValue<T>()
		{
			return ((ParameterOverride<T>)this).value;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00002430 File Offset: 0x00000630
		protected internal virtual void OnEnable()
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00002430 File Offset: 0x00000630
		protected internal virtual void OnDisable()
		{
		}

		// Token: 0x060000BE RID: 190
		internal abstract void SetValue(ParameterOverride parameter);

		// Token: 0x040000F8 RID: 248
		public bool overrideState;
	}
}
