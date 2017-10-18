using Magikarp.Platform.Definition.MVP;
using Magikarp.Platform.Utility.Region;
using Magikarp.Utility.MVVM;
using System;
using System.Windows.Input;

namespace Magikarp.Platform.UI.Entry
{
    /// <summary>
    /// 提供 Main 的在 MVC 架構中的 View 角色並套用 MVVM 架構實做的 ViewModel 角色功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171018
    /// </remarks>
    public class MainViewModel : IView
    {

        #region -- 變數宣告 ( Declarations ) --  

        private string m_sDTO = string.Empty;
        private IViewPresenter m_objPresenter = null;

        private MainView l_objView = null;
        private TabRegionManager l_objRegionManager = null;

        private ICommand m_objRelayCommand = null;

        #endregion

        #region -- 建構/解構 ( Constructors/Destructor ) --

        /// <summary>
        /// 建構元。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public MainViewModel()
        {
            this.m_objRelayCommand = new RelayCommand(OnExecute, OnCanExecute);
        }

        #endregion

        #region -- 事件處理 ( Event Handlers ) --

        /// <summary>
        /// 處理命令執行事件。
        /// </summary>
        /// <param name="pi_objParameter">命令參數。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void OnExecute(object pi_objParameter)
        {
            this.m_objPresenter.OnViewEvent((string)pi_objParameter);
        }

        /// <summary>
        /// 處理命令是否可供執行事件。
        /// </summary>
        /// <param name="pi_objParamter">命令參數。</param>
        /// <returns>命令是否可供執行事件。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public Boolean OnCanExecute(object pi_objParamter)
        {
            return true;
        }

        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得主畫面指定的 Command 執行個體。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public ICommand ViewCommand
        {
            get { return this.m_objRelayCommand; }
        }

        #endregion

        #region -- 介面實做 ( Implements ) - [IView] --

        /// <summary>
        /// 取得或設定 MVP 模式中在 Presenter 與 View 之間傳遞資料的載體。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
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
        /// Time: 2017/09/30
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
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        void IView.ExitView()
        {
            this.l_objView.Close();
        }

        /// <summary>
        /// 顯示 View 畫面。
        /// </summary>
        /// <returns>操作結果。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: 
        ///     改以 FunctionEntryPanel 提供應用功能進入點。 (黃竣祥 2017/10/18)
        /// DB Object: N/A      
        /// </remarks>
        string IView.ShowView()
        {
            this.l_objView = new MainView();
            this.l_objView.DataContext = this;

            FunctionEntryPanel objEmptyEntryPanel = new FunctionEntryPanel()
            {
                FunctionTitle = "POST",
                FunctionDescription = "帳務金額計算。",
                FunctionCommand = "Show_POST"
            };
            objEmptyEntryPanel.SetImage("");

            FunctionEntryPanel objXBRLEntryPanel = new FunctionEntryPanel()
            {
                FunctionTitle = "XBRL",
                FunctionDescription = "讀取 XBRL 定義檔，建立案例文件。",                             
                FunctionCommand ="Show_XBRL"                
            };
            objXBRLEntryPanel.SetImage("/UI;component/_Images/XBRLKiosk_l.png");


            this.l_objView.FunctionEntryStackPanel.Children.Add(objEmptyEntryPanel);
            this.l_objView.FunctionEntryStackPanel.Children.Add(objXBRLEntryPanel);

            this.l_objRegionManager = this.InitialRegionManager(this.l_objView);
            this.l_objView.ShowDialog();

            return string.Empty;
        }

        #endregion

        #region -- 私有函式 ( Private Method) --

        /// <summary>
        /// 初始 RegionManager 物件。
        /// </summary>
        /// <param name="pi_objView">View 執行個體。</param>
        /// <returns>RegionManager 物件執行個體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private TabRegionManager InitialRegionManager(MainView pi_objView)
        {
            TabRegionManager objReturn = new TabRegionManager(pi_objView.MainTabControl);
            ViewRegionCenter.GetInstance().RegisterRegionManager(objReturn);
            return objReturn;
        }

        #endregion
    }
}
