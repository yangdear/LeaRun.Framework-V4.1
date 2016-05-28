using LeaRun.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace LeaRun.WebService
{
    /// <summary>
    /// Index 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class Index : System.Web.Services.WebService
    {
        Base_InterfaceManageBll base_interfacemanagebll = new Base_InterfaceManageBll();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        /// <summary>
        /// 业务接口调用
        /// </summary>
        /// <param name="Xml">XML格式</param>
        /// <param name="Token">记号</param>
        /// <returns></returns>
        [WebMethod(EnableSession = true, Description = "业务接口调用，参数 Xml:xml参数、Token：秘钥")]
        public string DynamicInvoke(string Xml, string Token)
        {
            string str = base_interfacemanagebll.Invoke(Xml, Token);
            return str;
        }
    }
}
