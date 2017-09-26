using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Definition.MVP
{
    /// <summary>
    /// 定義進度視窗能力功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public interface IAbilityOfProgress
    {
        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 取得進度視窗種類。
        /// </summary>
        /// <param name="pi_sOperation">操作命令。</param>
        /// <returns>進度視窗種類。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        ProgressLevelEnum GetProgressType(string pi_sOperation);

        #endregion
    }
}
