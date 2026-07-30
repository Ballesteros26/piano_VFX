using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a compound Access Control Entry (ACE).</summary>
	// Token: 0x020005DB RID: 1499
	public sealed class CompoundAce : KnownAce
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CompoundAce" /> class.</summary>
		/// <param name="flags">Contains flags that specify information about the inheritance, inheritance propagation, and auditing conditions for the new Access Control Entry (ACE).</param>
		/// <param name="accessMask">The access mask for the ACE.</param>
		/// <param name="compoundAceType">A value from the <see cref="T:System.Security.AccessControl.CompoundAceType" /> enumeration.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> associated with the new ACE.</param>
		// Token: 0x06004200 RID: 16896 RVA: 0x000EA427 File Offset: 0x000E8627
		public CompoundAce(AceFlags flags, int accessMask, CompoundAceType compoundAceType, SecurityIdentifier sid)
			: base(AceType.AccessAllowedCompound, flags)
		{
			this.compound_ace_type = compoundAceType;
			base.AccessMask = accessMask;
			base.SecurityIdentifier = sid;
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CompoundAce" /> object. This length should be used before marshaling the ACL into a binary array with the <see cref="M:System.Security.AccessControl.CompoundAce.GetBinaryForm" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CompoundAce" /> object.</returns>
		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06004201 RID: 16897 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public override int BinaryLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the type of this <see cref="T:System.Security.AccessControl.CompoundAce" /> object.</summary>
		/// <returns>The type of this <see cref="T:System.Security.AccessControl.CompoundAce" /> object.</returns>
		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06004202 RID: 16898 RVA: 0x000EA447 File Offset: 0x000E8647
		// (set) Token: 0x06004203 RID: 16899 RVA: 0x000EA44F File Offset: 0x000E864F
		public CompoundAceType CompoundAceType
		{
			get
			{
				return this.compound_ace_type;
			}
			set
			{
				this.compound_ace_type = value;
			}
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.CompoundAce" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.CompoundAce" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.CompoundAce" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x06004204 RID: 16900 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004205 RID: 16901 RVA: 0x0002126B File Offset: 0x0001F46B
		internal override string GetSddlForm()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002171 RID: 8561
		private CompoundAceType compound_ace_type;
	}
}
