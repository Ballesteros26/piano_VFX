using System;
using System.Globalization;
using System.Reflection;

namespace System.Net
{
	// Token: 0x0200044D RID: 1101
	internal class WebRequestPrefixElement
	{
		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060020BC RID: 8380 RVA: 0x0007F3FC File Offset: 0x0007D5FC
		// (set) Token: 0x060020BD RID: 8381 RVA: 0x0007F47C File Offset: 0x0007D67C
		public IWebRequestCreate Creator
		{
			get
			{
				if (this.creator == null && this.creatorType != null)
				{
					lock (this)
					{
						if (this.creator == null)
						{
							this.creator = (IWebRequestCreate)Activator.CreateInstance(this.creatorType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[0], CultureInfo.InvariantCulture);
						}
					}
				}
				return this.creator;
			}
			set
			{
				this.creator = value;
			}
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x0007F488 File Offset: 0x0007D688
		public WebRequestPrefixElement(string P, Type creatorType)
		{
			if (!typeof(IWebRequestCreate).IsAssignableFrom(creatorType))
			{
				throw new InvalidCastException(global::SR.GetString("Invalid cast from {0} to {1}.", new object[] { creatorType.AssemblyQualifiedName, "IWebRequestCreate" }));
			}
			this.Prefix = P;
			this.creatorType = creatorType;
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x0007F4E2 File Offset: 0x0007D6E2
		public WebRequestPrefixElement(string P, IWebRequestCreate C)
		{
			this.Prefix = P;
			this.Creator = C;
		}

		// Token: 0x04001D76 RID: 7542
		public string Prefix;

		// Token: 0x04001D77 RID: 7543
		internal IWebRequestCreate creator;

		// Token: 0x04001D78 RID: 7544
		internal Type creatorType;
	}
}
