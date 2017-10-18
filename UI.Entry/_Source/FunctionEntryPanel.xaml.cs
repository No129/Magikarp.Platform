using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Magikarp.Platform.UI.Entry
{
    /// <summary>
    /// 提供功能起點按鍵。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171018
    /// </remarks>
    public partial class FunctionEntryPanel : UserControl
    {

        #region -- 變數宣告 ( Declarations ) --   

        private string m_sCommand = string.Empty;
        private string m_sImagePath = "/Magikarp.Platform.UI.Entry;component/_Images/EmptySetting.jpg";

        #endregion

        #region -- 建構/解構 ( Constructors/Destructor ) --

        /// <summary>
        /// 建構元。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/18
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public FunctionEntryPanel()
        {
            InitializeComponent();
        }

        #endregion

        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 設定圖片。
        /// </summary>
        /// <param name="pi_sImagePath">圖片路徑。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/18
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void SetImage(string pi_sImagePath)
        {
            if (string.IsNullOrEmpty(pi_sImagePath ) == false)
            {
                this.m_sImagePath = pi_sImagePath;
            }

            BitmapImage objImageSource = new BitmapImage();

            objImageSource.BeginInit();
            objImageSource.UriSource = new Uri(this.m_sImagePath, UriKind.Relative);
            objImageSource.EndInit();
            this.FunctionImageControl.Source = objImageSource;
        }

        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得或設定功能命令字串。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/18
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string FunctionCommand
        {
            get { return this.m_sCommand; }
            set
            {
                this.m_sCommand = value;
                this.FunctionButton.CommandParameter = value;
            }
        }

        /// <summary>
        /// 取得或設定功能名稱。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/18
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string FunctionTitle
        {
            get { return this.FunctionTitleTextBlock.Text; }
            set { this.FunctionTitleTextBlock.Text = value; }
        }

        /// <summary>
        /// 取得或設定功能描述。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/18
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string FunctionDescription
        {
            get { return this.FunctionDescriptionTextBlock.Text; }
            set { this.FunctionDescriptionTextBlock.Text = value; }
        }           

        #endregion        

    }
}
