using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Behavior.Progress
{
    /// <summary>
    /// 提供工作進度協助功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public class JobProgressHelper
    {

        #region -- 變數宣告 ( Declarations ) --          

        private string m_sJobDescription = string.Empty;
        private int m_nJobThreadID = 0;
        private int m_nProgressPercent = 0;

        private JobProgressHelper l_objParent = null;
        private Stack<JobProgressHelper> l_objChildren = new Stack<JobProgressHelper>();
        private JobStateEnum l_nJobStatement = JobStateEnum.Idle;// 目前工作狀態。
        private Boolean l_bSuspend = false;// 是否工作進度中斷。

        #endregion

        #region -- 建構/解構 ( Constructors/Destructor ) --

        /// <summary>
        /// 建構元。
        /// </summary>
        /// <param name="pi_sJobDescription">工作描述。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public JobProgressHelper(string pi_sJobDescription)
        {
            this.m_sJobDescription = pi_sJobDescription;
            this.m_nJobThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
            JobInfoCenter.GetInstance().RegisterJobProgress(this);
        }

        #endregion

        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 匯出為 ProgressEntry 物件。
        /// </summary>
        /// <returns>ProgressEntry 物件。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public ProgressEntry ExportToProgressEntry()
        {
            ProgressEntry objReturn = new ProgressEntry();
            Stack<JobProgressHelper> objTempChildren = new Stack<JobProgressHelper>();
            Stack<ProgressEntry> objEntryChildren = new Stack<ProgressEntry>();

            while (this.l_objChildren.Count > 0)
            {
                JobProgressHelper objTempJobProgressHelper = this.l_objChildren.Pop();

                objEntryChildren.Push(objTempJobProgressHelper.ExportToProgressEntry());
                objTempChildren.Push(objTempJobProgressHelper);
            }

            while (objTempChildren.Count > 0)
            {
                this.l_objChildren.Push(objTempChildren.Pop());
            }

            objReturn.SetDescription(this.JobDescription);
            objReturn.SetProgress(this.Progress);
            objReturn.SetChildren(objEntryChildren);

            return objReturn;
        }

        /// <summary>
        /// 設定進度完成。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void Finish()
        {
            Stack<JobProgressHelper> objTempChildren = new Stack<JobProgressHelper>();

            while (this.l_objChildren.Count > 0)
            {
                JobProgressHelper objChild = this.l_objChildren.Pop();

                objTempChildren.Push(objChild);
                objChild.Finish();
            }
            this.PushBackChild(objTempChildren);
            this.SetProgress(100);
        }

        /// <summary>
        /// 中斷工作進度，此工作進度及其子項都不可以更新或附掛工作進度。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void SuspendProgress()
        {
            JobProgressHelper objTempChild = null;
            Stack<JobProgressHelper> objTempStack = new Stack<JobProgressHelper>();

            while(this.l_objChildren.Count >0)
            {
                objTempChild = this.l_objChildren.Pop();
                objTempStack.Push(objTempChild);
                objTempChild.SuspendProgress();
            }
            this.PushBackChild(objTempStack);
        }

        /// <summary>
        /// 設定進度。
        /// </summary>
        /// <param name="pi_nProgressPercent">工作進度百分比。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void SetProgress(int pi_nProgressPercent)
        {
            JobStateEnum nLastJobStatement = this.l_nJobStatement;

            if (this.l_bSuspend)
            {
                // 若是工作中斷就不再更新進度。
            }
            else if (pi_nProgressPercent >= 100)
            {
                // 處理進度大於／等於 100 :設為結束狀態。
                this.m_nProgressPercent = 100;
                switch (this.l_nJobStatement)
                {
                    case JobStateEnum.Working:
                    case JobStateEnum.Idle:
                        this.l_nJobStatement = JobStateEnum.Completed;
                        break;
                    case JobStateEnum.Canceling:
                        this.l_nJobStatement = JobStateEnum.Canceled;
                        break;
                }
            }
            else if (pi_nProgressPercent > 0)
            {
                // 處理進度介於 0 到 100 :設為進行中狀態。
                this.m_nProgressPercent = pi_nProgressPercent;
                switch (this.l_nJobStatement)
                {
                    case JobStateEnum.Completed:
                    case JobStateEnum.Idle:
                        this.l_nJobStatement = JobStateEnum.Working;
                        break;
                    case JobStateEnum.Canceled:
                        this.l_nJobStatement = JobStateEnum.Canceling;
                        break;
                }
            }
            else
            {
                // 處理進度小於／等於 0 :設為等待狀態。
                this.m_nProgressPercent = 0;
                this.l_nJobStatement = JobStateEnum.Idle;
            }

            JobInfoCenter.GetInstance().Notify(JobNotifyObjectEnum.ProgressChange);
        }

        /// <summary>
        /// 更新工作描述。
        /// </summary>
        /// <param name="pi_sJobDescription">更新後工作描述。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void RenewDescription(string pi_sJobDescription)
        {
            this.m_sJobDescription = pi_sJobDescription;
        }

        /// <summary>
        /// 設定母項。
        /// </summary>
        /// <param name="pi_objParent">待設定母項。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        internal void SetParent(JobProgressHelper pi_objParent)
        {
            this.l_objParent = pi_objParent;
        }        

        /// <summary>
        /// 加入子項。
        /// </summary>
        /// <param name="pi_objChild">待加入子項。</param>
        /// <returns>是否成功加入。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        internal Boolean AddChild(JobProgressHelper pi_objChild)
        {
            Boolean bReturn = true;

            int nStep = 0;//程序進度指標。
            Boolean bRun = true;//程序中斷旗標。

            while (bRun)
            {
                nStep += 1;
                switch (nStep)
                {
                    case 1:// 判斷是否為自已的子項。
                        if(pi_objChild.m_nJobThreadID == this.m_nJobThreadID)
                        {
                            if (this.l_bSuspend == false )
                            {                                
                                this.KeepChild(pi_objChild);
                                bRun = false;
                            }
                        }
                        break;
                    case 2:// 確認是否為子項所有。
                        JobProgressHelper objTempChild = null;
                        Stack<JobProgressHelper> objTempStack = new Stack<JobProgressHelper>();
                        Boolean bIsFinded = false;

                        while (this.l_objChildren.Count > 0 && bIsFinded == false )
                        {
                            objTempChild = this.l_objChildren.Pop();
                            objTempStack.Push(objTempChild);
                            if (objTempChild.AddChild(pi_objChild))
                            {
                                bRun = false;// 有項目認領。                       
                                bIsFinded = true;
                            }
                        }
                        break;
                    case 3:// 若為根項目就收留下來。
                        if (this.l_objParent != null)
                        {
                            this.KeepChild(pi_objChild);
                            bRun = false;
                        }
                        break;
                    case 4:// 設定回傳值為未找到歸屬母項。
                        bReturn = false;
                        break;
                    default://結束。
                        bRun = false;
                        break;
                }
            }

            return bReturn;
        }
        
        /// <summary>
        /// 清除所有子項。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        internal void CleanChildren()
        {
            JobProgressHelper objTempChild = null;

            while(this.l_objChildren.Count > 0)
            {
                objTempChild = this.l_objChildren.Pop();
                objTempChild.CleanChildren();
                objTempChild = null;
            }
            this.l_objParent = null;
        }

        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得工作描述。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string JobDescription { get { return this.m_sJobDescription; } }

        /// <summary>
        /// 取得工作進度。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public int Progress { get { return this.m_nProgressPercent; } }

        /// <summary>
        /// 取得工作進度狀態。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public JobStateEnum State { get { return this.l_nJobStatement; } }

        #endregion

        #region -- 私有函式 ( Private Method) --

        /// <summary>
        /// 保存子項。
        /// </summary>
        /// <param name="pi_objChild">待保存子項執行個體。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private void KeepChild(JobProgressHelper pi_objChild)
        {
            pi_objChild.SetParent(this);
            this.l_objChildren.Push(pi_objChild);
        }             

        /// <summary>
        /// 倒回子項堆疊
        /// </summary>
        /// <param name="pi_objSource">待倒回的堆疊。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private void PushBackChild(Stack<JobProgressHelper> pi_objSource)
        {
            JobProgressHelper objTempChild = null;

            while (pi_objSource.Count > 0)
            {
                objTempChild = pi_objSource.Pop();
                this.l_objChildren.Push(objTempChild);
            }
        }

        #endregion
    }
}
