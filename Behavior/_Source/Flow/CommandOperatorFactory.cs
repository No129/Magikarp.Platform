using Magikarp.Platform.Definition.Flow;

namespace Magikarp.Platform.Behavior.Flow
{
    /// <summary>
    /// 提供建立命令執行器功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public class CommandOperatorFactory
    {
        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 產生命令執行器。
        /// </summary>
        /// <param name="pi_sCommand">待執行命令。</param>
        /// <returns>命令執行器實體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public virtual ICommandOperator Product(string pi_sCommand)
        {            
            return new CommandOperator(pi_sCommand);
        }

        #endregion
        
    }
}
