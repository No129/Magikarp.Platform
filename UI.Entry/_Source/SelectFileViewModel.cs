using System;
using Magikarp.Platform.Definition.MVP;
using System.Windows.Forms;
using Magikarp.Platform.Definition;
using Magikarp.Utility.TransitData;

namespace Magikarp.Platform.UI.Entry
{
    /// <summary>
    /// 提供檔案選取功能畫面的在 MVC 架構中的 View 角色並套用 MVVM 架構實做的 ViewModel 角色功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171023
    /// </remarks>
    public class SelectFileViewModel : IView
    {
        #region -- 變數宣告 ( Declarations ) --  

        private string m_sDTO = string.Empty;
        private IViewPresenter m_objPresenter = null;

        #endregion

        #region -- 介面實做 ( Implements ) - [IView] --

        /// <summary>
        /// 取得或設定 MVP 模式中在 Presenter 與 View 之間傳遞資料的載體。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/23
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        string IView.DTO
        {
            get { return this.m_sDTO; }
            set { this.m_sDTO = value; }
        }

        /// <summary>
        /// 取得或設定 MVP 模式中的 Presenter 角色。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/23
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        IViewPresenter IView.Presenter
        {
            get { return this.m_objPresenter; }
            set { this.m_objPresenter = value; }
        }

        /// <summary>
        /// 結束 View 畫面。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/23
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        void IView.ExitView()
        {

        }

        /// <summary>
        /// 顯示 View 畫面。
        /// </summary>
        /// <returns>操作結果。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/23
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        string IView.ShowView()
        {
            string sReturn = string.Empty;
            OpenFileDialog dialog = new OpenFileDialog();
            TransitDataAdapter objAdapter = new TransitDataAdapter();
            SelecteFileViewDTO objDTO = objAdapter.Loading<SelecteFileViewDTO>(this.m_sDTO);

            dialog.Title = objDTO.ViewTitle;
            dialog.InitialDirectory = objDTO.InitialDirectory;
            dialog.Filter = objDTO.FileFilter;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                sReturn = dialog.FileName;
            }
            return sReturn;
        }

        #endregion
    }
}
