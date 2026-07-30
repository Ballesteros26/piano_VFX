using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x020001DD RID: 477
	internal class ImageListConverter : ComponentConverter
	{
		// Token: 0x06001E76 RID: 7798 RVA: 0x000723A4 File Offset: 0x000705A4
		public ImageListConverter()
			: base(typeof(ImageList))
		{
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x000723B8 File Offset: 0x000705B8
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
