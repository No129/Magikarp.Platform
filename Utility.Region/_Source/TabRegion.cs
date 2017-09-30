using Magikarp.Platform.Definition.MVP;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Magikarp.Platform.Utility.Region
{
    /// <summary>
    /// 提供頁籤式的內嵌區域類別功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170930
    /// </remarks>
    public class TabRegion : IRegion
    {
        #region -- 變數宣告 ( Declarations ) --   

        private IRegionManager l_objRegionManager = null;
        private TabItem l_objTabItem = null;
        private IRegionContent l_objContent = null;

        #endregion

        #region -- 建構/解構 ( Constructors/Destructor ) --

        /// <summary>
        /// 建構元。
        /// </summary>
        /// <param name="pi_objContainer">容器物件。</param>
        /// <param name="pi_sCaption">標籤文字。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public TabRegion(IRegionManager pi_objContainer, string pi_sCaption)
        {
            this.l_objRegionManager = pi_objContainer;
            this.l_objTabItem = new TabItem();

            StackPanel objStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            TextBlock objTextBlock = new TextBlock();
            Image objImage = new Image();
            BitmapImage objBitmapImage = new BitmapImage();

            objTextBlock.Text = pi_sCaption;
            objTextBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            objBitmapImage.BeginInit();
            objBitmapImage.UriSource = new Uri("pack://application:,,,/Magikarp.Platform.Utility.Region;component/_Images/TabClose_s.png", UriKind.Absolute);
            objBitmapImage.EndInit();

            objImage.Width = 12;
            objImage.Height = 12;
            objImage.Margin = new System.Windows.Thickness(5, 0, 0, 0);
            objImage.Source = objBitmapImage;
            objImage.Cursor = System.Windows.Input.Cursors.Hand;
            objImage.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.OnMouseDown);

            objStackPanel.Children.Add(objTextBlock);
            objStackPanel.Children.Add(objImage);

            this.l_objTabItem.Header = objStackPanel;
            this.l_objTabItem.Tag = pi_sCaption;
        }
       
        #endregion

        #region -- 事件處理 ( Event Handlers ) --

        /// <summary>
        /// 處理關閉圖片點擊事件。
        /// </summary>
        /// <param name="pi_objSender">觸發事件個體。</param>
        /// <param name="pi_objEventArgs">事件參數。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private void OnMouseDown(System.Object pi_objSender, System.Windows.Input.MouseButtonEventArgs pi_objEventArgs)
        {
            if(this.l_objContent is IView)
            {
                ((IView)this.l_objContent).ExitView();
            }
            this.l_objContent = null;
            this.l_objTabItem.Content = null;
            this.l_objRegionManager.Remove(this);
        }

        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得區域所屬的頁籤控制項執行個體。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public TabItem TabItem { get { return this.l_objTabItem; } }

        #endregion

        #region -- 介面實做 ( Implements ) - [IRegion] --

        /// <summary>
        /// 新增內容到區域所屬控制項。
        /// </summary>
        /// <param name="pi_objContent">待新增至區域控制項中成為內容的控制項執行個體。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        void IRegion.Add(IRegionContent pi_objContent)
        {
            this.l_objContent = pi_objContent;
            this.l_objTabItem.Content = pi_objContent.Content;
        }

        #endregion      
    }
}
