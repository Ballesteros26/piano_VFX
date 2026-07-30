using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x020001BC RID: 444
	[Serializable]
	public class MissingReferenceException : SystemException
	{
		// Token: 0x060013FB RID: 5115 RVA: 0x00020AC1 File Offset: 0x0001ECC1
		public MissingReferenceException()
			: base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00020ADC File Offset: 0x0001ECDC
		public MissingReferenceException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00020AF3 File Offset: 0x0001ECF3
		public MissingReferenceException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x00020B0B File Offset: 0x0001ED0B
		protected MissingReferenceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000660 RID: 1632
		private const int Result = -2147467261;

		// Token: 0x04000661 RID: 1633
		private string unityStackTrace;
	}
}
