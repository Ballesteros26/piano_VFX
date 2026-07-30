using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Web
{
	/// <summary>Encapsulates the HTTP intrinsic object that provides access to individual files that have been uploaded by a client.</summary>
	// Token: 0x0200003A RID: 58
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class HttpPostedFileWrapper : HttpPostedFileBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpPostedFileWrapper" /> class. </summary>
		/// <param name="httpPostedFile">The object that this wrapper class provides access to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="httpApplicationState" /> is null.</exception>
		// Token: 0x06000290 RID: 656 RVA: 0x00006F82 File Offset: 0x00005182
		public HttpPostedFileWrapper(HttpPostedFile httpPostedFile)
		{
			if (httpPostedFile == null)
			{
				throw new ArgumentNullException("httpPostedFile");
			}
			this._file = httpPostedFile;
		}

		/// <summary>Gets the size of an uploaded file, in bytes.</summary>
		/// <returns>The length of the file, in bytes.</returns>
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00006F9F File Offset: 0x0000519F
		public override int ContentLength
		{
			get
			{
				return this._file.ContentLength;
			}
		}

		/// <summary>Gets the MIME content type of an uploaded file.</summary>
		/// <returns>The MIME content type of the file.</returns>
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00006FAC File Offset: 0x000051AC
		public override string ContentType
		{
			get
			{
				return this._file.ContentType;
			}
		}

		/// <summary>Gets the fully qualified name of the file on the client.</summary>
		/// <returns>The name of the file on the client, which includes the directory path.</returns>
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00006FB9 File Offset: 0x000051B9
		public override string FileName
		{
			get
			{
				return this._file.FileName;
			}
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object that points to an uploaded file to prepare for reading the contents of the file.</summary>
		/// <returns>An object for reading a file.</returns>
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00006FC6 File Offset: 0x000051C6
		public override Stream InputStream
		{
			get
			{
				return this._file.InputStream;
			}
		}

		/// <summary>Saves the contents of an uploaded file.</summary>
		/// <param name="filename">The name of the file to save.</param>
		// Token: 0x06000295 RID: 661 RVA: 0x00006FD3 File Offset: 0x000051D3
		public override void SaveAs(string filename)
		{
			this._file.SaveAs(filename);
		}

		// Token: 0x04000D9D RID: 3485
		private HttpPostedFile _file;
	}
}
