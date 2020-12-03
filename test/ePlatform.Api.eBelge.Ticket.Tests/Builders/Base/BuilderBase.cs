using System;

namespace ePlatform.Api.eBelge.Ticket.Tests.Builders.Base
{
    public class BuilderBase<TBuildResult, TBuilder> : IBuilder<TBuildResult, TBuilder>
        where TBuildResult : class, new()
        where TBuilder : class, IBuilder
    {
        protected TBuildResult _concreteObject;

        public TBuilder Create()
        {
            _concreteObject = new TBuildResult();
            return this as TBuilder;
        }

        public TBuildResult Build()
        {
            return _concreteObject;
        }

        public TBuilder With(Action<TBuildResult> setAction)
        {
            setAction?.Invoke(_concreteObject);
            return this as TBuilder;
        }

        public virtual TBuilder CreateWithDefaultValues()
        {
            _concreteObject = new TBuildResult();
            return this as TBuilder;
        }


    }

}
