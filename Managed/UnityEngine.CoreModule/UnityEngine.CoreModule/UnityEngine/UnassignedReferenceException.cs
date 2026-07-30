using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x020001BB RID: 443
	[Serializable]
	public class UnassignedReferenceException : SystemException
	{
		// Token: 0x060013F7 RID: 5111 RVA: 0x00020AC1 File Offset: 0x0001ECC1
		public UnassignedReferenceException()
			: base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00020ADC File Offset: 0x0001ECDC
		public UnassignedReferenceException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00020AF3 File Offset: 0x0001ECF3
		public UnassignedReferenceException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x00020B0B File Offset: 0x0001ED0B
		protected UnassignedReferenceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0400065E RID: 1630
		private const int Result = -2147467261;

		// Token: 0x0400065F RID: 1631
		private string unityStackTrace;
	}
}
