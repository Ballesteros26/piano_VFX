using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x020001BA RID: 442
	[Serializable]
	public class MissingComponentException : SystemException
	{
		// Token: 0x060013F3 RID: 5107 RVA: 0x00020AC1 File Offset: 0x0001ECC1
		public MissingComponentException()
			: base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00020ADC File Offset: 0x0001ECDC
		public MissingComponentException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x00020AF3 File Offset: 0x0001ECF3
		public MissingComponentException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x00020B0B File Offset: 0x0001ED0B
		protected MissingComponentException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0400065C RID: 1628
		private const int Result = -2147467261;

		// Token: 0x0400065D RID: 1629
		private string unityStackTrace;
	}
}
