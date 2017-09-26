using Magikarp.Platform.Definition;
using Magikarp.Platform.Definition.Flow;
using Magikarp.Utility.TransitData;
using System;

namespace Magikarp.Platform.Behavior.Flow
{
    /// <summary>
    /// 提供命令執行功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public class CommandOperator : ICommandOperator
    {

        #region -- 變數宣告 ( Declarations ) --   

        private string l_sCommand = string.Empty;

        #endregion

        #region -- 建構/解構 ( Constructors/Destructor ) --

        /// <summary>
        /// 建構元。
        /// </summary>
        /// <param name="pi_sCommand">命令字串。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public CommandOperator(string pi_sCommand)
        {
            this.l_sCommand = pi_sCommand;
        }

        #endregion

        #region -- 介面實做 ( Implements ) - [ICommandOperator] --

        /// <summary>
        /// 執行命令。
        /// </summary>
        /// <param name="pi_sParameter">執行參數。</param>
        /// <returns>執行結果。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        string ICommandOperator.Execute(string pi_sParameter)
        {
            string sReturn = string.Empty;
            CommandParser objCommandParser = new CommandParser(this.l_sCommand);
            IRequestHandler objHandler = null;

            int nStep = 0;//程序進度指標。
            Boolean bRun = true;//程序中斷旗標。

            while (bRun)
            {
                nStep += 1;
                switch (nStep)
                {
                    case 1:// 判斷是否具備對應的控制器。
                        bRun = (string.IsNullOrEmpty(objCommandParser.HandlerType) == false);
                        break;

                    case 2:// 取得對應控制器。
                        objHandler = this.GetHandler(objCommandParser.HandlerType);
                        break;

                    case 3:// 判斷是否具備對應的流程處理執行個體。
                        bRun = (objHandler != null);
                        break;

                    case 4:// 要求控制器處理對應功能。
                        string sParameter = string.Empty;
                        TransitDataAdapter objAdapter = new TransitDataAdapter();
                        HandlerModel objHandlerModel = new HandlerModel()
                        {
                            FunctionName = objCommandParser.FunctionName,
                            HandlerParameter = pi_sParameter
                        };
                                               
                        sParameter = objAdapter.Export<HandlerModel>(objHandlerModel);
                        sReturn = objHandler.GetResponse(sParameter);
                        break;

                    default://結束。
                        bRun = false;
                        break;
                }
            }           

            return sReturn;
        }

        #endregion

        #region -- 私有函式 ( Private Method) --

        /// <summary>
        /// 取得對應的程序控制器。
        /// </summary>
        /// <param name="pi_sHandlerType">程序型別。</param>
        /// <returns>對應的程序控制器。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        protected virtual IRequestHandler GetHandler(string pi_sHandlerType)
        {
            return HandlerManager.GetInstance().GetHandler(pi_sHandlerType);
        }

        #endregion     
    }
}
