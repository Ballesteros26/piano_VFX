using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000047 RID: 71
	[Serializable]
	public sealed class SplineParameter : ParameterOverride<Spline>
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00009667 File Offset: 0x00007867
		protected internal override void OnEnable()
		{
			if (this.value != null)
			{
				this.value.Cache(int.MinValue);
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00009681 File Offset: 0x00007881
		internal override void SetValue(ParameterOverride parameter)
		{
			base.SetValue(parameter);
			if (this.value != null)
			{
				this.value.Cache(Time.renderedFrameCount);
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000096A4 File Offset: 0x000078A4
		public override void Interp(Spline from, Spline to, float t)
		{
			if (from == null || to == null)
			{
				base.Interp(from, to, t);
				return;
			}
			int renderedFrameCount = Time.renderedFrameCount;
			from.Cache(renderedFrameCount);
			to.Cache(renderedFrameCount);
			for (int i = 0; i < 128; i++)
			{
				float num = from.cachedData[i];
				float num2 = to.cachedData[i];
				this.value.cachedData[i] = num + (num2 - num) * t;
			}
		}
	}
}
