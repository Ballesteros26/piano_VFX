using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Web
{
	/// <summary>Serves as the base class for classes that provide access to individual files that have been uploaded by a client.</summary>
	// Token: 0x02000039 RID: 57
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class HttpPostedFileBase
	{
		/// <summary>When overridden in a derived class, gets the size of an uploaded file, in bytes.</summary>
		/// <returns>The length of the file, in bytes.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int ContentLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the MIME content type of an uploaded file.</summary>
		/// <returns>The MIME content type of the file.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string ContentType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the fully qualified name of the file on the client.</summary>
		/// <returns>The name of the file on the client, which includes the directory path.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string FileName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a <see cref="T:System.IO.Stream" /> object that points to an uploaded file to prepare for reading the contents of the file.</summary>
		/// <returns>An object for reading a file.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Stream InputStream
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, saves the contents of an uploaded file.</summary>
		/// <param name="filename">The name of the file to save.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600028E RID: 654 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SaveAs(string filename)
		{
			throw new NotImplementedException();
		}
	}
}
