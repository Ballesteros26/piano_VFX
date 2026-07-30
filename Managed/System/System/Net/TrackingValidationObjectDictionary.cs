using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace System.Net
{
	// Token: 0x020004CA RID: 1226
	internal class TrackingValidationObjectDictionary : StringDictionary
	{
		// Token: 0x06002468 RID: 9320 RVA: 0x0008DE8C File Offset: 0x0008C08C
		internal TrackingValidationObjectDictionary(IDictionary<string, TrackingValidationObjectDictionary.ValidateAndParseValue> validators)
		{
			this.IsChanged = false;
			this.validators = validators;
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x0008DEA4 File Offset: 0x0008C0A4
		private void PersistValue(string key, string value, bool addValue)
		{
			key = key.ToLowerInvariant();
			if (!string.IsNullOrEmpty(value))
			{
				if (this.validators != null && this.validators.ContainsKey(key))
				{
					object obj = this.validators[key](value);
					if (this.internalObjects == null)
					{
						this.internalObjects = new Dictionary<string, object>();
					}
					if (addValue)
					{
						this.internalObjects.Add(key, obj);
						base.Add(key, obj.ToString());
					}
					else
					{
						this.internalObjects[key] = obj;
						base[key] = obj.ToString();
					}
				}
				else if (addValue)
				{
					base.Add(key, value);
				}
				else
				{
					base[key] = value;
				}
				this.IsChanged = true;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x0600246A RID: 9322 RVA: 0x0008DF57 File Offset: 0x0008C157
		// (set) Token: 0x0600246B RID: 9323 RVA: 0x0008DF5F File Offset: 0x0008C15F
		internal bool IsChanged { get; set; }

		// Token: 0x0600246C RID: 9324 RVA: 0x0008DF68 File Offset: 0x0008C168
		internal object InternalGet(string key)
		{
			if (this.internalObjects != null && this.internalObjects.ContainsKey(key))
			{
				return this.internalObjects[key];
			}
			return base[key];
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x0008DF94 File Offset: 0x0008C194
		internal void InternalSet(string key, object value)
		{
			if (this.internalObjects == null)
			{
				this.internalObjects = new Dictionary<string, object>();
			}
			this.internalObjects[key] = value;
			base[key] = value.ToString();
			this.IsChanged = true;
		}

		// Token: 0x17000788 RID: 1928
		public override string this[string key]
		{
			get
			{
				return base[key];
			}
			set
			{
				this.PersistValue(key, value, false);
			}
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x0008DFD5 File Offset: 0x0008C1D5
		public override void Add(string key, string value)
		{
			this.PersistValue(key, value, true);
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x0008DFE0 File Offset: 0x0008C1E0
		public override void Clear()
		{
			if (this.internalObjects != null)
			{
				this.internalObjects.Clear();
			}
			base.Clear();
			this.IsChanged = true;
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x0008E002 File Offset: 0x0008C202
		public override void Remove(string key)
		{
			if (this.internalObjects != null && this.internalObjects.ContainsKey(key))
			{
				this.internalObjects.Remove(key);
			}
			base.Remove(key);
			this.IsChanged = true;
		}

		// Token: 0x04002027 RID: 8231
		private IDictionary<string, object> internalObjects;

		// Token: 0x04002028 RID: 8232
		private readonly IDictionary<string, TrackingValidationObjectDictionary.ValidateAndParseValue> validators;

		// Token: 0x020004CB RID: 1227
		// (Invoke) Token: 0x06002474 RID: 9332
		internal delegate object ValidateAndParseValue(object valueToValidate);
	}
}
