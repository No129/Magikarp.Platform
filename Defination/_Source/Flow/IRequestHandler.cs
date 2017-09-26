using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Definition.Flow
{
    /// <summary>
    /// 定義需求處理功能操作介面。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public interface IRequestHandler
    {

        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 取回處理結果。
        /// </summary>
        /// <param name="pi_sRequest">需求描述。</param>
        /// <returns>處理結果。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        string GetResponse(string pi_sRequest);

        #endregion

    }
}
