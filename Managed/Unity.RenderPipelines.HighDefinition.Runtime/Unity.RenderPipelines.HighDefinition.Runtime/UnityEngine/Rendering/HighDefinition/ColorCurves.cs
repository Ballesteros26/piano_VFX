using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000CF RID: 207
	[VolumeComponentMenu("Post-processing/Color Curves")]
	[Serializable]
	public sealed class ColorCurves : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x00003AC0 File Offset: 0x00001CC0
		public bool IsActive()
		{
			return true;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0003814C File Offset: 0x0003634C
		public ColorCurves()
		{
			Keyframe[] array = new Keyframe[]
			{
				new Keyframe(0f, 0f, 1f, 1f),
				new Keyframe(1f, 1f, 1f, 1f)
			};
			float num = 0f;
			bool flag = false;
			Vector2 vector = new Vector2(0f, 1f);
			this.master = new TextureCurveParameter(new TextureCurve(array, num, flag, in vector), false);
			Keyframe[] array2 = new Keyframe[]
			{
				new Keyframe(0f, 0f, 1f, 1f),
				new Keyframe(1f, 1f, 1f, 1f)
			};
			float num2 = 0f;
			bool flag2 = false;
			vector = new Vector2(0f, 1f);
			this.red = new TextureCurveParameter(new TextureCurve(array2, num2, flag2, in vector), false);
			Keyframe[] array3 = new Keyframe[]
			{
				new Keyframe(0f, 0f, 1f, 1f),
				new Keyframe(1f, 1f, 1f, 1f)
			};
			float num3 = 0f;
			bool flag3 = false;
			vector = new Vector2(0f, 1f);
			this.green = new TextureCurveParameter(new TextureCurve(array3, num3, flag3, in vector), false);
			Keyframe[] array4 = new Keyframe[]
			{
				new Keyframe(0f, 0f, 1f, 1f),
				new Keyframe(1f, 1f, 1f, 1f)
			};
			float num4 = 0f;
			bool flag4 = false;
			vector = new Vector2(0f, 1f);
			this.blue = new TextureCurveParameter(new TextureCurve(array4, num4, flag4, in vector), false);
			Keyframe[] array5 = new Keyframe[0];
			float num5 = 0.5f;
			bool flag5 = true;
			vector = new Vector2(0f, 1f);
			this.hueVsHue = new TextureCurveParameter(new TextureCurve(array5, num5, flag5, in vector), false);
			Keyframe[] array6 = new Keyframe[0];
			float num6 = 0.5f;
			bool flag6 = true;
			vector = new Vector2(0f, 1f);
			this.hueVsSat = new TextureCurveParameter(new TextureCurve(array6, num6, flag6, in vector), false);
			Keyframe[] array7 = new Keyframe[0];
			float num7 = 0.5f;
			bool flag7 = false;
			vector = new Vector2(0f, 1f);
			this.satVsSat = new TextureCurveParameter(new TextureCurve(array7, num7, flag7, in vector), false);
			Keyframe[] array8 = new Keyframe[0];
			float num8 = 0.5f;
			bool flag8 = false;
			vector = new Vector2(0f, 1f);
			this.lumVsSat = new TextureCurveParameter(new TextureCurve(array8, num8, flag8, in vector), false);
			base..ctor();
		}

		// Token: 0x04000777 RID: 1911
		public TextureCurveParameter master;

		// Token: 0x04000778 RID: 1912
		public TextureCurveParameter red;

		// Token: 0x04000779 RID: 1913
		public TextureCurveParameter green;

		// Token: 0x0400077A RID: 1914
		public TextureCurveParameter blue;

		// Token: 0x0400077B RID: 1915
		public TextureCurveParameter hueVsHue;

		// Token: 0x0400077C RID: 1916
		public TextureCurveParameter hueVsSat;

		// Token: 0x0400077D RID: 1917
		public TextureCurveParameter satVsSat;

		// Token: 0x0400077E RID: 1918
		public TextureCurveParameter lumVsSat;

		// Token: 0x0400077F RID: 1919
		[SerializeField]
		private int m_SelectedCurve;
	}
}
