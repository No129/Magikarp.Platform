namespace Magikarp.Platform.Utility.Region
{
    /// <summary>
    /// 定義區域管理員功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170930
    /// </remarks>
    public interface IRegionManager
    {
        /// <summary>
        /// 取得指定名稱的區域執行個體。
        /// </summary>
        /// <param name="pi_sRegionName">區域名稱。</param>        
        /// <returns>指定名稱的區域執行個體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        IRegion Regions(string pi_sRegionName);

        /// <summary>
        /// 移除指定區域的執行個體。
        /// </summary>
        /// <param name="pi_objRegion">指定區域的執行個體。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        void Remove(IRegion pi_objRegion);
    }
}
