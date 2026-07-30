using System;
using System.Collections;
using System.IO;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents a collection of temporary files.</summary>
	// Token: 0x020007A2 RID: 1954
	[Serializable]
	public class TempFileCollection : ICollection, IEnumerable, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> class with default values.</summary>
		// Token: 0x06003DD8 RID: 15832 RVA: 0x000DAD3D File Offset: 0x000D8F3D
		public TempFileCollection()
			: this(null, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> class using the specified temporary directory that is set to delete the temporary files after their generation and use, by default.</summary>
		/// <param name="tempDir">A path to the temporary directory to use for storing the temporary files. </param>
		// Token: 0x06003DD9 RID: 15833 RVA: 0x000DAD47 File Offset: 0x000D8F47
		public TempFileCollection(string tempDir)
			: this(tempDir, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> class using the specified temporary directory and specified value indicating whether to keep or delete the temporary files after their generation and use, by default.</summary>
		/// <param name="tempDir">A path to the temporary directory to use for storing the temporary files. </param>
		/// <param name="keepFiles">true if the temporary files should be kept after use; false if the temporary files should be deleted. </param>
		// Token: 0x06003DDA RID: 15834 RVA: 0x000DAD51 File Offset: 0x000D8F51
		public TempFileCollection(string tempDir, bool keepFiles)
		{
			this.KeepFiles = keepFiles;
			this._tempDir = tempDir;
			this._files = new Hashtable(StringComparer.OrdinalIgnoreCase);
		}

		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. </summary>
		// Token: 0x06003DDB RID: 15835 RVA: 0x000DAD77 File Offset: 0x000D8F77
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003DDC RID: 15836 RVA: 0x000DAD86 File Offset: 0x000D8F86
		protected virtual void Dispose(bool disposing)
		{
			this.SafeDelete();
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x000DAD90 File Offset: 0x000D8F90
		~TempFileCollection()
		{
			this.Dispose(false);
		}

		/// <summary>Adds a file name with the specified file name extension to the collection.</summary>
		/// <returns>A file name with the specified extension that was just added to the collection.</returns>
		/// <param name="fileExtension">The file name extension for the auto-generated temporary file name to add to the collection. </param>
		// Token: 0x06003DDE RID: 15838 RVA: 0x000DADC0 File Offset: 0x000D8FC0
		public string AddExtension(string fileExtension)
		{
			return this.AddExtension(fileExtension, this.KeepFiles);
		}

		/// <summary>Adds a file name with the specified file name extension to the collection, using the specified value indicating whether the file should be deleted or retained.</summary>
		/// <returns>A file name with the specified extension that was just added to the collection.</returns>
		/// <param name="fileExtension">The file name extension for the auto-generated temporary file name to add to the collection. </param>
		/// <param name="keepFile">true if the file should be kept after use; false if the file should be deleted. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fileExtension" /> is null or an empty string.</exception>
		// Token: 0x06003DDF RID: 15839 RVA: 0x000DADD0 File Offset: 0x000D8FD0
		public string AddExtension(string fileExtension, bool keepFile)
		{
			if (string.IsNullOrEmpty(fileExtension))
			{
				throw new ArgumentException(global::SR.Format("Argument {0} cannot be null or zero-length.", "fileExtension"), "fileExtension");
			}
			string text = this.BasePath + "." + fileExtension;
			this.AddFile(text, keepFile);
			return text;
		}

		/// <summary>Adds the specified file to the collection, using the specified value indicating whether to keep the file after the collection is disposed or when the <see cref="M:System.CodeDom.Compiler.TempFileCollection.Delete" /> method is called.</summary>
		/// <param name="fileName">The name of the file to add to the collection. </param>
		/// <param name="keepFile">true if the file should be kept after use; false if the file should be deleted. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fileName" /> is null or an empty string.-or-<paramref name="fileName" /> is a duplicate.</exception>
		// Token: 0x06003DE0 RID: 15840 RVA: 0x000DAE1C File Offset: 0x000D901C
		public void AddFile(string fileName, bool keepFile)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				throw new ArgumentException(global::SR.Format("Argument {0} cannot be null or zero-length.", "fileName"), "fileName");
			}
			if (this._files[fileName] != null)
			{
				throw new ArgumentException(global::SR.Format("The file name '{0}' was already in the collection.", fileName), "fileName");
			}
			this._files.Add(fileName, keepFile);
		}

		/// <summary>Gets an enumerator that can enumerate the members of the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that contains the collection's members.</returns>
		// Token: 0x06003DE1 RID: 15841 RVA: 0x000DAE81 File Offset: 0x000D9081
		public IEnumerator GetEnumerator()
		{
			return this._files.Keys.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection. </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06003DE2 RID: 15842 RVA: 0x000DAE81 File Offset: 0x000D9081
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._files.Keys.GetEnumerator();
		}

		/// <summary>Copies the elements of the collection to an array, starting at the specified index of the target array. </summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="start">The zero-based index in array at which copying begins.</param>
		// Token: 0x06003DE3 RID: 15843 RVA: 0x000DAE93 File Offset: 0x000D9093
		void ICollection.CopyTo(Array array, int start)
		{
			this._files.Keys.CopyTo(array, start);
		}

		/// <summary>Copies the members of the collection to the specified string, beginning at the specified index.</summary>
		/// <param name="fileNames">The array of strings to copy to. </param>
		/// <param name="start">The index of the array to begin copying to. </param>
		// Token: 0x06003DE4 RID: 15844 RVA: 0x000DAE93 File Offset: 0x000D9093
		public void CopyTo(string[] fileNames, int start)
		{
			this._files.Keys.CopyTo(fileNames, start);
		}

		/// <summary>Gets the number of files in the collection.</summary>
		/// <returns>The number of files in the collection.</returns>
		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06003DE5 RID: 15845 RVA: 0x000DAEA7 File Offset: 0x000D90A7
		public int Count
		{
			get
			{
				return this._files.Count;
			}
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06003DE6 RID: 15846 RVA: 0x000DAEA7 File Offset: 0x000D90A7
		int ICollection.Count
		{
			get
			{
				return this._files.Count;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06003DE7 RID: 15847 RVA: 0x00009E57 File Offset: 0x00008057
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06003DE8 RID: 15848 RVA: 0x00004240 File Offset: 0x00002440
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the temporary directory to store the temporary files in.</summary>
		/// <returns>The temporary directory to store the temporary files in.</returns>
		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06003DE9 RID: 15849 RVA: 0x000DAEB4 File Offset: 0x000D90B4
		public string TempDir
		{
			get
			{
				return this._tempDir ?? string.Empty;
			}
		}

		/// <summary>Gets the full path to the base file name, without a file name extension, on the temporary directory path, that is used to generate temporary file names for the collection.</summary>
		/// <returns>The full path to the base file name, without a file name extension, on the temporary directory path, that is used to generate temporary file names for the collection.</returns>
		/// <exception cref="T:System.Security.SecurityException">If the <see cref="P:System.CodeDom.Compiler.TempFileCollection.BasePath" /> property has not been set or is set to null, and <see cref="F:System.Security.Permissions.FileIOPermissionAccess.AllAccess" /> is not granted for the temporary directory indicated by the <see cref="P:System.CodeDom.Compiler.TempFileCollection.TempDir" /> property. </exception>
		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06003DEA RID: 15850 RVA: 0x000DAEC5 File Offset: 0x000D90C5
		public string BasePath
		{
			get
			{
				this.EnsureTempNameCreated();
				return this._basePath;
			}
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x000DAED4 File Offset: 0x000D90D4
		private void EnsureTempNameCreated()
		{
			if (this._basePath == null)
			{
				string text = null;
				bool flag = false;
				int num = 5000;
				do
				{
					this._basePath = Path.Combine(string.IsNullOrEmpty(this.TempDir) ? Path.GetTempPath() : this.TempDir, Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));
					text = this._basePath + ".tmp";
					try
					{
						new FileStream(text, FileMode.CreateNew, FileAccess.Write).Dispose();
						flag = true;
					}
					catch (IOException ex)
					{
						num--;
						if (num == 0 || ex is DirectoryNotFoundException)
						{
							throw;
						}
						flag = false;
					}
				}
				while (!flag);
				this._files.Add(text, this.KeepFiles);
			}
		}

		/// <summary>Gets or sets a value indicating whether to keep the files, by default, when the <see cref="M:System.CodeDom.Compiler.TempFileCollection.Delete" /> method is called or the collection is disposed.</summary>
		/// <returns>true if the files should be kept; otherwise, false.</returns>
		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06003DEC RID: 15852 RVA: 0x000DAF88 File Offset: 0x000D9188
		// (set) Token: 0x06003DED RID: 15853 RVA: 0x000DAF90 File Offset: 0x000D9190
		public bool KeepFiles { get; set; }

		// Token: 0x06003DEE RID: 15854 RVA: 0x000DAF9C File Offset: 0x000D919C
		private bool KeepFile(string fileName)
		{
			object obj = this._files[fileName];
			return obj != null && (bool)obj;
		}

		/// <summary>Deletes the temporary files within this collection that were not marked to be kept.</summary>
		// Token: 0x06003DEF RID: 15855 RVA: 0x000DAD86 File Offset: 0x000D8F86
		public void Delete()
		{
			this.SafeDelete();
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x000DAFC4 File Offset: 0x000D91C4
		internal void Delete(string fileName)
		{
			try
			{
				File.Delete(fileName);
			}
			catch
			{
			}
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x000DAFEC File Offset: 0x000D91EC
		internal void SafeDelete()
		{
			if (this._files != null && this._files.Count > 0)
			{
				string[] array = new string[this._files.Count];
				this._files.Keys.CopyTo(array, 0);
				foreach (string text in array)
				{
					if (!this.KeepFile(text))
					{
						this.Delete(text);
						this._files.Remove(text);
					}
				}
			}
		}

		// Token: 0x04002E22 RID: 11810
		private string _basePath;

		// Token: 0x04002E23 RID: 11811
		private readonly string _tempDir;

		// Token: 0x04002E24 RID: 11812
		private readonly Hashtable _files;
	}
}
