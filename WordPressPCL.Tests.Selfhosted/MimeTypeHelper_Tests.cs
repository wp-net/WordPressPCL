using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Utility;

namespace WordPressPCL.Tests.Selfhosted {
    
    [TestClass]
    public class MimeTypeHelper_Tests {

        [TestMethod]
        public void MimeType_Defaults_To_Application_Octet_Stream_For_Unknown_Extension() {
            const string unknownExtension = "unknown";
            const string expectedMimeType = "application/octet-stream";

            var resultMimeType = MimeTypeHelper.GetMIMETypeFromExtension(unknownExtension);

            Assert.AreEqual(expectedMimeType, resultMimeType);
        }

    }
    
}