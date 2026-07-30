using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Represents a single relationship between an object and a member.</summary>
	// Token: 0x02000357 RID: 855
	public struct MemberRelationship
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> class. </summary>
		/// <param name="owner">The object that owns <paramref name="member" />.</param>
		/// <param name="member">The member which is to be related to <paramref name="owner" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> or <paramref name="member" /> is null.</exception>
		// Token: 0x06001A8E RID: 6798 RVA: 0x0006B5F6 File Offset: 0x000697F6
		public MemberRelationship(object owner, MemberDescriptor member)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			this._owner = owner;
			this._member = member;
		}

		/// <summary>Gets a value indicating whether this relationship is equal to the <see cref="F:System.ComponentModel.Design.Serialization.MemberRelationship.Empty" /> relationship. </summary>
		/// <returns>true if this relationship is equal to the <see cref="F:System.ComponentModel.Design.Serialization.MemberRelationship.Empty" /> relationship; otherwise, false.</returns>
		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x0006B622 File Offset: 0x00069822
		public bool IsEmpty
		{
			get
			{
				return this._owner == null;
			}
		}

		/// <summary>Gets the related member.</summary>
		/// <returns>The member that is passed in to the <see cref="M:System.ComponentModel.Design.Serialization.MemberRelationship.#ctor(System.Object,System.ComponentModel.MemberDescriptor)" />.</returns>
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001A90 RID: 6800 RVA: 0x0006B62D File Offset: 0x0006982D
		public MemberDescriptor Member
		{
			get
			{
				return this._member;
			}
		}

		/// <summary>Gets the owning object.</summary>
		/// <returns>The owning object that is passed in to the <see cref="M:System.ComponentModel.Design.Serialization.MemberRelationship.#ctor(System.Object,System.ComponentModel.MemberDescriptor)" />.</returns>
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x0006B635 File Offset: 0x00069835
		public object Owner
		{
			get
			{
				return this._owner;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> instances are equal.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> is equal to the current <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> to compare with the current <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" />.</param>
		// Token: 0x06001A92 RID: 6802 RVA: 0x0006B640 File Offset: 0x00069840
		public override bool Equals(object obj)
		{
			if (!(obj is MemberRelationship))
			{
				return false;
			}
			MemberRelationship memberRelationship = (MemberRelationship)obj;
			return memberRelationship.Owner == this.Owner && memberRelationship.Member == this.Member;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" />.</returns>
		// Token: 0x06001A93 RID: 6803 RVA: 0x0006B67E File Offset: 0x0006987E
		public override int GetHashCode()
		{
			if (this._owner == null)
			{
				return base.GetHashCode();
			}
			return this._owner.GetHashCode() ^ this._member.GetHashCode();
		}

		/// <summary>Tests whether two specified <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structures are equivalent.</summary>
		/// <returns>This operator returns true if the two <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structures are equal; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure that is to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure that is to the right of the equality operator.</param>
		// Token: 0x06001A94 RID: 6804 RVA: 0x0006B6B0 File Offset: 0x000698B0
		public static bool operator ==(MemberRelationship left, MemberRelationship right)
		{
			return left.Owner == right.Owner && left.Member == right.Member;
		}

		/// <summary>Tests whether two specified <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structures are different.</summary>
		/// <returns>This operator returns true if the two <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structures are different; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure that is to the left of the inequality operator.</param>
		/// <param name="right">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure that is to the right of the inequality operator.</param>
		// Token: 0x06001A95 RID: 6805 RVA: 0x0006B6D4 File Offset: 0x000698D4
		public static bool operator !=(MemberRelationship left, MemberRelationship right)
		{
			return !(left == right);
		}

		// Token: 0x04001840 RID: 6208
		private object _owner;

		// Token: 0x04001841 RID: 6209
		private MemberDescriptor _member;

		/// <summary>Represents the empty member relationship. This field is read-only.</summary>
		// Token: 0x04001842 RID: 6210
		public static readonly MemberRelationship Empty;
	}
}
