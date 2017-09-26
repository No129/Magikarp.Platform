using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Definition.MVP
{
    /// <summary>
    /// 定義 MVP 架構中的 View 角色的更新功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public interface IAbilityOfRefresh
    {

        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 刷新畫面。
        /// </summary>
        /// <param name="pi_sParameter">刷新參數。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        void RefreshView(string pi_sParameter);

        #endregion

    }
}
