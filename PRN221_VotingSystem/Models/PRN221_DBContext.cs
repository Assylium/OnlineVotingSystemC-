using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PRN221_VotingSystem.Models
{
    public partial class PRN221_DBContext : DbContext
    {
        public PRN221_DBContext()
        {
        }

        public PRN221_DBContext(DbContextOptions<PRN221_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Campaign> Campaigns { get; set; } = null!;
        public virtual DbSet<Campaignaccount> Campaignaccounts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Like> Likes { get; set; } = null!;
        public virtual DbSet<Mod> Mods { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Otp> Otps { get; set; } = null!;
        public virtual DbSet<Pinned> Pinneds { get; set; } = null!;
        public virtual DbSet<Thread> Threads { get; set; } = null!;
        public virtual DbSet<Vote> Votes { get; set; } = null!;
        public virtual DbSet<Votingobject> Votingobjects { get; set; } = null!;
        public virtual DbSet<Votingrule> Votingrules { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyStockDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__account__1788CC4C56369957");

                entity.ToTable("account");

                entity.HasIndex(e => e.Email, "UQ_Email")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Job)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.ToTable("campaign");

                entity.Property(e => e.CampaignDescription)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.CampaignImg)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CampaignName)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.IsPublic).HasColumnName("isPublic");

                entity.Property(e => e.Password)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.CampaignCategoryNavigation)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.CampaignCategory)
                    .HasConstraintName("FK_campaign_Category");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__campaign__Create__29572725");

                entity.HasOne(d => d.VotingRule)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.VotingRuleId)
                    .HasConstraintName("FK__campaign__Voting__2A4B4B5E");
            });

            modelBuilder.Entity<Campaignaccount>(entity =>
            {
                entity.ToTable("campaignaccount");

                entity.HasIndex(e => e.AccountId, "UQ__campaign__349DA5A780A734D1")
                    .IsUnique();

                entity.Property(e => e.CampaignAccountId).ValueGeneratedOnAdd();

                entity.Property(e => e.VotingResult)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithOne(p => p.Campaignaccount)
                    .HasForeignKey<Campaignaccount>(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__campaigna__Accou__300424B4");

                entity.HasOne(d => d.CampaignAccount)
                    .WithOne(p => p.Campaignaccount)
                    .HasForeignKey<Campaignaccount>(d => d.CampaignAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_campaignaccount_mod");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.Campaignaccounts)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__campaigna__Campa__2F10007B");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comment");

                entity.Property(e => e.Content)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.CampaignAccount)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.CampaignAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__comment__Campaig__5AEE82B9");

                entity.HasOne(d => d.Thread)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ThreadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__comment__ThreadI__5BE2A6F2");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.ToTable("like");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.CampaignAccount)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.CampaignAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__like__CampaignAc__5CD6CB2B");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK__like__CommentId__5DCAEF64");

                entity.HasOne(d => d.Thread)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.ThreadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__like__ThreadId__5EBF139D");
            });

            modelBuilder.Entity<Mod>(entity =>
            {
                entity.ToTable("mod");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ModEmail).HasMaxLength(50);

                entity.Property(e => e.ModName).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Mods)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_mod_category");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("notification");

                entity.Property(e => e.Content)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.IsRead).HasDefaultValueSql("((0))");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK__notificat__Comme__60A75C0F");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__notificat__PostI__619B8048");

                entity.HasOne(d => d.ReceiverAccount)
                    .WithMany(p => p.NotificationReceiverAccounts)
                    .HasForeignKey(d => d.ReceiverAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__notificat__Recei__628FA481");

                entity.HasOne(d => d.SenderAccount)
                    .WithMany(p => p.NotificationSenderAccounts)
                    .HasForeignKey(d => d.SenderAccountId)
                    .HasConstraintName("FK__notificat__Sende__6383C8BA");
            });

            modelBuilder.Entity<Otp>(entity =>
            {
                entity.ToTable("otp");

                entity.Property(e => e.Email)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Otp1)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Otp");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<Pinned>(entity =>
            {
                entity.HasKey(e => new { e.CampaignId, e.UserId })
                    .HasName("PK__pinned__EE26065DC6DF7834");

                entity.ToTable("pinned");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.Pinneds)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__pinned__Campaign__45F365D3");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Pinneds)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__pinned__UserId__46E78A0C");
            });

            modelBuilder.Entity<Thread>(entity =>
            {
                entity.ToTable("thread");

                entity.Property(e => e.Content)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.CampaignAccount)
                    .WithMany(p => p.Threads)
                    .HasForeignKey(d => d.CampaignAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__thread__Campaign__66603565");
            });

            modelBuilder.Entity<Vote>(entity =>
            {
                entity.ToTable("vote");

                entity.HasIndex(e => e.CampaignId, "CampaignId_idx");

                entity.HasIndex(e => e.VoterId, "VoteBy_idx");

                entity.HasIndex(e => e.VotingObjectId, "VoteFor_idx");

                entity.HasOne(d => d.CampaignNavigation)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Campaign");

                entity.HasOne(d => d.Voter)
                    .WithMany(p => p.Votes)
                    .HasPrincipalKey(p => p.AccountId)
                    .HasForeignKey(d => d.VoterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VoterId");

                entity.HasOne(d => d.VotingObject)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.VotingObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VotingObjectId");
            });

            modelBuilder.Entity<Votingobject>(entity =>
            {
                entity.ToTable("votingobject");

                entity.Property(e => e.Description)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.ImgPath)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Question)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.VotingObjectName)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.VotingObjectType)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.Votingobjects)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CampaignId");
            });

            modelBuilder.Entity<Votingrule>(entity =>
            {
                entity.ToTable("votingrule");

                entity.Property(e => e.RuleDescription)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.RuleName)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.RuleType)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
