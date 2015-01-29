﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FubuCore;
using Storyteller.Core.Conversion;
using Storyteller.Core.Model;

namespace Storyteller.Core.Engine
{
    public class SpecRunner : IDisposable
    {
        /*
         * TODO -- this time, SpecRunner will also handle the execution queue duties as
         * well because it's more intertwined.
         * 
         * 
         * 
         */

        private readonly ISystem _system;
        private Task _library;

        public SpecRunner(ISystem system)
        {
            _system = system;
            _library = Task.Factory.StartNew(() => FixtureLibrary.CreateForAppDomain(CellHandling.Basic()))
                .ContinueWith(t =>
                {
                    // TODO -- message out the fixture library
                });
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public interface ISystem : IDisposable
    {
        IExecutionContext CreateContext();

        IEnumerable<IConversionProvider> ConversionProviders();

        Task Warmup();
        Task Recycle();
    }

    public interface IExecutionContext : IDisposable
    {
        IServiceLocator Services { get; }
    }

    public class NulloSystem : ISystem
    {
        public readonly InMemoryServiceLocator Services = new InMemoryServiceLocator();

        public IExecutionContext CreateContext()
        {
            return new SimpleExecutionContext(Services);
        }

        public Task Recycle()
        {
            return Task.FromResult("recycled");
        }

        public Task Warmup()
        {
            return Task.FromResult("warmed up");
        }

        public void Dispose()
        {
        }

        public IEnumerable<IConversionProvider> ConversionProviders()
        {
            return new IConversionProvider[0];
        }
    }

    public class SimpleExecutionContext : IExecutionContext
    {
        public SimpleExecutionContext(IServiceLocator services)
        {
            Services = services;


        }

        void IDisposable.Dispose()
        {

        }

        IServiceLocator IExecutionContext.Services { get { return Services; } }

        public IServiceLocator Services { get; private set; }
    }


}