using Magikarp.Platform.Definition;
using Magikarp.Platform.Definition.MVP;

namespace Magikarp.Platform.Behavior.MVP
{
    /// <summary>
    /// 提供 MVC 架構的 Model 角色功能預設處理。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170927
    /// </remarks>
    public class DefaultModel : IModel
    {

        #region -- 變數宣告 ( Declarations ) --   

        string m_sDataModel = string.Empty;
        IController m_objController = null;

        #endregion

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
        string IModel.DataModel
        {
            get { return this.m_sDataModel; }
            set { this.m_sDataModel = value; }
        }

        /// <summary>
        /// 設定系統流程控制器。( 採用唯寫模式以避免 Presenter 直接使用控制器執行命令 )
        /// </summary>        
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        IController IModel.Controller { set { this.m_objController = value; } }

        /// <summary>
        /// 執行資料處理。
        /// </summary>
        /// <param name="pi_sCommand">前端操作觸發的命令。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: 
        ///     提供預設功能。 (黃竣祥 2017/09/27)
        /// DB Object: N/A      
        /// </remarks>
        void IModel.Execute(string pi_sCommand)
        {
            this.m_sDataModel = this.m_objController.DispatchCommand(pi_sCommand, this.m_sDataModel);
        }

        #endregion
    }
}
