using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public class CredentialException : Win32Exception
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00003142 File Offset: 0x00001342
		[SecurityPermission(6, Flags = 2)]
		public CredentialException()
			: base(Resources.CredentialError)
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003151 File Offset: 0x00001351
		[SecurityPermission(6, Flags = 2)]
		public CredentialException(int error)
			: base(error)
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000315C File Offset: 0x0000135C
		[SecurityPermission(6, Flags = 2)]
		public CredentialException(string message)
			: base(message)
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003167 File Offset: 0x00001367
		[SecurityPermission(6, Flags = 2)]
		public CredentialException(int error, string message)
			: base(error, message)
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003173 File Offset: 0x00001373
		[SecurityPermission(6, Flags = 2)]
		public CredentialException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000317F File Offset: 0x0000137F
		[SecurityPermission(6, Flags = 2)]
		protected CredentialException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
