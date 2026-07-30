using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000379 RID: 889
	public struct ShaderTagId : IEquatable<ShaderTagId>
	{
		// Token: 0x06001ED4 RID: 7892 RVA: 0x00034585 File Offset: 0x00032785
		public ShaderTagId(string name)
		{
			this.m_Id = Shader.TagToID(name);
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x00034594 File Offset: 0x00032794
		// (set) Token: 0x06001ED6 RID: 7894 RVA: 0x000345AC File Offset: 0x000327AC
		internal int id
		{
			get
			{
				return this.m_Id;
			}
			set
			{
				this.m_Id = value;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x000345B8 File Offset: 0x000327B8
		public string name
		{
			get
			{
				return Shader.IDToTag(this.id);
			}
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x000345D8 File Offset: 0x000327D8
		public override bool Equals(object obj)
		{
			return obj is ShaderTagId && this.Equals((ShaderTagId)obj);
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x00034604 File Offset: 0x00032804
		public bool Equals(ShaderTagId other)
		{
			return this.m_Id == other.m_Id;
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x00034624 File Offset: 0x00032824
		public override int GetHashCode()
		{
			int num = 2079669542;
			return num * -1521134295 + this.m_Id.GetHashCode();
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x00034654 File Offset: 0x00032854
		public static bool operator ==(ShaderTagId tag1, ShaderTagId tag2)
		{
			return tag1.Equals(tag2);
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x00034670 File Offset: 0x00032870
		public static bool operator !=(ShaderTagId tag1, ShaderTagId tag2)
		{
			return !(tag1 == tag2);
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x0003468C File Offset: 0x0003288C
		public static explicit operator ShaderTagId(string name)
		{
			return new ShaderTagId(name);
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x000346A4 File Offset: 0x000328A4
		public static explicit operator string(ShaderTagId tagId)
		{
			return tagId.name;
		}

		// Token: 0x04000AF3 RID: 2803
		public static readonly ShaderTagId none = default(ShaderTagId);

		// Token: 0x04000AF4 RID: 2804
		private int m_Id;
	}
}
