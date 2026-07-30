using System;
using System.IO;
using System.Runtime.InteropServices;
using Mono.Mozilla.DOM;
using Mono.WebBrowser;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla
{
	// Token: 0x02000061 RID: 97
	internal class DocumentEncoder : DOMObject
	{
		// Token: 0x0600026D RID: 621 RVA: 0x00003D5C File Offset: 0x00001F5C
		public DocumentEncoder(WebBrowser control)
			: base(control)
		{
			IntPtr zero = IntPtr.Zero;
			this.control.ServiceManager.getServiceByContractID("@mozilla.org/layout/documentEncoder;1?type=text/html", typeof(nsIDocumentEncoder).GUID, out zero);
			if (zero == IntPtr.Zero)
			{
				throw new Mono.WebBrowser.Exception(Mono.WebBrowser.Exception.ErrorCodes.DocumentEncoderService);
			}
			try
			{
				this.docEncoder = (nsIDocumentEncoder)Marshal.GetObjectForIUnknown(zero);
			}
			catch (global::System.Exception)
			{
				throw new Mono.WebBrowser.Exception(Mono.WebBrowser.Exception.ErrorCodes.DocumentEncoderService);
			}
			if (control.platform != control.enginePlatform)
			{
				this.docEncoder = nsDocumentEncoder.GetProxy(control, this.docEncoder);
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00003E00 File Offset: 0x00002000
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.docEncoder = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00003E1B File Offset: 0x0000201B
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00003E36 File Offset: 0x00002036
		public string MimeType
		{
			get
			{
				if (this.mimeType == null)
				{
					this.mimeType = "text/html";
				}
				return this.mimeType;
			}
			set
			{
				this.mimeType = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00003E3F File Offset: 0x0000203F
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00003E47 File Offset: 0x00002047
		public DocumentEncoderFlags Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00003E50 File Offset: 0x00002050
		private void Init(Document document, string mimeType, DocumentEncoderFlags flags)
		{
			UniString uniString = new UniString(mimeType);
			try
			{
				this.docEncoder.init((nsIDOMDocument)document.nodeNoProxy, uniString.Handle, (uint)flags);
			}
			catch (global::System.Exception ex)
			{
				throw new Mono.WebBrowser.Exception(Mono.WebBrowser.Exception.ErrorCodes.DocumentEncoderService, ex);
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00003EA0 File Offset: 0x000020A0
		public string EncodeToString(Document document)
		{
			this.Init(document, this.MimeType, this.Flags);
			this.docEncoder.encodeToString(this.storage);
			return Base.StringGet(this.storage);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00003ED4 File Offset: 0x000020D4
		public string EncodeToString(HTMLElement element)
		{
			this.Init((Document)element.Owner, this.MimeType, this.Flags);
			this.docEncoder.setNode(element.nodeNoProxy);
			this.docEncoder.encodeToString(this.storage);
			string text = Base.StringGet(this.storage);
			string tagName = element.TagName;
			string text2 = "<" + tagName;
			foreach (object obj in element.Attributes)
			{
				IAttribute attribute = (IAttribute)obj;
				text2 = string.Concat(new string[] { text2, " ", attribute.Name, "=\"", attribute.Value, "\"" });
			}
			text2 = string.Concat(new string[] { text2, ">", text, "</", tagName, ">" });
			return text2;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00003FF8 File Offset: 0x000021F8
		public Stream EncodeToStream(Document document)
		{
			this.Init(document, this.MimeType, this.Flags);
			Stream stream = new Stream(new MemoryStream());
			this.docEncoder.encodeToStream(stream);
			return stream.BaseStream;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00004038 File Offset: 0x00002238
		public Stream EncodeToStream(HTMLElement element)
		{
			this.Init((Document)element.Owner, this.MimeType, this.Flags);
			this.docEncoder.setNode(element.nodeNoProxy);
			Stream stream = new Stream(new MemoryStream());
			this.docEncoder.encodeToStream(stream);
			return stream.BaseStream;
		}

		// Token: 0x040000D1 RID: 209
		private nsIDocumentEncoder docEncoder;

		// Token: 0x040000D2 RID: 210
		private string mimeType;

		// Token: 0x040000D3 RID: 211
		private DocumentEncoderFlags flags;
	}
}
