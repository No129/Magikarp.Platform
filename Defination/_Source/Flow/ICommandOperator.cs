using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Definition.Flow
{
    /// <summary>
    /// 定義命令執行功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public interface ICommandOperator
    {

        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 執行命令。
        /// </summary>
        /// <param name="pi_sParameter">執行參數。</param>
        /// <returns>執行結果。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        string Execute(string pi_sParameter);

        #endregion

    }
}
