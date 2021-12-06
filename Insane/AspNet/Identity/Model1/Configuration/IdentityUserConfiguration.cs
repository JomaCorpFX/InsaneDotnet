using Insane.EntityFrameworkCore;
using Insane.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1.Configuration
{
    public class IdentityUserConfiguration<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog> : EntityTypeConfigurationBase<TUser>
       where TKey : IEquatable<TKey>
        where TUser : IdentityUserBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TRole : IdentityRoleBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TAccess : IdentityAccessBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TUserClaim : IdentityUserClaimBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TPlatform : IdentityPlatformBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TSession : IdentitySessionBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TLog : IdentityLogBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
    {
        public IdentityUserConfiguration(DatabaseFacade database, string? schema = null) : base(database, schema)
        {
        }

        public override void Configure(EntityTypeBuilder<TUser> builder)
        {
            builder.ToTable(Database, Schema);

            builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd(Database, builder, startsAt: Constants.IdentityColumnStartValue);
            builder.Ignore(e => e.UniqueId);
            builder.Property(e => e.Username).IsRequired().IsUnicode().HasMaxLength(Constants.UsernameMaxLength).IsConcurrencyToken();
            builder.Property(e => e.NormalizedUsername).IsRequired().IsUnicode().HasMaxLength(Constants.UsernameMaxLength).IsConcurrencyToken();
            builder.Property(e => e.Password).IsRequired().HasMaxLength(Constants.PasswordInfoMaxLength).IsConcurrencyToken();
            builder.Property(e => e.Email).IsRequired().IsUnicode().HasMaxLength(Constants.EmailMaxLength).IsConcurrencyToken();
            builder.Property(e => e.NormalizedEmail).IsRequired().IsUnicode().HasMaxLength(Constants.EmailMaxLength).IsConcurrencyToken();
            builder.Property(e => e.Phone).IsRequired().HasMaxLength(Constants.PhoneMaxLength);
            builder.Property(e => e.Mobile).IsRequired().HasMaxLength(Constants.PhoneMaxLength).IsConcurrencyToken();
            builder.Property(e => e.EmailConfirmed).IsRequired().IsConcurrencyToken();
            builder.Property(e => e.EmailConfirmationCode).IsRequired(false).HasMaxLength(Constants.IdentifierMaxLength).IsConcurrencyToken();
            builder.Property(e => e.EmailConfirmationDeadline).IsRequired(false).IsConcurrencyToken();
            builder.Property(e => e.MobileConfirmed).IsRequired().IsConcurrencyToken();
            builder.Property(e => e.MobileConfirmationCode).IsRequired(false).HasMaxLength(Constants.IdentifierMaxLength).IsConcurrencyToken();
            builder.Property(e => e.MobileConfirmationDeadline).IsRequired(false).IsConcurrencyToken();
            builder.Property(e => e.LoginFailCount).IsRequired().IsConcurrencyToken();
            builder.Property(e => e.LockoutUntil).IsRequired(false).IsConcurrencyToken();
            builder.Property(e => e.Summary).IsRequired(false).IsUnicode().HasMaxLength(Constants.SummaryMaxLength);
            builder.Property(e => e.ProfilePictureUri).IsRequired(false).IsUnicode().HasMaxLength(Constants.UriMaxLength);
            builder.Property(e => e.CreatedAt).IsRequired();
            builder.Property(e => e.Enabled).IsRequired().IsConcurrencyToken();
            builder.Property(e => e.TwoFactorEnabled).IsRequired().IsConcurrencyToken();
            builder.Property(e => e.TwoFactorSecretKey).IsRequired().HasMaxLength(Constants.KeyMaxLength).IsConcurrencyToken();
            builder.Property(e => e.ActiveUntil).IsRequired(false).IsConcurrencyToken();

            builder.HasPrimaryKeyIndex(Database, e => e.Id);
            builder.HasUniqueIndex(Database, e => e.Username);
            builder.HasUniqueIndex(Database, e => e.NormalizedUsername);
            builder.HasUniqueIndex(Database, e => e.Email);
            builder.HasUniqueIndex(Database, e => e.Mobile);
            builder.HasUniqueIndex(Database, e => e.Password);
        }
    }
}



//    //TODO: Implementar el método de extensión para agregar las restricciones check basado en el proveedor.
//    //TODO: Agregar las restricciones check para validar Email/NormalizedEmail, Username/NormalizedUsername. Los valores deben ser iguales en concepto pero distinto en forma
//    //ejem Username en texto normal y NormalizedUsername en mayúsculas.
//    //IDEA: Los códigos de confirmación se almacenan como hash no como el valor enviado al usuario.