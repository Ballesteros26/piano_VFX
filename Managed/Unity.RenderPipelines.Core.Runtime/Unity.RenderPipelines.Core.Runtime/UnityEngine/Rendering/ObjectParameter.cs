using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace UnityEngine.Rendering
{
	// Token: 0x0200008D RID: 141
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class ObjectParameter<T> : VolumeParameter<T>
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000D79B File Offset: 0x0000B99B
		// (set) Token: 0x06000368 RID: 872 RVA: 0x0000D7A3 File Offset: 0x0000B9A3
		internal ReadOnlyCollection<VolumeParameter> parameters { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000B492 File Offset: 0x00009692
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0000D7AC File Offset: 0x0000B9AC
		public sealed override bool overrideState
		{
			get
			{
				return true;
			}
			set
			{
				this.m_OverrideState = true;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000D7B5 File Offset: 0x0000B9B5
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0000D7C0 File Offset: 0x0000B9C0
		public sealed override T value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
				if (this.m_Value == null)
				{
					this.parameters = null;
					return;
				}
				this.parameters = (from t in this.m_Value.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
					where t.FieldType.IsSubclassOf(typeof(VolumeParameter))
					orderby t.MetadataToken
					select (VolumeParameter)t.GetValue(this.m_Value)).ToList<VolumeParameter>().AsReadOnly();
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000D86A File Offset: 0x0000BA6A
		public ObjectParameter(T value)
		{
			this.m_OverrideState = true;
			this.value = value;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000D880 File Offset: 0x0000BA80
		internal override void Interp(VolumeParameter from, VolumeParameter to, float t)
		{
			if (this.m_Value == null)
			{
				return;
			}
			ReadOnlyCollection<VolumeParameter> parameters = this.parameters;
			ReadOnlyCollection<VolumeParameter> parameters2 = ((ObjectParameter<T>)from).parameters;
			ReadOnlyCollection<VolumeParameter> parameters3 = ((ObjectParameter<T>)to).parameters;
			for (int i = 0; i < parameters2.Count; i++)
			{
				parameters[i].overrideState = parameters3[i].overrideState;
				if (parameters3[i].overrideState)
				{
					parameters[i].Interp(parameters2[i], parameters3[i], t);
				}
			}
		}
	}
}
