using eVault.Infrastructure.Entities;

namespace eVault.Infrastructure.Resources
{
    public static class DatabaseResources
    {
        #region Interceptors
        public static List<string> IgnoredAuditProperties = new List<string>()
                    {
                        nameof(IAuditableEntity.Id),
                        nameof(IAuditableEntity.CreatedById),
                        nameof(IAuditableEntity.CreatedOn),
                        nameof(IAuditableEntity.UpdatedById),
                        nameof(IAuditableEntity.UpdatedOn),
                        nameof(IAuditableEntity.IsActive),
                    };
        #endregion
    }
}
