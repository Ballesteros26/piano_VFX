using System;
using System.Globalization;
using System.Reflection;

namespace System.ComponentModel.DataAnnotations
{
	// Token: 0x0200001C RID: 28
	internal class LocalizableString
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x0000358A File Offset: 0x0000178A
		public LocalizableString(string propertyName)
		{
			this._propertyName = propertyName;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003599 File Offset: 0x00001799
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000035A1 File Offset: 0x000017A1
		public string Value
		{
			get
			{
				return this._propertyValue;
			}
			set
			{
				if (this._propertyValue != value)
				{
					this.ClearCache();
					this._propertyValue = value;
				}
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000035BE File Offset: 0x000017BE
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000035C6 File Offset: 0x000017C6
		public Type ResourceType
		{
			get
			{
				return this._resourceType;
			}
			set
			{
				if (this._resourceType != value)
				{
					this.ClearCache();
					this._resourceType = value;
				}
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000035E3 File Offset: 0x000017E3
		private void ClearCache()
		{
			this._cachedResult = null;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000035EC File Offset: 0x000017EC
		public string GetLocalizableValue()
		{
			if (this._cachedResult == null)
			{
				if (this._propertyValue == null || this._resourceType == null)
				{
					this._cachedResult = () => this._propertyValue;
				}
				else
				{
					PropertyInfo property = this._resourceType.GetProperty(this._propertyValue);
					bool flag = false;
					if (!this._resourceType.IsVisible || property == null || property.PropertyType != typeof(string))
					{
						flag = true;
					}
					else
					{
						MethodInfo getMethod = property.GetGetMethod();
						if (getMethod == null || !getMethod.IsPublic || !getMethod.IsStatic)
						{
							flag = true;
						}
					}
					if (flag)
					{
						string exceptionMessage = string.Format(CultureInfo.CurrentCulture, "Cannot retrieve property '{0}' because localization failed.  Type '{1}' is not public or does not contain a public static string property with the name '{2}'.", this._propertyName, this._resourceType.FullName, this._propertyValue);
						this._cachedResult = delegate
						{
							throw new InvalidOperationException(exceptionMessage);
						};
					}
					else
					{
						this._cachedResult = () => (string)property.GetValue(null, null);
					}
				}
			}
			return this._cachedResult();
		}

		// Token: 0x04000077 RID: 119
		private string _propertyName;

		// Token: 0x04000078 RID: 120
		private string _propertyValue;

		// Token: 0x04000079 RID: 121
		private Type _resourceType;

		// Token: 0x0400007A RID: 122
		private Func<string> _cachedResult;
	}
}
