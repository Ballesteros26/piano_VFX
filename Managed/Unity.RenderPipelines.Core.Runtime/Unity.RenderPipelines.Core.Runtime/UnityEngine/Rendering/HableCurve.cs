using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005B RID: 91
	public class HableCurve
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000B66C File Offset: 0x0000986C
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000B674 File Offset: 0x00009874
		public float whitePoint { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000B67D File Offset: 0x0000987D
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000B685 File Offset: 0x00009885
		public float inverseWhitePoint { get; private set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000B68E File Offset: 0x0000988E
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000B696 File Offset: 0x00009896
		public float x0 { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000B69F File Offset: 0x0000989F
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000B6A7 File Offset: 0x000098A7
		public float x1 { get; private set; }

		// Token: 0x060002B7 RID: 695 RVA: 0x0000B6B0 File Offset: 0x000098B0
		public HableCurve()
		{
			for (int i = 0; i < 3; i++)
			{
				this.segments[i] = new HableCurve.Segment();
			}
			this.uniforms = new HableCurve.Uniforms(this);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000B6F4 File Offset: 0x000098F4
		public float Eval(float x)
		{
			float num = x * this.inverseWhitePoint;
			int num2 = ((num < this.x0) ? 0 : ((num < this.x1) ? 1 : 2));
			return this.segments[num2].Eval(num);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000B734 File Offset: 0x00009934
		public void Init(float toeStrength, float toeLength, float shoulderStrength, float shoulderLength, float shoulderAngle, float gamma)
		{
			HableCurve.DirectParams directParams = default(HableCurve.DirectParams);
			toeLength = Mathf.Pow(Mathf.Clamp01(toeLength), 2.2f);
			toeStrength = Mathf.Clamp01(toeStrength);
			shoulderAngle = Mathf.Clamp01(shoulderAngle);
			shoulderStrength = Mathf.Clamp(shoulderStrength, 1E-05f, 0.99999f);
			shoulderLength = Mathf.Max(0f, shoulderLength);
			gamma = Mathf.Max(1E-05f, gamma);
			float num = toeLength * 0.5f;
			float num2 = (1f - toeStrength) * num;
			float num3 = 1f - num2;
			float num4 = num + num3;
			float num5 = (1f - shoulderStrength) * num3;
			float num6 = num + num5;
			float num7 = num2 + num5;
			float num8 = Mathf.Pow(2f, shoulderLength) - 1f;
			float num9 = num4 + num8;
			directParams.x0 = num;
			directParams.y0 = num2;
			directParams.x1 = num6;
			directParams.y1 = num7;
			directParams.W = num9;
			directParams.gamma = gamma;
			directParams.overshootX = directParams.W * 2f * shoulderAngle * shoulderLength;
			directParams.overshootY = 0.5f * shoulderAngle * shoulderLength;
			this.InitSegments(directParams);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000B850 File Offset: 0x00009A50
		private void InitSegments(HableCurve.DirectParams srcParams)
		{
			HableCurve.DirectParams directParams = srcParams;
			this.whitePoint = srcParams.W;
			this.inverseWhitePoint = 1f / srcParams.W;
			directParams.W = 1f;
			directParams.x0 /= srcParams.W;
			directParams.x1 /= srcParams.W;
			directParams.overshootX = srcParams.overshootX / srcParams.W;
			float num;
			float num2;
			this.AsSlopeIntercept(out num, out num2, directParams.x0, directParams.x1, directParams.y0, directParams.y1);
			float gamma = srcParams.gamma;
			HableCurve.Segment segment = this.segments[1];
			segment.offsetX = -(num2 / num);
			segment.offsetY = 0f;
			segment.scaleX = 1f;
			segment.scaleY = 1f;
			segment.lnA = gamma * Mathf.Log(num);
			segment.B = gamma;
			float num3 = this.EvalDerivativeLinearGamma(num, num2, gamma, directParams.x0);
			float num4 = this.EvalDerivativeLinearGamma(num, num2, gamma, directParams.x1);
			directParams.y0 = Mathf.Max(1E-05f, Mathf.Pow(directParams.y0, directParams.gamma));
			directParams.y1 = Mathf.Max(1E-05f, Mathf.Pow(directParams.y1, directParams.gamma));
			directParams.overshootY = Mathf.Pow(1f + directParams.overshootY, directParams.gamma) - 1f;
			this.x0 = directParams.x0;
			this.x1 = directParams.x1;
			HableCurve.Segment segment2 = this.segments[0];
			segment2.offsetX = 0f;
			segment2.offsetY = 0f;
			segment2.scaleX = 1f;
			segment2.scaleY = 1f;
			float num5;
			float num6;
			this.SolveAB(out num5, out num6, directParams.x0, directParams.y0, num3);
			segment2.lnA = num5;
			segment2.B = num6;
			HableCurve.Segment segment3 = this.segments[2];
			float num7 = 1f + directParams.overshootX - directParams.x1;
			float num8 = 1f + directParams.overshootY - directParams.y1;
			float num9;
			float num10;
			this.SolveAB(out num9, out num10, num7, num8, num4);
			segment3.offsetX = 1f + directParams.overshootX;
			segment3.offsetY = 1f + directParams.overshootY;
			segment3.scaleX = -1f;
			segment3.scaleY = -1f;
			segment3.lnA = num9;
			segment3.B = num10;
			float num11 = this.segments[2].Eval(1f);
			float num12 = 1f / num11;
			this.segments[0].offsetY *= num12;
			this.segments[0].scaleY *= num12;
			this.segments[1].offsetY *= num12;
			this.segments[1].scaleY *= num12;
			this.segments[2].offsetY *= num12;
			this.segments[2].scaleY *= num12;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000BB69 File Offset: 0x00009D69
		private void SolveAB(out float lnA, out float B, float x0, float y0, float m)
		{
			B = m * x0 / y0;
			lnA = Mathf.Log(y0) - B * Mathf.Log(x0);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000BB88 File Offset: 0x00009D88
		private void AsSlopeIntercept(out float m, out float b, float x0, float x1, float y0, float y1)
		{
			float num = y1 - y0;
			float num2 = x1 - x0;
			if (num2 == 0f)
			{
				m = 1f;
			}
			else
			{
				m = num / num2;
			}
			b = y0 - x0 * m;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000BBBF File Offset: 0x00009DBF
		private float EvalDerivativeLinearGamma(float m, float b, float g, float x)
		{
			return g * m * Mathf.Pow(m * x + b, g - 1f);
		}

		// Token: 0x04000188 RID: 392
		public readonly HableCurve.Segment[] segments = new HableCurve.Segment[3];

		// Token: 0x04000189 RID: 393
		public readonly HableCurve.Uniforms uniforms;

		// Token: 0x020000DC RID: 220
		public class Segment
		{
			// Token: 0x0600052F RID: 1327 RVA: 0x000120B4 File Offset: 0x000102B4
			public float Eval(float x)
			{
				float num = (x - this.offsetX) * this.scaleX;
				float num2 = 0f;
				if (num > 0f)
				{
					num2 = Mathf.Exp(this.lnA + this.B * Mathf.Log(num));
				}
				return num2 * this.scaleY + this.offsetY;
			}

			// Token: 0x040002BF RID: 703
			public float offsetX;

			// Token: 0x040002C0 RID: 704
			public float offsetY;

			// Token: 0x040002C1 RID: 705
			public float scaleX;

			// Token: 0x040002C2 RID: 706
			public float scaleY;

			// Token: 0x040002C3 RID: 707
			public float lnA;

			// Token: 0x040002C4 RID: 708
			public float B;
		}

		// Token: 0x020000DD RID: 221
		private struct DirectParams
		{
			// Token: 0x040002C5 RID: 709
			internal float x0;

			// Token: 0x040002C6 RID: 710
			internal float y0;

			// Token: 0x040002C7 RID: 711
			internal float x1;

			// Token: 0x040002C8 RID: 712
			internal float y1;

			// Token: 0x040002C9 RID: 713
			internal float W;

			// Token: 0x040002CA RID: 714
			internal float overshootX;

			// Token: 0x040002CB RID: 715
			internal float overshootY;

			// Token: 0x040002CC RID: 716
			internal float gamma;
		}

		// Token: 0x020000DE RID: 222
		public class Uniforms
		{
			// Token: 0x06000531 RID: 1329 RVA: 0x00012108 File Offset: 0x00010308
			internal Uniforms(HableCurve parent)
			{
				this.parent = parent;
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06000532 RID: 1330 RVA: 0x00012117 File Offset: 0x00010317
			public Vector4 curve
			{
				get
				{
					return new Vector4(this.parent.inverseWhitePoint, this.parent.x0, this.parent.x1, 0f);
				}
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x06000533 RID: 1331 RVA: 0x00012144 File Offset: 0x00010344
			public Vector4 toeSegmentA
			{
				get
				{
					return new Vector4(this.parent.segments[0].offsetX, this.parent.segments[0].offsetY, this.parent.segments[0].scaleX, this.parent.segments[0].scaleY);
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001219E File Offset: 0x0001039E
			public Vector4 toeSegmentB
			{
				get
				{
					return new Vector4(this.parent.segments[0].lnA, this.parent.segments[0].B, 0f, 0f);
				}
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x06000535 RID: 1333 RVA: 0x000121D4 File Offset: 0x000103D4
			public Vector4 midSegmentA
			{
				get
				{
					return new Vector4(this.parent.segments[1].offsetX, this.parent.segments[1].offsetY, this.parent.segments[1].scaleX, this.parent.segments[1].scaleY);
				}
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x06000536 RID: 1334 RVA: 0x0001222E File Offset: 0x0001042E
			public Vector4 midSegmentB
			{
				get
				{
					return new Vector4(this.parent.segments[1].lnA, this.parent.segments[1].B, 0f, 0f);
				}
			}

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x06000537 RID: 1335 RVA: 0x00012264 File Offset: 0x00010464
			public Vector4 shoSegmentA
			{
				get
				{
					return new Vector4(this.parent.segments[2].offsetX, this.parent.segments[2].offsetY, this.parent.segments[2].scaleX, this.parent.segments[2].scaleY);
				}
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x06000538 RID: 1336 RVA: 0x000122BE File Offset: 0x000104BE
			public Vector4 shoSegmentB
			{
				get
				{
					return new Vector4(this.parent.segments[2].lnA, this.parent.segments[2].B, 0f, 0f);
				}
			}

			// Token: 0x040002CD RID: 717
			private HableCurve parent;
		}
	}
}
