using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000849 RID: 2121
	public static class FormattableStringFactory
	{
		// Token: 0x060053F0 RID: 21488 RVA: 0x00126D99 File Offset: 0x00124F99
		public static FormattableString Create(string format, params object[] arguments)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (arguments == null)
			{
				throw new ArgumentNullException("arguments");
			}
			return new FormattableStringFactory.ConcreteFormattableString(format, arguments);
		}

		// Token: 0x0200084A RID: 2122
		private sealed class ConcreteFormattableString : FormattableString
		{
			// Token: 0x060053F1 RID: 21489 RVA: 0x00126DBE File Offset: 0x00124FBE
			internal ConcreteFormattableString(string format, object[] arguments)
			{
				this._format = format;
				this._arguments = arguments;
			}

			// Token: 0x17000EAA RID: 3754
			// (get) Token: 0x060053F2 RID: 21490 RVA: 0x00126DD4 File Offset: 0x00124FD4
			public override string Format
			{
				get
				{
					return this._format;
				}
			}

			// Token: 0x060053F3 RID: 21491 RVA: 0x00126DDC File Offset: 0x00124FDC
			public override object[] GetArguments()
			{
				return this._arguments;
			}

			// Token: 0x17000EAB RID: 3755
			// (get) Token: 0x060053F4 RID: 21492 RVA: 0x00126DE4 File Offset: 0x00124FE4
			public override int ArgumentCount
			{
				get
				{
					return this._arguments.Length;
				}
			}

			// Token: 0x060053F5 RID: 21493 RVA: 0x00126DEE File Offset: 0x00124FEE
			public override object GetArgument(int index)
			{
				return this._arguments[index];
			}

			// Token: 0x060053F6 RID: 21494 RVA: 0x00126DF8 File Offset: 0x00124FF8
			public override string ToString(IFormatProvider formatProvider)
			{
				return string.Format(formatProvider, this._format, this._arguments);
			}

			// Token: 0x04002B9E RID: 11166
			private readonly string _format;

			// Token: 0x04002B9F RID: 11167
			private readonly object[] _arguments;
		}
	}
}
