﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

namespace Microsoft.CodeAnalysis.CSharp
{
    internal enum ClosureKind
    {
        /// <summary>
        /// The closure doesn't declare any variables, and is never converted to a delegate.
        /// Lambdas are emitted directly to the containing class as a static method.
        /// </summary>
        Static,

        /// <summary>
        /// The closure doesn't declare any variables, and is converted to a delegate at least once.
        /// Display class is a singleton and may be shared with other top-level methods.
        /// </summary>
        Singleton,

        /// <summary>
        /// The closure only contains a reference to the containing class instance ("this").
        /// We don't emit a display class, lambdas are emitted directly to the containing class as its instance methods.
        /// </summary>
        ThisOnly,

        /// <summary>
        /// General closure.
        /// Display class may only contain lambdas defined in the same top-level method.
        /// </summary>
        General,
    }
}
