//=====================================================================================
// All Rights Reserved , Copyright @ Learun 2014
// Software Developers @ Learun 2014
//=====================================================================================

using LeaRun.Entity;
using LeaRun.Repository;
using LeaRun.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace LeaRun.Business
{
    /// <summary>
    /// ƒ£øÈ…Ë÷√
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.06.22 19:35</date>
    /// </author>
    /// </summary>
    public class Base_ModuleBll : RepositoryFactory<Base_Module>
    {
        public List<Base_Module> GetList()
        {
            return this.Repository().FindList("ORDER BY ParentId ASC,SortCode ASC");
        }
    }
}