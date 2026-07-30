using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides the base class for relating one member to another.</summary>
	// Token: 0x02000355 RID: 853
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class MemberRelationshipService
	{
		/// <summary>Establishes a relationship between a source and target object.</summary>
		/// <returns>The current relationship associated with <paramref name="source" />, or <see cref="F:System.ComponentModel.Design.Serialization.MemberRelationship.Empty" /> if there is no relationship.</returns>
		/// <param name="source">The source relationship. This is the left-hand side of a relationship assignment.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is empty, or the relationship is not supported by the service.</exception>
		// Token: 0x1700055C RID: 1372
		public MemberRelationship this[MemberRelationship source]
		{
			get
			{
				if (source.Owner == null)
				{
					throw new ArgumentNullException("Owner");
				}
				if (source.Member == null)
				{
					throw new ArgumentNullException("Member");
				}
				return this.GetRelationship(source);
			}
			set
			{
				if (source.Owner == null)
				{
					throw new ArgumentNullException("Owner");
				}
				if (source.Member == null)
				{
					throw new ArgumentNullException("Member");
				}
				this.SetRelationship(source, value);
			}
		}

		/// <summary>Establishes a relationship between a source and target object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure encapsulating the relationship between a source and target object, or null if there is no relationship.</returns>
		/// <param name="sourceOwner">The owner of a source relationship.</param>
		/// <param name="sourceMember">The member of a source relationship.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceOwner" /> or <paramref name="sourceMember" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceOwner" /> or <paramref name="sourceMember" /> is empty, or the relationship is not supported by the service.</exception>
		// Token: 0x1700055D RID: 1373
		public MemberRelationship this[object sourceOwner, MemberDescriptor sourceMember]
		{
			get
			{
				if (sourceOwner == null)
				{
					throw new ArgumentNullException("sourceOwner");
				}
				if (sourceMember == null)
				{
					throw new ArgumentNullException("sourceMember");
				}
				return this.GetRelationship(new MemberRelationship(sourceOwner, sourceMember));
			}
			set
			{
				if (sourceOwner == null)
				{
					throw new ArgumentNullException("sourceOwner");
				}
				if (sourceMember == null)
				{
					throw new ArgumentNullException("sourceMember");
				}
				this.SetRelationship(new MemberRelationship(sourceOwner, sourceMember), value);
			}
		}

		/// <summary>Gets a relationship to the given source relationship.</summary>
		/// <returns>A relationship to <paramref name="source" />, or <see cref="F:System.ComponentModel.Design.Serialization.MemberRelationship.Empty" /> if no relationship exists.</returns>
		/// <param name="source">The source relationship.</param>
		// Token: 0x06001A85 RID: 6789 RVA: 0x0006B3F0 File Offset: 0x000695F0
		protected virtual MemberRelationship GetRelationship(MemberRelationship source)
		{
			MemberRelationshipService.RelationshipEntry relationshipEntry;
			if (this._relationships != null && this._relationships.TryGetValue(new MemberRelationshipService.RelationshipEntry(source), out relationshipEntry) && relationshipEntry.Owner.IsAlive)
			{
				return new MemberRelationship(relationshipEntry.Owner.Target, relationshipEntry.Member);
			}
			return MemberRelationship.Empty;
		}

		/// <summary>Creates a relationship between the source object and target relationship.</summary>
		/// <param name="source">The source relationship.</param>
		/// <param name="relationship">The relationship to set into the source.</param>
		/// <exception cref="T:System.ArgumentException">The relationship is not supported by the service.</exception>
		// Token: 0x06001A86 RID: 6790 RVA: 0x0006B444 File Offset: 0x00069644
		protected virtual void SetRelationship(MemberRelationship source, MemberRelationship relationship)
		{
			if (!relationship.IsEmpty && !this.SupportsRelationship(source, relationship))
			{
				string text = TypeDescriptor.GetComponentName(source.Owner);
				string text2 = TypeDescriptor.GetComponentName(relationship.Owner);
				if (text == null)
				{
					text = source.Owner.ToString();
				}
				if (text2 == null)
				{
					text2 = relationship.Owner.ToString();
				}
				throw new ArgumentException(global::SR.GetString("Relationships between {0}.{1} and {2}.{3} are not supported.", new object[]
				{
					text,
					source.Member.Name,
					text2,
					relationship.Member.Name
				}));
			}
			if (this._relationships == null)
			{
				this._relationships = new Dictionary<MemberRelationshipService.RelationshipEntry, MemberRelationshipService.RelationshipEntry>();
			}
			this._relationships[new MemberRelationshipService.RelationshipEntry(source)] = new MemberRelationshipService.RelationshipEntry(relationship);
		}

		/// <summary>Gets a value indicating whether the given relationship is supported.</summary>
		/// <returns>true if a relationship between the given two objects is supported; otherwise, false.</returns>
		/// <param name="source">The source relationship.</param>
		/// <param name="relationship">The relationship to set into the source.</param>
		// Token: 0x06001A87 RID: 6791
		public abstract bool SupportsRelationship(MemberRelationship source, MemberRelationship relationship);

		// Token: 0x0400183C RID: 6204
		private Dictionary<MemberRelationshipService.RelationshipEntry, MemberRelationshipService.RelationshipEntry> _relationships = new Dictionary<MemberRelationshipService.RelationshipEntry, MemberRelationshipService.RelationshipEntry>();

		// Token: 0x02000356 RID: 854
		private struct RelationshipEntry
		{
			// Token: 0x06001A89 RID: 6793 RVA: 0x0006B51A File Offset: 0x0006971A
			internal RelationshipEntry(MemberRelationship rel)
			{
				this.Owner = new WeakReference(rel.Owner);
				this.Member = rel.Member;
				this.hashCode = ((rel.Owner == null) ? 0 : rel.Owner.GetHashCode());
			}

			// Token: 0x06001A8A RID: 6794 RVA: 0x0006B55C File Offset: 0x0006975C
			public override bool Equals(object o)
			{
				if (o is MemberRelationshipService.RelationshipEntry)
				{
					MemberRelationshipService.RelationshipEntry relationshipEntry = (MemberRelationshipService.RelationshipEntry)o;
					return this == relationshipEntry;
				}
				return false;
			}

			// Token: 0x06001A8B RID: 6795 RVA: 0x0006B588 File Offset: 0x00069788
			public static bool operator ==(MemberRelationshipService.RelationshipEntry re1, MemberRelationshipService.RelationshipEntry re2)
			{
				object obj = (re1.Owner.IsAlive ? re1.Owner.Target : null);
				object obj2 = (re2.Owner.IsAlive ? re2.Owner.Target : null);
				return obj == obj2 && re1.Member.Equals(re2.Member);
			}

			// Token: 0x06001A8C RID: 6796 RVA: 0x0006B5E2 File Offset: 0x000697E2
			public static bool operator !=(MemberRelationshipService.RelationshipEntry re1, MemberRelationshipService.RelationshipEntry re2)
			{
				return !(re1 == re2);
			}

			// Token: 0x06001A8D RID: 6797 RVA: 0x0006B5EE File Offset: 0x000697EE
			public override int GetHashCode()
			{
				return this.hashCode;
			}

			// Token: 0x0400183D RID: 6205
			internal WeakReference Owner;

			// Token: 0x0400183E RID: 6206
			internal MemberDescriptor Member;

			// Token: 0x0400183F RID: 6207
			private int hashCode;
		}
	}
}
