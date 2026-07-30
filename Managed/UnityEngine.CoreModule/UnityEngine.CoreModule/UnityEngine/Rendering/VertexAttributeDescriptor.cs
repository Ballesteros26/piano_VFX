using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000330 RID: 816
	[UsedByNativeCode]
	public struct VertexAttributeDescriptor : IEquatable<VertexAttributeDescriptor>
	{
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x0002C2CC File Offset: 0x0002A4CC
		// (set) Token: 0x06001B01 RID: 6913 RVA: 0x0002C2D4 File Offset: 0x0002A4D4
		public VertexAttribute attribute { get; set; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x0002C2DD File Offset: 0x0002A4DD
		// (set) Token: 0x06001B03 RID: 6915 RVA: 0x0002C2E5 File Offset: 0x0002A4E5
		public VertexAttributeFormat format { get; set; }

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x0002C2EE File Offset: 0x0002A4EE
		// (set) Token: 0x06001B05 RID: 6917 RVA: 0x0002C2F6 File Offset: 0x0002A4F6
		public int dimension { get; set; }

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001B06 RID: 6918 RVA: 0x0002C2FF File Offset: 0x0002A4FF
		// (set) Token: 0x06001B07 RID: 6919 RVA: 0x0002C307 File Offset: 0x0002A507
		public int stream { get; set; }

		// Token: 0x06001B08 RID: 6920 RVA: 0x0002C310 File Offset: 0x0002A510
		public VertexAttributeDescriptor(VertexAttribute attribute = VertexAttribute.Position, VertexAttributeFormat format = VertexAttributeFormat.Float32, int dimension = 3, int stream = 0)
		{
			this.attribute = attribute;
			this.format = format;
			this.dimension = dimension;
			this.stream = stream;
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0002C334 File Offset: 0x0002A534
		public override string ToString()
		{
			return string.Format("(attr={0} fmt={1} dim={2} stream={3})", new object[] { this.attribute, this.format, this.dimension, this.stream });
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0002C390 File Offset: 0x0002A590
		public override int GetHashCode()
		{
			int num = 17;
			num = (int)(num * 23 + this.attribute);
			num = (int)(num * 23 + this.format);
			num = num * 23 + this.dimension;
			return num * 23 + this.stream;
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0002C3D8 File Offset: 0x0002A5D8
		public override bool Equals(object other)
		{
			bool flag = !(other is VertexAttributeDescriptor);
			return !flag && this.Equals((VertexAttributeDescriptor)other);
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x0002C40C File Offset: 0x0002A60C
		public bool Equals(VertexAttributeDescriptor other)
		{
			return this.attribute == other.attribute && this.format == other.format && this.dimension == other.dimension && this.stream == other.stream;
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x0002C460 File Offset: 0x0002A660
		public static bool operator ==(VertexAttributeDescriptor lhs, VertexAttributeDescriptor rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x0002C47C File Offset: 0x0002A67C
		public static bool operator !=(VertexAttributeDescriptor lhs, VertexAttributeDescriptor rhs)
		{
			return !lhs.Equals(rhs);
		}
	}
}
