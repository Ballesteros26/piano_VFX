using System;
using System.Globalization;
using System.Security.Principal;
using System.Text;

namespace System.Security.AccessControl
{
	/// <summary>Represents a security descriptor. A security descriptor includes an owner, a primary group, a Discretionary Access Control List (DACL), and a System Access Control List (SACL).</summary>
	// Token: 0x020005F1 RID: 1521
	public abstract class GenericSecurityDescriptor
	{
		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object. This length should be used before marshaling the ACL into a binary array with the <see cref="M:System.Security.AccessControl.GenericSecurityDescriptor.GetBinaryForm" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</returns>
		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x060042BA RID: 17082 RVA: 0x000EB9A8 File Offset: 0x000E9BA8
		public int BinaryLength
		{
			get
			{
				int num = 20;
				if (this.Owner != null)
				{
					num += this.Owner.BinaryLength;
				}
				if (this.Group != null)
				{
					num += this.Group.BinaryLength;
				}
				if (this.DaclPresent && !this.DaclIsUnmodifiedAefa)
				{
					num += this.InternalDacl.BinaryLength;
				}
				if (this.SaclPresent)
				{
					num += this.InternalSacl.BinaryLength;
				}
				return num;
			}
		}

		/// <summary>Gets values that specify behavior of the <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</summary>
		/// <returns>One or more values of the <see cref="T:System.Security.AccessControl.ControlFlags" /> enumeration combined with a logical OR operation.</returns>
		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x060042BB RID: 17083
		public abstract ControlFlags ControlFlags { get; }

		/// <summary>Gets or sets the primary group for this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</summary>
		/// <returns>The primary group for this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</returns>
		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x060042BC RID: 17084
		// (set) Token: 0x060042BD RID: 17085
		public abstract SecurityIdentifier Group { get; set; }

		/// <summary>Gets or sets the owner of the object associated with this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</summary>
		/// <returns>The owner of the object associated with this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</returns>
		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x060042BE RID: 17086
		// (set) Token: 0x060042BF RID: 17087
		public abstract SecurityIdentifier Owner { get; set; }

		/// <summary>Gets the revision level of the <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</summary>
		/// <returns>A byte value that specifies the revision level of the <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" />.</returns>
		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x060042C0 RID: 17088 RVA: 0x00003B29 File Offset: 0x00001D29
		public static byte Revision
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x060042C1 RID: 17089 RVA: 0x0000A42E File Offset: 0x0000862E
		internal virtual GenericAcl InternalDacl
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x060042C2 RID: 17090 RVA: 0x0000A42E File Offset: 0x0000862E
		internal virtual GenericAcl InternalSacl
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x060042C3 RID: 17091 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal virtual byte InternalReservedField
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Returns an array of byte values that represents the information contained in this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x060042C4 RID: 17092 RVA: 0x000EBA28 File Offset: 0x000E9C28
		public void GetBinaryForm(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			int binaryLength = this.BinaryLength;
			if (offset < 0 || offset > binaryForm.Length - binaryLength)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			ControlFlags controlFlags = this.ControlFlags;
			if (this.DaclIsUnmodifiedAefa)
			{
				controlFlags &= ~ControlFlags.DiscretionaryAclPresent;
			}
			binaryForm[offset] = GenericSecurityDescriptor.Revision;
			binaryForm[offset + 1] = this.InternalReservedField;
			this.WriteUShort((ushort)controlFlags, binaryForm, offset + 2);
			int num = 20;
			if (this.Owner != null)
			{
				this.WriteInt(num, binaryForm, offset + 4);
				this.Owner.GetBinaryForm(binaryForm, offset + num);
				num += this.Owner.BinaryLength;
			}
			else
			{
				this.WriteInt(0, binaryForm, offset + 4);
			}
			if (this.Group != null)
			{
				this.WriteInt(num, binaryForm, offset + 8);
				this.Group.GetBinaryForm(binaryForm, offset + num);
				num += this.Group.BinaryLength;
			}
			else
			{
				this.WriteInt(0, binaryForm, offset + 8);
			}
			GenericAcl internalSacl = this.InternalSacl;
			if (this.SaclPresent)
			{
				this.WriteInt(num, binaryForm, offset + 12);
				internalSacl.GetBinaryForm(binaryForm, offset + num);
				num += this.InternalSacl.BinaryLength;
			}
			else
			{
				this.WriteInt(0, binaryForm, offset + 12);
			}
			GenericAcl internalDacl = this.InternalDacl;
			if (this.DaclPresent && !this.DaclIsUnmodifiedAefa)
			{
				this.WriteInt(num, binaryForm, offset + 16);
				internalDacl.GetBinaryForm(binaryForm, offset + num);
				num += this.InternalDacl.BinaryLength;
				return;
			}
			this.WriteInt(0, binaryForm, offset + 16);
		}

