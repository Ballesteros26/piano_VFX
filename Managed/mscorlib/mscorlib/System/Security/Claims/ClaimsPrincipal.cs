using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Security.Claims
{
	/// <summary>An <see cref="T:System.Security.Principal.IPrincipal" /> implementation that supports multiple claims-based identities.</summary>
	// Token: 0x02000638 RID: 1592
	[ComVisible(true)]
	[Serializable]
	public class ClaimsPrincipal : IPrincipal
	{
		// Token: 0x0600452C RID: 17708 RVA: 0x000F3944 File Offset: 0x000F1B44
		private static ClaimsIdentity SelectPrimaryIdentity(IEnumerable<ClaimsIdentity> identities)
		{
			if (identities == null)
			{
				throw new ArgumentNullException("identities");
			}
			ClaimsIdentity claimsIdentity = null;
			foreach (ClaimsIdentity claimsIdentity2 in identities)
			{
				if (claimsIdentity2 is WindowsIdentity)
				{
					claimsIdentity = claimsIdentity2;
					break;
				}
				if (claimsIdentity == null)
				{
					claimsIdentity = claimsIdentity2;
				}
			}
			return claimsIdentity;
		}

		// Token: 0x0600452D RID: 17709 RVA: 0x000F39A8 File Offset: 0x000F1BA8
		private static ClaimsPrincipal SelectClaimsPrincipal()
		{
			ClaimsPrincipal claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
			if (claimsPrincipal != null)
			{
				return claimsPrincipal;
			}
			return new ClaimsPrincipal(Thread.CurrentPrincipal);
		}

		/// <summary>Gets and sets the delegate used to select the claims identity returned by the <see cref="P:System.Security.Claims.ClaimsPrincipal.Identity" /> property.</summary>
		/// <returns>The delegate. The default is null.</returns>
		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x0600452E RID: 17710 RVA: 0x000F39CF File Offset: 0x000F1BCF
		// (set) Token: 0x0600452F RID: 17711 RVA: 0x000F39D6 File Offset: 0x000F1BD6
		public static Func<IEnumerable<ClaimsIdentity>, ClaimsIdentity> PrimaryIdentitySelector
		{
			get
			{
				return ClaimsPrincipal.s_identitySelector;
			}
			[SecurityCritical]
			set
			{
				ClaimsPrincipal.s_identitySelector = value;
			}
		}

		/// <summary>Gets and sets the delegate used to select the claims principal returned by the <see cref="P:System.Security.Claims.ClaimsPrincipal.Current" /> property.</summary>
		/// <returns>The delegate. The default is null.</returns>
		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06004530 RID: 17712 RVA: 0x000F39DE File Offset: 0x000F1BDE
		// (set) Token: 0x06004531 RID: 17713 RVA: 0x000F39E5 File Offset: 0x000F1BE5
		public static Func<ClaimsPrincipal> ClaimsPrincipalSelector
		{
			get
			{
				return ClaimsPrincipal.s_principalSelector;
			}
			[SecurityCritical]
			set
			{
				ClaimsPrincipal.s_principalSelector = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsPrincipal" /> class.</summary>
		// Token: 0x06004532 RID: 17714 RVA: 0x000F39ED File Offset: 0x000F1BED
		public ClaimsPrincipal()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsPrincipal" /> class using the specified claims identities.</summary>
		/// <param name="identities">The identities from which to initialize the new claims principal.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identities" /> is null.</exception>
		// Token: 0x06004533 RID: 17715 RVA: 0x000F3A0B File Offset: 0x000F1C0B
		public ClaimsPrincipal(IEnumerable<ClaimsIdentity> identities)
		{
			if (identities == null)
			{
				throw new ArgumentNullException("identities");
			}
			this.m_identities.AddRange(identities);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsPrincipal" /> class from the specified identity.</summary>
		/// <param name="identity">The identity from which to initialize the new claims principal.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null.</exception>
		// Token: 0x06004534 RID: 17716 RVA: 0x000F3A44 File Offset: 0x000F1C44
		public ClaimsPrincipal(IIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
			if (claimsIdentity != null)
			{
				this.m_identities.Add(claimsIdentity);
				return;
			}
			this.m_identities.Add(new ClaimsIdentity(identity));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsPrincipal" /> class from the specified principal.</summary>
		/// <param name="principal">The principal from which to initialize the new claims principal.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="principal" /> is null.</exception>
		// Token: 0x06004535 RID: 17717 RVA: 0x000F3AA4 File Offset: 0x000F1CA4
		public ClaimsPrincipal(IPrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			ClaimsPrincipal claimsPrincipal = principal as ClaimsPrincipal;
			if (claimsPrincipal == null)
			{
				this.m_identities.Add(new ClaimsIdentity(principal.Identity));
				return;
			}
			if (claimsPrincipal.Identities != null)
			{
				this.m_identities.AddRange(claimsPrincipal.Identities);
			}
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x000F3B15 File Offset: 0x000F1D15
		public ClaimsPrincipal(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Initialize(reader);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsPrincipal" /> class from a serialized stream created by using <see cref="T:System.Runtime.Serialization.ISerializable" />.</summary>
		/// <param name="info">The serialized data.</param>
		/// <param name="context">The context for serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06004537 RID: 17719 RVA: 0x000F3B48 File Offset: 0x000F1D48
		[SecurityCritical]
		protected ClaimsPrincipal(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.Deserialize(info, context);
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06004538 RID: 17720 RVA: 0x000F3B7C File Offset: 0x000F1D7C
		protected virtual byte[] CustomSerializationData
		{
			get
			{
				return this.m_userSerializationData;
			}
		}

		// Token: 0x06004539 RID: 17721 RVA: 0x000F3B84 File Offset: 0x000F1D84
		public virtual ClaimsPrincipal Clone()
		{
			return new ClaimsPrincipal(this);
		}

		// Token: 0x0600453A RID: 17722 RVA: 0x000F3B8C File Offset: 0x000F1D8C
		protected virtual ClaimsIdentity CreateClaimsIdentity(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return new ClaimsIdentity(reader);
		}

		// Token: 0x0600453B RID: 17723 RVA: 0x000F3BA2 File Offset: 0x000F1DA2
		[OnSerializing]
		[SecurityCritical]
		private void OnSerializingMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			this.m_serializedClaimsIdentities = this.SerializeIdentities();
		}

		// Token: 0x0600453C RID: 17724 RVA: 0x000F3BB9 File Offset: 0x000F1DB9
		[SecurityCritical]
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			this.DeserializeIdentities(this.m_serializedClaimsIdentities);
			this.m_serializedClaimsIdentities = null;
		}

		/// <summary>Populates the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with data needed to serialize the current <see cref="T:System.Security.Claims.ClaimsPrincipal" /> object.</summary>
		/// <param name="info">The object to populate with data.</param>
		/// <param name="context">The destination for this serialization. Can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x0600453D RID: 17725 RVA: 0x000F3BD7 File Offset: 0x000F1DD7
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("System.Security.ClaimsPrincipal.Identities", this.SerializeIdentities());
			info.AddValue("System.Security.ClaimsPrincipal.Version", this.m_version);
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x000F3C0C File Offset: 0x000F1E0C
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		private void Deserialize(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "System.Security.ClaimsPrincipal.Identities"))
				{
					if (name == "System.Security.ClaimsPrincipal.Version")
					{
						this.m_version = info.GetString("System.Security.ClaimsPrincipal.Version");
					}
				}
				else
				{
					this.DeserializeIdentities(info.GetString("System.Security.ClaimsPrincipal.Identities"));
				}
			}
		}

		// Token: 0x0600453F RID: 17727 RVA: 0x000F3C80 File Offset: 0x000F1E80
		[SecurityCritical]
		private void DeserializeIdentities(string identities)
		{
			this.m_identities = new List<ClaimsIdentity>();
			if (!string.IsNullOrEmpty(identities))
			{
				List<string> list = null;
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(identities)))
				{
					list = (List<string>)binaryFormatter.Deserialize(memoryStream, null, false);
					for (int i = 0; i < list.Count; i += 2)
					{
						ClaimsIdentity claimsIdentity = null;
						using (MemoryStream memoryStream2 = new MemoryStream(Convert.FromBase64String(list[i + 1])))
						{
							claimsIdentity = (ClaimsIdentity)binaryFormatter.Deserialize(memoryStream2, null, false);
						}
						if (!string.IsNullOrEmpty(list[i]))
						{
							long num;
							if (!long.TryParse(list[i], NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num))
							{
								throw new SerializationException(Environment.GetResourceString("Invalid BinaryFormatter stream."));
							}
							claimsIdentity = new WindowsIdentity(claimsIdentity, new IntPtr(num));
						}
						this.m_identities.Add(claimsIdentity);
					}
				}
			}
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x000F3D90 File Offset: 0x000F1F90
		[SecurityCritical]
		private string SerializeIdentities()
		{
			List<string> list = new List<string>();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			foreach (ClaimsIdentity claimsIdentity in this.m_identities)
			{
				if (claimsIdentity.GetType() == typeof(WindowsIdentity))
				{
					WindowsIdentity windowsIdentity = claimsIdentity as WindowsIdentity;
					list.Add(windowsIdentity.GetTokenInternal().ToInt64().ToString(NumberFormatInfo.InvariantInfo));
					using (MemoryStream memoryStream = new MemoryStream())
					{
						binaryFormatter.Serialize(memoryStream, windowsIdentity.CloneAsBase(), null, false);
						list.Add(Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length));
						continue;
					}
				}
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					list.Add("");
					binaryFormatter.Serialize(memoryStream2, claimsIdentity, null, false);
					list.Add(Convert.ToBase64String(memoryStream2.GetBuffer(), 0, (int)memoryStream2.Length));
				}
			}
			string text;
			using (MemoryStream memoryStream3 = new MemoryStream())
			{
				binaryFormatter.Serialize(memoryStream3, list, null, false);
				text = Convert.ToBase64String(memoryStream3.GetBuffer(), 0, (int)memoryStream3.Length);
			}
			return text;
		}

		/// <summary>Adds the specified claims identity to this claims principal.</summary>
		/// <param name="identity">The claims identity to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null.</exception>
		// Token: 0x06004541 RID: 17729 RVA: 0x000F3F18 File Offset: 0x000F2118
		[SecurityCritical]
		public virtual void AddIdentity(ClaimsIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.m_identities.Add(identity);
		}

		/// <summary>Adds the specified claims identities to this claims principal.</summary>
		/// <param name="identities">The claims identities to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identities" /> is null.</exception>
		// Token: 0x06004542 RID: 17730 RVA: 0x000F3F34 File Offset: 0x000F2134
		[SecurityCritical]
		public virtual void AddIdentities(IEnumerable<ClaimsIdentity> identities)
		{
			if (identities == null)
			{
				throw new ArgumentNullException("identities");
			}
			this.m_identities.AddRange(identities);
		}

		/// <summary>Gets a collection that contains all of the claims from all of the claims identities associated with this claims principal.</summary>
		/// <returns>The claims associated with this principal.</returns>
		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06004543 RID: 17731 RVA: 0x000F3F50 File Offset: 0x000F2150
		public virtual IEnumerable<Claim> Claims
		{
			get
			{
				foreach (ClaimsIdentity claimsIdentity in this.Identities)
				{
					foreach (Claim claim in claimsIdentity.Claims)
					{
						yield return claim;
					}
					IEnumerator<Claim> enumerator2 = null;
				}
				IEnumerator<ClaimsIdentity> enumerator = null;
				yield break;
				yield break;
			}
		}

		/// <summary>Gets the current claims principal.</summary>
		/// <returns>The current claims principal.</returns>
		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06004544 RID: 17732 RVA: 0x000F3F60 File Offset: 0x000F2160
		public static ClaimsPrincipal Current
		{
			get
			{
				if (ClaimsPrincipal.s_principalSelector != null)
				{
					return ClaimsPrincipal.s_principalSelector();
				}
				return ClaimsPrincipal.SelectClaimsPrincipal();
			}
		}

		/// <summary>Retrieves all of the claims that are matched by the specified predicate.</summary>
		/// <returns>The matching claims.</returns>
		/// <param name="match">The function that performs the matching logic.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is null.</exception>
		// Token: 0x06004545 RID: 17733 RVA: 0x000F3F7C File Offset: 0x000F217C
		public virtual IEnumerable<Claim> FindAll(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			List<Claim> list = new List<Claim>();
			foreach (ClaimsIdentity claimsIdentity in this.Identities)
			{
				if (claimsIdentity != null)
				{
					foreach (Claim claim in claimsIdentity.FindAll(match))
					{
						list.Add(claim);
					}
				}
			}
			return list.AsReadOnly();
		}

		/// <summary>Retrieves all or the claims that have the specified claim type.</summary>
		/// <returns>The matching claims.</returns>
		/// <param name="type">The claim type against which to match claims.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x06004546 RID: 17734 RVA: 0x000F4020 File Offset: 0x000F2220
		public virtual IEnumerable<Claim> FindAll(string type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			List<Claim> list = new List<Claim>();
			foreach (ClaimsIdentity claimsIdentity in this.Identities)
			{
				if (claimsIdentity != null)
				{
					foreach (Claim claim in claimsIdentity.FindAll(type))
					{
						list.Add(claim);
					}
				}
			}
			return list.AsReadOnly();
		}

		/// <summary>Retrieves the first claim that is matched by the specified predicate.</summary>
		/// <returns>The first matching claim or null if no match is found.</returns>
		/// <param name="match">The function that performs the matching logic.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is null.</exception>
		// Token: 0x06004547 RID: 17735 RVA: 0x000F40C4 File Offset: 0x000F22C4
		public virtual Claim FindFirst(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			Claim claim = null;
			foreach (ClaimsIdentity claimsIdentity in this.Identities)
			{
				if (claimsIdentity != null)
				{
					claim = claimsIdentity.FindFirst(match);
					if (claim != null)
					{
						return claim;
					}
				}
			}
			return claim;
		}

		/// <summary>Retrieves the first claim with the specified claim type.</summary>
		/// <returns>The first matching claim or null if no match is found.</returns>
		/// <param name="type">The claim type to match.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x06004548 RID: 17736 RVA: 0x000F4130 File Offset: 0x000F2330
		public virtual Claim FindFirst(string type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Claim claim = null;
			for (int i = 0; i < this.m_identities.Count; i++)
			{
				if (this.m_identities[i] != null)
				{
					claim = this.m_identities[i].FindFirst(type);
					if (claim != null)
					{
						return claim;
					}
				}
			}
			return claim;
		}

		/// <summary>Determines whether any of the claims identities associated with this claims principal contains a claim that is matched by the specified predicate.</summary>
		/// <returns>true if a matching claim exists; otherwise, false.</returns>
		/// <param name="match">The function that performs the matching logic.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is null.</exception>
		// Token: 0x06004549 RID: 17737 RVA: 0x000F418C File Offset: 0x000F238C
		public virtual bool HasClaim(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = 0; i < this.m_identities.Count; i++)
			{
				if (this.m_identities[i] != null && this.m_identities[i].HasClaim(match))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether any of the claims identities associated with this claims principal contains a claim with the specified claim type and value.</summary>
		/// <returns>true if a matching claim exists; otherwise, false.</returns>
		/// <param name="type">The type of the claim to match.</param>
		/// <param name="value">The value of the claim to match.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.-or-<paramref name="value" /> is null.</exception>
		// Token: 0x0600454A RID: 17738 RVA: 0x000F41E4 File Offset: 0x000F23E4
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
			for (int i = 0; i < this.m_identities.Count; i++)
			{
				if (this.m_identities[i] != null && this.m_identities[i].HasClaim(type, value))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Gets a collection that contains all of the claims identities associated with this claims principal.</summary>
		/// <returns>The collection of claims identities.</returns>
		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x0600454B RID: 17739 RVA: 0x000F4249 File Offset: 0x000F2449
		public virtual IEnumerable<ClaimsIdentity> Identities
		{
			get
			{
				return this.m_identities.AsReadOnly();
			}
		}

		/// <summary>Gets the primary claims identity associated with this claims principal.</summary>
		/// <returns>The primary claims identity associated with this claims principal.</returns>
		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x0600454C RID: 17740 RVA: 0x000F4256 File Offset: 0x000F2456
		public virtual IIdentity Identity
		{
			get
			{
				if (ClaimsPrincipal.s_identitySelector != null)
				{
					return ClaimsPrincipal.s_identitySelector(this.m_identities);
				}
				return ClaimsPrincipal.SelectPrimaryIdentity(this.m_identities);
			}
		}

		/// <summary>Returns a value that indicates whether the entity (user) represented by this claims principal is in the specified role.</summary>
		/// <returns>true if claims principal is in the specified role; otherwise, false.</returns>
		/// <param name="role">The role for which to check.</param>
		// Token: 0x0600454D RID: 17741 RVA: 0x000F427C File Offset: 0x000F247C
		public virtual bool IsInRole(string role)
		{
			for (int i = 0; i < this.m_identities.Count; i++)
			{
				if (this.m_identities[i] != null && this.m_identities[i].HasClaim(this.m_identities[i].RoleClaimType, role))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600454E RID: 17742 RVA: 0x000F42D8 File Offset: 0x000F24D8
		private void Initialize(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			ClaimsPrincipal.SerializationMask serializationMask = (ClaimsPrincipal.SerializationMask)reader.ReadInt32();
			int num = reader.ReadInt32();
			int num2 = 0;
			if ((serializationMask & ClaimsPrincipal.SerializationMask.HasIdentities) == ClaimsPrincipal.SerializationMask.HasIdentities)
			{
				num2++;
				int num3 = reader.ReadInt32();
				for (int i = 0; i < num3; i++)
				{
					this.m_identities.Add(this.CreateClaimsIdentity(reader));
				}
			}
			if ((serializationMask & ClaimsPrincipal.SerializationMask.UserData) == ClaimsPrincipal.SerializationMask.UserData)
			{
				int num4 = reader.ReadInt32();
				this.m_userSerializationData = reader.ReadBytes(num4);
				num2++;
			}
			for (int j = num2; j < num; j++)
			{
				reader.ReadString();
			}
		}

		// Token: 0x0600454F RID: 17743 RVA: 0x000F436D File Offset: 0x000F256D
		public virtual void WriteTo(BinaryWriter writer)
		{
			this.WriteTo(writer, null);
		}

		// Token: 0x06004550 RID: 17744 RVA: 0x000F4378 File Offset: 0x000F2578
		protected virtual void WriteTo(BinaryWriter writer, byte[] userData)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			int num = 0;
			ClaimsPrincipal.SerializationMask serializationMask = ClaimsPrincipal.SerializationMask.None;
			if (this.m_identities.Count > 0)
			{
				serializationMask |= ClaimsPrincipal.SerializationMask.HasIdentities;
				num++;
			}
			if (userData != null && userData.Length != 0)
			{
				num++;
				serializationMask |= ClaimsPrincipal.SerializationMask.UserData;
			}
			writer.Write((int)serializationMask);
			writer.Write(num);
			if ((serializationMask & ClaimsPrincipal.SerializationMask.HasIdentities) == ClaimsPrincipal.SerializationMask.HasIdentities)
			{
				writer.Write(this.m_identities.Count);
				foreach (ClaimsIdentity claimsIdentity in this.m_identities)
				{
					claimsIdentity.WriteTo(writer);
				}
			}
			if ((serializationMask & ClaimsPrincipal.SerializationMask.UserData) == ClaimsPrincipal.SerializationMask.UserData)
			{
				writer.Write(userData.Length);
				writer.Write(userData);
			}
			writer.Flush();
		}

		// Token: 0x040023A0 RID: 9120
		[NonSerialized]
		private byte[] m_userSerializationData;

		// Token: 0x040023A1 RID: 9121
		[NonSerialized]
		private const string PreFix = "System.Security.ClaimsPrincipal.";

		// Token: 0x040023A2 RID: 9122
		[NonSerialized]
		private const string IdentitiesKey = "System.Security.ClaimsPrincipal.Identities";

		// Token: 0x040023A3 RID: 9123
		[NonSerialized]
		private const string VersionKey = "System.Security.ClaimsPrincipal.Version";

		// Token: 0x040023A4 RID: 9124
		[OptionalField(VersionAdded = 2)]
		private string m_version = "1.0";

		// Token: 0x040023A5 RID: 9125
		[OptionalField(VersionAdded = 2)]
		private string m_serializedClaimsIdentities;

		// Token: 0x040023A6 RID: 9126
		[NonSerialized]
		private List<ClaimsIdentity> m_identities = new List<ClaimsIdentity>();

		// Token: 0x040023A7 RID: 9127
		[NonSerialized]
		private static Func<IEnumerable<ClaimsIdentity>, ClaimsIdentity> s_identitySelector = new Func<IEnumerable<ClaimsIdentity>, ClaimsIdentity>(ClaimsPrincipal.SelectPrimaryIdentity);

		// Token: 0x040023A8 RID: 9128
		[NonSerialized]
		private static Func<ClaimsPrincipal> s_principalSelector = ClaimsPrincipal.ClaimsPrincipalSelector;

		// Token: 0x02000639 RID: 1593
		private enum SerializationMask
		{
			// Token: 0x040023AA RID: 9130
			None,
			// Token: 0x040023AB RID: 9131
			HasIdentities,
			// Token: 0x040023AC RID: 9132
			UserData
		}
	}
}
