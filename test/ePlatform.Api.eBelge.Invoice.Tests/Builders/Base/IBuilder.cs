using System;

namespace ePlatform.Api.eBelge.Invoice.Tests.Builders.Base
{
    public interface IBuilder
    {
        /* maker to indicate a builder object */
    }

    public interface IBuilder<TBuildResult, TBuilder> : IBuilder
        where TBuildResult : class, new()
        where TBuilder : class, IBuilder
    {
        /// <summary>
        /// Builds <see cref="TBuildResult"/> object.
        /// </summary>
        /// <returns><see cref="TBuildResult"/> object.</returns>
        TBuildResult Build();

        /// <summary>
        /// Creates <see cref="TBuildResult"/> object.
        /// </summary>
        /// <returns><see cref="TBuilder"/> object.</returns>
        TBuilder Create();

        /// <summary>
        /// A generic way to set properties.
        /// </summary>
        /// <param name="setAction">The action for setting properties.</param>
        /// <returns><see cref="TBuilder"/> object.</returns>
        TBuilder With(Action<TBuildResult> setAction);

        /// <summary>
        /// Creates <see cref="TBuildResult"/> object with default values.
        /// </summary>
        /// <returns><see cref="TBuilder"/> object.</returns>
        TBuilder CreateWithDefaultValues();
    }
}
