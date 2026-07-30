using System;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x02000023 RID: 35
	[ExecuteInEditMode]
	[RequireComponent(typeof(VFXPropertyBinder))]
	public abstract class VFXBinderBase : MonoBehaviour
	{
		// Token: 0x060000E1 RID: 225
		public abstract bool IsValid(VisualEffect component);

		// Token: 0x060000E2 RID: 226 RVA: 0x00002091 File Offset: 0x00000291
		public virtual void Reset()
		{
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004812 File Offset: 0x00002A12
		protected virtual void Awake()
		{
			this.binder = base.GetComponent<VFXPropertyBinder>();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004820 File Offset: 0x00002A20
		protected virtual void OnEnable()
		{
			if (!this.binder.m_Bindings.Contains(this))
			{
				this.binder.m_Bindings.Add(this);
			}
			base.hideFlags = HideFlags.HideInInspector;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000484D File Offset: 0x00002A4D
		protected virtual void OnDisable()
		{
			if (this.binder.m_Bindings.Contains(this))
			{
				this.binder.m_Bindings.Remove(this);
			}
		}

		// Token: 0x060000E6 RID: 230
		public abstract void UpdateBinding(VisualEffect component);

		// Token: 0x060000E7 RID: 231 RVA: 0x00004874 File Offset: 0x00002A74
		public override string ToString()
		{
			return base.GetType().ToString();
		}

		// Token: 0x0400008F RID: 143
		protected VFXPropertyBinder binder;
	}
}
