using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides an object that enables two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls to form a connection. This class cannot be inherited.</summary>
	// Token: 0x020006BA RID: 1722
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true, "Transformers")]
	public sealed class WebPartConnection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> class. </summary>
		// Token: 0x0600490D RID: 18701 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebPartConnection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> object that is acting as the consumer control in a connection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> that represents the Web Parts control acting as the consumer in a connection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The length of the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartConnection.ConsumerID" /> property is zero.</exception>
		// Token: 0x1700167A RID: 5754
		// (get) Token: 0x0600490E RID: 18702 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPart Consumer
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the object that serves as a connection point for a control that is acting as a consumer in a connection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> associated with the consumer control in a connection. </returns>
		// Token: 0x1700167B RID: 5755
		// (get) Token: 0x0600490F RID: 18703 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ConsumerConnectionPoint ConsumerConnectionPoint
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the property value on a connection that references the ID of the object serving as the consumer connection point for that connection.</summary>
		/// <returns>A string that contains the ID for the consumer connection point.</returns>
		// Token: 0x1700167C RID: 5756
		// (get) Token: 0x06004910 RID: 18704 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004911 RID: 18705 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string ConsumerConnectionPointID
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the property value on a connection that references the ID of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control acting as a consumer for that connection.</summary>
		/// <returns>A string that contains the ID of the control acting as a consumer in a connection. </returns>
		// Token: 0x1700167D RID: 5757
		// (get) Token: 0x06004912 RID: 18706 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004913 RID: 18707 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string ConsumerID
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the ID of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object.</summary>
		/// <returns>A string that contains the ID of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" />.</returns>
		// Token: 0x1700167E RID: 5758
		// (get) Token: 0x06004914 RID: 18708 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004915 RID: 18709 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string ID
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object is currently established and able to exchange data between its provider and consumer controls.</summary>
		/// <returns>true if the connection is active; otherwise, false.</returns>
		// Token: 0x1700167F RID: 5759
		// (get) Token: 0x06004916 RID: 18710 RVA: 0x000CA0FC File Offset: 0x000C82FC
		public bool IsActive
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object is visible to all users or only to the current user.</summary>
		/// <returns>true if the connection is shared; otherwise, false.</returns>
		// Token: 0x17001680 RID: 5760
		// (get) Token: 0x06004917 RID: 18711 RVA: 0x000CA118 File Offset: 0x000C8318
		public bool IsShared
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object is declared in the markup of a Web page, or created programmatically. </summary>
		/// <returns>true if the connection is static; otherwise, false.</returns>
		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x06004918 RID: 18712 RVA: 0x000CA134 File Offset: 0x000C8334
		public bool IsStatic
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control that acts as the provider in a Web Parts connection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> that acts as the provider of data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The length of the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartConnection.ProviderID" /> property value is zero.</exception>
		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x06004919 RID: 18713 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPart Provider
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the object that serves as a connection point for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control acting as a provider for a connection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> associated with the provider control in a connection.</returns>
		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x0600491A RID: 18714 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ProviderConnectionPoint ProviderConnectionPoint
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the property value on a connection that references the ID of the object serving as the provider connection point for that connection.</summary>
		/// <returns>A string that contains the ID for a provider connection point object.</returns>
		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x0600491B RID: 18715 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x0600491C RID: 18716 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string ProviderConnectionPointID
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the property value on a connection that references the ID of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control acting as a provider for that connection.</summary>
		/// <returns>A string that contains the ID of the provider control.</returns>
		// Token: 0x17001685 RID: 5765
		// (get) Token: 0x0600491D RID: 18717 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x0600491E RID: 18718 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string ProviderID
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> object that is used to transform data between two otherwise incompatible connection points in a Web Parts connection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" />. The default value is null.</returns>
		// Token: 0x17001686 RID: 5766
		// (get) Token: 0x0600491F RID: 18719 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPartTransformer Transformer
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects used internally by the Web Parts control set. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformerCollection" /> containing <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects.</returns>
		// Token: 0x17001687 RID: 5767
		// (get) Token: 0x06004920 RID: 18720 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPartTransformerCollection Transformers
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
