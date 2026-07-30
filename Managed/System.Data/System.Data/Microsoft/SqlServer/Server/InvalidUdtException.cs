using System;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Thrown when SQL Server or the ADO.NET <see cref="N:System.Data.SqlClient" /> provider detects an invalid user-defined type (UDT). </summary>
	// Token: 0x020003C1 RID: 961
	[Serializable]
	public sealed class InvalidUdtException : SystemException
	{
		// Token: 0x06002E37 RID: 11831 RVA: 0x000C84B1 File Offset: 0x000C66B1
		internal InvalidUdtException()
		{
			base.HResult = -2146232009;
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x000C84C4 File Offset: 0x000C66C4
		internal InvalidUdtException(string message)
			: base(message)
		{
			base.HResult = -2146232009;
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x000C84D8 File Offset: 0x000C66D8
		internal InvalidUdtException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232009;
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x000C84ED File Offset: 0x000C66ED
		private InvalidUdtException(SerializationInfo si, StreamingContext sc)
			: base(si, sc)
		{
		}

		/// <summary>Streams all the <see cref="T:Microsoft.SqlServer.Server.InvalidUdtException" /> properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class for the given <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x06002E3B RID: 11835 RVA: 0x000107C2 File Offset: 0x0000E9C2
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			base.GetObjectData(si, context);
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x000C84F8 File Offset: 0x000C66F8
		internal static InvalidUdtException Create(Type udtType, string resourceReason)
		{
			string @string = Res.GetString(resourceReason);
			InvalidUdtException ex = new InvalidUdtException(Res.GetString("'{0}' is an invalid user defined type, reason: {1}.", new object[] { udtType.FullName, @string }));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x020003C2 RID: 962
		private class HResults
		{
			// Token: 0x04001C08 RID: 7176
			internal const int InvalidUdt = -2146232009;
		}
	}
}
