using System;
using System.Collections;
using System.Xml.Xsl.XsltOld.Debugger;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004FF RID: 1279
	internal class DbgCompiler : Compiler
	{
		// Token: 0x06003429 RID: 13353 RVA: 0x0012946C File Offset: 0x0012766C
		public DbgCompiler(IXsltDebugger debugger)
		{
			this.debugger = debugger;
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x0600342A RID: 13354 RVA: 0x00129491 File Offset: 0x00127691
		public override IXsltDebugger Debugger
		{
			get
			{
				return this.debugger;
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x0600342B RID: 13355 RVA: 0x00129499 File Offset: 0x00127699
		public virtual VariableAction[] GlobalVariables
		{
			get
			{
				if (this.globalVarsCache == null)
				{
					this.globalVarsCache = (VariableAction[])this.globalVars.ToArray(typeof(VariableAction));
				}
				return this.globalVarsCache;
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x0600342C RID: 13356 RVA: 0x001294C9 File Offset: 0x001276C9
		public virtual VariableAction[] LocalVariables
		{
			get
			{
				if (this.localVarsCache == null)
				{
					this.localVarsCache = (VariableAction[])this.localVars.ToArray(typeof(VariableAction));
				}
				return this.localVarsCache;
			}
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x001294FC File Offset: 0x001276FC
		private void DefineVariable(VariableAction variable)
		{
			if (variable.IsGlobal)
			{
				for (int i = 0; i < this.globalVars.Count; i++)
				{
					VariableAction variableAction = (VariableAction)this.globalVars[i];
					if (variableAction.Name == variable.Name)
					{
						if (variable.Stylesheetid < variableAction.Stylesheetid)
						{
							this.globalVars[i] = variable;
							this.globalVarsCache = null;
						}
						return;
					}
				}
				this.globalVars.Add(variable);
				this.globalVarsCache = null;
				return;
			}
			this.localVars.Add(variable);
			this.localVarsCache = null;
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x00129598 File Offset: 0x00127798
		private void UnDefineVariables(int count)
		{
			if (count != 0)
			{
				this.localVars.RemoveRange(this.localVars.Count - count, count);
				this.localVarsCache = null;
			}
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x001295BD File Offset: 0x001277BD
		internal override void PopScope()
		{
			this.UnDefineVariables(base.ScopeManager.CurrentScope.GetVeriablesCount());
			base.PopScope();
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x001295DB File Offset: 0x001277DB
		public override ApplyImportsAction CreateApplyImportsAction()
		{
			DbgCompiler.ApplyImportsActionDbg applyImportsActionDbg = new DbgCompiler.ApplyImportsActionDbg();
			applyImportsActionDbg.Compile(this);
			return applyImportsActionDbg;
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x001295E9 File Offset: 0x001277E9
		public override ApplyTemplatesAction CreateApplyTemplatesAction()
		{
			DbgCompiler.ApplyTemplatesActionDbg applyTemplatesActionDbg = new DbgCompiler.ApplyTemplatesActionDbg();
			applyTemplatesActionDbg.Compile(this);
			return applyTemplatesActionDbg;
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x001295F7 File Offset: 0x001277F7
		public override AttributeAction CreateAttributeAction()
		{
			DbgCompiler.AttributeActionDbg attributeActionDbg = new DbgCompiler.AttributeActionDbg();
			attributeActionDbg.Compile(this);
			return attributeActionDbg;
		}

		// Token: 0x06003433 RID: 13363 RVA: 0x00129605 File Offset: 0x00127805
		public override AttributeSetAction CreateAttributeSetAction()
		{
			DbgCompiler.AttributeSetActionDbg attributeSetActionDbg = new DbgCompiler.AttributeSetActionDbg();
			attributeSetActionDbg.Compile(this);
			return attributeSetActionDbg;
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x00129613 File Offset: 0x00127813
		public override CallTemplateAction CreateCallTemplateAction()
		{
			DbgCompiler.CallTemplateActionDbg callTemplateActionDbg = new DbgCompiler.CallTemplateActionDbg();
			callTemplateActionDbg.Compile(this);
			return callTemplateActionDbg;
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x001274C0 File Offset: 0x001256C0
		public override ChooseAction CreateChooseAction()
		{
			ChooseAction chooseAction = new ChooseAction();
			chooseAction.Compile(this);
			return chooseAction;
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x00129621 File Offset: 0x00127821
		public override CommentAction CreateCommentAction()
		{
			DbgCompiler.CommentActionDbg commentActionDbg = new DbgCompiler.CommentActionDbg();
			commentActionDbg.Compile(this);
			return commentActionDbg;
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x0012962F File Offset: 0x0012782F
		public override CopyAction CreateCopyAction()
		{
			DbgCompiler.CopyActionDbg copyActionDbg = new DbgCompiler.CopyActionDbg();
			copyActionDbg.Compile(this);
			return copyActionDbg;
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x0012963D File Offset: 0x0012783D
		public override CopyOfAction CreateCopyOfAction()
		{
			DbgCompiler.CopyOfActionDbg copyOfActionDbg = new DbgCompiler.CopyOfActionDbg();
			copyOfActionDbg.Compile(this);
			return copyOfActionDbg;
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x0012964B File Offset: 0x0012784B
		public override ElementAction CreateElementAction()
		{
			DbgCompiler.ElementActionDbg elementActionDbg = new DbgCompiler.ElementActionDbg();
			elementActionDbg.Compile(this);
			return elementActionDbg;
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x00129659 File Offset: 0x00127859
		public override ForEachAction CreateForEachAction()
		{
			DbgCompiler.ForEachActionDbg forEachActionDbg = new DbgCompiler.ForEachActionDbg();
			forEachActionDbg.Compile(this);
			return forEachActionDbg;
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x00129667 File Offset: 0x00127867
		public override IfAction CreateIfAction(IfAction.ConditionType type)
		{
			DbgCompiler.IfActionDbg ifActionDbg = new DbgCompiler.IfActionDbg(type);
			ifActionDbg.Compile(this);
			return ifActionDbg;
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x00129676 File Offset: 0x00127876
		public override MessageAction CreateMessageAction()
		{
			DbgCompiler.MessageActionDbg messageActionDbg = new DbgCompiler.MessageActionDbg();
			messageActionDbg.Compile(this);
			return messageActionDbg;
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x00129684 File Offset: 0x00127884
		public override NewInstructionAction CreateNewInstructionAction()
		{
			DbgCompiler.NewInstructionActionDbg newInstructionActionDbg = new DbgCompiler.NewInstructionActionDbg();
			newInstructionActionDbg.Compile(this);
			return newInstructionActionDbg;
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x00129692 File Offset: 0x00127892
		public override NumberAction CreateNumberAction()
		{
			DbgCompiler.NumberActionDbg numberActionDbg = new DbgCompiler.NumberActionDbg();
			numberActionDbg.Compile(this);
			return numberActionDbg;
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x001296A0 File Offset: 0x001278A0
		public override ProcessingInstructionAction CreateProcessingInstructionAction()
		{
			DbgCompiler.ProcessingInstructionActionDbg processingInstructionActionDbg = new DbgCompiler.ProcessingInstructionActionDbg();
			processingInstructionActionDbg.Compile(this);
			return processingInstructionActionDbg;
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x001296AE File Offset: 0x001278AE
		public override void CreateRootAction()
		{
			base.RootAction = new DbgCompiler.RootActionDbg();
			base.RootAction.Compile(this);
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x001296C7 File Offset: 0x001278C7
		public override SortAction CreateSortAction()
		{
			DbgCompiler.SortActionDbg sortActionDbg = new DbgCompiler.SortActionDbg();
			sortActionDbg.Compile(this);
			return sortActionDbg;
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x001296D5 File Offset: 0x001278D5
		public override TemplateAction CreateTemplateAction()
		{
			DbgCompiler.TemplateActionDbg templateActionDbg = new DbgCompiler.TemplateActionDbg();
			templateActionDbg.Compile(this);
			return templateActionDbg;
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x001296E3 File Offset: 0x001278E3
		public override TemplateAction CreateSingleTemplateAction()
		{
			DbgCompiler.TemplateActionDbg templateActionDbg = new DbgCompiler.TemplateActionDbg();
			templateActionDbg.CompileSingle(this);
			return templateActionDbg;
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x001296F1 File Offset: 0x001278F1
		public override TextAction CreateTextAction()
		{
			DbgCompiler.TextActionDbg textActionDbg = new DbgCompiler.TextActionDbg();
			textActionDbg.Compile(this);
			return textActionDbg;
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x001296FF File Offset: 0x001278FF
		public override UseAttributeSetsAction CreateUseAttributeSetsAction()
		{
			DbgCompiler.UseAttributeSetsActionDbg useAttributeSetsActionDbg = new DbgCompiler.UseAttributeSetsActionDbg();
			useAttributeSetsActionDbg.Compile(this);
			return useAttributeSetsActionDbg;
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x0012970D File Offset: 0x0012790D
		public override ValueOfAction CreateValueOfAction()
		{
			DbgCompiler.ValueOfActionDbg valueOfActionDbg = new DbgCompiler.ValueOfActionDbg();
			valueOfActionDbg.Compile(this);
			return valueOfActionDbg;
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x0012971B File Offset: 0x0012791B
		public override VariableAction CreateVariableAction(VariableType type)
		{
			DbgCompiler.VariableActionDbg variableActionDbg = new DbgCompiler.VariableActionDbg(type);
			variableActionDbg.Compile(this);
			return variableActionDbg;
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x0012972A File Offset: 0x0012792A
		public override WithParamAction CreateWithParamAction()
		{
			DbgCompiler.WithParamActionDbg withParamActionDbg = new DbgCompiler.WithParamActionDbg();
			withParamActionDbg.Compile(this);
			return withParamActionDbg;
		}

		// Token: 0x06003449 RID: 13385 RVA: 0x00129738 File Offset: 0x00127938
		public override BeginEvent CreateBeginEvent()
		{
			return new DbgCompiler.BeginEventDbg(this);
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x00129740 File Offset: 0x00127940
		public override TextEvent CreateTextEvent()
		{
			return new DbgCompiler.TextEventDbg(this);
		}

		// Token: 0x04002188 RID: 8584
		private IXsltDebugger debugger;

		// Token: 0x04002189 RID: 8585
		private ArrayList globalVars = new ArrayList();

		// Token: 0x0400218A RID: 8586
		private ArrayList localVars = new ArrayList();

		// Token: 0x0400218B RID: 8587
		private VariableAction[] globalVarsCache;

		// Token: 0x0400218C RID: 8588
		private VariableAction[] localVarsCache;

		// Token: 0x02000500 RID: 1280
		private class ApplyImportsActionDbg : ApplyImportsAction
		{
			// Token: 0x0600344B RID: 13387 RVA: 0x00129748 File Offset: 0x00127948
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x0600344C RID: 13388 RVA: 0x00129750 File Offset: 0x00127950
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x0600344D RID: 13389 RVA: 0x00129765 File Offset: 0x00127965
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x0400218D RID: 8589
			private DbgData dbgData;
		}

		// Token: 0x02000501 RID: 1281
		private class ApplyTemplatesActionDbg : ApplyTemplatesAction
		{
			// Token: 0x0600344F RID: 13391 RVA: 0x00129785 File Offset: 0x00127985
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06003450 RID: 13392 RVA: 0x0012978D File Offset: 0x0012798D
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06003451 RID: 13393 RVA: 0x001297A2 File Offset: 0x001279A2
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x0400218E RID: 8590
			private DbgData dbgData;
		}

		// Token: 0x02000502 RID: 1282
		private class AttributeActionDbg : AttributeAction
		{
			// Token: 0x06003453 RID: 13395 RVA: 0x001297C2 File Offset: 0x001279C2
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06003454 RID: 13396 RVA: 0x001297CA File Offset: 0x001279CA
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06003455 RID: 13397 RVA: 0x001297DF File Offset: 0x001279DF
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x0400218F RID: 8591
			private DbgData dbgData;
		}

		// Token: 0x02000503 RID: 1283
		private class AttributeSetActionDbg : AttributeSetAction
		{
			// Token: 0x06003457 RID: 13399 RVA: 0x001297FF File Offset: 0x001279FF
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06003458 RID: 13400 RVA: 0x00129807 File Offset: 0x00127A07
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06003459 RID: 13401 RVA: 0x0012981C File Offset: 0x00127A1C
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x04002190 RID: 8592
			private DbgData dbgData;
		}

		// Token: 0x02000504 RID: 1284
		private class CallTemplateActionDbg : CallTemplateAction
		{
			// Token: 0x0600345B RID: 13403 RVA: 0x0012983C File Offset: 0x00127A3C
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x0600345C RID: 13404 RVA: 0x00129844 File Offset: 0x00127A44
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x0600345D RID: 13405 RVA: 0x00129859 File Offset: 0x00127A59
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x04002191 RID: 8593
			private DbgData dbgData;
		}

		// Token: 0x02000505 RID: 1285
		private class CommentActionDbg : CommentAction
		{
			// Token: 0x0600345F RID: 13407 RVA: 0x00129879 File Offset: 0x00127A79
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06003460 RID: 13408 RVA: 0x00129881 File Offset: 0x00127A81
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06003461 RID: 13409 RVA: 0x00129896 File Offset: 0x00127A96
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x04002192 RID: 8594
			private DbgData dbgData;
		}

		// Token: 0x02000506 RID: 1286
		private class CopyActionDbg : CopyAction
		{
			// Token: 0x06003463 RID: 13411 RVA: 0x001298B6 File Offset: 0x00127AB6
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06003464 RID: 13412 RVA: 0x001298BE File Offset: 0x00127ABE
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06003465 RID: 13413 RVA: 0x001298D3 File Offset: 0x00127AD3
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x04002193 RID: 8595
			private DbgData dbgData;
		}

		// Token: 0x02000507 RID: 1287
		private class CopyOfActionDbg : CopyOfAction
		{
			// Token: 0x06003467 RID: 13415 RVA: 0x001298F3 File Offset: 0x00127AF3
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06003468 RID: 13416 RVA: 0x001298FB File Offset: 0x00127AFB
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06003469 RID: 13417 RVA: 0x00129910 File Offset: 0x00127B10
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x04002194 RID: 8596
			private DbgData dbgData;
		}

		// Token: 0x02000508 RID: 1288
		private class ElementActionDbg : ElementAction
		{
			// Token: 0x0600346B RID: 13419 RVA: 0x00129930 File Offset: 0x00127B30
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x0600346C RID: 13420 RVA: 0x00129938 File Offset: 0x00127B38
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x0600346D RID: 13421 RVA: 0x0012994D File Offset: 0x00127B4D
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x04002195 RID: 8597
			private DbgData dbgData;
		}

		// Token: 0x02000509 RID: 1289
		private class ForEachActionDbg : ForEachAction
		{
			// Token: 0x0600346F RID: 13423 RVA: 0x0012996D File Offset: 0x00127B6D
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06003470 RID: 13424 RVA: 0x00129975 File Offset: 0x00127B75
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06003471 RID: 13425 RVA: 0x0012998A File Offset: 0x00127B8A
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.PushDebuggerStack();
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
				if (frame.State == -1)
				{
					processor.PopDebuggerStack();
				}
			}

			// Token: 0x04002196 RID: 8598
			private DbgData dbgData;
		}

		// Token: 0x0200050A RID: 1290
		private class IfActionDbg : IfAction
		{
			// Token: 0x06003473 RID: 13427 RVA: 0x001299BF File Offset: 0x00127BBF
			internal IfActionDbg(IfAction.ConditionType type)
				: base(type)
			{
			}

			// Token: 0x06003474 RID: 13428 RVA: 0x001299C8 File Offset: 0x00127BC8
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06003475 RID: 13429 RVA: 0x001299D0 File Offset: 0x00127BD0
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06003476 RID: 13430 RVA: 0x001299E5 File Offset: 0x00127BE5
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x04002197 RID: 8599
			private DbgData dbgData;
		}

		// Token: 0x0200050B RID: 1291
		private class MessageActionDbg : MessageAction
		{
			// Token: 0x06003477 RID: 13431 RVA: 0x001299FD File Offset: 0x00127BFD
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06003478 RID: 13432 RVA: 0x00129A05 File Offset: 0x00127C05
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06003479 RID: 13433 RVA: 0x00129A1A File Offset: 0x00127C1A
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x04002198 RID: 8600
			private DbgData dbgData;
		}

		// Token: 0x0200050C RID: 1292
		private class NewInstructionActionDbg : NewInstructionAction
		{
			// Token: 0x0600347B RID: 13435 RVA: 0x00129A3A File Offset: 0x00127C3A
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x0600347C RID: 13436 RVA: 0x00129A42 File Offset: 0x00127C42
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x0600347D RID: 13437 RVA: 0x00129A57 File Offset: 0x00127C57
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x04002199 RID: 8601
			private DbgData dbgData;
		}

		// Token: 0x0200050D RID: 1293
		private class NumberActionDbg : NumberAction
		{
			// Token: 0x0600347F RID: 13439 RVA: 0x00129A77 File Offset: 0x00127C77
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06003480 RID: 13440 RVA: 0x00129A7F File Offset: 0x00127C7F
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06003481 RID: 13441 RVA: 0x00129A94 File Offset: 0x00127C94
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x0400219A RID: 8602
			private DbgData dbgData;
		}

		// Token: 0x0200050E RID: 1294
		private class ProcessingInstructionActionDbg : ProcessingInstructionAction
		{
			// Token: 0x06003483 RID: 13443 RVA: 0x00129AB4 File Offset: 0x00127CB4
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06003484 RID: 13444 RVA: 0x00129ABC File Offset: 0x00127CBC
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06003485 RID: 13445 RVA: 0x00129AD1 File Offset: 0x00127CD1
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x0400219B RID: 8603
			private DbgData dbgData;
		}

		// Token: 0x0200050F RID: 1295
		private class RootActionDbg : RootAction
		{
			// Token: 0x06003487 RID: 13447 RVA: 0x00129AF1 File Offset: 0x00127CF1
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06003488 RID: 13448 RVA: 0x00129AFC File Offset: 0x00127CFC
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
				string builtInTemplatesUri = compiler.Debugger.GetBuiltInTemplatesUri();
				if (builtInTemplatesUri != null && builtInTemplatesUri.Length != 0)
				{
					compiler.AllowBuiltInMode = true;
					this.builtInSheet = compiler.RootAction.CompileImport(compiler, compiler.ResolveUri(builtInTemplatesUri), int.MaxValue);
					compiler.AllowBuiltInMode = false;
				}
				this.dbgData.ReplaceVariables(((DbgCompiler)compiler).GlobalVariables);
			}

			// Token: 0x06003489 RID: 13449 RVA: 0x00129B75 File Offset: 0x00127D75
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.PushDebuggerStack();
					processor.OnInstructionExecute();
					processor.PushDebuggerStack();
				}
				base.Execute(processor, frame);
				if (frame.State == -1)
				{
					processor.PopDebuggerStack();
					processor.PopDebuggerStack();
				}
			}

			// Token: 0x0400219C RID: 8604
			private DbgData dbgData;
		}

		// Token: 0x02000510 RID: 1296
		private class SortActionDbg : SortAction
		{
			// Token: 0x0600348B RID: 13451 RVA: 0x00129BB6 File Offset: 0x00127DB6
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x0600348C RID: 13452 RVA: 0x00129BBE File Offset: 0x00127DBE
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x0600348D RID: 13453 RVA: 0x00129BD3 File Offset: 0x00127DD3
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x0400219D RID: 8605
			private DbgData dbgData;
		}

		// Token: 0x02000511 RID: 1297
		private class TemplateActionDbg : TemplateAction
		{
			// Token: 0x0600348F RID: 13455 RVA: 0x00129BF3 File Offset: 0x00127DF3
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06003490 RID: 13456 RVA: 0x00129BFB File Offset: 0x00127DFB
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06003491 RID: 13457 RVA: 0x00129C10 File Offset: 0x00127E10
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.PushDebuggerStack();
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
				if (frame.State == -1)
				{
					processor.PopDebuggerStack();
				}
			}

			// Token: 0x0400219E RID: 8606
			private DbgData dbgData;
		}

		// Token: 0x02000512 RID: 1298
		private class TextActionDbg : TextAction
		{
			// Token: 0x06003493 RID: 13459 RVA: 0x00129C45 File Offset: 0x00127E45
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06003494 RID: 13460 RVA: 0x00129C4D File Offset: 0x00127E4D
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06003495 RID: 13461 RVA: 0x00129C62 File Offset: 0x00127E62
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x0400219F RID: 8607
			private DbgData dbgData;
		}

		// Token: 0x02000513 RID: 1299
		private class UseAttributeSetsActionDbg : UseAttributeSetsAction
		{
			// Token: 0x06003497 RID: 13463 RVA: 0x00129C82 File Offset: 0x00127E82
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06003498 RID: 13464 RVA: 0x00129C8A File Offset: 0x00127E8A
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06003499 RID: 13465 RVA: 0x00129C9F File Offset: 0x00127E9F
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x040021A0 RID: 8608
			private DbgData dbgData;
		}

		// Token: 0x02000514 RID: 1300
		private class ValueOfActionDbg : ValueOfAction
		{
			// Token: 0x0600349B RID: 13467 RVA: 0x00129CBF File Offset: 0x00127EBF
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x0600349C RID: 13468 RVA: 0x00129CC7 File Offset: 0x00127EC7
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x0600349D RID: 13469 RVA: 0x00129CDC File Offset: 0x00127EDC
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x040021A1 RID: 8609
			private DbgData dbgData;
		}

		// Token: 0x02000515 RID: 1301
		private class VariableActionDbg : VariableAction
		{
			// Token: 0x0600349F RID: 13471 RVA: 0x00129CFC File Offset: 0x00127EFC
			internal VariableActionDbg(VariableType type)
				: base(type)
			{
			}

			// Token: 0x060034A0 RID: 13472 RVA: 0x00129D05 File Offset: 0x00127F05
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x060034A1 RID: 13473 RVA: 0x00129D0D File Offset: 0x00127F0D
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
				((DbgCompiler)compiler).DefineVariable(this);
			}

			// Token: 0x060034A2 RID: 13474 RVA: 0x00129D2E File Offset: 0x00127F2E
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x040021A2 RID: 8610
			private DbgData dbgData;
		}

		// Token: 0x02000516 RID: 1302
		private class WithParamActionDbg : WithParamAction
		{
			// Token: 0x060034A3 RID: 13475 RVA: 0x00129D46 File Offset: 0x00127F46
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x060034A4 RID: 13476 RVA: 0x00129D4E File Offset: 0x00127F4E
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x060034A5 RID: 13477 RVA: 0x00129D63 File Offset: 0x00127F63
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x040021A3 RID: 8611
			private DbgData dbgData;
		}

		// Token: 0x02000517 RID: 1303
		private class BeginEventDbg : BeginEvent
		{
			// Token: 0x17000B0C RID: 2828
			// (get) Token: 0x060034A7 RID: 13479 RVA: 0x00129D83 File Offset: 0x00127F83
			internal override DbgData DbgData
			{
				get
				{
					return this.dbgData;
				}
			}

			// Token: 0x060034A8 RID: 13480 RVA: 0x00129D8B File Offset: 0x00127F8B
			public BeginEventDbg(Compiler compiler)
				: base(compiler)
			{
				this.dbgData = new DbgData(compiler);
			}

			// Token: 0x060034A9 RID: 13481 RVA: 0x00129DA0 File Offset: 0x00127FA0
			public override bool Output(Processor processor, ActionFrame frame)
			{
				base.OnInstructionExecute(processor);
				return base.Output(processor, frame);
			}

			// Token: 0x040021A4 RID: 8612
			private DbgData dbgData;
		}

		// Token: 0x02000518 RID: 1304
		private class TextEventDbg : TextEvent
		{
			// Token: 0x17000B0D RID: 2829
			// (get) Token: 0x060034AA RID: 13482 RVA: 0x00129DB1 File Offset: 0x00127FB1
			internal override DbgData DbgData
			{
				get
				{
					return this.dbgData;
				}
			}

			// Token: 0x060034AB RID: 13483 RVA: 0x00129DB9 File Offset: 0x00127FB9
			public TextEventDbg(Compiler compiler)
				: base(compiler)
			{
				this.dbgData = new DbgData(compiler);
			}

			// Token: 0x060034AC RID: 13484 RVA: 0x00129DCE File Offset: 0x00127FCE
			public override bool Output(Processor processor, ActionFrame frame)
			{
				base.OnInstructionExecute(processor);
				return base.Output(processor, frame);
			}

			// Token: 0x040021A5 RID: 8613
			private DbgData dbgData;
		}
	}
}
