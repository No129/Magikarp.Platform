using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Definition.MVP
{
    /// <summary>
    /// 定義 MVP 架構中，提供 View 互動的 Presenter 的操作介面。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public interface IViewPresenter
    {
        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 處理前端操作事件。
        /// </summary>
        /// <param name="pi_sParameter">操作參數。</param>
        /// <returns>操作處理結果。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        string OnViewEvent(string pi_sParameter);

        #endregion
     
    }
}
