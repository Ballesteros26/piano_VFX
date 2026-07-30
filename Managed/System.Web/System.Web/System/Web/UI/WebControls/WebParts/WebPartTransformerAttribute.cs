using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Identifies the types of connection points that a transformer supports. </summary>
	// Token: 0x020007BA RID: 1978
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class WebPartTransformerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformerAttribute" /> class. </summary>
		/// <param name="consumerType">The <see cref="T:System.Type" /> of the interface supported by the consumer connection point.</param>
		/// <param name="providerType">The <see cref="T:System.Type" /> of the interface supported by the provider connection point.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="consumerType" /> or <paramref name="providerType" /> is not specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="consumerType" /> equals <paramref name="providerType" />.</exception>
		// Token: 0x06004FCA RID: 20426 RVA: 0x0000393A File Offset: 0x00001B3A
		public WebPartTransformerAttribute(Type consumerType, Type providerType)
		{
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the interface supported by the consumer connection point.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the interface supported by the consumer connection point.</returns>
		// Token: 0x17001841 RID: 6209
		// (get) Token: 0x06004FCB RID: 20427 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Type ConsumerType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the interface supported by the provider connection point.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the interface supported by the provider connection point.</returns>
		// Token: 0x17001842 RID: 6210
		// (get) Token: 0x06004FCC RID: 20428 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Type ProviderType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns the consumer type a transformer can accept on a connection point.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the consumer connection point.</returns>
		/// <param name="transformerType">The type of transformer.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="transformerType" /> is not specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="transformerType" /> is not an object derived from the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class.</exception>
		// Token: 0x06004FCD RID: 20429 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static Type GetConsumerType(Type transformerType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the provider type a transformer can accept on a connection point.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the provider connection point.</returns>
		/// <param name="transformerType">The type of transformer.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="transformerType" /> is not specified.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="transformerType" /> is not an object derived from the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class.</exception>
		// Token: 0x06004FCE RID: 20430 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static Type GetProviderType(Type transformerType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
