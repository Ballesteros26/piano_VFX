using System;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.Xml
{
	/// <summary>Represents an XML qualified name.</summary>
	// Token: 0x020002A4 RID: 676
	[Serializable]
	public class XmlQualifiedName
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlQualifiedName" /> class.</summary>
		// Token: 0x060018F0 RID: 6384 RVA: 0x0008FD5C File Offset: 0x0008DF5C
		public XmlQualifiedName()
			: this(string.Empty, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlQualifiedName" /> class with the specified name.</summary>
		/// <param name="name">The local name to use as the name of the <see cref="T:System.Xml.XmlQualifiedName" /> object. </param>
		// Token: 0x060018F1 RID: 6385 RVA: 0x0008FD6E File Offset: 0x0008DF6E
		public XmlQualifiedName(string name)
			: this(name, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlQualifiedName" /> class with the specified name and namespace.</summary>
		/// <param name="name">The local name to use as the name of the <see cref="T:System.Xml.XmlQualifiedName" /> object. </param>
		/// <param name="ns">The namespace for the <see cref="T:System.Xml.XmlQualifiedName" /> object. </param>
		// Token: 0x060018F2 RID: 6386 RVA: 0x0008FD7C File Offset: 0x0008DF7C
		public XmlQualifiedName(string name, string ns)
		{
			this.ns = ((ns == null) ? string.Empty : ns);
			this.name = ((name == null) ? string.Empty : name);
		}

		/// <summary>Gets a string representation of the namespace of the <see cref="T:System.Xml.XmlQualifiedName" />.</summary>
		/// <returns>A string representation of the namespace or String.Empty if a namespace is not defined for the object.</returns>
		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x0008FDA6 File Offset: 0x0008DFA6
		public string Namespace
		{
			get
			{
				return this.ns;
			}
		}

		/// <summary>Gets a string representation of the qualified name of the <see cref="T:System.Xml.XmlQualifiedName" />.</summary>
		/// <returns>A string representation of the qualified name or String.Empty if a name is not defined for the object.</returns>
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x0008FDAE File Offset: 0x0008DFAE
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Returns the hash code for the <see cref="T:System.Xml.XmlQualifiedName" />.</summary>
		/// <returns>A hash code for this object.</returns>
		// Token: 0x060018F5 RID: 6389 RVA: 0x0008FDB8 File Offset: 0x0008DFB8
		public override int GetHashCode()
		{
			if (this.hash == 0)
			{
				if (XmlQualifiedName.hashCodeDelegate == null)
				{
					XmlQualifiedName.hashCodeDelegate = XmlQualifiedName.GetHashCodeDelegate();
				}
				this.hash = XmlQualifiedName.hashCodeDelegate(this.Name, this.Name.Length, 0L);
			}
			return this.hash;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XmlQualifiedName" /> is empty.</summary>
		/// <returns>true if name and namespace are empty strings; otherwise, false.</returns>
		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060018F6 RID: 6390 RVA: 0x0008FE07 File Offset: 0x0008E007
		public bool IsEmpty
		{
			get
			{
				return this.Name.Length == 0 && this.Namespace.Length == 0;
			}
		}

		/// <summary>Returns the string value of the <see cref="T:System.Xml.XmlQualifiedName" />.</summary>
		/// <returns>The string value of the <see cref="T:System.Xml.XmlQualifiedName" /> in the format of namespace:localname. If the object does not have a namespace defined, this method returns just the local name.</returns>
		// Token: 0x060018F7 RID: 6391 RVA: 0x0008FE26 File Offset: 0x0008E026
		public override string ToString()
		{
			if (this.Namespace.Length != 0)
			{
				return this.Namespace + ":" + this.Name;
			}
			return this.Name;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Xml.XmlQualifiedName" /> object is equal to the current <see cref="T:System.Xml.XmlQualifiedName" /> object. </summary>
		/// <returns>true if the two are the same instance object; otherwise, false.</returns>
		/// <param name="other">The <see cref="T:System.Xml.XmlQualifiedName" /> to compare. </param>
		// Token: 0x060018F8 RID: 6392 RVA: 0x0008FE54 File Offset: 0x0008E054
		public override bool Equals(object other)
		{
			if (this == other)
			{
				return true;
			}
			XmlQualifiedName xmlQualifiedName = other as XmlQualifiedName;
			return xmlQualifiedName != null && this.Name == xmlQualifiedName.Name && this.Namespace == xmlQualifiedName.Namespace;
		}

		/// <summary>Compares two <see cref="T:System.Xml.XmlQualifiedName" /> objects.</summary>
		/// <returns>true if the two objects have the same name and namespace values; otherwise, false.</returns>
		/// <param name="a">An <see cref="T:System.Xml.XmlQualifiedName" /> to compare. </param>
		/// <param name="b">An <see cref="T:System.Xml.XmlQualifiedName" /> to compare. </param>
		// Token: 0x060018F9 RID: 6393 RVA: 0x0008FE9F File Offset: 0x0008E09F
		public static bool operator ==(XmlQualifiedName a, XmlQualifiedName b)
		{
			return a == b || (a != null && b != null && a.Name == b.Name && a.Namespace == b.Namespace);
		}

		/// <summary>Compares two <see cref="T:System.Xml.XmlQualifiedName" /> objects.</summary>
		/// <returns>true if the name and namespace values for the two objects differ; otherwise, false.</returns>
		/// <param name="a">An <see cref="T:System.Xml.XmlQualifiedName" /> to compare. </param>
		/// <param name="b">An <see cref="T:System.Xml.XmlQualifiedName" /> to compare. </param>
		// Token: 0x060018FA RID: 6394 RVA: 0x0008FED5 File Offset: 0x0008E0D5
		public static bool operator !=(XmlQualifiedName a, XmlQualifiedName b)
		{
			return !(a == b);
		}

		/// <summary>Returns the string value of the <see cref="T:System.Xml.XmlQualifiedName" />.</summary>
		/// <returns>The string value of the <see cref="T:System.Xml.XmlQualifiedName" /> in the format of namespace:localname. If the object does not have a namespace defined, this method returns just the local name.</returns>
		/// <param name="name">The name of the object. </param>
		/// <param name="ns">The namespace of the object. </param>
		// Token: 0x060018FB RID: 6395 RVA: 0x0008FEE1 File Offset: 0x0008E0E1
		public static string ToString(string name, string ns)
		{
			if (ns != null && ns.Length != 0)
			{
				return ns + ":" + name;
			}
			return name;
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0008FEFC File Offset: 0x0008E0FC
		[SecuritySafeCritical]
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		private static XmlQualifiedName.HashCodeOfStringDelegate GetHashCodeDelegate()
		{
			if (!XmlQualifiedName.IsRandomizedHashingDisabled())
			{
				MethodInfo method = typeof(string).GetMethod("InternalMarvin32HashString", BindingFlags.Static | BindingFlags.NonPublic);
				if (method != null)
				{
					return (XmlQualifiedName.HashCodeOfStringDelegate)Delegate.CreateDelegate(typeof(XmlQualifiedName.HashCodeOfStringDelegate), method);
				}
			}
			return new XmlQualifiedName.HashCodeOfStringDelegate(XmlQualifiedName.GetHashCodeOfString);
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x0000226C File Offset: 0x0000046C
		private static bool IsRandomizedHashingDisabled()
		{
			return false;
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0008FF52 File Offset: 0x0008E152
		private static int GetHashCodeOfString(string s, int length, long additionalEntropy)
		{
			return s.GetHashCode();
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0008FF5A File Offset: 0x0008E15A
		internal void Init(string name, string ns)
		{
			this.name = name;
			this.ns = ns;
			this.hash = 0;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x0008FF71 File Offset: 0x0008E171
		internal void SetNamespace(string ns)
		{
			this.ns = ns;
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0008FF7A File Offset: 0x0008E17A
		internal void Verify()
		{
			XmlConvert.VerifyNCName(this.name);
			if (this.ns.Length != 0)
			{
				XmlConvert.ToUri(this.ns);
			}
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0008FFA1 File Offset: 0x0008E1A1
		internal void Atomize(XmlNameTable nameTable)
		{
			this.name = nameTable.Add(this.name);
			this.ns = nameTable.Add(this.ns);
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0008FFC8 File Offset: 0x0008E1C8
		internal static XmlQualifiedName Parse(string s, IXmlNamespaceResolver nsmgr, out string prefix)
		{
			string text;
			ValidateNames.ParseQNameThrow(s, out prefix, out text);
			string text2 = nsmgr.LookupNamespace(prefix);
			if (text2 == null)
			{
				if (prefix.Length != 0)
				{
					throw new XmlException("'{0}' is an undeclared prefix.", prefix);
				}
				text2 = string.Empty;
			}
			return new XmlQualifiedName(text, text2);
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0009000D File Offset: 0x0008E20D
		internal XmlQualifiedName Clone()
		{
			return (XmlQualifiedName)base.MemberwiseClone();
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0009001C File Offset: 0x0008E21C
		internal static int Compare(XmlQualifiedName a, XmlQualifiedName b)
		{
			if (null == a)
			{
				if (!(null == b))
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (null == b)
				{
					return 1;
				}
				int num = string.CompareOrdinal(a.Namespace, b.Namespace);
				if (num == 0)
				{
					num = string.CompareOrdinal(a.Name, b.Name);
				}
				return num;
			}
		}

		// Token: 0x0400104E RID: 4174
		private static XmlQualifiedName.HashCodeOfStringDelegate hashCodeDelegate = null;

		// Token: 0x0400104F RID: 4175
		private string name;

		// Token: 0x04001050 RID: 4176
		private string ns;

		// Token: 0x04001051 RID: 4177
		[NonSerialized]
		private int hash;

		/// <summary>Provides an empty <see cref="T:System.Xml.XmlQualifiedName" />.</summary>
		// Token: 0x04001052 RID: 4178
		public static readonly XmlQualifiedName Empty = new XmlQualifiedName(string.Empty);

		// Token: 0x020002A5 RID: 677
		// (Invoke) Token: 0x06001908 RID: 6408
		private delegate int HashCodeOfStringDelegate(string s, int sLen, long additionalEntropy);
	}
}
