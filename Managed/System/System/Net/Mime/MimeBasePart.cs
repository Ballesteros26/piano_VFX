using System;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x0200059F RID: 1439
	internal class MimeBasePart
	{
		// Token: 0x06002CDF RID: 11487 RVA: 0x000020EB File Offset: 0x000002EB
		internal MimeBasePart()
		{
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x000B185A File Offset: 0x000AFA5A
		internal static bool ShouldUseBase64Encoding(Encoding encoding)
		{
			return encoding == Encoding.Unicode || encoding == Encoding.UTF8 || encoding == Encoding.UTF32 || encoding == Encoding.BigEndianUnicode;
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x000B187F File Offset: 0x000AFA7F
		internal static string EncodeHeaderValue(string value, Encoding encoding, bool base64Encoding)
		{
			return MimeBasePart.EncodeHeaderValue(value, encoding, base64Encoding, 0);
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x000B188C File Offset: 0x000AFA8C
		internal static string EncodeHeaderValue(string value, Encoding encoding, bool base64Encoding, int headerLength)
		{
			if (MimeBasePart.IsAscii(value, false))
			{
				return value;
			}
			if (encoding == null)
			{
				encoding = Encoding.GetEncoding("utf-8");
			}
			IEncodableStream encoderForHeader = new EncodedStreamFactory().GetEncoderForHeader(encoding, base64Encoding, headerLength);
			byte[] bytes = encoding.GetBytes(value);
			encoderForHeader.EncodeBytes(bytes, 0, bytes.Length);
			return encoderForHeader.GetEncodedString();
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x000B18DC File Offset: 0x000AFADC
		internal static string DecodeHeaderValue(string value)
		{
			if (value == null || value.Length == 0)
			{
				return string.Empty;
			}
			string text = string.Empty;
			string[] array = value.Split(new char[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[] { '?' });
				if (array2.Length != 5 || array2[0] != "=" || array2[4] != "=")
				{
					return value;
				}
				string text2 = array2[1];
				bool flag = array2[2] == "B";
				byte[] bytes = Encoding.ASCII.GetBytes(array2[3]);
				int num = new EncodedStreamFactory().GetEncoderForHeader(Encoding.GetEncoding(text2), flag, 0).DecodeBytes(bytes, 0, bytes.Length);
				Encoding encoding = Encoding.GetEncoding(text2);
				text += encoding.GetString(bytes, 0, num);
			}
			return text;
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x000B19CC File Offset: 0x000AFBCC
		internal static Encoding DecodeEncoding(string value)
		{
			if (value == null || value.Length == 0)
			{
				return null;
			}
			string[] array = value.Split(new char[] { '?', '\r', '\n' });
			if (array.Length < 5 || array[0] != "=" || array[4] != "=")
			{
				return null;
			}
			return Encoding.GetEncoding(array[1]);
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x000B1A2C File Offset: 0x000AFC2C
		internal static bool IsAscii(string value, bool permitCROrLF)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			foreach (char c in value)
			{
				if (c > '\u007f')
				{
					return false;
				}
				if (!permitCROrLF && (c == '\r' || c == '\n'))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x000B1A7C File Offset: 0x000AFC7C
		internal static bool IsAnsi(string value, bool permitCROrLF)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			foreach (char c in value)
			{
				if (c > 'ÿ')
				{
					return false;
				}
				if (!permitCROrLF && (c == '\r' || c == '\n'))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06002CE7 RID: 11495 RVA: 0x000B1ACC File Offset: 0x000AFCCC
		// (set) Token: 0x06002CE8 RID: 11496 RVA: 0x000B1ADF File Offset: 0x000AFCDF
		internal string ContentID
		{
			get
			{
				return this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentID)];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.ContentID));
					return;
				}
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentID)] = value;
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06002CE9 RID: 11497 RVA: 0x000B1B0D File Offset: 0x000AFD0D
		// (set) Token: 0x06002CEA RID: 11498 RVA: 0x000B1B20 File Offset: 0x000AFD20
		internal string ContentLocation
		{
			get
			{
				return this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentLocation)];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.ContentLocation));
					return;
				}
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentLocation)] = value;
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06002CEB RID: 11499 RVA: 0x000B1B50 File Offset: 0x000AFD50
		internal NameValueCollection Headers
		{
			get
			{
				if (this.headers == null)
				{
					this.headers = new HeaderCollection();
				}
				if (this.contentType == null)
				{
					this.contentType = new ContentType();
				}
				this.contentType.PersistIfNeeded(this.headers, false);
				if (this.contentDisposition != null)
				{
					this.contentDisposition.PersistIfNeeded(this.headers, false);
				}
				return this.headers;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06002CEC RID: 11500 RVA: 0x000B1BB5 File Offset: 0x000AFDB5
		// (set) Token: 0x06002CED RID: 11501 RVA: 0x000B1BD0 File Offset: 0x000AFDD0
		internal ContentType ContentType
		{
			get
			{
				if (this.contentType == null)
				{
					this.contentType = new ContentType();
				}
				return this.contentType;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.contentType = value;
				this.contentType.PersistIfNeeded((HeaderCollection)this.Headers, true);
			}
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x000B1C00 File Offset: 0x000AFE00
		internal void PrepareHeaders(bool allowUnicode)
		{
			this.contentType.PersistIfNeeded((HeaderCollection)this.Headers, false);
			this.headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentType), this.contentType.Encode(allowUnicode));
			if (this.contentDisposition != null)
			{
				this.contentDisposition.PersistIfNeeded((HeaderCollection)this.Headers, false);
				this.headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentDisposition), this.contentDisposition.Encode(allowUnicode));
			}
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x00004239 File Offset: 0x00002439
		internal virtual void Send(BaseWriter writer, bool allowUnicode)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x00004239 File Offset: 0x00002439
		internal virtual IAsyncResult BeginSend(BaseWriter writer, AsyncCallback callback, bool allowUnicode, object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x000B1C80 File Offset: 0x000AFE80
		internal void EndSend(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = asyncResult as MimeBasePart.MimePartAsyncResult;
			if (lazyAsyncResult == null || lazyAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException(global::SR.GetString("The IAsyncResult object was not returned from the corresponding asynchronous method on this class."), "asyncResult");
			}
			if (lazyAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(global::SR.GetString("{0} can only be called once for each asynchronous operation.", new object[] { "EndSend" }));
			}
			lazyAsyncResult.InternalWaitForCompletion();
			lazyAsyncResult.EndCalled = true;
			if (lazyAsyncResult.Result is Exception)
			{
				throw (Exception)lazyAsyncResult.Result;
			}
		}

		// Token: 0x0400251F RID: 9503
		protected ContentType contentType;

		// Token: 0x04002520 RID: 9504
		protected ContentDisposition contentDisposition;

		// Token: 0x04002521 RID: 9505
		private HeaderCollection headers;

		// Token: 0x04002522 RID: 9506
		internal const string defaultCharSet = "utf-8";

		// Token: 0x020005A0 RID: 1440
		internal class MimePartAsyncResult : LazyAsyncResult
		{
			// Token: 0x06002CF2 RID: 11506 RVA: 0x000B1D10 File Offset: 0x000AFF10
			internal MimePartAsyncResult(MimeBasePart part, object state, AsyncCallback callback)
				: base(part, state, callback)
			{
			}
		}
	}
}
