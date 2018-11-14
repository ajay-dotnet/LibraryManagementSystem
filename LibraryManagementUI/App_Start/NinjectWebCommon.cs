/// <copyright file="NinjectWebCommon.cs" company="XYZ Inc.">
/// Copyright (c) All Right Reserved
/// </copyright>
/// <author>Ajay Gupta</author>
/// <summary>Class to implement dependency injection</summary>

using System;
using System.Web;
using LibraryManagementUI;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using LibraryManagementUI.Repositories;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace LibraryManagementUI
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IBaseRepository>().To<BaseRepository>();
            kernel.Bind<ITitleRepository>().To<TitleRepository>();
            kernel.Bind<IBookRepository>().To<BookRepository>();
            kernel.Bind<ICheckInRepository>().To<CheckInRepository>();
        }
    }
}