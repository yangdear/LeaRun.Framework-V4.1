using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace LeaRun.Resource
{
    public class ResourceAccess
    {
        private ResourceManager resourceManager = null;
        private ResourceManager commonResourceManager = null;
        public ResourceAccess(ResourceManager resourceManager, ResourceManager commonResourceManager)
        {
            this.resourceManager = resourceManager;
            this.commonResourceManager = commonResourceManager;
        }
        public string GetString(string name)
        {
            string str = this.resourceManager.GetString(name);
            if (string.IsNullOrEmpty(str))
            {
                str = this.commonResourceManager.GetString(name);
                if (string.IsNullOrEmpty(str))
                {
                    str = string.Format("【{0}】not exist", name);
                }
            }
            return str;
        }
    }
}
