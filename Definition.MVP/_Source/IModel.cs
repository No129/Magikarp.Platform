using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Definition.MVP
{
    /// <summary>
    /// 定義 MVP 模式中的 Model 角色操作介面。
    /// Model 負責的「邏輯」應為程序邏輯，對應使用者的商業邏輯應在如 BC(資料庫)/Report(文件) 的組件進行實做。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public interface IModel
    {
        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 執行資料處理。
        /// </summary>
        /// <param name="pi_sCommand">前端操作觸發的命令。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        void Execute(string pi_sCommand);

        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得或設定處理資料。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        string DataModel { get; set; }

        /// <summary>
        /// 設定系統流程控制器。( 採用唯寫模式以避免 Presenter 直接使用控制器執行命令 )
        /// </summary>        
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        IController Controller { set; }

        #endregion

    }
}
