using Magikarp.Platform.Behavior.MVP;
using Magikarp.Platform.Definition;
using Magikarp.Platform.Definition.MVP;
using Magikarp.Utility.TransitData;
using System;

namespace Magikarp.Platform.Behavior.Flow
{
    /// <summary>
    /// 提供處理前瑞畫面顯示操作流程功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public class ViewHandler : BaseHandler
    {
        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 取回要求處理結果。
        /// </summary>
        /// <param name="pi_sRequest">待處理要求。</param>
        /// <returns>要求處理結果。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public override string GetResponse(string pi_sRequest)
        {
            string sReturn = string.Empty;
            HandlerModel objHandlerModel = null;
            IViewProvider objViewProvider = null;
            IView objView = null;
            IPresenter objPresenter = null;
            IModel objModel = null;

            int nStep = 0;//程序進度指標。
            Boolean bRun = true;//程序中斷旗標。

            while (bRun)
            {
                nStep += 1;
                switch (nStep)
                {
                    case 1:// 取得傳入參數。                        
                        objHandlerModel = new TransitDataAdapter().Loading<HandlerModel>(pi_sRequest);
                        break;

                    case 2:// 取得 ViewProvider 實體。
                        objViewProvider = AssemblyManager.GetInstance().CreateProduct<IViewProvider>(objHandlerModel.FunctionName, "ViewProvider", AssemblyTypeEnum.View);
                        break;

                    case 3:// 判斷是否取得 ViewProvider 物件。                                             
                        if(objViewProvider == null)
                        {
                            objViewProvider = new DefaultViewProvider(AssemblyManager.GetInstance().ViewAssemblyInfoModel);
                        }
                        break;

                    case 4:// 取得對應 View 實體。
                        objViewProvider.Parameter = objHandlerModel.HandlerParameter;
                        objViewProvider.FunctionName = objHandlerModel.FunctionName;
                        objView = objViewProvider.GetViewInstance();
                        break;

                    case 5:// 判斷是否取得 View 物件。
                        bRun = (objView != null);
                        break;

                    case 6:// 建立對應的 Presenter 實體。
                        objPresenter = AssemblyManager.GetInstance().CreateProduct<IPresenter>(objHandlerModel.FunctionName, "Presenter", AssemblyTypeEnum.Central);
                        break;

                    case 7:// 判斷是否取得 Presenter 物件。                       
                        if (objPresenter == null) { objPresenter = new DefaultPresenter(); }
                        break;

                    case 8:// 取得對應的 Model 實體。
                        objModel = AssemblyManager.GetInstance().CreateProduct<IModel>(objHandlerModel.FunctionName, "Model", AssemblyTypeEnum.Central);
                        break;

                    case 9:// 判斷是否取得 Model 物件。                        
                        if (objModel == null) { objModel = new DefaultModel(); }
                        break;

                    case 10: // 取得畫面初始資料。
                        objModel.Controller = new FlowController();
                        objView.DTO = objHandlerModel.HandlerParameter;
                        break;

                    case 11:// 透過 Presenter 呼叫畫面顯示並取回處理結果。
                        objPresenter.View = objView;
                        objPresenter.Model = objModel;
                        objView.Presenter = objPresenter;

                        sReturn = objPresenter.OnViewShow(objHandlerModel.HandlerParameter);
                        break;

                    default://結束。
                        bRun = false;
                        break;
                }
            }

            return sReturn;
        }

        #endregion        
    }
}
