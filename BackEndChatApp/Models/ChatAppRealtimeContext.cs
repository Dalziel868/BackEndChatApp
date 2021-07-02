using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BackEndChatApp.Models
{
    public partial class ChatAppRealtimeContext : DbContext
    {
        public ChatAppRealtimeContext()
        {
        }

        public ChatAppRealtimeContext(DbContextOptions<ChatAppRealtimeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<GroupPerson> GroupPeople { get; set; }
        public virtual DbSet<Ipconfig> Ipconfigs { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<SmsMessage> SmsMessages { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserPerson> UserPeople { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=.;Database=ChatAppRealtime;User Id=sa;password=AspireE15;Trusted_Connection=False;MultipleActiveResultSets=true;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasKey(e => new { e.FriendId, e.PersonId })
                    .HasName("pk_friends");

                entity.Property(e => e.FriendId).HasColumnName("FriendID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Friends)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("fk_frtouser");
            });

            modelBuilder.Entity<GroupPerson>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("PK__GroupPeo__149AF30A563E68D6");

                entity.HasIndex(e => e.AdminPerson, "UC_Admin")
                    .IsUnique();

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.GroupName).HasMaxLength(300);
            });

            modelBuilder.Entity<Ipconfig>(entity =>
            {
                entity.ToTable("IPConfig");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Ip)
                    .HasMaxLength(100)
                    .HasColumnName("IP");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Ipconfigs)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__IPConfig__Person__4AB81AF0");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => new { e.GroupId, e.UserId })
                    .HasName("pk_members");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__Membes__GroupID__628FA481");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Membes__UserID__6383C8BA");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.NotifiId)
                    .HasName("PK__Notifica__A8B53DB5065C5E92");

                entity.Property(e => e.NotifiId).HasColumnName("NotifiID");

                entity.Property(e => e.ContextText).HasMaxLength(500);

                entity.Property(e => e.FromId).HasColumnName("FromID");

                entity.Property(e => e.NotiType).HasMaxLength(20);

                entity.Property(e => e.ToId).HasColumnName("ToID");
            });

            modelBuilder.Entity<SmsMessage>(entity =>
            {
                entity.HasKey(e => e.MessId)
                    .HasName("PK__SmsMessa__9CC50CFDA2C0BCE3");

                entity.ToTable("SmsMessage");

                entity.Property(e => e.MessId)
                    .HasColumnName("MessID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.FromId).HasColumnName("FromID");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.SendTime).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(30)
                    .HasColumnName("_Status");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.SmsMessages)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SmsMessag__Group__5FB337D6");
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.HasKey(e => e.PersonId)
                    .HasName("PK__UserLogi__AA2FFB85DD1AB41C");

                entity.ToTable("UserLogin");

                entity.HasIndex(e => e.Email, "UQ__UserLogi__A9D1053423C90C65")
                    .IsUnique();

                entity.Property(e => e.PersonId)
                    .ValueGeneratedNever()
                    .HasColumnName("PersonID");

                entity.Property(e => e.Email).HasMaxLength(300);

                entity.Property(e => e.LoginTime).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("_Password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.UserLogin)
                    .HasForeignKey<UserLogin>(d => d.PersonId)
                    .HasConstraintName("fk_loginperson");
            });

            modelBuilder.Entity<UserPerson>(entity =>
            {
                entity.HasKey(e => e.PersonId)
                    .HasName("PK__UserPers__AA2FFB85291C71B0");

                entity.ToTable("UserPerson");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.Gender).HasMaxLength(20);

                entity.Property(e => e.Introduce).HasMaxLength(300);

                entity.Property(e => e.Status).HasColumnName("_Status");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
