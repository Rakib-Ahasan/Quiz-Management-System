using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppPermissions",
                columns: table => new
                {
                    AppPermissionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    AppResourceId = table.Column<int>(type: "integer", nullable: false),
                    vCreate = table.Column<bool>(type: "boolean", nullable: false),
                    vRead = table.Column<bool>(type: "boolean", nullable: false),
                    vUpdate = table.Column<bool>(type: "boolean", nullable: false),
                    vDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPermissions", x => x.AppPermissionId);
                });

            migrationBuilder.CreateTable(
                name: "AppResources",
                columns: table => new
                {
                    AppResourceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentId = table.Column<int>(type: "integer", nullable: false),
                    ModelName = table.Column<string>(type: "text", nullable: true),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    MenuOrder = table.Column<int>(type: "integer", nullable: true),
                    IsModel = table.Column<bool>(type: "boolean", nullable: false),
                    IconClass = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    MenuIsVisible = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Position = table.Column<string>(type: "text", nullable: true),
                    HasLeftMenu = table.Column<bool>(type: "boolean", nullable: true),
                    HasSeparator = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppResources", x => x.AppResourceId);
                });

            migrationBuilder.CreateTable(
                name: "Approval",
                columns: table => new
                {
                    ApprovalId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApprovalItemId = table.Column<string>(type: "text", nullable: true),
                    TableName = table.Column<string>(type: "text", nullable: true),
                    Text = table.Column<string>(type: "text", nullable: true),
                    Logo = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    RequestedBy = table.Column<int>(type: "integer", nullable: true),
                    RequestOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ApprovedBy = table.Column<int>(type: "integer", nullable: true),
                    ApprovedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "integer", nullable: true),
                    ApprovalUserType = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approval", x => x.ApprovalId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    RoleType = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsAllProgram = table.Column<bool>(type: "boolean", nullable: false),
                    IsAllFieldOffice = table.Column<bool>(type: "boolean", nullable: false),
                    RoleIndex = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    ModifiedBy = table.Column<int>(type: "integer", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NormalizedName = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    IdentityUserLoginId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => x.IdentityUserLoginId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppUserId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserType = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: true),
                    LanguageName = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateBy = table.Column<string>(type: "text", nullable: true),
                    EditDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EditBy = table.Column<string>(type: "text", nullable: true),
                    FBEmail = table.Column<string>(type: "text", nullable: true),
                    TWEmail = table.Column<string>(type: "text", nullable: true),
                    GoogleEmail = table.Column<string>(type: "text", nullable: true),
                    MSEmail = table.Column<string>(type: "text", nullable: true),
                    Photo = table.Column<string>(type: "text", nullable: true),
                    ResetPasswordCode = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditTrail",
                columns: table => new
                {
                    AuditId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecordId = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    ModelName = table.Column<string>(type: "text", nullable: true),
                    OperationType = table.Column<string>(type: "text", nullable: true),
                    AuditDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RefData = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrail", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "AuditTrailView",
                columns: table => new
                {
                    AuditId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecordId = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    ModelName = table.Column<string>(type: "text", nullable: true),
                    OperationType = table.Column<string>(type: "text", nullable: true),
                    AuditDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    UserEmail = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrailView", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "ChatGroup",
                columns: table => new
                {
                    ChatGroupId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    GroupType = table.Column<string>(type: "text", nullable: true),
                    CustomFlag = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatGroup", x => x.ChatGroupId);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessage",
                columns: table => new
                {
                    ChatMessageId = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: true),
                    Sender = table.Column<string>(type: "text", nullable: true),
                    SendTo = table.Column<string>(type: "text", nullable: true),
                    SendAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessage", x => x.ChatMessageId);
                });

            migrationBuilder.CreateTable(
                name: "ConfigBKashAuth",
                columns: table => new
                {
                    PId = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    AppKey = table.Column<string>(type: "text", nullable: true),
                    AppSecret = table.Column<string>(type: "text", nullable: true),
                    CheckoutURL = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigBKashAuth", x => x.PId);
                });

            migrationBuilder.CreateTable(
                name: "DataVerificationLog",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TableName = table.Column<string>(type: "text", nullable: true),
                    OperationType = table.Column<int>(type: "integer", nullable: false),
                    PrimaryKey = table.Column<string>(type: "text", nullable: true),
                    Id = table.Column<string>(type: "text", nullable: true),
                    UUID = table.Column<string>(type: "text", nullable: true),
                    RecordType = table.Column<int>(type: "integer", nullable: true),
                    NotifiedTo = table.Column<int>(type: "integer", nullable: true),
                    NotifiedBy = table.Column<int>(type: "integer", nullable: false),
                    IsNotified = table.Column<int>(type: "integer", nullable: false),
                    IsClientUpdated = table.Column<int>(type: "integer", nullable: true),
                    ClientUpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ChildTables = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataVerificationLog", x => x.LogId);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    DistrictId = table.Column<string>(type: "text", nullable: false),
                    DistrictName = table.Column<string>(type: "text", nullable: true),
                    DistrictNameBN = table.Column<string>(type: "text", nullable: true),
                    DivisionId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.DistrictId);
                });

            migrationBuilder.CreateTable(
                name: "Division",
                columns: table => new
                {
                    DivisionId = table.Column<string>(type: "text", nullable: false),
                    DivisionName = table.Column<string>(type: "text", nullable: true),
                    DivisionNameBN = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Division", x => x.DivisionId);
                });

            migrationBuilder.CreateTable(
                name: "EmailLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    From = table.Column<string>(type: "text", nullable: true),
                    To = table.Column<string>(type: "text", nullable: true),
                    CC = table.Column<string>(type: "text", nullable: true),
                    Subject = table.Column<string>(type: "text", nullable: true),
                    Body = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RecordId = table.Column<string>(type: "text", nullable: true),
                    ModelName = table.Column<string>(type: "text", nullable: true),
                    SentStatus = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FCMCUDSetup",
                columns: table => new
                {
                    FCMCUDSetupId = table.Column<string>(type: "text", nullable: false),
                    ModelName = table.Column<string>(type: "text", nullable: true),
                    CreateMessageTitle = table.Column<string>(type: "text", nullable: true),
                    CreateMessageBody = table.Column<string>(type: "text", nullable: true),
                    UpdateMessageTitle = table.Column<string>(type: "text", nullable: true),
                    UpdateMessageBody = table.Column<string>(type: "text", nullable: true),
                    DeleteMessageTitle = table.Column<string>(type: "text", nullable: true),
                    DeleteMessageBody = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FCMCUDSetup", x => x.FCMCUDSetupId);
                });

            migrationBuilder.CreateTable(
                name: "FCMToken",
                columns: table => new
                {
                    Token = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Device = table.Column<string>(type: "text", nullable: true),
                    UserTable = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FCMToken", x => x.Token);
                });

            migrationBuilder.CreateTable(
                name: "ModelDefaults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModelName = table.Column<string>(type: "text", nullable: true),
                    FieldName = table.Column<string>(type: "text", nullable: true),
                    OptionText = table.Column<string>(type: "text", nullable: true),
                    OptionValue = table.Column<string>(type: "text", nullable: true),
                    OptionOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelDefaults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: true),
                    Text = table.Column<string>(type: "text", nullable: true),
                    Logo = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    NotifiedBy = table.Column<int>(type: "integer", nullable: true),
                    NotifiedTo = table.Column<int>(type: "integer", nullable: true),
                    NotifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    NotificationViewOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NotificationUserType = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                });

            migrationBuilder.CreateTable(
                name: "NotificationRule",
                columns: table => new
                {
                    NotificationRuleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModelName = table.Column<string>(type: "text", nullable: false),
                    Operation = table.Column<string>(type: "text", nullable: false),
                    TitleTemplate = table.Column<string>(type: "text", nullable: true),
                    BodyTemplate = table.Column<string>(type: "text", nullable: true),
                    Condition = table.Column<string>(type: "text", nullable: true),
                    NotifyToAll = table.Column<bool>(type: "boolean", nullable: false),
                    RecipientRoles = table.Column<string>(type: "text", nullable: true),
                    RecipientUserFields = table.Column<string>(type: "text", nullable: true),
                    GeoFilterBy = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    InAppPopup = table.Column<bool>(type: "boolean", nullable: false),
                    ToUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationRule", x => x.NotificationRuleId);
                });

            migrationBuilder.CreateTable(
                name: "PushNotification",
                columns: table => new
                {
                    PushNotificationId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Body = table.Column<string>(type: "text", nullable: true),
                    ToUrl = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    SendTo = table.Column<string>(type: "text", nullable: true),
                    SendAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushNotification", x => x.PushNotificationId);
                });

            migrationBuilder.CreateTable(
                name: "PushNotificationSeen",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LastSeenAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushNotificationSeen", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "QuickSearch",
                columns: table => new
                {
                    SearchId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Logo = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    GroupTitle = table.Column<string>(type: "text", nullable: true),
                    SearchUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuickSearch", x => x.SearchId);
                });

            migrationBuilder.CreateTable(
                name: "RoleType",
                columns: table => new
                {
                    RoleTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleType", x => x.RoleTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Schedular",
                columns: table => new
                {
                    SchedularId = table.Column<string>(type: "text", nullable: false),
                    JobName = table.Column<string>(type: "text", nullable: true),
                    Seconds = table.Column<string>(type: "text", nullable: true),
                    Minutes = table.Column<string>(type: "text", nullable: true),
                    Hours = table.Column<string>(type: "text", nullable: true),
                    DayOfMonth = table.Column<string>(type: "text", nullable: true),
                    Months = table.Column<string>(type: "text", nullable: true),
                    DayOfWeek = table.Column<string>(type: "text", nullable: true),
                    NthDayOfWeek = table.Column<string>(type: "text", nullable: true),
                    Year = table.Column<string>(type: "text", nullable: true),
                    CornEx = table.Column<string>(type: "text", nullable: true),
                    CexSL = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedular", x => x.SchedularId);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    TenantId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SubdomainName = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Host = table.Column<string>(type: "text", nullable: true),
                    ContactEmail = table.Column<string>(type: "text", nullable: false),
                    ContactPhone = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    ContactAddress = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsPendingUpdate = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentStatus = table.Column<int>(type: "integer", nullable: true),
                    TenantType = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.TenantId);
                });

            migrationBuilder.CreateTable(
                name: "Unions",
                columns: table => new
                {
                    UnionId = table.Column<string>(type: "text", nullable: false),
                    UnionName = table.Column<string>(type: "text", nullable: true),
                    UnionNameBN = table.Column<string>(type: "text", nullable: true),
                    UpazilaId = table.Column<string>(type: "text", nullable: true),
                    DistrictId = table.Column<string>(type: "text", nullable: true),
                    DivisionId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unions", x => x.UnionId);
                });

            migrationBuilder.CreateTable(
                name: "Upazila",
                columns: table => new
                {
                    UpazilaId = table.Column<string>(type: "text", nullable: false),
                    UpazilaName = table.Column<string>(type: "text", nullable: true),
                    UpazilaNameBN = table.Column<string>(type: "text", nullable: true),
                    DistrictId = table.Column<string>(type: "text", nullable: true),
                    DivisionId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Upazila", x => x.UpazilaId);
                });

            migrationBuilder.CreateTable(
                name: "UserDesignations",
                columns: table => new
                {
                    DesignationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDesignations", x => x.DesignationId);
                });

            migrationBuilder.CreateTable(
                name: "UserGeo",
                columns: table => new
                {
                    UserGeoId = table.Column<string>(type: "text", nullable: false),
                    DivisionId = table.Column<string>(type: "text", nullable: true),
                    DistrictId = table.Column<string>(type: "text", nullable: true),
                    UpazilaId = table.Column<string>(type: "text", nullable: true),
                    UnionId = table.Column<string>(type: "text", nullable: true),
                    VillageId = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGeo", x => x.UserGeoId);
                });

            migrationBuilder.CreateTable(
                name: "UserGlobalGeo",
                columns: table => new
                {
                    UserGlobalGeoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    RegionId = table.Column<int>(type: "integer", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    ProgramId = table.Column<int>(type: "integer", nullable: false),
                    FieldOfficeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGlobalGeo", x => x.UserGlobalGeoId);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleGeo",
                columns: table => new
                {
                    UserRoleGeoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    DivisionCode = table.Column<string>(type: "text", nullable: true),
                    DistrictCode = table.Column<string>(type: "text", nullable: true),
                    UpazilaCode = table.Column<string>(type: "text", nullable: true),
                    UnionCode = table.Column<string>(type: "text", nullable: true),
                    VillageCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleGeo", x => x.UserRoleGeoId);
                });

            migrationBuilder.CreateTable(
                name: "UserSLGeo",
                columns: table => new
                {
                    UserSLGeoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    CompanyCode = table.Column<int>(type: "integer", nullable: false),
                    TeaEstateCode = table.Column<int>(type: "integer", nullable: false),
                    EstateDivisionCode = table.Column<int>(type: "integer", nullable: false),
                    ChildrenClubCode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSLGeo", x => x.UserSLGeoId);
                });

            migrationBuilder.CreateTable(
                name: "Village",
                columns: table => new
                {
                    VillageId = table.Column<string>(type: "text", nullable: false),
                    VillageName = table.Column<string>(type: "text", nullable: true),
                    VillageNameBN = table.Column<string>(type: "text", nullable: true),
                    UnionId = table.Column<string>(type: "text", nullable: true),
                    UpazilaId = table.Column<string>(type: "text", nullable: true),
                    DistrictId = table.Column<string>(type: "text", nullable: true),
                    DivisionId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Village", x => x.VillageId);
                });

            migrationBuilder.CreateTable(
                name: "ChatGroupMember",
                columns: table => new
                {
                    ChatGroupMemberId = table.Column<string>(type: "text", nullable: false),
                    ChatGroupId = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    IsRemoved = table.Column<bool>(type: "boolean", nullable: false),
                    UserTable = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatGroupMember", x => x.ChatGroupMemberId);
                    table.ForeignKey(
                        name: "FK_ChatGroupMember_ChatGroup_ChatGroupId",
                        column: x => x.ChatGroupId,
                        principalTable: "ChatGroup",
                        principalColumn: "ChatGroupId");
                });

            migrationBuilder.CreateTable(
                name: "ChatSeenUser",
                columns: table => new
                {
                    ChatSeenUserId = table.Column<string>(type: "text", nullable: false),
                    ChatMessageId = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    SeenAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSeenUser", x => x.ChatSeenUserId);
                    table.ForeignKey(
                        name: "FK_ChatSeenUser_ChatMessage_ChatMessageId",
                        column: x => x.ChatMessageId,
                        principalTable: "ChatMessage",
                        principalColumn: "ChatMessageId");
                });

            migrationBuilder.CreateTable(
                name: "FCMCUDTopic",
                columns: table => new
                {
                    FCMCUDTopicId = table.Column<string>(type: "text", nullable: false),
                    FCMCUDSetupId = table.Column<string>(type: "text", nullable: true),
                    RoleTopic = table.Column<string>(type: "text", nullable: true),
                    C = table.Column<bool>(type: "boolean", nullable: false),
                    U = table.Column<bool>(type: "boolean", nullable: false),
                    D = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FCMCUDTopic", x => x.FCMCUDTopicId);
                    table.ForeignKey(
                        name: "FK_FCMCUDTopic_FCMCUDSetup_FCMCUDSetupId",
                        column: x => x.FCMCUDSetupId,
                        principalTable: "FCMCUDSetup",
                        principalColumn: "FCMCUDSetupId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatGroupMember_ChatGroupId",
                table: "ChatGroupMember",
                column: "ChatGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatSeenUser_ChatMessageId",
                table: "ChatSeenUser",
                column: "ChatMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_FCMCUDTopic_FCMCUDSetupId",
                table: "FCMCUDTopic",
                column: "FCMCUDSetupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppPermissions");

            migrationBuilder.DropTable(
                name: "AppResources");

            migrationBuilder.DropTable(
                name: "Approval");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AuditTrail");

            migrationBuilder.DropTable(
                name: "AuditTrailView");

            migrationBuilder.DropTable(
                name: "ChatGroupMember");

            migrationBuilder.DropTable(
                name: "ChatSeenUser");

            migrationBuilder.DropTable(
                name: "ConfigBKashAuth");

            migrationBuilder.DropTable(
                name: "DataVerificationLog");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "Division");

            migrationBuilder.DropTable(
                name: "EmailLog");

            migrationBuilder.DropTable(
                name: "FCMCUDTopic");

            migrationBuilder.DropTable(
                name: "FCMToken");

            migrationBuilder.DropTable(
                name: "ModelDefaults");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "NotificationRule");

            migrationBuilder.DropTable(
                name: "PushNotification");

            migrationBuilder.DropTable(
                name: "PushNotificationSeen");

            migrationBuilder.DropTable(
                name: "QuickSearch");

            migrationBuilder.DropTable(
                name: "RoleType");

            migrationBuilder.DropTable(
                name: "Schedular");

            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.DropTable(
                name: "Unions");

            migrationBuilder.DropTable(
                name: "Upazila");

            migrationBuilder.DropTable(
                name: "UserDesignations");

            migrationBuilder.DropTable(
                name: "UserGeo");

            migrationBuilder.DropTable(
                name: "UserGlobalGeo");

            migrationBuilder.DropTable(
                name: "UserRoleGeo");

            migrationBuilder.DropTable(
                name: "UserSLGeo");

            migrationBuilder.DropTable(
                name: "Village");

            migrationBuilder.DropTable(
                name: "ChatGroup");

            migrationBuilder.DropTable(
                name: "ChatMessage");

            migrationBuilder.DropTable(
                name: "FCMCUDSetup");
        }
    }
}
