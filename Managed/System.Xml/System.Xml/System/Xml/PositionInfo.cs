using System;

namespace System.Xml
{
	// Token: 0x02000244 RID: 580
	internal class PositionInfo : IXmlLineInfo
	{
		// Token: 0x06001682 RID: 5762 RVA: 0x0000226C File Offset: 0x0000046C
		public virtual bool HasLineInfo()
		{
			return false;
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x0000226C File Offset: 0x0000046C
		public virtual int LineNumber
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x0000226C File Offset: 0x0000046C
		public virtual int LinePosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0007C25C File Offset: 0x0007A45C
		public static PositionInfo GetPositionInfo(object o)
		{
			IXmlLineInfo xmlLineInfo = o as IXmlLineInfo;
			if (xmlLineInfo != null)
			{
				return new ReaderPositionInfo(xmlLineInfo);
			}
			return new PositionInfo();
		}
	}
}
