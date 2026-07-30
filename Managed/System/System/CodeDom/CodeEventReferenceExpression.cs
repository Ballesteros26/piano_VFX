using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to an event.</summary>
	// Token: 0x0200076D RID: 1901
	[Serializable]
	public class CodeEventReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> class.</summary>
		// Token: 0x06003C49 RID: 15433 RVA: 0x000D82AC File Offset: 0x000D64AC
		public CodeEventReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> class using the specified target object and event name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event. </param>
		/// <param name="eventName">The name of the event to reference. </param>
		// Token: 0x06003C4A RID: 15434 RVA: 0x000D8F98 File Offset: 0x000D7198
		public CodeEventReferenceExpression(CodeExpression targetObject, string eventName)
		{
			this.TargetObject = targetObject;
			this._eventName = eventName;
		}

		/// <summary>Gets or sets the object that contains the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event.</returns>
		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06003C4B RID: 15435 RVA: 0x000D8FAE File Offset: 0x000D71AE
		// (set) Token: 0x06003C4C RID: 15436 RVA: 0x000D8FB6 File Offset: 0x000D71B6
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the name of the event.</summary>
		/// <returns>The name of the event.</returns>
		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06003C4D RID: 15437 RVA: 0x000D8FBF File Offset: 0x000D71BF
		// (set) Token: 0x06003C4E RID: 15438 RVA: 0x000D8FD0 File Offset: 0x000D71D0
		public string EventName
		{
			get
			{
				return this._eventName ?? string.Empty;
			}
			set
			{
				this._eventName = value;
			}
		}

		// Token: 0x04002D91 RID: 11665
		private string _eventName;
	}
}
