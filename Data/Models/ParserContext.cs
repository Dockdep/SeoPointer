using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SeoSoft.Models
{
    public partial class ParserContext : DbContext
    {
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Competitor> Competitor { get; set; }
        public virtual DbSet<CompetitorData> CompetitorData { get; set; }
        public virtual DbSet<CompetitorField> CompetitorField { get; set; }
        public virtual DbSet<CompetitorItem> CompetitorItem { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectStatus> ProjectStatus { get; set; }
        public virtual DbSet<Proxy> Proxy { get; set; }
        public virtual DbSet<UserStatus> UserStatus { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public ParserContext(DbContextOptions<ParserContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('parser_sections_id_seq'::regclass)");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnName("category_name")
                    .HasColumnType("varchar");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Category)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("fk_category_project");
            });

            modelBuilder.Entity<Competitor>(entity =>
            {
                entity.ToTable("competitor");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('parser_competitors_id_seq'::regclass)");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar");

                entity.Property(e => e.Status)
                    .HasColumnName("status");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("varchar");

                entity.Property(e => e.TestUrl)
                    .HasColumnName("test_url")
                    .HasColumnType("varchar");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Competitor)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("fk_competitor_projects");
            });

            modelBuilder.Entity<CompetitorData>(entity =>
            {
                entity.ToTable("competitor_data");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompetitorFieldId).HasColumnName("competitor_field_id");

                entity.Property(e => e.CompetitorId).HasColumnName("competitor_id");

                entity.Property(e => e.CompetitorItemId).HasColumnName("competitor_item_id");

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasColumnType("varchar")
                    .HasMaxLength(500);

                entity.HasOne(d => d.CompetitorField)
                    .WithMany(p => p.CompetitorData)
                    .HasForeignKey(d => d.CompetitorFieldId)
                    .HasConstraintName("fk_competitor_data_competitor_field");

                entity.HasOne(d => d.Competitor)
                    .WithMany(p => p.CompetitorData)
                    .HasForeignKey(d => d.CompetitorId)
                    .HasConstraintName("fk_competitor_data_competitor");

                entity.HasOne(d => d.CompetitorItem)
                    .WithMany(p => p.CompetitorData)
                    .HasForeignKey(d => d.CompetitorItemId)
                    .HasConstraintName("fk_competitor_data_competitor_item");
            });

            modelBuilder.Entity<CompetitorField>(entity =>
            {
                entity.ToTable("competitor_field");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompetitorId).HasColumnName("competitor_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar")
                    .HasMaxLength(255);

                entity.Property(e => e.Regexp)
                    .HasColumnName("regexp")
                    .HasColumnType("varchar")
                    .HasMaxLength(255);

                entity.Property(e => e.XPath)
                    .IsRequired()
                    .HasColumnName("x_path");

                entity.HasOne(d => d.Competitor)
                    .WithMany(p => p.CompetitorField)
                    .HasForeignKey(d => d.CompetitorId)
                    .HasConstraintName("fk_competitor_field_competitor");
            });

            modelBuilder.Entity<CompetitorItem>(entity =>
            {
                entity.ToTable("competitor_item");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('parser_coitem_id_seq'::regclass)");

                entity.Property(e => e.CompetitorId).HasColumnName("competitor_id");

                entity.Property(e => e.GoogleLink)
                    .HasColumnName("google_link")
                    .HasColumnType("varchar");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.LinkStatus)
                    .HasColumnName("link_status")
                    .HasColumnType("varchar");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("varchar");

                entity.HasOne(d => d.Competitor)
                    .WithMany(p => p.CompetitorItem)
                    .HasForeignKey(d => d.CompetitorId)
                    .HasConstraintName("fk_competitor_item_competitor");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.CompetitorItem)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("fk_competitor_item_item");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("item");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('parser_items_id_seq'::regclass)");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.GoogleUrl)
                    .HasColumnName("google_url")
                    .HasColumnType("varchar");

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasColumnName("item_name")
                    .HasColumnType("varchar");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("varchar");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("fk_item_category");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("fk_item_projects");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("project");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('projects_id_seq'::regclass)");

                entity.Property(e => e.Contacts).HasColumnName("contacts");

                entity.Property(e => e.EmailList)
                    .HasColumnName("email_list")
                    .HasColumnType("varchar");

                entity.Property(e => e.Info)
                    .HasColumnName("info")
                    .HasColumnType("varchar");

                entity.Property(e => e.KeyName)
                    .HasColumnName("key_name")
                    .HasColumnType("varchar")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar")
                    .HasMaxLength(255);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("varchar")
                    .HasMaxLength(255);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_project_user");
            });

            modelBuilder.Entity<ProjectStatus>(entity =>
            {
                entity.ToTable("project_status");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('projects_status_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar")
                    .HasMaxLength(100);

                entity.Property(e => e.Ru)
                    .HasColumnName("ru")
                    .HasColumnType("varchar")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Proxy>(entity =>
            {
                entity.ToTable("proxy");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Proxy1)
                    .HasColumnName("proxy")
                    .HasColumnType("varchar");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.ToTable("user_status");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('users_status_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar");

                entity.Property(e => e.Ru)
                    .HasColumnName("ru")
                    .HasColumnType("varchar")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar")
                    .HasMaxLength(255);

                entity.Property(e => e.LastOnline)
                    .HasColumnName("last_online")
                    .HasColumnType("varchar");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar")
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar")
                    .HasMaxLength(255);

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.HasSequence("parser_coitem_id_seq");

            modelBuilder.HasSequence("parser_competitors_id_seq");

            modelBuilder.HasSequence("parser_items_id_seq");

            modelBuilder.HasSequence("parser_sections_id_seq");

            modelBuilder.HasSequence("projects_id_seq");

            modelBuilder.HasSequence("projects_status_id_seq");

            modelBuilder.HasSequence("users_status_id_seq");
        }
    }
}