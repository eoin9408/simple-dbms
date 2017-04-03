using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using simple_dbms.Data.Server;
using Ploeh.AutoFixture;
using NSubstitute;
using System.Data;
using System.Linq;

namespace simple_dbms.Data.Tests.Server
{
    [TestClass]
    public class When_querying_server
    {
        private DbProviderFactory provider;
        private string connectionString;

        private DbConnection connection;
        private DbCommand command;
        private DbDataAdapter dataAdapter;

        private ServerInstance target;
        private Fixture fixture;

        [TestInitialize]
        public void Initialise()
        {
            fixture = new Fixture();

            provider = Substitute.For<DbProviderFactory>();
            connection = Substitute.For<DbConnection>();
            command = Substitute.For<DbCommand>();
            dataAdapter = Substitute.For<DbDataAdapter>();

            provider.CreateConnection().Returns(connection);
            provider.CreateCommand().Returns(command);
            provider.CreateDataAdapter().Returns(dataAdapter);

            connectionString = fixture.Create<string>();
            target = new ServerInstance(provider, connectionString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Should_throw_exception_if_query_is_null()
        {
            target.Select(null);
        }

        [TestMethod]
        public void Should_create_connection_on_first_use_but_reuse_on_second()
        {
            target.Select("SELECT test FROM sys.hello");
            provider.Received(1).CreateConnection();

            target.Select("SELECT a, b FROM sys.asdf");
            provider.Received(1).CreateConnection();
        }

        [TestMethod]
        public void Should_open_connection_and_submit_query()
        {
            var commandText = "SELECT * FROM sys.databases";
            target.Select(commandText);

            connection.Received(1).Open();
            command.Received(1).CommandText = commandText;
            command.Received(1).CommandType = CommandType.Text;
            command.Received(1).Connection = connection;

            dataAdapter.Received(1).SelectCommand = command;
            dataAdapter.Received(1).Fill(Arg.Any<DataTable>());
        }

        [TestMethod]
        public void Should_return_data_returned_by_data_adapter()
        {
            var commandText = "SELECT * FROM sys.databases";
            var data = fixture.CreateMany<int>(10).Cast<object>().ToArray();

            var reader = Substitute.For<DbDataReader>();
            
            command.ExecuteReader().Returns(reader);

            dataAdapter.Fill(Arg.Do<DataTable>(x => x.LoadDataRow(data, true)));

            var results = target.Select(commandText);

            Assert.AreEqual(1, results.Rows.Count);
            Assert.AreEqual(data, results.Rows[0].ItemArray);
        }
    }
}
