namespace Magikarp.Platform.Utility.Region
{
    /// <summary>
    /// 定義內嵌區域的標介面。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170930
    /// </remarks>
    public interface IRegion
    {
        /// <summary>
        /// 新增內容到區域所屬控制項。
        /// </summary>
        /// <param name="pi_objContent">待新增至區域控制項中成為內容的控制項執行個體。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        void Add(IRegionContent pi_objContent);

    }
}