using Magikarp.Platform.Definition.Flow;

namespace Magikarp.Platform.Behavior.Flow
{
    /// <summary>
    /// 提供 Handler 共同操作功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Time: 2017/09/26
    /// History: N/A
    /// DB Object: N/A      
    /// </remarks>
    abstract public class BaseHandler : IRequestHandler
    {

        #region -- 抽象函式 ( Abstract Method ) --        

        /// <summary>
        /// 取回要求處理結果。
        /// </summary>
        /// <param name="pi_sRequest">待處理要求。</param>
        /// <returns>要求處理結果。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public abstract string GetResponse(string pi_sRequest);

        #endregion

    }
}
