using System;
using System.IO;

namespace System.Windows.Forms
{
	/// <summary>Defines a method that opens a file from the current directory.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001CF RID: 463
	public interface IFileReaderService
	{
		/// <summary>Opens a file from the current directory.</summary>
		/// <param name="relativePath">The file to open.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DFF RID: 7679
		Stream OpenFileFromSource(string relativePath);
	}
}
