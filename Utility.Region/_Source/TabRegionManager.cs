using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Magikarp.Platform.Utility.Region
{
    /// <summary>
    /// 提供頁籤式內嵌區域管理功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171003
    /// </remarks>
    public class TabRegionManager : IRegionManager
    {
        #region -- 變數宣告 ( Declarations ) --   

        TabControl l_objTabControl = null;
        Dictionary<string, TabRegion> l_objRegions = new Dictionary<string, TabRegion>();

        #endregion

        #region -- 建構/解構 ( Constructors/Destructor ) --

        /// <summary>
        /// 建構元。
        /// </summary>
        /// <param name="pi_objTabControl">裝載內嵌區域內容的頁籤控制項執行個體。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public TabRegionManager(TabControl pi_objTabControl)
        {
            this.l_objTabControl = pi_objTabControl;
        }

        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得 MainRegion 中所有的 TabItem 執行個體。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public ItemCollection TabItems
        {
            get { return this.l_objTabControl.Items; }
        }

        #endregion

        #region -- 介面實做 ( Implements ) - [InterfaceName] --

        /// <summary>
        /// 取得指定名稱的區域執行個體。
        /// </summary>
        /// <param name="pi_sRegionName">區域名稱。</param>        
        /// <returns>指定名稱的區域執行個體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        IRegion IRegionManager.Regions(string pi_sRegionName)
        {
            TabRegion objReturn = null;
            int nSelectedIndex = 0;

            // 建立頁籤區域實體。
            if (this.l_objRegions.ContainsKey(pi_sRegionName))
            {
                objReturn = this.l_objRegions[pi_sRegionName];
            }
            else
            {
                objReturn = new TabRegion(this, pi_sRegionName);
                this.l_objRegions.Add(pi_sRegionName, objReturn);
                nSelectedIndex = this.l_objTabControl.Items.Add(objReturn.TabItem);
            }

            // 設定目前選取頁籤。
            for (int nIndex = nSelectedIndex; nIndex <= this.l_objTabControl.Items.Count - 1; nIndex++)
            {
                if (this.l_objTabControl.Items[nIndex] == objReturn.TabItem)
                {
                    this.l_objTabControl.SelectedIndex = nIndex;
                    break;
                }
            }

            return objReturn;
        }

        /// <summary>
        /// 移除指定區域的執行個體。
        /// </summary>
        /// <param name="pi_objRegion">指定區域的執行個體。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: 
        ///     加上資料回收呼叫。 (黃竣祥 2017/10/03)
        /// DB Object: N/A      
        /// </remarks>
        void IRegionManager.Remove(IRegion pi_objRegion)
        {
            string sKeyIndex =
                (from KeyValuePair<string, TabRegion> objPackage in this.l_objRegions
                 where objPackage.Value == pi_objRegion
                 select objPackage.Key).FirstOrDefault();

            if (string.IsNullOrEmpty(sKeyIndex) == false)
            {
                this.l_objTabControl.Items.Remove(this.l_objRegions[sKeyIndex].TabItem);
                this.l_objRegions.Remove(sKeyIndex);
                System.GC.Collect();
            }
        }

        #endregion
    }
}
