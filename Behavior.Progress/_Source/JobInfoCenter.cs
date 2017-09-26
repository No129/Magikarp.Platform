using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Behavior.Progress
{
    /// <summary>
    /// 工作資訊中心。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public class JobInfoCenter
    {

        #region -- 變數宣告 ( Declarations ) --   

        public delegate void ObserverResponse(JobNotifyObjectEnum pi_nNotifyInfo);

        private static JobInfoCenter l_objJobCenter = null;
        private static object l_objSyncObject = new object();

        private List<ObserverResponse> l_objObserverList = new List<ObserverResponse>();
        private int l_nThreadCount = 0; // 執行緒計數。
        private JobProgressHelper l_objEntryProgressHelper = null; // 進度資訊的根物件。
        private Object l_objSynclockThread = new object();
        private JobStateEnum l_nJobStatement = JobStateEnum.Idle;

        #endregion

        #region -- 建構/解構 ( Constructors/Destructor ) --

        /// <summary>
        /// 限定內部才能建立實體以達獨體模式。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private JobInfoCenter() { }

        #endregion

        #region -- 靜態方法 (Shared Method ) --

        /// <summary>
        /// 取得類別 JobCenter 物件實體。
        /// </summary>
        /// <returns>類別 JobCenter 物件實體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public static JobInfoCenter GetInstance()
        {
            if (JobInfoCenter.l_objJobCenter == null)
            {
                lock (JobInfoCenter.l_objSyncObject)
                {
                    if (JobInfoCenter.l_objJobCenter == null)
                    {
                        JobInfoCenter.l_objJobCenter = new JobInfoCenter();
                    }
                }
            }
            return JobInfoCenter.l_objJobCenter;
        }

        #endregion

        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 加入成為觀察者。
        /// </summary>
        /// <param name="pi_objObserver">用戶端自訂用以接收通知的委派函式。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void AddObserver(ObserverResponse pi_objObserver)
        {
            this.l_objObserverList.Add(pi_objObserver);
        }

        /// <summary>
        /// 移除觀察者。
        /// </summary>
        /// <param name="pi_objObserver">資料型態。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void RemoveObserver(ObserverResponse pi_objObserver)
        {
            this.l_objObserverList.Remove(pi_objObserver);
        }

        /// <summary>
        /// 通知觀察者發生異動。
        /// </summary>
        /// <param name="pi_nNotifyObject">異動資料物件。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void Notify(JobNotifyObjectEnum pi_nNotifyObject)
        {
            foreach (ObserverResponse objObserver in this.l_objObserverList)
            {
                objObserver.Invoke(pi_nNotifyObject);
            }
        }

        /// <summary>
        /// 註冊工作進度資訊物件。
        /// </summary>
        /// <param name="pi_objJobProgress">工作進度資訊物件。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void RegisterJobProgress(JobProgressHelper pi_objJobProgress)
        {
            if (this.l_objEntryProgressHelper == null)
            {
                this.l_objEntryProgressHelper = pi_objJobProgress;
            }
            else
            {
                this.l_objEntryProgressHelper.AddChild(pi_objJobProgress);
            }
        }

        /// <summary>
        /// 增加執行緒計數。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void IncreaseThreadCount()
        {
            lock (this.l_objSynclockThread)
            {
                this.l_nThreadCount++;
                if (this.l_nJobStatement == JobStateEnum.Idle)
                {
                    this.l_nJobStatement = JobStateEnum.Working;
                    JobInfoCenter.GetInstance().Notify(JobNotifyObjectEnum.StatementChange);
                }
            }
        }

        /// <summary>
        /// 減少執行緒計數。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void ReduceThreadCount()
        {

            int nStep = 0;//程序進度指標。
            Boolean bRun = true;//程序中斷旗標。

            while (bRun)
            {
                nStep += 1;
                switch (nStep)
                {
                    case 1:// 減去一個執行緒計數。
                        this.l_nThreadCount = this.l_nThreadCount - 1;
                        break;
                    case 2:// 判斷目前執行緒計數。
                        bRun = (this.l_nThreadCount <= 0);
                        break;
                    case 3:// 已無執行緒的處理。
                        JobStateEnum nTempStatement = this.l_nJobStatement;
                        switch (this.l_nJobStatement)
                        {
                            case JobStateEnum.Working:
                                nTempStatement = JobStateEnum.Completed;
                                break;
                            case JobStateEnum.Canceling:
                                nTempStatement = JobStateEnum.Completed;
                                break;
                        }

                        if (this.l_nJobStatement != nTempStatement) { this.l_nJobStatement = nTempStatement; }
                        if (this.l_objEntryProgressHelper != null)
                        {
                            this.l_objEntryProgressHelper.CleanChildren();
                            this.l_objEntryProgressHelper = null;
                        }
                        this.l_nThreadCount = 0; //計數歸零。
                        this.l_nJobStatement = JobStateEnum.Idle;

                        break;
                    default://結束。
                        bRun = false;
                        break;
                }
            }
        }
        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得工作進度資料物件。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public JobProgressHelper Progress { get { return this.l_objEntryProgressHelper; } }

        /// <summary>
        /// 取得工作狀態。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public JobStateEnum Statement { get { return this.l_nJobStatement; } }

        #endregion

    }
}
