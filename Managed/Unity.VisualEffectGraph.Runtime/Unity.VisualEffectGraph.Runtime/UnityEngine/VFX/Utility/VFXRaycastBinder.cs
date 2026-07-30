using System;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x0200001A RID: 26
	[AddComponentMenu("VFX/Property Binders/Raycast Binder")]
	[VFXBinder("Physics/Raycast")]
	internal class VFXRaycastBinder : VFXBinderBase
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003D48 File Offset: 0x00001F48
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00003D55 File Offset: 0x00001F55
		public string TargetPosition
		{
			get
			{
				return (string)this.m_TargetPosition;
			}
			set
			{
				this.m_TargetPosition = value;
				this.UpdateSubProperties();
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003D69 File Offset: 0x00001F69
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00003D76 File Offset: 0x00001F76
		public string TargetNormal
		{
			get
			{
				return (string)this.m_TargetNormal;
			}
			set
			{
				this.m_TargetNormal = value;
				this.UpdateSubProperties();
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003D8A File Offset: 0x00001F8A
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003D97 File Offset: 0x00001F97
		public string TargetHit
		{
			get
			{
				return (string)this.m_TargetHit;
			}
			set
			{
				this.m_TargetHit = value;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003DA5 File Offset: 0x00001FA5
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateSubProperties();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003DB3 File Offset: 0x00001FB3
		private void OnValidate()
		{
			this.UpdateSubProperties();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003DBB File Offset: 0x00001FBB
		private void UpdateSubProperties()
		{
			this.m_TargetPosition_position = this.m_TargetPosition + "_position";
			this.m_TargetNormal_direction = this.m_TargetNormal + "_direction";
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003DF4 File Offset: 0x00001FF4
		public override bool IsValid(VisualEffect component)
		{
			return component.HasVector3(this.m_TargetPosition_position) && component.HasVector3(this.m_TargetNormal_direction) && component.HasBool(this.m_TargetHit) && this.RaycastSource != null;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003E48 File Offset: 0x00002048
		public override void UpdateBinding(VisualEffect component)
		{
			Vector3 vector = ((this.RaycastDirectionSpace == VFXRaycastBinder.Space.Local) ? this.RaycastSource.transform.TransformDirection(this.RaycastDirection) : this.RaycastDirection);
			bool flag = Physics.Raycast(new Ray(this.RaycastSource.transform.position, vector), out this.m_HitInfo, this.MaxDistance, this.Layers);
			component.SetVector3(this.m_TargetPosition_position, this.m_HitInfo.point);
			component.SetVector3(this.m_TargetNormal_direction, this.m_HitInfo.normal);
			component.SetBool(this.TargetHit, flag);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003EF4 File Offset: 0x000020F4
		public override string ToString()
		{
			return string.Format(string.Format("Raycast : {0} -> {1} ({2})", (this.RaycastSource == null) ? "null" : this.RaycastSource.name, this.RaycastDirection, this.RaycastDirectionSpace), Array.Empty<object>());
		}

		// Token: 0x04000069 RID: 105
		[VFXPropertyBinding(new string[] { "UnityEditor.VFX.Position" })]
		[SerializeField]
		protected ExposedProperty m_TargetPosition = "TargetPosition";

		// Token: 0x0400006A RID: 106
		[VFXPropertyBinding(new string[] { "UnityEditor.VFX.DirectionType" })]
		[SerializeField]
		protected ExposedProperty m_TargetNormal = "TargetNormal";

		// Token: 0x0400006B RID: 107
		[VFXPropertyBinding(new string[] { "System.Boolean" })]
		[SerializeField]
		protected ExposedProperty m_TargetHit = "TargetHit";

		// Token: 0x0400006C RID: 108
		protected ExposedProperty m_TargetPosition_position;

		// Token: 0x0400006D RID: 109
		protected ExposedProperty m_TargetNormal_direction;

		// Token: 0x0400006E RID: 110
		public GameObject RaycastSource;

		// Token: 0x0400006F RID: 111
		public Vector3 RaycastDirection = Vector3.forward;

		// Token: 0x04000070 RID: 112
		public VFXRaycastBinder.Space RaycastDirectionSpace;

		// Token: 0x04000071 RID: 113
		public LayerMask Layers = -1;

		// Token: 0x04000072 RID: 114
		public float MaxDistance = 100f;

		// Token: 0x04000073 RID: 115
		private RaycastHit m_HitInfo;

		// Token: 0x02000033 RID: 51
		public enum Space
		{
			// Token: 0x040000CB RID: 203
			Local,
			// Token: 0x040000CC RID: 204
			World
		}
	}
}
