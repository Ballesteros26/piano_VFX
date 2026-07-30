using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Provides default functionality for all SOAP attributes.</summary>
	// Token: 0x020007C7 RID: 1991
	[ComVisible(true)]
	public class SoapAttribute : Attribute
	{
		/// <summary>Gets or sets a value indicating whether the type must be nested during SOAP serialization.</summary>
		/// <returns>true if the target object must be nested during SOAP serialization; otherwise, false.</returns>
		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x0600504C RID: 20556 RVA: 0x0011F756 File Offset: 0x0011D956
		// (set) Token: 0x0600504D RID: 20557 RVA: 0x0011F75E File Offset: 0x0011D95E
		public virtual bool Embedded
		{
			get
			{
				return this._nested;
			}
			set
			{
				this._nested = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the target of the current attribute will be serialized as an XML attribute instead of an XML field.</summary>
		/// <returns>true if the target object of the current attribute must be serialized as an XML attribute; false if the target object must be serialized as a subelement.</returns>
		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x0600504E RID: 20558 RVA: 0x0011F767 File Offset: 0x0011D967
		// (set) Token: 0x0600504F RID: 20559 RVA: 0x0011F76F File Offset: 0x0011D96F
		public virtual bool UseAttribute
		{
			get
			{
				return this._useAttribute;
			}
			set
			{
				this._useAttribute = value;
			}
		}

		/// <summary>Gets or sets the XML namespace name.</summary>
		/// <returns>The XML namespace name under which the target of the current attribute is serialized.</returns>
		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x06005050 RID: 20560 RVA: 0x0011F778 File Offset: 0x0011D978
		// (set) Token: 0x06005051 RID: 20561 RVA: 0x0011F780 File Offset: 0x0011D980
		public virtual string XmlNamespace
		{
			get
			{
				return this.ProtXmlNamespace;
			}
			set
			{
				this.ProtXmlNamespace = value;
			}
		}

		// Token: 0x06005052 RID: 20562 RVA: 0x0011F789 File Offset: 0x0011D989
		internal virtual void SetReflectionObject(object reflectionObject)
		{
			this.ReflectInfo = reflectionObject;
		}

		// Token: 0x04002A74 RID: 10868
		private bool _nested;

		// Token: 0x04002A75 RID: 10869
		private bool _useAttribute;

		/// <summary>The XML namespace to which the target of the current SOAP attribute is serialized.</summary>
		// Token: 0x04002A76 RID: 10870
		protected string ProtXmlNamespace;

		/// <summary>A reflection object used by attribute classes derived from the <see cref="T:System.Runtime.Remoting.Metadata.SoapAttribute" /> class to set XML serialization information.</summary>
		// Token: 0x04002A77 RID: 10871
		protected object ReflectInfo;
	}
}
