using System;

namespace System.Web.Util
{
	// Token: 0x0200010B RID: 267
	internal enum AppVerifierErrorCode
	{
		// Token: 0x04001174 RID: 4468
		Ok,
		// Token: 0x04001175 RID: 4469
		HttpApplicationInstanceWasNull,
		// Token: 0x04001176 RID: 4470
		BeginHandlerDelegateWasNull,
		// Token: 0x04001177 RID: 4471
		AsyncCallbackInvokedMultipleTimes,
		// Token: 0x04001178 RID: 4472
		AsyncCallbackInvokedWithNullParameter,
		// Token: 0x04001179 RID: 4473
		AsyncCallbackGivenAsyncResultWhichWasNotCompleted,
		// Token: 0x0400117A RID: 4474
		AsyncCallbackInvokedSynchronouslyButAsyncResultWasNotMarkedCompletedSynchronously,
		// Token: 0x0400117B RID: 4475
		AsyncCallbackInvokedAsynchronouslyButAsyncResultWasMarkedCompletedSynchronously,
		// Token: 0x0400117C RID: 4476
		AsyncCallbackInvokedWithUnexpectedAsyncResultInstance,
		// Token: 0x0400117D RID: 4477
		AsyncCallbackInvokedAsynchronouslyThenBeginHandlerThrew,
		// Token: 0x0400117E RID: 4478
		BeginHandlerThrewThenAsyncCallbackInvokedAsynchronously,
		// Token: 0x0400117F RID: 4479
		AsyncCallbackInvokedSynchronouslyThenBeginHandlerThrew,
		// Token: 0x04001180 RID: 4480
		AsyncCallbackInvokedWithUnexpectedAsyncResultAsyncState,
		// Token: 0x04001181 RID: 4481
		AsyncCallbackCalledAfterHttpApplicationReassigned,
		// Token: 0x04001182 RID: 4482
		BeginHandlerReturnedNull,
		// Token: 0x04001183 RID: 4483
		BeginHandlerReturnedAsyncResultMarkedCompletedSynchronouslyButWhichWasNotCompleted,
		// Token: 0x04001184 RID: 4484
		BeginHandlerReturnedAsyncResultMarkedCompletedSynchronouslyButAsyncCallbackNeverCalled,
		// Token: 0x04001185 RID: 4485
		BeginHandlerReturnedUnexpectedAsyncResultInstance,
		// Token: 0x04001186 RID: 4486
		BeginHandlerReturnedUnexpectedAsyncResultAsyncState,
		// Token: 0x04001187 RID: 4487
		SyncContextSendOrPostCalledAfterRequestCompleted,
		// Token: 0x04001188 RID: 4488
		SyncContextSendOrPostCalledBetweenNotifications,
		// Token: 0x04001189 RID: 4489
		SyncContextPostCalledInNestedNotification,
		// Token: 0x0400118A RID: 4490
		RequestNotificationCompletedSynchronouslyWithNotificationContextPending,
		// Token: 0x0400118B RID: 4491
		NotificationContextHasChangedAfterSynchronouslyProcessingNotification,
		// Token: 0x0400118C RID: 4492
		PendingProcessRequestNotificationStatusAfterCompletingNestedNotification
	}
}
