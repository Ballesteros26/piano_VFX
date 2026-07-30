using System;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x0200000C RID: 12
	[Serializable]
	public class ExposedProperty
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000026F1 File Offset: 0x000008F1
		public static implicit operator ExposedProperty(string name)
		{
			return new ExposedProperty(name);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026F9 File Offset: 0x000008F9
		public static explicit operator string(ExposedProperty parameter)
		{
			return parameter.m_Name;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002704 File Offset: 0x00000904
		public static implicit operator int(ExposedProperty parameter)
		{
			if (parameter.m_Id == 0 && !string.IsNullOrEmpty(parameter.m_Name))
			{
				throw new InvalidOperationException("Unexpected constructor has been called");
			}
			if (parameter.m_Id == -1)
			{
				parameter.m_Id = Shader.PropertyToID(parameter.m_Name);
			}
			return parameter.m_Id;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002751 File Offset: 0x00000951
		public static ExposedProperty operator +(ExposedProperty self, ExposedProperty other)
		{
			return new ExposedProperty(self.m_Name + other.m_Name);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002769 File Offset: 0x00000969
		public ExposedProperty()
		{
			this.m_Id = -1;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002778 File Offset: 0x00000978
		private ExposedProperty(string name)
		{
			this.m_Name = name;
			this.m_Id = -1;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000026F9 File Offset: 0x000008F9
		public override string ToString()
		{
			return this.m_Name;
		}

		// Token: 0x04000016 RID: 22
		[SerializeField]
		private string m_Name;

		// Token: 0x04000017 RID: 23
		private int m_Id;
	}
}
