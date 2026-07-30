using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a security descriptor. A security descriptor includes an owner, a primary group, a Discretionary Access Control List (DACL), and a System Access Control List (SACL).</summary>
	// Token: 0x0200060A RID: 1546
	public sealed class RawSecurityDescriptor : GenericSecurityDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> class from the specified Security Descriptor Definition Language (SDDL) string.</summary>
		/// <param name="sddlForm">The SDDL string from which to create the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		// Token: 0x060043AC RID: 17324 RVA: 0x000ED9E9 File Offset: 0x000EBBE9
		public RawSecurityDescriptor(string sddlForm)
		{
			if (sddlForm == null)
			{
				throw new ArgumentNullException("sddlForm");
			}
			this.ParseSddl(sddlForm.Replace(" ", ""));
			this.control_flags |= ControlFlags.SelfRelative;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> class from the specified array of byte values.</summary>
		/// <param name="binaryForm">The array of byte values from which to create the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		/// <param name="offset">The offset in the  <paramref name="binaryForm" /> array at which to begin copying.</param>
		// Token: 0x060043AD RID: 17325 RVA: 0x000EDA28 File Offset: 0x000EBC28
		public RawSecurityDescriptor(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0 || offset > binaryForm.Length - 20)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "Offset out of range");
			}
			if (binaryForm[offset] != 1)
			{
				throw new ArgumentException("Unrecognized Security Descriptor revision.", "binaryForm");
			}
			this.resourcemgr_control = binaryForm[offset + 1];
			this.control_flags = (ControlFlags)this.ReadUShort(binaryForm, offset + 2);
			int num = this.ReadInt(binaryForm, offset + 4);
			int num2 = this.ReadInt(binaryForm, offset + 8);
			int num3 = this.ReadInt(binaryForm, offset + 12);
			int num4 = this.ReadInt(binaryForm, offset + 16);
			if (num != 0)
			{
				this.owner_sid = new SecurityIdentifier(binaryForm, num);
			}
			if (num2 != 0)
			{
				this.group_sid = new SecurityIdentifier(binaryForm, num2);
			}
			if (num3 != 0)
			{
				this.system_acl = new RawAcl(binaryForm, num3);
			}
			if (num4 != 0)
			{
				this.discretionary_acl = new RawAcl(binaryForm, num4);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> class with the specified values.</summary>
		/// <param name="flags">Flags that specify behavior of the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		/// <param name="owner">The owner for the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		/// <param name="group">The primary group for the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		/// <param name="systemAcl">The System Access Control List (SACL) for the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		/// <param name="discretionaryAcl">The Discretionary Access Control List (DACL) for the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		// Token: 0x060043AE RID: 17326 RVA: 0x000EDB0B File Offset: 0x000EBD0B
		public RawSecurityDescriptor(ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, RawAcl systemAcl, RawAcl discretionaryAcl)
		{
			this.control_flags = flags;
			this.owner_sid = owner;
			this.group_sid = group;
			this.system_acl = systemAcl;
			this.discretionary_acl = discretionaryAcl;
		}

		/// <summary>Gets values that specify behavior of the <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</summary>
		/// <returns>One or more values of the <see cref="T:System.Security.AccessControl.ControlFlags" /> enumeration combined with a logical OR operation.</returns>
		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x060043AF RID: 17327 RVA: 0x000EDB38 File Offset: 0x000EBD38
		public override ControlFlags ControlFlags
		{
			get
			{
				return this.control_flags;
			}
		}

		/// <summary>Gets or sets the Discretionary Access Control List (DACL) for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object. The DACL contains access rules.</summary>
		/// <returns>The DACL for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</returns>
		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x060043B0 RID: 17328 RVA: 0x000EDB40 File Offset: 0x000EBD40
		// (set) Token: 0x060043B1 RID: 17329 RVA: 0x000EDB48 File Offset: 0x000EBD48
		public RawAcl DiscretionaryAcl
		{
			get
			{
				return this.discretionary_acl;
			}
			set
			{
				this.discretionary_acl = value;
			}
		}

		/// <summary>Gets or sets the primary group for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</summary>
		/// <returns>The primary group for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</returns>
		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x060043B2 RID: 17330 RVA: 0x000EDB51 File Offset: 0x000EBD51
		// (set) Token: 0x060043B3 RID: 17331 RVA: 0x000EDB59 File Offset: 0x000EBD59
		public override SecurityIdentifier Group
		{
			get
			{
				return this.group_sid;
			}
			set
			{
				this.group_sid = value;
			}
		}

		/// <summary>Gets or sets the owner of the object associated with this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</summary>
		/// <returns>The owner of the object associated with this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</returns>
		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x060043B4 RID: 17332 RVA: 0x000EDB62 File Offset: 0x000EBD62
		// (set) Token: 0x060043B5 RID: 17333 RVA: 0x000EDB6A File Offset: 0x000EBD6A
		public override SecurityIdentifier Owner
		{
			get
			{
				return this.owner_sid;
			}
			set
			{
				this.owner_sid = value;
			}
		}

		/// <summary>Gets or sets a byte value that represents the resource manager control bits associated with this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</summary>
		/// <returns>A byte value that represents the resource manager control bits associated with this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</returns>
		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x060043B6 RID: 17334 RVA: 0x000EDB73 File Offset: 0x000EBD73
		// (set) Token: 0x060043B7 RID: 17335 RVA: 0x000EDB7B File Offset: 0x000EBD7B
		public byte ResourceManagerControl
		{
			get
			{
				return this.resourcemgr_control;
			}
			set
			{
				this.resourcemgr_control = value;
			}
		}

		/// <summary>Gets or sets the System Access Control List (SACL) for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object. The SACL contains audit rules.</summary>
		/// <returns>The SACL for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</returns>
		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x060043B8 RID: 17336 RVA: 0x000EDB84 File Offset: 0x000EBD84
		// (set) Token: 0x060043B9 RID: 17337 RVA: 0x000EDB8C File Offset: 0x000EBD8C
		public RawAcl SystemAcl
		{
			get
			{
				return this.system_acl;
			}
			set
			{
				this.system_acl = value;
			}
		}

		/// <summary>Sets the <see cref="P:System.Security.AccessControl.RawSecurityDescriptor.ControlFlags" /> property of this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object to the specified value.</summary>
		/// <param name="flags">One or more values of the <see cref="T:System.Security.AccessControl.ControlFlags" /> enumeration combined with a logical OR operation.</param>
		// Token: 0x060043BA RID: 17338 RVA: 0x000EDB95 File Offset: 0x000EBD95
		public void SetFlags(ControlFlags flags)
		{
			this.control_flags = flags | ControlFlags.SelfRelative;
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x060043BB RID: 17339 RVA: 0x000EDBA4 File Offset: 0x000EBDA4
		internal override GenericAcl InternalDacl
		{
			get
			{
				return this.DiscretionaryAcl;
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x060043BC RID: 17340 RVA: 0x000EDBAC File Offset: 0x000EBDAC
		internal override GenericAcl InternalSacl
		{
			get
			{
				return this.SystemAcl;
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x060043BD RID: 17341 RVA: 0x000EDBB4 File Offset: 0x000EBDB4
		internal override byte InternalReservedField
		{
			get
			{
				return this.ResourceManagerControl;
			}
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x000EDBBC File Offset: 0x000EBDBC
		private void ParseSddl(string sddlForm)
		{
			ControlFlags controlFlags = ControlFlags.None;
			int i = 0;
			while (i < sddlForm.Length - 2)
			{
				string text = sddlForm.Substring(i, 2);
				if (!(text == "O:"))
				{
					if (!(text == "G:"))
					{
						if (!(text == "D:"))
						{
							if (!(text == "S:"))
							{
								throw new ArgumentException("Invalid SDDL.", "sddlForm");
							}
							i += 2;
							this.SystemAcl = RawAcl.ParseSddlForm(sddlForm, false, ref controlFlags, ref i);
							controlFlags |= ControlFlags.SystemAclPresent;
						}
						else
						{
							i += 2;
							this.DiscretionaryAcl = RawAcl.ParseSddlForm(sddlForm, true, ref controlFlags, ref i);
							controlFlags |= ControlFlags.DiscretionaryAclPresent;
						}
					}
					else
					{
						i += 2;
						this.Group = SecurityIdentifier.ParseSddlForm(sddlForm, ref i);
					}
				}
				else
				{
					i += 2;
					this.Owner = SecurityIdentifier.ParseSddlForm(sddlForm, ref i);
				}
			}
			if (i != sddlForm.Length)
			{
				throw new ArgumentException("Invalid SDDL.", "sddlForm");
			}
			this.SetFlags(controlFlags);
		}

		// Token: 0x060043BF RID: 17343 RVA: 0x000ED9DB File Offset: 0x000EBBDB
		private ushort ReadUShort(byte[] buffer, int offset)
		{
			return (ushort)((int)buffer[offset] | ((int)buffer[offset + 1] << 8));
		}

		// Token: 0x060043C0 RID: 17344 RVA: 0x000EDCAE File Offset: 0x000EBEAE
		private int ReadInt(byte[] buffer, int offset)
		{
			return (int)buffer[offset] | ((int)buffer[offset + 1] << 8) | ((int)buffer[offset + 2] << 16) | ((int)buffer[offset + 3] << 24);
		}

		// Token: 0x040021EB RID: 8683
		private ControlFlags control_flags;

		// Token: 0x040021EC RID: 8684
		private SecurityIdentifier owner_sid;

		// Token: 0x040021ED RID: 8685
		private SecurityIdentifier group_sid;

		// Token: 0x040021EE RID: 8686
		private RawAcl system_acl;

		// Token: 0x040021EF RID: 8687
		private RawAcl discretionary_acl;

		// Token: 0x040021F0 RID: 8688
		private byte resourcemgr_control;
	}
}
