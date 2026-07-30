using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;

namespace System.Security.Claims
{
	/// <summary>Represents a claim.</summary>
	// Token: 0x02000631 RID: 1585
	[Serializable]
	public class Claim
	{
		// Token: 0x060044D4 RID: 17620 RVA: 0x000F1DF8 File Offset: 0x000EFFF8
		public Claim(BinaryReader reader)
			: this(reader, null)
		{
		}

		// Token: 0x060044D5 RID: 17621 RVA: 0x000F1E02 File Offset: 0x000F0002
		public Claim(BinaryReader reader, ClaimsIdentity subject)
		{
			this.m_propertyLock = new object();
			base..ctor();
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Initialize(reader, subject);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class with the specified claim type, and value.</summary>
		/// <param name="type">The claim type.</param>
		/// <param name="value">The claim value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="value" /> is null.</exception>
		// Token: 0x060044D6 RID: 17622 RVA: 0x000F1E2B File Offset: 0x000F002B
		public Claim(string type, string value)
			: this(type, value, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class with the specified claim type, value, and value type.</summary>
		/// <param name="type">The claim type.</param>
		/// <param name="value">The claim value.</param>
		/// <param name="valueType">The claim value type. If this parameter is null, then <see cref="F:System.Security.Claims.ClaimValueTypes.String" /> is used.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="value" /> is null.</exception>
		// Token: 0x060044D7 RID: 17623 RVA: 0x000F1E45 File Offset: 0x000F0045
		public Claim(string type, string value, string valueType)
			: this(type, value, valueType, "LOCAL AUTHORITY", "LOCAL AUTHORITY", null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class with the specified claim type, value, value type, and issuer.</summary>
		/// <param name="type">The claim type.</param>
		/// <param name="value">The claim value.</param>
		/// <param name="valueType">The claim value type. If this parameter is null, then <see cref="F:System.Security.Claims.ClaimValueTypes.String" /> is used.</param>
		/// <param name="issuer">The claim issuer. If this parameter is empty or null, then <see cref="F:System.Security.Claims.ClaimsIdentity.DefaultIssuer" /> is used.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="value" /> is null.</exception>
		// Token: 0x060044D8 RID: 17624 RVA: 0x000F1E5B File Offset: 0x000F005B
		public Claim(string type, string value, string valueType, string issuer)
			: this(type, value, valueType, issuer, issuer, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class with the specified claim type, value, value type, issuer,  and original issuer.</summary>
		/// <param name="type">The claim type.</param>
		/// <param name="value">The claim value.</param>
		/// <param name="valueType">The claim value type. If this parameter is null, then <see cref="F:System.Security.Claims.ClaimValueTypes.String" /> is used.</param>
		/// <param name="issuer">The claim issuer. If this parameter is empty or null, then <see cref="F:System.Security.Claims.ClaimsIdentity.DefaultIssuer" /> is used.</param>
		/// <param name="originalIssuer">The original issuer of the claim. If this parameter is empty or null, then the <see cref="P:System.Security.Claims.Claim.OriginalIssuer" /> property is set to the value of the <see cref="P:System.Security.Claims.Claim.Issuer" /> property.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="value" /> is null.</exception>
		// Token: 0x060044D9 RID: 17625 RVA: 0x000F1E6B File Offset: 0x000F006B
		public Claim(string type, string value, string valueType, string issuer, string originalIssuer)
			: this(type, value, valueType, issuer, originalIssuer, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class with the specified claim type, value, value type, issuer, original issuer and subject.</summary>
		/// <param name="type">The claim type.</param>
		/// <param name="value">The claim value.</param>
		/// <param name="valueType">The claim value type. If this parameter is null, then <see cref="F:System.Security.Claims.ClaimValueTypes.String" /> is used.</param>
		/// <param name="issuer">The claim issuer. If this parameter is empty or null, then <see cref="F:System.Security.Claims.ClaimsIdentity.DefaultIssuer" /> is used.</param>
		/// <param name="originalIssuer">The original issuer of the claim. If this parameter is empty or null, then the <see cref="P:System.Security.Claims.Claim.OriginalIssuer" /> property is set to the value of the <see cref="P:System.Security.Claims.Claim.Issuer" /> property.</param>
		/// <param name="subject">The subject that this claim describes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="value" /> is null.</exception>
		// Token: 0x060044DA RID: 17626 RVA: 0x000F1E7C File Offset: 0x000F007C
		public Claim(string type, string value, string valueType, string issuer, string originalIssuer, ClaimsIdentity subject)
			: this(type, value, valueType, issuer, originalIssuer, subject, null, null)
		{
		}

		// Token: 0x060044DB RID: 17627 RVA: 0x000F1E9C File Offset: 0x000F009C
		internal Claim(string type, string value, string valueType, string issuer, string originalIssuer, ClaimsIdentity subject, string propertyKey, string propertyValue)
		{
			this.m_propertyLock = new object();
			base..ctor();
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_type = type;
			this.m_value = value;
			if (string.IsNullOrEmpty(valueType))
			{
				this.m_valueType = "http://www.w3.org/2001/XMLSchema#string";
			}
			else
			{
				this.m_valueType = valueType;
			}
			if (string.IsNullOrEmpty(issuer))
			{
				this.m_issuer = "LOCAL AUTHORITY";
			}
			else
			{
				this.m_issuer = issuer;
			}
			if (string.IsNullOrEmpty(originalIssuer))
			{
				this.m_originalIssuer = this.m_issuer;
			}
			else
			{
				this.m_originalIssuer = originalIssuer;
			}
			this.m_subject = subject;
			if (propertyKey != null)
			{
				this.Properties.Add(propertyKey, propertyValue);
			}
		}

		// Token: 0x060044DC RID: 17628 RVA: 0x000F1F58 File Offset: 0x000F0158
		protected Claim(Claim other)
			: this(other, (other == null) ? null : other.m_subject)
		{
		}

		// Token: 0x060044DD RID: 17629 RVA: 0x000F1F70 File Offset: 0x000F0170
		protected Claim(Claim other, ClaimsIdentity subject)
		{
			this.m_propertyLock = new object();
			base..ctor();
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.m_issuer = other.m_issuer;
			this.m_originalIssuer = other.m_originalIssuer;
			this.m_subject = subject;
			this.m_type = other.m_type;
			this.m_value = other.m_value;
			this.m_valueType = other.m_valueType;
			if (other.m_properties != null)
			{
				this.m_properties = new Dictionary<string, string>();
				foreach (string text in other.m_properties.Keys)
				{
					this.m_properties.Add(text, other.m_properties[text]);
				}
			}
			if (other.m_userSerializationData != null)
			{
				this.m_userSerializationData = other.m_userSerializationData.Clone() as byte[];
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x060044DE RID: 17630 RVA: 0x000F206C File Offset: 0x000F026C
		protected virtual byte[] CustomSerializationData
		{
			get
			{
				return this.m_userSerializationData;
			}
		}

		/// <summary>Gets the issuer of the claim.</summary>
		/// <returns>A name that refers to the issuer of the claim.</returns>
		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x060044DF RID: 17631 RVA: 0x000F2074 File Offset: 0x000F0274
		public string Issuer
		{
			get
			{
				return this.m_issuer;
			}
		}

		// Token: 0x060044E0 RID: 17632 RVA: 0x000F207C File Offset: 0x000F027C
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			this.m_propertyLock = new object();
		}

		/// <summary>Gets the original issuer of the claim. </summary>
		/// <returns>A name that refers to the original issuer of the claim.</returns>
		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x060044E1 RID: 17633 RVA: 0x000F2089 File Offset: 0x000F0289
		public string OriginalIssuer
		{
			get
			{
				return this.m_originalIssuer;
			}
		}

		/// <summary>Gets a dictionary that contains additional properties associated with this claim.</summary>
		/// <returns>A dictionary that contains additional properties associated with the claim. The properties are represented as name-value pairs.</returns>
		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x060044E2 RID: 17634 RVA: 0x000F2094 File Offset: 0x000F0294
		public IDictionary<string, string> Properties
		{
			get
			{
				if (this.m_properties == null)
				{
					object propertyLock = this.m_propertyLock;
					lock (propertyLock)
					{
						if (this.m_properties == null)
						{
							this.m_properties = new Dictionary<string, string>();
						}
					}
				}
				return this.m_properties;
			}
		}

		/// <summary>Gets the subject of the claim.</summary>
		/// <returns>The subject of the claim.</returns>
		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x060044E3 RID: 17635 RVA: 0x000F20F0 File Offset: 0x000F02F0
		// (set) Token: 0x060044E4 RID: 17636 RVA: 0x000F20F8 File Offset: 0x000F02F8
		public ClaimsIdentity Subject
		{
			get
			{
				return this.m_subject;
			}
			internal set
			{
				this.m_subject = value;
			}
		}

		/// <summary>Gets the claim type of the claim.</summary>
		/// <returns>The claim type.</returns>
		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x060044E5 RID: 17637 RVA: 0x000F2101 File Offset: 0x000F0301
		public string Type
		{
			get
			{
				return this.m_type;
			}
		}

		/// <summary>Gets the value of the claim.</summary>
		/// <returns>The claim value.</returns>
		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x060044E6 RID: 17638 RVA: 0x000F2109 File Offset: 0x000F0309
		public string Value
		{
			get
			{
				return this.m_value;
			}
		}

		/// <summary>Gets the value type of the claim.</summary>
		/// <returns>The claim value type.</returns>
		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x060044E7 RID: 17639 RVA: 0x000F2111 File Offset: 0x000F0311
		public string ValueType
		{
			get
			{
				return this.m_valueType;
			}
		}

		/// <summary>Returns a new <see cref="T:System.Security.Claims.Claim" /> object copied from this object. The new claim does not have a subject.</summary>
		/// <returns>The new claim object.</returns>
		// Token: 0x060044E8 RID: 17640 RVA: 0x000F2119 File Offset: 0x000F0319
		public virtual Claim Clone()
		{
			return this.Clone(null);
		}

		/// <summary>Returns a new <see cref="T:System.Security.Claims.Claim" /> object copied from this object. The subject of the new claim is set to the specified ClaimsIdentity.</summary>
		/// <returns>The new claim object.</returns>
		/// <param name="identity">The intended subject of the new claim.</param>
		// Token: 0x060044E9 RID: 17641 RVA: 0x000F2122 File Offset: 0x000F0322
		public virtual Claim Clone(ClaimsIdentity identity)
		{
			return new Claim(this, identity);
		}

		// Token: 0x060044EA RID: 17642 RVA: 0x000F212C File Offset: 0x000F032C
		private void Initialize(BinaryReader reader, ClaimsIdentity subject)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.m_subject = subject;
			Claim.SerializationMask serializationMask = (Claim.SerializationMask)reader.ReadInt32();
			int num = 1;
			int num2 = reader.ReadInt32();
			this.m_value = reader.ReadString();
			if ((serializationMask & Claim.SerializationMask.NameClaimType) == Claim.SerializationMask.NameClaimType)
			{
				this.m_type = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			}
			else if ((serializationMask & Claim.SerializationMask.RoleClaimType) == Claim.SerializationMask.RoleClaimType)
			{
				this.m_type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			}
			else
			{
				this.m_type = reader.ReadString();
				num++;
			}
			if ((serializationMask & Claim.SerializationMask.StringType) == Claim.SerializationMask.StringType)
			{
				this.m_valueType = reader.ReadString();
				num++;
			}
			else
			{
				this.m_valueType = "http://www.w3.org/2001/XMLSchema#string";
			}
			if ((serializationMask & Claim.SerializationMask.Issuer) == Claim.SerializationMask.Issuer)
			{
				this.m_issuer = reader.ReadString();
				num++;
			}
			else
			{
				this.m_issuer = "LOCAL AUTHORITY";
			}
			if ((serializationMask & Claim.SerializationMask.OriginalIssuerEqualsIssuer) == Claim.SerializationMask.OriginalIssuerEqualsIssuer)
			{
				this.m_originalIssuer = this.m_issuer;
			}
			else if ((serializationMask & Claim.SerializationMask.OriginalIssuer) == Claim.SerializationMask.OriginalIssuer)
			{
				this.m_originalIssuer = reader.ReadString();
				num++;
			}
			else
			{
				this.m_originalIssuer = "LOCAL AUTHORITY";
			}
			if ((serializationMask & Claim.SerializationMask.HasProperties) == Claim.SerializationMask.HasProperties)
			{
				int num3 = reader.ReadInt32();
				for (int i = 0; i < num3; i++)
				{
					this.Properties.Add(reader.ReadString(), reader.ReadString());
				}
			}
			if ((serializationMask & Claim.SerializationMask.UserData) == Claim.SerializationMask.UserData)
			{
				int num4 = reader.ReadInt32();
				this.m_userSerializationData = reader.ReadBytes(num4);
				num++;
			}
			for (int j = num; j < num2; j++)
			{
				reader.ReadString();
			}
		}

		// Token: 0x060044EB RID: 17643 RVA: 0x000F2296 File Offset: 0x000F0496
		public virtual void WriteTo(BinaryWriter writer)
		{
			this.WriteTo(writer, null);
		}

		// Token: 0x060044EC RID: 17644 RVA: 0x000F22A0 File Offset: 0x000F04A0
		protected virtual void WriteTo(BinaryWriter writer, byte[] userData)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			int num = 1;
			Claim.SerializationMask serializationMask = Claim.SerializationMask.None;
			if (string.Equals(this.m_type, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"))
			{
				serializationMask |= Claim.SerializationMask.NameClaimType;
			}
			else if (string.Equals(this.m_type, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"))
			{
				serializationMask |= Claim.SerializationMask.RoleClaimType;
			}
			else
			{
				num++;
			}
			if (!string.Equals(this.m_valueType, "http://www.w3.org/2001/XMLSchema#string", StringComparison.Ordinal))
			{
				num++;
				serializationMask |= Claim.SerializationMask.StringType;
			}
			if (!string.Equals(this.m_issuer, "LOCAL AUTHORITY", StringComparison.Ordinal))
			{
				num++;
				serializationMask |= Claim.SerializationMask.Issuer;
			}
			if (string.Equals(this.m_originalIssuer, this.m_issuer, StringComparison.Ordinal))
			{
				serializationMask |= Claim.SerializationMask.OriginalIssuerEqualsIssuer;
			}
			else if (!string.Equals(this.m_originalIssuer, "LOCAL AUTHORITY", StringComparison.Ordinal))
			{
				num++;
				serializationMask |= Claim.SerializationMask.OriginalIssuer;
			}
			if (this.Properties.Count > 0)
			{
				num++;
				serializationMask |= Claim.SerializationMask.HasProperties;
			}
			if (userData != null && userData.Length != 0)
			{
				num++;
				serializationMask |= Claim.SerializationMask.UserData;
			}
			writer.Write((int)serializationMask);
			writer.Write(num);
			writer.Write(this.m_value);
			if ((serializationMask & Claim.SerializationMask.NameClaimType) != Claim.SerializationMask.NameClaimType && (serializationMask & Claim.SerializationMask.RoleClaimType) != Claim.SerializationMask.RoleClaimType)
			{
				writer.Write(this.m_type);
			}
			if ((serializationMask & Claim.SerializationMask.StringType) == Claim.SerializationMask.StringType)
			{
				writer.Write(this.m_valueType);
			}
			if ((serializationMask & Claim.SerializationMask.Issuer) == Claim.SerializationMask.Issuer)
			{
				writer.Write(this.m_issuer);
			}
			if ((serializationMask & Claim.SerializationMask.OriginalIssuer) == Claim.SerializationMask.OriginalIssuer)
			{
				writer.Write(this.m_originalIssuer);
			}
			if ((serializationMask & Claim.SerializationMask.HasProperties) == Claim.SerializationMask.HasProperties)
			{
				writer.Write(this.Properties.Count);
				foreach (string text in this.Properties.Keys)
				{
					writer.Write(text);
					writer.Write(this.Properties[text]);
				}
			}
			if ((serializationMask & Claim.SerializationMask.UserData) == Claim.SerializationMask.UserData)
			{
				writer.Write(userData.Length);
				writer.Write(userData);
			}
			writer.Flush();
		}

		/// <summary>Returns a string representation of this <see cref="T:System.Security.Claims.Claim" /> object.</summary>
		/// <returns>The string representation of this <see cref="T:System.Security.Claims.Claim" /> object.</returns>
		// Token: 0x060044ED RID: 17645 RVA: 0x000F2488 File Offset: 0x000F0688
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}: {1}", this.m_type, this.m_value);
		}

		// Token: 0x0400230A RID: 8970
		private string m_issuer;

		// Token: 0x0400230B RID: 8971
		private string m_originalIssuer;

		// Token: 0x0400230C RID: 8972
		private string m_type;

		// Token: 0x0400230D RID: 8973
		private string m_value;

		// Token: 0x0400230E RID: 8974
		private string m_valueType;

		// Token: 0x0400230F RID: 8975
		[NonSerialized]
		private byte[] m_userSerializationData;

		// Token: 0x04002310 RID: 8976
		private Dictionary<string, string> m_properties;

		// Token: 0x04002311 RID: 8977
		[NonSerialized]
		private object m_propertyLock;

		// Token: 0x04002312 RID: 8978
		[NonSerialized]
		private ClaimsIdentity m_subject;

		// Token: 0x02000632 RID: 1586
		private enum SerializationMask
		{
			// Token: 0x04002314 RID: 8980
			None,
			// Token: 0x04002315 RID: 8981
			NameClaimType,
			// Token: 0x04002316 RID: 8982
			RoleClaimType,
			// Token: 0x04002317 RID: 8983
			StringType = 4,
			// Token: 0x04002318 RID: 8984
			Issuer = 8,
			// Token: 0x04002319 RID: 8985
			OriginalIssuerEqualsIssuer = 16,
			// Token: 0x0400231A RID: 8986
			OriginalIssuer = 32,
			// Token: 0x0400231B RID: 8987
			HasProperties = 64,
			// Token: 0x0400231C RID: 8988
			UserData = 128
		}
	}
}
