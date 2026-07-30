using System;

namespace System.Net
{
	/// <summary>Container class for <see cref="T:System.Net.WebRequestMethods.Ftp" />, <see cref="T:System.Net.WebRequestMethods.File" />, and <see cref="T:System.Net.WebRequestMethods.Http" /> classes. This class cannot be inherited</summary>
	// Token: 0x02000476 RID: 1142
	public static class WebRequestMethods
	{
		/// <summary>Represents the types of FTP protocol methods that can be used with an FTP request. This class cannot be inherited.</summary>
		// Token: 0x02000477 RID: 1143
		public static class Ftp
		{
			/// <summary>Represents the FTP RETR protocol method that is used to download a file from an FTP server.</summary>
			// Token: 0x04001E72 RID: 7794
			public const string DownloadFile = "RETR";

			/// <summary>Represents the FTP NLIST protocol method that gets a short listing of the files on an FTP server.</summary>
			// Token: 0x04001E73 RID: 7795
			public const string ListDirectory = "NLST";

			/// <summary>Represents the FTP STOR protocol method that uploads a file to an FTP server.</summary>
			// Token: 0x04001E74 RID: 7796
			public const string UploadFile = "STOR";

			/// <summary>Represents the FTP DELE protocol method that is used to delete a file on an FTP server.</summary>
			// Token: 0x04001E75 RID: 7797
			public const string DeleteFile = "DELE";

			/// <summary>Represents the FTP APPE protocol method that is used to append a file to an existing file on an FTP server.</summary>
			// Token: 0x04001E76 RID: 7798
			public const string AppendFile = "APPE";

			/// <summary>Represents the FTP SIZE protocol method that is used to retrieve the size of a file on an FTP server.</summary>
			// Token: 0x04001E77 RID: 7799
			public const string GetFileSize = "SIZE";

			/// <summary>Represents the FTP STOU protocol that uploads a file with a unique name to an FTP server.</summary>
			// Token: 0x04001E78 RID: 7800
			public const string UploadFileWithUniqueName = "STOU";

			/// <summary>Represents the FTP MKD protocol method creates a directory on an FTP server.</summary>
			// Token: 0x04001E79 RID: 7801
			public const string MakeDirectory = "MKD";

			/// <summary>Represents the FTP RMD protocol method that removes a directory.</summary>
			// Token: 0x04001E7A RID: 7802
			public const string RemoveDirectory = "RMD";

			/// <summary>Represents the FTP LIST protocol method that gets a detailed listing of the files on an FTP server.</summary>
			// Token: 0x04001E7B RID: 7803
			public const string ListDirectoryDetails = "LIST";

			/// <summary>Represents the FTP MDTM protocol method that is used to retrieve the date-time stamp from a file on an FTP server.</summary>
			// Token: 0x04001E7C RID: 7804
			public const string GetDateTimestamp = "MDTM";

			/// <summary>Represents the FTP PWD protocol method that prints the name of the current working directory.</summary>
			// Token: 0x04001E7D RID: 7805
			public const string PrintWorkingDirectory = "PWD";

			/// <summary>Represents the FTP RENAME protocol method that renames a directory.</summary>
			// Token: 0x04001E7E RID: 7806
			public const string Rename = "RENAME";
		}

		/// <summary>Represents the types of HTTP protocol methods that can be used with an HTTP request.</summary>
		// Token: 0x02000478 RID: 1144
		public static class Http
		{
			/// <summary>Represents an HTTP GET protocol method. </summary>
			// Token: 0x04001E7F RID: 7807
			public const string Get = "GET";

			/// <summary>Represents the HTTP CONNECT protocol method that is used with a proxy that can dynamically switch to tunneling, as in the case of SSL tunneling.</summary>
			// Token: 0x04001E80 RID: 7808
			public const string Connect = "CONNECT";

			/// <summary>Represents an HTTP HEAD protocol method. The HEAD method is identical to GET except that the server only returns message-headers in the response, without a message-body.</summary>
			// Token: 0x04001E81 RID: 7809
			public const string Head = "HEAD";

			/// <summary>Represents an HTTP PUT protocol method that is used to replace an entity identified by a URI.</summary>
			// Token: 0x04001E82 RID: 7810
			public const string Put = "PUT";

			/// <summary>Represents an HTTP POST protocol method that is used to post a new entity as an addition to a URI.</summary>
			// Token: 0x04001E83 RID: 7811
			public const string Post = "POST";

			/// <summary>Represents an HTTP MKCOL request that creates a new collection (such as a collection of pages) at the location specified by the request-Uniform Resource Identifier (URI).</summary>
			// Token: 0x04001E84 RID: 7812
			public const string MkCol = "MKCOL";
		}

		/// <summary>Represents the types of file protocol methods that can be used with a FILE request. This class cannot be inherited.</summary>
		// Token: 0x02000479 RID: 1145
		public static class File
		{
			/// <summary>Represents the FILE GET protocol method that is used to retrieve a file from a specified location.</summary>
			// Token: 0x04001E85 RID: 7813
			public const string DownloadFile = "GET";

			/// <summary>Represents the FILE PUT protocol method that is used to copy a file to a specified location.</summary>
			// Token: 0x04001E86 RID: 7814
			public const string UploadFile = "PUT";
		}
	}
}
