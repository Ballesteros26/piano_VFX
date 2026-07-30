using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides a stack object that can be used by a serializer to make information available to nested serializers.</summary>
	// Token: 0x02000349 RID: 841
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class ContextStack
	{
		/// <summary>Gets the current object on the stack.</summary>
		/// <returns>The current object on the stack, or null if no objects were pushed.</returns>
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x0006AE3D File Offset: 0x0006903D
		public object Current
		{
			get
			{
				if (this.contextStack != null && this.contextStack.Count > 0)
				{
					return this.contextStack[this.contextStack.Count - 1];
				}
				return null;
			}
		}

		/// <summary>Gets the object on the stack at the specified level.</summary>
		/// <returns>The object on the stack at the specified level, or null if no object exists at that level.</returns>
		/// <param name="level">The level of the object to retrieve on the stack. Level 0 is the top of the stack, level 1 is the next down, and so on. This level must be 0 or greater. If level is greater than the number of levels on the stack, it returns null. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="level" /> is less than 0.</exception>
		// Token: 0x1700054E RID: 1358
		public object this[int level]
		{
			get
			{
				if (level < 0)
				{
					throw new ArgumentOutOfRangeException("level");
				}
				if (this.contextStack != null && level < this.contextStack.Count)
				{
					return this.contextStack[this.contextStack.Count - 1 - level];
				}
				return null;
			}
		}

		/// <summary>Gets the first object on the stack that inherits from or implements the specified type.</summary>
		/// <returns>The first object on the stack that inherits from or implements the specified type, or null if no object on the stack implements the type.</returns>
		/// <param name="type">A type to retrieve from the context stack. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x1700054F RID: 1359
		public object this[Type type]
		{
			get
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				if (this.contextStack != null)
				{
					int i = this.contextStack.Count;
					while (i > 0)
					{
						object obj = this.contextStack[--i];
						if (type.IsInstanceOfType(obj))
						{
							return obj;
						}
					}
				}
				return null;
			}
		}

		/// <summary>Appends an object to the end of the stack, rather than pushing it onto the top of the stack.</summary>
		/// <param name="context">A context object to append to the stack.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> is null.</exception>
		// Token: 0x06001A4B RID: 6731 RVA: 0x0006AF18 File Offset: 0x00069118
		public void Append(object context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (this.contextStack == null)
			{
				this.contextStack = new ArrayList();
			}
			this.contextStack.Insert(0, context);
		}

		/// <summary>Removes the current object off of the stack, returning its value.</summary>
		/// <returns>The object removed from the stack; null if no objects are on the stack.</returns>
		// Token: 0x06001A4C RID: 6732 RVA: 0x0006AF48 File Offset: 0x00069148
		public object Pop()
		{
			object obj = null;
			if (this.contextStack != null && this.contextStack.Count > 0)
			{
				int num = this.contextStack.Count - 1;
				obj = this.contextStack[num];
				this.contextStack.RemoveAt(num);
			}
			return obj;
		}

		/// <summary>Pushes, or places, the specified object onto the stack.</summary>
		/// <param name="context">The context object to push onto the stack. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> is null.</exception>
		// Token: 0x06001A4D RID: 6733 RVA: 0x0006AF95 File Offset: 0x00069195
		public void Push(object context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (this.contextStack == null)
			{
				this.contextStack = new ArrayList();
			}
			this.contextStack.Add(context);
		}

		// Token: 0x04001834 RID: 6196
		private ArrayList contextStack;
	}
}
