using System;
using UnityEngine.Serialization;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x02000013 RID: 19
	[AddComponentMenu("VFX/Property Binders/Input Mouse Binder")]
	[VFXBinder("Input/Mouse")]
	internal class VFXInputMouseBinder : VFXBinderBase
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003289 File Offset: 0x00001489
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00003296 File Offset: 0x00001496
		public string MouseLeftClickProperty
		{
			get
			{
				return (string)this.m_MouseLeftClickProperty;
			}
			set
			{
				this.m_MouseLeftClickProperty = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000032A4 File Offset: 0x000014A4
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000032B1 File Offset: 0x000014B1
		public string MouseRightClickProperty
		{
			get
			{
				return (string)this.m_MouseRightClickProperty;
			}
			set
			{
				this.m_MouseRightClickProperty = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000032BF File Offset: 0x000014BF
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000032CC File Offset: 0x000014CC
		public string PositionProperty
		{
			get
			{
				return (string)this.m_PositionProperty;
			}
			set
			{
				this.m_PositionProperty = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000032DA File Offset: 0x000014DA
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000032E7 File Offset: 0x000014E7
		public string VelocityProperty
		{
			get
			{
				return (string)this.m_VelocityProperty;
			}
			set
			{
				this.m_VelocityProperty = value;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000032F8 File Offset: 0x000014F8
		public override bool IsValid(VisualEffect component)
		{
			return component.HasVector3(this.m_PositionProperty) && (!this.CheckLeftClick || component.HasBool(this.m_MouseLeftClickProperty)) && (!this.CheckRightClick || component.HasBool(this.m_MouseRightClickProperty)) && (!this.SetVelocity || component.HasVector3(this.m_VelocityProperty));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003374 File Offset: 0x00001574
		public override void UpdateBinding(VisualEffect component)
		{
			Vector3 vector = Vector3.zero;
			if (this.CheckLeftClick)
			{
				component.SetBool(this.MouseLeftClickProperty, Input.GetMouseButton(0));
			}
			if (this.CheckRightClick)
			{
				component.SetBool(this.MouseRightClickProperty, Input.GetMouseButton(1));
			}
			if (this.Target != null)
			{
				Vector3 mousePosition = Input.mousePosition;
				mousePosition.z = this.Distance;
				vector = this.Target.ScreenToWorldPoint(mousePosition);
			}
			else
			{
				vector = Input.mousePosition;
			}
			component.SetVector3(this.m_PositionProperty, vector);
			if (this.SetVelocity)
			{
				component.SetVector3(this.m_VelocityProperty, (vector - this.m_PreviousPosition) / Time.deltaTime);
			}
			this.m_PreviousPosition = vector;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003439 File Offset: 0x00001639
		public override string ToString()
		{
			return string.Format("Mouse: '{0}' -> {1}", this.m_PositionProperty, (this.Target == null) ? "(null)" : this.Target.name);
		}

		// Token: 0x04000040 RID: 64
		[VFXPropertyBinding(new string[] { "System.Boolean" })]
		[SerializeField]
		[FormerlySerializedAs("m_MouseLeftClickParameter")]
		protected ExposedProperty m_MouseLeftClickProperty = "LeftClick";

		// Token: 0x04000041 RID: 65
		[VFXPropertyBinding(new string[] { "System.Boolean" })]
		[SerializeField]
		[FormerlySerializedAs("m_MouseRightClickParameter")]
		protected ExposedProperty m_MouseRightClickProperty = "RightClick";

		// Token: 0x04000042 RID: 66
		[VFXPropertyBinding(new string[] { "UnityEditor.VFX.Position", "UnityEngine.Vector3" })]
		[SerializeField]
		[FormerlySerializedAs("m_PositionParameter")]
		protected ExposedProperty m_PositionProperty = "Position";

		// Token: 0x04000043 RID: 67
		[VFXPropertyBinding(new string[] { "UnityEngine.Vector3" })]
		[SerializeField]
		[FormerlySerializedAs("m_VelocityParameter")]
		protected ExposedProperty m_VelocityProperty = "Velocity";

		// Token: 0x04000044 RID: 68
		public Camera Target;

		// Token: 0x04000045 RID: 69
		public float Distance = 10f;

		// Token: 0x04000046 RID: 70
		public bool SetVelocity;

		// Token: 0x04000047 RID: 71
		public bool CheckLeftClick = true;

		// Token: 0x04000048 RID: 72
		public bool CheckRightClick;

		// Token: 0x04000049 RID: 73
		private Vector3 m_PreviousPosition;
	}
}
