using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x02000016 RID: 22
	[AddComponentMenu("VFX/Property Binders/Multiple Position Binder")]
	[VFXBinder("Point Cache/Multiple Position Binder")]
	internal class VFXMultiplePositionBinder : VFXBinderBase
	{
		// Token: 0x06000083 RID: 131 RVA: 0x0000389C File Offset: 0x00001A9C
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateTexture();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000038AA File Offset: 0x00001AAA
		public override bool IsValid(VisualEffect component)
		{
			return this.Targets != null && component.HasTexture(this.PositionMapProperty) && component.HasInt(this.PositionCountProperty);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000038DC File Offset: 0x00001ADC
		public override void UpdateBinding(VisualEffect component)
		{
			if (this.EveryFrame || Application.isEditor)
			{
				this.UpdateTexture();
			}
			component.SetTexture(this.PositionMapProperty, this.positionMap);
			component.SetInt(this.PositionCountProperty, this.count);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000392C File Offset: 0x00001B2C
		private void UpdateTexture()
		{
			if (this.Targets == null || this.Targets.Length == 0)
			{
				return;
			}
			List<Vector3> list = new List<Vector3>();
			foreach (GameObject gameObject in this.Targets)
			{
				if (gameObject != null)
				{
					list.Add(gameObject.transform.position);
				}
			}
			this.count = list.Count;
			if (this.positionMap == null || this.positionMap.width != this.count)
			{
				this.positionMap = new Texture2D(this.count, 1, TextureFormat.RGBAFloat, false);
			}
			List<Color> list2 = new List<Color>();
			foreach (Vector3 vector in list)
			{
				list2.Add(new Color(vector.x, vector.y, vector.z));
			}
			this.positionMap.name = base.gameObject.name + "_PositionMap";
			this.positionMap.filterMode = FilterMode.Point;
			this.positionMap.wrapMode = TextureWrapMode.Repeat;
			this.positionMap.SetPixels(list2.ToArray(), 0);
			this.positionMap.Apply();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003A80 File Offset: 0x00001C80
		public override string ToString()
		{
			return string.Format("Multiple Position Binder ({0} positions)", this.count);
		}

		// Token: 0x0400005A RID: 90
		[VFXPropertyBinding(new string[] { "UnityEngine.Texture2D" })]
		[FormerlySerializedAs("PositionMapParameter")]
		public ExposedProperty PositionMapProperty = "PositionMap";

		// Token: 0x0400005B RID: 91
		[VFXPropertyBinding(new string[] { "System.Int32" })]
		[FormerlySerializedAs("PositionCountParameter")]
		public ExposedProperty PositionCountProperty = "PositionCount";

		// Token: 0x0400005C RID: 92
		public GameObject[] Targets;

		// Token: 0x0400005D RID: 93
		public bool EveryFrame;

		// Token: 0x0400005E RID: 94
		private Texture2D positionMap;

		// Token: 0x0400005F RID: 95
		private int count;
	}
}
