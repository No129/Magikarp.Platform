namespace Magikarp.Platform.Utility.Region
{
    /// <summary>
    /// 定義區域內容物件屬性及操作。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170930
    /// </remarks>
    public interface IRegionContent
    {
        /// <summary>
        /// 取得內容控制項。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        System.Windows.Controls.Control Content { get; }

    }
}
