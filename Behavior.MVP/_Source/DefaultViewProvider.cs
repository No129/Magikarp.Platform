using Magikarp.Platform.Definition;
using Magikarp.Platform.Definition.MVP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Behavior.MVP
{
    /// <summary>
    /// 提供 ViewHandler 流程控制中的 ViewProvider 功能的預設處理。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170927
    /// </remarks>
    public class DefaultViewProvider : IViewProvider
    {

        #region -- 變數宣告 ( Declarations ) --   

        private List<AssemblyInfoModel> l_objViewAssemblyInfoModels = null;
        private string l_sParameter = string.Empty;
        private string l_sFunctionName = string.Empty;

        #endregion

        #region -- 建構/解構 ( Constructors/Destructor ) --

        /// <summary>
        /// 建構元。
        /// </summary>
        /// <param name="pi_objViewAssemblyInfoModels">View 的組件資訊物件。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public DefaultViewProvider(List<AssemblyInfoModel> pi_objViewAssemblyInfoModels)
        {
            this.l_objViewAssemblyInfoModels = pi_objViewAssemblyInfoModels;
        }

        #endregion

        #region -- 介面實做 ( Implements ) - [IViewProvider] --

        /// <summary>
        /// 設定或取得畫面類別執行個體產生參數，用以改變執行個體的特定行為。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        string IViewProvider.Parameter
        {
            get { return this.l_sParameter; }
            set { this.l_sParameter = value; }
        }

        /// <summary>
        /// 取得或設定功能名稱，提供 ViewProvider 建立對應 View 的參考使用。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        string IViewProvider.FunctionName
        {
            get { return this.l_sFunctionName; }
            set { this.l_sFunctionName = value; }
        }

        /// <summary>
        /// 取得 View 角色執行個體。
        /// </summary>
        /// <returns>View 角色執行個體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: 
        ///     遍歷所有 View 組件以生成對應類別實體。(黃竣祥 2017/09/27)
        /// DB Object: N/A      
        /// </remarks>
        IView IViewProvider.GetViewInstance()
        {
            IView objReturn = null;

            int nStep = 0;//程序進度指標。
            Boolean bRun = true;//程序中斷旗標。

            while (bRun)
            {
                nStep += 1;
                switch (nStep)
                {
                    case 1:// 依 FunctionName 動態建立 ViewModel 實體。
                        foreach(AssemblyInfoModel objModel in this.l_objViewAssemblyInfoModels)
                        {
                            string sNamespace = objModel.Namespace;
                            string sFullName = String.Format("{0}.{1}ViewModel", sNamespace, this.l_sFunctionName);

                            objReturn = (IView)objModel.Instance.CreateInstance(sFullName);
                            if(objReturn != null) { break; }
                        }                        
                        break;

                    case 2:// 判斷是否取得 ViewModel 實體。
                        bRun = (objReturn == null);
                        break;

                    case 3:// 未取得對應 ViewModel 實體。
                        objReturn = new UndoneViewModel();
                        break;

                    default://結束。
                        bRun = false;
                        break;
                }
            }

            return objReturn;
        }

        #endregion
    }
}
