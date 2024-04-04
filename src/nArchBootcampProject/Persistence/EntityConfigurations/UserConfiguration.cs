using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Security.Hashing;

namespace Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.Property(u => u.Id).HasColumnName("Id").IsRequired();

        builder.Property(x => x.FirstName).HasColumnName("FirstName").IsRequired();
        builder.Property(x => x.LastName).HasColumnName("LastName").IsRequired();
        builder.Property(x => x.UserName).HasColumnName("UserName").IsRequired();
        builder.Property(x => x.DateOfBirth).HasColumnName("DateOfBirth").IsRequired();
        builder.Property(x => x.NationalIdentity).HasColumnName("NationalIdentity").IsRequired();

        builder.Property(u => u.Email).HasColumnName("Email").IsRequired();
        builder.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt").IsRequired();
        builder.Property(u => u.PasswordHash).HasColumnName("PasswordHash").IsRequired();
        builder.Property(u => u.AuthenticatorType).HasColumnName("AuthenticatorType").IsRequired();
        builder.Property(u => u.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(u => u.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(u => u.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(u => !u.DeletedDate.HasValue);

        builder.HasMany(u => u.UserOperationClaims);
        builder.HasMany(u => u.RefreshTokens);
        builder.HasMany(u => u.EmailAuthenticators);
        builder.HasMany(u => u.OtpAuthenticators);

        builder.HasData(_seeds);

        builder.HasBaseType((string)null!);
    }

    public static Guid AdminId { get; } = Guid.NewGuid();
    private IEnumerable<User> _seeds
    {
        get
        {
            HashingHelper.CreatePasswordHash(
                password: "12345",
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            User adminUser =
                new()
                {
                    Id = AdminId,
                    FirstName = "Ege umut",
                    LastName = "Tali",
                    UserName = "EgeUmut",
                    NationalIdentity = "TC12312",
                    DateOfBirth = DateTime.Now,
                    Email = "ege@ege.com",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };
            yield return adminUser;
        }
    }
}
