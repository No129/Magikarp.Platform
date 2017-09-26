using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Definition.Environment
{
    /// <summary>
    /// 提供應用程式 app.config 所應提供的組件資訊。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public class AssemblyInfo
    {

        #region -- 變數宣告 ( Declarations ) --   

        private string m_sType = string.Empty;
        private string m_sNamespace = string.Empty;
        private string m_sFileName = string.Empty;
        private string m_sSubFolder = string.Empty;

        #endregion

        #region -- 建構/解構 ( Constructors/Destructor ) --

        /// <summary>
        /// 建構元。
        /// </summary>
        /// <param name="pi_sType">組件型別。</param>
        /// <param name="pi_sNamespace">組件名域空間。</param>
        /// <param name="pi_sFileName">組件檔名。</param>
        /// <param name="pi_sSubFolder">組件所在子檔案夾。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public AssemblyInfo(string pi_sType, string pi_sNamespace, string pi_sFileName, string pi_sSubFolder)
        {
            this.m_sType = pi_sType;
            this.m_sNamespace = pi_sNamespace;
            this.m_sFileName = pi_sFileName;
            this.m_sSubFolder = pi_sSubFolder;
        }

        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得組件類型。
        /// </summary>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string Type { get { return this.m_sType; } }

        /// <summary>
        /// 取得組件名域空間。
        /// </summary>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string Namespace { get { return this.m_sNamespace; } }

        /// <summary>
        /// 取得組件檔名。
        /// </summary>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string FileName { get { return this.m_sFileName; } }

        /// <summary>
        /// 取得組件所在子檔案夾。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string SubFolder { get { return this.m_sSubFolder; } }

        #endregion

    }
}
