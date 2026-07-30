using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Abstract class for all facets that are used when simple types are derived by restriction.</summary>
	// Token: 0x02000452 RID: 1106
	public abstract class XmlSchemaFacet : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the value attribute of the facet.</summary>
		/// <returns>The value attribute.</returns>
		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06002C25 RID: 11301 RVA: 0x00106DF4 File Offset: 0x00104FF4
		// (set) Token: 0x06002C26 RID: 11302 RVA: 0x00106DFC File Offset: 0x00104FFC
		[XmlAttribute("value")]
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		/// <summary>Gets or sets information that indicates that this facet is fixed.</summary>
		/// <returns>If true, value is fixed; otherwise, false. The default is false.Optional.</returns>
		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06002C27 RID: 11303 RVA: 0x00106E05 File Offset: 0x00105005
		// (set) Token: 0x06002C28 RID: 11304 RVA: 0x00106E0D File Offset: 0x0010500D
		[XmlAttribute("fixed")]
		[DefaultValue(false)]
		public virtual bool IsFixed
		{
			get
			{
				return this.isFixed;
			}
			set
			{
				if (!(this is XmlSchemaEnumerationFacet) && !(this is XmlSchemaPatternFacet))
				{
					this.isFixed = value;
				}
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06002C29 RID: 11305 RVA: 0x00106E26 File Offset: 0x00105026
		// (set) Token: 0x06002C2A RID: 11306 RVA: 0x00106E2E File Offset: 0x0010502E
		internal FacetType FacetType
		{
			get
			{
				return this.facetType;
			}
			set
			{
				this.facetType = value;
			}
		}

		// Token: 0x04001DB2 RID: 7602
		private string value;

		// Token: 0x04001DB3 RID: 7603
		private bool isFixed;

		// Token: 0x04001DB4 RID: 7604
		private FacetType facetType;
	}
}
