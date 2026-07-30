using System;
using UnityEngine.Serialization;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x02000014 RID: 20
	[AddComponentMenu("VFX/Property Binders/Input Touch Binder")]
	[VFXBinder("Input/Touch")]
	internal class VFXInputTouchBinder : VFXBinderBase
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000034D1 File Offset: 0x000016D1
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000034DE File Offset: 0x000016DE
		public string TouchEnabledProperty
		{
			get
			{
				return (string)this.m_TouchEnabledProperty;
			}
			set
			{
				this.m_TouchEnabledProperty = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000034EC File Offset: 0x000016EC
		// (set) Token: 0x06000072 RID: 114 RVA: 0x000034F9 File Offset: 0x000016F9
		public string Parameter
		{
			get
			{
				return (string)this.m_Parameter;
			}
			set
			{
				this.m_Parameter = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003507 File Offset: 0x00001707
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00003514 File Offset: 0x00001714
		public string VelocityParameter
		{
			get
			{
				return (string)this.m_VelocityParameter;
			}
			set
			{
				this.m_VelocityParameter = value;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003524 File Offset: 0x00001724
		public override bool IsValid(VisualEffect component)
		{
			return this.Target != null && component.HasVector3(this.m_Parameter) && component.HasBool(this.m_TouchEnabledProperty) && (!this.SetVelocity || component.HasVector3(this.m_VelocityParameter));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003584 File Offset: 0x00001784
		public override void UpdateBinding(VisualEffect component)
		{
			Vector3 vector = Vector3.zero;
			bool flag;
			if (Input.touchCount > this.TouchIndex)
			{
				Touch touch = Input.GetTouch(this.TouchIndex);
				flag = true;
				Vector3 vector2 = touch.position;
				vector2.z = this.Distance;
				vector = this.Target.ScreenToWorldPoint(vector2);
				component.SetBool(this.m_TouchEnabledProperty, true);
				component.SetVector3(this.m_Parameter, vector);
			}
			else
			{
				flag = false;
				component.SetBool(this.m_TouchEnabledProperty, false);
				component.SetVector3(this.m_Parameter, Vector3.zero);
			}
			if (this.SetVelocity)
			{
				if (this.m_PreviousTouch)
				{
					component.SetVector3(this.m_VelocityParameter, (vector - this.m_PreviousPosition) / Time.deltaTime);
				}
				else
				{
					component.SetVector3(this.m_VelocityParameter, Vector3.zero);
				}
			}
			this.m_PreviousTouch = flag;
			this.m_PreviousPosition = vector;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003688 File Offset: 0x00001888
		public override string ToString()
		{
			return string.Format("Touch #{2} : '{0}' -> {1}", this.m_Parameter, (this.Target == null) ? "(null)" : this.Target.name, this.TouchIndex);
		}

		// Token: 0x0400004A RID: 74
		[VFXPropertyBinding(new string[] { "System.Boolean" })]
		[SerializeField]
		[FormerlySerializedAs("m_TouchEnabledParameter")]
		protected ExposedProperty m_TouchEnabledProperty = "TouchEnabled";

		// Token: 0x0400004B RID: 75
		[VFXPropertyBinding(new string[] { "UnityEditor.VFX.Position", "UnityEngine.Vector3" })]
		[SerializeField]
		protected ExposedProperty m_Parameter = "Position";

		// Token: 0x0400004C RID: 76
		[VFXPropertyBinding(new string[] { "UnityEngine.Vector3" })]
		[SerializeField]
		protected ExposedProperty m_VelocityParameter = "Velocity";

		// Token: 0x0400004D RID: 77
		public int TouchIndex;

		// Token: 0x0400004E RID: 78
		public Camera Target;

		// Token: 0x0400004F RID: 79
		public float Distance = 10f;

		// Token: 0x04000050 RID: 80
		public bool SetVelocity;

		// Token: 0x04000051 RID: 81
		private Vector3 m_PreviousPosition;

		// Token: 0x04000052 RID: 82
		private bool m_PreviousTouch;
	}
}
