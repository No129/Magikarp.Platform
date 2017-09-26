namespace Magikarp.Platform.Definition
{
    /// <summary>
    /// 定義外掛容器物件操作介面。
    /// </summary>
    /// <typeparam name="TPluginContainer">外掛容器型別。</typeparam>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public interface IPluginContainer<TPluginContainer>
    {

        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 加上外掛實體。
        /// </summary>
        /// <param name="pi_objPluginInstance">外掛實體。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        void AddPluginInstance(TPluginContainer pi_objPluginInstance);

        #endregion

    }
}
