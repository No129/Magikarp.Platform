using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Definition.Flow
{
    /// <summary>
    /// 定義角色遮罩屬性，做為 Handler 建立指定對應角色時的依據。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>    
    [AttributeUsage(AttributeTargets.Class)]
    public class OperatorMaskAttribute : Attribute
    {

        #region -- 變數宣告 ( Declarations ) --   

        private string m_sFunctionName = string.Empty;

        #endregion

        #region -- 建構/解構 ( Constructors/Destructor ) --

        /// <summary>
        /// 建構元。
        /// </summary>
        /// <param name="pi_sFunctionName">等價 FunctionName 。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public OperatorMaskAttribute(string pi_sFunctionName)
        {
            this.m_sFunctionName = pi_sFunctionName;
        }

        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得功能函式名稱。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string FunctionName { get { return this.m_sFunctionName; } }

        #endregion

    }
}
