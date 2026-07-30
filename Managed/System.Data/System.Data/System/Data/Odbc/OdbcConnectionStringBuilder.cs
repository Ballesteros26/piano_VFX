using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.Odbc
{
	/// <summary>Provides a simple way to create and manage the contents of connection strings used by the <see cref="T:System.Data.Odbc.OdbcConnection" /> class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000294 RID: 660
	public sealed class OdbcConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x06001BE4 RID: 7140 RVA: 0x0008A2D8 File Offset: 0x000884D8
		static OdbcConnectionStringBuilder()
		{
			string[] array = new string[] { null, "Driver" };
			array[0] = "Dsn";
			OdbcConnectionStringBuilder.s_validKeywords = array;
			OdbcConnectionStringBuilder.s_keywords = new Dictionary<string, OdbcConnectionStringBuilder.Keywords>(2, StringComparer.OrdinalIgnoreCase)
			{
				{
					"Driver",
					OdbcConnectionStringBuilder.Keywords.Driver
				},
				{
					"Dsn",
					OdbcConnectionStringBuilder.Keywords.Dsn
				}
			};
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" /> class.</summary>
		// Token: 0x06001BE5 RID: 7141 RVA: 0x0008A328 File Offset: 0x00088528
		public OdbcConnectionStringBuilder()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" /> class. The provided connection string provides the data for the instance's internal connection information.</summary>
		/// <param name="connectionString">The basis for the object's internal connection information. Parsed into key/value pairs.</param>
		/// <exception cref="T:System.ArgumentException">The connection string is incorrectly formatted (perhaps missing the required "=" within a key/value pair).</exception>
		// Token: 0x06001BE6 RID: 7142 RVA: 0x0008A331 File Offset: 0x00088531
		public OdbcConnectionStringBuilder(string connectionString)
			: base(true)
		{
			if (!string.IsNullOrEmpty(connectionString))
			{
				base.ConnectionString = connectionString;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key. In C#, this property is the indexer.</summary>
		/// <returns>The value associated with the specified key.</returns>
		/// <param name="keyword">The key of the item to get or set.</param>
		/// <exception cref="T:System.ArgumentException">The connection string is incorrectly formatted (perhaps missing the required "=" within a key/value pair).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700051D RID: 1309
		public override object this[string keyword]
		{
			get
			{
				ADP.CheckArgumentNull(keyword, "keyword");
				OdbcConnectionStringBuilder.Keywords keywords;
				if (OdbcConnectionStringBuilder.s_keywords.TryGetValue(keyword, out keywords))
				{
					return this.GetAt(keywords);
				}
				return base[keyword];
			}
			set
			{
				ADP.CheckArgumentNull(keyword, "keyword");
				if (value == null)
				{
					this.Remove(keyword);
					return;
				}
				OdbcConnectionStringBuilder.Keywords keywords;
				if (!OdbcConnectionStringBuilder.s_keywords.TryGetValue(keyword, out keywords))
				{
					base[keyword] = value;
					base.ClearPropertyDescriptors();
					this._knownKeywords = null;
					return;
				}
				if (keywords == OdbcConnectionStringBuilder.Keywords.Dsn)
				{
					this.Dsn = OdbcConnectionStringBuilder.ConvertToString(value);
					return;
				}
				if (keywords == OdbcConnectionStringBuilder.Keywords.Driver)
				{
					this.Driver = OdbcConnectionStringBuilder.ConvertToString(value);
					return;
				}
				throw ADP.KeywordNotSupported(keyword);
			}
		}

		/// <summary>Gets or sets the name of the ODBC driver associated with the connection.</summary>
		/// <returns>The value of the <see cref="P:System.Data.Odbc.OdbcConnectionStringBuilder.Driver" /> property, or String.Empty if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x0008A408 File Offset: 0x00088608
		// (set) Token: 0x06001BEA RID: 7146 RVA: 0x0008A410 File Offset: 0x00088610
		[DisplayName("Driver")]
		public string Driver
		{
			get
			{
				return this._driver;
			}
			set
			{
				this.SetValue("Driver", value);
				this._driver = value;
			}
		}

		/// <summary>Gets or sets the name of the data source name (DSN) associated with the connection.</summary>
		/// <returns>The value of the <see cref="P:System.Data.Odbc.OdbcConnectionStringBuilder.Dsn" /> property, or String.Empty if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x0008A425 File Offset: 0x00088625
		// (set) Token: 0x06001BEC RID: 7148 RVA: 0x0008A42D File Offset: 0x0008862D
		[DisplayName("Dsn")]
		public string Dsn
		{
			get
			{
				return this._dsn;
			}
			set
			{
				this.SetValue("Dsn", value);
				this._dsn = value;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the keys in the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the keys in the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x0008A444 File Offset: 0x00088644
		public override ICollection Keys
		{
			get
			{
				string[] array = this._knownKeywords;
				if (array == null)
				{
					array = OdbcConnectionStringBuilder.s_validKeywords;
					int num = 0;
					foreach (object obj in base.Keys)
					{
						string text = (string)obj;
						bool flag = true;
						string[] array2 = array;
						for (int i = 0; i < array2.Length; i++)
						{
							if (array2[i] == text)
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							num++;
						}
					}
					if (0 < num)
					{
						string[] array3 = new string[array.Length + num];
						array.CopyTo(array3, 0);
						int num2 = array.Length;
						foreach (object obj2 in base.Keys)
						{
							string text2 = (string)obj2;
							bool flag2 = true;
							string[] array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								if (array2[i] == text2)
								{
									flag2 = false;
									break;
								}
							}
							if (flag2)
							{
								array3[num2++] = text2;
							}
						}
						array = array3;
					}
					this._knownKeywords = array;
				}
				return new ReadOnlyCollection<string>(array);
			}
		}

		/// <summary>Clears the contents of the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" /> instance.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001BEE RID: 7150 RVA: 0x0008A594 File Offset: 0x00088794
		public override void Clear()
		{
			base.Clear();
			for (int i = 0; i < OdbcConnectionStringBuilder.s_validKeywords.Length; i++)
			{
				this.Reset((OdbcConnectionStringBuilder.Keywords)i);
			}
			this._knownKeywords = OdbcConnectionStringBuilder.s_validKeywords;
		}

		/// <summary>Determines whether the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" /> contains an element that has the specified key; otherwise false.</returns>
		/// <param name="keyword">The key to locate in the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is null (Nothing in Visual Basic).</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BEF RID: 7151 RVA: 0x0008A5CB File Offset: 0x000887CB
		public override bool ContainsKey(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			return OdbcConnectionStringBuilder.s_keywords.ContainsKey(keyword) || base.ContainsKey(keyword);
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x0005F1FC File Offset: 0x0005D3FC
		private static string ConvertToString(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToString(value);
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x0008A5EE File Offset: 0x000887EE
		private object GetAt(OdbcConnectionStringBuilder.Keywords index)
		{
			if (index == OdbcConnectionStringBuilder.Keywords.Dsn)
			{
				return this.Dsn;
			}
			if (index == OdbcConnectionStringBuilder.Keywords.Driver)
			{
				return this.Driver;
			}
			throw ADP.KeywordNotSupported(OdbcConnectionStringBuilder.s_validKeywords[(int)index]);
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" /> instance.</summary>
		/// <returns>true if the key existed within the connection string and was removed; false if the key did not exist.</returns>
		/// <param name="keyword">The key of the key/value pair to be removed from the connection string in this <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is null (Nothing in Visual Basic).</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BF2 RID: 7154 RVA: 0x0008A614 File Offset: 0x00088814
		public override bool Remove(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			if (base.Remove(keyword))
			{
				OdbcConnectionStringBuilder.Keywords keywords;
				if (OdbcConnectionStringBuilder.s_keywords.TryGetValue(keyword, out keywords))
				{
					this.Reset(keywords);
				}
				else
				{
					base.ClearPropertyDescriptors();
					this._knownKeywords = null;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x0008A65D File Offset: 0x0008885D
		private void Reset(OdbcConnectionStringBuilder.Keywords index)
		{
			if (index == OdbcConnectionStringBuilder.Keywords.Dsn)
			{
				this._dsn = "";
				return;
			}
			if (index == OdbcConnectionStringBuilder.Keywords.Driver)
			{
				this._driver = "";
				return;
			}
			throw ADP.KeywordNotSupported(OdbcConnectionStringBuilder.s_validKeywords[(int)index]);
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x0005F615 File Offset: 0x0005D815
		private void SetValue(string keyword, string value)
		{
			ADP.CheckArgumentNull(value, keyword);
			base[keyword] = value;
		}

		/// <summary>Retrieves a value corresponding to the supplied key from this <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" />.</summary>
		/// <returns>true if <paramref name="keyword" /> was found within the connection string; otherwise false.</returns>
		/// <param name="keyword">The key of the item to retrieve.</param>
		/// <param name="value">The value corresponding to <paramref name="keyword." /></param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001BF5 RID: 7157 RVA: 0x0008A68C File Offset: 0x0008888C
		public override bool TryGetValue(string keyword, out object value)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			OdbcConnectionStringBuilder.Keywords keywords;
			if (OdbcConnectionStringBuilder.s_keywords.TryGetValue(keyword, out keywords))
			{
				value = this.GetAt(keywords);
				return true;
			}
			return base.TryGetValue(keyword, out value);
		}

		// Token: 0x040014FD RID: 5373
		private static readonly string[] s_validKeywords;

		// Token: 0x040014FE RID: 5374
		private static readonly Dictionary<string, OdbcConnectionStringBuilder.Keywords> s_keywords;

		// Token: 0x040014FF RID: 5375
		private string[] _knownKeywords;

		// Token: 0x04001500 RID: 5376
		private string _dsn = "";

		// Token: 0x04001501 RID: 5377
		private string _driver = "";

		// Token: 0x02000295 RID: 661
		private enum Keywords
		{
			// Token: 0x04001503 RID: 5379
			Dsn,
			// Token: 0x04001504 RID: 5380
			Driver
		}
	}
}
