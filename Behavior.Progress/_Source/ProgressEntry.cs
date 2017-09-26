using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Magikarp.Platform.Behavior.Progress
{

    /// <summary>
    /// 定義進度視窗的資料物件。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public class ProgressEntry
    {
        #region -- 變數宣告 ( Declarations ) --   

        private int l_nProgress = 0;
        private string l_sDescription = string.Empty;
        private Stack<ProgressEntry> l_objChildren = new Stack<ProgressEntry>();

        #endregion

        #region -- 靜態方法 (Shared Method ) --

        /// <summary>
        /// 載入字串以建立進度訊息物件。
        /// </summary>
        /// <param name="pi_sEntry">訊息字串。</param>
        /// <returns>進度訊息物件。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public static ProgressEntry Loading(string pi_sEntry)
        {
            ProgressEntry objReturn = new ProgressEntry();

            objReturn.LoadString(pi_sEntry);

            return objReturn;
        }

        #endregion

        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 匯出為 XML 結構的字串。
        /// </summary>
        /// <returns>XML 結構的字串。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string ExportToString()
        {
            string sReturn = string.Empty;
            XElement objReturn = new XElement("ProgressEntry");
            XElement objChildren = new XElement("Children");

            objReturn.Add(new XElement("Description", this.l_sDescription));
            objReturn.Add(new XElement("Progress", this.l_nProgress));
            while (this.l_objChildren.Count > 0)
            {
                objChildren.Add(XElement.Parse(this.l_objChildren.Pop().ExportToString()));
            }
            objReturn.Add(objChildren);
            sReturn = objReturn.ToString();

            return sReturn;
        }

        /// <summary>
        /// 載入 XML 格式字串資料。
        /// </summary>
        /// <param name="pi_sEntry">XML 結構字串格式資料。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void LoadString(string pi_sEntry)
        {
            if (string.IsNullOrEmpty(pi_sEntry) == false)
            {
                XElement objXElement = XElement.Parse(pi_sEntry);
                XElement objChildren = null;

                this.l_sDescription = objXElement.Element("Description").Value;
                this.l_nProgress = (int)objXElement.Element("Progress");
                objChildren = objXElement.Element("Children");
                foreach (XElement objChild in objChildren.Elements("ProgressEntry"))
                {
                    ProgressEntry objEntry = new ProgressEntry();

                    objEntry.LoadString(objChild.ToString());
                    this.l_objChildren.Push(objEntry);
                }
            }
        }

        /// <summary>
        /// 取得次層的子項資料集合物件以提供 BarPregress 顯示細項進度。
        /// </summary>
        /// <returns>次層的子項資料集合物件。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public ProgressEntry GetNextLevelChild()
        {
            ProgressEntry objReturn = null;
            Stack<ProgressEntry> objTempStack = new Stack<ProgressEntry>();

            while (this.l_objChildren.Count > 0 && objReturn == null)
            {
                ProgressEntry objTransit = this.l_objChildren.Pop();
                objTempStack.Push(objTransit);
                if (objTransit.Progress < 100)
                {
                    objReturn = objTransit;
                }
            }

            // 將拿出來的堆疊項目倒回去。
            while (objTempStack.Count > 0)
            {
                this.l_objChildren.Push(objTempStack.Pop());
            }

            return objReturn;
        }

        /// <summary>
        /// 設定描述字串。
        /// </summary>
        /// <param name="pi_sDescription">描述字串。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void SetDescription(string pi_sDescription)
        {
            this.l_sDescription = pi_sDescription;
        }

        /// <summary>
        /// 設定進度。
        /// </summary>
        /// <param name="pi_nProgress">進度。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void SetProgress(int pi_nProgress)
        {
            this.l_nProgress = pi_nProgress;
        }

        /// <summary>
        /// 設定子項。
        /// </summary>
        /// <param name="pi_objChildren">子項。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void SetChildren(Stack<ProgressEntry> pi_objChildren)
        {
            this.l_objChildren = pi_objChildren;
        }

        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得工作描述。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string Description { get { return this.l_sDescription; } }

        /// <summary>
        /// 取得工作進度
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public int Progress { get { return this.l_nProgress; } }

        #endregion

    }
}
