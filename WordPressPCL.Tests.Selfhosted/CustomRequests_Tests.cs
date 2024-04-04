﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WordPressPCL.Tests.Selfhosted.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class CustomRequests_Tests
{
    //Requires contact-form-7 plugin
    private class ContactFormItem
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("slug")]
        public string Slug { get; set; }
        [JsonProperty("locale")]
        public string Locale { get; set; }
    }

    private static WordPressClient _clientAuth;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task CustomRequests_Read()
    {
        List<ContactFormItem> forms = await _clientAuth.CustomRequest.GetAsync<List<ContactFormItem>>("contact-form-7/v1/contact-forms", false, true);
        Assert.IsNotNull(forms);
        Assert.AreNotEqual(forms.Count, 0);
    }

    // TODO: check why this isn't returning the form
    //[TestMethod]
    public async Task CustomRequests_Create()
    {
        string title = $"Test Form {Guid.NewGuid()}";
        ContactFormItem form = await _clientAuth.CustomRequest.CreateAsync<ContactFormItem, ContactFormItem>("contact-form-7/v1/contact-forms", new ContactFormItem() { Title = title, Locale = "en-US" });
        Assert.IsNotNull(form);
        Assert.IsNotNull(form.Id);
        Assert.AreEqual(form.Title, title);
    }

    // TODO: make test not depend on other tests
    //[TestMethod]
    public async Task CustomRequests_Update()
    {
        string title = $"Test Form {Guid.NewGuid()}";
        ContactFormItem form = await _clientAuth.CustomRequest.CreateAsync<ContactFormItem, ContactFormItem>("contact-form-7/v1/contact-forms", new ContactFormItem() { Title = title, Locale = "en-US" });

        List<ContactFormItem> forms = await _clientAuth.CustomRequest.GetAsync<List<ContactFormItem>>("contact-form-7/v1/contact-forms", false, true);
        Assert.IsNotNull(forms);
        Assert.AreNotEqual(forms.Count, 0);
        ContactFormItem editform = forms.First();
        editform.Title += "test";
        ContactFormItem form2 = await _clientAuth.CustomRequest.UpdateAsync<ContactFormItem, ContactFormItem>($"contact-form-7/v1/contact-forms/{editform.Id.Value}", editform);
        Assert.IsNotNull(form2);
        Assert.AreEqual(form.Title, editform.Title);
    }

    // TODO: make test not depend on other tests
    //[TestMethod]
    public async Task CustomRequests_Delete()
    {
        List<ContactFormItem> forms = await _clientAuth.CustomRequest.GetAsync<List<ContactFormItem>>("contact-form-7/v1/contact-forms", false, true);
        Assert.IsNotNull(forms);
        Assert.AreNotEqual(forms.Count, 0);
        ContactFormItem deleteform = forms.First();
        bool result = await _clientAuth.CustomRequest.DeleteAsync($"contact-form-7/v1/contact-forms/{deleteform.Id.Value}");
        Assert.IsTrue(result);
    }
}
