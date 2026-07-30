using System;
using System.Collections;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Represents a collection of connection points for a control in a Web Parts zone acting as a consumer. This class cannot be inherited.</summary>
	// Token: 0x020006D9 RID: 1753
	public sealed class ConsumerConnectionPointCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection" /> class. </summary>
		// Token: 0x06004A4B RID: 19019 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ConsumerConnectionPointCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection" /> class using the specified collection of connection points. </summary>
		/// <param name="connectionPoints">A collection of consumer connection points.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="connectionPoints" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The collection contains an invalid connection point.- or -The collection contains an item with a duplicate ID.</exception>
		// Token: 0x06004A4C RID: 19020 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ConsumerConnectionPointCollection(ICollection connectionPoints)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the default consumer connection point.</summary>
		/// <returns>The default <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" />.</returns>
		// Token: 0x170016EF RID: 5871
		// (get) Token: 0x06004A4D RID: 19021 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ConsumerConnectionPoint Default
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ConsumerConnectionPoint get_Item(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the connection point with the specified <see cref="P:System.Web.UI.WebControls.WebParts.ConnectionPoint.ID" /> property. </summary>
		/// <returns>The first <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> in the collection whose ID matches the value of <paramref name="id" />.</returns>
		/// <param name="id">A string value representing the connection point ID of the connection point to be retrieved.</param>
		// Token: 0x170016F0 RID: 5872
		public ConsumerConnectionPoint this[string id]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> object is in the collection.</summary>
		/// <returns>true if the consumer connection point is contained in the collection; otherwise, false.</returns>
		/// <param name="connectionPoint">The connection point to search for.</param>
		// Token: 0x06004A50 RID: 19024 RVA: 0x000CA700 File Offset: 0x000C8900
		public bool Contains(ConsumerConnectionPoint connectionPoint)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection" /> collection to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> objects, starting at the specified index in the target array.</summary>
		/// <param name="array">A zero-based array of <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> objects that receives the copied items from the current <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection" />.</param>
		/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
		// Token: 0x06004A51 RID: 19025 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void CopyTo(ConsumerConnectionPoint[] array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Determines the index of the specified <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> object in the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="connectionPoint" /> within the current <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPointCollection" />, if found; otherwise, -1.</returns>
		/// <param name="connectionPoint">The <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> to locate.</param>
		// Token: 0x06004A52 RID: 19026 RVA: 0x000CA71C File Offset: 0x000C891C
		public int IndexOf(ConsumerConnectionPoint connectionPoint)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
