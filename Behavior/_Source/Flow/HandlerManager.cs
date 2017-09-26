using Magikarp.Platform.Definition.Flow;
using System;
using System.Collections.Generic;

namespace Magikarp.Platform.Behavior.Flow
{
    /// <summary>
    /// 提供程序處理器管理功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public class HandlerManager
    {

        #region -- 變數宣告 ( Declarations ) --   

        private static HandlerManager l_objHandlerManager = null;
        private static object l_objSynclock = new object();

        private Dictionary<string, IRequestHandler> l_objHandlerCollection = new Dictionary<string, IRequestHandler>();

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
        public HandlerManager() { }

        #endregion

        #region -- 靜態方法 (Shared Method ) --

        /// <summary>
        /// 取得 HandlerManager 實體。
        /// </summary>
        /// <returns>HandlerManager 實體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public static HandlerManager GetInstance()
        {
            if (HandlerManager.l_objHandlerManager == null)
            {
                lock (HandlerManager.l_objSynclock)
                {
                    if (HandlerManager.l_objHandlerManager == null)
                    {
                        HandlerManager.l_objHandlerManager = new HandlerManager();
                    }
                }
            }
            return HandlerManager.l_objHandlerManager;
        }

        #endregion

        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 取得流程控制執行個體。
        /// </summary>
        /// <param name="pi_sHandlerType">流程控制類別。</param>
        /// <returns>流程控制執行個體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public IRequestHandler GetHandler(string pi_sHandlerType)
        {
            IRequestHandler objReturn = null;

            // 先取用外部指定執行個體。
            if (this.l_objHandlerCollection.ContainsKey(pi_sHandlerType))
            {
                objReturn = this.l_objHandlerCollection[pi_sHandlerType];
            }

            if (objReturn == null) // 若尚未取得才繼續。
            {
                string sFullname = String.Format("{0}.{1}Handler", this.GetType().Namespace, pi_sHandlerType);

                // 取得對應控制器。
                objReturn = (IRequestHandler)(this.GetType().Assembly.CreateInstance(sFullname));
                // 回存控管，藉以減少動態生成次數。
                this.l_objHandlerCollection.Add(pi_sHandlerType, objReturn);
            }

            return objReturn;
        }

        /// <summary>
        /// 提供客戶端掛載自訂的流程控制執行個體。
        /// </summary>
        /// <param name="pi_objHandler">自訂的流程控制執行個體。</param>
        /// <param name="pi_sHandlerKey">自訂的流程控制執行個體識別值。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void MountHandler(IRequestHandler pi_objHandler, string pi_sHandlerKey)
        {
            if (this.l_objHandlerCollection.ContainsKey(pi_sHandlerKey))
            {
                this.l_objHandlerCollection.Add(pi_sHandlerKey, pi_objHandler);
            }
        }

        /// <summary>
        /// 提供客戶端重載自訂的流程控制執行個體。
        /// </summary>
        /// <param name="pi_objHandler">自訂的流程控制執行個體。</param>
        /// <param name="pi_sHandlerKey">自訂的流程控制執行個體識別值。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void ResetHandler(IRequestHandler pi_objHandler, string pi_sHandlerKey)
        {
            if (this.l_objHandlerCollection.ContainsKey(pi_sHandlerKey) == false)
            {
                this.l_objHandlerCollection.Add(pi_sHandlerKey, pi_objHandler);
            }
            else
            {
                this.l_objHandlerCollection[pi_sHandlerKey] = pi_objHandler;
            }
        }

        #endregion

    }
}
