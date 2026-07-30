using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000051 RID: 81
	[Serializable]
	public class PostProcessEffectSettings : ScriptableObject
	{
		// Token: 0x06000113 RID: 275 RVA: 0x0000A318 File Offset: 0x00008518
		private void OnEnable()
		{
			this.parameters = (from t in base.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
				where t.FieldType.IsSubclassOf(typeof(ParameterOverride))
				orderby t.MetadataToken
				select (ParameterOverride)t.GetValue(this)).ToList<ParameterOverride>().AsReadOnly();
			foreach (ParameterOverride parameterOverride in this.parameters)
			{
				parameterOverride.OnEnable();
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000A3D8 File Offset: 0x000085D8
		private void OnDisable()
		{
			if (this.parameters == null)
			{
				return;
			}
			foreach (ParameterOverride parameterOverride in this.parameters)
			{
				parameterOverride.OnDisable();
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000A42C File Offset: 0x0000862C
		public void SetAllOverridesTo(bool state, bool excludeEnabled = true)
		{
			foreach (ParameterOverride parameterOverride in this.parameters)
			{
				if (!excludeEnabled || parameterOverride != this.enabled)
				{
					parameterOverride.overrideState = state;
				}
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000A488 File Offset: 0x00008688
		public virtual bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000A498 File Offset: 0x00008698
		public int GetHash()
		{
			int num = 17;
			foreach (ParameterOverride parameterOverride in this.parameters)
			{
				num = num * 23 + parameterOverride.GetHash();
			}
			return num;
		}

		// Token: 0x0400012A RID: 298
		public bool active = true;

		// Token: 0x0400012B RID: 299
		public BoolParameter enabled = new BoolParameter
		{
			overrideState = true,
			value = false
		};

		// Token: 0x0400012C RID: 300
		internal ReadOnlyCollection<ParameterOverride> parameters;
	}
}
