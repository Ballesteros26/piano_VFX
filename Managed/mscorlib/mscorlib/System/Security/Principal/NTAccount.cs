using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Represents a user or group account.</summary>
	// Token: 0x02000625 RID: 1573
	[ComVisible(false)]
	public sealed class NTAccount : IdentityReference
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.NTAccount" /> class by using the specified name.</summary>
		/// <param name="name">The name used to create the <see cref="T:System.Security.Principal.NTAccount" /> object. This parameter cannot be null or an empty string.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.-or-<paramref name="name" /> is too long.</exception>
		// Token: 0x0600445B RID: 17499 RVA: 0x000F0034 File Offset: 0x000EE234
		public NTAccount(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Locale.GetText("Empty"), "name");
			}
			this._value = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.NTAccount" /> class by using the specified domain name and account name. </summary>
		/// <param name="domainName">The name of the domain. This parameter can be null or an empty string. Domain names that are null values are treated like an empty string.</param>
		/// <param name="accountName">The name of the account. This parameter cannot be null or an empty string.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="accountName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="accountName" /> is an empty string.-or-<paramref name="accountName" /> is too long.-or-<paramref name="domainName" /> is too long.</exception>
		// Token: 0x0600445C RID: 17500 RVA: 0x000F0070 File Offset: 0x000EE270
		public NTAccount(string domainName, string accountName)
		{
			if (accountName == null)
			{
				throw new ArgumentNullException("accountName");
			}
			if (accountName.Length == 0)
			{
				throw new ArgumentException(Locale.GetText("Empty"), "accountName");
			}
			if (domainName == null)
			{
				this._value = accountName;
				return;
			}
			this._value = domainName + "\\" + accountName;
		}

		/// <summary>Returns an uppercase string representation of this <see cref="T:System.Security.Principal.NTAccount" /> object.</summary>
		/// <returns>The uppercase string representation of this <see cref="T:System.Security.Principal.NTAccount" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x0600445D RID: 17501 RVA: 0x000F00CB File Offset: 0x000EE2CB
		public override string Value
		{
			get
			{
				return this._value;
			}
		}

		/// <summary>Returns a value that indicates whether this <see cref="T:System.Security.Principal.NTAccount" /> object is equal to a specified object.</summary>
		/// <returns>true if <paramref name="o" /> is an object with the same underlying type and value as this <see cref="T:System.Security.Principal.NTAccount" /> object; otherwise, false.</returns>
		/// <param name="o">An object to compare with this <see cref="T:System.Security.Principal.NTAccount" /> object, or null.</param>
		// Token: 0x0600445E RID: 17502 RVA: 0x000F00D4 File Offset: 0x000EE2D4
		public override bool Equals(object o)
		{
			NTAccount ntaccount = o as NTAccount;
			return !(ntaccount == null) && ntaccount.Value == this.Value;
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Security.Principal.NTAccount" /> object. The <see cref="M:System.Security.Principal.NTAccount.GetHashCode" /> method is suitable for hashing algorithms and data structures like a hash table.</summary>
		/// <returns>A hash value for the current <see cref="T:System.Security.Principal.NTAccount" /> object.</returns>
		// Token: 0x0600445F RID: 17503 RVA: 0x000F0104 File Offset: 0x000EE304
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		/// <summary>Returns a value that indicates whether the specified type is a valid translation type for the <see cref="T:System.Security.Principal.NTAccount" /> class.</summary>
		/// <returns>true if <paramref name="targetType" /> is a valid translation type for the <see cref="T:System.Security.Principal.NTAccount" /> class; otherwise false.</returns>
		/// <param name="targetType">The type being queried for validity to serve as a conversion from <see cref="T:System.Security.Principal.NTAccount" />. The following target types are valid:- <see cref="T:System.Security.Principal.NTAccount" />- <see cref="T:System.Security.Principal.SecurityIdentifier" /></param>
		// Token: 0x06004460 RID: 17504 RVA: 0x000F0111 File Offset: 0x000EE311
		public override bool IsValidTargetType(Type targetType)
		{
			return targetType == typeof(NTAccount) || targetType == typeof(SecurityIdentifier);
		}

		/// <summary>Returns the account name, in Domain\Account format, for the account represented by the <see cref="T:System.Security.Principal.NTAccount" /> object.</summary>
		/// <returns>The account name, in Domain\Account format.</returns>
		// Token: 0x06004461 RID: 17505 RVA: 0x000F013C File Offset: 0x000EE33C
		public override string ToString()
		{
			return this.Value;
		}

		/// <summary>Translates the account name represented by the <see cref="T:System.Security.Principal.NTAccount" /> object into another <see cref="T:System.Security.Principal.IdentityReference" />-derived type.</summary>
		/// <returns>The converted identity.</returns>
		/// <param name="targetType">The target type for the conversion from <see cref="T:System.Security.Principal.NTAccount" />. The target type must be a type that is considered valid by the <see cref="M:System.Security.Principal.NTAccount.IsValidTargetType(System.Type)" /> method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetType " />is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="targetType " />is not an <see cref="T:System.Security.Principal.IdentityReference" />  type.</exception>
		/// <exception cref="T:System.Security.Principal.IdentityNotMappedException">Some or all identity references could not be translated.</exception>
		/// <exception cref="T:System.SystemException">The source account name is too long.-or-A Win32 error code was returned.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06004462 RID: 17506 RVA: 0x000F0144 File Offset: 0x000EE344
		public override IdentityReference Translate(Type targetType)
		{
			if (targetType == typeof(NTAccount))
			{
				return this;
			}
			if (!(targetType == typeof(SecurityIdentifier)))
			{
				throw new ArgumentException("Unknown type", "targetType");
			}
			WellKnownAccount wellKnownAccount = WellKnownAccount.LookupByName(this.Value);
			if (wellKnownAccount == null || wellKnownAccount.Sid == null)
			{
				throw new IdentityNotMappedException("Cannot map account name: " + this.Value);
			}
			return new SecurityIdentifier(wellKnownAccount.Sid);
		}

		/// <summary>Compares two <see cref="T:System.Security.Principal.NTAccount" /> objects to determine whether they are equal. They are considered equal if they have the same canonical name representation as the one returned by the <see cref="P:System.Security.Principal.NTAccount.Value" /> property or if they are both null. </summary>
		/// <returns>true if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise false.</returns>
		/// <param name="left">The left operand to use for the equality comparison. This parameter can be null.</param>
		/// <param name="right">The right operand to use for the equality comparison. This parameter can be null.</param>
		// Token: 0x06004463 RID: 17507 RVA: 0x000EFEA0 File Offset: 0x000EE0A0
		public static bool operator ==(NTAccount left, NTAccount right)
		{
			if (left == null)
			{
				return right == null;
			}
			return right != null && left.Value == right.Value;
		}

		/// <summary>Compares two <see cref="T:System.Security.Principal.NTAccount" /> objects to determine whether they are not equal. They are considered not equal if they have different canonical name representations than the one returned by the <see cref="P:System.Security.Principal.NTAccount.Value" /> property or if one of the objects is null and the other is not.</summary>
		/// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise false.</returns>
		/// <param name="left">The left operand to use for the inequality comparison. This parameter can be null.</param>
		/// <param name="right">The right operand to use for the inequality comparison. This parameter can be null.</param>
		// Token: 0x06004464 RID: 17508 RVA: 0x000EFEC0 File Offset: 0x000EE0C0
		public static bool operator !=(NTAccount left, NTAccount right)
		{
			if (left == null)
			{
				return right != null;
			}
			return right == null || left.Value != right.Value;
		}

		// Token: 0x0400226B RID: 8811
		private string _value;
	}
}
