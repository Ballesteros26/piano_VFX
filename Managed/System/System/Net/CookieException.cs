using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>The exception that is thrown when an error is made adding a <see cref="T:System.Net.Cookie" /> to a <see cref="T:System.Net.CookieContainer" />.</summary>
	// Token: 0x020004BD RID: 1213
	[Serializable]
	public class CookieException : FormatException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.CookieException" /> class.</summary>
		// Token: 0x060023D8 RID: 9176 RVA: 0x00023A5D File Offset: 0x00021C5D
		public CookieException()
		{
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x00023A65 File Offset: 0x00021C65
		internal CookieException(string message)
			: base(message)
		{
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x00023A6E File Offset: 0x00021C6E
		internal CookieException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.CookieException" /> class with specific values of <paramref name="serializationInfo" /> and <paramref name="streamingContext" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used. </param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> to be used. </param>
		// Token: 0x060023DB RID: 9179 RVA: 0x00023A78 File Offset: 0x00021C78
		protected CookieException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.CookieException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used. </param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> to be used. </param>
		// Token: 0x060023DC RID: 9180 RVA: 0x00023A82 File Offset: 0x00021C82
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.CookieException" />.</summary>
		/// <param name="serializationInfo">The object that holds the serialized object data. The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">The contextual information about the source or destination. A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x060023DD RID: 9181 RVA: 0x00023A82 File Offset: 0x00021C82
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}
	}
}
