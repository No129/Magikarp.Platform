using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Definition.MVP
{
    /// <summary>
    /// 定義 MVP 架構中 Presenter 角色功能的操作介面。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public interface IPresenter:IViewPresenter, IModelPresenter 
    {
        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得或設定 MVP 架構中對應操作的 Model 執行個體。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        IModel Model { get; set; }

        /// <summary>
        /// 取得或設定 MVP 架構中對應操作的 View 執行個體。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        IView View { get; set; }

        #endregion       
    }
}
