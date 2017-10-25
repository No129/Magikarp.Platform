using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Definition.Pakage
{
    /// <summary>
    /// 提供工具函數設定功能啟動設定。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171025
    /// </remarks>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class FunctionEntryAttribute : Attribute
    {
        #region -- 建構/解構 ( Constructors/Destructor ) --

        /// <summary>
        /// 建構元。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/25
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public FunctionEntryAttribute() : this("功能名稱", "功能說明文字。", string.Empty, string.Empty) { }

        /// <summary>
        /// 建構元。
        /// </summary>
        /// <param name="pi_sFunctionTitle">功能名稱抬頭。</param>
        /// <param name="pi_sFunctionDescription">功能描述文字。</param>
        /// <param name="pi_sFunctionCommand">功能啟動命令。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/25
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public FunctionEntryAttribute(string pi_sFunctionTitle, string pi_sFunctionDescription, string pi_sFunctionCommand) : this(pi_sFunctionTitle, pi_sFunctionDescription, pi_sFunctionCommand, string.Empty) { }

        /// <summary>
        /// 建構元。
        /// </summary>
        /// <param name="pi_sFunctionTitle">功能名稱抬頭。</param>
        /// <param name="pi_sFunctionDescription">功能描述文字。</param>
        /// <param name="pi_sFunctionCommand">功能啟動命令。</param>
        /// <param name="pi_sFunctionImagePath">功能圖樣路徑。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/25
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public FunctionEntryAttribute(string pi_sFunctionTitle, string pi_sFunctionDescription, string pi_sFunctionCommand, string pi_sFunctionImagePath)
        {
            this.EntryModel = new FunctionEntryModel()
            {
                FunctionTitle = pi_sFunctionTitle,
                FunctionDescription = pi_sFunctionDescription,
                FunctionCommand = pi_sFunctionCommand,
                FunctionImagePath = pi_sFunctionImagePath
            };
        }

        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得及設定功能入口資訊。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/25
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public FunctionEntryModel EntryModel { get; set; }

        #endregion

    }
}
