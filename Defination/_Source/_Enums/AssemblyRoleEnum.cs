using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Definition
{
    /// <summary>
    /// 列舉受控組件角色。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171025
    /// </remarks>
    public enum AssemblyRoleEnum
    {
        /// <summary>
        /// 系統中控組件。
        /// </summary>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        Central,

        /// <summary>
        /// 商業邏輯組件。
        /// </summary>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        Business,

        /// <summary>
        /// 使用者介面組件。
        /// </summary>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        View,

        /// <summary>
        /// 報表操作組件。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        Report
    }
}
