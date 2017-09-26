using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Definition.MVP
{
    /// <summary>
    /// 定義 MVP 模式中的 View 角色操作介面。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public interface IView
    {
        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 顯示 View 畫面。
        /// </summary>
        /// <returns>操作結果。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        string ShowView();

        /// <summary>
        /// 結束 View 畫面。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        void ExitView();

        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得或設定 MVP 模式中在 Presenter 與 View 之間傳遞資料的載體。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        string DTO { get; set; }

        /// <summary>
        /// 取得或設定 MVP 模式中的 Presenter 角色。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        IViewPresenter Presenter { get; set; }

        #endregion
        
    }
}
