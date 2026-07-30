using System;
using UnityEngine.Serialization;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x02000010 RID: 16
	[AddComponentMenu("VFX/Property Binders/Input Axis Binder")]
	[VFXBinder("Input/Axis")]
	internal class VFXInputAxisBinder : VFXBinderBase
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002E91 File Offset: 0x00001091
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002E9E File Offset: 0x0000109E
		public string AxisProperty
		{
			get
			{
				return (string)this.m_AxisProperty;
			}
			set
			{
				this.m_AxisProperty = value;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002EAC File Offset: 0x000010AC
		public override bool IsValid(VisualEffect component)
		{
			return component.HasFloat(this.m_AxisProperty);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002EC0 File Offset: 0x000010C0
		public override void UpdateBinding(VisualEffect component)
		{
			float axisRaw = Input.GetAxisRaw(this.AxisName);
			if (this.Accumulate)
			{
				float @float = component.GetFloat(this.m_AxisProperty);
				component.SetFloat(this.m_AxisProperty, @float + this.AccumulateSpeed * axisRaw * Time.deltaTime);
				return;
			}
			component.SetFloat(this.m_AxisProperty, axisRaw);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002F27 File Offset: 0x00001127
		public override string ToString()
		{
			return string.Format("Input Axis: '{0}' -> {1}", this.m_AxisProperty, this.AxisName.ToString());
		}

		// Token: 0x04000030 RID: 48
		[VFXPropertyBinding(new string[] { "System.Single" })]
		[SerializeField]
		[FormerlySerializedAs("m_AxisParameter")]
		protected ExposedProperty m_AxisProperty = "Axis";

		// Token: 0x04000031 RID: 49
		public string AxisName = "Horizontal";

		// Token: 0x04000032 RID: 50
		public float AccumulateSpeed = 1f;

		// Token: 0x04000033 RID: 51
		public bool Accumulate = true;
	}
}
