using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading;

namespace System.Data.Common
{
	/// <summary>Provides a base class for strongly typed connection string builders.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000341 RID: 833
	public class DbConnectionStringBuilder : IDictionary, ICollection, IEnumerable, ICustomTypeDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> class.</summary>
		// Token: 0x060026FD RID: 9981 RVA: 0x000AE2E8 File Offset: 0x000AC4E8
		public DbConnectionStringBuilder()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> class, optionally using ODBC rules for quoting values.</summary>
		/// <param name="useOdbcRules">true to use {} to delimit fields; false to use quotation marks.</param>
		// Token: 0x060026FE RID: 9982 RVA: 0x000AE312 File Offset: 0x000AC512
		public DbConnectionStringBuilder(bool useOdbcRules)
		{
			this._useOdbcRules = useOdbcRules;
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060026FF RID: 9983 RVA: 0x000AE343 File Offset: 0x000AC543
		private ICollection Collection
		{
			get
			{
				return this.CurrentValues;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06002700 RID: 9984 RVA: 0x000AE343 File Offset: 0x000AC543
		private IDictionary Dictionary
		{
			get
			{
				return this.CurrentValues;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06002701 RID: 9985 RVA: 0x000AE34C File Offset: 0x000AC54C
		private Dictionary<string, object> CurrentValues
		{
			get
			{
				Dictionary<string, object> dictionary = this._currentValues;
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
					this._currentValues = dictionary;
				}
				return dictionary;
			}
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <returns>The element with the specified key.</returns>
		/// <param name="keyword">The key of the element to get or set.</param>
		// Token: 0x170006C2 RID: 1730
		object IDictionary.this[object keyword]
		{
			get
			{
				return this[this.ObjectToString(keyword)];
			}
			set
			{
				this[this.ObjectToString(keyword)] = value;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, trying to get it returns a null reference (Nothing in Visual Basic), and trying to set it creates a new element using the specified key.Passing a null (Nothing in Visual Basic) key throws an <see cref="T:System.ArgumentNullException" />. Assigning a null value removes the key/value pair.</returns>
		/// <param name="keyword">The key of the item to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.NotSupportedException">The property is set, and the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> is read-only. -or-The property is set, <paramref name="keyword" /> does not exist in the collection, and the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> has a fixed size.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170006C3 RID: 1731
		[Browsable(false)]
		public virtual object this[string keyword]
		{
			get
			{
				DataCommonEventSource.Log.Trace<int, string>("<comm.DbConnectionStringBuilder.get_Item|API> {0}, keyword='{1}'", this.ObjectID, keyword);
				ADP.CheckArgumentNull(keyword, "keyword");
				object obj;
				if (this.CurrentValues.TryGetValue(keyword, out obj))
				{
					return obj;
				}
				throw ADP.KeywordNotSupported(keyword);
			}
			set
			{
				ADP.CheckArgumentNull(keyword, "keyword");
				bool flag;
				if (value != null)
				{
					string text = DbConnectionStringBuilderUtil.ConvertToString(value);
					DbConnectionOptions.ValidateKeyValuePair(keyword, text);
					flag = this.CurrentValues.ContainsKey(keyword);
					this.CurrentValues[keyword] = text;
				}
				else
				{
					flag = this.Remove(keyword);
				}
				this._connectionString = null;
				if (flag)
				{
					this._propertyDescriptors = null;
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="P:System.Data.Common.DbConnectionStringBuilder.ConnectionString" /> property is visible in Visual Studio designers.</summary>
		/// <returns>true if the connection string is visible within designers; false otherwise. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06002706 RID: 9990 RVA: 0x000AE440 File Offset: 0x000AC640
		// (set) Token: 0x06002707 RID: 9991 RVA: 0x000AE448 File Offset: 0x000AC648
		[Browsable(false)]
		[DesignOnly(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool BrowsableConnectionString
		{
			get
			{
				return this._browsableConnectionString;
			}
			set
			{
				this._browsableConnectionString = value;
				this._propertyDescriptors = null;
			}
		}

		/// <summary>Gets or sets the connection string associated with the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <returns>The current connection string, created from the key/value pairs that are contained within the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />. The default value is an empty string.</returns>
		/// <exception cref="T:System.ArgumentException">An invalid connection string argument has been supplied.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06002708 RID: 9992 RVA: 0x000AE458 File Offset: 0x000AC658
		// (set) Token: 0x06002709 RID: 9993 RVA: 0x000AE510 File Offset: 0x000AC710
		[RefreshProperties(RefreshProperties.All)]
		public string ConnectionString
		{
			get
			{
				DataCommonEventSource.Log.Trace<int>("<comm.DbConnectionStringBuilder.get_ConnectionString|API> {0}", this.ObjectID);
				string text = this._connectionString;
				if (text == null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (object obj in this.Keys)
					{
						string text2 = (string)obj;
						object obj2;
						if (this.ShouldSerialize(text2) && this.TryGetValue(text2, out obj2))
						{
							string text3 = this.ConvertValueToString(obj2);
							DbConnectionStringBuilder.AppendKeyValuePair(stringBuilder, text2, text3, this._useOdbcRules);
						}
					}
					text = stringBuilder.ToString();
					this._connectionString = text;
				}
				return text;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int>("<comm.DbConnectionStringBuilder.set_ConnectionString|API> {0}", this.ObjectID);
				DbConnectionOptions dbConnectionOptions = new DbConnectionOptions(value, null, this._useOdbcRules);
				string connectionString = this.ConnectionString;
				this.Clear();
				try
				{
					for (NameValuePair nameValuePair = dbConnectionOptions._keyChain; nameValuePair != null; nameValuePair = nameValuePair.Next)
					{
						if (nameValuePair.Value != null)
						{
							this[nameValuePair.Name] = nameValuePair.Value;
						}
						else
						{
							this.Remove(nameValuePair.Name);
						}
					}
					this._connectionString = null;
				}
				catch (ArgumentException)
				{
					this.ConnectionString = connectionString;
					this._connectionString = connectionString;
					throw;
				}
			}
		}

		/// <summary>Gets the current number of keys that are contained within the <see cref="P:System.Data.Common.DbConnectionStringBuilder.ConnectionString" /> property.</summary>
		/// <returns>The number of keys that are contained within the connection string maintained by the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x0600270A RID: 9994 RVA: 0x000AE5B4 File Offset: 0x000AC7B4
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				return this.CurrentValues.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> is read-only; otherwise false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x0600270B RID: 9995 RVA: 0x000061D5 File Offset: 0x000043D5
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> has a fixed size; otherwise false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x0600270C RID: 9996 RVA: 0x000061D5 File Offset: 0x000043D5
		[Browsable(false)]
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x0600270D RID: 9997 RVA: 0x000AE5C1 File Offset: 0x000AC7C1
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.Collection.IsSynchronized;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the keys in the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the keys in the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x0600270E RID: 9998 RVA: 0x000AE5CE File Offset: 0x000AC7CE
		[Browsable(false)]
		public virtual ICollection Keys
		{
			get
			{
				DataCommonEventSource.Log.Trace<int>("<comm.DbConnectionStringBuilder.Keys|API> {0}", this.ObjectID);
				return this.Dictionary.Keys;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x0600270F RID: 9999 RVA: 0x000AE5F0 File Offset: 0x000AC7F0
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06002710 RID: 10000 RVA: 0x000AE5F8 File Offset: 0x000AC7F8
		object ICollection.SyncRoot
		{
			get
			{
				return this.Collection.SyncRoot;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the values in the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the values in the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06002711 RID: 10001 RVA: 0x000AE608 File Offset: 0x000AC808
		[Browsable(false)]
		public virtual ICollection Values
		{
			get
			{
				DataCommonEventSource.Log.Trace<int>("<comm.DbConnectionStringBuilder.Values|API> {0}", this.ObjectID);
				ICollection<string> collection = (ICollection<string>)this.Keys;
				IEnumerator<string> enumerator = collection.GetEnumerator();
				object[] array = new object[collection.Count];
				for (int i = 0; i < array.Length; i++)
				{
					enumerator.MoveNext();
					array[i] = this[enumerator.Current];
				}
				return new ReadOnlyCollection<object>(array);
			}
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x000AE671 File Offset: 0x000AC871
		internal virtual string ConvertValueToString(object value)
		{
			if (value != null)
			{
				return Convert.ToString(value, CultureInfo.InvariantCulture);
			}
			return null;
		}

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <param name="keyword">The <see cref="T:System.Object" /> to use as the key of the element to add.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add.</param>
		// Token: 0x06002713 RID: 10003 RVA: 0x000AE683 File Offset: 0x000AC883
		void IDictionary.Add(object keyword, object value)
		{
			this.Add(this.ObjectToString(keyword), value);
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <param name="keyword">The key to add to the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</param>
		/// <param name="value">The value for the specified key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> is read-only. -or-The <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> has a fixed size.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002714 RID: 10004 RVA: 0x000AE693 File Offset: 0x000AC893
		public void Add(string keyword, object value)
		{
			this[keyword] = value;
		}

		/// <summary>Provides an efficient and safe way to append a key and value to an existing <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <param name="builder">The <see cref="T:System.Text.StringBuilder" /> to which to add the key/value pair.</param>
		/// <param name="keyword">The key to be added.</param>
		/// <param name="value">The value for the supplied key.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002715 RID: 10005 RVA: 0x000AE69D File Offset: 0x000AC89D
		public static void AppendKeyValuePair(StringBuilder builder, string keyword, string value)
		{
			DbConnectionOptions.AppendKeyValuePairBuilder(builder, keyword, value, false);
		}

		/// <summary>Provides an efficient and safe way to append a key and value to an existing <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <param name="builder">The <see cref="T:System.Text.StringBuilder" /> to which to add the key/value pair.</param>
		/// <param name="keyword">The key to be added.</param>
		/// <param name="value">The value for the supplied key.</param>
		/// <param name="useOdbcRules">true to use {} to delimit fields, false to use quotation marks.</param>
		// Token: 0x06002716 RID: 10006 RVA: 0x000AE6A8 File Offset: 0x000AC8A8
		public static void AppendKeyValuePair(StringBuilder builder, string keyword, string value, bool useOdbcRules)
		{
			DbConnectionOptions.AppendKeyValuePairBuilder(builder, keyword, value, useOdbcRules);
		}

		/// <summary>Clears the contents of the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> is read-only.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002717 RID: 10007 RVA: 0x000AE6B3 File Offset: 0x000AC8B3
		public virtual void Clear()
		{
			DataCommonEventSource.Log.Trace("<comm.DbConnectionStringBuilder.Clear|API>");
			this._connectionString = string.Empty;
			this._propertyDescriptors = null;
			this.CurrentValues.Clear();
		}

		/// <summary>Clears the collection of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects on the associated <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		// Token: 0x06002718 RID: 10008 RVA: 0x000AE6E1 File Offset: 0x000AC8E1
		protected internal void ClearPropertyDescriptors()
		{
			this._propertyDescriptors = null;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> object contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, false.</returns>
		/// <param name="keyword">The key to locate in the <see cref="T:System.Collections.IDictionary" /> object.</param>
		// Token: 0x06002719 RID: 10009 RVA: 0x000AE6EA File Offset: 0x000AC8EA
		bool IDictionary.Contains(object keyword)
		{
			return this.ContainsKey(this.ObjectToString(keyword));
		}

		/// <summary>Determines whether the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> contains an entry with the specified key; otherwise false.</returns>
		/// <param name="keyword">The key to locate in the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600271A RID: 10010 RVA: 0x000AE6F9 File Offset: 0x000AC8F9
		public virtual bool ContainsKey(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			return this.CurrentValues.ContainsKey(keyword);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x0600271B RID: 10011 RVA: 0x000AE712 File Offset: 0x000AC912
		void ICollection.CopyTo(Array array, int index)
		{
			DataCommonEventSource.Log.Trace<int>("<comm.DbConnectionStringBuilder.ICollection.CopyTo|API> {0}", this.ObjectID);
			this.Collection.CopyTo(array, index);
		}

		/// <summary>Compares the connection information in this <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> object with the connection information in the supplied object.</summary>
		/// <returns>true if the connection information in both of the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> objects causes an equivalent connection string; otherwise false.</returns>
		/// <param name="connectionStringBuilder">The <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> to be compared with this <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> object.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600271C RID: 10012 RVA: 0x000AE738 File Offset: 0x000AC938
		public virtual bool EquivalentTo(DbConnectionStringBuilder connectionStringBuilder)
		{
			ADP.CheckArgumentNull(connectionStringBuilder, "connectionStringBuilder");
			DataCommonEventSource.Log.Trace<int, int>("<comm.DbConnectionStringBuilder.EquivalentTo|API> {0}, connectionStringBuilder={1}", this.ObjectID, connectionStringBuilder.ObjectID);
			if (base.GetType() != connectionStringBuilder.GetType() || this.CurrentValues.Count != connectionStringBuilder.CurrentValues.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, object> keyValuePair in this.CurrentValues)
			{
				object obj;
				if (!connectionStringBuilder.CurrentValues.TryGetValue(keyValuePair.Key, out obj) || !keyValuePair.Value.Equals(obj))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x0600271D RID: 10013 RVA: 0x000AE804 File Offset: 0x000ACA04
		IEnumerator IEnumerable.GetEnumerator()
		{
			DataCommonEventSource.Log.Trace<int>("<comm.DbConnectionStringBuilder.IEnumerable.GetEnumerator|API> {0}", this.ObjectID);
			return this.Collection.GetEnumerator();
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.IDictionary" /> object.</returns>
		// Token: 0x0600271E RID: 10014 RVA: 0x000AE826 File Offset: 0x000ACA26
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			DataCommonEventSource.Log.Trace<int>("<comm.DbConnectionStringBuilder.IDictionary.GetEnumerator|API> {0}", this.ObjectID);
			return this.Dictionary.GetEnumerator();
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x000AE848 File Offset: 0x000ACA48
		private string ObjectToString(object keyword)
		{
			string text;
			try
			{
				text = (string)keyword;
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException("not a string", "keyword");
			}
			return text;
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <param name="keyword">The key of the element to remove.</param>
		// Token: 0x06002720 RID: 10016 RVA: 0x000AE880 File Offset: 0x000ACA80
		void IDictionary.Remove(object keyword)
		{
			this.Remove(this.ObjectToString(keyword));
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</summary>
		/// <returns>true if the key existed within the connection string and was removed; false if the key did not exist.</returns>
		/// <param name="keyword">The key of the key/value pair to be removed from the connection string in this <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is null (Nothing in Visual Basic)</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> is read-only, or the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> has a fixed size.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002721 RID: 10017 RVA: 0x000AE890 File Offset: 0x000ACA90
		public virtual bool Remove(string keyword)
		{
			DataCommonEventSource.Log.Trace<int, string>("<comm.DbConnectionStringBuilder.Remove|API> {0}, keyword='{1}'", this.ObjectID, keyword);
			ADP.CheckArgumentNull(keyword, "keyword");
			if (this.CurrentValues.Remove(keyword))
			{
				this._connectionString = null;
				this._propertyDescriptors = null;
				return true;
			}
			return false;
		}

		/// <summary>Indicates whether the specified key exists in this <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</summary>
		/// <returns>true if the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> contains an entry with the specified key; otherwise false.</returns>
		/// <param name="keyword">The key to locate in the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002722 RID: 10018 RVA: 0x000AE6F9 File Offset: 0x000AC8F9
		public virtual bool ShouldSerialize(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			return this.CurrentValues.ContainsKey(keyword);
		}

		/// <summary>Returns the connection string associated with this <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <returns>The current <see cref="P:System.Data.Common.DbConnectionStringBuilder.ConnectionString" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002723 RID: 10019 RVA: 0x000AE8DD File Offset: 0x000ACADD
		public override string ToString()
		{
			return this.ConnectionString;
		}

		/// <summary>Retrieves a value corresponding to the supplied key from this <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <returns>true if <paramref name="keyword" /> was found within the connection string, false otherwise.</returns>
		/// <param name="keyword">The key of the item to retrieve.</param>
		/// <param name="value">The value corresponding to the <paramref name="key" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> contains a null value (Nothing in Visual Basic).</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002724 RID: 10020 RVA: 0x000AE8E5 File Offset: 0x000ACAE5
		public virtual bool TryGetValue(string keyword, out object value)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			return this.CurrentValues.TryGetValue(keyword, out value);
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x000AE900 File Offset: 0x000ACB00
		internal Attribute[] GetAttributesFromCollection(AttributeCollection collection)
		{
			Attribute[] array = new Attribute[collection.Count];
			collection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x000AE924 File Offset: 0x000ACB24
		private PropertyDescriptorCollection GetProperties()
		{
			PropertyDescriptorCollection propertyDescriptorCollection = this._propertyDescriptors;
			if (propertyDescriptorCollection == null)
			{
				long num = DataCommonEventSource.Log.EnterScope<int>("<comm.DbConnectionStringBuilder.GetProperties|INFO> {0}", this.ObjectID);
				try
				{
					Hashtable hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
					this.GetProperties(hashtable);
					PropertyDescriptor[] array = new PropertyDescriptor[hashtable.Count];
					hashtable.Values.CopyTo(array, 0);
					propertyDescriptorCollection = new PropertyDescriptorCollection(array);
					this._propertyDescriptors = propertyDescriptorCollection;
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
			return propertyDescriptorCollection;
		}

		/// <summary>Fills a supplied <see cref="T:System.Collections.Hashtable" /> with information about all the properties of this <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <param name="propertyDescriptors">The <see cref="T:System.Collections.Hashtable" /> to be filled with information about this <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</param>
		// Token: 0x06002727 RID: 10023 RVA: 0x000AE9AC File Offset: 0x000ACBAC
		protected virtual void GetProperties(Hashtable propertyDescriptors)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<comm.DbConnectionStringBuilder.GetProperties|API> {0}", this.ObjectID);
			try
			{
				foreach (object obj in TypeDescriptor.GetProperties(this, true))
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
					if ("ConnectionString" != propertyDescriptor.Name)
					{
						string displayName = propertyDescriptor.DisplayName;
						if (!propertyDescriptors.ContainsKey(displayName))
						{
							Attribute[] array = this.GetAttributesFromCollection(propertyDescriptor.Attributes);
							PropertyDescriptor propertyDescriptor2 = new DbConnectionStringBuilderDescriptor(propertyDescriptor.Name, propertyDescriptor.ComponentType, propertyDescriptor.PropertyType, propertyDescriptor.IsReadOnly, array);
							propertyDescriptors[displayName] = propertyDescriptor2;
						}
					}
					else if (this.BrowsableConnectionString)
					{
						propertyDescriptors["ConnectionString"] = propertyDescriptor;
					}
					else
					{
						propertyDescriptors.Remove("ConnectionString");
					}
				}
				if (!this.IsFixedSize)
				{
					Attribute[] array = null;
					foreach (object obj2 in this.Keys)
					{
						string text = (string)obj2;
						if (!propertyDescriptors.ContainsKey(text))
						{
							object obj3 = this[text];
							Type type;
							if (obj3 != null)
							{
								type = obj3.GetType();
								if (typeof(string) == type)
								{
									int num2;
									bool flag;
									if (int.TryParse((string)obj3, out num2))
									{
										type = typeof(int);
									}
									else if (bool.TryParse((string)obj3, out flag))
									{
										type = typeof(bool);
									}
								}
							}
							else
							{
								type = typeof(string);
							}
							Attribute[] array2 = array;
							if (StringComparer.OrdinalIgnoreCase.Equals("Password", text) || StringComparer.OrdinalIgnoreCase.Equals("pwd", text))
							{
								array2 = new Attribute[]
								{
									BrowsableAttribute.Yes,
									PasswordPropertyTextAttribute.Yes,
									RefreshPropertiesAttribute.All
								};
							}
							else if (array == null)
							{
								array = new Attribute[]
								{
									BrowsableAttribute.Yes,
									RefreshPropertiesAttribute.All
								};
								array2 = array;
							}
							PropertyDescriptor propertyDescriptor3 = new DbConnectionStringBuilderDescriptor(text, base.GetType(), type, false, array2);
							propertyDescriptors[text] = propertyDescriptor3;
						}
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x000AEC38 File Offset: 0x000ACE38
		private PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = this.GetProperties();
			if (attributes == null || attributes.Length == 0)
			{
				return properties;
			}
			PropertyDescriptor[] array = new PropertyDescriptor[properties.Count];
			int num = 0;
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				bool flag = true;
				foreach (Attribute attribute in attributes)
				{
					Attribute attribute2 = propertyDescriptor.Attributes[attribute.GetType()];
					if ((attribute2 == null && !attribute.IsDefaultAttribute()) || !attribute2.Match(attribute))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					array[num] = propertyDescriptor;
					num++;
				}
			}
			PropertyDescriptor[] array2 = new PropertyDescriptor[num];
			Array.Copy(array, array2, num);
			return new PropertyDescriptorCollection(array2);
		}

		/// <summary>Returns the class name of this instance of a component.</summary>
		/// <returns>The class name of the object, or null if the class does not have a name.</returns>
		// Token: 0x06002729 RID: 10025 RVA: 0x000AED20 File Offset: 0x000ACF20
		string ICustomTypeDescriptor.GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		/// <summary>Returns the name of this instance of a component.</summary>
		/// <returns>The name of the object, or null if the object does not have a name.</returns>
		// Token: 0x0600272A RID: 10026 RVA: 0x000AED29 File Offset: 0x000ACF29
		string ICustomTypeDescriptor.GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		/// <summary>Returns a collection of custom attributes for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for this object.</returns>
		// Token: 0x0600272B RID: 10027 RVA: 0x000AED32 File Offset: 0x000ACF32
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		/// <summary>Returns an editor of the specified type for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.Object" /> of the specified type that is the editor for this object, or null if the editor cannot be found.</returns>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the editor for this object.</param>
		// Token: 0x0600272C RID: 10028 RVA: 0x000AED3B File Offset: 0x000ACF3B
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		/// <summary>Returns a type converter for this instance of a component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> that is the converter for this object, or null if there is no <see cref="T:System.ComponentModel.TypeConverter" /> for this object.</returns>
		// Token: 0x0600272D RID: 10029 RVA: 0x000AED45 File Offset: 0x000ACF45
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		/// <summary>Returns the default property for this instance of a component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the default property for this object, or null if this object does not have properties.</returns>
		// Token: 0x0600272E RID: 10030 RVA: 0x000AED4E File Offset: 0x000ACF4E
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		/// <summary>Returns the properties for this instance of a component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties for this component instance.</returns>
		// Token: 0x0600272F RID: 10031 RVA: 0x000AED57 File Offset: 0x000ACF57
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return this.GetProperties();
		}

		/// <summary>Returns the properties for this instance of a component using the attribute array as a filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the filtered properties for this component instance.</returns>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		// Token: 0x06002730 RID: 10032 RVA: 0x000AED5F File Offset: 0x000ACF5F
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			return this.GetProperties(attributes);
		}

		/// <summary>Returns the default event for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> that represents the default event for this object, or null if this object does not have events.</returns>
		// Token: 0x06002731 RID: 10033 RVA: 0x000AED68 File Offset: 0x000ACF68
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		/// <summary>Returns the events for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the events for this component instance.</returns>
		// Token: 0x06002732 RID: 10034 RVA: 0x000AED71 File Offset: 0x000ACF71
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		/// <summary>Returns the events for this instance of a component using the specified attribute array as a filter.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the filtered events for this component instance.</returns>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		// Token: 0x06002733 RID: 10035 RVA: 0x000AED7A File Offset: 0x000ACF7A
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		/// <summary>Returns an object that contains the property described by the specified property descriptor.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the owner of the specified property.</returns>
		/// <param name="pd">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property whose owner is to be found.</param>
		// Token: 0x06002734 RID: 10036 RVA: 0x00005D82 File Offset: 0x00003F82
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x040018B1 RID: 6321
		private Dictionary<string, object> _currentValues;

		// Token: 0x040018B2 RID: 6322
		private string _connectionString = string.Empty;

		// Token: 0x040018B3 RID: 6323
		private PropertyDescriptorCollection _propertyDescriptors;

		// Token: 0x040018B4 RID: 6324
		private bool _browsableConnectionString = true;

		// Token: 0x040018B5 RID: 6325
		private readonly bool _useOdbcRules;

		// Token: 0x040018B6 RID: 6326
		private static int s_objectTypeCount;

		// Token: 0x040018B7 RID: 6327
		internal readonly int _objectID = Interlocked.Increment(ref DbConnectionStringBuilder.s_objectTypeCount);
	}
}
