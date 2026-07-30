using System;
using System.Globalization;

namespace System
{
	// Token: 0x020000FC RID: 252
	public abstract class FormattableString : IFormattable
	{
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600096D RID: 2413
		public abstract string Format { get; }

		// Token: 0x0600096E RID: 2414
		public abstract object[] GetArguments();

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600096F RID: 2415
		public abstract int ArgumentCount { get; }

		// Token: 0x06000970 RID: 2416
		public abstract object GetArgument(int index);

		// Token: 0x06000971 RID: 2417
		public abstract string ToString(IFormatProvider formatProvider);

		// Token: 0x06000972 RID: 2418 RVA: 0x0003121D File Offset: 0x0002F41D
		string IFormattable.ToString(string ignored, IFormatProvider formatProvider)
		{
			return this.ToString(formatProvider);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00031226 File Offset: 0x0002F426
		public static string Invariant(FormattableString formattable)
		{
			if (formattable == null)
			{
				throw new ArgumentNullException("formattable");
			}
			return formattable.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00031241 File Offset: 0x0002F441
		public override string ToString()
		{
			return this.ToString(CultureInfo.CurrentCulture);
		}
	}
}
