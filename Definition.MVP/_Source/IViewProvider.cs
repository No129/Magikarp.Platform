using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Definition.MVP
{
    /// <summary>
    /// 定義 MVP 模式中 View 角色建立中介物件操作介面。
    /// 透過此介面將建立 View 角色物件的程序移轉出 ViewHandler 的處理程序，提供各個 View 角色物件的可自訂其建立流程。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public interface IViewProvider
    {

        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 取得 View 角色執行個體。
        /// </summary>
        /// <returns>View 角色執行個體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        IView GetViewInstance();

        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 設定或取得畫面類別執行個體產生參數，用以改變執行個體的特定行為。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        string Parameter { get; set; }

        /// <summary>
        /// 取得或設定功能名稱，提供 ViewProvider 建立對應 View 的參考使用。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        string FunctionName { get; set; }

        #endregion

    }
}
