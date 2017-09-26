using Magikarp.Platform.Definition.MVP;

namespace Magikarp.Platform.Behavior.MVP
{
    /// <summary>
    /// 提供 MVC 架構的 Model 角色功能預設處理。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public class DefaultModel : IModel
    {
        #region -- 介面實做 ( Implements ) - [IModel] --

        /// <summary>
        /// 取得或設定處理資料。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        string IModel.DataModel { get; set; }

        /// <summary>
        /// 設定系統流程控制器。( 採用唯寫模式以避免 Presenter 直接使用控制器執行命令 )
        /// </summary>        
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        Platform.Definition.IController IModel.Controller { set { } }

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
        void IModel.Execute(string pi_sCommand) { }

        #endregion
    }
}
