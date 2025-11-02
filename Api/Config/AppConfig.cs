using System;
using System.Linq.Expressions;
using Domain.Contexts;
using Domain.Handlers;
using RapidFireLib.Lib.Core;

namespace Api.Config
{
    public class AppConfig : IConfig
    {
        public void Configure(Configuration configuration)
        {
            //APP
            configuration.APP.BusinessModuleName = "Domain";
            configuration.APP.RootDomain = "localhost";
            configuration.APP.EnableCSP = false;
            configuration.APP.AttachmentStorageConfig = AttachmentStorageConfig.FileSystem();
            configuration.APP.AppTitle = "Quiz Mangement System";
            configuration.APP.AppSlogan = "Quiz Mangement System for online ";
            configuration.APP.AppLogo = "logo-4.png";
            configuration.APP.LoginHomeImage = "bg-4.jpg";
            configuration.APP.AppVersion = "1.0";


            //Authentication
            configuration.Authentication.LoginType = RapidFireLib.Lib.Authintication.LoginType.LoginDB;

            //DB
            configuration.DB.DefaultDbContext = new DefaultContext(SAASType.NoSaas);
            configuration.DB.CheckTablePermission = false;
            configuration.DB.DynamicViewModelHandlers = new IDbHandler[] { new UpdateCommonFields() };


            configuration.Messaging.Email = new ConfigEmailAuth
            {
                Username = "email@mail.com",
                Password = "epass"
            };
        }

        public void ConfigureGlobalFilter<TEntity>(ref Expression<Func<TEntity, bool>> exp, RFCoreDbContext ctx) where TEntity : class
        {

        }

        public void ConfigureSetting(AppSettings appSettings)
        {

        }
    }
}