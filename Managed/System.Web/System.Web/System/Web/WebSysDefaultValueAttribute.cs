using System;
using System.ComponentModel;

namespace System.Web
{
	// Token: 0x0200005E RID: 94
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class WebSysDefaultValueAttribute : DefaultValueAttribute
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x00007457 File Offset: 0x00005657
		internal WebSysDefaultValueAttribute(Type type, string value)
			: base(value)
		{
			this._type = type;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00007467 File Offset: 0x00005667
		internal WebSysDefaultValueAttribute(string value)
			: base(value)
		{
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00007470 File Offset: 0x00005670
		public override object TypeId
		{
			get
			{
				return typeof(DefaultValueAttribute);
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000747C File Offset: 0x0000567C
		public override object Value
		{
			get
			{
				if (!this._localized)
				{
					this._localized = true;
					string text = (string)base.Value;
					if (!string.IsNullOrEmpty(text))
					{
						object obj = global::SR.GetString(text);
						if (this._type != null)
						{
							try
							{
								obj = TypeDescriptor.GetConverter(this._type).ConvertFromInvariantString((string)obj);
							}
							catch (NotSupportedException)
							{
								obj = null;
							}
						}
						base.SetValue(obj);
					}
				}
				return base.Value;
			}
		}

		// Token: 0x04000E35 RID: 3637
		private Type _type;

		// Token: 0x04000E36 RID: 3638
		private bool _localized;
	}
}
