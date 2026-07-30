using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Resources
{
	/// <summary>Stores all the resources localized for one particular culture, ignoring all other cultures, including any fallback rules.</summary>
	// Token: 0x020002AC RID: 684
	[ComVisible(true)]
	[Serializable]
	public class ResourceSet : IDisposable, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceSet" /> class with default properties.</summary>
		// Token: 0x06001F76 RID: 8054 RVA: 0x0007B665 File Offset: 0x00079865
		protected ResourceSet()
		{
			this.CommonInit();
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x00002111 File Offset: 0x00000311
		internal ResourceSet(bool junk)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Resources.ResourceSet" /> class using the system default <see cref="T:System.Resources.ResourceReader" /> that opens and reads resources from the given file.</summary>
		/// <param name="fileName">Resource file to read. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="fileName" /> parameter is null. </exception>
		// Token: 0x06001F78 RID: 8056 RVA: 0x0007B673 File Offset: 0x00079873
		public ResourceSet(string fileName)
		{
			this.Reader = new ResourceReader(fileName);
			this.CommonInit();
			this.ReadResources();
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Resources.ResourceSet" /> class using the system default <see cref="T:System.Resources.ResourceReader" /> that reads resources from the given stream.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> of resources to be read. The stream should refer to an existing resources file. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> is not readable. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stream" /> parameter is null. </exception>
		// Token: 0x06001F79 RID: 8057 RVA: 0x0007B693 File Offset: 0x00079893
		[SecurityCritical]
		public ResourceSet(Stream stream)
		{
			this.Reader = new ResourceReader(stream);
			this.CommonInit();
			this.ReadResources();
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Resources.ResourceSet" /> class using the specified resource reader.</summary>
		/// <param name="reader">The reader that will be used. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="reader" /> parameter is null. </exception>
		// Token: 0x06001F7A RID: 8058 RVA: 0x0007B6B3 File Offset: 0x000798B3
		public ResourceSet(IResourceReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Reader = reader;
			this.CommonInit();
			this.ReadResources();
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x0007B6DC File Offset: 0x000798DC
		private void CommonInit()
		{
			this.Table = new Hashtable();
		}

		/// <summary>Closes and releases any resources used by this <see cref="T:System.Resources.ResourceSet" />.</summary>
		// Token: 0x06001F7C RID: 8060 RVA: 0x0007B6E9 File Offset: 0x000798E9
		public virtual void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases resources (other than memory) associated with the current instance, closing internal managed objects if requested.</summary>
		/// <param name="disposing">Indicates whether the objects contained in the current instance should be explicitly closed. </param>
		// Token: 0x06001F7D RID: 8061 RVA: 0x0007B6F4 File Offset: 0x000798F4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				IResourceReader reader = this.Reader;
				this.Reader = null;
				if (reader != null)
				{
					reader.Close();
				}
			}
			this.Reader = null;
			this._caseInsensitiveTable = null;
			this.Table = null;
		}

		/// <summary>Disposes of the resources (other than memory) used by the current instance of <see cref="T:System.Resources.ResourceSet" />.</summary>
		// Token: 0x06001F7E RID: 8062 RVA: 0x0007B6E9 File Offset: 0x000798E9
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Returns the preferred resource reader class for this kind of <see cref="T:System.Resources.ResourceSet" />.</summary>
		/// <returns>Returns the <see cref="T:System.Type" /> for the preferred resource reader for this kind of <see cref="T:System.Resources.ResourceSet" />.</returns>
		// Token: 0x06001F7F RID: 8063 RVA: 0x0007B730 File Offset: 0x00079930
		public virtual Type GetDefaultReader()
		{
			return typeof(ResourceReader);
		}

		/// <summary>Returns the preferred resource writer class for this kind of <see cref="T:System.Resources.ResourceSet" />.</summary>
		/// <returns>Returns the <see cref="T:System.Type" /> for the preferred resource writer for this kind of <see cref="T:System.Resources.ResourceSet" />.</returns>
		// Token: 0x06001F80 RID: 8064 RVA: 0x0007B73C File Offset: 0x0007993C
		public virtual Type GetDefaultWriter()
		{
			return typeof(ResourceWriter);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that can iterate through the <see cref="T:System.Resources.ResourceSet" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for this <see cref="T:System.Resources.ResourceSet" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The resource set has been closed or disposed. </exception>
		// Token: 0x06001F81 RID: 8065 RVA: 0x0007B748 File Offset: 0x00079948
		[ComVisible(false)]
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> object to avoid a race condition with Dispose. This member is not intended to be used directly from your code.</summary>
		/// <returns>An enumerator for the current <see cref="T:System.Resources.ResourceSet" /> object.</returns>
		// Token: 0x06001F82 RID: 8066 RVA: 0x0007B748 File Offset: 0x00079948
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x0007B750 File Offset: 0x00079950
		private IDictionaryEnumerator GetEnumeratorHelper()
		{
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed resource set."));
			}
			return table.GetEnumerator();
		}

		/// <summary>Searches for a <see cref="T:System.String" /> resource with the specified name.</summary>
		/// <returns>The value of a resource, if the value is a <see cref="T:System.String" />.</returns>
		/// <param name="name">Name of the resource to search for. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The resource specified by <paramref name="name" /> is not a <see cref="T:System.String" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been closed or disposed.</exception>
		// Token: 0x06001F84 RID: 8068 RVA: 0x0007B774 File Offset: 0x00079974
		public virtual string GetString(string name)
		{
			object objectInternal = this.GetObjectInternal(name);
			string text;
			try
			{
				text = (string)objectInternal;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Resource '{0}' was not a String - call GetObject instead.", new object[] { name }));
			}
			return text;
		}

		/// <summary>Searches for a <see cref="T:System.String" /> resource with the specified name in a case-insensitive manner, if requested.</summary>
		/// <returns>The value of a resource, if the value is a <see cref="T:System.String" />.</returns>
		/// <param name="name">Name of the resource to search for. </param>
		/// <param name="ignoreCase">Indicates whether the case of the case of the specified name should be ignored. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The resource specified by <paramref name="name" /> is not a <see cref="T:System.String" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been closed or disposed.</exception>
		// Token: 0x06001F85 RID: 8069 RVA: 0x0007B7C0 File Offset: 0x000799C0
		public virtual string GetString(string name, bool ignoreCase)
		{
			object obj = this.GetObjectInternal(name);
			string text;
			try
			{
				text = (string)obj;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Resource '{0}' was not a String - call GetObject instead.", new object[] { name }));
			}
			if (text != null || !ignoreCase)
			{
				return text;
			}
			obj = this.GetCaseInsensitiveObjectInternal(name);
			string text2;
			try
			{
				text2 = (string)obj;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Resource '{0}' was not a String - call GetObject instead.", new object[] { name }));
			}
			return text2;
		}

		/// <summary>Searches for a resource object with the specified name.</summary>
		/// <returns>The requested resource.</returns>
		/// <param name="name">Case-sensitive name of the resource to search for. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been closed or disposed.</exception>
		// Token: 0x06001F86 RID: 8070 RVA: 0x0007B84C File Offset: 0x00079A4C
		public virtual object GetObject(string name)
		{
			return this.GetObjectInternal(name);
		}

		/// <summary>Searches for a resource object with the specified name in a case-insensitive manner, if requested.</summary>
		/// <returns>The requested resource.</returns>
		/// <param name="name">Name of the resource to search for. </param>
		/// <param name="ignoreCase">Indicates whether the case of the specified name should be ignored. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been closed or disposed.</exception>
		// Token: 0x06001F87 RID: 8071 RVA: 0x0007B858 File Offset: 0x00079A58
		public virtual object GetObject(string name, bool ignoreCase)
		{
			object objectInternal = this.GetObjectInternal(name);
			if (objectInternal != null || !ignoreCase)
			{
				return objectInternal;
			}
			return this.GetCaseInsensitiveObjectInternal(name);
		}

		/// <summary>Reads all the resources and stores them in a <see cref="T:System.Collections.Hashtable" /> indicated in the <see cref="F:System.Resources.ResourceSet.Table" /> property.</summary>
		// Token: 0x06001F88 RID: 8072 RVA: 0x0007B87C File Offset: 0x00079A7C
		protected virtual void ReadResources()
		{
			IDictionaryEnumerator enumerator = this.Reader.GetEnumerator();
			while (enumerator.MoveNext())
			{
				object value = enumerator.Value;
				this.Table.Add(enumerator.Key, value);
			}
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x0007B8B8 File Offset: 0x00079AB8
		private object GetObjectInternal(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed resource set."));
			}
			return table[name];
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x0007B8E8 File Offset: 0x00079AE8
		private object GetCaseInsensitiveObjectInternal(string name)
		{
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed resource set."));
			}
			Hashtable hashtable = this._caseInsensitiveTable;
			if (hashtable == null)
			{
				hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
				IDictionaryEnumerator enumerator = table.GetEnumerator();
				while (enumerator.MoveNext())
				{
					hashtable.Add(enumerator.Key, enumerator.Value);
				}
				this._caseInsensitiveTable = hashtable;
			}
			return hashtable[name];
		}

		/// <summary>Indicates the <see cref="T:System.Resources.IResourceReader" /> used to read the resources.</summary>
		// Token: 0x040010FB RID: 4347
		[NonSerialized]
		protected IResourceReader Reader;

		/// <summary>The <see cref="T:System.Collections.Hashtable" /> in which the resources are stored.</summary>
		// Token: 0x040010FC RID: 4348
		protected Hashtable Table;

		// Token: 0x040010FD RID: 4349
		private Hashtable _caseInsensitiveTable;
	}
}
