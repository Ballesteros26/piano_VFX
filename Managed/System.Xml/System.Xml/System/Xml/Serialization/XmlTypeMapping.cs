using System;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Contains a mapping of one type to another.</summary>
	// Token: 0x0200036A RID: 874
	public class XmlTypeMapping : XmlMapping
	{
		// Token: 0x060023C0 RID: 9152 RVA: 0x000DC1B5 File Offset: 0x000DA3B5
		internal XmlTypeMapping(TypeScope scope, ElementAccessor accessor)
			: base(scope, accessor)
		{
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x060023C1 RID: 9153 RVA: 0x000DC1BF File Offset: 0x000DA3BF
		internal TypeMapping Mapping
		{
			get
			{
				return base.Accessor.Mapping;
			}
		}

		/// <summary>Gets the type name of the mapped object.</summary>
		/// <returns>The type name of the mapped object.</returns>
		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x060023C2 RID: 9154 RVA: 0x000DC1CC File Offset: 0x000DA3CC
		public string TypeName
		{
			get
			{
				return this.Mapping.TypeDesc.Name;
			}
		}

		/// <summary>The fully qualified type name that includes the namespace (or namespaces) and type.</summary>
		/// <returns>The fully qualified type name.</returns>
		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x060023C3 RID: 9155 RVA: 0x000DC1DE File Offset: 0x000DA3DE
		public string TypeFullName
		{
			get
			{
				return this.Mapping.TypeDesc.FullName;
			}
		}

		/// <summary>Gets the XML element name of the mapped object.</summary>
		/// <returns>The XML element name of the mapped object. The default is the class name of the object.</returns>
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x060023C4 RID: 9156 RVA: 0x000DC1F0 File Offset: 0x000DA3F0
		public string XsdTypeName
		{
			get
			{
				return this.Mapping.TypeName;
			}
		}

		/// <summary>Gets the XML namespace of the mapped object.</summary>
		/// <returns>The XML namespace of the mapped object. The default is an empty string ("").</returns>
		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x060023C5 RID: 9157 RVA: 0x000DC1FD File Offset: 0x000DA3FD
		public string XsdTypeNamespace
		{
			get
			{
				return this.Mapping.Namespace;
			}
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x000728B0 File Offset: 0x00070AB0
		internal XmlTypeMapping()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
