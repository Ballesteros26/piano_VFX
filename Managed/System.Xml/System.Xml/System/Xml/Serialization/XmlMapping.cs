using System;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Supports mappings between .NET Framework types and XML Schema data types. </summary>
	// Token: 0x02000336 RID: 822
	public abstract class XmlMapping
	{
		// Token: 0x06001F63 RID: 8035 RVA: 0x000A9CA2 File Offset: 0x000A7EA2
		internal XmlMapping(TypeScope scope, ElementAccessor accessor)
			: this(scope, accessor, XmlMappingAccess.Read | XmlMappingAccess.Write)
		{
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x000A9CAD File Offset: 0x000A7EAD
		internal XmlMapping(TypeScope scope, ElementAccessor accessor, XmlMappingAccess access)
		{
			this.scope = scope;
			this.accessor = accessor;
			this.access = access;
			this.shallow = scope == null;
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001F65 RID: 8037 RVA: 0x000A9CD4 File Offset: 0x000A7ED4
		internal ElementAccessor Accessor
		{
			get
			{
				return this.accessor;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001F66 RID: 8038 RVA: 0x000A9CDC File Offset: 0x000A7EDC
		internal TypeScope Scope
		{
			get
			{
				return this.scope;
			}
		}

		/// <summary>Get the name of the mapped element.</summary>
		/// <returns>The name of the mapped element.</returns>
		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x000A9CE4 File Offset: 0x000A7EE4
		public string ElementName
		{
			get
			{
				return global::System.Xml.Serialization.Accessor.UnescapeName(this.Accessor.Name);
			}
		}

		/// <summary>Gets the name of the XSD element of the mapping.</summary>
		/// <returns>The XSD element name.</returns>
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001F68 RID: 8040 RVA: 0x000A9CF6 File Offset: 0x000A7EF6
		public string XsdElementName
		{
			get
			{
				return this.Accessor.Name;
			}
		}

		/// <summary>Gets the namespace of the mapped element.</summary>
		/// <returns>The namespace of the mapped element.</returns>
		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001F69 RID: 8041 RVA: 0x000A9D03 File Offset: 0x000A7F03
		public string Namespace
		{
			get
			{
				return this.accessor.Namespace;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001F6A RID: 8042 RVA: 0x000A9D10 File Offset: 0x000A7F10
		// (set) Token: 0x06001F6B RID: 8043 RVA: 0x000A9D18 File Offset: 0x000A7F18
		internal bool GenerateSerializer
		{
			get
			{
				return this.generateSerializer;
			}
			set
			{
				this.generateSerializer = value;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001F6C RID: 8044 RVA: 0x000A9D21 File Offset: 0x000A7F21
		internal bool IsReadable
		{
			get
			{
				return (this.access & XmlMappingAccess.Read) > XmlMappingAccess.None;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001F6D RID: 8045 RVA: 0x000A9D2E File Offset: 0x000A7F2E
		internal bool IsWriteable
		{
			get
			{
				return (this.access & XmlMappingAccess.Write) > XmlMappingAccess.None;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001F6E RID: 8046 RVA: 0x000A9D3B File Offset: 0x000A7F3B
		// (set) Token: 0x06001F6F RID: 8047 RVA: 0x000A9D43 File Offset: 0x000A7F43
		internal bool IsSoap
		{
			get
			{
				return this.isSoap;
			}
			set
			{
				this.isSoap = value;
			}
		}

		/// <summary>Sets the key used to look up the mapping.</summary>
		/// <param name="key">A <see cref="T:System.String" /> that contains the lookup key.</param>
		// Token: 0x06001F70 RID: 8048 RVA: 0x000A9D4C File Offset: 0x000A7F4C
		public void SetKey(string key)
		{
			this.SetKeyInternal(key);
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x000A9D55 File Offset: 0x000A7F55
		internal void SetKeyInternal(string key)
		{
			this.key = key;
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x000A9D60 File Offset: 0x000A7F60
		internal static string GenerateKey(Type type, XmlRootAttribute root, string ns)
		{
			if (root == null)
			{
				root = (XmlRootAttribute)XmlAttributes.GetAttr(type, typeof(XmlRootAttribute));
			}
			return string.Concat(new string[]
			{
				type.FullName,
				":",
				(root == null) ? string.Empty : root.Key,
				":",
				(ns == null) ? string.Empty : ns
			});
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001F73 RID: 8051 RVA: 0x000A9DCC File Offset: 0x000A7FCC
		internal string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x000A9DD4 File Offset: 0x000A7FD4
		internal void CheckShallow()
		{
			if (this.shallow)
			{
				throw new InvalidOperationException(Res.GetString("This mapping was not crated by reflection importer and cannot be used in this context."));
			}
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x000A9DF0 File Offset: 0x000A7FF0
		internal static bool IsShallow(XmlMapping[] mappings)
		{
			for (int i = 0; i < mappings.Length; i++)
			{
				if (mappings[i] == null || mappings[i].shallow)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x000728B0 File Offset: 0x00070AB0
		internal XmlMapping()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001741 RID: 5953
		private TypeScope scope;

		// Token: 0x04001742 RID: 5954
		private bool generateSerializer;

		// Token: 0x04001743 RID: 5955
		private bool isSoap;

		// Token: 0x04001744 RID: 5956
		private ElementAccessor accessor;

		// Token: 0x04001745 RID: 5957
		private string key;

		// Token: 0x04001746 RID: 5958
		private bool shallow;

		// Token: 0x04001747 RID: 5959
		private XmlMappingAccess access;
	}
}