		/// <summary>Returns the Security Descriptor Definition Language (SDDL) representation of the specified sections of the security descriptor that this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object represents.</summary>
		/// <returns>The SDDL representation of the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</returns>
		/// <param name="includeSections">Specifies which sections (access rules, audit rules, primary group, owner) of the security descriptor to get.</param>
		// Token: 0x060042C5 RID: 17093 RVA: 0x000EBBA8 File Offset: 0x000E9DA8
		public string GetSddlForm(AccessControlSections includeSections)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None && this.Owner != null)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "O:{0}", this.Owner.GetSddlForm());
			}
			if ((includeSections & AccessControlSections.Group) != AccessControlSections.None && this.Group != null)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "G:{0}", this.Group.GetSddlForm());
			}
			if ((includeSections & AccessControlSections.Access) != AccessControlSections.None && this.DaclPresent && !this.DaclIsUnmodifiedAefa)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "D:{0}", this.InternalDacl.GetSddlForm(this.ControlFlags, true));
			}
			if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None && this.SaclPresent)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "S:{0}", this.InternalSacl.GetSddlForm(this.ControlFlags, false));
			}
			return stringBuilder.ToString();
		}

		/// <summary>Returns a boolean value that specifies whether the security descriptor associated with this  <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object can be converted to the Security Descriptor Definition Language (SDDL) format.</summary>
		/// <returns>true if the security descriptor associated with this  <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object can be converted to the Security Descriptor Definition Language (SDDL) format; otherwise, false.</returns>
		// Token: 0x060042C6 RID: 17094 RVA: 0x00003B29 File Offset: 0x00001D29
		public static bool IsSddlConversionSupported()
		{
			return true;
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x060042C7 RID: 17095 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal virtual bool DaclIsUnmodifiedAefa
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x060042C8 RID: 17096 RVA: 0x000EBC87 File Offset: 0x000E9E87
		private bool DaclPresent
		{
			get
			{
				return this.InternalDacl != null && (this.ControlFlags & ControlFlags.DiscretionaryAclPresent) > ControlFlags.None;
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x060042C9 RID: 17097 RVA: 0x000EBC9E File Offset: 0x000E9E9E
		private bool SaclPresent
		{
			get
			{
				return this.InternalSacl != null && (this.ControlFlags & ControlFlags.SystemAclPresent) > ControlFlags.None;
			}
		}

		// Token: 0x060042CA RID: 17098 RVA: 0x000EBCB6 File Offset: 0x000E9EB6
		private void WriteUShort(ushort val, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)val;
			buffer[offset + 1] = (byte)(val >> 8);
		}

		// Token: 0x060042CB RID: 17099 RVA: 0x000EBCC6 File Offset: 0x000E9EC6
		private void WriteInt(int val, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)val;
			buffer[offset + 1] = (byte)(val >> 8);
			buffer[offset + 2] = (byte)(val >> 16);
			buffer[offset + 3] = (byte)(val >> 24);
		}
	}
}
