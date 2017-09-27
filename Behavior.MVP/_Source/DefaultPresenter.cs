using Magikarp.Platform.Definition.MVP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Behavior.MVP
{
    /// <summary>
    /// 提供 MVC 架構的 Presenter 功能預設處理。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170927
    /// </remarks>
    public class DefaultPresenter : IPresenter
    {
        #region -- 變數宣告 ( Declarations ) --   

        private IView m_objView = null;
        private IModel m_objModel = null;       

        #endregion

        #region -- 介面實做 ( Implements ) - [IPresenter] --

        /// <summary>
        /// 取得或設定 MVP 架構中對應操作的 Model 執行個體。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        IModel IPresenter.Model {
            get { return this.m_objModel; }
            set { this.m_objModel = value; }
        }

        /// <summary>
        /// 取得或設定 MVP 架構中對應操作的 View 執行個體。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        IView IPresenter.View
        {
            get { return this.m_objView; }
            set { this.m_objView = value; }
        }

        #endregion

        #region -- 介面實做 ( Implements ) - [IViewPresenter] --

        /// <summary>
        /// 處理前端操作事件。
        /// </summary>
        /// <param name="pi_sParameter">操作參數。</param>
        /// <returns>操作處理結果。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: 
        ///     提供一般化操作。 (黃竣祥 2017/09/27)
        /// DB Object: N/A      
        /// </remarks>
        string IViewPresenter.OnViewEvent(string pi_sParameter)
        {
            this.m_objModel.DataModel = this.m_objView.DTO;
            this.m_objModel.Execute(pi_sParameter);

            return this.m_objModel.DataModel;
        }

        #endregion

        #region -- 介面實做 ( Implements ) - [IModelPresenter] --

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
        string IModelPresenter.OnViewShow(string pi_sInitialData)
        {
            return this.m_objView.ShowView();
        }

        #endregion
    }
}
