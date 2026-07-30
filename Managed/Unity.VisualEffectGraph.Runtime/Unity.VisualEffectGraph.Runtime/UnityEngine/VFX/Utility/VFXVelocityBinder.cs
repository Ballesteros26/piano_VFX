using System;
using UnityEngine.Serialization;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x02000021 RID: 33
	[AddComponentMenu("VFX/Property Binders/Velocity Binder")]
	[VFXBinder("Transform/Velocity")]
	internal class VFXVelocityBinder : VFXBinderBase
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000046B4 File Offset: 0x000028B4
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x000046C1 File Offset: 0x000028C1
		public string Property
		{
			get
			{
				return (string)this.m_Property;
			}
			set
			{
				this.m_Property = value;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000046CF File Offset: 0x000028CF
		public override bool IsValid(VisualEffect component)
		{
			return this.Target != null && component.HasVector3(this.m_Property);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000046F2 File Offset: 0x000028F2
		public override void Reset()
		{
			this.m_PreviousTime = VFXVelocityBinder.invalidPreviousTime;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004700 File Offset: 0x00002900
		public override void UpdateBinding(VisualEffect component)
		{
			Vector3 vector = Vector3.zero;
			float time = Time.time;
			if (this.m_PreviousTime != VFXVelocityBinder.invalidPreviousTime)
			{
				Vector3 vector2 = this.Target.transform.position - this.m_PreviousPosition;
				float num = time - this.m_PreviousTime;
				if (Vector3.SqrMagnitude(vector2) > Mathf.Epsilon && num > Mathf.Epsilon)
				{
					vector = vector2 / num;
				}
			}
			component.SetVector3(this.m_Property, vector);
			this.m_PreviousPosition = this.Target.transform.position;
			this.m_PreviousTime = time;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004797 File Offset: 0x00002997
		public override string ToString()
		{
			return string.Format("Velocity : '{0}' -> {1}", this.m_Property, (this.Target == null) ? "(null)" : this.Target.name);
		}

		// Token: 0x04000089 RID: 137
		[VFXPropertyBinding(new string[] { "UnityEngine.Vector3" })]
		[SerializeField]
		[FormerlySerializedAs("m_Parameter")]
		public ExposedProperty m_Property = "Velocity";

		// Token: 0x0400008A RID: 138
		public Transform Target;

		// Token: 0x0400008B RID: 139
		private static readonly float invalidPreviousTime = -1f;

		// Token: 0x0400008C RID: 140
		private float m_PreviousTime = VFXVelocityBinder.invalidPreviousTime;

		// Token: 0x0400008D RID: 141
		private Vector3 m_PreviousPosition = Vector3.zero;
	}
}
