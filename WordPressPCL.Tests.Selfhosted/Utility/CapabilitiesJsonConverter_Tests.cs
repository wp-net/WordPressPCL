using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WordPressPCL.Utility;

namespace WordPressPCL.Tests.Selfhosted.Utility
{
    [TestClass]
    public class CapabilitiesJsonConverter_Tests
    {
        private const string testData1 = "{    \"switch_themes\": true,    \"edit_themes\": true,    \"activate_plugins\": true,    \"edit_plugins\": true,    \"edit_users\": true,    \"edit_files\": true,    \"manage_options\": true,    \"moderate_comments\": true,    \"manage_categories\": true,    \"manage_links\": true,    \"upload_files\": true,    \"import\": true,    \"unfiltered_html\": true,    \"edit_posts\": true,    \"edit_others_posts\": true,    \"edit_published_posts\": true,    \"publish_posts\": true,    \"edit_pages\": true,    \"read\": true,    \"level_10\": true,    \"level_9\": true,    \"level_8\": true,    \"level_7\": true,    \"level_6\": true,    \"level_5\": true,    \"level_4\": true,    \"level_3\": true,    \"level_2\": true,    \"level_1\": true,    \"level_0\": true,    \"edit_others_pages\": true,    \"edit_published_pages\": true,    \"publish_pages\": true,    \"delete_pages\": true,    \"delete_others_pages\": true,    \"delete_published_pages\": true,    \"delete_posts\": true,    \"delete_others_posts\": true,    \"delete_published_posts\": true,    \"delete_private_posts\": true,    \"edit_private_posts\": true,    \"read_private_posts\": true,    \"delete_private_pages\": true,    \"edit_private_pages\": true,    \"read_private_pages\": true,    \"delete_users\": true,    \"create_users\": true,    \"unfiltered_upload\": true,    \"edit_dashboard\": true,    \"update_plugins\": true,    \"delete_plugins\": true,    \"install_plugins\": true,    \"update_themes\": true,    \"install_themes\": true,    \"update_core\": true,    \"list_users\": true,    \"remove_users\": true,    \"promote_users\": true,    \"edit_theme_options\": true,    \"delete_themes\": true,    \"export\": true,    \"manage_polls\": true,    \"NextGEN Manage tags\": true,    \"NextGEN Manage others gallery\": true,    \"MailPress_edit_dashboard\": true,    \"MailPress_manage_options\": true,    \"MailPress_edit_mails\": true,    \"MailPress_edit_others_mails\": true,    \"MailPress_send_mails\": true,    \"MailPress_delete_mails\": true,    \"MailPress_archive_mails\": true,    \"MailPress_mail_custom_fields\": true,    \"MailPress_switch_themes\": true,    \"MailPress_edit_users\": true,    \"MailPress_delete_users\": true,    \"MailPress_user_custom_fields\": true,    \"MailPress_manage_addons\": true,    \"MailPress_manage_autoresponders\": true,    \"MailPress_manage_newsletters\": true,    \"MailPress_test_newsletters\": true,    \"picasa_dialog\": true,    \"wysija_newsletters\": true,    \"wysija_subscribers\": true,    \"wysija_config\": true,    \"wysija_theme_tab\": true,    \"wysija_style_tab\": true,    \"wysija_stats_dashboard\": true,    \"aiosp_manage_seo\": true,    \"see_snap_box\": true,    \"make_snap_posts\": true,    \"wpseo_bulk_edit\": true,    \"wpseo_manage_options\": true,    \"haveown_snap_accss\": true,    \"administrator\": \"1\"  }";
        private const string badTestData1 = "{    \"switch_themes\": \"lalala\",    \"edit_themes\": true,    \"activate_plugins\": true,    \"edit_plugins\": true,    \"edit_users\": true,    \"edit_files\": true,    \"manage_options\": true,    \"moderate_comments\": true,    \"manage_categories\": true,    \"manage_links\": true,    \"upload_files\": true,    \"import\": true,    \"unfiltered_html\": true,    \"edit_posts\": true,    \"edit_others_posts\": true,    \"edit_published_posts\": true,    \"publish_posts\": true,    \"edit_pages\": true,    \"read\": true,    \"level_10\": true,    \"level_9\": true,    \"level_8\": true,    \"level_7\": true,    \"level_6\": true,    \"level_5\": true,    \"level_4\": true,    \"level_3\": true,    \"level_2\": true,    \"level_1\": true,    \"level_0\": true,    \"edit_others_pages\": true,    \"edit_published_pages\": true,    \"publish_pages\": true,    \"delete_pages\": true,    \"delete_others_pages\": true,    \"delete_published_pages\": true,    \"delete_posts\": true,    \"delete_others_posts\": true,    \"delete_published_posts\": true,    \"delete_private_posts\": true,    \"edit_private_posts\": true,    \"read_private_posts\": true,    \"delete_private_pages\": true,    \"edit_private_pages\": true,    \"read_private_pages\": true,    \"delete_users\": true,    \"create_users\": true,    \"unfiltered_upload\": true,    \"edit_dashboard\": true,    \"update_plugins\": true,    \"delete_plugins\": true,    \"install_plugins\": true,    \"update_themes\": true,    \"install_themes\": true,    \"update_core\": true,    \"list_users\": true,    \"remove_users\": true,    \"promote_users\": true,    \"edit_theme_options\": true,    \"delete_themes\": true,    \"export\": true,    \"manage_polls\": true,    \"NextGEN Manage tags\": true,    \"NextGEN Manage others gallery\": true,    \"MailPress_edit_dashboard\": true,    \"MailPress_manage_options\": true,    \"MailPress_edit_mails\": true,    \"MailPress_edit_others_mails\": true,    \"MailPress_send_mails\": true,    \"MailPress_delete_mails\": true,    \"MailPress_archive_mails\": true,    \"MailPress_mail_custom_fields\": true,    \"MailPress_switch_themes\": true,    \"MailPress_edit_users\": true,    \"MailPress_delete_users\": true,    \"MailPress_user_custom_fields\": true,    \"MailPress_manage_addons\": true,    \"MailPress_manage_autoresponders\": true,    \"MailPress_manage_newsletters\": true,    \"MailPress_test_newsletters\": true,    \"picasa_dialog\": true,    \"wysija_newsletters\": true,    \"wysija_subscribers\": true,    \"wysija_config\": true,    \"wysija_theme_tab\": true,    \"wysija_style_tab\": true,    \"wysija_stats_dashboard\": true,    \"aiosp_manage_seo\": true,    \"see_snap_box\": true,    \"make_snap_posts\": true,    \"wpseo_bulk_edit\": true,    \"wpseo_manage_options\": true,    \"haveown_snap_accss\": true,    \"administrator\": \"1\"  }";

        [TestMethod]
        public void CapabilitiesJsonConverterRead_Test()
        {
            var result = JsonConvert.DeserializeObject<Dictionary<string, bool>>(testData1, new CustomCapabilitiesJsonConverter());
            Assert.IsNotNull(result);
            Assert.IsTrue(result["administrator"]);
            Assert.IsTrue(result["level_0"]);
        }

        [TestMethod]
        public void MUST_FAIL_CapabilitiesJsonConverterRead_Test()
        {
            Assert.ThrowsException<FormatException>(() => JsonConvert.DeserializeObject<Dictionary<string, bool>>(badTestData1, new CustomCapabilitiesJsonConverter()));
        }

        [TestMethod]
        public void CapabilitiesJsonConverterWrite_Test()
        {
            var data = new Dictionary<string, bool>() {
                { "administrator", true },
                { "level_0", true }
            };
            var result = JsonConvert.SerializeObject(data, new CustomCapabilitiesJsonConverter());
            Assert.AreEqual("{\"administrator\":true,\"level_0\":true}", result);
        }
    }
}