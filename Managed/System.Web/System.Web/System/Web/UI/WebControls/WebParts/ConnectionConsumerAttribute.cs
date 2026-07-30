using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Identifies the callback method in a server control acting as the consumer in a Web Parts connection, and enables developers to specify details about the consumer's connection point.</summary>
	// Token: 0x020007A7 RID: 1959
	[AttributeUsage(AttributeTargets.Method)]
	public class ConnectionConsumerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionConsumerAttribute" /> class, specifying a display name for the consumer connection point.</summary>
		/// <param name="displayName">A string that contains a friendly name for the consumer connection point to display in the user interface (UI).</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="displayName" /> is null. </exception>
		// Token: 0x06004EE9 RID: 20201 RVA: 0x0000393A File Offset: 0x00001B3A
		public ConnectionConsumerAttribute(string displayName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionConsumerAttribute" /> class, specifying a display name and an ID for the consumer connection point.</summary>
		/// <param name="displayName">A string that contains a friendly name for the consumer connection point to display in the user interface (UI).</param>
		/// <param name="id">The <see cref="P:System.Web.UI.WebControls.WebParts.ConnectionConsumerAttribute.ID" />, a unique string value assigned to the consumer connection point.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="displayName" /> or <paramref name="id" /> is null. </exception>
		// Token: 0x06004EEA RID: 20202 RVA: 0x0000393A File Offset: 0x00001B3A
		public ConnectionConsumerAttribute(string displayName, string id)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionConsumerAttribute" /> class, specifying a display name, an ID, and a specific type of connection point object to use for the consumer connection point.</summary>
		/// <param name="displayName">A string that contains a friendly name for the consumer connection point to display in the user interface (UI).</param>
		/// <param name="id">The <see cref="P:System.Web.UI.WebControls.WebParts.ConnectionConsumerAttribute.ID" />, a unique string value assigned to the consumer connection point.</param>
		/// <param name="connectionPointType">A <see cref="T:System.Type" /> that derives from <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" />, and that you want to specify as the type of connection point object to use with a specific callback method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="displayName" />, <paramref name="id" />, or<paramref name=" connectionPointType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="connectionPointType" /> is not valid.</exception>
		// Token: 0x06004EEB RID: 20203 RVA: 0x0000393A File Offset: 0x00001B3A
		public ConnectionConsumerAttribute(string displayName, string id, Type connectionPointType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionConsumerAttribute" /> class, specifying a display name and a specific type of connection point object to use for the consumer connection point.</summary>
		/// <param name="displayName">A string that contains a friendly name for the consumer connection point to display in the user interface (UI).</param>
		/// <param name="connectionPointType">A <see cref="T:System.Type" /> that derives from <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" />, and that you want to specify as the type of connection point object to use with a specific callback method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="displayName" /> or<paramref name=" connectionPointType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="connectionPointType" /> is not valid.</exception>
		// Token: 0x06004EEC RID: 20204 RVA: 0x0000393A File Offset: 0x00001B3A
		public ConnectionConsumerAttribute(string displayName, Type connectionPointType)
		{
		}

		/// <summary>Gets or sets a value that indicates whether the connection point allows multiple connections.</summary>
		/// <returns>true if the connection point accepts multiple connections; otherwise, false.</returns>
		// Token: 0x170017FA RID: 6138
		// (get) Token: 0x06004EED RID: 20205 RVA: 0x000CB5FC File Offset: 0x000C97FC
		// (set) Token: 0x06004EEE RID: 20206 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool AllowsMultipleConnections
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the connection point type of the consumer connection point.</summary>
		/// <returns>A <see cref="T:System.Type" /> that indicates the type of the connection point.</returns>
		/// <exception cref="T:System.InvalidOperationException">An invalid connection point type (one that does not derive from the <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> class) was used.</exception>
		// Token: 0x170017FB RID: 6139
		// (get) Token: 0x06004EEF RID: 20207 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Type ConnectionPointType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the friendly name of the consumer connection point.</summary>
		/// <returns>A string containing a friendly display name for the consumer connection point.</returns>
		// Token: 0x170017FC RID: 6140
		// (get) Token: 0x06004EF0 RID: 20208 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual string DisplayName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the string used as the value of the <see cref="P:System.Web.UI.WebControls.WebParts.ConnectionConsumerAttribute.DisplayName" /> property, for use in localization scenarios.</summary>
		/// <returns>A string that is used as the value of <see cref="P:System.Web.UI.WebControls.WebParts.ConnectionConsumerAttribute.DisplayName" />. </returns>
		// Token: 0x170017FD RID: 6141
		// (get) Token: 0x06004EF1 RID: 20209 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004EF2 RID: 20210 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected string DisplayNameValue
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

		/// <summary>Gets a string that represents the unique identity of the consumer connection point.</summary>
		/// <returns>The unique ID assigned to the consumer connection point. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170017FE RID: 6142
		// (get) Token: 0x06004EF3 RID: 20211 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string ID
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
