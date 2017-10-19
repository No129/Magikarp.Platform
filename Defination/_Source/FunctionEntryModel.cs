namespace Magikarp.Platform.Definition
{

    /// <summary>
    /// 定義功能入口資訊。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171019
    /// </remarks>
    public class FunctionEntryModel
    {

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得及設定功能啟動命令。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/19
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string FunctionCommand { get; set; }

        /// <summary>
        /// 取得及設定功能名稱抬頭。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/19
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string FunctionTitle { get; set; }

        /// <summary>
        /// 取得及設定功能描述文字。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/19
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string FunctionDescription { get; set; }

        /// <summary>
        /// 取得及設定功能圖樣路徑。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/19
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public virtual string FunctionImagePath { get; set; }

        #endregion

    }
}
