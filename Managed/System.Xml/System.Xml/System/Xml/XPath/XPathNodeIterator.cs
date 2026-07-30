using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

namespace System.Xml.XPath
{
	/// <summary>Provides an iterator over a selected set of nodes.</summary>
	// Token: 0x020002C1 RID: 705
	[DebuggerDisplay("Position={CurrentPosition}, Current={debuggerDisplayProxy}")]
	public abstract class XPathNodeIterator : ICloneable, IEnumerable
	{
		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		// Token: 0x06001A56 RID: 6742 RVA: 0x00093B2F File Offset: 0x00091D2F
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>When overridden in a derived class, returns a clone of this <see cref="T:System.Xml.XPath.XPathNodeIterator" /> object.</summary>
		/// <returns>A new <see cref="T:System.Xml.XPath.XPathNodeIterator" /> object clone of this <see cref="T:System.Xml.XPath.XPathNodeIterator" /> object.</returns>
		// Token: 0x06001A57 RID: 6743
		public abstract XPathNodeIterator Clone();

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> object returned by the <see cref="P:System.Xml.XPath.XPathNodeIterator.Current" /> property to the next node in the selected node set.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XPath.XPathNavigator" /> object moved to the next node; false if there are no more selected nodes.</returns>
		// Token: 0x06001A58 RID: 6744
		public abstract bool MoveNext();

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Xml.XPath.XPathNavigator" /> object for this <see cref="T:System.Xml.XPath.XPathNodeIterator" />, positioned on the current context node.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the context node from which the node set was selected. The <see cref="M:System.Xml.XPath.XPathNodeIterator.MoveNext" /> method must be called to move the <see cref="T:System.Xml.XPath.XPathNodeIterator" /> to the first node in the selected set.</returns>
		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001A59 RID: 6745
		public abstract XPathNavigator Current { get; }

		/// <summary>When overridden in a derived class, gets the index of the current position in the selected set of nodes.</summary>
		/// <returns>The index of the current position.</returns>
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001A5A RID: 6746
		public abstract int CurrentPosition { get; }

		/// <summary>Gets the index of the last node in the selected set of nodes.</summary>
		/// <returns>The index of the last node in the selected set of nodes, or 0 if there are no selected nodes.</returns>
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001A5B RID: 6747 RVA: 0x00093B38 File Offset: 0x00091D38
		public virtual int Count
		{
			get
			{
				if (this.count == -1)
				{
					XPathNodeIterator xpathNodeIterator = this.Clone();
					while (xpathNodeIterator.MoveNext())
					{
					}
					this.count = xpathNodeIterator.CurrentPosition;
				}
				return this.count;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> object to iterate through the selected node set.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object to iterate through the selected node set.</returns>
		// Token: 0x06001A5C RID: 6748 RVA: 0x00093B6F File Offset: 0x00091D6F
		public virtual IEnumerator GetEnumerator()
		{
			return new XPathNodeIterator.Enumerator(this);
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x00093B77 File Offset: 0x00091D77
		private object debuggerDisplayProxy
		{
			get
			{
				if (this.Current != null)
				{
					return new XPathNavigator.DebuggerDisplayProxy(this.Current);
				}
				return null;
			}
		}

		// Token: 0x0400156C RID: 5484
		internal int count = -1;

		// Token: 0x020002C2 RID: 706
		private class Enumerator : IEnumerator
		{
			// Token: 0x06001A5F RID: 6751 RVA: 0x00093BA2 File Offset: 0x00091DA2
			public Enumerator(XPathNodeIterator original)
			{
				this.original = original.Clone();
			}

			// Token: 0x17000512 RID: 1298
			// (get) Token: 0x06001A60 RID: 6752 RVA: 0x00093BB8 File Offset: 0x00091DB8
			public virtual object Current
			{
				get
				{
					if (!this.iterationStarted)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has not started. Call MoveNext.", new object[] { string.Empty }));
					}
					if (this.current == null)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has already finished.", new object[] { string.Empty }));
					}
					return this.current.Current.Clone();
				}
			}

			// Token: 0x06001A61 RID: 6753 RVA: 0x00093C24 File Offset: 0x00091E24
			public virtual bool MoveNext()
			{
				if (!this.iterationStarted)
				{
					this.current = this.original.Clone();
					this.iterationStarted = true;
				}
				if (this.current == null || !this.current.MoveNext())
				{
					this.current = null;
					return false;
				}
				return true;
			}

			// Token: 0x06001A62 RID: 6754 RVA: 0x00093C70 File Offset: 0x00091E70
			public virtual void Reset()
			{
				this.iterationStarted = false;
			}

			// Token: 0x0400156D RID: 5485
			private XPathNodeIterator original;

			// Token: 0x0400156E RID: 5486
			private XPathNodeIterator current;

			// Token: 0x0400156F RID: 5487
			private bool iterationStarted;
		}

		// Token: 0x020002C3 RID: 707
		private struct DebuggerDisplayProxy
		{
			// Token: 0x06001A63 RID: 6755 RVA: 0x00093C79 File Offset: 0x00091E79
			public DebuggerDisplayProxy(XPathNodeIterator nodeIterator)
			{
				this.nodeIterator = nodeIterator;
			}

			// Token: 0x06001A64 RID: 6756 RVA: 0x00093C84 File Offset: 0x00091E84
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Position=");
				stringBuilder.Append(this.nodeIterator.CurrentPosition);
				stringBuilder.Append(", Current=");
				if (this.nodeIterator.Current == null)
				{
					stringBuilder.Append("null");
				}
				else
				{
					stringBuilder.Append('{');
					stringBuilder.Append(new XPathNavigator.DebuggerDisplayProxy(this.nodeIterator.Current).ToString());
					stringBuilder.Append('}');
				}
				return stringBuilder.ToString();
			}

			// Token: 0x04001570 RID: 5488
			private XPathNodeIterator nodeIterator;
		}
	}
}
