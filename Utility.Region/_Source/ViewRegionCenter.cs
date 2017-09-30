using System;
using System.Collections.Concurrent;

namespace Magikarp.Platform.Utility.Region
{
    /// <summary>
    /// 提供視窗區域交流功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: [Version]
    /// </remarks>
    public class ViewRegionCenter
    {
        #region -- 變數宣告 ( Declarations ) --   

        private static ViewRegionCenter m_objViewRegionCenter = null;
        private static object l_objLock = new object();

        private ConcurrentDictionary<Type, IRegionManager> l_objRegionCollection = new ConcurrentDictionary<Type, IRegionManager>();

        #endregion

        #region -- 建構/解構 ( Constructors/Destructor ) --

        /// <summary>
        /// 建構元。( 配合 Singleton Design Pattern 限制建構元呼叫 )
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private ViewRegionCenter() { }

        #endregion

        #region -- 靜態方法 (Shared Method ) --

        /// <summary>
        /// 取得單一實體。
        /// </summary>
        /// <returns>取得 ViewRegionCenter 單一實體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public static ViewRegionCenter GetInstance()
        {
            if (ViewRegionCenter.m_objViewRegionCenter == null)
            {
                lock (ViewRegionCenter.l_objLock)
                {
                    if (ViewRegionCenter.m_objViewRegionCenter == null)
                    {
                        ViewRegionCenter.m_objViewRegionCenter = new ViewRegionCenter();
                    }
                }
            }
            return ViewRegionCenter.m_objViewRegionCenter;
        }

        #endregion

        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 註冊 RegionManager 執行個體。
        /// </summary>
        /// <param name="pi_objTarget">待註冊的 RegionManager 執行個體。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void RegisterRegionManager(IRegionManager pi_objTarget)
        {
            if (this.l_objRegionCollection.ContainsKey(pi_objTarget.GetType()) == false)
            {
                this.l_objRegionCollection.TryAdd(pi_objTarget.GetType(), pi_objTarget);
            }
        }

        /// <summary>
        /// 註銷 RegionManger 執行個體。
        /// </summary>
        /// <param name="pi_objTarget">待註銷的 RegionManager 執行個體。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void UnregisterRegionManager(IRegionManager pi_objTarget)
        {
            this.l_objRegionCollection.TryRemove(pi_objTarget.GetType(), out pi_objTarget);
        }

        /// <summary>
        /// 取得已註冊的指定 RegionManager 的執行個體。
        /// </summary>
        /// <param name="pi_objTarget">要取得的 RegionManager 型別。</param>
        /// <returns>已註冊的 RegionManager 的執行個體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public IRegionManager GetRegionManager(Type pi_objTarget)
        {
            IRegionManager objReturn = null;

            if (this.l_objRegionCollection.ContainsKey(pi_objTarget))
            {
                objReturn = this.l_objRegionCollection[pi_objTarget];
            }

            return objReturn;
        }

        #endregion

    }
}
