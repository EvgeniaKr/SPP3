using DirectoryScanner.Core;
namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestNumberOfFilesInDirectory()
        {
            var directoryTracer = new Tracer();
            directoryTracer.Start("C:\\mywork\\VT");
            while (directoryTracer.queue.IsWorking != 3) ;

            Assert.AreEqual(directoryTracer.Files.Count, 1); //первая вложенность
            DirectoryScanner.Core.FilesParametrs file;
            directoryTracer.Files.TryGetValue("C:\\mywork\\VT", out file);

            Assert.IsNotNull(file);
            Assert.AreEqual(file.Files.Count, 5);

        }

        [TestMethod]
        public void TestNumberOfFilesInDirectory2()
        {
            var directoryTracer = new Tracer();
            directoryTracer.Start("C:\\mywork\\VT");
            while (directoryTracer.queue.IsWorking != 3) ;

            Assert.AreEqual(directoryTracer.Files.Count, 1); //первая вложенность
            DirectoryScanner.Core.FilesParametrs file;
            directoryTracer.Files.TryGetValue("C:\\mywork\\VT", out file);

            Assert.IsNotNull(file);
            Assert.AreEqual(file.Files.Count, 5);

            file.Files.TryGetValue("C:\\mywork\\VT\\l1", out file);

            Assert.IsNotNull(file);
            Assert.AreEqual(file.Files.Count, 5);

            file.Files.TryGetValue("C:\\mywork\\VT\\l1\\out", out file);

            Assert.IsNotNull(file);
            Assert.AreEqual(file.Files.Count, 2);

        }

        [TestMethod]
        public void TestNumberOfFilesInDirectory3()
        {
            var directoryTracer = new Tracer();
            directoryTracer.Start("C:\\mywork\\VT");
            Thread.Sleep(6000);

            Assert.AreEqual(directoryTracer.Files.Count, 1); //первая вложенность
            DirectoryScanner.Core.FilesParametrs file;
            directoryTracer.Files.TryGetValue("C:\\mywork\\VT", out file);

            Assert.IsNotNull(file);
            Assert.AreEqual(file.Files.Count, 5);

            file.Files.TryGetValue("C:\\mywork\\VT\\l1", out file);

            Assert.IsNotNull(file);
            Assert.AreEqual(file.Files.Count, 5);

        }
    }
}