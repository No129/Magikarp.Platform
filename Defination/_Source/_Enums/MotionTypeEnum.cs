using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Platform.Definition
{
    /// <summary>
    /// 列舉流程對應操作種類。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public enum MotionTypeEnum
    {
        /// <summary>
        /// 尚未定義動作。
        /// </summary>
        Undefind = 0,

        /// <summary>
        /// 顯示畫面動作。
        /// </summary>
        Show = 1,

        /// <summary>
        /// 處理資料動作。
        /// </summary>
        Process = 2
    }
}
