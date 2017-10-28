using Magikarp.Platform.Definition;
using Magikarp.Platform.Definition.Environment;
using Magikarp.Platform.Definition.Flow;
using Magikarp.Platform.Definition.Pakage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Magikarp.Platform.Behavior
{
    /// <summary>
    /// 提供客戶外掛組件管理功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171025
    /// </remarks>
    public class AssemblyManager
    {

        #region -- 變數宣告 ( Declarations ) --   

        private static object l_objSyncObject = new object();
        private static AssemblyManager m_objAssemblyManager = null;

        private Dictionary<AssemblyRoleEnum, Dictionary<string, AssemblyInfoModel>> l_objAssemblyCollection = new Dictionary<AssemblyRoleEnum, Dictionary<string, AssemblyInfoModel>>();
        private Dictionary<string, Dictionary<string, AssemblyInfoModel>> l_objCustomAssemblyCollection = new Dictionary<string, Dictionary<string, AssemblyInfoModel>>();

        #endregion

        #region -- 建構/解構 ( Constructors/Destructor ) --

        /// <summary>
        /// 建構元。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private AssemblyManager() { }

        #endregion

        #region -- 靜態方法 (Shared Method ) --

        /// <summary>
        /// 取得獨體模式下唯一的 AssemblyManager 個體。
        /// </summary>
        /// <returns>獨體模式下唯一的 AssemblyManager 個體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public static AssemblyManager GetInstance()
        {
            if (AssemblyManager.m_objAssemblyManager == null)
            {
                lock (AssemblyManager.l_objSyncObject)
                {
                    if (AssemblyManager.m_objAssemblyManager == null)
                    {
                        AssemblyManager.m_objAssemblyManager = new AssemblyManager();
                    }
                }
            }
            return AssemblyManager.m_objAssemblyManager;
        }

        #endregion

        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 掛載組件實體。
        /// </summary>
        /// <param name="pi_sRootPath">組件所在根目錄。</param>
        /// <param name="pi_sSectionTagName">app.config 中的區段節點名稱。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void MountAssembly(string pi_sRootPath, string pi_sSectionTagName)
        {
            List<AssemblyInfo> objAssemblyInfoList = new List<AssemblyInfo>();
            string sAppPath = (pi_sRootPath.EndsWith("\\") || pi_sRootPath.EndsWith("//")) ? pi_sRootPath : pi_sRootPath + System.IO.Path.DirectorySeparatorChar;

            objAssemblyInfoList = (List<AssemblyInfo>)System.Configuration.ConfigurationManager.GetSection(pi_sSectionTagName);
            foreach (AssemblyInfo objInfo in objAssemblyInfoList)
            {
                string sSubFolder = string.IsNullOrEmpty(objInfo.SubFolder) ? objInfo.SubFolder : objInfo.SubFolder + System.IO.Path.DirectorySeparatorChar;
                string sAssemblyFullName = String.Format("{0}{1}{2}.dll", sAppPath, sSubFolder, objInfo.FileName);
                System.Reflection.Assembly objAssemblyInstance = System.Reflection.Assembly.LoadFile(sAssemblyFullName);
                AssemblyInfoModel objAssemblyInfoModel = new AssemblyInfoModel() { Instance = objAssemblyInstance, Namespace = objInfo.Namespace };

                this.MountAssembly(objInfo.Type, objAssemblyInfoModel);
            }
        }

        /// <summary>
        /// 掛載組件實體。
        /// </summary>
        /// <param name="pi_objTargetAssembly">待掛載組件實體。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/25
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void MountAssembly(System.Reflection.Assembly pi_objTargetAssembly)
        {
            object[] objAttributes = pi_objTargetAssembly.GetCustomAttributes(typeof(AssemblyRoleAttribute), false);

            if (objAttributes != null)
            {
                List<object> objAssemblyRoleInfos = objAttributes.ToList<object>();

                foreach (object objInfo in objAssemblyRoleInfos)
                {
                    AssemblyRoleAttribute objAssemblyRoleInfo = (AssemblyRoleAttribute)objInfo;
                    AssemblyInfoModel objInfoModel = new AssemblyInfoModel()
                    {
                        Namespace = objAssemblyRoleInfo.AssemblyRootNamespace,
                        Instance = pi_objTargetAssembly
                    };

                    this.MountAssembly(objAssemblyRoleInfo.AssemblyRole, objInfoModel);
                }
            }
        }

        /// <summary>
        /// 掛載組件實體。
        /// </summary>
        /// <param name="pi_objTargetAssemblys">待掛載組件實體集合。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/25
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public void MountAssembly(List<System.Reflection.Assembly> pi_objTargetAssemblys)
        {
            foreach (System.Reflection.Assembly objAssembly in pi_objTargetAssemblys)
            {
                this.MountAssembly(objAssembly);
            }
        }

        /// <summary>
        /// 掛載組件實體。
        /// </summary>
        /// <param name="pi_sAssemblyKey">掛載組件鍵值。</param>
        /// <param name="pi_objAssemblyInfo">掛載組件資料物件。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: 
        ///     配合列舉名稱調整。(黃竣祥 2017/10/25)
        /// DB Object: N/A      
        /// </remarks>
        public void MountAssembly(string pi_sAssemblyKey, AssemblyInfoModel pi_objAssemblyInfo)
        {
            AssemblyRoleEnum nAssemblyType = AssemblyRoleEnum.Central;
            this.MountTextAssembly(pi_sAssemblyKey, pi_objAssemblyInfo);    // 掛載文字鍵值的組件清單。
            if (Enum.TryParse<AssemblyRoleEnum>(pi_sAssemblyKey, out nAssemblyType))
            {
                this.MountEunmAssembly(nAssemblyType, pi_objAssemblyInfo); // 掛載列舉清單。
            }
        }

        /// <summary>
        /// 掛載組件實體。
        /// </summary>
        /// <param name="pi_nAssemblyType">掛載組件型態列舉。</param>
        /// <param name="pi_objAssemblyInfo">掛載組件資料物件。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: 
        ///     配合列舉名稱調整。(黃竣祥 2017/10/25)
        /// DB Object: N/A      
        /// </remarks>
        public void MountAssembly(AssemblyRoleEnum pi_nAssemblyType, AssemblyInfoModel pi_objAssemblyInfo)
        {
            this.MountEunmAssembly(pi_nAssemblyType, pi_objAssemblyInfo);           // 處理列舉掛載。
            this.MountTextAssembly(pi_nAssemblyType.ToString(), pi_objAssemblyInfo);// 處理文字掛載。
        }

        /// <summary>
        /// 重置組件實體。
        /// </summary>
        /// <param name="pi_sAssemblyKey">掛載組件鍵值。</param>
        /// <param name="pi_objAssemblyInfo">掛載組件資料物件。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: 
        ///     配合列舉名稱調整。(黃竣祥 2017/10/25)
        /// DB Object: N/A      
        /// </remarks>
        public void ResetAssembly(string pi_sAssemblyKey, AssemblyInfoModel pi_objAssemblyInfo)
        {
            AssemblyRoleEnum nAssemblyType = AssemblyRoleEnum.Central;
            this.ResetTextAssembly(pi_sAssemblyKey, pi_objAssemblyInfo);    // 重置文字鍵值的組件清單。
            if (Enum.TryParse<AssemblyRoleEnum>(pi_sAssemblyKey, out nAssemblyType))
            {
                this.ResetEnumAssembly(nAssemblyType, pi_objAssemblyInfo);  // 重置列舉鍵值的組件清單。
            }
        }

        /// <summary>
        /// 重置組件實體。
        /// </summary>
        /// <param name="pi_nAssemblyType">掛載組件型態列舉。</param>
        /// <param name="pi_objAssemblyInfo">掛載組件資料物件。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: 
        ///     配合列舉名稱調整。(黃竣祥 2017/10/25)
        /// DB Object: N/A      
        /// </remarks>
        public void ResetAssembly(AssemblyRoleEnum pi_nAssemblyType, AssemblyInfoModel pi_objAssemblyInfo)
        {
            this.ResetEnumAssembly(pi_nAssemblyType, pi_objAssemblyInfo);
            this.ResetTextAssembly(pi_nAssemblyType.ToString(), pi_objAssemblyInfo);
        }

        /// <summary>
        /// 卸載組件實體。
        /// </summary>
        /// <param name="pi_sAssemblyKey">卸載組件鍵值。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: 
        ///     配合列舉名稱調整。(黃竣祥 2017/10/25)
        /// DB Object: N/A      
        /// </remarks>
        public void UnmountAssembly(string pi_sAssemblyKey)
        {
            AssemblyRoleEnum nAssemblyType = AssemblyRoleEnum.Central;

            // 卸載文字組件實體。
            if (this.l_objCustomAssemblyCollection.ContainsKey(pi_sAssemblyKey))
            {
                this.l_objCustomAssemblyCollection.Remove(pi_sAssemblyKey);
            }
            // 卸載列舉組件實體。
            if (Enum.TryParse<AssemblyRoleEnum>(pi_sAssemblyKey, out nAssemblyType))
            {
                if (this.l_objAssemblyCollection.ContainsKey(nAssemblyType))
                {
                    this.l_objAssemblyCollection.Remove(nAssemblyType);
                }
            }
        }

        /// <summary>
        /// 卸載組件實體。
        /// </summary>
        /// <param name="pi_nAssemblyType">卸載組件型態列舉。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: 
        ///     配合列舉名稱調整。(黃竣祥 2017/10/25)
        /// DB Object: N/A      
        /// </remarks>
        public void UnmountAssembly(AssemblyRoleEnum pi_nAssemblyType)
        {
            if (this.l_objAssemblyCollection.ContainsKey(pi_nAssemblyType))
            {
                this.l_objAssemblyCollection.Remove(pi_nAssemblyType);
            }

            if (this.l_objCustomAssemblyCollection.ContainsKey(pi_nAssemblyType.ToString()))
            {
                this.l_objCustomAssemblyCollection.Remove(pi_nAssemblyType.ToString());
            }
        }

        /// <summary>
        /// 建立對應產品執行物件。
        /// </summary>
        /// <typeparam name="TProduct">產品型別。</typeparam>
        /// <param name="pi_sFunctionName">指定建立功能。</param>
        /// <param name="pi_sProductName">產品的後置名稱。</param>
        /// <param name="pi_sAssemblyKey">對應產品的組件型別。</param>
        /// <param name="pi_objParameter">建立物件的參數。</param>
        /// <returns>對應產品執行物件。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public TProduct CreateProduct<TProduct>(string pi_sFunctionName, string pi_sProductName, string pi_sAssemblyKey, params object[] pi_objParameter)
        {
            TProduct objReturn = default(TProduct);
            string sFunctionName = string.Empty;

            int nStep = 0;      // 程序進度指標。
            Boolean bRun = true;// 程序中斷旗標。

            while (bRun)
            {
                nStep += 1;
                switch (nStep)
                {
                    case 1:// 取得指定的建立物件名稱。
                        sFunctionName = pi_sFunctionName;
                        break;
                    case 2:// 建立回傳物件。
                    case 5:
                        string sProductName = String.Format("{0}{1}", sFunctionName, pi_sProductName);

                        if (pi_objParameter == null || pi_objParameter.Length == 0)
                        {
                            objReturn = AssemblyManager.GetInstance().CreateProduct<TProduct>(pi_sAssemblyKey, sProductName);
                        }
                        else
                        {
                            objReturn = AssemblyManager.GetInstance().CreateProduct<TProduct>(pi_sAssemblyKey, sProductName, pi_objParameter);
                        }
                        break;

                    case 3:// 判斷是否取得。
                        bRun = (objReturn == null);
                        break;
                    case 4:// 取得通用的建立物件名稱。
                        string[] objFunctionNames = sFunctionName.Split(".".ToCharArray()).Reverse().ToArray();

                        objFunctionNames[0] = "Common";
                        sFunctionName = string.Join(".", objFunctionNames.Reverse());
                        break;

                    default://結束。
                        bRun = false;
                        break;
                }
            }

            return objReturn;
        }

        /// <summary>
        /// 建立對應產品執行物件。
        /// </summary>
        /// <typeparam name="TProduct">產品型別。</typeparam>
        /// <param name="pi_sFunctionName">指定建立功能。</param>
        /// <param name="pi_sProductName">產品的後置名稱。</param>
        /// <param name="pi_nAssemblyType">對應產品的組件型別。</param>
        /// <param name="pi_objParameter">建立物件的參數。</param>
        /// <returns>對應產品執行物件。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: 
        ///     配合列舉名稱調整。(黃竣祥 2017/10/25)
        /// DB Object: N/A      
        /// </remarks>
        public TProduct CreateProduct<TProduct>(string pi_sFunctionName, string pi_sProductName, AssemblyRoleEnum pi_nAssemblyType, params object[] pi_objParameter)
        {
            TProduct objReturn = default(TProduct);
            string sFunctionName = string.Empty;

            int nStep = 0;//程序進度指標。
            Boolean bRun = true;//程序中斷旗標。

            while (bRun)
            {
                nStep += 1;
                switch (nStep)
                {
                    case 1:// 取得指定的建立物件名稱。
                        sFunctionName = pi_sFunctionName;
                        break;

                    case 2:// 建立回傳物件。
                    case 5:
                        string sProductName = String.Format("{0}{1}", sFunctionName, pi_sProductName);
                        if (pi_objParameter == null || pi_objParameter.Length == 0)
                        {
                            objReturn = AssemblyManager.GetInstance().CreateProduct<TProduct>(pi_nAssemblyType, sProductName);
                        }
                        else
                        {
                            objReturn = AssemblyManager.GetInstance().CreateProduct<TProduct>(pi_nAssemblyType, sProductName, pi_objParameter);
                        }
                        break;

                    case 3:// 判斷是否取得。
                        bRun = (objReturn == null);
                        break;

                    case 4:// 取得通用的建立物件名稱。
                        string[] objFunctionNames = sFunctionName.Split(".".ToCharArray()).Reverse().ToArray();

                        objFunctionNames[0] = "Common";
                        sFunctionName = string.Join(".", objFunctionNames.Reverse());
                        break;

                    default://結束。
                        bRun = false;
                        break;
                }
            }

            return objReturn;
        }

        /// <summary>
        /// 提供特定類別的組件資料集合。
        /// </summary>
        /// <param name="pi_nAssemblyType">指定組件所屬類別。</param>
        /// <returns>特定類別的組件資料集合。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/30
        /// History: 
        ///     配合列舉名稱調整。(黃竣祥 2017/10/25)
        /// DB Object: N/A      
        /// </remarks>
        public List<AssemblyInfoModel> FindAssemblyInfoModels(AssemblyRoleEnum pi_nAssemblyType)
        {
            List<AssemblyInfoModel> objReturn =
                (from KeyValuePair<string, AssemblyInfoModel> objPackage in this.l_objAssemblyCollection[pi_nAssemblyType]
                 select objPackage.Value).ToList();

            return objReturn;
        }

        #endregion       

        #region -- 私有函式 ( Private Method) --

        /// <summary>
        /// 文字鍵值的組件清單。
        /// </summary>
        /// <param name="pi_sAssemblyKey">掛載組件鍵值。</param>
        /// <param name="pi_objAssemblyInfo">掛載組件資料物件。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private void MountTextAssembly(string pi_sAssemblyKey, AssemblyInfoModel pi_objAssemblyInfo)
        {
            if (this.l_objCustomAssemblyCollection.ContainsKey(pi_sAssemblyKey) == false)
            {
                Dictionary<string, AssemblyInfoModel> objAssemblyInfos = new Dictionary<string, AssemblyInfoModel>();

                objAssemblyInfos.Add(pi_objAssemblyInfo.Namespace, pi_objAssemblyInfo);
                this.l_objCustomAssemblyCollection.Add(pi_sAssemblyKey, objAssemblyInfos);
            }
            else
            {
                Dictionary<string, AssemblyInfoModel> objAssemblyInfos = this.l_objCustomAssemblyCollection[pi_sAssemblyKey];

                if (objAssemblyInfos.ContainsKey(pi_objAssemblyInfo.Namespace) == false)
                {
                    objAssemblyInfos.Add(pi_objAssemblyInfo.Namespace, pi_objAssemblyInfo);
                    this.l_objCustomAssemblyCollection[pi_sAssemblyKey] = objAssemblyInfos;
                }
            }
        }

        /// <summary>
        /// 列舉項的組件清單。
        /// </summary>
        /// <param name="pi_nAssemblyType">掛載組件鍵值。</param>
        /// <param name="pi_objAssemblyInfo">掛載組件資料物件。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: 
        ///     配合列舉名稱調整。(黃竣祥 2017/10/25)
        /// DB Object: N/A      
        /// </remarks>
        private void MountEunmAssembly(AssemblyRoleEnum pi_nAssemblyType, AssemblyInfoModel pi_objAssemblyInfo)
        {
            if (this.l_objAssemblyCollection.ContainsKey(pi_nAssemblyType) == false)
            {
                Dictionary<string, AssemblyInfoModel> objAssemblyInfos = new Dictionary<string, AssemblyInfoModel>();

                objAssemblyInfos.Add(pi_objAssemblyInfo.Namespace, pi_objAssemblyInfo);
                this.l_objAssemblyCollection.Add(pi_nAssemblyType, objAssemblyInfos);
            }
            else
            {
                Dictionary<string, AssemblyInfoModel> objAssemblyInfos = this.l_objAssemblyCollection[pi_nAssemblyType];

                if (objAssemblyInfos.ContainsKey(pi_objAssemblyInfo.Namespace) == false)
                {
                    objAssemblyInfos.Add(pi_objAssemblyInfo.Namespace, pi_objAssemblyInfo);
                    this.l_objAssemblyCollection[pi_nAssemblyType] = objAssemblyInfos;
                }
            }
        }

        /// <summary>
        /// 重置文字鍵值的組件清單。
        /// </summary>
        /// <param name="pi_sAssemblyKey">掛載組件鍵值。</param>
        /// <param name="pi_objAssemblyInfo">掛載組件資料物件。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private void ResetTextAssembly(string pi_sAssemblyKey, AssemblyInfoModel pi_objAssemblyInfo)
        {
            if (this.l_objCustomAssemblyCollection.ContainsKey(pi_sAssemblyKey) == false)
            {
                this.MountTextAssembly(pi_sAssemblyKey, pi_objAssemblyInfo);
            }
            else
            {
                Dictionary<string, AssemblyInfoModel> objAssemblyInfos = this.l_objCustomAssemblyCollection[pi_sAssemblyKey];

                if (objAssemblyInfos.ContainsKey(pi_objAssemblyInfo.Namespace))
                {
                    objAssemblyInfos[pi_objAssemblyInfo.Namespace] = pi_objAssemblyInfo;
                }
                else
                {
                    objAssemblyInfos.Add(pi_objAssemblyInfo.Namespace, pi_objAssemblyInfo);
                }
                this.l_objCustomAssemblyCollection[pi_sAssemblyKey] = objAssemblyInfos;
            }
        }

        /// <summary>
        /// 重置列舉項鍵值的組件清單。。
        /// </summary>
        /// <param name="pi_nAssemblyType">掛載組件鍵值。</param>
        /// <param name="pi_objAssemblyInfo">掛載組件資料物件。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: 
        ///     配合列舉名稱調整。(黃竣祥 2017/10/25)
        /// DB Object: N/A      
        /// </remarks>
        private void ResetEnumAssembly(AssemblyRoleEnum pi_nAssemblyType, AssemblyInfoModel pi_objAssemblyInfo)
        {
            if (this.l_objAssemblyCollection.ContainsKey(pi_nAssemblyType) == false)
            {
                this.MountEunmAssembly(pi_nAssemblyType, pi_objAssemblyInfo);
            }
            else
            {
                Dictionary<string, AssemblyInfoModel> objAssemblyInfos = this.l_objAssemblyCollection[pi_nAssemblyType];

                if (objAssemblyInfos.ContainsKey(pi_objAssemblyInfo.Namespace))
                {
                    objAssemblyInfos[pi_objAssemblyInfo.Namespace] = pi_objAssemblyInfo;
                }
                else
                {
                    objAssemblyInfos.Add(pi_objAssemblyInfo.Namespace, pi_objAssemblyInfo);
                }
                this.l_objAssemblyCollection[pi_nAssemblyType] = objAssemblyInfos;
            }
        }

        /// <summary>
        /// 依據傳入之 Product 名稱，傳回對應 Product 類別之執行個體。
        /// </summary>
        /// <typeparam name="TProduct">指定建立型別。</typeparam>
        /// <param name="pi_sAssemblyType">指定所要產生 Product 類別之執行個體所在 Assembly 執行個體。</param>
        /// <param name="pi_sFunctionName">完整名稱。</param>
        /// <returns>對應 Product 類別之執行個體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private TProduct CreateProduct<TProduct>(string pi_sAssemblyType, string pi_sFunctionName)
        {
            return this.CreateInstance<TProduct>(pi_sAssemblyType, pi_sFunctionName, null);
        }

        /// <summary>
        /// 依據傳入之 Product 名稱，傳回對應 Product 類別之執行個體。
        /// </summary>
        /// <typeparam name="TProduct">指定建立型別。</typeparam>
        /// <param name="pi_sAssemblyType">指定所要產生 Product 類別之執行個體所在 Assembly 執行個體。</param>
        /// <param name="pi_sFunctionName">完整名稱。</param>
        /// <param name="pi_objParameters">實體建構參數。</param>
        /// <returns>對應 Product 類別之執行個體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private TProduct CreateProduct<TProduct>(string pi_sAssemblyType, string pi_sFunctionName, params object[] pi_objParameters)
        {
            return this.CreateInstance<TProduct>(pi_sAssemblyType, pi_sFunctionName, pi_objParameters);
        }

        /// <summary>
        /// 依據傳入之 Product 名稱，傳回對應 Product 類別之執行個體。
        /// </summary>
        /// <typeparam name="TProduct">指定建立的型別。</typeparam>
        /// <param name="pi_nAssemblyType">指定所要產生 Product 類別之執行個體所在 Assembly 執行個體。</param>
        /// <param name="pi_sFunctionName">實體完整名稱。</param>
        /// <returns>指定型別的實體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: 
        ///     配合列舉名稱調整。(黃竣祥 2017/10/25)
        /// DB Object: N/A      
        /// </remarks>
        private TProduct CreateProduct<TProduct>(AssemblyRoleEnum pi_nAssemblyType, string pi_sFunctionName)
        {
            return this.CreateInstance<TProduct>(pi_nAssemblyType, pi_sFunctionName, null);
        }

        /// <summary>
        /// 依據傳入之 Product 名稱，傳回對應 Product 類別之執行個體。
        /// </summary>
        /// <typeparam name="TProduct">指定建立的型別。</typeparam>
        /// <param name="pi_nAssemblyType">指定所要產生 Product 類別之執行個體所在 Assembly 執行個體。</param>
        /// <param name="pi_sFunctionName">實體完整名稱。</param>
        /// <param name="pi_objParameters">實體建構參數。</param>
        /// <returns>指定型別的實體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: 
        ///     配合列舉名稱調整。(黃竣祥 2017/10/25)
        /// DB Object: N/A      
        /// </remarks>
        private TProduct CreateProduct<TProduct>(AssemblyRoleEnum pi_nAssemblyType, string pi_sFunctionName, params object[] pi_objParameters)
        {
            return this.CreateInstance<TProduct>(pi_nAssemblyType, pi_sFunctionName, pi_objParameters);
        }

        /// <summary>
        /// 建立指定型別的實體
        /// </summary>
        /// <typeparam name="TProduct">指定建立的型別。</typeparam>
        /// <param name="pi_sAssemblyType">指定組件列舉。</param>
        /// <param name="pi_sFunctionName">實體完整名稱。</param>
        /// <param name="pi_objParameters">實體建構參數。</param>
        /// <returns>指定型別的實體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private TProduct CreateInstance<TProduct>(string pi_sAssemblyType, string pi_sFunctionName, params object[] pi_objParameters)
        {
            TProduct objReturn = default(TProduct);

            if (this.l_objCustomAssemblyCollection.ContainsKey(pi_sAssemblyType))
            {
                Dictionary<string, AssemblyInfoModel> objAssemblyInfos = null;

                objAssemblyInfos = this.l_objCustomAssemblyCollection[pi_sAssemblyType];
                objReturn = this.CreateInstance<TProduct>(objAssemblyInfos, pi_sFunctionName, pi_objParameters);
            }
            return objReturn;
        }

        /// <summary>
        /// 建立指定型別的實體。
        /// </summary>
        /// <typeparam name="TProduct">指定建立的型別。</typeparam>
        /// <param name="pi_nAssemblyType">指定組件列舉。</param>
        /// <param name="pi_sFunctionName">實體完整名稱。</param>
        /// <param name="pi_objParameters">實體建構參數。</param>
        /// <returns>指定型別的實體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: 
        ///     配合列舉名稱調整。(黃竣祥 2017/10/25)
        /// DB Object: N/A      
        /// </remarks>
        private TProduct CreateInstance<TProduct>(AssemblyRoleEnum pi_nAssemblyType, string pi_sFunctionName, params object[] pi_objParameters)
        {
            TProduct objReturn = default(TProduct);

            if (this.l_objAssemblyCollection.ContainsKey(pi_nAssemblyType))
            {
                Dictionary<string, AssemblyInfoModel> objAssemblyInfos = null;

                objAssemblyInfos = this.l_objAssemblyCollection[pi_nAssemblyType];
                objReturn = this.CreateInstance<TProduct>(objAssemblyInfos, pi_sFunctionName, pi_objParameters);
            }
            return objReturn;
        }

        /// <summary>
        /// 建立指定型別的實體。
        /// </summary>
        /// <typeparam name="TProduct">指定建立的型別。</typeparam>
        /// <param name="pi_objAssemblyInfos">組件資料物件集合。</param>
        /// <param name="pi_sFunctionName">實體完整名稱。</param>
        /// <param name="pi_objParameters">實體建構參數。</param>
        /// <returns>指定型別的實體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private TProduct CreateInstance<TProduct>(Dictionary<string, AssemblyInfoModel> pi_objAssemblyInfos, string pi_sFunctionName, object[] pi_objParameters)
        {
            TProduct objReturn = default(TProduct);
            List<AssemblyInfoModel> objQuery = null;
            List<TProduct> objProductList = new List<TProduct>();
            List<string> objProductionNames = new List<string>() { pi_sFunctionName };

            int nStep = 0;//程序進度指標。
            Boolean bRun = true;//程序中斷旗標。

            while (bRun)
            {
                nStep += 1;
                switch (nStep)
                {
                    case 1:// 判斷是否具備對應組件資料物件。
                        bRun = (pi_objAssemblyInfos != null);
                        break;

                    case 2:// 取得具備組件實體的集合。
                        objQuery = (from KeyValuePair<string, AssemblyInfoModel> objAssemblyInfo in pi_objAssemblyInfos
                                    where objAssemblyInfo.Value.Instance != null
                                    orderby objAssemblyInfo.Value.Namespace
                                    select objAssemblyInfo.Value).ToList();

                        break;

                    case 3:// 判斷是否有對應的組件實體可供建立類別。
                        bRun = ((objQuery != null) && (objQuery.Any()));
                        break;

                    case 4:// 取得 OperatorMask 設定的 FunctionName 符合的項目。
                        foreach (AssemblyInfoModel objAssemblyInfo in objQuery)
                        {
                            IEnumerable<Type> objTypes =
                                from Type objType in objAssemblyInfo.Instance.GetTypes()
                                where (objType.IsClass == true)
                                    && (objType.IsAbstract == false)
                                    && (objType.GetInterface(typeof(TProduct).FullName) == null)
                                select objType;

                            if (objTypes.Any())
                            {
                                foreach (Type objType in objTypes)
                                {
                                    object[] objMasks = objType.GetCustomAttributes(typeof(OperatorMaskAttribute), false);
                                    IEnumerable<OperatorMaskAttribute> objQueryMasks =
                                        from OperatorMaskAttribute objMask in objMasks
                                        where objMask.FunctionName == pi_sFunctionName
                                        select objMask;

                                    if (objQueryMasks.Any())
                                    {
                                        objProductionNames.Add(objType.FullName.ToString().Substring(objAssemblyInfo.Namespace.Length + 1));
                                    }
                                }
                            }
                        }
                        break;

                    case 5:// 動態建立指定名稱物件。
                        foreach (string sProductionName in objProductionNames)
                        {
                            foreach (AssemblyInfoModel objAssemblyInfo in objQuery)
                            {
                                object objProduct = null;
                                System.Reflection.Assembly objAssembly = objAssemblyInfo.Instance;
                                string sNamespace = string.IsNullOrEmpty(objAssemblyInfo.Namespace) ? objAssemblyInfo.Namespace : objAssemblyInfo.Namespace + ".";
                                string sFullName = String.Format("{0}{1}", sNamespace, sProductionName);

                                if (pi_objParameters == null || pi_objParameters.Length == 0)
                                {
                                    objProduct = objAssembly.CreateInstance(sFullName);
                                }
                                else
                                {
                                    objProduct = objAssembly.CreateInstance(sFullName, false, System.Reflection.BindingFlags.Default, null, pi_objParameters, null, null);
                                }

                                if ((objProduct != null) && (objProduct is TProduct))
                                {
                                    if (objReturn == null && objProduct is IPluginContainer<TProduct>)
                                    {
                                        // 若是尚未有外掛容器的產品且回傳物件尚未設定時，指定給回傳物件。
                                        objReturn = (TProduct)objProduct;
                                    }
                                    else
                                    {
                                        // 加入產品清單。
                                        objProductList.Add((TProduct)objProduct);
                                    }
                                }
                            }
                        }
                        break;

                    case 6:// 回傳物件是否具備。
                        if (objReturn == null && objProductList.Count > 0)
                        {
                            // 若是未取得回傳物件且產品清單不只一個，就取第一個回傳。
                            objReturn = objProductList[0];
                        }
                        else if (objProductList.Count > 0)
                        {
                            // 產品清單不只一個(具備回傳物件)，將產品逐一塞入回傳物件，作為外掛。
                            foreach (TProduct objInstance in objProductList)
                            {
                                ((IPluginContainer<TProduct>)objReturn).AddPluginInstance(objInstance);
                            }
                        }
                        break;

                    default:// 結束。
                        bRun = false;
                        break;
                }
            }

            return objReturn;
        }

        #endregion
    }
}
