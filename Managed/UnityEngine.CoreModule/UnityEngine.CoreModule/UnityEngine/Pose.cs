using System;

namespace UnityEngine
{
	// Token: 0x020001F3 RID: 499
	[Serializable]
	public struct Pose : IEquatable<Pose>
	{
		// Token: 0x06001617 RID: 5655 RVA: 0x00024284 File Offset: 0x00022484
		public Pose(Vector3 position, Quaternion rotation)
		{
			this.position = position;
			this.rotation = rotation;
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x00024298 File Offset: 0x00022498
		public override string ToString()
		{
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.position.ToString(),
				this.rotation.ToString()
			});
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x000242E4 File Offset: 0x000224E4
		public string ToString(string format)
		{
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.position.ToString(format),
				this.rotation.ToString(format)
			});
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00024324 File Offset: 0x00022524
		public Pose GetTransformedBy(Pose lhs)
		{
			return new Pose
			{
				position = lhs.position + lhs.rotation * this.position,
				rotation = lhs.rotation * this.rotation
			};
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0002437C File Offset: 0x0002257C
		public Pose GetTransformedBy(Transform lhs)
		{
			return new Pose
			{
				position = lhs.TransformPoint(this.position),
				rotation = lhs.rotation * this.rotation
			};
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x000243C4 File Offset: 0x000225C4
		public Vector3 forward
		{
			get
			{
				return this.rotation * Vector3.forward;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600161D RID: 5661 RVA: 0x000243E8 File Offset: 0x000225E8
		public Vector3 right
		{
			get
			{
				return this.rotation * Vector3.right;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x0002440C File Offset: 0x0002260C
		public Vector3 up
		{
			get
			{
				return this.rotation * Vector3.up;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x00024430 File Offset: 0x00022630
		public static Pose identity
		{
			get
			{
				return Pose.k_Identity;
			}
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x00024448 File Offset: 0x00022648
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Pose);
			return !flag && this.Equals((Pose)obj);
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x0002447C File Offset: 0x0002267C
		public bool Equals(Pose other)
		{
			return this.position == other.position && this.rotation == other.rotation;
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x000244B8 File Offset: 0x000226B8
		public override int GetHashCode()
		{
			return this.position.GetHashCode() ^ (this.rotation.GetHashCode() << 1);
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x000244F0 File Offset: 0x000226F0
		public static bool operator ==(Pose a, Pose b)
		{
			return a.Equals(b);
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x0002450C File Offset: 0x0002270C
		public static bool operator !=(Pose a, Pose b)
		{
			return !(a == b);
		}

		// Token: 0x040006DA RID: 1754
		public Vector3 position;

		// Token: 0x040006DB RID: 1755
		public Quaternion rotation;

		// Token: 0x040006DC RID: 1756
		private static readonly Pose k_Identity = new Pose(Vector3.zero, Quaternion.identity);
	}
}
