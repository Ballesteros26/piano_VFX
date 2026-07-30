using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200013E RID: 318
	[Serializable]
	public class ScalableSetting<T> : ISerializationCallbackReceiver
	{
		// Token: 0x06000950 RID: 2384 RVA: 0x0004BE23 File Offset: 0x0004A023
		public ScalableSetting(T[] values, ScalableSettingSchemaId schemaId)
		{
			this.m_Values = values;
			this.m_SchemaId = schemaId;
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x0004BE39 File Offset: 0x0004A039
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x0004BE41 File Offset: 0x0004A041
		public ScalableSettingSchemaId schemaId
		{
			get
			{
				return this.m_SchemaId;
			}
			set
			{
				this.m_SchemaId = value;
			}
		}

		// Token: 0x1700016C RID: 364
		public T this[int index]
		{
			get
			{
				if (this.m_Values == null || index < 0 || index >= this.m_Values.Length)
				{
					return default(T);
				}
				return this.m_Values[index];
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0004BE86 File Offset: 0x0004A086
		public bool TryGet(int index, out T value)
		{
			if (index >= 0 && index < this.m_Values.Length)
			{
				value = this.m_Values[index];
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0004BEB4 File Offset: 0x0004A0B4
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			ScalableSettingSchema scalableSettingSchema;
			if (ScalableSettingSchema.Schemas.TryGetValue(this.m_SchemaId, out scalableSettingSchema))
			{
				Array.Resize<T>(ref this.m_Values, scalableSettingSchema.levelCount);
				return;
			}
			if (this.m_Values == null)
			{
				this.m_Values = new T[0];
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0004BEFC File Offset: 0x0004A0FC
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			ScalableSettingSchema scalableSettingSchema;
			if (ScalableSettingSchema.Schemas.TryGetValue(this.m_SchemaId, out scalableSettingSchema))
			{
				Array.Resize<T>(ref this.m_Values, scalableSettingSchema.levelCount);
				return;
			}
			if (this.m_Values == null)
			{
				this.m_Values = new T[0];
			}
		}

		// Token: 0x04000EF5 RID: 3829
		[SerializeField]
		private T[] m_Values;

		// Token: 0x04000EF6 RID: 3830
		[SerializeField]
		private ScalableSettingSchemaId m_SchemaId;
	}
}
