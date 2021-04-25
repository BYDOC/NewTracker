using Dapper;
using System.Data;
using TrackerLibrary.Models;


namespace TrackerLibrary.DataAccess

{
    public class SqlConnector : IDataConnection
    {
        //TODO- Make the CreatePrize actually save to the DB
        /// <summary>
        /// saves a new prize to DB
        /// </summary>
        /// <param name="model">The prize info</param>
        /// <returns></returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {

                //using bittiginde connection kapanır

                var p = new DynamicParameters();
                p.Add("@PlaceNumber", model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmount", model.PrizeAmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                p.Add("@id",
                      0,
                      dbType: DbType.Int32,
                      direction: ParameterDirection.Output);

                connection.Execute("dbo.spPrizes_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");
                
                return model;
            }
        }
    }
}
