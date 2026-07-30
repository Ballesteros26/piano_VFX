using System;
using System.IO;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the C14N XML canonicalization transform for a digital signature as defined by the World Wide Web Consortium (W3C), without comments.</summary>
	// Token: 0x0200007D RID: 125
	public class XmlDsigC14NTransform : Transform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> class.</summary>
		// Token: 0x0600038C RID: 908 RVA: 0x0000E618 File Offset: 0x0000C818
		public XmlDsigC14NTransform()
		{
			base.Algorithm = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> class with comments, if specified.</summary>
		/// <param name="includeComments">true to include comments; otherwise, false. </param>
		// Token: 0x0600038D RID: 909 RVA: 0x0000E684 File Offset: 0x0000C884
		public XmlDsigC14NTransform(bool includeComments)
		{
			this._includeComments = includeComments;
			base.Algorithm = (includeComments ? "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments" : "http://www.w3.org/TR/2001/REC-xml-c14n-20010315");
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigC14NTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigC14NTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</returns>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000E6FF File Offset: 0x0000C8FF
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigC14NTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object; the <see cref="M:System.Security.Cryptography.Xml.XmlDsigC14NTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object return only objects of one of these types.</returns>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000E707 File Offset: 0x0000C907
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a &lt;Transform&gt; element; this method is not supported because this element has no inner XML elements.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object. </param>
		// Token: 0x06000390 RID: 912 RVA: 0x00004938 File Offset: 0x00002B38
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
		}

		/// <summary>Returns an XML representation of the parameters of an <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object that are suitable to be included as subelements of an XMLDSIG &lt;Transform&gt; element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object in an XMLDSIG &lt;Transform&gt; element.</returns>
		// Token: 0x06000391 RID: 913 RVA: 0x00003BEE File Offset: 0x00001DEE
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		/// <summary>Loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="obj" /> parameter is a <see cref="T:System.IO.Stream" /> object and it is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000392 RID: 914 RVA: 0x0000E710 File Offset: 0x0000C910
		public override void LoadInput(object obj)
		{
			XmlResolver xmlResolver = (base.ResolverSet ? this._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI));
			if (obj is Stream)
			{
				this._cXml = new CanonicalXml((Stream)obj, this._includeComments, xmlResolver, base.BaseURI);
				return;
			}
			if (obj is XmlDocument)
			{
				this._cXml = new CanonicalXml((XmlDocument)obj, xmlResolver, this._includeComments);
				return;
			}
			if (obj is XmlNodeList)
			{
				this._cXml = new CanonicalXml((XmlNodeList)obj, xmlResolver, this._includeComments);
				return;
			}
			throw new ArgumentException("Type of input object is invalid.", "obj");
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</returns>
		// Token: 0x06000393 RID: 915 RVA: 0x0000E7B6 File Offset: 0x0000C9B6
		public override object GetOutput()
		{
			return new MemoryStream(this._cXml.GetBytes());
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object of type <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object of type <see cref="T:System.IO.Stream" />.</returns>
		/// <param name="type">The type of the output to return. <see cref="T:System.IO.Stream" /> is the only valid type for this parameter. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not a <see cref="T:System.IO.Stream" /> object.</exception>
		// Token: 0x06000394 RID: 916 RVA: 0x0000E7C8 File Offset: 0x0000C9C8
		public override object GetOutput(Type type)
		{
			if (type != typeof(Stream) && !type.IsSubclassOf(typeof(Stream)))
			{
				throw new ArgumentException("The input type was invalid for this transform.", "type");
			}
			return new MemoryStream(this._cXml.GetBytes());
		}

		/// <summary>Returns the digest associated with an <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object. </summary>
		/// <returns>The digest associated with an <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</returns>
		/// <param name="hash">The <see cref="T:System.Security.Cryptography.HashAlgorithm" /> object used to create a digest.</param>
		// Token: 0x06000395 RID: 917 RVA: 0x0000E819 File Offset: 0x0000CA19
		public override byte[] GetDigestedOutput(HashAlgorithm hash)
		{
			return this._cXml.GetDigestedBytes(hash);
		}

		// Token: 0x040001BF RID: 447
		private Type[] _inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlDocument),
			typeof(XmlNodeList)
		};

		// Token: 0x040001C0 RID: 448
		private Type[] _outputTypes = new Type[] { typeof(Stream) };

		// Token: 0x040001C1 RID: 449
		private CanonicalXml _cXml;

		// Token: 0x040001C2 RID: 450
		private bool _includeComments;
	}
}
