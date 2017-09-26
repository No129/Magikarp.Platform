using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Definition.MVP
{
    /// <summary>
    /// 定義 MVP 架構中，提供 Model 互動的 Presenter 的操作介面。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public interface IModelPresenter
    {
        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 觸發畫面顯示。
        /// </summary>
        /// <param name="pi_sInitialData">畫面初始資料。</param>
        /// <returns>關閉畫面後回傳資料。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        string OnViewShow(string pi_sInitialData);

        #endregion
    }
}
