using Magikarp.Platform.Behavior.Flow;
using Magikarp.Platform.Behavior.Progress;
using Magikarp.Platform.Definition;
using Magikarp.Platform.Definition.Flow;
using Magikarp.Platform.Definition.MVP;
using System;

namespace Magikarp.Platform.Behavior
{
    /// <summary>
    /// 提供流程控制功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public class FlowController : IController
    {
        #region -- 變數宣告 ( Declarations ) --   

        private BackgroundWorker l_objBackgroundWorker = null;
        private CommandOperatorFactory l_objCommandOperatorFactory = null;

        #endregion

        #region -- 建構/解構 ( Constructors/Destructor ) --

        /// <summary>
        /// 建構元。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public FlowController()
        {
            this.l_objBackgroundWorker = BackgroundWorker.GetInstance();
            this.l_objCommandOperatorFactory = new CommandOperatorFactory();
        }

        #endregion

        #region -- 介面實做 ( Implements ) - [IController] --

        /// <summary>
        /// 執行命令。
        /// </summary>
        /// <param name="pi_sCommand">待執行命令。</param>
        /// <returns>命令執行結果。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        string IController.DispatchCommand(string pi_sCommand)
        {
            return this.DispatchCommand(pi_sCommand, string.Empty, ProgressLevelEnum.None);
        }

        /// <summary>
        /// 執行命令。
        /// </summary>
        /// <param name="pi_sCommand">待執行命令。</param>
        /// <param name="pi_sParameter">命令參數。</param>
        /// <returns>命令執行結果。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        string IController.DispatchCommand(string pi_sCommand, string pi_sParameter)
        {
            return this.DispatchCommand(pi_sCommand, pi_sParameter, ProgressLevelEnum.None);
        }

        #endregion

        #region -- 私有函式 ( Private Method) --

        /// <summary>
        /// 執行命令。
        /// </summary>
        /// <param name="pi_sCommand">待執行命令。</param>
        /// <param name="pi_sParameter">命令參數。</param>
        /// <param name="pi_nProgressLevel">進度複雜度列舉項。</param>
        /// <returns>命令執行結果。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        [STAThread]
        private string DispatchCommand(string pi_sCommand, string pi_sParameter, ProgressLevelEnum pi_nProgressLevel)
        {
            string sReturn = string.Empty;
            ICommandOperator objCommandOperator = this.l_objCommandOperatorFactory.Product(pi_sCommand);

            if (pi_nProgressLevel == ProgressLevelEnum.None)
            {
                sReturn = objCommandOperator.Execute(pi_sParameter);
            }
            else
            {
                sReturn = this.l_objBackgroundWorker.RunJob(objCommandOperator.Execute, pi_sParameter, pi_nProgressLevel);
            }

            return sReturn;
        }

        #endregion

    }
}
