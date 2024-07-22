﻿using System.Reflection;
using Eventify.Modules.Users.Domain.Users;
using Eventify.Modules.Users.Infrastructure;

namespace Eventify.Modules.Users.ArchitectureTests.Abstractions;

public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(Users.Application.AssemblyReference).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(User).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(UsersModule).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(Users.Presentation.AssemblyReference).Assembly;
}
