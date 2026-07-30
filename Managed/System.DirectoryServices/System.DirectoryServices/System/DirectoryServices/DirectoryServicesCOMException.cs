using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.DirectoryServices
{
	/// <summary>Contains extended error information about an error that occurred when the <see cref="M:System.DirectoryServices.DirectoryEntry.Invoke(System.String,System.Object[])" /> method is called. </summary>
	// Token: 0x02000016 RID: 22
	[Serializable]
	public class DirectoryServicesCOMException : COMException, ISerializable
	{
		/// <summary>Gets the extended error code.</summary>
		/// <returns>The extended error code.</returns>
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000208C File Offset: 0x0000028C
		public int ExtendedError
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the extended error message.</summary>
		/// <returns>The extended error message.</returns>
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000208C File Offset: 0x0000028C
		public string ExtendedErrorMessage
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryServicesCOMException" /> class. </summary>
		// Token: 0x060000D1 RID: 209 RVA: 0x00003C9F File Offset: 0x00001E9F
		public DirectoryServicesCOMException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryServicesCOMException" /> class with the specified string.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x060000D2 RID: 210 RVA: 0x00003CA7 File Offset: 0x00001EA7
		public DirectoryServicesCOMException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryServicesCOMException" /> class with the specified string and exception.</summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="inner">The exception that is the cause of the current exception.</param>
		// Token: 0x060000D3 RID: 211 RVA: 0x00003CB0 File Offset: 0x00001EB0
		public DirectoryServicesCOMException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryServicesCOMException" /> class with the specified serialization information and streaming context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> destination for this serialization.</param>
		// Token: 0x060000D4 RID: 212 RVA: 0x00003CBA File Offset: 0x00001EBA
		protected DirectoryServicesCOMException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Populates the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the <see cref="T:System.DirectoryServices.DirectoryServicesCOMException" /> object.          </summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that is the destination for this serialization.</param>
		// Token: 0x060000D5 RID: 213 RVA: 0x0000208C File Offset: 0x0000028C
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new NotImplementedException();
		}
	}
}
