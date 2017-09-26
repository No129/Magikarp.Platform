using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Definition
{

    /// <summary>
    /// 定義控制功能操作介面。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public interface IController
    {
        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 執行命令。
        /// </summary>
        /// <param name="pi_sCommand">待執行命令。</param>
        /// <returns>命令執行結果。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        string DispatchCommand(string pi_sCommand);

        /// <summary>
        /// 執行命令。
        /// </summary>
        /// <param name="pi_sCommand">待執行命令。</param>
        /// <param name="pi_sParameter">命令參數。</param>
        /// <returns>命令執行結果。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        string DispatchCommand(string pi_sCommand, string pi_sParameter);

        #endregion
    }
}
