using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Common
{
	/// <summary>Enables a .NET Framework data provider to help ensure that a user has a security level adequate for accessing data.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000386 RID: 902
	[SecurityPermission(SecurityAction.InheritanceDemand, ControlEvidence = true, ControlPolicy = true)]
	[Serializable]
	public abstract class DBDataPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of a DBDataPermission class.</summary>
		// Token: 0x06002AAB RID: 10923 RVA: 0x000BD34C File Offset: 0x000BB54C
		[Obsolete("DBDataPermission() has been deprecated.  Use the DBDataPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		protected DBDataPermission()
			: this(PermissionState.None)
		{
		}

		/// <summary>Initializes a new instance of a DBDataPermission class with the specified <see cref="T:System.Security.Permissions.PermissionState" /> value.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		// Token: 0x06002AAC RID: 10924 RVA: 0x000BD355 File Offset: 0x000BB555
		protected DBDataPermission(PermissionState state)
		{
			this._keyvaluetree = NameValuePermission.Default;
			base..ctor();
			if (state == PermissionState.Unrestricted)
			{
				this._isUnrestricted = true;
				return;
			}
			if (state == PermissionState.None)
			{
				this._isUnrestricted = false;
				return;
			}
			throw ADP.InvalidPermissionState(state);
		}

		/// <summary>Initializes a new instance of a DBDataPermission class with the specified <see cref="T:System.Security.Permissions.PermissionState" /> value, and a value indicating whether a blank password is allowed.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <param name="allowBlankPassword">Indicates whether a blank password is allowed. </param>
		// Token: 0x06002AAD RID: 10925 RVA: 0x000BD385 File Offset: 0x000BB585
		[Obsolete("DBDataPermission(PermissionState state,Boolean allowBlankPassword) has been deprecated.  Use the DBDataPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		protected DBDataPermission(PermissionState state, bool allowBlankPassword)
			: this(state)
		{
			this.AllowBlankPassword = allowBlankPassword;
		}

		/// <summary>Initializes a new instance of a DBDataPermission class using an existing DBDataPermission.</summary>
		/// <param name="permission">An existing DBDataPermission used to create a new DBDataPermission. </param>
		// Token: 0x06002AAE RID: 10926 RVA: 0x000BD395 File Offset: 0x000BB595
		protected DBDataPermission(DBDataPermission permission)
		{
			this._keyvaluetree = NameValuePermission.Default;
			base..ctor();
			if (permission == null)
			{
				throw ADP.ArgumentNull("permissionAttribute");
			}
			this.CopyFrom(permission);
		}

		/// <summary>Initializes a new instance of a DBDataPermission class with the specified DBDataPermissionAttribute.</summary>
		/// <param name="permissionAttribute">A security action associated with a custom security attribute. </param>
		// Token: 0x06002AAF RID: 10927 RVA: 0x000BD3C0 File Offset: 0x000BB5C0
		protected DBDataPermission(DBDataPermissionAttribute permissionAttribute)
		{
			this._keyvaluetree = NameValuePermission.Default;
			base..ctor();
			if (permissionAttribute == null)
			{
				throw ADP.ArgumentNull("permissionAttribute");
			}
			this._isUnrestricted = permissionAttribute.Unrestricted;
			if (!this._isUnrestricted)
			{
				this._allowBlankPassword = permissionAttribute.AllowBlankPassword;
				if (permissionAttribute.ShouldSerializeConnectionString() || permissionAttribute.ShouldSerializeKeyRestrictions())
				{
					this.Add(permissionAttribute.ConnectionString, permissionAttribute.KeyRestrictions, permissionAttribute.KeyRestrictionBehavior);
				}
			}
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x000BD434 File Offset: 0x000BB634
		internal DBDataPermission(DbConnectionOptions connectionOptions)
		{
			this._keyvaluetree = NameValuePermission.Default;
			base..ctor();
			if (connectionOptions != null)
			{
				this._allowBlankPassword = connectionOptions.HasBlankPassword;
				this.AddPermissionEntry(new DBConnectionString(connectionOptions));
			}
		}

		/// <summary>Gets a value indicating whether a blank password is allowed.</summary>
		/// <returns>true if a blank password is allowed, otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06002AB1 RID: 10929 RVA: 0x000BD462 File Offset: 0x000BB662
		// (set) Token: 0x06002AB2 RID: 10930 RVA: 0x000BD46A File Offset: 0x000BB66A
		public bool AllowBlankPassword
		{
			get
			{
				return this._allowBlankPassword;
			}
			set
			{
				this._allowBlankPassword = value;
			}
		}

		/// <summary>Adds access for the specified connection string to the existing state of the DBDataPermission. </summary>
		/// <param name="connectionString">A permitted connection string.</param>
		/// <param name="restrictions">String that identifies connection string parameters that are allowed or disallowed.</param>
		/// <param name="behavior">One of the <see cref="T:System.Data.KeyRestrictionBehavior" /> properties.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002AB3 RID: 10931 RVA: 0x000BD474 File Offset: 0x000BB674
		public virtual void Add(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
		{
			DBConnectionString dbconnectionString = new DBConnectionString(connectionString, restrictions, behavior, null, false);
			this.AddPermissionEntry(dbconnectionString);
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x000BD494 File Offset: 0x000BB694
		internal void AddPermissionEntry(DBConnectionString entry)
		{
			if (this._keyvaluetree == null)
			{
				this._keyvaluetree = new NameValuePermission();
			}
			if (this._keyvalues == null)
			{
				this._keyvalues = new ArrayList();
			}
			NameValuePermission.AddEntry(this._keyvaluetree, this._keyvalues, entry);
			this._isUnrestricted = false;
		}

		/// <summary>Removes all permissions that were previous added using the <see cref="M:System.Data.Common.DBDataPermission.Add(System.String,System.String,System.Data.KeyRestrictionBehavior)" /> method.</summary>
		// Token: 0x06002AB5 RID: 10933 RVA: 0x000BD4E0 File Offset: 0x000BB6E0
		protected void Clear()
		{
			this._keyvaluetree = null;
			this._keyvalues = null;
		}

		/// <summary>Creates and returns an identical copy of the current permission object.</summary>
		/// <returns>A copy of the current permission object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002AB6 RID: 10934 RVA: 0x000BD4F0 File Offset: 0x000BB6F0
		public override IPermission Copy()
		{
			DBDataPermission dbdataPermission = this.CreateInstance();
			dbdataPermission.CopyFrom(this);
			return dbdataPermission;
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x000BD500 File Offset: 0x000BB700
		private void CopyFrom(DBDataPermission permission)
		{
			this._isUnrestricted = permission.IsUnrestricted();
			if (!this._isUnrestricted)
			{
				this._allowBlankPassword = permission.AllowBlankPassword;
				if (permission._keyvalues != null)
				{
					this._keyvalues = (ArrayList)permission._keyvalues.Clone();
					if (permission._keyvaluetree != null)
					{
						this._keyvaluetree = permission._keyvaluetree.CopyNameValue();
					}
				}
			}
		}

		/// <summary>Creates a new instance of the DBDataPermission class.</summary>
		/// <returns>A new DBDataPermission object.</returns>
		// Token: 0x06002AB8 RID: 10936 RVA: 0x000BD564 File Offset: 0x000BB764
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		protected virtual DBDataPermission CreateInstance()
		{
			return Activator.CreateInstance(base.GetType(), BindingFlags.Instance | BindingFlags.Public, null, null, CultureInfo.InvariantCulture, null) as DBDataPermission;
		}

		/// <summary>Returns a new permission object representing the intersection of the current permission object and the specified permission object.</summary>
		/// <returns>A new permission object that represents the intersection of the current permission object and the specified permission object. This new permission object is a null reference (Nothing in Visual Basic) if the intersection is empty.</returns>
		/// <param name="target">A permission object to intersect with the current permission object. It must be of the same type as the current permission object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not a null reference (Nothing in Visual Basic) and is not an instance of the same class as the current permission object. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002AB9 RID: 10937 RVA: 0x000BD580 File Offset: 0x000BB780
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (target.GetType() != base.GetType())
			{
				throw ADP.PermissionTypeMismatch();
			}
			if (this.IsUnrestricted())
			{
				return target.Copy();
			}
			DBDataPermission dbdataPermission = (DBDataPermission)target;
			if (dbdataPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			DBDataPermission dbdataPermission2 = (DBDataPermission)dbdataPermission.Copy();
			dbdataPermission2._allowBlankPassword &= this.AllowBlankPassword;
			if (this._keyvalues != null && dbdataPermission2._keyvalues != null)
			{
				dbdataPermission2._keyvalues.Clear();
				dbdataPermission2._keyvaluetree.Intersect(dbdataPermission2._keyvalues, this._keyvaluetree);
			}
			else
			{
				dbdataPermission2._keyvalues = null;
				dbdataPermission2._keyvaluetree = null;
			}
			if (dbdataPermission2.IsEmpty())
			{
				dbdataPermission2 = null;
			}
			return dbdataPermission2;
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x000BD63C File Offset: 0x000BB83C
		private bool IsEmpty()
		{
			ArrayList keyvalues = this._keyvalues;
			return !this.IsUnrestricted() && !this.AllowBlankPassword && (keyvalues == null || keyvalues.Count == 0);
		}

		/// <summary>Returns a value indicating whether the current permission object is a subset of the specified permission object.</summary>
		/// <returns>true if the current permission object is a subset of the specified permission object, otherwise false.</returns>
		/// <param name="target">A permission object that is to be tested for the subset relationship. This object must be of the same type as the current permission object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is an object that is not of the same type as the current permission object. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002ABB RID: 10939 RVA: 0x000BD670 File Offset: 0x000BB870
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.IsEmpty();
			}
			if (target.GetType() != base.GetType())
			{
				throw ADP.PermissionTypeMismatch();
			}
			DBDataPermission dbdataPermission = target as DBDataPermission;
			bool flag = dbdataPermission.IsUnrestricted();
			if (!flag && !this.IsUnrestricted() && (!this.AllowBlankPassword || dbdataPermission.AllowBlankPassword) && (this._keyvalues == null || dbdataPermission._keyvaluetree != null))
			{
				flag = true;
				if (this._keyvalues != null)
				{
					foreach (object obj in this._keyvalues)
					{
						DBConnectionString dbconnectionString = (DBConnectionString)obj;
						if (!dbdataPermission._keyvaluetree.CheckValueForKeyPermit(dbconnectionString))
						{
							flag = false;
							break;
						}
					}
				}
			}
			return flag;
		}

		/// <summary>Returns a value indicating whether the permission can be represented as unrestricted without any knowledge of the permission semantics.</summary>
		/// <returns>true if the permission can be represented as unrestricted.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002ABC RID: 10940 RVA: 0x000BD740 File Offset: 0x000BB940
		public bool IsUnrestricted()
		{
			return this._isUnrestricted;
		}

		/// <summary>Returns a new permission object that is the union of the current and specified permission objects.</summary>
		/// <returns>A new permission object that represents the union of the current permission object and the specified permission object.</returns>
		/// <param name="target">A permission object to combine with the current permission object. It must be of the same type as the current permission object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> object is not the same type as the current permission object.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002ABD RID: 10941 RVA: 0x000BD748 File Offset: 0x000BB948
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (target.GetType() != base.GetType())
			{
				throw ADP.PermissionTypeMismatch();
			}
			if (this.IsUnrestricted())
			{
				return this.Copy();
			}
			DBDataPermission dbdataPermission = (DBDataPermission)target.Copy();
			if (!dbdataPermission.IsUnrestricted())
			{
				dbdataPermission._allowBlankPassword |= this.AllowBlankPassword;
				if (this._keyvalues != null)
				{
					foreach (object obj in this._keyvalues)
					{
						DBConnectionString dbconnectionString = (DBConnectionString)obj;
						dbdataPermission.AddPermissionEntry(dbconnectionString);
					}
				}
			}
			if (!dbdataPermission.IsEmpty())
			{
				return dbdataPermission;
			}
			return null;
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x000BD810 File Offset: 0x000BBA10
		private string DecodeXmlValue(string value)
		{
			if (value != null && 0 < value.Length)
			{
				value = value.Replace("&quot;", "\"");
				value = value.Replace("&apos;", "'");
				value = value.Replace("&lt;", "<");
				value = value.Replace("&gt;", ">");
				value = value.Replace("&amp;", "&");
			}
			return value;
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x000BD884 File Offset: 0x000BBA84
		private string EncodeXmlValue(string value)
		{
			if (value != null && 0 < value.Length)
			{
				value = value.Replace('\0', ' ');
				value = value.Trim();
				value = value.Replace("&", "&amp;");
				value = value.Replace(">", "&gt;");
				value = value.Replace("<", "&lt;");
				value = value.Replace("'", "&apos;");
				value = value.Replace("\"", "&quot;");
			}
			return value;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding to use to reconstruct the security object. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002AC0 RID: 10944 RVA: 0x000BD90C File Offset: 0x000BBB0C
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw ADP.ArgumentNull("securityElement");
			}
			string text = securityElement.Tag;
			if (!text.Equals("Permission") && !text.Equals("IPermission"))
			{
				throw ADP.NotAPermissionElement();
			}
			string text2 = securityElement.Attribute("version");
			if (text2 != null && !text2.Equals("1"))
			{
				throw ADP.InvalidXMLBadVersion();
			}
			string text3 = securityElement.Attribute("Unrestricted");
			this._isUnrestricted = text3 != null && bool.Parse(text3);
			this.Clear();
			if (!this._isUnrestricted)
			{
				string text4 = securityElement.Attribute("AllowBlankPassword");
				this._allowBlankPassword = text4 != null && bool.Parse(text4);
				ArrayList children = securityElement.Children;
				if (children == null)
				{
					return;
				}
				using (IEnumerator enumerator = children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						SecurityElement securityElement2 = (SecurityElement)obj;
						text = securityElement2.Tag;
						if ("add" == text || (text != null && "add" == text.ToLower(CultureInfo.InvariantCulture)))
						{
							string text5 = securityElement2.Attribute("ConnectionString");
							string text6 = securityElement2.Attribute("KeyRestrictions");
							string text7 = securityElement2.Attribute("KeyRestrictionBehavior");
							KeyRestrictionBehavior keyRestrictionBehavior = KeyRestrictionBehavior.AllowOnly;
							if (text7 != null)
							{
								keyRestrictionBehavior = (KeyRestrictionBehavior)Enum.Parse(typeof(KeyRestrictionBehavior), text7, true);
							}
							text5 = this.DecodeXmlValue(text5);
							text6 = this.DecodeXmlValue(text6);
							this.Add(text5, text6, keyRestrictionBehavior);
						}
					}
					return;
				}
			}
			this._allowBlankPassword = false;
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002AC1 RID: 10945 RVA: 0x000BDAC0 File Offset: 0x000BBCC0
		public override SecurityElement ToXml()
		{
			Type type = base.GetType();
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", type.AssemblyQualifiedName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				securityElement.AddAttribute("AllowBlankPassword", this._allowBlankPassword.ToString(CultureInfo.InvariantCulture));
				if (this._keyvalues != null)
				{
					foreach (object obj in this._keyvalues)
					{
						DBConnectionString dbconnectionString = (DBConnectionString)obj;
						SecurityElement securityElement2 = new SecurityElement("add");
						string text = dbconnectionString.ConnectionString;
						text = this.EncodeXmlValue(text);
						if (!ADP.IsEmpty(text))
						{
							securityElement2.AddAttribute("ConnectionString", text);
						}
						text = dbconnectionString.Restrictions;
						text = this.EncodeXmlValue(text);
						if (text == null)
						{
							text = ADP.StrEmpty;
						}
						securityElement2.AddAttribute("KeyRestrictions", text);
						text = dbconnectionString.Behavior.ToString();
						securityElement2.AddAttribute("KeyRestrictionBehavior", text);
						securityElement.AddChild(securityElement2);
					}
				}
			}
			return securityElement;
		}

		// Token: 0x040019E4 RID: 6628
		private bool _isUnrestricted;

		// Token: 0x040019E5 RID: 6629
		private bool _allowBlankPassword;

		// Token: 0x040019E6 RID: 6630
		private NameValuePermission _keyvaluetree;

		// Token: 0x040019E7 RID: 6631
		private ArrayList _keyvalues;

		// Token: 0x02000387 RID: 903
		private static class XmlStr
		{
			// Token: 0x040019E8 RID: 6632
			internal const string _class = "class";

			// Token: 0x040019E9 RID: 6633
			internal const string _IPermission = "IPermission";

			// Token: 0x040019EA RID: 6634
			internal const string _Permission = "Permission";

			// Token: 0x040019EB RID: 6635
			internal const string _Unrestricted = "Unrestricted";

			// Token: 0x040019EC RID: 6636
			internal const string _AllowBlankPassword = "AllowBlankPassword";

			// Token: 0x040019ED RID: 6637
			internal const string _true = "true";

			// Token: 0x040019EE RID: 6638
			internal const string _Version = "version";

			// Token: 0x040019EF RID: 6639
			internal const string _VersionNumber = "1";

			// Token: 0x040019F0 RID: 6640
			internal const string _add = "add";

			// Token: 0x040019F1 RID: 6641
			internal const string _ConnectionString = "ConnectionString";

			// Token: 0x040019F2 RID: 6642
			internal const string _KeyRestrictions = "KeyRestrictions";

			// Token: 0x040019F3 RID: 6643
			internal const string _KeyRestrictionBehavior = "KeyRestrictionBehavior";
		}
	}
}
