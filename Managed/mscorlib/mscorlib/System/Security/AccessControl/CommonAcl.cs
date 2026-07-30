using System;
using System.Security.Principal;
using Unity;

namespace System.Security.AccessControl
{
	/// <summary>Represents an access control list (ACL) and is the base class for the <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> and <see cref="T:System.Security.AccessControl.SystemAcl" /> classes.</summary>
	// Token: 0x020005D2 RID: 1490
	public abstract class CommonAcl : GenericAcl
	{
		// Token: 0x06004197 RID: 16791 RVA: 0x000E90C0 File Offset: 0x000E72C0
		internal CommonAcl(bool isContainer, bool isDS, RawAcl rawAcl)
		{
			if (rawAcl == null)
			{
				rawAcl = new RawAcl(isDS ? GenericAcl.AclRevisionDS : GenericAcl.AclRevision, 10);
			}
			else
			{
				byte[] array = new byte[rawAcl.BinaryLength];
				rawAcl.GetBinaryForm(array, 0);
				rawAcl = new RawAcl(array, 0);
			}
			this.Init(isContainer, isDS, rawAcl);
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x000E9116 File Offset: 0x000E7316
		internal CommonAcl(bool isContainer, bool isDS, byte revision, int capacity)
		{
			this.Init(isContainer, isDS, new RawAcl(revision, capacity));
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x000E912E File Offset: 0x000E732E
		internal CommonAcl(bool isContainer, bool isDS, int capacity)
			: this(isContainer, isDS, isDS ? GenericAcl.AclRevisionDS : GenericAcl.AclRevision, capacity)
		{
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x000E9148 File Offset: 0x000E7348
		private void Init(bool isContainer, bool isDS, RawAcl rawAcl)
		{
			this.is_container = isContainer;
			this.is_ds = isDS;
			this.raw_acl = rawAcl;
			this.CanonicalizeAndClearAefa();
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object. This length should be used before marshaling the access control list (ACL) into a binary array by using the <see cref="M:System.Security.AccessControl.CommonAcl.GetBinaryForm" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object.</returns>
		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600419B RID: 16795 RVA: 0x000E9165 File Offset: 0x000E7365
		public sealed override int BinaryLength
		{
			get
			{
				return this.raw_acl.BinaryLength;
			}
		}

		/// <summary>Gets the number of access control entries (ACEs) in the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object.</summary>
		/// <returns>The number of ACEs in the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object.</returns>
		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x0600419C RID: 16796 RVA: 0x000E9172 File Offset: 0x000E7372
		public sealed override int Count
		{
			get
			{
				return this.raw_acl.Count;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the access control entries (ACEs) in the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object are in canonical order.</summary>
		/// <returns>true if the ACEs in the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object are in canonical order; otherwise, false.</returns>
		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x0600419D RID: 16797 RVA: 0x000E917F File Offset: 0x000E737F
		public bool IsCanonical
		{
			get
			{
				return this.is_canonical;
			}
		}

		/// <summary>Sets whether the <see cref="T:System.Security.AccessControl.CommonAcl" /> object is a container. </summary>
		/// <returns>true if the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object is a container.</returns>
		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x0600419E RID: 16798 RVA: 0x000E9187 File Offset: 0x000E7387
		public bool IsContainer
		{
			get
			{
				return this.is_container;
			}
		}

		/// <summary>Sets whether the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object is a directory object access control list (ACL).</summary>
		/// <returns>true if the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object is a directory object ACL.</returns>
		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x0600419F RID: 16799 RVA: 0x000E918F File Offset: 0x000E738F
		public bool IsDS
		{
			get
			{
				return this.is_ds;
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x060041A0 RID: 16800 RVA: 0x000E9197 File Offset: 0x000E7397
		// (set) Token: 0x060041A1 RID: 16801 RVA: 0x000E919F File Offset: 0x000E739F
		internal bool IsAefa
		{
			get
			{
				return this.is_aefa;
			}
			set
			{
				this.is_aefa = value;
			}
		}

		/// <summary>Gets the revision level of the <see cref="T:System.Security.AccessControl.CommonAcl" />.</summary>
		/// <returns>A byte value that specifies the revision level of the <see cref="T:System.Security.AccessControl.CommonAcl" />.</returns>
		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x060041A2 RID: 16802 RVA: 0x000E91A8 File Offset: 0x000E73A8
		public sealed override byte Revision
		{
			get
			{
				return this.raw_acl.Revision;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.AccessControl.CommonAce" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.CommonAce" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Security.AccessControl.CommonAce" /> to get or set.</param>
		// Token: 0x17000AEB RID: 2795
		public sealed override GenericAce this[int index]
		{
			get
			{
				return CommonAcl.CopyAce(this.raw_acl[index]);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.CommonAcl" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.CommonAcl" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		// Token: 0x060041A5 RID: 16805 RVA: 0x000E91C8 File Offset: 0x000E73C8
		public sealed override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			this.raw_acl.GetBinaryForm(binaryForm, offset);
		}

		/// <summary>Removes all access control entries (ACEs) contained by this <see cref="T:System.Security.AccessControl.CommonAcl" /> object that are associated with the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> object to check for.</param>
		// Token: 0x060041A6 RID: 16806 RVA: 0x000E91D8 File Offset: 0x000E73D8
		public void Purge(SecurityIdentifier sid)
		{
			this.RequireCanonicity();
			this.RemoveAces<KnownAce>((KnownAce ace) => ace.SecurityIdentifier == sid);
		}

		/// <summary>Removes all inherited access control entries (ACEs) from this <see cref="T:System.Security.AccessControl.CommonAcl" /> object.</summary>
		// Token: 0x060041A7 RID: 16807 RVA: 0x000E920A File Offset: 0x000E740A
		public void RemoveInheritedAces()
		{
			this.RequireCanonicity();
			this.RemoveAces<GenericAce>((GenericAce ace) => ace.IsInherited);
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x000E9237 File Offset: 0x000E7437
		internal void RequireCanonicity()
		{
			if (!this.IsCanonical)
			{
				throw new InvalidOperationException("ACL is not canonical.");
			}
		}

		// Token: 0x060041A9 RID: 16809 RVA: 0x000E924C File Offset: 0x000E744C
		internal void CanonicalizeAndClearAefa()
		{
			this.RemoveAces<GenericAce>(new CommonAcl.RemoveAcesCallback<GenericAce>(this.IsAceMeaningless));
			this.is_canonical = this.TestCanonicity();
			if (this.IsCanonical)
			{
				this.ApplyCanonicalSortToExplicitAces();
				this.MergeExplicitAces();
			}
			this.IsAefa = false;
		}

		// Token: 0x060041AA RID: 16810 RVA: 0x000E9288 File Offset: 0x000E7488
		internal virtual bool IsAceMeaningless(GenericAce ace)
		{
			AceFlags aceFlags = ace.AceFlags;
			KnownAce knownAce = ace as KnownAce;
			if (knownAce != null)
			{
				if (knownAce.AccessMask == 0)
				{
					return true;
				}
				if ((aceFlags & AceFlags.InheritOnly) != AceFlags.None)
				{
					if (knownAce is ObjectAce)
					{
						return true;
					}
					if (!this.IsContainer)
					{
						return true;
					}
					if ((aceFlags & (AceFlags.ObjectInherit | AceFlags.ContainerInherit)) == AceFlags.None)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060041AB RID: 16811 RVA: 0x000E92D8 File Offset: 0x000E74D8
		private bool TestCanonicity()
		{
			AceEnumerator aceEnumerator = base.GetEnumerator();
			while (aceEnumerator.MoveNext())
			{
				if (!(aceEnumerator.Current is QualifiedAce))
				{
					return false;
				}
			}
			bool flag = false;
			aceEnumerator = base.GetEnumerator();
			while (aceEnumerator.MoveNext())
			{
				if (((QualifiedAce)aceEnumerator.Current).IsInherited)
				{
					flag = true;
				}
				else if (flag)
				{
					return false;
				}
			}
			bool flag2 = false;
			foreach (GenericAce genericAce in this)
			{
				QualifiedAce qualifiedAce = (QualifiedAce)genericAce;
				if (qualifiedAce.IsInherited)
				{
					break;
				}
				if (qualifiedAce.AceQualifier == AceQualifier.AccessAllowed)
				{
					flag2 = true;
				}
				else if (AceQualifier.AccessDenied == qualifiedAce.AceQualifier && flag2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x000E9378 File Offset: 0x000E7578
		internal int GetCanonicalExplicitDenyAceCount()
		{
			int num = 0;
			while (num < this.Count && !this.raw_acl[num].IsInherited)
			{
				QualifiedAce qualifiedAce = this.raw_acl[num] as QualifiedAce;
				if (qualifiedAce == null || qualifiedAce.AceQualifier != AceQualifier.AccessDenied)
				{
					break;
				}
				num++;
			}
			return num;
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x000E93D0 File Offset: 0x000E75D0
		internal int GetCanonicalExplicitAceCount()
		{
			int num = 0;
			while (num < this.Count && !this.raw_acl[num].IsInherited)
			{
				num++;
			}
			return num;
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x000E9404 File Offset: 0x000E7604
		private void MergeExplicitAces()
		{
			int num = this.GetCanonicalExplicitAceCount();
			int i = 0;
			while (i < num - 1)
			{
				GenericAce genericAce = this.MergeExplicitAcePair(this.raw_acl[i], this.raw_acl[i + 1]);
				if (null != genericAce)
				{
					this.raw_acl[i] = genericAce;
					this.raw_acl.RemoveAce(i + 1);
					num--;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x060041AF RID: 16815 RVA: 0x000E9474 File Offset: 0x000E7674
		private GenericAce MergeExplicitAcePair(GenericAce ace1, GenericAce ace2)
		{
			QualifiedAce qualifiedAce = ace1 as QualifiedAce;
			QualifiedAce qualifiedAce2 = ace2 as QualifiedAce;
			if (!(null != qualifiedAce) || !(null != qualifiedAce2))
			{
				return null;
			}
			if (qualifiedAce.AceQualifier != qualifiedAce2.AceQualifier)
			{
				return null;
			}
			if (!(qualifiedAce.SecurityIdentifier == qualifiedAce2.SecurityIdentifier))
			{
				return null;
			}
			AceFlags aceFlags = qualifiedAce.AceFlags;
			AceFlags aceFlags2 = qualifiedAce2.AceFlags;
			int accessMask = qualifiedAce.AccessMask;
			int accessMask2 = qualifiedAce2.AccessMask;
			if (!this.IsContainer)
			{
				aceFlags &= ~(AceFlags.ObjectInherit | AceFlags.ContainerInherit | AceFlags.NoPropagateInherit | AceFlags.InheritOnly);
				aceFlags2 &= ~(AceFlags.ObjectInherit | AceFlags.ContainerInherit | AceFlags.NoPropagateInherit | AceFlags.InheritOnly);
			}
			AceFlags aceFlags3;
			int num;
			if (aceFlags != aceFlags2)
			{
				if (accessMask != accessMask2)
				{
					return null;
				}
				if ((aceFlags & ~(AceFlags.ObjectInherit | AceFlags.ContainerInherit)) == (aceFlags2 & ~(AceFlags.ObjectInherit | AceFlags.ContainerInherit)))
				{
					aceFlags3 = aceFlags | aceFlags2;
					num = accessMask;
				}
				else
				{
					if ((aceFlags & ~(AceFlags.SuccessfulAccess | AceFlags.FailedAccess)) != (aceFlags2 & ~(AceFlags.SuccessfulAccess | AceFlags.FailedAccess)))
					{
						return null;
					}
					aceFlags3 = aceFlags | aceFlags2;
					num = accessMask;
				}
			}
			else
			{
				aceFlags3 = aceFlags;
				num = accessMask | accessMask2;
			}
			CommonAce commonAce = ace1 as CommonAce;
			CommonAce commonAce2 = ace2 as CommonAce;
			if (null != commonAce && null != commonAce2)
			{
				return new CommonAce(aceFlags3, commonAce.AceQualifier, num, commonAce.SecurityIdentifier, commonAce.IsCallback, commonAce.GetOpaque());
			}
			ObjectAce objectAce = ace1 as ObjectAce;
			ObjectAce objectAce2 = ace2 as ObjectAce;
			if (null != objectAce && null != objectAce2)
			{
				Guid guid;
				Guid guid2;
				CommonAcl.GetObjectAceTypeGuids(objectAce, out guid, out guid2);
				Guid guid3;
				Guid guid4;
				CommonAcl.GetObjectAceTypeGuids(objectAce2, out guid3, out guid4);
				if (guid == guid3 && guid2 == guid4)
				{
					return new ObjectAce(aceFlags3, objectAce.AceQualifier, num, objectAce.SecurityIdentifier, objectAce.ObjectAceFlags, objectAce.ObjectAceType, objectAce.InheritedObjectAceType, objectAce.IsCallback, objectAce.GetOpaque());
				}
			}
			return null;
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x000E961C File Offset: 0x000E781C
		private static void GetObjectAceTypeGuids(ObjectAce ace, out Guid type, out Guid inheritedType)
		{
			type = Guid.Empty;
			inheritedType = Guid.Empty;
			if ((ace.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None)
			{
				type = ace.ObjectAceType;
			}
			if ((ace.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None)
			{
				inheritedType = ace.InheritedObjectAceType;
			}
		}

		// Token: 0x060041B1 RID: 16817
		internal abstract void ApplyCanonicalSortToExplicitAces();

		// Token: 0x060041B2 RID: 16818 RVA: 0x000E966C File Offset: 0x000E786C
		internal void ApplyCanonicalSortToExplicitAces(int start, int count)
		{
			for (int i = start + 1; i < start + count; i++)
			{
				KnownAce knownAce = (KnownAce)this.raw_acl[i];
				SecurityIdentifier securityIdentifier = knownAce.SecurityIdentifier;
				int num = i;
				while (num > start && ((KnownAce)this.raw_acl[num - 1]).SecurityIdentifier.CompareTo(securityIdentifier) > 0)
				{
					this.raw_acl[num] = this.raw_acl[num - 1];
					num--;
				}
				this.raw_acl[num] = knownAce;
			}
		}

		// Token: 0x060041B3 RID: 16819 RVA: 0x000E96F6 File Offset: 0x000E78F6
		internal override string GetSddlForm(ControlFlags sdFlags, bool isDacl)
		{
			return this.raw_acl.GetSddlForm(sdFlags, isDacl);
		}

		// Token: 0x060041B4 RID: 16820 RVA: 0x000E9708 File Offset: 0x000E7908
		internal void RemoveAces<T>(CommonAcl.RemoveAcesCallback<T> callback) where T : GenericAce
		{
			int i = 0;
			while (i < this.raw_acl.Count)
			{
				if (this.raw_acl[i] is T && callback((T)((object)this.raw_acl[i])))
				{
					this.raw_acl.RemoveAce(i);
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x060041B5 RID: 16821 RVA: 0x000E9768 File Offset: 0x000E7968
		internal void AddAce(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
		{
			QualifiedAce qualifiedAce = this.AddAceGetQualifiedAce(aceQualifier, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags);
			this.AddAce(qualifiedAce);
		}

		// Token: 0x060041B6 RID: 16822 RVA: 0x000E978C File Offset: 0x000E798C
		internal void AddAce(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			QualifiedAce qualifiedAce = this.AddAceGetQualifiedAce(aceQualifier, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags, objectFlags, objectType, inheritedObjectType);
			this.AddAce(qualifiedAce);
		}

		// Token: 0x060041B7 RID: 16823 RVA: 0x000E97B8 File Offset: 0x000E79B8
		private QualifiedAce AddAceGetQualifiedAce(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!this.IsDS)
			{
				throw new InvalidOperationException("For this overload, IsDS must be true.");
			}
			if (objectFlags == ObjectAceFlags.None)
			{
				return this.AddAceGetQualifiedAce(aceQualifier, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags);
			}
			return new ObjectAce(this.GetAceFlags(inheritanceFlags, propagationFlags, auditFlags), aceQualifier, accessMask, sid, objectFlags, objectType, inheritedObjectType, false, null);
		}

		// Token: 0x060041B8 RID: 16824 RVA: 0x000E9808 File Offset: 0x000E7A08
		private QualifiedAce AddAceGetQualifiedAce(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
		{
			return new CommonAce(this.GetAceFlags(inheritanceFlags, propagationFlags, auditFlags), aceQualifier, accessMask, sid, false, null);
		}

		// Token: 0x060041B9 RID: 16825 RVA: 0x000E9820 File Offset: 0x000E7A20
		private void AddAce(QualifiedAce newAce)
		{
			this.RequireCanonicity();
			int aceInsertPosition = this.GetAceInsertPosition(newAce.AceQualifier);
			this.raw_acl.InsertAce(aceInsertPosition, CommonAcl.CopyAce(newAce));
			this.CanonicalizeAndClearAefa();
		}

		// Token: 0x060041BA RID: 16826 RVA: 0x000E9858 File Offset: 0x000E7A58
		private static GenericAce CopyAce(GenericAce ace)
		{
			byte[] array = new byte[ace.BinaryLength];
			ace.GetBinaryForm(array, 0);
			return GenericAce.CreateFromBinaryForm(array, 0);
		}

		// Token: 0x060041BB RID: 16827
		internal abstract int GetAceInsertPosition(AceQualifier aceQualifier);

		// Token: 0x060041BC RID: 16828 RVA: 0x000E9880 File Offset: 0x000E7A80
		private AceFlags GetAceFlags(InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
		{
			if (inheritanceFlags != InheritanceFlags.None && !this.IsContainer)
			{
				throw new ArgumentException("Flags only work with containers.", "inheritanceFlags");
			}
			if (inheritanceFlags == InheritanceFlags.None && propagationFlags != PropagationFlags.None)
			{
				throw new ArgumentException("Propagation flags need inheritance flags.", "propagationFlags");
			}
			AceFlags aceFlags = AceFlags.None;
			if ((InheritanceFlags.ContainerInherit & inheritanceFlags) != InheritanceFlags.None)
			{
				aceFlags |= AceFlags.ContainerInherit;
			}
			if ((InheritanceFlags.ObjectInherit & inheritanceFlags) != InheritanceFlags.None)
			{
				aceFlags |= AceFlags.ObjectInherit;
			}
			if ((PropagationFlags.InheritOnly & propagationFlags) != PropagationFlags.None)
			{
				aceFlags |= AceFlags.InheritOnly;
			}
			if ((PropagationFlags.NoPropagateInherit & propagationFlags) != PropagationFlags.None)
			{
				aceFlags |= AceFlags.NoPropagateInherit;
			}
			if ((AuditFlags.Success & auditFlags) != AuditFlags.None)
			{
				aceFlags |= AceFlags.SuccessfulAccess;
			}
			if ((AuditFlags.Failure & auditFlags) != AuditFlags.None)
			{
				aceFlags |= AceFlags.FailedAccess;
			}
			return aceFlags;
		}

		// Token: 0x060041BD RID: 16829 RVA: 0x000E98FC File Offset: 0x000E7AFC
		internal void RemoveAceSpecific(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
		{
			this.RequireCanonicity();
			this.RemoveAces<CommonAce>((CommonAce ace) => ace.AccessMask == accessMask && ace.AceQualifier == aceQualifier && !(ace.SecurityIdentifier != sid) && ace.InheritanceFlags == inheritanceFlags && (inheritanceFlags == InheritanceFlags.None || ace.PropagationFlags == propagationFlags) && ace.AuditFlags == auditFlags);
			this.CanonicalizeAndClearAefa();
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x000E995C File Offset: 0x000E7B5C
		internal void RemoveAceSpecific(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!this.IsDS)
			{
				throw new InvalidOperationException("For this overload, IsDS must be true.");
			}
			if (objectFlags == ObjectAceFlags.None)
			{
				this.RemoveAceSpecific(aceQualifier, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags);
				return;
			}
			this.RequireCanonicity();
			this.RemoveAces<ObjectAce>((ObjectAce ace) => ace.AccessMask == accessMask && ace.AceQualifier == aceQualifier && !(ace.SecurityIdentifier != sid) && ace.InheritanceFlags == inheritanceFlags && (inheritanceFlags == InheritanceFlags.None || ace.PropagationFlags == propagationFlags) && ace.AuditFlags == auditFlags && ace.ObjectAceFlags == objectFlags && ((objectFlags & ObjectAceFlags.ObjectAceTypePresent) == ObjectAceFlags.None || !(ace.ObjectAceType != objectType)) && ((objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) == ObjectAceFlags.None || !(ace.InheritedObjectAceType != objectType)));
			this.CanonicalizeAndClearAefa();
		}

		// Token: 0x060041BF RID: 16831 RVA: 0x000E9A10 File Offset: 0x000E7C10
		internal void SetAce(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
		{
			QualifiedAce qualifiedAce = this.AddAceGetQualifiedAce(aceQualifier, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags);
			this.SetAce(qualifiedAce);
		}

		// Token: 0x060041C0 RID: 16832 RVA: 0x000E9A34 File Offset: 0x000E7C34
		internal void SetAce(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			QualifiedAce qualifiedAce = this.AddAceGetQualifiedAce(aceQualifier, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags, objectFlags, objectType, inheritedObjectType);
			this.SetAce(qualifiedAce);
		}

		// Token: 0x060041C1 RID: 16833 RVA: 0x000E9A60 File Offset: 0x000E7C60
		private void SetAce(QualifiedAce newAce)
		{
			this.RequireCanonicity();
			this.RemoveAces<QualifiedAce>((QualifiedAce oldAce) => oldAce.AceQualifier == newAce.AceQualifier && oldAce.SecurityIdentifier == newAce.SecurityIdentifier);
			this.CanonicalizeAndClearAefa();
			this.AddAce(newAce);
		}

		// Token: 0x060041C2 RID: 16834 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal CommonAcl()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002152 RID: 8530
		private const int default_capacity = 10;

		// Token: 0x04002153 RID: 8531
		private bool is_aefa;

		// Token: 0x04002154 RID: 8532
		private bool is_canonical;

		// Token: 0x04002155 RID: 8533
		private bool is_container;

		// Token: 0x04002156 RID: 8534
		private bool is_ds;

		// Token: 0x04002157 RID: 8535
		internal RawAcl raw_acl;

		// Token: 0x020005D3 RID: 1491
		// (Invoke) Token: 0x060041C4 RID: 16836
		internal delegate bool RemoveAcesCallback<T>(T ace);
	}
}
