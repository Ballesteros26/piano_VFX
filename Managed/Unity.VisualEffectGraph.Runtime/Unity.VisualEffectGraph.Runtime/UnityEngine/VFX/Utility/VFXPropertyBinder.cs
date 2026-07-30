using System;
using System.Collections.Generic;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x02000024 RID: 36
	[RequireComponent(typeof(VisualEffect))]
	[DefaultExecutionOrder(1)]
	[ExecuteInEditMode]
	public class VFXPropertyBinder : MonoBehaviour
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00004889 File Offset: 0x00002A89
		private void OnEnable()
		{
			this.m_VisualEffect = base.GetComponent<VisualEffect>();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004898 File Offset: 0x00002A98
		private void Update()
		{
			if (!this.m_ExecuteInEditor && Application.isEditor && !Application.isPlaying)
			{
				return;
			}
			for (int i = 0; i < this.m_Bindings.Count; i++)
			{
				VFXBinderBase vfxbinderBase = this.m_Bindings[i];
				if (vfxbinderBase == null)
				{
					Debug.LogWarning(string.Format("Parameter binder at index {0} of GameObject {1} is null or missing", i, base.gameObject.name));
				}
				else if (vfxbinderBase.IsValid(this.m_VisualEffect))
				{
					vfxbinderBase.UpdateBinding(this.m_VisualEffect);
				}
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004924 File Offset: 0x00002B24
		public T AddPropertyBinder<T>() where T : VFXBinderBase
		{
			return base.gameObject.AddComponent<T>();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004931 File Offset: 0x00002B31
		[Obsolete("Use AddPropertyBinder<T>() instead")]
		public T AddParameterBinder<T>() where T : VFXBinderBase
		{
			return this.AddPropertyBinder<T>();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000493C File Offset: 0x00002B3C
		public void ClearPropertyBinders()
		{
			VFXBinderBase[] components = base.GetComponents<VFXBinderBase>();
			for (int i = 0; i < components.Length; i++)
			{
				Object.Destroy(components[i]);
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004966 File Offset: 0x00002B66
		[Obsolete("Please use ClearPropertyBinders() instead")]
		public void ClearParameterBinders()
		{
			this.ClearPropertyBinders();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000496E File Offset: 0x00002B6E
		public void RemovePropertyBinder(VFXBinderBase binder)
		{
			if (binder.gameObject == base.gameObject)
			{
				Object.Destroy(binder);
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004989 File Offset: 0x00002B89
		[Obsolete("Please use RemovePropertyBinder() instead")]
		public void RemoveParameterBinder(VFXBinderBase binder)
		{
			this.RemovePropertyBinder(binder);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004994 File Offset: 0x00002B94
		public void RemovePropertyBinders<T>() where T : VFXBinderBase
		{
			foreach (VFXBinderBase vfxbinderBase in base.GetComponents<VFXBinderBase>())
			{
				if (vfxbinderBase is T)
				{
					Object.Destroy(vfxbinderBase);
				}
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000049C8 File Offset: 0x00002BC8
		[Obsolete("Please use RemovePropertyBinders<T>() instead")]
		public void RemoveParameterBinders<T>() where T : VFXBinderBase
		{
			this.RemovePropertyBinders<T>();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000049D0 File Offset: 0x00002BD0
		public IEnumerable<T> GetPropertyBinders<T>() where T : VFXBinderBase
		{
			foreach (VFXBinderBase vfxbinderBase in this.m_Bindings)
			{
				if (vfxbinderBase is T)
				{
					yield return vfxbinderBase as T;
				}
			}
			List<VFXBinderBase>.Enumerator enumerator = default(List<VFXBinderBase>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000049E0 File Offset: 0x00002BE0
		[Obsolete("Please use GetPropertyBinders<T>() instead")]
		public IEnumerable<T> GetParameterBinders<T>() where T : VFXBinderBase
		{
			return this.GetPropertyBinders<T>();
		}

		// Token: 0x04000090 RID: 144
		[SerializeField]
		protected bool m_ExecuteInEditor = true;

		// Token: 0x04000091 RID: 145
		public List<VFXBinderBase> m_Bindings = new List<VFXBinderBase>();

		// Token: 0x04000092 RID: 146
		[SerializeField]
		protected VisualEffect m_VisualEffect;
	}
}
