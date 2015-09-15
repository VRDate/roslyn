﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace Roslyn.Test.Utilities
{
    internal class TestMetadataReferenceResolver : MetadataReferenceResolver
    {
        private readonly RelativePathResolver _pathResolver;
        private readonly Dictionary<string, PortableExecutableReference> _assemblyNames;
        private readonly Dictionary<string, PortableExecutableReference> _files;

        public TestMetadataReferenceResolver(
            RelativePathResolver pathResolver = null,
            Dictionary<string, PortableExecutableReference> assemblyNames = null, 
            Dictionary<string, PortableExecutableReference> files = null)
        {
            _pathResolver = pathResolver;
            _assemblyNames = assemblyNames ?? new Dictionary<string, PortableExecutableReference>();
            _files = files ?? new Dictionary<string, PortableExecutableReference>();
        }

        public override ImmutableArray<PortableExecutableReference> ResolveReference(string reference, string baseFilePath, MetadataReferenceProperties properties)
        {
            Dictionary<string, PortableExecutableReference> map;

            if (PathUtilities.IsFilePath(reference))
            {
                if (_pathResolver != null)
                {
                    reference = _pathResolver.ResolvePath(reference, baseFilePath);
                }

                map = _files;
            }
            else
            {
                map = _assemblyNames;
            }

            PortableExecutableReference result;
            return map.TryGetValue(reference, out result) ? ImmutableArray.Create(result) : ImmutableArray<PortableExecutableReference>.Empty;
        }

        public override bool Equals(object other) => true;
        public override int GetHashCode() => 1;
    }
}