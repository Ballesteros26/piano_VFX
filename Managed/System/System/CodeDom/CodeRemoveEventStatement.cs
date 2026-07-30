using System;

namespace System.CodeDom
{
	/// <summary>Represents a statement that removes an event handler.</summary>
	// Token: 0x02000789 RID: 1929
	[Serializable]
	public class CodeRemoveEventStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRemoveEventStatement" /> class.</summary>
		// Token: 0x06003D25 RID: 15653 RVA: 0x000D84F9 File Offset: 0x000D66F9
		public CodeRemoveEventStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRemoveEventStatement" /> class with the specified event and event handler.</summary>
		/// <param name="eventRef">A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to detach the event handler from. </param>
		/// <param name="listener">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the event handler to remove. </param>
		// Token: 0x06003D26 RID: 15654 RVA: 0x000DA05E File Offset: 0x000D825E
		public CodeRemoveEventStatement(CodeEventReferenceExpression eventRef, CodeExpression listener)
		{
			this._eventRef = eventRef;
			this.Listener = listener;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRemoveEventStatement" /> class using the specified target object, event name, and event handler.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event. </param>
		/// <param name="eventName">The name of the event. </param>
		/// <param name="listener">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the event handler to remove. </param>
		// Token: 0x06003D27 RID: 15655 RVA: 0x000DA074 File Offset: 0x000D8274
		public CodeRemoveEventStatement(CodeExpression targetObject, string eventName, CodeExpression listener)
		{
			this._eventRef = new CodeEventReferenceExpression(targetObject, eventName);
			this.Listener = listener;
		}

		/// <summary>Gets or sets the event to remove a listener from.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to remove a listener from.</returns>
		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06003D28 RID: 15656 RVA: 0x000DA090 File Offset: 0x000D8290
		// (set) Token: 0x06003D29 RID: 15657 RVA: 0x000DA0B5 File Offset: 0x000D82B5
		public CodeEventReferenceExpression Event
		{
			get
			{
				CodeEventReferenceExpression codeEventReferenceExpression;
				if ((codeEventReferenceExpression = this._eventRef) == null)
				{
					codeEventReferenceExpression = (this._eventRef = new CodeEventReferenceExpression());
				}
				return codeEventReferenceExpression;
			}
			set
			{
				this._eventRef = value;
			}
		}

		/// <summary>Gets or sets the event handler to remove.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the event handler to remove.</returns>
		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06003D2A RID: 15658 RVA: 0x000DA0BE File Offset: 0x000D82BE
		// (set) Token: 0x06003D2B RID: 15659 RVA: 0x000DA0C6 File Offset: 0x000D82C6
		public CodeExpression Listener { get; set; }

		// Token: 0x04002DDD RID: 11741
		private CodeEventReferenceExpression _eventRef;
	}
}
