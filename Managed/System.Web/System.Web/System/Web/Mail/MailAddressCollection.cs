using System;
using System.Collections;
using System.Text;

namespace System.Web.Mail
{
	// Token: 0x020000F2 RID: 242
	internal class MailAddressCollection : IEnumerable
	{
		// Token: 0x17000487 RID: 1159
		public MailAddress this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x000234E3 File Offset: 0x000216E3
		public int Count
		{
			get
			{
				return this.data.Count;
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x000234F0 File Offset: 0x000216F0
		public void Add(MailAddress addr)
		{
			this.data.Add(addr);
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x000234FF File Offset: 0x000216FF
		public MailAddress Get(int index)
		{
			return (MailAddress)this.data[index];
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00023512 File Offset: 0x00021712
		public IEnumerator GetEnumerator()
		{
			return this.data.GetEnumerator();
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00023520 File Offset: 0x00021720
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.data.Count; i++)
			{
				MailAddress mailAddress = this.Get(i);
				stringBuilder.Append(mailAddress);
				if (i != this.data.Count - 1)
				{
					stringBuilder.Append(",\r\n  ");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0002357C File Offset: 0x0002177C
		public static MailAddressCollection Parse(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("Null is not allowed as an address string");
			}
			MailAddressCollection mailAddressCollection = new MailAddressCollection();
			string[] array = str.Split(new char[] { ',', ';' });
			for (int i = 0; i < array.Length; i++)
			{
				MailAddress mailAddress = MailAddress.Parse(array[i]);
				if (mailAddress != null)
				{
					mailAddressCollection.Add(mailAddress);
				}
			}
			return mailAddressCollection;
		}

		// Token: 0x04001126 RID: 4390
		protected ArrayList data = new ArrayList();
	}
}
