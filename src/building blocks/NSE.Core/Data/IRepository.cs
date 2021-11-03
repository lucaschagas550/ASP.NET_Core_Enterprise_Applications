﻿using NSE.Core.DomainObjects;
using System;

namespace NSE.Core.Data
{
    //somente entidade pode usar o repositório
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {

    }
}
