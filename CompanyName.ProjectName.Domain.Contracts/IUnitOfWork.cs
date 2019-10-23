using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyName.ProjectName.Domain.Contracts
{
    /// <summary>
    /// This interface is used to allow hitting the database once for all operations (Unit of Work Design Pattern).
    /// </summary>
    public interface IUnitOfWork
    {
        void Commit();
    }
}
