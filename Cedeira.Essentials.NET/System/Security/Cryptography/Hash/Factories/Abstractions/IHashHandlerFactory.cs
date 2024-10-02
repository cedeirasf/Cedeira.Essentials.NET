﻿using Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Abstractions;

namespace Cedeira.Essentials.NET.System.Security.Cryptography.Hash.Factories.Abstractions
{
    /// <summary>
    /// Defines a factory interface for creating instances of IHashHandler.
    /// </summary>
    public interface IHashHandlerFactory
    {
        /// <summary>
        /// Creates an instance of IHashHandler with default settings.
        /// </summary>
        /// <returns>An instance of IHashHandler.</returns>
        IHashHandler CreateHash();

        ///// <summary>
        ///// Creates an instance of IHashHandler with a specified output format.
        ///// </summary>
        ///// <returns>An instance of IHashHandler with the configured output format.</returns>
        //IHashHandler CreateHashWithOutputFormat();
    }
}
