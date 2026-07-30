using System;
using System.Globalization;
using System.Security.Principal;
using System.Text;
using Unity;

namespace System.Security.AccessControl
{
	/// <summary>Encapsulates all Access Control Entry (ACE) types currently defined by Microsoft Corporation. All <see cref="T:System.Security.AccessControl.KnownAce" /> objects contain a 32-bit access mask and a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
	// Token: 0x020005F3 RID: 1523
	public abstract class KnownAce : GenericAce
	{
		// Token: 0x060042CC RID: 17100 RVA: 0x000EBCEA File Offset: 0x000E9EEA
		internal KnownAce(AceType type, AceFlags flags)
			: base(type, flags)
		{
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x000EBCF4 File Offset: 0x000E9EF4
		internal KnownAce(byte[] binaryForm, int offset)
			: base(binaryForm, offset)
		{
		}

		/// <summary>Gets or sets the access mask for this <see cref="T:System.Security.AccessControl.KnownAce" /> object.</summary>
		/// <returns>The access mask for this <see cref="T:System.Security.AccessControl.KnownAce" /> object.</returns>
		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x060042CE RID: 17102 RVA: 0x000EBCFE File Offset: 0x000E9EFE
		// (set) Token: 0x060042CF RID: 17103 RVA: 0x000EBD06 File Offset: 0x000E9F06
		public int AccessMask
		{
			get
			{
				return this.access_mask;
			}
			set
			{
				this.access_mask = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object associated with this <see cref="T:System.Security.AccessControl.KnownAce" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.SecurityIdentifier" /> object associated with this <see cref="T:System.Security.AccessControl.KnownAce" /> object.</returns>
		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x060042D0 RID: 17104 RVA: 0x000EBD0F File Offset: 0x000E9F0F
		// (set) Token: 0x060042D1 RID: 17105 RVA: 0x000EBD17 File Offset: 0x000E9F17
		public SecurityIdentifier SecurityIdentifier
		{
			get
			{
				return this.identifier;
			}
			set
			{
				this.identifier = value;
			}
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x000EBD20 File Offset: 0x000E9F20
		internal static string GetSddlAccessRights(int accessMask)
		{
			string sddlAliasRights = KnownAce.GetSddlAliasRights(accessMask);
			if (!string.IsNullOrEmpty(sddlAliasRights))
			{
				return sddlAliasRights;
			}
			return string.Format(CultureInfo.InvariantCulture, "0x{0:x}", accessMask);
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x000EBD54 File Offset: 0x000E9F54
		private static string GetSddlAliasRights(int accessMask)
		{
			SddlAccessRight[] array = SddlAccessRight.Decompose(accessMask);
			if (array == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SddlAccessRight sddlAccessRight in array)
			{
				stringBuilder.Append(sddlAccessRight.Name);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal KnownAce()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040021C2 RID: 8642
		private int access_mask;

		// Token: 0x040021C3 RID: 8643
		private SecurityIdentifier identifier;
	}
}
