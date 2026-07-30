using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	/// <summary>Represents an ordered collection of nodes.</summary>
	// Token: 0x02000235 RID: 565
	public abstract class XmlNodeList : IEnumerable, IDisposable
	{
		/// <summary>Retrieves a node at the given index.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> with the specified index in the collection. If <paramref name="index" /> is greater than or equal to the number of nodes in the list, this returns null.</returns>
		/// <param name="index">The zero-based index into the list of nodes.</param>
		// Token: 0x060015A7 RID: 5543
		public abstract XmlNode Item(int index);

		/// <summary>Gets the number of nodes in the XmlNodeList.</summary>
		/// <returns>The number of nodes in the XmlNodeList.</returns>
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060015A8 RID: 5544
		public abstract int Count { get; }

		/// <summary>Gets an enumerator that iterates through the collection of nodes.</summary>
		/// <returns>An enumerator used to iterate through the collection of nodes.</returns>
		// Token: 0x060015A9 RID: 5545
		public abstract IEnumerator GetEnumerator();

		/// <summary>Gets a node at the given index.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> with the specified index in the collection. If index is greater than or equal to the number of nodes in the list, this returns null.</returns>
		/// <param name="i">The zero-based index into the list of nodes.</param>
		// Token: 0x1700042B RID: 1067
		[IndexerName("ItemOf")]
		public virtual XmlNode this[int i]
		{
			get
			{
				return this.Item(i);
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Xml.XmlNodeList" /> class.</summary>
		// Token: 0x060015AB RID: 5547 RVA: 0x00079A78 File Offset: 0x00077C78
		void IDisposable.Dispose()
		{
			this.PrivateDisposeNodeList();
		}

		/// <summary>Disposes resources in the node list privately.</summary>
		// Token: 0x060015AC RID: 5548 RVA: 0x00002F50 File Offset: 0x00001150
		protected virtual void PrivateDisposeNodeList()
		{
		}
	}
}
