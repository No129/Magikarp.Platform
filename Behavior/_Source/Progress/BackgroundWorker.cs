using Magikarp.Platform.Definition;
using Magikarp.Platform.Definition.MVP;
using System;

namespace Magikarp.Platform.Behavior.Progress
{
    /// <summary>
    /// 提供開啟進度畫面以控制的背景執行功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public class BackgroundWorker
    {
        #region -- 變數宣告 ( Declarations ) --   

        public delegate string JobWithReturnDelegate(string pi_sParameter);
        public delegate void JobWithoutReturnDelegate(string pi_sParameter);

        protected static BackgroundWorker l_objBackgroundWorker = null;
        protected static object l_objSyncObject = new object();

        private IViewProvider l_objViewProvider = null;
        private IView l_objProgressView = null;

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
        protected BackgroundWorker()
        {
            JobInfoCenter.GetInstance().AddObserver(this.OnNotify);
        }

        #endregion

        #region -- 事件處理 ( Event Handlers ) --

        /// <summary>
        /// 處理進度更新通知程序。
        /// </summary>
        /// <param name="pi_nNotifyObject">通知種類列舉。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private void OnNotify(JobNotifyObjectEnum pi_nNotifyObject)
        {
            int nStep = 0;      //程序進度指標。
            Boolean bRun = true;//程序中斷旗標。

            while (bRun)
            {
                nStep += 1;
                switch (nStep)
                {
                    case 1:// 是否具備進度視窗。
                        bRun = (this.l_objProgressView != null);
                        break;

                    case 2:// 進度視窗是否為可更新。                        
                        bRun = this.l_objProgressView is IAbilityOfRefresh;
                        break;

                    case 3:// 是否具備進度物件。
                        bRun = (JobInfoCenter.GetInstance().Progress != null);
                        break;

                    case 4:// 更新進度視窗。
                        string sParameter = JobInfoCenter.GetInstance().Progress.ExportToProgressEntry().ExportToString();
                        IAbilityOfRefresh objRefresh = (IAbilityOfRefresh)this.l_objProgressView;

                        objRefresh.RefreshView(sParameter);
                        break;

                    default://結束。
                        bRun = false;
                        break;
                }
            }           
        }

        /// <summary>
        /// 處理異步程序執行完畢。
        /// </summary>
        /// <param name="pi_objAsyncResult">異步程序索引值。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private void OnAsyncCompleted(IAsyncResult pi_objAsyncResult)
        {
            this.l_objProgressView.ExitView(); // 關閉進度視窗。
        }

        #endregion

        #region -- 靜態方法 (Shared Method ) --

        /// <summary>
        /// 取得類別 BackgroundWorker 物件實體。
        /// </summary>
        /// <returns>類別 BackgroundWorker 物件實體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public static BackgroundWorker GetInstance()
        {
            if (BackgroundWorker.l_objBackgroundWorker == null)
            {
                lock (BackgroundWorker.l_objSyncObject)
                {
                    if (BackgroundWorker.l_objBackgroundWorker == null)
                    {
                        BackgroundWorker.l_objBackgroundWorker = new BackgroundWorker();
                    }
                }
            }
            return BackgroundWorker.l_objBackgroundWorker;
        }

        #endregion

        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 設定進度視窗提供者。
        /// </summary>
        /// <param name="pi_objProvider">進度視窗提供者。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public virtual void SetProgressProvider(IViewProvider pi_objProvider)
        {
            this.l_objViewProvider = pi_objProvider;
        }

        /// <summary>
        /// 執行具備回傳值(字串)工作。
        /// </summary>
        /// <param name="pi_objJob">待執行的函式。</param>
        /// <param name="pi_sParameter">執行所需的參數。</param>
        /// <param name="pi_nJobComlexity">等待視窗種類。</param>
        /// <returns>執行後的結果。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string RunJob(JobWithReturnDelegate pi_objJob, string pi_sParameter, ProgressLevelEnum pi_nJobComlexity)
        {
            string sReturn = string.Empty;
            IAsyncResult objAsyncResult = null;

            int nStep = 0;      //程序進度指標。
            Boolean bRun = true;//程序中斷旗標。

            while (bRun)
            {
                nStep += 1;
                switch (nStep)
                {
                    case 1:// 開始異步呼叫。
                        objAsyncResult = pi_objJob.BeginInvoke(pi_sParameter, this.OnAsyncCompleted, pi_objJob);
                        break;
                    case 2:// 開啟等待視窗。    
                        this.OpenProgressView(this.l_objViewProvider, pi_nJobComlexity);
                        break;
                    case 3:// 等待異步呼叫結束。
                        sReturn = pi_objJob.EndInvoke(objAsyncResult);
                        break;
                    default://結束。
                        bRun = false;
                        break;
                }
            }

            return sReturn;
        }

        /// <summary>
        /// 執行沒有回傳值工作。
        /// </summary>
        /// <param name="pi_objJob">待執行的函式。</param>
        /// <param name="pi_sParamter">執行所需的參數。</param>
        /// <param name="pi_nJobComlexity">等待視窗種類。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void RunJob(JobWithoutReturnDelegate pi_objJob, string pi_sParamter, ProgressLevelEnum pi_nJobComlexity)
        {
            IAsyncResult objAsyncResult = null;

            int nStep = 0;//程序進度指標。
            Boolean bRun = true;//程序中斷旗標。

            while (bRun)
            {
                nStep += 1;
                switch (nStep)
                {
                    case 1:// 開始異步呼叫。
                        objAsyncResult = pi_objJob.BeginInvoke(pi_sParamter, this.OnAsyncCompleted, pi_objJob);
                        break;
                    case 2:// 開啟等待視窗。   
                        this.OpenProgressView(this.l_objViewProvider, pi_nJobComlexity);
                        break;
                    case 3:// 等待結束異步呼叫。
                        pi_objJob.EndInvoke(objAsyncResult);
                        break;
                    default://結束。
                        bRun = false;
                        break;
                }
            }
        }

        #endregion

        #region -- 私有函式 ( Private Method) --

        /// <summary>
        /// 開啟進度視窗。
        /// </summary>
        /// <param name="pi_objViewProvider">進度視窗提供器。</param>
        /// <param name="pi_nJobComlexity">進度類別。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private void OpenProgressView(IViewProvider pi_objViewProvider, ProgressLevelEnum pi_nJobComlexity)
        {

            int nStep = 0;      //程序進度指標。
            Boolean bRun = true;//程序中斷旗標。

            while (bRun)
            {
                nStep += 1;
                switch (nStep)
                {
                    case 1:// 排除不開啟進度畫面。
                        bRun = (pi_nJobComlexity != ProgressLevelEnum.None);
                        break;
                    case 2:// 取得進度畫面。
                        pi_objViewProvider.Parameter = pi_nJobComlexity.ToString();
                        this.l_objProgressView = pi_objViewProvider.GetViewInstance();
                        break;
                    case 3:// 開啟進度畫面。
                        this.l_objProgressView.ShowView();
                        break;
                    default://結束。
                        bRun = false;
                        break;
                }
            }
        }

        #endregion
    }
}
