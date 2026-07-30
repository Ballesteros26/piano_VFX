using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000144 RID: 324
	[Serializable]
	public struct ScalableSettingSchemaId : IEquatable<ScalableSettingSchemaId>
	{
		// Token: 0x06000960 RID: 2400 RVA: 0x0004C074 File Offset: 0x0004A274
		internal ScalableSettingSchemaId(string id)
		{
			this.m_Id = id;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0004C07D File Offset: 0x0004A27D
		public bool Equals(ScalableSettingSchemaId other)
		{
			return this.m_Id == other.m_Id;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0004C090 File Offset: 0x0004A290
		public override bool Equals(object obj)
		{
			if (obj is ScalableSettingSchemaId)
			{
				ScalableSettingSchemaId scalableSettingSchemaId = (ScalableSettingSchemaId)obj;
				return scalableSettingSchemaId.m_Id == this.m_Id;
			}
			return false;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0004C0C1 File Offset: 0x0004A2C1
		public override int GetHashCode()
		{
			string id = this.m_Id;
			if (id == null)
			{
				return 0;
			}
			return id.GetHashCode();
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0004C0D4 File Offset: 0x0004A2D4
		public override string ToString()
		{
			return this.m_Id;
		}

		// Token: 0x04000EF9 RID: 3833
		public static readonly ScalableSettingSchemaId With3Levels = new ScalableSettingSchemaId("With3Levels");

		// Token: 0x04000EFA RID: 3834
		public static readonly ScalableSettingSchemaId With4Levels = new ScalableSettingSchemaId("With4Levels");

		// Token: 0x04000EFB RID: 3835
		[SerializeField]
		private string m_Id;
	}
}
