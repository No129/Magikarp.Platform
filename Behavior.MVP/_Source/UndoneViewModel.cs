using Magikarp.Platform.Definition.MVP;
using System;

namespace Magikarp.Platform.Behavior.MVP
{
    /// <summary>
    /// 提供未定義功能的 ViewModel 功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public class UndoneViewModel : IView
    {
        #region -- 介面實做 ( Implements ) - [IView] --

        /// <summary>
        /// 取得或設定 MVP 模式中在 Presenter 與 View 之間傳遞資料的載體。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        string IView.DTO { get; set; }

        /// <summary>
        /// 取得或設定 MVP 模式中的 Presenter 角色。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        IViewPresenter IView.Presenter { get; set; }

        /// <summary>
        /// 結束 View 畫面。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        void IView.ExitView()
        {
            throw new NotImplementedException();
        }

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
        string IView.ShowView()
        {
            System.Windows.Forms.MessageBox.Show("功能尚未完成。");
            return string.Empty;
        }

        #endregion
    }
}
