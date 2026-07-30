using System;

namespace System.Web
{
	/// <summary>Specifies constants that indicate how the entity body of an HTTP request has been read.</summary>
	// Token: 0x02000054 RID: 84
	public enum ReadEntityBodyMode
	{
		/// <summary>The entity body has not been read.</summary>
		// Token: 0x04000E15 RID: 3605
		None,
		/// <summary>The entity body has already been read and its contents have been put into HTTP request collections like <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.Files" />, <see cref="P:System.Web.HttpRequest.InputStream" />, and <see cref="M:System.Web.HttpRequest.BinaryRead(System.Int32)" />.</summary>
		// Token: 0x04000E16 RID: 3606
		Classic,
		/// <summary>The entity body has been read into a <see cref="T:System.IO.Stream" /> object by using the <see cref="M:System.Web.HttpRequest.GetBufferlessInputStream" /> method.</summary>
		// Token: 0x04000E17 RID: 3607
		Bufferless,
		/// <summary>The entity body has been read into a <see cref="T:System.IO.Stream" /> object by using the <see cref="M:System.Web.HttpRequest.GetBufferedInputStream" /> method.</summary>
		// Token: 0x04000E18 RID: 3608
		Buffered
	}
}
