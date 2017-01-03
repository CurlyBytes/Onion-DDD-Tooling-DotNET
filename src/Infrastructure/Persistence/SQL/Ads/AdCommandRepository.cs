﻿using System;
using System.Linq;
using Domain.Core.Model.Ads;
using System.Data;
using Persistence.SQL.Ads.QueryObjects;
using System.Threading.Tasks;

namespace Persistence.SQL.Ads
{
    public class AdCommandRepository : IAdCommandRepository
    {
        private readonly IConnectionFactory connection;

        public AdCommandRepository(IConnectionFactory connectionFactory)
        {
            this.connection = connectionFactory;
        }

        public async Task<bool> Insert(Ad ad)
        {
            return true;
            return await Task.Run(() =>
            {
                using (IDbConnection dbConnection = connection.Create())
                {
                    var adInsert = new AdInsert();
                    int newId = (int)dbConnection.Query<Int64>(adInsert.Query(new { Name = "", Price = ad.Price.Amount })).Single();
                    return (newId > 0);
                }
            });
        }

        public async Task<bool> Update(Ad ad)
        {
            return true;
            return await Task.Run(() =>
            {
                using (IDbConnection dbConnection = connection.Create())
                {
                    var adUpdate = new AdUpdate();
                    int resultUpdate = dbConnection.Execute(adUpdate.Query(new { Price = ad.Price.Amount, Name = "name example" }));
                    return (resultUpdate > 0);
                }
            });
        }

        public async Task<bool> Delete(Ad ad)
        {
            return true;
            return await Task.Run(() =>
            {
                using (IDbConnection dbConnection = connection.Create())
                {
                    QueryObject adDelete = new AdDelete().ById(ad.Id.Id);
                    int resultUpdate = dbConnection.Execute(adDelete);
                    return (resultUpdate > 0);
                }
            });
        }
    }
}
