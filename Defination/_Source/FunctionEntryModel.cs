namespace Magikarp.Platform.Definition
{

    /// <summary>
    /// 定義功能入口資訊。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: [Version]
    /// </remarks>
    public class FunctionEntryModel
    {

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得及設定功能啟動命令。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: [Time]
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string FunctionCommand { get; set; }

        /// <summary>
        /// 取得及設定功能名稱抬頭。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: [Time]
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string FunctionTitle { get; set; }

        /// <summary>
        /// 取得及設定功能描述文字。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: [Time]
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string FunctionDescription { get; set; }

        /// <summary>
        /// 取得及設定功能圖樣路徑。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: [Time]
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public virtual string FunctionImagePath { get; set; }

        #endregion

    }
}
