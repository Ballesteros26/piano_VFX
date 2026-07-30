using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Security.Claims
{
	/// <summary>Represents a claims-based identity.</summary>
	// Token: 0x02000635 RID: 1589
	[ComVisible(true)]
	[Serializable]
	public class ClaimsIdentity : IIdentity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class with an empty claims collection.</summary>
		// Token: 0x060044EE RID: 17646 RVA: 0x000F24A5 File Offset: 0x000F06A5
		public ClaimsIdentity()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class using the name and authentication type from the specified <see cref="T:System.Security.Principal.IIdentity" />.</summary>
		/// <param name="identity">The identity from which to base the new claims identity.</param>
		// Token: 0x060044EF RID: 17647 RVA: 0x000F24AE File Offset: 0x000F06AE
		public ClaimsIdentity(IIdentity identity)
			: this(identity, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class using an enumerated collection of <see cref="T:System.Security.Claims.Claim" /> objects.</summary>
		/// <param name="claims">The claims with which to populate the claims identity.</param>
		// Token: 0x060044F0 RID: 17648 RVA: 0x000F24B8 File Offset: 0x000F06B8
		public ClaimsIdentity(IEnumerable<Claim> claims)
			: this(null, claims, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class with an empty claims collection and the specified authentication type.</summary>
		/// <param name="authenticationType">The type of authentication used.</param>
		// Token: 0x060044F1 RID: 17649 RVA: 0x000F24C5 File Offset: 0x000F06C5
		public ClaimsIdentity(string authenticationType)
			: this(null, null, authenticationType, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class with the specified claims and authentication type.</summary>
		/// <param name="claims">The claims with which to populate the claims identity.</param>
		/// <param name="authenticationType">The type of authentication used.</param>
		// Token: 0x060044F2 RID: 17650 RVA: 0x000F24D2 File Offset: 0x000F06D2
		public ClaimsIdentity(IEnumerable<Claim> claims, string authenticationType)
			: this(null, claims, authenticationType, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class using the specified claims and the specified <see cref="T:System.Security.Principal.IIdentity" />.</summary>
		/// <param name="identity">The identity from which to base the new claims identity.</param>
		/// <param name="claims">The claims with which to populate the claims identity.</param>
		// Token: 0x060044F3 RID: 17651 RVA: 0x000F24DF File Offset: 0x000F06DF
		public ClaimsIdentity(IIdentity identity, IEnumerable<Claim> claims)
			: this(identity, claims, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class with the specified authentication type, name claim type, and role claim type.</summary>
		/// <param name="authenticationType">The type of authentication used.</param>
		/// <param name="nameType">The claim type to use for name claims.</param>
		/// <param name="roleType">The claim type to use for role claims.</param>
		// Token: 0x060044F4 RID: 17652 RVA: 0x000F24EC File Offset: 0x000F06EC
		public ClaimsIdentity(string authenticationType, string nameType, string roleType)
			: this(null, null, authenticationType, nameType, roleType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class with the specified, claims, authentication type, name claim type, and role claim type.</summary>
		/// <param name="claims">The claims with which to populate the claims identity.</param>
		/// <param name="authenticationType">The type of authentication used.</param>
		/// <param name="nameType">The claim type to use for name claims.</param>
		/// <param name="roleType">The claim type to use for role claims.</param>
		// Token: 0x060044F5 RID: 17653 RVA: 0x000F24F9 File Offset: 0x000F06F9
		public ClaimsIdentity(IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType)
			: this(null, claims, authenticationType, nameType, roleType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class from the specified <see cref="T:System.Security.Principal.IIdentity" /> using the specified claims, authentication type, name claim type, and role claim type.</summary>
		/// <param name="identity">The identity from which to base the new claims identity.</param>
		/// <param name="claims">The claims with which to populate the new claims identity.</param>
		/// <param name="authenticationType">The type of authentication used.</param>
		/// <param name="nameType">The claim type to use for name claims.</param>
		/// <param name="roleType">The claim type to use for role claims.</param>
		// Token: 0x060044F6 RID: 17654 RVA: 0x000F2507 File Offset: 0x000F0707
		public ClaimsIdentity(IIdentity identity, IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType)
			: this(identity, claims, authenticationType, nameType, roleType, true)
		{
		}

		// Token: 0x060044F7 RID: 17655 RVA: 0x000F2518 File Offset: 0x000F0718
		internal ClaimsIdentity(IIdentity identity, IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType, bool checkAuthType)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			bool flag = false;
			bool flag2 = false;
			if (checkAuthType && identity != null && string.IsNullOrEmpty(authenticationType))
			{
				if (identity is WindowsIdentity)
				{
					try
					{
						this.m_authenticationType = identity.AuthenticationType;
						goto IL_0085;
					}
					catch (UnauthorizedAccessException)
					{
						this.m_authenticationType = null;
						goto IL_0085;
					}
				}
				this.m_authenticationType = identity.AuthenticationType;
			}
			else
			{
				this.m_authenticationType = authenticationType;
			}
			IL_0085:
			if (!string.IsNullOrEmpty(nameType))
			{
				this.m_nameType = nameType;
				flag = true;
			}
			if (!string.IsNullOrEmpty(roleType))
			{
				this.m_roleType = roleType;
				flag2 = true;
			}
			ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
			if (claimsIdentity != null)
			{
				this.m_label = claimsIdentity.m_label;
				if (!flag)
				{
					this.m_nameType = claimsIdentity.m_nameType;
				}
				if (!flag2)
				{
					this.m_roleType = claimsIdentity.m_roleType;
				}
				this.m_bootstrapContext = claimsIdentity.m_bootstrapContext;
				if (claimsIdentity.Actor != null)
				{
					if (this.IsCircular(claimsIdentity.Actor))
					{
						throw new InvalidOperationException(Environment.GetResourceString("Actor cannot be set so that circular directed graph will exist chaining the subjects together."));
					}
					if (!AppContextSwitches.SetActorAsReferenceWhenCopyingClaimsIdentity)
					{
						this.m_actor = claimsIdentity.Actor.Clone();
					}
					else
					{
						this.m_actor = claimsIdentity.Actor;
					}
				}
				if (claimsIdentity is WindowsIdentity && !(this is WindowsIdentity))
				{
					this.SafeAddClaims(claimsIdentity.Claims);
				}
				else
				{
					this.SafeAddClaims(claimsIdentity.m_instanceClaims);
				}
				if (claimsIdentity.m_userSerializationData != null)
				{
					this.m_userSerializationData = claimsIdentity.m_userSerializationData.Clone() as byte[];
				}
			}
			else if (identity != null && !string.IsNullOrEmpty(identity.Name))
			{
				this.SafeAddClaim(new Claim(this.m_nameType, identity.Name, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", this));
			}
			if (claims != null)
			{
				this.SafeAddClaims(claims);
			}
		}

		// Token: 0x060044F8 RID: 17656 RVA: 0x000F26FC File Offset: 0x000F08FC
		public ClaimsIdentity(BinaryReader reader)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Initialize(reader);
		}

		// Token: 0x060044F9 RID: 17657 RVA: 0x000F275C File Offset: 0x000F095C
		protected ClaimsIdentity(ClaimsIdentity other)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other.m_actor != null)
			{
				this.m_actor = other.m_actor.Clone();
			}
			this.m_authenticationType = other.m_authenticationType;
			this.m_bootstrapContext = other.m_bootstrapContext;
			this.m_label = other.m_label;
			this.m_nameType = other.m_nameType;
			this.m_roleType = other.m_roleType;
			if (other.m_userSerializationData != null)
			{
				this.m_userSerializationData = other.m_userSerializationData.Clone() as byte[];
			}
			this.SafeAddClaims(other.m_instanceClaims);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class from a serialized stream created by using <see cref="T:System.Runtime.Serialization.ISerializable" />.</summary>
		/// <param name="info">The serialized data.</param>
		/// <param name="context">The context for serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x060044FA RID: 17658 RVA: 0x000F2834 File Offset: 0x000F0A34
		[SecurityCritical]
		protected ClaimsIdentity(SerializationInfo info, StreamingContext context)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.Deserialize(info, context, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class from a serialized stream created by using <see cref="T:System.Runtime.Serialization.ISerializable" />.</summary>
		/// <param name="info">The serialized data.</param>
		// Token: 0x060044FB RID: 17659 RVA: 0x000F2898 File Offset: 0x000F0A98
		[SecurityCritical]
		protected ClaimsIdentity(SerializationInfo info)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.Deserialize(info, default(StreamingContext), false);
		}

		/// <summary>Gets the authentication type.</summary>
		/// <returns>The authentication type.</returns>
		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x060044FC RID: 17660 RVA: 0x000F2901 File Offset: 0x000F0B01
		public virtual string AuthenticationType
		{
			get
			{
				return this.m_authenticationType;
			}
		}

		/// <summary>Gets a value that indicates whether the identity has been authenticated.</summary>
		/// <returns>true if the identity has been authenticated; otherwise, false.</returns>
		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x060044FD RID: 17661 RVA: 0x000F2909 File Offset: 0x000F0B09
		public virtual bool IsAuthenticated
		{
			get
			{
				return !string.IsNullOrEmpty(this.m_authenticationType);
			}
		}

		/// <summary>Gets or sets the identity of the calling party that was granted delegation rights.</summary>
		/// <returns>The calling party that was granted delegation rights.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt to set the property to the current instance occurs.</exception>
		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x060044FE RID: 17662 RVA: 0x000F2919 File Offset: 0x000F0B19
		// (set) Token: 0x060044FF RID: 17663 RVA: 0x000F2921 File Offset: 0x000F0B21
		public ClaimsIdentity Actor
		{
			get
			{
				return this.m_actor;
			}
			set
			{
				if (value != null && this.IsCircular(value))
				{
					throw new InvalidOperationException(Environment.GetResourceString("Actor cannot be set so that circular directed graph will exist chaining the subjects together."));
				}
				this.m_actor = value;
			}
		}

		/// <summary>Gets or sets the token that was used to create this claims identity.</summary>
		/// <returns>The bootstrap context.</returns>
		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06004500 RID: 17664 RVA: 0x000F2946 File Offset: 0x000F0B46
		// (set) Token: 0x06004501 RID: 17665 RVA: 0x000F294E File Offset: 0x000F0B4E
		public object BootstrapContext
		{
			get
			{
				return this.m_bootstrapContext;
			}
			[SecurityCritical]
			set
			{
				this.m_bootstrapContext = value;
			}
		}

		/// <summary>Gets the claims associated with this claims identity.</summary>
		/// <returns>The collection of claims associated with this claims identity.</returns>
		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06004502 RID: 17666 RVA: 0x000F2957 File Offset: 0x000F0B57
		public virtual IEnumerable<Claim> Claims
		{
			get
			{
				int num;
				for (int i = 0; i < this.m_instanceClaims.Count; i = num + 1)
				{
					yield return this.m_instanceClaims[i];
					num = i;
				}
				if (this.m_externalClaims != null)
				{
					for (int j = 0; j < this.m_externalClaims.Count; j = num + 1)
					{
						if (this.m_externalClaims[j] != null)
						{
							foreach (Claim claim in this.m_externalClaims[j])
							{
								yield return claim;
							}
							IEnumerator<Claim> enumerator = null;
						}
						num = j;
					}
				}
				yield break;
				yield break;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06004503 RID: 17667 RVA: 0x000F2967 File Offset: 0x000F0B67
		protected virtual byte[] CustomSerializationData
		{
			get
			{
				return this.m_userSerializationData;
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06004504 RID: 17668 RVA: 0x000F296F File Offset: 0x000F0B6F
		internal Collection<IEnumerable<Claim>> ExternalClaims
		{
			[FriendAccessAllowed]
			get
			{
				return this.m_externalClaims;
			}
		}

		/// <summary>Gets or sets the label for this claims identity.</summary>
		/// <returns>The label.</returns>
		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06004505 RID: 17669 RVA: 0x000F2977 File Offset: 0x000F0B77
		// (set) Token: 0x06004506 RID: 17670 RVA: 0x000F297F File Offset: 0x000F0B7F
		public string Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		/// <summary>Gets the name of this claims identity.</summary>
		/// <returns>The name or null.</returns>
		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06004507 RID: 17671 RVA: 0x000F2988 File Offset: 0x000F0B88
		public virtual string Name
		{
			get
			{
				Claim claim = this.FindFirst(this.m_nameType);
				if (claim != null)
				{
					return claim.Value;
				}
				return null;
			}
		}

		/// <summary>Gets the claim type that is used to determine which claims provide the value for the <see cref="P:System.Security.Claims.ClaimsIdentity.Name" /> property of this claims identity.</summary>
		/// <returns>The name claim type.</returns>
		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06004508 RID: 17672 RVA: 0x000F29AD File Offset: 0x000F0BAD
		public string NameClaimType
		{
			get
			{
				return this.m_nameType;
			}
		}

		/// <summary>Gets the claim type that will be interpreted as a .NET Framework role among the claims in this claims identity.</summary>
		/// <returns>The role claim type.</returns>
		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06004509 RID: 17673 RVA: 0x000F29B5 File Offset: 0x000F0BB5
		public string RoleClaimType
		{
			get
			{
				return this.m_roleType;
			}
		}

		/// <summary>Returns a new <see cref="T:System.Security.Claims.ClaimsIdentity" /> copied from this claims identity.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x0600450A RID: 17674 RVA: 0x000F29C0 File Offset: 0x000F0BC0
		public virtual ClaimsIdentity Clone()
		{
			ClaimsIdentity claimsIdentity = new ClaimsIdentity(this.m_instanceClaims);
			claimsIdentity.m_authenticationType = this.m_authenticationType;
			claimsIdentity.m_bootstrapContext = this.m_bootstrapContext;
			claimsIdentity.m_label = this.m_label;
			claimsIdentity.m_nameType = this.m_nameType;
			claimsIdentity.m_roleType = this.m_roleType;
			if (this.Actor != null)
			{
				if (this.IsCircular(this.Actor))
				{
					throw new InvalidOperationException(Environment.GetResourceString("Actor cannot be set so that circular directed graph will exist chaining the subjects together."));
				}
				if (!AppContextSwitches.SetActorAsReferenceWhenCopyingClaimsIdentity)
				{
					claimsIdentity.Actor = this.Actor.Clone();
				}
				else
				{
					claimsIdentity.Actor = this.Actor;
				}
			}
			return claimsIdentity;
		}

		/// <summary>Adds a single claim to this claims identity.</summary>
		/// <param name="claim">The claim to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="claim" /> is null.</exception>
		// Token: 0x0600450B RID: 17675 RVA: 0x000F2A64 File Offset: 0x000F0C64
		[SecurityCritical]
		public virtual void AddClaim(Claim claim)
		{
			if (claim == null)
			{
				throw new ArgumentNullException("claim");
			}
			if (claim.Subject == this)
			{
				this.m_instanceClaims.Add(claim);
				return;
			}
			this.m_instanceClaims.Add(claim.Clone(this));
		}

		/// <summary>Adds a list of claims to this claims identity.</summary>
		/// <param name="claims">The claims to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="claims" /> is null.</exception>
		// Token: 0x0600450C RID: 17676 RVA: 0x000F2A9C File Offset: 0x000F0C9C
		[SecurityCritical]
		public virtual void AddClaims(IEnumerable<Claim> claims)
		{
			if (claims == null)
			{
				throw new ArgumentNullException("claims");
			}
			foreach (Claim claim in claims)
			{
				if (claim != null)
				{
					this.AddClaim(claim);
				}
			}
		}

		/// <summary>Attempts to remove a claim from the claims identity.</summary>
		/// <returns>true if the claim was successfully removed; otherwise, false.</returns>
		/// <param name="claim">The claim to remove.</param>
		// Token: 0x0600450D RID: 17677 RVA: 0x000F2AF8 File Offset: 0x000F0CF8
		[SecurityCritical]
		public virtual bool TryRemoveClaim(Claim claim)
		{
			bool flag = false;
			for (int i = 0; i < this.m_instanceClaims.Count; i++)
			{
				if (this.m_instanceClaims[i] == claim)
				{
					this.m_instanceClaims.RemoveAt(i);
					flag = true;
					break;
				}
			}
			return flag;
		}

		/// <summary>Attempts to remove a claim from the claims identity.</summary>
		/// <param name="claim">The claim to remove.</param>
		/// <exception cref="T:System.InvalidOperationException">The claim cannot be removed.</exception>
		// Token: 0x0600450E RID: 17678 RVA: 0x000F2B3D File Offset: 0x000F0D3D
		[SecurityCritical]
		public virtual void RemoveClaim(Claim claim)
		{
			if (!this.TryRemoveClaim(claim))
			{
				throw new InvalidOperationException(Environment.GetResourceString("The Claim '{0}' was not able to be removed.  It is either not part of this Identity or it is a claim that is owned by the Principal that contains this Identity. For example, the Principal will own the claim when creating a GenericPrincipal with roles. The roles will be exposed through the Identity that is passed in the constructor, but not actually owned by the Identity.  Similar logic exists for a RolePrincipal.", new object[] { claim }));
			}
		}

		// Token: 0x0600450F RID: 17679 RVA: 0x000F2B64 File Offset: 0x000F0D64
		[SecuritySafeCritical]
		private void SafeAddClaims(IEnumerable<Claim> claims)
		{
			foreach (Claim claim in claims)
			{
				if (claim.Subject == this)
				{
					this.m_instanceClaims.Add(claim);
				}
				else
				{
					this.m_instanceClaims.Add(claim.Clone(this));
				}
			}
		}

		// Token: 0x06004510 RID: 17680 RVA: 0x000F2BD0 File Offset: 0x000F0DD0
		[SecuritySafeCritical]
		private void SafeAddClaim(Claim claim)
		{
			if (claim.Subject == this)
			{
				this.m_instanceClaims.Add(claim);
				return;
			}
			this.m_instanceClaims.Add(claim.Clone(this));
		}

		/// <summary>Retrieves all of the claims that are matched by the specified predicate.</summary>
		/// <returns>The matching claims. The list is read-only.</returns>
		/// <param name="match">The function that performs the matching logic.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is null.</exception>
		// Token: 0x06004511 RID: 17681 RVA: 0x000F2BFC File Offset: 0x000F0DFC
		public virtual IEnumerable<Claim> FindAll(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			List<Claim> list = new List<Claim>();
			foreach (Claim claim in this.Claims)
			{
				if (match(claim))
				{
					list.Add(claim);
				}
			}
			return list.AsReadOnly();
		}

		/// <summary>Retrieves all of the claims that have the specified claim type.</summary>
		/// <returns>The matching claims. The list is read-only.</returns>
		/// <param name="type">The claim type against which to match claims.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x06004512 RID: 17682 RVA: 0x000F2C6C File Offset: 0x000F0E6C
		public virtual IEnumerable<Claim> FindAll(string type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			List<Claim> list = new List<Claim>();
			foreach (Claim claim in this.Claims)
			{
				if (claim != null && string.Equals(claim.Type, type, StringComparison.OrdinalIgnoreCase))
				{
					list.Add(claim);
				}
			}
			return list.AsReadOnly();
		}

		/// <summary>Determines whether this claims identity has a claim that is matched by the specified predicate.</summary>
		/// <returns>true if a matching claim exists; otherwise, false.</returns>
		/// <param name="match">The function that performs the matching logic.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is null.</exception>
		// Token: 0x06004513 RID: 17683 RVA: 0x000F2CE8 File Offset: 0x000F0EE8
		public virtual bool HasClaim(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			foreach (Claim claim in this.Claims)
			{
				if (match(claim))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether this claims identity has a claim with the specified claim type and value.</summary>
		/// <returns>true if a match is found; otherwise, false.</returns>
		/// <param name="type">The type of the claim to match.</param>
		/// <param name="value">The value of the claim to match.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.-or-<paramref name="value" /> is null.</exception>
		// Token: 0x06004514 RID: 17684 RVA: 0x000F2D4C File Offset: 0x000F0F4C
		public virtual bool HasClaim(string type, string value)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			foreach (Claim claim in this.Claims)
			{
				if (claim != null && claim != null && string.Equals(claim.Type, type, StringComparison.OrdinalIgnoreCase) && string.Equals(claim.Value, value, StringComparison.Ordinal))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Retrieves the first claim that is matched by the specified predicate.</summary>
		/// <returns>The first matching claim or null if no match is found.</returns>
		/// <param name="match">The function that performs the matching logic.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is null.</exception>
		// Token: 0x06004515 RID: 17685 RVA: 0x000F2DDC File Offset: 0x000F0FDC
		public virtual Claim FindFirst(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			foreach (Claim claim in this.Claims)
			{
				if (match(claim))
				{
					return claim;
				}
			}
			return null;
		}

		/// <summary>Retrieves the first claim with the specified claim type.</summary>
		/// <returns>The first matching claim or null if no match is found.</returns>
		/// <param name="type">The claim type to match.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x06004516 RID: 17686 RVA: 0x000F2E40 File Offset: 0x000F1040
		public virtual Claim FindFirst(string type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			foreach (Claim claim in this.Claims)
			{
				if (claim != null && string.Equals(claim.Type, type, StringComparison.OrdinalIgnoreCase))
				{
					return claim;
				}
			}
			return null;
		}

		// Token: 0x06004517 RID: 17687 RVA: 0x000F2EB0 File Offset: 0x000F10B0
		[OnSerializing]
		[SecurityCritical]
		private void OnSerializingMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			this.m_serializedClaims = this.SerializeClaims();
			this.m_serializedNameType = this.m_nameType;
			this.m_serializedRoleType = this.m_roleType;
		}

		// Token: 0x06004518 RID: 17688 RVA: 0x000F2EE0 File Offset: 0x000F10E0
		[OnDeserialized]
		[SecurityCritical]
		private void OnDeserializedMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.m_serializedClaims))
			{
				this.DeserializeClaims(this.m_serializedClaims);
				this.m_serializedClaims = null;
			}
			this.m_nameType = (string.IsNullOrEmpty(this.m_serializedNameType) ? "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name" : this.m_serializedNameType);
			this.m_roleType = (string.IsNullOrEmpty(this.m_serializedRoleType) ? "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" : this.m_serializedRoleType);
		}

		// Token: 0x06004519 RID: 17689 RVA: 0x000F2F56 File Offset: 0x000F1156
		[OnDeserializing]
		private void OnDeserializingMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
		}

		/// <summary>Populates the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with data needed to serialize the current <see cref="T:System.Security.Claims.ClaimsIdentity" /> object.</summary>
		/// <param name="info">The object to populate with data.</param>
		/// <param name="context">The destination for this serialization. Can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x0600451A RID: 17690 RVA: 0x000F2F78 File Offset: 0x000F1178
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			info.AddValue("System.Security.ClaimsIdentity.version", this.m_version);
			if (!string.IsNullOrEmpty(this.m_authenticationType))
			{
				info.AddValue("System.Security.ClaimsIdentity.authenticationType", this.m_authenticationType);
			}
			info.AddValue("System.Security.ClaimsIdentity.nameClaimType", this.m_nameType);
			info.AddValue("System.Security.ClaimsIdentity.roleClaimType", this.m_roleType);
			if (!string.IsNullOrEmpty(this.m_label))
			{
				info.AddValue("System.Security.ClaimsIdentity.label", this.m_label);
			}
			if (this.m_actor != null)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					binaryFormatter.Serialize(memoryStream, this.m_actor, null, false);
					info.AddValue("System.Security.ClaimsIdentity.actor", Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length));
				}
			}
			info.AddValue("System.Security.ClaimsIdentity.claims", this.SerializeClaims());
			if (this.m_bootstrapContext != null)
			{
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					binaryFormatter.Serialize(memoryStream2, this.m_bootstrapContext, null, false);
					info.AddValue("System.Security.ClaimsIdentity.bootstrapContext", Convert.ToBase64String(memoryStream2.GetBuffer(), 0, (int)memoryStream2.Length));
				}
			}
		}

		// Token: 0x0600451B RID: 17691 RVA: 0x000F30C4 File Offset: 0x000F12C4
		[SecurityCritical]
		private void DeserializeClaims(string serializedClaims)
		{
			if (!string.IsNullOrEmpty(serializedClaims))
			{
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(serializedClaims)))
				{
					this.m_instanceClaims = (List<Claim>)new BinaryFormatter().Deserialize(memoryStream, null, false);
					for (int i = 0; i < this.m_instanceClaims.Count; i++)
					{
						this.m_instanceClaims[i].Subject = this;
					}
				}
			}
			if (this.m_instanceClaims == null)
			{
				this.m_instanceClaims = new List<Claim>();
			}
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x000F3154 File Offset: 0x000F1354
		[SecurityCritical]
		private string SerializeClaims()
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new BinaryFormatter().Serialize(memoryStream, this.m_instanceClaims, null, false);
				text = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
			}
			return text;
		}

		// Token: 0x0600451D RID: 17693 RVA: 0x000F31AC File Offset: 0x000F13AC
		private bool IsCircular(ClaimsIdentity subject)
		{
			if (this == subject)
			{
				return true;
			}
			ClaimsIdentity claimsIdentity = subject;
			while (claimsIdentity.Actor != null)
			{
				if (this == claimsIdentity.Actor)
				{
					return true;
				}
				claimsIdentity = claimsIdentity.Actor;
			}
			return false;
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x000F31E0 File Offset: 0x000F13E0
		private void Initialize(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			int num = reader.ReadInt32();
			if ((num & 1) == 1)
			{
				this.m_authenticationType = reader.ReadString();
			}
			if ((num & 2) == 2)
			{
				this.m_bootstrapContext = reader.ReadString();
			}
			if ((num & 4) == 4)
			{
				this.m_nameType = reader.ReadString();
			}
			else
			{
				this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			}
			if ((num & 8) == 8)
			{
				this.m_roleType = reader.ReadString();
			}
			else
			{
				this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			}
			if ((num & 16) == 16)
			{
				int num2 = reader.ReadInt32();
				for (int i = 0; i < num2; i++)
				{
					Claim claim = new Claim(reader, this);
					this.m_instanceClaims.Add(claim);
				}
			}
		}

		// Token: 0x0600451F RID: 17695 RVA: 0x000F3291 File Offset: 0x000F1491
		protected virtual Claim CreateClaim(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return new Claim(reader, this);
		}

		// Token: 0x06004520 RID: 17696 RVA: 0x000F32A8 File Offset: 0x000F14A8
		public virtual void WriteTo(BinaryWriter writer)
		{
			this.WriteTo(writer, null);
		}

		// Token: 0x06004521 RID: 17697 RVA: 0x000F32B4 File Offset: 0x000F14B4
		protected virtual void WriteTo(BinaryWriter writer, byte[] userData)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			int num = 0;
			ClaimsIdentity.SerializationMask serializationMask = ClaimsIdentity.SerializationMask.None;
			if (this.m_authenticationType != null)
			{
				serializationMask |= ClaimsIdentity.SerializationMask.AuthenticationType;
				num++;
			}
			if (this.m_bootstrapContext != null && this.m_bootstrapContext is string)
			{
				serializationMask |= ClaimsIdentity.SerializationMask.BootstrapConext;
				num++;
			}
			if (!string.Equals(this.m_nameType, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", StringComparison.Ordinal))
			{
				serializationMask |= ClaimsIdentity.SerializationMask.NameClaimType;
				num++;
			}
			if (!string.Equals(this.m_roleType, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", StringComparison.Ordinal))
			{
				serializationMask |= ClaimsIdentity.SerializationMask.RoleClaimType;
				num++;
			}
			if (!string.IsNullOrWhiteSpace(this.m_label))
			{
				serializationMask |= ClaimsIdentity.SerializationMask.HasLabel;
				num++;
			}
			if (this.m_instanceClaims.Count > 0)
			{
				serializationMask |= ClaimsIdentity.SerializationMask.HasClaims;
				num++;
			}
			if (this.m_actor != null)
			{
				serializationMask |= ClaimsIdentity.SerializationMask.Actor;
				num++;
			}
			if (userData != null && userData.Length != 0)
			{
				num++;
				serializationMask |= ClaimsIdentity.SerializationMask.UserData;
			}
			writer.Write((int)serializationMask);
			writer.Write(num);
			if ((serializationMask & ClaimsIdentity.SerializationMask.AuthenticationType) == ClaimsIdentity.SerializationMask.AuthenticationType)
			{
				writer.Write(this.m_authenticationType);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.BootstrapConext) == ClaimsIdentity.SerializationMask.BootstrapConext)
			{
				writer.Write(this.m_bootstrapContext as string);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.NameClaimType) == ClaimsIdentity.SerializationMask.NameClaimType)
			{
				writer.Write(this.m_nameType);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.RoleClaimType) == ClaimsIdentity.SerializationMask.RoleClaimType)
			{
				writer.Write(this.m_roleType);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.HasLabel) == ClaimsIdentity.SerializationMask.HasLabel)
			{
				writer.Write(this.m_label);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.HasClaims) == ClaimsIdentity.SerializationMask.HasClaims)
			{
				writer.Write(this.m_instanceClaims.Count);
				foreach (Claim claim in this.m_instanceClaims)
				{
					claim.WriteTo(writer);
				}
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.Actor) == ClaimsIdentity.SerializationMask.Actor)
			{
				this.m_actor.WriteTo(writer);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.UserData) == ClaimsIdentity.SerializationMask.UserData)
			{
				writer.Write(userData.Length);
				writer.Write(userData);
			}
			writer.Flush();
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x000F348C File Offset: 0x000F168C
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		private void Deserialize(SerializationInfo info, StreamingContext context, bool useContext)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			BinaryFormatter binaryFormatter;
			if (useContext)
			{
				binaryFormatter = new BinaryFormatter(null, context);
			}
			else
			{
				binaryFormatter = new BinaryFormatter();
			}
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num <= 959168042U)
				{
					if (num <= 623923795U)
					{
						if (num != 373632733U)
						{
							if (num == 623923795U)
							{
								if (name == "System.Security.ClaimsIdentity.roleClaimType")
								{
									this.m_roleType = info.GetString("System.Security.ClaimsIdentity.roleClaimType");
								}
							}
						}
						else if (name == "System.Security.ClaimsIdentity.label")
						{
							this.m_label = info.GetString("System.Security.ClaimsIdentity.label");
						}
					}
					else if (num != 656336169U)
					{
						if (num == 959168042U)
						{
							if (name == "System.Security.ClaimsIdentity.nameClaimType")
							{
								this.m_nameType = info.GetString("System.Security.ClaimsIdentity.nameClaimType");
							}
						}
					}
					else if (name == "System.Security.ClaimsIdentity.authenticationType")
					{
						this.m_authenticationType = info.GetString("System.Security.ClaimsIdentity.authenticationType");
					}
				}
				else if (num <= 1476368026U)
				{
					if (num != 1453716852U)
					{
						if (num != 1476368026U)
						{
							continue;
						}
						if (!(name == "System.Security.ClaimsIdentity.actor"))
						{
							continue;
						}
						using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(info.GetString("System.Security.ClaimsIdentity.actor"))))
						{
							this.m_actor = (ClaimsIdentity)binaryFormatter.Deserialize(memoryStream, null, false);
							continue;
						}
					}
					else if (!(name == "System.Security.ClaimsIdentity.claims"))
					{
						continue;
					}
					this.DeserializeClaims(info.GetString("System.Security.ClaimsIdentity.claims"));
				}
				else if (num != 2480284791U)
				{
					if (num == 3659022112U)
					{
						if (name == "System.Security.ClaimsIdentity.bootstrapContext")
						{
							using (MemoryStream memoryStream2 = new MemoryStream(Convert.FromBase64String(info.GetString("System.Security.ClaimsIdentity.bootstrapContext"))))
							{
								this.m_bootstrapContext = binaryFormatter.Deserialize(memoryStream2, null, false);
							}
						}
					}
				}
				else if (name == "System.Security.ClaimsIdentity.version")
				{
					info.GetString("System.Security.ClaimsIdentity.version");
				}
			}
		}

		// Token: 0x04002376 RID: 9078
		[NonSerialized]
		private byte[] m_userSerializationData;

		// Token: 0x04002377 RID: 9079
		[NonSerialized]
		private const string PreFix = "System.Security.ClaimsIdentity.";

		// Token: 0x04002378 RID: 9080
		[NonSerialized]
		private const string ActorKey = "System.Security.ClaimsIdentity.actor";

		// Token: 0x04002379 RID: 9081
		[NonSerialized]
		private const string AuthenticationTypeKey = "System.Security.ClaimsIdentity.authenticationType";

		// Token: 0x0400237A RID: 9082
		[NonSerialized]
		private const string BootstrapContextKey = "System.Security.ClaimsIdentity.bootstrapContext";

		// Token: 0x0400237B RID: 9083
		[NonSerialized]
		private const string ClaimsKey = "System.Security.ClaimsIdentity.claims";

		// Token: 0x0400237C RID: 9084
		[NonSerialized]
		private const string LabelKey = "System.Security.ClaimsIdentity.label";

		// Token: 0x0400237D RID: 9085
		[NonSerialized]
		private const string NameClaimTypeKey = "System.Security.ClaimsIdentity.nameClaimType";

		// Token: 0x0400237E RID: 9086
		[NonSerialized]
		private const string RoleClaimTypeKey = "System.Security.ClaimsIdentity.roleClaimType";

		// Token: 0x0400237F RID: 9087
		[NonSerialized]
		private const string VersionKey = "System.Security.ClaimsIdentity.version";

		/// <summary>The default issuer; “LOCAL AUTHORITY”.</summary>
		// Token: 0x04002380 RID: 9088
		[NonSerialized]
		public const string DefaultIssuer = "LOCAL AUTHORITY";

		/// <summary>The default name claim type; <see cref="F:System.Security.Claims.ClaimTypes.Name" />.</summary>
		// Token: 0x04002381 RID: 9089
		[NonSerialized]
		public const string DefaultNameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

		/// <summary>The default role claim type; <see cref="F:System.Security.Claims.ClaimTypes.Role" />.</summary>
		// Token: 0x04002382 RID: 9090
		[NonSerialized]
		public const string DefaultRoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

		// Token: 0x04002383 RID: 9091
		[NonSerialized]
		private List<Claim> m_instanceClaims;

		// Token: 0x04002384 RID: 9092
		[NonSerialized]
		private Collection<IEnumerable<Claim>> m_externalClaims;

		// Token: 0x04002385 RID: 9093
		[NonSerialized]
		private string m_nameType;

		// Token: 0x04002386 RID: 9094
		[NonSerialized]
		private string m_roleType;

		// Token: 0x04002387 RID: 9095
		[OptionalField(VersionAdded = 2)]
		private string m_version;

		// Token: 0x04002388 RID: 9096
		[OptionalField(VersionAdded = 2)]
		private ClaimsIdentity m_actor;

		// Token: 0x04002389 RID: 9097
		[OptionalField(VersionAdded = 2)]
		private string m_authenticationType;

		// Token: 0x0400238A RID: 9098
		[OptionalField(VersionAdded = 2)]
		private object m_bootstrapContext;

		// Token: 0x0400238B RID: 9099
		[OptionalField(VersionAdded = 2)]
		private string m_label;

		// Token: 0x0400238C RID: 9100
		[OptionalField(VersionAdded = 2)]
		private string m_serializedNameType;

		// Token: 0x0400238D RID: 9101
		[OptionalField(VersionAdded = 2)]
		private string m_serializedRoleType;

		// Token: 0x0400238E RID: 9102
		[OptionalField(VersionAdded = 2)]
		private string m_serializedClaims;

		// Token: 0x02000636 RID: 1590
		private enum SerializationMask
		{
			// Token: 0x04002390 RID: 9104
			None,
			// Token: 0x04002391 RID: 9105
			AuthenticationType,
			// Token: 0x04002392 RID: 9106
			BootstrapConext,
			// Token: 0x04002393 RID: 9107
			NameClaimType = 4,
			// Token: 0x04002394 RID: 9108
			RoleClaimType = 8,
			// Token: 0x04002395 RID: 9109
			HasClaims = 16,
			// Token: 0x04002396 RID: 9110
			HasLabel = 32,
			// Token: 0x04002397 RID: 9111
			Actor = 64,
			// Token: 0x04002398 RID: 9112
			UserData = 128
		}
	}
}
