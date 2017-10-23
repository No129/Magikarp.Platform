using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Definition
{
    /// <summary>
    /// 定義檔案選取畫面建立資訊。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171023
    /// </remarks>
    public class SelecteFileViewDTO
    {
        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得或設定視窗抬頭。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/23
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string ViewTitle { get; set; } = "Selecte File";

        /// <summary>
        /// 取得或設定預設路徑。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/23
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string InitialDirectory { get; set; } = ".\\";

        /// <summary>
        /// 取得或設定過濾字串。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/23
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string FileFilter { get; set; }

        #endregion

    }
}
