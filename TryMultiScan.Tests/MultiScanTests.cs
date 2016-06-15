using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using Dapper;

namespace TryMultiScan.Tests
{
    [TestClass]
    public class MultiScanTests
    {
        private string _connectionString = @"data source=(localdb)\mssqllocaldb;initial catalog=TryMultiScan.MultiScanContext;integrated security=True";

        [TestMethod]
        public void TestMethod1()
        {
            // arrange
            int expected;
            using (var ctx = new MultiScanContext())
            {
                var post = ctx.Posts.First();
                expected = post.ViewCount + 2;
            }

            // act
            Task task1 = Task.Factory.StartNew(() => ViewPost());
            Task task2 = Task.Factory.StartNew(() => ViewPost());
            Task.WaitAll(task1, task2);

            // assert
            using (var ctx = new MultiScanContext())
            {
                var post = ctx.Posts.First();
                Assert.AreEqual(expected, post.ViewCount);
            }
        }

        // using ef
        private void ViewPost0()
        {
            using (var ctx = new MultiScanContext())
            {
                var post = ctx.Posts.First();
                post.ViewCount++;
                ctx.SaveChanges();
            }
        }

        // using ef w/ trans
        private void ViewPost()
        {
            var filename = Guid.NewGuid().ToString() + ".txt";
            using (var ctx = new MultiScanContext())
            {
                ctx.Database.Log = (s) =>
                {
                    File.AppendAllText(filename, s);
                };

                using (var trans = ctx.Database.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        var post = ctx.Posts.First();
                        post.ViewCount++;
                        ctx.SaveChanges();
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        // using dapper
        private void ViewPost2()
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                var post = cn.Query<Post>("SELECT * FROM Posts WHERE PostId = 1").Single();
                cn.Execute("UPDATE Posts SET ViewCount = @ViewCount WHERE PostId = @PostId",
                    new { PostId = 1, ViewCount = post.ViewCount + 1 });
            }
        }

        // using dapper w/ trans
        private void ViewPost3()
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                cn.Open();
                using (var trans = cn.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        var post = cn.Query<Post>("SELECT * FROM Posts WHERE PostId = 1", transaction: trans).Single();
                        cn.Execute("UPDATE Posts SET ViewCount = @ViewCount WHERE PostId = @PostId",
                            new { PostId = 1, ViewCount = post.ViewCount + 1 }, transaction: trans);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }


    }
}
