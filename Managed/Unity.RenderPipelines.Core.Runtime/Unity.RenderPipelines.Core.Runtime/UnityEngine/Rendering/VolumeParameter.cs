using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000069 RID: 105
	public abstract class VolumeParameter
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000CF98 File Offset: 0x0000B198
		// (set) Token: 0x06000309 RID: 777 RVA: 0x0000CFA0 File Offset: 0x0000B1A0
		public virtual bool overrideState
		{
			get
			{
				return this.m_OverrideState;
			}
			set
			{
				this.m_OverrideState = value;
			}
		}

		// Token: 0x0600030A RID: 778
		internal abstract void Interp(VolumeParameter from, VolumeParameter to, float t);

		// Token: 0x0600030B RID: 779 RVA: 0x0000CFA9 File Offset: 0x0000B1A9
		public T GetValue<T>()
		{
			return ((VolumeParameter<T>)this).value;
		}

		// Token: 0x0600030C RID: 780
		public abstract void SetValue(VolumeParameter parameter);

		// Token: 0x0600030D RID: 781 RVA: 0x00002788 File Offset: 0x00000988
		protected internal virtual void OnEnable()
		{
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00002788 File Offset: 0x00000988
		protected internal virtual void OnDisable()
		{
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000CFB6 File Offset: 0x0000B1B6
		public static bool IsObjectParameter(Type type)
		{
			return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ObjectParameter<>)) || (type.BaseType != null && VolumeParameter.IsObjectParameter(type.BaseType));
		}

		// Token: 0x040001AC RID: 428
		public const string k_DebuggerDisplay = "{m_Value} ({m_OverrideState})";

		// Token: 0x040001AD RID: 429
		[SerializeField]
		protected bool m_OverrideState;
	}
}
