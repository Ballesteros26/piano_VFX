using System;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;DataReference&gt; element used in XML encryption. This class cannot be inherited.</summary>
	// Token: 0x02000054 RID: 84
	public sealed class DataReference : EncryptedReference
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> class.</summary>
		// Token: 0x060001E3 RID: 483 RVA: 0x000073A8 File Offset: 0x000055A8
		public DataReference()
		{
			base.ReferenceType = "DataReference";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> class using the specified Uniform Resource Identifier (URI).</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) that points to the encrypted data.</param>
		// Token: 0x060001E4 RID: 484 RVA: 0x000073BB File Offset: 0x000055BB
		public DataReference(string uri)
			: base(uri)
		{
			base.ReferenceType = "DataReference";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> class using the specified Uniform Resource Identifier (URI) and a <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) that points to the encrypted data.</param>
		/// <param name="transformChain">A <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object that describes transforms to do on the encrypted data.</param>
		// Token: 0x060001E5 RID: 485 RVA: 0x000073CF File Offset: 0x000055CF
		public DataReference(string uri, TransformChain transformChain)
			: base(uri, transformChain)
		{
			base.ReferenceType = "DataReference";
		}
	}
}
