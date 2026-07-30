using System;

namespace System.CodeDom
{
	/// <summary>Represents a statement that attaches an event-handler delegate to an event.</summary>
	// Token: 0x02000754 RID: 1876
	[Serializable]
	public class CodeAttachEventStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttachEventStatement" /> class.</summary>
		// Token: 0x06003B98 RID: 15256 RVA: 0x000D84F9 File Offset: 0x000D66F9
		public CodeAttachEventStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttachEventStatement" /> class using the specified event and delegate.</summary>
		/// <param name="eventRef">A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to attach an event handler to. </param>
		/// <param name="listener">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the new event handler. </param>
		// Token: 0x06003B99 RID: 15257 RVA: 0x000D8539 File Offset: 0x000D6739
		public CodeAttachEventStatement(CodeEventReferenceExpression eventRef, CodeExpression listener)
		{
			this._eventRef = eventRef;
			this.Listener = listener;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttachEventStatement" /> class using the specified object containing the event, event name, and event-handler delegate.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event. </param>
		/// <param name="eventName">The name of the event to attach an event handler to. </param>
		/// <param name="listener">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the new event handler. </param>
		// Token: 0x06003B9A RID: 15258 RVA: 0x000D854F File Offset: 0x000D674F
		public CodeAttachEventStatement(CodeExpression targetObject, string eventName, CodeExpression listener)
			: this(new CodeEventReferenceExpression(targetObject, eventName), listener)
		{
		}

		/// <summary>Gets or sets the event to attach an event-handler delegate to.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to attach an event handler to.</returns>
		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06003B9B RID: 15259 RVA: 0x000D8560 File Offset: 0x000D6760
		// (set) Token: 0x06003B9C RID: 15260 RVA: 0x000D8585 File Offset: 0x000D6785
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

		/// <summary>Gets or sets the new event-handler delegate to attach to the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the new event handler to attach.</returns>
		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06003B9D RID: 15261 RVA: 0x000D858E File Offset: 0x000D678E
		// (set) Token: 0x06003B9E RID: 15262 RVA: 0x000D8596 File Offset: 0x000D6796
		public CodeExpression Listener { get; set; }

		// Token: 0x04002D58 RID: 11608
		private CodeEventReferenceExpression _eventRef;
	}
}
