using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000056 RID: 86
	public sealed class PostProcessProfile : ScriptableObject
	{
		// Token: 0x0600015A RID: 346 RVA: 0x0000CB9E File Offset: 0x0000AD9E
		private void OnEnable()
		{
			this.settings.RemoveAll((PostProcessEffectSettings x) => x == null);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000CBCB File Offset: 0x0000ADCB
		public T AddSettings<T>() where T : PostProcessEffectSettings
		{
			return (T)((object)this.AddSettings(typeof(T)));
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000CBE4 File Offset: 0x0000ADE4
		public PostProcessEffectSettings AddSettings(Type type)
		{
			if (this.HasSettings(type))
			{
				throw new InvalidOperationException("Effect already exists in the stack");
			}
			PostProcessEffectSettings postProcessEffectSettings = (PostProcessEffectSettings)ScriptableObject.CreateInstance(type);
			postProcessEffectSettings.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
			postProcessEffectSettings.name = type.Name;
			postProcessEffectSettings.enabled.value = true;
			this.settings.Add(postProcessEffectSettings);
			this.isDirty = true;
			return postProcessEffectSettings;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000CC44 File Offset: 0x0000AE44
		public PostProcessEffectSettings AddSettings(PostProcessEffectSettings effect)
		{
			if (this.HasSettings(this.settings.GetType()))
			{
				throw new InvalidOperationException("Effect already exists in the stack");
			}
			this.settings.Add(effect);
			this.isDirty = true;
			return effect;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000CC78 File Offset: 0x0000AE78
		public void RemoveSettings<T>() where T : PostProcessEffectSettings
		{
			this.RemoveSettings(typeof(T));
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000CC8C File Offset: 0x0000AE8C
		public void RemoveSettings(Type type)
		{
			int num = -1;
			for (int i = 0; i < this.settings.Count; i++)
			{
				if (this.settings[i].GetType() == type)
				{
					num = i;
					break;
				}
			}
			if (num < 0)
			{
				throw new InvalidOperationException("Effect doesn't exist in the profile");
			}
			this.settings.RemoveAt(num);
			this.isDirty = true;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000CCF0 File Offset: 0x0000AEF0
		public bool HasSettings<T>() where T : PostProcessEffectSettings
		{
			return this.HasSettings(typeof(T));
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000CD04 File Offset: 0x0000AF04
		public bool HasSettings(Type type)
		{
			using (List<PostProcessEffectSettings>.Enumerator enumerator = this.settings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetType() == type)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000CD64 File Offset: 0x0000AF64
		public T GetSetting<T>() where T : PostProcessEffectSettings
		{
			foreach (PostProcessEffectSettings postProcessEffectSettings in this.settings)
			{
				if (postProcessEffectSettings is T)
				{
					return postProcessEffectSettings as T;
				}
			}
			return default(T);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000CDD4 File Offset: 0x0000AFD4
		public bool TryGetSettings<T>(out T outSetting) where T : PostProcessEffectSettings
		{
			Type typeFromHandle = typeof(T);
			outSetting = default(T);
			foreach (PostProcessEffectSettings postProcessEffectSettings in this.settings)
			{
				if (postProcessEffectSettings.GetType() == typeFromHandle)
				{
					outSetting = (T)((object)postProcessEffectSettings);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400015E RID: 350
		[Tooltip("A list of all settings currently stored in this profile.")]
		public List<PostProcessEffectSettings> settings = new List<PostProcessEffectSettings>();

		// Token: 0x0400015F RID: 351
		[NonSerialized]
		public bool isDirty = true;
	}
}
