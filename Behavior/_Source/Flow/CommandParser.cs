using Magikarp.Platform.Definition;
using System;

namespace Magikarp.Platform.Behavior.Flow
{
    /// <summary>
    /// 提供命令解析功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20170926
    /// </remarks>
    public class CommandParser
    {
        #region -- 建構/解構 ( Constructors/Destructor ) --

        /// <summary>
        /// 建構元。
        /// </summary>
        /// <param name="pi_sCommand">待解析命令字串。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public CommandParser(string pi_sCommand)
        {
            this.ParseCommand(pi_sCommand);
        }

        #endregion
        
        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得或設定命令字串中包含的對應處理器名稱。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string HandlerType { get; set; }

        /// <summary>
        /// 取得或設定命令字串中包含的對應功能名稱。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string FunctionName { get; set; }

        #endregion        

        #region -- 私有函式 ( Private Method) --

        /// <summary>
        /// 解析命令字串。
        /// </summary>
        /// <param name="pi_sCommand">待解析命令字串。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/09/26
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private void ParseCommand(string pi_sCommand)
        {
            string[] objCommands = null;

            int nStep = 0;//程序進度指標。
            Boolean bRun = true;//程序中斷旗標。

            while (bRun)
            {
                nStep += 1;
                switch (nStep)
                {
                    case 1:// 分解傳入命令字串。
                        objCommands = pi_sCommand.Split("_".ToCharArray());
                        break;

                    case 2:// 判斷是否具備對應的功能名稱。
                        bRun = (objCommands.Length > 1);
                        break;

                    case 3:// 取得對應的 Handler 名稱。                        
                        this.HandlerType = objCommands[0].ToString();
                        foreach (MotionTypeEnum nMotionType in Enum.GetValues(typeof(MotionTypeEnum)))
                        {
                            if (nMotionType.ToString().ToUpper() == objCommands[0].ToUpper())
                            {
                                this.HandlerType = Enum.GetName(typeof(HandlerTypeEnum), (HandlerTypeEnum)(nMotionType));
                            }
                        }
                        break;

                    case 4:// 取得對應的功能名稱。
                        for (int nIndex = 1; nIndex <= (objCommands.Length - 1); nIndex++)
                        {
                            this.FunctionName =
                                string.IsNullOrEmpty(this.FunctionName) ?
                                objCommands[nIndex] :
                                String.Format("{0}.{1}", this.FunctionName, objCommands[nIndex]);
                        }
                        break;

                    default://結束。
                        bRun = false;
                        break;
                }
            }
        }

        #endregion        
    }
}
