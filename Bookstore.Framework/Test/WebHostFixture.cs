﻿using AutoMapper;
using Bookstore.Framework.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using Xunit;

[assembly: CollectionBehavior(
    CollectionBehavior.CollectionPerAssembly,
    DisableTestParallelization = true,
    MaxParallelThreads = 1)]
namespace Bookstore.Framework.Test
{
    public class WebHostFixture<TStartup> : IDisposable
        where TStartup : class
    {
        public WebHostFixture()
        {
            ServiceCollectionExtensions.UseStaticRegistration = false;
            this.TestServer = new TestServer(new WebHostBuilder()
                .UseEnvironment("IntegrationTests")
                .UseStartup<TStartup>());

            this.TestClient = this.TestServer.CreateClient();
        }

        public TestServer TestServer { get; }

        public HttpClient TestClient { get; }

        public T GetService<T>()
        {
            return this.TestServer.GetService<T>();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.TestClient != null)
                    this.TestClient.Dispose();

                if (this.TestServer != null)
                    this.TestServer.Dispose();
            }
        }
    }
}
