using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Definition
{
    /// <summary>
    /// 定義 Handler 傳入參數資料物件。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public class HandlerModel
    {

        /// <summary>
        /// 取得或設定功能名稱。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string FunctionName { get; set; }

        /// <summary>
        /// 取得或設定操作參數資料。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string HandlerParameter { get; set; }
    }
}
