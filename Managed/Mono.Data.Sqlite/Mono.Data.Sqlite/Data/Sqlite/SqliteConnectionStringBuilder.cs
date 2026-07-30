using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Reflection;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000017 RID: 23
	[DefaultProperty("DataSource")]
	[DefaultMember("Item")]
	public sealed class SqliteConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x0600015B RID: 347 RVA: 0x000085AD File Offset: 0x000067AD
		public SqliteConnectionStringBuilder()
		{
			this.Initialize(null);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000085BC File Offset: 0x000067BC
		public SqliteConnectionStringBuilder(string connectionString)
		{
			this.Initialize(connectionString);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000085CC File Offset: 0x000067CC
		private void Initialize(string cnnString)
		{
			this._properties = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
			try
			{
				base.GetProperties(this._properties);
			}
			catch (NotImplementedException)
			{
				this.FallbackGetProperties(this._properties);
			}
			if (!string.IsNullOrEmpty(cnnString))
			{
				base.ConnectionString = cnnString;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00008628 File Offset: 0x00006828
		// (set) Token: 0x0600015F RID: 351 RVA: 0x0000864E File Offset: 0x0000684E
		[Browsable(true)]
		[DefaultValue(3)]
		public int Version
		{
			get
			{
				object obj;
				this.TryGetValue("version", out obj);
				return Convert.ToInt32(obj, CultureInfo.CurrentCulture);
			}
			set
			{
				if (value != 3)
				{
					throw new NotSupportedException();
				}
				this["version"] = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000866C File Offset: 0x0000686C
		// (set) Token: 0x06000161 RID: 353 RVA: 0x000086B0 File Offset: 0x000068B0
		[DisplayName("Synchronous")]
		[Browsable(true)]
		[DefaultValue(SynchronizationModes.Normal)]
		public SynchronizationModes SyncMode
		{
			get
			{
				object obj;
				this.TryGetValue("synchronous", out obj);
				if (obj is string)
				{
					return (SynchronizationModes)TypeDescriptor.GetConverter(typeof(SynchronizationModes)).ConvertFrom(obj);
				}
				return (SynchronizationModes)obj;
			}
			set
			{
				this["synchronous"] = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000086C4 File Offset: 0x000068C4
		// (set) Token: 0x06000163 RID: 355 RVA: 0x000086E5 File Offset: 0x000068E5
		[Browsable(true)]
		[DefaultValue(false)]
		public bool UseUTF16Encoding
		{
			get
			{
				object obj;
				this.TryGetValue("useutf16encoding", out obj);
				return SqliteConvert.ToBoolean(obj);
			}
			set
			{
				this["useutf16encoding"] = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000086F8 File Offset: 0x000068F8
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00008719 File Offset: 0x00006919
		[Browsable(true)]
		[DefaultValue(false)]
		public bool Pooling
		{
			get
			{
				object obj;
				this.TryGetValue("pooling", out obj);
				return SqliteConvert.ToBoolean(obj);
			}
			set
			{
				this["pooling"] = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000872C File Offset: 0x0000692C
		// (set) Token: 0x06000167 RID: 359 RVA: 0x0000874D File Offset: 0x0000694D
		[Browsable(true)]
		[DefaultValue(true)]
		public bool BinaryGUID
		{
			get
			{
				object obj;
				this.TryGetValue("binaryguid", out obj);
				return SqliteConvert.ToBoolean(obj);
			}
			set
			{
				this["binaryguid"] = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00008760 File Offset: 0x00006960
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00008781 File Offset: 0x00006981
		[DisplayName("Data Source")]
		[Browsable(true)]
		[DefaultValue("")]
		public string DataSource
		{
			get
			{
				object obj;
				this.TryGetValue("data source", out obj);
				return obj.ToString();
			}
			set
			{
				this["data source"] = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00008790 File Offset: 0x00006990
		// (set) Token: 0x0600016B RID: 363 RVA: 0x000087B1 File Offset: 0x000069B1
		[Browsable(false)]
		public string Uri
		{
			get
			{
				object obj;
				this.TryGetValue("uri", out obj);
				return obj.ToString();
			}
			set
			{
				this["uri"] = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000087C0 File Offset: 0x000069C0
		// (set) Token: 0x0600016D RID: 365 RVA: 0x000087E6 File Offset: 0x000069E6
		[DisplayName("Default Timeout")]
		[Browsable(true)]
		[DefaultValue(30)]
		public int DefaultTimeout
		{
			get
			{
				object obj;
				this.TryGetValue("default timeout", out obj);
				return Convert.ToInt32(obj, CultureInfo.CurrentCulture);
			}
			set
			{
				this["default timeout"] = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000087FC File Offset: 0x000069FC
		// (set) Token: 0x0600016F RID: 367 RVA: 0x0000881D File Offset: 0x00006A1D
		[Browsable(true)]
		[DefaultValue(true)]
		public bool Enlist
		{
			get
			{
				object obj;
				this.TryGetValue("enlist", out obj);
				return SqliteConvert.ToBoolean(obj);
			}
			set
			{
				this["enlist"] = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00008830 File Offset: 0x00006A30
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00008851 File Offset: 0x00006A51
		[Browsable(true)]
		[DefaultValue(false)]
		public bool FailIfMissing
		{
			get
			{
				object obj;
				this.TryGetValue("failifmissing", out obj);
				return SqliteConvert.ToBoolean(obj);
			}
			set
			{
				this["failifmissing"] = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00008864 File Offset: 0x00006A64
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00008885 File Offset: 0x00006A85
		[DisplayName("Legacy Format")]
		[Browsable(true)]
		[DefaultValue(false)]
		public bool LegacyFormat
		{
			get
			{
				object obj;
				this.TryGetValue("legacy format", out obj);
				return SqliteConvert.ToBoolean(obj);
			}
			set
			{
				this["legacy format"] = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00008898 File Offset: 0x00006A98
		// (set) Token: 0x06000175 RID: 373 RVA: 0x000088B9 File Offset: 0x00006AB9
		[DisplayName("Read Only")]
		[Browsable(true)]
		[DefaultValue(false)]
		public bool ReadOnly
		{
			get
			{
				object obj;
				this.TryGetValue("read only", out obj);
				return SqliteConvert.ToBoolean(obj);
			}
			set
			{
				this["read only"] = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000088CC File Offset: 0x00006ACC
		// (set) Token: 0x06000177 RID: 375 RVA: 0x000088ED File Offset: 0x00006AED
		[Browsable(true)]
		[PasswordPropertyText(true)]
		[DefaultValue("")]
		public string Password
		{
			get
			{
				object obj;
				this.TryGetValue("password", out obj);
				return obj.ToString();
			}
			set
			{
				this["password"] = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000088FC File Offset: 0x00006AFC
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00008922 File Offset: 0x00006B22
		[DisplayName("Page Size")]
		[Browsable(true)]
		[DefaultValue(1024)]
		public int PageSize
		{
			get
			{
				object obj;
				this.TryGetValue("page size", out obj);
				return Convert.ToInt32(obj, CultureInfo.CurrentCulture);
			}
			set
			{
				this["page size"] = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00008938 File Offset: 0x00006B38
		// (set) Token: 0x0600017B RID: 379 RVA: 0x0000895E File Offset: 0x00006B5E
		[DisplayName("Max Page Count")]
		[Browsable(true)]
		[DefaultValue(0)]
		public int MaxPageCount
		{
			get
			{
				object obj;
				this.TryGetValue("max page count", out obj);
				return Convert.ToInt32(obj, CultureInfo.CurrentCulture);
			}
			set
			{
				this["max page count"] = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00008974 File Offset: 0x00006B74
		// (set) Token: 0x0600017D RID: 381 RVA: 0x0000899A File Offset: 0x00006B9A
		[DisplayName("Cache Size")]
		[Browsable(true)]
		[DefaultValue(2000)]
		public int CacheSize
		{
			get
			{
				object obj;
				this.TryGetValue("cache size", out obj);
				return Convert.ToInt32(obj, CultureInfo.CurrentCulture);
			}
			set
			{
				this["cache size"] = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000089B0 File Offset: 0x00006BB0
		// (set) Token: 0x0600017F RID: 383 RVA: 0x000089F4 File Offset: 0x00006BF4
		[Browsable(true)]
		[DefaultValue(SQLiteDateFormats.ISO8601)]
		public SQLiteDateFormats DateTimeFormat
		{
			get
			{
				object obj;
				this.TryGetValue("datetimeformat", out obj);
				if (obj is string)
				{
					return (SQLiteDateFormats)TypeDescriptor.GetConverter(typeof(SQLiteDateFormats)).ConvertFrom(obj);
				}
				return (SQLiteDateFormats)obj;
			}
			set
			{
				this["datetimeformat"] = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00008A08 File Offset: 0x00006C08
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00008A4C File Offset: 0x00006C4C
		[Browsable(true)]
		[DefaultValue(SQLiteJournalModeEnum.Delete)]
		[DisplayName("Journal Mode")]
		public SQLiteJournalModeEnum JournalMode
		{
			get
			{
				object obj;
				this.TryGetValue("journal mode", out obj);
				if (obj is string)
				{
					return (SQLiteJournalModeEnum)TypeDescriptor.GetConverter(typeof(SQLiteJournalModeEnum)).ConvertFrom(obj);
				}
				return (SQLiteJournalModeEnum)obj;
			}
			set
			{
				this["journal mode"] = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00008A60 File Offset: 0x00006C60
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00008AA4 File Offset: 0x00006CA4
		[Browsable(true)]
		[DefaultValue(IsolationLevel.Serializable)]
		[DisplayName("Default Isolation Level")]
		public IsolationLevel DefaultIsolationLevel
		{
			get
			{
				object obj;
				this.TryGetValue("default isolationlevel", out obj);
				if (obj is string)
				{
					return (IsolationLevel)TypeDescriptor.GetConverter(typeof(IsolationLevel)).ConvertFrom(obj);
				}
				return (IsolationLevel)obj;
			}
			set
			{
				this["default isolationlevel"] = value;
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00008AB8 File Offset: 0x00006CB8
		public override bool TryGetValue(string keyword, out object value)
		{
			bool flag = base.TryGetValue(keyword, out value);
			if (!this._properties.ContainsKey(keyword))
			{
				return flag;
			}
			PropertyDescriptor propertyDescriptor = this._properties[keyword] as PropertyDescriptor;
			if (propertyDescriptor == null)
			{
				return flag;
			}
			if (flag)
			{
				if (propertyDescriptor.PropertyType == typeof(bool))
				{
					value = SqliteConvert.ToBoolean(value);
				}
				else
				{
					value = TypeDescriptor.GetConverter(propertyDescriptor.PropertyType).ConvertFrom(value);
				}
			}
			else
			{
				DefaultValueAttribute defaultValueAttribute = propertyDescriptor.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
				if (defaultValueAttribute != null)
				{
					value = defaultValueAttribute.Value;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00008B60 File Offset: 0x00006D60
		private void FallbackGetProperties(Hashtable propertyList)
		{
			foreach (object obj in TypeDescriptor.GetProperties(this, true))
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Name != "ConnectionString" && !propertyList.ContainsKey(propertyDescriptor.DisplayName))
				{
					propertyList.Add(propertyDescriptor.DisplayName, propertyDescriptor);
				}
			}
		}

		// Token: 0x04000071 RID: 113
		private Hashtable _properties;
	}
}
