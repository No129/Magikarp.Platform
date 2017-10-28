using Magikarp.Utility.TransitData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Magikarp.Platform.Definition
{
    /// <summary>
    /// 定義主畫面建立資訊。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171025
    /// </remarks>
    public class MainViewDTO
    {

        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 添加功能入口資訊。
        /// </summary>
        /// <param name="pi_objFunctionEntryModel">功能入口資訊。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/25
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void AddFunctioinEntryModel(FunctionEntryModel pi_objFunctionEntryModel)
        {
            if (this.FunctionEntryModels == null)
            {
                this.FunctionEntryModels = new List<XElement>();
            }
            this.FunctionEntryModels.Add(XElement.Parse(new TransitDataAdapter().Export<FunctionEntryModel>(pi_objFunctionEntryModel)));
        }

        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得或設定功能進入點資訊模組清單。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/20
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public List<XElement> FunctionEntryModels { get; private set; }

        #endregion    

    }
}
