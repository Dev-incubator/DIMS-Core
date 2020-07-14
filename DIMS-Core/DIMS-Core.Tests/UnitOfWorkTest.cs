using System;
using System.Collections.Generic;
using System.Text;
using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Repositories;
using NUnit.Framework;

namespace DIMS_Core.Tests
{
    public class UnitOfWorkTest
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkTest(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async void SaveAsync()
        {
            await _unitOfWork.SaveAsync();
        }
    }
}
