using Magikarp.Platform.Definition;
using System.Windows.Input;

namespace Magikarp.Platform.UI.Entry
{

    /// <summary>
    /// 定義 Entry 組件專用的 FunctionEntryModel 資料物件。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: [Version]
    /// </remarks>
    public class EntryFunctionEntryModel : FunctionEntryModel
    {
        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得及設定功能圖樣路徑。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: [Time]
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public override string FunctionImagePath { get; set; } = "/Magikarp.Platform.UI.Entry;component/_Images/EmptySetting.jpg";

        /// <summary>
        /// 設定或取得 Command 執行個體。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: [Time]
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public ICommand ViewCommand { get; set; }

        #endregion        
    }
}
