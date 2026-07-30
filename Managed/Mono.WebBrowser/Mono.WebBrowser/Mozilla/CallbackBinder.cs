using System;

namespace Mono.Mozilla
{
	// Token: 0x0200005B RID: 91
	internal struct CallbackBinder
	{
		// Token: 0x0600026C RID: 620 RVA: 0x00003B20 File Offset: 0x00001D20
		internal CallbackBinder(Callback callback)
		{
			this.OnWidgetLoaded = new CallbackVoid(callback.OnWidgetLoaded);
			this.OnStateChange = new CallbackOnStateChange(callback.OnStateChange);
			this.OnProgress = new CallbackOnProgress(callback.OnProgress);
			this.OnLocationChanged = new CallbackOnLocationChanged(callback.OnLocationChanged);
			this.OnStatusChange = new CallbackOnStatusChange(callback.OnStatusChange);
			this.OnSecurityChange = new CallbackOnSecurityChange(callback.OnSecurityChange);
			this.OnKeyDown = new KeyCallback(callback.OnClientDomKeyDown);
			this.OnKeyUp = new KeyCallback(callback.OnClientDomKeyUp);
			this.OnKeyPress = new KeyCallback(callback.OnClientDomKeyPress);
			this.OnMouseDown = new MouseCallback(callback.OnClientMouseDown);
			this.OnMouseUp = new MouseCallback(callback.OnClientMouseUp);
			this.OnMouseClick = new MouseCallback(callback.OnClientMouseClick);
			this.OnMouseDoubleClick = new MouseCallback(callback.OnClientMouseDoubleClick);
			this.OnMouseOver = new MouseCallback(callback.OnClientMouseOver);
			this.OnMouseOut = new MouseCallback(callback.OnClientMouseOut);
			this.OnActivate = new Callback2(callback.OnClientActivate);
			this.OnFocus = new Callback2(callback.OnClientFocus);
			this.OnBlur = new Callback2(callback.OnClientBlur);
			this.OnAlert = new CallbackPtrPtr(callback.OnAlert);
			this.OnAlertCheck = new CallbackOnAlertCheck(callback.OnAlertCheck);
			this.OnConfirm = new CallbackOnConfirm(callback.OnConfirm);
			this.OnConfirmCheck = new CallbackOnConfirmCheck(callback.OnConfirmCheck);
			this.OnConfirmEx = new CallbackOnConfirmEx(callback.OnConfirmEx);
			this.OnPrompt = new CallbackOnPrompt(callback.OnPrompt);
			this.OnPromptUsernameAndPassword = new CallbackOnPromptUsernameAndPassword(callback.OnPromptUsernameAndPassword);
			this.OnPromptPassword = new CallbackOnPromptPassword(callback.OnPromptPassword);
			this.OnSelect = new CallbackOnSelect(callback.OnSelect);
			this.OnLoad = new CallbackVoid(callback.OnLoad);
			this.OnUnload = new CallbackVoid(callback.OnUnload);
			this.OnShowContextMenu = new CallbackOnShowContextMenu(callback.OnShowContextMenu);
			this.OnGeneric = new CallbackWString(callback.OnGeneric);
		}

		// Token: 0x04000094 RID: 148
		public CallbackVoid OnWidgetLoaded;

		// Token: 0x04000095 RID: 149
		public CallbackOnStateChange OnStateChange;

		// Token: 0x04000096 RID: 150
		public CallbackOnProgress OnProgress;

		// Token: 0x04000097 RID: 151
		public CallbackOnLocationChanged OnLocationChanged;

		// Token: 0x04000098 RID: 152
		public CallbackOnStatusChange OnStatusChange;

		// Token: 0x04000099 RID: 153
		public CallbackOnSecurityChange OnSecurityChange;

		// Token: 0x0400009A RID: 154
		public KeyCallback OnKeyDown;

		// Token: 0x0400009B RID: 155
		public KeyCallback OnKeyUp;

		// Token: 0x0400009C RID: 156
		public KeyCallback OnKeyPress;

		// Token: 0x0400009D RID: 157
		public MouseCallback OnMouseDown;

		// Token: 0x0400009E RID: 158
		public MouseCallback OnMouseUp;

		// Token: 0x0400009F RID: 159
		public MouseCallback OnMouseClick;

		// Token: 0x040000A0 RID: 160
		public MouseCallback OnMouseDoubleClick;

		// Token: 0x040000A1 RID: 161
		public MouseCallback OnMouseOver;

		// Token: 0x040000A2 RID: 162
		public MouseCallback OnMouseOut;

		// Token: 0x040000A3 RID: 163
		public Callback2 OnActivate;

		// Token: 0x040000A4 RID: 164
		public Callback2 OnFocus;

		// Token: 0x040000A5 RID: 165
		public Callback2 OnBlur;

		// Token: 0x040000A6 RID: 166
		public CallbackPtrPtr OnAlert;

		// Token: 0x040000A7 RID: 167
		public CallbackOnAlertCheck OnAlertCheck;

		// Token: 0x040000A8 RID: 168
		public CallbackOnConfirm OnConfirm;

		// Token: 0x040000A9 RID: 169
		public CallbackOnConfirmCheck OnConfirmCheck;

		// Token: 0x040000AA RID: 170
		public CallbackOnConfirmEx OnConfirmEx;

		// Token: 0x040000AB RID: 171
		public CallbackOnPrompt OnPrompt;

		// Token: 0x040000AC RID: 172
		public CallbackOnPromptUsernameAndPassword OnPromptUsernameAndPassword;

		// Token: 0x040000AD RID: 173
		public CallbackOnPromptPassword OnPromptPassword;

		// Token: 0x040000AE RID: 174
		public CallbackOnSelect OnSelect;

		// Token: 0x040000AF RID: 175
		public CallbackVoid OnLoad;

		// Token: 0x040000B0 RID: 176
		public CallbackVoid OnUnload;

		// Token: 0x040000B1 RID: 177
		public CallbackOnShowContextMenu OnShowContextMenu;

		// Token: 0x040000B2 RID: 178
		public CallbackWString OnGeneric;
	}
}
