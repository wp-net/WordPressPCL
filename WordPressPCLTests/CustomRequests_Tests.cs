using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordPressPCLTests.Utility;

namespace WordPressPCLTests
{
    [TestClass]
    public class CustomRequests_Tests
    {
        //Requires contact-form-7 plugin
        private class ContactFormItem
        {
            public int? id;
            public string title;
            public string slug;
            public string locale;
        }

        [TestMethod]
        public async Task CustomRequests_Read()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var forms = await client.CustomRequest.Get<IEnumerable<ContactFormItem>>("contact-form-7/v1/contact-forms", false, true);
            Assert.IsNotNull(forms);
            Assert.AreNotEqual(forms.Count(), 0);
        }

        [TestMethod]
        public async Task CustomRequests_Create()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var form = await client.CustomRequest.Create<ContactFormItem, ContactFormItem>("contact-form-7/v1/contact-forms", new ContactFormItem() { title = "test" });
            Assert.IsNotNull(form);
            Assert.AreEqual(form.title, "test");
        }

        [TestMethod]
        public async Task CustomRequests_Update()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var forms = await client.CustomRequest.Get<IEnumerable<ContactFormItem>>("contact-form-7/v1/contact-forms", false, true);
            Assert.IsNotNull(forms);
            Assert.AreNotEqual(forms.Count(), 0);
            var editform = forms.First();
            editform.title += "test";
            var form = await client.CustomRequest.Update<ContactFormItem, ContactFormItem>($"contact-form-7/v1/contact-forms/{editform.id.Value}", editform);
            Assert.IsNotNull(form);
            Assert.AreEqual(form.title, editform.title);
        }

        [TestMethod]
        public async Task CustomRequests_Delete()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var forms = await client.CustomRequest.Get<IEnumerable<ContactFormItem>>("contact-form-7/v1/contact-forms", false, true);
            Assert.IsNotNull(forms);
            Assert.AreNotEqual(forms.Count(), 0);
            var deleteform = forms.First();
            var result = await client.CustomRequest.Delete($"contact-form-7/v1/contact-forms/{deleteform.id.Value}");
            Assert.IsTrue(result.IsSuccessStatusCode);
        }
    }
}