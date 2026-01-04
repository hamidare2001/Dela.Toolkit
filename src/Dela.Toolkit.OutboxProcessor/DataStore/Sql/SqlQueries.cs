using System.Data; 
using Dapper;
using Dela.Toolkit.Persistence;

namespace Dela.Toolkit.OutboxProcessor.DataStore.Sql;

public static class SqlQueries
{
    extension(IDbConnection connection)
    {
        public long GetOutboxCursorPosition(string tableName)
        {
            return connection.Query<long>($"SELECT Position from {tableName}").First();
        }

        public void MovePosition(long position, string tableName)
        {
            connection.Execute($"UPDATE {tableName} SET Position={position}");
        }

        public List<OutboxItem> GetOutboxItemsFromPosition(long position,
            string tableName)
        {
            return connection.Query<OutboxItem>($"SELECT * from {tableName} WHERE Id > {position}").ToList();
        }
    }
}