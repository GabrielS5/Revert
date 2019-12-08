using AspectInjector.Broker;
using Core.Entities;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Aspects
{
    [Aspect(Scope.Global)]
    [Injection(typeof(Log))]
    public class Log : Attribute
    {
        private readonly ILogger logger;
        private readonly AppDbContext appDbContext;
        public Log()
        {
            var connectionstring = "Server=(localdb)\\MSSQLLocalDB;Database=Revert;Trusted_Connection=True;";

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionstring);


            appDbContext = new AppDbContext(optionsBuilder.Options);
            logger = new LoggerFactory().AddFile("Logs/aspectLog").CreateLogger(typeof(Log));
        }


        [Advice(Kind.Around, Targets = Target.Method)]
        public object HandleMethod(
            [Argument(Source.Name)] string name,
            [Argument(Source.Arguments)] object[] arguments,
            [Argument(Source.Target)] Func<object[], object> method)
        {
            var result = method(arguments);

            var argumentsString = "";

            foreach (object arg in arguments)
            {
                argumentsString = argumentsString + "  " + arg.ToString();
            }

            logger.LogInformation($"\nExecuted method {name} with arguments: {argumentsString}");

            try
            {
                var queryable = ((Task<IQueryable<Record>>)result).Result;
                var sql = queryable.ToSql(appDbContext);
                var hasWhere = sql.Contains("WHERE");
                var selected = sql.Substring(7, sql.IndexOf("FROM") - 9).Split(",").Select(s => s.Split(".")[1].Substring(1, s.Split(".")[1].Length - 2)).ToArray();
                var from = sql.Substring(sql.IndexOf("FROM") + 4, (hasWhere ? sql.IndexOf("WHERE") : sql.Length) - sql.IndexOf("FROM") - 4).Substring(2).Split("]")[0];
                var where = hasWhere? sql.Substring(sql.IndexOf("WHERE")).Split("AND").Select(s => s.Split(".")[1].Substring(1).Split("]")[0]).ToArray(): new string[] { "Not found"};

                logger.LogInformation($"\n\n   SQL query is {sql.Replace('\n',' ')}.\n   From: {from}.\n   Selected fields {selected.Join(", ")}.\n   Filtered by fields: {where.Join(", ")}.\n");
            }
            catch
            {

            }

            return result;
        }
    }
}
