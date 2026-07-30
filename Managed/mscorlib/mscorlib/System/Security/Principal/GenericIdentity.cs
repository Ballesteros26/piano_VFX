using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Claims;

namespace System.Security.Principal
{
	/// <summary>Represents a generic user.</summary>
	// Token: 0x0200061E RID: 1566
	[ComVisible(true)]
	[Serializable]
	public class GenericIdentity : ClaimsIdentity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.GenericIdentity" /> class representing the user with the specified name.</summary>
		/// <param name="name">The name of the user on whose behalf the code is running. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x0600442A RID: 17450 RVA: 0x000EFC6A File Offset: 0x000EDE6A
		[SecuritySafeCritical]
		public GenericIdentity(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.m_name = name;
			this.m_type = "";
			this.AddNameClaim();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.GenericIdentity" /> class representing the user with the specified name and authentication type.</summary>
		/// <param name="name">The name of the user on whose behalf the code is running. </param>
		/// <param name="type">The type of authentication used to identify the user. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null.-or- The <paramref name="type" /> parameter is null. </exception>
		// Token: 0x0600442B RID: 17451 RVA: 0x000EFC98 File Offset: 0x000EDE98
		[SecuritySafeCritical]
		public GenericIdentity(string name, string type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.m_name = name;
			this.m_type = type;
			this.AddNameClaim();
		}

		// Token: 0x0600442C RID: 17452 RVA: 0x000EFCD0 File Offset: 0x000EDED0
		private GenericIdentity()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.GenericIdentity" /> class by using the specified <see cref="T:System.Security.Principal.GenericIdentity" /> object.</summary>
		/// <param name="identity">The object from which to construct the new instance of <see cref="T:System.Security.Principal.GenericIdentity" />.</param>
		// Token: 0x0600442D RID: 17453 RVA: 0x000EFCD8 File Offset: 0x000EDED8
		protected GenericIdentity(GenericIdentity identity)
			: base(identity)
		{
			this.m_name = identity.m_name;
			this.m_type = identity.m_type;
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x0600442E RID: 17454 RVA: 0x000EFCF9 File Offset: 0x000EDEF9
		public override ClaimsIdentity Clone()
		{
			return new GenericIdentity(this);
		}

		/// <summary>Gets all claims for the user represented by this generic identity.</summary>
		/// <returns>A collection of claims for this <see cref="T:System.Security.Principal.GenericIdentity" /> object.</returns>
		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x0600442F RID: 17455 RVA: 0x000EFD01 File Offset: 0x000EDF01
		public override IEnumerable<Claim> Claims
		{
			get
			{
				return base.Claims;
			}
		}

		/// <summary>Gets the user's name.</summary>
		/// <returns>The name of the user on whose behalf the code is being run.</returns>
		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06004430 RID: 17456 RVA: 0x000EFD09 File Offset: 0x000EDF09
		public override string Name
		{
			get
			{
				return this.m_name;
			}
		}

		/// <summary>Gets the type of authentication used to identify the user.</summary>
		/// <returns>The type of authentication used to identify the user.</returns>
		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06004431 RID: 17457 RVA: 0x000EFD11 File Offset: 0x000EDF11
		public override string AuthenticationType
		{
			get
			{
				return this.m_type;
			}
		}

		/// <summary>Gets a value indicating whether the user has been authenticated.</summary>
		/// <returns>true if the user was has been authenticated; otherwise, false.</returns>
		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06004432 RID: 17458 RVA: 0x000EFD19 File Offset: 0x000EDF19
		public override bool IsAuthenticated
		{
			get
			{
				return !this.m_name.Equals("");
			}
		}

		// Token: 0x06004433 RID: 17459 RVA: 0x000EFD30 File Offset: 0x000EDF30
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			bool flag = false;
			using (IEnumerator<Claim> enumerator = base.Claims.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					Claim claim = enumerator.Current;
					flag = true;
				}
			}
			if (!flag)
			{
				this.AddNameClaim();
			}
		}

		// Token: 0x06004434 RID: 17460 RVA: 0x000EFD88 File Offset: 0x000EDF88
		[SecuritySafeCritical]
		private void AddNameClaim()
		{
			if (this.m_name != null)
			{
				base.AddClaim(new Claim(base.NameClaimType, this.m_name, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", this));
			}
		}

		// Token: 0x04002265 RID: 8805
		private string m_name;

		// Token: 0x04002266 RID: 8806
		private string m_type;
	}
}
