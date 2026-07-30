using System;
using System.Collections.Generic;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x0200000F RID: 15
	[AddComponentMenu("VFX/Property Binders/Hierarchy to Attribute Map Binder")]
	[VFXBinder("Point Cache/Hierarchy to Attribute Map")]
	internal class VFXHierarchyAttributeMapBinder : VFXBinderBase
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002A7A File Offset: 0x00000C7A
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateHierarchy();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A88 File Offset: 0x00000C88
		private void OnValidate()
		{
			this.UpdateHierarchy();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A90 File Offset: 0x00000C90
		private void UpdateHierarchy()
		{
			this.bones = this.ChildrenOf(this.HierarchyRoot, this.MaximumDepth);
			int count = this.bones.Count;
			Debug.Log("Found Bone Count: " + count);
			this.position = new Texture2D(count, 1, TextureFormat.RGBAHalf, false, true);
			this.targetPosition = new Texture2D(count, 1, TextureFormat.RGBAHalf, false, true);
			this.radius = new Texture2D(count, 1, TextureFormat.RHalf, false, true);
			this.UpdateData();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B10 File Offset: 0x00000D10
		private List<VFXHierarchyAttributeMapBinder.Bone> ChildrenOf(Transform source, uint depth)
		{
			List<VFXHierarchyAttributeMapBinder.Bone> list = new List<VFXHierarchyAttributeMapBinder.Bone>();
			foreach (object obj in source)
			{
				Transform transform = (Transform)obj;
				list.Add(new VFXHierarchyAttributeMapBinder.Bone
				{
					source = source.transform,
					target = transform.transform,
					sourceRadius = this.DefaultRadius,
					targetRadius = this.DefaultRadius
				});
				if (depth > 0U)
				{
					list.AddRange(this.ChildrenOf(transform, depth - 1U));
				}
			}
			return list;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002BC0 File Offset: 0x00000DC0
		private void UpdateData()
		{
			int count = this.bones.Count;
			if (this.position.width != count)
			{
				return;
			}
			List<Color> list = new List<Color>();
			List<Color> list2 = new List<Color>();
			List<Color> list3 = new List<Color>();
			for (int i = 0; i < count; i++)
			{
				VFXHierarchyAttributeMapBinder.Bone bone = this.bones[i];
				list.Add(new Color(bone.source.position.x, bone.source.position.y, bone.source.position.z, 1f));
				list2.Add(new Color(bone.target.position.x, bone.target.position.y, bone.target.position.z, 1f));
				list3.Add(new Color(bone.sourceRadius, 0f, 0f, 1f));
			}
			this.position.SetPixels(list.ToArray());
			this.targetPosition.SetPixels(list2.ToArray());
			this.radius.SetPixels(list3.ToArray());
			this.position.Apply();
			this.targetPosition.Apply();
			this.radius.Apply();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002D1C File Offset: 0x00000F1C
		public override bool IsValid(VisualEffect component)
		{
			return this.HierarchyRoot != null && component.HasTexture(this.m_PositionMap) && component.HasTexture(this.m_TargetPositionMap) && component.HasTexture(this.m_RadiusPositionMap) && component.HasUInt(this.m_BoneCount);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002D84 File Offset: 0x00000F84
		public override void UpdateBinding(VisualEffect component)
		{
			this.UpdateData();
			component.SetTexture(this.m_PositionMap, this.position);
			component.SetTexture(this.m_TargetPositionMap, this.targetPosition);
			component.SetTexture(this.m_RadiusPositionMap, this.radius);
			component.SetUInt(this.m_BoneCount, (uint)this.bones.Count);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public override string ToString()
		{
			return string.Format("Hierarchy: {0} -> {1}", (this.HierarchyRoot == null) ? "(null)" : this.HierarchyRoot.name, this.m_PositionMap);
		}

		// Token: 0x04000024 RID: 36
		[VFXPropertyBinding(new string[] { "System.UInt32" })]
		[SerializeField]
		protected ExposedProperty m_BoneCount = "BoneCount";

		// Token: 0x04000025 RID: 37
		[VFXPropertyBinding(new string[] { "UnityEngine.Texture2D" })]
		[SerializeField]
		protected ExposedProperty m_PositionMap = "PositionMap";

		// Token: 0x04000026 RID: 38
		[VFXPropertyBinding(new string[] { "UnityEngine.Texture2D" })]
		[SerializeField]
		protected ExposedProperty m_TargetPositionMap = "TargetPositionMap";

		// Token: 0x04000027 RID: 39
		[VFXPropertyBinding(new string[] { "UnityEngine.Texture2D" })]
		[SerializeField]
		protected ExposedProperty m_RadiusPositionMap = "RadiusPositionMap";

		// Token: 0x04000028 RID: 40
		public Transform HierarchyRoot;

		// Token: 0x04000029 RID: 41
		public float DefaultRadius = 0.1f;

		// Token: 0x0400002A RID: 42
		public uint MaximumDepth = 3U;

		// Token: 0x0400002B RID: 43
		public VFXHierarchyAttributeMapBinder.RadiusMode Radius;

		// Token: 0x0400002C RID: 44
		private Texture2D position;

		// Token: 0x0400002D RID: 45
		private Texture2D targetPosition;

		// Token: 0x0400002E RID: 46
		private Texture2D radius;

		// Token: 0x0400002F RID: 47
		private List<VFXHierarchyAttributeMapBinder.Bone> bones;

		// Token: 0x02000031 RID: 49
		public enum RadiusMode
		{
			// Token: 0x040000C4 RID: 196
			Fixed,
			// Token: 0x040000C5 RID: 197
			Interpolate
		}

		// Token: 0x02000032 RID: 50
		private struct Bone
		{
			// Token: 0x040000C6 RID: 198
			public Transform source;

			// Token: 0x040000C7 RID: 199
			public float sourceRadius;

			// Token: 0x040000C8 RID: 200
			public Transform target;

			// Token: 0x040000C9 RID: 201
			public float targetRadius;
		}
	}
}
