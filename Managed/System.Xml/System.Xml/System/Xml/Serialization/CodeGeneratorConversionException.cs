using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002D0 RID: 720
	internal class CodeGeneratorConversionException : Exception
	{
		// Token: 0x06001B1D RID: 6941 RVA: 0x00096A1A File Offset: 0x00094C1A
		public CodeGeneratorConversionException(Type sourceType, Type targetType, bool isAddress, string reason)
		{
			this.sourceType = sourceType;
			this.targetType = targetType;
			this.isAddress = isAddress;
			this.reason = reason;
		}

		// Token: 0x040015C7 RID: 5575
		private Type sourceType;

		// Token: 0x040015C8 RID: 5576
		private Type targetType;

		// Token: 0x040015C9 RID: 5577
		private bool isAddress;

		// Token: 0x040015CA RID: 5578
		private string reason;
	}
}
