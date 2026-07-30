using System;
using System.CodeDom;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000155 RID: 341
	internal class EventCodeDomSerializer : MemberCodeDomSerializer
	{
		// Token: 0x06000A6E RID: 2670 RVA: 0x0001562A File Offset: 0x0001382A
		public EventCodeDomSerializer()
		{
			this._thisReference = new CodeThisReferenceExpression();
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00015640 File Offset: 0x00013840
		public override void Serialize(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor, CodeStatementCollection statements)
		{
			if (statements == null)
			{
				throw new ArgumentNullException("statements");
			}
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (descriptor == null)
			{
				throw new ArgumentNullException("descriptor");
			}
			IEventBindingService eventBindingService = manager.GetService(typeof(IEventBindingService)) as IEventBindingService;
			if (eventBindingService != null)
			{
				EventDescriptor eventDescriptor = (EventDescriptor)descriptor;
				string text = (string)eventBindingService.GetEventProperty(eventDescriptor).GetValue(value);
				if (text != null)
				{
					CodeDelegateCreateExpression codeDelegateCreateExpression = new CodeDelegateCreateExpression(new CodeTypeReference(eventDescriptor.EventType), this._thisReference, text);
					CodeEventReferenceExpression codeEventReferenceExpression = new CodeEventReferenceExpression(base.SerializeToExpression(manager, value), eventDescriptor.Name);
					statements.Add(new CodeAttachEventStatement(codeEventReferenceExpression, codeDelegateCreateExpression));
				}
			}
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x000156FC File Offset: 0x000138FC
		public override bool ShouldSerialize(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor)
		{
			IEventBindingService eventBindingService = manager.GetService(typeof(IEventBindingService)) as IEventBindingService;
			return eventBindingService != null && eventBindingService.GetEventProperty((EventDescriptor)descriptor).GetValue(value) != null;
		}

		// Token: 0x04000267 RID: 615
		private CodeThisReferenceExpression _thisReference;
	}
}
