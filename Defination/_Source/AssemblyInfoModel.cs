using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Definition
{
    /// <summary>
    /// 定義組件資訊資料物件。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>    
    public class AssemblyInfoModel
    {

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得或設定組件的名域空間。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string Namespace { get; set; }

        /// <summary>
        /// 取得或設定組件執行個體。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public System.Reflection.Assembly Instance { get; set; }

        #endregion

    }
}
