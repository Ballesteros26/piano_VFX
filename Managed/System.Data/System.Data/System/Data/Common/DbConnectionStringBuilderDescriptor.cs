using System;
using System.ComponentModel;

namespace System.Data.Common
{
	// Token: 0x02000342 RID: 834
	internal class DbConnectionStringBuilderDescriptor : PropertyDescriptor
	{
		// Token: 0x06002735 RID: 10037 RVA: 0x000AED84 File Offset: 0x000ACF84
		internal DbConnectionStringBuilderDescriptor(string propertyName, Type componentType, Type propertyType, bool isReadOnly, Attribute[] attributes)
			: base(propertyName, attributes)
		{
			this.ComponentType = componentType;
			this.PropertyType = propertyType;
			this.IsReadOnly = isReadOnly;
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06002736 RID: 10038 RVA: 0x000AEDA5 File Offset: 0x000ACFA5
		// (set) Token: 0x06002737 RID: 10039 RVA: 0x000AEDAD File Offset: 0x000ACFAD
		internal bool RefreshOnChange { get; set; }

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06002738 RID: 10040 RVA: 0x000AEDB6 File Offset: 0x000ACFB6
		public override Type ComponentType { get; }

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06002739 RID: 10041 RVA: 0x000AEDBE File Offset: 0x000ACFBE
		public override bool IsReadOnly { get; }

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x0600273A RID: 10042 RVA: 0x000AEDC6 File Offset: 0x000ACFC6
		public override Type PropertyType { get; }

		// Token: 0x0600273B RID: 10043 RVA: 0x000AEDD0 File Offset: 0x000ACFD0
		public override bool CanResetValue(object component)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			return dbConnectionStringBuilder != null && dbConnectionStringBuilder.ShouldSerialize(this.DisplayName);
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000AEDF8 File Offset: 0x000ACFF8
		public override object GetValue(object component)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			object obj;
			if (dbConnectionStringBuilder != null && dbConnectionStringBuilder.TryGetValue(this.DisplayName, out obj))
			{
				return obj;
			}
			return null;
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x000AEE24 File Offset: 0x000AD024
		public override void ResetValue(object component)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			if (dbConnectionStringBuilder != null)
			{
				dbConnectionStringBuilder.Remove(this.DisplayName);
				if (this.RefreshOnChange)
				{
					dbConnectionStringBuilder.ClearPropertyDescriptors();
				}
			}
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x000AEE58 File Offset: 0x000AD058
		public override void SetValue(object component, object value)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			if (dbConnectionStringBuilder != null)
			{
				if (typeof(string) == this.PropertyType && string.Empty.Equals(value))
				{
					value = null;
				}
				dbConnectionStringBuilder[this.DisplayName] = value;
				if (this.RefreshOnChange)
				{
					dbConnectionStringBuilder.ClearPropertyDescriptors();
				}
			}
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000AEEB4 File Offset: 0x000AD0B4
		public override bool ShouldSerializeValue(object component)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			return dbConnectionStringBuilder != null && dbConnectionStringBuilder.ShouldSerialize(this.DisplayName);
		}
	}
}
