using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

namespace LeaRun.Resource
{
    public class ResourceFactory
    {
        public static ResourceAccess GetResource(ResourceManager resMgr)
        {
            ResourceManager rm = new ResourceManager("LeaRun.Resource.CommonResource", typeof(ResourceFactory).Assembly);
            return new ResourceAccess(resMgr, rm);
        }
    }
}
