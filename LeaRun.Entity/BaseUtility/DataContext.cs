using LeaRun.Utilities;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace LeaRun.Entity
{
    /// <summary>
    /// 创建一个EF实体框架 上下文
    /// </summary>
    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("LeaRunFramework_SqlServer")
        {
        }

        public DbSet<Base_BackupJob> Base_BackupJob { get; set; }
        public DbSet<Base_Button> Base_Button { get; set; }
        public DbSet<Base_ButtonPermission> Base_ButtonPermission { get; set; }
        public DbSet<Base_CodeRule> Base_CodeRule { get; set; }
        public DbSet<Base_CodeRuleDetail> Base_CodeRuleDetail { get; set; }
        public DbSet<Base_Company> Base_Company { get; set; }
        public DbSet<Base_DataDictionary> Base_DataDictionary { get; set; }
        public DbSet<Base_DataDictionaryDetail> Base_DataDictionaryDetail { get; set; }
        public DbSet<Base_DataScopePermission> Base_DataScopePermission { get; set; }
        public DbSet<Base_Department> Base_Department { get; set; }
        public DbSet<Base_Email> Base_Email { get; set; }
        public DbSet<Base_EmailAccessory> Base_EmailAccessory { get; set; }
        public DbSet<Base_EmailAddressee> Base_EmailAddressee { get; set; }
        public DbSet<Base_EmailCategory> Base_EmailCategory { get; set; }
        public DbSet<Base_Employee> Base_Employee { get; set; }
        public DbSet<Base_ExcelImport> Base_ExcelImport { get; set; }
        public DbSet<Base_ExcelImportDetail> Base_ExcelImportDetail { get; set; }
        public DbSet<Base_GroupUser> Base_GroupUser { get; set; }
        public DbSet<Base_Module> Base_Module { get; set; }
        public DbSet<Base_ModulePermission> Base_ModulePermission { get; set; }
        public DbSet<Base_NetworkFile> Base_NetworkFile { get; set; }
        public DbSet<Base_NetworkFolder> Base_NetworkFolder { get; set; }
        public DbSet<Base_ObjectUserRelation> Base_ObjectUserRelation { get; set; }
        public DbSet<Base_PhoneNote> Base_PhoneNote { get; set; }
        public DbSet<Base_Post> Base_Post { get; set; }
        public DbSet<Base_ProvinceCity> Base_ProvinceCity { get; set; }
        public DbSet<Base_QueryRecord> Base_QueryRecord { get; set; }
        public DbSet<Base_Roles> Base_Roles { get; set; }
        public DbSet<Base_Shortcuts> Base_Shortcuts { get; set; }
        public DbSet<Base_SysLog> Base_SysLog { get; set; }
        public DbSet<Base_SysLogDetail> Base_SysLogDetail { get; set; }
        public DbSet<Base_User> Base_User { get; set; }
        public DbSet<Base_View> Base_View { get; set; }
        public DbSet<Base_ViewPermission> Base_ViewPermission { get; set; }
        public DbSet<Base_ViewWhere> Base_ViewWhere { get; set; }
    }
}
