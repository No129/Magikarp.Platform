using System;

namespace Magikarp.Platform.Definition.Pakage
{
    /// <summary>
    /// 提供設定組件角色。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171025
    /// </remarks>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class AssemblyRoleAttribute : Attribute
    {

        #region -- 建構/解構 ( Constructors/Destructor ) --

        /// <summary>
        /// 建構元。
        /// </summary>
        /// <param name="pi_nAssemblyRole">組件角色。</param>
        /// <param name="pi_sAssemblyRootNamespace">組件根路徑。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/25
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public AssemblyRoleAttribute(AssemblyRoleEnum pi_nAssemblyRole, string pi_sAssemblyRootNamespace)
        {
            this.AssemblyRole = pi_nAssemblyRole;
            this.AssemblyRootNamespace = pi_sAssemblyRootNamespace;
        }

        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得或變定組件角色。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/25
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public AssemblyRoleEnum AssemblyRole { get; set; }

        /// <summary>
        /// 取得或變定組件根路徑。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/25
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string AssemblyRootNamespace { get; set; }

        #endregion

    }
}
