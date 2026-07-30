using System;
using System.Runtime.Serialization;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001B9 RID: 441
	[RequiredByNativeCode]
	[Serializable]
	public class UnityException : SystemException
	{
		// Token: 0x060013EF RID: 5103 RVA: 0x00020AC1 File Offset: 0x0001ECC1
		public UnityException()
			: base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00020ADC File Offset: 0x0001ECDC
		public UnityException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x00020AF3 File Offset: 0x0001ECF3
		public UnityException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00020B0B File Offset: 0x0001ED0B
		protected UnityException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0400065A RID: 1626
		private const int Result = -2147467261;

		// Token: 0x0400065B RID: 1627
		private string unityStackTrace;
	}
}
